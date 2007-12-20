﻿// Copyright (C) 2006 Richard Nelson, Ben Kenny, Philip Nelson
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
	partial class PasswordDialog
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
			this.label1 = new System.Windows.Forms.Label();
			this.edtPassword = new System.Windows.Forms.TextBox();
			this.btnPasswordJoin = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Password:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// edtPassword
			// 
			this.edtPassword.Location = new System.Drawing.Point(93, 12);
			this.edtPassword.Name = "edtPassword";
			this.edtPassword.Size = new System.Drawing.Size(167, 20);
			this.edtPassword.TabIndex = 1;
			// 
			// btnPasswordJoin
			// 
			this.btnPasswordJoin.Location = new System.Drawing.Point(12, 38);
			this.btnPasswordJoin.Name = "btnPasswordJoin";
			this.btnPasswordJoin.Size = new System.Drawing.Size(84, 23);
			this.btnPasswordJoin.TabIndex = 2;
			this.btnPasswordJoin.Text = "&Join Server";
			this.btnPasswordJoin.UseVisualStyleBackColor = true;
			// 
			// PasswordDialog
			// 
			this.AcceptButton = this.btnPasswordJoin;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(272, 68);
			this.Controls.Add(this.btnPasswordJoin);
			this.Controls.Add(this.edtPassword);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PasswordDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Enter Password";
			this.Load += new System.EventHandler(this.PasswordDialogLoad);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button btnPasswordJoin;
		private System.Windows.Forms.TextBox edtPassword;
		private System.Windows.Forms.Label label1;
	}
}
