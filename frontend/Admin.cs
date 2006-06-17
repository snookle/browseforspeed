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
using FullMotion.LiveForSpeed.InSim;
using FullMotion.LiveForSpeed.InSim.EventHandlers;
using libbrowseforspeed;
using System.Text.RegularExpressions;

namespace BrowseForSpeed.Frontend
{
	public partial class AdminForm
	{
		InSimHandler handler;
		ServerInformation info;
		FullMotion.LiveForSpeed.InSim.Configuration config;

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
			btnSend.Text = MainForm.languages.GetString("Admin.btnSend");
			btnConnect.Text = MainForm.languages.GetString("Admin.btnConnect");
			lblPassword.Text = MainForm.languages.GetString("Admin.lblPassword");
			lblinsimPort.Text = MainForm.languages.GetString("Admin.lblinsimPort");
			lblinsimPort.Text = MainForm.languages.GetString("Admin.chkRelay");
			this.Text = MainForm.languages.GetString("Admin.this");
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
					config.RaceTracking = RaceTrackType.NoTracking;
					config.GuaranteedDelivery = true;
					config.UseRelay = chkRelay.Checked;
					config.Hostname = info.hostname;
					handler.Initialize(3);
					handler.RequestState();
					handler.LFSMessage += new MessageEventHandler(cbMessage);
					//handler.LFSState += new StateEventHandler(cbState);
					btnSend.Enabled = true;
					btnConnect.Text = MainForm.languages.GetString("Admin.btnDisconnect");
					edtMessage.Enabled = true;
					btnConnect.Enabled = true;
					edtMessage.Focus();
				} catch (Exception ex) {					
					btnConnect.Enabled = true;
					chkRelay.Enabled = true;
					MessageBox.Show(MainForm.languages.GetString("InSimError"), MainForm.languages.GetString("Admin.this"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			} else {
				handler.LFSMessage -= new MessageEventHandler(cbMessage);
				handler.Close();
				btnSend.Enabled = false;
				edtMessage.Enabled = false;
				chkRelay.Enabled = true;
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
	}
}
