/*
 * Created by SharpDevelop.
 * User: Ben
 * Date: 20/05/2006
 * Time: 7:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace LFS_ServerBrowser
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
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.edtFindUserMain = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.edtPasswordMain = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnFindUserMain = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cbEmpty = new System.Windows.Forms.CheckBox();
			this.cbTracks = new System.Windows.Forms.ComboBox();
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
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.buttonRefreshFav = new System.Windows.Forms.Button();
			this.btnJoinFav = new System.Windows.Forms.Button();
			this.lvFavourites = new System.Windows.Forms.ListView();
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
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
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
			this.statusTotal = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusRefused = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusNoReply = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialogPS = new System.Windows.Forms.OpenFileDialog();
			this.listFontDialog = new System.Windows.Forms.FontDialog();
			this.menuStrip.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.contextMenuBrowser.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.contextMenuFav.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
									this.fileToolStripMenuItem,
									this.aboutToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(792, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
			this.closeToolStripMenuItem.Text = "&Close";
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
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage3);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.tabControl.Location = new System.Drawing.Point(0, 24);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(792, 527);
			this.tabControl.TabIndex = 1;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControlSelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.edtFindUserMain);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.edtPasswordMain);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.btnFindUserMain);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.lvMain);
			this.tabPage1.Controls.Add(this.btnRefreshMain);
			this.tabPage1.Controls.Add(this.btnJoinMain);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(784, 501);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Server Browser";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// edtFindUserMain
			// 
			this.edtFindUserMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.edtFindUserMain.Location = new System.Drawing.Point(497, 472);
			this.edtFindUserMain.Name = "edtFindUserMain";
			this.edtFindUserMain.Size = new System.Drawing.Size(158, 21);
			this.edtFindUserMain.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(441, 477);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 21);
			this.label1.TabIndex = 8;
			this.label1.Text = "Find User:";
			// 
			// edtPasswordMain
			// 
			this.edtPasswordMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.edtPasswordMain.Location = new System.Drawing.Point(262, 474);
			this.edtPasswordMain.Name = "edtPasswordMain";
			this.edtPasswordMain.PasswordChar = '*';
			this.edtPasswordMain.Size = new System.Drawing.Size(173, 21);
			this.edtPasswordMain.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.Location = new System.Drawing.Point(170, 477);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(97, 16);
			this.label6.TabIndex = 6;
			this.label6.Text = "Server Password:";
			// 
			// btnFindUserMain
			// 
			this.btnFindUserMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFindUserMain.Location = new System.Drawing.Point(661, 472);
			this.btnFindUserMain.Name = "btnFindUserMain";
			this.btnFindUserMain.Size = new System.Drawing.Size(115, 23);
			this.btnFindUserMain.TabIndex = 5;
			this.btnFindUserMain.Text = "&Find User Online";
			this.btnFindUserMain.UseVisualStyleBackColor = true;
			this.btnFindUserMain.Click += new System.EventHandler(this.btnFindUserClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.cbEmpty);
			this.groupBox2.Controls.Add(this.cbTracks);
			this.groupBox2.Location = new System.Drawing.Point(661, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(115, 458);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Filters";
			// 
			// cbEmpty
			// 
			this.cbEmpty.Location = new System.Drawing.Point(5, 396);
			this.cbEmpty.Name = "cbEmpty";
			this.cbEmpty.Size = new System.Drawing.Size(104, 31);
			this.cbEmpty.TabIndex = 11;
			this.cbEmpty.Text = "Hide Empty Servers";
			this.cbEmpty.UseVisualStyleBackColor = true;
			this.cbEmpty.CheckedChanged += new System.EventHandler(this.ComboBoxTracksSelectedIndexChanged);
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
									"Autocross",
									"Kyoto Ring",
									"Westhill",
									"Aston"});
			this.cbTracks.Location = new System.Drawing.Point(6, 369);
			this.cbTracks.Name = "cbTracks";
			this.cbTracks.Size = new System.Drawing.Size(103, 21);
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
			this.lvMain.Size = new System.Drawing.Size(647, 460);
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
			this.columnHeaderName.Width = 200;
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
			this.toolStripMenuItem1.Text = "&Add to Favourites";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1Click);
			// 
			// btnRefreshMain
			// 
			this.btnRefreshMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefreshMain.Location = new System.Drawing.Point(89, 472);
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
			this.btnJoinMain.Location = new System.Drawing.Point(8, 472);
			this.btnJoinMain.Name = "btnJoinMain";
			this.btnJoinMain.Size = new System.Drawing.Size(75, 23);
			this.btnJoinMain.TabIndex = 1;
			this.btnJoinMain.Text = "&Join";
			this.btnJoinMain.UseVisualStyleBackColor = true;
			this.btnJoinMain.Click += new System.EventHandler(this.btnJoinClick);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.buttonRefreshFav);
			this.tabPage3.Controls.Add(this.btnJoinFav);
			this.tabPage3.Controls.Add(this.lvFavourites);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(784, 501);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Favourites";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// buttonRefreshFav
			// 
			this.buttonRefreshFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonRefreshFav.Location = new System.Drawing.Point(89, 475);
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
			this.btnJoinFav.Location = new System.Drawing.Point(8, 475);
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
			this.lvFavourites.Location = new System.Drawing.Point(8, 8);
			this.lvFavourites.MultiSelect = false;
			this.lvFavourites.Name = "lvFavourites";
			this.lvFavourites.Size = new System.Drawing.Size(768, 461);
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
									this.removeFromFavouritesToolStripMenuItem});
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
			// removeFromFavouritesToolStripMenuItem
			// 
			this.removeFromFavouritesToolStripMenuItem.Name = "removeFromFavouritesToolStripMenuItem";
			this.removeFromFavouritesToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.removeFromFavouritesToolStripMenuItem.Text = "&Remove From Favourites";
			this.removeFromFavouritesToolStripMenuItem.Click += new System.EventHandler(this.RemoveFromFavouritesToolStripMenuItemClick);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(784, 501);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Configuration";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox4);
			this.groupBox1.Controls.Add(this.groupBox5);
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.btnCheckNewVersion);
			this.groupBox1.Controls.Add(this.cbNewVersion);
			this.groupBox1.Controls.Add(this.pathList);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.buttonBrowse);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(778, 495);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Configuration";
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
			this.groupBox5.Location = new System.Drawing.Point(6, 184);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(403, 124);
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
			this.groupBox3.Location = new System.Drawing.Point(6, 314);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(403, 174);
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
			this.btnCheckNewVersion.Location = new System.Drawing.Point(334, 141);
			this.btnCheckNewVersion.Name = "btnCheckNewVersion";
			this.btnCheckNewVersion.Size = new System.Drawing.Size(75, 23);
			this.btnCheckNewVersion.TabIndex = 13;
			this.btnCheckNewVersion.Text = "&Check Now";
			this.btnCheckNewVersion.UseVisualStyleBackColor = true;
			this.btnCheckNewVersion.Click += new System.EventHandler(this.btnCheckNewVersionClick);
			// 
			// cbNewVersion
			// 
			this.cbNewVersion.Location = new System.Drawing.Point(6, 141);
			this.cbNewVersion.Name = "cbNewVersion";
			this.cbNewVersion.Size = new System.Drawing.Size(322, 24);
			this.cbNewVersion.TabIndex = 12;
			this.cbNewVersion.Text = "Check for a new version of Browse For Speed on start up";
			this.cbNewVersion.UseVisualStyleBackColor = true;
			// 
			// pathList
			// 
			this.pathList.FormattingEnabled = true;
			this.pathList.Location = new System.Drawing.Point(6, 38);
			this.pathList.Name = "pathList";
			this.pathList.Size = new System.Drawing.Size(322, 56);
			this.pathList.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 97);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(403, 41);
			this.label5.TabIndex = 7;
			this.label5.Text = "If there is more than one entry in the box above, please select which one is the " +
			"correct path for your LFS.exe. If there are no entries, please click the Browse " +
			"button to manually locate it.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 18);
			this.label2.TabIndex = 5;
			this.label2.Text = "LFS Executable Path:";
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Location = new System.Drawing.Point(334, 38);
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
									this.statusTotal,
									this.statusRefused,
									this.statusNoReply});
			this.statusStrip.Location = new System.Drawing.Point(0, 551);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(792, 22);
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
			this.openFileDialogPS.FileName = "LFSspotter.exe";
			this.openFileDialogPS.Filter = "LFS Spotter Executable|LFSspotter.exe";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 573);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.statusStrip);
			this.DoubleBuffered = true;
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "Browse For Speed";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormClosed);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainKeyDown);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.contextMenuBrowser.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.contextMenuFav.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
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
		private System.Windows.Forms.FontDialog listFontDialog;
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
		private System.Windows.Forms.CheckBox cbEmpty;
		private System.Windows.Forms.NumericUpDown queryWait;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnCheckNewVersion;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.Button btnJoinFav;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ListView lvFavourites;
		private System.Windows.Forms.ComboBox cbTracks;
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
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ListBox pathList;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ColumnHeader columnHeaderConnections;
		private System.Windows.Forms.ColumnHeader columnHeaderPing;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
	}
}
