using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class frmMedUserControl : UserControl
	{
		private frmMainShopSimple fms;

		public string barcode = "";

		private IContainer components;

		private Label commodityName;

		private Label commodityClass;

		private Button button1;

		public event EventHandler OnClickMember;

		public frmMedUserControl()
		{
			InitializeComponent();
		}

		public void setfms(frmMainShopSimple fms)
		{
			this.fms = fms;
		}

		public void setCommodityName(string commodityName)
		{
			this.commodityName.Text = commodityName;
		}

		public void setCommodityClass(string commodityClass)
		{
			this.commodityClass.Text = commodityClass;
		}

		private void UC_Member_MouseEnter(object sender, EventArgs e)
		{
			BackColor = Color.FromArgb(255, 255, 204);
		}

		private void UC_Member_MouseLeave(object sender, EventArgs e)
		{
			BackColor = Control.DefaultBackColor;
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
			commodityName = new System.Windows.Forms.Label();
			commodityClass = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			SuspendLayout();
			commodityName.AutoSize = true;
			commodityName.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			commodityName.Location = new System.Drawing.Point(61, 25);
			commodityName.Name = "commodityName";
			commodityName.Size = new System.Drawing.Size(69, 25);
			commodityName.TabIndex = 2;
			commodityName.Text = "label1";
			commodityName.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			commodityClass.AutoSize = true;
			commodityClass.ForeColor = System.Drawing.Color.FromArgb(195, 186, 157);
			commodityClass.Location = new System.Drawing.Point(3, 65);
			commodityClass.Name = "commodityClass";
			commodityClass.Size = new System.Drawing.Size(33, 12);
			commodityClass.TabIndex = 3;
			commodityClass.Text = "label2";
			commodityClass.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			button1.Location = new System.Drawing.Point(316, -1);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(83, 81);
			button1.TabIndex = 4;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			base.Controls.Add(button1);
			base.Controls.Add(commodityClass);
			base.Controls.Add(commodityName);
			Cursor = System.Windows.Forms.Cursors.Hand;
			base.Name = "frmMedUserControl";
			base.Size = new System.Drawing.Size(398, 79);
			base.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			base.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
