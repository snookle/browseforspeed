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
	public enum FilterType{None, Track, Ping, HasCars, DoesNotHaveCars, Empty, Full, Private, Public};
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
				ServerListItem item = serverList[index];
				item.index = SelectedItems[0].Index + 1;
				return item;
			}
			else
				return null;
		}
		public void RemoveServer(ServerListItem item)
		{
			Items.RemoveAt(item.index);
			serverList.Remove(item);
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
				Sort();
			}
		}
		public List<ServerListItem> AllServers()
		{
			return serverList;
		}
		private static bool ServersNotFiltered(ServerListItem info)
		{
			return !info.filtered;
		}
		private static IPEndPoint hostMatch;
		private static bool MatchesHost(ServerListItem item)
		{
			return item.host == hostMatch;
		}
		public ListViewItem Display(ServerListItem item)
		{
			if (item.filtered)
				return new ListViewItem();
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
				lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, item.passworded == true ? "Yes" : "No"));
			}
			lvi.SubItems.Insert(3 - columnOffset, new ListViewItem.ListViewSubItem(lvi, item.players.ToString() +"/" + item.slots.ToString()));
			lvi.SubItems.Insert(4 - columnOffset, new ListViewItem.ListViewSubItem(lvi, rules));
			lvi.SubItems.Insert(5 - columnOffset, new ListViewItem.ListViewSubItem(lvi, item.track));			
			lvi.SubItems.Insert(6 - columnOffset, new ListViewItem.ListViewSubItem(lvi, cars));
			return lvi;
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
						//DO ZE CARS!
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
		}
	}
	
	public partial class MainForm
	{
		static string bfs_version = "0.3";
		static string version_check = "5";
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

		private List<string> friendList;

		private int totalServers;
		private int numQueried;
		private int numServersDone;
		private int numServersRefused;
		private int numServersNoReply;

		private int lastTabSelected;

		private Configuration config;
		private ServerInformationForm s;

		private bool exiting;

		private Thread t;


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
			SetControlProperty(buttonRefreshFav, "Text", "&Stop Refresh");
			SetControlProperty(btnRefreshMain, "Text", "&Stop Refresh");
			try {
				if (isFav) {
					q.query(0, 0, "browseforspeed", lvFavourites.GetAllHosts(), 1);
				} else {
					ulong compulsory;
					ulong illegal;
					CodeCars(out compulsory, out illegal);
					q.query(compulsory, illegal, "browseforspeed", 0, CodeFilters(), LFSQuery.VERSION_S2);
				}
			} catch(Exception e) {
					MessageBox.Show(e.Message + "Unable to contact the Master Server. Perhaps it is down, or your firewall is not configured properly." , "Unable to contact Master Server!", MessageBoxButtons.OK);
				}

			if (exiting) return;
			SetControlProperty(btnRefreshMain, "Enabled", true);
			SetControlProperty(buttonRefreshFav, "Enabled", true);
			SetControlProperty(buttonRefreshFav, "Text", "&Refresh");
			SetControlProperty(btnRefreshMain, "Text", "&Refresh");
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
			if (b.Text == "&Refresh"){
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
				statusTotal.Text = "Querying...";
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
						if (info.readFailed) {
							numServersNoReply++;
						} else {
							numServersRefused++;
						}
					}
					statusTotal.Text = String.Format("Checking host {0} of {1}", numQueried, totalServers);
					statusNoReply.Text = String.Format("No Reply: {0}", numServersNoReply);
					statusRefused.Text = String.Format("Refused Connection: {0}", numServersRefused);
				} else {
			    	AddServerDelegate addServer = new AddServerDelegate(AddServerToList);
			    	this.BeginInvoke(addServer, new object[] {info, list});
				}
			} catch(Exception e){ MessageBox.Show(e.Message + e.StackTrace,"",MessageBoxButtons.OK); }
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

		void LoadLFS(String hostName, String mode, String password)
		{
			String lfsPath = config.lfsPath;
			String psPath = config.psPath;
			FormWindowState ws = this.WindowState;
			LFSQuery.stopQuerying();
			Process ps = new Process();
			if (config.startPS){
				try {
					ps.StartInfo.FileName = psPath;
					ps.StartInfo.WorkingDirectory = Path.GetDirectoryName(psPath);
					ps.Start();
				} catch (Exception ex) {
					MessageBox.Show("Error executing Pit Spotter\n"+ex.Message+"\nRecheck your configuration.", appTitle, MessageBoxButtons.OK);
					return;
				}
			}
			try{
				this.WindowState = FormWindowState.Minimized;
				Process p = new Process();
				p.StartInfo.FileName = lfsPath;
				p.StartInfo.WorkingDirectory = Path.GetDirectoryName(lfsPath);
				p.StartInfo.Arguments = "/join=" + hostName + " /mode=" + mode + "/pass=" + password + (config.startPS ? "/insim=" + config.psInsimPort.ToString() : "");
				p.Start();
				p.WaitForExit();
				try {
					if (config.startPS) ps.Kill();
				} catch (Exception silly) {}
			} catch (Exception ex) {
				this.WindowState = ws;
				MessageBox.Show("Error executing LFS.exe\n"+ex.Message+"\nRecheck your configuration.", appTitle, MessageBoxButtons.OK);
			}
			this.WindowState = ws;
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
			bool writefav = false;
			if (sender is Button) {
				if (((Button)sender).Name == "btnJoinMain") {
					item = lvMain.GetSelectedServer();
				} else {
					item = lvFavourites.GetSelectedServer();
					writefav = true;
				}
			} else if (sender is ToolStripMenuItem) {
				if (((ToolStripMenuItem)sender).Name == "joinServerToolStripMenuItem") {
					item = lvMain.GetSelectedServer();
				} else {
					item = lvFavourites.GetSelectedServer();
					writefav = true;
				}
			}else {
				item = ((ServerListView)sender).GetSelectedServer();
			}
			if (item == null)
				return;
			if (writefav) {
				lvFavourites.GetSelectedServer().password = edtPasswordMain.Text;
				WriteFav();
			}
			LoadLFS(item.hostname, "S2", edtPasswordMain.Text);			
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
			MessageBox.Show("Browse For Speed "+bfs_version+" - http://www.browseforspeed.net\nCopyright 2006 Richard Nelson, Philip Nelson, Ben Kenny\n\nYou may modify and redistribute the program under the terms of the GPL (version 2 or later).\nA copy of the GPL is contained in the 'COPYING' file distributed with Browse For Speed.\nWe provide no warranty for this program.", "About", MessageBoxButtons.OK);
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
				MessageBox.Show(docVersion, "", MessageBoxButtons.OK);
				list = ((XmlElement)list[0]).GetElementsByTagName("favourite");
				foreach (XmlElement favourite in list) {
					try {
						ServerInformation info = new ServerInformation();
						if (docVersion == "2") {
							info.version = StringToVersion(favourite.GetElementsByTagName("name")[0].FirstChild.Value);
						} else {
							info.version = LFSQuery.VERSION_S2;
						}
						info.host = new IPEndPoint(IPAddress.Parse(favourite.GetElementsByTagName("ip")[0].FirstChild.Value), Convert.ToInt32(favourite.GetElementsByTagName("port")[0].FirstChild.Value));
						info.hostname = favourite.GetElementsByTagName("name")[0].FirstChild.Value;					
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
				MessageBox.Show("Error loading favourites: " + e.Message + "\nA copy was saved as "+filename +".backup", "", MessageBoxButtons.OK);
				File.Copy(filename, filename +".backup", true);
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

		String VersionToString(byte version)
		{
			if (version == LFSQuery.VERSION_S1)
			    return "S1";
			if (version == LFSQuery.VERSION_DEMO)
			    return "Demo";
			else return "S2";
		}
		
		byte StringToVersion(String version)
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
					tw.WriteEndElement();
				}
				tw.WriteFullEndElement();
    			tw.Close();
			}
    		catch (Exception ex) {
				MessageBox.Show("An error writing to the favourite server file occured: " +ex.Message, "Browse For Speed", MessageBoxButtons.OK);
			}

		}
		public void DisplayFriends()
		{
			lvFriends.Items.Clear();
			foreach (string name in friendList) {
				ServerInformation info;
				int result = LFSQuery.getPubStatInfo(name, out info);
				ListViewItem lvi;
				if (result == 0 && !cbHideOffline.Checked) { //offline
					lvi = lvFriends.Items.Add(name, name, "");
					lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, "Offline"));
					lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, "N/A"));
					lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, "N/A"));
				} else if (result == 1) {
					lvi = lvFriends.Items.Add(name, name, "");
					lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi,info.hostname));
					lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, (info.passworded ? "Yes" : "No")));
					String players = "";
					foreach (string player in info.racerNames) {
						players += player + ", ";
					}
					players = players.Remove(players.Length - 2, 2).ToString();	
					lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, players));
				} else if (result == -1) {
					lvi = lvFriends.Items.Add(name, name, "");
					lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, "Error Querying Pubstat"));
					lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, "Error"));
					lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, "Error"));
				}
			}
		}
		
		public void AddFriend(string name, bool writeToFile)
		{
			if (friendList.IndexOf(name) == -1)
				friendList.Add(name);
			lvFriends.Items.Add(name);
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
				tw.WriteAttributeString("version", "1");
				foreach (String friend in friendList) {
					tw.WriteStartElement("friend");
					tw.WriteAttributeString("name", friend);
					tw.WriteEndElement();
				}
				tw.WriteFullEndElement();
				tw.Close();
			} catch (Exception ex) {
				MessageBox.Show("An error writing to the friends file occured: " +ex.Message, "Browsw For Speed", MessageBoxButtons.OK);
			}
			
		}
		
		void ReadFriends()
		{
			try{
				XmlDocument doc = new XmlDocument();
				doc.Load(friendFilename);
				XmlNodeList list = doc.GetElementsByTagName("friends");
				list = ((XmlElement)list[0]).GetElementsByTagName("friend");
				foreach (XmlElement friend in list) {
					AddFriend(friend.GetAttribute("name"), false);
				}
			} catch (FileNotFoundException fnfe){
			} catch (Exception e) {
				MessageBox.Show("Error loading friends: " + e.Message + "\nA copy was saved as " + friendFilename + ".backup", "", MessageBoxButtons.OK);
				File.Copy(friendFilename, friendFilename +".backup", true);
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
			ServerListItem info;
			if (sender is ToolStripMenuItem){
				ToolStripMenuItem l = (ToolStripMenuItem)sender;
				if (l.Name == "viewServerInformationMain")
					sender = lvMain;
				else
					sender = lvFavourites;
			}
			info = ((ServerListView)sender).GetSelectedServer();
			if (info == null)
				return;
			s.SetInfo(info);
			if (s.ShowDialog(this) == DialogResult.OK) {
				info.password = s.GetInfo().password;
				WriteFav();
				LoadLFS(s.GetInfo().hostname, "S2", s.GetInfo().password);
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
			SortedList l = new SortedList();
		}


		void ComboBoxTracksSelectedIndexChanged(object sender, System.EventArgs e)
		{
			string filter = (cbTracks.Text == "All Tracks" ? "" : cbTracks.Text);
			trackFilter = filter;
			lvMain.Filter(FilterType.Track, filter);
			lvMain.DisplayAll();
		}
		
		static string trackFilter;
		static string hostnameFilter;
		private static bool FilterServerMatchesTrack(ServerInformation info)
		{
			return info.track.Contains(trackFilter);
		}
		
		private static bool FilterServerMatchesHost(ServerInformation info)
		{
			return info.hostname == hostnameFilter;
			
		}

		void btnFindUserClick(object sender, System.EventArgs e)
		{
			string hostname = q.findUser("browseforspeed", edtFindUserMain.Text);
			if (hostname != null){
				hostname = LFSQuery.removeColourCodes(hostname);
				if (MessageBox.Show("Join " + edtFindUserMain.Text + " at this host?\n-" + hostname +"-", appTitle, MessageBoxButtons.YesNo) == DialogResult.Yes){
					LoadLFS(hostname, "S2", "");
				}
			} else {
				MessageBox.Show("User was not found online", appTitle, MessageBoxButtons.OK);
			}
		}

		void TabControlSelectedIndexChanged(object sender, System.EventArgs e)
		{
			//if we've just come from the configuration panel to another one
			if (tabControl.TabPages[this.lastTabSelected].Text  == "Configuration") {
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
			config.queryWait = (int)queryWait.Value;
			config.joinOnClick = rbJoin.Checked;
			LFSQuery.xpsp2_wait = !config.disableWait;
			LFSQuery.THREAD_WAIT = config.queryWait;
			config.startPS = cbUsePS.Checked;
			config.psPath = txtPSPath.Text;
			try {
				config.psInsimPort = Convert.ToInt32(txtInsimPort.Text);
			} catch (Exception ex) {
				config.psInsimPort = 29999;
			}
		}

		void RemoveFromFavouritesToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			ServerListItem item = lvFavourites.GetSelectedServer();
			if (item == null)
				return;
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
							MessageBox.Show("Your version is up to date.", "", MessageBoxButtons.OK);
						}
					} else {
						if (MessageBox.Show("There is a new version available, do you wish to be taken to the download page?", "Browse For Speed", MessageBoxButtons.YesNo) == DialogResult.Yes){
							System.Diagnostics.Process.Start(download_url);
						}
					}
				}
			} catch (Exception e) {
				if (botherUser) {
					MessageBox.Show("There was an error, perhaps our site is down. Try again later!", "Error!", MessageBoxButtons.OK);
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
			txtPSPath.Text = openFileDialogPS.FileName;
		}

		void TxtInsimPortLeave(object sender, System.EventArgs e)
		{
			try {
				int value = Convert.ToInt32(txtInsimPort.Text);
				if (value > 65535) throw new Exception();
			} catch (Exception ex) {
				MessageBox.Show(txtInsimPort.Text + " is not a valid port number", "", MessageBoxButtons.OK);
				txtInsimPort.Focus();
			}

		}

		void CbUsePSCheckStateChanged(object sender, System.EventArgs e)
		{
			txtPSPath.Enabled = ((CheckBox)sender).Checked;
			txtInsimPort.Enabled = ((CheckBox)sender).Checked;
			btnBrowsePS.Enabled = ((CheckBox)sender).Checked;
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
				}
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
			btnJoinFriend.Enabled = (lvFriends.SelectedItems.Count > 0 && lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text != "Offline");
		}
		
		void JoinFriendClick(object sender, System.EventArgs e)
		{
			if ((lvFriends.SelectedItems.Count > 0) || (lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text == "Offline"))
				return;
			string hostname = lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text;
			LoadLFS(hostname, "S2", edtPasswordMain.Text);
		}
		
		void BtnAddFriendClick(object sender, System.EventArgs e)
		{			
			AddFriend(edtFriendName.Text, true);
			DisplayFriends();
			lvFriends.Sort();
			edtFriendName.Text = "";
		}
		
		void RemoveFriendToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			if (lvFriends.SelectedItems.Count == -1)
				return;
			string name =  lvFriends.Items[lvFriends.SelectedItems[0].Index].Text;
			friendList.RemoveAt(friendList.IndexOf(name));
			lvFriends.Items.RemoveByKey(name);
			
		}
		
		void ContextMenuFriendsOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			bool enabled = (lvFriends.SelectedItems.Count > 0);
			removeFriendToolStripMenuItem.Enabled = enabled;
			joinServerMenuFriends.Enabled = enabled && (lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text != "Offline");
		}
		
		void LvFriendsDoubleClick(object sender, System.EventArgs e)
		{
			if ((lvFriends.SelectedItems.Count < 0) || (lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text == "Offline"))
				return;
			string hostname = lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[1].Text;
			string friend = lvFriends.Items[lvFriends.SelectedItems[0].Index].SubItems[0].Text;
			if (MessageBox.Show("Do you want to join " + friend + " at " + hostname + "?", "Join Friend?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
				LoadLFS(hostname, "S2", edtPasswordMain.Text);
			}
		}
		
		void EdtFriendNameKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				AddFriend(edtFriendName.Text, true);
				lvFriends.Sort();
				edtFriendName.Text = "";
			}
			
		}
		
		void MainFormLoad(object sender, System.EventArgs e)
		{
			cars = new CheckBox[LFSQuery.CAR_BITS.Length];
			groups = new Button[LFSQuery.CAR_GROUP_BITS.Length];
			for (int i = 0; i < LFSQuery.CAR_BITS.Length; ++i) { //create car checkboxes
				cars[i] = new CheckBox();
				cars[i].Parent = groupBox2;
				if ((i % 2) == 0) {
					cars[i].Left = 8;
					cars[i].Top = (i/2 * 20) + 20;
				} else {
					cars[i].Top = cars[i-1].Top;
					cars[i].Left = 62;
				}
				cars[i].Text = LFSQuery.CAR_NAMES[i];
				cars[i].Width = 50;
				cars[i].Height = 22;
				cars[i].ThreeState = true;
				cars[i].CheckState = CheckState.Indeterminate;
			}

			for (int i = 0; i < LFSQuery.CAR_GROUP_BITS.Length; ++i) { //create group buttons
				groups[i] = new Button();
				groups[i].Parent = groupBox2;
				if ((i % 2) == 0) {
					groups[i].Left = 8;
					groups[i].Top = (i/2 * 30) + 235;
				} else {
					groups[i].Top = groups[i-1].Top;
					groups[i].Left = 62;
				}
				groups[i].Text = LFSQuery.CAR_GROUP_NAMES[i];
				groups[i].Width = 42;
				groups[i].Height = 23;
				groups[i].Tag = i;
				groups[i].Click += new EventHandler(CarsGroupButtonClick);
			}
            friendList = new List<string>();
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
			if (loadedconf) {
				//query wait
				cbQueryWait.Checked = config.disableWait;
				queryWait.Enabled = !config.disableWait;
				LFSQuery.xpsp2_wait = !config.disableWait;
				LFSQuery.THREAD_WAIT = config.queryWait;
				queryWait.Value = config.queryWait;
				//new version
				cbNewVersion.Checked = config.checkNewVersion;
				//Pit spotter
				cbUsePS.Checked = config.startPS;
				txtPSPath.Text = config.psPath;
				txtInsimPort.Text = config.psInsimPort != 0 ? config.psInsimPort.ToString() : "29999";
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
			cbTracks.SelectedIndex = 0;
			cbPing.SelectedIndex = 2;
			lvMain.Filter(FilterType.Ping, 100);

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
		if (columnName == "Ping") { //if we're sorting the ping column
			compareResult = ObjectCompare.Compare(Convert.ToInt32(listviewX.SubItems[ColumnToSort].Text), Convert.ToInt32(listviewY.SubItems[ColumnToSort].Text));
		} else if (columnName == "Connections") { //sorting connection column
				int playersX = Convert.ToInt32(listviewX.SubItems[ColumnToSort].Text.Split('/')[0]);
				int playersY = Convert.ToInt32(listviewY.SubItems[ColumnToSort].Text.Split('/')[0]);
				compareResult = ObjectCompare.Compare(playersX, playersY);
		} else  if (columnName == "Server" && (listviewX.SubItems[ColumnToSort].Text == "Offline" || listviewY.SubItems[ColumnToSort].Text == "Offline")) {
				compareResult = (listviewX.SubItems[ColumnToSort].Text == "Offline" ? listviewY.SubItems[ColumnToSort].Text == "Offline" ? 0 : 1 : -1);
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

