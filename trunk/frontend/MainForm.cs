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
using System.Reflection;

namespace BrowseForSpeed.Frontend
{
	public enum QueryType{Main, Favourite, Friend, Quick}


	public partial class MainForm
	{
		static string bfs_version = "0.6";
		static string version_check = "8";
		static string download_url = "http://www.browseforspeed.net";
		static string version_check_url = "http://www.browseforspeed.net/versioncheck.pl";

		public static String appTitle = "Browse For Speed";
		static String configFilename = Application.StartupPath +  + Path.DirectorySeparatorChar + "config.cfg";
		static String configXMLFilename = Application.StartupPath + Path.DirectorySeparatorChar + "config.xml";

		private CheckBox[] cars;
		private Button[] groups;

		private LFSQuery q;
		private ListViewColumnSorter lvwColumnSorter;

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

		public static LanguageManager languages = new LanguageManager(Application.StartupPath + Path.DirectorySeparatorChar + "lang");

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
			PropertyInfo propInfo = ctrl.GetType().GetProperty(propName);
			Delegate dgtSetValue = new SetValueDelegate(propInfo.SetValue);
      		ctrl.Invoke(dgtSetValue, new Object[3] { ctrl, val, null });
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

		void MakeQuery(QueryType type)
		{
			SetControlProperty(buttonRefreshFav, "Text", languages.GetString("MainForm.btnStop"));
			refreshing = true;
			try {
				if (type == QueryType.Favourite) {
					q.query(0, 0, "browseforspeed", lvFavourites.GetAllHosts(), 1);
				} else if (type == QueryType.Quick) {
					SetControlProperty(btnQuickRefresh, "Text", languages.GetString("MainForm.btnStop"));
					q.query(0,0, "browseforsped", lvMain.GetVisibleHosts(), 0);
				} else {
					SetControlProperty(btnRefreshMain, "Text", languages.GetString("MainForm.btnStop"));
					ulong compulsory, illegal;
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
			SetControlProperty(btnQuickRefresh, "Enabled", true);
			SetControlProperty(buttonRefreshFav, "Text", languages.GetString("MainForm.btnRefresh"));
			SetControlProperty(btnRefreshMain, "Text", languages.GetString("MainForm.btnRefresh"));
			SetControlProperty(btnQuickRefresh, "Text", languages.GetString("MainForm.btnQuickRefresh"));
		}

		void MainQuery()
		{
			MakeQuery(QueryType.Main);
		}
		
		void QuickQuery()
		{
			MakeQuery(QueryType.Quick);
		}

		void FavQuery()
		{
			MakeQuery(QueryType.Favourite);
		}

		void RefreshButtonClick(object sender, System.EventArgs e)
		{
            Console.WriteLine(sender.ToString());
			Button b = (Button)sender;
			if (!refreshing){
				LFSQuery.queried -= new ServerQueried(queryMainEventListener);
				LFSQuery.queried += new ServerQueried(queryMainEventListener);
				totalServers = 0;
				btnJoinMain.Enabled = false;
				if (b.Name == "btnRefreshMain") {
					btnQuickRefresh.Enabled = false;
					t = new Thread(new ThreadStart(MainQuery));
					lvMain.ClearServers();
				}
				else if (b.Name == "btnQuickRefresh") {
					btnRefreshMain.Enabled = false;
					t = new Thread(new ThreadStart(QuickQuery));
					lvMain.Items.Clear();
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
				btnQuickRefresh.Enabled = false;
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
					list.Sort();
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
				Process lfs = new Process();
				lfs.Exited += new EventHandler(LFSExit);
				lfs.EnableRaisingEvents = true;
				lfs.StartInfo.FileName = lfsPath;
				lfs.StartInfo.WorkingDirectory = Path.GetDirectoryName(lfsPath);
				lfs.StartInfo.Arguments = "/join=" + hostName + " /mode=" + mode + " /pass=" + password + "/insim=" + config.insimPort.ToString();
				lfs.Start();
				this.WindowState = FormWindowState.Minimized;
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
				lvFavourites.Save();
			}
			LoadLFS(item.rawHostname, VersionToString(item.version), pass);
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
			lvFavourites.Save();
		}

		public static String VersionToString(byte version)
		{
			if (version == LFSQuery.VERSION_S1)
			    return "S1";
			else if (version == LFSQuery.VERSION_DEMO)
			    return "Demo";
			else return "S2";
		}

		public static byte StringToVersion(String version)
		{
			switch (version){
				case "Demo" : return LFSQuery.VERSION_DEMO;
				case "S1" : return LFSQuery.VERSION_S1;
				case "S2" : default : return LFSQuery.VERSION_S2;
			}
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
				if (l.Name == "viewServerInformationMain") {
					info = lvMain.GetSelectedServer();
				} else if (l.Name == "viewServerInformationFav") {
					info = lvFavourites.GetSelectedServer();
				}
				else if (l.Name == "viewServerInformationFriend"){
					FriendListItem selectedFriend = lvFriends.GetSelectedFriend();
					if (selectedFriend != null && selectedFriend.status == FriendStatus.Offline) {
						return;
					}
					info = new ServerListItem(selectedFriend.server);
					info.version = LFSQuery.VERSION_S2;
				}
			} else {
				info = ((ServerListView)sender).GetSelectedServer();
			}

			if (info == null)
				return;
			s.SetInfo(info);
			if (s.ShowDialog(this) == DialogResult.OK) {
				info.password = s.GetInfo().password;
				lvFavourites.Save();
				LoadLFS(s.GetInfo().rawHostname, VersionToString(s.GetInfo().version), s.GetInfo().password);
			}
		}

		void MainFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
		{
			LFSQuery.stopQuerying();
			this.exiting = true;
			UpdateConfig();
			config.Save(configXMLFilename);
			lvFavourites.Save();
			lvFriends.Save();
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
			config.filter_version = cbVersion.Text;
			config.queryWait = (int)queryWait.Value;
			config.joinOnClick = (cbDoubleClick.SelectedIndex == 1);//rbJoin.Checked;
			config.fav_refresh = cbFavRefresh.Checked;
			config.friend_refresh = cbFriendRefresh.Checked;
			config.hide_offline = cbHideOffline.Checked;
			config.ping_threshold = Convert.ToInt32(cbPing.Text);
			config.filter_track = cbTracks.Text;
			config.fancy_hostnames = cbColouredHostnames.Checked;
			config.language = (cbConfigLang.SelectedItem ?? "").ToString();
			ulong allow, disallow;
			CodeCars(out allow, out disallow);
			config.filter_cars_allow = allow;
			config.filter_cars_disallow = disallow;
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
			lvFavourites.Save();
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
			lvFriends.DisplayAll();
			lvFriends.Sort();
		}

		void CheckBox2CheckedChanged(object sender, System.EventArgs e) {
			config.hide_offline = cbHideOffline.Checked;
			lvFriends.HideOffline = config.hide_offline;
		}

		void LvFriendsSelectedIndexChanged(object sender, System.EventArgs e)
		{
			FriendListItem friend = lvFriends.GetSelectedFriend();
			btnJoinFriend.Enabled = ((friend != null) && (friend.status == FriendStatus.Online));
		}

		void JoinFriendClick(object sender, System.EventArgs e)
		{
			FriendListItem friend = lvFriends.GetSelectedFriend();
			if ((friend == null) || (friend.status != FriendStatus.Online))
			    return;

			ServerInformation info = lvFavourites.GetServer(friend.server.hostname);
			string password = edtPasswordMain.Text;
			if (info != null) {
				password = info.password;
			}
			LoadLFS(friend.server.hostname, "S2", password);
		}

		void BtnAddFriendClick(object sender, System.EventArgs e)
		{
			if (edtFriendName.Text.Length > 0) {
				if (lvFriends.AddFriend(edtFriendName.Text)) {
					lvFriends.DisplayFriend(edtFriendName.Text);
					lvFriends.Sort();
					edtFriendName.Text = "";
				} else {
					MessageBox.Show("Friend Already Exists");
					edtFriendName.Focus();
				}
			}
		}

		void RemoveFriendToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			FriendListItem friend = lvFriends.GetSelectedFriend();
			if (friend == null){
				return;
			}
			if (sender is MainForm) { //delete key!
				string message = String.Format(languages.GetString("RemoveFriendQuery"), friend.name);
				if (MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No){
					return;
				}
			}
			lvFriends.RemoveFriend(friend);
		}

		void ContextMenuFriendsOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			FriendListItem friend = lvFriends.GetSelectedFriend();
			bool enabled = (friend != null);
			removeFriendToolStripMenuItem.Enabled = enabled;
			enabled = enabled && (friend.status == FriendStatus.Online);
			viewServerInformationFriend.Enabled = enabled;
			joinServerMenuFriends.Enabled = enabled;
		}

