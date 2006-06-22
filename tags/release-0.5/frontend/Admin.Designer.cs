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

namespace BrowseForSpeed.Frontend
{
	partial class AdminForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
			this.txtInfo = new System.Windows.Forms.TextBox();
			this.edtMessage = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.btnConnect = new System.Windows.Forms.Button();
			this.edtPassword = new System.Windows.Forms.TextBox();
			this.edtPort = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblinsimPort = new System.Windows.Forms.Label();
			this.chkRelay = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtInfo
			// 
			this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInfo.BackColor = System.Drawing.SystemColors.Window;
			this.txtInfo.Location = new System.Drawing.Point(12, 71);
			this.txtInfo.Multiline = true;
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.ReadOnly = true;
			this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtInfo.Size = new System.Drawing.Size(498, 342);
			this.txtInfo.TabIndex = 10;
			// 
			// edtMessage
			// 
			this.edtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.edtMessage.Enabled = false;
			this.edtMessage.Location = new System.Drawing.Point(12, 430);
			this.edtMessage.MaxLength = 63;
			this.edtMessage.Name = "edtMessage";
			this.edtMessage.Size = new System.Drawing.Size(426, 21);
			this.edtMessage.TabIndex = 3;
			// 
			// btnSend
			// 
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSend.Enabled = false;
			this.btnSend.Location = new System.Drawing.Point(444, 428);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(66, 23);
			this.btnSend.TabIndex = 2;
			this.btnSend.Text = "Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.BtnSendClick);
			// 
			// btnConnect
			// 
			this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConnect.Location = new System.Drawing.Point(407, 11);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(103, 23);
			this.btnConnect.TabIndex = 3;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.BtnConnectClick);
			// 
			// edtPassword
			// 
			this.edtPassword.Location = new System.Drawing.Point(93, 12);
			this.edtPassword.Name = "edtPassword";
			this.edtPassword.Size = new System.Drawing.Size(100, 21);
			this.edtPassword.TabIndex = 1;
			this.edtPassword.TextChanged += new System.EventHandler(this.EdtPasswordTextChanged);
			// 
			// edtPort
			// 
			this.edtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.edtPort.Location = new System.Drawing.Point(291, 12);
			this.edtPort.Name = "edtPort";
			this.edtPort.Size = new System.Drawing.Size(100, 21);
			this.edtPort.TabIndex = 2;
			this.edtPort.Text = "29999";
			this.edtPort.TextChanged += new System.EventHandler(this.EdtPortTextChanged);
			// 
			// lblPassword
			// 
			this.lblPassword.Location = new System.Drawing.Point(12, 17);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(78, 19);
			this.lblPassword.TabIndex = 6;
			this.lblPassword.Text = "Password:";
			this.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblinsimPort
			// 
			this.lblinsimPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblinsimPort.Location = new System.Drawing.Point(199, 14);
			this.lblinsimPort.Name = "lblinsimPort";
			this.lblinsimPort.Size = new System.Drawing.Size(91, 17);
			this.lblinsimPort.TabIndex = 7;
			this.lblinsimPort.Text = "InSim Port:";
			this.lblinsimPort.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// chkRelay
			// 
			this.chkRelay.Location = new System.Drawing.Point(291, 39);
			this.chkRelay.Name = "chkRelay";
			this.chkRelay.Size = new System.Drawing.Size(219, 24);
			this.chkRelay.TabIndex = 11;
			this.chkRelay.Text = "Connect to Relay";
			this.chkRelay.UseVisualStyleBackColor = true;
			this.chkRelay.CheckedChanged += new System.EventHandler(this.ChkRelayCheckedChanged);
			// 
			// AdminForm
			// 
			this.AcceptButton = this.btnSend;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(522, 463);
			this.Controls.Add(this.chkRelay);
			this.Controls.Add(this.lblinsimPort);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.edtPort);
			this.Controls.Add(this.edtPassword);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.edtMessage);
			this.Controls.Add(this.txtInfo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AdminForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Admin";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminFormFormClosed);
			this.Load += new System.EventHandler(this.AdminFormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox chkRelay;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblinsimPort;
		private System.Windows.Forms.TextBox edtPort;
		private System.Windows.Forms.TextBox edtPassword;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.TextBox txtInfo;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox edtMessage;
	}
}
