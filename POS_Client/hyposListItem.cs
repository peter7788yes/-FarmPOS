using POS_Client.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class hyposListItem : UserControl
	{
		private IContainer components;

		private FlowLayoutPanel flowLayoutPanel1;

		private Label l_seq;

		private PictureBox pb_img;

		private Label l_content;

		private Label l_timestamp;

		public hyposListItem()
		{
			InitializeComponent();
		}

		public hyposListItem(string seq, Image img, string content, string timestamp)
		{
			InitializeComponent();
			l_seq.Text = seq;
			pb_img.Image = img;
			l_content.Text = content;
			l_timestamp.Text = timestamp;
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
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			l_seq = new System.Windows.Forms.Label();
			pb_img = new System.Windows.Forms.PictureBox();
			l_content = new System.Windows.Forms.Label();
			l_timestamp = new System.Windows.Forms.Label();
			flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pb_img).BeginInit();
			SuspendLayout();
			flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			flowLayoutPanel1.Controls.Add(l_seq);
			flowLayoutPanel1.Controls.Add(pb_img);
			flowLayoutPanel1.Controls.Add(l_content);
			flowLayoutPanel1.Controls.Add(l_timestamp);
			flowLayoutPanel1.Location = new System.Drawing.Point(24, 6);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(730, 27);
			flowLayoutPanel1.TabIndex = 0;
			l_seq.AutoSize = true;
			l_seq.Dock = System.Windows.Forms.DockStyle.Fill;
			l_seq.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_seq.Location = new System.Drawing.Point(3, 0);
			l_seq.Name = "l_seq";
			l_seq.Size = new System.Drawing.Size(35, 30);
			l_seq.TabIndex = 0;
			l_seq.Text = "{0}";
			l_seq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			pb_img.Image = POS_Client.Properties.Resources.success;
			pb_img.Location = new System.Drawing.Point(44, 3);
			pb_img.Name = "pb_img";
			pb_img.Size = new System.Drawing.Size(52, 24);
			pb_img.TabIndex = 1;
			pb_img.TabStop = false;
			l_content.AutoSize = true;
			l_content.Dock = System.Windows.Forms.DockStyle.Fill;
			l_content.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_content.Location = new System.Drawing.Point(102, 0);
			l_content.Name = "l_content";
			l_content.Size = new System.Drawing.Size(35, 30);
			l_content.TabIndex = 2;
			l_content.Text = "{1}";
			l_content.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_timestamp.AutoSize = true;
			l_timestamp.Dock = System.Windows.Forms.DockStyle.Fill;
			l_timestamp.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_timestamp.Location = new System.Drawing.Point(143, 0);
			l_timestamp.Name = "l_timestamp";
			l_timestamp.Size = new System.Drawing.Size(35, 30);
			l_timestamp.TabIndex = 3;
			l_timestamp.Text = "{2}";
			l_timestamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(flowLayoutPanel1);
			base.Name = "hyposListItem";
			base.Size = new System.Drawing.Size(757, 42);
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pb_img).EndInit();
			ResumeLayout(false);
		}
	}
}
