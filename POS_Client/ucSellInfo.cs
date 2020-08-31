using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class ucSellInfo : UserControl
	{
		public string barcode = "";

		private IContainer components;

		private Label l_VIPNO;

		private Label sellDate;

		private Label MemberName;

		private Label items;

		private Label sellNo;

		private Label cellphone;

		private Label memberNo;

		private Label payType;

		private Label sum;

		public event EventHandler OnClickMember;

		public ucSellInfo()
		{
			InitializeComponent();
			if (Program.SystemMode == 1)
			{
				sum.Visible = false;
				payType.Visible = false;
				items.Location = new Point(4, 73);
			}
		}

		public void setsellNo(string sellNo)
		{
			this.sellNo.Text = sellNo;
		}

		public void setsellDate(string sellDate)
		{
			this.sellDate.Text = sellDate;
		}

		public void setMemberName(string MemberName)
		{
			this.MemberName.Text = MemberName;
		}

		public void setcellphone(string cellphone)
		{
			this.cellphone.Text = cellphone;
		}

		public void setmemberNo(string memberNo)
		{
			this.memberNo.Text = memberNo;
		}

		public void setitems(string items)
		{
			this.items.Text = items;
		}

		public void setPayType(string payType)
		{
			this.payType.Text = payType;
		}

		public void setSum(string sum)
		{
			this.sum.Text = sum;
		}

		private void UC_Member_MouseEnter(object sender, EventArgs e)
		{
			BackColor = Color.FromArgb(255, 255, 204);
		}

		private void UC_Member_MouseLeave(object sender, EventArgs e)
		{
			BackColor = Control.DefaultBackColor;
		}

		private void UC_Member_Click(object sender, EventArgs e)
		{
			this.OnClickMember(sellNo.Text, null);
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
			l_VIPNO = new System.Windows.Forms.Label();
			sellDate = new System.Windows.Forms.Label();
			MemberName = new System.Windows.Forms.Label();
			items = new System.Windows.Forms.Label();
			sellNo = new System.Windows.Forms.Label();
			cellphone = new System.Windows.Forms.Label();
			memberNo = new System.Windows.Forms.Label();
			payType = new System.Windows.Forms.Label();
			sum = new System.Windows.Forms.Label();
			SuspendLayout();
			l_VIPNO.AutoSize = true;
			l_VIPNO.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_VIPNO.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
			l_VIPNO.Location = new System.Drawing.Point(3, 9);
			l_VIPNO.Name = "l_VIPNO";
			l_VIPNO.Size = new System.Drawing.Size(59, 16);
			l_VIPNO.TabIndex = 0;
			l_VIPNO.Text = "銷售單號:";
			l_VIPNO.Click += new System.EventHandler(UC_Member_Click);
			l_VIPNO.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			sellDate.AutoSize = true;
			sellDate.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			sellDate.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
			sellDate.Location = new System.Drawing.Point(219, 9);
			sellDate.Name = "sellDate";
			sellDate.Size = new System.Drawing.Size(59, 16);
			sellDate.TabIndex = 1;
			sellDate.Text = "銷售日期:";
			sellDate.Click += new System.EventHandler(UC_Member_Click);
			sellDate.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			MemberName.AutoSize = true;
			MemberName.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			MemberName.Location = new System.Drawing.Point(3, 39);
			MemberName.Name = "MemberName";
			MemberName.Size = new System.Drawing.Size(69, 25);
			MemberName.TabIndex = 2;
			MemberName.Text = "label1";
			MemberName.Click += new System.EventHandler(UC_Member_Click);
			MemberName.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			items.AutoSize = true;
			items.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			items.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
			items.Location = new System.Drawing.Point(193, 73);
			items.Name = "items";
			items.Size = new System.Drawing.Size(63, 17);
			items.TabIndex = 3;
			items.Text = "購買品項:";
			items.Click += new System.EventHandler(UC_Member_Click);
			items.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			sellNo.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			sellNo.ForeColor = System.Drawing.Color.Red;
			sellNo.Location = new System.Drawing.Point(59, 9);
			sellNo.MaximumSize = new System.Drawing.Size(200, 20);
			sellNo.Name = "sellNo";
			sellNo.Size = new System.Drawing.Size(160, 20);
			sellNo.TabIndex = 4;
			sellNo.Text = "label1";
			cellphone.AutoSize = true;
			cellphone.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cellphone.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
			cellphone.Location = new System.Drawing.Point(92, 43);
			cellphone.Name = "cellphone";
			cellphone.Size = new System.Drawing.Size(44, 17);
			cellphone.TabIndex = 5;
			cellphone.Text = "label2";
			memberNo.AutoSize = true;
			memberNo.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			memberNo.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
			memberNo.Location = new System.Drawing.Point(213, 42);
			memberNo.Name = "memberNo";
			memberNo.Size = new System.Drawing.Size(50, 17);
			memberNo.TabIndex = 6;
			memberNo.Text = "會員號:";
			payType.AutoSize = true;
			payType.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			payType.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
			payType.Location = new System.Drawing.Point(4, 73);
			payType.Name = "payType";
			payType.Size = new System.Drawing.Size(63, 17);
			payType.TabIndex = 14;
			payType.Text = "付款模式:";
			sum.AutoSize = true;
			sum.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			sum.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
			sum.Location = new System.Drawing.Point(278, 73);
			sum.Name = "sum";
			sum.Size = new System.Drawing.Size(63, 17);
			sum.TabIndex = 13;
			sum.Text = "消費總額:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			base.Controls.Add(payType);
			base.Controls.Add(sum);
			base.Controls.Add(memberNo);
			base.Controls.Add(cellphone);
			base.Controls.Add(sellNo);
			base.Controls.Add(items);
			base.Controls.Add(MemberName);
			base.Controls.Add(sellDate);
			base.Controls.Add(l_VIPNO);
			Cursor = System.Windows.Forms.Cursors.Hand;
			MaximumSize = new System.Drawing.Size(398, 102);
			MinimumSize = new System.Drawing.Size(398, 102);
			base.Name = "ucSellInfo";
			base.Size = new System.Drawing.Size(396, 100);
			base.Click += new System.EventHandler(UC_Member_Click);
			base.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			base.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
