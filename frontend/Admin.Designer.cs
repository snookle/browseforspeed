/*
 * Created by SharpDevelop.
 * User: ${USER}
 * Date: ${DATE}
 * Time: ${TIME}
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace BrowseForSpeed
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtInfo
			// 
			this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInfo.Enabled = false;
			this.txtInfo.Location = new System.Drawing.Point(12, 45);
			this.txtInfo.Multiline = true;
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtInfo.Size = new System.Drawing.Size(474, 288);
			this.txtInfo.TabIndex = 0;
			// 
			// edtMessage
			// 
			this.edtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.edtMessage.Enabled = false;
			this.edtMessage.Location = new System.Drawing.Point(12, 350);
			this.edtMessage.Name = "edtMessage";
			this.edtMessage.Size = new System.Drawing.Size(402, 21);
			this.edtMessage.TabIndex = 1;
			// 
			// btnSend
			// 
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSend.Enabled = false;
			this.btnSend.Location = new System.Drawing.Point(420, 348);
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
			this.btnConnect.Location = new System.Drawing.Point(383, 11);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(75, 23);
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
			this.edtPassword.TabIndex = 4;
			this.edtPassword.TextChanged += new System.EventHandler(this.EdtPasswordTextChanged);
			// 
			// edtPort
			// 
			this.edtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.edtPort.Location = new System.Drawing.Point(267, 12);
			this.edtPort.Name = "edtPort";
			this.edtPort.Size = new System.Drawing.Size(100, 21);
			this.edtPort.TabIndex = 5;
			this.edtPort.Text = "29999";
			this.edtPort.TextChanged += new System.EventHandler(this.EdtPortTextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(31, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 19);
			this.label1.TabIndex = 6;
			this.label1.Text = "Password:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(204, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 17);
			this.label2.TabIndex = 7;
			this.label2.Text = "InSim Port:";
			// 
			// AdminForm
			// 
			this.AcceptButton = this.btnSend;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(498, 383);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.edtPort);
			this.Controls.Add(this.edtPassword);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.edtMessage);
			this.Controls.Add(this.txtInfo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AdminForm";
			this.Text = "Admin";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminFormFormClosed);
			this.Load += new System.EventHandler(this.AdminFormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox edtPort;
		private System.Windows.Forms.TextBox edtPassword;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.TextBox txtInfo;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox edtMessage;
	}
}
