using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmMain : MasterForm
	{
		private Button[] functions;

		private string tempKeyCode = "";

		private IContainer components;

		private Label label1;

		private Button button1;

		private TextBox textBox1;

		private Label l_userLogin;

		private TextBox textBox2;

		private TextBox textBox3;

		private Button button2;

		private Button button3;

		private ListView listView1;

		private Label label2;

		private Button function_1;

		private Button function_2;

		private Button function_3;

		private Button function_4;

		private Button function_5;

		private Button btn_switchMode;

		private Button btn_profile;

		private Button btn_logout;

		private Button function_6;

		private Button function_7;

		private Button function_8;

		private Button function_9;

		private Button function_10;

		private Button function_11;

		private Button function_12;

		private Button function_13;

		private Button function_14;

		private Button function_15;

		private TextBox textBox4;

		private Button button4;

		public frmMain()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			InitializeComponent();
			functions = new Button[15]
			{
				function_1,
				function_2,
				function_3,
				function_4,
				function_5,
				function_6,
				function_7,
				function_8,
				function_9,
				function_10,
				function_11,
				function_12,
				function_13,
				function_14,
				function_15
			};
			btn_switchMode.Visible = true;
			textBox4.Width = 0;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			string str = "";
			if (!string.IsNullOrEmpty(Program.ShopType))
			{
				if (Program.ShopType == "1")
				{
					str = " and Retailer = 1 ";
				}
				else if (Program.ShopType == "2")
				{
					str = " and Wholesaler = 1 ";
				}
				DataTable dataTable;
				if (Program.RoleType == -1)
				{
					string sql = "SELECT a.* FROM hypos_Form a, hypos_ACL b WHERE a.FormID = b.FormID and b.UserType in (-1,0) and a.FormType = {0}" + str + "Order By ShowOrder";
					dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[1]
					{
						Program.SystemMode.ToString()
					}, CommandOperationType.ExecuteReaderReturnDataTable);
				}
				else
				{
					if (Program.RoleType == 1)
					{
						btn_switchMode.Visible = false;
					}
					string sql2 = "SELECT a.* FROM hypos_Form a, hypos_ACL b WHERE a.FormID = b.FormID and b.UserType = {0} and a.FormType = {1}" + str + " Order By ShowOrder";
					dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, new string[2]
					{
						Program.RoleType.ToString(),
						Program.SystemMode.ToString()
					}, CommandOperationType.ExecuteReaderReturnDataTable);
				}
				int num = 0;
				Button[] array = functions;
				foreach (Button button in array)
				{
					if (dataTable.Rows.Count > num)
					{
						button.Name = dataTable.Rows[num]["FormClass"].ToString();
						button.Text = dataTable.Rows[num]["FormName"].ToString();
						switch (dataTable.Rows[num]["FormColor"].ToString())
						{
						case "Y":
							button.BackColor = Color.FromArgb(254, 201, 22);
							break;
						case "B":
							button.BackColor = Color.FromArgb(36, 168, 208);
							break;
						case "G":
							button.BackColor = Color.FromArgb(157, 189, 59);
							break;
						case "P":
							button.BackColor = Color.FromArgb(61, 89, 171);
							break;
						case "O":
							button.BackColor = Color.FromArgb(237, 145, 33);
							break;
						case "Coffee":
							button.BackColor = Color.FromArgb(192, 137, 17);
							break;
						}
						button.Click += new EventHandler(ftn_Click);
					}
					else
					{
						button.Visible = false;
					}
					num++;
				}
			}
			else
			{
				Button[] array = functions;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Visible = false;
				}
			}
		}

		private void ftn_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			object obj = Assembly.GetExecutingAssembly().CreateInstance("POS_Client." + button.Name);
			Type type = obj.GetType();
			if (type.Name == "frmBadPesticide")
			{
				string text = "";
				text = "http://10.10.4.161:8888/BAPHIQ/wSite/baphiq/service/unqualified_pesticide.jsp";
				string text2 = "";
				text2 = "https://pest.baphiq.gov.tw/BAPHIQ/wSite/baphiq/service/unqualified_pesticide.jsp";
				if (Program.IsHyweb)
				{
					Process.Start(text);
				}
				else
				{
					Process.Start(text2);
				}
			}
			else if ("MasterForm".Equals(type.BaseType.Name))
			{
				switchForm((MasterForm)obj);
			}
			else if ("MasterThinForm".Equals(type.BaseType.Name))
			{
				switchForm((MasterThinForm)obj);
			}
		}

		private void btn_logout_Click(object sender, EventArgs e)
		{
			switchForm(new frmLogin());
		}

		private void btn_profile_Click(object sender, EventArgs e)
		{
			switchForm(new frmProfileDetail());
		}

		private void frmMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.C)
			{
				tempKeyCode += "C";
			}
			else if (e.KeyCode == Keys.T)
			{
				tempKeyCode += "T";
			}
			else if (e.KeyCode == Keys.R)
			{
				tempKeyCode += "R";
			}
			else if (e.KeyCode == Keys.L)
			{
				tempKeyCode += "L";
			}
			else if (e.KeyCode == Keys.S)
			{
				tempKeyCode += "S";
			}
			else if (e.KeyCode == Keys.H)
			{
				tempKeyCode += "H";
			}
			else if (e.KeyCode == Keys.I)
			{
				tempKeyCode += "I";
			}
			else if (e.KeyCode == Keys.F)
			{
				tempKeyCode += "F";
			}
			if (e.Control && e.Shift && e.KeyCode == Keys.S)
			{
				switchForm(new frmMainShopSimple());
			}
			if (e.Control && e.Shift && e.KeyCode == Keys.R)
			{
				switchForm(new frmSearchSell_Return());
			}
			if (e.KeyCode != Keys.Return)
			{
				return;
			}
			if (tempKeyCode.Length >= 10)
			{
				tempKeyCode = tempKeyCode.Substring(tempKeyCode.Length - 10);
				if ("CTRLSHIFTS".Equals(tempKeyCode))
				{
					if (Program.SystemMode == 1)
					{
						switchForm(new frmMainShopSimple());
					}
					else
					{
						switchForm(new frmMainShopSimpleWithMoney());
					}
				}
				else if ("CTRLSHIFTR".Equals(tempKeyCode))
				{
					if (Program.SystemMode == 1)
					{
						switchForm(new frmSearchSell());
					}
					else
					{
						switchForm(new frmSearchSell_Return());
					}
				}
			}
			tempKeyCode = "";
		}

		private void btn_switchMode_Click(object sender, EventArgs e)
		{
			switchForm(new frmMode());
		}

		private void button4_Click(object sender, EventArgs e)
		{
			switchForm(new frmNews());
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
			function_1 = new System.Windows.Forms.Button();
			function_2 = new System.Windows.Forms.Button();
			function_3 = new System.Windows.Forms.Button();
			function_4 = new System.Windows.Forms.Button();
			function_5 = new System.Windows.Forms.Button();
			btn_switchMode = new System.Windows.Forms.Button();
			btn_profile = new System.Windows.Forms.Button();
			btn_logout = new System.Windows.Forms.Button();
			function_6 = new System.Windows.Forms.Button();
			function_7 = new System.Windows.Forms.Button();
			function_8 = new System.Windows.Forms.Button();
			function_9 = new System.Windows.Forms.Button();
			function_10 = new System.Windows.Forms.Button();
			function_11 = new System.Windows.Forms.Button();
			function_12 = new System.Windows.Forms.Button();
			function_13 = new System.Windows.Forms.Button();
			function_14 = new System.Windows.Forms.Button();
			function_15 = new System.Windows.Forms.Button();
			textBox4 = new System.Windows.Forms.TextBox();
			button4 = new System.Windows.Forms.Button();
			SuspendLayout();
			function_1.BackColor = System.Drawing.Color.Silver;
			function_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_1.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_1.ForeColor = System.Drawing.Color.White;
			function_1.Location = new System.Drawing.Point(26, 93);
			function_1.Name = "function_1";
			function_1.Size = new System.Drawing.Size(164, 117);
			function_1.TabIndex = 4;
			function_1.Text = "功能1";
			function_1.UseVisualStyleBackColor = false;
			function_2.BackColor = System.Drawing.Color.Silver;
			function_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_2.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_2.ForeColor = System.Drawing.Color.White;
			function_2.Location = new System.Drawing.Point(217, 93);
			function_2.Name = "function_2";
			function_2.Size = new System.Drawing.Size(164, 117);
			function_2.TabIndex = 6;
			function_2.Text = "功能2";
			function_2.UseVisualStyleBackColor = false;
			function_3.BackColor = System.Drawing.Color.Silver;
			function_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_3.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_3.ForeColor = System.Drawing.Color.White;
			function_3.Location = new System.Drawing.Point(408, 93);
			function_3.Name = "function_3";
			function_3.Size = new System.Drawing.Size(164, 117);
			function_3.TabIndex = 7;
			function_3.Text = "功能3";
			function_3.UseVisualStyleBackColor = false;
			function_4.BackColor = System.Drawing.Color.Silver;
			function_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_4.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_4.ForeColor = System.Drawing.Color.White;
			function_4.Location = new System.Drawing.Point(599, 93);
			function_4.Name = "function_4";
			function_4.Size = new System.Drawing.Size(164, 117);
			function_4.TabIndex = 8;
			function_4.Text = "功能4";
			function_4.UseVisualStyleBackColor = false;
			function_5.BackColor = System.Drawing.Color.Silver;
			function_5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_5.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_5.ForeColor = System.Drawing.Color.White;
			function_5.Location = new System.Drawing.Point(790, 93);
			function_5.Name = "function_5";
			function_5.Size = new System.Drawing.Size(164, 117);
			function_5.TabIndex = 9;
			function_5.Text = "功能5";
			function_5.UseVisualStyleBackColor = false;
			btn_switchMode.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_switchMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_switchMode.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_switchMode.ForeColor = System.Drawing.Color.White;
			btn_switchMode.Location = new System.Drawing.Point(522, 524);
			btn_switchMode.Name = "btn_switchMode";
			btn_switchMode.Size = new System.Drawing.Size(101, 80);
			btn_switchMode.TabIndex = 10;
			btn_switchMode.Text = "模式\r\n切換";
			btn_switchMode.UseVisualStyleBackColor = false;
			btn_switchMode.Visible = false;
			btn_switchMode.Click += new System.EventHandler(btn_switchMode_Click);
			btn_profile.BackColor = System.Drawing.Color.FromArgb(36, 168, 208);
			btn_profile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_profile.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_profile.ForeColor = System.Drawing.Color.White;
			btn_profile.Location = new System.Drawing.Point(733, 524);
			btn_profile.Name = "btn_profile";
			btn_profile.Size = new System.Drawing.Size(101, 80);
			btn_profile.TabIndex = 11;
			btn_profile.Text = "個人\r\n資料";
			btn_profile.UseVisualStyleBackColor = false;
			btn_profile.Click += new System.EventHandler(btn_profile_Click);
			btn_logout.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_logout.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_logout.ForeColor = System.Drawing.Color.White;
			btn_logout.Location = new System.Drawing.Point(852, 524);
			btn_logout.Name = "btn_logout";
			btn_logout.Size = new System.Drawing.Size(101, 80);
			btn_logout.TabIndex = 12;
			btn_logout.Text = "登出";
			btn_logout.UseVisualStyleBackColor = false;
			btn_logout.Click += new System.EventHandler(btn_logout_Click);
			function_6.BackColor = System.Drawing.Color.Silver;
			function_6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_6.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_6.ForeColor = System.Drawing.Color.White;
			function_6.Location = new System.Drawing.Point(26, 236);
			function_6.Name = "function_6";
			function_6.Size = new System.Drawing.Size(164, 117);
			function_6.TabIndex = 4;
			function_6.Text = "功能6";
			function_6.UseVisualStyleBackColor = false;
			function_7.BackColor = System.Drawing.Color.Silver;
			function_7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_7.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_7.ForeColor = System.Drawing.Color.White;
			function_7.Location = new System.Drawing.Point(217, 236);
			function_7.Name = "function_7";
			function_7.Size = new System.Drawing.Size(164, 117);
			function_7.TabIndex = 6;
			function_7.Text = "功能7";
			function_7.UseVisualStyleBackColor = false;
			function_8.BackColor = System.Drawing.Color.Silver;
			function_8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_8.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_8.ForeColor = System.Drawing.Color.White;
			function_8.Location = new System.Drawing.Point(412, 236);
			function_8.Name = "function_8";
			function_8.Size = new System.Drawing.Size(164, 117);
			function_8.TabIndex = 7;
			function_8.Text = "功能8";
			function_8.UseVisualStyleBackColor = false;
			function_9.BackColor = System.Drawing.Color.Silver;
			function_9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_9.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_9.ForeColor = System.Drawing.Color.White;
			function_9.Location = new System.Drawing.Point(599, 236);
			function_9.Name = "function_9";
			function_9.Size = new System.Drawing.Size(164, 117);
			function_9.TabIndex = 8;
			function_9.Text = "功能9";
			function_9.UseVisualStyleBackColor = false;
			function_10.BackColor = System.Drawing.Color.Silver;
			function_10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_10.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_10.ForeColor = System.Drawing.Color.White;
			function_10.Location = new System.Drawing.Point(790, 236);
			function_10.Name = "function_10";
			function_10.Size = new System.Drawing.Size(164, 117);
			function_10.TabIndex = 9;
			function_10.Text = "功能10";
			function_10.UseVisualStyleBackColor = false;
			function_11.BackColor = System.Drawing.Color.Silver;
			function_11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_11.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_11.ForeColor = System.Drawing.Color.White;
			function_11.Location = new System.Drawing.Point(26, 379);
			function_11.Name = "function_11";
			function_11.Size = new System.Drawing.Size(164, 117);
			function_11.TabIndex = 4;
			function_11.Text = "功能11";
			function_11.UseVisualStyleBackColor = false;
			function_12.BackColor = System.Drawing.Color.Silver;
			function_12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_12.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_12.ForeColor = System.Drawing.Color.White;
			function_12.Location = new System.Drawing.Point(217, 379);
			function_12.Name = "function_12";
			function_12.Size = new System.Drawing.Size(164, 117);
			function_12.TabIndex = 6;
			function_12.Text = "功能12";
			function_12.UseVisualStyleBackColor = false;
			function_13.BackColor = System.Drawing.Color.Silver;
			function_13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_13.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_13.ForeColor = System.Drawing.Color.White;
			function_13.Location = new System.Drawing.Point(412, 379);
			function_13.Name = "function_13";
			function_13.Size = new System.Drawing.Size(164, 117);
			function_13.TabIndex = 7;
			function_13.Text = "功能13";
			function_13.UseVisualStyleBackColor = false;
			function_14.BackColor = System.Drawing.Color.Silver;
			function_14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_14.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_14.ForeColor = System.Drawing.Color.White;
			function_14.Location = new System.Drawing.Point(599, 379);
			function_14.Name = "function_14";
			function_14.Size = new System.Drawing.Size(164, 117);
			function_14.TabIndex = 8;
			function_14.Text = "功能14";
			function_14.UseVisualStyleBackColor = false;
			function_15.BackColor = System.Drawing.Color.Silver;
			function_15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			function_15.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold);
			function_15.ForeColor = System.Drawing.Color.White;
			function_15.Location = new System.Drawing.Point(790, 379);
			function_15.Name = "function_15";
			function_15.Size = new System.Drawing.Size(164, 117);
			function_15.TabIndex = 9;
			function_15.Text = "功能15";
			function_15.UseVisualStyleBackColor = false;
			textBox4.ImeMode = System.Windows.Forms.ImeMode.Disable;
			textBox4.Location = new System.Drawing.Point(366, 549);
			textBox4.Name = "textBox4";
			textBox4.Size = new System.Drawing.Size(150, 22);
			textBox4.TabIndex = 0;
			button4.BackColor = System.Drawing.Color.FromArgb(237, 145, 33);
			button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button4.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			button4.ForeColor = System.Drawing.Color.White;
			button4.Location = new System.Drawing.Point(628, 524);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(101, 80);
			button4.TabIndex = 34;
			button4.Text = "農藥\r\n公告";
			button4.UseVisualStyleBackColor = false;
			button4.Click += new System.EventHandler(button4_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(button4);
			base.Controls.Add(textBox4);
			base.Controls.Add(btn_logout);
			base.Controls.Add(btn_profile);
			base.Controls.Add(btn_switchMode);
			base.Controls.Add(function_15);
			base.Controls.Add(function_14);
			base.Controls.Add(function_10);
			base.Controls.Add(function_9);
			base.Controls.Add(function_13);
			base.Controls.Add(function_5);
			base.Controls.Add(function_8);
			base.Controls.Add(function_12);
			base.Controls.Add(function_4);
			base.Controls.Add(function_7);
			base.Controls.Add(function_11);
			base.Controls.Add(function_3);
			base.Controls.Add(function_6);
			base.Controls.Add(function_2);
			base.Controls.Add(function_1);
			base.KeyPreview = true;
			base.Name = "frmMain";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			base.Load += new System.EventHandler(frmMain_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmMain_KeyDown);
			base.Controls.SetChildIndex(function_1, 0);
			base.Controls.SetChildIndex(function_2, 0);
			base.Controls.SetChildIndex(function_6, 0);
			base.Controls.SetChildIndex(function_3, 0);
			base.Controls.SetChildIndex(function_11, 0);
			base.Controls.SetChildIndex(function_7, 0);
			base.Controls.SetChildIndex(function_4, 0);
			base.Controls.SetChildIndex(function_12, 0);
			base.Controls.SetChildIndex(function_8, 0);
			base.Controls.SetChildIndex(function_5, 0);
			base.Controls.SetChildIndex(function_13, 0);
			base.Controls.SetChildIndex(function_9, 0);
			base.Controls.SetChildIndex(function_10, 0);
			base.Controls.SetChildIndex(function_14, 0);
			base.Controls.SetChildIndex(function_15, 0);
			base.Controls.SetChildIndex(btn_switchMode, 0);
			base.Controls.SetChildIndex(btn_profile, 0);
			base.Controls.SetChildIndex(btn_logout, 0);
			base.Controls.SetChildIndex(textBox4, 0);
			base.Controls.SetChildIndex(button4, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
