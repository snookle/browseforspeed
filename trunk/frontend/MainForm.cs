// Copyright (C) 2006 Richard Nelson, Ben Kenny, Philip Nelson
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.


using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using libbrowseforspeed;
using System.Threading;
using System.Globalization;
using System.Resources;
using Microsoft.Win32;
using System.Xml;

namespace BrowseForSpeed.Frontend
{
	
public class ListSorter: IComparer<ServerListItem>
{
	public SortOrder order;
	public SortType sortType;
	private CaseInsensitiveComparer ObjectCompare;

	public ListSorter()
	{
		order = SortOrder.None;
		sortType = SortType.Ping;
		ObjectCompare = new CaseInsensitiveComparer();
	}

	public int Compare(ServerListItem itemX, ServerListItem itemY)
	{
		int compareResult = 0;
			switch (sortType) {
				case SortType.Hostname:
					compareResult = ObjectCompare.Compare(itemX.hostname, itemY.hostname); break;
				case SortType.Ping:
					compareResult = ObjectCompare.Compare(Convert.ToInt32(itemX.ping), Convert.ToInt32(itemY.ping)); break;
				case SortType.Players:
					compareResult = ObjectCompare.Compare(Convert.ToInt32(itemX.players), Convert.ToInt32(itemY.players)); break;
				case SortType.Track:
					compareResult = ObjectCompare.Compare(itemX.track, itemY.track); break;
			}
			
		if (order == SortOrder.Ascending) {
			return compareResult;
		}
		else if (order == SortOrder.Descending) {
			return (-compareResult);
		}
		return 0;
	}
}

	
	public enum QueryType{Main, Favorite, Friend}
	public enum SortType{Hostname, Ping, Players, Track};
	public enum FilterType{None, Track, Ping, Cars, Empty, Full, Private, Public};
	public struct Filter {
		public FilterType type;
		public Object value;
		public Filter(Object v){
			type = FilterType.None;
			value = null;
		}
	}
	
	public class ServerListView : ListView
	{
		protected List<ServerListItem> serverList = new List<ServerListItem>();
		protected ListSorter sorter = new ListSorter();
		public void AddServer(ServerInformation info)
		{
			ServerListItem item = new ServerListItem(info);
			//iterate through the list, and see if we have a match.
			for (int i = 0; i < serverList.Count; ++i){
				if (serverList[i].host == item.host) {
					serverList[i] = item;
					Display(item).Tag = i;
					return;
				}
			}
			item.filtered = false;
			serverList.Add(item);
			if (this is MainListView)
				FilterServer(item);
			Display(item);
			this.Sort();
		}
		public ServerListItem GetSelectedServer()
		{
			if (SelectedItems.Count > 0) {
				int index = (int)SelectedItems[0].Tag;
				if (index == -1)
					return null;				
				ServerListItem item = serverList[index];
				item.index = SelectedItems[0].Index;
				return item;
			}
			else
				return null;
		}
		public void RemoveServer(ServerListItem item)
		{
			Items.RemoveAt(item.index);
			serverList.Remove(item);
			for (int i = 0; i < this.Items.Count; ++i){
				if ((int)Items[i].Tag > item.index){
					int tmp = (int)(Items[i].Tag); Items[i].Tag = --tmp;
				}
			}
		}
		public void ClearServers()
		{
			serverList.Clear();
			serverList.TrimExcess();
			this.Items.Clear();
		}
		public void DisplayAll()
		{
			Items.Clear();
			foreach(ServerListItem item in serverList.FindAll(ServersNotFiltered)){
				Display(item);
			}
			Sort();

		}
		public List<ServerListItem> AllServers()
		{
			return serverList;
		}
		private static bool ServersNotFiltered(ServerListItem info)
		{
			return !info.filtered;
		}

