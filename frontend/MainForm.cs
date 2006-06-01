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
using Microsoft.Win32;
using libbrowseforspeed;
using System.Threading;
using System.Xml;

namespace LFS_ServerBrowser
{
	public class Configuration
	{
		public string userName;
		public string password;
		public string lfsPath;
		public string filename;
		public bool disableWait;
		public bool checkNewVersion;
		public int queryWait;
		public bool joinOnClick;
		public bool startPS;
		public string psPath;
		public int psInsimPort;

		public Configuration(string filename) {
			this.filename = filename;			
		}

		public bool LoadXML() {
			try {
				XmlDocument doc = new XmlDocument();				
				doc.Load(filename);
				XmlNodeList list = doc.GetElementsByTagName("bfsconfig");
				try {
					lfsPath = ((XmlElement)list[0]).GetElementsByTagName("exepath")[0].FirstChild.Value;
				} catch (Exception e) { lfsPath = ""; }
				try {
					disableWait = ((XmlElement)list[0]).GetElementsByTagName("qwait")[0].Attributes["enabled"].Value == "True";
				} catch (Exception e) { disableWait = false; }
				try {
					queryWait = Convert.ToInt32(((XmlElement)list[0]).GetElementsByTagName("qwait")[0].FirstChild.Value);
				} catch (Exception e) { queryWait = 150; }
				try {
					checkNewVersion = ((XmlElement)list[0]).GetElementsByTagName("checkversion")[0].FirstChild.Value == "True";
				} catch (Exception e) { checkNewVersion = false; }
				try {
					XmlNode spot = ((XmlElement)list[0]).GetElementsByTagName("spotter")[0];
					startPS = (spot.Attributes["enabled"].Value == "True");
					psInsimPort = Convert.ToInt32(((XmlElement)spot).GetElementsByTagName("insimport")[0].FirstChild.Value);
					psPath = ((XmlElement)spot).GetElementsByTagName("path")[0].FirstChild.Value;
				} catch (Exception e) {
					startPS = false;
					psInsimPort = 29999;
					psPath = "";
				}
				try {
					joinOnClick = ((XmlElement)list[0]).GetElementsByTagName("listclick")[0].FirstChild.Value == "join";
				} catch (Exception e) { joinOnClick = true; }
				return true;
			} catch (FileNotFoundException fe) {
				MessageBox.Show("No existing configuration data found.\nPlease set this up now.", "", MessageBoxButtons.OK);
				return false;
			}
			catch (Exception e) {
				MessageBox.Show("Error loading configuration: " + e.Message + "\nA copy was saved as "+filename +".backup", "", MessageBoxButtons.OK);
				File.Copy(filename, filename +".backup", true);
				return false;
			}
		}


		public bool Load() {
			try {
				TextReader tr = new StreamReader(this.filename);
				//this.userName = tr.ReadLine();
				//this.password = decrypt(tr.ReadLine());
				this.lfsPath = tr.ReadLine();
				this.disableWait = (tr.ReadLine() == "True");
				this.checkNewVersion = (tr.ReadLine() == "True");
				try {
					this.queryWait = Convert.ToInt32(tr.ReadLine());
				} catch (Exception e) {
					this.queryWait = 150;
				}
				try {
					this.joinOnClick = (tr.ReadLine() != "view");
				} catch (Exception e) {
					this.joinOnClick = true;
				}
				try {
					this.psInsimPort = Convert.ToInt32(tr.ReadLine());
				} catch (Exception e) {
					this.psInsimPort = 29999;
				}
				this.psPath = tr.ReadLine();
				this.startPS = (tr.ReadLine() == "True");
				tr.Close();
				return true;
			} catch (Exception e) {
				return false;
			}
		}

