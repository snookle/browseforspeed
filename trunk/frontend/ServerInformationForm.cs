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

namespace BrowseForSpeed.Frontend
{
	public partial class ServerInformationForm
	{
		private ServerInformation info;
		private bool exiting;
		
		public void RefreshPlayerList()
		{
			SetControlProperty(buttonInfoJoin, "Enabled", false);
			SetControlProperty(buttonInfoRefresh, "Enabled", false);
			listPlayers.Items.Clear();			
			int i = LFSQuery.getPubStatInfo(ref info);
			if (this.exiting) return;
			if (i == 1) { //LFSQuery.getPubStatInfo(ref info)) {
				labelPrivate.Text = info.passworded ? "Yes" : "No";
				if (info.players > 0) {
					foreach (string player in info.racerNames) {
						listPlayers.Items.Add(player);
					}
				} else {
					listPlayers.Items.Add("No players currently on the server.");
				}
			} else {
				if (i == 0) {
					listPlayers.Items.Add("Couldn't find server on pubstat!");
				} else if (i == -1) {
					listPlayers.Items.Add("Error querying pubstat!");
				}
				labelPrivate.Text = "N/A";				
			}
			SetControlProperty(buttonInfoRefresh, "Enabled", true);
			SetControlProperty(buttonInfoJoin, "Enabled", true);
			SetControlProperty(buttonInfoRefresh, "Text", "&Refresh");
		}
		public void SetInfo(ServerInformation info) {
			if (info == null) return;
			this.info = info;
			textInfoPassword.Text = info.password;
		}
		
		public ServerInformation GetInfo() {
			return info;
		}
		private MainForm main;
		
		public ServerInformationForm(MainForm m) {
			this.main = m;
			this.exiting = false;
			InitializeComponent();
		}		
		
		void ButtonInfoCloseClick(object sender, System.EventArgs e) {
			LFSQuery.queried -= new ServerQueried(queryCallback);
			labelPrivate.Text = "Dunno yet LOL";
			this.exiting = true;
			buttonInfoRefresh.Enabled = true;
			buttonInfoJoin.Enabled = true;
			buttonInfoRefresh.Text = "&Refresh";
			this.Close();
		}
		
		void ServerInformationFormLoad(object sender, System.EventArgs e) {
			this.CenterToParent();
			this.exiting = false;
			RefreshButtonClick(sender, e);
		}
		
		delegate void SetValueDelegate(Object obj, Object val, Object[] index);
		
		public void SetControlProperty(Control ctrl, String propName, Object val) {
			System.Reflection.PropertyInfo propInfo = ctrl.GetType().GetProperty(propName);
			Delegate dgtSetValue = new SetValueDelegate(propInfo.SetValue);
      		ctrl.Invoke(dgtSetValue, new Object[3] { ctrl, val, /*index*/null });
		}
				
		void MakeMainQuery() {
			LFSQuery q;
			SetControlProperty(buttonInfoRefresh, "Text", "&Stop");
			SetControlProperty(buttonInfoJoin, "Enabled", false);
			try{
				q = new LFSQuery();
				ServerInformation[] server = new ServerInformation[1];
				server[0] = this.info;
				q.query(0, 0, "browseforspeed", server, 3);
				Thread t = new Thread(new ThreadStart(RefreshPlayerList));
				t.Start();
			} catch(Exception e) {
				MessageBox.Show(e.Message + " - " + e.StackTrace, "", MessageBoxButtons.OK);
			}
		}
		
		public void queryCallback(object o, ServerInformation info, Object cbObj) {
			if ((int)cbObj != 3) return;
			if (info != null) {				
				refreshServer(info);
			}
		}
		
		void RefreshButtonClick(object sender, System.EventArgs e) {
			Thread t;
			if (buttonInfoRefresh.Text == "&Refresh") {				
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
			if (info.success){
				this.info = info;
				info.hostname = LFSQuery.removeColourCodes(info.hostname);				
				labelServerName.Text = info.hostname;
				labelCars.Text = MainForm.CarsToString(LFSQuery.getCarNames(info.cars));
				labelInfo.Text = MainForm.RulesToString(info.rules);
				labelPing.Text = info.ping.ToString();
				labelTrack.Text = info.track;				
			} else {				
				labelServerName.Text = "Query timed out";
				labelPing.Text = "9999";
				labelCars.Text = "N/A";
				labelTrack.Text = "N/A";				
			}
			} catch(Exception e){}
  		}
		
		void TextInfoPasswordLeave(object sender, System.EventArgs e)
		{
			this.info.password = textInfoPassword.Text;
		}
		
		void ContextMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			btnAddFriend.Enabled = ((listPlayers.SelectedIndex != -1) && (listPlayers.Items[listPlayers.SelectedIndex].ToString() != "No players currently on the server."));
		}
		
		void BtnAddFriendClick(object sender, System.EventArgs e)
		{
			if (listPlayers.SelectedIndex == -1)
				return;
			main.AddFriend(listPlayers.Items[listPlayers.SelectedIndex].ToString(), true);
		}
		
		void TextInfoPasswordTextChanged(object sender, System.EventArgs e) {
			info.password = textInfoPassword.Text;
		}
	}
}
