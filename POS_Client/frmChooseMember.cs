using POS_Client.Properties;
using POS_Client.WebService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmChooseMember : MasterThinForm
	{
		private UC_Member[] ucMembers;

		private int pageNow = 1;

		public int pageTotal = 1;

		public DataTable dt;

		private IContainer components;

		private Button btn_cancel;

		private Button btn_enter;

		private Button btn_create;

		private Button btn_reset;

		private Panel panel5;

		private Label label10;

		private Panel panel3;

		private Label label6;

		private Panel panel6;

		private Label label12;

		private TextBox tb_vipNo;

		private TextBox tb_name;

		private TextBox tb_telephone;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label1;

		private UC_Member uC_Member1;

		private UC_Member uC_Member2;

		private UC_Member uC_Member4;

		private UC_Member uC_Member3;

		private UC_Member uC_Member6;

		private UC_Member uC_Member5;

		private Panel panel1;

		private Button btn_pageLeft;

		private Button btn_pageRight;

		private Label l_pageInfo;

		private Panel panel2;

		private Label label2;

		private TextBox textBox1;

		public frmChooseMember()
			: base("會員選擇")
		{
			InitializeComponent();
			ucMembers = new UC_Member[6]
			{
				uC_Member1,
				uC_Member2,
				uC_Member3,
				uC_Member4,
				uC_Member5,
				uC_Member6
			};
			UC_Member[] array = ucMembers;
			foreach (UC_Member obj in array)
			{
				obj.OnClickMember += new EventHandler(viewMemberInfo);
				obj.showCheckBox(false);
				obj.showCancelBtn(false);
			}
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", " status = 0 ", "datetime(BuyDate) DESC limit 6", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
		}

		public void changePage(int page)
		{
			int num = 0;
			pageNow = page;
			pageTotal = (int)Math.Ceiling((double)dt.Rows.Count / 6.0);
			for (int i = (pageNow - 1) * 6; i < pageNow * 6; i++)
			{
				if (i < dt.Rows.Count)
				{
					ucMembers[num].setMemberName(dt.Rows[i]["Name"].ToString());
					ucMembers[num].setMemberVipNo(dt.Rows[i]["VipNo"].ToString());
					string memberHTEL = string.IsNullOrEmpty(dt.Rows[i]["Telphone"].ToString()) ? dt.Rows[i]["Mobile"].ToString() : dt.Rows[i]["Telphone"].ToString();
					ucMembers[num].setMemberHTEL(memberHTEL);
					ucMembers[num].setMemberIdNo(dt.Rows[i]["IdNo"].ToString());
					ucMembers[num].setCredit(dt.Rows[i]["Credit"].ToString());
					ucMembers[num].setTotal(dt.Rows[i]["Total"].ToString());
					ucMembers[num].Visible = true;
				}
				else
				{
					ucMembers[num].Visible = false;
				}
				ucMembers[num].checkMember(false);
				ucMembers[num].BackColor = Color.White;
				num++;
			}
			l_pageInfo.Text = string.Format("共{0}筆．{1}頁｜目前在第{2}頁", dt.Rows.Count, pageTotal, pageNow);
		}

		public void viewMemberInfo(object vipNo, EventArgs s)
		{
			int dBVersion = Program.GetDBVersion();
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			if (dBVersion == 0)
			{
				if (new frmDialogMember(vipNo.ToString()).ShowDialog() == DialogResult.Yes)
				{
					if (Program.SystemMode == 1)
					{
						switchForm(new frmMainShopSimple(vipNo.ToString()));
					}
					else
					{
						switchForm(new frmMainShopSimpleWithMoney(vipNo.ToString()));
					}
				}
			}
			else
			{
				if (dBVersion < 1 || !Program.IsFertilizer)
				{
					return;
				}
				DialogResult dialogResult = new frmDialogMember(vipNo.ToString()).ShowDialog();
				string[] strParameterArray = new string[1]
				{
					vipNo.ToString()
				};
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_CUST_RTL WHERE VipNo = {0} ", strParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count <= 0)
				{
					return;
				}
				if (dataTable.Rows[0]["IdNo"].ToString().Length == 10)
				{
					if ("Y".Equals(dataTable.Rows[0]["Verification"].ToString()))
					{
						if (dialogResult == DialogResult.Yes)
						{
							if (Program.SystemMode == 1)
							{
								switchForm(new frmMainShopSimple(vipNo.ToString()));
							}
							else
							{
								switchForm(new frmMainShopSimpleWithMoney(vipNo.ToString()));
							}
							AutoClosingMessageBox.Show("會員符合購肥補助資格");
						}
						return;
					}
					string text2 = new UploadVerification().farmerInfo(dataTable.Rows[0]["IdNo"].ToString());
					string[] strParameterArray2 = new string[4]
					{
						dataTable.Rows[0]["Name"].ToString(),
						dataTable.Rows[0]["IdNo"].ToString(),
						text,
						vipNo.ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_CUST_RTL ", null, CommandOperationType.ExecuteReaderReturnDataTable);
					if (text2.Equals("符合補助資格"))
					{
						if (dataTable2.Rows.Count > 0)
						{
							DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Verification = 'Y', IdNo = {1}, LastVerificationTime = {2} WHERE VipNo = {3}", strParameterArray2, CommandOperationType.ExecuteNonQuery);
						}
						else
						{
							DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_CUST_RTL ( Name, IdNo, Verification, LastVerificationTime) VALUES( {0}, {1}, 'Y', {2}) ", strParameterArray2, CommandOperationType.ExecuteNonQuery);
						}
						AutoClosingMessageBox.Show("驗證成功。");
					}
					else if (text2.Equals("購肥帳號密碼驗證錯誤"))
					{
						AutoClosingMessageBox.Show("帳號密碼有誤，請重新確認您的帳號密碼。");
						if (dataTable2.Rows.Count > 0)
						{
							DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Verification = 'N', IdNo = {1}, LastVerificationTime = {2} WHERE VipNo = {3} ", strParameterArray2, CommandOperationType.ExecuteNonQuery);
						}
						else
						{
							DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_CUST_RTL ( Name, IdNo, Verification, LastVerificationTime) VALUES( {0}, {1}, 'N', {2}) ", strParameterArray2, CommandOperationType.ExecuteNonQuery);
						}
					}
					else if (text2.Equals("偵測不到網路連線，請確認網路正常後再使用檢查補助") && dataTable.Rows.Count > 0 && "Y".Equals(dataTable.Rows[0]["Verification"].ToString()))
					{
						AutoClosingMessageBox.Show("會員符合購肥補助資格");
					}
					if (dialogResult == DialogResult.Yes)
					{
						if (Program.SystemMode == 1)
						{
							switchForm(new frmMainShopSimple(vipNo.ToString()));
						}
						else
						{
							switchForm(new frmMainShopSimpleWithMoney(vipNo.ToString()));
						}
					}
				}
				else if ("".Equals(dataTable.Rows[0]["IdNo"].ToString()))
				{
					if (dialogResult == DialogResult.Yes)
					{
						if (Program.SystemMode == 1)
						{
							switchForm(new frmMainShopSimple(vipNo.ToString()));
						}
						else
						{
							switchForm(new frmMainShopSimpleWithMoney(vipNo.ToString()));
						}
					}
				}
				else
				{
					AutoClosingMessageBox.Show("身分證字號錯誤");
				}
			}
		}

		private void btn_cancel_Click(object sender, EventArgs e)
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

		private void tb_vipNo_Enter(object sender, EventArgs e)
		{
			if (tb_vipNo.Text == "請刷會員卡(條碼)或輸入會員號")
			{
				tb_vipNo.Text = "";
			}
		}

		private void tb_vipNo_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_vipNo.Text))
			{
				tb_vipNo.Text = "請刷會員卡(條碼)或輸入會員號";
			}
		}

		private void tb_telephone_Enter(object sender, EventArgs e)
		{
			if (tb_telephone.Text == "請輸入會員聯絡電話")
			{
				tb_telephone.Text = "";
			}
		}

		private void tb_telephone_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_telephone.Text))
			{
				tb_telephone.Text = "請輸入會員聯絡電話";
			}
		}

		private void tb_idNo_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_name.Text))
			{
				tb_name.Text = "請輸入會員姓名";
			}
		}

		private void tb_idNo_Enter(object sender, EventArgs e)
		{
			if (tb_name.Text == "請輸入會員姓名")
			{
				tb_name.Text = "";
			}
		}

		private void btn_reset_Click(object sender, EventArgs e)
		{
			tb_name.Text = "請輸入會員姓名";
			tb_telephone.Text = "請輸入會員聯絡電話";
			textBox1.Text = "請輸入身分證字號";
			tb_vipNo.Text = "";
			tb_vipNo.Focus();
		}

		private void tb_vipNo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Return)
			{
				return;
			}
			if (tb_vipNo.Text.Length == 10)
			{
				string value = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "VipNo", "hypos_CUST_RTL", "VipNo = {0} and status = 0", "", null, new string[1]
				{
					tb_vipNo.Text
				}, CommandOperationType.ExecuteScalar).ToString();
				if (Program.SystemMode == 1)
				{
					if (!"-1".Equals(value))
					{
						switchForm(new frmMainShopSimple(tb_vipNo.Text));
					}
					else
					{
						AutoClosingMessageBox.Show("查無此會員");
					}
				}
				else if (!"-1".Equals(value))
				{
					switchForm(new frmMainShopSimpleWithMoney(tb_vipNo.Text));
				}
				else
				{
					AutoClosingMessageBox.Show("查無此會員");
				}
			}
			else
			{
				btn_enter_Click(sender, e);
			}
		}

		private void btn_create_Click(object sender, EventArgs e)
		{
			if (Program.SystemMode == 1)
			{
				switchForm(new frmNewMember("frmMainShopSimple"));
			}
			else
			{
				switchForm(new frmNewMember("frmMainShopSimpleWithMoney"));
			}
		}

		private void btn_pageRight_Click(object sender, EventArgs e)
		{
			if (pageNow < pageTotal)
			{
				changePage(pageNow + 1);
			}
		}

		private void btn_pageLeft_Click(object sender, EventArgs e)
		{
			if (pageNow > 1)
			{
				changePage(pageNow - 1);
			}
		}

		private void btn_enter_Click(object sender, EventArgs e)
		{
			if (tb_vipNo.Text == "請刷會員卡(條碼)或輸入會員號" && tb_name.Text == "請輸入會員姓名" && tb_telephone.Text == "請輸入會員聯絡電話" && textBox1.Text == "請輸入身分證字號")
			{
				AutoClosingMessageBox.Show("必須輸入查詢條件");
				return;
			}
			btn_pageLeft.Visible = true;
			btn_pageRight.Visible = true;
			l_pageInfo.Visible = true;
			label1.Text = "會員搜尋結果";
			int num = 0;
			List<string> list = new List<string>();
			string text = "SELECT * FROM hypos_CUST_RTL WHERE status = 0 ";
			if (tb_vipNo.Text != "請刷會員卡(條碼)或輸入會員號")
			{
				text = text + " AND VipNo like {" + num + "}";
				list.Add("%" + tb_vipNo.Text + "%");
				num++;
			}
			if (tb_name.Text != "請輸入會員姓名")
			{
				text = text + " AND Name like {" + num + "}";
				list.Add("%" + tb_name.Text + "%");
				num++;
			}
			if (tb_telephone.Text != "請輸入會員聯絡電話")
			{
				text = text + " AND (Telphone like {" + num + "}";
				list.Add("%" + tb_telephone.Text + "%");
				num++;
				text = text + " OR Mobile like {" + num + "})";
				list.Add("%" + tb_telephone.Text + "%");
				num++;
			}
			if (textBox1.Text != "請輸入身分證字號")
			{
				text = text + " AND IdNo like {" + num + "}";
				list.Add("%" + textBox1.Text + "%");
				num++;
			}
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text, list.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
		}

		private void textBox1_Enter(object sender, EventArgs e)
		{
			if (textBox1.Text == "請輸入身分證字號")
			{
				textBox1.Text = "";
			}
		}

		private void textBox1_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBox1.Text))
			{
				textBox1.Text = "請輸入身分證字號";
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS_Client.frmChooseMember));
			btn_cancel = new System.Windows.Forms.Button();
			btn_enter = new System.Windows.Forms.Button();
			btn_create = new System.Windows.Forms.Button();
			btn_reset = new System.Windows.Forms.Button();
			panel5 = new System.Windows.Forms.Panel();
			label10 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			tb_vipNo = new System.Windows.Forms.TextBox();
			tb_name = new System.Windows.Forms.TextBox();
			tb_telephone = new System.Windows.Forms.TextBox();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			textBox1 = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			uC_Member1 = new POS_Client.UC_Member();
			uC_Member2 = new POS_Client.UC_Member();
			uC_Member4 = new POS_Client.UC_Member();
			uC_Member3 = new POS_Client.UC_Member();
			uC_Member6 = new POS_Client.UC_Member();
			uC_Member5 = new POS_Client.UC_Member();
			panel1 = new System.Windows.Forms.Panel();
			btn_pageLeft = new System.Windows.Forms.Button();
			btn_pageRight = new System.Windows.Forms.Button();
			l_pageInfo = new System.Windows.Forms.Label();
			panel2 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel5.SuspendLayout();
			panel3.SuspendLayout();
			panel6.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(580, 229);
			btn_cancel.Name = "btn_cancel";
			btn_cancel.Size = new System.Drawing.Size(113, 35);
			btn_cancel.TabIndex = 43;
			btn_cancel.TabStop = false;
			btn_cancel.Text = "返回前頁";
			btn_cancel.UseVisualStyleBackColor = false;
			btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
			btn_enter.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_enter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_enter.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_enter.ForeColor = System.Drawing.Color.White;
			btn_enter.Location = new System.Drawing.Point(298, 229);
			btn_enter.Name = "btn_enter";
			btn_enter.Size = new System.Drawing.Size(113, 35);
			btn_enter.TabIndex = 1;
			btn_enter.TabStop = false;
			btn_enter.Text = "查詢";
			btn_enter.UseVisualStyleBackColor = false;
			btn_enter.Click += new System.EventHandler(btn_enter_Click);
			btn_create.BackColor = System.Drawing.Color.FromArgb(36, 168, 208);
			btn_create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_create.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_create.ForeColor = System.Drawing.Color.White;
			btn_create.Location = new System.Drawing.Point(779, 229);
			btn_create.Name = "btn_create";
			btn_create.Size = new System.Drawing.Size(110, 35);
			btn_create.TabIndex = 44;
			btn_create.TabStop = false;
			btn_create.Text = "新建會員";
			btn_create.UseVisualStyleBackColor = false;
			btn_create.Click += new System.EventHandler(btn_create_Click);
			btn_reset.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_reset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_reset.ForeColor = System.Drawing.Color.White;
			btn_reset.Location = new System.Drawing.Point(439, 229);
			btn_reset.Name = "btn_reset";
			btn_reset.Size = new System.Drawing.Size(113, 35);
			btn_reset.TabIndex = 42;
			btn_reset.TabStop = false;
			btn_reset.Text = "重設";
			btn_reset.UseVisualStyleBackColor = false;
			btn_reset.Click += new System.EventHandler(btn_reset_Click);
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label10);
			panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			panel5.Location = new System.Drawing.Point(1, 53);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(175, 51);
			panel5.TabIndex = 23;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(65, 15);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(90, 21);
			label10.TabIndex = 0;
			label10.Text = "身分證字號";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label6);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 1);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(175, 51);
			panel3.TabIndex = 21;
			label6.AutoSize = true;
			label6.BackColor = System.Drawing.Color.Transparent;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(65, 15);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(74, 21);
			label6.TabIndex = 0;
			label6.Text = "會員編號";
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			panel6.Location = new System.Drawing.Point(1, 105);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(175, 53);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(97, 18);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(42, 21);
			label12.TabIndex = 0;
			label12.Text = "姓名";
			tb_vipNo.BackColor = System.Drawing.Color.White;
			tb_vipNo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_vipNo.ForeColor = System.Drawing.Color.DarkGray;
			tb_vipNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_vipNo.Location = new System.Drawing.Point(187, 11);
			tb_vipNo.Margin = new System.Windows.Forms.Padding(10);
			tb_vipNo.MaxLength = 10;
			tb_vipNo.Name = "tb_vipNo";
			tb_vipNo.Size = new System.Drawing.Size(598, 29);
			tb_vipNo.TabIndex = 1;
			tb_vipNo.Text = "請刷會員卡(條碼)或輸入會員號";
			tb_vipNo.Enter += new System.EventHandler(tb_vipNo_Enter);
			tb_vipNo.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_vipNo_KeyDown);
			tb_vipNo.Leave += new System.EventHandler(tb_vipNo_Leave);
			tb_name.BackColor = System.Drawing.Color.White;
			tb_name.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_name.ForeColor = System.Drawing.Color.DarkGray;
			tb_name.Location = new System.Drawing.Point(187, 115);
			tb_name.Margin = new System.Windows.Forms.Padding(10);
			tb_name.Name = "tb_name";
			tb_name.Size = new System.Drawing.Size(243, 29);
			tb_name.TabIndex = 3;
			tb_name.Text = "請輸入會員姓名";
			tb_name.Enter += new System.EventHandler(tb_idNo_Enter);
			tb_name.Leave += new System.EventHandler(tb_idNo_Leave);
			tb_telephone.BackColor = System.Drawing.Color.White;
			tb_telephone.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_telephone.ForeColor = System.Drawing.Color.DarkGray;
			tb_telephone.Location = new System.Drawing.Point(650, 173);
			tb_telephone.Margin = new System.Windows.Forms.Padding(10);
			tb_telephone.Name = "tb_telephone";
			tb_telephone.Size = new System.Drawing.Size(228, 29);
			tb_telephone.TabIndex = 2;
			tb_telephone.Text = "請輸入會員聯絡電話";
			tb_telephone.Enter += new System.EventHandler(tb_telephone_Enter);
			tb_telephone.Leave += new System.EventHandler(tb_telephone_Leave);
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.13836f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.86163f));
			tableLayoutPanel1.Controls.Add(textBox1, 1, 1);
			tableLayoutPanel1.Controls.Add(tb_name, 1, 2);
			tableLayoutPanel1.Controls.Add(panel6, 0, 2);
			tableLayoutPanel1.Controls.Add(panel3, 0, 0);
			tableLayoutPanel1.Controls.Add(panel5, 0, 1);
			tableLayoutPanel1.Controls.Add(tb_vipNo, 1, 0);
			tableLayoutPanel1.Location = new System.Drawing.Point(93, 53);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 3;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel1.Size = new System.Drawing.Size(796, 159);
			tableLayoutPanel1.TabIndex = 40;
			textBox1.BackColor = System.Drawing.Color.White;
			textBox1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			textBox1.ForeColor = System.Drawing.Color.DarkGray;
			textBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
			textBox1.Location = new System.Drawing.Point(187, 63);
			textBox1.Margin = new System.Windows.Forms.Padding(10);
			textBox1.MaxLength = 10;
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(598, 29);
			textBox1.TabIndex = 61;
			textBox1.Text = "請輸入身分證字號";
			textBox1.Enter += new System.EventHandler(textBox1_Enter);
			textBox1.Leave += new System.EventHandler(textBox1_Leave);
			label1.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.Image = POS_Client.Properties.Resources.oblique;
			label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label1.Location = new System.Drawing.Point(88, 270);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(152, 23);
			label1.TabIndex = 46;
			label1.Text = "最近購買會員";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			uC_Member1.AutoSize = true;
			uC_Member1.BackColor = System.Drawing.Color.White;
			uC_Member1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member1.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member1.Location = new System.Drawing.Point(-10, 3);
			uC_Member1.Margin = new System.Windows.Forms.Padding(0);
			uC_Member1.Name = "uC_Member1";
			uC_Member1.Size = new System.Drawing.Size(423, 102);
			uC_Member1.TabIndex = 0;
			uC_Member2.AutoSize = true;
			uC_Member2.BackColor = System.Drawing.Color.White;
			uC_Member2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member2.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member2.Location = new System.Drawing.Point(413, 3);
			uC_Member2.Margin = new System.Windows.Forms.Padding(0);
			uC_Member2.Name = "uC_Member2";
			uC_Member2.Size = new System.Drawing.Size(423, 102);
			uC_Member2.TabIndex = 1;
			uC_Member4.AutoSize = true;
			uC_Member4.BackColor = System.Drawing.Color.White;
			uC_Member4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member4.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member4.Location = new System.Drawing.Point(413, 105);
			uC_Member4.Margin = new System.Windows.Forms.Padding(0);
			uC_Member4.Name = "uC_Member4";
			uC_Member4.Size = new System.Drawing.Size(423, 102);
			uC_Member4.TabIndex = 2;
			uC_Member3.AutoSize = true;
			uC_Member3.BackColor = System.Drawing.Color.White;
			uC_Member3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member3.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member3.Location = new System.Drawing.Point(-10, 105);
			uC_Member3.Margin = new System.Windows.Forms.Padding(0);
			uC_Member3.Name = "uC_Member3";
			uC_Member3.Size = new System.Drawing.Size(423, 102);
			uC_Member3.TabIndex = 3;
			uC_Member6.AutoScroll = true;
			uC_Member6.AutoSize = true;
			uC_Member6.BackColor = System.Drawing.Color.White;
			uC_Member6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member6.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member6.Location = new System.Drawing.Point(413, 207);
			uC_Member6.Margin = new System.Windows.Forms.Padding(0);
			uC_Member6.Name = "uC_Member6";
			uC_Member6.Size = new System.Drawing.Size(423, 102);
			uC_Member6.TabIndex = 6;
			uC_Member5.AutoScroll = true;
			uC_Member5.AutoSize = true;
			uC_Member5.BackColor = System.Drawing.Color.White;
			uC_Member5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member5.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member5.Location = new System.Drawing.Point(-10, 207);
			uC_Member5.Margin = new System.Windows.Forms.Padding(0);
			uC_Member5.Name = "uC_Member5";
			uC_Member5.Size = new System.Drawing.Size(423, 102);
			uC_Member5.TabIndex = 7;
			panel1.Controls.Add(uC_Member5);
			panel1.Controls.Add(uC_Member6);
			panel1.Controls.Add(uC_Member3);
			panel1.Controls.Add(uC_Member4);
			panel1.Controls.Add(uC_Member2);
			panel1.Controls.Add(uC_Member1);
			panel1.Location = new System.Drawing.Point(90, 305);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(801, 312);
			panel1.TabIndex = 45;
			btn_pageLeft.FlatAppearance.BorderSize = 0;
			btn_pageLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageLeft.Image = POS_Client.Properties.Resources.left;
			btn_pageLeft.Location = new System.Drawing.Point(0, 308);
			btn_pageLeft.Name = "btn_pageLeft";
			btn_pageLeft.Size = new System.Drawing.Size(48, 306);
			btn_pageLeft.TabIndex = 53;
			btn_pageLeft.UseVisualStyleBackColor = true;
			btn_pageLeft.Visible = false;
			btn_pageLeft.Click += new System.EventHandler(btn_pageLeft_Click);
			btn_pageRight.FlatAppearance.BorderSize = 0;
			btn_pageRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageRight.Image = POS_Client.Properties.Resources.right;
			btn_pageRight.Location = new System.Drawing.Point(933, 308);
			btn_pageRight.Name = "btn_pageRight";
			btn_pageRight.Size = new System.Drawing.Size(48, 306);
			btn_pageRight.TabIndex = 52;
			btn_pageRight.UseVisualStyleBackColor = true;
			btn_pageRight.Visible = false;
			btn_pageRight.Click += new System.EventHandler(btn_pageRight_Click);
			l_pageInfo.AutoSize = true;
			l_pageInfo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageInfo.Location = new System.Drawing.Point(382, 629);
			l_pageInfo.Name = "l_pageInfo";
			l_pageInfo.Size = new System.Drawing.Size(216, 20);
			l_pageInfo.TabIndex = 59;
			l_pageInfo.Text = "共{0}筆．{1}頁｜目前在第1頁\r\n";
			l_pageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			l_pageInfo.Visible = false;
			panel2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel2.Location = new System.Drawing.Point(530, 158);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(115, 53);
			panel2.TabIndex = 60;
			label2.AutoSize = true;
			label2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(559, 176);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(74, 21);
			label2.TabIndex = 1;
			label2.Text = "聯絡電話";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(tb_telephone);
			base.Controls.Add(label2);
			base.Controls.Add(panel2);
			base.Controls.Add(l_pageInfo);
			base.Controls.Add(btn_pageLeft);
			base.Controls.Add(btn_pageRight);
			base.Controls.Add(panel1);
			base.Controls.Add(label1);
			base.Controls.Add(btn_cancel);
			base.Controls.Add(btn_reset);
			base.Controls.Add(btn_enter);
			base.Controls.Add(btn_create);
			base.Controls.Add(tableLayoutPanel1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "frmChooseMember";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "會員選擇";
			base.Controls.SetChildIndex(tableLayoutPanel1, 0);
			base.Controls.SetChildIndex(btn_create, 0);
			base.Controls.SetChildIndex(btn_enter, 0);
			base.Controls.SetChildIndex(btn_reset, 0);
			base.Controls.SetChildIndex(btn_cancel, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(btn_pageRight, 0);
			base.Controls.SetChildIndex(btn_pageLeft, 0);
			base.Controls.SetChildIndex(l_pageInfo, 0);
			base.Controls.SetChildIndex(panel2, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(tb_telephone, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
