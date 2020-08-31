using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmNewCommodity : MasterThinForm
	{
		private string _defaultGDSNO = "";

		private string barcodeTemp = "";

		private IContainer components;

		private Label label3;

		private Label label4;

		private Button btn_back;

		private Panel panel2;

		private TabControl tabControl;

		private TabPage BasicData;

		private Button btn_cancel;

		private Button btn_save;

		private TableLayoutPanel tableLayoutPanel1;

		private TextBox tb_Price;

		private TextBox tb_Cost;

		private TextBox tb_SubsidyMoney;

		private TextBox tb_spec;

		private TextBox tb_domManufId;

		private Panel panel11;

		private Label label15;

		private Panel panel9;

		private Label label7;

		private Panel panel8;

		private Label label16;

		private Panel panel7;

		private Label label14;

		private Panel panel6;

		private Label label12;

		private Panel panel3;

		private Label label6;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private Panel panel4;

		private Label label8;

		private Panel panel5;

		private Label label10;

		private Panel panel10;

		private Label label11;

		private Label label13;

		private Panel panel12;

		private Label label17;

		private Panel panel13;

		private Label label18;

		private FlowLayoutPanel flowLayoutPanel1;

		private Panel panel15;

		private Label label23;

		private Label label5;

		private Label label21;

		private Panel panel14;

		private Label label9;

		private Panel panel18;

		private Label label34;

		private Label label31;

		private Panel panel19;

		private Label label32;

		private Panel panel20;

		private Label label20;

		private Panel panel21;

		private Label label22;

		private TextBox tb_GDNAME;

		private TextBox tb_CName;

		private TextBox tb_barcode;

		private TextBox tb_barndName;

		private TextBox tb_EName;

		private TextBox tb_contents;

		private ComboBox cb_CLA1NO;

		private TextBox tb_formCode;

		private TextBox tb_capacity;

		private ComboBox cb_status;

		private TextBox tb_GDSNO;

		private MyCheckBox cb_useCustomBarcode;

		private FlowLayoutPanel flowLayoutPanel2;

		private MyCheckBox cb_HighlyToxic;

		private MyCheckBox cb_SubsidyFertilizer;

		private Panel panel16;

		private TextBox textBox1;

		private Label label19;

		private ComboBox cb_dataType;

		public frmNewCommodity()
			: base("新增商品")
		{
			InitializeComponent();
		}

		private void frmNewCommodity_Load(object sender, EventArgs e)
		{
			tb_GDSNO.Text = (_defaultGDSNO = getNewGDSNO());
			cb_dataType.Items.Add(new ComboboxItem("自建", "N"));
			cb_dataType.Items.Add(new ComboboxItem("介接", "Y"));
			cb_dataType.SelectedIndex = 0;
			cb_CLA1NO.Items.Add(new ComboboxItem("請選擇", "-1"));
			cb_CLA1NO.Items.Add(new ComboboxItem("農藥", "0302"));
			cb_CLA1NO.Items.Add(new ComboboxItem("肥料", "0303"));
			cb_CLA1NO.Items.Add(new ComboboxItem("資材", "0305"));
			cb_CLA1NO.Items.Add(new ComboboxItem("其他", "0308"));
			cb_CLA1NO.SelectedIndex = 0;
			cb_status.Items.Add(new ComboboxItem("請選擇", "-1"));
			cb_status.Items.Add(new ComboboxItem("未使用", "N"));
			cb_status.Items.Add(new ComboboxItem("使用中", "U"));
			cb_status.Items.Add(new ComboboxItem("停用", "S"));
			cb_status.Items.Add(new ComboboxItem("禁用", "D"));
			cb_status.SelectedIndex = 2;
		}

		public static string getNewGDSNO()
		{
			string sql = "SELECT GDSNO FROM hypos_GOODSLST where GDSNO like 'G%' and length(GDSNO)=13 order by GDSNO desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			string text2 = DateTime.Now.Year.ToString().Substring(2, 2);
			if ("-1".Equals(text))
			{
				return string.Format("G{0}{1}00000001", Program.SiteNo, text2);
			}
			if (text.Length != 13)
			{
				return string.Format("G{0}{1}00000001", Program.SiteNo, text2);
			}
			string value = text.Substring(3, 2);
			if (!text2.Equals(value))
			{
				return string.Format("G{0}{1}00000001", Program.SiteNo, text2);
			}
			string arg = string.Format("{0:00000000}", int.Parse(text.Substring(5, 8)) + 1);
			return string.Format("G{0}{1}{2}", Program.SiteNo, text2, arg);
		}

		private void btn_back_Click(object sender, EventArgs e)
		{
			backToPreviousForm();
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			switchForm(new frmCommodityMangement());
		}

		private void btn_save_Click(object sender, EventArgs e)
		{
			string text = "";
			if (cb_useCustomBarcode.Checked && string.IsNullOrEmpty(tb_barcode.Text))
			{
				text += "「商品條碼」必填，請檢查\n";
			}
			if (string.IsNullOrEmpty(tb_GDNAME.Text))
			{
				text += "「商品名稱」必填，請檢查\n";
			}
			if ("-1".Equals((cb_CLA1NO.SelectedItem as ComboboxItem).Value.ToString()))
			{
				text += "請選擇商品類型\n";
			}
			if ("-1".Equals((cb_status.SelectedItem as ComboboxItem).Value.ToString()))
			{
				text += "請選擇狀態\n";
			}
			if (!"".Equals(text))
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			string text2 = tb_GDSNO.Text;
			string text3 = "";
			if (cb_useCustomBarcode.Checked)
			{
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_GOODSLST", "barcode = {0}", "", null, new string[1]
				{
					tb_barcode.Text
				}, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					AutoClosingMessageBox.Show("此商品條碼已建有資料，無法重複建立商品\n請透過商品查詢功能、檢查此條碼商品");
					return;
				}
				text2 = tb_barcode.Text;
				text3 = tb_barcode.Text;
			}
			try
			{
				string sql = " select hot_key from hypos_GOODSLST where  hot_key = {0} and hot_key !='' ";
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[1]
				{
					textBox1.Text
				}, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					MessageBox.Show("此商品快捷鍵已有商品使用");
					return;
				}
				string[,] strFieldArray = new string[20, 2]
				{
					{
						"GDSNO",
						text2
					},
					{
						"barcode",
						text3
					},
					{
						"domManufId",
						tb_domManufId.Text
					},
					{
						"ISWS",
						(cb_dataType.SelectedItem as ComboboxItem).Value.ToString()
					},
					{
						"GDName",
						tb_GDNAME.Text
					},
					{
						"brandName",
						tb_barndName.Text
					},
					{
						"CName",
						tb_CName.Text
					},
					{
						"EName",
						tb_EName.Text
					},
					{
						"formCode",
						tb_formCode.Text
					},
					{
						"contents",
						tb_contents.Text
					},
					{
						"spec",
						tb_spec.Text
					},
					{
						"capacity",
						tb_capacity.Text
					},
					{
						"CLA1NO",
						(cb_CLA1NO.SelectedItem as ComboboxItem).Value.ToString()
					},
					{
						"HighlyToxic",
						cb_HighlyToxic.Checked ? "Y" : "N"
					},
					{
						"SubsidyFertilizer",
						cb_SubsidyFertilizer.Checked ? "Y" : "N"
					},
					{
						"Cost",
						tb_Cost.Text
					},
					{
						"Price",
						tb_Price.Text
					},
					{
						"SubsidyMoney",
						tb_SubsidyMoney.Text
					},
					{
						"status",
						(cb_status.SelectedItem as ComboboxItem).Value.ToString()
					},
					{
						"hot_key",
						textBox1.Text
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_GOODSLST", null, null, strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				if (MessageBox.Show("商品新建成功，是否繼續編輯進階資料？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					switchForm(new frmEditCommodity(tb_GDSNO.Text, 1));
				}
				else
				{
					backToPreviousForm();
				}
			}
			catch (Exception)
			{
			}
		}

		private void cb_useCustomBarcode_CheckedChanged(object sender, EventArgs e)
		{
			if (cb_useCustomBarcode.Checked)
			{
				AutoClosingMessageBox.Show("使用商品條碼做為店內碼。\n請確定輸入的條碼必須是唯一的商品");
				tb_GDSNO.Text = tb_barcode.Text;
			}
			else
			{
				tb_GDSNO.Text = _defaultGDSNO;
			}
		}

		private void tb_GDSNO2_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b'));
		}

		private void digitOnly_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b'));
		}

		private void tb_GDSNO2_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
			{
				barcodeTemp += "0";
			}
			else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
			{
				barcodeTemp += "1";
			}
			else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
			{
				barcodeTemp += "2";
			}
			else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
			{
				barcodeTemp += "3";
			}
			else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
			{
				barcodeTemp += "4";
			}
			else if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
			{
				barcodeTemp += "5";
			}
			else if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
			{
				barcodeTemp += "6";
			}
			else if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
			{
				barcodeTemp += "7";
			}
			else if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
			{
				barcodeTemp += "8";
			}
			else if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
			{
				barcodeTemp += "9";
			}
			else if (e.KeyCode == Keys.Back)
			{
				barcodeTemp = "";
				tb_barcode.Text = "";
				if (cb_useCustomBarcode.Checked)
				{
					tb_GDSNO.Text = "";
				}
			}
			if (cb_useCustomBarcode.Checked)
			{
				tb_GDSNO.Text = barcodeTemp;
			}
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b') && !char.IsUpper(e.KeyChar) && !char.IsLower(e.KeyChar));
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
			panel2 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			btn_back = new System.Windows.Forms.Button();
			tabControl = new System.Windows.Forms.TabControl();
			BasicData = new System.Windows.Forms.TabPage();
			btn_cancel = new System.Windows.Forms.Button();
			btn_save = new System.Windows.Forms.Button();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			panel16 = new System.Windows.Forms.Panel();
			label11 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			tb_Price = new System.Windows.Forms.TextBox();
			tb_Cost = new System.Windows.Forms.TextBox();
			tb_spec = new System.Windows.Forms.TextBox();
			tb_domManufId = new System.Windows.Forms.TextBox();
			panel11 = new System.Windows.Forms.Panel();
			label15 = new System.Windows.Forms.Label();
			panel9 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			panel8 = new System.Windows.Forms.Panel();
			label16 = new System.Windows.Forms.Label();
			panel7 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label21 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label10 = new System.Windows.Forms.Label();
			panel10 = new System.Windows.Forms.Panel();
			label19 = new System.Windows.Forms.Label();
			panel12 = new System.Windows.Forms.Panel();
			label17 = new System.Windows.Forms.Label();
			panel13 = new System.Windows.Forms.Panel();
			label18 = new System.Windows.Forms.Label();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			tb_GDSNO = new System.Windows.Forms.TextBox();
			cb_useCustomBarcode = new POS_Client.MyCheckBox();
			panel15 = new System.Windows.Forms.Panel();
			label23 = new System.Windows.Forms.Label();
			panel14 = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			panel18 = new System.Windows.Forms.Panel();
			label34 = new System.Windows.Forms.Label();
			label31 = new System.Windows.Forms.Label();
			panel19 = new System.Windows.Forms.Panel();
			label32 = new System.Windows.Forms.Label();
			panel20 = new System.Windows.Forms.Panel();
			label20 = new System.Windows.Forms.Label();
			panel21 = new System.Windows.Forms.Panel();
			label22 = new System.Windows.Forms.Label();
			tb_GDNAME = new System.Windows.Forms.TextBox();
			tb_CName = new System.Windows.Forms.TextBox();
			tb_barcode = new System.Windows.Forms.TextBox();
			tb_barndName = new System.Windows.Forms.TextBox();
			tb_EName = new System.Windows.Forms.TextBox();
			tb_contents = new System.Windows.Forms.TextBox();
			cb_CLA1NO = new System.Windows.Forms.ComboBox();
			tb_formCode = new System.Windows.Forms.TextBox();
			tb_capacity = new System.Windows.Forms.TextBox();
			cb_status = new System.Windows.Forms.ComboBox();
			flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			cb_HighlyToxic = new POS_Client.MyCheckBox();
			cb_SubsidyFertilizer = new POS_Client.MyCheckBox();
			tb_SubsidyMoney = new System.Windows.Forms.TextBox();
			textBox1 = new System.Windows.Forms.TextBox();
			cb_dataType = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel2.SuspendLayout();
			tabControl.SuspendLayout();
			BasicData.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			panel16.SuspendLayout();
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
			panel15.SuspendLayout();
			panel14.SuspendLayout();
			panel18.SuspendLayout();
			panel19.SuspendLayout();
			panel20.SuspendLayout();
			panel21.SuspendLayout();
			flowLayoutPanel2.SuspendLayout();
			SuspendLayout();
			panel2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
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
			label4.TabIndex = 0;
			label4.Text = "會員姓名";
			btn_back.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_back.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_back.ForeColor = System.Drawing.Color.White;
			btn_back.Location = new System.Drawing.Point(890, 47);
			btn_back.Name = "btn_back";
			btn_back.Size = new System.Drawing.Size(77, 28);
			btn_back.TabIndex = 3;
			btn_back.Text = "返回前頁";
			btn_back.UseVisualStyleBackColor = false;
			btn_back.Click += new System.EventHandler(btn_back_Click);
			tabControl.Controls.Add(BasicData);
			tabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			tabControl.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tabControl.Location = new System.Drawing.Point(0, 39);
			tabControl.Name = "tabControl";
			tabControl.Padding = new System.Drawing.Point(15, 10);
			tabControl.SelectedIndex = 0;
			tabControl.Size = new System.Drawing.Size(981, 622);
			tabControl.TabIndex = 54;
			BasicData.Controls.Add(btn_cancel);
			BasicData.Controls.Add(btn_save);
			BasicData.Controls.Add(tableLayoutPanel1);
			BasicData.Location = new System.Drawing.Point(4, 47);
			BasicData.Name = "BasicData";
			BasicData.Padding = new System.Windows.Forms.Padding(3);
			BasicData.Size = new System.Drawing.Size(973, 571);
			BasicData.TabIndex = 0;
			BasicData.Text = "基本資料";
			BasicData.UseVisualStyleBackColor = true;
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(544, 533);
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
			btn_save.Location = new System.Drawing.Point(408, 535);
			btn_save.Name = "btn_save";
			btn_save.Size = new System.Drawing.Size(103, 32);
			btn_save.TabIndex = 1;
			btn_save.Text = "儲存變更";
			btn_save.UseVisualStyleBackColor = false;
			btn_save.Click += new System.EventHandler(btn_save_Click);
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 4;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.Controls.Add(panel16, 0, 9);
			tableLayoutPanel1.Controls.Add(tb_Price, 3, 7);
			tableLayoutPanel1.Controls.Add(tb_Cost, 1, 7);
			tableLayoutPanel1.Controls.Add(tb_spec, 1, 5);
			tableLayoutPanel1.Controls.Add(tb_domManufId, 1, 1);
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
			tableLayoutPanel1.Controls.Add(panel15, 0, 1);
			tableLayoutPanel1.Controls.Add(panel14, 0, 8);
			tableLayoutPanel1.Controls.Add(panel18, 2, 8);
			tableLayoutPanel1.Controls.Add(panel19, 2, 4);
			tableLayoutPanel1.Controls.Add(panel20, 2, 3);
			tableLayoutPanel1.Controls.Add(panel21, 2, 2);
			tableLayoutPanel1.Controls.Add(tb_GDNAME, 1, 2);
			tableLayoutPanel1.Controls.Add(tb_CName, 1, 3);
			tableLayoutPanel1.Controls.Add(tb_barcode, 3, 0);
			tableLayoutPanel1.Controls.Add(tb_barndName, 3, 2);
			tableLayoutPanel1.Controls.Add(tb_EName, 3, 3);
			tableLayoutPanel1.Controls.Add(tb_contents, 3, 4);
			tableLayoutPanel1.Controls.Add(cb_CLA1NO, 1, 6);
			tableLayoutPanel1.Controls.Add(tb_formCode, 1, 4);
			tableLayoutPanel1.Controls.Add(tb_capacity, 3, 5);
			tableLayoutPanel1.Controls.Add(cb_status, 3, 8);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 3, 6);
			tableLayoutPanel1.Controls.Add(tb_SubsidyMoney, 1, 8);
			tableLayoutPanel1.Controls.Add(textBox1, 3, 1);
			tableLayoutPanel1.Controls.Add(cb_dataType, 1, 9);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 10;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.01001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.909912f));
			tableLayoutPanel1.Size = new System.Drawing.Size(967, 524);
			tableLayoutPanel1.TabIndex = 0;
			panel16.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel16.Controls.Add(label11);
			panel16.Controls.Add(label13);
			panel16.Dock = System.Windows.Forms.DockStyle.Fill;
			panel16.Location = new System.Drawing.Point(1, 469);
			panel16.Margin = new System.Windows.Forms.Padding(0);
			panel16.Name = "panel16";
			panel16.Size = new System.Drawing.Size(162, 54);
			panel16.TabIndex = 42;
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label11.ForeColor = System.Drawing.Color.Red;
			label11.Location = new System.Drawing.Point(53, 17);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(17, 21);
			label11.TabIndex = 1;
			label11.Text = "*";
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.White;
			label13.Location = new System.Drawing.Point(70, 17);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(74, 21);
			label13.TabIndex = 0;
			label13.Text = "資料類型";
			tb_Price.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_Price.Location = new System.Drawing.Point(657, 374);
			tb_Price.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_Price.Name = "tb_Price";
			tb_Price.Size = new System.Drawing.Size(299, 33);
			tb_Price.TabIndex = 38;
			tb_Price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(digitOnly_KeyPress);
			tb_Cost.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_Cost.Location = new System.Drawing.Point(174, 374);
			tb_Cost.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_Cost.Name = "tb_Cost";
			tb_Cost.Size = new System.Drawing.Size(299, 33);
			tb_Cost.TabIndex = 37;
			tb_Cost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(digitOnly_KeyPress);
			tb_spec.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_spec.Location = new System.Drawing.Point(174, 270);
			tb_spec.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_spec.Name = "tb_spec";
			tb_spec.Size = new System.Drawing.Size(299, 33);
			tb_spec.TabIndex = 33;
			tb_domManufId.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_domManufId.Location = new System.Drawing.Point(174, 63);
			tb_domManufId.Margin = new System.Windows.Forms.Padding(10);
			tb_domManufId.MaxLength = 5;
			tb_domManufId.Name = "tb_domManufId";
			tb_domManufId.Size = new System.Drawing.Size(299, 33);
			tb_domManufId.TabIndex = 32;
			panel11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel11.Controls.Add(label15);
			panel11.Dock = System.Windows.Forms.DockStyle.Fill;
			panel11.Location = new System.Drawing.Point(484, 261);
			panel11.Margin = new System.Windows.Forms.Padding(0);
			panel11.Name = "panel11";
			panel11.Size = new System.Drawing.Size(162, 51);
			panel11.TabIndex = 23;
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label15.ForeColor = System.Drawing.Color.White;
			label15.Location = new System.Drawing.Point(44, 17);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(113, 21);
			label15.TabIndex = 0;
			label15.Text = "容量/單位重量";
			panel9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel9.Controls.Add(label7);
			panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			panel9.Location = new System.Drawing.Point(484, 1);
			panel9.Margin = new System.Windows.Forms.Padding(0);
			panel9.Name = "panel9";
			panel9.Size = new System.Drawing.Size(162, 51);
			panel9.TabIndex = 20;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(83, 16);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(74, 21);
			label7.TabIndex = 0;
			label7.Text = "商品條碼";
			panel8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel8.Controls.Add(label16);
			panel8.Location = new System.Drawing.Point(1, 365);
			panel8.Margin = new System.Windows.Forms.Padding(0);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(162, 51);
			panel8.TabIndex = 20;
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label16.ForeColor = System.Drawing.Color.White;
			label16.Location = new System.Drawing.Point(74, 16);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(86, 21);
			label16.TabIndex = 0;
			label16.Text = "定價(原價)";
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(label5);
			panel7.Controls.Add(label14);
			panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			panel7.Location = new System.Drawing.Point(1, 313);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(162, 51);
			panel7.TabIndex = 20;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Red;
			label5.Location = new System.Drawing.Point(74, 16);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(17, 21);
			label5.TabIndex = 1;
			label5.Text = "*";
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label14.ForeColor = System.Drawing.Color.White;
			label14.Location = new System.Drawing.Point(86, 16);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(74, 21);
			label14.TabIndex = 0;
			label14.Text = "商品類型";
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			panel6.Location = new System.Drawing.Point(1, 261);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(162, 51);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(112, 17);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(42, 21);
			label12.TabIndex = 0;
			label12.Text = "容器";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label21);
			panel3.Controls.Add(label6);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 105);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(162, 51);
			panel3.TabIndex = 21;
			label21.AutoSize = true;
			label21.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label21.ForeColor = System.Drawing.Color.Red;
			label21.Location = new System.Drawing.Point(70, 16);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(17, 21);
			label21.TabIndex = 1;
			label21.Text = "*";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(83, 16);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(74, 21);
			label6.TabIndex = 0;
			label6.Text = "商品名稱";
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(1, 1);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(162, 51);
			panel1.TabIndex = 19;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.Red;
			label2.Location = new System.Drawing.Point(83, 17);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(17, 21);
			label2.TabIndex = 1;
			label2.Text = "*";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(96, 17);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(58, 21);
			label1.TabIndex = 0;
			label1.Text = "店內碼";
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label8);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Location = new System.Drawing.Point(1, 157);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(162, 51);
			panel4.TabIndex = 22;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.White;
			label8.Location = new System.Drawing.Point(48, 16);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(106, 21);
			label8.TabIndex = 0;
			label8.Text = "中文普通名稱";
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label10);
			panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			panel5.Location = new System.Drawing.Point(1, 209);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(162, 51);
			panel5.TabIndex = 23;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(112, 13);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(42, 21);
			label10.TabIndex = 0;
			label10.Text = "劑型";
			panel10.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel10.Controls.Add(label19);
			panel10.Dock = System.Windows.Forms.DockStyle.Fill;
			panel10.Location = new System.Drawing.Point(484, 53);
			panel10.Margin = new System.Windows.Forms.Padding(0);
			panel10.Name = "panel10";
			panel10.Size = new System.Drawing.Size(162, 51);
			panel10.TabIndex = 24;
			label19.AutoSize = true;
			label19.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label19.ForeColor = System.Drawing.Color.White;
			label19.Location = new System.Drawing.Point(70, 21);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(90, 21);
			label19.TabIndex = 1;
			label19.Text = "商品快捷鍵";
			panel12.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel12.Controls.Add(label17);
			panel12.Dock = System.Windows.Forms.DockStyle.Fill;
			panel12.Location = new System.Drawing.Point(484, 313);
			panel12.Margin = new System.Windows.Forms.Padding(0);
			panel12.Name = "panel12";
			panel12.Size = new System.Drawing.Size(162, 51);
			panel12.TabIndex = 24;
			label17.AutoSize = true;
			label17.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label17.ForeColor = System.Drawing.Color.White;
			label17.Location = new System.Drawing.Point(83, 16);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(74, 21);
			label17.TabIndex = 0;
			label17.Text = "其他設定";
			panel13.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel13.Controls.Add(label18);
			panel13.Dock = System.Windows.Forms.DockStyle.Fill;
			panel13.Location = new System.Drawing.Point(484, 365);
			panel13.Margin = new System.Windows.Forms.Padding(0);
			panel13.Name = "panel13";
			panel13.Size = new System.Drawing.Size(162, 51);
			panel13.TabIndex = 24;
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label18.ForeColor = System.Drawing.Color.White;
			label18.Location = new System.Drawing.Point(115, 16);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(42, 21);
			label18.TabIndex = 0;
			label18.Text = "售價";
			flowLayoutPanel1.Controls.Add(tb_GDSNO);
			flowLayoutPanel1.Controls.Add(cb_useCustomBarcode);
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel1.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(319, 51);
			flowLayoutPanel1.TabIndex = 25;
			tb_GDSNO.Cursor = System.Windows.Forms.Cursors.No;
			tb_GDSNO.Enabled = false;
			tb_GDSNO.Location = new System.Drawing.Point(10, 10);
			tb_GDSNO.Margin = new System.Windows.Forms.Padding(10);
			tb_GDSNO.MaxLength = 13;
			tb_GDSNO.Name = "tb_GDSNO";
			tb_GDSNO.ReadOnly = true;
			tb_GDSNO.Size = new System.Drawing.Size(152, 33);
			tb_GDSNO.TabIndex = 2;
			tb_GDSNO.Text = "{0}";
			cb_useCustomBarcode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_useCustomBarcode.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_useCustomBarcode.Location = new System.Drawing.Point(175, 14);
			cb_useCustomBarcode.Name = "cb_useCustomBarcode";
			cb_useCustomBarcode.Size = new System.Drawing.Size(134, 24);
			cb_useCustomBarcode.TabIndex = 3;
			cb_useCustomBarcode.Text = "使用商品條碼";
			cb_useCustomBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			cb_useCustomBarcode.UseVisualStyleBackColor = true;
			cb_useCustomBarcode.CheckedChanged += new System.EventHandler(cb_useCustomBarcode_CheckedChanged);
			panel15.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel15.Controls.Add(label23);
			panel15.Dock = System.Windows.Forms.DockStyle.Fill;
			panel15.Location = new System.Drawing.Point(1, 53);
			panel15.Margin = new System.Windows.Forms.Padding(0);
			panel15.Name = "panel15";
			panel15.Size = new System.Drawing.Size(162, 51);
			panel15.TabIndex = 24;
			label23.AutoSize = true;
			label23.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label23.ForeColor = System.Drawing.Color.White;
			label23.Location = new System.Drawing.Point(70, 17);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(90, 21);
			label23.TabIndex = 0;
			label23.Text = "許可證字號";
			panel14.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel14.Controls.Add(label9);
			panel14.Dock = System.Windows.Forms.DockStyle.Fill;
			panel14.Location = new System.Drawing.Point(1, 417);
			panel14.Margin = new System.Windows.Forms.Padding(0);
			panel14.Name = "panel14";
			panel14.Size = new System.Drawing.Size(162, 51);
			panel14.TabIndex = 20;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label9.ForeColor = System.Drawing.Color.White;
			label9.Location = new System.Drawing.Point(76, 20);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(86, 21);
			label9.TabIndex = 0;
			label9.Text = "補助(金額)";
			panel18.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel18.Controls.Add(label34);
			panel18.Controls.Add(label31);
			panel18.Dock = System.Windows.Forms.DockStyle.Fill;
			panel18.Location = new System.Drawing.Point(484, 417);
			panel18.Margin = new System.Windows.Forms.Padding(0);
			panel18.Name = "panel18";
			panel18.Size = new System.Drawing.Size(162, 51);
			panel18.TabIndex = 20;
			label34.AutoSize = true;
			label34.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label34.ForeColor = System.Drawing.Color.Red;
			label34.Location = new System.Drawing.Point(101, 23);
			label34.Name = "label34";
			label34.Size = new System.Drawing.Size(17, 21);
			label34.TabIndex = 1;
			label34.Text = "*";
			label31.AutoSize = true;
			label31.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label31.ForeColor = System.Drawing.Color.White;
			label31.Location = new System.Drawing.Point(115, 23);
			label31.Name = "label31";
			label31.Size = new System.Drawing.Size(42, 21);
			label31.TabIndex = 0;
			label31.Text = "狀態";
			panel19.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel19.Controls.Add(label32);
			panel19.Location = new System.Drawing.Point(484, 209);
			panel19.Margin = new System.Windows.Forms.Padding(0);
			panel19.Name = "panel19";
			panel19.Size = new System.Drawing.Size(162, 51);
			panel19.TabIndex = 20;
			label32.AutoSize = true;
			label32.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label32.ForeColor = System.Drawing.Color.White;
			label32.Location = new System.Drawing.Point(115, 13);
			label32.Name = "label32";
			label32.Size = new System.Drawing.Size(42, 21);
			label32.TabIndex = 0;
			label32.Text = "含量";
			panel20.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel20.Controls.Add(label20);
			panel20.Location = new System.Drawing.Point(484, 157);
			panel20.Margin = new System.Windows.Forms.Padding(0);
			panel20.Name = "panel20";
			panel20.Size = new System.Drawing.Size(162, 51);
			panel20.TabIndex = 20;
			label20.AutoSize = true;
			label20.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label20.ForeColor = System.Drawing.Color.White;
			label20.Location = new System.Drawing.Point(51, 16);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(106, 21);
			label20.TabIndex = 0;
			label20.Text = "英文普通名稱";
			panel21.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel21.Controls.Add(label22);
			panel21.Location = new System.Drawing.Point(484, 105);
			panel21.Margin = new System.Windows.Forms.Padding(0);
			panel21.Name = "panel21";
			panel21.Size = new System.Drawing.Size(162, 51);
			panel21.TabIndex = 20;
			label22.AutoSize = true;
			label22.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label22.ForeColor = System.Drawing.Color.White;
			label22.Location = new System.Drawing.Point(85, 16);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(74, 21);
			label22.TabIndex = 0;
			label22.Text = "廠商名稱";
			tb_GDNAME.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_GDNAME.Location = new System.Drawing.Point(174, 114);
			tb_GDNAME.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_GDNAME.Name = "tb_GDNAME";
			tb_GDNAME.Size = new System.Drawing.Size(299, 33);
			tb_GDNAME.TabIndex = 38;
			tb_CName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_CName.Location = new System.Drawing.Point(174, 166);
			tb_CName.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_CName.MaxLength = 14;
			tb_CName.Name = "tb_CName";
			tb_CName.Size = new System.Drawing.Size(299, 33);
			tb_CName.TabIndex = 38;
			tb_barcode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_barcode.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_barcode.Location = new System.Drawing.Point(657, 11);
			tb_barcode.Margin = new System.Windows.Forms.Padding(10);
			tb_barcode.MaxLength = 13;
			tb_barcode.Name = "tb_barcode";
			tb_barcode.Size = new System.Drawing.Size(299, 33);
			tb_barcode.TabIndex = 32;
			tb_barcode.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_GDSNO2_KeyDown);
			tb_barcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tb_GDSNO2_KeyPress);
			tb_barndName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_barndName.Location = new System.Drawing.Point(657, 115);
			tb_barndName.Margin = new System.Windows.Forms.Padding(10);
			tb_barndName.Name = "tb_barndName";
			tb_barndName.Size = new System.Drawing.Size(299, 33);
			tb_barndName.TabIndex = 32;
			tb_EName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_EName.Location = new System.Drawing.Point(657, 167);
			tb_EName.Margin = new System.Windows.Forms.Padding(10);
			tb_EName.Name = "tb_EName";
			tb_EName.Size = new System.Drawing.Size(299, 33);
			tb_EName.TabIndex = 32;
			tb_contents.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_contents.Location = new System.Drawing.Point(657, 219);
			tb_contents.Margin = new System.Windows.Forms.Padding(10);
			tb_contents.Name = "tb_contents";
			tb_contents.Size = new System.Drawing.Size(299, 33);
			tb_contents.TabIndex = 32;
			cb_CLA1NO.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_CLA1NO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_CLA1NO.FormattingEnabled = true;
			cb_CLA1NO.Location = new System.Drawing.Point(174, 322);
			cb_CLA1NO.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			cb_CLA1NO.Name = "cb_CLA1NO";
			cb_CLA1NO.Size = new System.Drawing.Size(299, 32);
			cb_CLA1NO.TabIndex = 39;
			tb_formCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_formCode.Location = new System.Drawing.Point(174, 218);
			tb_formCode.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_formCode.Name = "tb_formCode";
			tb_formCode.Size = new System.Drawing.Size(299, 33);
			tb_formCode.TabIndex = 33;
			tb_capacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_capacity.Location = new System.Drawing.Point(657, 270);
			tb_capacity.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_capacity.Name = "tb_capacity";
			tb_capacity.Size = new System.Drawing.Size(299, 33);
			tb_capacity.TabIndex = 33;
			cb_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_status.FormattingEnabled = true;
			cb_status.Location = new System.Drawing.Point(657, 426);
			cb_status.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			cb_status.Name = "cb_status";
			cb_status.Size = new System.Drawing.Size(299, 32);
			cb_status.TabIndex = 39;
			flowLayoutPanel2.Controls.Add(cb_HighlyToxic);
			flowLayoutPanel2.Controls.Add(cb_SubsidyFertilizer);
			flowLayoutPanel2.Location = new System.Drawing.Point(647, 313);
			flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel2.Name = "flowLayoutPanel2";
			flowLayoutPanel2.Size = new System.Drawing.Size(319, 51);
			flowLayoutPanel2.TabIndex = 25;
			cb_HighlyToxic.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_HighlyToxic.Enabled = false;
			cb_HighlyToxic.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_HighlyToxic.Location = new System.Drawing.Point(10, 14);
			cb_HighlyToxic.Margin = new System.Windows.Forms.Padding(10, 14, 10, 10);
			cb_HighlyToxic.Name = "cb_HighlyToxic";
			cb_HighlyToxic.Size = new System.Drawing.Size(99, 24);
			cb_HighlyToxic.TabIndex = 3;
			cb_HighlyToxic.Text = "劇毒農藥";
			cb_HighlyToxic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			cb_HighlyToxic.UseVisualStyleBackColor = true;
			cb_SubsidyFertilizer.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_SubsidyFertilizer.Enabled = false;
			cb_SubsidyFertilizer.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_SubsidyFertilizer.Location = new System.Drawing.Point(129, 14);
			cb_SubsidyFertilizer.Margin = new System.Windows.Forms.Padding(10, 14, 10, 10);
			cb_SubsidyFertilizer.Name = "cb_SubsidyFertilizer";
			cb_SubsidyFertilizer.Size = new System.Drawing.Size(99, 24);
			cb_SubsidyFertilizer.TabIndex = 3;
			cb_SubsidyFertilizer.Text = "補助肥料";
			cb_SubsidyFertilizer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			cb_SubsidyFertilizer.UseVisualStyleBackColor = true;
			tb_SubsidyMoney.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SubsidyMoney.Enabled = false;
			tb_SubsidyMoney.Location = new System.Drawing.Point(174, 426);
			tb_SubsidyMoney.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_SubsidyMoney.Name = "tb_SubsidyMoney";
			tb_SubsidyMoney.Size = new System.Drawing.Size(299, 33);
			tb_SubsidyMoney.TabIndex = 36;
			tb_SubsidyMoney.KeyPress += new System.Windows.Forms.KeyPressEventHandler(digitOnly_KeyPress);
			textBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			textBox1.Location = new System.Drawing.Point(657, 62);
			textBox1.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			textBox1.MaxLength = 10;
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(299, 33);
			textBox1.TabIndex = 41;
			textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox1_KeyPress);
			cb_dataType.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_dataType.Enabled = false;
			cb_dataType.FormattingEnabled = true;
			cb_dataType.Location = new System.Drawing.Point(174, 484);
			cb_dataType.Margin = new System.Windows.Forms.Padding(10, 0, 3, 3);
			cb_dataType.Name = "cb_dataType";
			cb_dataType.Size = new System.Drawing.Size(299, 32);
			cb_dataType.TabIndex = 43;
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(btn_back);
			base.Controls.Add(tabControl);
			Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "frmNewCommodity";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmNewCommodity_Load);
			base.Controls.SetChildIndex(tabControl, 0);
			base.Controls.SetChildIndex(btn_back, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			tabControl.ResumeLayout(false);
			BasicData.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel16.ResumeLayout(false);
			panel16.PerformLayout();
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
			panel15.ResumeLayout(false);
			panel15.PerformLayout();
			panel14.ResumeLayout(false);
			panel14.PerformLayout();
			panel18.ResumeLayout(false);
			panel18.PerformLayout();
			panel19.ResumeLayout(false);
			panel19.PerformLayout();
			panel20.ResumeLayout(false);
			panel20.PerformLayout();
			panel21.ResumeLayout(false);
			panel21.PerformLayout();
			flowLayoutPanel2.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
