using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmNews : MasterForm
	{
		private IContainer components;

		private WebBrowser webBrowser1;

		private Button button1;

		public frmNews()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string sql = "SELECT SystemMode FROM hypos_SysParam";
			string text = DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar).ToString();
			if ("".Equals(text))
			{
				switchForm(new frmMode());
				return;
			}
			Program.SystemMode = int.Parse(text);
			switchForm(new frmMain());
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS_Client.frmNews));
			webBrowser1 = new System.Windows.Forms.WebBrowser();
			button1 = new System.Windows.Forms.Button();
			SuspendLayout();
			webBrowser1.Location = new System.Drawing.Point(0, 127);
			webBrowser1.Margin = new System.Windows.Forms.Padding(0);
			webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			webBrowser1.Name = "webBrowser1";
			webBrowser1.Size = new System.Drawing.Size(981, 493);
			webBrowser1.TabIndex = 2;
			webBrowser1.Url = new System.Uri("https://pest.baphiq.gov.tw/BAPHIQ/wSite/baphiq/lp.jsp?pk=All", System.UriKind.Absolute);
			button1.BackColor = System.Drawing.Color.FromArgb(36, 168, 208);
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			button1.ForeColor = System.Drawing.Color.White;
			button1.Location = new System.Drawing.Point(852, 74);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(117, 41);
			button1.TabIndex = 1;
			button1.Text = "進入系統";
			button1.UseVisualStyleBackColor = false;
			button1.Click += new System.EventHandler(button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(button1);
			base.Controls.Add(webBrowser1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "frmNews";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			base.Controls.SetChildIndex(webBrowser1, 0);
			base.Controls.SetChildIndex(button1, 0);
			ResumeLayout(false);
		}
	}
}
