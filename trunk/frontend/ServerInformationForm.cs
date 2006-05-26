/*
 * Created by SharpDevelop.
 * User: Ben
 * Date: 23/05/2006
 * Time: 10:51 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Net;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using liblfsbrowser;

namespace LFS_ServerBrowser
{
	/// <summary>
	/// Description of ServerInformationForm.
	/// </summary>
	public partial class ServerInformationForm
	{
		private serverInformation info;
		
		public void RefreshPlayerList()
		{
			//if (!info.success) return;
			SetControlProperty(buttonInfoJoin, "Enabled", false);
			ArrayList players = LFSQuery.getPlayers(LFSQuery.removeColourCodes(info.hostname));
			listPlayers.Items.Clear();
			if (players.Count == 0) {
				listPlayers.Items.Add("No players currently on the server.");
			} else {
				foreach(String player in players){
					listPlayers.Items.Add(player);
				}
			}
			SetControlProperty(buttonInfoJoin, "Enabled", true);
			SetControlProperty(buttonInfoRefresh, "Text", "&Refresh");
		}
		public void SetInfo(serverInformation info)
		{
			if (info == null) return;
			this.info = info;
			labelServerName.Text = LFSQuery.removeColourCodes(info.hostname);
			labelCars.Text = MainForm.CarsToString(info.cars);
			labelInfo.Text = MainForm.RulesToString(info.rules);
			labelPing.Text = info.ping.ToString();
			labelTrack.Text = info.track;
			listPlayers.Items.Clear();
		}
		
		public serverInformation GetInfo()
		{
			return info;
		}
		private MainForm main;
		
		public ServerInformationForm(MainForm m)
		{
			this.main = m;			
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ServerInformationFormActivated(object sender, System.EventArgs e)
		{
		}
		
		void ButtonInfoCloseClick(object sender, System.EventArgs e)
		{
			LFSQuery.queried -= new ServerQueried(queryCallback);
		}
		
		void ServerInformationFormLoad(object sender, System.EventArgs e)
		{
			RefreshButtonClick(sender, e);
		}
		
		delegate void SetValueDelegate(Object obj, Object val, Object[] index);
		public void SetControlProperty(Control ctrl, String propName, Object val)
		{
			System.Reflection.PropertyInfo propInfo = ctrl.GetType().GetProperty(propName);
			Delegate dgtSetValue = new SetValueDelegate(propInfo.SetValue);
      		ctrl.Invoke(dgtSetValue, new Object[3] { ctrl, val, /*index*/null });
		}
		
		LFSQuery q;
		void MakeMainQuery()
		{
			SetControlProperty(buttonInfoRefresh, "Text", "&Stop");
			SetControlProperty(buttonInfoJoin, "Enabled", false);
			
			try{
				q = new LFSQuery();
				IPEndPoint[] server = new IPEndPoint[1];
				server[0] = this.info.host;
				//MessageBox.Show(this.info.host.ToString(), "", MessageBoxButtons.OK);
				q.query(0, 0, "browseforspeed", server);				
				Thread t = new Thread(new ThreadStart(RefreshPlayerList));
				t.Start();
			} catch(Exception e) {
				MessageBox.Show(e.Message + " - " + e.StackTrace, "", MessageBoxButtons.OK);
			}
		}
		
		public void queryCallback(object o, serverInformation info) {
			if (info != null) {
				//MessageBox.Show("queryCallback firing", "", MessageBoxButtons.OK);
				refreshServer(info);
			}
		}
		Thread t;
		void RefreshButtonClick(object sender, System.EventArgs e)
		{
			if (buttonInfoRefresh.Text == "&Refresh"){				
				LFSQuery.queried -= new ServerQueried(main.queryMainEventListener);
				LFSQuery.queried -= new ServerQueried(main.queryFavEventListener);
				LFSQuery.queried -= new ServerQueried(queryCallback);
				LFSQuery.queried += new ServerQueried(queryCallback);
				t = new Thread(new ThreadStart(MakeMainQuery));
	  			t.Start();
			} else {
				LFSQuery.stopQuerying();
			}
		}
		
		delegate void RefreshServerDelegate(serverInformation info);

		public void refreshServer(serverInformation info)
		{
			try{
			// Make sure we're on the right thread
			//MessageBox.Show("in refreshserver", "", MessageBoxButtons.OK);
			//if(this.InvokeRequired == false) {
				
		
			if (info.success){
					SetInfo(info);
			} else {
				labelServerName.Text = "Not Responding";
				labelPing.Text = "9999";
				labelCars.Text = "N/A";
				labelTrack.Text = "N/A";
				listPlayers.Items.Add("Server timed out");
			}

			    //add the server asynchonously
			   // RefreshServerDelegate refreshServerdel = new RefreshServerDelegate(refreshServer);
			  //  this.BeginInvoke(refreshServerdel, new object[] {info});
			} catch(Exception e){}
  		}
	}
}
