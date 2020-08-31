using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class UC_Member : UserControl
	{
		private string _IDNO = "";

		private string _SupplierName = "";

		private string _SupplierIdNo = "";

		private string _vendorId = "";

		private string _SupplierNo = "";

		private IContainer components;

		private MyCheckBox myCheckBox1;

		private Label l_VIPNO;

		private Label l_credit;

		private Label l_IDNO;

		private Label l_total;

		private Label l_NAME;

		private Label label7;

		private Label l_HTEL;

		private Label label5;

		private Label label6;

		private Button btn_Cancel;

		public event EventHandler OnClickMember;

		public UC_Member()
		{
			InitializeComponent();
		}

		private void UC_Member_Load(object sender, EventArgs e)
		{
			if (Program.SystemMode == 1)
			{
				label5.Visible = false;
				label6.Visible = false;
				label7.Visible = false;
				l_total.Visible = false;
				l_credit.Visible = false;
			}
		}

		public void setMemberName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				l_NAME.Text = "姓名空白";
			}
			l_NAME.Text = name;
		}

		public void setMemberIdNo(string IdNo)
		{
			if (string.IsNullOrEmpty(IdNo))
			{
				l_IDNO.Text = "身分證空白";
			}
			else if (IdNo.Length > 9)
			{
				_IDNO = IdNo;
				l_IDNO.Text = IdNo.Substring(0, 1) + "*****" + IdNo.Substring(6, 4);
			}
			else
			{
				_IDNO = IdNo;
				l_IDNO.Text = IdNo;
			}
		}

		public void setVendorInfo(string SupplierName, string SupplierNo, string SupplierIdNo, string vendorId)
		{
			l_IDNO.Visible = false;
			l_total.Visible = false;
			label5.Visible = false;
			l_credit.Visible = false;
			label7.Visible = false;
			l_HTEL.Visible = false;
			_SupplierName = SupplierName;
			_SupplierIdNo = SupplierIdNo;
			_vendorId = vendorId;
			_SupplierNo = SupplierNo;
			l_NAME.Text = SupplierName;
			label6.Text = "統一編號:" + SupplierIdNo;
			l_VIPNO.Text = "商業證號:" + vendorId;
		}

		public string getVendorSupplierName()
		{
			return _SupplierName;
		}

		public string getVendorSupplierIdNo()
		{
			return _SupplierIdNo;
		}

		public string getVendorvendorId()
		{
			return _vendorId;
		}

		public string getVendorSupplierNo()
		{
			return _SupplierNo;
		}

		public void setMemberVipNo(string VipNo)
		{
			if (string.IsNullOrEmpty(VipNo))
			{
				l_VIPNO.Text = "無會員號碼";
			}
			else
			{
				l_VIPNO.Text = VipNo;
			}
		}

		public string getMemberVipNo()
		{
			return l_VIPNO.Text;
		}

		public string getMemberName()
		{
			return l_NAME.Text;
		}

		public string getMemberIdNo()
		{
			return _IDNO;
		}

		public void setMemberHTEL(string hTEL)
		{
			l_HTEL.Text = hTEL;
		}

		public void showCheckBox(bool isShow)
		{
			myCheckBox1.Visible = isShow;
		}

		public void showCancelBtn(bool isShow)
		{
		}

		public void checkMember(bool check)
		{
			myCheckBox1.Checked = check;
		}

		public bool isChecked()
		{
			return myCheckBox1.Checked;
		}

		public void setTotal(string total)
		{
			if (string.IsNullOrEmpty(total))
			{
				l_total.Text = "0";
			}
			else
			{
				l_total.Text = string.Format("{0:n0}", int.Parse(total));
			}
		}

		public void setCredit(string credit)
		{
			if (string.IsNullOrEmpty(credit))
			{
				l_credit.Text = "0";
			}
			else
			{
				l_credit.Text = string.Format("{0:n0}", int.Parse(credit));
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

		private void UC_Member_Click(object sender, EventArgs e)
		{
			this.OnClickMember(l_VIPNO.Text, null);
		}

		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			int num = 0;
			string text = "1999-01-01";
			string str = DateTime.Now.ToString("D");
			try
			{
				string text2 = "";
				string text3 = "";
				if (!"".Equals(text))
				{
					text2 = text + " 00:00:00";
					text3 = str + " 23:59:59";
				}
				else
				{
					DateTime now = DateTime.Now;
					text2 = now.AddDays(-7.0).ToString("yyyy-MM-dd 00:00:00");
					text3 = now.ToString("yyyy-MM-dd 23:59:59");
				}
				string str2 = "SELECT * FROM hypos_user_consumelog WHERE memberId ={0} AND editdate >={1} AND editdate <={2}";
				switch (num)
				{
				case 1:
					str2 += " and sellType ='0'";
					break;
				case 2:
					str2 += " and sellType ='1'";
					break;
				case 3:
					str2 += " and sellType ='2'";
					break;
				}
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, str2 + " ORDER BY editdate desc", new string[3]
				{
					l_VIPNO.Text,
					text2,
					text3
				}, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					MessageBox.Show("會員已有消費紀錄，不可刪除");
					return;
				}
				DialogResult dialogResult = MessageBox.Show("確定刪除?", "會員號: " + l_VIPNO.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					string sql = "DELETE FROM hypos_CUST_RTL WHERE VipNo ={0}";
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[1]
					{
						l_VIPNO.Text
					}, CommandOperationType.ExecuteReaderReturnDataTable);
					frmMemberMangement frmMemberMangement = new frmMemberMangement();
					frmMemberMangement.Location = new Point(Form.ActiveForm.Location.X, Form.ActiveForm.Location.Y);
					Form.ActiveForm.Close();
					frmMemberMangement.Show();
					frmMemberMangement.Focus();
				}
				else
				{
					int num2 = 7;
				}
			}
			catch (Exception)
			{
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
			l_VIPNO = new System.Windows.Forms.Label();
			l_credit = new System.Windows.Forms.Label();
			l_IDNO = new System.Windows.Forms.Label();
			l_total = new System.Windows.Forms.Label();
			l_NAME = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			l_HTEL = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			btn_Cancel = new System.Windows.Forms.Button();
			myCheckBox1 = new POS_Client.MyCheckBox();
			SuspendLayout();
			l_VIPNO.AutoSize = true;
			l_VIPNO.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_VIPNO.Location = new System.Drawing.Point(18, 10);
			l_VIPNO.Name = "l_VIPNO";
			l_VIPNO.Size = new System.Drawing.Size(44, 16);
			l_VIPNO.TabIndex = 11;
			l_VIPNO.Text = "會員號";
			l_VIPNO.Click += new System.EventHandler(UC_Member_Click);
			l_VIPNO.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			l_VIPNO.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			l_credit.AutoSize = true;
			l_credit.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_credit.ForeColor = System.Drawing.Color.Red;
			l_credit.Location = new System.Drawing.Point(239, 68);
			l_credit.Name = "l_credit";
			l_credit.Size = new System.Drawing.Size(65, 24);
			l_credit.TabIndex = 20;
			l_credit.Text = "label1";
			l_credit.Click += new System.EventHandler(UC_Member_Click);
			l_credit.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			l_credit.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			l_IDNO.AutoSize = true;
			l_IDNO.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_IDNO.Location = new System.Drawing.Point(100, 10);
			l_IDNO.Name = "l_IDNO";
			l_IDNO.Size = new System.Drawing.Size(68, 16);
			l_IDNO.TabIndex = 12;
			l_IDNO.Text = "身分證字號";
			l_IDNO.Click += new System.EventHandler(UC_Member_Click);
			l_IDNO.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			l_IDNO.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			l_total.AutoSize = true;
			l_total.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_total.ForeColor = System.Drawing.Color.Red;
			l_total.Location = new System.Drawing.Point(92, 68);
			l_total.Name = "l_total";
			l_total.Size = new System.Drawing.Size(65, 24);
			l_total.TabIndex = 19;
			l_total.Text = "label1";
			l_total.Click += new System.EventHandler(UC_Member_Click);
			l_total.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			l_total.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			l_NAME.AutoSize = true;
			l_NAME.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_NAME.Location = new System.Drawing.Point(17, 39);
			l_NAME.Name = "l_NAME";
			l_NAME.Size = new System.Drawing.Size(86, 24);
			l_NAME.TabIndex = 13;
			l_NAME.Text = "農友姓名";
			l_NAME.Click += new System.EventHandler(UC_Member_Click);
			l_NAME.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			l_NAME.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label7.Location = new System.Drawing.Point(324, 75);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(56, 16);
			label7.TabIndex = 18;
			label7.Text = "輔助資格";
			label7.Click += new System.EventHandler(UC_Member_Click);
			label7.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			label7.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			l_HTEL.AutoSize = true;
			l_HTEL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_HTEL.Location = new System.Drawing.Point(129, 39);
			l_HTEL.Name = "l_HTEL";
			l_HTEL.Size = new System.Drawing.Size(86, 24);
			l_HTEL.TabIndex = 14;
			l_HTEL.Text = "電話號碼";
			l_HTEL.Click += new System.EventHandler(UC_Member_Click);
			l_HTEL.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			l_HTEL.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label5.Location = new System.Drawing.Point(177, 75);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(56, 16);
			label5.TabIndex = 17;
			label5.Text = "賒帳金額";
			label5.Click += new System.EventHandler(UC_Member_Click);
			label5.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			label5.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label6.Location = new System.Drawing.Point(18, 75);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(68, 16);
			label6.TabIndex = 16;
			label6.Text = "總消費金額";
			label6.Click += new System.EventHandler(UC_Member_Click);
			label6.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			label6.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			btn_Cancel.Image = POS_Client.Properties.Resources.trash_can_32;
			btn_Cancel.Location = new System.Drawing.Point(384, 4);
			btn_Cancel.Name = "btn_Cancel";
			btn_Cancel.Size = new System.Drawing.Size(34, 38);
			btn_Cancel.TabIndex = 21;
			btn_Cancel.UseVisualStyleBackColor = true;
			btn_Cancel.Visible = false;
			btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
			myCheckBox1.Location = new System.Drawing.Point(352, 10);
			myCheckBox1.Name = "myCheckBox1";
			myCheckBox1.Size = new System.Drawing.Size(30, 31);
			myCheckBox1.TabIndex = 15;
			myCheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox1.UseVisualStyleBackColor = true;
			myCheckBox1.Click += new System.EventHandler(myCheckBox1_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoSize = true;
			BackColor = System.Drawing.Color.White;
			base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			base.Controls.Add(btn_Cancel);
			base.Controls.Add(myCheckBox1);
			base.Controls.Add(l_VIPNO);
			base.Controls.Add(l_credit);
			base.Controls.Add(l_IDNO);
			base.Controls.Add(l_total);
			base.Controls.Add(l_NAME);
			base.Controls.Add(label7);
			base.Controls.Add(l_HTEL);
			base.Controls.Add(label5);
			base.Controls.Add(label6);
			Cursor = System.Windows.Forms.Cursors.Hand;
			base.Name = "UC_Member";
			base.Size = new System.Drawing.Size(425, 102);
			base.Load += new System.EventHandler(UC_Member_Load);
			base.Click += new System.EventHandler(UC_Member_Click);
			base.MouseEnter += new System.EventHandler(UC_Member_MouseEnter);
			base.MouseLeave += new System.EventHandler(UC_Member_MouseLeave);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
