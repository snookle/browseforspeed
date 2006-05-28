// project created on 22/05/2006 at 12:29 P
using System;
using Gtk;
using Gdk;
using libbrowseforspeed;
using System.Threading;
using System.Text;

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
			LFSQuery.QTHREADS = 64;
			LFSQuery q = new LFSQuery();
			LFSQuery.queried += new ServerQueried(queryEventListener);
			int start = System.Environment.TickCount;
			q.query(0, 0, "browseforspeed", 1);
			int end = System.Environment.TickCount;
			Console.Write("query took " + (end - start) + " secs\n");
		}

		public void queryEventListener(object o, ServerInformation info, object callbackObj) {
			if (info != null && info.success) {
				Gdk.Threads.Enter();
				win.serverListStore.AppendValues(LFSQuery.removeColourCodes(info.hostname), info.ping, info.players + "/" + info.slots, info.track, CarsToString(info.cars));
				Gdk.Threads.Leave();
			}
		}

		private string CarsToString(ulong c) {
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
	}
}