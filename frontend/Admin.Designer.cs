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
			this.components = new System.ComponentModel.Container();
			this.txtInfo = new System.Windows.Forms.TextBox();
			this.edtMessage = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.btnConnect = new System.Windows.Forms.Button();
			this.edtPassword = new System.Windows.Forms.TextBox();
			this.edtPort = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblinsimPort = new System.Windows.Forms.Label();
			this.chkRelay = new System.Windows.Forms.CheckBox();
			this.lstRacers = new System.Windows.Forms.ListBox();
			this.racerMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.kick = new System.Windows.Forms.ToolStripMenuItem();
			this.btnBan = new System.Windows.Forms.ToolStripMenuItem();
			this.ban12h = new System.Windows.Forms.ToolStripMenuItem();
			this.ban1d = new System.Windows.Forms.ToolStripMenuItem();
			this.ban2d = new System.Windows.Forms.ToolStripMenuItem();
			this.ban3d = new System.Windows.Forms.ToolStripMenuItem();
			this.ban1w = new System.Windows.Forms.ToolStripMenuItem();
			this.banCustom = new System.Windows.Forms.ToolStripMenuItem();
			this.spectate = new System.Windows.Forms.ToolStripMenuItem();
			this.sendPrivate = new System.Windows.Forms.Button();
			this.racerMenuStrip.SuspendLayout();
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
			this.txtInfo.Size = new System.Drawing.Size(482, 342);
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
			this.edtMessage.Size = new System.Drawing.Size(410, 21);
			this.edtMessage.TabIndex = 3;
			// 
			// btnSend
			// 
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSend.Enabled = false;
			this.btnSend.Location = new System.Drawing.Point(428, 428);
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
			this.btnConnect.Location = new System.Drawing.Point(448, 13);
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
			this.edtPort.Location = new System.Drawing.Point(332, 14);
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
			this.lblinsimPort.Location = new System.Drawing.Point(240, 16);
			this.lblinsimPort.Name = "lblinsimPort";
			this.lblinsimPort.Size = new System.Drawing.Size(91, 17);
			this.lblinsimPort.TabIndex = 7;
			this.lblinsimPort.Text = "InSim Port:";
			this.lblinsimPort.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// chkRelay
			// 
			this.chkRelay.Location = new System.Drawing.Point(276, 41);
			this.chkRelay.Name = "chkRelay";
			this.chkRelay.Size = new System.Drawing.Size(195, 24);
			this.chkRelay.TabIndex = 11;
			this.chkRelay.Text = "Connect to Relay";
			this.chkRelay.UseVisualStyleBackColor = true;
			this.chkRelay.CheckedChanged += new System.EventHandler(this.ChkRelayCheckedChanged);
			// 
			// lstRacers
			// 
			this.lstRacers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lstRacers.ContextMenuStrip = this.racerMenuStrip;
			this.lstRacers.FormattingEnabled = true;
			this.lstRacers.HorizontalScrollbar = true;
			this.lstRacers.Location = new System.Drawing.Point(502, 71);
			this.lstRacers.Name = "lstRacers";
			this.lstRacers.Size = new System.Drawing.Size(165, 342);
			this.lstRacers.TabIndex = 12;
			// 
			// racerMenuStrip
			// 
			this.racerMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.kick,
									this.btnBan,
									this.spectate});
			this.racerMenuStrip.Name = "racerMenuStrip";
			this.racerMenuStrip.Size = new System.Drawing.Size(158, 92);
			// 
			// kick
			// 
			this.kick.Name = "kick";
			this.kick.Size = new System.Drawing.Size(157, 22);
			this.kick.Text = "Kick";
			this.kick.Click += new System.EventHandler(this.KickToolStripMenuItemClick);
			// 
			// btnBan
			// 
			this.btnBan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.ban12h,
									this.ban1d,
									this.ban2d,
									this.ban3d,
									this.ban1w,
									this.banCustom});
			this.btnBan.Name = "btnBan";
			this.btnBan.Size = new System.Drawing.Size(157, 22);
			this.btnBan.Text = "Ban";
			// 
			// ban12h
			// 
			this.ban12h.Name = "ban12h";
			this.ban12h.Size = new System.Drawing.Size(152, 22);
			this.ban12h.Text = "12 hours";
			this.ban12h.Click += new System.EventHandler(this.HoursToolStripMenuItemClick);
			// 
			// ban1d
			// 
			this.ban1d.Name = "ban1d";
			this.ban1d.Size = new System.Drawing.Size(152, 22);
			this.ban1d.Text = "1 day";
			this.ban1d.Click += new System.EventHandler(this.DayToolStripMenuItemClick);
			// 
			// ban2d
			// 
			this.ban2d.Name = "ban2d";
			this.ban2d.Size = new System.Drawing.Size(152, 22);
			this.ban2d.Text = "2 days";
			this.ban2d.Click += new System.EventHandler(this.DaysToolStripMenuItemClick);
			// 
			// ban3d
			// 
			this.ban3d.Name = "ban3d";
			this.ban3d.Size = new System.Drawing.Size(152, 22);
			this.ban3d.Text = "3 days";
			this.ban3d.Click += new System.EventHandler(this.DaysToolStripMenuItem1Click);
			// 
			// ban1w
			// 
			this.ban1w.Name = "ban1w";
			this.ban1w.Size = new System.Drawing.Size(152, 22);
			this.ban1w.Text = "1 week";
			this.ban1w.Click += new System.EventHandler(this.WeekToolStripMenuItemClick);
			// 
			// banCustom
			// 
			this.banCustom.Name = "banCustom";
			this.banCustom.Size = new System.Drawing.Size(152, 22);
			this.banCustom.Text = "Custom...";
			this.banCustom.Click += new System.EventHandler(this.CustomToolStripMenuItemClick);
			// 
			// spectate
			// 
			this.spectate.Name = "spectate";
			this.spectate.Size = new System.Drawing.Size(157, 22);
			this.spectate.Text = "Force spectate";
			this.spectate.Click += new System.EventHandler(this.ForceSpectateToolStripMenuItemClick);
			// 
			// sendPrivate
			// 
			this.sendPrivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.sendPrivate.Enabled = false;
			this.sendPrivate.Location = new System.Drawing.Point(502, 428);
			this.sendPrivate.Name = "sendPrivate";
			this.sendPrivate.Size = new System.Drawing.Size(165, 23);
			this.sendPrivate.TabIndex = 13;
			this.sendPrivate.Text = "Send to selected racer";
			this.sendPrivate.UseVisualStyleBackColor = true;
			this.sendPrivate.Click += new System.EventHandler(this.Button1Click);
			// 
			// AdminForm
			// 
			this.AcceptButton = this.btnSend;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(677, 463);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.lblinsimPort);
			this.Controls.Add(this.chkRelay);
			this.Controls.Add(this.lstRacers);
			this.Controls.Add(this.sendPrivate);
			this.Controls.Add(this.edtPassword);
			this.Controls.Add(this.edtPort);
			this.Controls.Add(this.txtInfo);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.edtMessage);
			this.Controls.Add(this.btnSend);
			this.Name = "AdminForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Admin";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminFormFormClosed);
			this.Load += new System.EventHandler(this.AdminFormLoad);
			this.racerMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem btnBan;
		private System.Windows.Forms.ToolStripMenuItem kick;
		private System.Windows.Forms.ToolStripMenuItem ban12h;
		private System.Windows.Forms.ToolStripMenuItem ban1d;
		private System.Windows.Forms.ToolStripMenuItem ban2d;
		private System.Windows.Forms.ToolStripMenuItem ban3d;
		private System.Windows.Forms.ToolStripMenuItem ban1w;
		private System.Windows.Forms.ToolStripMenuItem spectate;
		private System.Windows.Forms.Button sendPrivate;
		private System.Windows.Forms.ToolStripMenuItem banCustom;
		private System.Windows.Forms.ContextMenuStrip racerMenuStrip;
		private System.Windows.Forms.ListBox lstRacers;
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
