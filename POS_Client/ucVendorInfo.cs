using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class ucVendorInfo : UserControl
	{
		private string VendorID = "";

		private string VendorNo = "";

		private IContainer components;

		private Label l_Date;

		private Label l_vendorName;

		private Label l_Phone;

		private Label l_vendorID;

		private Label l_SupplierName;

		private Label label3;

		private Label label1;

		public event EventHandler OnClickVendorInfo;

		public ucVendorInfo()
		{
			InitializeComponent();
		}

		public void setVendorNo(string VendorNo)
		{
			this.VendorNo = VendorNo;
		}

		public string getVendorNo()
		{
			return VendorNo;
		}

		public void setDate(string date)
		{
			l_Date.Text = date;
		}

		public string getDate()
		{
			return l_Date.Text;
		}

		public void setVendorName(string Name)
		{
			l_vendorName.Text = Name;
		}

		public string getVendorName()
		{
			return l_vendorName.Text;
		}

		public void setVendorID(string ID)
		{
			VendorID = ID;
			l_vendorID.Text = "營業資訊：" + ID;
		}

		public string getVendorID()
		{
			return VendorID;
		}

		public void setPhone(string Phone)
		{
			l_Phone.Text = Phone;
		}

		public string getPhone()
		{
			return l_Phone.Text;
		}

		public void setSupplierName(string Name)
		{
			l_SupplierName.Text = Name;
		}

		public string getSupplierName()
		{
			return l_SupplierName.Text;
		}

		private void ucVendorInfo_MouseEnter(object sender, EventArgs e)
		{
			BackColor = Color.FromArgb(255, 255, 204);
		}

		private void ucVendorInfo_MouseLeave(object sender, EventArgs e)
		{
			BackColor = Control.DefaultBackColor;
		}

		private void ucVendorInfo_Click(object sender, EventArgs e)
		{
			this.OnClickVendorInfo(this, null);
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
			l_Date = new System.Windows.Forms.Label();
			l_vendorName = new System.Windows.Forms.Label();
			l_Phone = new System.Windows.Forms.Label();
			l_vendorID = new System.Windows.Forms.Label();
			l_SupplierName = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			SuspendLayout();
			l_Date.AutoSize = true;
			l_Date.Font = new System.Drawing.Font("微軟正黑體", 9f);
			l_Date.ForeColor = System.Drawing.Color.Gray;
			l_Date.Location = new System.Drawing.Point(13, 13);
			l_Date.Name = "l_Date";
			l_Date.Size = new System.Drawing.Size(43, 16);
			l_Date.TabIndex = 0;
			l_Date.Text = "l_Date";
			l_Date.Click += new System.EventHandler(ucVendorInfo_Click);
			l_vendorName.AutoSize = true;
			l_vendorName.Font = new System.Drawing.Font("微軟正黑體", 15f);
			l_vendorName.Location = new System.Drawing.Point(10, 35);
			l_vendorName.Name = "l_vendorName";
			l_vendorName.Size = new System.Drawing.Size(149, 25);
			l_vendorName.TabIndex = 1;
			l_vendorName.Text = "l_vendorName";
			l_vendorName.Click += new System.EventHandler(ucVendorInfo_Click);
			l_Phone.AutoSize = true;
			l_Phone.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_Phone.ForeColor = System.Drawing.Color.DimGray;
			l_Phone.Location = new System.Drawing.Point(241, 39);
			l_Phone.Name = "l_Phone";
			l_Phone.Size = new System.Drawing.Size(69, 20);
			l_Phone.TabIndex = 4;
			l_Phone.Text = "l_Phone";
			l_Phone.Click += new System.EventHandler(ucVendorInfo_Click);
			l_vendorID.AutoSize = true;
			l_vendorID.Font = new System.Drawing.Font("微軟正黑體", 12f);
			l_vendorID.Location = new System.Drawing.Point(12, 73);
			l_vendorID.Name = "l_vendorID";
			l_vendorID.Size = new System.Drawing.Size(90, 20);
			l_vendorID.TabIndex = 5;
			l_vendorID.Text = "l_vendorID";
			l_vendorID.Click += new System.EventHandler(ucVendorInfo_Click);
			l_SupplierName.AutoSize = true;
			l_SupplierName.Font = new System.Drawing.Font("微軟正黑體", 12f);
			l_SupplierName.Location = new System.Drawing.Point(191, 73);
			l_SupplierName.Name = "l_SupplierName";
			l_SupplierName.Size = new System.Drawing.Size(128, 20);
			l_SupplierName.TabIndex = 6;
			l_SupplierName.Text = "l_SupplierName";
			l_SupplierName.Click += new System.EventHandler(ucVendorInfo_Click);
			label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			label3.Location = new System.Drawing.Point(10, 73);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(2, 18);
			label3.TabIndex = 13;
			label3.Text = "label3";
			label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			label1.Location = new System.Drawing.Point(183, 75);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(2, 18);
			label1.TabIndex = 14;
			label1.Text = "label1";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			base.Controls.Add(label1);
			base.Controls.Add(label3);
			base.Controls.Add(l_SupplierName);
			base.Controls.Add(l_vendorID);
			base.Controls.Add(l_Phone);
			base.Controls.Add(l_vendorName);
			base.Controls.Add(l_Date);
			Cursor = System.Windows.Forms.Cursors.Hand;
			base.Name = "ucVendorInfo";
			base.Size = new System.Drawing.Size(396, 100);
			base.Click += new System.EventHandler(ucVendorInfo_Click);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
