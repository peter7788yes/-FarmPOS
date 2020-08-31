using POS_Client.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class frmImportResult : Form
	{
		private IContainer components;

		private Label label1;

		private TextBox textBox1;

		public frmImportResult(string result)
		{
			InitializeComponent();
			textBox1.Text = result;
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
			label1 = new System.Windows.Forms.Label();
			textBox1 = new System.Windows.Forms.TextBox();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.Image = POS_Client.Properties.Resources.oblique;
			label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label1.Location = new System.Drawing.Point(216, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(101, 24);
			label1.TabIndex = 0;
			label1.Text = "   匯入結果";
			textBox1.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			textBox1.Location = new System.Drawing.Point(12, 52);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBox1.Size = new System.Drawing.Size(509, 415);
			textBox1.TabIndex = 1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(533, 479);
			base.Controls.Add(textBox1);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmImportResult";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "frmImportResult";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