		public void Save(string filename)
		{
			try {				
				XmlTextWriter tw = new XmlTextWriter(filename, null);
				tw.Formatting = Formatting.Indented;
				tw.WriteStartDocument();
				tw.WriteStartElement("bfsconfig");
				tw.WriteAttributeString("version", "1");
				tw.WriteElementString("exepath", lfsPath);
				tw.WriteStartElement("qwait");
				tw.WriteAttributeString("enabled", disableWait.ToString());
				tw.WriteString(queryWait.ToString());
				tw.WriteEndElement();				
				tw.WriteElementString("checkversion", checkNewVersion.ToString());
				tw.WriteStartElement("spotter");
				tw.WriteAttributeString("enabled", startPS.ToString());
				tw.WriteElementString("insimport", psInsimPort.ToString());
				tw.WriteElementString("path", psPath);				
				tw.WriteEndElement();
				tw.WriteElementString("listclick", joinOnClick ? "join" : "view");
				tw.WriteEndElement();
				tw.Close();
			}
			catch (Exception ex) {
				MessageBox.Show("There was an error writing to the config file:" +ex.Message, MainForm.appTitle, MessageBoxButtons.OK);
			}

		}


	}
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm
	{
		static string bfs_version = "0.3";
		static string version_check = "4";
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

		private ArrayList serverList;
		private ArrayList favServerList;
		private ArrayList friendList;

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
			serverList = new ArrayList();
			favServerList = new ArrayList();
			friendList = new ArrayList();
			lvwColumnSorter = new ListViewColumnSorter();
			lvwColumnSorter.SortColumn = 1;
			lvwColumnSorter.Order = SortOrder.Ascending;
			lvMain.ListViewItemSorter = lvwColumnSorter;
			lvMain.Sort();
			lvFavourites.ListViewItemSorter = lvwColumnSorter;
			lvFavourites.Sort();
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

		public string CarsToString(ulong c) {
			StringBuilder carNames = new StringBuilder();
			Array cars = LFSQuery.getCarNames(c);
			foreach (string car in cars) {
				carNames.Append(car + ", ");
			}
			if (carNames.Length == 0) {
				return carNames.ToString();
			}
			return carNames.Remove(carNames.Length - 2, 2).ToString();			
		}

		//nice helper method to set UI properties across threads.
		delegate void SetValueDelegate(Object obj, Object val, Object[] index);

		public void SetControlProperty(Control ctrl, String propName, Object val)
		{
			System.Reflection.PropertyInfo propInfo = ctrl.GetType().GetProperty(propName);
			Delegate dgtSetValue = new SetValueDelegate(propInfo.SetValue);
      		ctrl.Invoke(dgtSetValue, new Object[3] { ctrl, val, /*index*/null });
		}

		void MakeQuery(bool isFav)
		{
			SetControlProperty(buttonRefreshFav, "Text", "&Stop Refresh");
			SetControlProperty(btnRefreshMain, "Text", "&Stop Refresh");
			try {
				if (isFav) {
					if (favServerList.Count > 0) {
						IPEndPoint[] stupidArray = new IPEndPoint[favServerList.Count];
						for (int i = 0; i < favServerList.Count; i++){
							stupidArray[i] = ((ServerInformation)favServerList[i]).host;
						}
						q.query(0, 0, "browseforspeed", stupidArray, 0);
					}
				} else {
					ulong compulsory;
					ulong illegal;
					CodeCars(out compulsory, out illegal);
					q.query(compulsory, illegal, "browseforspeed", 0, cbEmpty.Checked);
				}
			} catch(Exception e) {
					MessageBox.Show("Unable to contact the Master Server. Perhaps it is down, or your firewall is not configured properly." , "Unable to contact Master Server!", MessageBoxButtons.OK);
				}

			if (exiting) return;
			//invoke
			this.Invoke(new MethodInvoker(((isFav == true ? lvFavourites : lvMain).Sort)));
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
				LFSQuery.queried -= new ServerQueried(queryFavEventListener);
				LFSQuery.queried += (b.Name == "btnRefreshMain") ? new ServerQueried(queryMainEventListener) : new ServerQueried(queryFavEventListener);
				totalServers = 0;
				(b.Name == "btnRefreshMain" ? lvMain : lvFavourites).Items.Clear();
				if (b.Name == "btnRefreshMain"){
					serverList.Clear();
					btnJoinMain.Enabled = false;
					t = new Thread(new ThreadStart(MainQuery));
				} else
					t = new Thread(new ThreadStart(FavQuery));
  				t.Start();
			} else {
				LFSQuery.stopQuerying();
				btnRefreshMain.Enabled = false;
				buttonRefreshFav.Enabled = false;
			}
		}

