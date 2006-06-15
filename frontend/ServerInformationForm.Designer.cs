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
	partial class ServerInformationForm : System.Windows.Forms.Form
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
			this.buttonInfoJoin = new System.Windows.Forms.Button();
			this.buttonInfoClose = new System.Windows.Forms.Button();
			this.buttonInfoRefresh = new System.Windows.Forms.Button();
			this.lblServerName = new System.Windows.Forms.Label();
			this.labe3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblInformation = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textInfoPassword = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listPlayers = new System.Windows.Forms.ListBox();
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.btnAddFriend = new System.Windows.Forms.ToolStripMenuItem();
			this.lblCars = new System.Windows.Forms.Label();
			this.labelServerName = new System.Windows.Forms.Label();
			this.labelInfo = new System.Windows.Forms.Label();
			this.labelCars = new System.Windows.Forms.Label();
			this.labelPing = new System.Windows.Forms.Label();
			this.labelTrack = new System.Windows.Forms.Label();
			this.lblPrivate = new System.Windows.Forms.Label();
			this.labelPrivate = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonInfoJoin
			// 
			this.buttonInfoJoin.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonInfoJoin.Location = new System.Drawing.Point(301, 343);
			this.buttonInfoJoin.Name = "buttonInfoJoin";
			this.buttonInfoJoin.Size = new System.Drawing.Size(102, 23);
			this.buttonInfoJoin.TabIndex = 0;
			this.buttonInfoJoin.Text = "&Join";
			this.buttonInfoJoin.UseVisualStyleBackColor = true;
			// 
			// buttonInfoClose
			// 
			this.buttonInfoClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonInfoClose.Location = new System.Drawing.Point(409, 342);
			this.buttonInfoClose.Name = "buttonInfoClose";
			this.buttonInfoClose.Size = new System.Drawing.Size(102, 23);
			this.buttonInfoClose.TabIndex = 1;
			this.buttonInfoClose.Text = "&Close";
			this.buttonInfoClose.UseVisualStyleBackColor = true;
			this.buttonInfoClose.Click += new System.EventHandler(this.ButtonInfoCloseClick);
			// 
			// buttonInfoRefresh
			// 
			this.buttonInfoRefresh.Location = new System.Drawing.Point(12, 343);
			this.buttonInfoRefresh.Name = "buttonInfoRefresh";
			this.buttonInfoRefresh.Size = new System.Drawing.Size(102, 23);
			this.buttonInfoRefresh.TabIndex = 2;
			this.buttonInfoRefresh.Text = "&Refresh";
			this.buttonInfoRefresh.UseVisualStyleBackColor = true;
			this.buttonInfoRefresh.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// lblServerName
			// 
			this.lblServerName.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblServerName.Location = new System.Drawing.Point(12, 9);
			this.lblServerName.Name = "lblServerName";
			this.lblServerName.Size = new System.Drawing.Size(118, 23);
			this.lblServerName.TabIndex = 4;
			this.lblServerName.Text = "Server Name:";
			this.lblServerName.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labe3
			// 
			this.labe3.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.labe3.Location = new System.Drawing.Point(331, 9);
			this.labe3.Name = "labe3";
			this.labe3.Size = new System.Drawing.Size(53, 23);
			this.labe3.TabIndex = 5;
			this.labe3.Text = "Ping:";
			this.labe3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.label1.Location = new System.Drawing.Point(253, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(131, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "Current Track:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblInformation
			// 
			this.lblInformation.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblInformation.Location = new System.Drawing.Point(12, 32);
			this.lblInformation.Name = "lblInformation";
			this.lblInformation.Size = new System.Drawing.Size(118, 23);
			this.lblInformation.TabIndex = 7;
			this.lblInformation.Text = "Information:";
			this.lblInformation.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(93, 348);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(106, 23);
			this.label5.TabIndex = 8;
			this.label5.Text = "Password:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textInfoPassword
			// 
			this.textInfoPassword.Location = new System.Drawing.Point(205, 345);
			this.textInfoPassword.Name = "textInfoPassword";
			this.textInfoPassword.Size = new System.Drawing.Size(90, 21);
			this.textInfoPassword.TabIndex = 9;
			this.textInfoPassword.Leave += new System.EventHandler(this.TextInfoPasswordLeave);
			this.textInfoPassword.TextChanged += new System.EventHandler(this.TextInfoPasswordTextChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listPlayers);
			this.groupBox1.Location = new System.Drawing.Point(12, 103);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(499, 233);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Players";
			// 
			// listPlayers
			// 
			this.listPlayers.ContextMenuStrip = this.contextMenu;
			this.listPlayers.FormattingEnabled = true;
			this.listPlayers.Items.AddRange(new object[] {
									"Retrieving...."});
			this.listPlayers.Location = new System.Drawing.Point(6, 15);
			this.listPlayers.Name = "listPlayers";
			this.listPlayers.Size = new System.Drawing.Size(487, 212);
			this.listPlayers.TabIndex = 0;
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.btnAddFriend});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(156, 26);
			this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuOpening);
			// 
			// btnAddFriend
			// 
			this.btnAddFriend.Name = "btnAddFriend";
			this.btnAddFriend.Size = new System.Drawing.Size(155, 22);
			this.btnAddFriend.Text = "&Add to Friends";
			this.btnAddFriend.Click += new System.EventHandler(this.BtnAddFriendClick);
			// 
			// lblCars
			// 
			this.lblCars.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblCars.Location = new System.Drawing.Point(12, 77);
			this.lblCars.Name = "lblCars";
			this.lblCars.Size = new System.Drawing.Size(118, 23);
			this.lblCars.TabIndex = 11;
			this.lblCars.Text = "Cars:";
			this.lblCars.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelServerName
			// 
			this.labelServerName.Location = new System.Drawing.Point(136, 9);
			this.labelServerName.Name = "labelServerName";
			this.labelServerName.Size = new System.Drawing.Size(180, 23);
			this.labelServerName.TabIndex = 12;
			this.labelServerName.Text = "SERVERNAME HERE";
			// 
			// labelInfo
			// 
			this.labelInfo.Location = new System.Drawing.Point(136, 32);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(115, 23);
			this.labelInfo.TabIndex = 13;
			this.labelInfo.Text = "INFO HERE";
			// 
			// labelCars
			// 
			this.labelCars.Location = new System.Drawing.Point(136, 77);
			this.labelCars.Name = "labelCars";
			this.labelCars.Size = new System.Drawing.Size(375, 23);
			this.labelCars.TabIndex = 14;
			this.labelCars.Text = "CARS HERE LOL";
			// 
			// labelPing
			// 
			this.labelPing.Location = new System.Drawing.Point(396, 9);
			this.labelPing.Name = "labelPing";
			this.labelPing.Size = new System.Drawing.Size(115, 23);
			this.labelPing.TabIndex = 15;
			this.labelPing.Text = "label3";
			// 
			// labelTrack
			// 
			this.labelTrack.Location = new System.Drawing.Point(396, 32);
			this.labelTrack.Name = "labelTrack";
			this.labelTrack.Size = new System.Drawing.Size(115, 23);
			this.labelTrack.TabIndex = 16;
			this.labelTrack.Text = "label3";
			// 
			// lblPrivate
			// 
			this.lblPrivate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPrivate.Location = new System.Drawing.Point(12, 55);
			this.lblPrivate.Name = "lblPrivate";
			this.lblPrivate.Size = new System.Drawing.Size(118, 23);
			this.lblPrivate.TabIndex = 17;
			this.lblPrivate.Text = "Private:";
			this.lblPrivate.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelPrivate
			// 
			this.labelPrivate.Location = new System.Drawing.Point(136, 54);
			this.labelPrivate.Name = "labelPrivate";
			this.labelPrivate.Size = new System.Drawing.Size(100, 23);
			this.labelPrivate.TabIndex = 18;
			this.labelPrivate.Text = "Dunno yet, LOL";
			// 
			// ServerInformationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(523, 373);
			this.Controls.Add(this.lblPrivate);
			this.Controls.Add(this.labelPrivate);
			this.Controls.Add(this.labelTrack);
			this.Controls.Add(this.labelCars);
			this.Controls.Add(this.labelPing);
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.lblCars);
			this.Controls.Add(this.labelServerName);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textInfoPassword);
			this.Controls.Add(this.lblInformation);
			this.Controls.Add(this.lblServerName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labe3);
			this.Controls.Add(this.buttonInfoClose);
			this.Controls.Add(this.buttonInfoJoin);
			this.Controls.Add(this.buttonInfoRefresh);
			this.Controls.Add(this.label5);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ServerInformationForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Server Information";
			this.Load += new System.EventHandler(this.ServerInformationFormLoad);
			this.groupBox1.ResumeLayout(false);
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label lblServerName;
		private System.Windows.Forms.Label lblInformation;
		private System.Windows.Forms.Label lblCars;
		private System.Windows.Forms.Label lblPrivate;
		private System.Windows.Forms.ToolStripMenuItem btnAddFriend;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.Label labelPrivate;
		private System.Windows.Forms.ListBox listPlayers;
		private System.Windows.Forms.Label labelTrack;
		private System.Windows.Forms.Label labelPing;
		private System.Windows.Forms.Label labe3;
		private System.Windows.Forms.Button buttonInfoJoin;
		private System.Windows.Forms.Label labelCars;
		private System.Windows.Forms.Label labelInfo;
		private System.Windows.Forms.Label labelServerName;
		private System.Windows.Forms.Button buttonInfoClose;
		private System.Windows.Forms.Button buttonInfoRefresh;
		private System.Windows.Forms.TextBox textInfoPassword;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
	}
}
