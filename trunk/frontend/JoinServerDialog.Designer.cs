

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
	partial class JoinServerDialog : System.Windows.Forms.Form
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
			this.btnJoin = new System.Windows.Forms.Button();
			this.lblServerName = new System.Windows.Forms.Label();
			this.edtServerName = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cbVersion = new System.Windows.Forms.ComboBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnJoin
			// 
			this.btnJoin.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnJoin.Enabled = false;
			this.btnJoin.Location = new System.Drawing.Point(61, 69);
			this.btnJoin.Name = "btnJoin";
			this.btnJoin.Size = new System.Drawing.Size(108, 23);
			this.btnJoin.TabIndex = 4;
			this.btnJoin.Text = "&Join Server";
			this.btnJoin.UseVisualStyleBackColor = true;
			// 
			// lblServerName
			// 
			this.lblServerName.Location = new System.Drawing.Point(12, 17);
			this.lblServerName.Name = "lblServerName";
			this.lblServerName.Size = new System.Drawing.Size(74, 23);
			this.lblServerName.TabIndex = 1;
			this.lblServerName.Text = "Server Name:";
			// 
			// edtServerName
			// 
			this.edtServerName.Location = new System.Drawing.Point(92, 14);
			this.edtServerName.Name = "edtServerName";
			this.edtServerName.Size = new System.Drawing.Size(192, 20);
			this.edtServerName.TabIndex = 1;
			this.edtServerName.TextChanged += new System.EventHandler(this.EdtServerNameTextChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(175, 69);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(109, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// cbVersion
			// 
			this.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbVersion.FormattingEnabled = true;
			this.cbVersion.Items.AddRange(new object[] {
									"S2",
									"S1",
									"Demo"});
			this.cbVersion.Location = new System.Drawing.Point(92, 40);
			this.cbVersion.Name = "cbVersion";
			this.cbVersion.Size = new System.Drawing.Size(88, 21);
			this.cbVersion.TabIndex = 3;
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(12, 43);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(74, 23);
			this.lblVersion.TabIndex = 7;
			this.lblVersion.Text = "Version:";
			// 
			// JoinServerDialog
			// 
			this.AcceptButton = this.btnJoin;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(298, 101);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.cbVersion);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.edtServerName);
			this.Controls.Add(this.lblServerName);
			this.Controls.Add(this.btnJoin);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "JoinServerDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Join Server";
			this.Load += new System.EventHandler(this.JoinServerDialogLoad);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label lblServerName;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.ComboBox cbVersion;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox edtServerName;
		private System.Windows.Forms.Button btnJoin;
	}
}
