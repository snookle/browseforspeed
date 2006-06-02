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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using Microsoft.Win32;

using LFS.BrowseForSpeed.Network;

namespace LFS.BrowseForSpeed.Windows.Forms
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm
    {

        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            Application.Run(new MainForm());
        }

        public MainForm()
        {
            InitializeComponent();
        }

        #region Protected Methods
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        
            MasterServerQuery.Instance.Stop();
            _exiting = true;
            UpdateConfig();
            config.Save(configXMLFilename);
            WriteFav();
            WriteFriends();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            statusVersion.Text = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            statusVersion.Click += new EventHandler(StatusVersion_Click);

            carChecks = new CheckBox[MasterServerQuery.CAR_BITS.Length];
            carGroupChecks = new RadioButton[MasterServerQuery.CAR_GROUP_BITS.Length];
            for (int i = 0; i < MasterServerQuery.CAR_BITS.Length; ++i)
            { 
                //create car checkboxes
                carChecks[i] = new CheckBox();
                carChecks[i].Parent = CarsGroup;
                if ((i % 2) == 0)
                {
                    carChecks[i].Left = 8;
                    carChecks[i].Top = (i / 2 * 18) + 14;
                }
                else
                {
                    carChecks[i].Top = _carChecks[i - 1].Top;
                    carChecks[i].Left = 62;
                }
                carChecks[i].Text = MasterServerQuery.CAR_NAMES[i];
                carChecks[i].Width = 50;
                carChecks[i].Height = 22;
                carChecks[i].ThreeState = true;
                carChecks[i].CheckState = CheckState.Indeterminate;
            }

            for (int i = 0; i < MasterServerQuery.CAR_GROUP_BITS.Length; ++i)
            { 
                //create group radio buttons
                carGroupChecks[i] = new RadioButton();
                carGroupChecks[i].Parent = CarGroups;
                if ((i % 2) == 0)
                {
                    carGroupChecks[i].Left = 8;
                    carGroupChecks[i].Top = (i / 2 * 18) + 14;
                }
                else
                {
                    carGroupChecks[i].Top = _carGroupChecks[i - 1].Top;
                    carGroupChecks[i].Left = 62;
                }
                carGroupChecks[i].Text = MasterServerQuery.CAR_GROUP_NAMES[i];
                carGroupChecks[i].Width = 50;
                carGroupChecks[i].Height = 22;
                carGroupChecks[i].Checked = false;
                carGroupChecks[i].Tag = i;
                carGroupChecks[i].Click += new EventHandler(CarsGroupButtonClick);
            }

            serverList = new List<ServerInformation>();
            favoriteServerList = new List<ServerInformation>();
            friendList = new List<string>();
            lvwColumnSorter = new ListViewColumnSorter();
            lvwColumnSorter.SortColumn = 1;
            lvwColumnSorter.Order = SortOrder.Ascending;
            lvMain.ListViewItemSorter = _lvwColumnSorter;
            lvMain.Sort();
            lvFavorites.ListViewItemSorter = _lvwColumnSorter;
            lvFavorites.Sort();
            lvFriends.ListViewItemSorter = _lvwColumnSorter;
            lvwColumnSorter.SortColumn = 1;
            lvFriends.Sort();
            s = new ServerInformationForm(this);
            //search for LFS.exe
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\ShellNoRoam\\MUICache\\");
            foreach (string valuename in key.GetValueNames())
            {
                //insert all the found values into the list
                if (key.GetValue(valuename).ToString() == "LFS")
                {
                    pathList.Items.Add(valuename);
                }
            }
            //setup the config
            config = new Configuration(configXMLFilename);
            bool loadedconf = false;
            if (!config.LoadXML())
            {
                config = new Configuration(configFilename);
                if (!config.Load())
                {
                    //config not valid, take them to config screen
                    BrowserTabControl.SelectTab(BrowserTabControl.TabPages.IndexOf(ConfigurationTab));
                    config.disableWait = false;
                    config.checkNewVersion = false;
                    this.lastTabSelected = 2;
                    if (pathList.Items.Count > 0)
                    {
                        pathList.SelectedIndex = 0;
                    }
                }
                else
                {
                    loadedconf = true;
                    File.Delete(configFilename); //xml'ised!
                }
            }
            else
            {
                loadedconf = true;
            }
            if (!ReadXMLFav(favXMLFilename))
            {
                if (ReadFav(favFilename))
                {
                    File.Delete(favFilename); //xml'ised!
                }
            }
            //if we have previous config data
            if (loadedconf)
            {
                //query wait
                cbQueryWait.Checked = config.disableWait;
                queryWait.Enabled = !config.disableWait;
                MasterServerQuery.Instance.UseQueryDelay = !config.disableWait;
                MasterServerQuery.Instance.QueryDelayMilliseconds = config.queryWait;
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

                if (config.checkNewVersion)
                {
                    versionCheck(false);
                }
                int pathIndex = pathList.Items.IndexOf(config.lfsPath);
                //if we actually found some lfs.exe's and our config exists in the list
                if (pathIndex >= 0 && pathList.Items.Count > 0)
                {
                    pathList.SelectedIndex = pathIndex;
                }
                //if we found some, but it doesn't exist in the list, select the first one
                else if (pathIndex < 0 && pathList.Items.Count > 0)
                    pathList.SelectedIndex = 0;
                else
                {//we didn't find anything, something's gone wrong!@
                    pathList.Items.Add(config.lfsPath);
                    pathList.SelectedIndex = 0;
                }
            }
            Thread readFriends = new Thread(new ThreadStart(ReadFriends));
            readFriends.Start();
            //ReadFriends();
            TracksList.SelectedIndex = 0;
        }
        #endregion

        #region Private Event Handlers
        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBoxDialogForm about = null;
            try
            {
                about = new AboutBoxDialogForm();
                about.ShowDialog();
            }
            finally
            {
                about.Dispose();
                about = null;
            }
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            System.Text.StringBuilder sbErrMsg = new System.Text.StringBuilder();
            for (Exception exCurrent = e.Exception; exCurrent != null; exCurrent = exCurrent.InnerException)
            {
                sbErrMsg.Append(exCurrent.Message);
                sbErrMsg.Append(Environment.NewLine);
                sbErrMsg.Append(Environment.NewLine);
            }
            string sErrMsg = sbErrMsg.ToString();

            log.Fatal(sErrMsg);

            MessageBox.Show(string.Format("There are problems with {0}, version {1}.{2}Please check the following error messages: {3}.{4}For more information check the logs.",
                Application.ProductName,
                Application.ProductVersion,
                Environment.NewLine,
                sErrMsg,
                Environment.NewLine),
                "Application error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void CarsGroupButtonClick(object sender, System.EventArgs e)
        {
            if ((sender == null) || !(sender is RadioButton))
                return;

            int groupIndex = (int)((RadioButton)sender).Tag;
            ulong group = MasterServerQuery.CAR_GROUP_BITS[groupIndex];
            for (int i = 0; i < MasterServerQuery.CAR_BITS.Length; ++i)
            {
                if ((MasterServerQuery.CAR_BITS[i] & group) != 0 && (MasterServerQuery.CAR_BITS[i] & MasterServerQuery.CAR_GROUP_DONTCARE[groupIndex]) == 0)
                    _carChecks[i].CheckState = CheckState.Checked;
                else
                {
                    _carChecks[i].CheckState = CheckState.Indeterminate;
                    for (int j = 0; j < MasterServerQuery.CAR_GROUP_BITS.Length; ++j)
                    {
                        if ((MasterServerQuery.CAR_BITS[i] & MasterServerQuery.CAR_GROUP_DISALLOW[groupIndex]) != 0)
                        {
                            _carChecks[i].CheckState = CheckState.Unchecked;
                            break;
                        }
                    }
                }
            }
        }

        private void FavoriteHostQueriedCallback(object sender, ServerInformationEventArgs args)
        {
            if ((args == null) || (args.QueryType != QueryType.Favorite))
                return;

            if ((args != null) && (args.Server != null))
                AddServer(args.Server, QueryType.Favorite);
        }

        private void HostQueriedCallback(object sender, ServerInformationEventArgs args)
        {
            if ((args == null) || (args.QueryType != QueryType.Main))
                return;

            if ((args != null) && (args.Server != null))
                AddServer(args.Server, QueryType.Main);
        }

        void StatusVersion_Click(object sender, EventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region Constants
        private const string download_url = "http://www.browseforspeed.net";
        private const string VersionCheckUrl = "http://www.browseforspeed.net/versioncheck.pl";
		
		public static String appTitle = "Browse For Speed";
        #endregion

        #region Fields
        // TODO: Remove this shit; this is amateur level bullshit.
        static String configFilename = Application.StartupPath + "\\config.cfg";
		static String configXMLFilename = Application.StartupPath + "\\config.xml";
		static String favFilename = Application.StartupPath + "\\favourite.servers";
		static String favXMLFilename = Application.StartupPath + "\\Favorites.xml";
		static String friendFilename = Application.StartupPath + "\\friends.xml"; 

		private CheckBox[] carChecks;
        private RadioButton[] carGroupChecks;

		private ListViewColumnSorter lvwColumnSorter;

		private List<ServerInformation> serverList;
		private List<ServerInformation> favoriteServerList;
		private List<string> friendList;

		private int serversTotalCount;
		private int serversQueriedCount;
		private int numServersDone;
		private int serversRefusedCount;
		private int serversNoReplyCount;

		private int lastTabSelected;

		private Configuration config;
		private ServerInformationForm s;

		private bool exiting;

		private Thread t;
        #endregion

        void CodeCars(out ulong compulsory, out ulong illegal) {
			compulsory = 0;
			illegal = 0;
            for (int i = 0; i < MasterServerQuery.CAR_BITS.Length; ++i)
            {
                if (_carChecks[i].CheckState == CheckState.Checked)
                {
                    compulsory ^= MasterServerQuery.CAR_BITS[i];
                }
                else if (_carChecks[i].CheckState == CheckState.Unchecked)
                {
                    illegal ^= MasterServerQuery.CAR_BITS[i];
				}
			}
		}

		public string CarsToString(ulong c) {
			StringBuilder carNames = new StringBuilder();
            Array cars = MasterServerQuery.getCarNames(c);
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
					if (_favoriteServerList.Count > 0) {
						IPEndPoint[] stupidArray = new IPEndPoint[_favoriteServerList.Count];
						for (int i = 0; i < _favoriteServerList.Count; i++){
							stupidArray[i] = ((ServerInformation)_favoriteServerList[i]).Host;
						}
                        MasterServerQuery.Instance.Perform(0, 0, stupidArray, 0);
					}
				} else {
					ulong compulsory;
					ulong illegal;
					CodeCars(out compulsory, out illegal);
                    MasterServerQuery.Instance.Perform(compulsory, illegal, EmptyCheck.Checked, false, 0);
				}
			} catch(Exception e) {
					MessageBox.Show("Unable to contact the Master Server. Perhaps it is down, or your firewall is not configured properly." , "Unable to contact Master Server!", MessageBoxButtons.OK);
				}

            if (_exiting) 
                return;
			
            //invoke
			this.Invoke(new MethodInvoker(((isFav == true ? lvFavorites : lvMain).Sort)));
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
                MasterServerQuery.Instance.HostQueried -= new EventHandler<ServerInformationEventArgs>(HostQueriedCallback);
                MasterServerQuery.Instance.HostQueried -= new EventHandler<ServerInformationEventArgs>(FavoriteHostQueriedCallback);
                MasterServerQuery.Instance.HostQueried += (b.Name == "btnRefreshMain") ? new EventHandler<ServerInformationEventArgs>(HostQueriedCallback) : new EventHandler<ServerInformationEventArgs>(FavoriteHostQueriedCallback);
				_serversTotalCount = 0;
				(b.Name == "btnRefreshMain" ? lvMain : lvFavorites).Items.Clear();
				if (b.Name == "btnRefreshMain"){
					_serverList.Clear();
					btnJoinMain.Enabled = false;
					t = new Thread(new ThreadStart(MainQuery));
				} else
					t = new Thread(new ThreadStart(FavQuery));
  				t.Start();
			} else {
                MasterServerQuery.Instance.Stop();
				btnRefreshMain.Enabled = false;
				buttonRefreshFav.Enabled = false;
			}
		}

        public void DisplayServer(ServerInformation info, string trackFilter, bool hideEmpty, QueryType queryType)
		{
			if (!info.Track.Contains(trackFilter)) 
                return;

            if (hideEmpty && info.Players == 0 && (queryType == QueryType.Main)) 
                return;

			string serverName;
            info.Hostname = serverName = Utility.RemoveColorCodes(info.Hostname);
			string cars = CarsToString(info.Cars);
            ListViewItem lvi = (queryType == QueryType.Main ? lvMain : (queryType == QueryType.Favorite ? lvFavorites : null)).Items.Add(info.Host.ToString());
			lvi.SubItems.Insert(0, new ListViewItem.ListViewSubItem(lvi, serverName));
			lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, info.Ping.ToString()));
			int columnOffset = 0;
            if (queryType == QueryType.Favorite) {
				columnOffset = 1;
			} else {
				lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, info.Passworded == true ? "Yes" : "No"));
			}
			lvi.SubItems.Insert(3 - columnOffset, new ListViewItem.ListViewSubItem(lvi, info.Players.ToString() +"/" + info.Slots.ToString()));
			lvi.SubItems.Insert(4 - columnOffset, new ListViewItem.ListViewSubItem(lvi, RulesToString(info.Rules)));
			lvi.SubItems.Insert(5 - columnOffset, new ListViewItem.ListViewSubItem(lvi, info.Track));			
			lvi.SubItems.Insert(6 - columnOffset, new ListViewItem.ListViewSubItem(lvi, cars));

		}

        delegate void AddServerDelegate(ServerInformation info, QueryType queryType);
        public void AddServer(ServerInformation info, QueryType queryType)
		{
			if (_exiting) 
                return;
			
            try
            {
				// Make sure we're on the right thread
				if (!InvokeRequired) 
                {
					if (_serversTotalCount == 0) 
                    {
						_serversTotalCount = info.TotalServers;
						_serversQueriedCount = 0;
						numServersDone = 0;
						_serversRefusedCount = 0;
						_serversNoReplyCount = 0;
					}

					_serversQueriedCount++;
					string filter = TracksList.Text == "All Tracks" ? "" : TracksList.Text;					
					if (info.Success)
                    {
						numServersDone++;
                        DisplayServer(info, filter, EmptyCheck.Checked, queryType);
                        if (queryType == QueryType.Main)
                            _serverList.Add(info);
                    } 
                    else 
                    {
						if (info.ReadFailed) 
                        {
							_serversNoReplyCount++;
						} 
                        else 
                        {
							_serversRefusedCount++;
						}
					}

                    if (statusCheckProgress.Maximum < _serversTotalCount)
                        statusCheckProgress.Maximum = _serversTotalCount;
                    statusCheckProgress.Increment(_serversQueriedCount - statusCheckProgress.Value);
                    statusCheckProgress.ToolTipText = string.Concat("Checked ", _serversQueriedCount, " of ", _serversTotalCount);

                    statusNoReply.Text = _serversNoReplyCount.ToString();
                    statusRefused.Text = _serversRefusedCount.ToString();
				} 
                else 
                {
			    	//add the server asynchonously
			    	AddServerDelegate addServer = new AddServerDelegate(AddServer);
                    this.BeginInvoke(addServer, new object[] { info, queryType });
				}
			} catch(Exception e){ }
  		}

		
        void CloseToolStripMenuItemClick(object sender, System.EventArgs e) {
			Close();
		}

		void lvMainColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Determine if clicked column is already the column that is being sorted.
			if ( e.Column == _lvwColumnSorter.SortColumn ) {
				// Reverse the current sort direction for this column.
				if (_lvwColumnSorter.Order == SortOrder.Ascending) {
					_lvwColumnSorter.Order = SortOrder.Descending;
				} else {
					_lvwColumnSorter.Order = SortOrder.Ascending;
				}
			} else {
				// Set the column number that is to be sorted; default to ascending.
				_lvwColumnSorter.SortColumn = e.Column;
				_lvwColumnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			((ListView)sender).Sort();
		}

		void LoadLFS(String hostName, String mode, String password)
		{
			String lfsPath = config.lfsPath;
			String psPath = config.psPath;
			FormWindowState ws = this.WindowState;
            MasterServerQuery.Instance.Stop();
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
					sel = lvFavorites.SelectedItems;
					writefav = true;
				}
			} else if (sender is ToolStripMenuItem) {
				if (((ToolStripMenuItem)sender).Name == "joinServerToolStripMenuItem") {
					sel = lvMain.SelectedItems;
				} else {
					sel = lvFavorites.SelectedItems;
					writefav = true;
				}
			}else {
				sel = ((ListView)sender).SelectedItems;
			}
			if (sel.Count < 1) { return; }
			String serverName = sel[0].Text;
			if (writefav) {
				foreach(ServerInformation info in _favoriteServerList) { //this is stupid. need to fix.
					if (info.Hostname == sel[0].Text) {
						info.Password = edtPasswordMain.Text;
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

		void ContextMenuBrowserOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ListView.SelectedListViewItemCollection coll = lvMain.SelectedItems;
			contextMenuBrowser.Enabled = (coll.Count > 0);
		}

		void ToolStripMenuItem1Click(object sender, System.EventArgs e)
		{
			ListView.SelectedListViewItemCollection coll = lvMain.SelectedItems;
			if (coll.Count < 1) return;
			//lvFavorites.Items.Add((ListViewItem)coll[0].Clone());
			//write the host:ip out to file.
			foreach(ServerInformation info in _serverList){
				if (info.Hostname == coll[0].Text){
                    DisplayServer(info, "", false, QueryType.Favorite);
					_favoriteServerList.Add(info);
					WriteFav();
					break;
				}
			}
		}

		bool ReadXMLFav(string filename) {
			try {
				XmlDocument doc = new XmlDocument();
				doc.Load(filename);
				XmlNodeList list = doc.GetElementsByTagName("Favorites");
				list = ((XmlElement)list[0]).GetElementsByTagName("favourite");
				foreach (XmlElement favourite in list) {
					try {
						ServerInformation info = new ServerInformation();					
						info.Host = new IPEndPoint(IPAddress.Parse(favourite.GetElementsByTagName("ip")[0].FirstChild.Value), Convert.ToInt32(favourite.GetElementsByTagName("port")[0].FirstChild.Value));
						info.Hostname = favourite.GetElementsByTagName("name")[0].FirstChild.Value;					
						try {
							info.Password = favourite.GetElementsByTagName("password")[0].FirstChild.Value;
						} catch (Exception e) {
							info.Password = "";
						}
						_favoriteServerList.Add(info);
						lvFavorites.Items.Add(info.Hostname);
					} catch (Exception e) { }
				}
				return true;
			} catch (FileNotFoundException fnfe) {
				return false;	
			} catch (Exception e) {
				MessageBox.Show("Error loading Favorites: " + e.Message + "\nA copy was saved as "+filename +".backup", "", MessageBoxButtons.OK);
				File.Copy(filename, filename +".backup", true);
				return false;
			}
		}

		bool ReadFav(string filename)
		{
			try {
				TextReader tr = new StreamReader(filename);
				_favoriteServerList.Clear();
				lvFavorites.Items.Clear();
				string s = tr.ReadToEnd();
				foreach(String server in s.Split(new Char[]{'\n'}))
				{
					if (server != ""){
						String[] favInfoTmp = server.Split(new Char[]{' '}, 2);
						String[] ipAddress = favInfoTmp[0].Split(':');
						ServerInformation info = new ServerInformation();
						info.Hostname = favInfoTmp[1].Trim();
						info.Host = new IPEndPoint(IPAddress.Parse(ipAddress[0]), Convert.ToInt32(ipAddress[1]));
						_favoriteServerList.Add(info);
						lvFavorites.Items.Add(info.Hostname);
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
				tw.WriteStartElement("Favorites");
				tw.WriteAttributeString("version", "1");
				foreach (ServerInformation info in _favoriteServerList){
					tw.WriteStartElement("favourite");
					tw.WriteElementString("ip", info.Host.Address.ToString());
					tw.WriteElementString("port", info.Host.Port.ToString());
					tw.WriteElementString("name", info.Hostname);
					tw.WriteElementString("password", info.Password);
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
			if (_friendList.IndexOf(name) == -1)
				_friendList.Add(name);
			if (lvFriends.Items.IndexOfKey(name) == -1) {
				ServerInformation info;
                int result = LFSWorldQuery.FetchPublicStatsInfo(name, out info);
				ListViewItem lvi;
				if (result == 0 && !cbHideOffline.Checked) { //offline
					lvi = lvFriends.Items.Add(name, name, "");
					lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi, "Offline"));
					lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, "N/A"));
					lvi.SubItems.Insert(3, new ListViewItem.ListViewSubItem(lvi, "N/A"));
				} else if (result == 1) {
					lvi = lvFriends.Items.Add(name, name, "");
					lvi.SubItems.Insert(1, new ListViewItem.ListViewSubItem(lvi,info.Hostname));
					lvi.SubItems.Insert(2, new ListViewItem.ListViewSubItem(lvi, (info.Passworded ? "Yes" : "No")));
					String players = "";
					foreach (string player in info.RacerNames) {
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
				foreach (String friend in _friendList) {
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
			ListView.SelectedListViewItemCollection coll = lvFavorites.SelectedItems;
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

		void lvFavoritesSelectedIndexChanged(object sender, System.EventArgs e) {
			btnJoinFav.Enabled = (lvFavorites.SelectedItems.Count > 0 && lvFavorites.Items[0].SubItems.Count > 1);
		}

		void ViewServerInformationToolStripMenuItemClick(object sender, System.EventArgs e) 
        {
			ListView.SelectedListViewItemCollection coll;
			ServerInformation i;
			List<ServerInformation> myList;
			if (sender is ListView) {
				coll = ((ListView)sender).SelectedItems;
				myList = (((ListView)sender).Name == "lvMain") ? _serverList : _favoriteServerList;
			} else {
				ToolStripMenuItem l = (ToolStripMenuItem)sender;
				if (l.Name == "viewServerInformationMain") {
					coll = lvMain.SelectedItems;
					myList = _serverList;
				} else {
					coll = lvFavorites.SelectedItems;
					myList = _favoriteServerList;
				}
			}
			if (coll.Count < 1) return;
            s.Info = null;
			foreach (ServerInformation info in myList) {
				if (info.Hostname == coll[0].Text) {
                    s.Info = info;
					if (s.ShowDialog(this) == DialogResult.OK) {
						info.Password = s.Info.Password;
						WriteFav();
                        LoadLFS(s.Info.Hostname, "S2", s.Info.Password);
					}
					break;
				}
			}
		}

		void ComboBoxTracksSelectedIndexChanged(object sender, System.EventArgs e)
		{
			string filter = (TracksList.Text == "All Tracks" ? "" : TracksList.Text);
			lvMain.Items.Clear();
			try{
				foreach (ServerInformation info in _serverList){
					if (info.Track.Contains(filter)){
						DisplayServer(info, filter, EmptyCheck.Checked, QueryType.Main);
					}
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "", MessageBoxButtons.OK);
			}
		}

		void btnFindUserClick(object sender, System.EventArgs e)
		{
            string hostname = MasterServerQuery.Instance.FindUser("wabz", edtFindUserMain.Text);
			if (hostname != null){
                hostname = Utility.RemoveColorCodes(hostname);
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
			this.lastTabSelected = BrowserTabControl.SelectedIndex;
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
            MasterServerQuery.Instance.UseQueryDelay = !config.disableWait;
            MasterServerQuery.Instance.QueryDelayMilliseconds = config.queryWait;
			config.startPS = cbUsePS.Checked;
			config.psPath = txtPSPath.Text;
			try {
				config.psInsimPort = Convert.ToInt32(txtInsimPort.Text);
			} catch (Exception ex) {
				config.psInsimPort = 29999;
			}
		}

		void RemoveFromFavoritesToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			ListView.SelectedListViewItemCollection coll = lvFavorites.SelectedItems;
			if (coll.Count < 1) return;
			foreach(ServerInformation info in _favoriteServerList){
				if (info.Hostname == coll[0].Text){
					_favoriteServerList.Remove(info);
					WriteFav();
					lvFavorites.Items.Remove((ListViewItem)coll[0]);
					break;
				}
			}

		}

		public static void versionCheck(bool botherUser) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(VersionCheckUrl);
			request.Timeout = 3000;
			try {
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				if (response.StatusCode == HttpStatusCode.OK) {
					Stream stream = response.GetResponseStream();
					byte[] buf = new byte[1];
					stream.Read(buf, 0, buf.Length);
                    if (buf[0] == System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Minor)
                    {
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
					lvFavorites.Font = new Font(lvFavorites.Font.FontFamily.Name, lvFavorites.Font.Size + 1);

				} else if (lvMain.Font.Size > 1 && (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)) {
					lvMain.Font = new Font(lvMain.Font.FontFamily.Name, lvMain.Font.Size - 1);
					lvFavorites.Font = new Font(lvFavorites.Font.FontFamily.Name, lvFavorites.Font.Size - 1);
				}
			}
		}
		
		void BtnRefreshFriendClick(object sender, System.EventArgs e)
		{
			lvFriends.Items.Clear();
			foreach(string friend in _friendList){
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
			_friendList.RemoveAt(_friendList.IndexOf(name));
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

        public Configuration(string filename)
        {
            this.filename = filename;
        }

        public bool LoadXML()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlNodeList list = doc.GetElementsByTagName("bfsconfig");
                try
                {
                    lfsPath = ((XmlElement)list[0]).GetElementsByTagName("exepath")[0].FirstChild.Value;
                }
                catch (Exception e) { lfsPath = ""; }
                try
                {
                    disableWait = ((XmlElement)list[0]).GetElementsByTagName("qwait")[0].Attributes["enabled"].Value == "True";
                }
                catch (Exception e) { disableWait = false; }
                try
                {
                    queryWait = Convert.ToInt32(((XmlElement)list[0]).GetElementsByTagName("qwait")[0].FirstChild.Value);
                }
                catch (Exception e) { queryWait = 150; }
                try
                {
                    checkNewVersion = ((XmlElement)list[0]).GetElementsByTagName("checkversion")[0].FirstChild.Value == "True";
                }
                catch (Exception e) { checkNewVersion = false; }
                try
                {
                    XmlNode spot = ((XmlElement)list[0]).GetElementsByTagName("spotter")[0];
                    startPS = (spot.Attributes["enabled"].Value == "True");
                    psInsimPort = Convert.ToInt32(((XmlElement)spot).GetElementsByTagName("insimport")[0].FirstChild.Value);
                    psPath = ((XmlElement)spot).GetElementsByTagName("path")[0].FirstChild.Value;
                }
                catch (Exception e)
                {
                    startPS = false;
                    psInsimPort = 29999;
                    psPath = "";
                }
                try
                {
                    joinOnClick = ((XmlElement)list[0]).GetElementsByTagName("listclick")[0].FirstChild.Value == "join";
                }
                catch (Exception e) { joinOnClick = true; }
                return true;
            }
            catch (FileNotFoundException fe)
            {
                MessageBox.Show("No existing configuration data found.\nPlease set this up now.", "", MessageBoxButtons.OK);
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading configuration: " + e.Message + "\nA copy was saved as " + filename + ".backup", "", MessageBoxButtons.OK);
                File.Copy(filename, filename + ".backup", true);
                return false;
            }
        }


        public bool Load()
        {
            try
            {
                TextReader tr = new StreamReader(this.filename);
                //this.userName = tr.ReadLine();
                //this.password = decrypt(tr.ReadLine());
                this.lfsPath = tr.ReadLine();
                this.disableWait = (tr.ReadLine() == "True");
                this.checkNewVersion = (tr.ReadLine() == "True");
                try
                {
                    this.queryWait = Convert.ToInt32(tr.ReadLine());
                }
                catch (Exception e)
                {
                    this.queryWait = 150;
                }
                try
                {
                    this.joinOnClick = (tr.ReadLine() != "view");
                }
                catch (Exception e)
                {
                    this.joinOnClick = true;
                }
                try
                {
                    this.psInsimPort = Convert.ToInt32(tr.ReadLine());
                }
                catch (Exception e)
                {
                    this.psInsimPort = 29999;
                }
                this.psPath = tr.ReadLine();
                this.startPS = (tr.ReadLine() == "True");
                tr.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Save(string filename)
        {
            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("There was an error writing to the config file:" + ex.Message, MainForm.appTitle, MessageBoxButtons.OK);
            }

        }


    }
	

}
