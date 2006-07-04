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
			this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.banToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.daysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.daysToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.weekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.forceSpectateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnSendRacer = new System.Windows.Forms.Button();
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
			this.txtInfo.Size = new System.Drawing.Size(512, 342);
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
			this.edtMessage.Size = new System.Drawing.Size(434, 21);
			this.edtMessage.TabIndex = 3;
			// 
			// btnSend
			// 
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSend.Enabled = false;
			this.btnSend.Location = new System.Drawing.Point(458, 428);
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
			this.btnConnect.Location = new System.Drawing.Point(421, 13);
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
			this.edtPort.Location = new System.Drawing.Point(305, 14);
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
			this.lblinsimPort.Location = new System.Drawing.Point(213, 16);
			this.lblinsimPort.Name = "lblinsimPort";
			this.lblinsimPort.Size = new System.Drawing.Size(91, 17);
			this.lblinsimPort.TabIndex = 7;
			this.lblinsimPort.Text = "InSim Port:";
			this.lblinsimPort.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// chkRelay
			// 
			this.chkRelay.Location = new System.Drawing.Point(213, 41);
			this.chkRelay.Name = "chkRelay";
			this.chkRelay.Size = new System.Drawing.Size(195, 24);
			this.chkRelay.TabIndex = 11;
			this.chkRelay.Text = "Connect to Relay";
			this.chkRelay.UseVisualStyleBackColor = true;
			this.chkRelay.CheckedChanged += new System.EventHandler(this.ChkRelayCheckedChanged);
			// 
			// lstRacers
			// 
			this.lstRacers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lstRacers.ContextMenuStrip = this.racerMenuStrip;
			this.lstRacers.FormattingEnabled = true;
			this.lstRacers.HorizontalScrollbar = true;
			this.lstRacers.Location = new System.Drawing.Point(540, 71);
			this.lstRacers.Name = "lstRacers";
			this.lstRacers.Size = new System.Drawing.Size(132, 342);
			this.lstRacers.TabIndex = 12;
			// 
			// racerMenuStrip
			// 
			this.racerMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.kickToolStripMenuItem,
									this.banToolStripMenuItem,
									this.forceSpectateToolStripMenuItem});
			this.racerMenuStrip.Name = "racerMenuStrip";
			this.racerMenuStrip.Size = new System.Drawing.Size(158, 70);
			// 
			// kickToolStripMenuItem
			// 
			this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
			this.kickToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.kickToolStripMenuItem.Text = "Kick";
			this.kickToolStripMenuItem.Click += new System.EventHandler(this.KickToolStripMenuItemClick);
			// 
			// banToolStripMenuItem
			// 
			this.banToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.hoursToolStripMenuItem,
									this.dayToolStripMenuItem,
									this.daysToolStripMenuItem,
									this.daysToolStripMenuItem1,
									this.weekToolStripMenuItem});
			this.banToolStripMenuItem.Name = "banToolStripMenuItem";
			this.banToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.banToolStripMenuItem.Text = "Ban";
			// 
			// hoursToolStripMenuItem
			// 
			this.hoursToolStripMenuItem.Name = "hoursToolStripMenuItem";
			this.hoursToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.hoursToolStripMenuItem.Text = "12 hours";
			this.hoursToolStripMenuItem.Click += new System.EventHandler(this.HoursToolStripMenuItemClick);
			// 
			// dayToolStripMenuItem
			// 
			this.dayToolStripMenuItem.Name = "dayToolStripMenuItem";
			this.dayToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.dayToolStripMenuItem.Text = "1 day";
			this.dayToolStripMenuItem.Click += new System.EventHandler(this.DayToolStripMenuItemClick);
			// 
			// daysToolStripMenuItem
			// 
			this.daysToolStripMenuItem.Name = "daysToolStripMenuItem";
			this.daysToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.daysToolStripMenuItem.Text = "2 days";
			this.daysToolStripMenuItem.Click += new System.EventHandler(this.DaysToolStripMenuItemClick);
			// 
			// daysToolStripMenuItem1
			// 
			this.daysToolStripMenuItem1.Name = "daysToolStripMenuItem1";
			this.daysToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
			this.daysToolStripMenuItem1.Text = "3 days";
			this.daysToolStripMenuItem1.Click += new System.EventHandler(this.DaysToolStripMenuItem1Click);
			// 
			// weekToolStripMenuItem
			// 
			this.weekToolStripMenuItem.Name = "weekToolStripMenuItem";
			this.weekToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.weekToolStripMenuItem.Text = "1 week";
			this.weekToolStripMenuItem.Click += new System.EventHandler(this.WeekToolStripMenuItemClick);
			// 
			// forceSpectateToolStripMenuItem
			// 
			this.forceSpectateToolStripMenuItem.Name = "forceSpectateToolStripMenuItem";
			this.forceSpectateToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.forceSpectateToolStripMenuItem.Text = "Force spectate";
			this.forceSpectateToolStripMenuItem.Click += new System.EventHandler(this.ForceSpectateToolStripMenuItemClick);
			// 
			// btnSendRacer
			// 
			this.btnSendRacer.Enabled = false;
			this.btnSendRacer.Location = new System.Drawing.Point(540, 428);
			this.btnSendRacer.Name = "btnSendRacer";
			this.btnSendRacer.Size = new System.Drawing.Size(132, 23);
			this.btnSendRacer.TabIndex = 13;
			this.btnSendRacer.Text = "Send to selected racer";
			this.btnSendRacer.UseVisualStyleBackColor = true;
			this.btnSendRacer.Click += new System.EventHandler(this.Button1Click);
			// 
			// AdminForm
			// 
			this.AcceptButton = this.btnSend;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 463);
			this.Controls.Add(this.btnSendRacer);
			this.Controls.Add(this.lstRacers);
			this.Controls.Add(this.chkRelay);
			this.Controls.Add(this.lblinsimPort);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.edtPort);
			this.Controls.Add(this.edtPassword);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.edtMessage);
			this.Controls.Add(this.txtInfo);
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
		private System.Windows.Forms.Button btnSendRacer;
		private System.Windows.Forms.ToolStripMenuItem forceSpectateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem weekToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem daysToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem daysToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dayToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hoursToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem banToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem kickToolStripMenuItem;
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
