using KeyboardClassLibrary;
using POS_Client.Properties;
using POS_Client.WebService;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmDialogMember : Form
	{
		private string _vipNo;

		private string _LicenseCode;

		private IContainer components;

		private Button btn_back;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel12;

		private Label l_RepayDate;

		private Panel panel4;

		private Label label3;

		private Panel panel6;

		private Label label12;

		private Panel panel3;

		private Label label6;

		private Panel panel5;

		private Label label10;

		private Panel panel1;

		private Label label1;

		private Panel panel2;

		private Label label2;

		private TextBox tb_idno;

		private Panel panel7;

		private Label l_totalMoney;

		private Panel panel8;

		private Label l_creditMoney;

		private Panel panel9;

		private Label label7;

		private Panel panel10;

		private Label label8;

		private Panel panel11;

		private Label l_BuyDate;

		private TextBox tb_name;

		private Button btn_MemberSelect;

		private Label l_shopVipNo;

		private Panel panel14;

		private TextBox tb_zipcode;

		private ComboBox cb_area;

		private ComboBox cb_city;

		private TextBox tb_addr;

		private TextBox tb_companyIdno;

		private DateTimePicker dt_birthDate;

		private TextBox tb_companyName;

		private Panel panel16;

		private Panel panel15;

		private Panel panel13;

		private Label l_creditMoney2;

		private Label l_RepayDate2;

		private Label l_totalMoney2;

		private Label l_BuyDate2;

		private Label label20;

		private TextBox tb_tel;

		private Label label22;

		private TextBox tb_mobile;

		private Button btn_LowIncome;

		private PictureBox pictureBox1;

		private Panel panel17;

		private Button btn_down;

		private Button btn_top;

		private PictureBox pictureBox2;

		private Keyboardcontrol keyboardcontrol1;

		private Button btn_SaveMemberDataAndSelect;

		private Button button1;

		public frmDialogMember(string vipNo)
		{
			InitializeComponent();
			_vipNo = vipNo;
		}

		private void frmDialogMember_Load(object sender, EventArgs e)
		{
			if (Program.SystemMode == 1)
			{
				btn_LowIncome.Visible = false;
				l_totalMoney.Visible = false;
				l_totalMoney2.Visible = false;
				l_creditMoney.Visible = false;
				l_creditMoney2.Visible = false;
				l_RepayDate.Visible = false;
				l_RepayDate2.Visible = false;
			}
			DataTable dataSource = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "ADDRCITY", "", "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			cb_city.DisplayMember = "city";
			cb_city.ValueMember = "cityno";
			cb_city.DataSource = dataSource;
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", "VipNo = {0}", "", null, new string[1]
			{
				_vipNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				_LicenseCode = dataTable.Rows[0]["LicenseCode"].ToString();
				l_shopVipNo.Text = string.Format("{0}-{1}", dataTable.Rows[0]["LicenseCode"].ToString(), _vipNo);
				tb_name.Text = dataTable.Rows[0]["Name"].ToString();
				tb_idno.Text = dataTable.Rows[0]["IdNo"].ToString();
				tb_tel.Text = dataTable.Rows[0]["Telphone"].ToString();
				tb_mobile.Text = dataTable.Rows[0]["Mobile"].ToString();
				tb_companyName.Text = dataTable.Rows[0]["CompanyName"].ToString();
				tb_companyIdno.Text = dataTable.Rows[0]["CompanyIdNo"].ToString();
				if (!"".Equals(dataTable.Rows[0]["City"].ToString()))
				{
					cb_city.SelectedValue = dataTable.Rows[0]["City"].ToString();
				}
				else
				{
					cb_city.SelectedValue = 0;
				}
				if (!"".Equals(dataTable.Rows[0]["Area"].ToString()))
				{
					cb_area.SelectedValue = dataTable.Rows[0]["Area"].ToString();
				}
				else
				{
					cb_area.SelectedValue = 0;
				}
				tb_addr.Text = dataTable.Rows[0]["Address"].ToString();
				string text = dataTable.Rows[0]["BirthDate"].ToString();
				string value = "-";
				if (text.Length >= 8)
				{
					if (text.Length == 8)
					{
						if (text.IndexOf(value) > -1)
						{
							dt_birthDate.Text = dataTable.Rows[0]["BirthDate"].ToString();
						}
						else
						{
							string text2 = text.Insert(4, "-").Insert(7, "-");
							dt_birthDate.Text = text2;
						}
					}
					else
					{
						dt_birthDate.Text = dataTable.Rows[0]["BirthDate"].ToString();
					}
				}
				else
				{
					string text3 = text.Insert(4, "-").Insert(7, "-");
					dt_birthDate.Text = text3;
				}
				if (string.IsNullOrEmpty(dataTable.Rows[0]["Total"].ToString()))
				{
					l_totalMoney2.Text = "0";
				}
				else
				{
					l_totalMoney2.Text = string.Format("{0:n0}", int.Parse(dataTable.Rows[0]["Total"].ToString()));
				}
				if (string.IsNullOrEmpty(dataTable.Rows[0]["Credit"].ToString()))
				{
					l_creditMoney2.Text = "0";
				}
				else
				{
					l_creditMoney2.Text = string.Format("{0:n0}", int.Parse(dataTable.Rows[0]["Credit"].ToString()));
				}
				l_BuyDate2.Text = dataTable.Rows[0]["BuyDate"].ToString();
				l_RepayDate2.Text = dataTable.Rows[0]["RepayDate"].ToString();
			}
			else
			{
				AutoClosingMessageBox.Show("會員號碼錯誤!");
				Close();
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

		private void btn_back_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btn_save_Click(object sender, EventArgs e)
		{
			string text = "";
			if (cb_city.SelectedValue == null)
			{
				text += "請輸入會員所在城市\n";
			}
			if (cb_area.SelectedValue == null)
			{
				text += "請輸入會員所在區域\n";
			}
			if (!text.Equals(""))
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			string[,] strFieldArray = new string[4, 2]
			{
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
					"UpdateDate",
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_CUST_RTL", "VipNo = {0}", "", strFieldArray, new string[1]
			{
				_vipNo
			}, CommandOperationType.ExecuteNonQuery);
			base.DialogResult = DialogResult.Yes;
			AutoClosingMessageBox.Show("會員已選入");
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

		private void btn_LowIncome_Click(object sender, EventArgs e)
		{
			int dBVersion = Program.GetDBVersion();
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			if (dBVersion == 0)
			{
				AutoClosingMessageBox.Show(new UploadVerification().farmerInfo(tb_idno.Text));
			}
			else
			{
				if (dBVersion < 1 || !Program.IsFertilizer)
				{
					return;
				}
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
					_vipNo
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
		}

		private void btn_SaveMemberDataAndSelect_Click(object sender, EventArgs e)
		{
			string text = "";
			if (!"".Equals(tb_idno.Text.Trim()) && tb_idno.Text.Length != 10)
			{
				AutoClosingMessageBox.Show("身分證字號錯誤");
				return;
			}
			if (cb_city.SelectedValue == null)
			{
				text += "請輸入會員所在城市\n";
			}
			if (cb_area.SelectedValue == null)
			{
				text += "請輸入會員所在區域\n";
			}
			if (!text.Equals(""))
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			string[,] strFieldArray = new string[12, 2]
			{
				{
					"LicenseCode",
					_LicenseCode
				},
				{
					"IdNo",
					tb_idno.Text.Trim()
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
					"UpdateDate",
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
				},
				{
					"CompanyIdNo",
					tb_companyIdno.Text
				},
				{
					"CompanyName",
					tb_companyName.Text
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_CUST_RTL", "VipNo = {0}", "", strFieldArray, new string[1]
			{
				_vipNo
			}, CommandOperationType.ExecuteNonQuery);
			base.DialogResult = DialogResult.Yes;
			AutoClosingMessageBox.Show("會員資料已儲存，並選入會員");
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			frmEditMember frmEditMember = new frmEditMember(_vipNo, "frmDialogMember");
			frmEditMember.frmName = base.Name;
			frmEditMember.Location = new Point(base.Location.X, base.Location.Y);
			frmEditMember.Show();
			frmEditMember.Focus();
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
			btn_back = new System.Windows.Forms.Button();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			l_creditMoney2 = new System.Windows.Forms.Label();
			l_RepayDate2 = new System.Windows.Forms.Label();
			l_totalMoney2 = new System.Windows.Forms.Label();
			l_BuyDate2 = new System.Windows.Forms.Label();
			panel16 = new System.Windows.Forms.Panel();
			label20 = new System.Windows.Forms.Label();
			tb_tel = new System.Windows.Forms.TextBox();
			label22 = new System.Windows.Forms.Label();
			tb_mobile = new System.Windows.Forms.TextBox();
			panel8 = new System.Windows.Forms.Panel();
			l_creditMoney = new System.Windows.Forms.Label();
			panel7 = new System.Windows.Forms.Panel();
			l_totalMoney = new System.Windows.Forms.Label();
			panel15 = new System.Windows.Forms.Panel();
			btn_LowIncome = new System.Windows.Forms.Button();
			tb_idno = new System.Windows.Forms.TextBox();
			panel13 = new System.Windows.Forms.Panel();
			l_shopVipNo = new System.Windows.Forms.Label();
			panel14 = new System.Windows.Forms.Panel();
			tb_zipcode = new System.Windows.Forms.TextBox();
			cb_area = new System.Windows.Forms.ComboBox();
			cb_city = new System.Windows.Forms.ComboBox();
			tb_addr = new System.Windows.Forms.TextBox();
			panel4 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label10 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			panel2 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			panel9 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			panel10 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			tb_companyIdno = new System.Windows.Forms.TextBox();
			dt_birthDate = new System.Windows.Forms.DateTimePicker();
			panel11 = new System.Windows.Forms.Panel();
			l_BuyDate = new System.Windows.Forms.Label();
			panel12 = new System.Windows.Forms.Panel();
			l_RepayDate = new System.Windows.Forms.Label();
			tb_companyName = new System.Windows.Forms.TextBox();
			tb_name = new System.Windows.Forms.TextBox();
			btn_MemberSelect = new System.Windows.Forms.Button();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			panel17 = new System.Windows.Forms.Panel();
			btn_down = new System.Windows.Forms.Button();
			btn_top = new System.Windows.Forms.Button();
			pictureBox2 = new System.Windows.Forms.PictureBox();
			keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
			btn_SaveMemberDataAndSelect = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			tableLayoutPanel1.SuspendLayout();
			panel16.SuspendLayout();
			panel8.SuspendLayout();
			panel7.SuspendLayout();
			panel15.SuspendLayout();
			panel13.SuspendLayout();
			panel14.SuspendLayout();
			panel4.SuspendLayout();
			panel6.SuspendLayout();
			panel3.SuspendLayout();
			panel5.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			panel9.SuspendLayout();
			panel10.SuspendLayout();
			panel11.SuspendLayout();
			panel12.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			panel17.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			SuspendLayout();
			btn_back.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_back.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_back.ForeColor = System.Drawing.Color.White;
			btn_back.Location = new System.Drawing.Point(566, 532);
			btn_back.Name = "btn_back";
			btn_back.Size = new System.Drawing.Size(92, 40);
			btn_back.TabIndex = 0;
			btn_back.Text = "重新選擇";
			btn_back.UseVisualStyleBackColor = false;
			btn_back.Click += new System.EventHandler(btn_back_Click);
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 4;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.Controls.Add(l_creditMoney2, 3, 8);
			tableLayoutPanel1.Controls.Add(l_RepayDate2, 1, 8);
			tableLayoutPanel1.Controls.Add(l_totalMoney2, 3, 7);
			tableLayoutPanel1.Controls.Add(l_BuyDate2, 1, 7);
			tableLayoutPanel1.Controls.Add(panel16, 1, 4);
			tableLayoutPanel1.Controls.Add(panel8, 2, 8);
			tableLayoutPanel1.Controls.Add(panel7, 2, 7);
			tableLayoutPanel1.Controls.Add(panel15, 1, 1);
			tableLayoutPanel1.Controls.Add(panel13, 1, 0);
			tableLayoutPanel1.Controls.Add(panel14, 1, 5);
			tableLayoutPanel1.Controls.Add(panel4, 0, 5);
			tableLayoutPanel1.Controls.Add(panel6, 0, 2);
			tableLayoutPanel1.Controls.Add(panel3, 0, 0);
			tableLayoutPanel1.Controls.Add(panel5, 0, 1);
			tableLayoutPanel1.Controls.Add(panel1, 0, 3);
			tableLayoutPanel1.Controls.Add(panel2, 0, 4);
			tableLayoutPanel1.Controls.Add(panel9, 2, 2);
			tableLayoutPanel1.Controls.Add(panel10, 2, 3);
			tableLayoutPanel1.Controls.Add(tb_companyIdno, 3, 3);
			tableLayoutPanel1.Controls.Add(dt_birthDate, 3, 2);
			tableLayoutPanel1.Controls.Add(panel11, 0, 7);
			tableLayoutPanel1.Controls.Add(panel12, 0, 8);
			tableLayoutPanel1.Controls.Add(tb_companyName, 1, 3);
			tableLayoutPanel1.Controls.Add(tb_name, 1, 2);
			tableLayoutPanel1.Location = new System.Drawing.Point(51, 51);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
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
			tableLayoutPanel1.Size = new System.Drawing.Size(847, 462);
			tableLayoutPanel1.TabIndex = 41;
			l_creditMoney2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_creditMoney2.AutoSize = true;
			l_creditMoney2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_creditMoney2.Location = new System.Drawing.Point(590, 425);
			l_creditMoney2.Name = "l_creditMoney2";
			l_creditMoney2.Size = new System.Drawing.Size(28, 20);
			l_creditMoney2.TabIndex = 59;
			l_creditMoney2.Text = "{0}";
			l_RepayDate2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_RepayDate2.AutoSize = true;
			l_RepayDate2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_RepayDate2.Location = new System.Drawing.Point(167, 425);
			l_RepayDate2.Name = "l_RepayDate2";
			l_RepayDate2.Size = new System.Drawing.Size(28, 20);
			l_RepayDate2.TabIndex = 58;
			l_RepayDate2.Text = "{0}";
			l_totalMoney2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_totalMoney2.AutoSize = true;
			l_totalMoney2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_totalMoney2.Location = new System.Drawing.Point(590, 373);
			l_totalMoney2.Name = "l_totalMoney2";
			l_totalMoney2.Size = new System.Drawing.Size(28, 20);
			l_totalMoney2.TabIndex = 57;
			l_totalMoney2.Text = "{0}";
			l_BuyDate2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_BuyDate2.AutoSize = true;
			l_BuyDate2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_BuyDate2.Location = new System.Drawing.Point(167, 373);
			l_BuyDate2.Name = "l_BuyDate2";
			l_BuyDate2.Size = new System.Drawing.Size(28, 20);
			l_BuyDate2.TabIndex = 53;
			l_BuyDate2.Text = "{0}";
			tableLayoutPanel1.SetColumnSpan(panel16, 3);
			panel16.Controls.Add(label20);
			panel16.Controls.Add(tb_tel);
			panel16.Controls.Add(label22);
			panel16.Controls.Add(tb_mobile);
			panel16.Dock = System.Windows.Forms.DockStyle.Fill;
			panel16.Location = new System.Drawing.Point(164, 205);
			panel16.Margin = new System.Windows.Forms.Padding(0);
			panel16.Name = "panel16";
			panel16.Size = new System.Drawing.Size(682, 50);
			panel16.TabIndex = 56;
			label20.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label20.AutoSize = true;
			label20.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label20.ForeColor = System.Drawing.Color.Black;
			label20.Location = new System.Drawing.Point(10, 14);
			label20.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(73, 20);
			label20.TabIndex = 7;
			label20.Text = "室內電話";
			tb_tel.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_tel.Location = new System.Drawing.Point(95, 10);
			tb_tel.Margin = new System.Windows.Forms.Padding(10);
			tb_tel.Name = "tb_tel";
			tb_tel.Size = new System.Drawing.Size(239, 29);
			tb_tel.TabIndex = 6;
			label22.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label22.AutoSize = true;
			label22.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label22.ForeColor = System.Drawing.Color.Black;
			label22.Location = new System.Drawing.Point(346, 14);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(73, 20);
			label22.TabIndex = 9;
			label22.Text = "行動電話";
			tb_mobile.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_mobile.Location = new System.Drawing.Point(431, 10);
			tb_mobile.Margin = new System.Windows.Forms.Padding(10);
			tb_mobile.Name = "tb_mobile";
			tb_mobile.Size = new System.Drawing.Size(239, 29);
			tb_mobile.TabIndex = 8;
			panel8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel8.Controls.Add(l_creditMoney);
			panel8.Dock = System.Windows.Forms.DockStyle.Fill;
			panel8.ForeColor = System.Drawing.Color.FromArgb(82, 82, 82);
			panel8.Location = new System.Drawing.Point(424, 409);
			panel8.Margin = new System.Windows.Forms.Padding(0);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(162, 52);
			panel8.TabIndex = 47;
			l_creditMoney.AutoSize = true;
			l_creditMoney.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_creditMoney.ForeColor = System.Drawing.Color.White;
			l_creditMoney.Location = new System.Drawing.Point(85, 15);
			l_creditMoney.Name = "l_creditMoney";
			l_creditMoney.Size = new System.Drawing.Size(74, 21);
			l_creditMoney.TabIndex = 0;
			l_creditMoney.Text = "賒帳金額";
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(l_totalMoney);
			panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			panel7.ForeColor = System.Drawing.Color.FromArgb(82, 82, 82);
			panel7.Location = new System.Drawing.Point(424, 358);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(162, 50);
			panel7.TabIndex = 46;
			l_totalMoney.AutoSize = true;
			l_totalMoney.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_totalMoney.ForeColor = System.Drawing.Color.White;
			l_totalMoney.Location = new System.Drawing.Point(69, 14);
			l_totalMoney.Name = "l_totalMoney";
			l_totalMoney.Size = new System.Drawing.Size(90, 21);
			l_totalMoney.TabIndex = 0;
			l_totalMoney.Text = "總消費金額";
			tableLayoutPanel1.SetColumnSpan(panel15, 3);
			panel15.Controls.Add(btn_LowIncome);
			panel15.Controls.Add(tb_idno);
			panel15.Dock = System.Windows.Forms.DockStyle.Fill;
			panel15.Location = new System.Drawing.Point(164, 52);
			panel15.Margin = new System.Windows.Forms.Padding(0);
			panel15.Name = "panel15";
			panel15.Size = new System.Drawing.Size(682, 50);
			panel15.TabIndex = 55;
			btn_LowIncome.BackColor = System.Drawing.Color.FromArgb(255, 203, 24);
			btn_LowIncome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_LowIncome.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_LowIncome.ForeColor = System.Drawing.Color.White;
			btn_LowIncome.Location = new System.Drawing.Point(262, 10);
			btn_LowIncome.Name = "btn_LowIncome";
			btn_LowIncome.Size = new System.Drawing.Size(87, 29);
			btn_LowIncome.TabIndex = 43;
			btn_LowIncome.Text = "檢查輔助";
			btn_LowIncome.UseVisualStyleBackColor = false;
			btn_LowIncome.Click += new System.EventHandler(btn_LowIncome_Click);
			tb_idno.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_idno.ForeColor = System.Drawing.Color.DarkGray;
			tb_idno.Location = new System.Drawing.Point(10, 10);
			tb_idno.Margin = new System.Windows.Forms.Padding(10);
			tb_idno.MaxLength = 10;
			tb_idno.Name = "tb_idno";
			tb_idno.Size = new System.Drawing.Size(239, 29);
			tb_idno.TabIndex = 42;
			tableLayoutPanel1.SetColumnSpan(panel13, 3);
			panel13.Controls.Add(l_shopVipNo);
			panel13.Dock = System.Windows.Forms.DockStyle.Fill;
			panel13.Location = new System.Drawing.Point(164, 1);
			panel13.Margin = new System.Windows.Forms.Padding(0);
			panel13.Name = "panel13";
			panel13.Size = new System.Drawing.Size(682, 50);
			panel13.TabIndex = 54;
			l_shopVipNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_shopVipNo.AutoSize = true;
			l_shopVipNo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_shopVipNo.Location = new System.Drawing.Point(11, 15);
			l_shopVipNo.Name = "l_shopVipNo";
			l_shopVipNo.Size = new System.Drawing.Size(54, 20);
			l_shopVipNo.TabIndex = 52;
			l_shopVipNo.Text = "{0}-{1}";
			tableLayoutPanel1.SetColumnSpan(panel14, 3);
			panel14.Controls.Add(tb_zipcode);
			panel14.Controls.Add(cb_area);
			panel14.Controls.Add(cb_city);
			panel14.Controls.Add(tb_addr);
			panel14.Dock = System.Windows.Forms.DockStyle.Fill;
			panel14.Location = new System.Drawing.Point(164, 256);
			panel14.Margin = new System.Windows.Forms.Padding(0);
			panel14.Name = "panel14";
			tableLayoutPanel1.SetRowSpan(panel14, 2);
			panel14.Size = new System.Drawing.Size(682, 101);
			panel14.TabIndex = 53;
			tb_zipcode.Cursor = System.Windows.Forms.Cursors.No;
			tb_zipcode.Enabled = false;
			tb_zipcode.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_zipcode.Location = new System.Drawing.Point(288, 16);
			tb_zipcode.Name = "tb_zipcode";
			tb_zipcode.ReadOnly = true;
			tb_zipcode.Size = new System.Drawing.Size(100, 29);
			tb_zipcode.TabIndex = 6;
			cb_area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_area.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_area.FormattingEnabled = true;
			cb_area.Location = new System.Drawing.Point(151, 16);
			cb_area.Name = "cb_area";
			cb_area.Size = new System.Drawing.Size(121, 28);
			cb_area.TabIndex = 5;
			cb_area.SelectedIndexChanged += new System.EventHandler(cb_area_SelectedIndexChanged);
			cb_city.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_city.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_city.FormattingEnabled = true;
			cb_city.Location = new System.Drawing.Point(14, 16);
			cb_city.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			cb_city.Name = "cb_city";
			cb_city.Size = new System.Drawing.Size(121, 28);
			cb_city.TabIndex = 4;
			cb_city.SelectedIndexChanged += new System.EventHandler(cb_city_SelectedIndexChanged);
			tb_addr.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_addr.Location = new System.Drawing.Point(14, 57);
			tb_addr.Margin = new System.Windows.Forms.Padding(0);
			tb_addr.Name = "tb_addr";
			tb_addr.Size = new System.Drawing.Size(656, 29);
			tb_addr.TabIndex = 7;
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label3);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel4.ForeColor = System.Drawing.Color.White;
			panel4.Location = new System.Drawing.Point(1, 256);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			tableLayoutPanel1.SetRowSpan(panel4, 2);
			panel4.Size = new System.Drawing.Size(162, 101);
			panel4.TabIndex = 45;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.White;
			label3.Location = new System.Drawing.Point(119, 37);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(42, 21);
			label3.TabIndex = 0;
			label3.Text = "地址";
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			panel6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel6.ForeColor = System.Drawing.Color.White;
			panel6.Location = new System.Drawing.Point(1, 103);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(162, 50);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(119, 17);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(42, 21);
			label12.TabIndex = 0;
			label12.Text = "姓名";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label6);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel3.ForeColor = System.Drawing.Color.White;
			panel3.Location = new System.Drawing.Point(1, 1);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(162, 50);
			panel3.TabIndex = 21;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(48, 13);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(113, 21);
			label6.TabIndex = 0;
			label6.Text = "門市/會員編號";
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label10);
			panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			panel5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel5.ForeColor = System.Drawing.Color.White;
			panel5.Location = new System.Drawing.Point(1, 52);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(162, 50);
			panel5.TabIndex = 23;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(71, 15);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(90, 21);
			label10.TabIndex = 0;
			label10.Text = "身分證字號";
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel1.ForeColor = System.Drawing.Color.White;
			panel1.Location = new System.Drawing.Point(1, 154);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(162, 50);
			panel1.TabIndex = 43;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(87, 17);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(74, 21);
			label1.TabIndex = 0;
			label1.Text = "公司名稱";
			panel2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel2.Controls.Add(label2);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel2.ForeColor = System.Drawing.Color.White;
			panel2.Location = new System.Drawing.Point(1, 205);
			panel2.Margin = new System.Windows.Forms.Padding(0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(162, 50);
			panel2.TabIndex = 44;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(87, 17);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(74, 21);
			label2.TabIndex = 0;
			label2.Text = "電話號碼";
			panel9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel9.Controls.Add(label7);
			panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			panel9.ForeColor = System.Drawing.Color.FromArgb(82, 82, 82);
			panel9.Location = new System.Drawing.Point(424, 103);
			panel9.Margin = new System.Windows.Forms.Padding(0);
			panel9.Name = "panel9";
			panel9.Size = new System.Drawing.Size(162, 50);
			panel9.TabIndex = 48;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(69, 17);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(90, 21);
			label7.TabIndex = 0;
			label7.Text = "出生年月日";
			panel10.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel10.Controls.Add(label8);
			panel10.Dock = System.Windows.Forms.DockStyle.Fill;
			panel10.ForeColor = System.Drawing.Color.FromArgb(82, 82, 82);
			panel10.Location = new System.Drawing.Point(424, 154);
			panel10.Margin = new System.Windows.Forms.Padding(0);
			panel10.Name = "panel10";
			panel10.Size = new System.Drawing.Size(162, 50);
			panel10.TabIndex = 49;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.White;
			label8.Location = new System.Drawing.Point(85, 13);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(74, 21);
			label8.TabIndex = 0;
			label8.Text = "統一編號";
			tb_companyIdno.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_companyIdno.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_companyIdno.ForeColor = System.Drawing.Color.DarkGray;
			tb_companyIdno.Location = new System.Drawing.Point(597, 164);
			tb_companyIdno.Margin = new System.Windows.Forms.Padding(10);
			tb_companyIdno.Name = "tb_companyIdno";
			tb_companyIdno.Size = new System.Drawing.Size(239, 29);
			tb_companyIdno.TabIndex = 54;
			dt_birthDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dt_birthDate.CustomFormat = "yyyy-MM-dd";
			dt_birthDate.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dt_birthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dt_birthDate.Location = new System.Drawing.Point(597, 111);
			dt_birthDate.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			dt_birthDate.Name = "dt_birthDate";
			dt_birthDate.Size = new System.Drawing.Size(239, 33);
			dt_birthDate.TabIndex = 55;
			panel11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel11.Controls.Add(l_BuyDate);
			panel11.Dock = System.Windows.Forms.DockStyle.Fill;
			panel11.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel11.ForeColor = System.Drawing.Color.White;
			panel11.Location = new System.Drawing.Point(1, 358);
			panel11.Margin = new System.Windows.Forms.Padding(0);
			panel11.Name = "panel11";
			panel11.Size = new System.Drawing.Size(162, 50);
			panel11.TabIndex = 50;
			l_BuyDate.AutoSize = true;
			l_BuyDate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_BuyDate.ForeColor = System.Drawing.Color.White;
			l_BuyDate.Location = new System.Drawing.Point(54, 17);
			l_BuyDate.Name = "l_BuyDate";
			l_BuyDate.Size = new System.Drawing.Size(106, 21);
			l_BuyDate.TabIndex = 0;
			l_BuyDate.Text = "最近消費日期";
			panel12.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel12.Controls.Add(l_RepayDate);
			panel12.Dock = System.Windows.Forms.DockStyle.Fill;
			panel12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			panel12.ForeColor = System.Drawing.Color.White;
			panel12.Location = new System.Drawing.Point(1, 409);
			panel12.Margin = new System.Windows.Forms.Padding(0);
			panel12.Name = "panel12";
			panel12.Size = new System.Drawing.Size(162, 52);
			panel12.TabIndex = 50;
			l_RepayDate.AutoSize = true;
			l_RepayDate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_RepayDate.ForeColor = System.Drawing.Color.White;
			l_RepayDate.Location = new System.Drawing.Point(54, 15);
			l_RepayDate.Name = "l_RepayDate";
			l_RepayDate.Size = new System.Drawing.Size(106, 21);
			l_RepayDate.TabIndex = 0;
			l_RepayDate.Text = "最近還款日期";
			tb_companyName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_companyName.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_companyName.ForeColor = System.Drawing.Color.DarkGray;
			tb_companyName.Location = new System.Drawing.Point(174, 164);
			tb_companyName.Margin = new System.Windows.Forms.Padding(10);
			tb_companyName.Name = "tb_companyName";
			tb_companyName.Size = new System.Drawing.Size(239, 29);
			tb_companyName.TabIndex = 56;
			tb_name.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_name.ForeColor = System.Drawing.Color.DarkGray;
			tb_name.Location = new System.Drawing.Point(174, 113);
			tb_name.Margin = new System.Windows.Forms.Padding(10);
			tb_name.Name = "tb_name";
			tb_name.Size = new System.Drawing.Size(239, 29);
			tb_name.TabIndex = 51;
			btn_MemberSelect.BackColor = System.Drawing.Color.Red;
			btn_MemberSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_MemberSelect.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_MemberSelect.ForeColor = System.Drawing.Color.White;
			btn_MemberSelect.Location = new System.Drawing.Point(99, 532);
			btn_MemberSelect.Name = "btn_MemberSelect";
			btn_MemberSelect.Size = new System.Drawing.Size(113, 40);
			btn_MemberSelect.TabIndex = 42;
			btn_MemberSelect.Text = "選入會員";
			btn_MemberSelect.UseVisualStyleBackColor = false;
			btn_MemberSelect.Visible = false;
			btn_MemberSelect.Click += new System.EventHandler(btn_save_Click);
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
			btn_SaveMemberDataAndSelect.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
			btn_SaveMemberDataAndSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SaveMemberDataAndSelect.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SaveMemberDataAndSelect.ForeColor = System.Drawing.Color.White;
			btn_SaveMemberDataAndSelect.Location = new System.Drawing.Point(299, 532);
			btn_SaveMemberDataAndSelect.Name = "btn_SaveMemberDataAndSelect";
			btn_SaveMemberDataAndSelect.Size = new System.Drawing.Size(197, 40);
			btn_SaveMemberDataAndSelect.TabIndex = 54;
			btn_SaveMemberDataAndSelect.Text = "確定選擇(並儲存變更)";
			btn_SaveMemberDataAndSelect.UseVisualStyleBackColor = false;
			btn_SaveMemberDataAndSelect.Click += new System.EventHandler(btn_SaveMemberDataAndSelect_Click);
			button1.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.ForeColor = System.Drawing.Color.White;
			button1.Location = new System.Drawing.Point(741, 532);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(122, 40);
			button1.TabIndex = 55;
			button1.Text = "會員消費記錄";
			button1.UseVisualStyleBackColor = false;
			button1.Click += new System.EventHandler(button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Silver;
			base.ClientSize = new System.Drawing.Size(949, 636);
			base.Controls.Add(button1);
			base.Controls.Add(btn_SaveMemberDataAndSelect);
			base.Controls.Add(panel17);
			base.Controls.Add(pictureBox1);
			base.Controls.Add(btn_MemberSelect);
			base.Controls.Add(tableLayoutPanel1);
			base.Controls.Add(btn_back);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmDialogMember";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "選擇會員 / 會員編修";
			base.Load += new System.EventHandler(frmDialogMember_Load);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel16.ResumeLayout(false);
			panel16.PerformLayout();
			panel8.ResumeLayout(false);
			panel8.PerformLayout();
			panel7.ResumeLayout(false);
			panel7.PerformLayout();
			panel15.ResumeLayout(false);
			panel15.PerformLayout();
			panel13.ResumeLayout(false);
			panel13.PerformLayout();
			panel14.ResumeLayout(false);
			panel14.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel9.ResumeLayout(false);
			panel9.PerformLayout();
			panel10.ResumeLayout(false);
			panel10.PerformLayout();
			panel11.ResumeLayout(false);
			panel11.PerformLayout();
			panel12.ResumeLayout(false);
			panel12.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			panel17.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			ResumeLayout(false);
		}
	}
}
