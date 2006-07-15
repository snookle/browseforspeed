using System;
using System.Collections;
using Gdk;
using Gtk;
using libbrowseforspeed;

public class MainWindow: Gtk.Window {
	public TreeView serverListTreeView;
	public ListStore serverListStore;
	protected Gtk.VBox mainVBox;

	public MainWindow (): base ("") {
		Stetic.Gui.Build (this, typeof(MainWindow));
		serverListStore = new ListStore(typeof(string), typeof(int), typeof(string), typeof(string), typeof(string));
		serverListStore.SetSortColumnId(1, Gtk.SortType.Ascending);
		serverListTreeView = new TreeView(serverListStore);
		serverListTreeView.AppendColumn("Host", new CellRendererText(), "text", 0);
		serverListTreeView.AppendColumn("Ping", new CellRendererText(), "text", 1);
		serverListTreeView.AppendColumn("Players", new CellRendererText(), "text", 2);
		serverListTreeView.AppendColumn("Track", new CellRendererText(), "text", 3);
		serverListTreeView.AppendColumn("Cars", new CellRendererText(), "text", 4);
		//serverListTreeView.AppendColumn("Info", new Gtk.CellRendererText(), "text", 4);
		serverListTreeView.EnableSearch = true;
		serverListTreeView.HeadersClickable = true;
		serverListTreeView.ButtonReleaseEvent += new ButtonReleaseEventHandler(menuPopup);
		ScrolledWindow sw = new ScrolledWindow();
		sw.Add(serverListTreeView);
		mainVBox.Add(sw);
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a) {
		Application.Quit ();
		a.RetVal = true;
	}

	private void menuPopup(object o, ButtonReleaseEventArgs args) {
		Menu menu = new Menu();
		MenuItem item = new MenuItem("Show players");
		menu.Append(item);
		item.Activated += new EventHandler(showPlayers);
		item.Show();
		menu.Popup();
		args.RetVal = true;
	}

	private void showPlayers(object o, EventArgs args) {
		TreeIter iter;
		TreeModel model;
		TreeSelection selection = serverListTreeView.Selection;
		if (selection.GetSelected(out model, out iter)) {
			ServerInformation serverInfo = new ServerInformation();
			serverInfo.hostname = (string)model.GetValue(iter, 0);
			int ret = LFSQuery.getPubStatInfo(ref serverInfo);
			if (ret == 1) {
				Console.Write("Players in " + serverInfo.hostname + ": ");
				if (serverInfo.players > 0) {
					foreach (string player in serverInfo.racerNames) {
						Console.Write(player + " ");
					}
				}
				Console.WriteLine();
				Console.Write("This server is ");
				if (!serverInfo.passworded) {
					Console.Write("not ");
				}
				Console.Write("passworded\n");
			} else {
				Console.Write("Pubstat query failed!\n");
			}
		}
	}
}

