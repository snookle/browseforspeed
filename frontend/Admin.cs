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
using System.Collections.Generic;
using System.Collections;
using FullMotion.LiveForSpeed.InSim;
using FullMotion.LiveForSpeed.InSim.EventHandlers;
using libbrowseforspeed;
using System.Text.RegularExpressions;

namespace BrowseForSpeed.Frontend
{
	public class racer {
		public racer(string playername, int connection) {
			this.playername = playername;
			this.connection = connection;
		}
		public string playername;
		public int connection;
	}
	public partial class AdminForm
	{
		InSimHandler handler;
		ServerInformation info;
		FullMotion.LiveForSpeed.InSim.Configuration config;
		Dictionary<String, racer> racers;
		frmBan ban;

		public AdminForm(ServerInformation info) {
			InitializeComponent();
			this.info = info;
			handler = new InSimHandler(true, false);
			this.Text = MainForm.languages.GetString("Admin.this") + " - " + info.hostname;
			edtPassword.Text = info.adminPassword;
			if (info.insimPort != 0) {
				edtPort.Text = info.insimPort.ToString();
			} else {
				edtPort.Text = "29999";
			}
		}

		void AdminFormLoad(object sender, System.EventArgs e) {
			this.Icon = new Icon(GetType().Assembly.GetManifestResourceStream("BrowseForSpeed.ca3r.ico"));
			btnSend.Text = MainForm.languages.GetString("Admin.btnSend");
			btnConnect.Text = MainForm.languages.GetString("Admin.btnConnect");
			lblPassword.Text = MainForm.languages.GetString("Admin.lblPassword");
			lblinsimPort.Text = MainForm.languages.GetString("Admin.lblinsimPort");
			chkRelay.Text = MainForm.languages.GetString("Admin.chkRelay");
			kickToolStripMenuItem.Text = MainForm.languages.GetString("Admin.kick");
			forceSpectateToolStripMenuItem.Text = MainForm.languages.GetString("Admin.spectate");
			banToolStripMenuItem.Text = MainForm.languages.GetString("Admin.ban");
			hoursToolStripMenuItem.Text = MainForm.languages.GetString("Admin.ban12h");
			dayToolStripMenuItem.Text = MainForm.languages.GetString("Admin.ban1d");
			daysToolStripMenuItem.Text = MainForm.languages.GetString("Admin.ban2d");
			daysToolStripMenuItem1.Text = MainForm.languages.GetString("Admin.ban3d");
			weekToolStripMenuItem.Text = MainForm.languages.GetString("Admin.ban1w");
			customToolStripMenuItem.Text = MainForm.languages.GetString("Admin.banCustom");
			this.Text = MainForm.languages.GetString("Admin.this") + " - " + info.hostname;
			racers = new Dictionary<String, racer>();			
		}

		void BtnSendClick(object sender, System.EventArgs e) {
			handler.SendMessage(edtMessage.Text);
			edtMessage.Clear();
		}
		
		/*private void cbState(FullMotion.LiveForSpeed.InSim.Events.) {			
			handler.LFSMessage -= new MessageEventHandler(cbMessage);
			handler.Close();
			btnSend.Enabled = false;
			edtMessage.Enabled = false;
			btnConnect.Text = MainForm.languages.GetString("Admin.btnConnect");
		}*/

		private void cbMessage(FullMotion.LiveForSpeed.InSim.Events.Message m) {
			string msg = "";
			if (m.IsSplitMessage) {
				msg = "<" + m.UserName + "> " + m.MessageText;
			} else {
				msg = m.MessageText;
			}
			msg = Regex.Replace(msg, "\\^L", "");
			txtInfo.Text += "\r\n" + msg;
			txtInfo.SelectionStart = txtInfo.Text.Length;
			txtInfo.ScrollToCaret();
		}
		
		private void cbConnection(FullMotion.LiveForSpeed.InSim.Events.RaceTrackConnection c) {
			//MessageBox.Show("Got a connection!");
			if (!racers.ContainsKey(c.Username)) {
				racers.Add(c.Username, new racer(c.Playername, c.ConnectionNumber));
				lstRacers.Items.Add(c.Username);
			}
		}
		private void cbConnectionLeave(FullMotion.LiveForSpeed.InSim.Events.RaceTrackConnectionLeave c) {
			racers.Remove(c.Username);
			lstRacers.Items.Remove(c.Username);
		}

		void AdminFormFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e) {
			try {
				handler.LFSMessage -= new MessageEventHandler(cbMessage);
				if (handler.State == InSimHandler.HandlerState.Connected) {
					handler.Close();
				}
			} catch (Exception) {
			} finally {
				this.Dispose(true);
			}
		}

