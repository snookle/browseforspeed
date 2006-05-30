/*
 * Created by SharpDevelop.
 * User: ${USER}
 * Date: ${DATE}
 * Time: ${TIME}
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace LFS_ServerBrowser
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
			this.buttonInfoJoin = new System.Windows.Forms.Button();
			this.buttonInfoClose = new System.Windows.Forms.Button();
			this.buttonInfoRefresh = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.labe3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textInfoPassword = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.listPlayers = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.labelServerName = new System.Windows.Forms.Label();
			this.labelInfo = new System.Windows.Forms.Label();
			this.labelCars = new System.Windows.Forms.Label();
			this.labelPing = new System.Windows.Forms.Label();
			this.labelTrack = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labelPrivate = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonInfoJoin
			// 
			this.buttonInfoJoin.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonInfoJoin.Location = new System.Drawing.Point(12, 338);
			this.buttonInfoJoin.Name = "buttonInfoJoin";
			this.buttonInfoJoin.Size = new System.Drawing.Size(75, 23);
			this.buttonInfoJoin.TabIndex = 0;
			this.buttonInfoJoin.Text = "&Join";
			this.buttonInfoJoin.UseVisualStyleBackColor = true;
			// 
			// buttonInfoClose
			// 
			this.buttonInfoClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonInfoClose.Location = new System.Drawing.Point(349, 338);
			this.buttonInfoClose.Name = "buttonInfoClose";
			this.buttonInfoClose.Size = new System.Drawing.Size(75, 23);
			this.buttonInfoClose.TabIndex = 1;
			this.buttonInfoClose.Text = "&Close";
			this.buttonInfoClose.UseVisualStyleBackColor = true;
			this.buttonInfoClose.Click += new System.EventHandler(this.ButtonInfoCloseClick);
			// 
			// buttonInfoRefresh
			// 
			this.buttonInfoRefresh.Location = new System.Drawing.Point(268, 338);
			this.buttonInfoRefresh.Name = "buttonInfoRefresh";
			this.buttonInfoRefresh.Size = new System.Drawing.Size(75, 23);
			this.buttonInfoRefresh.TabIndex = 2;
			this.buttonInfoRefresh.Text = "&Refresh";
			this.buttonInfoRefresh.UseVisualStyleBackColor = true;
			this.buttonInfoRefresh.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.label2.Location = new System.Drawing.Point(12, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "Server Name:";
			// 
			// labe3
			// 
			this.labe3.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.labe3.Location = new System.Drawing.Point(291, 9);
			this.labe3.Name = "labe3";
			this.labe3.Size = new System.Drawing.Size(36, 23);
			this.labe3.TabIndex = 5;
			this.labe3.Text = "Ping:";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.label1.Location = new System.Drawing.Point(237, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "Current Track:";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.label4.Location = new System.Drawing.Point(12, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Information:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(93, 343);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 8;
			this.label5.Text = "Password:";
			// 
			// textInfoPassword
			// 
			this.textInfoPassword.Location = new System.Drawing.Point(153, 340);
			this.textInfoPassword.Name = "textInfoPassword";
			this.textInfoPassword.PasswordChar = '*';
			this.textInfoPassword.Size = new System.Drawing.Size(109, 21);
			this.textInfoPassword.TabIndex = 9;
			this.textInfoPassword.Leave += new System.EventHandler(this.TextInfoPasswordLeave);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.listPlayers);
			this.groupBox1.Location = new System.Drawing.Point(12, 103);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(412, 233);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Players";
			// 
			// listPlayers
			// 
			this.listPlayers.FormattingEnabled = true;
			this.listPlayers.Items.AddRange(new object[] {
									"Retrieving...."});
			this.listPlayers.Location = new System.Drawing.Point(6, 15);
			this.listPlayers.Name = "listPlayers";
			this.listPlayers.Size = new System.Drawing.Size(400, 212);
			this.listPlayers.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.label6.Location = new System.Drawing.Point(12, 77);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 23);
			this.label6.TabIndex = 11;
			this.label6.Text = "Cars:";
			// 
			// labelServerName
			// 
			this.labelServerName.Location = new System.Drawing.Point(105, 9);
			this.labelServerName.Name = "labelServerName";
			this.labelServerName.Size = new System.Drawing.Size(180, 23);
			this.labelServerName.TabIndex = 12;
			this.labelServerName.Text = "SERVERNAME HERE";
			// 
			// labelInfo
			// 
			this.labelInfo.Location = new System.Drawing.Point(105, 32);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(126, 23);
			this.labelInfo.TabIndex = 13;
			this.labelInfo.Text = "INFO HERE";
			// 
			// labelCars
			// 
			this.labelCars.Location = new System.Drawing.Point(45, 77);
			this.labelCars.Name = "labelCars";
			this.labelCars.Size = new System.Drawing.Size(379, 23);
			this.labelCars.TabIndex = 14;
			this.labelCars.Text = "CARS HERE LOL";
			// 
			// labelPing
			// 
			this.labelPing.Location = new System.Drawing.Point(333, 9);
			this.labelPing.Name = "labelPing";
			this.labelPing.Size = new System.Drawing.Size(91, 23);
			this.labelPing.TabIndex = 15;
			this.labelPing.Text = "label3";
			// 
			// labelTrack
			// 
			this.labelTrack.Location = new System.Drawing.Point(333, 32);
			this.labelTrack.Name = "labelTrack";
			this.labelTrack.Size = new System.Drawing.Size(91, 23);
			this.labelTrack.TabIndex = 16;
			this.labelTrack.Text = "label3";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(12, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 23);
			this.label3.TabIndex = 17;
			this.label3.Text = "Private:";
			// 
			// labelPrivate
			// 
			this.labelPrivate.Location = new System.Drawing.Point(105, 55);
			this.labelPrivate.Name = "labelPrivate";
			this.labelPrivate.Size = new System.Drawing.Size(100, 23);
			this.labelPrivate.TabIndex = 18;
			this.labelPrivate.Text = "Dunno yet, LOL";
			// 
			// ServerInformationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(436, 373);
			this.ControlBox = false;
			this.Controls.Add(this.labelPrivate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelTrack);
			this.Controls.Add(this.labelPing);
			this.Controls.Add(this.labelCars);
			this.Controls.Add(this.labelInfo);
			this.Controls.Add(this.labelServerName);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textInfoPassword);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labe3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonInfoRefresh);
			this.Controls.Add(this.buttonInfoClose);
			this.Controls.Add(this.buttonInfoJoin);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ServerInformationForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Server Information";
			this.Load += new System.EventHandler(this.ServerInformationFormLoad);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label labelPrivate;
		private System.Windows.Forms.Label label3;
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
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}
