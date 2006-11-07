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
namespace BrowseForSpeed.Frontend
{
	partial class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("No Friends Online");
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.joinServerToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabMain = new System.Windows.Forms.TabPage();
			this.btnQuickRefresh = new System.Windows.Forms.Button();
			this.edtFindUserMain = new System.Windows.Forms.TextBox();
			this.lblFindUserMain = new System.Windows.Forms.Label();
			this.edtPasswordMain = new System.Windows.Forms.TextBox();
			this.lblPasswordMain = new System.Windows.Forms.Label();
			this.btnFindUserMain = new System.Windows.Forms.Button();
			this.gbFilters = new System.Windows.Forms.GroupBox();
			this.cbVersion = new System.Windows.Forms.ComboBox();
			this.cbPublic = new System.Windows.Forms.CheckBox();
			this.cbPrivate = new System.Windows.Forms.CheckBox();
			this.cbFull = new System.Windows.Forms.CheckBox();
			this.lblPingThreshold = new System.Windows.Forms.Label();
			this.cbPing = new System.Windows.Forms.ComboBox();
			this.cbEmpty = new System.Windows.Forms.CheckBox();
			this.cbTracks = new System.Windows.Forms.ComboBox();
			this.lvMain = new BrowseForSpeed.Frontend.MainListView();
			this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderPing = new System.Windows.Forms.ColumnHeader();
			this.columnPrivate = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderConnections = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderInfo = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderTrack = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderCars = new System.Windows.Forms.ColumnHeader();
			this.contextMenuBrowser = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.joinServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewServerInformationMain = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.copyServerToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.administrateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.btnRefreshMain = new System.Windows.Forms.Button();
			this.btnJoinMain = new System.Windows.Forms.Button();
			this.tabFavourites = new System.Windows.Forms.TabPage();
			this.cbAddServerVersion = new System.Windows.Forms.ComboBox();
			this.lblAddressFav = new System.Windows.Forms.Label();
			this.edtAddServerAddress = new System.Windows.Forms.TextBox();
			this.btnAddServer = new System.Windows.Forms.Button();
			this.btnRefreshFav = new System.Windows.Forms.Button();
			this.btnJoinFav = new System.Windows.Forms.Button();
			this.lvFavourites = new BrowseForSpeed.Frontend.FavouriteListView();
			this.columnHeaderFavServerName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavPing = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavVersion = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavSlots = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavInfo = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavTrack = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavCars = new System.Windows.Forms.ColumnHeader();
			this.contextMenuFav = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.joinServerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.viewServerInformationFav = new System.Windows.Forms.ToolStripMenuItem();
			this.removeFromFavouritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyServerToClipboardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.administrateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabFriends = new System.Windows.Forms.TabPage();
			this.edtFriendName = new System.Windows.Forms.TextBox();
			this.btnAddFriend = new System.Windows.Forms.Button();
			this.cbHideOffline = new System.Windows.Forms.CheckBox();
			this.btnRefreshFriend = new System.Windows.Forms.Button();
			this.btnJoinFriend = new System.Windows.Forms.Button();
			this.lvFriends = new BrowseForSpeed.Frontend.FriendListView();
			this.columnFriendName = new System.Windows.Forms.ColumnHeader();
			this.columnFriendServer = new System.Windows.Forms.ColumnHeader();
			this.columnFriendPrivate = new System.Windows.Forms.ColumnHeader();
			this.columnFriendPlayers = new System.Windows.Forms.ColumnHeader();
			this.contextMenuFriends = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.joinServerMenuFriends = new System.Windows.Forms.ToolStripMenuItem();
			this.viewServerInformationFriend = new System.Windows.Forms.ToolStripMenuItem();
			this.removeFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabConfig = new System.Windows.Forms.TabPage();
			this.gbConfigurationMain = new System.Windows.Forms.GroupBox();
			this.gbLiveForSpeed = new System.Windows.Forms.GroupBox();
			this.txtInsimPort = new System.Windows.Forms.TextBox();
			this.lblInsimPortConfig = new System.Windows.Forms.Label();
			this.pathList = new System.Windows.Forms.ListBox();
			this.lblExeDescriptionConfig = new System.Windows.Forms.Label();
			this.lblExePathConfig = new System.Windows.Forms.Label();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.gbLanguage = new System.Windows.Forms.GroupBox();
			this.gbLangDetails = new System.Windows.Forms.GroupBox();
			this.lblCommentReal = new System.Windows.Forms.Label();
			this.lblEmailReal = new System.Windows.Forms.Label();
			this.lblAuthorReal = new System.Windows.Forms.Label();
			this.lblLangComment = new System.Windows.Forms.Label();
			this.lblLangContact = new System.Windows.Forms.Label();
			this.lblLangAuthor = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbConfigLang = new System.Windows.Forms.ComboBox();
			this.lblLanguageConfig = new System.Windows.Forms.Label();
			this.gbStartBeforeLFS = new System.Windows.Forms.GroupBox();
			this.btnProgramEnable = new System.Windows.Forms.Button();
			this.btnProgramDelete = new System.Windows.Forms.Button();
			this.btnProgramNew = new System.Windows.Forms.Button();
			this.gbProgramConfig = new System.Windows.Forms.GroupBox();
			this.btnProgramCancel = new System.Windows.Forms.Button();
			this.btnProgramSave = new System.Windows.Forms.Button();
			this.edtProgramOptions = new System.Windows.Forms.TextBox();
			this.lblProgramConfigArg = new System.Windows.Forms.Label();
			this.edtProgramName = new System.Windows.Forms.TextBox();
			this.lblProgramConfigName = new System.Windows.Forms.Label();
			this.btnProgramBrowse = new System.Windows.Forms.Button();
			this.lblProgramConfigPath = new System.Windows.Forms.Label();
			this.edtProgramPath = new System.Windows.Forms.TextBox();
			this.lbPreStart = new System.Windows.Forms.ListBox();
			this.gbAdvanced = new System.Windows.Forms.GroupBox();
			this.cbDoubleClick = new System.Windows.Forms.ComboBox();
			this.gbQueryWait = new System.Windows.Forms.GroupBox();
			this.lblQueryWaitDescription = new System.Windows.Forms.Label();
			this.queryWait = new System.Windows.Forms.NumericUpDown();
			this.cbQueryWait = new System.Windows.Forms.CheckBox();
			this.lblQueryWaitHelp = new System.Windows.Forms.Label();
			this.gbWhenBFSStarts = new System.Windows.Forms.GroupBox();
			this.cbFriendRefresh = new System.Windows.Forms.CheckBox();
			this.btnCheckNewVersion = new System.Windows.Forms.Button();
			this.cbFavRefresh = new System.Windows.Forms.CheckBox();
			this.cbNewVersion = new System.Windows.Forms.CheckBox();
			this.lblDoubleClick = new System.Windows.Forms.Label();
			this.cbColouredHostnames = new System.Windows.Forms.CheckBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusTotal = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusRefused = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusNoReply = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialogPS = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.gbFilters.SuspendLayout();
			this.contextMenuBrowser.SuspendLayout();
			this.tabFavourites.SuspendLayout();
			this.contextMenuFav.SuspendLayout();
			this.tabFriends.SuspendLayout();
			this.contextMenuFriends.SuspendLayout();
			this.tabConfig.SuspendLayout();
			this.gbConfigurationMain.SuspendLayout();
			this.gbLiveForSpeed.SuspendLayout();
			this.gbLanguage.SuspendLayout();
			this.gbLangDetails.SuspendLayout();
			this.gbStartBeforeLFS.SuspendLayout();
			this.gbProgramConfig.SuspendLayout();
			this.gbAdvanced.SuspendLayout();
			this.gbQueryWait.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.queryWait)).BeginInit();
			this.gbWhenBFSStarts.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.aboutToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(946, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.joinServerToolStripMenuItem2,
									this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// joinServerToolStripMenuItem2
			// 
			this.joinServerToolStripMenuItem2.Name = "joinServerToolStripMenuItem2";
			this.joinServerToolStripMenuItem2.Size = new System.Drawing.Size(140, 22);
			this.joinServerToolStripMenuItem2.Text = "&Join Server...";
			this.joinServerToolStripMenuItem2.Click += new System.EventHandler(this.JoinServerToolStripMenuItem2Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
			this.closeToolStripMenuItem.Text = "&Exit";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItemClick);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.aboutToolStripMenuItem1});
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.aboutToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem1
			// 
			this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
			this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
			this.aboutToolStripMenuItem1.Text = "&About...";
			this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.AboutToolStripMenuItem1Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabMain);
			this.tabControl.Controls.Add(this.tabFavourites);
			this.tabControl.Controls.Add(this.tabFriends);
			this.tabControl.Controls.Add(this.tabConfig);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.tabControl.Location = new System.Drawing.Point(0, 24);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(946, 587);
			this.tabControl.TabIndex = 1;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControlSelectedIndexChanged);
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.btnQuickRefresh);
			this.tabMain.Controls.Add(this.edtFindUserMain);
			this.tabMain.Controls.Add(this.lblFindUserMain);
			this.tabMain.Controls.Add(this.edtPasswordMain);
			this.tabMain.Controls.Add(this.lblPasswordMain);
			this.tabMain.Controls.Add(this.btnFindUserMain);
			this.tabMain.Controls.Add(this.gbFilters);
			this.tabMain.Controls.Add(this.lvMain);
			this.tabMain.Controls.Add(this.btnRefreshMain);
			this.tabMain.Controls.Add(this.btnJoinMain);
			this.tabMain.Location = new System.Drawing.Point(4, 22);
			this.tabMain.Name = "tabMain";
			this.tabMain.Padding = new System.Windows.Forms.Padding(3);
			this.tabMain.Size = new System.Drawing.Size(938, 561);
			this.tabMain.TabIndex = 0;
			this.tabMain.Text = "Server Browser";
			this.tabMain.UseVisualStyleBackColor = true;
			// 
			// btnQuickRefresh
			// 
			this.btnQuickRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnQuickRefresh.Location = new System.Drawing.Point(170, 535);
			this.btnQuickRefresh.Name = "btnQuickRefresh";
			this.btnQuickRefresh.Size = new System.Drawing.Size(90, 23);
			this.btnQuickRefresh.TabIndex = 10;
			this.btnQuickRefresh.Text = "&Quick Refresh";
			this.btnQuickRefresh.UseVisualStyleBackColor = true;
			this.btnQuickRefresh.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// edtFindUserMain
			// 
			this.edtFindUserMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.edtFindUserMain.Location = new System.Drawing.Point(651, 535);
			this.edtFindUserMain.Name = "edtFindUserMain";
			this.edtFindUserMain.Size = new System.Drawing.Size(116, 21);
			this.edtFindUserMain.TabIndex = 9;
			this.edtFindUserMain.WordWrap = false;
			this.edtFindUserMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EdtFindUserMainKeyDown);
			// 
			// lblFindUserMain
			// 
			this.lblFindUserMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblFindUserMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblFindUserMain.Location = new System.Drawing.Point(525, 538);
			this.lblFindUserMain.Name = "lblFindUserMain";
			this.lblFindUserMain.Size = new System.Drawing.Size(120, 21);
			this.lblFindUserMain.TabIndex = 8;
			this.lblFindUserMain.Text = "Find User:";
			this.lblFindUserMain.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// edtPasswordMain
			// 
			this.edtPasswordMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.edtPasswordMain.Location = new System.Drawing.Point(380, 535);
			this.edtPasswordMain.Name = "edtPasswordMain";
			this.edtPasswordMain.Size = new System.Drawing.Size(139, 21);
			this.edtPasswordMain.TabIndex = 7;
			// 
			// lblPasswordMain
			// 
			this.lblPasswordMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPasswordMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPasswordMain.Location = new System.Drawing.Point(226, 538);
			this.lblPasswordMain.Name = "lblPasswordMain";
			this.lblPasswordMain.Size = new System.Drawing.Size(148, 16);
			this.lblPasswordMain.TabIndex = 6;
			this.lblPasswordMain.Text = "Server Password:";
			this.lblPasswordMain.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnFindUserMain
			// 
			this.btnFindUserMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFindUserMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnFindUserMain.Location = new System.Drawing.Point(773, 533);
			this.btnFindUserMain.Name = "btnFindUserMain";
			this.btnFindUserMain.Size = new System.Drawing.Size(157, 23);
			this.btnFindUserMain.TabIndex = 5;
			this.btnFindUserMain.Text = "&Find User Online";
			this.btnFindUserMain.UseVisualStyleBackColor = true;
			this.btnFindUserMain.Click += new System.EventHandler(this.btnFindUserClick);
			// 
			// gbFilters
			// 
			this.gbFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.gbFilters.Controls.Add(this.cbVersion);
			this.gbFilters.Controls.Add(this.cbPublic);
			this.gbFilters.Controls.Add(this.cbPrivate);
			this.gbFilters.Controls.Add(this.cbFull);
			this.gbFilters.Controls.Add(this.lblPingThreshold);
			this.gbFilters.Controls.Add(this.cbPing);
			this.gbFilters.Controls.Add(this.cbEmpty);
			this.gbFilters.Controls.Add(this.cbTracks);
			this.gbFilters.Location = new System.Drawing.Point(793, 6);
			this.gbFilters.Name = "gbFilters";
			this.gbFilters.Size = new System.Drawing.Size(137, 521);
			this.gbFilters.TabIndex = 4;
			this.gbFilters.TabStop = false;
			this.gbFilters.Text = "Filters";
			// 
			// cbVersion
			// 
			this.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbVersion.FormattingEnabled = true;
			this.cbVersion.Items.AddRange(new object[] {
									"Demo",
									"S1",
									"S2"});
			this.cbVersion.Location = new System.Drawing.Point(6, 372);
			this.cbVersion.Name = "cbVersion";
			this.cbVersion.Size = new System.Drawing.Size(124, 21);
			this.cbVersion.TabIndex = 17;
			this.cbVersion.SelectedIndexChanged += new System.EventHandler(this.CbVersionSelectedIndexChanged);
			// 
			// cbPublic
			// 
			this.cbPublic.Checked = true;
			this.cbPublic.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbPublic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cbPublic.Location = new System.Drawing.Point(75, 420);
			this.cbPublic.Name = "cbPublic";
			this.cbPublic.Size = new System.Drawing.Size(55, 24);
			this.cbPublic.TabIndex = 16;
			this.cbPublic.Text = "Public";
			this.cbPublic.UseVisualStyleBackColor = true;
			this.cbPublic.CheckedChanged += new System.EventHandler(this.CbPublicCheckedChanged);
			// 
			// cbPrivate
			// 
			this.cbPrivate.Checked = true;
			this.cbPrivate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbPrivate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cbPrivate.Location = new System.Drawing.Point(6, 424);
			this.cbPrivate.Name = "cbPrivate";
			this.cbPrivate.Size = new System.Drawing.Size(63, 16);
			this.cbPrivate.TabIndex = 15;
			this.cbPrivate.Text = "Private";
			this.cbPrivate.UseVisualStyleBackColor = true;
			this.cbPrivate.CheckedChanged += new System.EventHandler(this.CbPrivateCheckedChanged);
			// 
			// cbFull
			// 
			this.cbFull.Checked = true;
			this.cbFull.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbFull.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cbFull.Location = new System.Drawing.Point(75, 400);
			this.cbFull.Name = "cbFull";
			this.cbFull.Size = new System.Drawing.Size(44, 16);
			this.cbFull.TabIndex = 14;
			this.cbFull.Text = "Full";
			this.cbFull.UseVisualStyleBackColor = true;
			this.cbFull.CheckedChanged += new System.EventHandler(this.CbFullCheckedChanged);
			// 
			// lblPingThreshold
			// 
			this.lblPingThreshold.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPingThreshold.Location = new System.Drawing.Point(6, 450);
			this.lblPingThreshold.Name = "lblPingThreshold";
			this.lblPingThreshold.Size = new System.Drawing.Size(100, 15);
			this.lblPingThreshold.TabIndex = 13;
			this.lblPingThreshold.Text = "Ping Threshold:";
			// 
			// cbPing
			// 
			this.cbPing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPing.FormattingEnabled = true;
			this.cbPing.Items.AddRange(new object[] {
									"50",
									"100",
									"150",
									"200",
									"300",
									"500",
									"1000",
									"5000"});
			this.cbPing.Location = new System.Drawing.Point(7, 468);
			this.cbPing.Name = "cbPing";
			this.cbPing.Size = new System.Drawing.Size(123, 21);
			this.cbPing.TabIndex = 12;
			this.cbPing.SelectedIndexChanged += new System.EventHandler(this.CbPingSelectedIndexChanged);
			// 
			// cbEmpty
			// 
			this.cbEmpty.Checked = true;
			this.cbEmpty.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbEmpty.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cbEmpty.Location = new System.Drawing.Point(6, 399);
			this.cbEmpty.Name = "cbEmpty";
			this.cbEmpty.Size = new System.Drawing.Size(104, 19);
			this.cbEmpty.TabIndex = 11;
			this.cbEmpty.Text = "Empty";
			this.cbEmpty.UseVisualStyleBackColor = true;
			this.cbEmpty.CheckedChanged += new System.EventHandler(this.CbEmptyCheckedChanged);
			// 
			// cbTracks
			// 
			this.cbTracks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTracks.FormattingEnabled = true;
			this.cbTracks.Items.AddRange(new object[] {
									"All Tracks",
									"Blackwood",
									"South City",
									"Fern Bay",
									"Kyoto",
									"Westhill",
									"Aston",
									"Autocross",
									"Drag",
									"Skid Pad"});
			this.cbTracks.Location = new System.Drawing.Point(7, 495);
			this.cbTracks.Name = "cbTracks";
			this.cbTracks.Size = new System.Drawing.Size(124, 21);
			this.cbTracks.TabIndex = 10;
			this.cbTracks.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTracksSelectedIndexChanged);
			// 
			// lvMain
			// 
			this.lvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvMain.AutoArrange = false;
			this.lvMain.BackColor = System.Drawing.Color.CornflowerBlue;
			this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeaderName,
									this.columnHeaderPing,
									this.columnPrivate,
									this.columnHeaderConnections,
									this.columnHeaderInfo,
									this.columnHeaderTrack,
									this.columnHeaderCars});
			this.lvMain.ContextMenuStrip = this.contextMenuBrowser;
			this.lvMain.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lvMain.FullRowSelect = true;
			this.lvMain.GridLines = true;
			this.lvMain.ImeMode = System.Windows.Forms.ImeMode.On;
			this.lvMain.Location = new System.Drawing.Point(8, 6);
			this.lvMain.MultiSelect = false;
			this.lvMain.Name = "lvMain";
			this.lvMain.OwnerDraw = true;
			this.lvMain.Size = new System.Drawing.Size(779, 520);
			this.lvMain.TabIndex = 3;
			this.lvMain.UseCompatibleStateImageBehavior = false;
			this.lvMain.View = System.Windows.Forms.View.Details;
			this.lvMain.DoubleClick += new System.EventHandler(this.listDblClick);
			this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMainSelectedIndexChanged);
			this.lvMain.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListViewDrawSubItem);
			this.lvMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMainColumnClick);
			this.lvMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ListViewMouseMove);
			this.lvMain.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListViewDrawColumnHeader);
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Server Name";
			this.columnHeaderName.Width = 203;
			// 
			// columnHeaderPing
			// 
			this.columnHeaderPing.Text = "Ping";
			this.columnHeaderPing.Width = 50;
			// 
			// columnPrivate
			// 
			this.columnPrivate.Text = "Private";
			this.columnPrivate.Width = 58;
			// 
			// columnHeaderConnections
			// 
			this.columnHeaderConnections.Text = "Connections";
			this.columnHeaderConnections.Width = 85;
			// 
			// columnHeaderInfo
			// 
			this.columnHeaderInfo.Text = "Info";
			// 
			// columnHeaderTrack
			// 
			this.columnHeaderTrack.Text = "Track";
			this.columnHeaderTrack.Width = 134;
			// 
			// columnHeaderCars
			// 
			this.columnHeaderCars.Text = "Cars";
			this.columnHeaderCars.Width = 180;
			// 
			// contextMenuBrowser
			// 
			this.contextMenuBrowser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.joinServerToolStripMenuItem,
									this.viewServerInformationMain,
									this.toolStripMenuItem1,
									this.copyServerToClipboardToolStripMenuItem,
									this.administrateToolStripMenuItem1});
			this.contextMenuBrowser.Name = "contextMenuBrowser";
			this.contextMenuBrowser.Size = new System.Drawing.Size(203, 114);
			this.contextMenuBrowser.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuBrowserOpening);
			// 
			// joinServerToolStripMenuItem
			// 
			this.joinServerToolStripMenuItem.Name = "joinServerToolStripMenuItem";
			this.joinServerToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.joinServerToolStripMenuItem.Text = "&Join Server";
			this.joinServerToolStripMenuItem.Click += new System.EventHandler(this.btnJoinClick);
			// 
			// viewServerInformationMain
			// 
			this.viewServerInformationMain.Name = "viewServerInformationMain";
			this.viewServerInformationMain.Size = new System.Drawing.Size(202, 22);
			this.viewServerInformationMain.Text = "&View Server Information...";
			this.viewServerInformationMain.Click += new System.EventHandler(this.ViewServerInformationToolStripMenuItemClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
			this.toolStripMenuItem1.Text = "&Add to Favourites";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1Click);
			// 
			// copyServerToClipboardToolStripMenuItem
			// 
			this.copyServerToClipboardToolStripMenuItem.Name = "copyServerToClipboardToolStripMenuItem";
			this.copyServerToClipboardToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.copyServerToClipboardToolStripMenuItem.Text = "&Copy Server to Clipboard";
			this.copyServerToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyServerToClipboardToolStripMenuItemClick);
			// 
			// administrateToolStripMenuItem1
			// 
			this.administrateToolStripMenuItem1.Name = "administrateToolStripMenuItem1";
			this.administrateToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
			this.administrateToolStripMenuItem1.Text = "Administrate...";
			this.administrateToolStripMenuItem1.Click += new System.EventHandler(this.AdministrateToolStripMenuItemClick);
			// 
			// btnRefreshMain
			// 
			this.btnRefreshMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefreshMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnRefreshMain.Location = new System.Drawing.Point(89, 535);
			this.btnRefreshMain.Name = "btnRefreshMain";
			this.btnRefreshMain.Size = new System.Drawing.Size(75, 23);
			this.btnRefreshMain.TabIndex = 2;
			this.btnRefreshMain.Text = "&Refresh";
			this.btnRefreshMain.UseVisualStyleBackColor = true;
			this.btnRefreshMain.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// btnJoinMain
			// 
			this.btnJoinMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnJoinMain.Enabled = false;
			this.btnJoinMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnJoinMain.Location = new System.Drawing.Point(8, 535);
			this.btnJoinMain.Name = "btnJoinMain";
			this.btnJoinMain.Size = new System.Drawing.Size(75, 23);
			this.btnJoinMain.TabIndex = 1;
			this.btnJoinMain.Text = "&Join";
			this.btnJoinMain.UseVisualStyleBackColor = true;
			this.btnJoinMain.Click += new System.EventHandler(this.btnJoinClick);
			// 
			// tabFavourites
			// 
			this.tabFavourites.Controls.Add(this.cbAddServerVersion);
			this.tabFavourites.Controls.Add(this.lblAddressFav);
			this.tabFavourites.Controls.Add(this.edtAddServerAddress);
			this.tabFavourites.Controls.Add(this.btnAddServer);
			this.tabFavourites.Controls.Add(this.btnRefreshFav);
			this.tabFavourites.Controls.Add(this.btnJoinFav);
			this.tabFavourites.Controls.Add(this.lvFavourites);
			this.tabFavourites.Location = new System.Drawing.Point(4, 22);
			this.tabFavourites.Name = "tabFavourites";
			this.tabFavourites.Size = new System.Drawing.Size(938, 561);
			this.tabFavourites.TabIndex = 2;
			this.tabFavourites.Text = "Favourites";
			this.tabFavourites.UseVisualStyleBackColor = true;
			// 
			// cbAddServerVersion
			// 
			this.cbAddServerVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cbAddServerVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAddServerVersion.FormattingEnabled = true;
			this.cbAddServerVersion.Items.AddRange(new object[] {
									"S2",
									"S1",
									"Demo"});
			this.cbAddServerVersion.Location = new System.Drawing.Point(677, 534);
			this.cbAddServerVersion.Name = "cbAddServerVersion";
			this.cbAddServerVersion.Size = new System.Drawing.Size(59, 21);
			this.cbAddServerVersion.TabIndex = 10;
			// 
			// lblAddressFav
			// 
			this.lblAddressFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblAddressFav.Location = new System.Drawing.Point(312, 537);
			this.lblAddressFav.Name = "lblAddressFav";
			this.lblAddressFav.Size = new System.Drawing.Size(186, 16);
			this.lblAddressFav.TabIndex = 9;
			this.lblAddressFav.Text = "Server IP Address:";
			this.lblAddressFav.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// edtAddServerAddress
			// 
			this.edtAddServerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.edtAddServerAddress.Location = new System.Drawing.Point(504, 534);
			this.edtAddServerAddress.Name = "edtAddServerAddress";
			this.edtAddServerAddress.Size = new System.Drawing.Size(167, 21);
			this.edtAddServerAddress.TabIndex = 8;
			this.edtAddServerAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EdtAddServerAddressKeyDown);
			// 
			// btnAddServer
			// 
			this.btnAddServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddServer.Location = new System.Drawing.Point(742, 532);
			this.btnAddServer.Name = "btnAddServer";
			this.btnAddServer.Size = new System.Drawing.Size(132, 23);
			this.btnAddServer.TabIndex = 7;
			this.btnAddServer.Text = "&Add Server";
			this.btnAddServer.UseVisualStyleBackColor = true;
			this.btnAddServer.Click += new System.EventHandler(this.BtnAddServerClick);
			// 
			// btnRefreshFav
			// 
			this.btnRefreshFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefreshFav.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnRefreshFav.Location = new System.Drawing.Point(89, 535);
			this.btnRefreshFav.Name = "btnRefreshFav";
			this.btnRefreshFav.Size = new System.Drawing.Size(75, 23);
			this.btnRefreshFav.TabIndex = 6;
			this.btnRefreshFav.Text = "&Refresh";
			this.btnRefreshFav.UseVisualStyleBackColor = true;
			this.btnRefreshFav.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// btnJoinFav
			// 
			this.btnJoinFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnJoinFav.Enabled = false;
			this.btnJoinFav.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnJoinFav.Location = new System.Drawing.Point(8, 535);
			this.btnJoinFav.Name = "btnJoinFav";
			this.btnJoinFav.Size = new System.Drawing.Size(75, 23);
			this.btnJoinFav.TabIndex = 5;
			this.btnJoinFav.Text = "&Join";
			this.btnJoinFav.UseVisualStyleBackColor = true;
			this.btnJoinFav.Click += new System.EventHandler(this.btnJoinClick);
			// 
			// lvFavourites
			// 
			this.lvFavourites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvFavourites.BackColor = System.Drawing.Color.CornflowerBlue;
			this.lvFavourites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeaderFavServerName,
									this.columnHeaderFavPing,
									this.columnHeaderFavVersion,
									this.columnHeaderFavSlots,
									this.columnHeaderFavInfo,
									this.columnHeaderFavTrack,
									this.columnHeaderFavCars});
			this.lvFavourites.ContextMenuStrip = this.contextMenuFav;
			this.lvFavourites.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvFavourites.FullRowSelect = true;
			this.lvFavourites.GridLines = true;
			this.lvFavourites.Location = new System.Drawing.Point(8, 6);
			this.lvFavourites.MultiSelect = false;
			this.lvFavourites.Name = "lvFavourites";
			this.lvFavourites.OwnerDraw = true;
			this.lvFavourites.Size = new System.Drawing.Size(922, 520);
			this.lvFavourites.TabIndex = 4;
			this.lvFavourites.UseCompatibleStateImageBehavior = false;
			this.lvFavourites.View = System.Windows.Forms.View.Details;
			this.lvFavourites.DoubleClick += new System.EventHandler(this.listDblClick);
			this.lvFavourites.SelectedIndexChanged += new System.EventHandler(this.lvFavouritesSelectedIndexChanged);
			this.lvFavourites.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListViewDrawSubItem);
			this.lvFavourites.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMainColumnClick);
			this.lvFavourites.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ListViewMouseMove);
			this.lvFavourites.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListViewDrawColumnHeader);
			// 
			// columnHeaderFavServerName
			// 
			this.columnHeaderFavServerName.Text = "Server Name";
			this.columnHeaderFavServerName.Width = 158;
			// 
			// columnHeaderFavPing
			// 
			this.columnHeaderFavPing.Text = "Ping";
			this.columnHeaderFavPing.Width = 50;
			// 
			// columnHeaderFavVersion
			// 
			this.columnHeaderFavVersion.Text = "Version";
			this.columnHeaderFavVersion.Width = 63;
			// 
			// columnHeaderFavSlots
			// 
			this.columnHeaderFavSlots.Text = "Connections";
			this.columnHeaderFavSlots.Width = 87;
			// 
			// columnHeaderFavInfo
			// 
			this.columnHeaderFavInfo.Text = "Info";
			// 
			// columnHeaderFavTrack
			// 
			this.columnHeaderFavTrack.Text = "Track";
			this.columnHeaderFavTrack.Width = 117;
			// 
			// columnHeaderFavCars
			// 
			this.columnHeaderFavCars.Text = "Cars";
			this.columnHeaderFavCars.Width = 374;
			// 
			// contextMenuFav
			// 
			this.contextMenuFav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.joinServerToolStripMenuItem1,
									this.viewServerInformationFav,
									this.removeFromFavouritesToolStripMenuItem,
									this.copyServerToClipboardToolStripMenuItem1,
									this.administrateToolStripMenuItem});
			this.contextMenuFav.Name = "contextMenuFav";
			this.contextMenuFav.Size = new System.Drawing.Size(203, 114);
			this.contextMenuFav.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuFavOpening);
			// 
			// joinServerToolStripMenuItem1
			// 
			this.joinServerToolStripMenuItem1.Name = "joinServerToolStripMenuItem1";
			this.joinServerToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
			this.joinServerToolStripMenuItem1.Text = "&Join Server";
			this.joinServerToolStripMenuItem1.Click += new System.EventHandler(this.btnJoinClick);
			// 
			// viewServerInformationFav
			// 
			this.viewServerInformationFav.Name = "viewServerInformationFav";
			this.viewServerInformationFav.Size = new System.Drawing.Size(202, 22);
			this.viewServerInformationFav.Text = "&View Server Information...";
			this.viewServerInformationFav.Click += new System.EventHandler(this.ViewServerInformationToolStripMenuItemClick);
			// 
			// removeFromFavouritesToolStripMenuItem
			// 
			this.removeFromFavouritesToolStripMenuItem.Name = "removeFromFavouritesToolStripMenuItem";
			this.removeFromFavouritesToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.removeFromFavouritesToolStripMenuItem.Text = "&Remove From Favourites";
			this.removeFromFavouritesToolStripMenuItem.Click += new System.EventHandler(this.RemoveFromFavouritesToolStripMenuItemClick);
			// 
			// copyServerToClipboardToolStripMenuItem1
			// 
			this.copyServerToClipboardToolStripMenuItem1.Name = "copyServerToClipboardToolStripMenuItem1";
			this.copyServerToClipboardToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
			this.copyServerToClipboardToolStripMenuItem1.Text = "&Copy Server to Clipboard";
			this.copyServerToClipboardToolStripMenuItem1.Click += new System.EventHandler(this.CopyServerToClipboardToolStripMenuItemClick);
			// 
			// administrateToolStripMenuItem
			// 
			this.administrateToolStripMenuItem.Name = "administrateToolStripMenuItem";
			this.administrateToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.administrateToolStripMenuItem.Text = "Administrate...";
			this.administrateToolStripMenuItem.Click += new System.EventHandler(this.AdministrateToolStripMenuItemClick);
			// 
			// tabFriends
			// 
			this.tabFriends.Controls.Add(this.edtFriendName);
			this.tabFriends.Controls.Add(this.btnAddFriend);
			this.tabFriends.Controls.Add(this.cbHideOffline);
			this.tabFriends.Controls.Add(this.btnRefreshFriend);
			this.tabFriends.Controls.Add(this.btnJoinFriend);
			this.tabFriends.Controls.Add(this.lvFriends);
			this.tabFriends.Location = new System.Drawing.Point(4, 22);
			this.tabFriends.Name = "tabFriends";
			this.tabFriends.Size = new System.Drawing.Size(938, 561);
			this.tabFriends.TabIndex = 3;
			this.tabFriends.Text = "Friends";
			this.tabFriends.UseVisualStyleBackColor = true;
			// 
			// edtFriendName
			// 
			this.edtFriendName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.edtFriendName.Location = new System.Drawing.Point(536, 534);
			this.edtFriendName.Name = "edtFriendName";
			this.edtFriendName.Size = new System.Drawing.Size(182, 21);
			this.edtFriendName.TabIndex = 6;
			this.edtFriendName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EdtFriendNameKeyDown);
			// 
			// btnAddFriend
			// 
			this.btnAddFriend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFriend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAddFriend.Location = new System.Drawing.Point(724, 532);
			this.btnAddFriend.Name = "btnAddFriend";
			this.btnAddFriend.Size = new System.Drawing.Size(118, 23);
			this.btnAddFriend.TabIndex = 5;
			this.btnAddFriend.Text = "&Add Friend";
			this.btnAddFriend.UseVisualStyleBackColor = true;
			this.btnAddFriend.Click += new System.EventHandler(this.BtnAddFriendClick);
			// 
			// cbHideOffline
			// 
			this.cbHideOffline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbHideOffline.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cbHideOffline.Location = new System.Drawing.Point(256, 533);
			this.cbHideOffline.Name = "cbHideOffline";
			this.cbHideOffline.Size = new System.Drawing.Size(278, 24);
			this.cbHideOffline.TabIndex = 4;
			this.cbHideOffline.Text = "Hide Offline Friends";
			this.cbHideOffline.UseVisualStyleBackColor = true;
			this.cbHideOffline.CheckedChanged += new System.EventHandler(this.CheckBox2CheckedChanged);
			// 
			// btnRefreshFriend
			// 
			this.btnRefreshFriend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefreshFriend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnRefreshFriend.Location = new System.Drawing.Point(119, 532);
			this.btnRefreshFriend.Name = "btnRefreshFriend";
			this.btnRefreshFriend.Size = new System.Drawing.Size(105, 23);
			this.btnRefreshFriend.TabIndex = 2;
			this.btnRefreshFriend.Text = "&Refresh List";
			this.btnRefreshFriend.UseVisualStyleBackColor = true;
			this.btnRefreshFriend.Click += new System.EventHandler(this.BtnRefreshFriendClick);
			// 
			// btnJoinFriend
			// 
			this.btnJoinFriend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnJoinFriend.Enabled = false;
			this.btnJoinFriend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnJoinFriend.Location = new System.Drawing.Point(8, 532);
			this.btnJoinFriend.Name = "btnJoinFriend";
			this.btnJoinFriend.Size = new System.Drawing.Size(105, 23);
			this.btnJoinFriend.TabIndex = 1;
			this.btnJoinFriend.Text = "&Join Friend";
			this.btnJoinFriend.UseVisualStyleBackColor = true;
			this.btnJoinFriend.Click += new System.EventHandler(this.JoinFriendClick);
			// 
			// lvFriends
			// 
			this.lvFriends.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvFriends.AutoArrange = false;
			this.lvFriends.BackColor = System.Drawing.Color.CornflowerBlue;
			this.lvFriends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnFriendName,
									this.columnFriendServer,
									this.columnFriendPrivate,
									this.columnFriendPlayers});
			this.lvFriends.ContextMenuStrip = this.contextMenuFriends;
			this.lvFriends.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvFriends.FullRowSelect = true;
			this.lvFriends.GridLines = true;
			this.lvFriends.HideOffline = false;
			this.lvFriends.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem1});
			this.lvFriends.Location = new System.Drawing.Point(8, 6);
			this.lvFriends.MultiSelect = false;
			this.lvFriends.Name = "lvFriends";
			this.lvFriends.OwnerDraw = true;
			this.lvFriends.Size = new System.Drawing.Size(922, 520);
			this.lvFriends.TabIndex = 0;
			this.lvFriends.UseCompatibleStateImageBehavior = false;
			this.lvFriends.View = System.Windows.Forms.View.Details;
			this.lvFriends.DoubleClick += new System.EventHandler(this.LvFriendsDoubleClick);
			this.lvFriends.SelectedIndexChanged += new System.EventHandler(this.LvFriendsSelectedIndexChanged);
			this.lvFriends.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.ListViewDrawSubItem);
			this.lvFriends.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMainColumnClick);
			this.lvFriends.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ListViewMouseMove);
			this.lvFriends.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListViewDrawColumnHeader);
			// 
			// columnFriendName
			// 
			this.columnFriendName.Text = "Friend Name";
			this.columnFriendName.Width = 138;
			// 
			// columnFriendServer
			// 
			this.columnFriendServer.Text = "Server Name";
			this.columnFriendServer.Width = 173;
			// 
			// columnFriendPrivate
			// 
			this.columnFriendPrivate.Text = "Private";
			this.columnFriendPrivate.Width = 66;
			// 
			// columnFriendPlayers
			// 
			this.columnFriendPlayers.Text = "Players";
			this.columnFriendPlayers.Width = 523;
			// 
			// contextMenuFriends
			// 
			this.contextMenuFriends.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.joinServerMenuFriends,
									this.viewServerInformationFriend,
									this.removeFriendToolStripMenuItem});
			this.contextMenuFriends.Name = "contextMenuFriends";
			this.contextMenuFriends.Size = new System.Drawing.Size(203, 70);
			this.contextMenuFriends.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuFriendsOpening);
			// 
			// joinServerMenuFriends
			// 
			this.joinServerMenuFriends.Name = "joinServerMenuFriends";
			this.joinServerMenuFriends.Size = new System.Drawing.Size(202, 22);
			this.joinServerMenuFriends.Text = "Join Server";
			this.joinServerMenuFriends.Click += new System.EventHandler(this.JoinFriendClick);
			// 
			// viewServerInformationFriend
			// 
			this.viewServerInformationFriend.Name = "viewServerInformationFriend";
			this.viewServerInformationFriend.Size = new System.Drawing.Size(202, 22);
			this.viewServerInformationFriend.Text = "View Server Information...";
			this.viewServerInformationFriend.Click += new System.EventHandler(this.ViewServerInformationToolStripMenuItemClick);
			// 
			// removeFriendToolStripMenuItem
			// 
			this.removeFriendToolStripMenuItem.Name = "removeFriendToolStripMenuItem";
			this.removeFriendToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.removeFriendToolStripMenuItem.Text = "Remove Friend";
			this.removeFriendToolStripMenuItem.Click += new System.EventHandler(this.RemoveFriendToolStripMenuItemClick);
			// 
			// tabConfig
			// 
			this.tabConfig.Controls.Add(this.gbConfigurationMain);
			this.tabConfig.Location = new System.Drawing.Point(4, 22);
			this.tabConfig.Name = "tabConfig";
			this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
			this.tabConfig.Size = new System.Drawing.Size(938, 561);
			this.tabConfig.TabIndex = 1;
			this.tabConfig.Text = "Configuration";
			this.tabConfig.UseVisualStyleBackColor = true;
			// 
			// gbConfigurationMain
			// 
			this.gbConfigurationMain.Controls.Add(this.gbLiveForSpeed);
			this.gbConfigurationMain.Controls.Add(this.gbLanguage);
			this.gbConfigurationMain.Controls.Add(this.gbStartBeforeLFS);
			this.gbConfigurationMain.Controls.Add(this.gbAdvanced);
			this.gbConfigurationMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbConfigurationMain.Location = new System.Drawing.Point(3, 3);
			this.gbConfigurationMain.Name = "gbConfigurationMain";
			this.gbConfigurationMain.Size = new System.Drawing.Size(932, 555);
			this.gbConfigurationMain.TabIndex = 0;
			this.gbConfigurationMain.TabStop = false;
			this.gbConfigurationMain.Text = "Configuration";
			// 
			// gbLiveForSpeed
			// 
			this.gbLiveForSpeed.Controls.Add(this.txtInsimPort);
			this.gbLiveForSpeed.Controls.Add(this.lblInsimPortConfig);
			this.gbLiveForSpeed.Controls.Add(this.pathList);
			this.gbLiveForSpeed.Controls.Add(this.lblExeDescriptionConfig);
			this.gbLiveForSpeed.Controls.Add(this.lblExePathConfig);
			this.gbLiveForSpeed.Controls.Add(this.buttonBrowse);
			this.gbLiveForSpeed.Location = new System.Drawing.Point(13, 20);
			this.gbLiveForSpeed.Name = "gbLiveForSpeed";
			this.gbLiveForSpeed.Size = new System.Drawing.Size(437, 202);
			this.gbLiveForSpeed.TabIndex = 18;
			this.gbLiveForSpeed.TabStop = false;
			this.gbLiveForSpeed.Text = "Live For Speed";
			// 
			// txtInsimPort
			// 
			this.txtInsimPort.Location = new System.Drawing.Point(131, 152);
			this.txtInsimPort.Name = "txtInsimPort";
			this.txtInsimPort.Size = new System.Drawing.Size(67, 21);
			this.txtInsimPort.TabIndex = 26;
			this.txtInsimPort.Text = "29999";
			// 
			// lblInsimPortConfig
			// 
			this.lblInsimPortConfig.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblInsimPortConfig.Location = new System.Drawing.Point(11, 155);
			this.lblInsimPortConfig.Name = "lblInsimPortConfig";
			this.lblInsimPortConfig.Size = new System.Drawing.Size(90, 23);
			this.lblInsimPortConfig.TabIndex = 25;
			this.lblInsimPortConfig.Text = "Insim Port:";
			// 
			// pathList
			// 
			this.pathList.FormattingEnabled = true;
			this.pathList.Location = new System.Drawing.Point(11, 45);
			this.pathList.Name = "pathList";
			this.pathList.Size = new System.Drawing.Size(322, 56);
			this.pathList.TabIndex = 24;
			// 
			// lblExeDescriptionConfig
			// 
			this.lblExeDescriptionConfig.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblExeDescriptionConfig.Location = new System.Drawing.Point(9, 104);
			this.lblExeDescriptionConfig.Name = "lblExeDescriptionConfig";
			this.lblExeDescriptionConfig.Size = new System.Drawing.Size(419, 41);
			this.lblExeDescriptionConfig.TabIndex = 23;
			this.lblExeDescriptionConfig.Text = "If there is more than one entry in the box above, please select which one is the " +
			"correct path for your LFS.exe. If there are no entries, please click the Browse " +
			"button to manually locate it.";
			// 
			// lblExePathConfig
			// 
			this.lblExePathConfig.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblExePathConfig.Location = new System.Drawing.Point(11, 24);
			this.lblExePathConfig.Name = "lblExePathConfig";
			this.lblExePathConfig.Size = new System.Drawing.Size(114, 18);
			this.lblExePathConfig.TabIndex = 22;
			this.lblExePathConfig.Text = "Executable Path:";
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.buttonBrowse.Location = new System.Drawing.Point(339, 45);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(89, 23);
			this.buttonBrowse.TabIndex = 21;
			this.buttonBrowse.Text = "&Browse...";
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.btnBrowseClick);
			// 
			// gbLanguage
			// 
			this.gbLanguage.Controls.Add(this.gbLangDetails);
			this.gbLanguage.Controls.Add(this.label1);
			this.gbLanguage.Controls.Add(this.cbConfigLang);
			this.gbLanguage.Controls.Add(this.lblLanguageConfig);
			this.gbLanguage.Location = new System.Drawing.Point(489, 20);
			this.gbLanguage.Name = "gbLanguage";
			this.gbLanguage.Size = new System.Drawing.Size(437, 171);
			this.gbLanguage.TabIndex = 17;
			this.gbLanguage.TabStop = false;
			this.gbLanguage.Text = "Language";
			// 
			// gbLangDetails
			// 
			this.gbLangDetails.Controls.Add(this.lblCommentReal);
			this.gbLangDetails.Controls.Add(this.lblEmailReal);
			this.gbLangDetails.Controls.Add(this.lblAuthorReal);
			this.gbLangDetails.Controls.Add(this.lblLangComment);
			this.gbLangDetails.Controls.Add(this.lblLangContact);
			this.gbLangDetails.Controls.Add(this.lblLangAuthor);
			this.gbLangDetails.Location = new System.Drawing.Point(24, 53);
			this.gbLangDetails.Name = "gbLangDetails";
			this.gbLangDetails.Size = new System.Drawing.Size(390, 104);
			this.gbLangDetails.TabIndex = 24;
			this.gbLangDetails.TabStop = false;
			this.gbLangDetails.Text = "Language Details";
			// 
			// lblCommentReal
			// 
			this.lblCommentReal.Location = new System.Drawing.Point(116, 63);
			this.lblCommentReal.Name = "lblCommentReal";
			this.lblCommentReal.Size = new System.Drawing.Size(225, 38);
			this.lblCommentReal.TabIndex = 5;
			this.lblCommentReal.Text = "N/A";
			// 
			// lblEmailReal
			// 
			this.lblEmailReal.Location = new System.Drawing.Point(116, 40);
			this.lblEmailReal.Name = "lblEmailReal";
			this.lblEmailReal.Size = new System.Drawing.Size(225, 23);
			this.lblEmailReal.TabIndex = 4;
			this.lblEmailReal.Text = "N/A";
			// 
			// lblAuthorReal
			// 
			this.lblAuthorReal.Location = new System.Drawing.Point(116, 17);
			this.lblAuthorReal.Name = "lblAuthorReal";
			this.lblAuthorReal.Size = new System.Drawing.Size(212, 23);
			this.lblAuthorReal.TabIndex = 3;
			this.lblAuthorReal.Text = "N/A";
			// 
			// lblLangComment
			// 
			this.lblLangComment.Location = new System.Drawing.Point(10, 63);
			this.lblLangComment.Name = "lblLangComment";
			this.lblLangComment.Size = new System.Drawing.Size(100, 23);
			this.lblLangComment.TabIndex = 2;
			this.lblLangComment.Text = "Comments:";
			// 
			// lblLangContact
			// 
			this.lblLangContact.Location = new System.Drawing.Point(10, 40);
			this.lblLangContact.Name = "lblLangContact";
			this.lblLangContact.Size = new System.Drawing.Size(100, 23);
			this.lblLangContact.TabIndex = 1;
			this.lblLangContact.Text = "Contact Address:";
			// 
			// lblLangAuthor
			// 
			this.lblLangAuthor.Location = new System.Drawing.Point(10, 17);
			this.lblLangAuthor.Name = "lblLangAuthor";
			this.lblLangAuthor.Size = new System.Drawing.Size(100, 23);
			this.lblLangAuthor.TabIndex = 0;
			this.lblLangAuthor.Text = "Author:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(34, 59);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 27);
			this.label1.TabIndex = 23;
			// 
			// cbConfigLang
			// 
			this.cbConfigLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbConfigLang.FormattingEnabled = true;
			this.cbConfigLang.Location = new System.Drawing.Point(99, 24);
			this.cbConfigLang.Name = "cbConfigLang";
			this.cbConfigLang.Size = new System.Drawing.Size(188, 21);
			this.cbConfigLang.Sorted = true;
			this.cbConfigLang.TabIndex = 22;
			this.cbConfigLang.SelectedIndexChanged += new System.EventHandler(this.CbConfigLangSelectedIndexChanged);
			// 
			// lblLanguageConfig
			// 
			this.lblLanguageConfig.Location = new System.Drawing.Point(24, 27);
			this.lblLanguageConfig.Name = "lblLanguageConfig";
			this.lblLanguageConfig.Size = new System.Drawing.Size(69, 23);
			this.lblLanguageConfig.TabIndex = 21;
			this.lblLanguageConfig.Text = "Language:";
			// 
			// gbStartBeforeLFS
			// 
			this.gbStartBeforeLFS.Controls.Add(this.btnProgramEnable);
			this.gbStartBeforeLFS.Controls.Add(this.btnProgramDelete);
			this.gbStartBeforeLFS.Controls.Add(this.btnProgramNew);
			this.gbStartBeforeLFS.Controls.Add(this.gbProgramConfig);
			this.gbStartBeforeLFS.Controls.Add(this.lbPreStart);
			this.gbStartBeforeLFS.Location = new System.Drawing.Point(13, 228);
			this.gbStartBeforeLFS.Name = "gbStartBeforeLFS";
			this.gbStartBeforeLFS.Size = new System.Drawing.Size(437, 321);
			this.gbStartBeforeLFS.TabIndex = 16;
			this.gbStartBeforeLFS.TabStop = false;
			this.gbStartBeforeLFS.Text = "Start Before LFS";
			// 
			// btnProgramEnable
			// 
			this.btnProgramEnable.Enabled = false;
			this.btnProgramEnable.Location = new System.Drawing.Point(292, 116);
			this.btnProgramEnable.Name = "btnProgramEnable";
			this.btnProgramEnable.Size = new System.Drawing.Size(130, 23);
			this.btnProgramEnable.TabIndex = 8;
			this.btnProgramEnable.Text = "&Enable";
			this.btnProgramEnable.UseVisualStyleBackColor = true;
			this.btnProgramEnable.Click += new System.EventHandler(this.BtnProgramEnableClick);
			// 
			// btnProgramDelete
			// 
			this.btnProgramDelete.Enabled = false;
			this.btnProgramDelete.Location = new System.Drawing.Point(292, 49);
			this.btnProgramDelete.Name = "btnProgramDelete";
			this.btnProgramDelete.Size = new System.Drawing.Size(130, 23);
			this.btnProgramDelete.TabIndex = 7;
			this.btnProgramDelete.Text = "&Remove Program";
			this.btnProgramDelete.UseVisualStyleBackColor = true;
			this.btnProgramDelete.Click += new System.EventHandler(this.BtnProgramDeleteClick);
			// 
			// btnProgramNew
			// 
			this.btnProgramNew.Location = new System.Drawing.Point(292, 20);
			this.btnProgramNew.Name = "btnProgramNew";
			this.btnProgramNew.Size = new System.Drawing.Size(130, 23);
			this.btnProgramNew.TabIndex = 6;
			this.btnProgramNew.Text = "&New Program";
			this.btnProgramNew.UseVisualStyleBackColor = true;
			this.btnProgramNew.Click += new System.EventHandler(this.BtnProgramNewClick);
			// 
			// gbProgramConfig
			// 
			this.gbProgramConfig.Controls.Add(this.btnProgramCancel);
			this.gbProgramConfig.Controls.Add(this.btnProgramSave);
			this.gbProgramConfig.Controls.Add(this.edtProgramOptions);
			this.gbProgramConfig.Controls.Add(this.lblProgramConfigArg);
			this.gbProgramConfig.Controls.Add(this.edtProgramName);
			this.gbProgramConfig.Controls.Add(this.lblProgramConfigName);
			this.gbProgramConfig.Controls.Add(this.btnProgramBrowse);
			this.gbProgramConfig.Controls.Add(this.lblProgramConfigPath);
			this.gbProgramConfig.Controls.Add(this.edtProgramPath);
			this.gbProgramConfig.Location = new System.Drawing.Point(12, 173);
			this.gbProgramConfig.Name = "gbProgramConfig";
			this.gbProgramConfig.Size = new System.Drawing.Size(410, 135);
			this.gbProgramConfig.TabIndex = 5;
			this.gbProgramConfig.TabStop = false;
			this.gbProgramConfig.Text = "Program Configuration";
			// 
			// btnProgramCancel
			// 
			this.btnProgramCancel.Enabled = false;
			this.btnProgramCancel.Location = new System.Drawing.Point(108, 101);
			this.btnProgramCancel.Name = "btnProgramCancel";
			this.btnProgramCancel.Size = new System.Drawing.Size(96, 23);
			this.btnProgramCancel.TabIndex = 10;
			this.btnProgramCancel.Text = "C&ancel";
			this.btnProgramCancel.UseVisualStyleBackColor = true;
			this.btnProgramCancel.Click += new System.EventHandler(this.BtnProgramCancelClick);
			// 
			// btnProgramSave
			// 
			this.btnProgramSave.Enabled = false;
			this.btnProgramSave.Location = new System.Drawing.Point(6, 101);
			this.btnProgramSave.Name = "btnProgramSave";
			this.btnProgramSave.Size = new System.Drawing.Size(96, 23);
			this.btnProgramSave.TabIndex = 9;
			this.btnProgramSave.Text = "&Save";
			this.btnProgramSave.UseVisualStyleBackColor = true;
			this.btnProgramSave.Click += new System.EventHandler(this.BtnProgramSaveClick);
			// 
			// edtProgramOptions
			// 
			this.edtProgramOptions.Enabled = false;
			this.edtProgramOptions.Location = new System.Drawing.Point(87, 74);
			this.edtProgramOptions.Name = "edtProgramOptions";
			this.edtProgramOptions.Size = new System.Drawing.Size(210, 21);
			this.edtProgramOptions.TabIndex = 8;
			// 
			// lblProgramConfigArg
			// 
			this.lblProgramConfigArg.Location = new System.Drawing.Point(6, 77);
			this.lblProgramConfigArg.Name = "lblProgramConfigArg";
			this.lblProgramConfigArg.Size = new System.Drawing.Size(70, 14);
			this.lblProgramConfigArg.TabIndex = 7;
			this.lblProgramConfigArg.Text = "Arguments:";
			// 
			// edtProgramName
			// 
			this.edtProgramName.Enabled = false;
			this.edtProgramName.Location = new System.Drawing.Point(87, 20);
			this.edtProgramName.Name = "edtProgramName";
			this.edtProgramName.Size = new System.Drawing.Size(137, 21);
			this.edtProgramName.TabIndex = 6;
			this.edtProgramName.TextChanged += new System.EventHandler(this.EdtProgramNameTextChanged);
			// 
			// lblProgramConfigName
			// 
			this.lblProgramConfigName.Location = new System.Drawing.Point(6, 23);
			this.lblProgramConfigName.Name = "lblProgramConfigName";
			this.lblProgramConfigName.Size = new System.Drawing.Size(100, 20);
			this.lblProgramConfigName.TabIndex = 5;
			this.lblProgramConfigName.Text = "Name:";
			// 
			// btnProgramBrowse
			// 
			this.btnProgramBrowse.Enabled = false;
			this.btnProgramBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnProgramBrowse.Location = new System.Drawing.Point(303, 45);
			this.btnProgramBrowse.Name = "btnProgramBrowse";
			this.btnProgramBrowse.Size = new System.Drawing.Size(98, 23);
			this.btnProgramBrowse.TabIndex = 4;
			this.btnProgramBrowse.Text = "B&rowse...";
			this.btnProgramBrowse.UseVisualStyleBackColor = true;
			this.btnProgramBrowse.Click += new System.EventHandler(this.BtnBrowsePSClick);
			// 
			// lblProgramConfigPath
			// 
			this.lblProgramConfigPath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblProgramConfigPath.Location = new System.Drawing.Point(6, 50);
			this.lblProgramConfigPath.Name = "lblProgramConfigPath";
			this.lblProgramConfigPath.Size = new System.Drawing.Size(59, 15);
			this.lblProgramConfigPath.TabIndex = 3;
			this.lblProgramConfigPath.Text = "Path:";
			// 
			// edtProgramPath
			// 
			this.edtProgramPath.Enabled = false;
			this.edtProgramPath.Location = new System.Drawing.Point(87, 47);
			this.edtProgramPath.Name = "edtProgramPath";
			this.edtProgramPath.Size = new System.Drawing.Size(210, 21);
			this.edtProgramPath.TabIndex = 7;
			this.edtProgramPath.TextChanged += new System.EventHandler(this.EdtProgramNameTextChanged);
			// 
			// lbPreStart
			// 
			this.lbPreStart.FormattingEnabled = true;
			this.lbPreStart.Location = new System.Drawing.Point(12, 20);
			this.lbPreStart.Name = "lbPreStart";
			this.lbPreStart.Size = new System.Drawing.Size(274, 147);
			this.lbPreStart.TabIndex = 4;
			this.lbPreStart.DoubleClick += new System.EventHandler(this.LbPreStartDoubleClick);
			this.lbPreStart.SelectedIndexChanged += new System.EventHandler(this.LbPreStartSelectedIndexChanged);
			// 
			// gbAdvanced
			// 
			this.gbAdvanced.Controls.Add(this.cbDoubleClick);
			this.gbAdvanced.Controls.Add(this.gbQueryWait);
			this.gbAdvanced.Controls.Add(this.gbWhenBFSStarts);
			this.gbAdvanced.Controls.Add(this.lblDoubleClick);
			this.gbAdvanced.Controls.Add(this.cbColouredHostnames);
			this.gbAdvanced.Location = new System.Drawing.Point(489, 197);
			this.gbAdvanced.Name = "gbAdvanced";
			this.gbAdvanced.Size = new System.Drawing.Size(437, 352);
			this.gbAdvanced.TabIndex = 14;
			this.gbAdvanced.TabStop = false;
			this.gbAdvanced.Text = "Advanced";
			// 
			// cbDoubleClick
			// 
			this.cbDoubleClick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDoubleClick.FormattingEnabled = true;
			this.cbDoubleClick.Items.AddRange(new object[] {
									"View Server Information",
									"Join Server"});
			this.cbDoubleClick.Location = new System.Drawing.Point(224, 16);
			this.cbDoubleClick.Name = "cbDoubleClick";
			this.cbDoubleClick.Size = new System.Drawing.Size(207, 21);
			this.cbDoubleClick.TabIndex = 27;
			// 
			// gbQueryWait
			// 
			this.gbQueryWait.Controls.Add(this.lblQueryWaitDescription);
			this.gbQueryWait.Controls.Add(this.queryWait);
			this.gbQueryWait.Controls.Add(this.cbQueryWait);
			this.gbQueryWait.Controls.Add(this.lblQueryWaitHelp);
			this.gbQueryWait.Location = new System.Drawing.Point(6, 191);
			this.gbQueryWait.Name = "gbQueryWait";
			this.gbQueryWait.Size = new System.Drawing.Size(425, 155);
			this.gbQueryWait.TabIndex = 24;
			this.gbQueryWait.TabStop = false;
			this.gbQueryWait.Text = "Query Wait";
			// 
			// lblQueryWaitDescription
			// 
			this.lblQueryWaitDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblQueryWaitDescription.Location = new System.Drawing.Point(79, 41);
			this.lblQueryWaitDescription.Name = "lblQueryWaitDescription";
			this.lblQueryWaitDescription.Size = new System.Drawing.Size(329, 57);
			this.lblQueryWaitDescription.TabIndex = 25;
			this.lblQueryWaitDescription.Text = "Query Wait value. This is the time to wait before adding another query to the con" +
			"current queries. Increase this value if, when querying, all servers start giving" +
			" \"Connection Refused\"";
			// 
			// queryWait
			// 
			this.queryWait.Increment = new decimal(new int[] {
									10,
									0,
									0,
									0});
			this.queryWait.Location = new System.Drawing.Point(15, 44);
			this.queryWait.Maximum = new decimal(new int[] {
									1000,
									0,
									0,
									0});
			this.queryWait.Minimum = new decimal(new int[] {
									100,
									0,
									0,
									0});
			this.queryWait.Name = "queryWait";
			this.queryWait.Size = new System.Drawing.Size(52, 21);
			this.queryWait.TabIndex = 24;
			this.queryWait.Value = new decimal(new int[] {
									150,
									0,
									0,
									0});
			// 
			// cbQueryWait
			// 
			this.cbQueryWait.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cbQueryWait.Location = new System.Drawing.Point(15, 14);
			this.cbQueryWait.Name = "cbQueryWait";
			this.cbQueryWait.Size = new System.Drawing.Size(294, 24);
			this.cbQueryWait.TabIndex = 23;
			this.cbQueryWait.Text = "Disable Query Wait";
			this.cbQueryWait.UseVisualStyleBackColor = true;
			this.cbQueryWait.CheckedChanged += new System.EventHandler(this.CbQueryWaitCheckedChanged);
			// 
			// lblQueryWaitHelp
			// 
			this.lblQueryWaitHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblQueryWaitHelp.Location = new System.Drawing.Point(18, 98);
			this.lblQueryWaitHelp.Name = "lblQueryWaitHelp";
			this.lblQueryWaitHelp.Size = new System.Drawing.Size(401, 50);
			this.lblQueryWaitHelp.TabIndex = 22;
			this.lblQueryWaitHelp.Text = "Only disable Query Wait if you are certain your Operating System does not have a " +
			"connection limit.  Please see readme.txt for more information. ";
			// 
			// gbWhenBFSStarts
			// 
			this.gbWhenBFSStarts.Controls.Add(this.cbFriendRefresh);
			this.gbWhenBFSStarts.Controls.Add(this.btnCheckNewVersion);
			this.gbWhenBFSStarts.Controls.Add(this.cbFavRefresh);
			this.gbWhenBFSStarts.Controls.Add(this.cbNewVersion);
			this.gbWhenBFSStarts.Location = new System.Drawing.Point(6, 73);
			this.gbWhenBFSStarts.Name = "gbWhenBFSStarts";
			this.gbWhenBFSStarts.Size = new System.Drawing.Size(425, 112);
			this.gbWhenBFSStarts.TabIndex = 23;
			this.gbWhenBFSStarts.TabStop = false;
			this.gbWhenBFSStarts.Text = "When Browse For Speed Starts";
			// 
			// cbFriendRefresh
			// 
			this.cbFriendRefresh.Location = new System.Drawing.Point(12, 48);
			this.cbFriendRefresh.Name = "cbFriendRefresh";
			this.cbFriendRefresh.Size = new System.Drawing.Size(361, 24);
			this.cbFriendRefresh.TabIndex = 21;
			this.cbFriendRefresh.Text = "Refresh Friends list";
			this.cbFriendRefresh.UseVisualStyleBackColor = true;
			// 
			// btnCheckNewVersion
			// 
			this.btnCheckNewVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCheckNewVersion.Location = new System.Drawing.Point(317, 21);
			this.btnCheckNewVersion.Name = "btnCheckNewVersion";
			this.btnCheckNewVersion.Size = new System.Drawing.Size(91, 23);
			this.btnCheckNewVersion.TabIndex = 13;
			this.btnCheckNewVersion.Text = "&Check Now";
			this.btnCheckNewVersion.UseVisualStyleBackColor = true;
			this.btnCheckNewVersion.Click += new System.EventHandler(this.btnCheckNewVersionClick);
			// 
			// cbFavRefresh
			// 
			this.cbFavRefresh.Location = new System.Drawing.Point(12, 78);
			this.cbFavRefresh.Name = "cbFavRefresh";
			this.cbFavRefresh.Size = new System.Drawing.Size(350, 24);
			this.cbFavRefresh.TabIndex = 19;
			this.cbFavRefresh.Text = "Refresh Favourite Servers";
			this.cbFavRefresh.UseVisualStyleBackColor = true;
			this.cbFavRefresh.CheckedChanged += new System.EventHandler(this.ChkStartupRefreshCheckedChanged);
			// 
			// cbNewVersion
			// 
			this.cbNewVersion.AutoEllipsis = true;
			this.cbNewVersion.Checked = true;
			this.cbNewVersion.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbNewVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cbNewVersion.Location = new System.Drawing.Point(12, 24);
			this.cbNewVersion.Name = "cbNewVersion";
			this.cbNewVersion.Size = new System.Drawing.Size(253, 18);
			this.cbNewVersion.TabIndex = 12;
			this.cbNewVersion.Text = "Check for a new version of BFS";
			this.cbNewVersion.UseVisualStyleBackColor = true;
			// 
			// lblDoubleClick
			// 
			this.lblDoubleClick.Location = new System.Drawing.Point(18, 17);
			this.lblDoubleClick.Name = "lblDoubleClick";
			this.lblDoubleClick.Size = new System.Drawing.Size(200, 23);
			this.lblDoubleClick.TabIndex = 22;
			this.lblDoubleClick.Text = "When double clicking a server:";
			// 
			// cbColouredHostnames
			// 
			this.cbColouredHostnames.Checked = true;
			this.cbColouredHostnames.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbColouredHostnames.Location = new System.Drawing.Point(18, 43);
			this.cbColouredHostnames.Name = "cbColouredHostnames";
			this.cbColouredHostnames.Size = new System.Drawing.Size(364, 24);
			this.cbColouredHostnames.TabIndex = 20;
			this.cbColouredHostnames.Text = "Show coloured server names";
			this.cbColouredHostnames.UseVisualStyleBackColor = true;
			this.cbColouredHostnames.CheckedChanged += new System.EventHandler(this.CbColouredHostnamesCheckedChanged);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "LFS.exe";
			this.openFileDialog.Filter = "LFS Executable|LFS.exe";
			this.openFileDialog.SupportMultiDottedExtensions = true;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.statusTotal,
									this.statusRefused,
									this.statusNoReply});
			this.statusStrip.Location = new System.Drawing.Point(0, 611);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(946, 22);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 2;
			// 
			// statusTotal
			// 
			this.statusTotal.Name = "statusTotal";
			this.statusTotal.Size = new System.Drawing.Size(0, 17);
			// 
			// statusRefused
			// 
			this.statusRefused.Name = "statusRefused";
			this.statusRefused.Size = new System.Drawing.Size(0, 17);
			// 
			// statusNoReply
			// 
			this.statusNoReply.Name = "statusNoReply";
			this.statusNoReply.Size = new System.Drawing.Size(0, 17);
			this.statusNoReply.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
			// 
			// openFileDialogPS
			// 
			this.openFileDialogPS.Filter = "Executables|*.exe";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(946, 633);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.statusStrip);
			this.DoubleBuffered = true;
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Browse For Speed";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormClosed);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainKeyDown);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.tabMain.PerformLayout();
			this.gbFilters.ResumeLayout(false);
			this.contextMenuBrowser.ResumeLayout(false);
			this.tabFavourites.ResumeLayout(false);
			this.tabFavourites.PerformLayout();
			this.contextMenuFav.ResumeLayout(false);
			this.tabFriends.ResumeLayout(false);
			this.tabFriends.PerformLayout();
			this.contextMenuFriends.ResumeLayout(false);
			this.tabConfig.ResumeLayout(false);
			this.gbConfigurationMain.ResumeLayout(false);
			this.gbLiveForSpeed.ResumeLayout(false);
			this.gbLiveForSpeed.PerformLayout();
			this.gbLanguage.ResumeLayout(false);
			this.gbLangDetails.ResumeLayout(false);
			this.gbStartBeforeLFS.ResumeLayout(false);
			this.gbProgramConfig.ResumeLayout(false);
			this.gbProgramConfig.PerformLayout();
			this.gbAdvanced.ResumeLayout(false);
			this.gbQueryWait.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.queryWait)).EndInit();
			this.gbWhenBFSStarts.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.GroupBox gbLangDetails;
		private System.Windows.Forms.Button btnRefreshFav;
		private System.Windows.Forms.GroupBox gbAdvanced;
		private System.Windows.Forms.Button btnQuickRefresh;
		private System.Windows.Forms.GroupBox gbProgramConfig;
		private System.Windows.Forms.Label lblDoubleClick;
		private System.Windows.Forms.ComboBox cbDoubleClick;
		private System.Windows.Forms.GroupBox gbLiveForSpeed;
		private System.Windows.Forms.CheckBox cbFavRefresh;
		private System.Windows.Forms.CheckBox cbFriendRefresh;
		private System.Windows.Forms.GroupBox gbWhenBFSStarts;
		private System.Windows.Forms.CheckBox cbColouredHostnames;
		private System.Windows.Forms.ColumnHeader columnHeaderFavVersion;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblLangAuthor;
		private System.Windows.Forms.Label lblLangContact;
		private System.Windows.Forms.Label lblLangComment;
		private System.Windows.Forms.Label lblAuthorReal;
		private System.Windows.Forms.Label lblEmailReal;
		private System.Windows.Forms.Label lblCommentReal;
		private System.Windows.Forms.GroupBox gbLanguage;
		private System.Windows.Forms.GroupBox gbFilters;
		private System.Windows.Forms.Label lblExePathConfig;
		private System.Windows.Forms.Label lblExeDescriptionConfig;
		private System.Windows.Forms.Label lblPasswordMain;
		private System.Windows.Forms.Label lblFindUserMain;
		private System.Windows.Forms.Label lblQueryWaitHelp;
		private System.Windows.Forms.Label lblQueryWaitDescription;
		private System.Windows.Forms.GroupBox gbStartBeforeLFS;
		private System.Windows.Forms.Label lblInsimPortConfig;
		private System.Windows.Forms.Label lblProgramConfigPath;
		private System.Windows.Forms.Label lblPingThreshold;
		private System.Windows.Forms.Label lblAddressFav;
		private System.Windows.Forms.Label lblProgramConfigArg;
		private System.Windows.Forms.Label lblProgramConfigName;
		private System.Windows.Forms.Label lblLanguageConfig;
		private System.Windows.Forms.ComboBox cbConfigLang;
		private System.Windows.Forms.ToolStripMenuItem viewServerInformationFriend;
		private System.Windows.Forms.ToolStripMenuItem joinServerToolStripMenuItem2;
		private System.Windows.Forms.Button btnProgramEnable;
		private System.Windows.Forms.TextBox edtProgramPath;
		private System.Windows.Forms.Button btnProgramBrowse;
		private System.Windows.Forms.TextBox edtProgramName;
		private System.Windows.Forms.TextBox edtProgramOptions;
		private System.Windows.Forms.Button btnProgramSave;
		private System.Windows.Forms.Button btnProgramCancel;
		private System.Windows.Forms.Button btnProgramNew;
		private System.Windows.Forms.Button btnProgramDelete;
		private System.Windows.Forms.ListBox lbPreStart;
		private System.Windows.Forms.ToolStripMenuItem administrateToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem administrateToolStripMenuItem;
		private System.Windows.Forms.ComboBox cbAddServerVersion;
		private System.Windows.Forms.Button btnAddServer;
		private System.Windows.Forms.TextBox edtAddServerAddress;
		private System.Windows.Forms.ToolStripMenuItem copyServerToClipboardToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem copyServerToClipboardToolStripMenuItem;
		private System.Windows.Forms.ComboBox cbVersion;
		private System.Windows.Forms.CheckBox cbFull;
		private System.Windows.Forms.CheckBox cbPrivate;
		private System.Windows.Forms.CheckBox cbPublic;
		private System.Windows.Forms.ComboBox cbPing;
		private System.Windows.Forms.ToolStripMenuItem joinServerMenuFriends;
		private System.Windows.Forms.ToolStripMenuItem removeFriendToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuFriends;
		private System.Windows.Forms.Button btnAddFriend;
		private System.Windows.Forms.TextBox edtFriendName;
		private System.Windows.Forms.CheckBox cbHideOffline;
		protected internal BrowseForSpeed.Frontend.FriendListView lvFriends;
		private System.Windows.Forms.Button btnRefreshFriend;
		private System.Windows.Forms.Button btnJoinFriend;
		private System.Windows.Forms.ColumnHeader columnFriendPlayers;
		private System.Windows.Forms.ColumnHeader columnFriendPrivate;
		private System.Windows.Forms.TabPage tabMain;
		private System.Windows.Forms.TabPage tabConfig;
		private System.Windows.Forms.TabPage tabFavourites;
		private System.Windows.Forms.ColumnHeader columnFriendServer;
		private System.Windows.Forms.ColumnHeader columnFriendName;
		private System.Windows.Forms.TabPage tabFriends;
		private System.Windows.Forms.OpenFileDialog openFileDialogPS;
		private System.Windows.Forms.TextBox txtInsimPort;
		private System.Windows.Forms.GroupBox gbQueryWait;
		private System.Windows.Forms.CheckBox cbEmpty;
		private System.Windows.Forms.NumericUpDown queryWait;
		private System.Windows.Forms.Button btnCheckNewVersion;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.Button btnJoinFav;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private FavouriteListView lvFavourites;
		private System.Windows.Forms.ComboBox cbTracks;
		private MainListView lvMain;
		private System.Windows.Forms.Button btnRefreshMain;
		private System.Windows.Forms.Button btnFindUserMain;
		private System.Windows.Forms.TextBox edtPasswordMain;
		private System.Windows.Forms.TextBox edtFindUserMain;
		private System.Windows.Forms.Button btnJoinMain;
		private System.Windows.Forms.ColumnHeader columnPrivate;
		private System.Windows.Forms.CheckBox cbNewVersion;
		private System.Windows.Forms.CheckBox cbQueryWait;
		private System.Windows.Forms.ToolStripMenuItem viewServerInformationFav;
		private System.Windows.Forms.ToolStripMenuItem viewServerInformationMain;
		private System.Windows.Forms.ColumnHeader columnHeaderFavCars;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusNoReply;
		private System.Windows.Forms.ToolStripStatusLabel statusRefused;
		private System.Windows.Forms.ToolStripStatusLabel statusTotal;
		private System.Windows.Forms.ColumnHeader columnHeaderCars;
		private System.Windows.Forms.ColumnHeader columnHeaderFavTrack;
		private System.Windows.Forms.ColumnHeader columnHeaderTrack;
		private System.Windows.Forms.ColumnHeader columnHeaderFavInfo;
		private System.Windows.Forms.ColumnHeader columnHeaderInfo;
		private System.Windows.Forms.ColumnHeader columnHeaderFavServerName;
		private System.Windows.Forms.ColumnHeader columnHeaderFavPing;
		private System.Windows.Forms.ColumnHeader columnHeaderFavSlots;
		private System.Windows.Forms.ToolStripMenuItem removeFromFavouritesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem joinServerToolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip contextMenuFav;
		private System.Windows.Forms.ToolStripMenuItem joinServerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip contextMenuBrowser;
		private System.Windows.Forms.ListBox pathList;
		private System.Windows.Forms.ColumnHeader columnHeaderConnections;
		private System.Windows.Forms.ColumnHeader columnHeaderPing;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.GroupBox gbConfigurationMain;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	}
}
