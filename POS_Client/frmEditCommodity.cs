using POS_Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmEditCommodity : MasterThinForm
	{
		private string _GDSNO = "";

		private string _GDNAME = "";

		private string _CName = "";

		private string _status = "";

		public string _Price = "0";

		public string _DeliveryPrice = "0";

		public string _DeliveryOpenPrice = "0";

		private string _strAdjustPlusOrMinus = "minus";

		private int comboBox4SelectedIndex = 99;

		private IContainer components;

		private Label label3;

		private Label label4;

		private Button btn_back;

		private Button btn_printBarcode;

		private Panel panel2;

		private TabControl DeliveryRecord;

		private TabPage AdvancedConfig;

		private TabPage InventoryManage;

		private TableLayoutPanel tableLayoutPanel2;

		private Panel panel22;

		private Label label33;

		private Panel panel29;

		private Label label43;

		private Label l_InventoryTotal;

		private Panel panel16;

		private Label label19;

		private TabPage ConsumeRecord;

		private Button button1;

		private Button btn_AdvancedConfig_save;

		private TableLayoutPanel tableLayoutPanel3;

		private Panel panel23;

		private Label label35;

		private Panel panel28;

		private Label label44;

		private FlowLayoutPanel flowLayoutPanel2;

		private Panel panel35;

		private Label label51;

		private TextBox tb_adjustCount;

		private ComboBox cb_adjustType;

		private FlowLayoutPanel flowLayoutPanel3;

		private MyCheckBox mCB_OpenPrice;

		private ComboBox comboBox4;

		private Label label42;

		private TabPage BasicData;

		private Button btn_cancel;

		private Button btn_BasicData_save;

		private TableLayoutPanel tableLayoutPanel1;

		private TextBox tb_Cost;

		private TextBox tb_spec;

		private TextBox tb_domManufId;

		private Panel panel11;

		private Label label15;

		private Panel panel9;

		private Label label7;

		private Panel panel8;

		private Label label16;

		private Panel panel7;

		private Label label5;

		private Label label14;

		private Panel panel6;

		private Label label12;

		private Panel panel3;

		private Label label21;

		private Label label6;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private Panel panel4;

		private Label label8;

		private Panel panel5;

		private Label label10;

		private Panel panel10;

		private Panel panel12;

		private Label label17;

		private Panel panel13;

		private Label label18;

		private FlowLayoutPanel flowLayoutPanel1;

		private TextBox tb_GDSNO;

		private MyCheckBox cb_useCustomBarcode;

		private Panel panel15;

		private Label label23;

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

		private TextBox tb_SubsidyMoney;

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

		private FlowLayoutPanel flowLayoutPanel6;

		private MyCheckBox cb_HighlyToxic;

		private MyCheckBox cb_SubsidyFertilizer;

		private ComboBox cb_dataType;

		private FlowLayoutPanel flowLayoutPanel7;

		private Button btn_UpdatePrice;

		public TextBox tb_Price;

		private Button btn_enter;

		private Label label24;

		private DataGridView dataGridView1;

		public TextBox tb_SpecialPrice1;

		public TextBox tb_SpecialPrice2;

		private Button btn_minus;

		private Button btn_plus;

		private TabPage tabPage1;

		private Label label26;

		private ComboBox comboBox1;

		private Label label25;

		private Label label30;

		private Label label29;

		private DateTimePicker dateTimePicker2;

		private Label label28;

		private DateTimePicker dateTimePicker3;

		private DataGridView dataGridView2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewLinkColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn Column7;

		private DataGridViewTextBoxColumn hidden_status;

		private Panel panel24;

		private Label label48;

		private RadioButton radioButton1;

		private Panel panel25;

		private RadioButton radioButton2;

		private Button btn_settingDeliveryPrice;

		private MyCheckBox myCheckBox1;

		private Label label47;

		public TextBox textBox1;

		private DateTimePicker dateTimePicker1;

		private Label label49;

		private DateTimePicker dateTimePicker4;

		private Label label50;

		private Label label46;

		private Label label52;

		private Label label53;

		private DateTimePicker dateTimePicker5;

		private Label label54;

		private DateTimePicker dateTimePicker6;

		private Label label55;

		private DataGridView dataGridView3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewLinkColumn dataGridViewLinkColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

		private DataGridViewTextBoxColumn hidden_status2;

		private TextBox tb_pesticideRestrictedName;

		private Panel panel17;

		private Label label27;

		private Panel panel26;

		private TextBox textBox2;

		private Label label36;

		private Label label37;

		private Label label11;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewLinkColumn Column5;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column6;

		public frmEditCommodity(string GDSNO)
			: base("商品管理")
		{
			InitializeComponent();
			_GDSNO = GDSNO;
			tb_GDSNO.Text = GDSNO;
		}

		public frmEditCommodity(string GDSNO, int tabIndex)
			: base("商品管理")
		{
			InitializeComponent();
			_GDSNO = GDSNO;
			tb_GDSNO.Text = GDSNO;
			DeliveryRecord.SelectedIndex = tabIndex;
		}

		private void frmEditCommodity_Load(object sender, EventArgs e)
		{
			ComboboxItem[] items = new ComboboxItem[1]
			{
				new ComboboxItem("店內盤點", "0")
			};
			cb_adjustType.Items.AddRange(items);
			cb_adjustType.SelectedIndex = 0;
			ComboboxItem[] items2 = new ComboboxItem[4]
			{
				new ComboboxItem("全部", "0"),
				new ComboboxItem("正常", "1"),
				new ComboboxItem("取消", "2"),
				new ComboboxItem("編修", "3")
			};
			comboBox4.Items.AddRange(items2);
			comboBox4.SelectedIndex = 0;
			ComboboxItem[] items3 = new ComboboxItem[4]
			{
				new ComboboxItem("全部", "9"),
				new ComboboxItem("正常", "0"),
				new ComboboxItem("取消", "1"),
				new ComboboxItem("編修", "2")
			};
			comboBox1.Items.AddRange(items3);
			comboBox1.SelectedIndex = 0;
			dateTimePicker5.Value = DateTime.Today.AddDays(-7.0);
			dateTimePicker6.Value = DateTime.Today;
			dateTimePicker1.Value = DateTime.Today.AddDays(-7.0);
			dateTimePicker4.Value = DateTime.Today;
			dateTimePicker2.Value = DateTime.Today.AddDays(-7.0);
			dateTimePicker3.Value = DateTime.Today;
			showInfoData();
			showInventoryManage();
			showSalesRecord(0);
			showDeliveryRecord();
		}

		private void showInventoryManage()
		{
			dataGridView1.Rows.Clear();
			string sql = "SELECT m.UpdateDate updateDate, '進貨' Type, s.SupplierName, m.PurchaseNo,        d.GDSNO GDSNO, Quantity adjustCount, d.GoodsTotalCountLog GoodsTotalCountLog   FROM hypos_PurchaseGoods_Master m,hypos_PurchaseGoods_Detail d, hypos_Supplier s      WHERE m.PurchaseNo = d.PurchaseNo and m.SupplierNo = s.SupplierNo and d.GDSNO = {0}   and m.UpdateDate between '" + dateTimePicker5.Value.ToString("yyyy-MM-dd") + "' and datetime(date( '" + dateTimePicker6.Value.ToString("yyyy-MM-dd") + "' ), '+1 days') UNION                                                                               SELECT updateDate,adjustType Type, '' SupplierName,'' PurchaseNo,                    GDSNO, adjustCount ,GoodsTotalCountLog                                         FROM hypos_InventoryAdjustment                                                        WHERE GDSNO = {0} and  updateDate between '" + dateTimePicker5.Value.ToString("yyyy-MM-dd") + "' and datetime(date( '" + dateTimePicker6.Value.ToString("yyyy-MM-dd") + "' ), '+1 days')   order by updateDate DESC";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[1]
			{
				_GDSNO
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				string text = "";
				switch (row["Type"].ToString())
				{
				case "A":
					text = "破損";
					break;
				case "B":
					text = "盤減";
					break;
				case "C":
					text = "盤增";
					break;
				case "E":
					text = "退回原廠";
					break;
				case "F":
					text = "過期退回";
					break;
				case "G":
					text = "過期銷毀";
					break;
				case "H":
					text = "資料異常";
					break;
				case "I":
					text = "破損回收";
					break;
				case "J":
					text = "劣農藥回收(含過期)";
					break;
				case "X":
					text = "其他";
					break;
				case "0":
					text = "店內盤點";
					break;
				default:
					text = row["Type"].ToString();
					break;
				}
				dataGridView1.Rows.Add(row["updateDate"].ToString(), text, row["SupplierName"].ToString(), row["PurchaseNo"].ToString(), row["adjustCount"].ToString(), row["GoodsTotalCountLog"].ToString());
			}
		}

		private void showInfoData()
		{
			ComboboxItem[] array = new ComboboxItem[2]
			{
				new ComboboxItem("自建", "N"),
				new ComboboxItem("介接", "Y")
			};
			cb_dataType.Items.AddRange(array);
			cb_dataType.SelectedIndex = 0;
			ComboboxItem[] array2 = new ComboboxItem[5]
			{
				new ComboboxItem("請選擇", "-1"),
				new ComboboxItem("農藥", "0302"),
				new ComboboxItem("肥料", "0303"),
				new ComboboxItem("資材", "0305"),
				new ComboboxItem("其他", "0308")
			};
			cb_CLA1NO.Items.AddRange(array2);
			cb_CLA1NO.SelectedIndex = 0;
			ComboboxItem[] array3 = new ComboboxItem[4]
			{
				new ComboboxItem("請選擇", "-1"),
				new ComboboxItem("未使用", "N"),
				new ComboboxItem("使用中", "U"),
				new ComboboxItem("停用", "S")
			};
			cb_status.Items.AddRange(array3);
			cb_status.SelectedIndex = 0;
			try
			{
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_GOODSLST", "GDSNO = {0}", "", null, new string[1]
				{
					_GDSNO
				}, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					_Price = dataTable.Rows[0]["Price"].ToString();
					tb_barcode.Text = dataTable.Rows[0]["barcode"].ToString();
					tb_domManufId.Text = dataTable.Rows[0]["domManufId"].ToString();
					ComboboxItem[] array4 = array;
					foreach (ComboboxItem comboboxItem in array4)
					{
						if (comboboxItem.Value.Equals(dataTable.Rows[0]["ISWS"].ToString()))
						{
							cb_dataType.SelectedItem = comboboxItem;
						}
					}
					_GDNAME = dataTable.Rows[0]["GDName"].ToString();
					_CName = dataTable.Rows[0]["CName"].ToString();
					tb_GDNAME.Text = _GDNAME;
					tb_barndName.Text = dataTable.Rows[0]["brandName"].ToString();
					tb_CName.Text = _CName;
					tb_EName.Text = dataTable.Rows[0]["EName"].ToString();
					tb_formCode.Text = dataTable.Rows[0]["formCode"].ToString();
					tb_contents.Text = dataTable.Rows[0]["contents"].ToString();
					tb_spec.Text = dataTable.Rows[0]["spec"].ToString();
					tb_capacity.Text = dataTable.Rows[0]["capacity"].ToString();
					l_InventoryTotal.Text = (string.IsNullOrEmpty(dataTable.Rows[0]["inventory"].ToString()) ? "0" : dataTable.Rows[0]["inventory"].ToString());
					textBox2.Text = dataTable.Rows[0]["hot_key"].ToString();
					if (dataTable.Rows[0]["status"].ToString() == "D")
					{
						cb_status.Enabled = false;
					}
					else
					{
						cb_status.Enabled = true;
					}
					string text = dataTable.Rows[0]["SpecialPrice1"].ToString();
					string text2 = dataTable.Rows[0]["SpecialPrice2"].ToString();
					if (string.IsNullOrEmpty(text))
					{
						text = "";
					}
					if (string.IsNullOrEmpty(text2))
					{
						text2 = "";
					}
					tb_SpecialPrice1.Text = text;
					tb_SpecialPrice2.Text = text2;
					if ("1".Equals(dataTable.Rows[0]["OpenPrice"].ToString()))
					{
						mCB_OpenPrice.Checked = true;
						mCB_OpenPrice.Refresh();
					}
					array4 = array2;
					foreach (ComboboxItem comboboxItem2 in array4)
					{
						if (comboboxItem2.Value.Equals(dataTable.Rows[0]["CLA1NO"].ToString()))
						{
							cb_CLA1NO.SelectedItem = comboboxItem2;
						}
					}
					cb_HighlyToxic.Checked = ("Y".Equals(dataTable.Rows[0]["HighlyToxic"].ToString()) ? true : false);
					cb_SubsidyFertilizer.Checked = ("Y".Equals(dataTable.Rows[0]["SubsidyFertilizer"].ToString()) ? true : false);
					tb_Cost.Text = dataTable.Rows[0]["Cost"].ToString();
					tb_Price.Text = dataTable.Rows[0]["Price"].ToString();
					tb_SubsidyMoney.Text = dataTable.Rows[0]["SubsidyMoney"].ToString();
					array4 = array3;
					foreach (ComboboxItem comboboxItem3 in array4)
					{
						if (comboboxItem3.Value.Equals(dataTable.Rows[0]["status"].ToString()))
						{
							cb_status.SelectedItem = comboboxItem3;
							_status = comboboxItem3.Value.ToString();
						}
					}
					if ("0".Equals(dataTable.Rows[0]["DeliveryPriceSetType"].ToString()))
					{
						radioButton1.Checked = true;
						radioButton2.Checked = false;
					}
					else
					{
						radioButton1.Checked = false;
						radioButton2.Checked = true;
						textBox1.Text = dataTable.Rows[0]["DeliveryPrice"].ToString();
						_DeliveryPrice = dataTable.Rows[0]["DeliveryPrice"].ToString();
						if ("1".Equals(dataTable.Rows[0]["DeliveryOpenPrice"].ToString()))
						{
							myCheckBox1.Checked = true;
							_DeliveryOpenPrice = "1";
						}
					}
				}
				else
				{
					MessageBox.Show("查無此店內碼: " + _GDSNO, "編修商品");
					backToPreviousForm();
				}
				if ("N".Equals(dataTable.Rows[0]["ISWS"].ToString()))
				{
					tb_Cost.Enabled = true;
					btn_UpdatePrice.Enabled = true;
					tb_barndName.Enabled = true;
					tb_domManufId.Enabled = true;
					tb_CName.Enabled = true;
					tb_EName.Enabled = true;
					tb_formCode.Enabled = true;
					tb_contents.Enabled = true;
					tb_spec.Enabled = true;
					tb_capacity.Enabled = true;
					tb_SubsidyMoney.Enabled = true;
				}
			}
			catch (Exception)
			{
				MessageBox.Show("店內碼異常: " + _GDSNO, "編修商品");
				switchForm(new frmMain());
			}
		}

		private void showSalesRecord(int salesRecordIndex)
		{
			try
			{
				dataGridView3.Rows.Clear();
				string text = "";
				switch (salesRecordIndex)
				{
				case 1:
					text = " and m.status='0'";
					break;
				case 2:
					text = " and m.status='1'";
					break;
				case 3:
					text = " and m.status='2'";
					break;
				}
				string sql = "SELECT d.sellNo,d.sellingPrice,d.num,d.discount,d.total,m.editDate,m.sum,m.status FROM hypos_detail_sell as d JOIN hypos_main_sell as m ON d.sellNo = m.sellNo where 1=1 " + text + " and d.barcode = '" + _GDSNO + "' and m.editDate between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and datetime(date( '" + dateTimePicker4.Value.ToString("yyyy-MM-dd") + "' ), '+1 days') order by m.editDate desc";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count <= 0)
				{
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					string text2 = "";
					switch (dataTable.Rows[i]["status"].ToString())
					{
					case "0":
						text2 = "正常";
						break;
					case "1":
						text2 = "取消";
						break;
					case "2":
						text2 = "變更";
						break;
					}
					dataGridView3.Rows.Insert(i, dataTable.Rows[i]["editDate"].ToString(), dataTable.Rows[i]["sellNo"].ToString(), dataTable.Rows[i]["sum"].ToString(), dataTable.Rows[i]["sellingPrice"].ToString(), dataTable.Rows[i]["num"].ToString(), dataTable.Rows[i]["discount"].ToString(), dataTable.Rows[i]["total"].ToString(), text2, dataTable.Rows[i]["status"].ToString());
				}
			}
			catch (Exception)
			{
			}
		}

		private void showDeliveryRecord()
		{
			try
			{
				dataGridView2.Rows.Clear();
				string text = "";
				switch (comboBox1.SelectedIndex.ToString())
				{
				case "0":
					text = " and m.status in (0,1,2) ";
					break;
				case "1":
					text = " and m.status = 0 ";
					break;
				case "2":
					text = " and m.status = 1 ";
					break;
				case "3":
					text = " and m.status = 2 ";
					break;
				}
				string sql = "SELECT m.editDate, m.DeliveryNo, m.CurSum, m.sumDiscount, m.status, d.sellingPrice, d.num, d.subtotal from hypos_DeliveryGoods_Master as m join hypos_DeliveryGoods_Detail as d on m.DeliveryNo = d.DeliveryNo where 1=1 " + text + " and d.barcode = '" + _GDSNO + "' and m.editDate between '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and datetime(date( '" + dateTimePicker3.Value.ToString("yyyy-MM-dd") + "' ), '+1 days') order by m.editDate desc";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count <= 0)
				{
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					string text2 = "";
					switch (dataTable.Rows[i]["status"].ToString())
					{
					case "0":
						text2 = "正常";
						break;
					case "1":
						text2 = "取消";
						break;
					case "2":
						text2 = "變更";
						break;
					}
					dataGridView2.Rows.Insert(i, dataTable.Rows[i]["editDate"].ToString(), dataTable.Rows[i]["DeliveryNo"].ToString(), (int.Parse(dataTable.Rows[i]["CurSum"].ToString()) - int.Parse(dataTable.Rows[i]["sumDiscount"].ToString())).ToString(), dataTable.Rows[i]["sellingPrice"].ToString(), dataTable.Rows[i]["num"].ToString(), dataTable.Rows[i]["subtotal"].ToString(), text2, dataTable.Rows[i]["status"].ToString());
				}
			}
			catch (Exception)
			{
			}
		}

		private void tableLayoutPanel4_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			using (SolidBrush brush = new SolidBrush(Color.FromArgb(102, 102, 102)))
			{
				e.Graphics.FillRectangle(brush, e.CellBounds);
			}
		}

		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = comboBox4.SelectedIndex;
			if (comboBox4SelectedIndex == 99 || comboBox4SelectedIndex != selectedIndex)
			{
				comboBox4SelectedIndex = selectedIndex;
				showSalesRecord(selectedIndex);
			}
		}

		private void btn_back_Click(object sender, EventArgs e)
		{
			switchForm(new frmCommodityMangement());
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			switchForm(new frmCommodityMangement());
		}

		private void btn_printBarcode_Click(object sender, EventArgs e)
		{
			if (!"D".Equals(_status))
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				string[] strWhereParameterArray = new string[1]
				{
					_GDSNO
				};
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "cropId,pestId", "hypos_user_pair", "barcode ={0}", "total desc", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						string[] strWhereParameterArray2 = new string[5]
						{
							dataTable2.Rows[0]["pesticideId"].ToString(),
							dataTable2.Rows[0]["formCode"].ToString(),
							dataTable2.Rows[0]["contents"].ToString(),
							dataTable.Rows[i]["cropId"].ToString(),
							dataTable.Rows[i]["pestId"].ToString()
						};
						if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "HyScope", " pesticideId = {0} AND formCode = {1} AND contents = {2} AND cropId = {3} AND pestId = {4} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
						{
							list.Add(dataTable.Rows[i]["cropId"].ToString());
							list2.Add(dataTable.Rows[i]["pestId"].ToString());
							continue;
						}
						string[] strWhereParameterArray3 = new string[1]
						{
							dataTable.Rows[i]["cropId"].ToString()
						};
						DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyCrop", " code = {0}  ", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
						string[] strWhereParameterArray4 = new string[1]
						{
							dataTable.Rows[i]["pestId"].ToString()
						};
						DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyBlight", " code = {0}  ", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
						MessageBox.Show("此 " + dataTable3.Rows[0]["name"].ToString() + " x " + dataTable4.Rows[0]["name"].ToString() + " 配對已不存在，請選擇其他配對。是否從常用配對紀錄中刪除此配對？", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						string[] strParameterArray = new string[3]
						{
							_GDSNO,
							dataTable.Rows[i]["cropId"].ToString(),
							dataTable.Rows[i]["pestId"].ToString()
						};
						string sql = " DELETE FROM hypos_user_pair WHERE barcode = {0} AND cropId = {1} AND pestId = {2} ";
						DataBaseUtilities.DBOperation(Program.ConnectionString, sql, strParameterArray, CommandOperationType.ExecuteNonQuery);
					}
				}
				new frmBatchPrintCommodity_pair(_GDSNO, list, list2, this, _CName + " " + _GDNAME).ShowDialog();
			}
			else
			{
				MessageBox.Show("商品已禁用");
			}
		}

		private void tlp_consumeRecord_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			if (e.Row == 0)
			{
				e.Graphics.FillRectangle(Brushes.DarkGray, e.CellBounds);
			}
		}

		private void btn_BasicData_save_Click(object sender, EventArgs e)
		{
			if (!"D".Equals(_status))
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
				if (cb_useCustomBarcode.Checked && ((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_GOODSLST", "barcode = {0}", "", null, new string[1]
				{
					tb_barcode.Text
				}, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					AutoClosingMessageBox.Show("此商品條碼已建有資料，無法重複建立商品\n請透過商品查詢功能、檢查此條碼商品");
					return;
				}
				try
				{
					string sql = " select hot_key from hypos_GOODSLST where  hot_key = {0} and hot_key !='' and GDSNO !={1} ";
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[2]
					{
						textBox2.Text,
						tb_GDSNO.Text
					}, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						MessageBox.Show("此商品快捷鍵已有商品使用");
						return;
					}
					string[,] strFieldArray = new string[21, 2]
					{
						{
							"GDSNO",
							tb_GDSNO.Text
						},
						{
							"barcode",
							tb_barcode.Text
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
							"UpdateDate",
							DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
						},
						{
							"hot_key",
							textBox2.Text
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_GOODSLST", "GDSNO = {0}", "", strFieldArray, new string[1]
					{
						tb_GDSNO.Text
					}, CommandOperationType.ExecuteNonQuery);
					MessageBox.Show("商品基本資料編修成功");
					switchForm(new frmCommodityMangement());
				}
				catch
				{
				}
			}
			else
			{
				MessageBox.Show("商品已禁用");
			}
		}

		private void btn_UpdatePrice_Click(object sender, EventArgs e)
		{
			if (!"D".Equals(_status))
			{
				new dialogSellPriceLog(tb_GDSNO.Text).ShowDialog(this);
			}
			else
			{
				MessageBox.Show("商品已禁用");
			}
		}

		private void btn_img_Commodity_Click(object sender, EventArgs e)
		{
		}

		private void btn_AdvancedConfig_save_Click(object sender, EventArgs e)
		{
			if (!"D".Equals(_status))
			{
				string text = tb_SpecialPrice1.Text;
				string text2 = tb_SpecialPrice2.Text;
				if ("".Equals(text))
				{
					text = _Price;
				}
				if ("".Equals(text2))
				{
					text2 = _Price;
				}
				string text3 = "0";
				if (radioButton2.Checked)
				{
					text3 = "1";
				}
				if (myCheckBox1.Checked)
				{
					_DeliveryOpenPrice = "1";
				}
				else
				{
					_DeliveryOpenPrice = "0";
				}
				string[,] strFieldArray = new string[7, 2]
				{
					{
						"SpecialPrice1",
						text
					},
					{
						"SpecialPrice2",
						text2
					},
					{
						"OpenPrice",
						mCB_OpenPrice.Checked ? "1" : "0"
					},
					{
						"UpdateDate",
						DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
					},
					{
						"DeliveryPrice",
						_DeliveryPrice
					},
					{
						"DeliveryPriceSetType",
						text3
					},
					{
						"DeliveryOpenPrice",
						_DeliveryOpenPrice
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_GOODSLST", " GDSNO = {0} ", "", strFieldArray, new string[1]
				{
					tb_GDSNO.Text
				}, CommandOperationType.ExecuteNonQuery);
				MessageBox.Show("商品進階設定編修成功");
				switchForm(new frmCommodityMangement());
			}
			else
			{
				MessageBox.Show("商品已禁用");
			}
		}

		private void digitOnly_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b'));
		}

		private void checkSell(object sender, EventArgs e)
		{
			switchForm(new frmMainShopSimpleReturn((sender as Label).Text, "frmEditCommodity", _GDSNO));
		}

		private void tb_adjustCount_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == 13)
			{
				btn_enter_Click(sender, e);
			}
		}

		private void tb_adjustCount_Enter(object sender, EventArgs e)
		{
			if ("請輸入調整數字".Equals(tb_adjustCount.Text))
			{
				tb_adjustCount.Text = "";
			}
		}

		private void tb_adjustCount_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_adjustCount.Text))
			{
				tb_adjustCount.Text = "請輸入調整數字";
			}
		}

		private void btn_enter_Click(object sender, EventArgs e)
		{
			if (!"D".Equals(_status))
			{
				string text = "";
				string text2 = (cb_adjustType.SelectedItem as ComboboxItem).Value.ToString();
				string text3 = (cb_adjustType.SelectedItem as ComboboxItem).Text;
				if (string.IsNullOrEmpty(text2))
				{
					text += "請選擇調整理由\n";
				}
				if ("請輸入調整數字".Equals(tb_adjustCount.Text) || string.IsNullOrEmpty(tb_adjustCount.Text))
				{
					text += "請輸入調整數字\n";
				}
				if (!string.IsNullOrEmpty(text))
				{
					AutoClosingMessageBox.Show(text);
					return;
				}
				try
				{
					string text4 = tb_adjustCount.Text;
					if (_strAdjustPlusOrMinus.Equals("minus"))
					{
						text4 = "-" + tb_adjustCount.Text;
					}
					else if (_strAdjustPlusOrMinus.Equals("plus"))
					{
						text4 = "+" + tb_adjustCount.Text;
					}
					string text5 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
					l_InventoryTotal.Text = (int.Parse(l_InventoryTotal.Text) + int.Parse(text4)).ToString();
					dataGridView1.Rows.Insert(0, text5, text3, "", "", text4, l_InventoryTotal.Text);
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST set inventory = {1} WHERE GDSNO = {0}", new string[2]
					{
						_GDSNO,
						l_InventoryTotal.Text
					}, CommandOperationType.ExecuteNonQuery);
					string[,] strFieldArray = new string[6, 2]
					{
						{
							"AdjustNo",
							dialogInventoryAdjustment.getNewAdjustNo()
						},
						{
							"GDSNO",
							_GDSNO
						},
						{
							"adjustType",
							text2
						},
						{
							"adjustCount",
							text4
						},
						{
							"updateDate",
							text5
						},
						{
							"GoodsTotalCountLog",
							l_InventoryTotal.Text
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_InventoryAdjustment", null, null, strFieldArray, null, CommandOperationType.ExecuteNonQuery);
					string[,] strFieldArray2 = new string[1, 2]
					{
						{
							"UpdateDate",
							text5
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_GOODSLST", " GDSNO = {0} ", "", strFieldArray2, new string[1]
					{
						tb_GDSNO.Text
					}, CommandOperationType.ExecuteNonQuery);
					if (text2.Equals("0"))
					{
						AutoClosingMessageBox.Show("此調整不會回傳防檢局，僅供店內管理使用");
					}
					cb_adjustType.SelectedIndex = 0;
					tb_adjustCount.Text = "";
				}
				catch (FormatException)
				{
					AutoClosingMessageBox.Show("金額格式錯誤");
				}
				catch (Exception)
				{
					MessageBox.Show("調整錯誤");
				}
			}
			else
			{
				MessageBox.Show("商品已禁用");
			}
		}

		private void tb_adjustCount_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b'));
		}

		private void btn_minus_Click(object sender, EventArgs e)
		{
			FocusOnbtnMinus();
		}

		private void btn_plus_Click(object sender, EventArgs e)
		{
			FocusOnbtnPlus();
		}

		private void FocusOnbtnPlus()
		{
			if (_strAdjustPlusOrMinus.Equals("minus"))
			{
				_strAdjustPlusOrMinus = "plus";
				btn_minus.BackColor = Color.Gainsboro;
				btn_plus.BackColor = SystemColors.ButtonShadow;
			}
		}

		private void FocusOnbtnMinus()
		{
			if (_strAdjustPlusOrMinus.Equals("plus"))
			{
				_strAdjustPlusOrMinus = "minus";
				btn_minus.BackColor = SystemColors.ButtonShadow;
				btn_plus.BackColor = Color.Gainsboro;
			}
		}

		private void cb_adjustType_SelectedValueChanged(object sender, EventArgs e)
		{
			string text = (cb_adjustType.SelectedItem as ComboboxItem).Text;
			if (text.Equals("破損"))
			{
				FocusOnbtnMinus();
			}
			else if (text.Equals("盤減"))
			{
				FocusOnbtnMinus();
			}
			else if (text.Equals("盤增"))
			{
				FocusOnbtnPlus();
			}
			else if (text.Equals("過期銷毀"))
			{
				FocusOnbtnMinus();
			}
			else if (text.Equals("資料異常"))
			{
				FocusOnbtnMinus();
			}
			else if (text.Equals("店內盤點"))
			{
				FocusOnbtnPlus();
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			showDeliveryRecord();
		}

		private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
		{
			showDeliveryRecord();
		}

		private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
		{
			showDeliveryRecord();
		}

		private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1 && e.RowIndex >= 0)
			{
				string deliveryNo = dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString();
				string value = dataGridView2["hidden_Status", e.RowIndex].Value.ToString();
				if ("1".Equals(value))
				{
					MessageBox.Show("出貨單已取消，無法編修");
				}
				else
				{
					switchForm(new frmEditDeliveryOrder(deliveryNo), this);
				}
			}
		}

		private void btn_settingDeliveryPrice_Click(object sender, EventArgs e)
		{
			new dialogDeliveryPriceLog(tb_GDSNO.Text).ShowDialog(this);
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
			{
				btn_settingDeliveryPrice.Enabled = false;
				myCheckBox1.Enabled = false;
			}
			else
			{
				btn_settingDeliveryPrice.Enabled = true;
				myCheckBox1.Enabled = true;
			}
		}

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			showSalesRecord(comboBox4.SelectedIndex);
		}

		private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
		{
			showSalesRecord(comboBox4.SelectedIndex);
		}

		private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
		{
			showInventoryManage();
		}

		private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
		{
			showInventoryManage();
		}

		private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1 && e.RowIndex >= 0)
			{
				string sellno = dataGridView3[e.ColumnIndex, e.RowIndex].Value.ToString();
				string value = dataGridView3["hidden_status2", e.RowIndex].Value.ToString();
				if ("1".Equals(value))
				{
					MessageBox.Show("銷售單已取消，無法編修");
				}
				else if (Program.SystemMode == 1)
				{
					switchForm(new frmMainShopSimpleReturn(sellno, "frmEditCommodity", _GDSNO));
				}
				else
				{
					switchForm(new frmMainShopSimpleReturnWithMoney(sellno, "frmEditCommodity", _GDSNO));
				}
			}
		}

		private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b') && !char.IsUpper(e.KeyChar) && !char.IsLower(e.KeyChar));
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 3 && e.RowIndex >= 0)
			{
				string purchaseNo = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
				switchForm(new frmEditInventory(purchaseNo), this);
			}
		}

		protected string GetSupplierNameData(string vendorId)
		{
			try
			{
				string sql = " select * from hypos_Supplier where vendorId = '" + vendorId + "'";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[0], CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					vendorId = dataTable.Rows[0]["SupplierName"].ToString();
					return vendorId;
				}
				vendorId = "";
				return vendorId;
			}
			catch (Exception)
			{
				vendorId = "";
				return vendorId;
			}
		}

		protected string Gethypos_PurchaseGoods_DetailData(string MFGDate, string BatchNo)
		{
			string text = "";
			try
			{
				string sql = " select * from hypos_PurchaseGoods_Detail where MFGDate = '" + MFGDate + "' and BatchNo ='" + BatchNo + "'";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[0], CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					return dataTable.Rows[0]["PurchaseNo"].ToString();
				}
				return "";
			}
			catch (Exception)
			{
				return "";
			}
		}

		protected string Get_hypos_PurchaseGoods_MasterData(string PurchaseNo)
		{
			string text = "";
			try
			{
				string sql = " select * from hypos_PurchaseGoods_Master where PurchaseNo = '" + PurchaseNo + "'";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[0], CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					return dataTable.Rows[0]["SupplierNo"].ToString();
				}
				return "";
			}
			catch (Exception)
			{
				return "";
			}
		}

		protected string Get_hypos_SupplierData(string SupplierNo)
		{
			string text = "";
			try
			{
				string sql = " select * from hypos_Supplier where SupplierNo = '" + SupplierNo + "'";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[0], CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					return dataTable.Rows[0]["SupplierName"].ToString();
				}
				return "";
			}
			catch (Exception)
			{
				return "";
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			panel2 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			btn_back = new System.Windows.Forms.Button();
			btn_printBarcode = new System.Windows.Forms.Button();
			DeliveryRecord = new System.Windows.Forms.TabControl();
			BasicData = new System.Windows.Forms.TabPage();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			textBox2 = new System.Windows.Forms.TextBox();
			panel26 = new System.Windows.Forms.Panel();
			label36 = new System.Windows.Forms.Label();
			label37 = new System.Windows.Forms.Label();
			tb_pesticideRestrictedName = new System.Windows.Forms.TextBox();
			panel17 = new System.Windows.Forms.Panel();
			label27 = new System.Windows.Forms.Label();
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
			label11 = new System.Windows.Forms.Label();
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
			tb_SubsidyMoney = new System.Windows.Forms.TextBox();
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
			flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
			cb_HighlyToxic = new POS_Client.MyCheckBox();
			cb_SubsidyFertilizer = new POS_Client.MyCheckBox();
			flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
			tb_Price = new System.Windows.Forms.TextBox();
			btn_UpdatePrice = new System.Windows.Forms.Button();
			cb_dataType = new System.Windows.Forms.ComboBox();
			btn_cancel = new System.Windows.Forms.Button();
			btn_BasicData_save = new System.Windows.Forms.Button();
			AdvancedConfig = new System.Windows.Forms.TabPage();
			button1 = new System.Windows.Forms.Button();
			btn_AdvancedConfig_save = new System.Windows.Forms.Button();
			tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			panel24 = new System.Windows.Forms.Panel();
			label48 = new System.Windows.Forms.Label();
			panel23 = new System.Windows.Forms.Panel();
			label35 = new System.Windows.Forms.Label();
			panel28 = new System.Windows.Forms.Panel();
			label44 = new System.Windows.Forms.Label();
			panel25 = new System.Windows.Forms.Panel();
			radioButton1 = new System.Windows.Forms.RadioButton();
			btn_settingDeliveryPrice = new System.Windows.Forms.Button();
			myCheckBox1 = new POS_Client.MyCheckBox();
			label47 = new System.Windows.Forms.Label();
			textBox1 = new System.Windows.Forms.TextBox();
			radioButton2 = new System.Windows.Forms.RadioButton();
			flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			tb_SpecialPrice1 = new System.Windows.Forms.TextBox();
			panel35 = new System.Windows.Forms.Panel();
			label51 = new System.Windows.Forms.Label();
			tb_SpecialPrice2 = new System.Windows.Forms.TextBox();
			flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			mCB_OpenPrice = new POS_Client.MyCheckBox();
			InventoryManage = new System.Windows.Forms.TabPage();
			dateTimePicker5 = new System.Windows.Forms.DateTimePicker();
			label54 = new System.Windows.Forms.Label();
			dateTimePicker6 = new System.Windows.Forms.DateTimePicker();
			label55 = new System.Windows.Forms.Label();
			label53 = new System.Windows.Forms.Label();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewLinkColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			panel22 = new System.Windows.Forms.Panel();
			label33 = new System.Windows.Forms.Label();
			panel29 = new System.Windows.Forms.Panel();
			label43 = new System.Windows.Forms.Label();
			l_InventoryTotal = new System.Windows.Forms.Label();
			panel16 = new System.Windows.Forms.Panel();
			btn_minus = new System.Windows.Forms.Button();
			btn_plus = new System.Windows.Forms.Button();
			label24 = new System.Windows.Forms.Label();
			btn_enter = new System.Windows.Forms.Button();
			tb_adjustCount = new System.Windows.Forms.TextBox();
			cb_adjustType = new System.Windows.Forms.ComboBox();
			label19 = new System.Windows.Forms.Label();
			ConsumeRecord = new System.Windows.Forms.TabPage();
			dataGridView3 = new System.Windows.Forms.DataGridView();
			dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewLinkColumn1 = new System.Windows.Forms.DataGridViewLinkColumn();
			dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_status2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label46 = new System.Windows.Forms.Label();
			label52 = new System.Windows.Forms.Label();
			dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			label49 = new System.Windows.Forms.Label();
			dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
			label50 = new System.Windows.Forms.Label();
			comboBox4 = new System.Windows.Forms.ComboBox();
			label42 = new System.Windows.Forms.Label();
			tabPage1 = new System.Windows.Forms.TabPage();
			dataGridView2 = new System.Windows.Forms.DataGridView();
			dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewLinkColumn();
			dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label30 = new System.Windows.Forms.Label();
			label29 = new System.Windows.Forms.Label();
			dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			label28 = new System.Windows.Forms.Label();
			dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
			label26 = new System.Windows.Forms.Label();
			comboBox1 = new System.Windows.Forms.ComboBox();
			label25 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel2.SuspendLayout();
			DeliveryRecord.SuspendLayout();
			BasicData.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			panel26.SuspendLayout();
			panel17.SuspendLayout();
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
			flowLayoutPanel6.SuspendLayout();
			flowLayoutPanel7.SuspendLayout();
			AdvancedConfig.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			panel24.SuspendLayout();
			panel23.SuspendLayout();
			panel28.SuspendLayout();
			panel25.SuspendLayout();
			flowLayoutPanel2.SuspendLayout();
			panel35.SuspendLayout();
			flowLayoutPanel3.SuspendLayout();
			InventoryManage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			tableLayoutPanel2.SuspendLayout();
			panel22.SuspendLayout();
			panel29.SuspendLayout();
			panel16.SuspendLayout();
			ConsumeRecord.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
			tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
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
			btn_printBarcode.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_printBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_printBarcode.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_printBarcode.ForeColor = System.Drawing.Color.White;
			btn_printBarcode.Location = new System.Drawing.Point(725, 47);
			btn_printBarcode.Name = "btn_printBarcode";
			btn_printBarcode.Size = new System.Drawing.Size(156, 28);
			btn_printBarcode.TabIndex = 35;
			btn_printBarcode.Text = "條碼列印(含常用配對)";
			btn_printBarcode.UseVisualStyleBackColor = false;
			btn_printBarcode.Click += new System.EventHandler(btn_printBarcode_Click);
			DeliveryRecord.Controls.Add(BasicData);
			DeliveryRecord.Controls.Add(AdvancedConfig);
			DeliveryRecord.Controls.Add(InventoryManage);
			DeliveryRecord.Controls.Add(ConsumeRecord);
			DeliveryRecord.Controls.Add(tabPage1);
			DeliveryRecord.Dock = System.Windows.Forms.DockStyle.Bottom;
			DeliveryRecord.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			DeliveryRecord.Location = new System.Drawing.Point(0, 39);
			DeliveryRecord.Name = "DeliveryRecord";
			DeliveryRecord.Padding = new System.Drawing.Point(15, 10);
			DeliveryRecord.SelectedIndex = 0;
			DeliveryRecord.Size = new System.Drawing.Size(981, 622);
			DeliveryRecord.TabIndex = 54;
			BasicData.Controls.Add(tableLayoutPanel1);
			BasicData.Controls.Add(btn_cancel);
			BasicData.Controls.Add(btn_BasicData_save);
			BasicData.Location = new System.Drawing.Point(4, 47);
			BasicData.Name = "BasicData";
			BasicData.Padding = new System.Windows.Forms.Padding(3);
			BasicData.Size = new System.Drawing.Size(973, 571);
			BasicData.TabIndex = 0;
			BasicData.Text = "基本資料";
			BasicData.UseVisualStyleBackColor = true;
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 4;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.Controls.Add(textBox2, 3, 1);
			tableLayoutPanel1.Controls.Add(panel26, 2, 9);
			tableLayoutPanel1.Controls.Add(tb_pesticideRestrictedName, 1, 9);
			tableLayoutPanel1.Controls.Add(panel17, 0, 9);
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
			tableLayoutPanel1.Controls.Add(tb_SubsidyMoney, 1, 8);
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
			tableLayoutPanel1.Controls.Add(flowLayoutPanel6, 3, 6);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel7, 3, 7);
			tableLayoutPanel1.Controls.Add(cb_dataType, 3, 9);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 10;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0001f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.999102f));
			tableLayoutPanel1.Size = new System.Drawing.Size(967, 503);
			tableLayoutPanel1.TabIndex = 3;
			textBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			textBox2.Location = new System.Drawing.Point(657, 59);
			textBox2.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			textBox2.MaxLength = 10;
			textBox2.Name = "textBox2";
			textBox2.Size = new System.Drawing.Size(299, 33);
			textBox2.TabIndex = 42;
			textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox2_KeyPress_1);
			panel26.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel26.Controls.Add(label36);
			panel26.Controls.Add(label37);
			panel26.Location = new System.Drawing.Point(484, 451);
			panel26.Margin = new System.Windows.Forms.Padding(0);
			panel26.Name = "panel26";
			panel26.Size = new System.Drawing.Size(162, 49);
			panel26.TabIndex = 41;
			label36.AutoSize = true;
			label36.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label36.ForeColor = System.Drawing.Color.Red;
			label36.Location = new System.Drawing.Point(67, 14);
			label36.Name = "label36";
			label36.Size = new System.Drawing.Size(17, 21);
			label36.TabIndex = 3;
			label36.Text = "*";
			label37.AutoSize = true;
			label37.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label37.ForeColor = System.Drawing.Color.White;
			label37.Location = new System.Drawing.Point(84, 14);
			label37.Name = "label37";
			label37.Size = new System.Drawing.Size(74, 21);
			label37.TabIndex = 2;
			label37.Text = "資料類型";
			tb_pesticideRestrictedName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_pesticideRestrictedName.Enabled = false;
			tb_pesticideRestrictedName.Location = new System.Drawing.Point(174, 460);
			tb_pesticideRestrictedName.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_pesticideRestrictedName.Name = "tb_pesticideRestrictedName";
			tb_pesticideRestrictedName.ReadOnly = true;
			tb_pesticideRestrictedName.Size = new System.Drawing.Size(299, 33);
			tb_pesticideRestrictedName.TabIndex = 41;
			panel17.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel17.Controls.Add(label27);
			panel17.Location = new System.Drawing.Point(1, 451);
			panel17.Margin = new System.Windows.Forms.Padding(0);
			panel17.Name = "panel17";
			panel17.Size = new System.Drawing.Size(162, 49);
			panel17.TabIndex = 40;
			label27.AutoSize = true;
			label27.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label27.ForeColor = System.Drawing.Color.White;
			label27.Location = new System.Drawing.Point(85, 16);
			label27.Name = "label27";
			label27.Size = new System.Drawing.Size(74, 21);
			label27.TabIndex = 0;
			label27.Text = "農藥資格";
			tb_Cost.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_Cost.Location = new System.Drawing.Point(174, 359);
			tb_Cost.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_Cost.Name = "tb_Cost";
			tb_Cost.Size = new System.Drawing.Size(299, 33);
			tb_Cost.TabIndex = 37;
			tb_spec.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_spec.Enabled = false;
			tb_spec.Location = new System.Drawing.Point(174, 259);
			tb_spec.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_spec.Name = "tb_spec";
			tb_spec.Size = new System.Drawing.Size(299, 33);
			tb_spec.TabIndex = 33;
			tb_domManufId.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_domManufId.Enabled = false;
			tb_domManufId.Location = new System.Drawing.Point(174, 61);
			tb_domManufId.Margin = new System.Windows.Forms.Padding(10);
			tb_domManufId.MaxLength = 5;
			tb_domManufId.Name = "tb_domManufId";
			tb_domManufId.Size = new System.Drawing.Size(299, 33);
			tb_domManufId.TabIndex = 32;
			panel11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel11.Controls.Add(label15);
			panel11.Dock = System.Windows.Forms.DockStyle.Fill;
			panel11.Location = new System.Drawing.Point(484, 251);
			panel11.Margin = new System.Windows.Forms.Padding(0);
			panel11.Name = "panel11";
			panel11.Size = new System.Drawing.Size(162, 49);
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
			panel9.Size = new System.Drawing.Size(162, 49);
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
			panel8.Location = new System.Drawing.Point(1, 351);
			panel8.Margin = new System.Windows.Forms.Padding(0);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(162, 49);
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
			panel7.Location = new System.Drawing.Point(1, 301);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(162, 49);
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
			panel6.Location = new System.Drawing.Point(1, 251);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(162, 49);
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
			panel3.Location = new System.Drawing.Point(1, 101);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(162, 49);
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
			panel1.Size = new System.Drawing.Size(162, 49);
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
			panel4.Location = new System.Drawing.Point(1, 151);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(162, 49);
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
			panel5.Location = new System.Drawing.Point(1, 201);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(162, 49);
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
			panel10.Controls.Add(label11);
			panel10.Dock = System.Windows.Forms.DockStyle.Fill;
			panel10.Location = new System.Drawing.Point(484, 51);
			panel10.Margin = new System.Windows.Forms.Padding(0);
			panel10.Name = "panel10";
			panel10.Size = new System.Drawing.Size(162, 49);
			panel10.TabIndex = 24;
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label11.ForeColor = System.Drawing.Color.White;
			label11.Location = new System.Drawing.Point(66, 14);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(90, 21);
			label11.TabIndex = 2;
			label11.Text = "商品快捷鍵";
			panel12.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel12.Controls.Add(label17);
			panel12.Dock = System.Windows.Forms.DockStyle.Fill;
			panel12.Location = new System.Drawing.Point(484, 301);
			panel12.Margin = new System.Windows.Forms.Padding(0);
			panel12.Name = "panel12";
			panel12.Size = new System.Drawing.Size(162, 49);
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
			panel13.Location = new System.Drawing.Point(484, 351);
			panel13.Margin = new System.Windows.Forms.Padding(0);
			panel13.Name = "panel13";
			panel13.Size = new System.Drawing.Size(162, 49);
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
			flowLayoutPanel1.Size = new System.Drawing.Size(319, 49);
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
			cb_useCustomBarcode.Enabled = false;
			cb_useCustomBarcode.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_useCustomBarcode.Location = new System.Drawing.Point(175, 14);
			cb_useCustomBarcode.Name = "cb_useCustomBarcode";
			cb_useCustomBarcode.Size = new System.Drawing.Size(134, 24);
			cb_useCustomBarcode.TabIndex = 3;
			cb_useCustomBarcode.Text = "使用商品條碼";
			cb_useCustomBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			cb_useCustomBarcode.UseVisualStyleBackColor = true;
			panel15.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel15.Controls.Add(label23);
			panel15.Dock = System.Windows.Forms.DockStyle.Fill;
			panel15.Location = new System.Drawing.Point(1, 51);
			panel15.Margin = new System.Windows.Forms.Padding(0);
			panel15.Name = "panel15";
			panel15.Size = new System.Drawing.Size(162, 49);
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
			panel14.Location = new System.Drawing.Point(1, 401);
			panel14.Margin = new System.Windows.Forms.Padding(0);
			panel14.Name = "panel14";
			panel14.Size = new System.Drawing.Size(162, 49);
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
			panel18.Location = new System.Drawing.Point(484, 401);
			panel18.Margin = new System.Windows.Forms.Padding(0);
			panel18.Name = "panel18";
			panel18.Size = new System.Drawing.Size(162, 49);
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
			panel19.Location = new System.Drawing.Point(484, 201);
			panel19.Margin = new System.Windows.Forms.Padding(0);
			panel19.Name = "panel19";
			panel19.Size = new System.Drawing.Size(162, 49);
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
			panel20.Location = new System.Drawing.Point(484, 151);
			panel20.Margin = new System.Windows.Forms.Padding(0);
			panel20.Name = "panel20";
			panel20.Size = new System.Drawing.Size(162, 49);
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
			panel21.Location = new System.Drawing.Point(484, 101);
			panel21.Margin = new System.Windows.Forms.Padding(0);
			panel21.Name = "panel21";
			panel21.Size = new System.Drawing.Size(162, 49);
			panel21.TabIndex = 20;
			label22.AutoSize = true;
			label22.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label22.ForeColor = System.Drawing.Color.White;
			label22.Location = new System.Drawing.Point(85, 16);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(74, 21);
			label22.TabIndex = 0;
			label22.Text = "廠商名稱";
			tb_SubsidyMoney.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SubsidyMoney.Enabled = false;
			tb_SubsidyMoney.Location = new System.Drawing.Point(174, 409);
			tb_SubsidyMoney.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_SubsidyMoney.Name = "tb_SubsidyMoney";
			tb_SubsidyMoney.Size = new System.Drawing.Size(299, 33);
			tb_SubsidyMoney.TabIndex = 36;
			tb_GDNAME.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_GDNAME.Location = new System.Drawing.Point(174, 109);
			tb_GDNAME.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_GDNAME.Name = "tb_GDNAME";
			tb_GDNAME.Size = new System.Drawing.Size(299, 33);
			tb_GDNAME.TabIndex = 38;
			tb_CName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_CName.Enabled = false;
			tb_CName.Location = new System.Drawing.Point(174, 159);
			tb_CName.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_CName.MaxLength = 14;
			tb_CName.Name = "tb_CName";
			tb_CName.Size = new System.Drawing.Size(299, 33);
			tb_CName.TabIndex = 38;
			tb_barcode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_barcode.Enabled = false;
			tb_barcode.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_barcode.Location = new System.Drawing.Point(657, 11);
			tb_barcode.Margin = new System.Windows.Forms.Padding(10);
			tb_barcode.MaxLength = 13;
			tb_barcode.Name = "tb_barcode";
			tb_barcode.Size = new System.Drawing.Size(299, 33);
			tb_barcode.TabIndex = 32;
			tb_barndName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_barndName.Enabled = false;
			tb_barndName.Location = new System.Drawing.Point(657, 111);
			tb_barndName.Margin = new System.Windows.Forms.Padding(10);
			tb_barndName.Name = "tb_barndName";
			tb_barndName.Size = new System.Drawing.Size(299, 33);
			tb_barndName.TabIndex = 32;
			tb_EName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_EName.Enabled = false;
			tb_EName.Location = new System.Drawing.Point(657, 161);
			tb_EName.Margin = new System.Windows.Forms.Padding(10);
			tb_EName.Name = "tb_EName";
			tb_EName.Size = new System.Drawing.Size(299, 33);
			tb_EName.TabIndex = 32;
			tb_contents.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_contents.Enabled = false;
			tb_contents.Location = new System.Drawing.Point(657, 211);
			tb_contents.Margin = new System.Windows.Forms.Padding(10);
			tb_contents.Name = "tb_contents";
			tb_contents.Size = new System.Drawing.Size(299, 33);
			tb_contents.TabIndex = 32;
			cb_CLA1NO.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_CLA1NO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_CLA1NO.FormattingEnabled = true;
			cb_CLA1NO.Location = new System.Drawing.Point(174, 315);
			cb_CLA1NO.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			cb_CLA1NO.Name = "cb_CLA1NO";
			cb_CLA1NO.Size = new System.Drawing.Size(299, 32);
			cb_CLA1NO.TabIndex = 39;
			tb_formCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_formCode.Enabled = false;
			tb_formCode.Location = new System.Drawing.Point(174, 209);
			tb_formCode.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_formCode.Name = "tb_formCode";
			tb_formCode.Size = new System.Drawing.Size(299, 33);
			tb_formCode.TabIndex = 33;
			tb_capacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_capacity.Enabled = false;
			tb_capacity.Location = new System.Drawing.Point(657, 259);
			tb_capacity.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_capacity.Name = "tb_capacity";
			tb_capacity.Size = new System.Drawing.Size(299, 33);
			tb_capacity.TabIndex = 33;
			cb_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_status.FormattingEnabled = true;
			cb_status.Location = new System.Drawing.Point(657, 415);
			cb_status.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			cb_status.Name = "cb_status";
			cb_status.Size = new System.Drawing.Size(299, 32);
			cb_status.TabIndex = 39;
			flowLayoutPanel6.Controls.Add(cb_HighlyToxic);
			flowLayoutPanel6.Controls.Add(cb_SubsidyFertilizer);
			flowLayoutPanel6.Location = new System.Drawing.Point(647, 301);
			flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel6.Name = "flowLayoutPanel6";
			flowLayoutPanel6.Size = new System.Drawing.Size(319, 49);
			flowLayoutPanel6.TabIndex = 25;
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
			flowLayoutPanel7.Controls.Add(tb_Price);
			flowLayoutPanel7.Controls.Add(btn_UpdatePrice);
			flowLayoutPanel7.Location = new System.Drawing.Point(647, 351);
			flowLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel7.Name = "flowLayoutPanel7";
			flowLayoutPanel7.Size = new System.Drawing.Size(319, 49);
			flowLayoutPanel7.TabIndex = 25;
			tb_Price.Enabled = false;
			tb_Price.Location = new System.Drawing.Point(10, 10);
			tb_Price.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
			tb_Price.Name = "tb_Price";
			tb_Price.Size = new System.Drawing.Size(218, 33);
			tb_Price.TabIndex = 38;
			btn_UpdatePrice.BackColor = System.Drawing.Color.White;
			btn_UpdatePrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_UpdatePrice.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_UpdatePrice.ForeColor = System.Drawing.Color.Black;
			btn_UpdatePrice.Image = POS_Client.Properties.Resources.ic_toc_black_24dp_1x;
			btn_UpdatePrice.Location = new System.Drawing.Point(238, 10);
			btn_UpdatePrice.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
			btn_UpdatePrice.Name = "btn_UpdatePrice";
			btn_UpdatePrice.Size = new System.Drawing.Size(54, 33);
			btn_UpdatePrice.TabIndex = 2;
			btn_UpdatePrice.UseVisualStyleBackColor = false;
			btn_UpdatePrice.Click += new System.EventHandler(btn_UpdatePrice_Click);
			cb_dataType.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_dataType.Enabled = false;
			cb_dataType.FormattingEnabled = true;
			cb_dataType.Location = new System.Drawing.Point(657, 465);
			cb_dataType.Margin = new System.Windows.Forms.Padding(10, 0, 3, 3);
			cb_dataType.Name = "cb_dataType";
			cb_dataType.Size = new System.Drawing.Size(299, 32);
			cb_dataType.TabIndex = 39;
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(544, 518);
			btn_cancel.Name = "btn_cancel";
			btn_cancel.Size = new System.Drawing.Size(70, 32);
			btn_cancel.TabIndex = 2;
			btn_cancel.Text = "取消";
			btn_cancel.UseVisualStyleBackColor = false;
			btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
			btn_BasicData_save.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_BasicData_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_BasicData_save.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_BasicData_save.ForeColor = System.Drawing.Color.White;
			btn_BasicData_save.Location = new System.Drawing.Point(408, 519);
			btn_BasicData_save.Name = "btn_BasicData_save";
			btn_BasicData_save.Size = new System.Drawing.Size(103, 32);
			btn_BasicData_save.TabIndex = 1;
			btn_BasicData_save.Text = "儲存變更";
			btn_BasicData_save.UseVisualStyleBackColor = false;
			btn_BasicData_save.Click += new System.EventHandler(btn_BasicData_save_Click);
			AdvancedConfig.Controls.Add(button1);
			AdvancedConfig.Controls.Add(btn_AdvancedConfig_save);
			AdvancedConfig.Controls.Add(tableLayoutPanel3);
			AdvancedConfig.Location = new System.Drawing.Point(4, 47);
			AdvancedConfig.Name = "AdvancedConfig";
			AdvancedConfig.Padding = new System.Windows.Forms.Padding(3);
			AdvancedConfig.Size = new System.Drawing.Size(973, 571);
			AdvancedConfig.TabIndex = 1;
			AdvancedConfig.Text = "進階設定";
			AdvancedConfig.UseVisualStyleBackColor = true;
			button1.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.ForeColor = System.Drawing.Color.White;
			button1.Location = new System.Drawing.Point(517, 311);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(70, 32);
			button1.TabIndex = 4;
			button1.Text = "取消";
			button1.UseVisualStyleBackColor = false;
			button1.Click += new System.EventHandler(btn_cancel_Click);
			btn_AdvancedConfig_save.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_AdvancedConfig_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_AdvancedConfig_save.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_AdvancedConfig_save.ForeColor = System.Drawing.Color.White;
			btn_AdvancedConfig_save.Location = new System.Drawing.Point(381, 312);
			btn_AdvancedConfig_save.Name = "btn_AdvancedConfig_save";
			btn_AdvancedConfig_save.Size = new System.Drawing.Size(103, 32);
			btn_AdvancedConfig_save.TabIndex = 3;
			btn_AdvancedConfig_save.Text = "儲存變更";
			btn_AdvancedConfig_save.UseVisualStyleBackColor = false;
			btn_AdvancedConfig_save.Click += new System.EventHandler(btn_AdvancedConfig_save_Click);
			tableLayoutPanel3.BackColor = System.Drawing.Color.White;
			tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel3.ColumnCount = 4;
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel3.Controls.Add(panel24, 0, 2);
			tableLayoutPanel3.Controls.Add(panel23, 2, 0);
			tableLayoutPanel3.Controls.Add(panel28, 0, 0);
			tableLayoutPanel3.Controls.Add(panel25, 1, 2);
			tableLayoutPanel3.Controls.Add(flowLayoutPanel2, 1, 0);
			tableLayoutPanel3.Controls.Add(panel35, 0, 1);
			tableLayoutPanel3.Controls.Add(tb_SpecialPrice2, 3, 0);
			tableLayoutPanel3.Controls.Add(flowLayoutPanel3, 1, 1);
			tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 4;
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49f));
			tableLayoutPanel3.Size = new System.Drawing.Size(967, 303);
			tableLayoutPanel3.TabIndex = 1;
			panel24.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel24.Controls.Add(label48);
			panel24.Dock = System.Windows.Forms.DockStyle.Fill;
			panel24.Location = new System.Drawing.Point(1, 103);
			panel24.Margin = new System.Windows.Forms.Padding(0);
			panel24.Name = "panel24";
			tableLayoutPanel3.SetRowSpan(panel24, 2);
			panel24.Size = new System.Drawing.Size(162, 199);
			panel24.TabIndex = 34;
			label48.AutoSize = true;
			label48.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label48.ForeColor = System.Drawing.Color.White;
			label48.Location = new System.Drawing.Point(83, 69);
			label48.Name = "label48";
			label48.Size = new System.Drawing.Size(74, 21);
			label48.TabIndex = 0;
			label48.Text = "出貨設定";
			panel23.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel23.Controls.Add(label35);
			panel23.Dock = System.Windows.Forms.DockStyle.Fill;
			panel23.Location = new System.Drawing.Point(482, 1);
			panel23.Margin = new System.Windows.Forms.Padding(0);
			panel23.Name = "panel23";
			panel23.Size = new System.Drawing.Size(165, 50);
			panel23.TabIndex = 20;
			label35.AutoSize = true;
			label35.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label35.ForeColor = System.Drawing.Color.White;
			label35.Location = new System.Drawing.Point(83, 16);
			label35.Name = "label35";
			label35.Size = new System.Drawing.Size(80, 21);
			label35.TabIndex = 0;
			label35.Text = "優惠價(2)";
			panel28.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel28.Controls.Add(label44);
			panel28.Dock = System.Windows.Forms.DockStyle.Fill;
			panel28.Location = new System.Drawing.Point(1, 1);
			panel28.Margin = new System.Windows.Forms.Padding(0);
			panel28.Name = "panel28";
			panel28.Size = new System.Drawing.Size(162, 50);
			panel28.TabIndex = 19;
			label44.AutoSize = true;
			label44.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label44.ForeColor = System.Drawing.Color.White;
			label44.Location = new System.Drawing.Point(77, 16);
			label44.Name = "label44";
			label44.Size = new System.Drawing.Size(80, 21);
			label44.TabIndex = 0;
			label44.Text = "優惠價(1)";
			tableLayoutPanel3.SetColumnSpan(panel25, 3);
			panel25.Controls.Add(radioButton1);
			panel25.Controls.Add(btn_settingDeliveryPrice);
			panel25.Controls.Add(myCheckBox1);
			panel25.Controls.Add(label47);
			panel25.Controls.Add(textBox1);
			panel25.Controls.Add(radioButton2);
			panel25.Location = new System.Drawing.Point(167, 106);
			panel25.Name = "panel25";
			tableLayoutPanel3.SetRowSpan(panel25, 2);
			panel25.Size = new System.Drawing.Size(796, 193);
			panel25.TabIndex = 36;
			radioButton1.AutoSize = true;
			radioButton1.Location = new System.Drawing.Point(7, 18);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new System.Drawing.Size(161, 28);
			radioButton1.TabIndex = 0;
			radioButton1.Text = "預設同銷售設定";
			radioButton1.UseVisualStyleBackColor = true;
			radioButton1.CheckedChanged += new System.EventHandler(radioButton1_CheckedChanged);
			btn_settingDeliveryPrice.BackColor = System.Drawing.Color.White;
			btn_settingDeliveryPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_settingDeliveryPrice.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_settingDeliveryPrice.ForeColor = System.Drawing.Color.Black;
			btn_settingDeliveryPrice.Image = POS_Client.Properties.Resources.ic_toc_black_24dp_1x;
			btn_settingDeliveryPrice.Location = new System.Drawing.Point(282, 101);
			btn_settingDeliveryPrice.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
			btn_settingDeliveryPrice.Name = "btn_settingDeliveryPrice";
			btn_settingDeliveryPrice.Size = new System.Drawing.Size(54, 33);
			btn_settingDeliveryPrice.TabIndex = 45;
			btn_settingDeliveryPrice.UseVisualStyleBackColor = false;
			btn_settingDeliveryPrice.Click += new System.EventHandler(btn_settingDeliveryPrice_Click);
			myCheckBox1.Location = new System.Drawing.Point(29, 143);
			myCheckBox1.Margin = new System.Windows.Forms.Padding(10, 8, 3, 3);
			myCheckBox1.Name = "myCheckBox1";
			myCheckBox1.Size = new System.Drawing.Size(139, 34);
			myCheckBox1.TabIndex = 1;
			myCheckBox1.Text = "開放出貨價";
			myCheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox1.UseVisualStyleBackColor = true;
			label47.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label47.AutoSize = true;
			label47.Location = new System.Drawing.Point(24, 105);
			label47.Name = "label47";
			label47.Size = new System.Drawing.Size(67, 24);
			label47.TabIndex = 44;
			label47.Text = "出貨價";
			textBox1.BackColor = System.Drawing.Color.White;
			textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
			textBox1.Enabled = false;
			textBox1.ForeColor = System.Drawing.SystemColors.WindowFrame;
			textBox1.Location = new System.Drawing.Point(95, 101);
			textBox1.Margin = new System.Windows.Forms.Padding(10);
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(180, 33);
			textBox1.TabIndex = 2;
			textBox1.Text = "請設定商品出貨價格";
			radioButton2.AutoSize = true;
			radioButton2.Location = new System.Drawing.Point(7, 59);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new System.Drawing.Size(142, 28);
			radioButton2.TabIndex = 1;
			radioButton2.Text = "自訂出貨設定";
			radioButton2.UseVisualStyleBackColor = true;
			flowLayoutPanel2.Controls.Add(tb_SpecialPrice1);
			flowLayoutPanel2.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel2.Name = "flowLayoutPanel2";
			flowLayoutPanel2.Size = new System.Drawing.Size(317, 50);
			flowLayoutPanel2.TabIndex = 25;
			tb_SpecialPrice1.BackColor = System.Drawing.Color.White;
			tb_SpecialPrice1.Cursor = System.Windows.Forms.Cursors.IBeam;
			tb_SpecialPrice1.Location = new System.Drawing.Point(10, 10);
			tb_SpecialPrice1.Margin = new System.Windows.Forms.Padding(10);
			tb_SpecialPrice1.Name = "tb_SpecialPrice1";
			tb_SpecialPrice1.Size = new System.Drawing.Size(298, 33);
			tb_SpecialPrice1.TabIndex = 1;
			tb_SpecialPrice1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(digitOnly_KeyPress);
			panel35.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel35.Controls.Add(label51);
			panel35.Dock = System.Windows.Forms.DockStyle.Fill;
			panel35.Location = new System.Drawing.Point(1, 52);
			panel35.Margin = new System.Windows.Forms.Padding(0);
			panel35.Name = "panel35";
			panel35.Size = new System.Drawing.Size(162, 50);
			panel35.TabIndex = 24;
			label51.AutoSize = true;
			label51.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label51.ForeColor = System.Drawing.Color.White;
			label51.Location = new System.Drawing.Point(83, 16);
			label51.Name = "label51";
			label51.Size = new System.Drawing.Size(74, 21);
			label51.TabIndex = 0;
			label51.Text = "其他設定";
			tb_SpecialPrice2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SpecialPrice2.Location = new System.Drawing.Point(658, 11);
			tb_SpecialPrice2.Margin = new System.Windows.Forms.Padding(10);
			tb_SpecialPrice2.Name = "tb_SpecialPrice2";
			tb_SpecialPrice2.Size = new System.Drawing.Size(298, 33);
			tb_SpecialPrice2.TabIndex = 32;
			tb_SpecialPrice2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(digitOnly_KeyPress);
			tableLayoutPanel3.SetColumnSpan(flowLayoutPanel3, 3);
			flowLayoutPanel3.Controls.Add(mCB_OpenPrice);
			flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel3.Location = new System.Drawing.Point(164, 52);
			flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel3.Name = "flowLayoutPanel3";
			flowLayoutPanel3.Size = new System.Drawing.Size(802, 50);
			flowLayoutPanel3.TabIndex = 25;
			mCB_OpenPrice.Location = new System.Drawing.Point(10, 8);
			mCB_OpenPrice.Margin = new System.Windows.Forms.Padding(10, 8, 3, 3);
			mCB_OpenPrice.Name = "mCB_OpenPrice";
			mCB_OpenPrice.Size = new System.Drawing.Size(252, 34);
			mCB_OpenPrice.TabIndex = 0;
			mCB_OpenPrice.Text = "開放售價(包括允許折讓)";
			mCB_OpenPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			mCB_OpenPrice.UseVisualStyleBackColor = true;
			InventoryManage.Controls.Add(dateTimePicker5);
			InventoryManage.Controls.Add(label54);
			InventoryManage.Controls.Add(dateTimePicker6);
			InventoryManage.Controls.Add(label55);
			InventoryManage.Controls.Add(label53);
			InventoryManage.Controls.Add(dataGridView1);
			InventoryManage.Controls.Add(tableLayoutPanel2);
			InventoryManage.Controls.Add(label19);
			InventoryManage.Location = new System.Drawing.Point(4, 47);
			InventoryManage.Name = "InventoryManage";
			InventoryManage.Padding = new System.Windows.Forms.Padding(3);
			InventoryManage.Size = new System.Drawing.Size(973, 571);
			InventoryManage.TabIndex = 2;
			InventoryManage.Text = "庫存管理";
			InventoryManage.UseVisualStyleBackColor = true;
			dateTimePicker5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker5.CustomFormat = "yyyy-MM-dd";
			dateTimePicker5.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker5.Location = new System.Drawing.Point(561, 121);
			dateTimePicker5.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker5.Name = "dateTimePicker5";
			dateTimePicker5.ShowCheckBox = true;
			dateTimePicker5.Size = new System.Drawing.Size(181, 33);
			dateTimePicker5.TabIndex = 62;
			dateTimePicker5.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateTimePicker5.ValueChanged += new System.EventHandler(dateTimePicker5_ValueChanged);
			label54.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label54.AutoSize = true;
			label54.Location = new System.Drawing.Point(748, 125);
			label54.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label54.Name = "label54";
			label54.Size = new System.Drawing.Size(24, 24);
			label54.TabIndex = 64;
			label54.Text = "~";
			dateTimePicker6.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker6.CustomFormat = "yyyy-MM-dd";
			dateTimePicker6.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker6.Location = new System.Drawing.Point(778, 121);
			dateTimePicker6.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker6.Name = "dateTimePicker6";
			dateTimePicker6.ShowCheckBox = true;
			dateTimePicker6.Size = new System.Drawing.Size(181, 33);
			dateTimePicker6.TabIndex = 63;
			dateTimePicker6.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateTimePicker6.ValueChanged += new System.EventHandler(dateTimePicker6_ValueChanged);
			label55.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label55.AutoSize = true;
			label55.Location = new System.Drawing.Point(438, 127);
			label55.Name = "label55";
			label55.Size = new System.Drawing.Size(124, 24);
			label55.TabIndex = 61;
			label55.Text = "日期區間查詢";
			label53.AutoSize = true;
			label53.Font = new System.Drawing.Font("微軟正黑體", 10f, System.Drawing.FontStyle.Bold);
			label53.ForeColor = System.Drawing.Color.Black;
			label53.Location = new System.Drawing.Point(128, 130);
			label53.Name = "label53";
			label53.Size = new System.Drawing.Size(88, 18);
			label53.TabIndex = 60;
			label53.Text = "(預設一週內)";
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.BackgroundColor = System.Drawing.Color.White;
			dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(3);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(Column1, Column2, Column4, Column5, Column3, Column6);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.Location = new System.Drawing.Point(3, 157);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 35;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView1.Size = new System.Drawing.Size(967, 411);
			dataGridView1.TabIndex = 59;
			dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
			Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column1.HeaderText = "調整日期時間";
			Column1.MinimumWidth = 150;
			Column1.Name = "Column1";
			Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 150;
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column2.HeaderText = "類型";
			Column2.MinimumWidth = 60;
			Column2.Name = "Column2";
			Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.Width = 60;
			Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			Column4.HeaderText = "進貨廠商";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column5.HeaderText = "進貨單號";
			Column5.MinimumWidth = 100;
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column3.HeaderText = "數量";
			Column3.MinimumWidth = 60;
			Column3.Name = "Column3";
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column3.Width = 60;
			Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column6.HeaderText = "調整後現品庫存";
			Column6.MinimumWidth = 155;
			Column6.Name = "Column6";
			Column6.ReadOnly = true;
			Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column6.Width = 155;
			tableLayoutPanel2.BackColor = System.Drawing.Color.White;
			tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel2.ColumnCount = 2;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel2.Controls.Add(panel22, 0, 0);
			tableLayoutPanel2.Controls.Add(panel29, 0, 1);
			tableLayoutPanel2.Controls.Add(l_InventoryTotal, 1, 0);
			tableLayoutPanel2.Controls.Add(panel16, 1, 1);
			tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 2;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.30158f));
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.69842f));
			tableLayoutPanel2.Size = new System.Drawing.Size(967, 114);
			tableLayoutPanel2.TabIndex = 5;
			panel22.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel22.Controls.Add(label33);
			panel22.Dock = System.Windows.Forms.DockStyle.Fill;
			panel22.Location = new System.Drawing.Point(1, 1);
			panel22.Margin = new System.Windows.Forms.Padding(0);
			panel22.Name = "panel22";
			panel22.Size = new System.Drawing.Size(162, 41);
			panel22.TabIndex = 19;
			label33.AutoSize = true;
			label33.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label33.ForeColor = System.Drawing.Color.White;
			label33.Location = new System.Drawing.Point(83, 11);
			label33.Name = "label33";
			label33.Size = new System.Drawing.Size(74, 21);
			label33.TabIndex = 0;
			label33.Text = "現品庫存";
			panel29.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel29.Controls.Add(label43);
			panel29.Dock = System.Windows.Forms.DockStyle.Fill;
			panel29.Location = new System.Drawing.Point(1, 43);
			panel29.Margin = new System.Windows.Forms.Padding(0);
			panel29.Name = "panel29";
			panel29.Size = new System.Drawing.Size(162, 70);
			panel29.TabIndex = 24;
			label43.AutoSize = true;
			label43.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label43.ForeColor = System.Drawing.Color.White;
			label43.Location = new System.Drawing.Point(83, 23);
			label43.Name = "label43";
			label43.Size = new System.Drawing.Size(74, 21);
			label43.TabIndex = 0;
			label43.Text = "庫存調整";
			l_InventoryTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_InventoryTotal.AutoSize = true;
			l_InventoryTotal.Location = new System.Drawing.Point(167, 9);
			l_InventoryTotal.Name = "l_InventoryTotal";
			l_InventoryTotal.Size = new System.Drawing.Size(33, 24);
			l_InventoryTotal.TabIndex = 25;
			l_InventoryTotal.Text = "{0}";
			panel16.BackColor = System.Drawing.Color.White;
			panel16.Controls.Add(btn_minus);
			panel16.Controls.Add(btn_plus);
			panel16.Controls.Add(label24);
			panel16.Controls.Add(btn_enter);
			panel16.Controls.Add(tb_adjustCount);
			panel16.Controls.Add(cb_adjustType);
			panel16.Dock = System.Windows.Forms.DockStyle.Fill;
			panel16.Location = new System.Drawing.Point(164, 43);
			panel16.Margin = new System.Windows.Forms.Padding(0);
			panel16.Name = "panel16";
			panel16.Size = new System.Drawing.Size(802, 70);
			panel16.TabIndex = 24;
			btn_minus.BackColor = System.Drawing.SystemColors.ButtonShadow;
			btn_minus.Location = new System.Drawing.Point(231, 4);
			btn_minus.Name = "btn_minus";
			btn_minus.Size = new System.Drawing.Size(40, 31);
			btn_minus.TabIndex = 44;
			btn_minus.Text = "-";
			btn_minus.UseVisualStyleBackColor = false;
			btn_minus.Click += new System.EventHandler(btn_minus_Click);
			btn_plus.BackColor = System.Drawing.Color.Gainsboro;
			btn_plus.Location = new System.Drawing.Point(187, 4);
			btn_plus.Name = "btn_plus";
			btn_plus.Size = new System.Drawing.Size(40, 31);
			btn_plus.TabIndex = 43;
			btn_plus.Text = "+";
			btn_plus.UseVisualStyleBackColor = false;
			btn_plus.Click += new System.EventHandler(btn_plus_Click);
			label24.AutoSize = true;
			label24.Font = new System.Drawing.Font("微軟正黑體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label24.ForeColor = System.Drawing.Color.Red;
			label24.Location = new System.Drawing.Point(8, 43);
			label24.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			label24.Name = "label24";
			label24.Size = new System.Drawing.Size(750, 19);
			label24.TabIndex = 6;
			label24.Text = "*請輸入數字，正負數值請點選「＋ / －」按鈕，預設為「－」扣除。將依輸入正負數字增加或扣除目前庫存數字";
			btn_enter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_enter.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_enter.Image = POS_Client.Properties.Resources.ic_input_black_24dp_1x;
			btn_enter.Location = new System.Drawing.Point(683, 4);
			btn_enter.Name = "btn_enter";
			btn_enter.Size = new System.Drawing.Size(48, 29);
			btn_enter.TabIndex = 42;
			btn_enter.UseVisualStyleBackColor = true;
			btn_enter.Click += new System.EventHandler(btn_enter_Click);
			tb_adjustCount.Anchor = System.Windows.Forms.AnchorStyles.None;
			tb_adjustCount.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_adjustCount.Location = new System.Drawing.Point(284, 2);
			tb_adjustCount.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			tb_adjustCount.Name = "tb_adjustCount";
			tb_adjustCount.Size = new System.Drawing.Size(394, 33);
			tb_adjustCount.TabIndex = 41;
			tb_adjustCount.Text = "請輸入調整數字";
			tb_adjustCount.Enter += new System.EventHandler(tb_adjustCount_Enter);
			tb_adjustCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tb_adjustCount_KeyPress);
			tb_adjustCount.KeyUp += new System.Windows.Forms.KeyEventHandler(tb_adjustCount_KeyUp);
			tb_adjustCount.Leave += new System.EventHandler(tb_adjustCount_Leave);
			cb_adjustType.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_adjustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_adjustType.FormattingEnabled = true;
			cb_adjustType.Location = new System.Drawing.Point(7, 3);
			cb_adjustType.Margin = new System.Windows.Forms.Padding(10, 8, 3, 3);
			cb_adjustType.Name = "cb_adjustType";
			cb_adjustType.Size = new System.Drawing.Size(173, 32);
			cb_adjustType.TabIndex = 40;
			cb_adjustType.SelectedValueChanged += new System.EventHandler(cb_adjustType_SelectedValueChanged);
			label19.AutoSize = true;
			label19.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label19.ForeColor = System.Drawing.Color.Black;
			label19.Location = new System.Drawing.Point(17, 128);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(106, 21);
			label19.TabIndex = 3;
			label19.Text = "調整記錄一覽";
			ConsumeRecord.Controls.Add(dataGridView3);
			ConsumeRecord.Controls.Add(label46);
			ConsumeRecord.Controls.Add(label52);
			ConsumeRecord.Controls.Add(dateTimePicker1);
			ConsumeRecord.Controls.Add(label49);
			ConsumeRecord.Controls.Add(dateTimePicker4);
			ConsumeRecord.Controls.Add(label50);
			ConsumeRecord.Controls.Add(comboBox4);
			ConsumeRecord.Controls.Add(label42);
			ConsumeRecord.Location = new System.Drawing.Point(4, 47);
			ConsumeRecord.Name = "ConsumeRecord";
			ConsumeRecord.Padding = new System.Windows.Forms.Padding(3);
			ConsumeRecord.Size = new System.Drawing.Size(973, 571);
			ConsumeRecord.TabIndex = 3;
			ConsumeRecord.Text = "銷售紀錄";
			ConsumeRecord.UseVisualStyleBackColor = true;
			dataGridView3.AllowUserToAddRows = false;
			dataGridView3.AllowUserToDeleteRows = false;
			dataGridView3.AllowUserToResizeColumns = false;
			dataGridView3.AllowUserToResizeRows = false;
			dataGridView3.BackgroundColor = System.Drawing.Color.White;
			dataGridView3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridView3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(3);
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView3.Columns.AddRange(dataGridViewTextBoxColumn7, dataGridViewLinkColumn1, dataGridViewTextBoxColumn8, dataGridViewTextBoxColumn9, dataGridViewTextBoxColumn10, dataGridViewTextBoxColumn11, dataGridViewTextBoxColumn12, dataGridViewTextBoxColumn13, hidden_status2);
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView3.DefaultCellStyle = dataGridViewCellStyle4;
			dataGridView3.Dock = System.Windows.Forms.DockStyle.Bottom;
			dataGridView3.EnableHeadersVisualStyles = false;
			dataGridView3.Location = new System.Drawing.Point(3, 76);
			dataGridView3.Name = "dataGridView3";
			dataGridView3.ReadOnly = true;
			dataGridView3.RowHeadersVisible = false;
			dataGridView3.RowTemplate.Height = 35;
			dataGridView3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView3.Size = new System.Drawing.Size(967, 492);
			dataGridView3.TabIndex = 61;
			dataGridView3.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView3_CellContentClick);
			dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn7.HeaderText = "銷售/編修日期時間";
			dataGridViewTextBoxColumn7.MinimumWidth = 150;
			dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			dataGridViewTextBoxColumn7.ReadOnly = true;
			dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn7.Width = 200;
			dataGridViewLinkColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewLinkColumn1.HeaderText = "銷售單號";
			dataGridViewLinkColumn1.MinimumWidth = 60;
			dataGridViewLinkColumn1.Name = "dataGridViewLinkColumn1";
			dataGridViewLinkColumn1.ReadOnly = true;
			dataGridViewLinkColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewLinkColumn1.Width = 240;
			dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle5;
			dataGridViewTextBoxColumn8.HeaderText = "銷售單總額";
			dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			dataGridViewTextBoxColumn8.ReadOnly = true;
			dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn8.Width = 125;
			dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle6;
			dataGridViewTextBoxColumn9.HeaderText = "單價";
			dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			dataGridViewTextBoxColumn9.ReadOnly = true;
			dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn9.Width = 70;
			dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle7;
			dataGridViewTextBoxColumn10.HeaderText = "數量";
			dataGridViewTextBoxColumn10.MinimumWidth = 60;
			dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			dataGridViewTextBoxColumn10.ReadOnly = true;
			dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn10.Width = 60;
			dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle8;
			dataGridViewTextBoxColumn11.HeaderText = "折扣/讓";
			dataGridViewTextBoxColumn11.MinimumWidth = 100;
			dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			dataGridViewTextBoxColumn11.ReadOnly = true;
			dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle9;
			dataGridViewTextBoxColumn12.HeaderText = "合計";
			dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			dataGridViewTextBoxColumn12.ReadOnly = true;
			dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn12.Width = 80;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle10;
			dataGridViewTextBoxColumn13.HeaderText = "狀態";
			dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			dataGridViewTextBoxColumn13.ReadOnly = true;
			hidden_status2.HeaderText = "hidden_status";
			hidden_status2.Name = "hidden_status2";
			hidden_status2.ReadOnly = true;
			hidden_status2.Visible = false;
			label46.AutoSize = true;
			label46.Font = new System.Drawing.Font("微軟正黑體", 10f, System.Drawing.FontStyle.Bold);
			label46.ForeColor = System.Drawing.Color.Black;
			label46.Location = new System.Drawing.Point(506, 55);
			label46.Name = "label46";
			label46.Size = new System.Drawing.Size(88, 18);
			label46.TabIndex = 52;
			label46.Text = "(預設一週內)";
			label52.AutoSize = true;
			label52.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label52.ForeColor = System.Drawing.Color.Black;
			label52.Location = new System.Drawing.Point(394, 52);
			label52.Name = "label52";
			label52.Size = new System.Drawing.Size(106, 21);
			label52.TabIndex = 51;
			label52.Text = "銷售記錄一覽";
			dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker1.Location = new System.Drawing.Point(400, 6);
			dateTimePicker1.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker1.Name = "dateTimePicker1";
			dateTimePicker1.ShowCheckBox = true;
			dateTimePicker1.Size = new System.Drawing.Size(181, 33);
			dateTimePicker1.TabIndex = 48;
			dateTimePicker1.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateTimePicker1.ValueChanged += new System.EventHandler(dateTimePicker1_ValueChanged);
			label49.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label49.AutoSize = true;
			label49.Location = new System.Drawing.Point(587, 10);
			label49.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label49.Name = "label49";
			label49.Size = new System.Drawing.Size(24, 24);
			label49.TabIndex = 50;
			label49.Text = "~";
			dateTimePicker4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker4.CustomFormat = "yyyy-MM-dd";
			dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker4.Location = new System.Drawing.Point(617, 6);
			dateTimePicker4.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker4.Name = "dateTimePicker4";
			dateTimePicker4.ShowCheckBox = true;
			dateTimePicker4.Size = new System.Drawing.Size(181, 33);
			dateTimePicker4.TabIndex = 49;
			dateTimePicker4.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateTimePicker4.ValueChanged += new System.EventHandler(dateTimePicker4_ValueChanged);
			label50.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label50.AutoSize = true;
			label50.Location = new System.Drawing.Point(277, 12);
			label50.Name = "label50";
			label50.Size = new System.Drawing.Size(124, 24);
			label50.TabIndex = 47;
			label50.Text = "日期區間查詢";
			comboBox4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox4.FormattingEnabled = true;
			comboBox4.Location = new System.Drawing.Point(120, 8);
			comboBox4.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			comboBox4.Name = "comboBox4";
			comboBox4.Size = new System.Drawing.Size(128, 32);
			comboBox4.TabIndex = 40;
			comboBox4.SelectedIndexChanged += new System.EventHandler(comboBox4_SelectedIndexChanged);
			label42.Anchor = System.Windows.Forms.AnchorStyles.None;
			label42.AutoSize = true;
			label42.ForeColor = System.Drawing.Color.Black;
			label42.Location = new System.Drawing.Point(5, 12);
			label42.Margin = new System.Windows.Forms.Padding(0);
			label42.Name = "label42";
			label42.Size = new System.Drawing.Size(105, 24);
			label42.TabIndex = 0;
			label42.Text = "條件篩選：";
			tabPage1.Controls.Add(dataGridView2);
			tabPage1.Controls.Add(label30);
			tabPage1.Controls.Add(label29);
			tabPage1.Controls.Add(dateTimePicker2);
			tabPage1.Controls.Add(label28);
			tabPage1.Controls.Add(dateTimePicker3);
			tabPage1.Controls.Add(label26);
			tabPage1.Controls.Add(comboBox1);
			tabPage1.Controls.Add(label25);
			tabPage1.Location = new System.Drawing.Point(4, 47);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new System.Windows.Forms.Padding(3);
			tabPage1.Size = new System.Drawing.Size(973, 571);
			tabPage1.TabIndex = 4;
			tabPage1.Text = "出貨紀錄";
			tabPage1.UseVisualStyleBackColor = true;
			dataGridView2.AllowUserToAddRows = false;
			dataGridView2.AllowUserToDeleteRows = false;
			dataGridView2.AllowUserToResizeColumns = false;
			dataGridView2.AllowUserToResizeRows = false;
			dataGridView2.BackgroundColor = System.Drawing.Color.White;
			dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle11.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(3);
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
			dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView2.Columns.AddRange(dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, Column7, hidden_status);
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView2.DefaultCellStyle = dataGridViewCellStyle12;
			dataGridView2.Dock = System.Windows.Forms.DockStyle.Bottom;
			dataGridView2.EnableHeadersVisualStyles = false;
			dataGridView2.Location = new System.Drawing.Point(3, 76);
			dataGridView2.Name = "dataGridView2";
			dataGridView2.ReadOnly = true;
			dataGridView2.RowHeadersVisible = false;
			dataGridView2.RowTemplate.Height = 35;
			dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView2.Size = new System.Drawing.Size(967, 492);
			dataGridView2.TabIndex = 60;
			dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView2_CellContentClick);
			dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn1.HeaderText = "出貨/編修日期時間";
			dataGridViewTextBoxColumn1.MinimumWidth = 150;
			dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			dataGridViewTextBoxColumn1.ReadOnly = true;
			dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn1.Width = 230;
			dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn2.HeaderText = "出貨系統單號";
			dataGridViewTextBoxColumn2.MinimumWidth = 60;
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.ReadOnly = true;
			dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewTextBoxColumn2.Width = 240;
			dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle13;
			dataGridViewTextBoxColumn3.HeaderText = "出貨單總額";
			dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			dataGridViewTextBoxColumn3.ReadOnly = true;
			dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn3.Width = 125;
			dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle14;
			dataGridViewTextBoxColumn4.HeaderText = "出貨價";
			dataGridViewTextBoxColumn4.MinimumWidth = 100;
			dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			dataGridViewTextBoxColumn4.ReadOnly = true;
			dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle15;
			dataGridViewTextBoxColumn5.HeaderText = "數量";
			dataGridViewTextBoxColumn5.MinimumWidth = 60;
			dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			dataGridViewTextBoxColumn5.ReadOnly = true;
			dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn5.Width = 60;
			dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle16;
			dataGridViewTextBoxColumn6.HeaderText = "合計";
			dataGridViewTextBoxColumn6.MinimumWidth = 100;
			dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			dataGridViewTextBoxColumn6.ReadOnly = true;
			dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn6.Width = 130;
			Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			Column7.DefaultCellStyle = dataGridViewCellStyle17;
			Column7.HeaderText = "狀態";
			Column7.Name = "Column7";
			Column7.ReadOnly = true;
			hidden_status.HeaderText = "hidden_status";
			hidden_status.Name = "hidden_status";
			hidden_status.ReadOnly = true;
			hidden_status.Visible = false;
			label30.AutoSize = true;
			label30.Font = new System.Drawing.Font("微軟正黑體", 10f, System.Drawing.FontStyle.Bold);
			label30.ForeColor = System.Drawing.Color.Black;
			label30.Location = new System.Drawing.Point(507, 55);
			label30.Name = "label30";
			label30.Size = new System.Drawing.Size(88, 18);
			label30.TabIndex = 48;
			label30.Text = "(預設一週內)";
			label29.AutoSize = true;
			label29.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label29.ForeColor = System.Drawing.Color.Black;
			label29.Location = new System.Drawing.Point(395, 52);
			label29.Name = "label29";
			label29.Size = new System.Drawing.Size(106, 21);
			label29.TabIndex = 47;
			label29.Text = "出貨記錄一覽";
			dateTimePicker2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker2.Location = new System.Drawing.Point(400, 6);
			dateTimePicker2.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker2.Name = "dateTimePicker2";
			dateTimePicker2.ShowCheckBox = true;
			dateTimePicker2.Size = new System.Drawing.Size(181, 33);
			dateTimePicker2.TabIndex = 44;
			dateTimePicker2.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateTimePicker2.ValueChanged += new System.EventHandler(dateTimePicker2_ValueChanged);
			label28.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label28.AutoSize = true;
			label28.Location = new System.Drawing.Point(587, 10);
			label28.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label28.Name = "label28";
			label28.Size = new System.Drawing.Size(24, 24);
			label28.TabIndex = 46;
			label28.Text = "~";
			dateTimePicker3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker3.CustomFormat = "yyyy-MM-dd";
			dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker3.Location = new System.Drawing.Point(617, 6);
			dateTimePicker3.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker3.Name = "dateTimePicker3";
			dateTimePicker3.ShowCheckBox = true;
			dateTimePicker3.Size = new System.Drawing.Size(181, 33);
			dateTimePicker3.TabIndex = 45;
			dateTimePicker3.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateTimePicker3.ValueChanged += new System.EventHandler(dateTimePicker3_ValueChanged);
			label26.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label26.AutoSize = true;
			label26.Location = new System.Drawing.Point(277, 12);
			label26.Name = "label26";
			label26.Size = new System.Drawing.Size(124, 24);
			label26.TabIndex = 43;
			label26.Text = "日期區間查詢";
			comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new System.Drawing.Point(120, 8);
			comboBox1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new System.Drawing.Size(128, 32);
			comboBox1.TabIndex = 42;
			comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
			label25.Anchor = System.Windows.Forms.AnchorStyles.None;
			label25.AutoSize = true;
			label25.ForeColor = System.Drawing.Color.Black;
			label25.Location = new System.Drawing.Point(5, 12);
			label25.Margin = new System.Windows.Forms.Padding(0);
			label25.Name = "label25";
			label25.Size = new System.Drawing.Size(105, 24);
			label25.TabIndex = 41;
			label25.Text = "條件篩選：";
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(btn_printBarcode);
			base.Controls.Add(btn_back);
			base.Controls.Add(DeliveryRecord);
			Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "frmEditCommodity";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmEditCommodity_Load);
			base.Controls.SetChildIndex(DeliveryRecord, 0);
			base.Controls.SetChildIndex(btn_back, 0);
			base.Controls.SetChildIndex(btn_printBarcode, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			DeliveryRecord.ResumeLayout(false);
			BasicData.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel26.ResumeLayout(false);
			panel26.PerformLayout();
			panel17.ResumeLayout(false);
			panel17.PerformLayout();
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
			flowLayoutPanel6.ResumeLayout(false);
			flowLayoutPanel7.ResumeLayout(false);
			flowLayoutPanel7.PerformLayout();
			AdvancedConfig.ResumeLayout(false);
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			panel24.ResumeLayout(false);
			panel24.PerformLayout();
			panel23.ResumeLayout(false);
			panel23.PerformLayout();
			panel28.ResumeLayout(false);
			panel28.PerformLayout();
			panel25.ResumeLayout(false);
			panel25.PerformLayout();
			flowLayoutPanel2.ResumeLayout(false);
			flowLayoutPanel2.PerformLayout();
			panel35.ResumeLayout(false);
			panel35.PerformLayout();
			flowLayoutPanel3.ResumeLayout(false);
			InventoryManage.ResumeLayout(false);
			InventoryManage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			panel22.ResumeLayout(false);
			panel22.PerformLayout();
			panel29.ResumeLayout(false);
			panel29.PerformLayout();
			panel16.ResumeLayout(false);
			panel16.PerformLayout();
			ConsumeRecord.ResumeLayout(false);
			ConsumeRecord.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
			tabPage1.ResumeLayout(false);
			tabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
			ResumeLayout(false);
		}
	}
}
