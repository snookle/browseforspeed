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
using System.Text.RegularExpressions;
using Microsoft.Win32;
using libbrowseforspeed;
using System.Threading;

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
		public bool configValid;
		public int queryWait;
		
		public Configuration(string filename)
		{
			this.filename = filename;
			this.Load();
		}
		
		public void Load()
		{
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
				tr.Close();
				configValid = true;
			} catch (FileNotFoundException fnfe) {
				MessageBox.Show("No configuration data found.\nPlease setup your configuration now.", MainForm.appTitle, MessageBoxButtons.OK);
				configValid = false;
			}
		}
		
		public void Save()
		{
			try {
				TextWriter tw = new StreamWriter(filename);
				//tw.WriteLine(this.userName);
				//tw.WriteLine(encrypt(this.password));
				tw.WriteLine(this.lfsPath);
				tw.WriteLine(this.disableWait.ToString());
				tw.WriteLine(this.checkNewVersion.ToString());
				tw.WriteLine(this.queryWait.ToString());
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
		public static String appTitle = "Browse For Speed";
		static String configFilename = Application.StartupPath + "\\config.cfg";
		static String favFilename = Application.StartupPath + "\\favourite.servers";
		
		private CheckBox[] cars;
		private Button[] groups;
		
		private LFSQuery q;
		private ListViewColumnSorter lvwColumnSorter;
				
		private ArrayList serverList;
		private ArrayList favServerList;
				
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
			lvwColumnSorter = new ListViewColumnSorter();
			lvwColumnSorter.SortColumn = 1;
			lvwColumnSorter.Order = SortOrder.Ascending;
			lvMain.ListViewItemSorter = lvwColumnSorter;
			lvMain.Sort();
			lvFavourites.ListViewItemSorter = lvwColumnSorter;
			lvFavourites.Sort();
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
			config = new Configuration(configFilename);
			ReadFav();
			//if we have previous config data
			if (config.configValid){
				cbQueryWait.Checked = config.disableWait;
				queryWait.Enabled = !config.disableWait;
				LFSQuery.xpsp2_wait = !config.disableWait;
				LFSQuery.THREAD_WAIT = config.queryWait;
				cbNewVersion.Checked = config.checkNewVersion;
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
			} else {
				//config not valid, make em pay!
				tabControl.SelectTab(2);
				config.disableWait =  false;
				config.checkNewVersion = false;
				this.lastTabSelected = 2;
				if (pathList.Items.Count > 0)
					pathList.SelectedIndex = 0;
			}
			
			cbTracks.SelectedIndex = 0;
			
		}

		void CodeCars(out ulong compulsory, out ulong illegal)
		{
			compulsory = 0;
			illegal = 0;
			for (int i = 0; i < LFSQuery.CAR_BITS.Length; ++i) {
				if (cars[i].CheckState == CheckState.Checked) {
					compulsory ^= LFSQuery.CAR_BITS[i];
				} else if (cars[i].CheckState == CheckState.Checked) {
					illegal ^= LFSQuery.CAR_BITS[i]; 
				}
			}
		}
		
		public static string CarsToString(ulong c)
		{
			String result = "";
			for (int i = 0; i < LFSQuery.CAR_GROUP_BITS.Length; ++i) {
				if (((c & LFSQuery.CAR_GROUP_BITS[i]) != 0) && c >= LFSQuery.CAR_GROUP_BITS[i]) { //is this right???
					result += LFSQuery.CAR_GROUP_NAMES[i] + ", ";					
					c -= LFSQuery.CAR_GROUP_BITS[i];
				}
			}			
			for (int i = 0; i < LFSQuery.CAR_BITS.Length; ++i) {
				if ((c & LFSQuery.CAR_BITS[i]) != 0) {
					result += LFSQuery.CAR_NAMES[i] + ", ";					
					c -= LFSQuery.CAR_BITS[i];
				}
			}
			if (result.Length == 0)
				return result;
			return result.Remove(result.Length - 2, 2);
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
					q.query(compulsory, illegal, "browseforspeed", 0);
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

		public void DisplayServer(ServerInformation info, bool isFavQuery)
		{
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
					string filter = isFavQuery ? "" : GetTrackFilter(cbTracks.Text);
					if (info.success){
						numServersDone++;
						if (info.track.Contains(filter)){
							DisplayServer(info, isFavQuery);
						}
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
			String path = config.lfsPath;
			FormWindowState ws = this.WindowState;
			LFSQuery.stopQuerying();
			try{
				this.WindowState = FormWindowState.Minimized;
				Process p = new Process();
				p.StartInfo.FileName = path;
				p.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
				p.StartInfo.Arguments = "/join=" + hostName + " /mode=" + mode + "/pass=" +password;
				p.Start();
				p.WaitForExit();
			} catch (Exception ex) {
				MessageBox.Show("Error executing: "+path+"\n"+ex.Message+"\nRecheck your configuration.", appTitle, MessageBoxButtons.OK);
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
		
		void btnJoinClick(object sender, System.EventArgs e)
		{
			ListView.SelectedListViewItemCollection coll = lvMain.SelectedItems;
			if (coll.Count < 1) return;
			String serverName = coll[0].Text;

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
			MessageBox.Show("Browse For Speed - http://browseforspeed.whatsbeef.net\nCopyright 2006 Richard Nelson, Philip Nelson, Ben Kenny\n\nYou may modify and redistribute the program under the terms of the GPL (version 2 or later).\nA copy of the GPL is contained in the 'COPYING' file distributed with Browse For Speed.\nWe provide no warranty for this program.\n\nIf you haven't already, go buy LFS S2 now - the LFS developers deserve your support!", "About", MessageBoxButtons.OK);
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
			lvFavourites.Items.Add((ListViewItem)coll[0].Clone());
			//write the host:ip out to file.
			foreach(ServerInformation info in serverList){
				if (info.hostname == coll[0].Text){
					favServerList.Add(info);
					WriteFav();
					break;
				}
			}			
		}
		
		void ReadFav()
		{
			try {
				TextReader tr = new StreamReader(favFilename);
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
			} catch (Exception e) {
				//MessageBox.Show(e.Message, appTitle, MessageBoxButtons.OK);
				//squash message :D
			}
		}
		
		void WriteFav()
		{
			try {
				StreamWriter sw = new StreamWriter(favFilename);
				foreach (ServerInformation info in favServerList){
					sw.Write(info.host.ToString() + " ");
					sw.WriteLine(info.hostname.Trim());
				}
    			sw.Close();
			}
    		catch (Exception ex) {
				MessageBox.Show("An error writing to the favourite server file occured: " +ex.Message, "Browsw For Speed", MessageBoxButtons.OK);
			}
			
		}
		
		void ContextMenuFavOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ListView.SelectedListViewItemCollection coll = lvFavourites.SelectedItems;
			contextMenuFav.Enabled = (coll.Count > 0);
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
		
		void lvFavouritesSelectedIndexChanged(object sender, System.EventArgs e)
		{
			btnJoinFav.Enabled = (lvFavourites.SelectedItems.Count > 0 && lvFavourites.Items[0].SubItems.Count > 1);
		}
		
		void btnJoinFavClick(object sender, System.EventArgs e)
		{
			ListView.SelectedListViewItemCollection coll = lvFavourites.SelectedItems;
			if (coll.Count < 1) return;
			String serverName = coll[0].Text;
			LoadLFS(serverName, "S2", edtPasswordMain.Text);
		}
		
		void ViewServerInformationToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			ToolStripMenuItem l = (ToolStripMenuItem)sender;			
			ListView.SelectedListViewItemCollection coll;
			if (l.Name == "viewServerInformationMain") {
				coll = lvMain.SelectedItems;
			} else {
				coll = lvFavourites.SelectedItems;
			}
			if (coll.Count < 1) return;
			s.SetInfo(null);
			foreach (ServerInformation info in (l.Name == "viewServerInformationMain") ? serverList : favServerList){
				if (info.hostname == coll[0].Text){
					s.SetInfo(info);
					break;
				}
			}
			if (s.ShowDialog(this) == DialogResult.OK) {
				LoadLFS(s.GetInfo().hostname, "S2", edtPasswordMain.Text);
			}			
		}
		
		void MainFormFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
			LFSQuery.stopQuerying();
			this.exiting = true;
			this.Hide();
		}

		
		String GetTrackFilter(string track)
		{
			string filter = "";
			switch (track){
				case "All" : filter  = ""; break;
				case "Blackwood" : filter = "BL"; break;
				case "South City" : filter = "SO"; break;
				case "Fern Bay" : filter = "FE"; break;
				case "Autocross" : filter = "AU"; break;
				case "Kyoto Ring" : filter = "KY"; break;
				case "Westhill" : filter = "WE"; break;
				case "Aston" : filter = "AS"; break;
			}
			return filter;
			
		}
		
		void ComboBoxTracksSelectedIndexChanged(object sender, System.EventArgs e)
		{
			string filter = GetTrackFilter(cbTracks.Text);
			lvMain.Items.Clear();
			try{
				foreach (ServerInformation info in serverList){
					if (info.track.Contains(filter)){
						DisplayServer(info, false);
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
				config.lfsPath = pathList.Items[pathList.SelectedIndex].ToString();
				config.disableWait = cbQueryWait.Checked;
				config.checkNewVersion = cbNewVersion.Checked;
				config.queryWait = (int)queryWait.Value;
				LFSQuery.xpsp2_wait = !config.disableWait;
				LFSQuery.THREAD_WAIT = config.queryWait;
				config.Save();
			}
			this.lastTabSelected = tabControl.SelectedIndex;
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
		
		static string bfs_version = "1";
		static string download_url = "http://browseforspeed.whatsbeef.net";
		
		public static void versionCheck(bool botherUser) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://browseforspeed.whatsbeef.net/versioncheck.pl");
			request.Timeout = 3000;
			try {
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				if (response.StatusCode == HttpStatusCode.OK) {
					Stream stream = response.GetResponseStream();
					byte[] buf = new byte[1];
					stream.Read(buf, 0, buf.Length);
					if (buf[0] == bfs_version[0]) {
						if (botherUser) {
							MessageBox.Show("Your version is up to date.", "", MessageBoxButtons.OK);
						}
					} else {
						if (MessageBox.Show("There is a new version avaliable, do you wish to be taken to the download page?", "Browse For Speed", MessageBoxButtons.YesNo) == DialogResult.Yes){
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
		if (ColumnToSort == 1) { //if we're sorting the ping column
			compareResult = ObjectCompare.Compare(Convert.ToInt32(listviewX.SubItems[ColumnToSort].Text), Convert.ToInt32(listviewY.SubItems[ColumnToSort].Text));
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
			//suppress any exceptions relating to indexes out of range (stupid bug I dunno how to fix, or what's causing it) :)
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
