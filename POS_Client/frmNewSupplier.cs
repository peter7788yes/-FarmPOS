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
	public class frmNewSupplier : MasterThinForm
	{
		private string vendorId = "";

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

		private Panel panel2;

		private Label label3;

		private Label label4;

		private Panel panel1;

		private Panel panel4;

		private Label label8;

		private Panel panel9;

		private Label label5;

		private Label label7;

		private Panel panel10;

		private Label label13;

		private Panel panel12;

		private Label label17;

		private Panel panel13;

		private Label label18;

		private TextBox tb_DutyName;

		private FlowLayoutPanel flowLayoutPanel1;

		private TextBox tb_supplierNo;

		private FlowLayoutPanel flowLayoutPanel3;

		private Button btn_cancel;

		private Button btn_save;

		private Button btn_back;

		private Panel panel14;

		private TextBox tb_addr;

		private Panel panel15;

		private Label label23;

		private TextBox tb_Fax;

		private Label label1;

		private TextBox tb_Email;

		private ComboBox cb_type;

		private TextBox tb_Mobile;

		private Label label2;

		private TextBox tb_ContactName;

		private TextBox tb_ContactJob;

		private TextBox tb_TelNo;

		private TextBox tb_TelExt;

		private FlowLayoutPanel flowLayoutPanel2;

		private ComboBox cb_city;

		private ComboBox cb_area;

		private TextBox tb_zipcode;

		private TextBox tb_Address;

		private TextBox tb_SupplierIdNo;

		private Label label6;

		private ComboBox cb_status;

		private Panel panel11;

		private Panel panel5;

		private Label label9;

		private Panel panel16;

		private CheckBox cb_DeliveryType;

		private CheckBox cb_PurchaseType;

		private Label label15;

		private Button btn_checkVendorID;

		private TextBox tb_vendorId;

		private TextBox tb_vendorName;

		private Label label11;

		private Label label10;

		private Panel panel17;

		private Label label19;

		private TextBox tb_SupplierName;

		public frmNewSupplier()
			: base("新增廠商")
		{
			InitializeComponent();
		}

		private void frmNewSupplier_Load(object sender, EventArgs e)
		{
			DataTable dataSource = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "ADDRCITY", "", "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			cb_city.DisplayMember = "city";
			cb_city.ValueMember = "cityno";
			cb_city.DataSource = dataSource;
			cb_status.Items.Add(new ComboboxItem("正常", "0"));
			cb_status.Items.Add(new ComboboxItem("停用", "1"));
			cb_status.SelectedIndex = 0;
			cb_type.Items.Add(new ComboboxItem("本地廠商", "0"));
			cb_type.Items.Add(new ComboboxItem("進口廠商", "1"));
			cb_type.SelectedIndex = 0;
			tb_supplierNo.Text = getSupplierNo();
		}

		public static string getSupplierNo()
		{
			string sql = "SELECT SupplierNo FROM hypos_Supplier where SupplierNo like 'S" + Program.SiteNo.ToString() + "%'order by SupplierNo desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			string text2 = DateTime.Now.Year.ToString().Substring(2, 2);
			if ("-1".Equals(text))
			{
				return string.Format("S{0}{1}00001", Program.SiteNo, text2);
			}
			string value = text.Substring(3, 2);
			if (!text2.Equals(value))
			{
				return string.Format("S{0}{1}00001", Program.SiteNo, text2);
			}
			string arg = string.Format("{0:00000}", int.Parse(text.Substring(5, 5)) + 1);
			return string.Format("S{0}{1}{2}", Program.SiteNo, text2, arg);
		}

		private void btn_back_Click(object sender, EventArgs e)
		{
			backToPreviousForm();
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			switchForm(new frmSupplierManage());
		}

		private void btn_save_Click(object sender, EventArgs e)
		{
			if (!tb_vendorId.Text.Equals("") && tb_vendorName.Text.Equals(""))
			{
				if (MessageBox.Show("廠商營業資訊尚未檢查。是否放棄營業資訊驗證？", "營業資訊驗證", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
				{
					return;
				}
				vendorId = "";
				tb_vendorId.Text = "";
			}
			string text = "";
			if (tb_SupplierName.Text.Trim().Equals(""))
			{
				text += "請輸入廠商名稱\n";
			}
			if (Program.IsDeployClickOnce && !string.IsNullOrEmpty(text))
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			if (Program.GetDBVersion() >= 6)
			{
				string text2 = "0";
				if (cb_PurchaseType.Checked && !cb_DeliveryType.Checked)
				{
					text2 = "1";
				}
				else if (!cb_PurchaseType.Checked && cb_DeliveryType.Checked)
				{
					text2 = "2";
				}
				else if (!cb_PurchaseType.Checked && !cb_DeliveryType.Checked)
				{
					text2 = "-1";
				}
				string[,] strFieldArray = new string[19, 2]
				{
					{
						"SupplierNo",
						tb_supplierNo.Text
					},
					{
						"SupplierName",
						tb_SupplierName.Text
					},
					{
						"SupplierIdNo",
						tb_SupplierIdNo.Text
					},
					{
						"DutyName",
						tb_DutyName.Text
					},
					{
						"TelNo",
						tb_TelNo.Text
					},
					{
						"TelExt",
						tb_TelExt.Text
					},
					{
						"Fax",
						tb_Fax.Text
					},
					{
						"Mobile",
						tb_Mobile.Text
					},
					{
						"ContactName",
						tb_ContactName.Text
					},
					{
						"ContactJob",
						tb_ContactJob.Text
					},
					{
						"CityNo",
						cb_city.SelectedValue.ToString()
					},
					{
						"Zipcode",
						cb_area.SelectedValue.ToString()
					},
					{
						"Address",
						tb_addr.Text
					},
					{
						"Email",
						tb_Email.Text
					},
					{
						"Type",
						(cb_type.SelectedItem as ComboboxItem).Value.ToString()
					},
					{
						"Status",
						(cb_status.SelectedItem as ComboboxItem).Value.ToString()
					},
					{
						"vendorType",
						text2
					},
					{
						"vendorId",
						vendorId
					},
					{
						"vendorName",
						tb_vendorName.Text
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Supplier", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			}
			else
			{
				string[,] strFieldArray2 = new string[16, 2]
				{
					{
						"SupplierNo",
						tb_supplierNo.Text
					},
					{
						"SupplierName",
						tb_SupplierName.Text
					},
					{
						"SupplierIdNo",
						tb_SupplierIdNo.Text
					},
					{
						"DutyName",
						tb_DutyName.Text
					},
					{
						"TelNo",
						tb_TelNo.Text
					},
					{
						"TelExt",
						tb_TelExt.Text
					},
					{
						"Fax",
						tb_Fax.Text
					},
					{
						"Mobile",
						tb_Mobile.Text
					},
					{
						"ContactName",
						tb_ContactName.Text
					},
					{
						"ContactJob",
						tb_ContactJob.Text
					},
					{
						"CityNo",
						cb_city.SelectedValue.ToString()
					},
					{
						"Zipcode",
						cb_area.SelectedValue.ToString()
					},
					{
						"Address",
						tb_addr.Text
					},
					{
						"Email",
						tb_Email.Text
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
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Supplier", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
			}
			AutoClosingMessageBox.Show("廠商新增完成");
			switchForm(new frmSupplierManage());
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

		private void btn_checkVendorID_Click(object sender, EventArgs e)
		{
			vendorId = tb_vendorId.Text.Trim();
			if (vendorId.Equals("請輸入執照號碼後點選檢查"))
			{
				vendorId = "";
			}
			if (vendorId.Equals(""))
			{
				tb_vendorName.Text = "";
				AutoClosingMessageBox.Show("「販賣執照號碼」必填，請檢查");
				return;
			}
			if (!checkVendorID(vendorId))
			{
				tb_vendorName.Text = "";
				AutoClosingMessageBox.Show("請輸入廠商之新販賣業執照號碼，包含一碼英文+五碼數字");
				return;
			}
			VendorResultObject vendorResultObject = new VerifyVendorInfoWS().vendorData(tb_vendorId.Text);
			if (vendorResultObject.success == "Y")
			{
				if (MessageBox.Show("證號：" + vendorResultObject.vendorId + "、名稱：" + vendorResultObject.vendorName + " \n是否使用此廠商資訊？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
				{
					tb_vendorName.Text = vendorResultObject.vendorName;
				}
			}
			else if (vendorResultObject.success == "N")
			{
				string text = "";
				if (vendorResultObject.message.Equals("廠商販賣執照號碼不存在"))
				{
					text = "廠商營業執照號碼不存在，請檢查輸入值";
				}
				else if (vendorResultObject.message.Equals("廠商已歇業或停業"))
				{
					text = "此廠商目前已停用，請先確認廠商狀態";
				}
				tb_vendorId.Text = "";
				tb_vendorName.Text = "";
				MessageBox.Show(text);
			}
		}

		private bool checkVendorID(string id)
		{
			List<string> list = new List<string>();
			list.Add("A");
			list.Add("B");
			list.Add("C");
			list.Add("D");
			list.Add("E");
			list.Add("F");
			list.Add("G");
			list.Add("H");
			list.Add("J");
			list.Add("K");
			list.Add("L");
			list.Add("M");
			list.Add("N");
			list.Add("P");
			list.Add("Q");
			list.Add("R");
			list.Add("S");
			list.Add("T");
			list.Add("U");
			list.Add("V");
			list.Add("X");
			list.Add("Y");
			list.Add("W");
			list.Add("Z");
			list.Add("I");
			list.Add("O");
			List<string> list2 = list;
			if (id.Trim().Length == 6)
			{
				for (int i = 1; i < 6; i++)
				{
					byte b = Convert.ToByte(id.Trim().Substring(i, 1));
					if (b > 9 || b < 0)
					{
						return false;
					}
				}
				id = id.ToUpper();
				int j;
				for (j = 0; j < list2.Count && !(id.Substring(0, 1) == list2[j]); j++)
				{
				}
				if (j > 25)
				{
					return false;
				}
				return true;
			}
			return false;
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
			tb_SupplierName = new System.Windows.Forms.TextBox();
			panel9 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			tb_Fax = new System.Windows.Forms.TextBox();
			tb_Email = new System.Windows.Forms.TextBox();
			panel3 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			panel10 = new System.Windows.Forms.Panel();
			label13 = new System.Windows.Forms.Label();
			panel13 = new System.Windows.Forms.Panel();
			label16 = new System.Windows.Forms.Label();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			tb_supplierNo = new System.Windows.Forms.TextBox();
			flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			tb_TelNo = new System.Windows.Forms.TextBox();
			tb_TelExt = new System.Windows.Forms.TextBox();
			panel15 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			label23 = new System.Windows.Forms.Label();
			panel6 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			tb_DutyName = new System.Windows.Forms.TextBox();
			panel8 = new System.Windows.Forms.Panel();
			label14 = new System.Windows.Forms.Label();
			panel14 = new System.Windows.Forms.Panel();
			cb_DeliveryType = new System.Windows.Forms.CheckBox();
			cb_PurchaseType = new System.Windows.Forms.CheckBox();
			tb_addr = new System.Windows.Forms.TextBox();
			cb_type = new System.Windows.Forms.ComboBox();
			panel7 = new System.Windows.Forms.Panel();
			label18 = new System.Windows.Forms.Label();
			panel12 = new System.Windows.Forms.Panel();
			label17 = new System.Windows.Forms.Label();
			flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			cb_city = new System.Windows.Forms.ComboBox();
			cb_area = new System.Windows.Forms.ComboBox();
			tb_zipcode = new System.Windows.Forms.TextBox();
			tb_Address = new System.Windows.Forms.TextBox();
			tb_Mobile = new System.Windows.Forms.TextBox();
			cb_status = new System.Windows.Forms.ComboBox();
			panel11 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			panel16 = new System.Windows.Forms.Panel();
			label15 = new System.Windows.Forms.Label();
			btn_checkVendorID = new System.Windows.Forms.Button();
			tb_vendorId = new System.Windows.Forms.TextBox();
			tb_vendorName = new System.Windows.Forms.TextBox();
			label11 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			panel17 = new System.Windows.Forms.Panel();
			tb_ContactJob = new System.Windows.Forms.TextBox();
			tb_ContactName = new System.Windows.Forms.TextBox();
			label19 = new System.Windows.Forms.Label();
			tb_SupplierIdNo = new System.Windows.Forms.TextBox();
			panel2 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			btn_back = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			tabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			panel9.SuspendLayout();
			panel3.SuspendLayout();
			panel1.SuspendLayout();
			panel4.SuspendLayout();
			panel10.SuspendLayout();
			panel13.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			flowLayoutPanel3.SuspendLayout();
			panel15.SuspendLayout();
			panel6.SuspendLayout();
			panel8.SuspendLayout();
			panel14.SuspendLayout();
			panel7.SuspendLayout();
			panel12.SuspendLayout();
			flowLayoutPanel2.SuspendLayout();
			panel11.SuspendLayout();
			panel5.SuspendLayout();
			panel16.SuspendLayout();
			panel17.SuspendLayout();
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
			tableLayoutPanel1.Controls.Add(tb_SupplierName, 1, 1);
			tableLayoutPanel1.Controls.Add(panel9, 2, 0);
			tableLayoutPanel1.Controls.Add(tb_Fax, 1, 7);
			tableLayoutPanel1.Controls.Add(tb_Email, 3, 7);
			tableLayoutPanel1.Controls.Add(panel3, 0, 2);
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(panel4, 0, 3);
			tableLayoutPanel1.Controls.Add(panel10, 2, 1);
			tableLayoutPanel1.Controls.Add(panel13, 2, 7);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 1, 2);
			tableLayoutPanel1.Controls.Add(panel15, 0, 1);
			tableLayoutPanel1.Controls.Add(panel6, 0, 4);
			tableLayoutPanel1.Controls.Add(tb_DutyName, 3, 1);
			tableLayoutPanel1.Controls.Add(panel8, 0, 8);
			tableLayoutPanel1.Controls.Add(panel14, 1, 8);
			tableLayoutPanel1.Controls.Add(panel7, 0, 7);
			tableLayoutPanel1.Controls.Add(panel12, 0, 5);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 1, 5);
			tableLayoutPanel1.Controls.Add(tb_Mobile, 1, 4);
			tableLayoutPanel1.Controls.Add(cb_status, 3, 8);
			tableLayoutPanel1.Controls.Add(panel11, 2, 8);
			tableLayoutPanel1.Controls.Add(panel5, 2, 2);
			tableLayoutPanel1.Controls.Add(panel16, 3, 2);
			tableLayoutPanel1.Controls.Add(panel17, 1, 3);
			tableLayoutPanel1.Controls.Add(tb_SupplierIdNo, 3, 0);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
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
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel1.Size = new System.Drawing.Size(967, 503);
			tableLayoutPanel1.TabIndex = 90;
			tb_SupplierName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SupplierName.Location = new System.Drawing.Point(174, 66);
			tb_SupplierName.Margin = new System.Windows.Forms.Padding(10);
			tb_SupplierName.Name = "tb_SupplierName";
			tb_SupplierName.Size = new System.Drawing.Size(299, 33);
			tb_SupplierName.TabIndex = 91;
			panel9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel9.Controls.Add(label7);
			panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			panel9.Location = new System.Drawing.Point(484, 1);
			panel9.Margin = new System.Windows.Forms.Padding(0);
			panel9.Name = "panel9";
			panel9.Size = new System.Drawing.Size(162, 54);
			panel9.TabIndex = 20;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(80, 17);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(74, 21);
			label7.TabIndex = 10;
			label7.Text = "統一編號";
			tb_Fax.Location = new System.Drawing.Point(174, 396);
			tb_Fax.Margin = new System.Windows.Forms.Padding(10);
			tb_Fax.Name = "tb_Fax";
			tb_Fax.Size = new System.Drawing.Size(299, 33);
			tb_Fax.TabIndex = 5;
			tb_Email.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_Email.Location = new System.Drawing.Point(657, 396);
			tb_Email.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_Email.Name = "tb_Email";
			tb_Email.Size = new System.Drawing.Size(299, 33);
			tb_Email.TabIndex = 14;
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label8);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 111);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(162, 54);
			panel3.TabIndex = 21;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.White;
			label8.Location = new System.Drawing.Point(81, 18);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(74, 21);
			label8.TabIndex = 100;
			label8.Text = "聯絡電話";
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
			label1.Location = new System.Drawing.Point(67, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(90, 21);
			label1.TabIndex = 90;
			label1.Text = "供應商編號";
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label12);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Location = new System.Drawing.Point(1, 166);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(162, 54);
			panel4.TabIndex = 22;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(96, 16);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(58, 21);
			label12.TabIndex = 10;
			label12.Text = "聯絡人";
			panel10.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel10.Controls.Add(label13);
			panel10.Dock = System.Windows.Forms.DockStyle.Fill;
			panel10.Location = new System.Drawing.Point(484, 56);
			panel10.Margin = new System.Windows.Forms.Padding(0);
			panel10.Name = "panel10";
			panel10.Size = new System.Drawing.Size(162, 54);
			panel10.TabIndex = 24;
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.White;
			label13.Location = new System.Drawing.Point(95, 17);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(58, 21);
			label13.TabIndex = 90;
			label13.Text = "負責人";
			panel13.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel13.Controls.Add(label16);
			panel13.Dock = System.Windows.Forms.DockStyle.Fill;
			panel13.Location = new System.Drawing.Point(484, 386);
			panel13.Margin = new System.Windows.Forms.Padding(0);
			panel13.Name = "panel13";
			panel13.Size = new System.Drawing.Size(162, 54);
			panel13.TabIndex = 24;
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label16.ForeColor = System.Drawing.Color.White;
			label16.Location = new System.Drawing.Point(79, 16);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(74, 21);
			label16.TabIndex = 10;
			label16.Text = "電子信箱";
			flowLayoutPanel1.Controls.Add(tb_supplierNo);
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel1.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(319, 54);
			flowLayoutPanel1.TabIndex = 25;
			tb_supplierNo.Cursor = System.Windows.Forms.Cursors.No;
			tb_supplierNo.Enabled = false;
			tb_supplierNo.Location = new System.Drawing.Point(10, 10);
			tb_supplierNo.Margin = new System.Windows.Forms.Padding(10, 10, 3, 10);
			tb_supplierNo.Name = "tb_supplierNo";
			tb_supplierNo.ReadOnly = true;
			tb_supplierNo.Size = new System.Drawing.Size(297, 33);
			tb_supplierNo.TabIndex = 90;
			flowLayoutPanel3.Controls.Add(tb_TelNo);
			flowLayoutPanel3.Controls.Add(tb_TelExt);
			flowLayoutPanel3.Location = new System.Drawing.Point(164, 111);
			flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel3.Name = "flowLayoutPanel3";
			flowLayoutPanel3.Size = new System.Drawing.Size(319, 54);
			flowLayoutPanel3.TabIndex = 27;
			tb_TelNo.Location = new System.Drawing.Point(10, 10);
			tb_TelNo.Margin = new System.Windows.Forms.Padding(10);
			tb_TelNo.Name = "tb_TelNo";
			tb_TelNo.Size = new System.Drawing.Size(212, 33);
			tb_TelNo.TabIndex = 5;
			tb_TelExt.Location = new System.Drawing.Point(242, 10);
			tb_TelExt.Margin = new System.Windows.Forms.Padding(10);
			tb_TelExt.Name = "tb_TelExt";
			tb_TelExt.Size = new System.Drawing.Size(67, 33);
			tb_TelExt.TabIndex = 5;
			panel15.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel15.Controls.Add(label5);
			panel15.Controls.Add(label23);
			panel15.Dock = System.Windows.Forms.DockStyle.Fill;
			panel15.Location = new System.Drawing.Point(1, 56);
			panel15.Margin = new System.Windows.Forms.Padding(0);
			panel15.Name = "panel15";
			panel15.Size = new System.Drawing.Size(162, 54);
			panel15.TabIndex = 24;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Red;
			label5.Location = new System.Drawing.Point(69, 16);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(17, 21);
			label5.TabIndex = 1;
			label5.Text = "*";
			label23.AutoSize = true;
			label23.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label23.ForeColor = System.Drawing.Color.White;
			label23.Location = new System.Drawing.Point(83, 17);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(74, 21);
			label23.TabIndex = 90;
			label23.Text = "廠商名稱";
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label2);
			panel6.Location = new System.Drawing.Point(1, 221);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(162, 54);
			panel6.TabIndex = 20;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(80, 14);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(74, 21);
			label2.TabIndex = 10;
			label2.Text = "行動電話";
			tb_DutyName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_DutyName.Location = new System.Drawing.Point(657, 66);
			tb_DutyName.Margin = new System.Windows.Forms.Padding(10);
			tb_DutyName.Name = "tb_DutyName";
			tb_DutyName.Size = new System.Drawing.Size(299, 33);
			tb_DutyName.TabIndex = 2;
			panel8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel8.Controls.Add(label14);
			panel8.Location = new System.Drawing.Point(1, 441);
			panel8.Margin = new System.Windows.Forms.Padding(0);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(162, 61);
			panel8.TabIndex = 20;
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label14.ForeColor = System.Drawing.Color.White;
			label14.Location = new System.Drawing.Point(80, 17);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(74, 21);
			label14.TabIndex = 10;
			label14.Text = "廠商類型";
			panel14.Controls.Add(cb_DeliveryType);
			panel14.Controls.Add(cb_PurchaseType);
			panel14.Controls.Add(tb_addr);
			panel14.Controls.Add(cb_type);
			panel14.Location = new System.Drawing.Point(164, 441);
			panel14.Margin = new System.Windows.Forms.Padding(0);
			panel14.Name = "panel14";
			panel14.Size = new System.Drawing.Size(319, 61);
			panel14.TabIndex = 40;
			cb_DeliveryType.AutoSize = true;
			cb_DeliveryType.BackColor = System.Drawing.Color.White;
			cb_DeliveryType.Checked = true;
			cb_DeliveryType.CheckState = System.Windows.Forms.CheckState.Checked;
			cb_DeliveryType.Location = new System.Drawing.Point(238, 13);
			cb_DeliveryType.Name = "cb_DeliveryType";
			cb_DeliveryType.Size = new System.Drawing.Size(67, 28);
			cb_DeliveryType.TabIndex = 13;
			cb_DeliveryType.Text = "出貨";
			cb_DeliveryType.UseVisualStyleBackColor = false;
			cb_PurchaseType.AutoSize = true;
			cb_PurchaseType.Checked = true;
			cb_PurchaseType.CheckState = System.Windows.Forms.CheckState.Checked;
			cb_PurchaseType.Location = new System.Drawing.Point(170, 13);
			cb_PurchaseType.Name = "cb_PurchaseType";
			cb_PurchaseType.Size = new System.Drawing.Size(67, 28);
			cb_PurchaseType.TabIndex = 12;
			cb_PurchaseType.Text = "進貨";
			cb_PurchaseType.UseVisualStyleBackColor = true;
			tb_addr.Location = new System.Drawing.Point(14, 64);
			tb_addr.Margin = new System.Windows.Forms.Padding(0);
			tb_addr.Name = "tb_addr";
			tb_addr.Size = new System.Drawing.Size(603, 33);
			tb_addr.TabIndex = 9;
			cb_type.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_type.FormattingEnabled = true;
			cb_type.Location = new System.Drawing.Point(10, 11);
			cb_type.Margin = new System.Windows.Forms.Padding(10, 0, 3, 8);
			cb_type.Name = "cb_type";
			cb_type.Size = new System.Drawing.Size(143, 32);
			cb_type.TabIndex = 11;
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(label18);
			panel7.Location = new System.Drawing.Point(1, 386);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(162, 54);
			panel7.TabIndex = 20;
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label18.ForeColor = System.Drawing.Color.White;
			label18.Location = new System.Drawing.Point(83, 16);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(74, 21);
			label18.TabIndex = 90;
			label18.Text = "傳真機號";
			panel12.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel12.Controls.Add(label17);
			panel12.Dock = System.Windows.Forms.DockStyle.Fill;
			panel12.Location = new System.Drawing.Point(1, 276);
			panel12.Margin = new System.Windows.Forms.Padding(0);
			panel12.Name = "panel12";
			tableLayoutPanel1.SetRowSpan(panel12, 2);
			panel12.Size = new System.Drawing.Size(162, 109);
			panel12.TabIndex = 24;
			label17.AutoSize = true;
			label17.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label17.ForeColor = System.Drawing.Color.White;
			label17.Location = new System.Drawing.Point(112, 46);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(42, 21);
			label17.TabIndex = 90;
			label17.Text = "地址";
			tableLayoutPanel1.SetColumnSpan(flowLayoutPanel2, 3);
			flowLayoutPanel2.Controls.Add(cb_city);
			flowLayoutPanel2.Controls.Add(cb_area);
			flowLayoutPanel2.Controls.Add(tb_zipcode);
			flowLayoutPanel2.Controls.Add(tb_Address);
			flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel2.Location = new System.Drawing.Point(164, 276);
			flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel2.Name = "flowLayoutPanel2";
			tableLayoutPanel1.SetRowSpan(flowLayoutPanel2, 2);
			flowLayoutPanel2.Size = new System.Drawing.Size(802, 109);
			flowLayoutPanel2.TabIndex = 27;
			cb_city.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_city.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_city.FormattingEnabled = true;
			cb_city.Location = new System.Drawing.Point(10, 17);
			cb_city.Margin = new System.Windows.Forms.Padding(10, 0, 3, 3);
			cb_city.Name = "cb_city";
			cb_city.Size = new System.Drawing.Size(155, 32);
			cb_city.TabIndex = 11;
			cb_city.SelectedIndexChanged += new System.EventHandler(cb_city_SelectedIndexChanged);
			cb_area.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_area.FormattingEnabled = true;
			cb_area.Location = new System.Drawing.Point(178, 17);
			cb_area.Margin = new System.Windows.Forms.Padding(10, 0, 3, 3);
			cb_area.Name = "cb_area";
			cb_area.Size = new System.Drawing.Size(165, 32);
			cb_area.TabIndex = 11;
			cb_area.SelectedIndexChanged += new System.EventHandler(cb_area_SelectedIndexChanged);
			tb_zipcode.Enabled = false;
			tb_zipcode.Location = new System.Drawing.Point(356, 15);
			tb_zipcode.Margin = new System.Windows.Forms.Padding(10, 15, 10, 10);
			tb_zipcode.Name = "tb_zipcode";
			tb_zipcode.Size = new System.Drawing.Size(99, 33);
			tb_zipcode.TabIndex = 5;
			tb_Address.Location = new System.Drawing.Point(10, 61);
			tb_Address.Margin = new System.Windows.Forms.Padding(10, 3, 10, 10);
			tb_Address.Name = "tb_Address";
			tb_Address.Size = new System.Drawing.Size(781, 33);
			tb_Address.TabIndex = 5;
			tb_Mobile.Location = new System.Drawing.Point(174, 231);
			tb_Mobile.Margin = new System.Windows.Forms.Padding(10);
			tb_Mobile.Name = "tb_Mobile";
			tb_Mobile.Size = new System.Drawing.Size(299, 33);
			tb_Mobile.TabIndex = 5;
			cb_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_status.FormattingEnabled = true;
			cb_status.Location = new System.Drawing.Point(657, 457);
			cb_status.Margin = new System.Windows.Forms.Padding(10, 0, 3, 8);
			cb_status.Name = "cb_status";
			cb_status.Size = new System.Drawing.Size(299, 32);
			cb_status.TabIndex = 11;
			panel11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel11.Controls.Add(label6);
			panel11.Location = new System.Drawing.Point(484, 441);
			panel11.Margin = new System.Windows.Forms.Padding(0);
			panel11.Name = "panel11";
			panel11.Size = new System.Drawing.Size(162, 61);
			panel11.TabIndex = 42;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(111, 17);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(42, 21);
			label6.TabIndex = 10;
			label6.Text = "狀態";
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label9);
			panel5.Location = new System.Drawing.Point(484, 111);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			tableLayoutPanel1.SetRowSpan(panel5, 3);
			panel5.Size = new System.Drawing.Size(162, 164);
			panel5.TabIndex = 43;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label9.ForeColor = System.Drawing.Color.White;
			label9.Location = new System.Drawing.Point(79, 71);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(74, 21);
			label9.TabIndex = 91;
			label9.Text = "營業資訊";
			panel16.Controls.Add(label15);
			panel16.Controls.Add(btn_checkVendorID);
			panel16.Controls.Add(tb_vendorId);
			panel16.Controls.Add(tb_vendorName);
			panel16.Controls.Add(label11);
			panel16.Controls.Add(label10);
			panel16.Location = new System.Drawing.Point(650, 114);
			panel16.Name = "panel16";
			tableLayoutPanel1.SetRowSpan(panel16, 3);
			panel16.Size = new System.Drawing.Size(313, 158);
			panel16.TabIndex = 44;
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label15.Location = new System.Drawing.Point(10, 89);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(287, 57);
			label15.TabIndex = 5;
			label15.Text = "請使用 販賣執照號碼線上查詢 確認廠商執\r\n照號碼請注意，農藥商品的出貨廠商必需\r\n驗證其商業執照號碼與名稱。";
			btn_checkVendorID.Location = new System.Drawing.Point(224, 9);
			btn_checkVendorID.Name = "btn_checkVendorID";
			btn_checkVendorID.Size = new System.Drawing.Size(75, 72);
			btn_checkVendorID.TabIndex = 4;
			btn_checkVendorID.Text = "檢查";
			btn_checkVendorID.UseVisualStyleBackColor = true;
			btn_checkVendorID.Click += new System.EventHandler(btn_checkVendorID_Click);
			tb_vendorId.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_vendorId.Location = new System.Drawing.Point(101, 9);
			tb_vendorId.MaxLength = 6;
			tb_vendorId.Name = "tb_vendorId";
			tb_vendorId.Size = new System.Drawing.Size(113, 33);
			tb_vendorId.TabIndex = 3;
			tb_vendorName.Enabled = false;
			tb_vendorName.Location = new System.Drawing.Point(101, 48);
			tb_vendorName.Name = "tb_vendorName";
			tb_vendorName.Size = new System.Drawing.Size(113, 33);
			tb_vendorName.TabIndex = 2;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(4, 51);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(105, 24);
			label11.TabIndex = 1;
			label11.Text = "商業名稱：";
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(4, 15);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(105, 24);
			label10.TabIndex = 0;
			label10.Text = "執照號碼：";
			panel17.Controls.Add(tb_ContactJob);
			panel17.Controls.Add(tb_ContactName);
			panel17.Controls.Add(label19);
			panel17.Location = new System.Drawing.Point(167, 169);
			panel17.Name = "panel17";
			panel17.Size = new System.Drawing.Size(313, 48);
			panel17.TabIndex = 45;
			tb_ContactJob.Location = new System.Drawing.Point(197, 9);
			tb_ContactJob.Margin = new System.Windows.Forms.Padding(10);
			tb_ContactJob.Name = "tb_ContactJob";
			tb_ContactJob.Size = new System.Drawing.Size(109, 33);
			tb_ContactJob.TabIndex = 5;
			tb_ContactName.Location = new System.Drawing.Point(7, 9);
			tb_ContactName.Margin = new System.Windows.Forms.Padding(10);
			tb_ContactName.Name = "tb_ContactName";
			tb_ContactName.Size = new System.Drawing.Size(130, 33);
			tb_ContactName.TabIndex = 5;
			label19.AutoSize = true;
			label19.Location = new System.Drawing.Point(137, 13);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(67, 24);
			label19.TabIndex = 6;
			label19.Text = "職稱：";
			tb_SupplierIdNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SupplierIdNo.Location = new System.Drawing.Point(657, 11);
			tb_SupplierIdNo.Margin = new System.Windows.Forms.Padding(10);
			tb_SupplierIdNo.Name = "tb_SupplierIdNo";
			tb_SupplierIdNo.Size = new System.Drawing.Size(299, 33);
			tb_SupplierIdNo.TabIndex = 2;
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
			btn_back.Location = new System.Drawing.Point(890, 39);
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
			base.Name = "frmNewSupplier";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmNewSupplier_Load);
			base.Controls.SetChildIndex(tabControl1, 0);
			base.Controls.SetChildIndex(btn_back, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			tabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel9.ResumeLayout(false);
			panel9.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel10.ResumeLayout(false);
			panel10.PerformLayout();
			panel13.ResumeLayout(false);
			panel13.PerformLayout();
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			flowLayoutPanel3.ResumeLayout(false);
			flowLayoutPanel3.PerformLayout();
			panel15.ResumeLayout(false);
			panel15.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			panel8.ResumeLayout(false);
			panel8.PerformLayout();
			panel14.ResumeLayout(false);
			panel14.PerformLayout();
			panel7.ResumeLayout(false);
			panel7.PerformLayout();
			panel12.ResumeLayout(false);
			panel12.PerformLayout();
			flowLayoutPanel2.ResumeLayout(false);
			flowLayoutPanel2.PerformLayout();
			panel11.ResumeLayout(false);
			panel11.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel16.ResumeLayout(false);
			panel16.PerformLayout();
			panel17.ResumeLayout(false);
			panel17.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			ResumeLayout(false);
		}
	}
}
