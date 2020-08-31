using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmNoSaleReport : MasterThinForm
	{
		private string _RegisterCode = "";

		private IContainer components;

		private WebBrowser webBrowser1;

		public frmNoSaleReport()
			: base("無銷售回報")
		{
			InitializeComponent();
			string sql = "SELECT RegisterCode FROM hypos_RegisterLicense where isApproved = 'Y' order by CreateDate desc limit 1";
			_RegisterCode = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			string text = "";
			string str = Uri.EscapeDataString(_RegisterCode);
			text = "https://pest.baphiq.gov.tw/BAPHIQ/wSite/upos/sale_no.jsp?applySerial=" + str + "&version=" + Program.Version;
			webBrowser1.Navigate(text);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS_Client.frmNoSaleReport));
			webBrowser1 = new System.Windows.Forms.WebBrowser();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			SuspendLayout();
			webBrowser1.Location = new System.Drawing.Point(0, 35);
			webBrowser1.Margin = new System.Windows.Forms.Padding(0);
			webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			webBrowser1.Name = "webBrowser1";
			webBrowser1.Size = new System.Drawing.Size(981, 585);
			webBrowser1.TabIndex = 2;
			webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(webBrowser1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "frmNoSaleReport";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(webBrowser1, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			ResumeLayout(false);
		}
	}
}
