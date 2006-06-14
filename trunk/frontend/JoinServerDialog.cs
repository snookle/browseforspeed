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

namespace BrowseForSpeed.Frontend
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class JoinServerDialog
	{
		public JoinServerDialog()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void JoinServerDialogLoad(object sender, System.EventArgs e)
		{
			cbVersion.SelectedIndex = 0;
			edtServerName.Text = "";
			edtPassword.Text = "";
			btnJoin.Text = MainForm.languages.GetString("JoinServerDialog.btnJoin");
			lblServerName.Text = MainForm.languages.GetString("JoinServerDialog.lblServerName");
			btnCancel.Text = MainForm.languages.GetString("JoinServerDialog.btnCancel");
			lblPassword.Text = MainForm.languages.GetString("JoinServerDialog.lblPassword");
			lblVersion.Text = MainForm.languages.GetString("JoinServerDialog.lblVersion");
			this.Text = MainForm.languages.GetString("JoinServerDialog.this");
		}
		
		void EdtServerNameTextChanged(object sender, System.EventArgs e)
		{
			btnJoin.Enabled = (edtServerName.Text != "");
			
		}
		
		public string serverName {
			get	{
				return edtServerName.Text;
			}
		}
		
		public string version {
			get {
				return cbVersion.SelectedItem.ToString();
			}
		}
		
		public string password {
			get {
				return edtPassword.Text;
			}
		}
	}
}
