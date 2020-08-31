using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class ucCommodityInfo : UserControl
	{
		private frmMainShopSimple fms;

		private IContainer components;

		private Label l_label1;

		private Label l_label2;

		private Label l_commodityName;

		private Label commodityClass;

		private MyCheckBox myCheckBox1;

		private Label l_price_r0;

		private Label l_price;

		private Label l_GDSNO;

		private Label l_barcode;

		private Label lb_SubsideMoney;

		public event EventHandler OnClickCommodity;

		public ucCommodityInfo()
		{
			InitializeComponent();
			if (Program.SystemMode == 1)
			{
				l_price_r0.Visible = false;
				l_price.Visible = false;
			}
		}

		public void setPrice(string price)
		{
			l_price.Text = price;
		}

		public void setfms(frmMainShopSimple fms)
		{
			this.fms = fms;
		}

		public void showCheckBox(bool isShow)
		{
			myCheckBox1.Visible = isShow;
		}

		public void setGDSNO(string GDSNO)
		{
			l_GDSNO.Text = GDSNO;
		}

		public string getGDSNO()
		{
			return l_GDSNO.Text;
		}

		public void setBarcode(string barcode)
		{
			l_barcode.Text = barcode;
		}

		public void setCommodityName(string commodityName)
		{
			l_commodityName.Text = commodityName;
		}

		public string getCommodityName()
		{
			return l_commodityName.Text;
		}

		public void setCommodityClass(string commodityClass)
		{
			this.commodityClass.Text = commodityClass;
		}

		public void checkCommodity(bool check)
		{
			myCheckBox1.Checked = check;
		}

		public void setSubsideMoney(string SubsideMoney)
		{
			lb_SubsideMoney.Text = SubsideMoney;
		}

		public bool isChecked()
		{
			return myCheckBox1.Checked;
		}

		private void UC_Member_MouseEnter(object sender, EventArgs e)
		{
			if (!myCheckBox1.Checked)
			{
				BackColor = Color.FromArgb(255, 255, 204);
			}
		}

		private void UC_Member_MouseLeave(object sender, EventArgs e)
		{
			if (!myCheckBox1.Checked)
			{
				BackColor = Color.White;
			}
		}

		private void myCheckBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (myCheckBox1.Checked)
			{
				BackColor = Color.FromArgb(255, 215, 215);
			}
			else
			{
				BackColor = Color.White;
			}
		}

		private void UC_Commodity_Click(object sender, EventArgs e)
		{
			this.OnClickCommodity(getGDSNO(), null);
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
			l_label1 = new System.Windows.Forms.Label();
			l_label2 = new System.Windows.Forms.Label();
			l_commodityName = new System.Windows.Forms.Label();
			commodityClass = new System.Windows.Forms.Label();
			l_price_r0 = new System.Windows.Forms.Label();
			l_price = new System.Windows.Forms.Label();
			myCheckBox1 = new POS_Client.MyCheckBox();
			l_GDSNO = new System.Windows.Forms.Label();
			l_barcode = new System.Windows.Forms.Label();
			lb_SubsideMoney = new System.Windows.Forms.Label();
			SuspendLayout();
			l_label1.AutoSize = true;
			l_label1.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_label1.ForeColor = System.Drawing.Color.FromArgb(0, 126, 25);
			l_label1.Location = new System.Drawing.Point(10, 9);
			l_label1.Name = "l_label1";
			l_label1.Size = new System.Drawing.Size(47, 16);
			l_label1.TabIndex = 0;
			l_label1.Text = "店內碼:";
			l_label1.Click += new System.EventHandler(UC_Commodity_Click);
			l_label1.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			l_label2.AutoSize = true;
			l_label2.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_label2.ForeColor = System.Drawing.Color.FromArgb(0, 126, 25);
			l_label2.Location = new System.Drawing.Point(169, 9);
			l_label2.Name = "l_label2";
			l_label2.Size = new System.Drawing.Size(59, 16);
			l_label2.TabIndex = 1;
			l_label2.Text = "商品條碼:";
			l_label2.Click += new System.EventHandler(UC_Commodity_Click);
			l_label2.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			l_commodityName.AutoSize = true;
			l_commodityName.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_commodityName.Location = new System.Drawing.Point(0, 40);
			l_commodityName.Name = "l_commodityName";
			l_commodityName.Size = new System.Drawing.Size(69, 25);
			l_commodityName.TabIndex = 2;
			l_commodityName.Text = "label1";
			l_commodityName.Click += new System.EventHandler(UC_Commodity_Click);
			l_commodityName.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			commodityClass.AutoSize = true;
			commodityClass.ForeColor = System.Drawing.Color.FromArgb(195, 186, 157);
			commodityClass.Location = new System.Drawing.Point(3, 75);
			commodityClass.Name = "commodityClass";
			commodityClass.Size = new System.Drawing.Size(33, 12);
			commodityClass.TabIndex = 3;
			commodityClass.Text = "label2";
			commodityClass.Click += new System.EventHandler(UC_Commodity_Click);
			commodityClass.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			l_price_r0.AutoSize = true;
			l_price_r0.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_price_r0.Location = new System.Drawing.Point(280, 67);
			l_price_r0.Name = "l_price_r0";
			l_price_r0.Size = new System.Drawing.Size(45, 20);
			l_price_r0.TabIndex = 17;
			l_price_r0.Text = "售價:";
			l_price.AutoSize = true;
			l_price.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_price.ForeColor = System.Drawing.Color.Red;
			l_price.Location = new System.Drawing.Point(331, 67);
			l_price.Name = "l_price";
			l_price.Size = new System.Drawing.Size(28, 20);
			l_price.TabIndex = 17;
			l_price.Text = "{0}";
			myCheckBox1.Location = new System.Drawing.Point(356, 9);
			myCheckBox1.Name = "myCheckBox1";
			myCheckBox1.Size = new System.Drawing.Size(30, 31);
			myCheckBox1.TabIndex = 16;
			myCheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox1.UseVisualStyleBackColor = true;
			myCheckBox1.CheckedChanged += new System.EventHandler(myCheckBox1_CheckedChanged);
			l_GDSNO.AutoSize = true;
			l_GDSNO.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_GDSNO.ForeColor = System.Drawing.Color.FromArgb(0, 126, 25);
			l_GDSNO.Location = new System.Drawing.Point(63, 9);
			l_GDSNO.Name = "l_GDSNO";
			l_GDSNO.Size = new System.Drawing.Size(23, 16);
			l_GDSNO.TabIndex = 0;
			l_GDSNO.Text = "{0}";
			l_GDSNO.Click += new System.EventHandler(UC_Commodity_Click);
			l_GDSNO.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			l_barcode.AutoSize = true;
			l_barcode.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_barcode.ForeColor = System.Drawing.Color.FromArgb(0, 126, 25);
			l_barcode.Location = new System.Drawing.Point(234, 9);
			l_barcode.Name = "l_barcode";
			l_barcode.Size = new System.Drawing.Size(23, 16);
			l_barcode.TabIndex = 1;
			l_barcode.Text = "{0}";
			l_barcode.Click += new System.EventHandler(UC_Commodity_Click);
			l_barcode.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			lb_SubsideMoney.AutoSize = true;
			lb_SubsideMoney.Font = new System.Drawing.Font("微軟正黑體", 10f);
			lb_SubsideMoney.Location = new System.Drawing.Point(150, 69);
			lb_SubsideMoney.Name = "lb_SubsideMoney";
			lb_SubsideMoney.Size = new System.Drawing.Size(0, 18);
			lb_SubsideMoney.TabIndex = 18;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			base.Controls.Add(lb_SubsideMoney);
			base.Controls.Add(l_price);
			base.Controls.Add(l_price_r0);
			base.Controls.Add(myCheckBox1);
			base.Controls.Add(commodityClass);
			base.Controls.Add(l_commodityName);
			base.Controls.Add(l_barcode);
			base.Controls.Add(l_label2);
			base.Controls.Add(l_GDSNO);
			base.Controls.Add(l_label1);
			Cursor = System.Windows.Forms.Cursors.Hand;
			MaximumSize = new System.Drawing.Size(398, 102);
			MinimumSize = new System.Drawing.Size(398, 102);
			base.Name = "ucCommodityInfo";
			base.Size = new System.Drawing.Size(396, 100);
			base.Click += new System.EventHandler(UC_Commodity_Click);
			base.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			base.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