		public void DisplayServer(ServerInformation info, string trackFilter, bool hideEmpty, bool isFavQuery)
		{
			if (!info.track.Contains(trackFilter)) return;
			if (hideEmpty && info.players == 0 && !isFavQuery) return;
			string serverName;
			info.hostname = serverName = LFSQuery.removeColourCodes(info.hostname);
			string cars = CarsToString(info.cars);
			ListViewItem lvi = (isFavQuery == false ? lvMain : lvFavourites).Items.Add(info.host.ToString());
			lvi.SubItems.Insert(0, new ListViewItem.ListViewSubItem(lvi, serverName));
			lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, info.ping.ToString()));
			int columnOffset = 0;
			if (isFavQuery){
				columnOffset = 1;
			} else {
				lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, info.passworded == true ? "Yes" : "No"));
			}
			lvi.SubItems.Insert(3 - columnOffset, new ListViewItem.ListViewSubItem(lvi, info.players.ToString() +"/" + info.slots.ToString()));
			lvi.SubItems.Insert(4 - columnOffset, new ListViewItem.ListViewSubItem(lvi, RulesToString(info.rules)));
			lvi.SubItems.Insert(5 - columnOffset, new ListViewItem.ListViewSubItem(lvi, info.track));			
			lvi.SubItems.Insert(6 - columnOffset, new ListViewItem.ListViewSubItem(lvi, cars));

		}

		public void addServerToList(ServerInformation info, ListView l, bool isFavQuery)
		{
			if (this.exiting) return;
			try{
				// Make sure we're on the right thread
				if(this.InvokeRequired == false) {
					if (totalServers == 0) {
						totalServers = info.totalServers;
						numQueried = 0;
						numServersDone = 0;
						numServersRefused = 0;
						numServersNoReply = 0;
					}
					numQueried++;
					string filter = cbTracks.Text == "All Tracks" ? "" : cbTracks.Text;					
					if (info.success){
						numServersDone++;
						DisplayServer(info, filter, cbEmpty.Checked, isFavQuery);
						if (!isFavQuery) serverList.Add(info);
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
			    	//add the server asynchonously
			    	AddServerDelegate addServer = new AddServerDelegate(addServerToList);
			    	this.BeginInvoke(addServer, new object[] {info, l, isFavQuery});
				}
			} catch(Exception e){ }
  		}

		delegate void AddServerDelegate(ServerInformation info, ListView l, bool isFavQuery);

		public void queryMainEventListener(object o, ServerInformation info, object cbObj) {
			if ((int)cbObj != 0) return;
			if (info != null) {
				addServerToList(info, lvMain, false);
			}
		}

		public void queryFavEventListener(object o, ServerInformation info, object cbObj) {
			if ((int)cbObj != 0) return;
			if (info != null) {
				addServerToList(info, lvFavourites, true);
			}
		}


		void CloseToolStripMenuItemClick(object sender, System.EventArgs e) {
			Close();
		}

		void lvMainColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Determine if clicked column is already the column that is being sorted.
			if ( e.Column == lvwColumnSorter.SortColumn ) {
				// Reverse the current sort direction for this column.
				if (lvwColumnSorter.Order == SortOrder.Ascending) {
					lvwColumnSorter.Order = SortOrder.Descending;
				} else {
					lvwColumnSorter.Order = SortOrder.Ascending;
				}
			} else {
				// Set the column number that is to be sorted; default to ascending.
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
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
			ListView.SelectedListViewItemCollection sel;
			bool writefav = false;
			if (sender is Button) {
				if (((Button)sender).Name == "btnJoinMain") {
					sel = lvMain.SelectedItems;
				} else {
					sel = lvFavourites.SelectedItems;
					writefav = true;
				}
			} else if (sender is ToolStripMenuItem) {
				if (((ToolStripMenuItem)sender).Name == "joinServerToolStripMenuItem") {
					sel = lvMain.SelectedItems;
				} else {
					sel = lvFavourites.SelectedItems;
					writefav = true;
				}
			}else {
				sel = ((ListView)sender).SelectedItems;
			}
			if (sel.Count < 1) { return; }
			String serverName = sel[0].Text;
			if (writefav) {
				foreach(ServerInformation info in favServerList) { //this is stupid. need to fix.
					if (info.hostname == sel[0].Text) {
						info.password = edtPasswordMain.Text;
						WriteFav();
						break;
					}
				}

			}			
			LoadLFS(serverName, "S2", edtPasswordMain.Text);			
		}

		void lvMainSelectedIndexChanged(object sender, System.EventArgs e)
		{
			btnJoinMain.Enabled = (lvMain.SelectedItems.Count > 0);
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
			ListView.SelectedListViewItemCollection coll = lvMain.SelectedItems;
			contextMenuBrowser.Enabled = (coll.Count > 0);
		}

		void ToolStripMenuItem1Click(object sender, System.EventArgs e)
		{
			ListView.SelectedListViewItemCollection coll = lvMain.SelectedItems;
			if (coll.Count < 1) return;
			//lvFavourites.Items.Add((ListViewItem)coll[0].Clone());
			//write the host:ip out to file.
			foreach(ServerInformation info in serverList){
				if (info.hostname == coll[0].Text){
					DisplayServer(info, "", false, true);
					favServerList.Add(info);
					WriteFav();
					break;
				}
			}
		}

		bool ReadXMLFav(string filename) {
			try {
				XmlDocument doc = new XmlDocument();
				doc.Load(filename);
				XmlNodeList list = doc.GetElementsByTagName("favourites");
				list = ((XmlElement)list[0]).GetElementsByTagName("favourite");
				foreach (XmlElement favourite in list) {
					try {
						ServerInformation info = new ServerInformation();					
						info.host = new IPEndPoint(IPAddress.Parse(favourite.GetElementsByTagName("ip")[0].FirstChild.Value), Convert.ToInt32(favourite.GetElementsByTagName("port")[0].FirstChild.Value));
						info.hostname = favourite.GetElementsByTagName("name")[0].FirstChild.Value;					
						try {
							info.password = favourite.GetElementsByTagName("password")[0].FirstChild.Value;
						} catch (Exception e) {
							info.password = "";
						}
						favServerList.Add(info);
						lvFavourites.Items.Add(info.hostname);
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
				favServerList.Clear();
				lvFavourites.Items.Clear();
				string s = tr.ReadToEnd();
				foreach(String server in s.Split(new Char[]{'\n'}))
				{
					if (server != ""){
						String[] favInfoTmp = server.Split(new Char[]{' '}, 2);
						String[] ipAddress = favInfoTmp[0].Split(':');
						ServerInformation info = new ServerInformation();
						info.hostname = favInfoTmp[1].Trim();
						info.host = new IPEndPoint(IPAddress.Parse(ipAddress[0]), Convert.ToInt32(ipAddress[1]));
						favServerList.Add(info);
						lvFavourites.Items.Add(info.hostname);
					}
				}
				tr.Close();
				return true;
			} catch (Exception e) {
				return false;
			}
		}

		void WriteFav() {
			try {
				XmlTextWriter tw = new XmlTextWriter(favXMLFilename, null);
				tw.Formatting = Formatting.Indented;
				tw.WriteStartDocument();
				tw.WriteStartElement("favourites");
				tw.WriteAttributeString("version", "1");
				foreach (ServerInformation info in favServerList){
					tw.WriteStartElement("favourite");
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

		public void AddFriend(string name, bool writeToFile)
		{
			if (friendList.IndexOf(name) == -1)
				friendList.Add(name);
			if (lvFriends.Items.IndexOfKey(name) == -1) {
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
				}
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
			ListView.SelectedListViewItemCollection coll = lvFavourites.SelectedItems;
			contextMenuFav.Enabled = (coll.Count > 0);
		}

		public String RulesToString(ulong rules)
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

		void lvFavouritesSelectedIndexChanged(object sender, System.EventArgs e) {
			btnJoinFav.Enabled = (lvFavourites.SelectedItems.Count > 0 && lvFavourites.Items[0].SubItems.Count > 1);
		}

		void ViewServerInformationToolStripMenuItemClick(object sender, System.EventArgs e) {
			ListView.SelectedListViewItemCollection coll;
			ServerInformation i;
			ArrayList myList;
			if (sender is ListView) {
				coll = ((ListView)sender).SelectedItems;
				myList = (((ListView)sender).Name == "lvMain") ? serverList : favServerList;
			} else {
				ToolStripMenuItem l = (ToolStripMenuItem)sender;
				if (l.Name == "viewServerInformationMain") {
					coll = lvMain.SelectedItems;
					myList = serverList;
				} else {
					coll = lvFavourites.SelectedItems;
					myList = favServerList;
				}
			}
			if (coll.Count < 1) return;
			s.SetInfo(null);
			foreach (ServerInformation info in myList) {
				if (info.hostname == coll[0].Text) {
					s.SetInfo(info);
					if (s.ShowDialog(this) == DialogResult.OK) {
						info.password = s.GetInfo().password;
						WriteFav();
						LoadLFS(s.GetInfo().hostname, "S2", s.GetInfo().password);
					}
					break;
				}
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
			lvMain.Items.Clear();
			try{
				foreach (ServerInformation info in serverList){
					if (info.track.Contains(filter)){
						DisplayServer(info, filter, cbEmpty.Checked, false);
					}
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "", MessageBoxButtons.OK);
			}
		}

		void btnFindUserClick(object sender, System.EventArgs e)
		{
			string hostname = q.findUser("wabz", edtFindUserMain.Text);
			if (hostname != null){
				hostname = LFSQuery.removeColourCodes(hostname);
				if (MessageBox.Show("Join " + edtFindUserMain.Text + " at this host?\n" + hostname, appTitle, MessageBoxButtons.YesNo) == DialogResult.Yes){
					LoadLFS(hostname, "S2", "");
				}
			} else {
				MessageBox.Show("User was not found online", appTitle, MessageBoxButtons.OK);
			}
		}

		void TabControlSelectedIndexChanged(object sender, System.EventArgs e)
		{
			//if we've just come from the configuration panel to another one
			if (this.lastTabSelected == 2) {
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
			ListView.SelectedListViewItemCollection coll = lvFavourites.SelectedItems;
			if (coll.Count < 1) return;
			foreach(ServerInformation info in favServerList){
				if (info.hostname == coll[0].Text){
					favServerList.Remove(info);
					WriteFav();
					lvFavourites.Items.Remove((ListViewItem)coll[0]);
					break;
				}
			}

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

				} else if (lvMain.Font.Size > 1 && (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)) {
					lvMain.Font = new Font(lvMain.Font.FontFamily.Name, lvMain.Font.Size - 1);
					lvFavourites.Font = new Font(lvFavourites.Font.FontFamily.Name, lvFavourites.Font.Size - 1);
				}
			}
		}
		
		void BtnRefreshFriendClick(object sender, System.EventArgs e)
		{
			lvFriends.Items.Clear();
			foreach(string friend in friendList){
				AddFriend(friend, false);
			}
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
		
		void EdtFriendNameKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {

			
		}
		
		void EdtFriendNameKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				AddFriend(edtFriendName.Text, true);
				lvFriends.Sort();
				edtFriendName.Text = "";
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
