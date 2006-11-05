/*
 * Created by SharpDevelop.
 * User: ${USER}
 * Date: ${DATE}
 * Time: ${TIME}
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace BrowseForSpeed.Frontend
{
	partial class frmBan : System.Windows.Forms.Form
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
			this.nmDays = new System.Windows.Forms.NumericUpDown();
			this.lblDays = new System.Windows.Forms.Label();
			this.ok = new System.Windows.Forms.Button();
			this.cancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nmDays)).BeginInit();
			this.SuspendLayout();
			// 
			// nmDays
			// 
			this.nmDays.Location = new System.Drawing.Point(35, 12);
			this.nmDays.Maximum = new decimal(new int[] {
									99999,
									0,
									0,
									0});
			this.nmDays.Name = "nmDays";
			this.nmDays.Size = new System.Drawing.Size(68, 21);
			this.nmDays.TabIndex = 0;
			this.nmDays.Value = new decimal(new int[] {
									30,
									0,
									0,
									0});
			this.nmDays.ValueChanged += new System.EventHandler(this.NumericUpDown1ValueChanged);
			// 
			// lblDays
			// 
			this.lblDays.Location = new System.Drawing.Point(123, 14);
			this.lblDays.Name = "lblDays";
			this.lblDays.Size = new System.Drawing.Size(37, 15);
			this.lblDays.TabIndex = 1;
			this.lblDays.Text = "Days";
			// 
			// ok
			// 
			this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ok.Location = new System.Drawing.Point(109, 46);
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size(75, 23);
			this.ok.TabIndex = 2;
			this.ok.Text = "OK";
			this.ok.UseVisualStyleBackColor = true;
			// 
			// cancel
			// 
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point(12, 46);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(75, 23);
			this.cancel.TabIndex = 3;
			this.cancel.Text = "Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			// 
			// frmBan
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size(202, 81);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.ok);
			this.Controls.Add(this.lblDays);
			this.Controls.Add(this.nmDays);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmBan";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Ban user";
			this.Load += new System.EventHandler(this.FrmBanLoad);
			((System.ComponentModel.ISupportInitialize)(this.nmDays)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.NumericUpDown nmDays;
		private System.Windows.Forms.Label lblDays;
	}
}
