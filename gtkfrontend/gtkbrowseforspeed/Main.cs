// project created on 22/05/2006 at 12:29 P
using System;
using Gtk;
using Gdk;
using libbrowseforspeed;
using System.Threading;

namespace gtkbrowseforspeed {
	class MainClass {
		private MainWindow win;
		public static void Main (string[] args) {
			Gdk.Threads.Init();
			MainClass m = new MainClass();
			m.go();
		}
		public void go() {
			Thread queryThread = new Thread(new ThreadStart(this.go2));
			queryThread.Start();
			Application.Init();
			win = new MainWindow();
			win.ShowAll();
			Application.Run();
		}
		
		public void go2() {
			LFSQuery.xpsp2_wait = false;
			LFSQuery q = new LFSQuery();
			LFSQuery.queried += new ServerQueried(queryEventListener);
			int start = System.Environment.TickCount;
			q.query(4, 256, "browseforspeed", 8);
			int end = System.Environment.TickCount;
			Console.Write("query took " + (end - start) + " secs\n");
		}

		public void queryEventListener(object o, ServerInformation info, object callbackObj) {
			Console.Write("HUZZAH! " + (int)callbackObj + "\n");
			if (info != null && info.success) {
				//Console.Write("got host " + LFSQuery.removeColourCodes(info.hostname) + "\n");
				
				Gdk.Threads.Enter();
				win.serverListStore.AppendValues(LFSQuery.removeColourCodes(info.hostname), info.ping, info.players + "/" + info.slots, info.track);
				Gdk.Threads.Leave();
				
			}
		}
	}
}