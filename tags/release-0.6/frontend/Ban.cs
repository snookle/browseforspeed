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
	public partial class frmBan
	{
		public decimal days;
		public frmBan() {			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.Icon = new Icon(GetType().Assembly.GetManifestResourceStream("BrowseForSpeed.ca3r.ico"));
			days = 30;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void NumericUpDown1ValueChanged(object sender, System.EventArgs e) {
			days = nmDays.Value;			
		}
		
		void FrmBanLoad(object sender, System.EventArgs e) {
			nmDays.Text = MainForm.languages.GetString("BanDialog.nmDays");
			btnOkay.Text = MainForm.languages.GetString("BanDialog.ok");
			btnCancel.Text = MainForm.languages.GetString("BanDialog.cancel");
		}
	}
}
