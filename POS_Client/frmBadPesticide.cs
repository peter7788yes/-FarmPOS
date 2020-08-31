using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class frmBadPesticide : MasterThinForm
	{
		private IContainer components;

		private WebBrowser webBrowser1;

		public frmBadPesticide()
			: base("劣農藥查詢")
		{
			InitializeComponent();
			bool isHyweb = Program.IsHyweb;
		}

		public void OpenUrl(string url)
		{
			Process process = new Process();
			process.StartInfo.FileName = "iexplore.exe";
			process.StartInfo.Arguments = url;
			process.Start();
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
			new System.ComponentModel.ComponentResourceManager(typeof(POS_Client.frmBadPesticide));
			webBrowser1 = new System.Windows.Forms.WebBrowser();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			SuspendLayout();
			webBrowser1.Location = new System.Drawing.Point(0, 38);
			webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			webBrowser1.Name = "webBrowser1";
			webBrowser1.Size = new System.Drawing.Size(978, 576);
			webBrowser1.TabIndex = 52;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(webBrowser1);
			base.Name = "frmBadPesticide";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(webBrowser1, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			ResumeLayout(false);
		}
	}
}
