using KeyboardClassLibrary;
using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmEditUser : Form
	{
		private string _account;

		private IContainer components;

		private Button btn_cancel;

		private Button btn_save;

		private PictureBox pictureBox1;

		private Panel panel17;

		private Button btn_down;

		private Button btn_top;

		private PictureBox pictureBox2;

		private Keyboardcontrol keyboardcontrol1;

		private TextBox tb_Repassword;

		private TextBox tb_Name;

		private Panel panel2;

		private Label label2;

		private Panel panel1;

		private Label label1;

		private Panel panel5;

		private Label label6;

		private Label label10;

		private Panel panel3;

		private Panel panel6;

		private Label label12;

		private Panel panel4;

		private Label label3;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel13;

		private TextBox tb_email;

		private Panel panel7;

		private Label label4;

		private Label label5;

		private TextBox tb_password;

		private ComboBox cb_type;

		private ComboBox cb_status;

		private Label l_account;

		public frmEditUser(string account)
		{
			InitializeComponent();
			_account = account;
		}

		private void frmEditUser_Load(object sender, EventArgs e)
		{
			cb_status.Items.Add(new ComboboxItem("正常", 0));
			cb_status.Items.Add(new ComboboxItem("停用", 1));
			cb_status.SelectedIndex = 0;
			cb_type.Items.Add(new ComboboxItem("管理者", 0));
			cb_type.Items.Add(new ComboboxItem("使用者", 1));
			cb_type.SelectedIndex = 1;
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_User", "Account = {0}", "", null, new string[1]
			{
				_account
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				l_account.Text = dataTable.Rows[0]["Account"].ToString();
				tb_password.Text = dataTable.Rows[0]["Password"].ToString();
				tb_Repassword.Text = dataTable.Rows[0]["Password"].ToString();
				tb_Name.Text = dataTable.Rows[0]["Name"].ToString();
				tb_email.Text = dataTable.Rows[0]["EMail"].ToString();
				cb_status.SelectedIndex = int.Parse(dataTable.Rows[0]["Status"].ToString());
				cb_type.SelectedIndex = int.Parse(dataTable.Rows[0]["Type"].ToString());
				if ("001".Equals(l_account.Text))
				{
					cb_type.Enabled = false;
					cb_status.Enabled = false;
				}
			}
			else
			{
				AutoClosingMessageBox.Show("使用者錯誤!");
				Close();
			}
		}

		private void btn_save_Click(object sender, EventArgs e)
		{
			string text = "";
			if (!tb_Repassword.Text.Equals(tb_password.Text))
			{
				text += "請確認密碼\n";
			}
			if (!"".Equals(text))
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			string[,] strFieldArray = new string[5, 2]
			{
				{
					"Password",
					tb_password.Text
				},
				{
					"Name",
					tb_Name.Text
				},
				{
					"Email",
					tb_email.Text
				},
				{
					"Type",
					(cb_type.SelectedItem as ComboboxItem).Value.ToString()
				},
				{
					"Status",
					(cb_status.SelectedItem as ComboboxItem).Value.ToString()
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_User", "Account = {0}", "", strFieldArray, new string[1]
			{
				l_account.Text
			}, CommandOperationType.ExecuteNonQuery);
			TableLayoutPanel tlp_userManage = (base.Owner as frmSystemSetup).tlp_userManage;
			for (int i = 1; i < tlp_userManage.RowCount; i++)
			{
				Label label = (Label)tlp_userManage.GetControlFromPosition(1, i);
				if (l_account.Text.Equals(label.Text))
				{
					(tlp_userManage.GetControlFromPosition(2, i) as Label).Text = tb_Name.Text;
					(tlp_userManage.GetControlFromPosition(3, i) as Label).Text = (cb_type.SelectedItem as ComboboxItem).Text;
					(tlp_userManage.GetControlFromPosition(4, i) as Label).Text = (cb_status.SelectedItem as ComboboxItem).Text;
					break;
				}
			}
			AutoClosingMessageBox.Show("已變更");
			Close();
		}

		private void btn_KeyboardLocation_Click(object sender, EventArgs e)
		{
			if (panel17.Location.Y > 300)
			{
				panel17.Location = new Point(panel17.Location.X, 0);
			}
			else
			{
				panel17.Location = new Point(panel17.Location.X, 367);
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			panel17.BringToFront();
			if (panel17.Location.X > 900)
			{
				panel17.Location = new Point(0, panel17.Location.Y);
			}
			else
			{
				panel17.Location = new Point(997, panel17.Location.Y);
			}
		}

		private void keyboardcontrol1_UserKeyPressed(object sender, KeyboardEventArgs e)
		{
			SendKeys.Send(e.KeyboardKeyPressed);
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.No;
			Close();
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
			btn_cancel = new System.Windows.Forms.Button();
			btn_save = new System.Windows.Forms.Button();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			panel17 = new System.Windows.Forms.Panel();
			btn_down = new System.Windows.Forms.Button();
			btn_top = new System.Windows.Forms.Button();
			pictureBox2 = new System.Windows.Forms.PictureBox();
			keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
			tb_Repassword = new System.Windows.Forms.TextBox();
			tb_Name = new System.Windows.Forms.TextBox();
			panel2 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			panel13 = new System.Windows.Forms.Panel();
			l_account = new System.Windows.Forms.Label();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			tb_password = new System.Windows.Forms.TextBox();
			tb_email = new System.Windows.Forms.TextBox();
			panel7 = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			cb_type = new System.Windows.Forms.ComboBox();
			cb_status = new System.Windows.Forms.ComboBox();
			label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			panel17.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			panel2.SuspendLayout();
			panel1.SuspendLayout();
			panel5.SuspendLayout();
			panel3.SuspendLayout();
			panel6.SuspendLayout();
			panel4.SuspendLayout();
			panel13.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			panel7.SuspendLayout();
			SuspendLayout();
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(567, 555);
			btn_cancel.Name = "btn_cancel";
			btn_cancel.Size = new System.Drawing.Size(92, 40);
			btn_cancel.TabIndex = 0;
			btn_cancel.Text = "取消";
			btn_cancel.UseVisualStyleBackColor = false;
			btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
			btn_save.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_save.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_save.ForeColor = System.Drawing.Color.White;
			btn_save.Location = new System.Drawing.Point(359, 555);
			btn_save.Name = "btn_save";
			btn_save.Size = new System.Drawing.Size(134, 40);
			btn_save.TabIndex = 42;
			btn_save.Text = "儲存變更";
			btn_save.UseVisualStyleBackColor = false;
			btn_save.Click += new System.EventHandler(btn_save_Click);
			pictureBox1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom);
			pictureBox1.BackColor = System.Drawing.Color.Silver;
			pictureBox1.Image = POS_Client.Properties.Resources.keyboard;
			pictureBox1.Location = new System.Drawing.Point(874, 588);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(70, 46);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			pictureBox1.TabIndex = 52;
			pictureBox1.TabStop = false;
			pictureBox1.Click += new System.EventHandler(pictureBox1_Click);
			panel17.BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
			panel17.Controls.Add(btn_down);
			panel17.Controls.Add(btn_top);
			panel17.Controls.Add(pictureBox2);
			panel17.Controls.Add(keyboardcontrol1);
			panel17.Location = new System.Drawing.Point(953, 367);
			panel17.Margin = new System.Windows.Forms.Padding(0);
			panel17.Name = "panel17";
			panel17.Size = new System.Drawing.Size(949, 269);
			panel17.TabIndex = 53;
			btn_down.Location = new System.Drawing.Point(862, 112);
			btn_down.Name = "btn_down";
			btn_down.Size = new System.Drawing.Size(58, 40);
			btn_down.TabIndex = 52;
			btn_down.Text = "Down";
			btn_down.UseVisualStyleBackColor = true;
			btn_down.Click += new System.EventHandler(btn_KeyboardLocation_Click);
			btn_top.Location = new System.Drawing.Point(862, 55);
			btn_top.Name = "btn_top";
			btn_top.Size = new System.Drawing.Size(58, 40);
			btn_top.TabIndex = 51;
			btn_top.Text = "Top";
			btn_top.UseVisualStyleBackColor = true;
			btn_top.Click += new System.EventHandler(btn_KeyboardLocation_Click);
			pictureBox2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom);
			pictureBox2.BackColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
			pictureBox2.Image = POS_Client.Properties.Resources.keyboard_close;
			pictureBox2.Location = new System.Drawing.Point(842, 7);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new System.Drawing.Size(59, 34);
			pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			pictureBox2.TabIndex = 50;
			pictureBox2.TabStop = false;
			pictureBox2.Click += new System.EventHandler(pictureBox1_Click);
			keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard;
			keyboardcontrol1.Location = new System.Drawing.Point(12, 6);
			keyboardcontrol1.Name = "keyboardcontrol1";
			keyboardcontrol1.Size = new System.Drawing.Size(816, 260);
			keyboardcontrol1.TabIndex = 0;
			keyboardcontrol1.UserKeyPressed += new KeyboardClassLibrary.KeyboardDelegate(keyboardcontrol1_UserKeyPressed);
			tb_Repassword.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_Repassword.ForeColor = System.Drawing.Color.Black;
			tb_Repassword.Location = new System.Drawing.Point(185, 142);
			tb_Repassword.Margin = new System.Windows.Forms.Padding(15);
			tb_Repassword.Name = "tb_Repassword";
			tb_Repassword.PasswordChar = '*';
			tb_Repassword.Size = new System.Drawing.Size(187, 29);
			tb_Repassword.TabIndex = 51;
			tb_Name.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_Name.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_Name.ForeColor = System.Drawing.Color.Black;
			tb_Name.Location = new System.Drawing.Point(185, 206);
			tb_Name.Margin = new System.Windows.Forms.Padding(15);
			tb_Name.Name = "tb_Name";
			tb_Name.Size = new System.Drawing.Size(186, 29);
			tb_Name.TabIndex = 56;
			panel2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel2.Controls.Add(label2);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel2.ForeColor = System.Drawing.Color.White;
			panel2.Location = new System.Drawing.Point(1, 253);
			panel2.Margin = new System.Windows.Forms.Padding(0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(168, 62);
			panel2.TabIndex = 44;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(102, 19);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(58, 21);
			label2.TabIndex = 0;
			label2.Text = "身分別";
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel1.ForeColor = System.Drawing.Color.White;
			panel1.Location = new System.Drawing.Point(1, 190);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(168, 62);
			panel1.TabIndex = 43;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(70, 22);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(90, 21);
			label1.TabIndex = 0;
			label1.Text = "使用者姓名";
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label6);
			panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			panel5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel5.ForeColor = System.Drawing.Color.White;
			panel5.Location = new System.Drawing.Point(1, 64);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(168, 62);
			panel5.TabIndex = 23;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(86, 20);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(74, 21);
			label6.TabIndex = 0;
			label6.Text = "登入密碼";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(118, 21);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(42, 21);
			label10.TabIndex = 0;
			label10.Text = "帳號";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label10);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel3.ForeColor = System.Drawing.Color.White;
			panel3.Location = new System.Drawing.Point(1, 1);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(168, 62);
			panel3.TabIndex = 21;
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			panel6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel6.ForeColor = System.Drawing.Color.White;
			panel6.Location = new System.Drawing.Point(1, 127);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(168, 62);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(70, 22);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(90, 21);
			label12.TabIndex = 0;
			label12.Text = "密碼再確認";
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label3);
			panel4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel4.ForeColor = System.Drawing.Color.White;
			panel4.Location = new System.Drawing.Point(1, 316);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(168, 62);
			panel4.TabIndex = 45;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.White;
			label3.Location = new System.Drawing.Point(86, 20);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(74, 21);
			label3.TabIndex = 0;
			label3.Text = "電子信箱";
			panel13.Controls.Add(l_account);
			panel13.Dock = System.Windows.Forms.DockStyle.Fill;
			panel13.Location = new System.Drawing.Point(170, 1);
			panel13.Margin = new System.Windows.Forms.Padding(0);
			panel13.Name = "panel13";
			panel13.Size = new System.Drawing.Size(676, 62);
			panel13.TabIndex = 54;
			l_account.AutoSize = true;
			l_account.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_account.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_account.Location = new System.Drawing.Point(16, 21);
			l_account.Name = "l_account";
			l_account.Size = new System.Drawing.Size(0, 24);
			l_account.TabIndex = 54;
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80f));
			tableLayoutPanel1.Controls.Add(tb_password, 1, 1);
			tableLayoutPanel1.Controls.Add(panel13, 1, 0);
			tableLayoutPanel1.Controls.Add(panel4, 0, 5);
			tableLayoutPanel1.Controls.Add(panel6, 0, 2);
			tableLayoutPanel1.Controls.Add(panel3, 0, 0);
			tableLayoutPanel1.Controls.Add(panel5, 0, 1);
			tableLayoutPanel1.Controls.Add(panel1, 0, 3);
			tableLayoutPanel1.Controls.Add(panel2, 0, 4);
			tableLayoutPanel1.Controls.Add(tb_Name, 1, 3);
			tableLayoutPanel1.Controls.Add(tb_Repassword, 1, 2);
			tableLayoutPanel1.Controls.Add(tb_email, 1, 5);
			tableLayoutPanel1.Controls.Add(panel7, 0, 6);
			tableLayoutPanel1.Controls.Add(cb_type, 1, 4);
			tableLayoutPanel1.Controls.Add(cb_status, 1, 6);
			tableLayoutPanel1.Location = new System.Drawing.Point(50, 77);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 7;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572f));
			tableLayoutPanel1.Size = new System.Drawing.Size(847, 442);
			tableLayoutPanel1.TabIndex = 41;
			tb_password.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_password.ForeColor = System.Drawing.Color.Black;
			tb_password.Location = new System.Drawing.Point(185, 79);
			tb_password.Margin = new System.Windows.Forms.Padding(15);
			tb_password.Name = "tb_password";
			tb_password.PasswordChar = '*';
			tb_password.Size = new System.Drawing.Size(187, 29);
			tb_password.TabIndex = 52;
			tb_email.Anchor = System.Windows.Forms.AnchorStyles.None;
			tb_email.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_email.Location = new System.Drawing.Point(185, 332);
			tb_email.Margin = new System.Windows.Forms.Padding(0);
			tb_email.Name = "tb_email";
			tb_email.Size = new System.Drawing.Size(645, 29);
			tb_email.TabIndex = 57;
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(label4);
			panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			panel7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel7.ForeColor = System.Drawing.Color.White;
			panel7.Location = new System.Drawing.Point(1, 379);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(168, 62);
			panel7.TabIndex = 45;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.White;
			label4.Location = new System.Drawing.Point(118, 23);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(42, 21);
			label4.TabIndex = 0;
			label4.Text = "狀態";
			cb_type.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_type.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_type.FormattingEnabled = true;
			cb_type.Location = new System.Drawing.Point(190, 268);
			cb_type.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			cb_type.Name = "cb_type";
			cb_type.Size = new System.Drawing.Size(189, 32);
			cb_type.TabIndex = 59;
			cb_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_status.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_status.FormattingEnabled = true;
			cb_status.Location = new System.Drawing.Point(190, 394);
			cb_status.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			cb_status.Name = "cb_status";
			cb_status.Size = new System.Drawing.Size(189, 32);
			cb_status.TabIndex = 58;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label5.Image = POS_Client.Properties.Resources.oblique;
			label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label5.Location = new System.Drawing.Point(414, 32);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(120, 24);
			label5.TabIndex = 54;
			label5.Text = "   使用者編修";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Silver;
			base.ClientSize = new System.Drawing.Size(949, 636);
			base.Controls.Add(label5);
			base.Controls.Add(panel17);
			base.Controls.Add(pictureBox1);
			base.Controls.Add(btn_save);
			base.Controls.Add(tableLayoutPanel1);
			base.Controls.Add(btn_cancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmEditUser";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "選擇會員 / 會員編修";
			base.Load += new System.EventHandler(frmEditUser_Load);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			panel17.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel13.ResumeLayout(false);
			panel13.PerformLayout();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel7.ResumeLayout(false);
			panel7.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
