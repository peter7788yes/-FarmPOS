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
	public class frmEditSupplier : MasterThinForm
	{
		private string _SupplierNo = "";

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

		private TextBox tb_SupplierName;

		private TextBox tb_Email;

		private ComboBox cb_type;

		private TextBox tb_Mobile;

		private Panel panel5;

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

		private TabPage tabPage2;

		private Label label19;

		private TableLayoutPanel tableLayoutPanel2;

		private Panel panel17;

		private Label label27;

		private Panel panel22;

		private Label label33;

		private Label l_lastPurchaseDate;

		public DataGridView dataGridView1;

		public Label l_Total;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewLinkColumn Column2;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn hidden_status;

		private TabPage tabPage3;

		public DataGridView dataGridView2;

		private Label label11;

		private TableLayoutPanel tableLayoutPanel3;

		private Panel panel18;

		private Label label10;

		public Label l_DeliveryTot;

		private Panel panel16;

		private Label label9;

		private Panel panel19;

		private Label l_lastDeliveryDate;

		private Panel panel20;

		private Panel panel21;

		private Label label20;

		private Panel panel11;

		private Label label15;

		private Panel panel23;

		private Label label21;

		private Button btn_checkVendorID;

		private TextBox tb_vendorId;

		private TextBox tb_vendorName;

		private Label label22;

		private Label label24;

		private CheckBox cb_DeliveryType;

		private CheckBox cb_PurchaseType;

		private DataGridViewTextBoxColumn hidden_status_delivery;

		private DataGridViewLinkColumn dataGridViewLinkColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DateTimePicker dateTimePicker1;

		private Label label49;

		private DateTimePicker dateTimePicker4;

		private Label label50;

		private ComboBox comboBox4;

		private Label label42;

		private Label label46;

		private DateTimePicker dateTimePicker2;

		private Label label26;

		private DateTimePicker dateTimePicker3;

		private Label label28;

		private ComboBox comboBox1;

		private Label label29;

		private Label label25;

		public frmEditSupplier(string SupplierNo)
			: base("編修廠商")
		{
			InitializeComponent();
			_SupplierNo = SupplierNo;
		}

		private void frmEditSupplier_Load(object sender, EventArgs e)
		{
			ComboboxItem[] items = new ComboboxItem[4]
			{
				new ComboboxItem("全部", "9"),
				new ComboboxItem("正常", "0"),
				new ComboboxItem("正常(變更)", "1"),
				new ComboboxItem("取消", "2")
			};
			comboBox4.Items.AddRange(items);
			comboBox4.SelectedIndex = 0;
			dateTimePicker1.Value = DateTime.Today.AddDays(-7.0);
			dateTimePicker4.Value = DateTime.Today;
			ComboboxItem[] items2 = new ComboboxItem[4]
			{
				new ComboboxItem("全部", "9"),
				new ComboboxItem("正常", "0"),
				new ComboboxItem("取消", "1"),
				new ComboboxItem("變更(編修)", "2")
			};
			comboBox1.Items.AddRange(items2);
			comboBox1.SelectedIndex = 0;
			dateTimePicker2.Value = DateTime.Today.AddDays(-7.0);
			dateTimePicker3.Value = DateTime.Today;
			showBasicData();
			showPurchaseLog();
			showDeliveryLog();
		}

		private void showBasicData()
		{
			DataTable dataSource = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "ADDRCITY", "", "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			cb_city.DisplayMember = "city";
			cb_city.ValueMember = "cityno";
			cb_city.DataSource = dataSource;
			ComboboxItem[] array = new ComboboxItem[2]
			{
				new ComboboxItem("正常", "0"),
				new ComboboxItem("停用", "1")
			};
			cb_status.Items.AddRange(array);
			cb_status.SelectedIndex = 0;
			ComboboxItem[] array2 = new ComboboxItem[2]
			{
				new ComboboxItem("本地廠商", "0"),
				new ComboboxItem("進口廠商", "1")
			};
			cb_type.Items.AddRange(array2);
			cb_type.SelectedIndex = 0;
			tb_supplierNo.Text = _SupplierNo;
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_Supplier", "SupplierNo = {0}", "", null, new string[1]
			{
				_SupplierNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			try
			{
				if (dataTable.Rows.Count > 0)
				{
					ComboboxItem[] array3;
					if (Program.GetDBVersion() >= 5)
					{
						string value = dataTable.Rows[0]["vendorType"].ToString();
						if ("0".Equals(value))
						{
							cb_PurchaseType.Checked = true;
							cb_DeliveryType.Checked = true;
						}
						else if ("1".Equals(value))
						{
							cb_PurchaseType.Checked = true;
							cb_DeliveryType.Checked = false;
						}
						else if ("2".Equals(value))
						{
							cb_PurchaseType.Checked = false;
							cb_DeliveryType.Checked = true;
						}
						else
						{
							cb_PurchaseType.Checked = false;
							cb_DeliveryType.Checked = false;
						}
						tb_vendorId.Text = dataTable.Rows[0]["vendorId"].ToString();
						tb_vendorName.Text = dataTable.Rows[0]["vendorName"].ToString();
						tb_SupplierName.Text = dataTable.Rows[0]["SupplierName"].ToString();
						tb_SupplierIdNo.Text = dataTable.Rows[0]["SupplierIdNo"].ToString();
						tb_DutyName.Text = dataTable.Rows[0]["DutyName"].ToString();
						tb_TelNo.Text = dataTable.Rows[0]["TelNo"].ToString();
						tb_TelExt.Text = dataTable.Rows[0]["TelExt"].ToString();
						tb_Fax.Text = dataTable.Rows[0]["Fax"].ToString();
						tb_Mobile.Text = dataTable.Rows[0]["Mobile"].ToString();
						tb_ContactName.Text = dataTable.Rows[0]["ContactName"].ToString();
						tb_ContactJob.Text = dataTable.Rows[0]["ContactJob"].ToString();
						cb_city.SelectedValue = dataTable.Rows[0]["CityNo"].ToString();
						cb_city_SelectedIndexChanged(null, null);
						cb_area.SelectedValue = dataTable.Rows[0]["Zipcode"].ToString();
						cb_area_SelectedIndexChanged(null, null);
						tb_Address.Text = dataTable.Rows[0]["Address"].ToString();
						tb_Email.Text = dataTable.Rows[0]["Email"].ToString();
						array3 = array;
						foreach (ComboboxItem comboboxItem in array3)
						{
							if (comboboxItem.Value.Equals(dataTable.Rows[0]["Status"].ToString()))
							{
								cb_status.SelectedItem = comboboxItem;
							}
						}
						array3 = array2;
						foreach (ComboboxItem comboboxItem2 in array3)
						{
							if (comboboxItem2.Value.Equals(dataTable.Rows[0]["Type"].ToString()))
							{
								cb_type.SelectedItem = comboboxItem2;
							}
						}
						return;
					}
					tb_SupplierName.Text = dataTable.Rows[0]["SupplierName"].ToString();
					tb_SupplierIdNo.Text = dataTable.Rows[0]["SupplierIdNo"].ToString();
					tb_DutyName.Text = dataTable.Rows[0]["DutyName"].ToString();
					tb_TelNo.Text = dataTable.Rows[0]["TelNo"].ToString();
					tb_TelExt.Text = dataTable.Rows[0]["TelExt"].ToString();
					tb_Fax.Text = dataTable.Rows[0]["Fax"].ToString();
					tb_Mobile.Text = dataTable.Rows[0]["Mobile"].ToString();
					tb_ContactName.Text = dataTable.Rows[0]["ContactName"].ToString();
					tb_ContactJob.Text = dataTable.Rows[0]["ContactJob"].ToString();
					cb_city.SelectedValue = dataTable.Rows[0]["CityNo"].ToString();
					cb_city_SelectedIndexChanged(null, null);
					cb_area.SelectedValue = dataTable.Rows[0]["Zipcode"].ToString();
					cb_area_SelectedIndexChanged(null, null);
					tb_Address.Text = dataTable.Rows[0]["Address"].ToString();
					tb_Email.Text = dataTable.Rows[0]["Email"].ToString();
					array3 = array;
					foreach (ComboboxItem comboboxItem3 in array3)
					{
						if (comboboxItem3.Value.Equals(dataTable.Rows[0]["Status"].ToString()))
						{
							cb_status.SelectedItem = comboboxItem3;
						}
					}
					array3 = array2;
					foreach (ComboboxItem comboboxItem4 in array3)
					{
						if (comboboxItem4.Value.Equals(dataTable.Rows[0]["Type"].ToString()))
						{
							cb_type.SelectedItem = comboboxItem4;
						}
					}
				}
				else
				{
					MessageBox.Show("會員號碼錯誤!");
					backToPreviousForm();
				}
			}
			catch (Exception)
			{
			}
		}

		private void showPurchaseLog()
		{
			dataGridView1.Rows.Clear();
			string text = "";
			switch (comboBox4.SelectedIndex.ToString())
			{
			case "1":
				text = " and Status = 0 ";
				break;
			case "2":
				text = " and Status = 1 ";
				break;
			case "3":
				text = " and Status = 2 ";
				break;
			}
			string sql = "select * from hypos_PurchaseGoods_Master where SupplierNo = '" + _SupplierNo + "' " + text + " and UpdateDate between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and datetime(date( '" + dateTimePicker4.Value.ToString("yyyy-MM-dd") + "' ), '+1 days')  Order By UpdateDate DESC";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			l_lastPurchaseDate.Text = dataTable.Rows[0]["PurchaseDate"].ToString();
			int num = 0;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				num += int.Parse(dataTable.Rows[i]["Total"].ToString());
				string text2 = dataTable.Rows[i]["Status"].ToString();
				switch (text2)
				{
				case "0":
					text2 = "正常";
					break;
				case "1":
					text2 = "正常(變更)";
					break;
				case "2":
					text2 = "取消";
					break;
				}
				dataGridView1.Rows.Insert(i, dataTable.Rows[i]["UpdateDate"].ToString(), dataTable.Rows[i]["PurchaseNo"].ToString(), dataTable.Rows[i]["Total"].ToString(), text2, dataTable.Rows[i]["Status"].ToString());
			}
			l_Total.Text = num.ToString();
		}

		public void showDeliveryLog()
		{
			dataGridView2.Rows.Clear();
			string text = "";
			switch (comboBox1.SelectedIndex.ToString())
			{
			case "1":
				text = " and status = 0 ";
				break;
			case "2":
				text = " and status = 1 ";
				break;
			case "3":
				text = " and status = 2 ";
				break;
			}
			string sql = "select * from hypos_DeliveryGoods_Master where vendorNo = '" + _SupplierNo + "' " + text + " and editDate between '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and datetime(date( '" + dateTimePicker3.Value.ToString("yyyy-MM-dd") + "' ), '+1 days')  Order By editDate DESC";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			l_lastDeliveryDate.Text = dataTable.Rows[0]["editDate"].ToString();
			int num = 0;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string sql2 = "SELECT sumDiscount FROM hypos_DeliveryGoods_Master_log where DeliveryNo = '" + dataTable.Rows[i]["DeliveryNo"].ToString() + "' order by DeliveryLogId";
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteReaderReturnDataTable);
				string s = "0";
				if (dataTable2.Rows.Count > 0)
				{
					s = dataTable2.Rows[0]["sumDiscount"].ToString();
				}
				string text2 = (int.Parse(dataTable.Rows[i]["OriSum"].ToString()) - int.Parse(s)).ToString();
				int num2 = int.Parse(dataTable.Rows[i]["CurSum"].ToString()) - int.Parse(dataTable.Rows[i]["sumDiscount"].ToString());
				num += num2;
				string text3 = dataTable.Rows[i]["status"].ToString();
				switch (text3)
				{
				case "0":
					text3 = "正常";
					break;
				case "2":
					text3 = "正常(變更)";
					break;
				case "1":
					text3 = "取消";
					break;
				}
				dataGridView2.Rows.Insert(i, dataTable.Rows[i]["editDate"].ToString(), dataTable.Rows[i]["DeliveryNo"].ToString(), text2 + "(" + num2 + ")", text3, dataTable.Rows[i]["status"].ToString());
			}
			l_DeliveryTot.Text = num.ToString();
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
			string text = "";
			if (string.IsNullOrEmpty(tb_SupplierName.Text))
			{
				text += "請輸入廠商名稱\n";
			}
			if (Program.IsDeployClickOnce && !string.IsNullOrEmpty(text))
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			if (Program.GetDBVersion() >= 5)
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
						"vendorId",
						tb_vendorId.Text
					},
					{
						"vendorName",
						tb_vendorName.Text
					},
					{
						"vendorType",
						text2
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
						tb_Address.Text
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
						"Email",
						tb_Email.Text
					},
					{
						"EditDate",
						DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_Supplier", "SupplierNo ={0}", "", strFieldArray, new string[1]
				{
					tb_supplierNo.Text
				}, CommandOperationType.ExecuteNonQuery);
			}
			else
			{
				string[,] strFieldArray2 = new string[16, 2]
				{
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
						tb_Address.Text
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
						"Email",
						tb_Email.Text
					},
					{
						"EditDate",
						DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_Supplier", "SupplierNo ={0}", "", strFieldArray2, new string[1]
				{
					tb_supplierNo.Text
				}, CommandOperationType.ExecuteNonQuery);
			}
			AutoClosingMessageBox.Show("廠商編修完成");
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

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1 && e.RowIndex >= 0)
			{
				string value = dataGridView1["hidden_status", e.RowIndex].Value.ToString();
				if ("2".Equals(value))
				{
					MessageBox.Show("進貨單已取消，無法編修");
				}
				else
				{
					switchForm(new frmEditInventory(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()), this);
				}
			}
		}

		private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1 && e.RowIndex >= 0)
			{
				string value = dataGridView2["hidden_status_delivery", e.RowIndex].Value.ToString();
				if ("1".Equals(value))
				{
					MessageBox.Show("進貨單已取消，無法編修");
				}
				else
				{
					switchForm(new frmEditDeliveryOrder(dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString()), this);
				}
			}
		}

		private void btn_checkVendorID_Click(object sender, EventArgs e)
		{
			string text = tb_vendorId.Text.Trim();
			if (text.Equals("請輸入執照號碼後點選檢查"))
			{
				text = "";
			}
			if (text.Equals(""))
			{
				tb_vendorName.Text = "";
				AutoClosingMessageBox.Show("「販賣執照號碼」必填，請檢查");
				return;
			}
			if (!checkVendorID(text))
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
				string text2 = "";
				if (vendorResultObject.message.Equals("廠商販賣執照號碼不存在"))
				{
					text2 = "廠商營業執照號碼不存在，請檢查輸入值";
				}
				else if (vendorResultObject.message.Equals("廠商已歇業或停業"))
				{
					text2 = "此廠商目前已停用，請先確認廠商狀態";
				}
				tb_vendorId.Text = "";
				tb_vendorName.Text = "";
				MessageBox.Show(text2);
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

		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
		{
			showPurchaseLog();
		}

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			showPurchaseLog();
		}

		private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
		{
			showPurchaseLog();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			showDeliveryLog();
		}

		private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
		{
			showDeliveryLog();
		}

		private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
		{
			showDeliveryLog();
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
			tabControl1 = new System.Windows.Forms.TabControl();
			tabPage1 = new System.Windows.Forms.TabPage();
			btn_cancel = new System.Windows.Forms.Button();
			btn_save = new System.Windows.Forms.Button();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			panel11 = new System.Windows.Forms.Panel();
			label15 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			panel13 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			panel10 = new System.Windows.Forms.Panel();
			label13 = new System.Windows.Forms.Label();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			tb_supplierNo = new System.Windows.Forms.TextBox();
			flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			tb_TelNo = new System.Windows.Forms.TextBox();
			tb_TelExt = new System.Windows.Forms.TextBox();
			tb_DutyName = new System.Windows.Forms.TextBox();
			panel12 = new System.Windows.Forms.Panel();
			label17 = new System.Windows.Forms.Label();
			flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			cb_city = new System.Windows.Forms.ComboBox();
			cb_area = new System.Windows.Forms.ComboBox();
			tb_zipcode = new System.Windows.Forms.TextBox();
			tb_Address = new System.Windows.Forms.TextBox();
			cb_status = new System.Windows.Forms.ComboBox();
			panel8 = new System.Windows.Forms.Panel();
			label16 = new System.Windows.Forms.Label();
			panel14 = new System.Windows.Forms.Panel();
			tb_addr = new System.Windows.Forms.TextBox();
			tb_Email = new System.Windows.Forms.TextBox();
			panel7 = new System.Windows.Forms.Panel();
			label14 = new System.Windows.Forms.Label();
			panel20 = new System.Windows.Forms.Panel();
			cb_DeliveryType = new System.Windows.Forms.CheckBox();
			cb_PurchaseType = new System.Windows.Forms.CheckBox();
			cb_type = new System.Windows.Forms.ComboBox();
			panel4 = new System.Windows.Forms.Panel();
			label18 = new System.Windows.Forms.Label();
			tb_Fax = new System.Windows.Forms.TextBox();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			tb_Mobile = new System.Windows.Forms.TextBox();
			panel21 = new System.Windows.Forms.Panel();
			tb_ContactName = new System.Windows.Forms.TextBox();
			tb_ContactJob = new System.Windows.Forms.TextBox();
			label20 = new System.Windows.Forms.Label();
			panel9 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			tb_SupplierName = new System.Windows.Forms.TextBox();
			panel15 = new System.Windows.Forms.Panel();
			label23 = new System.Windows.Forms.Label();
			tb_SupplierIdNo = new System.Windows.Forms.TextBox();
			panel23 = new System.Windows.Forms.Panel();
			label21 = new System.Windows.Forms.Label();
			btn_checkVendorID = new System.Windows.Forms.Button();
			tb_vendorId = new System.Windows.Forms.TextBox();
			tb_vendorName = new System.Windows.Forms.TextBox();
			label22 = new System.Windows.Forms.Label();
			label24 = new System.Windows.Forms.Label();
			tabPage2 = new System.Windows.Forms.TabPage();
			label46 = new System.Windows.Forms.Label();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewLinkColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label19 = new System.Windows.Forms.Label();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			panel17 = new System.Windows.Forms.Panel();
			label27 = new System.Windows.Forms.Label();
			panel22 = new System.Windows.Forms.Panel();
			label33 = new System.Windows.Forms.Label();
			l_Total = new System.Windows.Forms.Label();
			l_lastPurchaseDate = new System.Windows.Forms.Label();
			dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			label49 = new System.Windows.Forms.Label();
			dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
			label50 = new System.Windows.Forms.Label();
			comboBox4 = new System.Windows.Forms.ComboBox();
			label42 = new System.Windows.Forms.Label();
			tabPage3 = new System.Windows.Forms.TabPage();
			dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			label26 = new System.Windows.Forms.Label();
			dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
			label28 = new System.Windows.Forms.Label();
			comboBox1 = new System.Windows.Forms.ComboBox();
			label29 = new System.Windows.Forms.Label();
			label25 = new System.Windows.Forms.Label();
			dataGridView2 = new System.Windows.Forms.DataGridView();
			hidden_status_delivery = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewLinkColumn1 = new System.Windows.Forms.DataGridViewLinkColumn();
			dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label11 = new System.Windows.Forms.Label();
			tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			panel18 = new System.Windows.Forms.Panel();
			label10 = new System.Windows.Forms.Label();
			l_DeliveryTot = new System.Windows.Forms.Label();
			panel16 = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			panel19 = new System.Windows.Forms.Panel();
			l_lastDeliveryDate = new System.Windows.Forms.Label();
			panel2 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			btn_back = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			tabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			panel11.SuspendLayout();
			panel5.SuspendLayout();
			panel3.SuspendLayout();
			panel13.SuspendLayout();
			panel1.SuspendLayout();
			panel10.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			flowLayoutPanel3.SuspendLayout();
			panel12.SuspendLayout();
			flowLayoutPanel2.SuspendLayout();
			panel8.SuspendLayout();
			panel14.SuspendLayout();
			panel7.SuspendLayout();
			panel20.SuspendLayout();
			panel4.SuspendLayout();
			panel6.SuspendLayout();
			panel21.SuspendLayout();
			panel9.SuspendLayout();
			panel15.SuspendLayout();
			panel23.SuspendLayout();
			tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			tableLayoutPanel2.SuspendLayout();
			panel17.SuspendLayout();
			panel22.SuspendLayout();
			tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
			tableLayoutPanel3.SuspendLayout();
			panel18.SuspendLayout();
			panel16.SuspendLayout();
			panel19.SuspendLayout();
			panel2.SuspendLayout();
			SuspendLayout();
			tabControl1.Controls.Add(tabPage1);
			tabControl1.Controls.Add(tabPage2);
			tabControl1.Controls.Add(tabPage3);
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
			tableLayoutPanel1.Controls.Add(panel11, 2, 2);
			tableLayoutPanel1.Controls.Add(panel5, 0, 4);
			tableLayoutPanel1.Controls.Add(panel3, 0, 2);
			tableLayoutPanel1.Controls.Add(panel13, 2, 8);
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(panel10, 2, 1);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 1, 2);
			tableLayoutPanel1.Controls.Add(tb_DutyName, 3, 1);
			tableLayoutPanel1.Controls.Add(panel12, 0, 5);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 1, 5);
			tableLayoutPanel1.Controls.Add(cb_status, 3, 8);
			tableLayoutPanel1.Controls.Add(panel8, 2, 7);
			tableLayoutPanel1.Controls.Add(panel14, 3, 7);
			tableLayoutPanel1.Controls.Add(panel7, 0, 8);
			tableLayoutPanel1.Controls.Add(panel20, 1, 8);
			tableLayoutPanel1.Controls.Add(panel4, 0, 7);
			tableLayoutPanel1.Controls.Add(tb_Fax, 1, 7);
			tableLayoutPanel1.Controls.Add(panel6, 0, 3);
			tableLayoutPanel1.Controls.Add(tb_Mobile, 1, 4);
			tableLayoutPanel1.Controls.Add(panel21, 1, 3);
			tableLayoutPanel1.Controls.Add(panel9, 0, 1);
			tableLayoutPanel1.Controls.Add(tb_SupplierName, 1, 1);
			tableLayoutPanel1.Controls.Add(panel15, 2, 0);
			tableLayoutPanel1.Controls.Add(tb_SupplierIdNo, 3, 0);
			tableLayoutPanel1.Controls.Add(panel23, 3, 2);
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
			panel11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel11.Controls.Add(label15);
			panel11.Location = new System.Drawing.Point(484, 111);
			panel11.Margin = new System.Windows.Forms.Padding(0);
			panel11.Name = "panel11";
			tableLayoutPanel1.SetRowSpan(panel11, 3);
			panel11.Size = new System.Drawing.Size(162, 164);
			panel11.TabIndex = 43;
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label15.ForeColor = System.Drawing.Color.White;
			label15.Location = new System.Drawing.Point(82, 73);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(74, 21);
			label15.TabIndex = 90;
			label15.Text = "營業資訊";
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label2);
			panel5.Location = new System.Drawing.Point(1, 221);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(162, 54);
			panel5.TabIndex = 23;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(79, 22);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(74, 21);
			label2.TabIndex = 10;
			label2.Text = "行動電話";
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
			label8.Location = new System.Drawing.Point(51, 18);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(106, 21);
			label8.TabIndex = 100;
			label8.Text = "公司聯絡電話";
			panel13.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel13.Controls.Add(label6);
			panel13.Dock = System.Windows.Forms.DockStyle.Fill;
			panel13.Location = new System.Drawing.Point(484, 441);
			panel13.Margin = new System.Windows.Forms.Padding(0);
			panel13.Name = "panel13";
			panel13.Size = new System.Drawing.Size(162, 61);
			panel13.TabIndex = 24;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(111, 20);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(42, 21);
			label6.TabIndex = 10;
			label6.Text = "狀態";
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
			label1.Location = new System.Drawing.Point(64, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(90, 21);
			label1.TabIndex = 90;
			label1.Text = "供應商編號";
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
			label13.Location = new System.Drawing.Point(98, 16);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(58, 21);
			label13.TabIndex = 90;
			label13.Text = "負責人";
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
			tb_TelNo.Size = new System.Drawing.Size(188, 33);
			tb_TelNo.TabIndex = 5;
			tb_TelExt.Location = new System.Drawing.Point(218, 10);
			tb_TelExt.Margin = new System.Windows.Forms.Padding(10);
			tb_TelExt.Name = "tb_TelExt";
			tb_TelExt.Size = new System.Drawing.Size(89, 33);
			tb_TelExt.TabIndex = 5;
			tb_DutyName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_DutyName.Location = new System.Drawing.Point(657, 66);
			tb_DutyName.Margin = new System.Windows.Forms.Padding(10);
			tb_DutyName.Name = "tb_DutyName";
			tb_DutyName.Size = new System.Drawing.Size(299, 33);
			tb_DutyName.TabIndex = 2;
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
			cb_city.Location = new System.Drawing.Point(10, 11);
			cb_city.Margin = new System.Windows.Forms.Padding(10, 0, 3, 3);
			cb_city.Name = "cb_city";
			cb_city.Size = new System.Drawing.Size(155, 32);
			cb_city.TabIndex = 11;
			cb_city.SelectedIndexChanged += new System.EventHandler(cb_city_SelectedIndexChanged);
			cb_area.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_area.FormattingEnabled = true;
			cb_area.Location = new System.Drawing.Point(178, 11);
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
			cb_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_status.FormattingEnabled = true;
			cb_status.Location = new System.Drawing.Point(657, 457);
			cb_status.Margin = new System.Windows.Forms.Padding(10, 0, 3, 8);
			cb_status.Name = "cb_status";
			cb_status.Size = new System.Drawing.Size(169, 32);
			cb_status.TabIndex = 11;
			panel8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel8.Controls.Add(label16);
			panel8.Location = new System.Drawing.Point(484, 386);
			panel8.Margin = new System.Windows.Forms.Padding(0);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(162, 54);
			panel8.TabIndex = 20;
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label16.ForeColor = System.Drawing.Color.White;
			label16.Location = new System.Drawing.Point(83, 23);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(74, 21);
			label16.TabIndex = 10;
			label16.Text = "電子信箱";
			panel14.Controls.Add(tb_addr);
			panel14.Controls.Add(tb_Email);
			panel14.Location = new System.Drawing.Point(647, 386);
			panel14.Margin = new System.Windows.Forms.Padding(0);
			panel14.Name = "panel14";
			panel14.Size = new System.Drawing.Size(319, 54);
			panel14.TabIndex = 40;
			tb_addr.Location = new System.Drawing.Point(14, 64);
			tb_addr.Margin = new System.Windows.Forms.Padding(0);
			tb_addr.Name = "tb_addr";
			tb_addr.Size = new System.Drawing.Size(603, 33);
			tb_addr.TabIndex = 9;
			tb_Email.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_Email.Location = new System.Drawing.Point(10, 14);
			tb_Email.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_Email.Name = "tb_Email";
			tb_Email.Size = new System.Drawing.Size(297, 33);
			tb_Email.TabIndex = 14;
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(label14);
			panel7.Location = new System.Drawing.Point(1, 441);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(162, 61);
			panel7.TabIndex = 20;
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label14.ForeColor = System.Drawing.Color.White;
			label14.Location = new System.Drawing.Point(80, 21);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(74, 21);
			label14.TabIndex = 10;
			label14.Text = "廠商類型";
			panel20.Controls.Add(cb_DeliveryType);
			panel20.Controls.Add(cb_PurchaseType);
			panel20.Controls.Add(cb_type);
			panel20.Location = new System.Drawing.Point(167, 444);
			panel20.Name = "panel20";
			panel20.Size = new System.Drawing.Size(313, 55);
			panel20.TabIndex = 41;
			cb_DeliveryType.AutoSize = true;
			cb_DeliveryType.BackColor = System.Drawing.Color.White;
			cb_DeliveryType.Checked = true;
			cb_DeliveryType.CheckState = System.Windows.Forms.CheckState.Checked;
			cb_DeliveryType.Location = new System.Drawing.Point(244, 13);
			cb_DeliveryType.Name = "cb_DeliveryType";
			cb_DeliveryType.Size = new System.Drawing.Size(67, 28);
			cb_DeliveryType.TabIndex = 15;
			cb_DeliveryType.Text = "出貨";
			cb_DeliveryType.UseVisualStyleBackColor = false;
			cb_PurchaseType.AutoSize = true;
			cb_PurchaseType.Checked = true;
			cb_PurchaseType.CheckState = System.Windows.Forms.CheckState.Checked;
			cb_PurchaseType.Location = new System.Drawing.Point(176, 13);
			cb_PurchaseType.Name = "cb_PurchaseType";
			cb_PurchaseType.Size = new System.Drawing.Size(67, 28);
			cb_PurchaseType.TabIndex = 14;
			cb_PurchaseType.Text = "進貨";
			cb_PurchaseType.UseVisualStyleBackColor = true;
			cb_type.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_type.FormattingEnabled = true;
			cb_type.Location = new System.Drawing.Point(5, 13);
			cb_type.Margin = new System.Windows.Forms.Padding(10, 0, 3, 8);
			cb_type.Name = "cb_type";
			cb_type.Size = new System.Drawing.Size(157, 32);
			cb_type.TabIndex = 11;
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label18);
			panel4.Location = new System.Drawing.Point(1, 386);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(162, 54);
			panel4.TabIndex = 22;
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label18.ForeColor = System.Drawing.Color.White;
			label18.Location = new System.Drawing.Point(80, 20);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(74, 21);
			label18.TabIndex = 90;
			label18.Text = "傳真機號";
			tb_Fax.Location = new System.Drawing.Point(174, 396);
			tb_Fax.Margin = new System.Windows.Forms.Padding(10);
			tb_Fax.Name = "tb_Fax";
			tb_Fax.Size = new System.Drawing.Size(299, 33);
			tb_Fax.TabIndex = 5;
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Location = new System.Drawing.Point(1, 166);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(162, 54);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(96, 18);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(58, 21);
			label12.TabIndex = 10;
			label12.Text = "聯絡人";
			tb_Mobile.Location = new System.Drawing.Point(174, 231);
			tb_Mobile.Margin = new System.Windows.Forms.Padding(10);
			tb_Mobile.Name = "tb_Mobile";
			tb_Mobile.Size = new System.Drawing.Size(299, 33);
			tb_Mobile.TabIndex = 5;
			panel21.Controls.Add(tb_ContactName);
			panel21.Controls.Add(tb_ContactJob);
			panel21.Controls.Add(label20);
			panel21.Location = new System.Drawing.Point(167, 169);
			panel21.Name = "panel21";
			panel21.Size = new System.Drawing.Size(313, 48);
			panel21.TabIndex = 42;
			tb_ContactName.Location = new System.Drawing.Point(7, 9);
			tb_ContactName.Margin = new System.Windows.Forms.Padding(10);
			tb_ContactName.Name = "tb_ContactName";
			tb_ContactName.Size = new System.Drawing.Size(128, 33);
			tb_ContactName.TabIndex = 5;
			tb_ContactJob.Location = new System.Drawing.Point(199, 9);
			tb_ContactJob.Margin = new System.Windows.Forms.Padding(10);
			tb_ContactJob.Name = "tb_ContactJob";
			tb_ContactJob.Size = new System.Drawing.Size(109, 33);
			tb_ContactJob.TabIndex = 5;
			label20.AutoSize = true;
			label20.Location = new System.Drawing.Point(140, 12);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(67, 24);
			label20.TabIndex = 43;
			label20.Text = "職稱：";
			panel9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel9.Controls.Add(label5);
			panel9.Controls.Add(label7);
			panel9.Location = new System.Drawing.Point(1, 56);
			panel9.Margin = new System.Windows.Forms.Padding(0);
			panel9.Name = "panel9";
			panel9.Size = new System.Drawing.Size(162, 54);
			panel9.TabIndex = 20;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Red;
			label5.Location = new System.Drawing.Point(48, 17);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(17, 21);
			label5.TabIndex = 1;
			label5.Text = "*";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(63, 17);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(90, 21);
			label7.TabIndex = 10;
			label7.Text = "供應商名稱";
			tb_SupplierName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SupplierName.Location = new System.Drawing.Point(174, 66);
			tb_SupplierName.Margin = new System.Windows.Forms.Padding(10);
			tb_SupplierName.Name = "tb_SupplierName";
			tb_SupplierName.Size = new System.Drawing.Size(299, 33);
			tb_SupplierName.TabIndex = 2;
			panel15.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel15.Controls.Add(label23);
			panel15.Location = new System.Drawing.Point(484, 1);
			panel15.Margin = new System.Windows.Forms.Padding(0);
			panel15.Name = "panel15";
			panel15.Size = new System.Drawing.Size(162, 54);
			panel15.TabIndex = 24;
			label23.AutoSize = true;
			label23.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label23.ForeColor = System.Drawing.Color.White;
			label23.Location = new System.Drawing.Point(83, 17);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(74, 21);
			label23.TabIndex = 90;
			label23.Text = "統一編號";
			tb_SupplierIdNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SupplierIdNo.Location = new System.Drawing.Point(657, 11);
			tb_SupplierIdNo.Margin = new System.Windows.Forms.Padding(10);
			tb_SupplierIdNo.Name = "tb_SupplierIdNo";
			tb_SupplierIdNo.Size = new System.Drawing.Size(299, 33);
			tb_SupplierIdNo.TabIndex = 2;
			panel23.Controls.Add(label21);
			panel23.Controls.Add(btn_checkVendorID);
			panel23.Controls.Add(tb_vendorId);
			panel23.Controls.Add(tb_vendorName);
			panel23.Controls.Add(label22);
			panel23.Controls.Add(label24);
			panel23.Location = new System.Drawing.Point(650, 114);
			panel23.Name = "panel23";
			tableLayoutPanel1.SetRowSpan(panel23, 3);
			panel23.Size = new System.Drawing.Size(313, 158);
			panel23.TabIndex = 44;
			label21.AutoSize = true;
			label21.Font = new System.Drawing.Font("微軟正黑體", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label21.Location = new System.Drawing.Point(15, 91);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(287, 57);
			label21.TabIndex = 11;
			label21.Text = "請使用 販賣執照號碼線上查詢 確認廠商執\r\n照號碼請注意，農藥商品的出貨廠商必需\r\n驗證其商業執照號碼與名稱。";
			btn_checkVendorID.Location = new System.Drawing.Point(229, 11);
			btn_checkVendorID.Name = "btn_checkVendorID";
			btn_checkVendorID.Size = new System.Drawing.Size(75, 72);
			btn_checkVendorID.TabIndex = 10;
			btn_checkVendorID.Text = "檢查";
			btn_checkVendorID.UseVisualStyleBackColor = true;
			btn_checkVendorID.Click += new System.EventHandler(btn_checkVendorID_Click);
			tb_vendorId.Location = new System.Drawing.Point(106, 11);
			tb_vendorId.MaxLength = 6;
			tb_vendorId.Name = "tb_vendorId";
			tb_vendorId.Size = new System.Drawing.Size(113, 33);
			tb_vendorId.TabIndex = 9;
			tb_vendorName.Enabled = false;
			tb_vendorName.Location = new System.Drawing.Point(106, 50);
			tb_vendorName.Name = "tb_vendorName";
			tb_vendorName.Size = new System.Drawing.Size(113, 33);
			tb_vendorName.TabIndex = 8;
			label22.AutoSize = true;
			label22.Location = new System.Drawing.Point(9, 53);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(105, 24);
			label22.TabIndex = 7;
			label22.Text = "商業名稱：";
			label24.AutoSize = true;
			label24.Location = new System.Drawing.Point(9, 17);
			label24.Name = "label24";
			label24.Size = new System.Drawing.Size(105, 24);
			label24.TabIndex = 6;
			label24.Text = "執照號碼：";
			tabPage2.Controls.Add(label46);
			tabPage2.Controls.Add(dataGridView1);
			tabPage2.Controls.Add(label19);
			tabPage2.Controls.Add(tableLayoutPanel2);
			tabPage2.Controls.Add(dateTimePicker1);
			tabPage2.Controls.Add(label49);
			tabPage2.Controls.Add(dateTimePicker4);
			tabPage2.Controls.Add(label50);
			tabPage2.Controls.Add(comboBox4);
			tabPage2.Controls.Add(label42);
			tabPage2.Location = new System.Drawing.Point(4, 47);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new System.Windows.Forms.Padding(3);
			tabPage2.Size = new System.Drawing.Size(973, 572);
			tabPage2.TabIndex = 11;
			tabPage2.Text = "進貨紀錄";
			tabPage2.UseVisualStyleBackColor = true;
			label46.AutoSize = true;
			label46.Font = new System.Drawing.Font("微軟正黑體", 10f, System.Drawing.FontStyle.Bold);
			label46.ForeColor = System.Drawing.Color.Black;
			label46.Location = new System.Drawing.Point(558, 97);
			label46.Name = "label46";
			label46.Size = new System.Drawing.Size(88, 18);
			label46.TabIndex = 79;
			label46.Text = "(預設一週內)";
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.BackgroundColor = System.Drawing.Color.White;
			dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(Column1, Column2, Column4, Column5, hidden_status);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.Location = new System.Drawing.Point(5, 120);
			dataGridView1.Margin = new System.Windows.Forms.Padding(0);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 24;
			dataGridView1.Size = new System.Drawing.Size(965, 447);
			dataGridView1.TabIndex = 72;
			dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
			Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			Column1.DefaultCellStyle = dataGridViewCellStyle4;
			Column1.HeaderText = "編修日期時間";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Blue;
			Column2.DefaultCellStyle = dataGridViewCellStyle5;
			Column2.HeaderText = "進貨系統單號";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
			Column4.DefaultCellStyle = dataGridViewCellStyle6;
			Column4.HeaderText = "進貨單總額";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
			Column5.DefaultCellStyle = dataGridViewCellStyle7;
			Column5.FillWeight = 60f;
			Column5.HeaderText = "進貨單狀態";
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			hidden_status.HeaderText = "(隱藏_訂單狀態)";
			hidden_status.Name = "hidden_status";
			hidden_status.ReadOnly = true;
			hidden_status.Visible = false;
			label19.AutoSize = true;
			label19.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label19.ForeColor = System.Drawing.Color.Black;
			label19.Location = new System.Drawing.Point(417, 94);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(138, 21);
			label19.TabIndex = 3;
			label19.Text = "近期進貨記錄一覽";
			tableLayoutPanel2.BackColor = System.Drawing.Color.White;
			tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel2.ColumnCount = 4;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel2.Controls.Add(panel17, 2, 0);
			tableLayoutPanel2.Controls.Add(panel22, 0, 0);
			tableLayoutPanel2.Controls.Add(l_Total, 1, 0);
			tableLayoutPanel2.Controls.Add(l_lastPurchaseDate, 3, 0);
			tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 1;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23292f));
			tableLayoutPanel2.Size = new System.Drawing.Size(967, 47);
			tableLayoutPanel2.TabIndex = 4;
			panel17.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel17.Controls.Add(label27);
			panel17.Dock = System.Windows.Forms.DockStyle.Fill;
			panel17.Location = new System.Drawing.Point(484, 1);
			panel17.Margin = new System.Windows.Forms.Padding(0);
			panel17.Name = "panel17";
			panel17.Size = new System.Drawing.Size(162, 45);
			panel17.TabIndex = 20;
			label27.AutoSize = true;
			label27.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label27.ForeColor = System.Drawing.Color.White;
			label27.Location = new System.Drawing.Point(53, 14);
			label27.Name = "label27";
			label27.Size = new System.Drawing.Size(106, 21);
			label27.TabIndex = 0;
			label27.Text = "最近進貨日期";
			panel22.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel22.Controls.Add(label33);
			panel22.Dock = System.Windows.Forms.DockStyle.Fill;
			panel22.Location = new System.Drawing.Point(1, 1);
			panel22.Margin = new System.Windows.Forms.Padding(0);
			panel22.Name = "panel22";
			panel22.Size = new System.Drawing.Size(162, 45);
			panel22.TabIndex = 19;
			label33.AutoSize = true;
			label33.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label33.ForeColor = System.Drawing.Color.White;
			label33.Location = new System.Drawing.Point(67, 14);
			label33.Name = "label33";
			label33.Size = new System.Drawing.Size(90, 21);
			label33.TabIndex = 0;
			label33.Text = "總進貨金額";
			l_Total.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_Total.AutoSize = true;
			l_Total.Location = new System.Drawing.Point(167, 11);
			l_Total.Name = "l_Total";
			l_Total.Size = new System.Drawing.Size(33, 24);
			l_Total.TabIndex = 25;
			l_Total.Text = "{0}";
			l_lastPurchaseDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_lastPurchaseDate.AutoSize = true;
			l_lastPurchaseDate.Location = new System.Drawing.Point(650, 11);
			l_lastPurchaseDate.Name = "l_lastPurchaseDate";
			l_lastPurchaseDate.Size = new System.Drawing.Size(33, 24);
			l_lastPurchaseDate.TabIndex = 25;
			l_lastPurchaseDate.Text = "{0}";
			dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker1.Location = new System.Drawing.Point(408, 54);
			dateTimePicker1.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker1.Name = "dateTimePicker1";
			dateTimePicker1.ShowCheckBox = true;
			dateTimePicker1.Size = new System.Drawing.Size(181, 33);
			dateTimePicker1.TabIndex = 76;
			dateTimePicker1.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateTimePicker1.ValueChanged += new System.EventHandler(dateTimePicker1_ValueChanged);
			label49.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label49.AutoSize = true;
			label49.Location = new System.Drawing.Point(595, 58);
			label49.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label49.Name = "label49";
			label49.Size = new System.Drawing.Size(24, 24);
			label49.TabIndex = 78;
			label49.Text = "~";
			dateTimePicker4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker4.CustomFormat = "yyyy-MM-dd";
			dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker4.Location = new System.Drawing.Point(625, 54);
			dateTimePicker4.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker4.Name = "dateTimePicker4";
			dateTimePicker4.ShowCheckBox = true;
			dateTimePicker4.Size = new System.Drawing.Size(181, 33);
			dateTimePicker4.TabIndex = 77;
			dateTimePicker4.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateTimePicker4.ValueChanged += new System.EventHandler(dateTimePicker4_ValueChanged);
			label50.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label50.AutoSize = true;
			label50.Location = new System.Drawing.Point(285, 60);
			label50.Name = "label50";
			label50.Size = new System.Drawing.Size(124, 24);
			label50.TabIndex = 75;
			label50.Text = "日期區間查詢";
			comboBox4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox4.FormattingEnabled = true;
			comboBox4.Location = new System.Drawing.Point(128, 56);
			comboBox4.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			comboBox4.Name = "comboBox4";
			comboBox4.Size = new System.Drawing.Size(128, 32);
			comboBox4.TabIndex = 74;
			comboBox4.SelectedIndexChanged += new System.EventHandler(comboBox4_SelectedIndexChanged);
			label42.Anchor = System.Windows.Forms.AnchorStyles.None;
			label42.AutoSize = true;
			label42.ForeColor = System.Drawing.Color.Black;
			label42.Location = new System.Drawing.Point(13, 60);
			label42.Margin = new System.Windows.Forms.Padding(0);
			label42.Name = "label42";
			label42.Size = new System.Drawing.Size(105, 24);
			label42.TabIndex = 73;
			label42.Text = "條件篩選：";
			tabPage3.Controls.Add(dateTimePicker2);
			tabPage3.Controls.Add(label26);
			tabPage3.Controls.Add(dateTimePicker3);
			tabPage3.Controls.Add(label28);
			tabPage3.Controls.Add(comboBox1);
			tabPage3.Controls.Add(label29);
			tabPage3.Controls.Add(label25);
			tabPage3.Controls.Add(dataGridView2);
			tabPage3.Controls.Add(label11);
			tabPage3.Controls.Add(tableLayoutPanel3);
			tabPage3.Location = new System.Drawing.Point(4, 47);
			tabPage3.Name = "tabPage3";
			tabPage3.Padding = new System.Windows.Forms.Padding(3);
			tabPage3.Size = new System.Drawing.Size(973, 572);
			tabPage3.TabIndex = 12;
			tabPage3.Text = "出貨紀錄";
			tabPage3.UseVisualStyleBackColor = true;
			dateTimePicker2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker2.Location = new System.Drawing.Point(408, 54);
			dateTimePicker2.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker2.Name = "dateTimePicker2";
			dateTimePicker2.ShowCheckBox = true;
			dateTimePicker2.Size = new System.Drawing.Size(181, 33);
			dateTimePicker2.TabIndex = 84;
			dateTimePicker2.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateTimePicker2.ValueChanged += new System.EventHandler(dateTimePicker2_ValueChanged);
			label26.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label26.AutoSize = true;
			label26.Location = new System.Drawing.Point(595, 58);
			label26.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label26.Name = "label26";
			label26.Size = new System.Drawing.Size(24, 24);
			label26.TabIndex = 86;
			label26.Text = "~";
			dateTimePicker3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker3.CustomFormat = "yyyy-MM-dd";
			dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker3.Location = new System.Drawing.Point(625, 54);
			dateTimePicker3.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker3.Name = "dateTimePicker3";
			dateTimePicker3.ShowCheckBox = true;
			dateTimePicker3.Size = new System.Drawing.Size(181, 33);
			dateTimePicker3.TabIndex = 85;
			dateTimePicker3.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateTimePicker3.ValueChanged += new System.EventHandler(dateTimePicker3_ValueChanged);
			label28.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label28.AutoSize = true;
			label28.Location = new System.Drawing.Point(285, 60);
			label28.Name = "label28";
			label28.Size = new System.Drawing.Size(124, 24);
			label28.TabIndex = 83;
			label28.Text = "日期區間查詢";
			comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new System.Drawing.Point(128, 56);
			comboBox1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new System.Drawing.Size(128, 32);
			comboBox1.TabIndex = 82;
			comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
			label29.Anchor = System.Windows.Forms.AnchorStyles.None;
			label29.AutoSize = true;
			label29.ForeColor = System.Drawing.Color.Black;
			label29.Location = new System.Drawing.Point(13, 60);
			label29.Margin = new System.Windows.Forms.Padding(0);
			label29.Name = "label29";
			label29.Size = new System.Drawing.Size(105, 24);
			label29.TabIndex = 81;
			label29.Text = "條件篩選：";
			label25.AutoSize = true;
			label25.Font = new System.Drawing.Font("微軟正黑體", 10f, System.Drawing.FontStyle.Bold);
			label25.ForeColor = System.Drawing.Color.Black;
			label25.Location = new System.Drawing.Point(558, 97);
			label25.Name = "label25";
			label25.Size = new System.Drawing.Size(88, 18);
			label25.TabIndex = 80;
			label25.Text = "(預設一週內)";
			dataGridView2.AllowUserToAddRows = false;
			dataGridView2.AllowUserToDeleteRows = false;
			dataGridView2.AllowUserToResizeColumns = false;
			dataGridView2.AllowUserToResizeRows = false;
			dataGridView2.BackgroundColor = System.Drawing.Color.White;
			dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle8.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
			dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView2.Columns.AddRange(hidden_status_delivery, dataGridViewLinkColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4);
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView2.DefaultCellStyle = dataGridViewCellStyle9;
			dataGridView2.EnableHeadersVisualStyles = false;
			dataGridView2.Location = new System.Drawing.Point(5, 119);
			dataGridView2.Margin = new System.Windows.Forms.Padding(0);
			dataGridView2.Name = "dataGridView2";
			dataGridView2.ReadOnly = true;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
			dataGridView2.RowHeadersVisible = false;
			dataGridView2.RowTemplate.Height = 24;
			dataGridView2.Size = new System.Drawing.Size(965, 448);
			dataGridView2.TabIndex = 73;
			dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView2_CellContentClick);
			hidden_status_delivery.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
			hidden_status_delivery.DefaultCellStyle = dataGridViewCellStyle11;
			hidden_status_delivery.HeaderText = "出貨/編修日期時間";
			hidden_status_delivery.Name = "hidden_status_delivery";
			hidden_status_delivery.ReadOnly = true;
			hidden_status_delivery.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewLinkColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Blue;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Blue;
			dataGridViewLinkColumn1.DefaultCellStyle = dataGridViewCellStyle12;
			dataGridViewLinkColumn1.HeaderText = "出貨系統單號";
			dataGridViewLinkColumn1.Name = "dataGridViewLinkColumn1";
			dataGridViewLinkColumn1.ReadOnly = true;
			dataGridViewLinkColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle13.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle13;
			dataGridViewTextBoxColumn2.HeaderText = "出貨單總額(變更後總額)";
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.ReadOnly = true;
			dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle14.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle14;
			dataGridViewTextBoxColumn3.FillWeight = 60f;
			dataGridViewTextBoxColumn3.HeaderText = "出貨單狀態";
			dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			dataGridViewTextBoxColumn3.ReadOnly = true;
			dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn4.HeaderText = "(隱藏_出貨單狀態)";
			dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			dataGridViewTextBoxColumn4.ReadOnly = true;
			dataGridViewTextBoxColumn4.Visible = false;
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label11.ForeColor = System.Drawing.Color.Black;
			label11.Location = new System.Drawing.Point(417, 94);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(138, 21);
			label11.TabIndex = 4;
			label11.Text = "近期出貨記錄一覽";
			tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel3.ColumnCount = 4;
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel3.Controls.Add(panel18, 0, 0);
			tableLayoutPanel3.Controls.Add(l_DeliveryTot, 0, 0);
			tableLayoutPanel3.Controls.Add(panel16, 0, 0);
			tableLayoutPanel3.Controls.Add(panel19, 3, 0);
			tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 1;
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel3.Size = new System.Drawing.Size(967, 47);
			tableLayoutPanel3.TabIndex = 0;
			panel18.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel18.Controls.Add(label10);
			panel18.Dock = System.Windows.Forms.DockStyle.Fill;
			panel18.Location = new System.Drawing.Point(484, 1);
			panel18.Margin = new System.Windows.Forms.Padding(0);
			panel18.Name = "panel18";
			panel18.Size = new System.Drawing.Size(162, 45);
			panel18.TabIndex = 27;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(53, 14);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(106, 21);
			label10.TabIndex = 0;
			label10.Text = "最近出貨日期";
			l_DeliveryTot.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_DeliveryTot.AutoSize = true;
			l_DeliveryTot.Location = new System.Drawing.Point(167, 11);
			l_DeliveryTot.Name = "l_DeliveryTot";
			l_DeliveryTot.Size = new System.Drawing.Size(33, 24);
			l_DeliveryTot.TabIndex = 26;
			l_DeliveryTot.Text = "{0}";
			panel16.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel16.Controls.Add(label9);
			panel16.Dock = System.Windows.Forms.DockStyle.Fill;
			panel16.Location = new System.Drawing.Point(1, 1);
			panel16.Margin = new System.Windows.Forms.Padding(0);
			panel16.Name = "panel16";
			panel16.Size = new System.Drawing.Size(162, 45);
			panel16.TabIndex = 20;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label9.ForeColor = System.Drawing.Color.White;
			label9.Location = new System.Drawing.Point(67, 14);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(90, 21);
			label9.TabIndex = 0;
			label9.Text = "總出貨金額";
			panel19.Controls.Add(l_lastDeliveryDate);
			panel19.Location = new System.Drawing.Point(650, 4);
			panel19.Name = "panel19";
			panel19.Size = new System.Drawing.Size(313, 39);
			panel19.TabIndex = 28;
			l_lastDeliveryDate.AutoSize = true;
			l_lastDeliveryDate.Location = new System.Drawing.Point(3, 8);
			l_lastDeliveryDate.Name = "l_lastDeliveryDate";
			l_lastDeliveryDate.Size = new System.Drawing.Size(33, 24);
			l_lastDeliveryDate.TabIndex = 28;
			l_lastDeliveryDate.Text = "{0}";
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
			base.Name = "frmEditSupplier";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmEditSupplier_Load);
			base.Controls.SetChildIndex(tabControl1, 0);
			base.Controls.SetChildIndex(btn_back, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			tabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel11.ResumeLayout(false);
			panel11.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel13.ResumeLayout(false);
			panel13.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel10.ResumeLayout(false);
			panel10.PerformLayout();
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			flowLayoutPanel3.ResumeLayout(false);
			flowLayoutPanel3.PerformLayout();
			panel12.ResumeLayout(false);
			panel12.PerformLayout();
			flowLayoutPanel2.ResumeLayout(false);
			flowLayoutPanel2.PerformLayout();
			panel8.ResumeLayout(false);
			panel8.PerformLayout();
			panel14.ResumeLayout(false);
			panel14.PerformLayout();
			panel7.ResumeLayout(false);
			panel7.PerformLayout();
			panel20.ResumeLayout(false);
			panel20.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			panel21.ResumeLayout(false);
			panel21.PerformLayout();
			panel9.ResumeLayout(false);
			panel9.PerformLayout();
			panel15.ResumeLayout(false);
			panel15.PerformLayout();
			panel23.ResumeLayout(false);
			panel23.PerformLayout();
			tabPage2.ResumeLayout(false);
			tabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			panel17.ResumeLayout(false);
			panel17.PerformLayout();
			panel22.ResumeLayout(false);
			panel22.PerformLayout();
			tabPage3.ResumeLayout(false);
			tabPage3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			panel18.ResumeLayout(false);
			panel18.PerformLayout();
			panel16.ResumeLayout(false);
			panel16.PerformLayout();
			panel19.ResumeLayout(false);
			panel19.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			ResumeLayout(false);
		}
	}
}
