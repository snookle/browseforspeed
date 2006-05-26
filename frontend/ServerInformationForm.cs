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
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Net;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using libbrowseforspeed;

namespace LFS_ServerBrowser
{
	/// <summary>
	/// Description of ServerInformationForm.
	/// </summary>
	public partial class ServerInformationForm
	{
		private ServerInformation info;
		
		public void RefreshPlayerList()
		{						
			SetControlProperty(buttonInfoJoin, "Enabled", false);
			SetControlProperty(buttonInfoRefresh, "Enabled", false);
			listPlayers.Items.Clear();
			if (LFSQuery.getPubStatInfo(ref info)) {
				labelPrivate.Text = info.passworded ? "Yes" : "No";
				if (info.players > 0) {
					foreach (string player in info.racerNames) {
						listPlayers.Items.Add(player);
					}
				} else {
					listPlayers.Items.Add("No players currently on the server.");
				}
			} else {
				labelPrivate.Text = "N/A";
				listPlayers.Items.Add("Error querying server.");
			}
			SetControlProperty(buttonInfoRefresh, "Enabled", true);
			SetControlProperty(buttonInfoJoin, "Enabled", true);
			SetControlProperty(buttonInfoRefresh, "Text", "&Refresh");
		}
		public void SetInfo(ServerInformation info)
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
		
		public ServerInformation GetInfo()
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
				q.query(0, 0, "browseforspeed", server, 3);
				Thread t = new Thread(new ThreadStart(RefreshPlayerList));
				t.Start();
			} catch(Exception e) {
				MessageBox.Show(e.Message + " - " + e.StackTrace, "", MessageBoxButtons.OK);
			}
		}
		
		public void queryCallback(object o, ServerInformation info, Object cbObj) {
			if ((int)cbObj != 3) { //not for us
				return;
			}
			if (info != null) {
				//MessageBox.Show("queryCallback firing", "", MessageBoxButtons.OK);
				refreshServer(info);
			}
		}
		Thread t;
		void RefreshButtonClick(object sender, System.EventArgs e)
		{
			if (buttonInfoRefresh.Text == "&Refresh"){				
				//LFSQuery.queried -= new ServerQueried(main.queryMainEventListener);
				//LFSQuery.queried -= new ServerQueried(main.queryFavEventListener);				
				LFSQuery.queried += new ServerQueried(queryCallback);
				t = new Thread(new ThreadStart(MakeMainQuery));
	  			t.Start();
			} else {
				LFSQuery.stopQuerying();
			}
		}
		
		delegate void RefreshServerDelegate(ServerInformation info);

		public void refreshServer(ServerInformation info)
		{
			try{
			// Make sure we're on the right thread
			//MessageBox.Show("in refreshserver", "", MessageBoxButtons.OK);
			//if(this.InvokeRequired == false) {
				
		
			if (info.success){
					SetInfo(info);
			} else {
				listPlayers.Items.Clear();
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
