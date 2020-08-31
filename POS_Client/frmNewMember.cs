using POS_Client.WebService;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmNewMember : MasterThinForm
	{
		private bool hasCallback;

		private string callbackForm = "";

		private IContainer components;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel8;

		private Label label16;

		private Panel panel7;

		private Label label14;

		private Panel panel6;

		private Label label12;

		private Panel panel3;

		private Label label6;

		private Panel panel2;

		private Label label3;

		private Label label4;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private Panel panel4;

		private Label label8;

		private Panel panel5;

		private Label label9;

		private Label label10;

		private Panel panel11;

		private Label label15;

		private Panel panel9;

		private Label label5;

		private Label label7;

		private Panel panel10;

		private Label label11;

		private Label label13;

		private Panel panel12;

		private Label label17;

		private Panel panel13;

		private Label label18;

		private ComboBox cb_status;

		private TextBox tb_fax;

		private TextBox tb_companyTel;

		private TextBox tb_companyIdno;

		private TextBox tb_companyName;

		private TextBox tb_email;

		private TextBox tb_name;

		private FlowLayoutPanel flowLayoutPanel4;

		private Label label20;

		private TextBox tb_tel;

		private Label label22;

		private TextBox tb_mobile;

		private FlowLayoutPanel flowLayoutPanel1;

		private TextBox tb_licenseCode;

		private TextBox tb_vipNo;

		private FlowLayoutPanel flowLayoutPanel3;

		private TextBox tb_idno;

		private Button btn_LowIncome;

		private Label l_LowIncome;

		private ComboBox cb_type;

		private DateTimePicker dt_birthDate;

		private Button btn_cancel;

		private Button btn_save;

		private Button btn_back;

		private Panel panel14;

		private TextBox tb_zipcode;

		private ComboBox cb_area;

		private ComboBox cb_city;

		private TextBox tb_addr;

		private Panel panel15;

		private Label label21;

		private Label label23;

		public frmNewMember()
			: base("會員管理")
		{
			InitializeComponent();
		}

		public frmNewMember(string callbackForm)
			: base("會員管理")
		{
			InitializeComponent();
			btn_save.Focus();
			hasCallback = true;
			this.callbackForm = callbackForm;
		}

		private void frmNewMember_Load(object sender, EventArgs e)
		{
			dt_birthDate.Text = DateTime.Now.AddYears(-20).ToString("yyyy-MM-dd");
			if (Program.SystemMode == 1)
			{
				btn_LowIncome.Visible = false;
				l_LowIncome.Visible = false;
			}
			DataTable dataSource = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "ADDRCITY", "", "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			cb_city.DisplayMember = "city";
			cb_city.ValueMember = "cityno";
			cb_city.DataSource = dataSource;
			cb_status.Items.Add(new ComboboxItem("正常", "0"));
			cb_status.Items.Add(new ComboboxItem("停用", "1"));
			cb_status.SelectedIndex = 0;
			cb_type.Items.Add(new ComboboxItem("一般會員", "1"));
			cb_type.Items.Add(new ComboboxItem("優惠會員(1)", "2"));
			cb_type.Items.Add(new ComboboxItem("優惠會員(2)", "3"));
			cb_type.SelectedIndex = 0;
			tb_licenseCode.Text = Program.LincenseCode;
			tb_vipNo.Text = getNewVipNo();
		}

		public static string getNewVipNo()
		{
			string sql = "SELECT VipNo FROM hypos_CUST_RTL order by VipNo desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			string text2 = DateTime.Now.Year.ToString().Substring(2, 2);
			if ("-1".Equals(text))
			{
				return string.Format("M{0}{1}00001", Program.SiteNo.PadLeft(2, '0'), text2);
			}
			string value = text.Substring(3, 2);
			if (!text2.Equals(value))
			{
				return string.Format("M{0}{1}00001", Program.SiteNo, text2);
			}
			string arg = string.Format("{0:00000}", int.Parse(text.Substring(5, 5)) + 1);
			return string.Format("M{0}{1}{2}", Program.SiteNo, text2, arg);
		}

		private void btn_back_Click(object sender, EventArgs e)
		{
			backToPreviousForm();
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			backToPreviousForm();
		}

		private void btn_save_Click(object sender, EventArgs e)
		{
			if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "IdNo", "hypos_CUST_RTL", "IdNo ={0}", "", null, new string[1]
			{
				tb_idno.Text.Trim()
			}, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !"".Equals(tb_idno.Text.Trim()))
			{
				AutoClosingMessageBox.Show("此身分證已使用");
				return;
			}
			string text = "";
			if (!dt_birthDate.Checked)
			{
				text += "請輸入出生日期\n";
			}
			if (string.IsNullOrEmpty(tb_name.Text))
			{
				text += "請輸入會員姓名\n";
			}
			if (string.IsNullOrEmpty(tb_addr.Text))
			{
				text += "請輸入地址\n";
			}
			if (Program.IsDeployClickOnce && !string.IsNullOrEmpty(text))
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			string[,] strFieldArray = new string[17, 2]
			{
				{
					"LicenseCode",
					tb_licenseCode.Text
				},
				{
					"VipNo",
					tb_vipNo.Text
				},
				{
					"IdNo",
					tb_idno.Text
				},
				{
					"Name",
					tb_name.Text
				},
				{
					"BirthDate",
					dt_birthDate.Text
				},
				{
					"Telphone",
					tb_tel.Text
				},
				{
					"Mobile",
					tb_mobile.Text
				},
				{
					"Type",
					(cb_type.SelectedItem as ComboboxItem).Value.ToString()
				},
				{
					"City",
					cb_city.SelectedValue.ToString()
				},
				{
					"Area",
					cb_area.SelectedValue.ToString()
				},
				{
					"Address",
					tb_addr.Text
				},
				{
					"EMail",
					tb_email.Text
				},
				{
					"CompanyIdNo",
					tb_companyIdno.Text
				},
				{
					"CompanyName",
					tb_companyName.Text
				},
				{
					"CompanyTel",
					tb_companyTel.Text
				},
				{
					"Status",
					(cb_status.SelectedItem as ComboboxItem).Value.ToString()
				},
				{
					"Fax",
					tb_fax.Text
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_CUST_RTL", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			AutoClosingMessageBox.Show("會員新增完成");
			if (hasCallback)
			{
				if (callbackForm == "frmMainShopSimple")
				{
					switchForm(new frmMainShopSimple(tb_vipNo.Text));
					return;
				}
				if (callbackForm == "frmMainShopSimpleWithMoney")
				{
					switchForm(new frmMainShopSimpleWithMoney(tb_vipNo.Text));
					return;
				}
				object obj = Assembly.GetExecutingAssembly().CreateInstance("POS_Client." + callbackForm);
				Type type = obj.GetType();
				if ("MasterForm".Equals(type.BaseType.Name))
				{
					switchForm((MasterForm)obj);
				}
				else if ("MasterThinForm".Equals(type.BaseType.Name))
				{
					switchForm((MasterThinForm)obj);
				}
			}
			else
			{
				backToPreviousForm();
			}
		}

		private void cb_city_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cb_city.SelectedValue != null)
			{
				cb_area.DataSource = null;
				DataTable dataSource = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "ADDRAREA", "cityno = {0}", "", null, new string[1]
				{
					cb_city.SelectedValue.ToString()
				}, CommandOperationType.ExecuteReaderReturnDataTable);
				cb_area.DisplayMember = "area";
				cb_area.ValueMember = "zipcode";
				cb_area.DataSource = dataSource;
			}
		}

		private void cb_area_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cb_area.SelectedValue != null)
			{
				tb_zipcode.Text = cb_area.SelectedValue.ToString();
			}
		}

		private void btn_LowIncome_Click(object sender, EventArgs e)
		{
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string text2 = new UploadVerification().farmerInfo(tb_idno.Text);
			if (tb_idno.Text.Equals(""))
			{
				AutoClosingMessageBox.Show("請輸入身分證字號");
				return;
			}
			if (tb_idno.Text.Length != 10 || !Verification.checkIDNo(tb_idno.Text))
			{
				AutoClosingMessageBox.Show("身分證格式錯誤，請檢查輸入值");
				return;
			}
			string[] strParameterArray = new string[4]
			{
				tb_name.Text,
				tb_idno.Text,
				text,
				tb_vipNo.Text
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_CUST_RTL ", null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (text2.Equals("符合補助資格"))
			{
				if (dataTable.Rows.Count > 0)
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Verification = 'Y', IdNo = {1}, LastVerificationTime = {2} WHERE VipNo = {3}", strParameterArray, CommandOperationType.ExecuteNonQuery);
				}
				else
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_CUST_RTL ( Name, IdNo, Verification, LastVerificationTime) VALUES( {0}, {1}, 'Y', {2}) ", strParameterArray, CommandOperationType.ExecuteNonQuery);
				}
				AutoClosingMessageBox.Show("驗證成功。");
			}
			else if (text2.Equals("購肥帳號密碼驗證錯誤"))
			{
				AutoClosingMessageBox.Show("帳號密碼有誤，請重新確認您的帳號密碼。");
				if (dataTable.Rows.Count > 0)
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Verification = 'N', IdNo = {1}, LastVerificationTime = {2} WHERE VipNo = {3} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
				}
				else
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_CUST_RTL ( Name, IdNo, Verification, LastVerificationTime) VALUES( {0}, {1}, 'N', {2}) ", strParameterArray, CommandOperationType.ExecuteNonQuery);
				}
			}
			else if (text2.Equals("不符合補助資格"))
			{
				AutoClosingMessageBox.Show("不符合補助資格");
				if (dataTable.Rows.Count > 0)
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Verification = 'N', IdNo = {1}, LastVerificationTime = {2} WHERE VipNo = {3} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
				}
				else
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_CUST_RTL ( Name, IdNo, Verification, LastVerificationTime) VALUES( {0}, {1}, 'N', {2}) ", strParameterArray, CommandOperationType.ExecuteNonQuery);
				}
			}
		}

		private void tb_idno_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b') && !char.IsUpper(e.KeyChar));
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
			tabControl1 = new System.Windows.Forms.TabControl();
			tabPage1 = new System.Windows.Forms.TabPage();
			btn_cancel = new System.Windows.Forms.Button();
			btn_save = new System.Windows.Forms.Button();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			cb_status = new System.Windows.Forms.ComboBox();
			tb_fax = new System.Windows.Forms.TextBox();
			tb_companyTel = new System.Windows.Forms.TextBox();
			tb_companyIdno = new System.Windows.Forms.TextBox();
			tb_companyName = new System.Windows.Forms.TextBox();
			tb_email = new System.Windows.Forms.TextBox();
			tb_name = new System.Windows.Forms.TextBox();
			flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			label20 = new System.Windows.Forms.Label();
			tb_tel = new System.Windows.Forms.TextBox();
			label22 = new System.Windows.Forms.Label();
			tb_mobile = new System.Windows.Forms.TextBox();
			panel11 = new System.Windows.Forms.Panel();
			label15 = new System.Windows.Forms.Label();
			panel9 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			panel8 = new System.Windows.Forms.Panel();
			label16 = new System.Windows.Forms.Label();
			panel7 = new System.Windows.Forms.Panel();
			label14 = new System.Windows.Forms.Label();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			panel10 = new System.Windows.Forms.Panel();
			label11 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			panel12 = new System.Windows.Forms.Panel();
			label17 = new System.Windows.Forms.Label();
			panel13 = new System.Windows.Forms.Panel();
			label18 = new System.Windows.Forms.Label();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			tb_licenseCode = new System.Windows.Forms.TextBox();
			tb_vipNo = new System.Windows.Forms.TextBox();
			flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			tb_idno = new System.Windows.Forms.TextBox();
			btn_LowIncome = new System.Windows.Forms.Button();
			l_LowIncome = new System.Windows.Forms.Label();
			dt_birthDate = new System.Windows.Forms.DateTimePicker();
			panel14 = new System.Windows.Forms.Panel();
			tb_zipcode = new System.Windows.Forms.TextBox();
			cb_area = new System.Windows.Forms.ComboBox();
			cb_city = new System.Windows.Forms.ComboBox();
			tb_addr = new System.Windows.Forms.TextBox();
			panel15 = new System.Windows.Forms.Panel();
			label21 = new System.Windows.Forms.Label();
			label23 = new System.Windows.Forms.Label();
			cb_type = new System.Windows.Forms.ComboBox();
			panel2 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			btn_back = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			tabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			flowLayoutPanel4.SuspendLayout();
			panel11.SuspendLayout();
			panel9.SuspendLayout();
			panel8.SuspendLayout();
			panel7.SuspendLayout();
			panel6.SuspendLayout();
			panel3.SuspendLayout();
			panel1.SuspendLayout();
			panel4.SuspendLayout();
			panel5.SuspendLayout();
			panel10.SuspendLayout();
			panel12.SuspendLayout();
			panel13.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			flowLayoutPanel3.SuspendLayout();
			panel14.SuspendLayout();
			panel15.SuspendLayout();
			panel2.SuspendLayout();
			SuspendLayout();
			tabControl1.Controls.Add(tabPage1);
			tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			tabControl1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tabControl1.Location = new System.Drawing.Point(0, 38);
			tabControl1.Multiline = true;
			tabControl1.Name = "tabControl1";
			tabControl1.Padding = new System.Drawing.Point(15, 10);
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new System.Drawing.Size(981, 623);
			tabControl1.TabIndex = 34;
			tabPage1.Controls.Add(btn_cancel);
			tabPage1.Controls.Add(btn_save);
			tabPage1.Controls.Add(tableLayoutPanel1);
			tabPage1.Location = new System.Drawing.Point(4, 47);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new System.Windows.Forms.Padding(3);
			tabPage1.Size = new System.Drawing.Size(973, 572);
			tabPage1.TabIndex = 10;
			tabPage1.Text = "基本資料";
			tabPage1.UseVisualStyleBackColor = true;
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(516, 520);
			btn_cancel.Name = "btn_cancel";
			btn_cancel.Size = new System.Drawing.Size(70, 32);
			btn_cancel.TabIndex = 2;
			btn_cancel.Text = "取消";
			btn_cancel.UseVisualStyleBackColor = false;
			btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
			btn_save.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_save.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_save.ForeColor = System.Drawing.Color.White;
			btn_save.Location = new System.Drawing.Point(387, 520);
			btn_save.Name = "btn_save";
			btn_save.Size = new System.Drawing.Size(103, 32);
			btn_save.TabIndex = 0;
			btn_save.Text = "新建存檔";
			btn_save.UseVisualStyleBackColor = false;
			btn_save.Click += new System.EventHandler(btn_save_Click);
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 4;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.Controls.Add(cb_status, 3, 5);
			tableLayoutPanel1.Controls.Add(tb_fax, 3, 7);
			tableLayoutPanel1.Controls.Add(tb_companyTel, 1, 7);
			tableLayoutPanel1.Controls.Add(tb_companyIdno, 3, 6);
			tableLayoutPanel1.Controls.Add(tb_companyName, 1, 6);
			tableLayoutPanel1.Controls.Add(tb_email, 1, 5);
			tableLayoutPanel1.Controls.Add(tb_name, 1, 1);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel4, 1, 3);
			tableLayoutPanel1.Controls.Add(panel11, 2, 5);
			tableLayoutPanel1.Controls.Add(panel9, 2, 0);
			tableLayoutPanel1.Controls.Add(panel8, 0, 7);
			tableLayoutPanel1.Controls.Add(panel7, 0, 6);
			tableLayoutPanel1.Controls.Add(panel6, 0, 5);
			tableLayoutPanel1.Controls.Add(panel3, 0, 2);
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(panel4, 0, 3);
			tableLayoutPanel1.Controls.Add(panel5, 0, 4);
			tableLayoutPanel1.Controls.Add(panel10, 2, 1);
			tableLayoutPanel1.Controls.Add(panel12, 2, 6);
			tableLayoutPanel1.Controls.Add(panel13, 2, 7);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 1, 2);
			tableLayoutPanel1.Controls.Add(dt_birthDate, 3, 1);
			tableLayoutPanel1.Controls.Add(panel14, 1, 4);
			tableLayoutPanel1.Controls.Add(panel15, 0, 1);
			tableLayoutPanel1.Controls.Add(cb_type, 3, 0);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 8;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23292f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.36951f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.Size = new System.Drawing.Size(967, 503);
			tableLayoutPanel1.TabIndex = 90;
			cb_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_status.FormattingEnabled = true;
			cb_status.Location = new System.Drawing.Point(657, 348);
			cb_status.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			cb_status.Name = "cb_status";
			cb_status.Size = new System.Drawing.Size(299, 32);
			cb_status.TabIndex = 11;
			tb_fax.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_fax.Location = new System.Drawing.Point(657, 456);
			tb_fax.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_fax.Name = "tb_fax";
			tb_fax.Size = new System.Drawing.Size(299, 33);
			tb_fax.TabIndex = 15;
			tb_companyTel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_companyTel.Location = new System.Drawing.Point(174, 456);
			tb_companyTel.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_companyTel.Name = "tb_companyTel";
			tb_companyTel.Size = new System.Drawing.Size(299, 33);
			tb_companyTel.TabIndex = 14;
			tb_companyIdno.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_companyIdno.Location = new System.Drawing.Point(657, 398);
			tb_companyIdno.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_companyIdno.Name = "tb_companyIdno";
			tb_companyIdno.Size = new System.Drawing.Size(299, 33);
			tb_companyIdno.TabIndex = 13;
			tb_companyName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_companyName.Location = new System.Drawing.Point(174, 398);
			tb_companyName.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_companyName.Name = "tb_companyName";
			tb_companyName.Size = new System.Drawing.Size(299, 33);
			tb_companyName.TabIndex = 12;
			tb_email.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_email.Location = new System.Drawing.Point(174, 342);
			tb_email.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_email.Name = "tb_email";
			tb_email.Size = new System.Drawing.Size(299, 33);
			tb_email.TabIndex = 10;
			tb_name.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_name.Location = new System.Drawing.Point(174, 68);
			tb_name.Margin = new System.Windows.Forms.Padding(10);
			tb_name.Name = "tb_name";
			tb_name.Size = new System.Drawing.Size(299, 33);
			tb_name.TabIndex = 2;
			tableLayoutPanel1.SetColumnSpan(flowLayoutPanel4, 3);
			flowLayoutPanel4.Controls.Add(label20);
			flowLayoutPanel4.Controls.Add(tb_tel);
			flowLayoutPanel4.Controls.Add(label22);
			flowLayoutPanel4.Controls.Add(tb_mobile);
			flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel4.Location = new System.Drawing.Point(164, 169);
			flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel4.Name = "flowLayoutPanel4";
			flowLayoutPanel4.Size = new System.Drawing.Size(802, 55);
			flowLayoutPanel4.TabIndex = 28;
			label20.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label20.AutoSize = true;
			label20.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label20.ForeColor = System.Drawing.Color.Black;
			label20.Location = new System.Drawing.Point(10, 16);
			label20.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(73, 20);
			label20.TabIndex = 3;
			label20.Text = "室內電話";
			tb_tel.Location = new System.Drawing.Point(96, 10);
			tb_tel.Margin = new System.Windows.Forms.Padding(10);
			tb_tel.Name = "tb_tel";
			tb_tel.Size = new System.Drawing.Size(211, 33);
			tb_tel.TabIndex = 5;
			label22.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label22.AutoSize = true;
			label22.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label22.ForeColor = System.Drawing.Color.Black;
			label22.Location = new System.Drawing.Point(320, 16);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(73, 20);
			label22.TabIndex = 5;
			label22.Text = "行動電話";
			tb_mobile.Location = new System.Drawing.Point(406, 10);
			tb_mobile.Margin = new System.Windows.Forms.Padding(10);
			tb_mobile.Name = "tb_mobile";
			tb_mobile.Size = new System.Drawing.Size(211, 33);
			tb_mobile.TabIndex = 6;
			panel11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel11.Controls.Add(label15);
			panel11.Dock = System.Windows.Forms.DockStyle.Fill;
			panel11.Location = new System.Drawing.Point(484, 331);
			panel11.Margin = new System.Windows.Forms.Padding(0);
			panel11.Name = "panel11";
			panel11.Size = new System.Drawing.Size(162, 55);
			panel11.TabIndex = 23;
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label15.ForeColor = System.Drawing.Color.White;
			label15.Location = new System.Drawing.Point(111, 22);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(42, 21);
			label15.TabIndex = 10;
			label15.Text = "狀態";
			panel9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel9.Controls.Add(label5);
			panel9.Controls.Add(label7);
			panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			panel9.Location = new System.Drawing.Point(484, 1);
			panel9.Margin = new System.Windows.Forms.Padding(0);
			panel9.Name = "panel9";
			panel9.Size = new System.Drawing.Size(162, 55);
			panel9.TabIndex = 20;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Red;
			label5.Location = new System.Drawing.Point(67, 16);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(17, 21);
			label5.TabIndex = 1;
			label5.Text = "*";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(82, 16);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(74, 21);
			label7.TabIndex = 10;
			label7.Text = "會員類型";
			panel8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel8.Controls.Add(label16);
			panel8.Dock = System.Windows.Forms.DockStyle.Fill;
			panel8.Location = new System.Drawing.Point(1, 443);
			panel8.Margin = new System.Windows.Forms.Padding(0);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(162, 59);
			panel8.TabIndex = 20;
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label16.ForeColor = System.Drawing.Color.White;
			label16.Location = new System.Drawing.Point(83, 23);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(74, 21);
			label16.TabIndex = 10;
			label16.Text = "公司電話";
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(label14);
			panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			panel7.Location = new System.Drawing.Point(1, 387);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(162, 55);
			panel7.TabIndex = 20;
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label14.ForeColor = System.Drawing.Color.White;
			label14.Location = new System.Drawing.Point(80, 23);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(74, 21);
			label14.TabIndex = 10;
			label14.Text = "公司名稱";
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			panel6.Location = new System.Drawing.Point(1, 331);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(162, 55);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(83, 22);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(74, 21);
			label12.TabIndex = 10;
			label12.Text = "電子信箱";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label6);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 113);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(162, 55);
			panel3.TabIndex = 21;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(64, 24);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(90, 21);
			label6.TabIndex = 10;
			label6.Text = "身分證字號";
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(1, 1);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(162, 55);
			panel1.TabIndex = 19;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.Red;
			label2.Location = new System.Drawing.Point(35, 18);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(17, 21);
			label2.TabIndex = 1;
			label2.Text = "*";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(48, 18);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(105, 21);
			label1.TabIndex = 90;
			label1.Text = "門市 / 會員號";
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label8);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Location = new System.Drawing.Point(1, 169);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(162, 55);
			panel4.TabIndex = 22;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.White;
			label8.Location = new System.Drawing.Point(80, 22);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(74, 21);
			label8.TabIndex = 100;
			label8.Text = "連絡電話";
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label9);
			panel5.Controls.Add(label10);
			panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			panel5.Location = new System.Drawing.Point(1, 225);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(162, 105);
			panel5.TabIndex = 23;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label9.ForeColor = System.Drawing.Color.Red;
			label9.Location = new System.Drawing.Point(99, 48);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(17, 21);
			label9.TabIndex = 1;
			label9.Text = "*";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(112, 48);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(42, 21);
			label10.TabIndex = 100;
			label10.Text = "地址";
			panel10.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel10.Controls.Add(label11);
			panel10.Controls.Add(label13);
			panel10.Dock = System.Windows.Forms.DockStyle.Fill;
			panel10.Location = new System.Drawing.Point(484, 57);
			panel10.Margin = new System.Windows.Forms.Padding(0);
			panel10.Name = "panel10";
			panel10.Size = new System.Drawing.Size(162, 55);
			panel10.TabIndex = 24;
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label11.ForeColor = System.Drawing.Color.Red;
			label11.Location = new System.Drawing.Point(49, 23);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(17, 21);
			label11.TabIndex = 1;
			label11.Text = "*";
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.White;
			label13.Location = new System.Drawing.Point(63, 23);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(90, 21);
			label13.TabIndex = 90;
			label13.Text = "出生年月日";
			panel12.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel12.Controls.Add(label17);
			panel12.Dock = System.Windows.Forms.DockStyle.Fill;
			panel12.Location = new System.Drawing.Point(484, 387);
			panel12.Margin = new System.Windows.Forms.Padding(0);
			panel12.Name = "panel12";
			panel12.Size = new System.Drawing.Size(162, 55);
			panel12.TabIndex = 24;
			label17.AutoSize = true;
			label17.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label17.ForeColor = System.Drawing.Color.White;
			label17.Location = new System.Drawing.Point(79, 23);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(74, 21);
			label17.TabIndex = 90;
			label17.Text = "統一編號";
			panel13.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel13.Controls.Add(label18);
			panel13.Dock = System.Windows.Forms.DockStyle.Fill;
			panel13.Location = new System.Drawing.Point(484, 443);
			panel13.Margin = new System.Windows.Forms.Padding(0);
			panel13.Name = "panel13";
			panel13.Size = new System.Drawing.Size(162, 59);
			panel13.TabIndex = 24;
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label18.ForeColor = System.Drawing.Color.White;
			label18.Location = new System.Drawing.Point(79, 22);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(74, 21);
			label18.TabIndex = 90;
			label18.Text = "傳真機號";
			flowLayoutPanel1.Controls.Add(tb_licenseCode);
			flowLayoutPanel1.Controls.Add(tb_vipNo);
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel1.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(319, 55);
			flowLayoutPanel1.TabIndex = 25;
			tb_licenseCode.Cursor = System.Windows.Forms.Cursors.No;
			tb_licenseCode.Enabled = false;
			tb_licenseCode.Location = new System.Drawing.Point(10, 10);
			tb_licenseCode.Margin = new System.Windows.Forms.Padding(10, 10, 3, 10);
			tb_licenseCode.Name = "tb_licenseCode";
			tb_licenseCode.ReadOnly = true;
			tb_licenseCode.Size = new System.Drawing.Size(109, 33);
			tb_licenseCode.TabIndex = 90;
			tb_vipNo.Cursor = System.Windows.Forms.Cursors.No;
			tb_vipNo.Enabled = false;
			tb_vipNo.Location = new System.Drawing.Point(132, 10);
			tb_vipNo.Margin = new System.Windows.Forms.Padding(10);
			tb_vipNo.Name = "tb_vipNo";
			tb_vipNo.ReadOnly = true;
			tb_vipNo.Size = new System.Drawing.Size(175, 33);
			tb_vipNo.TabIndex = 1;
			tableLayoutPanel1.SetColumnSpan(flowLayoutPanel3, 3);
			flowLayoutPanel3.Controls.Add(tb_idno);
			flowLayoutPanel3.Controls.Add(btn_LowIncome);
			flowLayoutPanel3.Controls.Add(l_LowIncome);
			flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel3.Location = new System.Drawing.Point(164, 113);
			flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel3.Name = "flowLayoutPanel3";
			flowLayoutPanel3.Size = new System.Drawing.Size(802, 55);
			flowLayoutPanel3.TabIndex = 27;
			tb_idno.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_idno.Location = new System.Drawing.Point(10, 10);
			tb_idno.Margin = new System.Windows.Forms.Padding(10);
			tb_idno.MaxLength = 10;
			tb_idno.Name = "tb_idno";
			tb_idno.Size = new System.Drawing.Size(299, 33);
			tb_idno.TabIndex = 4;
			tb_idno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tb_idno_KeyPress);
			btn_LowIncome.Anchor = System.Windows.Forms.AnchorStyles.Left;
			btn_LowIncome.BackColor = System.Drawing.Color.FromArgb(255, 203, 24);
			btn_LowIncome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_LowIncome.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_LowIncome.ForeColor = System.Drawing.Color.White;
			btn_LowIncome.Location = new System.Drawing.Point(322, 9);
			btn_LowIncome.Name = "btn_LowIncome";
			btn_LowIncome.Size = new System.Drawing.Size(96, 35);
			btn_LowIncome.TabIndex = 2;
			btn_LowIncome.Text = "檢查輔助";
			btn_LowIncome.UseVisualStyleBackColor = false;
			btn_LowIncome.Click += new System.EventHandler(btn_LowIncome_Click);
			l_LowIncome.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_LowIncome.AutoSize = true;
			l_LowIncome.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_LowIncome.ForeColor = System.Drawing.Color.Red;
			l_LowIncome.Location = new System.Drawing.Point(424, 18);
			l_LowIncome.Name = "l_LowIncome";
			l_LowIncome.Size = new System.Drawing.Size(242, 17);
			l_LowIncome.TabIndex = 3;
			l_LowIncome.Text = "若需驗證補助身分請務必填寫身分證字號";
			dt_birthDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dt_birthDate.CustomFormat = "yyyy-MM-dd";
			dt_birthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dt_birthDate.Location = new System.Drawing.Point(657, 68);
			dt_birthDate.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			dt_birthDate.Name = "dt_birthDate";
			dt_birthDate.ShowCheckBox = true;
			dt_birthDate.Size = new System.Drawing.Size(299, 33);
			dt_birthDate.TabIndex = 3;
			dt_birthDate.Value = new System.DateTime(2016, 10, 11, 0, 0, 0, 0);
			tableLayoutPanel1.SetColumnSpan(panel14, 3);
			panel14.Controls.Add(tb_zipcode);
			panel14.Controls.Add(cb_area);
			panel14.Controls.Add(cb_city);
			panel14.Controls.Add(tb_addr);
			panel14.Dock = System.Windows.Forms.DockStyle.Fill;
			panel14.Location = new System.Drawing.Point(164, 225);
			panel14.Margin = new System.Windows.Forms.Padding(0);
			panel14.Name = "panel14";
			panel14.Size = new System.Drawing.Size(802, 105);
			panel14.TabIndex = 40;
			tb_zipcode.Cursor = System.Windows.Forms.Cursors.No;
			tb_zipcode.Enabled = false;
			tb_zipcode.Location = new System.Drawing.Point(268, 18);
			tb_zipcode.Name = "tb_zipcode";
			tb_zipcode.ReadOnly = true;
			tb_zipcode.Size = new System.Drawing.Size(100, 33);
			tb_zipcode.TabIndex = 6;
			cb_area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_area.FormattingEnabled = true;
			cb_area.Location = new System.Drawing.Point(141, 19);
			cb_area.Name = "cb_area";
			cb_area.Size = new System.Drawing.Size(121, 32);
			cb_area.TabIndex = 8;
			cb_area.SelectedIndexChanged += new System.EventHandler(cb_area_SelectedIndexChanged);
			cb_city.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_city.FormattingEnabled = true;
			cb_city.Location = new System.Drawing.Point(14, 19);
			cb_city.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			cb_city.Name = "cb_city";
			cb_city.Size = new System.Drawing.Size(121, 32);
			cb_city.TabIndex = 7;
			cb_city.SelectedIndexChanged += new System.EventHandler(cb_city_SelectedIndexChanged);
			tb_addr.Location = new System.Drawing.Point(14, 64);
			tb_addr.Margin = new System.Windows.Forms.Padding(0);
			tb_addr.Name = "tb_addr";
			tb_addr.Size = new System.Drawing.Size(603, 33);
			tb_addr.TabIndex = 9;
			panel15.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel15.Controls.Add(label21);
			panel15.Controls.Add(label23);
			panel15.Dock = System.Windows.Forms.DockStyle.Fill;
			panel15.Location = new System.Drawing.Point(1, 57);
			panel15.Margin = new System.Windows.Forms.Padding(0);
			panel15.Name = "panel15";
			panel15.Size = new System.Drawing.Size(162, 55);
			panel15.TabIndex = 24;
			label21.AutoSize = true;
			label21.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label21.ForeColor = System.Drawing.Color.Red;
			label21.Location = new System.Drawing.Point(64, 21);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(17, 21);
			label21.TabIndex = 1;
			label21.Text = "*";
			label23.AutoSize = true;
			label23.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label23.ForeColor = System.Drawing.Color.White;
			label23.Location = new System.Drawing.Point(79, 21);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(74, 21);
			label23.TabIndex = 90;
			label23.Text = "會員姓名";
			cb_type.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_type.FormattingEnabled = true;
			cb_type.Location = new System.Drawing.Point(657, 13);
			cb_type.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
			cb_type.Name = "cb_type";
			cb_type.Size = new System.Drawing.Size(299, 32);
			cb_type.TabIndex = 1;
			panel2.BackColor = System.Drawing.Color.FromArgb(41, 162, 198);
			panel2.Controls.Add(label3);
			panel2.Controls.Add(label4);
			panel2.Location = new System.Drawing.Point(1, 60);
			panel2.Margin = new System.Windows.Forms.Padding(0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(156, 58);
			panel2.TabIndex = 20;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.Red;
			label3.Location = new System.Drawing.Point(67, 24);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(17, 21);
			label3.TabIndex = 1;
			label3.Text = "*";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.White;
			label4.Location = new System.Drawing.Point(80, 24);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(74, 21);
			label4.TabIndex = 90;
			label4.Text = "會員姓名";
			btn_back.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_back.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_back.ForeColor = System.Drawing.Color.White;
			btn_back.Location = new System.Drawing.Point(891, 44);
			btn_back.Name = "btn_back";
			btn_back.Size = new System.Drawing.Size(77, 28);
			btn_back.TabIndex = 3;
			btn_back.Text = "返回前頁";
			btn_back.UseVisualStyleBackColor = false;
			btn_back.Click += new System.EventHandler(btn_back_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(btn_back);
			base.Controls.Add(tabControl1);
			Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "frmNewMember";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmNewMember_Load);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(tabControl1, 0);
			base.Controls.SetChildIndex(btn_back, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			tabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			flowLayoutPanel4.ResumeLayout(false);
			flowLayoutPanel4.PerformLayout();
			panel11.ResumeLayout(false);
			panel11.PerformLayout();
			panel9.ResumeLayout(false);
			panel9.PerformLayout();
			panel8.ResumeLayout(false);
			panel8.PerformLayout();
			panel7.ResumeLayout(false);
			panel7.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel10.ResumeLayout(false);
			panel10.PerformLayout();
			panel12.ResumeLayout(false);
			panel12.PerformLayout();
			panel13.ResumeLayout(false);
			panel13.PerformLayout();
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			flowLayoutPanel3.ResumeLayout(false);
			flowLayoutPanel3.PerformLayout();
			panel14.ResumeLayout(false);
			panel14.PerformLayout();
			panel15.ResumeLayout(false);
			panel15.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			ResumeLayout(false);
		}
	}
}