		void LvFriendsDoubleClick(object sender, System.EventArgs e)
		{
			if (config.joinOnClick) {
				FriendListItem friend = lvFriends.GetSelectedFriend();
				if (friend == null || (friend.status != FriendStatus.Online))
					return;

				string message = String.Format(languages.GetString("JoinFriendQuery"), friend.name, friend.server.hostname);
				ServerInformation info = lvFavourites.GetServer(friend.server.hostname);
				string password = edtPasswordMain.Text;
				if (info != null) {
					password = info.password;
				}
				if (MessageBox.Show(message, languages.GetString("MainForm.this"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
					LoadLFS(friend.server.rawHostname, "S2", password);
				}
			} else {
				ViewServerInformationToolStripMenuItemClick(viewServerInformationFriend, e);
			}
		}

		void EdtFriendNameKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter && edtFriendName.Text.Length > 0) {
				lvFriends.AddFriend(edtFriendName.Text);
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
				if (valuename.EndsWith("LFS.exe")) {
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
					tabControl.SelectedTab = tabConfig;
					config.disableWait =  false;
					config.checkNewVersion = true;
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
			if (!lvFavourites.LoadXML()) {
				lvFavourites.LoadOld();
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
				cbFavRefresh.Checked = config.fav_refresh;
				cbFriendRefresh.Checked = config.friend_refresh;
				cbColouredHostnames.Checked = config.fancy_hostnames;
				cbHideOffline.Checked = config.hide_offline;
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
				//rbJoin.Checked = config.joinOnClick;
				//rbView.Checked = !config.joinOnClick;
				cbDoubleClick.SelectedIndex = config.joinOnClick ? 1 : 0;

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
				if (config.filter_track.Length != 0) {
					cbTracks.Text = config.filter_track;
				} else {
					cbTracks.SelectedIndex = 0;
				}
				cbPing.Text = config.ping_threshold.ToString();
				cbVersion.Text = config.filter_version;
				for(int i = 0; i < LFSQuery.CAR_BITS.Length; ++i) {
					if ((LFSQuery.CAR_BITS[i] & config.filter_cars_allow) != 0){
						cars[i].CheckState = CheckState.Checked;
					} else if ((LFSQuery.CAR_BITS[i] & config.filter_cars_disallow) != 0) {
						cars[i].CheckState = CheckState.Unchecked;
					}
				}
			} else {
				cbPing.SelectedIndex = 7;
				cbTracks.SelectedIndex = 0;
				cbVersion.SelectedIndex = 2;
			}
			lvFriends.Load();
			lvFriends.DisplayAll();
			cbAddServerVersion.SelectedIndex = 0;
			if (config.fav_refresh) {
				RefreshButtonClick(buttonRefreshFav, null);
			}
			if (config.friend_refresh) {
				BtnRefreshFriendClick(btnRefreshFriend, null);
			}
		}

		void CbEmptyCheckedChanged(object sender, System.EventArgs e)
		{
			lvMain.Filter(FilterType.Empty, cbEmpty.Checked);
			lvMain.DisplayAll();

		}

		void CbPingSelectedIndexChanged(object sender, System.EventArgs e) {
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
				info.rawHostname = info.hostname;
				lvFavourites.AddServer(info);
				edtAddServerAddress.Text = "";
			} catch (Exception ex) {
				string message = String.Format(languages.GetString("AddFavError"), ex.Message + ex.StackTrace);
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
				config.psp.Remove((PreStartProgram)lbPreStart.SelectedItem);
				lbPreStart.Items[lbPreStart.SelectedIndex] = p;				
				config.psp.Add(p);
			} else {				
				lbPreStart.Items.Add(p);
				config.psp.Add(p);
			}
		}

		void BtnProgramEnableClick(object sender, System.EventArgs e) {
			PreStartProgram p = (PreStartProgram)lbPreStart.SelectedItem;
			p.enabled = !p.enabled;
			lbPreStart.Items[lbPreStart.SelectedIndex] = p;
		}

		void BtnProgramDeleteClick(object sender, System.EventArgs e) {
			config.psp.Remove((PreStartProgram)lbPreStart.SelectedItem);
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

		public List<ToolStripMenuItem> GetControls(ToolStripMenuItem menu)
		{
			List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
			if (menu.DropDownItems.Count == 0) {
				list.Add(menu);
				return list;
			} else {
				foreach (ToolStripMenuItem item in menu.DropDownItems) {
					list.AddRange(GetControls(item));
				}
				return list;
			}
		}

		public List<Control> GetControls(Control c)
		{
			List<Control> controlList = new List<Control>();
			//tab controls inherit from control, but have a different collection for sub-controls
			if (c is TabControl) {
				TabControl tc = c as TabControl;
				if (tc.TabPages.Count == 0) {
					controlList.Add(c);
					return controlList;
				} else {
					foreach (TabPage page in tc.TabPages) {
						controlList.AddRange(GetControls(page));
						controlList.Add(page);
					}
					return controlList;
				}
			}
			
			if (c.Controls.Count == 0) {
				controlList.Add(c);
				return controlList;
			} else {
				foreach (Control cont in c.Controls) {
					controlList.AddRange(GetControls(cont));
				}
				return controlList;
			}
		}

		private void UpdateUI()
		{
			if (languages.Count == 0)
				return;
			
			string formPrefix = "MainForm.";

			List<string> stringsToAdd = new List<string>();
			List<Control> contList = GetControls(this);
			for(int i = 0; i < contList.Count; i++){
				Control c = contList[i];
				//context menus of any controls need to be added to the control list too.
				if (c.ContextMenuStrip != null) {
					contList.Add(c.ContextMenuStrip);
				}
				if (String.IsNullOrEmpty(c.Text) || String.IsNullOrEmpty(c.Name) || c is ComboBox) //controls with empty text/names are going to stuff up. we also dont want ComboBoxes
					continue;
				//we need to process menu sub-items seperately because they're not inherited from control.
				if (c is ToolStrip) {
					Console.WriteLine(c.Name);
					foreach (ToolStripMenuItem menu in (c as ToolStrip).Items) {
						//process each sub menu.
						foreach (ToolStripMenuItem item in GetControls(menu)) {
							string name = formPrefix + item.Name;
							string result = languages.GetString(name);
							if (name == result) {
								Console.WriteLine(name + " not found in lang.");
								stringsToAdd.Add("<component name=\"" + result + "\">" + item.Text.Replace("&", "&amp;") + "</component>");
								continue;
							} else {
								item.Text = result;
							}
						}
						//process the parent menu text.
						string menuname = formPrefix + menu.Name;
						string menuresult = languages.GetString(menuname);
						if (menuname == menuresult) {
							Console.WriteLine(menuname + " not found in lang.");
							stringsToAdd.Add("<component name=\"" + menuresult + "\">" + menu.Text.Replace("&", "&amp;") + "</component>");
							continue;
						} else {
							menu.Text = menuresult;
						}

					}
				//now process controls as normal
				} else {
					string name = formPrefix + c.Name;
					string result = languages.GetString(name);
					if (name == result) {
						Console.WriteLine(name + " not found in lang.");
						stringsToAdd.Add("<component name=\"" + result + "\">" + c.Text.Replace("&", "&amp;") + "</component>");
						continue;
					} else {
						c.Text = result;
					}
				}
			}
			
			//Write some cool shit about missing strings.
			if (stringsToAdd.Count > 0) {
				Console.WriteLine("\nPlease add the following lines to " + Path.GetFileName(languages.Filename) + " (translating them if needed):");
				foreach (string s in stringsToAdd) {
					Console.WriteLine(s);
				}
			}

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
			BtnProgramCancelClick(this, null);
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

		void ChkStartupRefreshCheckedChanged(object sender, System.EventArgs e) {
			config.fav_refresh = cbFavRefresh.Checked;
		}
		
		void PathListSelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (pathList.SelectedIndex < 0)
				config.lfsPath = "";
			else
				config.lfsPath = pathList.Items[pathList.SelectedIndex].ToString();	
		}

		private static Brush[] LFSColours = {SystemBrushes.ControlText, Brushes.Red, Brushes.LightGreen,
										Brushes.Yellow, Brushes.Blue, Brushes.Purple,
										Brushes.LightBlue, Brushes.White, Brushes.Black};
		
		void ListViewDrawSubItem(object sender, System.Windows.Forms.DrawListViewSubItemEventArgs e)
		{
			if (e.Item.Selected) {
				e.DrawDefault = true;
			}
			ListView list = (ListView)sender;
			if (list is FriendListView) {
				if (e.ColumnIndex == 1) {
					FriendListItem friend = lvFriends.GetFriend((string)e.Item.Tag);
					if (friend.status == FriendStatus.Online) {
						DrawColouredHostname(e.Graphics, friend.server.hostname, list.Font, e.SubItem.Bounds);
						return;
					}
				} 
				e.DrawDefault = true;
				return;
			} else {
				if (e.ColumnIndex == 0) {
					ServerListItem server = ((ServerListView)list).GetServer((IPEndPoint)e.Item.Tag);
					e.DrawBackground();
					DrawColouredHostname(e.Graphics, server.hostname, list.Font, e.Bounds);
				} else {
					e.DrawDefault = true;
					return;
				}
			}
		}
		
		public static void DrawColouredHostname(Graphics g, string hostname, Font f, Rectangle bounds)
		{
			try {
			string cleanHostname = LFSQuery.removeColourCodes(hostname);
			CharacterRange[] ranges = new CharacterRange[cleanHostname.Length];
			StringFormat format = new StringFormat();
			Region[] stringRegions = new Region[cleanHostname.Length];
			
			format.FormatFlags = StringFormatFlags.NoClip;
			for(int i = 0;  i < cleanHostname.Length; i++)
    				ranges[i] = new CharacterRange(i, 1);
			
			SizeF size = g.MeasureString(cleanHostname, f);			
			RectangleF layoutRect = new RectangleF(0, 0, size.Width, size.Height);
			format.SetMeasurableCharacterRanges(ranges);
			stringRegions = g.MeasureCharacterRanges(cleanHostname, f, layoutRect, format);			
			Brush b = SystemBrushes.ControlText;
			for (int i = 0, j = 0; i < hostname.Length; ++i, ++j) {
				if (i < hostname.Length - 1 && hostname[i] == '^') {
					++i;
					if (char.IsDigit(hostname[i])) {
						int c = Int32.Parse(hostname[i].ToString());
						if (c > 8)
							c = 8;
						--j;
						b = LFSColours[c];
						continue;
					}
				}
				Region region = stringRegions[j] as Region;
        		RectangleF rect = region.GetBounds(g);
				//yay ellipsissss
/*				if ((rect.X + (rect.Width + stringRegions[j-(1 < j ? 1 : 0)].GetBounds(g).Width + stringRegions[j-(2 < j ? 2 : 0)].GetBounds(g).Width)) > bounds.Width) {
					g.DrawString("...", f, SystemBrushes.ControlText, stringRegions[0].GetBounds(g).X + bounds.X + rect.X, bounds.Y, format);
					return;
				}*/
        		g.DrawString(cleanHostname[j].ToString(), f, b, stringRegions[0].GetBounds(g).X + bounds.X + rect.X, bounds.Y, format);
			}
			} catch (Exception ex) {
				//g.DrawString(ex.Message + ex.StackTrace, f, Brushes.Black, g.VisibleClipBounds);
				Console.WriteLine(ex);
				Console.WriteLine(hostname);
			}
		}
		
		void CbColouredHostnamesCheckedChanged(object sender, System.EventArgs e)
		{
			config.fancy_hostnames = cbColouredHostnames.Checked;
			lvFriends.OwnerDraw = config.fancy_hostnames;
			lvMain.OwnerDraw = config.fancy_hostnames;
			lvFavourites.OwnerDraw = config.fancy_hostnames;
		}
		
		void ListViewDrawColumnHeader(object sender, System.Windows.Forms.DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}
		
		void ListViewMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListViewItem item = ((ListView)sender).GetItemAt(e.X, e.Y);
			if (item != null && item.Checked == false) {
				//stops the gridlines disappearing when the mouse first moves over an item.
				((ListView)sender).Invalidate(item.Bounds);
				item.Checked = true;
			}
		}
	}
}