		public ListViewItem Display(ServerListItem item)
		{
			if (item.filtered)
				return new ListViewItem();
			try {
				ListViewItem lvi;
				lvi = this.Items.Add(item.host.ToString());
				lvi.Tag = serverList.IndexOf(item);
				item.hostname = LFSQuery.removeColourCodes(item.hostname);
				lvi.SubItems.Insert(0, new ListViewItem.ListViewSubItem(lvi, item.hostname));
				string cars = MainForm.CarsToString(LFSQuery.getCarNames(item.cars));
				string rules = MainForm.RulesToString(item.rules);
				lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, item.ping.ToString()));
				int columnOffset = 0;
				if (this is FavouriteListView){
					columnOffset = 1;
				} else {
					lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, item.passworded == true ? MainForm.languages.GetString("Global.Yes") : MainForm.languages.GetString("Global.No")));
				}
				lvi.SubItems.Insert(3 - columnOffset, new ListViewItem.ListViewSubItem(lvi, item.players.ToString() +"/" + item.slots.ToString()));
				lvi.SubItems.Insert(4 - columnOffset, new ListViewItem.ListViewSubItem(lvi, rules));
				lvi.SubItems.Insert(5 - columnOffset, new ListViewItem.ListViewSubItem(lvi, item.track));			
				lvi.SubItems.Insert(6 - columnOffset, new ListViewItem.ListViewSubItem(lvi, cars));
				return lvi;
			}catch (Exception ex) {
				return null;
			}
		}
		private Filter[] filters = new Filter[10];
		public void Filter(FilterType filter, Object value)
		{
			filters[(int)filter].type = filter;
			filters[(int)filter].value = value;
			foreach(ServerListItem item in serverList){
				FilterServer(item);
			}
		}
		private void FilterServer(ServerListItem item)
		{
			item.filtered = false;
			foreach (Filter f in filters){
				if (f.type == FilterType.None)
					continue;
				switch (f.type){
						case FilterType.Track: item.filtered = item.filtered || !(item.track.Contains((string)f.value)); break;
						case FilterType.Empty: item.filtered = (item.filtered || ((item.players == 0) && !(bool)f.value)); break;
						case FilterType.Full : item.filtered = (item.filtered || ((item.players == item.slots) && !(bool)f.value)); break;
						case FilterType.Private : item.filtered = (item.filtered || ((item.passworded == true) && !(bool)f.value)); break;
						case FilterType.Public : item.filtered = (item.filtered || ((item.passworded == false) && !(bool)f.value)); break;
						case FilterType.Ping : item.filtered = item.filtered || (item.ping > (int)f.value); break;
						case FilterType.Cars :
							if (item.filtered) break;
							foreach (CheckBox car in (CheckBox[])(f.value)) {
								ulong i = LFSQuery.CAR_BITS[(int)car.Tag] & item.cars;
								if ((i != 0 && car.CheckState == CheckState.Unchecked) ||
								    (i == 0 && car.CheckState == CheckState.Checked)) {
									item.filtered = true;
								}
							}
							break;
				}
			}
		}
	}
	public class FavouriteListView : ServerListView
	{
		public ServerInformation[] GetAllHosts()
		{
			return serverList.ToArray();
		}
	}
	public class MainListView : ServerListView{}
	
	
	public class ServerListItem : ServerInformation
	{
		public bool filtered;
		public int index;
		public ServerListItem(ServerInformation info)
		{
			this.cars = info.cars;
			this.connectFailed = info.connectFailed;
			this.filtered = false;
			this.host = info.host;
			this.hostname = info.hostname;
			this.password = info.password;
			this.passworded = info.passworded;
			this.ping = info.ping;
			this.players = info.players;
			this.racerNames = info.racerNames;
			this.readFailed = info.readFailed;
			this.rules = info.rules;
			this.slots = info.slots;
			this.success = info.success;
			this.totalServers = info.totalServers;
			this.track = info.track;
			this.version = info.version;
			this.adminPassword = info.adminPassword;
			this.insimPort = info.insimPort;
		}
	}
	
	public class FriendListItem
	{
		public ServerInformation server;
		public string name;
	}
	
	public partial class MainForm
	{
		static string bfs_version = "0.6 ALPHA";
		static string version_check = "7";
		static string download_url = "http://www.browseforspeed.net";
		static string version_check_url = "http://www.browseforspeed.net/versioncheck.pl";
		
		public static String appTitle = "Browse For Speed";
		static String configFilename = Application.StartupPath + "\\config.cfg";
		static String configXMLFilename = Application.StartupPath + "\\config.xml";
		static String favFilename = Application.StartupPath + "\\favourite.servers";
		static String favXMLFilename = Application.StartupPath + "\\favourites.xml";
		static String friendFilename = Application.StartupPath + "\\friends.xml"; 

		private CheckBox[] cars;
		private Button[] groups;

		private LFSQuery q;
		private ListViewColumnSorter lvwColumnSorter;

		private List<FriendListItem> friendList;

		private int totalServers;
		private int numQueried;
		private int numServersDone;
		private int numServersRefused;
		private int numServersNoReply;

		private int lastTabSelected;

		private Configuration config;
		private ServerInformationForm s;

		private bool exiting;
		private bool refreshing;

		private Thread t;
		
		public static LanguageManager languages = new LanguageManager(Application.StartupPath + "\\lang");


		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		public MainForm()
		{
			InitializeComponent();
			this.Icon = new Icon(GetType().Assembly.GetManifestResourceStream("BrowseForSpeed.ca3r.ico"));
		}

		public static string CarsToString(Array cars) {
			StringBuilder carNames = new StringBuilder();
			foreach (string car in cars) {
				carNames.Append(car + ", ");
			}
			if (carNames.Length == 0) {
				return carNames.ToString();
			}
			return carNames.Remove(carNames.Length - 2, 2).ToString();			
		}
		
		
		
		public static String RulesToString(ulong rules)
		{
			String str = "";
			if ((rules & 1) != 0) str += "V"; //CAN_VOTE
			if ((rules & 2) != 0) str += "S"; //CAN_SELECT
			if ((rules & 4) != 0) str += "Q"; //QUALIFY
			if ((rules & 8) != 0) str += "P"; //PRIVATE - apparently this is wrong??
			if ((rules & 16) != 0) str += "m"; //MODIFIED (dunno what to put here!)
			if ((rules & 32) != 0) str += "M"; //MIDRACEJOIN
			if ((rules & 64) != 0) str += "p"; //MUSTPIT
			return str;
		}
		
		void CodeCars(out ulong compulsory, out ulong illegal) {
			compulsory = 0;
			illegal = 0;
			for (int i = 0; i < LFSQuery.CAR_BITS.Length; ++i) {
				if (cars[i].CheckState == CheckState.Checked) {
					compulsory ^= LFSQuery.CAR_BITS[i];
				} else if (cars[i].CheckState == CheckState.Unchecked) {
					illegal ^= LFSQuery.CAR_BITS[i];
				}
			}
		}

		//nice helper method to set UI properties across threads.
		delegate void SetValueDelegate(Object obj, Object val, Object[] index);

		public void SetControlProperty(Control ctrl, String propName, Object val)
		{
			System.Reflection.PropertyInfo propInfo = ctrl.GetType().GetProperty(propName);
			Delegate dgtSetValue = new SetValueDelegate(propInfo.SetValue);
      		ctrl.Invoke(dgtSetValue, new Object[3] { ctrl, val, /*index*/null });
		}
		
		byte CodeFilters()
		{
			byte filters = 0x00;
			filters ^= (byte)(!cbPrivate.Checked ? (byte)(LFSQuery.msFilters["Private"]) : (byte)0x00);
			filters ^= (byte)(!cbPublic.Checked ? (byte)(LFSQuery.msFilters["Public"]) : (byte)0x00);
			filters ^= (byte)(!cbEmpty.Checked ? (byte)(LFSQuery.msFilters["Empty"]) : (byte)0x00);
            filters ^= (byte)(!cbFull.Checked ? (byte)(LFSQuery.msFilters["Full"]) : (byte)0x00);
			return filters;
		}
		
		void MakeQuery(bool isFav)
		{
			SetControlProperty(buttonRefreshFav, "Text", languages.GetString("MainForm.btnStop"));
			SetControlProperty(btnRefreshMain, "Text", languages.GetString("MainForm.btnStop"));
			refreshing = true;
			try {
				if (isFav) {
					q.query(0, 0, "browseforspeed", lvFavourites.GetAllHosts(), 1);
				} else {
					ulong compulsory;
					ulong illegal;
					CodeCars(out compulsory, out illegal);
					q.query(compulsory, illegal, "browseforspeed", 0, CodeFilters(), version);
				}
			} catch(Exception e) {
				MessageBox.Show(languages.GetString("MasterServerError") , languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

			if (exiting) return;
			if (totalServers == 0) {
				statusTotal.Text = languages.GetString("MainForm.NoServers");
			}
			refreshing = false;
			SetControlProperty(btnRefreshMain, "Enabled", true);
			SetControlProperty(buttonRefreshFav, "Enabled", true);
			SetControlProperty(buttonRefreshFav, "Text", languages.GetString("MainForm.btnRefresh"));
			SetControlProperty(btnRefreshMain, "Text", languages.GetString("MainForm.btnRefresh"));
		}

		void MainQuery()
		{
			MakeQuery(false);
		}
		
		void FavQuery()
		{
			MakeQuery(true);
		}

		void RefreshButtonClick(object sender, System.EventArgs e)
		{
			Button b = (Button)sender;
			if (!refreshing){
				LFSQuery.queried -= new ServerQueried(queryMainEventListener);
				LFSQuery.queried += new ServerQueried(queryMainEventListener);
				totalServers = 0;
				btnJoinMain.Enabled = false;
				if (b.Name == "btnRefreshMain") {
					t = new Thread(new ThreadStart(MainQuery));
					lvMain.ClearServers();
				}
				else {
					lvFavourites.Items.Clear();
					t = new Thread(new ThreadStart(FavQuery));
				}
				statusTotal.Text = languages.GetString("MainForm.QueryStatus");
				statusNoReply.Text = "";
				statusRefused.Text = "";
  				t.Start();
			} else {
				LFSQuery.stopQuerying();
				btnRefreshMain.Enabled = false;
				buttonRefreshFav.Enabled = false;
			}
		}

		public void AddServerToList(ServerInformation info, ServerListView list)
		{
			if (this.exiting) return;
			try{
				if(this.InvokeRequired == false) {
					if (totalServers == 0) {
						totalServers = info.totalServers;
						numQueried = 0;
						numServersDone = 0;
						numServersRefused = 0;
						numServersNoReply = 0;
					}
					numQueried++;
					if (info.success) {
						numServersDone++;
						list.AddServer(info);
					} else {
						if (list.Name == "lvFavourites") {
							info.ping = 9999;
							info.track = "N/A";
							list.AddServer(info);
						}
						if (info.readFailed) {
							numServersNoReply++;
						} else {
							numServersRefused++;
						}
					}
					statusTotal.Text = String.Format(languages.GetString("MainForm.StatusTotal"), numQueried, totalServers);
					statusNoReply.Text = String.Format(languages.GetString("MainForm.NoReply"), numServersNoReply);
					statusRefused.Text = String.Format(languages.GetString("MainForm.Refused"), numServersRefused);
				} else {
			    	AddServerDelegate addServer = new AddServerDelegate(AddServerToList);
			    	this.BeginInvoke(addServer, new object[] {info, list});
				}
			} catch(Exception e){ MessageBox.Show(e.Message + e.StackTrace, languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
  		}

		delegate void AddServerDelegate(ServerInformation info, ServerListView list);

		public void queryMainEventListener(object o, ServerInformation info, object cbObj) {
			if (info != null) {
				if ((int)cbObj == 0)
					AddServerToList(info, lvMain);
				else if ((int)cbObj == 1)
					AddServerToList(info, lvFavourites);
			}
		}

		void CloseToolStripMenuItemClick(object sender, System.EventArgs e) {
			Close();
		}

		void lvMainColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{                     
			if (e.Column == lvwColumnSorter.SortColumn) {
				if (lvwColumnSorter.Order == SortOrder.Ascending)
					lvwColumnSorter.Order = SortOrder.Descending;
				 else
					lvwColumnSorter.Order = SortOrder.Ascending;
			} else {
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}
			((ListView)sender).Sort();
		}
		
		
		Process[] prestart;
		FormWindowState ws;		
		
		private void KillPreStartPrograms()
		{
			foreach (Process p in prestart){
				if (p != null && !p.HasExited)
					p.Kill();
			}
			
		}
		
		private void LFSExit(object sender, EventArgs e)
		{
			KillPreStartPrograms();
			this.WindowState = ws;
		}
		
			
		void LoadLFS(String hostName, String mode, String password)
		{
			String lfsPath = config.lfsPath;
			ws= this.WindowState;
			LFSQuery.stopQuerying();
			prestart = new Process[config.psp.Count];
			for(int i = 0; i < config.psp.Count; i++){
					if (config.psp[i].enabled == false)
						continue;
				try {
					prestart[i] = new Process();
					prestart[i].StartInfo.FileName = config.psp[i].path;
					prestart[i].StartInfo.WorkingDirectory = Path.GetDirectoryName(config.psp[i].path);
					prestart[i].StartInfo.Arguments = config.psp[i].options;
					prestart[i].Start();
				} catch (Exception ex) {
					string message = String.Format(languages.GetString("StartPSPError"), config.psp[i].name, config.psp[i].path, ex.Message);
					MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			try{
				this.WindowState = FormWindowState.Minimized;
				Process lfs = new Process();
				lfs.Exited += new EventHandler(LFSExit);
				lfs.EnableRaisingEvents = true;
				lfs.StartInfo.FileName = lfsPath;
				lfs.StartInfo.WorkingDirectory = Path.GetDirectoryName(lfsPath);
				lfs.StartInfo.Arguments = "/join=" + hostName + " /mode=" + mode + " /pass=" + password + "/insim=" + config.insimPort.ToString();
				lfs.Start();
			} catch (Exception ex) {
				this.WindowState = ws;
				KillPreStartPrograms();
				string message = string.Format(languages.GetString("StartLFSError"), ex.Message);
				MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void btnBrowseClick(object sender, System.EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.Cancel)
				return;
			pathList.Items.Add(openFileDialog.FileName);
			pathList.SelectedIndex = pathList.Items.Count - 1;
		}

		void listDblClick(object sender, System.EventArgs e) {
			if (config.joinOnClick) {
				btnJoinClick(sender, e);
			} else {
				ViewServerInformationToolStripMenuItemClick(sender, e);
			}
		}

		void btnJoinClick(object sender, System.EventArgs e) {
			ServerListItem item;
			string pass = "";
			bool writefav = false;
			if (sender is Button) {
				if (((Button)sender).Name == "btnJoinMain") {
					item = lvMain.GetSelectedServer();
				} else { //is this even possible?
					item = lvFavourites.GetSelectedServer();
					pass = item.password;
					//writefav = true; WHY?!?
				}
			} else if (sender is ToolStripMenuItem) {
				if (((ToolStripMenuItem)sender).Name == "joinServerToolStripMenuItem") {
					item = lvMain.GetSelectedServer();
					pass = edtPasswordMain.Text;
				} else {
					item = lvFavourites.GetSelectedServer();
					pass = item.password;
					//writefav = true;
				}
			} else {
				item = ((ServerListView)sender).GetSelectedServer();
				if (item.password != null && item.password != "") {
					pass = item.password;
				} else {
					pass = edtPasswordMain.Text;
				}
			}
			if (item == null)
				return;
			if (writefav) { //why would anyone put the password in there?
				lvFavourites.GetSelectedServer().password = edtPasswordMain.Text;
				WriteFav();
			}
			LoadLFS(item.hostname, VersionToString(item.version), pass);
		}

		void lvMainSelectedIndexChanged(object sender, System.EventArgs e)
		{
			btnJoinMain.Enabled = (lvMain.GetSelectedServer() != null);
		}

		void CarsGroupButtonClick(object sender, System.EventArgs e)
		{
			int groupIndex = (int)((Button)sender).Tag;
			ulong group = LFSQuery.CAR_GROUP_BITS[groupIndex];
			for (int i = 0; i < LFSQuery.CAR_BITS.Length; ++i) {
				if ((LFSQuery.CAR_BITS[i] & group) != 0 && (LFSQuery.CAR_BITS[i] & LFSQuery.CAR_GROUP_DONTCARE[groupIndex]) == 0) {
					cars[i].CheckState = CheckState.Checked;
				} else {
					cars[i].CheckState = CheckState.Indeterminate;
					for (int j = 0; j < LFSQuery.CAR_GROUP_BITS.Length; ++j) {
						if ((LFSQuery.CAR_BITS[i] & LFSQuery.CAR_GROUP_DISALLOW[groupIndex]) != 0) {
							cars[i].CheckState = CheckState.Unchecked;
							break;
						}
					}
				}
			}
		}

		void AboutToolStripMenuItem1Click(object sender, System.EventArgs e) {
			string message = String.Format(languages.GetString("AboutBoxText"), bfs_version);
			MessageBox.Show(message, languages.GetString("AboutBoxTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		void ContextMenuBrowserOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			contextMenuBrowser.Enabled = (lvMain.GetSelectedServer() != null);
		}

		void ToolStripMenuItem1Click(object sender, System.EventArgs e)
		{
			ServerListItem item = lvMain.GetSelectedServer();
			if (item == null)
				return;
			lvFavourites.AddServer(item);
			WriteFav();
		}

		bool ReadXMLFav(string filename) {
			try {
				XmlDocument doc = new XmlDocument();
				doc.Load(filename);
				XmlNodeList list = doc.GetElementsByTagName("favourites");
				String docVersion = ((XmlElement)list[0]).GetAttribute("version");
				list = ((XmlElement)list[0]).GetElementsByTagName("favourite");
				foreach (XmlElement favourite in list) {
					try {
						ServerInformation info = new ServerInformation();
						if (docVersion == "2") {
							info.version = StringToVersion(favourite.GetElementsByTagName("version")[0].FirstChild.Value);
						} else {
							info.version = LFSQuery.VERSION_S2;
						}
						info.host = new IPEndPoint(IPAddress.Parse(favourite.GetElementsByTagName("ip")[0].FirstChild.Value), Convert.ToInt32(favourite.GetElementsByTagName("port")[0].FirstChild.Value));
						info.hostname = favourite.GetElementsByTagName("name")[0].FirstChild.Value;
						try {
							info.adminPassword = favourite.GetElementsByTagName("adminPassword")[0].FirstChild.Value;
							info.insimPort = Int32.Parse(favourite.GetElementsByTagName("insimPort")[0].FirstChild.Value);
						} catch (Exception e) {							
							info.adminPassword = "";
							info.insimPort = 29999;
						}
						try {
							info.password = favourite.GetElementsByTagName("password")[0].FirstChild.Value;
						} catch (Exception e) {
							info.password = "";
						}
						((ServerListView)lvFavourites).AddServer(info);
					} catch (Exception e) { }
				}
				return true;
			} catch (FileNotFoundException fnfe) {
				return false;	
			} catch (Exception e) {
				File.Copy(filename, filename +".backup", true);
				string message = String.Format(languages.GetString("LoadFavError"), e.Message, filename);
				MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		bool ReadFav(string filename)
		{
			try {
				TextReader tr = new StreamReader(filename);
				string s = tr.ReadToEnd();
				foreach(String server in s.Split(new Char[]{'\n'}))
				{
					if (server != ""){
						String[] favInfoTmp = server.Split(new Char[]{' '}, 2);
						String[] ipAddress = favInfoTmp[0].Split(':');
						ServerInformation info = new ServerInformation();
						info.hostname = favInfoTmp[1].Trim();
						info.host = new IPEndPoint(IPAddress.Parse(ipAddress[0]), Convert.ToInt32(ipAddress[1]));
						lvFavourites.AddServer(info);
					}
				}
				tr.Close();
				return true;
			} catch (Exception e) {
				return false;
			}
		}

		private String VersionToString(byte version)
		{
			if (version == LFSQuery.VERSION_S1)
			    return "S1";
			else if (version == LFSQuery.VERSION_DEMO)
			    return "Demo";
			else return "S2";
		}
		
		private byte StringToVersion(String version)
		{
			switch (version){
				case "Demo" : return LFSQuery.VERSION_DEMO;
				case "S1" : return LFSQuery.VERSION_S1;
				case "S2" : default : return LFSQuery.VERSION_S2;
			}
		}
		
		void WriteFav() {
			try {
				XmlTextWriter tw = new XmlTextWriter(favXMLFilename, null);
				tw.Formatting = Formatting.Indented;
				tw.WriteStartDocument();
				tw.WriteStartElement("favourites");
				tw.WriteAttributeString("version", "2");
				foreach (ServerListItem info in lvFavourites.AllServers()){
					tw.WriteStartElement("favourite");
					tw.WriteElementString("version", VersionToString(info.version));
					tw.WriteElementString("ip", info.host.Address.ToString());
					tw.WriteElementString("port", info.host.Port.ToString());
					tw.WriteElementString("name", info.hostname);
					tw.WriteElementString("password", info.password);
					tw.WriteElementString("insimPort", info.insimPort.ToString());
					tw.WriteElementString("adminPassword", info.adminPassword);
					tw.WriteEndElement();
				}
				tw.WriteFullEndElement();
    			tw.Close();
			}
    		catch (Exception ex) {
				string message = String.Format(languages.GetString("SaveFavError"), ex.Message);
				MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}
		public void DisplayFriends() {
			btnRefreshFriend.Enabled = false;
			btnAddFriend.Enabled = false;
			Thread t = new Thread(new ThreadStart(DisplayFriendsT));
			t.Start();
		}
		
		public void DisplayFriendsT() {
			lvFriends.Items.Clear();
			for (int i = 0; i < friendList.Count; ++i){
				FriendListItem friend = friendList[i];
				int result = LFSQuery.getPubStatInfo(friend.name, out friend.server);
				ListViewItem lvi;
				if (result == 0 && !cbHideOffline.Checked) { //offline
					lvi = lvFriends.Items.Add(friend.name, friend.name, "");
					lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, languages.GetString("MainForm.Offline")));
					lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, "N/A"));
					lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, "N/A"));
				} else if (result == 1) {
					lvi = lvFriends.Items.Add(friend.name, friend.name, "");
					lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi,friend.server.hostname));
					lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, (friend.server.passworded ? languages.GetString("Global.Yes") : languages.GetString("Global.No"))));
					String players = "";
					foreach (string player in friend.server.racerNames) {
						players += player + ", ";
					}
					players = players.Remove(players.Length - 2, 2).ToString();	
					lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, players));
				} else if (result == -1) {
					lvi = lvFriends.Items.Add(friend.name, friend.name, "");
					lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, languages.GetString("ServerInformationForm.PubstatError")));
	                lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, languages.GetString("Global.Error")));
					lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, languages.GetString("Global.Error")));
				}
			}
			btnRefreshFriend.Enabled = true;
			btnAddFriend.Enabled = true;
		}
		
		public void AddFriend(string name, bool writeToFile)
		{
			ListViewItem lvi;
			bool exists = false; //do this better!
			foreach(FriendListItem f in friendList){
				if (f.name == name){
					exists = true;
					break;
				}
			}
			if (!exists){
				lvi = lvFriends.Items.Add(name, name, "");
				lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, languages.GetString("Global.Error")));
				lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, "N/A"));
				lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, "N/A"));
				FriendListItem friend = new FriendListItem();
				friend.name = name;
				friendList.Add(friend);
			}
			if (writeToFile)
				WriteFriends();
		}
		
		void WriteFriends()
		{
			try {
				XmlTextWriter tw = new XmlTextWriter(friendFilename, null);
				tw.Formatting = Formatting.Indented;
				tw.WriteStartDocument();
				tw.WriteStartElement("friends");
				tw.WriteAttributeString("version", "2");
				foreach (FriendListItem friend in friendList) {
					tw.WriteStartElement("friend");
					tw.WriteAttributeString("license", friend.name);
					tw.WriteEndElement();
				}
				tw.WriteFullEndElement();
				tw.Close();
			} catch (Exception ex) {
				string message = String.Format(languages.GetString("SaveFriendsError"), ex.Message);
				MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
		}
		
		void ReadFriends()
		{
			try{
				XmlDocument doc = new XmlDocument();
				doc.Load(friendFilename);
				XmlNodeList list = doc.GetElementsByTagName("friends");
				String docVersion = ((XmlElement)list[0]).GetAttribute("version");
				list = ((XmlElement)list[0]).GetElementsByTagName("friend");				
				foreach (XmlElement friend in list) {
					if (docVersion == "2")
						AddFriend(friend.GetAttribute("license"), false);
					else
						AddFriend(friend.GetAttribute("name"), false);
				}
			} catch (FileNotFoundException fnfe){
			} catch (Exception e) {
				File.Copy(friendFilename, friendFilename +".backup", true);
				string message = String.Format(languages.GetString("LoadFriendsError"), e.Message, friendFilename);
				MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			lvFriends.Sort();
		}


		void ContextMenuFavOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			contextMenuFav.Enabled = (lvFavourites.GetSelectedServer() !=  null);
		}

		void lvFavouritesSelectedIndexChanged(object sender, System.EventArgs e) {
			btnJoinFav.Enabled = (lvFavourites.GetSelectedServer() !=  null);
		}

		void ViewServerInformationToolStripMenuItemClick(object sender, System.EventArgs e) {
			ServerListItem info = null;
			if (sender is ToolStripMenuItem){
				ToolStripMenuItem l = (ToolStripMenuItem)sender;
				if (l.Name == "viewServerInformationMain")
					info = lvMain.GetSelectedServer();
				else if (l.Name == "viewServerInformationFav")
					info = lvFavourites.GetSelectedServer();
				else if (l.Name == "viewServerInformationFriend"){
				//	if ((lvFriends.SelectedItems.Count > 0) || (lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text == "Offline"))
			//			return;
					string friend = lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[0].Text;
					foreach (FriendListItem f in friendList){
						if (f.name == friend){
							info = new ServerListItem(f.server);
							MessageBox.Show(info.host.ToString());
							info.version = LFSQuery.VERSION_S2;
							break;
						}
					}
				}
			} else {
				info = ((ServerListView)sender).GetSelectedServer();
			}
			
			if (info == null)
				return;
			s.SetInfo(info);
			if (s.ShowDialog(this) == DialogResult.OK) {
				info.password = s.GetInfo().password;
				WriteFav();
				LoadLFS(s.GetInfo().hostname, VersionToString(s.GetInfo().version), s.GetInfo().password);
			}
		}

		void MainFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
			LFSQuery.stopQuerying();
			this.exiting = true;
			UpdateConfig();
			config.Save(configXMLFilename);
			WriteFav();
			WriteFriends();
			this.Hide();
		}


		void ComboBoxTracksSelectedIndexChanged(object sender, System.EventArgs e)
		{
			string filter = (cbTracks.Text == "All Tracks" ? "" : cbTracks.Text);
			trackFilter = filter;
			lvMain.Filter(FilterType.Track, filter);
			lvMain.DisplayAll();
		}
		
		static string trackFilter;
		private static bool FilterServerMatchesTrack(ServerInformation info)
		{
			return info.track.Contains(trackFilter);
		}

		void btnFindUserClick(object sender, System.EventArgs e)
		{
			string player = edtFindUserMain.Text;
			if (player.Length > 32) {
				string message = String.Format(languages.GetString("PlayerNameTooLongError"));
				MessageBox.Show(message, languages.GetString("MainForm.title"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			string hostname = q.findUser("wabz", player);
			if (hostname != null){
				hostname = LFSQuery.removeColourCodes(hostname);
				string message = String.Format(languages.GetString("JoinPlayerQuery"), player, hostname);
				if (MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
					LoadLFS(hostname, "S2", "");
				}
			} else {
				MessageBox.Show(languages.GetString("UserNotFound"), languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		void TabControlSelectedIndexChanged(object sender, System.EventArgs e)
		{
			//if we've just come from the configuration panel to another one
			if (tabControl.TabPages[this.lastTabSelected] == tabConfig) {
				UpdateConfig();
				config.Save(configXMLFilename);
			}
			this.lastTabSelected = tabControl.SelectedIndex;
		}

		void UpdateConfig()
		{
			if (pathList.SelectedIndex < 0)
				config.lfsPath = "";
			else
				config.lfsPath = pathList.Items[pathList.SelectedIndex].ToString();
			config.disableWait = cbQueryWait.Checked;
			config.checkNewVersion = cbNewVersion.Checked;
			config.filter_empty = cbEmpty.Checked;
			config.filter_full = cbFull.Checked;
			config.filter_private = cbPrivate.Checked;
			config.filter_public = cbPublic.Checked;
			config.queryWait = (int)queryWait.Value;
			config.joinOnClick = rbJoin.Checked;
			config.language = (cbConfigLang.SelectedItem ?? "").ToString();
			LFSQuery.xpsp2_wait = !config.disableWait;
			LFSQuery.THREAD_WAIT = config.queryWait;
			config.psp.Clear();
			config.psp.TrimExcess();

			foreach(Object o in lbPreStart.Items){
				if (o.ToString() == "[New Program]")
					continue;
				config.psp.Add((PreStartProgram)o);
			}
			
			try {
				config.insimPort = Convert.ToInt32(txtInsimPort.Text);
			} catch (Exception ex) {
				config.insimPort = 29999;
			}
		}

		void RemoveFromFavouritesToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			ServerListItem item = lvFavourites.GetSelectedServer();
			if (item == null)
				return;
			if (sender is MainForm) { //delete key pressed.
				string message = String.Format(languages.GetString("RemoveFavQuery"), item.hostname);
				if (MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No){
					return;
				}
			}
			lvFavourites.RemoveServer(item);
			WriteFav();
		}


		public static void versionCheck(bool botherUser) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(version_check_url);
			request.Timeout = 3000;
			try {
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				if (response.StatusCode == HttpStatusCode.OK) {
					Stream stream = response.GetResponseStream();
					byte[] buf = new byte[1];
					stream.Read(buf, 0, buf.Length);
					if (buf[0] == version_check[0]) {
						if (botherUser) {
							MessageBox.Show(languages.GetString("UpToDate"), languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					} else {
						if (MessageBox.Show(languages.GetString("NewVersion"), languages.GetString("MainForm.this"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes){
							System.Diagnostics.Process.Start(download_url);
						}
					}
				}
			} catch (Exception e) {
				if (botherUser) {
					MessageBox.Show(languages.GetString("UpdateError"), languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		void btnCheckNewVersionClick(object sender, System.EventArgs e)
		{
			MainForm.versionCheck(true);
		}

		void CbQueryWaitCheckedChanged(object sender, System.EventArgs e) {
			queryWait.Enabled = (cbQueryWait.CheckState != CheckState.Checked);
		}


		void BtnBrowsePSClick(object sender, System.EventArgs e)
		{
			if (openFileDialogPS.ShowDialog() == DialogResult)
					return;
			edtProgramPath.Text = openFileDialogPS.FileName;
			if (edtProgramName.Text == "")
				edtProgramName.Text = Path.GetFileName(openFileDialogPS.FileName);
		}

		void TxtInsimPortLeave(object sender, System.EventArgs e)
		{
			try {
				int value = Convert.ToInt32(txtInsimPort.Text);
				if (value > 65535) throw new Exception();
			} catch (Exception ex) {
				string message = String.Format(languages.GetString("InvalidPort"), txtInsimPort.Text);
				MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtInsimPort.Focus();
			}

		}

		void CbUsePSCheckStateChanged(object sender, System.EventArgs e)
		{
			//txtPSPath.Enabled = ((CheckBox)sender).Checked;
			//txtInsimPort.Enabled = ((CheckBox)sender).Checked;
			//btnBrowsePS.Enabled = ((CheckBox)sender).Checked;
		}

		void MainKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.Control) {
				if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus) {
					lvMain.Font = new Font(lvMain.Font.FontFamily.Name, lvMain.Font.Size + 1);
					lvFavourites.Font = new Font(lvFavourites.Font.FontFamily.Name, lvFavourites.Font.Size + 1);
					lvFriends.Font = new Font(lvFriends.Font.FontFamily.Name, lvFriends.Font.Size + 1);
				} else if (lvMain.Font.Size > 1 && (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)) {
					lvMain.Font = new Font(lvMain.Font.FontFamily.Name, lvMain.Font.Size - 1);
					lvFavourites.Font = new Font(lvFavourites.Font.FontFamily.Name, lvFavourites.Font.Size - 1);
					lvFriends.Font = new Font(lvFriends.Font.FontFamily.Name, lvFriends.Font.Size - 1);
				} else if (e.KeyCode == Keys.C){
					if (tabControl.TabPages[tabControl.SelectedIndex] == tabMain){
						SetClipboard(lvMain);
					}
					else if (tabControl.TabPages[tabControl.SelectedIndex] == tabFavourites) {
						SetClipboard(lvFavourites);
					}
				}
			} else {
				if (e.KeyCode == Keys.Delete) {
					if (edtAddServerAddress.Focused || edtFriendName.Focused){
							return;
					}
					if (tabControl.TabPages[tabControl.SelectedIndex] == tabFavourites) {
						RemoveFromFavouritesToolStripMenuItemClick(sender, e);
					} else if (tabControl.TabPages[tabControl.SelectedIndex] == tabFriends) {
						RemoveFriendToolStripMenuItemClick(sender, e);
					}
				}
			}
		}
		
		void SetClipboard(ServerListView list)
		{
			ServerListItem item = null;
			item = list.GetSelectedServer();
			if (item != null && item.hostname != null) {
				Clipboard.SetText(item.hostname + " | " + item.host.ToString());
			}
		}
		
		void BtnRefreshFriendClick(object sender, System.EventArgs e)
		{
			DisplayFriends();
			lvFriends.Sort();
		}
		
		void CheckBox2CheckedChanged(object sender, System.EventArgs e)
		{
			BtnRefreshFriendClick(sender, e);
		}
		
		void LvFriendsSelectedIndexChanged(object sender, System.EventArgs e)
		{
			btnJoinFriend.Enabled = (lvFriends.SelectedItems.Count > 0 && lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text != languages.GetString("MainForm.Offline"));
		}
		
		void JoinFriendClick(object sender, System.EventArgs e)
		{
			if ((lvFriends.SelectedItems.Count > 0) || (lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text == languages.GetString("MainForm.Offline")))
				return;
			string hostname = lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text;
			LoadLFS(hostname, "S2", edtPasswordMain.Text);
		}
		
		void BtnAddFriendClick(object sender, System.EventArgs e)
		{
			if (edtFriendName.Text.Length > 0) {
				AddFriend(edtFriendName.Text, true);
				DisplayFriends();
				lvFriends.Sort();
				edtFriendName.Text = "";
			}
		}
		
		void RemoveFriendToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			if (lvFriends.SelectedItems.Count == -1)
				return;
			string name = lvFriends.Items[lvFriends.SelectedItems[0].Index].Text;
			if (sender is MainForm) { //delete key!
				string message = String.Format(languages.GetString("RemoveFriendQuery"), name);
				if (MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No){
					return;
				}
			}
			for (int i = 0; i < friendList.Count; i++){
				if (friendList[i].name == name){
					friendList.RemoveAt(i);
					break;
				}
			}
			lvFriends.Items.RemoveByKey(name);
		}
		
		void ContextMenuFriendsOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			bool enabled = (lvFriends.SelectedItems.Count > 0);
			removeFriendToolStripMenuItem.Enabled = enabled;
			joinServerMenuFriends.Enabled = enabled && (lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text != languages.GetString("MainForm.Offline"));
		}
		
		void LvFriendsDoubleClick(object sender, System.EventArgs e)
		{
			if ((lvFriends.SelectedItems.Count <= 0) || (lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text == languages.GetString("MainForm.Offline")))
				return;
			string hostname = lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text;
			string friend = lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[0].Text;
			string message = String.Format(languages.GetString("JoinFriendQuery"), friend, hostname);
			if (MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
				LoadLFS(hostname, "S2", edtPasswordMain.Text);
			}
		}
		
		void EdtFriendNameKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter && edtFriendName.Text.Length > 0) {
				AddFriend(edtFriendName.Text, true);
				lvFriends.Sort();
				edtFriendName.Clear();
			}
			
		}
		
		void MainFormLoad(object sender, System.EventArgs e)
		{
			cars = new CheckBox[LFSQuery.CAR_BITS.Length];
			groups = new Button[LFSQuery.CAR_GROUP_BITS.Length];
			for (int i = 0; i < LFSQuery.CAR_BITS.Length; ++i) { //create car checkboxes
				cars[i] = new CheckBox();
				cars[i].Parent = gbFilters;
				if ((i % 2) == 0) {
					cars[i].Left = 16;
					cars[i].Top = (i/2 * 20) + 20;
				} else {
					cars[i].Top = cars[i-1].Top;
					cars[i].Left = 78;
				}
				cars[i].Text = LFSQuery.CAR_NAMES[i];
				cars[i].Width = 50;
				cars[i].Height = 22;
				cars[i].ThreeState = true;
				cars[i].CheckState = CheckState.Indeterminate;
				cars[i].Tag = i;
				cars[i].Click +=  new EventHandler(carsChanged);
			}

			for (int i = 0; i < LFSQuery.CAR_GROUP_BITS.Length; ++i) { //create group buttons
				groups[i] = new Button();
				groups[i].Parent = gbFilters;
				if ((i % 2) == 0) {
					groups[i].Left = 16;
					groups[i].Top = (i/2 * 30) + 235;
				} else {
					groups[i].Top = groups[i-1].Top;
					groups[i].Left = 78;
				}
				groups[i].Text = LFSQuery.CAR_GROUP_NAMES[i];
				groups[i].Width = 42;
				groups[i].Height = 23;
				groups[i].Tag = i;
				groups[i].Click += new EventHandler(CarsGroupButtonClick);
				groups[i].Click +=  new EventHandler(carsChanged);
			}
			friendList = new List<FriendListItem>();
			lvwColumnSorter = new ListViewColumnSorter();
			lvwColumnSorter.SortColumn = 1;
			lvwColumnSorter.Order = SortOrder.Ascending;
			lvFavourites.ListViewItemSorter = lvwColumnSorter;
			lvFavourites.Sort();
			lvMain.ListViewItemSorter = lvwColumnSorter;
			lvMain.Sort();
			lvFriends.ListViewItemSorter = lvwColumnSorter;
			lvwColumnSorter.SortColumn = 1;
			lvFriends.Sort();
			s = new ServerInformationForm(this);
			q = new LFSQuery();
			//search for LFS.exe
			RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\ShellNoRoam\\MUICache\\" );
			foreach(string valuename in key.GetValueNames()){
					//insert all the found values into the list
					if (key.GetValue(valuename).ToString() == "LFS"){
						pathList.Items.Add(valuename);
					}
			}
			//setup the config
			config = new Configuration(configXMLFilename);
			bool loadedconf = false;
			if (!config.LoadXML()) {				
				config = new Configuration(configFilename);
				if (!config.Load()) {
					//config not valid, take them to config screen
					tabControl.SelectTab(tabControl.TabPages.IndexOf(tabConfig));
					config.disableWait =  false;
					config.checkNewVersion = false;
					this.lastTabSelected = 2;
					if (pathList.Items.Count > 0) {
						pathList.SelectedIndex = 0;
					}
				} else {
					loadedconf = true;
					File.Delete(configFilename); //xml'ised!
				}
			} else {				
				loadedconf = true; 
			}
			if (!ReadXMLFav(favXMLFilename)) {
				if (ReadFav(favFilename)) {
					File.Delete(favFilename); //xml'ised!
				}
			}
			//if we have previous config data
			cbConfigLang.Items.Clear();
			cbConfigLang.Items.AddRange(languages.Languages.ToArray());
			cbConfigLang.SelectedItem = "English";
			
			if (loadedconf) {
				//query wait
				cbEmpty.Checked = config.filter_empty;
				cbFull.Checked = config.filter_full;
				cbPrivate.Checked = config.filter_private;
				cbPublic.Checked = config.filter_public;
				cbQueryWait.Checked = config.disableWait;
				queryWait.Enabled = !config.disableWait;
				LFSQuery.xpsp2_wait = !config.disableWait;
				LFSQuery.THREAD_WAIT = config.queryWait;
				queryWait.Value = config.queryWait;
				if (String.IsNullOrEmpty(config.language) && languages.Count > 0)
					cbConfigLang.SelectedItem = "English";
				else
					cbConfigLang.SelectedItem = config.language;

				cbNewVersion.Checked = config.checkNewVersion;
				//Pre-start programs
				lbPreStart.Items.Clear();
				foreach (PreStartProgram p in config.psp){
					lbPreStart.Items.Add(p);
				}
				txtInsimPort.Text = config.insimPort != 0 ? config.insimPort.ToString() : "29999";
				//join on click
				rbJoin.Checked = config.joinOnClick;
				rbView.Checked = !config.joinOnClick;

				if (config.checkNewVersion) {
					versionCheck(false);
				}
				int pathIndex = pathList.Items.IndexOf(config.lfsPath);
				//if we actually found some lfs.exe's and our config exists in the list
				if (pathIndex >= 0 && pathList.Items.Count > 0) {
					pathList.SelectedIndex = pathIndex;
				}
				//if we found some, but it doesn't exist in the list, select the first one
				else if (pathIndex < 0 && pathList.Items.Count > 0)
					pathList.SelectedIndex = 0;
				else {//we didn't find anything, something's gone wrong!@
					pathList.Items.Add(config.lfsPath);
					pathList.SelectedIndex = 0;
				}
			}
			ReadFriends();
			DisplayFriends();
			cbTracks.SelectedIndex = 0;
			cbPing.SelectedIndex = 7;
			cbVersion.SelectedIndex = 2;
			cbAddServerVersion.SelectedIndex = 0;

		}
		
		void CbEmptyCheckedChanged(object sender, System.EventArgs e)
		{
			lvMain.Filter(FilterType.Empty, cbEmpty.Checked);
			lvMain.DisplayAll();
			
		}
		
		void CbPingSelectedIndexChanged(object sender, System.EventArgs e)
		{
			lvMain.Filter(FilterType.Ping, Convert.ToInt32(cbPing.Text));
			lvMain.DisplayAll();
			
		}
		
		void CbFullCheckedChanged(object sender, System.EventArgs e)
		{
			lvMain.Filter(FilterType.Full, cbFull.Checked);
			lvMain.DisplayAll();
		}
		
		void CbPrivateCheckedChanged(object sender, System.EventArgs e)
		{
			lvMain.Filter(FilterType.Private, cbPrivate.Checked);
			lvMain.DisplayAll();
		}
		
		void CbPublicCheckedChanged(object sender, System.EventArgs e)
		{
			lvMain.Filter(FilterType.Public, cbPublic.Checked);
			lvMain.DisplayAll();
		}
		
		void carsChanged(object sender, System.EventArgs e) {
			lvMain.Filter(FilterType.Cars, cars);
			lvMain.DisplayAll();
		}
		
		private byte version;
		void CbVersionSelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.version = StringToVersion(cbVersion.Text);
		}
	
		void AdministrateToolStripMenuItemClick(object sender, System.EventArgs e) {
			ServerListItem i;			
			if (((ToolStripMenuItem)sender).Name == "administrateToolStripMenuItem") {
				i = lvFavourites.GetSelectedServer();
			} else {
				i = lvMain.GetSelectedServer();
			}
			AdminForm admin = new AdminForm(i);
			admin.Show(this);
		}
		
		void CopyServerToClipboardToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			if (((ToolStripMenuItem)sender).Name == "copyServerToClipboardToolStripMenuItem") {
				SetClipboard(lvMain);
			} else {
				SetClipboard(lvFavourites);
			}
		}
		
		void BtnAddServerClick(object sender, System.EventArgs e)
		{
			string host = edtAddServerAddress.Text;
			if (!host.Contains(":")){
				host += ":63392";
			}
			try {
				string[] hostname = host.Split(':');
				if (hostname.Length != 2) {
					MessageBox.Show(languages.GetString("InvalidIPAddress"), languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				ServerInformation info = new ServerInformation();
				int port;
				try {
					port = Convert.ToInt32(hostname[1]);
					if (port < 1 || port > 65535){
						throw new Exception();
					}
				} catch (Exception ex) {
					string message = String.Format(languages.GetString("InvalidPort"), hostname[1]);
					MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);				
					return;
				}
				info.host = new IPEndPoint(IPAddress.Parse(hostname[0]), port);
				info.version = StringToVersion(cbAddServerVersion.Text);
				info.hostname = info.host.ToString();
				lvFavourites.AddServer(info);
				edtAddServerAddress.Text = "";
			} catch (Exception ex) {
				string message = String.Format(languages.GetString("AddFavError"), ex.Message);
				MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		void EdtAddServerAddressKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (edtAddServerAddress.Text.Length > 0 && e.KeyCode == Keys.Enter) {
				BtnAddServerClick(sender, e);
				edtAddServerAddress.Clear();
			}
		}
		
		void BtnProgramNewClick(object sender, System.EventArgs e)
		{
			edtProgramName.Enabled = true;
			edtProgramName.Text = "";		
			edtProgramName.Focus();
			
			edtProgramOptions.Enabled = true;
			edtProgramOptions.Text = "";
			
			edtProgramPath.Enabled = true;
			edtProgramPath.Text = "";
			
			btnProgramCancel.Enabled = true;
			btnProgramBrowse.Enabled = true;
			btnProgramSave.Text = languages.GetString("MainForm.btnProgramSave");
			lbPreStart.SelectedIndex = -1;
		}
		
		void EdtProgramNameTextChanged(object sender, System.EventArgs e)
		{
			btnProgramSave.Enabled = ((edtProgramName.Text != "") && (edtProgramPath.Text != ""));
			
		}
		
		void BtnProgramCancelClick(object sender, System.EventArgs e)
		{
			edtProgramName.Enabled = false;
			edtProgramName.Text = "";		
			
			edtProgramOptions.Enabled = false;
			edtProgramOptions.Text = "";
			
			edtProgramPath.Enabled = false;
			edtProgramPath.Text = "";
			
			btnProgramCancel.Enabled = false;
			btnProgramBrowse.Enabled = false;
			btnProgramSave.Text = languages.GetString("MainForm.btnProgramSave");
			btnProgramEnable.Enabled = false;
			
		}
	
		void LbPreStartDoubleClick(object sender, System.EventArgs e) {
			if (lbPreStart.SelectedItem == null)
				return;
			string item = lbPreStart.SelectedItem.ToString();
			if (item == "[New Program]"){
				BtnProgramNewClick(sender, e);
			}
			
		}
		
		void LbPreStartSelectedIndexChanged(object sender, System.EventArgs e) {
			if (lbPreStart.SelectedItem == null){
				btnProgramEnable.Enabled = false;
				return;
			}
			
			if (lbPreStart.SelectedItem.ToString() == "[New Program]"){
				btnProgramDelete.Enabled = false;
				BtnProgramCancelClick(sender, e);
				return;
			}
				
			
			PreStartProgram p = (PreStartProgram)lbPreStart.SelectedItem;
			edtProgramName.Enabled = true;
			edtProgramName.Text = p.name;		
			
			edtProgramOptions.Enabled = true;
			edtProgramOptions.Text = p.options;
			
			edtProgramPath.Enabled = true;
			edtProgramPath.Text = p.path;
			
			btnProgramCancel.Enabled = true;
			btnProgramBrowse.Enabled = true;
			btnProgramSave.Text = languages.GetString("MainForm.btnProgramUpdate");
			
			btnProgramEnable.Enabled = true;
			btnProgramEnable.Text = (p.enabled ? languages.GetString("MainForm.btnProgramDisable") : languages.GetString("MainForm.btnProgramEnable"));
			
			btnProgramDelete.Enabled = true;
		}
		
		void BtnProgramSaveClick(object sender, System.EventArgs e) {
			PreStartProgram p = new PreStartProgram(edtProgramName.Text, edtProgramPath.Text, edtProgramOptions.Text);
			if (btnProgramSave.Text == languages.GetString("MainForm.btnProgramUpdate")){
				lbPreStart.Items[lbPreStart.SelectedIndex] = p;
			} else {
				lbPreStart.Items.Add(p);
			}
		}
		
		void BtnProgramEnableClick(object sender, System.EventArgs e) {
			PreStartProgram p = (PreStartProgram)lbPreStart.SelectedItem;
			p.enabled = !p.enabled;
			lbPreStart.Items[lbPreStart.SelectedIndex] = p;

		}
		
		void BtnProgramDeleteClick(object sender, System.EventArgs e) {
			lbPreStart.Items.Remove(lbPreStart.SelectedItem);
			BtnProgramCancelClick(sender, e);
			
		}
		
		void JoinServerToolStripMenuItem2Click(object sender, System.EventArgs e)
		{
			JoinServerDialog j = new JoinServerDialog();
			if (j.ShowDialog() == DialogResult.OK){
				LoadLFS(j.serverName, j.version, j.password);
			}
			
		}

		private void UpdateUI()
		{
			if (languages.Count == 0)
				return;
			fileToolStripMenuItem.Text = languages.GetString("MainForm.fileToolStripMenuItem");
			joinServerToolStripMenuItem2.Text = languages.GetString("MainForm.joinServerToolStripMenuItem2");
			closeToolStripMenuItem.Text = languages.GetString("MainForm.closeToolStripMenuItem");
			aboutToolStripMenuItem.Text = languages.GetString("MainForm.aboutToolStripMenuItem");
			aboutToolStripMenuItem1.Text = languages.GetString("MainForm.aboutToolStripMenuItem1");
			tabMain.Text = languages.GetString("MainForm.tabMain");
			lblFindUserMain.Text = languages.GetString("MainForm.lblFindUserMain");
			lblPasswordMain.Text = languages.GetString("MainForm.lblPasswordMain");
			btnFindUserMain.Text = languages.GetString("MainForm.btnFindUserMain");
			gbFilters.Text = languages.GetString("MainForm.gbFilters");
			cbPublic.Text = languages.GetString("MainForm.cbPublic");
			cbPrivate.Text = languages.GetString("MainForm.cbPrivate");
			cbFull.Text = languages.GetString("MainForm.cbFull");
			lblPingThreshold.Text = languages.GetString("MainForm.lblPingThreshold");
			cbEmpty.Text = languages.GetString("MainForm.cbEmpty");
			columnHeaderName.Text = languages.GetString("MainForm.columnHeaderName");
			columnHeaderPing.Text = languages.GetString("MainForm.columnHeaderPing");
			columnPrivate.Text = languages.GetString("MainForm.columnPrivate");
			columnHeaderConnections.Text = languages.GetString("MainForm.columnHeaderConnections");
			columnHeaderInfo.Text = languages.GetString("MainForm.columnHeaderInfo");
			columnHeaderTrack.Text = languages.GetString("MainForm.columnHeaderTrack");
			columnHeaderCars.Text = languages.GetString("MainForm.columnHeaderCars");
			joinServerToolStripMenuItem.Text = languages.GetString("MainForm.joinServerToolStripMenuItem");
			viewServerInformationMain.Text = languages.GetString("MainForm.viewServerInformationMain");
			toolStripMenuItem1.Text = languages.GetString("MainForm.toolStripMenuItem1");
			copyServerToClipboardToolStripMenuItem.Text = languages.GetString("MainForm.copyServerToClipboardToolStripMenuItem");
			administrateToolStripMenuItem1.Text = languages.GetString("MainForm.administrateToolStripMenuItem1");
			btnRefreshMain.Text = languages.GetString("MainForm.btnRefresh");
			btnJoinMain.Text = languages.GetString("MainForm.btnJoin");
			tabFavourites.Text = languages.GetString("MainForm.tabFavourites");
			lblAddressFav.Text = languages.GetString("MainForm.lblAddressFav");
			btnAddServer.Text = languages.GetString("MainForm.btnAddServer");
			buttonRefreshFav.Text = languages.GetString("MainForm.btnRefresh");
			btnJoinFav.Text = languages.GetString("MainForm.btnJoin");
			columnHeaderFavServerName.Text = languages.GetString("MainForm.columnHeaderFavServerName");
			columnHeaderFavPing.Text = languages.GetString("MainForm.columnHeaderFavPing");
			columnHeaderFavSlots.Text = languages.GetString("MainForm.columnHeaderFavSlots");
			columnHeaderFavInfo.Text = languages.GetString("MainForm.columnHeaderFavInfo");
			columnHeaderFavTrack.Text = languages.GetString("MainForm.columnHeaderFavTrack");
			columnHeaderFavCars.Text = languages.GetString("MainForm.columnHeaderFavCars");
			joinServerToolStripMenuItem1.Text = languages.GetString("MainForm.joinServerToolStripMenuItem1");
			viewServerInformationFav.Text = languages.GetString("MainForm.viewServerInformationFav");
			removeFromFavouritesToolStripMenuItem.Text = languages.GetString("MainForm.removeFromFavouritesToolStripMenuItem");
			copyServerToClipboardToolStripMenuItem1.Text = languages.GetString("MainForm.copyServerToClipboardToolStripMenuItem1");
			administrateToolStripMenuItem.Text = languages.GetString("MainForm.administrateToolStripMenuItem");
			tabFriends.Text = languages.GetString("MainForm.tabFriends");
			btnAddFriend.Text = languages.GetString("MainForm.btnAddFriend");
			cbHideOffline.Text = languages.GetString("MainForm.cbHideOffline");
			btnRefreshFriend.Text = languages.GetString("MainForm.btnRefreshFriend");
			btnJoinFriend.Text = languages.GetString("MainForm.btnJoinFriend");
			columnFriendName.Text = languages.GetString("MainForm.columnFriendName");
			columnFriendServer.Text = languages.GetString("MainForm.columnFriendServer");
			columnFriendPrivate.Text = languages.GetString("MainForm.columnFriendPrivate");
			columnFriendPlayers.Text = languages.GetString("MainForm.columnFriendPlayers");
			joinServerMenuFriends.Text = languages.GetString("MainForm.joinServerMenuFriends");
			//viewServerInformationFriend.Text = languages.GetString("MainForm.viewServerInformationFriend");
			removeFriendToolStripMenuItem.Text = languages.GetString("MainForm.removeFriendToolStripMenuItem");
			tabConfig.Text = languages.GetString("MainForm.tabConfig");
			groupBox1.Text = languages.GetString("MainForm.groupBox1");
			groupBox2.Text = languages.GetString("MainForm.groupBox2");
			groupBox5.Text = languages.GetString("MainForm.groupBox5");
			lblCommentReal.Text = languages.GetString("MainForm.lblCommentReal");
			lblEmailReal.Text = languages.GetString("MainForm.lblEmailReal");
			lblAuthorReal.Text = languages.GetString("MainForm.lblAuthorReal");
			lblLangComment.Text = languages.GetString("MainForm.lblLangComment");
			lblLangContact.Text = languages.GetString("MainForm.lblLangContact");
			lblLangAuthor.Text = languages.GetString("MainForm.lblLangAuthor");
			lblLanguageConfig.Text = languages.GetString("MainForm.lblLanguageConfig");
			groupBox4.Text = languages.GetString("MainForm.groupBox4");
			rbJoin.Text = languages.GetString("MainForm.rbJoin");
			rbView.Text = languages.GetString("MainForm.rbView");
			gbStartBeforeLFS.Text = languages.GetString("MainForm.gbStartBeforeLFS");
			btnProgramEnable.Text = languages.GetString("MainForm.btnProgramEnable");
			btnProgramDelete.Text = languages.GetString("MainForm.btnProgramDelete");
			btnProgramNew.Text = languages.GetString("MainForm.btnProgramNew");
			bgProgramConfig.Text = languages.GetString("MainForm.bgProgramConfig");
			btnProgramCancel.Text = languages.GetString("MainForm.btnProgramCancel");
			btnProgramSave.Text = languages.GetString("MainForm.btnProgramSave");
			lblProgramConfigArg.Text = languages.GetString("MainForm.lblProgramConfigArg");
			lblProgramConfigName.Text = languages.GetString("MainForm.lblProgramConfigName");
			btnProgramBrowse.Text = languages.GetString("MainForm.btnProgramBrowse");
			lblProgramConfigPath.Text = languages.GetString("MainForm.lblProgramConfigPath");
			groupBox3.Text = languages.GetString("MainForm.groupBox3");
			txtInsimPort.Text = languages.GetString("MainForm.txtInsimPort");
			lblQueryWaitDescription.Text = languages.GetString("MainForm.lblQueryWaitDescription");
			lblInsimPortConfig.Text = languages.GetString("MainForm.lblInsimPortConfig");
			cbQueryWait.Text = languages.GetString("MainForm.cbQueryWait");
			lblQueryWaitHelp.Text = languages.GetString("MainForm.lblQueryWaitHelp");
			btnCheckNewVersion.Text = languages.GetString("MainForm.btnCheckNewVersion");
			cbNewVersion.Text = languages.GetString("MainForm.cbNewVersion");
			lblExeDescriptionConfig.Text = languages.GetString("MainForm.lblExeDescriptionConfig");
			lblExePathConfig.Text = languages.GetString("MainForm.lblExePathConfig");
			buttonBrowse.Text = languages.GetString("MainForm.buttonBrowse");
			this.Text = languages.GetString("MainForm.this");
			
			lbPreStart.Items.Clear();
			foreach (PreStartProgram p in config.psp){
				lbPreStart.Items.Add(p);
			}
			
			if (statusTotal.Text != ""){
				statusTotal.Text = String.Format(languages.GetString("MainForm.StatusTotal"), numQueried, totalServers);
				statusNoReply.Text = String.Format(languages.GetString("MainForm.NoReply"), numServersNoReply);
				statusRefused.Text = String.Format(languages.GetString("MainForm.Refused"), numServersRefused);
			}
			
			lvMain.DisplayAll();
			lvFavourites.DisplayAll();
			
			lblAuthorReal.Text = languages.Author;
			lblEmailReal.Text = languages.Email;
			lblCommentReal.Text = languages.Comment;
		}
		
		void CbConfigLangSelectedIndexChanged(object sender, System.EventArgs e)
		{
			languages.ChangeLanguage(cbConfigLang.SelectedItem.ToString());
			UpdateUI();
		}
	
		void EdtFindUserMainKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter && edtFindUserMain.Text.Length > 0) {
				btnFindUserClick(sender, e);
				edtFindUserMain.Clear();
			}
		}
	}
/// Horray for code nicked from the MSDN!
public class ListViewColumnSorter : IComparer
{
	private int ColumnToSort;
	private SortOrder OrderOfSort;
	private CaseInsensitiveComparer ObjectCompare;

	public ListViewColumnSorter()
	{
		ColumnToSort = 0;
		OrderOfSort = SortOrder.None;
		ObjectCompare = new CaseInsensitiveComparer();
	}

	public int Compare(object x, object y)
	{
		int compareResult;
		ListViewItem listviewX, listviewY;
		listviewX = (ListViewItem)x;
		listviewY = (ListViewItem)y;
		try {
		string columnName = listviewX.ListView.Columns[ColumnToSort].Text;
		if (columnName == MainForm.languages.GetString("MainForm.columnHeaderFavPing")) { //if we're sorting the ping column
			compareResult = ObjectCompare.Compare(Convert.ToInt32(listviewX.SubItems[ColumnToSort].Text), Convert.ToInt32(listviewY.SubItems[ColumnToSort].Text));
		} else if (columnName == MainForm.languages.GetString("MainForm.columnHeaderFavSlots")) { //sorting connection column
				int playersX = Convert.ToInt32(listviewX.SubItems[ColumnToSort].Text.Split('/')[0]);
				int playersY = Convert.ToInt32(listviewY.SubItems[ColumnToSort].Text.Split('/')[0]);
				compareResult = ObjectCompare.Compare(playersX, playersY);
		} else  if (columnName == MainForm.languages.GetString("MainForm.columnFriendServer") && (listviewX.SubItems[ColumnToSort].Text == MainForm.languages.GetString("MainForm.Offline")) || (listviewY.SubItems[ColumnToSort].Text == MainForm.languages.GetString("MainForm.Offline"))) {
				compareResult = (listviewX.SubItems[ColumnToSort].Text == MainForm.languages.GetString("MainForm.Offline") ? listviewY.SubItems[ColumnToSort].Text == MainForm.languages.GetString("MainForm.Offline") ? 0 : 1 : -1);
		} else {
			compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
		}
		if (OrderOfSort == SortOrder.Ascending) {
			return compareResult;
		}
		else if (OrderOfSort == SortOrder.Descending) {
			return (-compareResult);
		}

		} catch (Exception ex) {
			//suppress any exceptions relating to indexes out of range.
		}
		return 0;
	}

	public int SortColumn
	{
		set	{
			ColumnToSort = value;
		}
		get	{
			return ColumnToSort;
		}
	}

	public SortOrder Order
	{
		set	{
			OrderOfSort = value;
		}
		get	{
			return OrderOfSort;
		}
	}

}
}

