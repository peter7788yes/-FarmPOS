using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class ucShowString : UserControl
	{
		private FlowLayoutPanel _flp;

		private string _info = "";

		private IContainer components;

		private CheckBox cb_Name;

		public event EventHandler OnClickRemove;

		public ucShowString(FlowLayoutPanel flp, string info)
		{
			InitializeComponent();
			_flp = flp;
			_info = info;
			string[] array = info.Split(',');
			cb_Name.Text = array[1];
		}

		private void checkBox1_MouseClick(object sender, MouseEventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			this.OnClickRemove(_info, null);
			if (e.X > Math.Abs(checkBox.Width - 30))
			{
				_flp.Controls.Remove(this);
				Dispose();
			}
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
			cb_Name = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			cb_Name.Appearance = System.Windows.Forms.Appearance.Button;
			cb_Name.AutoSize = true;
			cb_Name.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_Name.Image = POS_Client.Properties.Resources.multiplication;
			cb_Name.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			cb_Name.Location = new System.Drawing.Point(0, 0);
			cb_Name.Margin = new System.Windows.Forms.Padding(0);
			cb_Name.Name = "cb_Name";
			cb_Name.Size = new System.Drawing.Size(130, 36);
			cb_Name.TabIndex = 0;
			cb_Name.Text = "checkBox1";
			cb_Name.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			cb_Name.UseVisualStyleBackColor = true;
			cb_Name.MouseClick += new System.Windows.Forms.MouseEventHandler(checkBox1_MouseClick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.Controls.Add(cb_Name);
			base.Name = "ucShowString";
			base.Size = new System.Drawing.Size(130, 36);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
