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
namespace BrowseForSpeed.Frontend
{
	public partial class AdminForm
	{
		InSimHandler handler;
		ServerInformation info;
		
		public AdminForm(ServerInformation info) {
			InitializeComponent();
			this.info = info;
			handler = new InSimHandler(true, false);
			this.Text = "Admin - " + info.hostname;
			edtPassword.Text = info.adminPassword;
			edtPort.Text = info.insimPort.ToString();
		}
		
		void AdminFormLoad(object sender, System.EventArgs e) {
			
		}
		
		void BtnSendClick(object sender, System.EventArgs e) {
			handler.SendMessage(edtMessage.Text);
			edtMessage.Clear();
		}

		private void cbMessage(FullMotion.LiveForSpeed.InSim.Events.Message m) {			
			string msg = "";
			if (m.IsSplitMessage) {
				msg = "<" + m.UserName + "> " + m.MessageText;
			} else {
				msg = m.MessageText;
			}
			txtInfo.Text += msg + "\r\n";
		}
		
		void AdminFormFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e) {
			try {
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
					btnConnect.Enabled = false;
					FullMotion.LiveForSpeed.InSim.Configuration config = handler.Configuration;
					config.LFSHost = info.host.Address.ToString();
					//config.LFSHostPort = info.host.Port;
					config.LFSHostPort = Int32.Parse(edtPort.Text);
					config.AdminPass = edtPassword.Text;
					config.ReplyPort = 80085;
					config.UseSplitMessages = true;
					config.UseKeepAlives = true;
					config.RaceTracking = RaceTrackType.NoTracking;
					config.GuaranteedDelivery = true;
					handler.LFSMessage += new MessageEventHandler(cbMessage);
					handler.Initialize(3);
					handler.RequestState();					
					btnSend.Enabled = true;
					btnConnect.Text = "Disconnect";
					edtMessage.Enabled = true;
					btnConnect.Enabled = true;
					edtMessage.Focus();
				} catch (Exception) {
					btnConnect.Enabled = true;
					MessageBox.Show("Couldn't connect to InSim!");
				}
			} else {
				handler.LFSMessage -= new MessageEventHandler(cbMessage);
				handler.Close();
				btnSend.Enabled = false;
				edtMessage.Enabled = false;
				btnConnect.Text = "Connect";
			}
		}
		
		void EdtPasswordTextChanged(object sender, System.EventArgs e) {			
			info.adminPassword = edtPassword.Text;
		}
		
		void EdtPortTextChanged(object sender, System.EventArgs e) {
			if (edtPort.Text.Length == 0) return;
			info.insimPort = Int32.Parse(edtPort.Text);
		}
	}
}
