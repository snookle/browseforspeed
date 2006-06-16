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
		private bool refreshing = false;
		
		public void RefreshPlayerList()
		{
			SetControlProperty(buttonInfoJoin, "Enabled", false);
			SetControlProperty(buttonInfoRefresh, "Enabled", false);
			refreshing = true;
			listPlayers.Items.Clear();			
			int i = LFSQuery.getPubStatInfo(ref info);
			if (this.exiting) return;
			if (i == 1) { //LFSQuery.getPubStatInfo(ref info)) {
				labelPrivate.Text = info.passworded ? MainForm.languages.GetString("Global.Yes") : MainForm.languages.GetString("Global.No");
				if (info.players > 0) {
					foreach (string player in info.racerNames) {
						listPlayers.Items.Add(player);
					}
				} else {
					listPlayers.Items.Add(MainForm.languages.GetString("ServerInformationForm.EmptyServer"));
				}
			} else {
				if (i == 0) {
					listPlayers.Items.Add(MainForm.languages.GetString("ServerInformationForm.NoServer"));
				} else if (i == -1) {
					listPlayers.Items.Add(MainForm.languages.GetString("ServerInformationForm.PubstatError"));
				}
				labelPrivate.Text = "N/A";				
			}
			refreshing = false;
			SetControlProperty(buttonInfoRefresh, "Enabled", true);
			SetControlProperty(buttonInfoJoin, "Enabled", true);
			SetControlProperty(buttonInfoRefresh, "Text", MainForm.languages.GetString("ServerInformationForm.buttonInfoRefresh"));
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
			labelPrivate.Text = "N/A";
			labelInfo.Text = "N/A";
			this.exiting = true;
			buttonInfoRefresh.Enabled = true;
			buttonInfoJoin.Enabled = true;
			this.Close();
		}
		
		void ServerInformationFormLoad(object sender, System.EventArgs e) {
			buttonInfoJoin.Text = MainForm.languages.GetString("ServerInformationForm.buttonInfoJoin");
			buttonInfoClose.Text = MainForm.languages.GetString("ServerInformationForm.buttonInfoClose");
			buttonInfoRefresh.Text = MainForm.languages.GetString("ServerInformationForm.buttonInfoRefresh");
			lblServerName.Text = MainForm.languages.GetString("ServerInformationForm.lblServerName");
			labe3.Text = MainForm.languages.GetString("ServerInformationForm.labe3");
			label1.Text = MainForm.languages.GetString("ServerInformationForm.label1");
			lblInformation.Text = MainForm.languages.GetString("ServerInformationForm.lblInformation");
			label5.Text = MainForm.languages.GetString("ServerInformationForm.label5");
			groupBox1.Text = MainForm.languages.GetString("ServerInformationForm.groupBox1");
			btnAddFriend.Text = MainForm.languages.GetString("ServerInformationForm.btnAddFriend");
			lblCars.Text = MainForm.languages.GetString("ServerInformationForm.lblCars");
			lblPrivate.Text = MainForm.languages.GetString("ServerInformationForm.lblPrivate");
			this.Text = MainForm.languages.GetString("ServerInformationForm.this");
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
			SetControlProperty(buttonInfoRefresh, "Text", MainForm.languages.GetString("ServerInformationForm.buttonInfoStop"));
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
			if (!refreshing) {
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
					labelServerName.Text = MainForm.languages.GetString("ServerInformationForm.QueryTimeOut");;
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
			btnAddFriend.Enabled = ((listPlayers.SelectedIndex != -1) && (listPlayers.Items[listPlayers.SelectedIndex].ToString() != MainForm.languages.GetString("ServerInformationForm.EmptyServer")));
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