		void BtnConnectClick(object sender, System.EventArgs e) {
			if (handler.State != InSimHandler.HandlerState.Connected) {				
				try {
					config = handler.Configuration;
					btnConnect.Enabled = false;
					chkRelay.Enabled = false;
					config.LFSHost = info.host.Address.ToString();
					config.LFSHostPort = Int32.Parse(edtPort.Text);
					config.AdminPass = edtPassword.Text;
					config.ReplyPort = 80085;
					config.UseSplitMessages = true;
					config.UseKeepAlives = true;
					config.RaceTracking = RaceTrackType.MultiCarTracking;
					config.GuaranteedDelivery = true;
					config.UseRelay = chkRelay.Checked;
					config.Hostname = info.hostname;
					handler.Initialize(3);
					handler.LFSMessage += new MessageEventHandler(cbMessage);
					handler.RaceTrackConnection += new RaceTrackNewConnectionHandler(cbConnection);
					handler.RaceTrackConnectionLeave += new RaceTrackConnectionLeaveHandler(cbConnectionLeave);
					//handler.RequestRaceTrackingMultiCarInfo();
					for (int i = 0; i < 24; i++) {
						handler.RequestRaceTrackingConnectionInfo(i);
					}
					//handler.LFSState += new StateEventHandler(cbState);
					btnSend.Enabled = true;
					btnConnect.Text = MainForm.languages.GetString("Admin.btnDisconnect");
					edtMessage.Enabled = true;
					btnConnect.Enabled = true;
					btnSendRacer.Enabled = true;
					edtMessage.Focus();
				} catch (Exception ex) {
					btnConnect.Enabled = true;
					chkRelay.Enabled = true;					
					MessageBox.Show(MainForm.languages.GetString("InSimError"), MainForm.languages.GetString("Admin.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			} else {
				handler.LFSMessage -= new MessageEventHandler(cbMessage);
				handler.Close();
				btnSendRacer.Enabled = false;
				btnSend.Enabled = false;
				edtMessage.Enabled = false;
				chkRelay.Enabled = true;
				lstRacers.Items.Clear();
				racers.Clear();
				btnConnect.Text = MainForm.languages.GetString("Admin.btnConnect");
			}
		}

		void EdtPasswordTextChanged(object sender, System.EventArgs e) {
			info.adminPassword = edtPassword.Text;
		}

		void EdtPortTextChanged(object sender, System.EventArgs e) {
			if (edtPort.Text.Length == 0) return;
			try {
				info.insimPort = Int32.Parse(edtPort.Text);
				if (info.insimPort <= 0 || info.insimPort >= 65536) throw new Exception();
			} catch (Exception) {
				string message = String.Format(MainForm.languages.GetString("InvalidPort"), edtPort.Text);
				MessageBox.Show(message, MainForm.languages.GetString("Admin.this"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				edtPort.Text = edtPort.Text.Remove(edtPort.Text.Length - 1, 1);
			}
		}
		
		void ChkRelayCheckedChanged(object sender, System.EventArgs e) {			
			edtPort.Enabled = !chkRelay.Checked;
		}
		
		void KickToolStripMenuItemClick(object sender, System.EventArgs e) {
			if (lstRacers.SelectedIndex == -1) return;
			handler.SendMessage("/kick " + lstRacers.SelectedItem.ToString());
		}
		
		void HoursToolStripMenuItemClick(object sender, System.EventArgs e) {
			if (lstRacers.SelectedIndex == -1) return;
			handler.SendMessage("/ban " + lstRacers.Text + " 0");
		}
		
		void DayToolStripMenuItemClick(object sender, System.EventArgs e) {
			if (lstRacers.SelectedIndex == -1) return;
			handler.SendMessage("/ban " + lstRacers.Text + " 1");
		}
		
		void DaysToolStripMenuItemClick(object sender, System.EventArgs e) {
			if (lstRacers.SelectedIndex == -1) return;
			handler.SendMessage("/ban " + lstRacers.Text + " 2");
		}
		
		void DaysToolStripMenuItem1Click(object sender, System.EventArgs e) {
			if (lstRacers.SelectedIndex == -1) return;
			handler.SendMessage("/ban " + lstRacers.Text + " 3");
		}
		
		void WeekToolStripMenuItemClick(object sender, System.EventArgs e) {
			if (lstRacers.SelectedIndex == -1) return;
			handler.SendMessage("/ban " + lstRacers.Text + " 7");
		}
		
		void ForceSpectateToolStripMenuItemClick(object sender, System.EventArgs e) {
			if (lstRacers.SelectedIndex == -1) return;
			handler.SendMessage("/spectate " + lstRacers.Text);			
		}		
		
		void Button1Click(object sender, System.EventArgs e) {
			if (lstRacers.Text != null && lstRacers.Text != "") {
				handler.SendMessageToConnection(edtMessage.Text, racers[lstRacers.Text].connection);
				edtMessage.Clear();
			}
		}
		
		void CustomToolStripMenuItemClick(object sender, System.EventArgs e) {
			if (lstRacers.SelectedIndex == -1) return;
			ban = new frmBan();
			if (ban.ShowDialog(this) == DialogResult.OK) {
				handler.SendMessage("/ban " + lstRacers.Text + " " + ban.days);
			}
			ban.Dispose();
		}
	}
}
