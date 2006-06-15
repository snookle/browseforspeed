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
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.joinServerToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabMain = new System.Windows.Forms.TabPage();
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
			this.buttonRefreshFav = new System.Windows.Forms.Button();
			this.btnJoinFav = new System.Windows.Forms.Button();
			this.lvFavourites = new BrowseForSpeed.Frontend.FavouriteListView();
			this.columnHeaderFavServerName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavPing = new System.Windows.Forms.ColumnHeader();
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
			this.lvFriends = new System.Windows.Forms.ListView();
			this.columnFriendName = new System.Windows.Forms.ColumnHeader();
			this.columnFriendServer = new System.Windows.Forms.ColumnHeader();
			this.columnFriendPrivate = new System.Windows.Forms.ColumnHeader();
			this.columnFriendPlayers = new System.Windows.Forms.ColumnHeader();
			this.contextMenuFriends = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.joinServerMenuFriends = new System.Windows.Forms.ToolStripMenuItem();
			this.removeFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabConfig = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.lblCommentReal = new System.Windows.Forms.Label();
			this.lblEmailReal = new System.Windows.Forms.Label();
			this.lblAuthorReal = new System.Windows.Forms.Label();
			this.lblLangComment = new System.Windows.Forms.Label();
			this.lblLangContact = new System.Windows.Forms.Label();
			this.lblLangAuthor = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbConfigLang = new System.Windows.Forms.ComboBox();
			this.lblLanguageConfig = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.rbJoin = new System.Windows.Forms.RadioButton();
			this.rbView = new System.Windows.Forms.RadioButton();
			this.gbStartBeforeLFS = new System.Windows.Forms.GroupBox();
			this.btnProgramEnable = new System.Windows.Forms.Button();
			this.btnProgramDelete = new System.Windows.Forms.Button();
			this.btnProgramNew = new System.Windows.Forms.Button();
			this.bgProgramConfig = new System.Windows.Forms.GroupBox();
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtInsimPort = new System.Windows.Forms.TextBox();
			this.lblQueryWaitDescription = new System.Windows.Forms.Label();
			this.lblInsimPortConfig = new System.Windows.Forms.Label();
			this.queryWait = new System.Windows.Forms.NumericUpDown();
			this.cbQueryWait = new System.Windows.Forms.CheckBox();
			this.lblQueryWaitHelp = new System.Windows.Forms.Label();
			this.btnCheckNewVersion = new System.Windows.Forms.Button();
			this.cbNewVersion = new System.Windows.Forms.CheckBox();
			this.pathList = new System.Windows.Forms.ListBox();
			this.lblExeDescriptionConfig = new System.Windows.Forms.Label();
			this.lblExePathConfig = new System.Windows.Forms.Label();
			this.buttonBrowse = new System.Windows.Forms.Button();
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
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.gbStartBeforeLFS.SuspendLayout();
			this.bgProgramConfig.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.queryWait)).BeginInit();
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
			this.menuStrip.Size = new System.Drawing.Size(890, 24);
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
			this.joinServerToolStripMenuItem2.Size = new System.Drawing.Size(151, 22);
			this.joinServerToolStripMenuItem2.Text = "&Join Server...";
			this.joinServerToolStripMenuItem2.Click += new System.EventHandler(this.JoinServerToolStripMenuItem2Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
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
			this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
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
			this.tabControl.Size = new System.Drawing.Size(890, 591);
			this.tabControl.TabIndex = 1;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControlSelectedIndexChanged);
			// 
			// tabMain
			// 
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
			this.tabMain.Size = new System.Drawing.Size(882, 565);
			this.tabMain.TabIndex = 0;
			this.tabMain.Text = "Server Browser";
			this.tabMain.UseVisualStyleBackColor = true;
			// 
			// edtFindUserMain
			// 
			this.edtFindUserMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.edtFindUserMain.Location = new System.Drawing.Point(595, 536);
			this.edtFindUserMain.Name = "edtFindUserMain";
			this.edtFindUserMain.Size = new System.Drawing.Size(158, 21);
			this.edtFindUserMain.TabIndex = 9;
			this.edtFindUserMain.WordWrap = false;
			this.edtFindUserMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EdtFindUserMainKeyDown);
			// 
			// lblFindUserMain
			// 
			this.lblFindUserMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblFindUserMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblFindUserMain.Location = new System.Drawing.Point(503, 541);
			this.lblFindUserMain.Name = "lblFindUserMain";
			this.lblFindUserMain.Size = new System.Drawing.Size(94, 21);
			this.lblFindUserMain.TabIndex = 8;
			this.lblFindUserMain.Text = "Find User:";
			this.lblFindUserMain.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// edtPasswordMain
			// 
			this.edtPasswordMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.edtPasswordMain.Location = new System.Drawing.Point(324, 536);
			this.edtPasswordMain.Name = "edtPasswordMain";
			this.edtPasswordMain.Size = new System.Drawing.Size(173, 21);
			this.edtPasswordMain.TabIndex = 7;
			// 
			// lblPasswordMain
			// 
			this.lblPasswordMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPasswordMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblPasswordMain.Location = new System.Drawing.Point(170, 541);
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
			this.btnFindUserMain.Location = new System.Drawing.Point(759, 536);
			this.btnFindUserMain.Name = "btnFindUserMain";
			this.btnFindUserMain.Size = new System.Drawing.Size(115, 23);
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
			this.gbFilters.Location = new System.Drawing.Point(737, 6);
			this.gbFilters.Name = "gbFilters";
			this.gbFilters.Size = new System.Drawing.Size(137, 522);
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
			this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeaderName,
									this.columnHeaderPing,
									this.columnPrivate,
									this.columnHeaderConnections,
									this.columnHeaderInfo,
									this.columnHeaderTrack,
									this.columnHeaderCars});
			this.lvMain.ContextMenuStrip = this.contextMenuBrowser;
			this.lvMain.FullRowSelect = true;
			this.lvMain.GridLines = true;
			this.lvMain.Location = new System.Drawing.Point(8, 6);
			this.lvMain.MultiSelect = false;
			this.lvMain.Name = "lvMain";
			this.lvMain.Size = new System.Drawing.Size(723, 524);
			this.lvMain.TabIndex = 3;
			this.lvMain.UseCompatibleStateImageBehavior = false;
			this.lvMain.View = System.Windows.Forms.View.Details;
			this.lvMain.DoubleClick += new System.EventHandler(this.listDblClick);
			this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMainSelectedIndexChanged);
			this.lvMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMainColumnClick);
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Server Name";
			this.columnHeaderName.Width = 158;
			// 
			// columnHeaderPing
			// 
			this.columnHeaderPing.Text = "Ping";
			this.columnHeaderPing.Width = 50;
			// 
			// columnPrivate
			// 
			this.columnPrivate.Text = "Private";
			this.columnPrivate.Width = 50;
			// 
			// columnHeaderConnections
			// 
			this.columnHeaderConnections.Text = "Connections";
			this.columnHeaderConnections.Width = 80;
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
			this.contextMenuBrowser.Size = new System.Drawing.Size(214, 114);
			this.contextMenuBrowser.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuBrowserOpening);
			// 
			// joinServerToolStripMenuItem
			// 
			this.joinServerToolStripMenuItem.Name = "joinServerToolStripMenuItem";
			this.joinServerToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.joinServerToolStripMenuItem.Text = "&Join Server";
			this.joinServerToolStripMenuItem.Click += new System.EventHandler(this.btnJoinClick);
			// 
			// viewServerInformationMain
			// 
			this.viewServerInformationMain.Name = "viewServerInformationMain";
			this.viewServerInformationMain.Size = new System.Drawing.Size(213, 22);
			this.viewServerInformationMain.Text = "&View Server Information...";
			this.viewServerInformationMain.Click += new System.EventHandler(this.ViewServerInformationToolStripMenuItemClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
			this.toolStripMenuItem1.Text = "&Add to Favourites";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1Click);
			// 
			// copyServerToClipboardToolStripMenuItem
			// 
			this.copyServerToClipboardToolStripMenuItem.Name = "copyServerToClipboardToolStripMenuItem";
			this.copyServerToClipboardToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.copyServerToClipboardToolStripMenuItem.Text = "&Copy Server to Clipboard";
			this.copyServerToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyServerToClipboardToolStripMenuItemClick);
			// 
			// administrateToolStripMenuItem1
			// 
			this.administrateToolStripMenuItem1.Name = "administrateToolStripMenuItem1";
			this.administrateToolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
			this.administrateToolStripMenuItem1.Text = "Administrate...";
			this.administrateToolStripMenuItem1.Click += new System.EventHandler(this.AdministrateToolStripMenuItemClick);
			// 
			// btnRefreshMain
			// 
			this.btnRefreshMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefreshMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnRefreshMain.Location = new System.Drawing.Point(89, 539);
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
			this.btnJoinMain.Location = new System.Drawing.Point(8, 539);
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
			this.tabFavourites.Controls.Add(this.buttonRefreshFav);
			this.tabFavourites.Controls.Add(this.btnJoinFav);
			this.tabFavourites.Controls.Add(this.lvFavourites);
			this.tabFavourites.Location = new System.Drawing.Point(4, 22);
			this.tabFavourites.Name = "tabFavourites";
			this.tabFavourites.Size = new System.Drawing.Size(882, 565);
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
			this.cbAddServerVersion.Location = new System.Drawing.Point(706, 541);
			this.cbAddServerVersion.Name = "cbAddServerVersion";
			this.cbAddServerVersion.Size = new System.Drawing.Size(59, 21);
			this.cbAddServerVersion.TabIndex = 10;
			// 
			// lblAddressFav
			// 
			this.lblAddressFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblAddressFav.Location = new System.Drawing.Point(427, 544);
			this.lblAddressFav.Name = "lblAddressFav";
			this.lblAddressFav.Size = new System.Drawing.Size(100, 16);
			this.lblAddressFav.TabIndex = 9;
			this.lblAddressFav.Text = "Server IP Address:";
			// 
			// edtAddServerAddress
			// 
			this.edtAddServerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.edtAddServerAddress.Location = new System.Drawing.Point(533, 541);
			this.edtAddServerAddress.Name = "edtAddServerAddress";
			this.edtAddServerAddress.Size = new System.Drawing.Size(167, 21);
			this.edtAddServerAddress.TabIndex = 8;
			this.edtAddServerAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EdtAddServerAddressKeyDown);
			// 
			// btnAddServer
			// 
			this.btnAddServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddServer.Location = new System.Drawing.Point(771, 539);
			this.btnAddServer.Name = "btnAddServer";
			this.btnAddServer.Size = new System.Drawing.Size(75, 23);
			this.btnAddServer.TabIndex = 7;
			this.btnAddServer.Text = "&Add Server";
			this.btnAddServer.UseVisualStyleBackColor = true;
			this.btnAddServer.Click += new System.EventHandler(this.BtnAddServerClick);
			// 
			// buttonRefreshFav
			// 
			this.buttonRefreshFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRefreshFav.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.buttonRefreshFav.Location = new System.Drawing.Point(89, 539);
			this.buttonRefreshFav.Name = "buttonRefreshFav";
			this.buttonRefreshFav.Size = new System.Drawing.Size(75, 23);
			this.buttonRefreshFav.TabIndex = 6;
			this.buttonRefreshFav.Text = "&Refresh";
			this.buttonRefreshFav.UseVisualStyleBackColor = true;
			this.buttonRefreshFav.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// btnJoinFav
			// 
			this.btnJoinFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnJoinFav.Enabled = false;
			this.btnJoinFav.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnJoinFav.Location = new System.Drawing.Point(8, 539);
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
			this.lvFavourites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeaderFavServerName,
									this.columnHeaderFavPing,
									this.columnHeaderFavSlots,
									this.columnHeaderFavInfo,
									this.columnHeaderFavTrack,
									this.columnHeaderFavCars});
			this.lvFavourites.ContextMenuStrip = this.contextMenuFav;
			this.lvFavourites.FullRowSelect = true;
			this.lvFavourites.GridLines = true;
			this.lvFavourites.Location = new System.Drawing.Point(8, 6);
			this.lvFavourites.MultiSelect = false;
			this.lvFavourites.Name = "lvFavourites";
			this.lvFavourites.Size = new System.Drawing.Size(838, 525);
			this.lvFavourites.TabIndex = 4;
			this.lvFavourites.UseCompatibleStateImageBehavior = false;
			this.lvFavourites.View = System.Windows.Forms.View.Details;
			this.lvFavourites.DoubleClick += new System.EventHandler(this.listDblClick);
			this.lvFavourites.SelectedIndexChanged += new System.EventHandler(this.lvFavouritesSelectedIndexChanged);
			this.lvFavourites.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMainColumnClick);
			// 
			// columnHeaderFavServerName
			// 
			this.columnHeaderFavServerName.Text = "Server Name";
			this.columnHeaderFavServerName.Width = 200;
			// 
			// columnHeaderFavPing
			// 
			this.columnHeaderFavPing.Text = "Ping";
			this.columnHeaderFavPing.Width = 50;
			// 
			// columnHeaderFavSlots
			// 
			this.columnHeaderFavSlots.Text = "Connections";
			this.columnHeaderFavSlots.Width = 80;
			// 
			// columnHeaderFavInfo
			// 
			this.columnHeaderFavInfo.Text = "Info";
			// 
			// columnHeaderFavTrack
			// 
			this.columnHeaderFavTrack.Text = "Track";
			this.columnHeaderFavTrack.Width = 140;
			// 
			// columnHeaderFavCars
			// 
			this.columnHeaderFavCars.Text = "Cars";
			this.columnHeaderFavCars.Width = 200;
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
			this.contextMenuFav.Size = new System.Drawing.Size(214, 114);
			this.contextMenuFav.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuFavOpening);
			// 
			// joinServerToolStripMenuItem1
			// 
			this.joinServerToolStripMenuItem1.Name = "joinServerToolStripMenuItem1";
			this.joinServerToolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
			this.joinServerToolStripMenuItem1.Text = "&Join Server";
			this.joinServerToolStripMenuItem1.Click += new System.EventHandler(this.btnJoinClick);
			// 
			// viewServerInformationFav
			// 
			this.viewServerInformationFav.Name = "viewServerInformationFav";
			this.viewServerInformationFav.Size = new System.Drawing.Size(213, 22);
			this.viewServerInformationFav.Text = "&View Server Information...";
			this.viewServerInformationFav.Click += new System.EventHandler(this.ViewServerInformationToolStripMenuItemClick);
			// 
			// removeFromFavouritesToolStripMenuItem
			// 
			this.removeFromFavouritesToolStripMenuItem.Name = "removeFromFavouritesToolStripMenuItem";
			this.removeFromFavouritesToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.removeFromFavouritesToolStripMenuItem.Text = "&Remove From Favourites";
			this.removeFromFavouritesToolStripMenuItem.Click += new System.EventHandler(this.RemoveFromFavouritesToolStripMenuItemClick);
			// 
			// copyServerToClipboardToolStripMenuItem1
			// 
			this.copyServerToClipboardToolStripMenuItem1.Name = "copyServerToClipboardToolStripMenuItem1";
			this.copyServerToClipboardToolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
			this.copyServerToClipboardToolStripMenuItem1.Text = "&Copy Server to Clipboard";
			this.copyServerToClipboardToolStripMenuItem1.Click += new System.EventHandler(this.CopyServerToClipboardToolStripMenuItemClick);
			// 
			// administrateToolStripMenuItem
			// 
			this.administrateToolStripMenuItem.Name = "administrateToolStripMenuItem";
			this.administrateToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
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
			this.tabFriends.Size = new System.Drawing.Size(882, 565);
			this.tabFriends.TabIndex = 3;
			this.tabFriends.Text = "Friends";
			this.tabFriends.UseVisualStyleBackColor = true;
			// 
			// edtFriendName
			// 
			this.edtFriendName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.edtFriendName.Location = new System.Drawing.Point(583, 537);
			this.edtFriendName.Name = "edtFriendName";
			this.edtFriendName.Size = new System.Drawing.Size(182, 21);
			this.edtFriendName.TabIndex = 6;
			this.edtFriendName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EdtFriendNameKeyDown);
			// 
			// btnAddFriend
			// 
			this.btnAddFriend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFriend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnAddFriend.Location = new System.Drawing.Point(771, 536);
			this.btnAddFriend.Name = "btnAddFriend";
			this.btnAddFriend.Size = new System.Drawing.Size(75, 23);
			this.btnAddFriend.TabIndex = 5;
			this.btnAddFriend.Text = "&Add Friend";
			this.btnAddFriend.UseVisualStyleBackColor = true;
			this.btnAddFriend.Click += new System.EventHandler(this.BtnAddFriendClick);
			// 
			// cbHideOffline
			// 
			this.cbHideOffline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbHideOffline.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cbHideOffline.Location = new System.Drawing.Point(166, 538);
			this.cbHideOffline.Name = "cbHideOffline";
			this.cbHideOffline.Size = new System.Drawing.Size(129, 24);
			this.cbHideOffline.TabIndex = 4;
			this.cbHideOffline.Text = "Hide Offline Friends";
			this.cbHideOffline.UseVisualStyleBackColor = true;
			this.cbHideOffline.CheckedChanged += new System.EventHandler(this.CheckBox2CheckedChanged);
			// 
			// btnRefreshFriend
			// 
			this.btnRefreshFriend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefreshFriend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnRefreshFriend.Location = new System.Drawing.Point(85, 538);
			this.btnRefreshFriend.Name = "btnRefreshFriend";
			this.btnRefreshFriend.Size = new System.Drawing.Size(75, 23);
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
			this.btnJoinFriend.Location = new System.Drawing.Point(4, 538);
			this.btnJoinFriend.Name = "btnJoinFriend";
			this.btnJoinFriend.Size = new System.Drawing.Size(75, 23);
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
			this.lvFriends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnFriendName,
									this.columnFriendServer,
									this.columnFriendPrivate,
									this.columnFriendPlayers});
			this.lvFriends.ContextMenuStrip = this.contextMenuFriends;
			this.lvFriends.FullRowSelect = true;
			this.lvFriends.GridLines = true;
			this.lvFriends.Location = new System.Drawing.Point(8, 6);
			this.lvFriends.MultiSelect = false;
			this.lvFriends.Name = "lvFriends";
			this.lvFriends.Size = new System.Drawing.Size(838, 524);
			this.lvFriends.TabIndex = 0;
			this.lvFriends.UseCompatibleStateImageBehavior = false;
			this.lvFriends.View = System.Windows.Forms.View.Details;
			this.lvFriends.DoubleClick += new System.EventHandler(this.LvFriendsDoubleClick);
			this.lvFriends.SelectedIndexChanged += new System.EventHandler(this.LvFriendsSelectedIndexChanged);
			this.lvFriends.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMainColumnClick);
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
			this.columnFriendPrivate.Width = 51;
			// 
			// columnFriendPlayers
			// 
			this.columnFriendPlayers.Text = "Players";
			this.columnFriendPlayers.Width = 391;
			// 
			// contextMenuFriends
			// 
			this.contextMenuFriends.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.joinServerMenuFriends,
									this.removeFriendToolStripMenuItem});
			this.contextMenuFriends.Name = "contextMenuFriends";
			this.contextMenuFriends.Size = new System.Drawing.Size(158, 48);
			this.contextMenuFriends.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuFriendsOpening);
			// 
			// joinServerMenuFriends
			// 
			this.joinServerMenuFriends.Name = "joinServerMenuFriends";
			this.joinServerMenuFriends.Size = new System.Drawing.Size(157, 22);
			this.joinServerMenuFriends.Text = "Join Server";
			this.joinServerMenuFriends.Click += new System.EventHandler(this.JoinFriendClick);
			// 
			// removeFriendToolStripMenuItem
			// 
			this.removeFriendToolStripMenuItem.Name = "removeFriendToolStripMenuItem";
			this.removeFriendToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.removeFriendToolStripMenuItem.Text = "Remove Friend";
			this.removeFriendToolStripMenuItem.Click += new System.EventHandler(this.RemoveFriendToolStripMenuItemClick);
			// 
			// tabConfig
			// 
			this.tabConfig.Controls.Add(this.groupBox1);
			this.tabConfig.Location = new System.Drawing.Point(4, 22);
			this.tabConfig.Name = "tabConfig";
			this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
			this.tabConfig.Size = new System.Drawing.Size(882, 565);
			this.tabConfig.TabIndex = 1;
			this.tabConfig.Text = "Configuration";
			this.tabConfig.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.groupBox4);
			this.groupBox1.Controls.Add(this.gbStartBeforeLFS);
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.btnCheckNewVersion);
			this.groupBox1.Controls.Add(this.cbNewVersion);
			this.groupBox1.Controls.Add(this.pathList);
			this.groupBox1.Controls.Add(this.lblExeDescriptionConfig);
			this.groupBox1.Controls.Add(this.lblExePathConfig);
			this.groupBox1.Controls.Add(this.buttonBrowse);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(876, 559);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Configuration";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.groupBox5);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.cbConfigLang);
			this.groupBox2.Controls.Add(this.lblLanguageConfig);
			this.groupBox2.Location = new System.Drawing.Point(466, 100);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(394, 176);
			this.groupBox2.TabIndex = 17;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Language";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.lblCommentReal);
			this.groupBox5.Controls.Add(this.lblEmailReal);
			this.groupBox5.Controls.Add(this.lblAuthorReal);
			this.groupBox5.Controls.Add(this.lblLangComment);
			this.groupBox5.Controls.Add(this.lblLangContact);
			this.groupBox5.Controls.Add(this.lblLangAuthor);
			this.groupBox5.Location = new System.Drawing.Point(24, 53);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(347, 104);
			this.groupBox5.TabIndex = 24;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Language Details";
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
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.rbJoin);
			this.groupBox4.Controls.Add(this.rbView);
			this.groupBox4.Location = new System.Drawing.Point(466, 17);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(394, 77);
			this.groupBox4.TabIndex = 15;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Server double click behaviour";
			// 
			// rbJoin
			// 
			this.rbJoin.Checked = true;
			this.rbJoin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.rbJoin.Location = new System.Drawing.Point(21, 48);
			this.rbJoin.Name = "rbJoin";
			this.rbJoin.Size = new System.Drawing.Size(104, 22);
			this.rbJoin.TabIndex = 1;
			this.rbJoin.TabStop = true;
			this.rbJoin.Text = "Join server";
			this.rbJoin.UseVisualStyleBackColor = true;
			// 
			// rbView
			// 
			this.rbView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.rbView.Location = new System.Drawing.Point(21, 20);
			this.rbView.Name = "rbView";
			this.rbView.Size = new System.Drawing.Size(142, 22);
			this.rbView.TabIndex = 0;
			this.rbView.TabStop = true;
			this.rbView.Text = "View server information";
			this.rbView.UseVisualStyleBackColor = true;
			// 
			// gbStartBeforeLFS
			// 
			this.gbStartBeforeLFS.Controls.Add(this.btnProgramEnable);
			this.gbStartBeforeLFS.Controls.Add(this.btnProgramDelete);
			this.gbStartBeforeLFS.Controls.Add(this.btnProgramNew);
			this.gbStartBeforeLFS.Controls.Add(this.bgProgramConfig);
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
			this.btnProgramEnable.Location = new System.Drawing.Point(315, 116);
			this.btnProgramEnable.Name = "btnProgramEnable";
			this.btnProgramEnable.Size = new System.Drawing.Size(107, 23);
			this.btnProgramEnable.TabIndex = 8;
			this.btnProgramEnable.Text = "&Enable";
			this.btnProgramEnable.UseVisualStyleBackColor = true;
			this.btnProgramEnable.Click += new System.EventHandler(this.BtnProgramEnableClick);
			// 
			// btnProgramDelete
			// 
			this.btnProgramDelete.Enabled = false;
			this.btnProgramDelete.Location = new System.Drawing.Point(315, 49);
			this.btnProgramDelete.Name = "btnProgramDelete";
			this.btnProgramDelete.Size = new System.Drawing.Size(107, 23);
			this.btnProgramDelete.TabIndex = 7;
			this.btnProgramDelete.Text = "&Remove Program";
			this.btnProgramDelete.UseVisualStyleBackColor = true;
			this.btnProgramDelete.Click += new System.EventHandler(this.BtnProgramDeleteClick);
			// 
			// btnProgramNew
			// 
			this.btnProgramNew.Location = new System.Drawing.Point(315, 20);
			this.btnProgramNew.Name = "btnProgramNew";
			this.btnProgramNew.Size = new System.Drawing.Size(107, 23);
			this.btnProgramNew.TabIndex = 6;
			this.btnProgramNew.Text = "&New Program";
			this.btnProgramNew.UseVisualStyleBackColor = true;
			this.btnProgramNew.Click += new System.EventHandler(this.BtnProgramNewClick);
			// 
			// bgProgramConfig
			// 
			this.bgProgramConfig.Controls.Add(this.btnProgramCancel);
			this.bgProgramConfig.Controls.Add(this.btnProgramSave);
			this.bgProgramConfig.Controls.Add(this.edtProgramOptions);
			this.bgProgramConfig.Controls.Add(this.lblProgramConfigArg);
			this.bgProgramConfig.Controls.Add(this.edtProgramName);
			this.bgProgramConfig.Controls.Add(this.lblProgramConfigName);
			this.bgProgramConfig.Controls.Add(this.btnProgramBrowse);
			this.bgProgramConfig.Controls.Add(this.lblProgramConfigPath);
			this.bgProgramConfig.Controls.Add(this.edtProgramPath);
			this.bgProgramConfig.Location = new System.Drawing.Point(12, 173);
			this.bgProgramConfig.Name = "bgProgramConfig";
			this.bgProgramConfig.Size = new System.Drawing.Size(410, 135);
			this.bgProgramConfig.TabIndex = 5;
			this.bgProgramConfig.TabStop = false;
			this.bgProgramConfig.Text = "Program Configuration";
			// 
			// btnProgramCancel
			// 
			this.btnProgramCancel.Enabled = false;
			this.btnProgramCancel.Location = new System.Drawing.Point(87, 101);
			this.btnProgramCancel.Name = "btnProgramCancel";
			this.btnProgramCancel.Size = new System.Drawing.Size(75, 23);
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
			this.btnProgramSave.Size = new System.Drawing.Size(75, 23);
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
			this.edtProgramOptions.Size = new System.Drawing.Size(231, 21);
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
			this.btnProgramBrowse.Location = new System.Drawing.Point(326, 45);
			this.btnProgramBrowse.Name = "btnProgramBrowse";
			this.btnProgramBrowse.Size = new System.Drawing.Size(75, 23);
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
			this.edtProgramPath.Size = new System.Drawing.Size(231, 21);
			this.edtProgramPath.TabIndex = 7;
			this.edtProgramPath.TextChanged += new System.EventHandler(this.EdtProgramNameTextChanged);
			// 
			// lbPreStart
			// 
			this.lbPreStart.FormattingEnabled = true;
			this.lbPreStart.Location = new System.Drawing.Point(12, 20);
			this.lbPreStart.Name = "lbPreStart";
			this.lbPreStart.Size = new System.Drawing.Size(294, 147);
			this.lbPreStart.TabIndex = 4;
			this.lbPreStart.DoubleClick += new System.EventHandler(this.LbPreStartDoubleClick);
			this.lbPreStart.SelectedIndexChanged += new System.EventHandler(this.LbPreStartSelectedIndexChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtInsimPort);
			this.groupBox3.Controls.Add(this.lblQueryWaitDescription);
			this.groupBox3.Controls.Add(this.lblInsimPortConfig);
			this.groupBox3.Controls.Add(this.queryWait);
			this.groupBox3.Controls.Add(this.cbQueryWait);
			this.groupBox3.Controls.Add(this.lblQueryWaitHelp);
			this.groupBox3.Location = new System.Drawing.Point(466, 282);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(394, 267);
			this.groupBox3.TabIndex = 14;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Advanced";
			// 
			// txtInsimPort
			// 
			this.txtInsimPort.Location = new System.Drawing.Point(107, 174);
			this.txtInsimPort.Name = "txtInsimPort";
			this.txtInsimPort.Size = new System.Drawing.Size(67, 21);
			this.txtInsimPort.TabIndex = 18;
			this.txtInsimPort.Text = "29999";
			// 
			// lblQueryWaitDescription
			// 
			this.lblQueryWaitDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblQueryWaitDescription.Location = new System.Drawing.Point(85, 50);
			this.lblQueryWaitDescription.Name = "lblQueryWaitDescription";
			this.lblQueryWaitDescription.Size = new System.Drawing.Size(286, 57);
			this.lblQueryWaitDescription.TabIndex = 15;
			this.lblQueryWaitDescription.Text = "Query Wait value. This is the time to wait before adding another query to the con" +
			"current queries. Increase this value if, when querying, all servers start giving" +
			" \"Connection Refused\"";
			// 
			// lblInsimPortConfig
			// 
			this.lblInsimPortConfig.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblInsimPortConfig.Location = new System.Drawing.Point(21, 177);
			this.lblInsimPortConfig.Name = "lblInsimPortConfig";
			this.lblInsimPortConfig.Size = new System.Drawing.Size(90, 23);
			this.lblInsimPortConfig.TabIndex = 17;
			this.lblInsimPortConfig.Text = "LFS Insim Port:";
			// 
			// queryWait
			// 
			this.queryWait.Increment = new decimal(new int[] {
									10,
									0,
									0,
									0});
			this.queryWait.Location = new System.Drawing.Point(24, 62);
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
			this.queryWait.TabIndex = 14;
			this.queryWait.Value = new decimal(new int[] {
									150,
									0,
									0,
									0});
			// 
			// cbQueryWait
			// 
			this.cbQueryWait.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cbQueryWait.Location = new System.Drawing.Point(24, 20);
			this.cbQueryWait.Name = "cbQueryWait";
			this.cbQueryWait.Size = new System.Drawing.Size(139, 24);
			this.cbQueryWait.TabIndex = 13;
			this.cbQueryWait.Text = "Disable Query Wait";
			this.cbQueryWait.UseVisualStyleBackColor = true;
			this.cbQueryWait.CheckedChanged += new System.EventHandler(this.CbQueryWaitCheckedChanged);
			// 
			// lblQueryWaitHelp
			// 
			this.lblQueryWaitHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblQueryWaitHelp.Location = new System.Drawing.Point(21, 116);
			this.lblQueryWaitHelp.Name = "lblQueryWaitHelp";
			this.lblQueryWaitHelp.Size = new System.Drawing.Size(364, 40);
			this.lblQueryWaitHelp.TabIndex = 12;
			this.lblQueryWaitHelp.Text = "Only disable Query Wait if you are certain your Operating System does not have a " +
			"connection limit.  Please see readme.txt for more information. ";
			// 
			// btnCheckNewVersion
			// 
			this.btnCheckNewVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCheckNewVersion.Location = new System.Drawing.Point(339, 163);
			this.btnCheckNewVersion.Name = "btnCheckNewVersion";
			this.btnCheckNewVersion.Size = new System.Drawing.Size(91, 23);
			this.btnCheckNewVersion.TabIndex = 13;
			this.btnCheckNewVersion.Text = "&Check Now";
			this.btnCheckNewVersion.UseVisualStyleBackColor = true;
			this.btnCheckNewVersion.Click += new System.EventHandler(this.btnCheckNewVersionClick);
			// 
			// cbNewVersion
			// 
			this.cbNewVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.cbNewVersion.Location = new System.Drawing.Point(11, 162);
			this.cbNewVersion.Name = "cbNewVersion";
			this.cbNewVersion.Size = new System.Drawing.Size(322, 24);
			this.cbNewVersion.TabIndex = 12;
			this.cbNewVersion.Text = "Check for a new version of Browse For Speed on start up";
			this.cbNewVersion.UseVisualStyleBackColor = true;
			// 
			// pathList
			// 
			this.pathList.FormattingEnabled = true;
			this.pathList.Location = new System.Drawing.Point(13, 41);
			this.pathList.Name = "pathList";
			this.pathList.Size = new System.Drawing.Size(322, 56);
			this.pathList.TabIndex = 8;
			// 
			// lblExeDescriptionConfig
			// 
			this.lblExeDescriptionConfig.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblExeDescriptionConfig.Location = new System.Drawing.Point(11, 100);
			this.lblExeDescriptionConfig.Name = "lblExeDescriptionConfig";
			this.lblExeDescriptionConfig.Size = new System.Drawing.Size(403, 41);
			this.lblExeDescriptionConfig.TabIndex = 7;
			this.lblExeDescriptionConfig.Text = "If there is more than one entry in the box above, please select which one is the " +
			"correct path for your LFS.exe. If there are no entries, please click the Browse " +
			"button to manually locate it.";
			// 
			// lblExePathConfig
			// 
			this.lblExePathConfig.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblExePathConfig.Location = new System.Drawing.Point(13, 20);
			this.lblExePathConfig.Name = "lblExePathConfig";
			this.lblExePathConfig.Size = new System.Drawing.Size(114, 18);
			this.lblExePathConfig.TabIndex = 5;
			this.lblExePathConfig.Text = "LFS Executable Path:";
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.buttonBrowse.Location = new System.Drawing.Point(341, 41);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(89, 23);
			this.buttonBrowse.TabIndex = 4;
			this.buttonBrowse.Text = "&Browse...";
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.btnBrowseClick);
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
			this.statusStrip.Location = new System.Drawing.Point(0, 615);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(890, 22);
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
			this.ClientSize = new System.Drawing.Size(890, 637);
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
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.gbStartBeforeLFS.ResumeLayout(false);
			this.bgProgramConfig.ResumeLayout(false);
			this.bgProgramConfig.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.queryWait)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblLangAuthor;
		private System.Windows.Forms.Label lblLangContact;
		private System.Windows.Forms.Label lblLangComment;
		private System.Windows.Forms.Label lblAuthorReal;
		private System.Windows.Forms.Label lblEmailReal;
		private System.Windows.Forms.Label lblCommentReal;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox2;
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
		private System.Windows.Forms.GroupBox bgProgramConfig;
		private System.Windows.Forms.Label lblProgramConfigArg;
		private System.Windows.Forms.Label lblProgramConfigName;
		private System.Windows.Forms.Label lblLanguageConfig;
		private System.Windows.Forms.ComboBox cbConfigLang;
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
		private System.Windows.Forms.ListView lvFriends;
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
		private System.Windows.Forms.RadioButton rbView;
		private System.Windows.Forms.RadioButton rbJoin;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox cbEmpty;
		private System.Windows.Forms.NumericUpDown queryWait;
		private System.Windows.Forms.GroupBox groupBox3;
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
		private System.Windows.Forms.Button buttonRefreshFav;
		private System.Windows.Forms.ListBox pathList;
		private System.Windows.Forms.ColumnHeader columnHeaderConnections;
		private System.Windows.Forms.ColumnHeader columnHeaderPing;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	}
}
