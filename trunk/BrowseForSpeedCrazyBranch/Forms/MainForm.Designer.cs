/*
 * Created by SharpDevelop.
 * User: Ben
 * Date: 20/05/2006
 * Time: 7:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace LFS.BrowseForSpeed.Windows.Forms
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BrowserTabControl = new System.Windows.Forms.TabControl();
			this.MainServerTab = new System.Windows.Forms.TabPage();
			this.edtFindUserMain = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.edtPasswordMain = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnFindUserMain = new System.Windows.Forms.Button();
			this.ServerFiltersGroup = new System.Windows.Forms.GroupBox();
			this.CarGroups = new System.Windows.Forms.GroupBox();
			this.CarsGroup = new System.Windows.Forms.GroupBox();
			this.PingGroup = new System.Windows.Forms.GroupBox();
			this.MaxPingNumeric = new System.Windows.Forms.NumericUpDown();
			this.ServerGroup = new System.Windows.Forms.GroupBox();
			this.FullCheck = new System.Windows.Forms.CheckBox();
			this.EmptyCheck = new System.Windows.Forms.CheckBox();
			this.PublicCheck = new System.Windows.Forms.CheckBox();
			this.PrivateCheck = new System.Windows.Forms.CheckBox();
			this.TracksGroup = new System.Windows.Forms.GroupBox();
			this.TracksList = new System.Windows.Forms.ComboBox();
			this.lvMain = new System.Windows.Forms.ListView();
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
			this.btnRefreshMain = new System.Windows.Forms.Button();
			this.btnJoinMain = new System.Windows.Forms.Button();
			this.FavoritesServerTab = new System.Windows.Forms.TabPage();
			this.buttonRefreshFav = new System.Windows.Forms.Button();
			this.btnJoinFav = new System.Windows.Forms.Button();
			this.lvFavorites = new System.Windows.Forms.ListView();
			this.columnHeaderFavServerName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavPing = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavSlots = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavInfo = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavTrack = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderFavCars = new System.Windows.Forms.ColumnHeader();
			this.contextMenuFav = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.joinServerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.viewServerInformationFav = new System.Windows.Forms.ToolStripMenuItem();
			this.removeFromFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FriendsTab = new System.Windows.Forms.TabPage();
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
			this.ConfigurationTab = new System.Windows.Forms.TabPage();
			this.ConfigurationGroupBox = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.rbJoin = new System.Windows.Forms.RadioButton();
			this.rbView = new System.Windows.Forms.RadioButton();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.txtInsimPort = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.btnBrowsePS = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.txtPSPath = new System.Windows.Forms.TextBox();
			this.cbUsePS = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.queryWait = new System.Windows.Forms.NumericUpDown();
			this.cbQueryWait = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnCheckNewVersion = new System.Windows.Forms.Button();
			this.cbNewVersion = new System.Windows.Forms.CheckBox();
			this.pathList = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusCurrent = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusTotal = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusRefusedConnectionLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusRefused = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusNoReplyLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusNoReply = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusFill = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusVersion = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialogPS = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip.SuspendLayout();
			this.BrowserTabControl.SuspendLayout();
			this.MainServerTab.SuspendLayout();
			this.ServerFiltersGroup.SuspendLayout();
			this.PingGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MaxPingNumeric)).BeginInit();
			this.ServerGroup.SuspendLayout();
			this.TracksGroup.SuspendLayout();
			this.contextMenuBrowser.SuspendLayout();
			this.FavoritesServerTab.SuspendLayout();
			this.contextMenuFav.SuspendLayout();
			this.FriendsTab.SuspendLayout();
			this.contextMenuFriends.SuspendLayout();
			this.ConfigurationTab.SuspendLayout();
			this.ConfigurationGroupBox.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.queryWait)).BeginInit();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.FileToolStripMenuItem,
									this.HelpToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(842, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// FileToolStripMenuItem
			// 
			this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.closeToolStripMenuItem});
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			this.FileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.FileToolStripMenuItem.Text = "&File";
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItemClick);
			// 
			// HelpToolStripMenuItem
			// 
			this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.AboutToolStripMenuItem});
			this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
			this.HelpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.HelpToolStripMenuItem.Text = "&Help";
			// 
			// AboutToolStripMenuItem
			// 
			this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
			this.AboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.AboutToolStripMenuItem.Text = "&About...";
			this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem1_Click);
			// 
			// BrowserTabControl
			// 
			this.BrowserTabControl.Controls.Add(this.MainServerTab);
			this.BrowserTabControl.Controls.Add(this.FavoritesServerTab);
			this.BrowserTabControl.Controls.Add(this.FriendsTab);
			this.BrowserTabControl.Controls.Add(this.ConfigurationTab);
			this.BrowserTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BrowserTabControl.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.BrowserTabControl.Location = new System.Drawing.Point(0, 24);
			this.BrowserTabControl.Name = "BrowserTabControl";
			this.BrowserTabControl.SelectedIndex = 0;
			this.BrowserTabControl.Size = new System.Drawing.Size(842, 620);
			this.BrowserTabControl.TabIndex = 1;
			this.BrowserTabControl.SelectedIndexChanged += new System.EventHandler(this.TabControlSelectedIndexChanged);
			// 
			// MainServerTab
			// 
			this.MainServerTab.Controls.Add(this.edtFindUserMain);
			this.MainServerTab.Controls.Add(this.label1);
			this.MainServerTab.Controls.Add(this.edtPasswordMain);
			this.MainServerTab.Controls.Add(this.label6);
			this.MainServerTab.Controls.Add(this.btnFindUserMain);
			this.MainServerTab.Controls.Add(this.ServerFiltersGroup);
			this.MainServerTab.Controls.Add(this.lvMain);
			this.MainServerTab.Controls.Add(this.btnRefreshMain);
			this.MainServerTab.Controls.Add(this.btnJoinMain);
			this.MainServerTab.Location = new System.Drawing.Point(4, 22);
			this.MainServerTab.Name = "MainServerTab";
			this.MainServerTab.Padding = new System.Windows.Forms.Padding(3);
			this.MainServerTab.Size = new System.Drawing.Size(834, 594);
			this.MainServerTab.TabIndex = 0;
			this.MainServerTab.Text = "Server Browser";
			this.MainServerTab.UseVisualStyleBackColor = true;
			// 
			// edtFindUserMain
			// 
			this.edtFindUserMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.edtFindUserMain.Location = new System.Drawing.Point(547, 565);
			this.edtFindUserMain.Name = "edtFindUserMain";
			this.edtFindUserMain.Size = new System.Drawing.Size(158, 21);
			this.edtFindUserMain.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(491, 570);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 21);
			this.label1.TabIndex = 8;
			this.label1.Text = "Find User:";
			// 
			// edtPasswordMain
			// 
			this.edtPasswordMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.edtPasswordMain.Location = new System.Drawing.Point(262, 567);
			this.edtPasswordMain.Name = "edtPasswordMain";
			this.edtPasswordMain.Size = new System.Drawing.Size(173, 21);
			this.edtPasswordMain.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.Location = new System.Drawing.Point(170, 570);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(97, 16);
			this.label6.TabIndex = 6;
			this.label6.Text = "Server Password:";
			// 
			// btnFindUserMain
			// 
			this.btnFindUserMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFindUserMain.Location = new System.Drawing.Point(711, 565);
			this.btnFindUserMain.Name = "btnFindUserMain";
			this.btnFindUserMain.Size = new System.Drawing.Size(115, 23);
			this.btnFindUserMain.TabIndex = 5;
			this.btnFindUserMain.Text = "&Find User Online";
			this.btnFindUserMain.UseVisualStyleBackColor = true;
			this.btnFindUserMain.Click += new System.EventHandler(this.btnFindUserClick);
			// 
			// ServerFiltersGroup
			// 
			this.ServerFiltersGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.ServerFiltersGroup.Controls.Add(this.CarGroups);
			this.ServerFiltersGroup.Controls.Add(this.CarsGroup);
			this.ServerFiltersGroup.Controls.Add(this.PingGroup);
			this.ServerFiltersGroup.Controls.Add(this.ServerGroup);
			this.ServerFiltersGroup.Controls.Add(this.TracksGroup);
			this.ServerFiltersGroup.Location = new System.Drawing.Point(694, 6);
			this.ServerFiltersGroup.Name = "ServerFiltersGroup";
			this.ServerFiltersGroup.Size = new System.Drawing.Size(132, 551);
			this.ServerFiltersGroup.TabIndex = 4;
			this.ServerFiltersGroup.TabStop = false;
			this.ServerFiltersGroup.Text = "Filters";
			// 
			// CarGroups
			// 
			this.CarGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.CarGroups.Location = new System.Drawing.Point(6, 232);
			this.CarGroups.Name = "CarGroups";
			this.CarGroups.Size = new System.Drawing.Size(120, 94);
			this.CarGroups.TabIndex = 18;
			this.CarGroups.TabStop = false;
			this.CarGroups.Text = "Car Groups";
			// 
			// CarsGroup
			// 
			this.CarsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.CarsGroup.Location = new System.Drawing.Point(6, 20);
			this.CarsGroup.Name = "CarsGroup";
			this.CarsGroup.Size = new System.Drawing.Size(120, 206);
			this.CarsGroup.TabIndex = 16;
			this.CarsGroup.TabStop = false;
			this.CarsGroup.Text = "Cars";
			// 
			// PingGroup
			// 
			this.PingGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.PingGroup.Controls.Add(this.MaxPingNumeric);
			this.PingGroup.Location = new System.Drawing.Point(6, 332);
			this.PingGroup.Name = "PingGroup";
			this.PingGroup.Size = new System.Drawing.Size(120, 52);
			this.PingGroup.TabIndex = 15;
			this.PingGroup.TabStop = false;
			this.PingGroup.Text = "Max Ping";
			// 
			// MaxPingNumeric
			// 
			this.MaxPingNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.MaxPingNumeric.Location = new System.Drawing.Point(7, 21);
			this.MaxPingNumeric.Maximum = new decimal(new int[] {
									500,
									0,
									0,
									0});
			this.MaxPingNumeric.Name = "MaxPingNumeric";
			this.MaxPingNumeric.Size = new System.Drawing.Size(107, 21);
			this.MaxPingNumeric.TabIndex = 0;
			this.MaxPingNumeric.Value = new decimal(new int[] {
									500,
									0,
									0,
									0});
			// 
			// ServerGroup
			// 
			this.ServerGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.ServerGroup.Controls.Add(this.FullCheck);
			this.ServerGroup.Controls.Add(this.EmptyCheck);
			this.ServerGroup.Controls.Add(this.PublicCheck);
			this.ServerGroup.Controls.Add(this.PrivateCheck);
			this.ServerGroup.Location = new System.Drawing.Point(6, 444);
			this.ServerGroup.Name = "ServerGroup";
			this.ServerGroup.Size = new System.Drawing.Size(120, 100);
			this.ServerGroup.TabIndex = 14;
			this.ServerGroup.TabStop = false;
			this.ServerGroup.Text = "Servers";
			// 
			// FullCheck
			// 
			this.FullCheck.Location = new System.Drawing.Point(6, 79);
			this.FullCheck.Name = "FullCheck";
			this.FullCheck.Size = new System.Drawing.Size(97, 19);
			this.FullCheck.TabIndex = 20;
			this.FullCheck.Text = "Full";
			this.FullCheck.UseVisualStyleBackColor = true;
			// 
			// EmptyCheck
			// 
			this.EmptyCheck.Location = new System.Drawing.Point(6, 58);
			this.EmptyCheck.Name = "EmptyCheck";
			this.EmptyCheck.Size = new System.Drawing.Size(97, 21);
			this.EmptyCheck.TabIndex = 19;
			this.EmptyCheck.Text = "Empty";
			this.EmptyCheck.UseVisualStyleBackColor = true;
			// 
			// PublicCheck
			// 
			this.PublicCheck.Location = new System.Drawing.Point(6, 38);
			this.PublicCheck.Name = "PublicCheck";
			this.PublicCheck.Size = new System.Drawing.Size(97, 19);
			this.PublicCheck.TabIndex = 13;
			this.PublicCheck.Text = "Public";
			this.PublicCheck.UseVisualStyleBackColor = true;
			// 
			// PrivateCheck
			// 
			this.PrivateCheck.Location = new System.Drawing.Point(6, 17);
			this.PrivateCheck.Name = "PrivateCheck";
			this.PrivateCheck.Size = new System.Drawing.Size(97, 21);
			this.PrivateCheck.TabIndex = 12;
			this.PrivateCheck.Text = "Private";
			this.PrivateCheck.UseVisualStyleBackColor = true;
			// 
			// TracksGroup
			// 
			this.TracksGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.TracksGroup.Controls.Add(this.TracksList);
			this.TracksGroup.Location = new System.Drawing.Point(6, 390);
			this.TracksGroup.Name = "TracksGroup";
			this.TracksGroup.Size = new System.Drawing.Size(120, 48);
			this.TracksGroup.TabIndex = 13;
			this.TracksGroup.TabStop = false;
			this.TracksGroup.Text = "Tracks";
			// 
			// TracksList
			// 
			this.TracksList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.TracksList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TracksList.FormattingEnabled = true;
			this.TracksList.Items.AddRange(new object[] {
									"All Tracks",
									"Blackwood",
									"South City",
									"Fern Bay",
									"Autocross",
									"Kyoto",
									"Westhill",
									"Aston"});
			this.TracksList.Location = new System.Drawing.Point(6, 20);
			this.TracksList.Name = "TracksList";
			this.TracksList.Size = new System.Drawing.Size(108, 21);
			this.TracksList.TabIndex = 10;
			this.TracksList.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTracksSelectedIndexChanged);
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
			this.lvMain.Size = new System.Drawing.Size(680, 553);
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
									this.toolStripMenuItem1});
			this.contextMenuBrowser.Name = "contextMenuBrowser";
			this.contextMenuBrowser.Size = new System.Drawing.Size(214, 70);
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
			this.toolStripMenuItem1.Text = "&Add to Favorites";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1Click);
			// 
			// btnRefreshMain
			// 
			this.btnRefreshMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefreshMain.Location = new System.Drawing.Point(89, 568);
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
			this.btnJoinMain.Location = new System.Drawing.Point(8, 568);
			this.btnJoinMain.Name = "btnJoinMain";
			this.btnJoinMain.Size = new System.Drawing.Size(75, 23);
			this.btnJoinMain.TabIndex = 1;
			this.btnJoinMain.Text = "&Join";
			this.btnJoinMain.UseVisualStyleBackColor = true;
			this.btnJoinMain.Click += new System.EventHandler(this.btnJoinClick);
			// 
			// FavoritesServerTab
			// 
			this.FavoritesServerTab.Controls.Add(this.buttonRefreshFav);
			this.FavoritesServerTab.Controls.Add(this.btnJoinFav);
			this.FavoritesServerTab.Controls.Add(this.lvFavorites);
			this.FavoritesServerTab.Location = new System.Drawing.Point(4, 22);
			this.FavoritesServerTab.Name = "FavoritesServerTab";
			this.FavoritesServerTab.Size = new System.Drawing.Size(834, 594);
			this.FavoritesServerTab.TabIndex = 2;
			this.FavoritesServerTab.Text = "Favorites";
			this.FavoritesServerTab.UseVisualStyleBackColor = true;
			// 
			// buttonRefreshFav
			// 
			this.buttonRefreshFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRefreshFav.Location = new System.Drawing.Point(89, 468);
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
			this.btnJoinFav.Location = new System.Drawing.Point(8, 468);
			this.btnJoinFav.Name = "btnJoinFav";
			this.btnJoinFav.Size = new System.Drawing.Size(75, 23);
			this.btnJoinFav.TabIndex = 5;
			this.btnJoinFav.Text = "&Join";
			this.btnJoinFav.UseVisualStyleBackColor = true;
			this.btnJoinFav.Click += new System.EventHandler(this.btnJoinClick);
			// 
			// lvFavorites
			// 
			this.lvFavorites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvFavorites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeaderFavServerName,
									this.columnHeaderFavPing,
									this.columnHeaderFavSlots,
									this.columnHeaderFavInfo,
									this.columnHeaderFavTrack,
									this.columnHeaderFavCars});
			this.lvFavorites.ContextMenuStrip = this.contextMenuFav;
			this.lvFavorites.FullRowSelect = true;
			this.lvFavorites.GridLines = true;
			this.lvFavorites.Location = new System.Drawing.Point(8, 6);
			this.lvFavorites.MultiSelect = false;
			this.lvFavorites.Name = "lvFavorites";
			this.lvFavorites.Size = new System.Drawing.Size(768, 454);
			this.lvFavorites.TabIndex = 4;
			this.lvFavorites.UseCompatibleStateImageBehavior = false;
			this.lvFavorites.View = System.Windows.Forms.View.Details;
			this.lvFavorites.DoubleClick += new System.EventHandler(this.listDblClick);
			this.lvFavorites.SelectedIndexChanged += new System.EventHandler(this.lvFavoritesSelectedIndexChanged);
			this.lvFavorites.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMainColumnClick);
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
									this.removeFromFavoritesToolStripMenuItem});
			this.contextMenuFav.Name = "contextMenuFav";
			this.contextMenuFav.Size = new System.Drawing.Size(214, 70);
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
			// removeFromFavoritesToolStripMenuItem
			// 
			this.removeFromFavoritesToolStripMenuItem.Name = "removeFromFavoritesToolStripMenuItem";
			this.removeFromFavoritesToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.removeFromFavoritesToolStripMenuItem.Text = "&Remove From Favorites";
			this.removeFromFavoritesToolStripMenuItem.Click += new System.EventHandler(this.RemoveFromFavoritesToolStripMenuItemClick);
			// 
			// FriendsTab
			// 
			this.FriendsTab.Controls.Add(this.edtFriendName);
			this.FriendsTab.Controls.Add(this.btnAddFriend);
			this.FriendsTab.Controls.Add(this.cbHideOffline);
			this.FriendsTab.Controls.Add(this.btnRefreshFriend);
			this.FriendsTab.Controls.Add(this.btnJoinFriend);
			this.FriendsTab.Controls.Add(this.lvFriends);
			this.FriendsTab.Location = new System.Drawing.Point(4, 22);
			this.FriendsTab.Name = "FriendsTab";
			this.FriendsTab.Size = new System.Drawing.Size(834, 594);
			this.FriendsTab.TabIndex = 3;
			this.FriendsTab.Text = "Friends";
			this.FriendsTab.UseVisualStyleBackColor = true;
			// 
			// edtFriendName
			// 
			this.edtFriendName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.edtFriendName.Location = new System.Drawing.Point(513, 466);
			this.edtFriendName.Name = "edtFriendName";
			this.edtFriendName.Size = new System.Drawing.Size(182, 21);
			this.edtFriendName.TabIndex = 6;
			this.edtFriendName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EdtFriendNameKeyDown);
			// 
			// btnAddFriend
			// 
			this.btnAddFriend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFriend.Location = new System.Drawing.Point(701, 465);
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
			this.cbHideOffline.Location = new System.Drawing.Point(166, 467);
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
			this.btnRefreshFriend.Location = new System.Drawing.Point(85, 467);
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
			this.btnJoinFriend.Location = new System.Drawing.Point(4, 467);
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
			this.lvFriends.Size = new System.Drawing.Size(768, 453);
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
			this.columnFriendServer.Text = "Server";
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
			// ConfigurationTab
			// 
			this.ConfigurationTab.Controls.Add(this.ConfigurationGroupBox);
			this.ConfigurationTab.Location = new System.Drawing.Point(4, 22);
			this.ConfigurationTab.Name = "ConfigurationTab";
			this.ConfigurationTab.Padding = new System.Windows.Forms.Padding(3);
			this.ConfigurationTab.Size = new System.Drawing.Size(834, 594);
			this.ConfigurationTab.TabIndex = 1;
			this.ConfigurationTab.Text = "Configuration";
			this.ConfigurationTab.UseVisualStyleBackColor = true;
			// 
			// ConfigurationGroupBox
			// 
			this.ConfigurationGroupBox.Controls.Add(this.groupBox4);
			this.ConfigurationGroupBox.Controls.Add(this.groupBox5);
			this.ConfigurationGroupBox.Controls.Add(this.groupBox3);
			this.ConfigurationGroupBox.Controls.Add(this.btnCheckNewVersion);
			this.ConfigurationGroupBox.Controls.Add(this.cbNewVersion);
			this.ConfigurationGroupBox.Controls.Add(this.pathList);
			this.ConfigurationGroupBox.Controls.Add(this.label5);
			this.ConfigurationGroupBox.Controls.Add(this.label2);
			this.ConfigurationGroupBox.Controls.Add(this.buttonBrowse);
			this.ConfigurationGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ConfigurationGroupBox.Location = new System.Drawing.Point(3, 3);
			this.ConfigurationGroupBox.Name = "ConfigurationGroupBox";
			this.ConfigurationGroupBox.Size = new System.Drawing.Size(828, 588);
			this.ConfigurationGroupBox.TabIndex = 0;
			this.ConfigurationGroupBox.TabStop = false;
			this.ConfigurationGroupBox.Text = "Configuration";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.rbJoin);
			this.groupBox4.Controls.Add(this.rbView);
			this.groupBox4.Location = new System.Drawing.Point(454, 20);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(292, 77);
			this.groupBox4.TabIndex = 15;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Server double click behaviour";
			// 
			// rbJoin
			// 
			this.rbJoin.Checked = true;
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
			this.rbView.Location = new System.Drawing.Point(21, 20);
			this.rbView.Name = "rbView";
			this.rbView.Size = new System.Drawing.Size(142, 22);
			this.rbView.TabIndex = 0;
			this.rbView.TabStop = true;
			this.rbView.Text = "View server information";
			this.rbView.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.txtInsimPort);
			this.groupBox5.Controls.Add(this.label8);
			this.groupBox5.Controls.Add(this.btnBrowsePS);
			this.groupBox5.Controls.Add(this.label7);
			this.groupBox5.Controls.Add(this.txtPSPath);
			this.groupBox5.Controls.Add(this.cbUsePS);
			this.groupBox5.Location = new System.Drawing.Point(11, 210);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(419, 124);
			this.groupBox5.TabIndex = 16;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Pit Spotter";
			// 
			// txtInsimPort
			// 
			this.txtInsimPort.Enabled = false;
			this.txtInsimPort.Location = new System.Drawing.Point(107, 92);
			this.txtInsimPort.Name = "txtInsimPort";
			this.txtInsimPort.Size = new System.Drawing.Size(67, 21);
			this.txtInsimPort.TabIndex = 5;
			this.txtInsimPort.Text = "29999";
			this.txtInsimPort.Leave += new System.EventHandler(this.TxtInsimPortLeave);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(24, 95);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(90, 23);
			this.label8.TabIndex = 4;
			this.label8.Text = "LFS Insim Port:";
			// 
			// btnBrowsePS
			// 
			this.btnBrowsePS.Enabled = false;
			this.btnBrowsePS.Location = new System.Drawing.Point(322, 63);
			this.btnBrowsePS.Name = "btnBrowsePS";
			this.btnBrowsePS.Size = new System.Drawing.Size(75, 23);
			this.btnBrowsePS.TabIndex = 3;
			this.btnBrowsePS.Text = "B&rowse...";
			this.btnBrowsePS.UseVisualStyleBackColor = true;
			this.btnBrowsePS.Click += new System.EventHandler(this.BtnBrowsePSClick);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(24, 47);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(256, 15);
			this.label7.TabIndex = 2;
			this.label7.Text = "Pit Spotter Executable Path:";
			// 
			// txtPSPath
			// 
			this.txtPSPath.Enabled = false;
			this.txtPSPath.Location = new System.Drawing.Point(24, 65);
			this.txtPSPath.Name = "txtPSPath";
			this.txtPSPath.Size = new System.Drawing.Size(292, 21);
			this.txtPSPath.TabIndex = 1;
			// 
			// cbUsePS
			// 
			this.cbUsePS.Location = new System.Drawing.Point(24, 20);
			this.cbUsePS.Name = "cbUsePS";
			this.cbUsePS.Size = new System.Drawing.Size(184, 24);
			this.cbUsePS.TabIndex = 0;
			this.cbUsePS.Text = "Start Pit Spotter before LFS";
			this.cbUsePS.UseVisualStyleBackColor = true;
			this.cbUsePS.CheckStateChanged += new System.EventHandler(this.CbUsePSCheckStateChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.queryWait);
			this.groupBox3.Controls.Add(this.cbQueryWait);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Location = new System.Drawing.Point(11, 365);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(419, 174);
			this.groupBox3.TabIndex = 14;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Advanced";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(85, 50);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(286, 57);
			this.label3.TabIndex = 15;
			this.label3.Text = "Query Wait value. This is the time to wait before adding another query to the con" +
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
			this.cbQueryWait.Location = new System.Drawing.Point(24, 20);
			this.cbQueryWait.Name = "cbQueryWait";
			this.cbQueryWait.Size = new System.Drawing.Size(139, 24);
			this.cbQueryWait.TabIndex = 13;
			this.cbQueryWait.Text = "Disable Query Wait";
			this.cbQueryWait.UseVisualStyleBackColor = true;
			this.cbQueryWait.CheckedChanged += new System.EventHandler(this.CbQueryWaitCheckedChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 116);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(357, 48);
			this.label4.TabIndex = 12;
			this.label4.Text = "Only disable Query Wait if you are certain your Operating System does not have a " +
			"connection limit.  Please see the FAQ at http://browseforspeed.whatsbeef.net for" +
			" more information.";
			// 
			// btnCheckNewVersion
			// 
			this.btnCheckNewVersion.Location = new System.Drawing.Point(355, 153);
			this.btnCheckNewVersion.Name = "btnCheckNewVersion";
			this.btnCheckNewVersion.Size = new System.Drawing.Size(75, 23);
			this.btnCheckNewVersion.TabIndex = 13;
			this.btnCheckNewVersion.Text = "&Check Now";
			this.btnCheckNewVersion.UseVisualStyleBackColor = true;
			this.btnCheckNewVersion.Click += new System.EventHandler(this.btnCheckNewVersionClick);
			// 
			// cbNewVersion
			// 
			this.cbNewVersion.Location = new System.Drawing.Point(13, 151);
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
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(11, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(403, 41);
			this.label5.TabIndex = 7;
			this.label5.Text = "If there is more than one entry in the box above, please select which one is the " +
			"correct path for your LFS.exe. If there are no entries, please click the Browse " +
			"button to manually locate it.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 18);
			this.label2.TabIndex = 5;
			this.label2.Text = "LFS Executable Path:";
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Location = new System.Drawing.Point(355, 41);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
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
									this.statusCurrent,
									this.statusTotal,
									this.statusRefusedConnectionLabel,
									this.statusRefused,
									this.statusNoReplyLabel,
									this.statusNoReply,
									this.statusFill,
									this.statusVersion});
			this.statusStrip.Location = new System.Drawing.Point(0, 644);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(842, 22);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "asdfasdf";
			// 
			// statusCurrent
			// 
			this.statusCurrent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.statusCurrent.Name = "statusCurrent";
			this.statusCurrent.Size = new System.Drawing.Size(0, 17);
			// 
			// statusTotal
			// 
			this.statusTotal.Name = "statusTotal";
			this.statusTotal.Size = new System.Drawing.Size(0, 17);
			// 
			// statusRefusedConnectionLabel
			// 
			this.statusRefusedConnectionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusRefusedConnectionLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.statusRefusedConnectionLabel.Name = "statusRefusedConnectionLabel";
			this.statusRefusedConnectionLabel.Size = new System.Drawing.Size(111, 17);
			this.statusRefusedConnectionLabel.Text = "Refused Connection: ";
			// 
			// statusRefused
			// 
			this.statusRefused.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.statusRefused.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusRefused.Name = "statusRefused";
			this.statusRefused.Size = new System.Drawing.Size(0, 17);
			// 
			// statusNoReplyLabel
			// 
			this.statusNoReplyLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusNoReplyLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.statusNoReplyLabel.Name = "statusNoReplyLabel";
			this.statusNoReplyLabel.Size = new System.Drawing.Size(57, 17);
			this.statusNoReplyLabel.Text = "No Reply: ";
			// 
			// statusNoReply
			// 
			this.statusNoReply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.statusNoReply.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusNoReply.Name = "statusNoReply";
			this.statusNoReply.Size = new System.Drawing.Size(0, 17);
			this.statusNoReply.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
			// 
			// statusFill
			// 
			this.statusFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.statusFill.Name = "statusFill";
			this.statusFill.Size = new System.Drawing.Size(586, 17);
			this.statusFill.Spring = true;
			// 
			// statusVersion
			// 
			this.statusVersion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.statusVersion.Name = "statusVersion";
			this.statusVersion.Size = new System.Drawing.Size(42, 17);
			this.statusVersion.Text = "Version";
			// 
			// openFileDialogPS
			// 
			this.openFileDialogPS.FileName = "LFSspotter.exe";
			this.openFileDialogPS.Filter = "LFS Spotter Executable|LFSspotter.exe";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(842, 666);
			this.Controls.Add(this.BrowserTabControl);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.statusStrip);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.Text = "Browse For Speed";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainKeyDown);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.BrowserTabControl.ResumeLayout(false);
			this.MainServerTab.ResumeLayout(false);
			this.MainServerTab.PerformLayout();
			this.ServerFiltersGroup.ResumeLayout(false);
			this.PingGroup.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MaxPingNumeric)).EndInit();
			this.ServerGroup.ResumeLayout(false);
			this.TracksGroup.ResumeLayout(false);
			this.contextMenuBrowser.ResumeLayout(false);
			this.FavoritesServerTab.ResumeLayout(false);
			this.contextMenuFav.ResumeLayout(false);
			this.FriendsTab.ResumeLayout(false);
			this.FriendsTab.PerformLayout();
			this.contextMenuFriends.ResumeLayout(false);
			this.ConfigurationTab.ResumeLayout(false);
			this.ConfigurationGroupBox.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.queryWait)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
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
		private System.Windows.Forms.TabPage MainServerTab;
		private System.Windows.Forms.TabPage ConfigurationTab;
		private System.Windows.Forms.TabPage FavoritesServerTab;
		private System.Windows.Forms.ColumnHeader columnFriendServer;
		private System.Windows.Forms.ColumnHeader columnFriendName;
		private System.Windows.Forms.TabPage FriendsTab;
		private System.Windows.Forms.OpenFileDialog openFileDialogPS;
		private System.Windows.Forms.CheckBox cbUsePS;
		private System.Windows.Forms.TextBox txtPSPath;
		private System.Windows.Forms.Button btnBrowsePS;
		private System.Windows.Forms.TextBox txtInsimPort;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton rbView;
		private System.Windows.Forms.RadioButton rbJoin;
        private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.NumericUpDown queryWait;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnCheckNewVersion;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.Button btnJoinFav;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ListView lvFavorites;
		private System.Windows.Forms.ComboBox TracksList;
		private System.Windows.Forms.ListView lvMain;
		private System.Windows.Forms.Button btnRefreshMain;
		private System.Windows.Forms.Button btnFindUserMain;
		private System.Windows.Forms.TextBox edtPasswordMain;
		private System.Windows.Forms.TextBox edtFindUserMain;
		private System.Windows.Forms.Button btnJoinMain;
		private System.Windows.Forms.ColumnHeader columnPrivate;
		private System.Windows.Forms.CheckBox cbNewVersion;
		private System.Windows.Forms.CheckBox cbQueryWait;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ToolStripMenuItem viewServerInformationFav;
		private System.Windows.Forms.ToolStripMenuItem viewServerInformationMain;
		private System.Windows.Forms.ColumnHeader columnHeaderFavCars;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusNoReply;
		private System.Windows.Forms.ToolStripStatusLabel statusRefused;
		private System.Windows.Forms.ToolStripStatusLabel statusCurrent;
		private System.Windows.Forms.ColumnHeader columnHeaderCars;
		private System.Windows.Forms.ColumnHeader columnHeaderFavTrack;
		private System.Windows.Forms.ColumnHeader columnHeaderTrack;
		private System.Windows.Forms.ColumnHeader columnHeaderFavInfo;
		private System.Windows.Forms.ColumnHeader columnHeaderInfo;
		private System.Windows.Forms.ColumnHeader columnHeaderFavServerName;
		private System.Windows.Forms.ColumnHeader columnHeaderFavPing;
		private System.Windows.Forms.ColumnHeader columnHeaderFavSlots;
		private System.Windows.Forms.ToolStripMenuItem removeFromFavoritesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem joinServerToolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip contextMenuFav;
		private System.Windows.Forms.ToolStripMenuItem joinServerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip contextMenuBrowser;
		private System.Windows.Forms.Button buttonRefreshFav;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ListBox pathList;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox ServerFiltersGroup;
		private System.Windows.Forms.ColumnHeader columnHeaderConnections;
		private System.Windows.Forms.ColumnHeader columnHeaderPing;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.GroupBox ConfigurationGroupBox;
		private System.Windows.Forms.TabControl BrowserTabControl;
		private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.GroupBox TracksGroup;
        private System.Windows.Forms.GroupBox ServerGroup;
        private System.Windows.Forms.CheckBox PublicCheck;
        private System.Windows.Forms.CheckBox PrivateCheck;
        private System.Windows.Forms.CheckBox FullCheck;
        private System.Windows.Forms.CheckBox EmptyCheck;
        private System.Windows.Forms.GroupBox PingGroup;
        private System.Windows.Forms.NumericUpDown MaxPingNumeric;
        private System.Windows.Forms.GroupBox CarsGroup;
        private System.Windows.Forms.GroupBox CarGroups;
        private System.Windows.Forms.ToolStripStatusLabel statusVersion;
        private System.Windows.Forms.ToolStripStatusLabel statusFill;
        private System.Windows.Forms.ToolStripStatusLabel statusRefusedConnectionLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusNoReplyLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusTotal;
	}
}
