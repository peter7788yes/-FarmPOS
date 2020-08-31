using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmProfileDetail : MasterThinForm
	{
		private IContainer components;

		private Panel panel5;

		private Label label10;

		private Panel panel4;

		private Label label8;

		private Panel panel1;

		private Label label1;

		private Panel panel2;

		private Label label4;

		private Panel panel3;

		private Label label6;

		private Panel panel6;

		private Label label12;

		private Panel panel7;

		private Label label14;

		private Panel panel8;

		private Label label16;

		private FlowLayoutPanel flowLayoutPanel3;

		private TextBox tb_password;

		private Label label19;

		private TextBox tb_rePassword;

		private TableLayoutPanel tableLayoutPanel1;

		private Label l_lastLogin;

		private Label l_createDate;

		private Label l_status;

		private Label l_type;

		private TextBox tb_name;

		private TextBox tb_email;

		private Panel panel9;

		private Label label2;

		private Label l_account;

		private Button btn_save;

		public frmProfileDetail()
			: base("個人資料")
		{
			InitializeComponent();
		}

		private void btn_save_Click(object sender, EventArgs e)
		{
			if (!tb_password.Text.Equals(tb_rePassword.Text))
			{
				AutoClosingMessageBox.Show("密碼再確認錯誤，請確認");
				return;
			}
			DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_User set Name={1}, Email = {2}, UpdateDate = dateTime('now') WHERE Account = {0}", new string[3]
			{
				l_account.Text,
				tb_name.Text,
				tb_email.Text
			}, CommandOperationType.ExecuteNonQuery);
			if (!string.IsNullOrEmpty(tb_password.Text))
			{
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_User set Password={1} WHERE Account = {0}", new string[2]
				{
					l_account.Text,
					tb_password.Text
				}, CommandOperationType.ExecuteNonQuery);
			}
			AutoClosingMessageBox.Show("個人資料已更新");
			backToPreviousForm();
		}

		private void frmProfileDetail_Load(object sender, EventArgs e)
		{
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_User", "Account = {0}", "", null, new string[1]
			{
				Program.Casher
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			l_account.Text = dataTable.Rows[0]["Account"].ToString();
			int num = int.Parse(dataTable.Rows[0]["Type"].ToString());
			tb_name.Text = dataTable.Rows[0]["Name"].ToString();
			tb_email.Text = dataTable.Rows[0]["Email"].ToString();
			int num2 = int.Parse(dataTable.Rows[0]["Status"].ToString());
			l_createDate.Text = dataTable.Rows[0]["CreateDate"].ToString();
			l_lastLogin.Text = dataTable.Rows[0]["LastLogin"].ToString();
			switch (num)
			{
			case -1:
				l_type.Text = "凌網管理員";
				break;
			case 0:
				l_type.Text = "管理員";
				break;
			default:
				l_type.Text = "使用者";
				break;
			}
			if (num2 == 0)
			{
				l_status.Text = "正常";
			}
			else
			{
				l_status.Text = "停用";
			}
			if ("hywebAdmin".Equals(l_account.Text))
			{
				tb_password.Enabled = false;
				tb_rePassword.Enabled = false;
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
			panel5 = new System.Windows.Forms.Panel();
			label10 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			panel2 = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			panel7 = new System.Windows.Forms.Panel();
			label14 = new System.Windows.Forms.Label();
			panel8 = new System.Windows.Forms.Panel();
			label16 = new System.Windows.Forms.Label();
			flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			tb_password = new System.Windows.Forms.TextBox();
			label19 = new System.Windows.Forms.Label();
			tb_rePassword = new System.Windows.Forms.TextBox();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			l_lastLogin = new System.Windows.Forms.Label();
			l_createDate = new System.Windows.Forms.Label();
			l_status = new System.Windows.Forms.Label();
			l_type = new System.Windows.Forms.Label();
			tb_name = new System.Windows.Forms.TextBox();
			tb_email = new System.Windows.Forms.TextBox();
			panel9 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			l_account = new System.Windows.Forms.Label();
			btn_save = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel5.SuspendLayout();
			panel4.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			panel3.SuspendLayout();
			panel6.SuspendLayout();
			panel7.SuspendLayout();
			panel8.SuspendLayout();
			flowLayoutPanel3.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			panel9.SuspendLayout();
			SuspendLayout();
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label10);
			panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			panel5.Location = new System.Drawing.Point(1, 221);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(162, 54);
			panel5.TabIndex = 23;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(64, 20);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(90, 21);
			label10.TabIndex = 0;
			label10.Text = "使用者姓名";
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label8);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Location = new System.Drawing.Point(1, 166);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(162, 54);
			panel4.TabIndex = 22;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.White;
			label8.Location = new System.Drawing.Point(96, 22);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(58, 21);
			label8.TabIndex = 0;
			label8.Text = "身分別";
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(1, 1);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(162, 54);
			panel1.TabIndex = 19;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(112, 19);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(42, 21);
			label1.TabIndex = 0;
			label1.Text = "帳號";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			panel2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel2.Controls.Add(label4);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Location = new System.Drawing.Point(1, 56);
			panel2.Margin = new System.Windows.Forms.Padding(0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(162, 54);
			panel2.TabIndex = 20;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.White;
			label4.Location = new System.Drawing.Point(112, 22);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(42, 21);
			label4.TabIndex = 0;
			label4.Text = "密碼";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label6);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 111);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(162, 54);
			panel3.TabIndex = 21;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(64, 24);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(90, 21);
			label6.TabIndex = 0;
			label6.Text = "密碼再確認";
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			panel6.Location = new System.Drawing.Point(1, 276);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(162, 54);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(80, 22);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(74, 21);
			label12.TabIndex = 0;
			label12.Text = "電子信箱";
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(label14);
			panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			panel7.Location = new System.Drawing.Point(1, 331);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(162, 54);
			panel7.TabIndex = 20;
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label14.ForeColor = System.Drawing.Color.White;
			label14.Location = new System.Drawing.Point(112, 23);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(42, 21);
			label14.TabIndex = 0;
			label14.Text = "狀態";
			panel8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel8.Controls.Add(label16);
			panel8.Dock = System.Windows.Forms.DockStyle.Fill;
			panel8.Location = new System.Drawing.Point(1, 386);
			panel8.Margin = new System.Windows.Forms.Padding(0);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(162, 54);
			panel8.TabIndex = 20;
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label16.ForeColor = System.Drawing.Color.White;
			label16.Location = new System.Drawing.Point(48, 23);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(106, 21);
			label16.TabIndex = 0;
			label16.Text = "建立日期時間";
			flowLayoutPanel3.Controls.Add(tb_password);
			flowLayoutPanel3.Controls.Add(label19);
			flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel3.Location = new System.Drawing.Point(164, 56);
			flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel3.Name = "flowLayoutPanel3";
			flowLayoutPanel3.Size = new System.Drawing.Size(792, 54);
			flowLayoutPanel3.TabIndex = 27;
			tb_password.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_password.Location = new System.Drawing.Point(15, 15);
			tb_password.Margin = new System.Windows.Forms.Padding(15);
			tb_password.Name = "tb_password";
			tb_password.PasswordChar = '*';
			tb_password.Size = new System.Drawing.Size(188, 33);
			tb_password.TabIndex = 1;
			label19.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label19.AutoSize = true;
			label19.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label19.ForeColor = System.Drawing.Color.Red;
			label19.Location = new System.Drawing.Point(221, 23);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(183, 17);
			label19.TabIndex = 3;
			label19.Text = "*若不修改密碼請保持欄位空值";
			tb_rePassword.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_rePassword.Location = new System.Drawing.Point(179, 126);
			tb_rePassword.Margin = new System.Windows.Forms.Padding(15);
			tb_rePassword.Name = "tb_rePassword";
			tb_rePassword.PasswordChar = '*';
			tb_rePassword.Size = new System.Drawing.Size(188, 33);
			tb_rePassword.TabIndex = 41;
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel1.Controls.Add(l_lastLogin, 1, 8);
			tableLayoutPanel1.Controls.Add(l_createDate, 1, 7);
			tableLayoutPanel1.Controls.Add(l_status, 1, 6);
			tableLayoutPanel1.Controls.Add(l_type, 1, 3);
			tableLayoutPanel1.Controls.Add(tb_name, 1, 4);
			tableLayoutPanel1.Controls.Add(tb_email, 1, 5);
			tableLayoutPanel1.Controls.Add(tb_rePassword, 1, 2);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 1, 1);
			tableLayoutPanel1.Controls.Add(panel8, 0, 7);
			tableLayoutPanel1.Controls.Add(panel7, 0, 6);
			tableLayoutPanel1.Controls.Add(panel6, 0, 5);
			tableLayoutPanel1.Controls.Add(panel3, 0, 2);
			tableLayoutPanel1.Controls.Add(panel2, 0, 1);
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(panel4, 0, 3);
			tableLayoutPanel1.Controls.Add(panel5, 0, 4);
			tableLayoutPanel1.Controls.Add(panel9, 0, 8);
			tableLayoutPanel1.Controls.Add(l_account, 1, 0);
			tableLayoutPanel1.Location = new System.Drawing.Point(12, 54);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 9;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111f));
			tableLayoutPanel1.Size = new System.Drawing.Size(957, 501);
			tableLayoutPanel1.TabIndex = 1;
			l_lastLogin.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_lastLogin.AutoSize = true;
			l_lastLogin.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_lastLogin.Location = new System.Drawing.Point(179, 460);
			l_lastLogin.Margin = new System.Windows.Forms.Padding(15);
			l_lastLogin.Name = "l_lastLogin";
			l_lastLogin.Size = new System.Drawing.Size(66, 21);
			l_lastLogin.TabIndex = 44;
			l_lastLogin.Text = "label11";
			l_createDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_createDate.AutoSize = true;
			l_createDate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_createDate.Location = new System.Drawing.Point(179, 402);
			l_createDate.Margin = new System.Windows.Forms.Padding(15);
			l_createDate.Name = "l_createDate";
			l_createDate.Size = new System.Drawing.Size(56, 21);
			l_createDate.TabIndex = 44;
			l_createDate.Text = "label9";
			l_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_status.AutoSize = true;
			l_status.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_status.Location = new System.Drawing.Point(179, 347);
			l_status.Margin = new System.Windows.Forms.Padding(15);
			l_status.Name = "l_status";
			l_status.Size = new System.Drawing.Size(56, 21);
			l_status.TabIndex = 44;
			l_status.Text = "label7";
			l_type.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_type.AutoSize = true;
			l_type.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_type.Location = new System.Drawing.Point(179, 182);
			l_type.Margin = new System.Windows.Forms.Padding(15);
			l_type.Name = "l_type";
			l_type.Size = new System.Drawing.Size(56, 21);
			l_type.TabIndex = 44;
			l_type.Text = "label5";
			tb_name.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_name.Location = new System.Drawing.Point(179, 236);
			tb_name.Margin = new System.Windows.Forms.Padding(15);
			tb_name.Name = "tb_name";
			tb_name.Size = new System.Drawing.Size(188, 33);
			tb_name.TabIndex = 42;
			tb_email.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_email.Location = new System.Drawing.Point(179, 291);
			tb_email.Margin = new System.Windows.Forms.Padding(15);
			tb_email.Name = "tb_email";
			tb_email.Size = new System.Drawing.Size(472, 33);
			tb_email.TabIndex = 42;
			panel9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel9.Controls.Add(label2);
			panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			panel9.Location = new System.Drawing.Point(1, 441);
			panel9.Margin = new System.Windows.Forms.Padding(0);
			panel9.Name = "panel9";
			panel9.Size = new System.Drawing.Size(162, 59);
			panel9.TabIndex = 42;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(48, 23);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(106, 21);
			label2.TabIndex = 0;
			label2.Text = "最近登入日期";
			l_account.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_account.AutoSize = true;
			l_account.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_account.Location = new System.Drawing.Point(179, 17);
			l_account.Margin = new System.Windows.Forms.Padding(15);
			l_account.Name = "l_account";
			l_account.Size = new System.Drawing.Size(56, 21);
			l_account.TabIndex = 43;
			l_account.Text = "label3";
			btn_save.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_save.FlatAppearance.BorderSize = 0;
			btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_save.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_save.ForeColor = System.Drawing.Color.White;
			btn_save.Location = new System.Drawing.Point(406, 576);
			btn_save.Name = "btn_save";
			btn_save.Size = new System.Drawing.Size(168, 38);
			btn_save.TabIndex = 33;
			btn_save.Text = "編修存檔";
			btn_save.UseVisualStyleBackColor = false;
			btn_save.Click += new System.EventHandler(btn_save_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(btn_save);
			base.Controls.Add(tableLayoutPanel1);
			base.Name = "frmProfileDetail";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "frm_profileDetail";
			base.Load += new System.EventHandler(frmProfileDetail_Load);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(tableLayoutPanel1, 0);
			base.Controls.SetChildIndex(btn_save, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			panel7.ResumeLayout(false);
			panel7.PerformLayout();
			panel8.ResumeLayout(false);
			panel8.PerformLayout();
			flowLayoutPanel3.ResumeLayout(false);
			flowLayoutPanel3.PerformLayout();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel9.ResumeLayout(false);
			panel9.PerformLayout();
			ResumeLayout(false);
		}
	}
}
