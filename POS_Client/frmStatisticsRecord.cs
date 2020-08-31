using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using POS_Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmStatisticsRecord : MasterThinForm
	{
		private HSSFWorkbook wb;

		private HSSFSheet sh;

		private HSSFWorkbook wb1;

		private HSSFSheet sh1;

		private string str_file_location = "\\";

		private string str_file_name = "report_daily_";

		private string str_file_type = ".xls";

		private string str_file = "";

		private int report_daily_rowcount;

		private int report_DeliveryDaily_rowcount;

		private List<string> lst_Sheet = new List<string>();

		private DataTable dt_details1;

		private DataTable dt_details2;

		private List<string> lst_selmember = new List<string>();

		private List<string> lst_Salesselmember = new List<string>();

		private List<string> lst_commodity = new List<string>();

		private List<string> lst_commodity2 = new List<string>();

		private List<string> lst_selvendor = new List<string>();

		private List<string> lst_selTEMP = new List<string>();

		private IContainer components;

		private Label label3;

		private Label label4;

		private Button btn_view_store_upload_history;

		private Panel panel2;

		private TabControl tabControl;

		private TabPage BasicData;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private TabPage tabPage4;

		private TableLayoutPanel tableLayoutPanel1;

		private FlowLayoutPanel flowLayoutPanel1;

		private FlowLayoutPanel flowLayoutPanel3;

		private Panel panel6;

		private Label label12;

		private Panel panel3;

		private Label label6;

		private Panel panel1;

		private Label label1;

		private Panel panel4;

		private Label label2;

		private Panel panel5;

		private Label label8;

		private Panel panel9;

		private Label label10;

		private FlowLayoutPanel flowLayoutPanel2;

		private FlowLayoutPanel flowLayoutPanel4;

		private FlowLayoutPanel flowLayoutPanel5;

		private FlowLayoutPanel flowLayoutPanel6;

		private TableLayoutPanel tableLayoutPanel2;

		private FlowLayoutPanel flowLayoutPanel7;

		private FlowLayoutPanel flowLayoutPanel8;

		private Panel panel8;

		private Label label7;

		private Panel panel10;

		private Label label9;

		private Panel panel11;

		private Label label11;

		private Panel panel12;

		private Label label13;

		private Panel panel13;

		private Label label14;

		private FlowLayoutPanel flowLayoutPanel9;

		private FlowLayoutPanel flowLayoutPanel10;

		private FlowLayoutPanel flowLayoutPanel11;

		private TableLayoutPanel tableLayoutPanel3;

		private FlowLayoutPanel flowLayoutPanel13;

		private FlowLayoutPanel flowLayoutPanel14;

		private Panel panel14;

		private Label label15;

		private Panel panel15;

		private Label label16;

		private Panel panel16;

		private Label label17;

		private Panel panel17;

		private Label label18;

		private Panel panel18;

		private Label label19;

		private Panel panel19;

		private Label label20;

		private FlowLayoutPanel flowLayoutPanel15;

		private FlowLayoutPanel flowLayoutPanel16;

		private FlowLayoutPanel flowLayoutPanel17;

		private FlowLayoutPanel flowLayoutPanel18;

		private DateTimePicker dateTimePicker0;

		private DateTimePicker dateTimePicker1;

		private MyCheckBox myCheckBox1;

		private MyCheckBox myCheckBox2;

		private MyCheckBox myCheckBox3;

		private MyCheckBox myCheckBox4;

		private MyCheckBox myCheckBox5;

		private MyCheckBox myCheckBox6;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private RadioButton radioButton3;

		private RadioButton radioButton4;

		private RadioButton radioButton5;

		private NumericUpDown numericUpDown1;

		private NumericUpDown numericUpDown2;

		private Label label21;

		private Label label22;

		private DateTimePicker dateTimePicker2;

		private Label label5;

		private DateTimePicker dateTimePicker3;

		private MyCheckBox myCheckBox7;

		private MyCheckBox myCheckBox8;

		private MyCheckBox myCheckBox9;

		private RadioButton radioButton6;

		private RadioButton radioButton7;

		private MyCheckBox myCheckBox10;

		private MyCheckBox myCheckBox11;

		private ComboBox cb_area;

		private DateTimePicker dateTimePicker4;

		private Label label23;

		private DateTimePicker dateTimePicker5;

		private MyCheckBox myCheckBox12;

		private MyCheckBox myCheckBox13;

		private MyCheckBox myCheckBox14;

		private MyCheckBox myCheckBox15;

		private MyCheckBox myCheckBox16;

		private MyCheckBox myCheckBox17;

		private MyCheckBox myCheckBox18;

		private MyCheckBox myCheckBox19;

		private MyCheckBox myCheckBox20;

		private RadioButton radioButton8;

		private RadioButton radioButton9;

		private Button btn_reset;

		private Button btn_SearchPeriodTransactions;

		private Button btn_reset_member;

		private Button btn_SearchMemberSalesSummary;

		private Button btn_reset_Commodity;

		private Button btn_SearchCommodityTradingSummary;

		private DataGridView dgv_SynchronizeList;

		private TableLayoutPanel tableLayoutPanel4;

		private Panel panel20;

		private Label label25;

		private FlowLayoutPanel flowLayoutPanel12;

		public Label l_title;

		private DateTimePicker dtp_SynchronizeDate;

		private Button btn_SynchronizeFilter;

		private DataGridView dgv_saleDetail;

		private Button btn_ExportTodayReport;

		private DataGridView dgv_saleDetailTotal;

		private Label l_nowDate;

		private Button btn_SelectMember;

		private Button btn_SelectCommodity;

		private FlowLayoutPanel FLP_saleDetail;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private TabPage tabPage5;

		private TabPage tabPage6;

		private TabPage tabPage7;

		private FlowLayoutPanel FLP_DeliveryDetail;

		private DataGridView dgv_DeliveryDetail;

		private Button btn_ExportTodayDeliveryReport;

		private DataGridView dgv_DeliveryTotal;

		private Label l_nowDate2;

		private Button btn_CommDelReset;

		private Button btn_SearchGoodsDelivery;

		private TableLayoutPanel tableLayoutPanel5;

		private FlowLayoutPanel flowLayoutPanel20;

		private DateTimePicker dateTimePicker6;

		private Label label24;

		private DateTimePicker dateTimePicker7;

		private FlowLayoutPanel flowLayoutPanel21;

		private Button btn_SelectCommodity2;

		private Panel panel7;

		private Label label26;

		private Panel panel21;

		private Label label27;

		private Panel panel22;

		private Label label28;

		private Panel panel23;

		private Label label29;

		private Panel panel24;

		private Label label30;

		private Panel panel25;

		private Label label31;

		private FlowLayoutPanel flowLayoutPanel22;

		private MyCheckBox myCheckBox21;

		private MyCheckBox myCheckBox22;

		private FlowLayoutPanel flowLayoutPanel23;

		private MyCheckBox myCheckBox23;

		private MyCheckBox myCheckBox24;

		private MyCheckBox myCheckBox25;

		private MyCheckBox myCheckBox26;

		private FlowLayoutPanel flowLayoutPanel24;

		private MyCheckBox myCheckBox27;

		private MyCheckBox myCheckBox28;

		private MyCheckBox myCheckBox29;

		private FlowLayoutPanel flowLayoutPanel25;

		private RadioButton radioButton10;

		private RadioButton radioButton11;

		private Button btn_VendorDelReset;

		private Button btn_SearchVendorDelivery;

		private TableLayoutPanel tableLayoutPanel6;

		private FlowLayoutPanel flowLayoutPanel26;

		private DateTimePicker dateTimePicker8;

		private Label label32;

		private DateTimePicker dateTimePicker9;

		private FlowLayoutPanel flowLayoutPanel27;

		private Button btn_SelectVendor;

		private Panel panel26;

		private Label label33;

		private Panel panel27;

		private Label label34;

		private Panel panel28;

		private Label label35;

		private Panel panel29;

		private Label label36;

		private Panel panel30;

		private Label label37;

		private FlowLayoutPanel flowLayoutPanel28;

		private MyCheckBox myCheckBox30;

		private MyCheckBox myCheckBox31;

		private MyCheckBox myCheckBox32;

		private FlowLayoutPanel flowLayoutPanel29;

		private ComboBox cb_vendorstatus;

		private FlowLayoutPanel flowLayoutPanel30;

		private RadioButton radioButton12;

		private RadioButton radioButton13;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

		private DateTimePicker dateTimePicker10;

		private Label label38;

		private DateTimePicker dateTimePicker11;

		private FlowLayoutPanel flowLayoutPanel19;

		private Button btn_SalesSelectMember;

		private Button btn_saleSearch;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

		private DataGridViewTextBoxColumn Column7;

		private DataGridViewTextBoxColumn Column8;

		private DataGridViewTextBoxColumn Column9;

		public frmStatisticsRecord()
			: base("報表查詢")
		{
			InitializeComponent();
			cb_area.Items.Add(new ComboboxItem("全部", "*"));
			cb_area.Items.Add(new ComboboxItem("正常", "0"));
			cb_area.Items.Add(new ComboboxItem("已停用", "1"));
			cb_area.SelectedIndex = 0;
			cb_vendorstatus.Items.Add(new ComboboxItem("全部", "*"));
			cb_vendorstatus.Items.Add(new ComboboxItem("正常", "0"));
			cb_vendorstatus.Items.Add(new ComboboxItem("已停用", "1"));
			cb_vendorstatus.SelectedIndex = 0;
			dateTimePicker0.Value = DateTime.Today.AddMonths(-3);
			dateTimePicker1.Value = DateTime.Today;
			dateTimePicker2.Value = DateTime.Today.AddMonths(-3);
			dateTimePicker3.Value = DateTime.Today;
			dateTimePicker4.Value = DateTime.Today.AddMonths(-3);
			dateTimePicker5.Value = DateTime.Today;
			dateTimePicker6.Value = DateTime.Today.AddMonths(-3);
			dateTimePicker7.Value = DateTime.Today;
			dateTimePicker8.Value = DateTime.Today.AddMonths(-3);
			dateTimePicker9.Value = DateTime.Today;
			l_nowDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
			l_nowDate2.Text = DateTime.Today.ToString("yyyy-MM-dd");
			dtp_SynchronizeDate.Value = DateTime.Today;
			dateTimePicker10.Value = DateTime.Today;
			dateTimePicker11.Value = DateTime.Today;
		}

		private void frmStatisticsRecord_Load(object sender, EventArgs e)
		{
			string sql = "SELECT sum(ms.sumExcept) as sum, sum(ms.Refund) as Refund, sum(ms.cash) as cash, sum(ms.Credit) as Credit, sum(ms.returnChange) as returnChange, sum(ms.itemstotalExcept) as itemstotal FROM ( SELECT *, CASE status WHEN '1' THEN 0 ELSE ms3.sum END as sumExcept, CASE status WHEN '1' THEN 0 ELSE itemstotal END as itemstotalExcept FROM hypos_main_sell as ms2 INNER JOIN ( SELECT sellNo, sum FROM hypos_mainsell_log where sum is not null GROUP BY sellNo ORDER BY changeDate desc ) as ms3 on ms2.sellNo = ms3.sellNo ) as ms WHERE ms.sellTime between datetime(date('now'), '-1 seconds') and datetime(date('now'), '+1 days') ";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
			string sql2 = "SELECT count(DISTINCT ds1.barcode) as items FROM ( SELECT ds.* , ms1.status as status, ms1.sellTime FROM hypos_detail_sell as ds INNER JOIN hypos_main_sell as ms1 on ds.sellNo = ms1.sellNo WHERE ms1.sellTime between datetime(date('now'), '-1 seconds') and datetime(date('now'), '+1 days') ORDER BY ms1.sellTime desc ) as ds1 WHERE ds1.status != '1'";
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				int result = 0;
				int result2 = 0;
				int result3 = 0;
				int result4 = 0;
				int.TryParse(dataTable.Rows[0]["sum"].ToString(), out result);
				int.TryParse(dataTable.Rows[0]["cash"].ToString(), out result2);
				int.TryParse(dataTable.Rows[0]["Credit"].ToString(), out result3);
				int.TryParse(dataTable.Rows[0]["returnChange"].ToString(), out result4);
				dgv_saleDetailTotal.Rows.Add(result + "(" + (result2 + result3 - result4).ToString() + ")", string.IsNullOrEmpty(dataTable.Rows[0]["Refund"].ToString()) ? "0" : dataTable.Rows[0]["Refund"].ToString(), "現金(" + (string.IsNullOrEmpty(dataTable.Rows[0]["cash"].ToString()) ? "0" : dataTable.Rows[0]["cash"].ToString()).ToString() + ")/賒帳(" + (string.IsNullOrEmpty(dataTable.Rows[0]["Credit"].ToString()) ? "0" : dataTable.Rows[0]["Credit"].ToString()).ToString() + ")/找零(" + (string.IsNullOrEmpty(dataTable.Rows[0]["returnChange"].ToString()) ? "0" : dataTable.Rows[0]["returnChange"].ToString()).ToString() + ")", string.IsNullOrEmpty(dataTable2.Rows[0]["items"].ToString()) ? "0" : dataTable2.Rows[0]["items"].ToString(), string.IsNullOrEmpty(dataTable.Rows[0]["itemstotal"].ToString()) ? "0" : dataTable.Rows[0]["itemstotal"].ToString());
			}
			showSyncLog();
			showTodaysSalesSummaryLog();
			showTodayDeliverySummary();
			showTodayDeliverySummaryLog();
		}

		private void showTodayDeliverySummary()
		{
			string sql = "SELECT sum(CurSum) as CurSum, sum(OriSum) as OriSum, sum(items) as items, sum(itemstotal) as itemstotal FROM hypos_DeliveryGoods_Master where CreateDate between datetime(date('now'), '-1 seconds') and datetime(date('now'), '+1 days')";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				int result = 0;
				int result2 = 0;
				int result3 = 0;
				int result4 = 0;
				int.TryParse(dataTable.Rows[0]["CurSum"].ToString(), out result);
				int.TryParse(dataTable.Rows[0]["OriSum"].ToString(), out result2);
				int.TryParse(dataTable.Rows[0]["items"].ToString(), out result3);
				int.TryParse(dataTable.Rows[0]["itemstotal"].ToString(), out result4);
				dgv_DeliveryTotal.Rows.Add(result, result2, result3, result4);
			}
		}

		private void showTodayDeliverySummaryLog()
		{
			try
			{
				string sql = "SELECT DeliveryNo, vendorNo, CurSum, OriSum, items, itemstotal, status FROM hypos_DeliveryGoods_Master where CreateDate between datetime(date('now'), '-1 seconds') and datetime(date('now'), '+1 days') order by CreateDate desc";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count <= 0)
				{
					return;
				}
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					int result = 0;
					int result2 = 0;
					int result3 = 0;
					int result4 = 0;
					string text = "";
					int.TryParse(dataTable.Rows[i]["CurSum"].ToString(), out result);
					int.TryParse(dataTable.Rows[i]["OriSum"].ToString(), out result2);
					int.TryParse(dataTable.Rows[i]["items"].ToString(), out result3);
					int.TryParse(dataTable.Rows[i]["itemstotal"].ToString(), out result4);
					ucDeliveryOrderInfo ucDeliveryOrderInfo = new ucDeliveryOrderInfo();
					if (i % 2 == 0)
					{
						ucDeliveryOrderInfo.setBackColor("even_cellstyle");
					}
					else
					{
						ucDeliveryOrderInfo.setBackColor("odd_cellstyle");
					}
					ucDeliveryOrderInfo.setDeliveryNo(string.IsNullOrEmpty(dataTable.Rows[i]["DeliveryNo"].ToString()) ? "N/A" : dataTable.Rows[i]["DeliveryNo"].ToString());
					string sql2 = "SELECT SupplierName, vendorId, vendorName FROM hypos_Supplier where SupplierNo = '" + dataTable.Rows[i]["vendorNo"].ToString() + "'";
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count > 0)
					{
						ucDeliveryOrderInfo.setVendor(dataTable2.Rows[0]["SupplierName"].ToString() + "(" + dataTable2.Rows[0]["vendorId"].ToString() + "/" + dataTable2.Rows[0]["vendorName"].ToString() + ")");
					}
					else
					{
						ucDeliveryOrderInfo.setVendor("N/A");
					}
					ucDeliveryOrderInfo.setSum(result + "(" + result2 + ")");
					ucDeliveryOrderInfo.setItems(string.IsNullOrEmpty(dataTable.Rows[i]["items"].ToString()) ? "N/A" : dataTable.Rows[i]["items"].ToString());
					ucDeliveryOrderInfo.setNum(string.IsNullOrEmpty(dataTable.Rows[i]["itemstotal"].ToString()) ? "N/A" : dataTable.Rows[i]["itemstotal"].ToString());
					switch (string.IsNullOrEmpty(dataTable.Rows[i]["status"].ToString()) ? "N/A" : dataTable.Rows[i]["status"].ToString())
					{
					case "0":
						text = "正常";
						break;
					case "1":
						text = "取消";
						break;
					case "2":
						text = "變更(編修)";
						break;
					default:
						text = "N/A";
						break;
					}
					ucDeliveryOrderInfo.setStatus(text);
					FLP_DeliveryDetail.Controls.Add(ucDeliveryOrderInfo);
					string sql3 = "SELECT barcode,num,BatchNo,MFGDate FROM hypos_DeliveryGoods_Detail where DeliveryNo = '" + dataTable.Rows[i]["DeliveryNo"].ToString() + "'";
					DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql3, null, CommandOperationType.ExecuteReaderReturnDataTable);
					for (int j = 0; j < dataTable3.Rows.Count; j++)
					{
						ucDeliveryOrderInfoDetail ucDeliveryOrderInfoDetail = new ucDeliveryOrderInfoDetail();
						if (i % 2 == 0)
						{
							ucDeliveryOrderInfoDetail.setBackColor("even_cellstyle");
						}
						else
						{
							ucDeliveryOrderInfoDetail.setBackColor("odd_cellstyle");
						}
						string text2 = string.IsNullOrEmpty(dataTable3.Rows[j]["barcode"].ToString()) ? "N/A" : dataTable3.Rows[j]["barcode"].ToString();
						string sql4 = "SELECT GDName,CName,formCode,contents,brandName,spec,capacity FROM hypos_GOODSLST where GDSNO = '" + text2 + "'";
						DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql4, null, CommandOperationType.ExecuteReaderReturnDataTable);
						string text3 = dataTable4.Rows[0]["GDName"].ToString() + "[" + dataTable4.Rows[0]["CName"].ToString() + "-" + dataTable4.Rows[0]["formCode"].ToString() + "．" + dataTable4.Rows[0]["contents"].ToString() + "-" + dataTable4.Rows[0]["brandName"].ToString() + "]";
						string text4 = dataTable4.Rows[0]["spec"].ToString() + dataTable4.Rows[0]["capacity"].ToString();
						string text5 = "(" + (string.IsNullOrEmpty(dataTable3.Rows[j]["BatchNo"].ToString()) ? "無設定批號" : dataTable3.Rows[j]["BatchNo"].ToString()) + " / " + (string.IsNullOrEmpty(dataTable3.Rows[j]["MFGDate"].ToString()) ? "無設定製造日期" : dataTable3.Rows[j]["MFGDate"].ToString()) + ")";
						ucDeliveryOrderInfoDetail.setSellNo(text2.PadRight(20, ' ') + text3 + Environment.NewLine + text4.PadRight(20, ' ') + text5);
						ucDeliveryOrderInfoDetail.setCommodityInfo(dataTable3.Rows[j]["num"].ToString());
						FLP_DeliveryDetail.Controls.Add(ucDeliveryOrderInfoDetail);
					}
					report_DeliveryDaily_rowcount += dataTable3.Rows.Count;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void showTodaysSalesSummary()
		{
			dgv_saleDetailTotal.Rows.Clear();
			string text = "";
			string text2 = "";
			string text3 = dateTimePicker10.Value.ToString("yyyy-MM-dd");
			string text4 = dateTimePicker11.Value.ToString("yyyy-MM-dd");
			for (int i = 0; i < lst_Salesselmember.Count; i++)
			{
				string[] array = lst_Salesselmember[i].Split(',');
				if (i == 0)
				{
					text = text + " and ms2.memberId in ( '" + array[0] + "' ";
					text2 = text2 + " and ms1.memberId in ( '" + array[0] + "' ";
				}
				else
				{
					text = text + " , '" + array[0] + "'";
					text2 = text2 + " , '" + array[0] + "'";
				}
			}
			if (lst_Salesselmember.Count > 0)
			{
				text += ") ";
				text2 += ") ";
			}
			string sql = "SELECT sum(ms.sumExcept) as sum, sum(ms.Refund) as Refund, sum(ms.cash) as cash, sum(ms.Credit) as Credit, sum(ms.returnChange) as returnChange, sum(ms.itemstotalExcept) as itemstotal FROM  ( SELECT *, CASE status WHEN '1' THEN 0 ELSE ms3.sum END as sumExcept, CASE status WHEN '1' THEN 0 ELSE itemstotal END as itemstotalExcept FROM hypos_main_sell as ms2 INNER JOIN ( SELECT sellNo, sum FROM hypos_mainsell_log where sum is not null GROUP BY sellNo ORDER BY changeDate desc ) as ms3 on ms2.sellNo = ms3.sellNo  WHERE ms2.sellTime between datetime(date('" + text3 + "'), '-1 seconds') and datetime(date('" + text4 + "'), '+1 days') " + text + ") as ms ";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
			string sql2 = "SELECT count(DISTINCT ds1.barcode) as items FROM ( SELECT ds.* , ms1.status as status, ms1.sellTime FROM hypos_detail_sell as ds INNER JOIN hypos_main_sell as ms1 on ds.sellNo = ms1.sellNo WHERE ms1.sellTime between datetime(date('" + text3 + "'), '-1 seconds') and datetime(date('" + text4 + "'), '+1 days') " + text2 + " ORDER BY ms1.sellTime desc ) as ds1 WHERE ds1.status != '1'";
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				int result = 0;
				int result2 = 0;
				int result3 = 0;
				int result4 = 0;
				int.TryParse(dataTable.Rows[0]["sum"].ToString(), out result);
				int.TryParse(dataTable.Rows[0]["cash"].ToString(), out result2);
				int.TryParse(dataTable.Rows[0]["Credit"].ToString(), out result3);
				int.TryParse(dataTable.Rows[0]["returnChange"].ToString(), out result4);
				dgv_saleDetailTotal.Rows.Add(result + "(" + (result2 + result3 - result4).ToString() + ")", string.IsNullOrEmpty(dataTable.Rows[0]["Refund"].ToString()) ? "0" : dataTable.Rows[0]["Refund"].ToString(), "現金(" + (string.IsNullOrEmpty(dataTable.Rows[0]["cash"].ToString()) ? "0" : dataTable.Rows[0]["cash"].ToString()).ToString() + ")/賒帳(" + (string.IsNullOrEmpty(dataTable.Rows[0]["Credit"].ToString()) ? "0" : dataTable.Rows[0]["Credit"].ToString()).ToString() + ")/找零(" + (string.IsNullOrEmpty(dataTable.Rows[0]["returnChange"].ToString()) ? "0" : dataTable.Rows[0]["returnChange"].ToString()).ToString() + ")", string.IsNullOrEmpty(dataTable2.Rows[0]["items"].ToString()) ? "0" : dataTable2.Rows[0]["items"].ToString(), string.IsNullOrEmpty(dataTable.Rows[0]["itemstotal"].ToString()) ? "0" : dataTable.Rows[0]["itemstotal"].ToString());
			}
		}

		private void showTodaysSalesSummaryLog()
		{
			FLP_saleDetail.Controls.Clear();
			string text = "";
			string text2 = dateTimePicker10.Value.ToString("yyyy-MM-dd");
			string text3 = dateTimePicker11.Value.ToString("yyyy-MM-dd");
			for (int i = 0; i < lst_Salesselmember.Count; i++)
			{
				string[] array = lst_Salesselmember[i].Split(',');
				text = ((i != 0) ? (text + " , '" + array[0] + "'") : (text + " and ms.memberId in ( '" + array[0] + "' "));
			}
			if (lst_Salesselmember.Count > 0)
			{
				text += ") ";
			}
			string sql = "SELECT ms.sellNo as sellNo, ms.sellTime, cr.Name as Name, ms.memberId as memberId, (ms.sum-ms.sumDiscount) as sum, ms.cash as cash, ms.Credit as Credit, ms.items as items, ms.itemstotal as itemstotal, ms.status as status, ds.sellNoCount as sellNoCount, ms.returnChange as returnChange FROM hypos_main_sell as ms LEFT JOIN hypos_CUST_RTL as cr on cr.VipNo = ms.memberId INNER JOIN ( SELECT ds.sellNo as sellNo, count(ds.sellNo) as sellNoCount FROM hypos_detail_sell as ds GROUP BY ds.sellNo ) as ds on ms.sellNo = ds.sellNo WHERE ms.sellTime between datetime(date('" + text2 + "'), '-1 seconds') and datetime(date('" + text3 + "'), '+1 days') " + text + " ORDER BY ms.sellTime desc";
			dt_details1 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
			string sql2 = "SELECT ds1.barcode as barcode, ds1.sellNo as sellNo, sellTime, ds1.sellTime, gl.GDName as GDName, gl.CName as CName, gl.formCode as formCode, gl.contents as contents, gl.brandName as brandName, gl.spec as spec, gl.capacity as capacity, ds1.num as num FROM ( SELECT ds.* , ms.sellTime FROM hypos_detail_sell as ds INNER JOIN hypos_main_sell as ms on ds.sellNo = ms.sellNo WHERE ms.sellTime between datetime(date('" + text2 + "'), '-1 seconds') and datetime(date('" + text3 + "'), '+1 days') " + text + " ORDER BY ms.sellTime desc, ds.sellDeatialId desc  ) as ds1 LEFT JOIN hypos_GOODSLST as gl on ds1.barcode = gl.GDSNO";
			dt_details2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dt_details1.Rows.Count <= 0)
			{
				return;
			}
			int num = 0;
			report_daily_rowcount = 0;
			for (int j = 0; j < dt_details1.Rows.Count; j++)
			{
				int result = 0;
				int result2 = 0;
				int result3 = 0;
				int result4 = 0;
				int result5 = 0;
				string status = "";
				int.TryParse(dt_details1.Rows[j]["cash"].ToString(), out result);
				int.TryParse(dt_details1.Rows[j]["Credit"].ToString(), out result2);
				int.TryParse(dt_details1.Rows[j]["returnChange"].ToString(), out result5);
				int.TryParse(dt_details1.Rows[j]["items"].ToString(), out result3);
				int.TryParse(dt_details1.Rows[j]["sellNoCount"].ToString(), out result4);
				ucSaleOrderInfo ucSaleOrderInfo = new ucSaleOrderInfo();
				if (j % 2 == 0)
				{
					ucSaleOrderInfo.setBackColor("even_cellstyle");
				}
				else
				{
					ucSaleOrderInfo.setBackColor("odd_cellstyle");
				}
				ucSaleOrderInfo.setSellNo(string.IsNullOrEmpty(dt_details1.Rows[j]["sellTime"].ToString()) ? "0" : dt_details1.Rows[j]["sellTime"].ToString());
				ucSaleOrderInfo.setName(string.IsNullOrEmpty(dt_details1.Rows[j]["Name"].ToString()) ? "非會員" : (dt_details1.Rows[j]["Name"].ToString() + "(" + dt_details1.Rows[j]["memberId"].ToString() + ")"));
				ucSaleOrderInfo.setSum(string.IsNullOrEmpty(dt_details1.Rows[j]["Sum"].ToString()) ? "0" : (dt_details1.Rows[j]["Sum"].ToString() + "(" + (result + result2 - result5) + ")"));
				ucSaleOrderInfo.setCashCredit("現金(" + result + ")/賒帳(" + result2 + ")/找零(" + result5 + ")");
				ucSaleOrderInfo.setItems(string.IsNullOrEmpty(dt_details1.Rows[j]["items"].ToString()) ? "0" : dt_details1.Rows[j]["items"].ToString());
				ucSaleOrderInfo.setNum(string.IsNullOrEmpty(dt_details1.Rows[j]["itemstotal"].ToString()) ? "0" : dt_details1.Rows[j]["itemstotal"].ToString());
				switch (string.IsNullOrEmpty(dt_details1.Rows[j]["status"].ToString()) ? "0" : dt_details1.Rows[j]["status"].ToString())
				{
				case "0":
					status = "正常";
					break;
				case "1":
					status = "取消";
					break;
				case "2":
					status = "變更";
					break;
				}
				ucSaleOrderInfo.setStatus(status);
				FLP_saleDetail.Controls.Add(ucSaleOrderInfo);
				report_daily_rowcount += result4;
				for (int k = 0; k < result4; k++)
				{
					ucSaleOrderInfoDetail ucSaleOrderInfoDetail = new ucSaleOrderInfoDetail();
					if (j % 2 == 0)
					{
						ucSaleOrderInfoDetail.setBackColor("even_cellstyle");
					}
					else
					{
						ucSaleOrderInfoDetail.setBackColor("odd_cellstyle");
					}
					ucSaleOrderInfoDetail.setSellNo(string.IsNullOrEmpty(dt_details2.Rows[num]["sellTime"].ToString()) ? "0" : dt_details2.Rows[num]["sellTime"].ToString());
					ucSaleOrderInfoDetail.setCommodityInfo(dt_details2.Rows[num]["GDName"].ToString() + "[" + dt_details2.Rows[num]["CName"].ToString() + "-" + dt_details2.Rows[num]["formCode"].ToString() + "．" + dt_details2.Rows[num]["contents"].ToString() + "-" + dt_details2.Rows[num]["brandName"].ToString() + "]" + dt_details2.Rows[num]["spec"].ToString() + dt_details2.Rows[num]["capacity"].ToString());
					ucSaleOrderInfoDetail.setNum(string.IsNullOrEmpty(dt_details2.Rows[num]["num"].ToString()) ? "0" : dt_details2.Rows[num]["num"].ToString());
					FLP_saleDetail.Controls.Add(ucSaleOrderInfoDetail);
					num++;
				}
			}
		}

		private void showSyncLog()
		{
			dtp_SynchronizeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_Synchronize_main_log", "updateDate > date('now', '-7 day')", "updateDate DESC", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				string text = "";
				string a = row["status"].ToString();
				if (!(a == "0"))
				{
					if (a == "1")
					{
						text = "失敗";
					}
				}
				else
				{
					text = "正常";
				}
				string name = Enum.GetName(typeof(SynchronizeType), int.Parse(row["updateType"].ToString()));
				if (text == "正常" && name == "銷售紀錄檔上傳")
				{
					dgv_SynchronizeList.Rows.Add(row["updateDate"].ToString(), text, name, "檢視詳細");
				}
				else if (text == "正常" && name == "庫存調整紀錄檔上傳")
				{
					dgv_SynchronizeList.Rows.Add(row["updateDate"].ToString(), text, name, "檢視詳細");
				}
				else if (text == "正常" && name == "出貨紀錄檔上傳")
				{
					dgv_SynchronizeList.Rows.Add(row["updateDate"].ToString(), text, name, "檢視詳細");
				}
				else
				{
					dgv_SynchronizeList.Rows.Add(row["updateDate"].ToString(), text, name, "");
				}
			}
		}

		private void btn_SynchronizeFilter_Click(object sender, EventArgs e)
		{
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_Synchronize_main_log", "updateDate between datetime(date({0}), '-60 day') and datetime(date({0}), '+1 days')  ", "updateDate DESC", null, new string[1]
			{
				dtp_SynchronizeDate.Value.ToString("yyyy-MM-dd")
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			dgv_SynchronizeList.Rows.Clear();
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				string text = "";
				string a = row["status"].ToString();
				if (!(a == "0"))
				{
					if (a == "1")
					{
						text = "失敗";
					}
				}
				else
				{
					text = "正常";
				}
				string name = Enum.GetName(typeof(SynchronizeType), int.Parse(row["updateType"].ToString()));
				if (text == "正常" && name == "銷售紀錄檔上傳")
				{
					dgv_SynchronizeList.Rows.Add(row["updateDate"].ToString(), text, name, "檢視詳細");
				}
				else if (text == "正常" && name == "庫存調整紀錄檔上傳")
				{
					dgv_SynchronizeList.Rows.Add(row["updateDate"].ToString(), text, name, "檢視詳細");
				}
				else if (text == "正常" && name == "出貨紀錄檔上傳")
				{
					dgv_SynchronizeList.Rows.Add(row["updateDate"].ToString(), text, name, "檢視詳細");
				}
				else
				{
					dgv_SynchronizeList.Rows.Add(row["updateDate"].ToString(), text, name, "");
				}
			}
		}

		private void btn_SearchPeriodTransactions_Click(object sender, EventArgs e)
		{
			string text = "";
			string text2 = "";
			string str = "";
			string reportMode = "[日報表]";
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			int num = 0;
			int num2 = 0;
			string text3 = "SELECT sum(ms.sumtrue) as sum, sum(ms.cash) as cash, sum(ms.Credit) as Credit,sum(ms.returnChange) as returnChange, sum(ms.Refund) as Refund, count(DISTINCT ms.sellNo) as consumptionTimes, sum(ms.itemstotalExcept) as itemstotal FROM ( SELECT *, CASE ms2.status WHEN '1' THEN 0 ELSE ms3.sum END as sumtrue, CASE ms2.status WHEN '1' THEN 0 ELSE ms2.itemstotal END as itemstotalExcept FROM hypos_main_sell as ms2 INNER JOIN ( SELECT sellNo, sum FROM hypos_mainsell_log GROUP BY sellNo ORDER BY changeDate desc ) as ms3 on ms2.sellNo = ms3.sellNo";
			string text4 = "";
			string str2 = "";
			if (radioButton1.Checked)
			{
				text4 += " SELECT strftime('%Y-%m', ms.sellTime) as 'Time', sum(ms.sumtrue) as sum, sum(ms.cash) as cash, sum(ms.Credit) as Credit, sum(ms.returnChange) as returnChange, sum(ms.Refund) as Refund, count(DISTINCT ms.sellNo) as consumptionTimes, sum(ms.itemstotalExcept) as itemstotal FROM ( SELECT *, CASE ms2.status WHEN '1' THEN 0 ELSE ms3.sum END as sumtrue, CASE ms2.status WHEN '1' THEN 0 ELSE ms2.itemstotal END as itemstotalExcept FROM hypos_main_sell as ms2 INNER JOIN ( SELECT sellNo, sum FROM hypos_mainsell_log GROUP BY sellNo ORDER BY changeDate desc ) as ms3 on ms2.sellNo = ms3.sellNo ";
			}
			else if (radioButton2.Checked)
			{
				text4 += " SELECT strftime('%Y-%m', ms.sellTime) as 'Time', strftime('%W', date(ms.sellTime))-strftime('%W', date(ms.sellTime,'start of month')) as 'week', sum(ms.sumtrue) as sum, sum(ms.cash) as cash, sum(ms.Credit) as Credit, sum(ms.returnChange) as returnChange, sum(ms.Refund) as Refund, count(DISTINCT ms.sellNo) as consumptionTimes, sum(ms.itemstotalExcept) as itemstotal FROM ( SELECT *, CASE ms2.status WHEN '1' THEN 0 ELSE ms3.sum END as sumtrue, CASE ms2.status WHEN '1' THEN 0 ELSE ms2.itemstotal END as itemstotalExcept FROM hypos_main_sell as ms2 INNER JOIN ( SELECT sellNo, sum FROM hypos_mainsell_log GROUP BY sellNo ORDER BY changeDate desc ) as ms3 on ms2.sellNo = ms3.sellNo";
			}
			else if (radioButton3.Checked)
			{
				text4 += " SELECT strftime('%Y-%m-%d', ms.sellTime) as 'Time', sum(ms.sumtrue) as sum, sum(ms.cash) as cash, sum(ms.Credit) as Credit, sum(ms.returnChange) as returnChange, sum(ms.Refund) as Refund, count(DISTINCT ms.sellNo) as consumptionTimes, sum(ms.itemstotalExcept) as itemstotal FROM ( SELECT *, CASE ms2.status WHEN '1' THEN 0 ELSE ms3.sum END as sumtrue, CASE ms2.status WHEN '1' THEN 0 ELSE ms2.itemstotal END as itemstotalExcept FROM hypos_main_sell as ms2 INNER JOIN ( SELECT sellNo, sum FROM hypos_mainsell_log GROUP BY sellNo ORDER BY changeDate desc ) as ms3 on ms2.sellNo = ms3.sellNo ";
			}
			text3 = text3 + " WHERE ms2.sellTime between {" + num2 + "}  ";
			list.Add(dateTimePicker0.Value.ToString("yyyy-MM-dd"));
			num2++;
			text3 = text3 + "  and datetime(date( {" + num2 + "} ), '+1 days')";
			list.Add(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
			num2++;
			text4 = text4 + " WHERE ms2.sellTime between {" + num + "}  ";
			list2.Add(dateTimePicker0.Value.ToString("yyyy-MM-dd"));
			num++;
			text4 = text4 + "  and datetime(date( {" + num + "} ), '+1 days')";
			list2.Add(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
			num++;
			if (myCheckBox1.Checked | myCheckBox2.Checked | myCheckBox3.Checked)
			{
				str2 += " AND ms2.status in (";
				if (myCheckBox1.Checked)
				{
					text += "[正常]";
					str2 += " '0' ";
				}
				if (myCheckBox2.Checked)
				{
					text += "[變更]";
					if (myCheckBox1.Checked)
					{
						str2 += ",";
					}
					str2 += " '2' ";
				}
				if (myCheckBox3.Checked)
				{
					text += "[取消]";
					if (myCheckBox1.Checked | myCheckBox2.Checked)
					{
						str2 += ",";
					}
					str2 += " '1' ";
				}
				str2 += ") ";
			}
			else
			{
				text += "[全部]";
			}
			if (myCheckBox4.Checked && !myCheckBox5.Checked && !myCheckBox6.Checked)
			{
				text2 += "[現金]";
				str2 += " AND ms2.Credit = 0 AND ms2.cash >= 0 ";
			}
			if (!myCheckBox4.Checked && myCheckBox5.Checked && !myCheckBox6.Checked)
			{
				text2 += "[賒帳]";
				str2 += " AND ms2.cash = 0 AND ms2.Credit > 0 ";
			}
			if (!myCheckBox4.Checked && !myCheckBox5.Checked && myCheckBox6.Checked)
			{
				text2 += "[部分現金部分賒帳]";
				str2 += " AND ms2.cash > 0 AND ms2.Credit > 0 ";
			}
			if (myCheckBox4.Checked && myCheckBox5.Checked && !myCheckBox6.Checked)
			{
				text2 += "[現金][賒帳]";
				str2 += " AND ( (ms2.cash = '0' AND ms2.Credit > 0) OR (ms2.Credit = '0' AND ms2.cash > 0) ) ";
			}
			if (!myCheckBox4.Checked && myCheckBox5.Checked && myCheckBox6.Checked)
			{
				text2 += "[賒帳][部分現金部分賒帳]";
				str2 += " AND ( (ms2.cash > 0 AND ms2.Credit > 0) OR (ms2.Credit = '0' AND ms2.cash > 0) ) ";
			}
			if (myCheckBox4.Checked && myCheckBox5.Checked && myCheckBox6.Checked)
			{
				text2 += "[全部]";
				str2 += " AND ( ms2.cash >= 0 OR ms2.Credit > 0 ) ";
			}
			if (!myCheckBox4.Checked && !myCheckBox5.Checked && !myCheckBox6.Checked)
			{
				text2 += "[全部]";
				str2 += " AND ( ms2.cash >= 0 OR ms2.Credit > 0 ) ";
			}
			str2 += " AND ms2.sum >= ";
			str2 += numericUpDown1.Value;
			str2 += " AND ms2.sum <= ";
			str2 += numericUpDown2.Value;
			str = str + numericUpDown1.Value + "~" + numericUpDown2.Value;
			text4 += str2;
			text3 += str2;
			text4 += " )as ms ";
			text3 += " )as ms ";
			if (radioButton1.Checked)
			{
				reportMode = "[月報表]";
				text4 += " GROUP BY strftime('%Y-%m', ms.sellTime) ";
			}
			else if (radioButton2.Checked)
			{
				reportMode = "[週報表]";
				text4 += " GROUP BY strftime('%Y-%m', ms.sellTime), strftime('%W', date(ms.sellTime))-strftime('%W', date(ms.sellTime,'start of month')) ";
			}
			else if (radioButton3.Checked)
			{
				reportMode = "[日報表]";
				text4 += " GROUP BY strftime('%Y-%m-%d', ms.sellTime) ";
			}
			if (radioButton4.Checked)
			{
				text4 += " ORDER BY sum(ms.sum) desc ";
			}
			else if (radioButton5.Checked)
			{
				if (radioButton1.Checked)
				{
					text4 += " ORDER BY strftime('%Y-%m', ms.sellTime) desc ";
				}
				else if (radioButton2.Checked)
				{
					text4 += " ORDER BY strftime('%Y-%m', ms.sellTime), strftime('%W', date(ms.sellTime))-strftime('%W', date(ms.sellTime,'start of month')) desc ";
				}
				else if (radioButton3.Checked)
				{
					text4 += " ORDER BY strftime('%Y-%m-%d', ms.sellTime) desc ";
				}
			}
			try
			{
				Close();
				DataTable dt_summary = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text3, list.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				DataTable dt_details = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text4, list2.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				frmSearchPeriodTransactions frmSearchPeriodTransactions = new frmSearchPeriodTransactions(dt_summary, dt_details, dateTimePicker0.Value.ToString("yyyy-MM-dd"), dateTimePicker1.Value.ToString("yyyy-MM-dd"), text, text2, reportMode, str);
				frmSearchPeriodTransactions.Location = new Point(base.Location.X, base.Location.Y);
				frmSearchPeriodTransactions.Show();
				Hide();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error Message : " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_SearchMemberSalesSummary_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			int num = 0;
			int num2 = 0;
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "SELECT M.*,T.consumerItems FROM ( SELECT sum(ms.sumExcept) as sum, sum(ms.sumDiscount) sumDiscount, sum(ms.sumRebate) sumRebate, sum(ms.cash) cash, sum(ms.Credit) Credit, sum(ms.returnChange) as returnChange, sum(ms.Refund) Refund, sum(ms.itemstotalExcept) as itemstotal, count(ms.sellNo) as consumerTimes FROM ( SELECT *, CASE status WHEN '1' THEN 0 ELSE ms3.sum END as sumExcept, CASE status WHEN '1' THEN 0 ELSE itemstotal END as itemstotalExcept FROM hypos_main_sell as ms2 INNER JOIN ( SELECT sellNo, sum FROM hypos_mainsell_log GROUP BY sellNo ORDER BY changeDate desc ) as ms3 on ms2.sellNo = ms3.sellNo ) as ms LEFT JOIN hypos_CUST_RTL as cr on cr.VipNo = ms.memberId ";
			string text5 = "SELECT M.*,T.consumerItems FROM ( SELECT ms.memberId, cr.Name, sum(ms.sumExcept) as sum, sum(ms.sumDiscount) sumDiscount, sum(ms.sumRebate) sumRebate, sum(ms.cash) cash, sum(ms.Credit) Credit, sum(ms.returnChange) as returnChange, sum(ms.Refund) Refund, sum(ms.itemstotalExcept) itemstotal, count(ms.sellNo) as consumerTimes FROM ( SELECT *, CASE status WHEN '1' THEN 0 ELSE ms3.sum END as sumExcept, CASE status WHEN '1' THEN 0 ELSE itemstotal END as itemstotalExcept FROM hypos_main_sell as ms4  INNER JOIN ( SELECT sellNo, sum FROM hypos_mainsell_log GROUP BY sellNo ORDER BY changeDate desc ) as ms3 on ms4.sellNo = ms3.sellNo ) as ms LEFT JOIN hypos_CUST_RTL as cr on cr.VipNo = ms.memberId  ";
			string text6 = "";
			text4 = text4 + " WHERE ms.sellTime between {" + num2 + "}  ";
			list.Add(dateTimePicker2.Value.ToString("yyyy-MM-dd"));
			num2++;
			text4 = text4 + "  and datetime(date( {" + num2 + "} ), '+1 days') ";
			list.Add(dateTimePicker3.Value.ToString("yyyy-MM-dd"));
			num2++;
			text5 = text5 + " WHERE ms.sellTime between {" + num + "}  ";
			list2.Add(dateTimePicker2.Value.ToString("yyyy-MM-dd"));
			num++;
			text5 = text5 + "  and datetime(date( {" + num + "} ), '+1 days') ";
			list2.Add(dateTimePicker3.Value.ToString("yyyy-MM-dd"));
			num++;
			for (int i = 0; i < lst_selmember.Count; i++)
			{
				string[] array = lst_selmember[i].Split(',');
				if (i == 0)
				{
					text3 = text3 + "[" + array[1] + "]";
					text6 = text6 + " AND cr.Name IN ( '" + array[1] + "' ";
				}
				else
				{
					text3 = text3 + "[" + array[1] + "]";
					text6 = text6 + " , '" + array[1] + "'";
				}
			}
			if (lst_selmember.Count > 0)
			{
				text6 += ") ";
			}
			if (myCheckBox7.Checked | myCheckBox8.Checked | myCheckBox9.Checked)
			{
				text6 += " AND cr.Type IN (";
				if (myCheckBox7.Checked)
				{
					text += "[一般會員]";
					text6 += " '1' ";
				}
				if (myCheckBox8.Checked)
				{
					text += "[優惠會員(1)]";
					if (myCheckBox7.Checked)
					{
						text6 += ",";
					}
					text6 += " '2' ";
				}
				if (myCheckBox9.Checked)
				{
					text += "[優惠會員(2)]";
					if (myCheckBox7.Checked | myCheckBox8.Checked)
					{
						text6 += ",";
					}
					text6 += " '3' ";
				}
				text6 += ") ";
			}
			else
			{
				text += "[全部]";
			}
			switch (cb_area.SelectedIndex)
			{
			case 0:
				text2 += "[全部]";
				break;
			case 1:
				text2 += "[正常]";
				text6 += " AND cr.Status = '0' ";
				break;
			case 2:
				text2 += "[已停用]";
				text6 += " AND cr.Status = '1' ";
				break;
			}
			text5 += text6;
			text4 += text6;
			text4 += ") as M INNER JOIN (SELECT  count(DISTINCT ds2.barcode) as consumerItems FROM   hypos_detail_sell as ds2 INNER JOIN hypos_main_sell as ms2 ON ds2.sellNo = ms2.sellNo ";
			text4 = text4 + " WHERE ms2.sellTime between {" + num2 + "}  ";
			list.Add(dateTimePicker2.Value.ToString("yyyy-MM-dd"));
			num2++;
			text4 = text4 + "  and datetime(date( {" + num2 + "} ), '+1 days') ";
			list.Add(dateTimePicker3.Value.ToString("yyyy-MM-dd"));
			num2++;
			text4 += " AND ms2.status != '1' )  as T ";
			text5 += " GROUP BY ms.memberId, cr.Name ) as M LEFT JOIN (SELECT ms2.memberId, count(DISTINCT ds2.barcode) as consumerItems FROM   hypos_main_sell as ms2 INNER JOIN hypos_detail_sell as ds2 on ms2.sellNo = ds2.sellNo ";
			text5 = text5 + " WHERE ms2.sellTime between {" + num + "}  ";
			list2.Add(dateTimePicker2.Value.ToString("yyyy-MM-dd"));
			num++;
			text5 = text5 + "  and datetime(date( {" + num + "} ), '+1 days') ";
			list2.Add(dateTimePicker3.Value.ToString("yyyy-MM-dd"));
			num++;
			text5 += " AND ms2.status != '1' GROUP BY ms2.memberId )  as T on T.memberId = M.memberId ";
			if (radioButton6.Checked)
			{
				text5 += " ORDER BY sum DESC";
			}
			else if (radioButton7.Checked)
			{
				text5 += " ORDER BY consumerTimes DESC";
			}
			try
			{
				Close();
				DataTable dt_summary = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text4, list.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				DataTable dt_details = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text5, list2.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				frmSearchMemberSalesSummary frmSearchMemberSalesSummary = new frmSearchMemberSalesSummary(dt_summary, dt_details, dateTimePicker2.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"), text3, text, text2);
				frmSearchMemberSalesSummary.Location = new Point(base.Location.X, base.Location.Y);
				frmSearchMemberSalesSummary.Show();
				Hide();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error Message : " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_btn_SearchCommodityTradingSummary_Click(object sender, EventArgs e)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			int num = 0;
			int num2 = 0;
			string text4 = "SELECT sum(dss.total) as total, sum(dss.num) as num, sum(dss.returnNum) as returnNum, sum(dss.consumptionTimes) as consumptionTimes FROM ( SELECT ds1.barcode as barcode, ds1.total as total, ds1.num as num, ds2.diffNum as returnNum, ds1.consumptionTimes as consumptionTimes FROM ( SELECT ds1.barcode as barcode, sum(ds1.totalExcept) as total, sum(ds1.num) as num, count(DISTINCT ds1.sellNo) as consumptionTimes FROM ( SELECT ds.* , ms.sellTime, ms.status, CASE ms.status WHEN '1' THEN 0 ELSE ds.total END as totalExcept FROM hypos_detail_sell as ds INNER JOIN hypos_main_sell as ms on ds.sellNo = ms.sellNo ";
			string text5 = "select data2.barcode as barcode, gl.GDName as GDName, gl.CName as CName, gl.formCode as formCode, gl.contents as contents, gl.brandName as brandName, gl.spec as spec, gl.capacity as capacity, data2.total as total, data2.num as num, diffNumdata.diffNum as returnNum, data2.consumptionTimes as consumptionTimes from (select data.barcode as barcode, sum(data.num) as num, sum(data.total) as total, count(data.barcode) as consumptionTimes from(select ds.sellNo, ds.barcode, CASE ms.status WHEN '1' THEN 0 ELSE ds.num END as num, ms.sellTime, CASE ms.status WHEN '1' THEN 0 ELSE ds.total END as total from hypos_detail_sell as ds left join hypos_main_sell as ms on ds.sellNo = ms.sellNo ";
			string str = "";
			text4 = text4 + " WHERE ms.sellTime between {" + num2 + "}  ";
			list.Add(dateTimePicker4.Value.ToString("yyyy-MM-dd"));
			num2++;
			text4 = text4 + "  and datetime(date( {" + num2 + "} ), '+1 days') ";
			list.Add(dateTimePicker5.Value.ToString("yyyy-MM-dd"));
			num2++;
			text4 += " ) as ds1 LEFT JOIN hypos_GOODSLST as gl on ds1.barcode = gl.barcode";
			text5 = text5 + " WHERE ms.sellTime between {" + num + "}  ";
			list2.Add(dateTimePicker4.Value.ToString("yyyy-MM-dd"));
			num++;
			text5 = text5 + "  and datetime(date( {" + num + "} ), '+1 days') ";
			list2.Add(dateTimePicker5.Value.ToString("yyyy-MM-dd"));
			num++;
			text5 += " ) as data GROUP BY data.barcode ) as data2 left JOIN ( select ds.barcode, sum(dslog.diffNum) as diffNum from hypos_detail_sell as ds left join hypos_main_sell as ms on ds.sellNo = ms.sellNo left join hypos_detailsell_log as dslog on dslog.sellDetailId = ds.sellDeatialId ";
			text5 = text5 + " WHERE ms.sellTime between {" + num + "}  ";
			list2.Add(dateTimePicker4.Value.ToString("yyyy-MM-dd"));
			num++;
			text5 = text5 + "  and datetime(date( {" + num + "} ), '+1 days') ";
			list2.Add(dateTimePicker5.Value.ToString("yyyy-MM-dd"));
			num++;
			text5 += " group by ds.barcode ) as diffNumdata on diffNumdata.barcode = data2.barcode left JOIN hypos_GOODSLST as gl on data2.barcode = gl.GDSNO ";
			str += " WHERE 1=1 ";
			if (lst_commodity.Count > 0)
			{
				str += " AND ";
			}
			for (int i = 0; i < lst_commodity.Count; i++)
			{
				string[] array = lst_commodity[i].Split(',');
				str = ((i != 0) ? (str + " , '" + array[0] + "'") : (str + " data2.barcode IN ( '" + array[0] + "' "));
			}
			if (lst_commodity.Count > 0)
			{
				str += ") ";
			}
			if (myCheckBox12.Checked | myCheckBox13.Checked)
			{
				str += " AND ";
				if (myCheckBox12.Checked && myCheckBox13.Checked)
				{
					text += "[全部]";
					str += " gl.ISWS IN ( 'Y' , 'N' ) ";
				}
				else if (myCheckBox12.Checked)
				{
					text += "[介接]";
					str += " gl.ISWS IN ( 'Y' ) ";
				}
				else if (myCheckBox13.Checked)
				{
					text += "[自建]";
					str += " gl.ISWS IN ( 'N' ) ";
				}
			}
			if (myCheckBox14.Checked | myCheckBox15.Checked | myCheckBox16.Checked | myCheckBox17.Checked)
			{
				str += " AND gl.CLA1NO IN ( ";
				if (myCheckBox14.Checked)
				{
					text2 += "[農藥]";
					str += " '0302' ";
				}
				if (myCheckBox15.Checked)
				{
					text2 += "[肥料]";
					if (myCheckBox14.Checked)
					{
						str += " , ";
					}
					str += " '0303' ";
				}
				if (myCheckBox16.Checked)
				{
					text2 += "[資材]";
					if (myCheckBox14.Checked || myCheckBox15.Checked)
					{
						str += " , ";
					}
					str += " '0305' ";
				}
				if (myCheckBox17.Checked)
				{
					text2 += "[其他]";
					if (myCheckBox14.Checked || myCheckBox15.Checked || myCheckBox16.Checked)
					{
						str += " , ";
					}
					str += " '0308' ";
				}
				str += " ) ";
			}
			else
			{
				text2 += "[全部]";
			}
			if (myCheckBox18.Checked | myCheckBox19.Checked | myCheckBox20.Checked)
			{
				str += " AND gl.status IN ( ";
				if (myCheckBox18.Checked)
				{
					text3 += "[使用中]";
					str += " 'U' ";
				}
				if (myCheckBox19.Checked)
				{
					text3 += "[未使用]";
					if (myCheckBox18.Checked)
					{
						str += " , ";
					}
					str += " 'N' ";
				}
				if (myCheckBox20.Checked)
				{
					text3 += "[停用]";
					if (myCheckBox18.Checked || myCheckBox19.Checked)
					{
						str += " , ";
					}
					str += " 'S' ";
				}
				str += " ) ";
			}
			else
			{
				text3 += "[全部]";
			}
			text4 += str;
			text4 += " GROUP BY ds1.barcode ) as ds1 LEFT JOIN ( SELECT dslog.barcode as barcode, sum(dslog.diffNum) as diffNum FROM hypos_detailsell_log as dslog GROUP BY dslog.barcode ) as ds2 on ds1.barcode = ds2.barcode ) as dss ";
			text5 += str;
			if (radioButton8.Checked)
			{
				text5 += " ORDER BY total desc ";
			}
			else if (radioButton9.Checked)
			{
				text5 += " ORDER BY consumptionTimes desc ";
			}
			try
			{
				Close();
				DataTable dt_summary = new DataTable();
				DataTable dt_details = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text5, list2.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				frmSearchCommodityTradingSummary frmSearchCommodityTradingSummary = new frmSearchCommodityTradingSummary(dt_summary, dt_details, dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker5.Value.ToString("yyyy-MM-dd"), text, text2, text3);
				frmSearchCommodityTradingSummary.Location = new Point(base.Location.X, base.Location.Y);
				frmSearchCommodityTradingSummary.Show();
				Hide();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error Message : " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_SelectMember_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel8.Controls.Count > 1)
			{
				for (int num = flowLayoutPanel8.Controls.Count - 1; num > 0; num--)
				{
					flowLayoutPanel8.Controls.RemoveAt(num);
				}
			}
			lst_selTEMP.Clear();
			new dialogChooseMember(lst_selTEMP).ShowDialog(this);
			lst_selmember.AddRange(lst_selTEMP);
			Enumerable.Distinct(lst_selmember);
			if (lst_selmember.Count > 0)
			{
				for (int i = 0; i < lst_selmember.Count; i++)
				{
					ucShowString ucShowString = new ucShowString(flowLayoutPanel8, lst_selmember[i]);
					ucShowString.OnClickRemove += new EventHandler(MemberRemove);
					flowLayoutPanel8.Controls.Add(ucShowString);
				}
			}
		}

		public void MemberRemove(object Name, EventArgs s)
		{
			string text = Name as string;
			if (text != null)
			{
				lst_selmember.Remove(text);
			}
		}

		private void btn_SelectCommodity_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel14.Controls.Count > 1)
			{
				for (int num = flowLayoutPanel14.Controls.Count - 1; num > 0; num--)
				{
					flowLayoutPanel14.Controls.RemoveAt(num);
				}
			}
			lst_selTEMP.Clear();
			new dialogChooseCommodity(lst_selTEMP).ShowDialog(this);
			lst_commodity.AddRange(lst_selTEMP);
			Enumerable.Distinct(lst_commodity);
			if (lst_commodity.Count > 0)
			{
				for (int i = 0; i < lst_commodity.Count; i++)
				{
					ucShowString ucShowString = new ucShowString(flowLayoutPanel14, lst_commodity[i]);
					ucShowString.OnClickRemove += new EventHandler(CommodityRemove);
					flowLayoutPanel14.Controls.Add(ucShowString);
				}
			}
		}

		public void CommodityRemove(object Name, EventArgs s)
		{
			string text = Name as string;
			if (text != null)
			{
				lst_commodity.Remove(text);
			}
		}

		private void btn_ExportTodayReport_Click(object sender, EventArgs e)
		{
			try
			{
				if (dt_details1.Rows.Count > 0)
				{
					FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
					if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
					{
						return;
					}
					string selectedPath = folderBrowserDialog.SelectedPath;
					string text = selectedPath + str_file_location + str_file_name + l_nowDate.Text.Replace("-", "") + str_file_type;
					FileInfo file = new FileInfo(text);
					if (!File.Exists(text))
					{
						wb = HSSFWorkbook.Create(InternalWorkbook.CreateWorkbook());
						sh = (HSSFSheet)wb.CreateSheet("Sheet1");
						for (int i = 0; i < report_daily_rowcount + 1; i++)
						{
							IRow row = sh.CreateRow(i);
							for (int j = 0; j < 14; j++)
							{
								row.CreateCell(j);
							}
						}
						using (FileStream @out = new FileStream(text, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8, FileOptions.None))
						{
							wb.Write(@out);
						}
					}
					if (!IsFileLocked(file))
					{
						using (FileStream s = new FileStream(text, FileMode.Open, FileAccess.Read))
						{
							wb = new HSSFWorkbook(s);
							for (int k = 0; k < wb.Count; k++)
							{
								lst_Sheet.Add(wb.GetSheetAt(k).SheetName);
							}
						}
					}
					if (!IsFileLocked(file))
					{
						sh = (HSSFSheet)wb.GetSheet(lst_Sheet[0]);
						int l = 0;
						int num = 1;
						string[] array = new string[2]
						{
							"日期:",
							l_nowDate.Text
						};
						string[] array2 = new string[14]
						{
							"排序",
							"銷售單號",
							"購買會員",
							"銷售單總價",
							"原始總價",
							"現金(原始)",
							"賒帳(原始)",
							"數量",
							"狀態",
							"品項編號",
							"商品條碼",
							"商品名稱",
							"規格",
							"數量"
						};
						num = ((array.Length > array2.Length) ? array.Length : array2.Length);
						for (; l < 2; l++)
						{
							for (int m = 0; m < num; m++)
							{
								if (sh.GetRow(l).GetCell(m) == null)
								{
									sh.GetRow(l).CreateCell(m);
								}
								if (l == 0 && m < array.Length)
								{
									sh.GetRow(l).GetCell(m).SetCellValue(array[m]);
								}
								if (l == 1 && m < array2.Length)
								{
									sh.GetRow(l).GetCell(m).SetCellValue(array2[m]);
								}
							}
						}
						int num2 = 0;
						int result = 0;
						int num3 = 1;
						int num4 = 0;
						int num5 = 0;
						int num6 = dt_details1.Rows.Count;
						for (int n = l; n < report_daily_rowcount + l; n++)
						{
							if (sh.GetRow(n) == null)
							{
								sh.CreateRow(n);
							}
							for (int num7 = 0; num7 < num; num7++)
							{
								if (sh.GetRow(n).GetCell(num7) == null)
								{
									sh.GetRow(n).CreateCell(num7);
								}
							}
							if (result == 0)
							{
								int.TryParse(dt_details1.Rows[num4]["sellNoCount"].ToString(), out result);
								sh.GetRow(n).GetCell(num2).SetCellValue(num6);
								num6--;
								sh.GetRow(n).GetCell(num2 + 1).SetCellValue(dt_details1.Rows[num4]["sellNo"].ToString());
								sh.GetRow(n).GetCell(num2 + 2).SetCellValue(string.IsNullOrEmpty(dt_details1.Rows[num4]["Name"].ToString()) ? "非會員" : dt_details1.Rows[num4]["Name"].ToString());
								sh.GetRow(n).GetCell(num2 + 3).SetCellValue(dt_details1.Rows[num4]["Sum"].ToString());
								int result2 = 0;
								int result3 = 0;
								int.TryParse(dt_details1.Rows[num4]["cash"].ToString(), out result2);
								int.TryParse(dt_details1.Rows[num4]["Credit"].ToString(), out result3);
								sh.GetRow(n).GetCell(num2 + 4).SetCellValue((result2 + result3).ToString());
								sh.GetRow(n).GetCell(num2 + 5).SetCellValue(result2.ToString());
								sh.GetRow(n).GetCell(num2 + 6).SetCellValue(result3.ToString());
								sh.GetRow(n).GetCell(num2 + 7).SetCellValue(dt_details1.Rows[num4]["itemstotal"].ToString());
								string cellValue = "";
								switch (string.IsNullOrEmpty(dt_details1.Rows[num4]["status"].ToString()) ? "0" : dt_details1.Rows[num4]["status"].ToString())
								{
								case "0":
									cellValue = "正常";
									break;
								case "1":
									cellValue = "取消";
									break;
								case "2":
									cellValue = "變更";
									break;
								}
								sh.GetRow(n).GetCell(num2 + 8).SetCellValue(cellValue);
							}
							if (result > 0)
							{
								sh.GetRow(n).GetCell(num2 + 9).SetCellValue(num3);
								sh.GetRow(n).GetCell(num2 + 10).SetCellValue(dt_details2.Rows[num5]["barcode"].ToString());
								sh.GetRow(n).GetCell(num2 + 11).SetCellValue(dt_details2.Rows[num5]["GDName"].ToString() + "[" + dt_details2.Rows[num5]["CName"].ToString() + "-" + dt_details2.Rows[num5]["formCode"].ToString() + "．" + dt_details2.Rows[num5]["contents"].ToString() + "-" + dt_details2.Rows[num5]["brandName"].ToString() + "]");
								sh.GetRow(n).GetCell(num2 + 12).SetCellValue(dt_details2.Rows[num5]["spec"].ToString() + dt_details2.Rows[num5]["capacity"].ToString());
								sh.GetRow(n).GetCell(num2 + 13).SetCellValue(dt_details2.Rows[num5]["num"].ToString());
								num3++;
								num5++;
								result--;
								if (result == 0)
								{
									num3 = 1;
									num4++;
								}
							}
						}
						using (FileStream out2 = new FileStream(text, FileMode.Open, FileAccess.Write))
						{
							wb.Write(out2);
							AutoClosingMessageBox.Show("匯出報表於" + text);
						}
					}
					else
					{
						AutoClosingMessageBox.Show(text + "檔案使用中，請確認檔案是在未開啟的狀態下");
					}
					return;
				}
				MessageBox.Show("本日無銷售紀錄");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private bool IsFileLocked(FileInfo file)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
			}
			catch (IOException)
			{
				return true;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return false;
		}

		private void dgv_SynchronizeList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex <= -1)
			{
				return;
			}
			int index = dgv_SynchronizeList.Columns["Column3"].Index;
			int index2 = dgv_SynchronizeList.Columns["Column4"].Index;
			int index3 = 0;
			if (e.ColumnIndex == index2)
			{
				if (dgv_SynchronizeList.Rows[e.RowIndex].Cells[index].Value.ToString() == "銷售紀錄檔上傳")
				{
					new dialogSyncSellDetailLog(dgv_SynchronizeList.Rows[e.RowIndex].Cells[index3].Value.ToString()).ShowDialog(this);
				}
				if (dgv_SynchronizeList.Rows[e.RowIndex].Cells[index].Value.ToString() == "庫存調整紀錄檔上傳")
				{
					new dialogSyncInventoryDetailLog(dgv_SynchronizeList.Rows[e.RowIndex].Cells[index3].Value.ToString()).ShowDialog(this);
				}
				if (dgv_SynchronizeList.Rows[e.RowIndex].Cells[index].Value.ToString() == "出貨紀錄檔上傳")
				{
					new dialogSyncShipDetailLog(dgv_SynchronizeList.Rows[e.RowIndex].Cells[index3].Value.ToString()).ShowDialog(this);
				}
			}
		}

		private void btn_reset_Click(object sender, EventArgs e)
		{
			myCheckBox1.Checked = false;
			myCheckBox2.Checked = false;
			myCheckBox3.Checked = false;
			myCheckBox4.Checked = false;
			myCheckBox5.Checked = false;
			myCheckBox6.Checked = false;
			numericUpDown1.Value = 0m;
			numericUpDown2.Value = 99999m;
			radioButton1.Checked = false;
			radioButton2.Checked = false;
			radioButton3.Checked = true;
			radioButton4.Checked = false;
			radioButton5.Checked = false;
		}

		private void btn_reset_member_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel8.Controls.Count > 1)
			{
				for (int num = flowLayoutPanel8.Controls.Count - 1; num > 0; num--)
				{
					flowLayoutPanel8.Controls.RemoveAt(num);
				}
			}
			lst_selTEMP.Clear();
			myCheckBox7.Checked = false;
			myCheckBox8.Checked = false;
			myCheckBox9.Checked = false;
			myCheckBox10.Checked = false;
			myCheckBox11.Checked = false;
			cb_area.SelectedIndex = 0;
			radioButton6.Checked = false;
			radioButton7.Checked = false;
		}

		private void btn_reset_Commodity_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel14.Controls.Count > 1)
			{
				for (int num = flowLayoutPanel14.Controls.Count - 1; num > 0; num--)
				{
					flowLayoutPanel14.Controls.RemoveAt(num);
				}
			}
			lst_selTEMP.Clear();
			myCheckBox12.Checked = false;
			myCheckBox13.Checked = false;
			myCheckBox14.Checked = false;
			myCheckBox15.Checked = false;
			myCheckBox16.Checked = false;
			myCheckBox17.Checked = false;
			myCheckBox18.Checked = false;
			myCheckBox19.Checked = false;
			myCheckBox20.Checked = false;
			radioButton8.Checked = false;
			radioButton9.Checked = false;
		}

		private void btn_ExportTodayDeliveryReport_Click(object sender, EventArgs e)
		{
			try
			{
				string sql = "SELECT DeliveryNo, vendorNo, CurSum, OriSum, items, itemstotal, status FROM hypos_DeliveryGoods_Master where CreateDate between datetime(date('now'), '-1 seconds') and datetime(date('now'), '+1 days') order by CreateDate desc";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					List<string> list = new List<string>();
					FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
					if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
					{
						return;
					}
					string selectedPath = folderBrowserDialog.SelectedPath;
					string text = selectedPath + str_file_location + "report_DeliveryDaily_" + l_nowDate.Text.Replace("-", "") + str_file_type;
					FileInfo file = new FileInfo(text);
					if (!File.Exists(text))
					{
						wb1 = HSSFWorkbook.Create(InternalWorkbook.CreateWorkbook());
						sh1 = (HSSFSheet)wb1.CreateSheet("Sheet1");
						for (int i = 0; i < report_DeliveryDaily_rowcount + 1; i++)
						{
							IRow row = sh1.CreateRow(i);
							for (int j = 0; j < 15; j++)
							{
								row.CreateCell(j);
							}
						}
						using (FileStream @out = new FileStream(text, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8, FileOptions.None))
						{
							wb1.Write(@out);
						}
					}
					if (!IsFileLocked(file))
					{
						using (FileStream s = new FileStream(text, FileMode.Open, FileAccess.Read))
						{
							wb1 = new HSSFWorkbook(s);
							for (int k = 0; k < wb1.Count; k++)
							{
								list.Add(wb1.GetSheetAt(k).SheetName);
							}
						}
					}
					if (!IsFileLocked(file))
					{
						sh1 = (HSSFSheet)wb1.GetSheet(list[0]);
						int l = 0;
						int num = 1;
						string[] array = new string[2]
						{
							"日期:",
							l_nowDate.Text
						};
						string[] array2 = new string[15]
						{
							"排序",
							"出貨單號",
							"業者",
							"出貨總額",
							"原始總額",
							"品項",
							"數量",
							"狀態",
							"品項編號",
							"商品條碼",
							"商品名稱",
							"規格",
							"數量",
							"批號",
							"製造日期"
						};
						num = ((array.Length > array2.Length) ? array.Length : array2.Length);
						for (; l < 2; l++)
						{
							for (int m = 0; m < num; m++)
							{
								if (sh1.GetRow(l).GetCell(m) == null)
								{
									sh1.GetRow(l).CreateCell(m);
								}
								if (l == 0 && m < array.Length)
								{
									sh1.GetRow(l).GetCell(m).SetCellValue(array[m]);
								}
								if (l == 1 && m < array2.Length)
								{
									sh1.GetRow(l).GetCell(m).SetCellValue(array2[m]);
								}
							}
						}
						for (int n = l; n < report_DeliveryDaily_rowcount + l; n++)
						{
							if (sh1.GetRow(n) == null)
							{
								sh1.CreateRow(n);
							}
							for (int num2 = 0; num2 < num; num2++)
							{
								if (sh1.GetRow(n).GetCell(num2) == null)
								{
									sh1.GetRow(n).CreateCell(num2);
								}
							}
						}
						int num3 = 2;
						int num4 = dataTable.Rows.Count;
						int num5 = 1;
						for (int num6 = 0; num6 < dataTable.Rows.Count; num6++)
						{
							string text2 = string.IsNullOrEmpty(dataTable.Rows[num6]["DeliveryNo"].ToString()) ? "N/A" : dataTable.Rows[num6]["DeliveryNo"].ToString();
							string cellValue = string.IsNullOrEmpty(dataTable.Rows[num6]["CurSum"].ToString()) ? "N/A" : dataTable.Rows[num6]["CurSum"].ToString();
							string cellValue2 = string.IsNullOrEmpty(dataTable.Rows[num6]["OriSum"].ToString()) ? "N/A" : dataTable.Rows[num6]["OriSum"].ToString();
							string cellValue3 = string.IsNullOrEmpty(dataTable.Rows[num6]["items"].ToString()) ? "N/A" : dataTable.Rows[num6]["items"].ToString();
							string cellValue4 = string.IsNullOrEmpty(dataTable.Rows[num6]["itemstotal"].ToString()) ? "N/A" : dataTable.Rows[num6]["itemstotal"].ToString();
							string text3 = "";
							string text4 = "N/A";
							string text5 = "N/A";
							string text6 = "N/A";
							string sql2 = "SELECT SupplierName, vendorId, vendorName FROM hypos_Supplier where SupplierNo = '" + dataTable.Rows[num6]["vendorNo"].ToString() + "'";
							DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteReaderReturnDataTable);
							if (dataTable2.Rows.Count > 0)
							{
								text4 = (string.IsNullOrEmpty(dataTable2.Rows[0]["SupplierName"].ToString()) ? "N/A" : dataTable2.Rows[0]["SupplierName"].ToString());
								text5 = (string.IsNullOrEmpty(dataTable2.Rows[0]["vendorId"].ToString()) ? "N/A" : dataTable2.Rows[0]["vendorId"].ToString());
								text6 = (string.IsNullOrEmpty(dataTable2.Rows[0]["vendorName"].ToString()) ? "N/A" : dataTable2.Rows[0]["vendorName"].ToString());
							}
							switch (string.IsNullOrEmpty(dataTable.Rows[num6]["status"].ToString()) ? "N/A" : dataTable.Rows[num6]["status"].ToString())
							{
							case "0":
								text3 = "正常";
								break;
							case "1":
								text3 = "取消";
								break;
							case "2":
								text3 = "變更(編修)";
								break;
							default:
								text3 = "N/A";
								break;
							}
							sh1.GetRow(num3).GetCell(0).SetCellValue(num4.ToString());
							num4--;
							sh1.GetRow(num3).GetCell(1).SetCellValue(text2);
							sh1.GetRow(num3).GetCell(2).SetCellValue(text4 + "(" + text5 + "/" + text6 + ")");
							sh1.GetRow(num3).GetCell(3).SetCellValue(cellValue);
							sh1.GetRow(num3).GetCell(4).SetCellValue(cellValue2);
							sh1.GetRow(num3).GetCell(5).SetCellValue(cellValue3);
							sh1.GetRow(num3).GetCell(6).SetCellValue(cellValue4);
							sh1.GetRow(num3).GetCell(7).SetCellValue(text3);
							string sql3 = "SELECT barcode,num,BatchNo,MFGDate FROM hypos_DeliveryGoods_Detail where DeliveryNo = '" + text2 + "'";
							DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql3, null, CommandOperationType.ExecuteReaderReturnDataTable);
							int count = dataTable3.Rows.Count;
							for (int num7 = 0; num7 < dataTable3.Rows.Count; num7++)
							{
								string text7 = string.IsNullOrEmpty(dataTable3.Rows[num7]["barcode"].ToString()) ? "N/A" : dataTable3.Rows[num7]["barcode"].ToString();
								string sql4 = "SELECT GDName,CName,formCode,contents,brandName,spec,capacity FROM hypos_GOODSLST where GDSNO = '" + text7 + "'";
								DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql4, null, CommandOperationType.ExecuteReaderReturnDataTable);
								string cellValue5 = dataTable4.Rows[0]["GDName"].ToString() + "[" + dataTable4.Rows[0]["CName"].ToString() + "-" + dataTable4.Rows[0]["formCode"].ToString() + "．" + dataTable4.Rows[0]["contents"].ToString() + "-" + dataTable4.Rows[0]["brandName"].ToString() + "]";
								string cellValue6 = dataTable4.Rows[0]["spec"].ToString() + dataTable4.Rows[0]["capacity"].ToString();
								string cellValue7 = string.IsNullOrEmpty(dataTable3.Rows[num7]["BatchNo"].ToString()) ? "無設定批號" : dataTable3.Rows[num7]["BatchNo"].ToString();
								string cellValue8 = string.IsNullOrEmpty(dataTable3.Rows[num7]["MFGDate"].ToString()) ? "無設定製造日期" : dataTable3.Rows[num7]["MFGDate"].ToString();
								string cellValue9 = string.IsNullOrEmpty(dataTable3.Rows[num7]["num"].ToString()) ? "N/A" : dataTable3.Rows[num7]["num"].ToString();
								if (num7 == 0)
								{
									num5 = 1;
								}
								sh1.GetRow(num3).GetCell(8).SetCellValue(num5.ToString());
								num5++;
								sh1.GetRow(num3).GetCell(9).SetCellValue(text7);
								sh1.GetRow(num3).GetCell(10).SetCellValue(cellValue5);
								sh1.GetRow(num3).GetCell(11).SetCellValue(cellValue6);
								sh1.GetRow(num3).GetCell(12).SetCellValue(cellValue9);
								sh1.GetRow(num3).GetCell(13).SetCellValue(cellValue7);
								sh1.GetRow(num3).GetCell(14).SetCellValue(cellValue8);
								num3++;
							}
						}
						using (FileStream out2 = new FileStream(text, FileMode.Open, FileAccess.Write))
						{
							wb1.Write(out2);
							AutoClosingMessageBox.Show("匯出報表於" + text);
						}
					}
					else
					{
						AutoClosingMessageBox.Show(text + "檔案使用中，請確認檔案是在未開啟的狀態下");
					}
					return;
				}
				MessageBox.Show("本日無出貨紀錄");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_SearchGoodsDelivery_Click(object sender, EventArgs e)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			List<string> list = new List<string>();
			int num = 0;
			string text4 = "select data.*, gl.GDName as GDName, gl.CName as CName, gl.formCode as formCode, gl.contents as contents, gl.brandName as brandName, gl.spec as spec, gl.capacity as capacity from ( SELECT dgd.barcode as barcode, sum(num) as num, sum(subtotal) as total FROM hypos_DeliveryGoods_Detail as dgd left join hypos_DeliveryGoods_Master as dgm on dgd.DeliveryNo = dgm.DeliveryNo where dgm.status <> 1 ";
			string str = "";
			text4 = text4 + " and dgd.DeliveryDate between {" + num + "}  ";
			list.Add(dateTimePicker6.Value.ToString("yyyy-MM-dd"));
			num++;
			text4 = text4 + "  and datetime(date( {" + num + "} ), '+1 days') ";
			list.Add(dateTimePicker7.Value.ToString("yyyy-MM-dd"));
			num++;
			text4 += " group by dgd.barcode ) as data left JOIN hypos_GOODSLST as gl on data.barcode = gl.GDSNO ";
			str += " WHERE 1=1 ";
			if (lst_commodity2.Count > 0)
			{
				str += " AND ";
			}
			for (int i = 0; i < lst_commodity2.Count; i++)
			{
				string[] array = lst_commodity2[i].Split(',');
				str = ((i != 0) ? (str + " , '" + array[0] + "'") : (str + " data.barcode IN ( '" + array[0] + "' "));
			}
			if (lst_commodity2.Count > 0)
			{
				str += ") ";
			}
			if (myCheckBox21.Checked | myCheckBox22.Checked)
			{
				str += " AND ";
				if (myCheckBox21.Checked && myCheckBox22.Checked)
				{
					text += "[全部]";
					str += " gl.ISWS IN ( 'Y' , 'N' ) ";
				}
				else if (myCheckBox21.Checked)
				{
					text += "[介接]";
					str += " gl.ISWS IN ( 'Y' ) ";
				}
				else if (myCheckBox22.Checked)
				{
					text += "[自建]";
					str += " gl.ISWS IN ( 'N' ) ";
				}
			}
			if (myCheckBox23.Checked | myCheckBox24.Checked | myCheckBox25.Checked | myCheckBox26.Checked)
			{
				str += " AND gl.CLA1NO IN ( ";
				if (myCheckBox23.Checked)
				{
					text2 += "[農藥]";
					str += " '0302' ";
				}
				if (myCheckBox24.Checked)
				{
					text2 += "[肥料]";
					if (myCheckBox23.Checked)
					{
						str += " , ";
					}
					str += " '0303' ";
				}
				if (myCheckBox25.Checked)
				{
					text2 += "[資材]";
					if (myCheckBox23.Checked || myCheckBox24.Checked)
					{
						str += " , ";
					}
					str += " '0305' ";
				}
				if (myCheckBox26.Checked)
				{
					text2 += "[其他]";
					if (myCheckBox23.Checked || myCheckBox24.Checked || myCheckBox25.Checked)
					{
						str += " , ";
					}
					str += " '0308' ";
				}
				str += " ) ";
			}
			else
			{
				text2 += "[全部]";
			}
			if (myCheckBox27.Checked | myCheckBox28.Checked | myCheckBox29.Checked)
			{
				str += " AND gl.status IN ( ";
				if (myCheckBox27.Checked)
				{
					text3 += "[使用中]";
					str += " 'U' ";
				}
				if (myCheckBox28.Checked)
				{
					text3 += "[未使用]";
					if (myCheckBox27.Checked)
					{
						str += " , ";
					}
					str += " 'N' ";
				}
				if (myCheckBox29.Checked)
				{
					text3 += "[停用]";
					if (myCheckBox27.Checked || myCheckBox28.Checked)
					{
						str += " , ";
					}
					str += " 'S' ";
				}
				str += " ) ";
			}
			else
			{
				text3 += "[全部]";
			}
			text4 += str;
			if (radioButton10.Checked)
			{
				text4 += " order by num desc ";
			}
			else if (radioButton11.Checked)
			{
				text4 += " order by total desc ";
			}
			try
			{
				DataTable dt_summary = new DataTable();
				DataTable dt_details = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text4, list.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				frmSearchCommodityDeliverySummary frmSearchCommodityDeliverySummary = new frmSearchCommodityDeliverySummary(dt_summary, dt_details, dateTimePicker6.Value.ToString("yyyy-MM-dd"), dateTimePicker7.Value.ToString("yyyy-MM-dd"), text, text2, text3);
				frmSearchCommodityDeliverySummary.Location = new Point(base.Location.X, base.Location.Y);
				frmSearchCommodityDeliverySummary.Show();
				Hide();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error Message : " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_SelectCommodity2_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel21.Controls.Count > 1)
			{
				for (int num = flowLayoutPanel21.Controls.Count - 1; num > 0; num--)
				{
					flowLayoutPanel21.Controls.RemoveAt(num);
				}
			}
			lst_selTEMP.Clear();
			new dialogChooseCommodity(lst_selTEMP).ShowDialog(this);
			lst_commodity2.AddRange(lst_selTEMP);
			lst_commodity2 = Enumerable.ToList(Enumerable.Distinct(lst_commodity2));
			if (lst_commodity2.Count > 0)
			{
				for (int i = 0; i < lst_commodity2.Count; i++)
				{
					ucShowString ucShowString = new ucShowString(flowLayoutPanel21, lst_commodity2[i]);
					ucShowString.OnClickRemove += new EventHandler(CommodityRemove2);
					flowLayoutPanel21.Controls.Add(ucShowString);
				}
			}
		}

		public void CommodityRemove2(object Name, EventArgs s)
		{
			string text = Name as string;
			if (text != null)
			{
				lst_commodity2.Remove(text);
			}
		}

		private void btn_CommDelReset_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel21.Controls.Count > 1)
			{
				for (int num = flowLayoutPanel21.Controls.Count - 1; num > 0; num--)
				{
					flowLayoutPanel21.Controls.RemoveAt(num);
				}
			}
			lst_commodity2.Clear();
			lst_selTEMP.Clear();
			myCheckBox21.Checked = true;
			myCheckBox22.Checked = true;
			myCheckBox23.Checked = true;
			myCheckBox24.Checked = true;
			myCheckBox25.Checked = true;
			myCheckBox26.Checked = true;
			myCheckBox27.Checked = true;
			myCheckBox28.Checked = false;
			myCheckBox29.Checked = false;
			radioButton10.Checked = true;
			radioButton11.Checked = false;
		}

		private void btn_SearchVendorDelivery_Click(object sender, EventArgs e)
		{
			new List<string>();
			List<string> list = new List<string>();
			int num = 0;
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "SELECT t1.vendorNo,t1.OriSum as OriSum,t1.CurSum as CurSum,t1.itemstotal as itemstotal,t1.DeliveryCount as DeliveryCount,t1.SupplierName,t1.vendorId,t1.vendorName,t2.items from  ( SELECT dgm.vendorNo as vendorNo, sum(OriSum) as OriSum, sum(CurSum) as CurSum, sum(itemstotal) as itemstotal, count(DeliveryNo) as DeliveryCount, SupplierName, vendorId, vendorName FROM [hypos_DeliveryGoods_Master] as dgm left join hypos_Supplier as sl on sl.SupplierNo = dgm.vendorNo where 1 = 1 ";
			string text5 = "";
			text4 = text4 + " and dgm.DeliveryDate between {" + num + "}  ";
			list.Add(dateTimePicker8.Value.ToString("yyyy-MM-dd"));
			num++;
			text4 = text4 + "  and datetime(date( {" + num + "} ), '+1 days') ";
			list.Add(dateTimePicker9.Value.ToString("yyyy-MM-dd"));
			num++;
			for (int i = 0; i < lst_selvendor.Count; i++)
			{
				string[] array = lst_selvendor[i].Split(',');
				if (i == 0)
				{
					text3 = text3 + "[" + array[1] + "]";
					text5 = text5 + " AND dgm.vendorNo IN ( '" + array[0] + "' ";
				}
				else
				{
					text3 = text3 + "[" + array[1] + "]";
					text5 = text5 + " , '" + array[0] + "'";
				}
			}
			if (lst_selvendor.Count > 0)
			{
				text5 += ") ";
			}
			if (myCheckBox30.Checked && myCheckBox31.Checked && myCheckBox32.Checked)
			{
				text = "[全部]";
			}
			else if (myCheckBox30.Checked && myCheckBox31.Checked && !myCheckBox32.Checked)
			{
				text5 += " and sl.Type in (0,1) ";
				text = "[本地廠商][進口廠商]";
			}
			else if (myCheckBox30.Checked && !myCheckBox31.Checked && myCheckBox32.Checked)
			{
				text5 += " and ( sl.Type in (0) or (sl.vendorId is not null and sl.vendorName is not null) ) ";
				text = "[本地廠商][農藥廠商]";
			}
			else if (!myCheckBox30.Checked && myCheckBox31.Checked && myCheckBox32.Checked)
			{
				text5 += " and ( sl.Type in (1) or (sl.vendorId is not null and sl.vendorName is not null) ) ";
				text = "[進口廠商][農藥廠商]";
			}
			else if (!myCheckBox30.Checked && !myCheckBox31.Checked && myCheckBox32.Checked)
			{
				text5 += " and sl.vendorId is not null and sl.vendorName is not null ";
				text = "[農藥廠商]";
			}
			else if (!myCheckBox30.Checked && myCheckBox31.Checked && !myCheckBox32.Checked)
			{
				text5 += " and sl.Type in (1) ";
				text = "[進口廠商]";
			}
			else if (myCheckBox30.Checked && !myCheckBox31.Checked && !myCheckBox32.Checked)
			{
				text5 += " and sl.Type in (0) ";
				text = "[本地廠商]";
			}
			else
			{
				text = "[全部]";
			}
			switch (cb_vendorstatus.SelectedIndex)
			{
			case 0:
				text2 += "[全部]";
				break;
			case 1:
				text2 += "[正常]";
				text5 += " AND sl.Status = 0 ";
				break;
			case 2:
				text2 += "[已停用]";
				text5 += " AND sl.Status = 1 ";
				break;
			}
			text4 += text5;
			text4 += " group by dgm.vendorNo ";
			if (radioButton12.Checked)
			{
				text4 += " order by CurSum desc ";
			}
			else if (radioButton13.Checked)
			{
				text4 += " order by DeliveryCount desc ";
			}
			text4 += " ) as t1 left join (  SELECT dgm.vendorNo as vendorNo, COUNT(DISTINCT barcode) as items FROM hypos_DeliveryGoods_Detail as dgd left join hypos_DeliveryGoods_Master as dgm on dgm.DeliveryNo = dgd.DeliveryNo left join hypos_Supplier as sl on sl.SupplierNo = dgm.vendorNo where 1 = 1 ";
			text4 = text4 + " and dgm.DeliveryDate between {" + num + "}  ";
			list.Add(dateTimePicker8.Value.ToString("yyyy-MM-dd"));
			num++;
			text4 = text4 + "  and datetime(date( {" + num + "} ), '+1 days') ";
			list.Add(dateTimePicker9.Value.ToString("yyyy-MM-dd"));
			num++;
			text4 += text5;
			text4 += " group by dgm.vendorNo ) as t2 on t1.vendorNo = t2.vendorNo ";
			try
			{
				DataTable dt_summary = new DataTable();
				DataTable dt_details = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text4, list.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				frmSearchVendorDeliverySummary frmSearchVendorDeliverySummary = new frmSearchVendorDeliverySummary(dt_summary, dt_details, dateTimePicker8.Value.ToString("yyyy-MM-dd"), dateTimePicker9.Value.ToString("yyyy-MM-dd"), text3, text, text2);
				frmSearchVendorDeliverySummary.Location = new Point(base.Location.X, base.Location.Y);
				frmSearchVendorDeliverySummary.Show();
				Hide();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error Message : " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_VendorDelReset_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel27.Controls.Count > 1)
			{
				for (int num = flowLayoutPanel27.Controls.Count - 1; num > 0; num--)
				{
					flowLayoutPanel27.Controls.RemoveAt(num);
				}
			}
			myCheckBox30.Checked = true;
			myCheckBox31.Checked = true;
			myCheckBox32.Checked = false;
			radioButton12.Checked = true;
			radioButton13.Checked = false;
		}

		private void btn_SelectVendor_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel27.Controls.Count > 1)
			{
				for (int num = flowLayoutPanel27.Controls.Count - 1; num > 0; num--)
				{
					flowLayoutPanel27.Controls.RemoveAt(num);
				}
			}
			lst_selTEMP.Clear();
			new dialogChooseVendor(lst_selTEMP).ShowDialog(this);
			lst_selvendor.AddRange(lst_selTEMP);
			lst_selvendor = Enumerable.ToList(Enumerable.Distinct(lst_selvendor));
			if (lst_selvendor.Count > 0)
			{
				for (int i = 0; i < lst_selvendor.Count; i++)
				{
					ucShowString ucShowString = new ucShowString(flowLayoutPanel27, lst_selvendor[i]);
					ucShowString.OnClickRemove += new EventHandler(VendorRemove);
					flowLayoutPanel27.Controls.Add(ucShowString);
				}
			}
		}

		public void VendorRemove(object Name, EventArgs s)
		{
			string text = Name as string;
			if (text != null)
			{
				lst_selvendor.Remove(text);
			}
		}

		private void btn_SalesSelectMember_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel19.Controls.Count > 1)
			{
				for (int num = flowLayoutPanel19.Controls.Count - 1; num > 0; num--)
				{
					flowLayoutPanel19.Controls.RemoveAt(num);
				}
			}
			lst_selTEMP.Clear();
			new dialogChooseMember(lst_selTEMP).ShowDialog(this);
			lst_Salesselmember.AddRange(lst_selTEMP);
			lst_Salesselmember = Enumerable.ToList(Enumerable.Distinct(lst_Salesselmember));
			if (lst_Salesselmember.Count > 0)
			{
				for (int i = 0; i < lst_Salesselmember.Count; i++)
				{
					ucShowString ucShowString = new ucShowString(flowLayoutPanel19, lst_Salesselmember[i]);
					ucShowString.OnClickRemove += new EventHandler(MemberRemove2);
					flowLayoutPanel19.Controls.Add(ucShowString);
				}
			}
		}

		public void MemberRemove2(object Name, EventArgs s)
		{
			string text = Name as string;
			if (text != null)
			{
				lst_Salesselmember.Remove(text);
			}
		}

		private void btn_saleSearch_Click(object sender, EventArgs e)
		{
			try
			{
				showTodaysSalesSummary();
				showTodaysSalesSummaryLog();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error Message : " + ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void dgv_saleDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == 0 && e.RowIndex >= -1)
				{
					MessageBox.Show(dgv_saleDetail[e.ColumnIndex, e.RowIndex].Value.ToString());
				}
			}
			catch (Exception)
			{
			}
		}

		private void FLP_saleDetail_Click(object sender, EventArgs e)
		{
		}

		private void FLP_saleDetail_Paint(object sender, PaintEventArgs e)
		{
		}

		private void FLP_saleDetail_MouseClick(object sender, MouseEventArgs e)
		{
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
			panel2 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			btn_view_store_upload_history = new System.Windows.Forms.Button();
			tabControl = new System.Windows.Forms.TabControl();
			BasicData = new System.Windows.Forms.TabPage();
			btn_saleSearch = new System.Windows.Forms.Button();
			dateTimePicker10 = new System.Windows.Forms.DateTimePicker();
			label38 = new System.Windows.Forms.Label();
			dateTimePicker11 = new System.Windows.Forms.DateTimePicker();
			FLP_saleDetail = new System.Windows.Forms.FlowLayoutPanel();
			dgv_saleDetail = new System.Windows.Forms.DataGridView();
			btn_ExportTodayReport = new System.Windows.Forms.Button();
			dgv_saleDetailTotal = new System.Windows.Forms.DataGridView();
			dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			l_nowDate = new System.Windows.Forms.Label();
			flowLayoutPanel19 = new System.Windows.Forms.FlowLayoutPanel();
			btn_SalesSelectMember = new System.Windows.Forms.Button();
			tabPage1 = new System.Windows.Forms.TabPage();
			btn_reset = new System.Windows.Forms.Button();
			btn_SearchPeriodTransactions = new System.Windows.Forms.Button();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			dateTimePicker0 = new System.Windows.Forms.DateTimePicker();
			label21 = new System.Windows.Forms.Label();
			dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox1 = new POS_Client.MyCheckBox();
			myCheckBox2 = new POS_Client.MyCheckBox();
			myCheckBox3 = new POS_Client.MyCheckBox();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			panel9 = new System.Windows.Forms.Panel();
			label10 = new System.Windows.Forms.Label();
			flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox4 = new POS_Client.MyCheckBox();
			myCheckBox5 = new POS_Client.MyCheckBox();
			myCheckBox6 = new POS_Client.MyCheckBox();
			flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			label22 = new System.Windows.Forms.Label();
			numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
			radioButton1 = new System.Windows.Forms.RadioButton();
			radioButton2 = new System.Windows.Forms.RadioButton();
			radioButton3 = new System.Windows.Forms.RadioButton();
			flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
			radioButton4 = new System.Windows.Forms.RadioButton();
			radioButton5 = new System.Windows.Forms.RadioButton();
			tabPage2 = new System.Windows.Forms.TabPage();
			btn_reset_member = new System.Windows.Forms.Button();
			btn_SearchMemberSalesSummary = new System.Windows.Forms.Button();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
			dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			label5 = new System.Windows.Forms.Label();
			dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
			flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
			btn_SelectMember = new System.Windows.Forms.Button();
			panel8 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			panel10 = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			panel11 = new System.Windows.Forms.Panel();
			label11 = new System.Windows.Forms.Label();
			panel12 = new System.Windows.Forms.Panel();
			label13 = new System.Windows.Forms.Label();
			panel13 = new System.Windows.Forms.Panel();
			label14 = new System.Windows.Forms.Label();
			flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox7 = new POS_Client.MyCheckBox();
			myCheckBox8 = new POS_Client.MyCheckBox();
			myCheckBox9 = new POS_Client.MyCheckBox();
			myCheckBox10 = new POS_Client.MyCheckBox();
			myCheckBox11 = new POS_Client.MyCheckBox();
			flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
			cb_area = new System.Windows.Forms.ComboBox();
			flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
			radioButton6 = new System.Windows.Forms.RadioButton();
			radioButton7 = new System.Windows.Forms.RadioButton();
			tabPage3 = new System.Windows.Forms.TabPage();
			btn_reset_Commodity = new System.Windows.Forms.Button();
			btn_SearchCommodityTradingSummary = new System.Windows.Forms.Button();
			tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			flowLayoutPanel13 = new System.Windows.Forms.FlowLayoutPanel();
			dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
			label23 = new System.Windows.Forms.Label();
			dateTimePicker5 = new System.Windows.Forms.DateTimePicker();
			flowLayoutPanel14 = new System.Windows.Forms.FlowLayoutPanel();
			btn_SelectCommodity = new System.Windows.Forms.Button();
			panel14 = new System.Windows.Forms.Panel();
			label15 = new System.Windows.Forms.Label();
			panel15 = new System.Windows.Forms.Panel();
			label16 = new System.Windows.Forms.Label();
			panel16 = new System.Windows.Forms.Panel();
			label17 = new System.Windows.Forms.Label();
			panel17 = new System.Windows.Forms.Panel();
			label18 = new System.Windows.Forms.Label();
			panel18 = new System.Windows.Forms.Panel();
			label19 = new System.Windows.Forms.Label();
			panel19 = new System.Windows.Forms.Panel();
			label20 = new System.Windows.Forms.Label();
			flowLayoutPanel15 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox12 = new POS_Client.MyCheckBox();
			myCheckBox13 = new POS_Client.MyCheckBox();
			flowLayoutPanel16 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox14 = new POS_Client.MyCheckBox();
			myCheckBox15 = new POS_Client.MyCheckBox();
			myCheckBox16 = new POS_Client.MyCheckBox();
			myCheckBox17 = new POS_Client.MyCheckBox();
			flowLayoutPanel17 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox18 = new POS_Client.MyCheckBox();
			myCheckBox19 = new POS_Client.MyCheckBox();
			myCheckBox20 = new POS_Client.MyCheckBox();
			flowLayoutPanel18 = new System.Windows.Forms.FlowLayoutPanel();
			radioButton8 = new System.Windows.Forms.RadioButton();
			radioButton9 = new System.Windows.Forms.RadioButton();
			tabPage5 = new System.Windows.Forms.TabPage();
			FLP_DeliveryDetail = new System.Windows.Forms.FlowLayoutPanel();
			btn_ExportTodayDeliveryReport = new System.Windows.Forms.Button();
			dgv_DeliveryTotal = new System.Windows.Forms.DataGridView();
			dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			l_nowDate2 = new System.Windows.Forms.Label();
			dgv_DeliveryDetail = new System.Windows.Forms.DataGridView();
			dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			tabPage6 = new System.Windows.Forms.TabPage();
			btn_CommDelReset = new System.Windows.Forms.Button();
			btn_SearchGoodsDelivery = new System.Windows.Forms.Button();
			tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			flowLayoutPanel20 = new System.Windows.Forms.FlowLayoutPanel();
			dateTimePicker6 = new System.Windows.Forms.DateTimePicker();
			label24 = new System.Windows.Forms.Label();
			dateTimePicker7 = new System.Windows.Forms.DateTimePicker();
			flowLayoutPanel21 = new System.Windows.Forms.FlowLayoutPanel();
			btn_SelectCommodity2 = new System.Windows.Forms.Button();
			panel7 = new System.Windows.Forms.Panel();
			label26 = new System.Windows.Forms.Label();
			panel21 = new System.Windows.Forms.Panel();
			label27 = new System.Windows.Forms.Label();
			panel22 = new System.Windows.Forms.Panel();
			label28 = new System.Windows.Forms.Label();
			panel23 = new System.Windows.Forms.Panel();
			label29 = new System.Windows.Forms.Label();
			panel24 = new System.Windows.Forms.Panel();
			label30 = new System.Windows.Forms.Label();
			panel25 = new System.Windows.Forms.Panel();
			label31 = new System.Windows.Forms.Label();
			flowLayoutPanel22 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox21 = new POS_Client.MyCheckBox();
			myCheckBox22 = new POS_Client.MyCheckBox();
			flowLayoutPanel23 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox23 = new POS_Client.MyCheckBox();
			myCheckBox24 = new POS_Client.MyCheckBox();
			myCheckBox25 = new POS_Client.MyCheckBox();
			myCheckBox26 = new POS_Client.MyCheckBox();
			flowLayoutPanel24 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox27 = new POS_Client.MyCheckBox();
			myCheckBox28 = new POS_Client.MyCheckBox();
			myCheckBox29 = new POS_Client.MyCheckBox();
			flowLayoutPanel25 = new System.Windows.Forms.FlowLayoutPanel();
			radioButton10 = new System.Windows.Forms.RadioButton();
			radioButton11 = new System.Windows.Forms.RadioButton();
			tabPage7 = new System.Windows.Forms.TabPage();
			btn_VendorDelReset = new System.Windows.Forms.Button();
			btn_SearchVendorDelivery = new System.Windows.Forms.Button();
			tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			flowLayoutPanel26 = new System.Windows.Forms.FlowLayoutPanel();
			dateTimePicker8 = new System.Windows.Forms.DateTimePicker();
			label32 = new System.Windows.Forms.Label();
			dateTimePicker9 = new System.Windows.Forms.DateTimePicker();
			flowLayoutPanel27 = new System.Windows.Forms.FlowLayoutPanel();
			btn_SelectVendor = new System.Windows.Forms.Button();
			panel26 = new System.Windows.Forms.Panel();
			label33 = new System.Windows.Forms.Label();
			panel27 = new System.Windows.Forms.Panel();
			label34 = new System.Windows.Forms.Label();
			panel28 = new System.Windows.Forms.Panel();
			label35 = new System.Windows.Forms.Label();
			panel29 = new System.Windows.Forms.Panel();
			label36 = new System.Windows.Forms.Label();
			panel30 = new System.Windows.Forms.Panel();
			label37 = new System.Windows.Forms.Label();
			flowLayoutPanel28 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox30 = new POS_Client.MyCheckBox();
			myCheckBox31 = new POS_Client.MyCheckBox();
			myCheckBox32 = new POS_Client.MyCheckBox();
			flowLayoutPanel29 = new System.Windows.Forms.FlowLayoutPanel();
			cb_vendorstatus = new System.Windows.Forms.ComboBox();
			flowLayoutPanel30 = new System.Windows.Forms.FlowLayoutPanel();
			radioButton12 = new System.Windows.Forms.RadioButton();
			radioButton13 = new System.Windows.Forms.RadioButton();
			tabPage4 = new System.Windows.Forms.TabPage();
			dgv_SynchronizeList = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			panel20 = new System.Windows.Forms.Panel();
			label25 = new System.Windows.Forms.Label();
			flowLayoutPanel12 = new System.Windows.Forms.FlowLayoutPanel();
			dtp_SynchronizeDate = new System.Windows.Forms.DateTimePicker();
			btn_SynchronizeFilter = new System.Windows.Forms.Button();
			l_title = new System.Windows.Forms.Label();
			dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel2.SuspendLayout();
			tabControl.SuspendLayout();
			BasicData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgv_saleDetail).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgv_saleDetailTotal).BeginInit();
			flowLayoutPanel19.SuspendLayout();
			tabPage1.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			flowLayoutPanel3.SuspendLayout();
			panel6.SuspendLayout();
			panel3.SuspendLayout();
			panel1.SuspendLayout();
			panel4.SuspendLayout();
			panel5.SuspendLayout();
			panel9.SuspendLayout();
			flowLayoutPanel2.SuspendLayout();
			flowLayoutPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
			flowLayoutPanel5.SuspendLayout();
			flowLayoutPanel6.SuspendLayout();
			tabPage2.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			flowLayoutPanel7.SuspendLayout();
			flowLayoutPanel8.SuspendLayout();
			panel8.SuspendLayout();
			panel10.SuspendLayout();
			panel11.SuspendLayout();
			panel12.SuspendLayout();
			panel13.SuspendLayout();
			flowLayoutPanel9.SuspendLayout();
			flowLayoutPanel10.SuspendLayout();
			flowLayoutPanel11.SuspendLayout();
			tabPage3.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			flowLayoutPanel13.SuspendLayout();
			flowLayoutPanel14.SuspendLayout();
			panel14.SuspendLayout();
			panel15.SuspendLayout();
			panel16.SuspendLayout();
			panel17.SuspendLayout();
			panel18.SuspendLayout();
			panel19.SuspendLayout();
			flowLayoutPanel15.SuspendLayout();
			flowLayoutPanel16.SuspendLayout();
			flowLayoutPanel17.SuspendLayout();
			flowLayoutPanel18.SuspendLayout();
			tabPage5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgv_DeliveryTotal).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgv_DeliveryDetail).BeginInit();
			tabPage6.SuspendLayout();
			tableLayoutPanel5.SuspendLayout();
			flowLayoutPanel20.SuspendLayout();
			flowLayoutPanel21.SuspendLayout();
			panel7.SuspendLayout();
			panel21.SuspendLayout();
			panel22.SuspendLayout();
			panel23.SuspendLayout();
			panel24.SuspendLayout();
			panel25.SuspendLayout();
			flowLayoutPanel22.SuspendLayout();
			flowLayoutPanel23.SuspendLayout();
			flowLayoutPanel24.SuspendLayout();
			flowLayoutPanel25.SuspendLayout();
			tabPage7.SuspendLayout();
			tableLayoutPanel6.SuspendLayout();
			flowLayoutPanel26.SuspendLayout();
			flowLayoutPanel27.SuspendLayout();
			panel26.SuspendLayout();
			panel27.SuspendLayout();
			panel28.SuspendLayout();
			panel29.SuspendLayout();
			panel30.SuspendLayout();
			flowLayoutPanel28.SuspendLayout();
			flowLayoutPanel29.SuspendLayout();
			flowLayoutPanel30.SuspendLayout();
			tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgv_SynchronizeList).BeginInit();
			tableLayoutPanel4.SuspendLayout();
			panel20.SuspendLayout();
			flowLayoutPanel12.SuspendLayout();
			SuspendLayout();
			pb_virtualKeyBoard.Visible = false;
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
			btn_view_store_upload_history.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_view_store_upload_history.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_view_store_upload_history.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_view_store_upload_history.ForeColor = System.Drawing.Color.White;
			btn_view_store_upload_history.Location = new System.Drawing.Point(839, 47);
			btn_view_store_upload_history.Name = "btn_view_store_upload_history";
			btn_view_store_upload_history.Size = new System.Drawing.Size(128, 28);
			btn_view_store_upload_history.TabIndex = 3;
			btn_view_store_upload_history.Text = "檢視分店上傳紀錄";
			btn_view_store_upload_history.UseVisualStyleBackColor = false;
			btn_view_store_upload_history.Visible = false;
			tabControl.Controls.Add(BasicData);
			tabControl.Controls.Add(tabPage1);
			tabControl.Controls.Add(tabPage2);
			tabControl.Controls.Add(tabPage3);
			tabControl.Controls.Add(tabPage5);
			tabControl.Controls.Add(tabPage6);
			tabControl.Controls.Add(tabPage7);
			tabControl.Controls.Add(tabPage4);
			tabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			tabControl.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tabControl.Location = new System.Drawing.Point(0, 34);
			tabControl.Name = "tabControl";
			tabControl.Padding = new System.Drawing.Point(15, 10);
			tabControl.SelectedIndex = 0;
			tabControl.Size = new System.Drawing.Size(981, 627);
			tabControl.TabIndex = 54;
			BasicData.Controls.Add(btn_saleSearch);
			BasicData.Controls.Add(dateTimePicker10);
			BasicData.Controls.Add(label38);
			BasicData.Controls.Add(dateTimePicker11);
			BasicData.Controls.Add(FLP_saleDetail);
			BasicData.Controls.Add(dgv_saleDetail);
			BasicData.Controls.Add(btn_ExportTodayReport);
			BasicData.Controls.Add(dgv_saleDetailTotal);
			BasicData.Controls.Add(l_nowDate);
			BasicData.Controls.Add(flowLayoutPanel19);
			BasicData.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			BasicData.Location = new System.Drawing.Point(4, 47);
			BasicData.Name = "BasicData";
			BasicData.Padding = new System.Windows.Forms.Padding(3);
			BasicData.Size = new System.Drawing.Size(973, 576);
			BasicData.TabIndex = 0;
			BasicData.Text = "銷售摘要";
			BasicData.UseVisualStyleBackColor = true;
			btn_saleSearch.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_saleSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_saleSearch.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_saleSearch.ForeColor = System.Drawing.Color.White;
			btn_saleSearch.Location = new System.Drawing.Point(5, 118);
			btn_saleSearch.Name = "btn_saleSearch";
			btn_saleSearch.Size = new System.Drawing.Size(62, 38);
			btn_saleSearch.TabIndex = 49;
			btn_saleSearch.TabStop = false;
			btn_saleSearch.Text = "查詢";
			btn_saleSearch.UseVisualStyleBackColor = false;
			btn_saleSearch.Click += new System.EventHandler(btn_saleSearch_Click);
			dateTimePicker10.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker10.CustomFormat = "yyyy-MM-dd";
			dateTimePicker10.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker10.Location = new System.Drawing.Point(3, 10);
			dateTimePicker10.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker10.Name = "dateTimePicker10";
			dateTimePicker10.ShowCheckBox = true;
			dateTimePicker10.Size = new System.Drawing.Size(181, 29);
			dateTimePicker10.TabIndex = 65;
			dateTimePicker10.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			label38.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label38.AutoSize = true;
			label38.Location = new System.Drawing.Point(190, 14);
			label38.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label38.Name = "label38";
			label38.Size = new System.Drawing.Size(21, 20);
			label38.TabIndex = 67;
			label38.Text = "~";
			dateTimePicker11.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker11.CustomFormat = "yyyy-MM-dd";
			dateTimePicker11.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker11.Location = new System.Drawing.Point(217, 10);
			dateTimePicker11.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker11.Name = "dateTimePicker11";
			dateTimePicker11.ShowCheckBox = true;
			dateTimePicker11.Size = new System.Drawing.Size(181, 29);
			dateTimePicker11.TabIndex = 66;
			dateTimePicker11.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			FLP_saleDetail.AutoScroll = true;
			FLP_saleDetail.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			FLP_saleDetail.Location = new System.Drawing.Point(2, 199);
			FLP_saleDetail.Margin = new System.Windows.Forms.Padding(0);
			FLP_saleDetail.Name = "FLP_saleDetail";
			FLP_saleDetail.Size = new System.Drawing.Size(967, 357);
			FLP_saleDetail.TabIndex = 64;
			FLP_saleDetail.WrapContents = false;
			FLP_saleDetail.Click += new System.EventHandler(FLP_saleDetail_Click);
			FLP_saleDetail.Paint += new System.Windows.Forms.PaintEventHandler(FLP_saleDetail_Paint);
			FLP_saleDetail.MouseClick += new System.Windows.Forms.MouseEventHandler(FLP_saleDetail_MouseClick);
			dgv_saleDetail.AllowUserToAddRows = false;
			dgv_saleDetail.AllowUserToDeleteRows = false;
			dgv_saleDetail.AllowUserToResizeColumns = false;
			dgv_saleDetail.AllowUserToResizeRows = false;
			dgv_saleDetail.BackgroundColor = System.Drawing.Color.White;
			dgv_saleDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dgv_saleDetail.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgv_saleDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dgv_saleDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv_saleDetail.Columns.AddRange(dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8, Column7, Column8, Column9);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgv_saleDetail.DefaultCellStyle = dataGridViewCellStyle2;
			dgv_saleDetail.EnableHeadersVisualStyles = false;
			dgv_saleDetail.Location = new System.Drawing.Point(2, 163);
			dgv_saleDetail.Name = "dgv_saleDetail";
			dgv_saleDetail.RowHeadersVisible = false;
			dgv_saleDetail.RowTemplate.Height = 35;
			dgv_saleDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgv_saleDetail.Size = new System.Drawing.Size(967, 400);
			dgv_saleDetail.TabIndex = 63;
			dgv_saleDetail.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_saleDetail_CellContentClick);
			btn_ExportTodayReport.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_ExportTodayReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_ExportTodayReport.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_ExportTodayReport.ForeColor = System.Drawing.Color.White;
			btn_ExportTodayReport.Location = new System.Drawing.Point(835, 118);
			btn_ExportTodayReport.Name = "btn_ExportTodayReport";
			btn_ExportTodayReport.Size = new System.Drawing.Size(128, 28);
			btn_ExportTodayReport.TabIndex = 3;
			btn_ExportTodayReport.Text = "匯出本日報表";
			btn_ExportTodayReport.UseVisualStyleBackColor = false;
			btn_ExportTodayReport.Click += new System.EventHandler(btn_ExportTodayReport_Click);
			dgv_saleDetailTotal.AllowUserToAddRows = false;
			dgv_saleDetailTotal.AllowUserToDeleteRows = false;
			dgv_saleDetailTotal.AllowUserToResizeColumns = false;
			dgv_saleDetailTotal.AllowUserToResizeRows = false;
			dgv_saleDetailTotal.BackgroundColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dgv_saleDetailTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dgv_saleDetailTotal.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgv_saleDetailTotal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dgv_saleDetailTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv_saleDetailTotal.Columns.AddRange(dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, Column5);
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgv_saleDetailTotal.DefaultCellStyle = dataGridViewCellStyle4;
			dgv_saleDetailTotal.EnableHeadersVisualStyles = false;
			dgv_saleDetailTotal.Location = new System.Drawing.Point(3, 40);
			dgv_saleDetailTotal.Name = "dgv_saleDetailTotal";
			dgv_saleDetailTotal.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dgv_saleDetailTotal.RowHeadersVisible = false;
			dgv_saleDetailTotal.RowTemplate.Height = 40;
			dgv_saleDetailTotal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgv_saleDetailTotal.Size = new System.Drawing.Size(967, 72);
			dgv_saleDetailTotal.TabIndex = 62;
			dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewTextBoxColumn1.HeaderText = "銷售總額（原始）";
			dataGridViewTextBoxColumn1.MinimumWidth = 250;
			dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			dataGridViewTextBoxColumn1.ReadOnly = true;
			dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn1.Width = 250;
			dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewTextBoxColumn2.HeaderText = "退貨金額";
			dataGridViewTextBoxColumn2.MinimumWidth = 150;
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.ReadOnly = true;
			dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn2.Width = 150;
			dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewTextBoxColumn3.HeaderText = "付款模式總計";
			dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			dataGridViewTextBoxColumn3.ReadOnly = true;
			dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewTextBoxColumn4.HeaderText = "銷售品項總計";
			dataGridViewTextBoxColumn4.MinimumWidth = 130;
			dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			dataGridViewTextBoxColumn4.ReadOnly = true;
			dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn4.Width = 130;
			Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column5.HeaderText = "銷售數量總計";
			Column5.MinimumWidth = 130;
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column5.Width = 130;
			l_nowDate.AutoSize = true;
			l_nowDate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_nowDate.ForeColor = System.Drawing.Color.Black;
			l_nowDate.Location = new System.Drawing.Point(404, 18);
			l_nowDate.Name = "l_nowDate";
			l_nowDate.Size = new System.Drawing.Size(104, 21);
			l_nowDate.TabIndex = 1;
			l_nowDate.Text = "2017-01-01";
			l_nowDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			l_nowDate.Visible = false;
			flowLayoutPanel19.Controls.Add(btn_SalesSelectMember);
			flowLayoutPanel19.Location = new System.Drawing.Point(71, 112);
			flowLayoutPanel19.Name = "flowLayoutPanel19";
			flowLayoutPanel19.Size = new System.Drawing.Size(758, 47);
			flowLayoutPanel19.TabIndex = 68;
			btn_SalesSelectMember.BackColor = System.Drawing.Color.White;
			btn_SalesSelectMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SalesSelectMember.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SalesSelectMember.ForeColor = System.Drawing.Color.Black;
			btn_SalesSelectMember.Image = POS_Client.Properties.Resources.ic_toc_black_24dp_1x;
			btn_SalesSelectMember.Location = new System.Drawing.Point(10, 10);
			btn_SalesSelectMember.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
			btn_SalesSelectMember.Name = "btn_SalesSelectMember";
			btn_SalesSelectMember.Size = new System.Drawing.Size(35, 33);
			btn_SalesSelectMember.TabIndex = 5;
			btn_SalesSelectMember.UseVisualStyleBackColor = false;
			btn_SalesSelectMember.Click += new System.EventHandler(btn_SalesSelectMember_Click);
			tabPage1.Controls.Add(btn_reset);
			tabPage1.Controls.Add(btn_SearchPeriodTransactions);
			tabPage1.Controls.Add(tableLayoutPanel1);
			tabPage1.Location = new System.Drawing.Point(4, 47);
			tabPage1.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new System.Windows.Forms.Padding(3);
			tabPage1.Size = new System.Drawing.Size(973, 576);
			tabPage1.TabIndex = 1;
			tabPage1.Text = "期間交易";
			tabPage1.UseVisualStyleBackColor = true;
			btn_reset.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_reset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_reset.ForeColor = System.Drawing.Color.White;
			btn_reset.Location = new System.Drawing.Point(500, 380);
			btn_reset.Name = "btn_reset";
			btn_reset.Size = new System.Drawing.Size(113, 35);
			btn_reset.TabIndex = 44;
			btn_reset.TabStop = false;
			btn_reset.Text = "重設";
			btn_reset.UseVisualStyleBackColor = false;
			btn_reset.Click += new System.EventHandler(btn_reset_Click);
			btn_SearchPeriodTransactions.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_SearchPeriodTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SearchPeriodTransactions.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_SearchPeriodTransactions.ForeColor = System.Drawing.Color.White;
			btn_SearchPeriodTransactions.Location = new System.Drawing.Point(359, 380);
			btn_SearchPeriodTransactions.Name = "btn_SearchPeriodTransactions";
			btn_SearchPeriodTransactions.Size = new System.Drawing.Size(113, 35);
			btn_SearchPeriodTransactions.TabIndex = 43;
			btn_SearchPeriodTransactions.TabStop = false;
			btn_SearchPeriodTransactions.Text = "查詢";
			btn_SearchPeriodTransactions.UseVisualStyleBackColor = false;
			btn_SearchPeriodTransactions.Click += new System.EventHandler(btn_SearchPeriodTransactions_Click);
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 1, 1);
			tableLayoutPanel1.Controls.Add(panel6, 0, 5);
			tableLayoutPanel1.Controls.Add(panel3, 0, 2);
			tableLayoutPanel1.Controls.Add(panel1, 0, 1);
			tableLayoutPanel1.Controls.Add(panel4, 0, 0);
			tableLayoutPanel1.Controls.Add(panel5, 0, 3);
			tableLayoutPanel1.Controls.Add(panel9, 0, 4);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 1, 2);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel4, 1, 3);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel5, 1, 4);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel6, 1, 5);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 6;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel1.Size = new System.Drawing.Size(967, 356);
			tableLayoutPanel1.TabIndex = 2;
			flowLayoutPanel1.Controls.Add(dateTimePicker0);
			flowLayoutPanel1.Controls.Add(label21);
			flowLayoutPanel1.Controls.Add(dateTimePicker1);
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel1.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(802, 58);
			flowLayoutPanel1.TabIndex = 27;
			dateTimePicker0.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker0.CustomFormat = "yyyy-MM-dd";
			dateTimePicker0.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker0.Location = new System.Drawing.Point(10, 13);
			dateTimePicker0.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker0.Name = "dateTimePicker0";
			dateTimePicker0.ShowCheckBox = true;
			dateTimePicker0.Size = new System.Drawing.Size(181, 33);
			dateTimePicker0.TabIndex = 4;
			dateTimePicker0.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			label21.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label21.AutoSize = true;
			label21.Location = new System.Drawing.Point(197, 17);
			label21.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(24, 24);
			label21.TabIndex = 5;
			label21.Text = "~";
			dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker1.Location = new System.Drawing.Point(234, 13);
			dateTimePicker1.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker1.Name = "dateTimePicker1";
			dateTimePicker1.ShowCheckBox = true;
			dateTimePicker1.Size = new System.Drawing.Size(181, 33);
			dateTimePicker1.TabIndex = 4;
			dateTimePicker1.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			flowLayoutPanel3.Controls.Add(myCheckBox1);
			flowLayoutPanel3.Controls.Add(myCheckBox2);
			flowLayoutPanel3.Controls.Add(myCheckBox3);
			flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel3.Location = new System.Drawing.Point(164, 60);
			flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel3.Name = "flowLayoutPanel3";
			flowLayoutPanel3.Size = new System.Drawing.Size(802, 58);
			flowLayoutPanel3.TabIndex = 27;
			myCheckBox1.Location = new System.Drawing.Point(10, 13);
			myCheckBox1.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox1.Name = "myCheckBox1";
			myCheckBox1.Size = new System.Drawing.Size(71, 24);
			myCheckBox1.TabIndex = 0;
			myCheckBox1.Text = "正常";
			myCheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox1.UseVisualStyleBackColor = true;
			myCheckBox2.Location = new System.Drawing.Point(94, 13);
			myCheckBox2.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox2.Name = "myCheckBox2";
			myCheckBox2.Size = new System.Drawing.Size(126, 24);
			myCheckBox2.TabIndex = 0;
			myCheckBox2.Text = "正常(變更)";
			myCheckBox2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox2.UseVisualStyleBackColor = true;
			myCheckBox3.Location = new System.Drawing.Point(233, 13);
			myCheckBox3.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox3.Name = "myCheckBox3";
			myCheckBox3.Size = new System.Drawing.Size(72, 24);
			myCheckBox3.TabIndex = 0;
			myCheckBox3.Text = "取消";
			myCheckBox3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox3.UseVisualStyleBackColor = true;
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			panel6.Location = new System.Drawing.Point(1, 296);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(162, 59);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(80, 22);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(74, 21);
			label12.TabIndex = 0;
			label12.Text = "報表排序";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label6);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 119);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(162, 58);
			panel3.TabIndex = 21;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(80, 21);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(74, 21);
			label6.TabIndex = 0;
			label6.Text = "結帳模式";
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(1, 60);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(162, 58);
			panel1.TabIndex = 20;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(80, 19);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(74, 21);
			label1.TabIndex = 0;
			label1.Text = "訂單狀態";
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label2);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Location = new System.Drawing.Point(1, 1);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(162, 58);
			panel4.TabIndex = 19;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(48, 22);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(106, 21);
			label2.TabIndex = 0;
			label2.Text = "查詢日期區間";
			label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label8);
			panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			panel5.Location = new System.Drawing.Point(1, 178);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(162, 58);
			panel5.TabIndex = 22;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.White;
			label8.Location = new System.Drawing.Point(32, 16);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(122, 21);
			label8.TabIndex = 0;
			label8.Text = "銷售單總價範圍";
			panel9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel9.Controls.Add(label10);
			panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			panel9.Location = new System.Drawing.Point(1, 237);
			panel9.Margin = new System.Windows.Forms.Padding(0);
			panel9.Name = "panel9";
			panel9.Size = new System.Drawing.Size(162, 58);
			panel9.TabIndex = 23;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(80, 21);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(74, 21);
			label10.TabIndex = 0;
			label10.Text = "報表模式";
			flowLayoutPanel2.Controls.Add(myCheckBox4);
			flowLayoutPanel2.Controls.Add(myCheckBox5);
			flowLayoutPanel2.Controls.Add(myCheckBox6);
			flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel2.Location = new System.Drawing.Point(164, 119);
			flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel2.Name = "flowLayoutPanel2";
			flowLayoutPanel2.Size = new System.Drawing.Size(802, 58);
			flowLayoutPanel2.TabIndex = 27;
			myCheckBox4.Location = new System.Drawing.Point(10, 13);
			myCheckBox4.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox4.Name = "myCheckBox4";
			myCheckBox4.Size = new System.Drawing.Size(71, 24);
			myCheckBox4.TabIndex = 0;
			myCheckBox4.Text = "現金";
			myCheckBox4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox4.UseVisualStyleBackColor = true;
			myCheckBox5.Location = new System.Drawing.Point(94, 13);
			myCheckBox5.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox5.Name = "myCheckBox5";
			myCheckBox5.Size = new System.Drawing.Size(70, 24);
			myCheckBox5.TabIndex = 0;
			myCheckBox5.Text = "賒帳";
			myCheckBox5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox5.UseVisualStyleBackColor = true;
			myCheckBox6.Location = new System.Drawing.Point(177, 13);
			myCheckBox6.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox6.Name = "myCheckBox6";
			myCheckBox6.Size = new System.Drawing.Size(194, 24);
			myCheckBox6.TabIndex = 0;
			myCheckBox6.Text = "部分現金部分賒帳";
			myCheckBox6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox6.UseVisualStyleBackColor = true;
			flowLayoutPanel4.Controls.Add(numericUpDown1);
			flowLayoutPanel4.Controls.Add(label22);
			flowLayoutPanel4.Controls.Add(numericUpDown2);
			flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel4.Location = new System.Drawing.Point(164, 178);
			flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel4.Name = "flowLayoutPanel4";
			flowLayoutPanel4.Size = new System.Drawing.Size(802, 58);
			flowLayoutPanel4.TabIndex = 27;
			numericUpDown1.Location = new System.Drawing.Point(10, 13);
			numericUpDown1.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			numericUpDown1.Maximum = new decimal(new int[4]
			{
				99999,
				0,
				0,
				0
			});
			numericUpDown1.Name = "numericUpDown1";
			numericUpDown1.Size = new System.Drawing.Size(180, 33);
			numericUpDown1.TabIndex = 0;
			numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label22.AutoSize = true;
			label22.Location = new System.Drawing.Point(196, 15);
			label22.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(24, 24);
			label22.TabIndex = 5;
			label22.Text = "~";
			numericUpDown2.Location = new System.Drawing.Point(233, 13);
			numericUpDown2.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			numericUpDown2.Maximum = new decimal(new int[4]
			{
				99999,
				0,
				0,
				0
			});
			numericUpDown2.Name = "numericUpDown2";
			numericUpDown2.Size = new System.Drawing.Size(180, 33);
			numericUpDown2.TabIndex = 0;
			numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			numericUpDown2.Value = new decimal(new int[4]
			{
				99999,
				0,
				0,
				0
			});
			flowLayoutPanel5.Controls.Add(radioButton1);
			flowLayoutPanel5.Controls.Add(radioButton2);
			flowLayoutPanel5.Controls.Add(radioButton3);
			flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel5.Location = new System.Drawing.Point(164, 237);
			flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel5.Name = "flowLayoutPanel5";
			flowLayoutPanel5.Size = new System.Drawing.Size(802, 58);
			flowLayoutPanel5.TabIndex = 27;
			radioButton1.AutoSize = true;
			radioButton1.Location = new System.Drawing.Point(10, 13);
			radioButton1.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new System.Drawing.Size(85, 28);
			radioButton1.TabIndex = 0;
			radioButton1.Text = "月報表";
			radioButton1.UseVisualStyleBackColor = true;
			radioButton2.AutoSize = true;
			radioButton2.Location = new System.Drawing.Point(108, 13);
			radioButton2.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new System.Drawing.Size(85, 28);
			radioButton2.TabIndex = 0;
			radioButton2.Text = "週報表";
			radioButton2.UseVisualStyleBackColor = true;
			radioButton3.AutoSize = true;
			radioButton3.Checked = true;
			radioButton3.Location = new System.Drawing.Point(206, 13);
			radioButton3.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton3.Name = "radioButton3";
			radioButton3.Size = new System.Drawing.Size(85, 28);
			radioButton3.TabIndex = 0;
			radioButton3.TabStop = true;
			radioButton3.Text = "日報表";
			radioButton3.UseVisualStyleBackColor = true;
			flowLayoutPanel6.Controls.Add(radioButton4);
			flowLayoutPanel6.Controls.Add(radioButton5);
			flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel6.Location = new System.Drawing.Point(164, 296);
			flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel6.Name = "flowLayoutPanel6";
			flowLayoutPanel6.Size = new System.Drawing.Size(802, 59);
			flowLayoutPanel6.TabIndex = 27;
			radioButton4.AutoSize = true;
			radioButton4.Location = new System.Drawing.Point(10, 13);
			radioButton4.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton4.Name = "radioButton4";
			radioButton4.Size = new System.Drawing.Size(180, 28);
			radioButton4.TabIndex = 0;
			radioButton4.TabStop = true;
			radioButton4.Text = "依銷售總額高至低";
			radioButton4.UseVisualStyleBackColor = true;
			radioButton5.AutoSize = true;
			radioButton5.Location = new System.Drawing.Point(203, 13);
			radioButton5.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton5.Name = "radioButton5";
			radioButton5.Size = new System.Drawing.Size(142, 28);
			radioButton5.TabIndex = 0;
			radioButton5.TabStop = true;
			radioButton5.Text = "依日期近至遠";
			radioButton5.UseVisualStyleBackColor = true;
			tabPage2.Controls.Add(btn_reset_member);
			tabPage2.Controls.Add(btn_SearchMemberSalesSummary);
			tabPage2.Controls.Add(tableLayoutPanel2);
			tabPage2.Location = new System.Drawing.Point(4, 47);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new System.Windows.Forms.Padding(3);
			tabPage2.Size = new System.Drawing.Size(973, 576);
			tabPage2.TabIndex = 2;
			tabPage2.Text = "客次交易";
			tabPage2.UseVisualStyleBackColor = true;
			btn_reset_member.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_reset_member.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_reset_member.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_reset_member.ForeColor = System.Drawing.Color.White;
			btn_reset_member.Location = new System.Drawing.Point(495, 392);
			btn_reset_member.Name = "btn_reset_member";
			btn_reset_member.Size = new System.Drawing.Size(113, 35);
			btn_reset_member.TabIndex = 46;
			btn_reset_member.TabStop = false;
			btn_reset_member.Text = "重設";
			btn_reset_member.UseVisualStyleBackColor = false;
			btn_reset_member.Click += new System.EventHandler(btn_reset_member_Click);
			btn_SearchMemberSalesSummary.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_SearchMemberSalesSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SearchMemberSalesSummary.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_SearchMemberSalesSummary.ForeColor = System.Drawing.Color.White;
			btn_SearchMemberSalesSummary.Location = new System.Drawing.Point(354, 392);
			btn_SearchMemberSalesSummary.Name = "btn_SearchMemberSalesSummary";
			btn_SearchMemberSalesSummary.Size = new System.Drawing.Size(113, 35);
			btn_SearchMemberSalesSummary.TabIndex = 45;
			btn_SearchMemberSalesSummary.TabStop = false;
			btn_SearchMemberSalesSummary.Text = "查詢";
			btn_SearchMemberSalesSummary.UseVisualStyleBackColor = false;
			btn_SearchMemberSalesSummary.Click += new System.EventHandler(btn_SearchMemberSalesSummary_Click);
			tableLayoutPanel2.BackColor = System.Drawing.Color.White;
			tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel2.ColumnCount = 2;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel2.Controls.Add(flowLayoutPanel7, 1, 0);
			tableLayoutPanel2.Controls.Add(flowLayoutPanel8, 1, 1);
			tableLayoutPanel2.Controls.Add(panel8, 0, 2);
			tableLayoutPanel2.Controls.Add(panel10, 0, 1);
			tableLayoutPanel2.Controls.Add(panel11, 0, 0);
			tableLayoutPanel2.Controls.Add(panel12, 0, 3);
			tableLayoutPanel2.Controls.Add(panel13, 0, 4);
			tableLayoutPanel2.Controls.Add(flowLayoutPanel9, 1, 2);
			tableLayoutPanel2.Controls.Add(flowLayoutPanel10, 1, 3);
			tableLayoutPanel2.Controls.Add(flowLayoutPanel11, 1, 4);
			tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 5;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel2.Size = new System.Drawing.Size(967, 372);
			tableLayoutPanel2.TabIndex = 3;
			flowLayoutPanel7.Controls.Add(dateTimePicker2);
			flowLayoutPanel7.Controls.Add(label5);
			flowLayoutPanel7.Controls.Add(dateTimePicker3);
			flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel7.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel7.Name = "flowLayoutPanel7";
			flowLayoutPanel7.Size = new System.Drawing.Size(802, 61);
			flowLayoutPanel7.TabIndex = 27;
			dateTimePicker2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker2.Location = new System.Drawing.Point(10, 13);
			dateTimePicker2.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker2.Name = "dateTimePicker2";
			dateTimePicker2.ShowCheckBox = true;
			dateTimePicker2.Size = new System.Drawing.Size(181, 33);
			dateTimePicker2.TabIndex = 6;
			dateTimePicker2.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(197, 17);
			label5.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(24, 24);
			label5.TabIndex = 8;
			label5.Text = "~";
			dateTimePicker3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker3.CustomFormat = "yyyy-MM-dd";
			dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker3.Location = new System.Drawing.Point(234, 13);
			dateTimePicker3.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker3.Name = "dateTimePicker3";
			dateTimePicker3.ShowCheckBox = true;
			dateTimePicker3.Size = new System.Drawing.Size(181, 33);
			dateTimePicker3.TabIndex = 7;
			dateTimePicker3.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			flowLayoutPanel8.AutoScroll = true;
			flowLayoutPanel8.Controls.Add(btn_SelectMember);
			flowLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel8.Location = new System.Drawing.Point(164, 63);
			flowLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel8.Name = "flowLayoutPanel8";
			flowLayoutPanel8.Size = new System.Drawing.Size(802, 121);
			flowLayoutPanel8.TabIndex = 27;
			btn_SelectMember.BackColor = System.Drawing.Color.White;
			btn_SelectMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SelectMember.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SelectMember.ForeColor = System.Drawing.Color.Black;
			btn_SelectMember.Image = POS_Client.Properties.Resources.ic_toc_black_24dp_1x;
			btn_SelectMember.Location = new System.Drawing.Point(10, 10);
			btn_SelectMember.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
			btn_SelectMember.Name = "btn_SelectMember";
			btn_SelectMember.Size = new System.Drawing.Size(35, 33);
			btn_SelectMember.TabIndex = 3;
			btn_SelectMember.UseVisualStyleBackColor = false;
			btn_SelectMember.Click += new System.EventHandler(btn_SelectMember_Click);
			panel8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel8.Controls.Add(label7);
			panel8.Dock = System.Windows.Forms.DockStyle.Fill;
			panel8.Location = new System.Drawing.Point(1, 185);
			panel8.Margin = new System.Windows.Forms.Padding(0);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(162, 61);
			panel8.TabIndex = 21;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(80, 21);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(74, 21);
			label7.TabIndex = 0;
			label7.Text = "會員類型";
			panel10.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel10.Controls.Add(label9);
			panel10.Dock = System.Windows.Forms.DockStyle.Fill;
			panel10.Location = new System.Drawing.Point(1, 63);
			panel10.Margin = new System.Windows.Forms.Padding(0);
			panel10.Name = "panel10";
			panel10.Size = new System.Drawing.Size(162, 121);
			panel10.TabIndex = 20;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label9.ForeColor = System.Drawing.Color.White;
			label9.Location = new System.Drawing.Point(13, 52);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(138, 21);
			label9.TabIndex = 0;
			label9.Text = "選擇特定購買會員";
			panel11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel11.Controls.Add(label11);
			panel11.Dock = System.Windows.Forms.DockStyle.Fill;
			panel11.Location = new System.Drawing.Point(1, 1);
			panel11.Margin = new System.Windows.Forms.Padding(0);
			panel11.Name = "panel11";
			panel11.Size = new System.Drawing.Size(162, 61);
			panel11.TabIndex = 19;
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label11.ForeColor = System.Drawing.Color.White;
			label11.Location = new System.Drawing.Point(48, 22);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(106, 21);
			label11.TabIndex = 0;
			label11.Text = "查詢日期區間";
			label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			panel12.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel12.Controls.Add(label13);
			panel12.Dock = System.Windows.Forms.DockStyle.Fill;
			panel12.Location = new System.Drawing.Point(1, 247);
			panel12.Margin = new System.Windows.Forms.Padding(0);
			panel12.Name = "panel12";
			panel12.Size = new System.Drawing.Size(162, 61);
			panel12.TabIndex = 22;
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.White;
			label13.Location = new System.Drawing.Point(77, 19);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(74, 21);
			label13.TabIndex = 0;
			label13.Text = "會員狀態";
			panel13.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel13.Controls.Add(label14);
			panel13.Dock = System.Windows.Forms.DockStyle.Fill;
			panel13.Location = new System.Drawing.Point(1, 309);
			panel13.Margin = new System.Windows.Forms.Padding(0);
			panel13.Name = "panel13";
			panel13.Size = new System.Drawing.Size(162, 62);
			panel13.TabIndex = 23;
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label14.ForeColor = System.Drawing.Color.White;
			label14.Location = new System.Drawing.Point(80, 21);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(74, 21);
			label14.TabIndex = 0;
			label14.Text = "報表排序";
			flowLayoutPanel9.Controls.Add(myCheckBox7);
			flowLayoutPanel9.Controls.Add(myCheckBox8);
			flowLayoutPanel9.Controls.Add(myCheckBox9);
			flowLayoutPanel9.Controls.Add(myCheckBox10);
			flowLayoutPanel9.Controls.Add(myCheckBox11);
			flowLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel9.Location = new System.Drawing.Point(164, 185);
			flowLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel9.Name = "flowLayoutPanel9";
			flowLayoutPanel9.Size = new System.Drawing.Size(802, 61);
			flowLayoutPanel9.TabIndex = 27;
			myCheckBox7.Location = new System.Drawing.Point(10, 13);
			myCheckBox7.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox7.Name = "myCheckBox7";
			myCheckBox7.Size = new System.Drawing.Size(114, 24);
			myCheckBox7.TabIndex = 1;
			myCheckBox7.Text = "一般會員";
			myCheckBox7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox7.UseVisualStyleBackColor = true;
			myCheckBox8.Location = new System.Drawing.Point(137, 13);
			myCheckBox8.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox8.Name = "myCheckBox8";
			myCheckBox8.Size = new System.Drawing.Size(134, 24);
			myCheckBox8.TabIndex = 2;
			myCheckBox8.Text = "優惠會員(1)";
			myCheckBox8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox8.UseVisualStyleBackColor = true;
			myCheckBox9.Location = new System.Drawing.Point(284, 13);
			myCheckBox9.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox9.Name = "myCheckBox9";
			myCheckBox9.Size = new System.Drawing.Size(131, 24);
			myCheckBox9.TabIndex = 3;
			myCheckBox9.Text = "優惠會員(2)";
			myCheckBox9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox9.UseVisualStyleBackColor = true;
			myCheckBox10.Location = new System.Drawing.Point(428, 13);
			myCheckBox10.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox10.Name = "myCheckBox10";
			myCheckBox10.Size = new System.Drawing.Size(150, 24);
			myCheckBox10.TabIndex = 2;
			myCheckBox10.Text = "購肥補助會員";
			myCheckBox10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox10.UseVisualStyleBackColor = true;
			myCheckBox11.Location = new System.Drawing.Point(591, 13);
			myCheckBox11.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox11.Name = "myCheckBox11";
			myCheckBox11.Size = new System.Drawing.Size(205, 24);
			myCheckBox11.TabIndex = 3;
			myCheckBox11.Text = "尚有賒帳未還款會員";
			myCheckBox11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox11.UseVisualStyleBackColor = true;
			flowLayoutPanel10.Controls.Add(cb_area);
			flowLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel10.Location = new System.Drawing.Point(164, 247);
			flowLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel10.Name = "flowLayoutPanel10";
			flowLayoutPanel10.Size = new System.Drawing.Size(802, 61);
			flowLayoutPanel10.TabIndex = 27;
			cb_area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_area.FormattingEnabled = true;
			cb_area.Location = new System.Drawing.Point(10, 13);
			cb_area.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			cb_area.Name = "cb_area";
			cb_area.Size = new System.Drawing.Size(181, 32);
			cb_area.TabIndex = 9;
			flowLayoutPanel11.Controls.Add(radioButton6);
			flowLayoutPanel11.Controls.Add(radioButton7);
			flowLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel11.Location = new System.Drawing.Point(164, 309);
			flowLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel11.Name = "flowLayoutPanel11";
			flowLayoutPanel11.Size = new System.Drawing.Size(802, 62);
			flowLayoutPanel11.TabIndex = 27;
			radioButton6.AutoSize = true;
			radioButton6.Location = new System.Drawing.Point(10, 13);
			radioButton6.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton6.Name = "radioButton6";
			radioButton6.Size = new System.Drawing.Size(180, 28);
			radioButton6.TabIndex = 1;
			radioButton6.TabStop = true;
			radioButton6.Text = "依銷售總額高至低";
			radioButton6.UseVisualStyleBackColor = true;
			radioButton7.AutoSize = true;
			radioButton7.Location = new System.Drawing.Point(203, 13);
			radioButton7.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton7.Name = "radioButton7";
			radioButton7.Size = new System.Drawing.Size(199, 28);
			radioButton7.TabIndex = 2;
			radioButton7.TabStop = true;
			radioButton7.Text = "依消費次數由多至少";
			radioButton7.UseVisualStyleBackColor = true;
			tabPage3.Controls.Add(btn_reset_Commodity);
			tabPage3.Controls.Add(btn_SearchCommodityTradingSummary);
			tabPage3.Controls.Add(tableLayoutPanel3);
			tabPage3.Location = new System.Drawing.Point(4, 47);
			tabPage3.Name = "tabPage3";
			tabPage3.Padding = new System.Windows.Forms.Padding(3);
			tabPage3.Size = new System.Drawing.Size(973, 576);
			tabPage3.TabIndex = 3;
			tabPage3.Text = "商品交易";
			tabPage3.UseVisualStyleBackColor = true;
			btn_reset_Commodity.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_reset_Commodity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_reset_Commodity.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_reset_Commodity.ForeColor = System.Drawing.Color.White;
			btn_reset_Commodity.Location = new System.Drawing.Point(492, 438);
			btn_reset_Commodity.Name = "btn_reset_Commodity";
			btn_reset_Commodity.Size = new System.Drawing.Size(113, 35);
			btn_reset_Commodity.TabIndex = 46;
			btn_reset_Commodity.TabStop = false;
			btn_reset_Commodity.Text = "重設";
			btn_reset_Commodity.UseVisualStyleBackColor = false;
			btn_reset_Commodity.Click += new System.EventHandler(btn_reset_Commodity_Click);
			btn_SearchCommodityTradingSummary.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_SearchCommodityTradingSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SearchCommodityTradingSummary.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_SearchCommodityTradingSummary.ForeColor = System.Drawing.Color.White;
			btn_SearchCommodityTradingSummary.Location = new System.Drawing.Point(351, 438);
			btn_SearchCommodityTradingSummary.Name = "btn_SearchCommodityTradingSummary";
			btn_SearchCommodityTradingSummary.Size = new System.Drawing.Size(113, 35);
			btn_SearchCommodityTradingSummary.TabIndex = 45;
			btn_SearchCommodityTradingSummary.TabStop = false;
			btn_SearchCommodityTradingSummary.Text = "查詢";
			btn_SearchCommodityTradingSummary.UseVisualStyleBackColor = false;
			btn_SearchCommodityTradingSummary.Click += new System.EventHandler(btn_btn_SearchCommodityTradingSummary_Click);
			tableLayoutPanel3.BackColor = System.Drawing.Color.White;
			tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel3.ColumnCount = 2;
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel3.Controls.Add(flowLayoutPanel13, 1, 0);
			tableLayoutPanel3.Controls.Add(flowLayoutPanel14, 1, 1);
			tableLayoutPanel3.Controls.Add(panel14, 0, 5);
			tableLayoutPanel3.Controls.Add(panel15, 0, 2);
			tableLayoutPanel3.Controls.Add(panel16, 0, 1);
			tableLayoutPanel3.Controls.Add(panel17, 0, 0);
			tableLayoutPanel3.Controls.Add(panel18, 0, 3);
			tableLayoutPanel3.Controls.Add(panel19, 0, 4);
			tableLayoutPanel3.Controls.Add(flowLayoutPanel15, 1, 2);
			tableLayoutPanel3.Controls.Add(flowLayoutPanel16, 1, 3);
			tableLayoutPanel3.Controls.Add(flowLayoutPanel17, 1, 4);
			tableLayoutPanel3.Controls.Add(flowLayoutPanel18, 1, 5);
			tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 6;
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel3.Size = new System.Drawing.Size(967, 419);
			tableLayoutPanel3.TabIndex = 3;
			flowLayoutPanel13.Controls.Add(dateTimePicker4);
			flowLayoutPanel13.Controls.Add(label23);
			flowLayoutPanel13.Controls.Add(dateTimePicker5);
			flowLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel13.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel13.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel13.Name = "flowLayoutPanel13";
			flowLayoutPanel13.Size = new System.Drawing.Size(802, 58);
			flowLayoutPanel13.TabIndex = 27;
			dateTimePicker4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker4.CustomFormat = "yyyy-MM-dd";
			dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker4.Location = new System.Drawing.Point(10, 13);
			dateTimePicker4.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker4.Name = "dateTimePicker4";
			dateTimePicker4.ShowCheckBox = true;
			dateTimePicker4.Size = new System.Drawing.Size(181, 33);
			dateTimePicker4.TabIndex = 9;
			dateTimePicker4.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			label23.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label23.AutoSize = true;
			label23.Location = new System.Drawing.Point(197, 17);
			label23.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(24, 24);
			label23.TabIndex = 11;
			label23.Text = "~";
			dateTimePicker5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker5.CustomFormat = "yyyy-MM-dd";
			dateTimePicker5.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker5.Location = new System.Drawing.Point(234, 13);
			dateTimePicker5.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker5.Name = "dateTimePicker5";
			dateTimePicker5.ShowCheckBox = true;
			dateTimePicker5.Size = new System.Drawing.Size(181, 33);
			dateTimePicker5.TabIndex = 10;
			dateTimePicker5.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			flowLayoutPanel14.AutoScroll = true;
			flowLayoutPanel14.Controls.Add(btn_SelectCommodity);
			flowLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel14.Location = new System.Drawing.Point(164, 60);
			flowLayoutPanel14.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel14.Name = "flowLayoutPanel14";
			flowLayoutPanel14.Size = new System.Drawing.Size(802, 117);
			flowLayoutPanel14.TabIndex = 27;
			btn_SelectCommodity.BackColor = System.Drawing.Color.White;
			btn_SelectCommodity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SelectCommodity.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SelectCommodity.ForeColor = System.Drawing.Color.Black;
			btn_SelectCommodity.Image = POS_Client.Properties.Resources.ic_toc_black_24dp_1x;
			btn_SelectCommodity.Location = new System.Drawing.Point(10, 10);
			btn_SelectCommodity.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
			btn_SelectCommodity.Name = "btn_SelectCommodity";
			btn_SelectCommodity.Size = new System.Drawing.Size(35, 33);
			btn_SelectCommodity.TabIndex = 4;
			btn_SelectCommodity.UseVisualStyleBackColor = false;
			btn_SelectCommodity.Click += new System.EventHandler(btn_SelectCommodity_Click);
			panel14.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel14.Controls.Add(label15);
			panel14.Dock = System.Windows.Forms.DockStyle.Fill;
			panel14.Location = new System.Drawing.Point(1, 355);
			panel14.Margin = new System.Windows.Forms.Padding(0);
			panel14.Name = "panel14";
			panel14.Size = new System.Drawing.Size(162, 63);
			panel14.TabIndex = 20;
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label15.ForeColor = System.Drawing.Color.White;
			label15.Location = new System.Drawing.Point(80, 22);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(74, 21);
			label15.TabIndex = 0;
			label15.Text = "報表排序";
			panel15.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel15.Controls.Add(label16);
			panel15.Dock = System.Windows.Forms.DockStyle.Fill;
			panel15.Location = new System.Drawing.Point(1, 178);
			panel15.Margin = new System.Windows.Forms.Padding(0);
			panel15.Name = "panel15";
			panel15.Size = new System.Drawing.Size(162, 58);
			panel15.TabIndex = 21;
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label16.ForeColor = System.Drawing.Color.White;
			label16.Location = new System.Drawing.Point(80, 21);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(74, 21);
			label16.TabIndex = 0;
			label16.Text = "資料模式";
			panel16.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel16.Controls.Add(label17);
			panel16.Dock = System.Windows.Forms.DockStyle.Fill;
			panel16.Location = new System.Drawing.Point(1, 60);
			panel16.Margin = new System.Windows.Forms.Padding(0);
			panel16.Name = "panel16";
			panel16.Size = new System.Drawing.Size(162, 117);
			panel16.TabIndex = 20;
			label17.AutoSize = true;
			label17.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label17.ForeColor = System.Drawing.Color.White;
			label17.Location = new System.Drawing.Point(26, 50);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(122, 21);
			label17.TabIndex = 0;
			label17.Text = "銷售單包含商品";
			panel17.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel17.Controls.Add(label18);
			panel17.Dock = System.Windows.Forms.DockStyle.Fill;
			panel17.Location = new System.Drawing.Point(1, 1);
			panel17.Margin = new System.Windows.Forms.Padding(0);
			panel17.Name = "panel17";
			panel17.Size = new System.Drawing.Size(162, 58);
			panel17.TabIndex = 19;
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label18.ForeColor = System.Drawing.Color.White;
			label18.Location = new System.Drawing.Point(48, 22);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(106, 21);
			label18.TabIndex = 0;
			label18.Text = "查詢日期區間";
			label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			panel18.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel18.Controls.Add(label19);
			panel18.Dock = System.Windows.Forms.DockStyle.Fill;
			panel18.Location = new System.Drawing.Point(1, 237);
			panel18.Margin = new System.Windows.Forms.Padding(0);
			panel18.Name = "panel18";
			panel18.Size = new System.Drawing.Size(162, 58);
			panel18.TabIndex = 22;
			label19.AutoSize = true;
			label19.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label19.ForeColor = System.Drawing.Color.White;
			label19.Location = new System.Drawing.Point(80, 19);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(74, 21);
			label19.TabIndex = 0;
			label19.Text = "商品類型";
			panel19.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel19.Controls.Add(label20);
			panel19.Dock = System.Windows.Forms.DockStyle.Fill;
			panel19.Location = new System.Drawing.Point(1, 296);
			panel19.Margin = new System.Windows.Forms.Padding(0);
			panel19.Name = "panel19";
			panel19.Size = new System.Drawing.Size(162, 58);
			panel19.TabIndex = 23;
			label20.AutoSize = true;
			label20.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label20.ForeColor = System.Drawing.Color.White;
			label20.Location = new System.Drawing.Point(80, 21);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(74, 21);
			label20.TabIndex = 0;
			label20.Text = "商品狀態";
			flowLayoutPanel15.Controls.Add(myCheckBox12);
			flowLayoutPanel15.Controls.Add(myCheckBox13);
			flowLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel15.Location = new System.Drawing.Point(164, 178);
			flowLayoutPanel15.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel15.Name = "flowLayoutPanel15";
			flowLayoutPanel15.Size = new System.Drawing.Size(802, 58);
			flowLayoutPanel15.TabIndex = 27;
			myCheckBox12.Location = new System.Drawing.Point(10, 13);
			myCheckBox12.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox12.Name = "myCheckBox12";
			myCheckBox12.Size = new System.Drawing.Size(74, 24);
			myCheckBox12.TabIndex = 3;
			myCheckBox12.Text = "介接";
			myCheckBox12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox12.UseVisualStyleBackColor = true;
			myCheckBox13.Location = new System.Drawing.Point(97, 13);
			myCheckBox13.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox13.Name = "myCheckBox13";
			myCheckBox13.Size = new System.Drawing.Size(80, 24);
			myCheckBox13.TabIndex = 4;
			myCheckBox13.Text = "自建";
			myCheckBox13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox13.UseVisualStyleBackColor = true;
			flowLayoutPanel16.Controls.Add(myCheckBox14);
			flowLayoutPanel16.Controls.Add(myCheckBox15);
			flowLayoutPanel16.Controls.Add(myCheckBox16);
			flowLayoutPanel16.Controls.Add(myCheckBox17);
			flowLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel16.Location = new System.Drawing.Point(164, 237);
			flowLayoutPanel16.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel16.Name = "flowLayoutPanel16";
			flowLayoutPanel16.Size = new System.Drawing.Size(802, 58);
			flowLayoutPanel16.TabIndex = 27;
			myCheckBox14.Location = new System.Drawing.Point(10, 13);
			myCheckBox14.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox14.Name = "myCheckBox14";
			myCheckBox14.Size = new System.Drawing.Size(74, 24);
			myCheckBox14.TabIndex = 3;
			myCheckBox14.Text = "農藥";
			myCheckBox14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox14.UseVisualStyleBackColor = true;
			myCheckBox15.Location = new System.Drawing.Point(97, 13);
			myCheckBox15.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox15.Name = "myCheckBox15";
			myCheckBox15.Size = new System.Drawing.Size(74, 24);
			myCheckBox15.TabIndex = 4;
			myCheckBox15.Text = "肥料";
			myCheckBox15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox15.UseVisualStyleBackColor = true;
			myCheckBox16.Location = new System.Drawing.Point(184, 13);
			myCheckBox16.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox16.Name = "myCheckBox16";
			myCheckBox16.Size = new System.Drawing.Size(74, 24);
			myCheckBox16.TabIndex = 5;
			myCheckBox16.Text = "資材";
			myCheckBox16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox16.UseVisualStyleBackColor = true;
			myCheckBox17.Location = new System.Drawing.Point(271, 13);
			myCheckBox17.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox17.Name = "myCheckBox17";
			myCheckBox17.Size = new System.Drawing.Size(74, 24);
			myCheckBox17.TabIndex = 6;
			myCheckBox17.Text = "其他";
			myCheckBox17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox17.UseVisualStyleBackColor = true;
			flowLayoutPanel17.Controls.Add(myCheckBox18);
			flowLayoutPanel17.Controls.Add(myCheckBox19);
			flowLayoutPanel17.Controls.Add(myCheckBox20);
			flowLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel17.Location = new System.Drawing.Point(164, 296);
			flowLayoutPanel17.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel17.Name = "flowLayoutPanel17";
			flowLayoutPanel17.Size = new System.Drawing.Size(802, 58);
			flowLayoutPanel17.TabIndex = 27;
			myCheckBox18.Location = new System.Drawing.Point(10, 13);
			myCheckBox18.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox18.Name = "myCheckBox18";
			myCheckBox18.Size = new System.Drawing.Size(91, 24);
			myCheckBox18.TabIndex = 3;
			myCheckBox18.Text = "使用中";
			myCheckBox18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox18.UseVisualStyleBackColor = true;
			myCheckBox19.Location = new System.Drawing.Point(114, 13);
			myCheckBox19.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox19.Name = "myCheckBox19";
			myCheckBox19.Size = new System.Drawing.Size(91, 24);
			myCheckBox19.TabIndex = 4;
			myCheckBox19.Text = "未使用";
			myCheckBox19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox19.UseVisualStyleBackColor = true;
			myCheckBox20.Location = new System.Drawing.Point(218, 13);
			myCheckBox20.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox20.Name = "myCheckBox20";
			myCheckBox20.Size = new System.Drawing.Size(91, 24);
			myCheckBox20.TabIndex = 5;
			myCheckBox20.Text = "已停用";
			myCheckBox20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox20.UseVisualStyleBackColor = true;
			flowLayoutPanel18.Controls.Add(radioButton8);
			flowLayoutPanel18.Controls.Add(radioButton9);
			flowLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel18.Location = new System.Drawing.Point(164, 355);
			flowLayoutPanel18.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel18.Name = "flowLayoutPanel18";
			flowLayoutPanel18.Size = new System.Drawing.Size(802, 63);
			flowLayoutPanel18.TabIndex = 27;
			radioButton8.AutoSize = true;
			radioButton8.Location = new System.Drawing.Point(10, 13);
			radioButton8.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton8.Name = "radioButton8";
			radioButton8.Size = new System.Drawing.Size(180, 28);
			radioButton8.TabIndex = 3;
			radioButton8.TabStop = true;
			radioButton8.Text = "依銷售總額高至低";
			radioButton8.UseVisualStyleBackColor = true;
			radioButton9.AutoSize = true;
			radioButton9.Location = new System.Drawing.Point(203, 13);
			radioButton9.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton9.Name = "radioButton9";
			radioButton9.Size = new System.Drawing.Size(199, 28);
			radioButton9.TabIndex = 4;
			radioButton9.TabStop = true;
			radioButton9.Text = "依消費次數由多至少";
			radioButton9.UseVisualStyleBackColor = true;
			tabPage5.Controls.Add(FLP_DeliveryDetail);
			tabPage5.Controls.Add(btn_ExportTodayDeliveryReport);
			tabPage5.Controls.Add(dgv_DeliveryTotal);
			tabPage5.Controls.Add(l_nowDate2);
			tabPage5.Controls.Add(dgv_DeliveryDetail);
			tabPage5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tabPage5.Location = new System.Drawing.Point(4, 47);
			tabPage5.Name = "tabPage5";
			tabPage5.Size = new System.Drawing.Size(973, 576);
			tabPage5.TabIndex = 5;
			tabPage5.Text = "今日出貨摘要";
			tabPage5.UseVisualStyleBackColor = true;
			FLP_DeliveryDetail.AutoScroll = true;
			FLP_DeliveryDetail.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			FLP_DeliveryDetail.Location = new System.Drawing.Point(2, 190);
			FLP_DeliveryDetail.Margin = new System.Windows.Forms.Padding(0);
			FLP_DeliveryDetail.Name = "FLP_DeliveryDetail";
			FLP_DeliveryDetail.Size = new System.Drawing.Size(967, 368);
			FLP_DeliveryDetail.TabIndex = 69;
			FLP_DeliveryDetail.WrapContents = false;
			btn_ExportTodayDeliveryReport.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_ExportTodayDeliveryReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_ExportTodayDeliveryReport.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_ExportTodayDeliveryReport.ForeColor = System.Drawing.Color.White;
			btn_ExportTodayDeliveryReport.Location = new System.Drawing.Point(835, 120);
			btn_ExportTodayDeliveryReport.Name = "btn_ExportTodayDeliveryReport";
			btn_ExportTodayDeliveryReport.Size = new System.Drawing.Size(128, 28);
			btn_ExportTodayDeliveryReport.TabIndex = 66;
			btn_ExportTodayDeliveryReport.Text = "匯出本日報表";
			btn_ExportTodayDeliveryReport.UseVisualStyleBackColor = false;
			btn_ExportTodayDeliveryReport.Click += new System.EventHandler(btn_ExportTodayDeliveryReport_Click);
			dgv_DeliveryTotal.AllowUserToAddRows = false;
			dgv_DeliveryTotal.AllowUserToDeleteRows = false;
			dgv_DeliveryTotal.AllowUserToResizeColumns = false;
			dgv_DeliveryTotal.AllowUserToResizeRows = false;
			dgv_DeliveryTotal.BackgroundColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dgv_DeliveryTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dgv_DeliveryTotal.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgv_DeliveryTotal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			dgv_DeliveryTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv_DeliveryTotal.Columns.AddRange(dataGridViewTextBoxColumn16, dataGridViewTextBoxColumn17, dataGridViewTextBoxColumn18, dataGridViewTextBoxColumn19);
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dataGridViewCellStyle6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgv_DeliveryTotal.DefaultCellStyle = dataGridViewCellStyle6;
			dgv_DeliveryTotal.EnableHeadersVisualStyles = false;
			dgv_DeliveryTotal.Location = new System.Drawing.Point(3, 42);
			dgv_DeliveryTotal.Name = "dgv_DeliveryTotal";
			dgv_DeliveryTotal.ReadOnly = true;
			dgv_DeliveryTotal.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dgv_DeliveryTotal.RowHeadersVisible = false;
			dgv_DeliveryTotal.RowTemplate.Height = 40;
			dgv_DeliveryTotal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgv_DeliveryTotal.Size = new System.Drawing.Size(967, 72);
			dgv_DeliveryTotal.TabIndex = 67;
			dataGridViewTextBoxColumn16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle7;
			dataGridViewTextBoxColumn16.HeaderText = "本日出貨單金額總計";
			dataGridViewTextBoxColumn16.MinimumWidth = 2;
			dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
			dataGridViewTextBoxColumn16.ReadOnly = true;
			dataGridViewTextBoxColumn16.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn16.Width = 280;
			dataGridViewTextBoxColumn17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewTextBoxColumn17.DefaultCellStyle = dataGridViewCellStyle8;
			dataGridViewTextBoxColumn17.HeaderText = "變更前總額";
			dataGridViewTextBoxColumn17.MinimumWidth = 150;
			dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
			dataGridViewTextBoxColumn17.ReadOnly = true;
			dataGridViewTextBoxColumn17.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn17.Width = 280;
			dataGridViewTextBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewTextBoxColumn18.DefaultCellStyle = dataGridViewCellStyle9;
			dataGridViewTextBoxColumn18.HeaderText = "出貨品項總計";
			dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
			dataGridViewTextBoxColumn18.ReadOnly = true;
			dataGridViewTextBoxColumn18.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn18.Width = 205;
			dataGridViewTextBoxColumn19.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewTextBoxColumn19.DefaultCellStyle = dataGridViewCellStyle10;
			dataGridViewTextBoxColumn19.HeaderText = "出貨數量總計";
			dataGridViewTextBoxColumn19.MinimumWidth = 130;
			dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
			dataGridViewTextBoxColumn19.ReadOnly = true;
			dataGridViewTextBoxColumn19.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn19.Width = 205;
			l_nowDate2.AutoSize = true;
			l_nowDate2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_nowDate2.ForeColor = System.Drawing.Color.Black;
			l_nowDate2.Location = new System.Drawing.Point(8, 18);
			l_nowDate2.Name = "l_nowDate2";
			l_nowDate2.Size = new System.Drawing.Size(104, 21);
			l_nowDate2.TabIndex = 65;
			l_nowDate2.Text = "2017-01-01";
			l_nowDate2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			dgv_DeliveryDetail.AllowUserToAddRows = false;
			dgv_DeliveryDetail.AllowUserToDeleteRows = false;
			dgv_DeliveryDetail.AllowUserToResizeColumns = false;
			dgv_DeliveryDetail.AllowUserToResizeRows = false;
			dgv_DeliveryDetail.BackgroundColor = System.Drawing.Color.White;
			dgv_DeliveryDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dgv_DeliveryDetail.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle11.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgv_DeliveryDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
			dgv_DeliveryDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv_DeliveryDetail.Columns.AddRange(dataGridViewTextBoxColumn9, dataGridViewTextBoxColumn10, dataGridViewTextBoxColumn11, dataGridViewTextBoxColumn14, dataGridViewTextBoxColumn13, dataGridViewTextBoxColumn15);
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgv_DeliveryDetail.DefaultCellStyle = dataGridViewCellStyle12;
			dgv_DeliveryDetail.EnableHeadersVisualStyles = false;
			dgv_DeliveryDetail.Location = new System.Drawing.Point(2, 155);
			dgv_DeliveryDetail.Name = "dgv_DeliveryDetail";
			dgv_DeliveryDetail.ReadOnly = true;
			dgv_DeliveryDetail.RowHeadersVisible = false;
			dgv_DeliveryDetail.RowTemplate.Height = 35;
			dgv_DeliveryDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgv_DeliveryDetail.Size = new System.Drawing.Size(967, 400);
			dgv_DeliveryDetail.TabIndex = 68;
			dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn9.HeaderText = "出貨單號";
			dataGridViewTextBoxColumn9.MinimumWidth = 150;
			dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			dataGridViewTextBoxColumn9.ReadOnly = true;
			dataGridViewTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn9.Width = 210;
			dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn10.HeaderText = "業者";
			dataGridViewTextBoxColumn10.MinimumWidth = 150;
			dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			dataGridViewTextBoxColumn10.ReadOnly = true;
			dataGridViewTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn10.Width = 360;
			dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn11.HeaderText = "出貨總額(原始)\t";
			dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			dataGridViewTextBoxColumn11.ReadOnly = true;
			dataGridViewTextBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn11.Width = 160;
			dataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn14.HeaderText = "品項";
			dataGridViewTextBoxColumn14.MinimumWidth = 60;
			dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
			dataGridViewTextBoxColumn14.ReadOnly = true;
			dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn14.Width = 70;
			dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn13.HeaderText = "數量";
			dataGridViewTextBoxColumn13.MinimumWidth = 60;
			dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			dataGridViewTextBoxColumn13.ReadOnly = true;
			dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn13.Width = 80;
			dataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn15.HeaderText = "狀態";
			dataGridViewTextBoxColumn15.MinimumWidth = 60;
			dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
			dataGridViewTextBoxColumn15.ReadOnly = true;
			dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn15.Width = 70;
			tabPage6.Controls.Add(btn_CommDelReset);
			tabPage6.Controls.Add(btn_SearchGoodsDelivery);
			tabPage6.Controls.Add(tableLayoutPanel5);
			tabPage6.Location = new System.Drawing.Point(4, 47);
			tabPage6.Name = "tabPage6";
			tabPage6.Size = new System.Drawing.Size(973, 576);
			tabPage6.TabIndex = 6;
			tabPage6.Text = "商品出貨";
			tabPage6.UseVisualStyleBackColor = true;
			btn_CommDelReset.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_CommDelReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_CommDelReset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_CommDelReset.ForeColor = System.Drawing.Color.White;
			btn_CommDelReset.Location = new System.Drawing.Point(499, 438);
			btn_CommDelReset.Name = "btn_CommDelReset";
			btn_CommDelReset.Size = new System.Drawing.Size(113, 35);
			btn_CommDelReset.TabIndex = 49;
			btn_CommDelReset.TabStop = false;
			btn_CommDelReset.Text = "重設";
			btn_CommDelReset.UseVisualStyleBackColor = false;
			btn_CommDelReset.Click += new System.EventHandler(btn_CommDelReset_Click);
			btn_SearchGoodsDelivery.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_SearchGoodsDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SearchGoodsDelivery.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_SearchGoodsDelivery.ForeColor = System.Drawing.Color.White;
			btn_SearchGoodsDelivery.Location = new System.Drawing.Point(351, 438);
			btn_SearchGoodsDelivery.Name = "btn_SearchGoodsDelivery";
			btn_SearchGoodsDelivery.Size = new System.Drawing.Size(113, 35);
			btn_SearchGoodsDelivery.TabIndex = 48;
			btn_SearchGoodsDelivery.TabStop = false;
			btn_SearchGoodsDelivery.Text = "查詢";
			btn_SearchGoodsDelivery.UseVisualStyleBackColor = false;
			btn_SearchGoodsDelivery.Click += new System.EventHandler(btn_SearchGoodsDelivery_Click);
			tableLayoutPanel5.BackColor = System.Drawing.Color.White;
			tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel5.ColumnCount = 2;
			tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel5.Controls.Add(flowLayoutPanel20, 1, 0);
			tableLayoutPanel5.Controls.Add(flowLayoutPanel21, 1, 1);
			tableLayoutPanel5.Controls.Add(panel7, 0, 5);
			tableLayoutPanel5.Controls.Add(panel21, 0, 2);
			tableLayoutPanel5.Controls.Add(panel22, 0, 1);
			tableLayoutPanel5.Controls.Add(panel23, 0, 0);
			tableLayoutPanel5.Controls.Add(panel24, 0, 3);
			tableLayoutPanel5.Controls.Add(panel25, 0, 4);
			tableLayoutPanel5.Controls.Add(flowLayoutPanel22, 1, 2);
			tableLayoutPanel5.Controls.Add(flowLayoutPanel23, 1, 3);
			tableLayoutPanel5.Controls.Add(flowLayoutPanel24, 1, 4);
			tableLayoutPanel5.Controls.Add(flowLayoutPanel25, 1, 5);
			tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel5.Name = "tableLayoutPanel5";
			tableLayoutPanel5.RowCount = 6;
			tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143f));
			tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel5.Size = new System.Drawing.Size(973, 419);
			tableLayoutPanel5.TabIndex = 47;
			flowLayoutPanel20.Controls.Add(dateTimePicker6);
			flowLayoutPanel20.Controls.Add(label24);
			flowLayoutPanel20.Controls.Add(dateTimePicker7);
			flowLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel20.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel20.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel20.Name = "flowLayoutPanel20";
			flowLayoutPanel20.Size = new System.Drawing.Size(808, 58);
			flowLayoutPanel20.TabIndex = 27;
			dateTimePicker6.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker6.CustomFormat = "yyyy-MM-dd";
			dateTimePicker6.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker6.Location = new System.Drawing.Point(10, 13);
			dateTimePicker6.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker6.Name = "dateTimePicker6";
			dateTimePicker6.ShowCheckBox = true;
			dateTimePicker6.Size = new System.Drawing.Size(181, 33);
			dateTimePicker6.TabIndex = 9;
			dateTimePicker6.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			label24.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label24.AutoSize = true;
			label24.Location = new System.Drawing.Point(197, 17);
			label24.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label24.Name = "label24";
			label24.Size = new System.Drawing.Size(24, 24);
			label24.TabIndex = 11;
			label24.Text = "~";
			dateTimePicker7.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker7.CustomFormat = "yyyy-MM-dd";
			dateTimePicker7.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker7.Location = new System.Drawing.Point(234, 13);
			dateTimePicker7.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker7.Name = "dateTimePicker7";
			dateTimePicker7.ShowCheckBox = true;
			dateTimePicker7.Size = new System.Drawing.Size(181, 33);
			dateTimePicker7.TabIndex = 10;
			dateTimePicker7.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			flowLayoutPanel21.AutoScroll = true;
			flowLayoutPanel21.Controls.Add(btn_SelectCommodity2);
			flowLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel21.Location = new System.Drawing.Point(164, 60);
			flowLayoutPanel21.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel21.Name = "flowLayoutPanel21";
			flowLayoutPanel21.Size = new System.Drawing.Size(808, 117);
			flowLayoutPanel21.TabIndex = 27;
			btn_SelectCommodity2.BackColor = System.Drawing.Color.White;
			btn_SelectCommodity2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SelectCommodity2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SelectCommodity2.ForeColor = System.Drawing.Color.Black;
			btn_SelectCommodity2.Image = POS_Client.Properties.Resources.ic_toc_black_24dp_1x;
			btn_SelectCommodity2.Location = new System.Drawing.Point(10, 10);
			btn_SelectCommodity2.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
			btn_SelectCommodity2.Name = "btn_SelectCommodity2";
			btn_SelectCommodity2.Size = new System.Drawing.Size(35, 33);
			btn_SelectCommodity2.TabIndex = 4;
			btn_SelectCommodity2.UseVisualStyleBackColor = false;
			btn_SelectCommodity2.Click += new System.EventHandler(btn_SelectCommodity2_Click);
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(label26);
			panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			panel7.Location = new System.Drawing.Point(1, 355);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(162, 63);
			panel7.TabIndex = 20;
			label26.AutoSize = true;
			label26.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label26.ForeColor = System.Drawing.Color.White;
			label26.Location = new System.Drawing.Point(80, 22);
			label26.Name = "label26";
			label26.Size = new System.Drawing.Size(74, 21);
			label26.TabIndex = 0;
			label26.Text = "報表排序";
			panel21.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel21.Controls.Add(label27);
			panel21.Dock = System.Windows.Forms.DockStyle.Fill;
			panel21.Location = new System.Drawing.Point(1, 178);
			panel21.Margin = new System.Windows.Forms.Padding(0);
			panel21.Name = "panel21";
			panel21.Size = new System.Drawing.Size(162, 58);
			panel21.TabIndex = 21;
			label27.AutoSize = true;
			label27.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label27.ForeColor = System.Drawing.Color.White;
			label27.Location = new System.Drawing.Point(80, 21);
			label27.Name = "label27";
			label27.Size = new System.Drawing.Size(74, 21);
			label27.TabIndex = 0;
			label27.Text = "資料模式";
			panel22.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel22.Controls.Add(label28);
			panel22.Dock = System.Windows.Forms.DockStyle.Fill;
			panel22.Location = new System.Drawing.Point(1, 60);
			panel22.Margin = new System.Windows.Forms.Padding(0);
			panel22.Name = "panel22";
			panel22.Size = new System.Drawing.Size(162, 117);
			panel22.TabIndex = 20;
			label28.AutoSize = true;
			label28.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label28.ForeColor = System.Drawing.Color.White;
			label28.Location = new System.Drawing.Point(80, 51);
			label28.Name = "label28";
			label28.Size = new System.Drawing.Size(74, 21);
			label28.TabIndex = 0;
			label28.Text = "包含商品";
			panel23.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel23.Controls.Add(label29);
			panel23.Dock = System.Windows.Forms.DockStyle.Fill;
			panel23.Location = new System.Drawing.Point(1, 1);
			panel23.Margin = new System.Windows.Forms.Padding(0);
			panel23.Name = "panel23";
			panel23.Size = new System.Drawing.Size(162, 58);
			panel23.TabIndex = 19;
			label29.AutoSize = true;
			label29.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label29.ForeColor = System.Drawing.Color.White;
			label29.Location = new System.Drawing.Point(48, 22);
			label29.Name = "label29";
			label29.Size = new System.Drawing.Size(106, 21);
			label29.TabIndex = 0;
			label29.Text = "查詢日期區間";
			label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			panel24.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel24.Controls.Add(label30);
			panel24.Dock = System.Windows.Forms.DockStyle.Fill;
			panel24.Location = new System.Drawing.Point(1, 237);
			panel24.Margin = new System.Windows.Forms.Padding(0);
			panel24.Name = "panel24";
			panel24.Size = new System.Drawing.Size(162, 58);
			panel24.TabIndex = 22;
			label30.AutoSize = true;
			label30.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label30.ForeColor = System.Drawing.Color.White;
			label30.Location = new System.Drawing.Point(80, 19);
			label30.Name = "label30";
			label30.Size = new System.Drawing.Size(74, 21);
			label30.TabIndex = 0;
			label30.Text = "商品類型";
			panel25.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel25.Controls.Add(label31);
			panel25.Dock = System.Windows.Forms.DockStyle.Fill;
			panel25.Location = new System.Drawing.Point(1, 296);
			panel25.Margin = new System.Windows.Forms.Padding(0);
			panel25.Name = "panel25";
			panel25.Size = new System.Drawing.Size(162, 58);
			panel25.TabIndex = 23;
			label31.AutoSize = true;
			label31.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label31.ForeColor = System.Drawing.Color.White;
			label31.Location = new System.Drawing.Point(80, 21);
			label31.Name = "label31";
			label31.Size = new System.Drawing.Size(74, 21);
			label31.TabIndex = 0;
			label31.Text = "商品狀態";
			flowLayoutPanel22.Controls.Add(myCheckBox21);
			flowLayoutPanel22.Controls.Add(myCheckBox22);
			flowLayoutPanel22.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel22.Location = new System.Drawing.Point(164, 178);
			flowLayoutPanel22.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel22.Name = "flowLayoutPanel22";
			flowLayoutPanel22.Size = new System.Drawing.Size(808, 58);
			flowLayoutPanel22.TabIndex = 27;
			myCheckBox21.Checked = true;
			myCheckBox21.CheckState = System.Windows.Forms.CheckState.Checked;
			myCheckBox21.Location = new System.Drawing.Point(10, 13);
			myCheckBox21.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox21.Name = "myCheckBox21";
			myCheckBox21.Size = new System.Drawing.Size(74, 24);
			myCheckBox21.TabIndex = 3;
			myCheckBox21.Text = "介接";
			myCheckBox21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox21.UseVisualStyleBackColor = true;
			myCheckBox22.Checked = true;
			myCheckBox22.CheckState = System.Windows.Forms.CheckState.Checked;
			myCheckBox22.Location = new System.Drawing.Point(97, 13);
			myCheckBox22.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox22.Name = "myCheckBox22";
			myCheckBox22.Size = new System.Drawing.Size(80, 24);
			myCheckBox22.TabIndex = 4;
			myCheckBox22.Text = "自建";
			myCheckBox22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox22.UseVisualStyleBackColor = true;
			flowLayoutPanel23.Controls.Add(myCheckBox23);
			flowLayoutPanel23.Controls.Add(myCheckBox24);
			flowLayoutPanel23.Controls.Add(myCheckBox25);
			flowLayoutPanel23.Controls.Add(myCheckBox26);
			flowLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel23.Location = new System.Drawing.Point(164, 237);
			flowLayoutPanel23.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel23.Name = "flowLayoutPanel23";
			flowLayoutPanel23.Size = new System.Drawing.Size(808, 58);
			flowLayoutPanel23.TabIndex = 27;
			myCheckBox23.Checked = true;
			myCheckBox23.CheckState = System.Windows.Forms.CheckState.Checked;
			myCheckBox23.Location = new System.Drawing.Point(10, 13);
			myCheckBox23.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox23.Name = "myCheckBox23";
			myCheckBox23.Size = new System.Drawing.Size(74, 24);
			myCheckBox23.TabIndex = 3;
			myCheckBox23.Text = "農藥";
			myCheckBox23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox23.UseVisualStyleBackColor = true;
			myCheckBox24.Checked = true;
			myCheckBox24.CheckState = System.Windows.Forms.CheckState.Checked;
			myCheckBox24.Location = new System.Drawing.Point(97, 13);
			myCheckBox24.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox24.Name = "myCheckBox24";
			myCheckBox24.Size = new System.Drawing.Size(74, 24);
			myCheckBox24.TabIndex = 4;
			myCheckBox24.Text = "肥料";
			myCheckBox24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox24.UseVisualStyleBackColor = true;
			myCheckBox25.Checked = true;
			myCheckBox25.CheckState = System.Windows.Forms.CheckState.Checked;
			myCheckBox25.Location = new System.Drawing.Point(184, 13);
			myCheckBox25.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox25.Name = "myCheckBox25";
			myCheckBox25.Size = new System.Drawing.Size(74, 24);
			myCheckBox25.TabIndex = 5;
			myCheckBox25.Text = "資材";
			myCheckBox25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox25.UseVisualStyleBackColor = true;
			myCheckBox26.Checked = true;
			myCheckBox26.CheckState = System.Windows.Forms.CheckState.Checked;
			myCheckBox26.Location = new System.Drawing.Point(271, 13);
			myCheckBox26.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox26.Name = "myCheckBox26";
			myCheckBox26.Size = new System.Drawing.Size(74, 24);
			myCheckBox26.TabIndex = 6;
			myCheckBox26.Text = "其他";
			myCheckBox26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox26.UseVisualStyleBackColor = true;
			flowLayoutPanel24.Controls.Add(myCheckBox27);
			flowLayoutPanel24.Controls.Add(myCheckBox28);
			flowLayoutPanel24.Controls.Add(myCheckBox29);
			flowLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel24.Location = new System.Drawing.Point(164, 296);
			flowLayoutPanel24.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel24.Name = "flowLayoutPanel24";
			flowLayoutPanel24.Size = new System.Drawing.Size(808, 58);
			flowLayoutPanel24.TabIndex = 27;
			myCheckBox27.Checked = true;
			myCheckBox27.CheckState = System.Windows.Forms.CheckState.Checked;
			myCheckBox27.Location = new System.Drawing.Point(10, 13);
			myCheckBox27.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox27.Name = "myCheckBox27";
			myCheckBox27.Size = new System.Drawing.Size(91, 24);
			myCheckBox27.TabIndex = 3;
			myCheckBox27.Text = "使用中";
			myCheckBox27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox27.UseVisualStyleBackColor = true;
			myCheckBox28.Location = new System.Drawing.Point(114, 13);
			myCheckBox28.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox28.Name = "myCheckBox28";
			myCheckBox28.Size = new System.Drawing.Size(91, 24);
			myCheckBox28.TabIndex = 4;
			myCheckBox28.Text = "未使用";
			myCheckBox28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox28.UseVisualStyleBackColor = true;
			myCheckBox29.Location = new System.Drawing.Point(218, 13);
			myCheckBox29.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox29.Name = "myCheckBox29";
			myCheckBox29.Size = new System.Drawing.Size(91, 24);
			myCheckBox29.TabIndex = 5;
			myCheckBox29.Text = "已停用";
			myCheckBox29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox29.UseVisualStyleBackColor = true;
			flowLayoutPanel25.Controls.Add(radioButton10);
			flowLayoutPanel25.Controls.Add(radioButton11);
			flowLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel25.Location = new System.Drawing.Point(164, 355);
			flowLayoutPanel25.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel25.Name = "flowLayoutPanel25";
			flowLayoutPanel25.Size = new System.Drawing.Size(808, 63);
			flowLayoutPanel25.TabIndex = 27;
			radioButton10.AutoSize = true;
			radioButton10.Checked = true;
			radioButton10.Location = new System.Drawing.Point(10, 13);
			radioButton10.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton10.Name = "radioButton10";
			radioButton10.Size = new System.Drawing.Size(180, 28);
			radioButton10.TabIndex = 3;
			radioButton10.TabStop = true;
			radioButton10.Text = "依出貨數量多到少";
			radioButton10.UseVisualStyleBackColor = true;
			radioButton11.AutoSize = true;
			radioButton11.Location = new System.Drawing.Point(203, 13);
			radioButton11.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton11.Name = "radioButton11";
			radioButton11.Size = new System.Drawing.Size(180, 28);
			radioButton11.TabIndex = 4;
			radioButton11.TabStop = true;
			radioButton11.Text = "依出貨金額高到低";
			radioButton11.UseVisualStyleBackColor = true;
			tabPage7.Controls.Add(btn_VendorDelReset);
			tabPage7.Controls.Add(btn_SearchVendorDelivery);
			tabPage7.Controls.Add(tableLayoutPanel6);
			tabPage7.Location = new System.Drawing.Point(4, 47);
			tabPage7.Name = "tabPage7";
			tabPage7.Size = new System.Drawing.Size(973, 576);
			tabPage7.TabIndex = 7;
			tabPage7.Text = "廠商出貨";
			tabPage7.UseVisualStyleBackColor = true;
			btn_VendorDelReset.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_VendorDelReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_VendorDelReset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_VendorDelReset.ForeColor = System.Drawing.Color.White;
			btn_VendorDelReset.Location = new System.Drawing.Point(506, 399);
			btn_VendorDelReset.Name = "btn_VendorDelReset";
			btn_VendorDelReset.Size = new System.Drawing.Size(113, 35);
			btn_VendorDelReset.TabIndex = 49;
			btn_VendorDelReset.TabStop = false;
			btn_VendorDelReset.Text = "重設";
			btn_VendorDelReset.UseVisualStyleBackColor = false;
			btn_VendorDelReset.Click += new System.EventHandler(btn_VendorDelReset_Click);
			btn_SearchVendorDelivery.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_SearchVendorDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SearchVendorDelivery.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_SearchVendorDelivery.ForeColor = System.Drawing.Color.White;
			btn_SearchVendorDelivery.Location = new System.Drawing.Point(365, 399);
			btn_SearchVendorDelivery.Name = "btn_SearchVendorDelivery";
			btn_SearchVendorDelivery.Size = new System.Drawing.Size(113, 35);
			btn_SearchVendorDelivery.TabIndex = 48;
			btn_SearchVendorDelivery.TabStop = false;
			btn_SearchVendorDelivery.Text = "查詢";
			btn_SearchVendorDelivery.UseVisualStyleBackColor = false;
			btn_SearchVendorDelivery.Click += new System.EventHandler(btn_SearchVendorDelivery_Click);
			tableLayoutPanel6.BackColor = System.Drawing.Color.White;
			tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel6.ColumnCount = 2;
			tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel6.Controls.Add(flowLayoutPanel26, 1, 0);
			tableLayoutPanel6.Controls.Add(flowLayoutPanel27, 1, 1);
			tableLayoutPanel6.Controls.Add(panel26, 0, 2);
			tableLayoutPanel6.Controls.Add(panel27, 0, 1);
			tableLayoutPanel6.Controls.Add(panel28, 0, 0);
			tableLayoutPanel6.Controls.Add(panel29, 0, 3);
			tableLayoutPanel6.Controls.Add(panel30, 0, 4);
			tableLayoutPanel6.Controls.Add(flowLayoutPanel28, 1, 2);
			tableLayoutPanel6.Controls.Add(flowLayoutPanel29, 1, 3);
			tableLayoutPanel6.Controls.Add(flowLayoutPanel30, 1, 4);
			tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel6.Name = "tableLayoutPanel6";
			tableLayoutPanel6.RowCount = 5;
			tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel6.Size = new System.Drawing.Size(973, 372);
			tableLayoutPanel6.TabIndex = 47;
			flowLayoutPanel26.Controls.Add(dateTimePicker8);
			flowLayoutPanel26.Controls.Add(label32);
			flowLayoutPanel26.Controls.Add(dateTimePicker9);
			flowLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel26.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel26.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel26.Name = "flowLayoutPanel26";
			flowLayoutPanel26.Size = new System.Drawing.Size(808, 61);
			flowLayoutPanel26.TabIndex = 27;
			dateTimePicker8.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker8.CustomFormat = "yyyy-MM-dd";
			dateTimePicker8.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker8.Location = new System.Drawing.Point(10, 13);
			dateTimePicker8.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker8.Name = "dateTimePicker8";
			dateTimePicker8.ShowCheckBox = true;
			dateTimePicker8.Size = new System.Drawing.Size(181, 33);
			dateTimePicker8.TabIndex = 6;
			dateTimePicker8.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			label32.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label32.AutoSize = true;
			label32.Location = new System.Drawing.Point(197, 17);
			label32.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label32.Name = "label32";
			label32.Size = new System.Drawing.Size(24, 24);
			label32.TabIndex = 8;
			label32.Text = "~";
			dateTimePicker9.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker9.CustomFormat = "yyyy-MM-dd";
			dateTimePicker9.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker9.Location = new System.Drawing.Point(234, 13);
			dateTimePicker9.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker9.Name = "dateTimePicker9";
			dateTimePicker9.ShowCheckBox = true;
			dateTimePicker9.Size = new System.Drawing.Size(181, 33);
			dateTimePicker9.TabIndex = 7;
			dateTimePicker9.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			flowLayoutPanel27.AutoScroll = true;
			flowLayoutPanel27.Controls.Add(btn_SelectVendor);
			flowLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel27.Location = new System.Drawing.Point(164, 63);
			flowLayoutPanel27.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel27.Name = "flowLayoutPanel27";
			flowLayoutPanel27.Size = new System.Drawing.Size(808, 121);
			flowLayoutPanel27.TabIndex = 27;
			btn_SelectVendor.BackColor = System.Drawing.Color.White;
			btn_SelectVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SelectVendor.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SelectVendor.ForeColor = System.Drawing.Color.Black;
			btn_SelectVendor.Image = POS_Client.Properties.Resources.ic_toc_black_24dp_1x;
			btn_SelectVendor.Location = new System.Drawing.Point(10, 10);
			btn_SelectVendor.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
			btn_SelectVendor.Name = "btn_SelectVendor";
			btn_SelectVendor.Size = new System.Drawing.Size(35, 33);
			btn_SelectVendor.TabIndex = 3;
			btn_SelectVendor.UseVisualStyleBackColor = false;
			btn_SelectVendor.Click += new System.EventHandler(btn_SelectVendor_Click);
			panel26.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel26.Controls.Add(label33);
			panel26.Dock = System.Windows.Forms.DockStyle.Fill;
			panel26.Location = new System.Drawing.Point(1, 185);
			panel26.Margin = new System.Windows.Forms.Padding(0);
			panel26.Name = "panel26";
			panel26.Size = new System.Drawing.Size(162, 61);
			panel26.TabIndex = 21;
			label33.AutoSize = true;
			label33.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label33.ForeColor = System.Drawing.Color.White;
			label33.Location = new System.Drawing.Point(80, 21);
			label33.Name = "label33";
			label33.Size = new System.Drawing.Size(74, 21);
			label33.TabIndex = 0;
			label33.Text = "廠商類型";
			panel27.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel27.Controls.Add(label34);
			panel27.Dock = System.Windows.Forms.DockStyle.Fill;
			panel27.Location = new System.Drawing.Point(1, 63);
			panel27.Margin = new System.Windows.Forms.Padding(0);
			panel27.Name = "panel27";
			panel27.Size = new System.Drawing.Size(162, 121);
			panel27.TabIndex = 20;
			label34.AutoSize = true;
			label34.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label34.ForeColor = System.Drawing.Color.White;
			label34.Location = new System.Drawing.Point(15, 52);
			label34.Name = "label34";
			label34.Size = new System.Drawing.Size(138, 21);
			label34.TabIndex = 0;
			label34.Text = "選擇特定出貨廠商";
			panel28.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel28.Controls.Add(label35);
			panel28.Dock = System.Windows.Forms.DockStyle.Fill;
			panel28.Location = new System.Drawing.Point(1, 1);
			panel28.Margin = new System.Windows.Forms.Padding(0);
			panel28.Name = "panel28";
			panel28.Size = new System.Drawing.Size(162, 61);
			panel28.TabIndex = 19;
			label35.AutoSize = true;
			label35.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label35.ForeColor = System.Drawing.Color.White;
			label35.Location = new System.Drawing.Point(48, 22);
			label35.Name = "label35";
			label35.Size = new System.Drawing.Size(106, 21);
			label35.TabIndex = 0;
			label35.Text = "查詢日期區間";
			label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			panel29.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel29.Controls.Add(label36);
			panel29.Dock = System.Windows.Forms.DockStyle.Fill;
			panel29.Location = new System.Drawing.Point(1, 247);
			panel29.Margin = new System.Windows.Forms.Padding(0);
			panel29.Name = "panel29";
			panel29.Size = new System.Drawing.Size(162, 61);
			panel29.TabIndex = 22;
			label36.AutoSize = true;
			label36.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label36.ForeColor = System.Drawing.Color.White;
			label36.Location = new System.Drawing.Point(110, 19);
			label36.Name = "label36";
			label36.Size = new System.Drawing.Size(42, 21);
			label36.TabIndex = 0;
			label36.Text = "狀態";
			panel30.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel30.Controls.Add(label37);
			panel30.Dock = System.Windows.Forms.DockStyle.Fill;
			panel30.Location = new System.Drawing.Point(1, 309);
			panel30.Margin = new System.Windows.Forms.Padding(0);
			panel30.Name = "panel30";
			panel30.Size = new System.Drawing.Size(162, 62);
			panel30.TabIndex = 23;
			label37.AutoSize = true;
			label37.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label37.ForeColor = System.Drawing.Color.White;
			label37.Location = new System.Drawing.Point(80, 21);
			label37.Name = "label37";
			label37.Size = new System.Drawing.Size(74, 21);
			label37.TabIndex = 0;
			label37.Text = "報表排序";
			flowLayoutPanel28.Controls.Add(myCheckBox30);
			flowLayoutPanel28.Controls.Add(myCheckBox31);
			flowLayoutPanel28.Controls.Add(myCheckBox32);
			flowLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel28.Location = new System.Drawing.Point(164, 185);
			flowLayoutPanel28.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel28.Name = "flowLayoutPanel28";
			flowLayoutPanel28.Size = new System.Drawing.Size(808, 61);
			flowLayoutPanel28.TabIndex = 27;
			myCheckBox30.Checked = true;
			myCheckBox30.CheckState = System.Windows.Forms.CheckState.Checked;
			myCheckBox30.Location = new System.Drawing.Point(10, 13);
			myCheckBox30.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox30.Name = "myCheckBox30";
			myCheckBox30.Size = new System.Drawing.Size(114, 24);
			myCheckBox30.TabIndex = 1;
			myCheckBox30.Text = "本地廠商";
			myCheckBox30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox30.UseVisualStyleBackColor = true;
			myCheckBox31.Checked = true;
			myCheckBox31.CheckState = System.Windows.Forms.CheckState.Checked;
			myCheckBox31.Location = new System.Drawing.Point(137, 13);
			myCheckBox31.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox31.Name = "myCheckBox31";
			myCheckBox31.Size = new System.Drawing.Size(112, 24);
			myCheckBox31.TabIndex = 2;
			myCheckBox31.Text = "進口廠商";
			myCheckBox31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox31.UseVisualStyleBackColor = true;
			myCheckBox32.Location = new System.Drawing.Point(262, 13);
			myCheckBox32.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			myCheckBox32.Name = "myCheckBox32";
			myCheckBox32.Size = new System.Drawing.Size(279, 24);
			myCheckBox32.TabIndex = 3;
			myCheckBox32.Text = "農藥廠商(已驗證營業資訊者)";
			myCheckBox32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			myCheckBox32.UseVisualStyleBackColor = true;
			flowLayoutPanel29.Controls.Add(cb_vendorstatus);
			flowLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel29.Location = new System.Drawing.Point(164, 247);
			flowLayoutPanel29.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel29.Name = "flowLayoutPanel29";
			flowLayoutPanel29.Size = new System.Drawing.Size(808, 61);
			flowLayoutPanel29.TabIndex = 27;
			cb_vendorstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_vendorstatus.FormattingEnabled = true;
			cb_vendorstatus.Location = new System.Drawing.Point(10, 13);
			cb_vendorstatus.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			cb_vendorstatus.Name = "cb_vendorstatus";
			cb_vendorstatus.Size = new System.Drawing.Size(181, 32);
			cb_vendorstatus.TabIndex = 9;
			flowLayoutPanel30.Controls.Add(radioButton12);
			flowLayoutPanel30.Controls.Add(radioButton13);
			flowLayoutPanel30.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel30.Location = new System.Drawing.Point(164, 309);
			flowLayoutPanel30.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel30.Name = "flowLayoutPanel30";
			flowLayoutPanel30.Size = new System.Drawing.Size(808, 62);
			flowLayoutPanel30.TabIndex = 27;
			radioButton12.AutoSize = true;
			radioButton12.Checked = true;
			radioButton12.Location = new System.Drawing.Point(10, 13);
			radioButton12.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton12.Name = "radioButton12";
			radioButton12.Size = new System.Drawing.Size(180, 28);
			radioButton12.TabIndex = 1;
			radioButton12.TabStop = true;
			radioButton12.Text = "依出貨總額高至低";
			radioButton12.UseVisualStyleBackColor = true;
			radioButton13.AutoSize = true;
			radioButton13.Location = new System.Drawing.Point(203, 13);
			radioButton13.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			radioButton13.Name = "radioButton13";
			radioButton13.Size = new System.Drawing.Size(199, 28);
			radioButton13.TabIndex = 2;
			radioButton13.Text = "依出貨次數由多至少";
			radioButton13.UseVisualStyleBackColor = true;
			tabPage4.Controls.Add(dgv_SynchronizeList);
			tabPage4.Controls.Add(tableLayoutPanel4);
			tabPage4.Controls.Add(l_title);
			tabPage4.Location = new System.Drawing.Point(4, 47);
			tabPage4.Name = "tabPage4";
			tabPage4.Padding = new System.Windows.Forms.Padding(3);
			tabPage4.Size = new System.Drawing.Size(973, 576);
			tabPage4.TabIndex = 4;
			tabPage4.Text = "同步紀錄";
			tabPage4.UseVisualStyleBackColor = true;
			dgv_SynchronizeList.AllowUserToAddRows = false;
			dgv_SynchronizeList.AllowUserToDeleteRows = false;
			dgv_SynchronizeList.AllowUserToResizeColumns = false;
			dgv_SynchronizeList.AllowUserToResizeRows = false;
			dgv_SynchronizeList.BackgroundColor = System.Drawing.Color.White;
			dgv_SynchronizeList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dgv_SynchronizeList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle13.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle13.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgv_SynchronizeList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
			dgv_SynchronizeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv_SynchronizeList.Columns.AddRange(Column1, Column2, Column3, Column4);
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle14.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle14.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgv_SynchronizeList.DefaultCellStyle = dataGridViewCellStyle14;
			dgv_SynchronizeList.Dock = System.Windows.Forms.DockStyle.Bottom;
			dgv_SynchronizeList.EnableHeadersVisualStyles = false;
			dgv_SynchronizeList.Location = new System.Drawing.Point(3, 112);
			dgv_SynchronizeList.Name = "dgv_SynchronizeList";
			dgv_SynchronizeList.RowHeadersVisible = false;
			dgv_SynchronizeList.RowTemplate.Height = 35;
			dgv_SynchronizeList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgv_SynchronizeList.Size = new System.Drawing.Size(967, 461);
			dgv_SynchronizeList.TabIndex = 61;
			dgv_SynchronizeList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_SynchronizeList_CellClick);
			Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column1.HeaderText = "日期時間";
			Column1.MinimumWidth = 150;
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 150;
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column2.HeaderText = "狀態";
			Column2.MinimumWidth = 150;
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.Width = 150;
			Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			Column3.HeaderText = "更新項目";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			Column4.DefaultCellStyle = dataGridViewCellStyle15;
			Column4.HeaderText = "詳細資訊";
			Column4.MinimumWidth = 150;
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column4.Width = 150;
			tableLayoutPanel4.BackColor = System.Drawing.Color.White;
			tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel4.ColumnCount = 2;
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel4.Controls.Add(panel20, 0, 0);
			tableLayoutPanel4.Controls.Add(flowLayoutPanel12, 1, 0);
			tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.RowCount = 1;
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40f));
			tableLayoutPanel4.Size = new System.Drawing.Size(967, 63);
			tableLayoutPanel4.TabIndex = 60;
			panel20.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel20.Controls.Add(label25);
			panel20.Dock = System.Windows.Forms.DockStyle.Fill;
			panel20.Location = new System.Drawing.Point(1, 1);
			panel20.Margin = new System.Windows.Forms.Padding(0);
			panel20.Name = "panel20";
			panel20.Size = new System.Drawing.Size(162, 61);
			panel20.TabIndex = 19;
			label25.AutoSize = true;
			label25.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label25.ForeColor = System.Drawing.Color.White;
			label25.Location = new System.Drawing.Point(53, 19);
			label25.Name = "label25";
			label25.Size = new System.Drawing.Size(106, 21);
			label25.TabIndex = 0;
			label25.Text = "日期快速篩選";
			flowLayoutPanel12.Controls.Add(dtp_SynchronizeDate);
			flowLayoutPanel12.Controls.Add(btn_SynchronizeFilter);
			flowLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel12.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel12.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel12.Name = "flowLayoutPanel12";
			flowLayoutPanel12.Size = new System.Drawing.Size(802, 61);
			flowLayoutPanel12.TabIndex = 22;
			dtp_SynchronizeDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dtp_SynchronizeDate.CustomFormat = "yyyy-MM-dd";
			dtp_SynchronizeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dtp_SynchronizeDate.Location = new System.Drawing.Point(10, 13);
			dtp_SynchronizeDate.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dtp_SynchronizeDate.Name = "dtp_SynchronizeDate";
			dtp_SynchronizeDate.Size = new System.Drawing.Size(181, 33);
			dtp_SynchronizeDate.TabIndex = 5;
			dtp_SynchronizeDate.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			btn_SynchronizeFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
			btn_SynchronizeFilter.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_SynchronizeFilter.FlatAppearance.BorderSize = 0;
			btn_SynchronizeFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SynchronizeFilter.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SynchronizeFilter.ForeColor = System.Drawing.Color.White;
			btn_SynchronizeFilter.Location = new System.Drawing.Point(204, 14);
			btn_SynchronizeFilter.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			btn_SynchronizeFilter.Name = "btn_SynchronizeFilter";
			btn_SynchronizeFilter.Size = new System.Drawing.Size(102, 30);
			btn_SynchronizeFilter.TabIndex = 58;
			btn_SynchronizeFilter.Text = "篩選";
			btn_SynchronizeFilter.UseVisualStyleBackColor = false;
			btn_SynchronizeFilter.Click += new System.EventHandler(btn_SynchronizeFilter_Click);
			l_title.AutoSize = true;
			l_title.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_title.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_title.Location = new System.Drawing.Point(424, 80);
			l_title.Name = "l_title";
			l_title.Size = new System.Drawing.Size(124, 24);
			l_title.TabIndex = 59;
			l_title.Text = "本周更新紀錄";
			l_title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn5.HeaderText = "銷售時間";
			dataGridViewTextBoxColumn5.MinimumWidth = 150;
			dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			dataGridViewTextBoxColumn5.ReadOnly = true;
			dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn5.Width = 210;
			dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn6.HeaderText = "購買會員";
			dataGridViewTextBoxColumn6.MinimumWidth = 150;
			dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			dataGridViewTextBoxColumn6.ReadOnly = true;
			dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn6.Width = 200;
			dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn7.HeaderText = "銷售總價(原始)\t";
			dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			dataGridViewTextBoxColumn7.ReadOnly = true;
			dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn7.Width = 160;
			dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewTextBoxColumn8.HeaderText = "付款模式總計";
			dataGridViewTextBoxColumn8.MinimumWidth = 150;
			dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			dataGridViewTextBoxColumn8.ReadOnly = true;
			dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewTextBoxColumn8.Width = 200;
			Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column7.HeaderText = "數量";
			Column7.MinimumWidth = 60;
			Column7.Name = "Column7";
			Column7.ReadOnly = true;
			Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column7.Width = 60;
			Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column8.HeaderText = "品項";
			Column8.MinimumWidth = 60;
			Column8.Name = "Column8";
			Column8.ReadOnly = true;
			Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column8.Width = 60;
			Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column9.HeaderText = "狀態";
			Column9.MinimumWidth = 60;
			Column9.Name = "Column9";
			Column9.ReadOnly = true;
			Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column9.Width = 60;
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(btn_view_store_upload_history);
			base.Controls.Add(tabControl);
			Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "frmStatisticsRecord";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmStatisticsRecord_Load);
			base.Controls.SetChildIndex(tabControl, 0);
			base.Controls.SetChildIndex(btn_view_store_upload_history, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			tabControl.ResumeLayout(false);
			BasicData.ResumeLayout(false);
			BasicData.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgv_saleDetail).EndInit();
			((System.ComponentModel.ISupportInitialize)dgv_saleDetailTotal).EndInit();
			flowLayoutPanel19.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			flowLayoutPanel3.ResumeLayout(false);
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
			panel9.ResumeLayout(false);
			panel9.PerformLayout();
			flowLayoutPanel2.ResumeLayout(false);
			flowLayoutPanel4.ResumeLayout(false);
			flowLayoutPanel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
			flowLayoutPanel5.ResumeLayout(false);
			flowLayoutPanel5.PerformLayout();
			flowLayoutPanel6.ResumeLayout(false);
			flowLayoutPanel6.PerformLayout();
			tabPage2.ResumeLayout(false);
			tableLayoutPanel2.ResumeLayout(false);
			flowLayoutPanel7.ResumeLayout(false);
			flowLayoutPanel7.PerformLayout();
			flowLayoutPanel8.ResumeLayout(false);
			panel8.ResumeLayout(false);
			panel8.PerformLayout();
			panel10.ResumeLayout(false);
			panel10.PerformLayout();
			panel11.ResumeLayout(false);
			panel11.PerformLayout();
			panel12.ResumeLayout(false);
			panel12.PerformLayout();
			panel13.ResumeLayout(false);
			panel13.PerformLayout();
			flowLayoutPanel9.ResumeLayout(false);
			flowLayoutPanel10.ResumeLayout(false);
			flowLayoutPanel11.ResumeLayout(false);
			flowLayoutPanel11.PerformLayout();
			tabPage3.ResumeLayout(false);
			tableLayoutPanel3.ResumeLayout(false);
			flowLayoutPanel13.ResumeLayout(false);
			flowLayoutPanel13.PerformLayout();
			flowLayoutPanel14.ResumeLayout(false);
			panel14.ResumeLayout(false);
			panel14.PerformLayout();
			panel15.ResumeLayout(false);
			panel15.PerformLayout();
			panel16.ResumeLayout(false);
			panel16.PerformLayout();
			panel17.ResumeLayout(false);
			panel17.PerformLayout();
			panel18.ResumeLayout(false);
			panel18.PerformLayout();
			panel19.ResumeLayout(false);
			panel19.PerformLayout();
			flowLayoutPanel15.ResumeLayout(false);
			flowLayoutPanel16.ResumeLayout(false);
			flowLayoutPanel17.ResumeLayout(false);
			flowLayoutPanel18.ResumeLayout(false);
			flowLayoutPanel18.PerformLayout();
			tabPage5.ResumeLayout(false);
			tabPage5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgv_DeliveryTotal).EndInit();
			((System.ComponentModel.ISupportInitialize)dgv_DeliveryDetail).EndInit();
			tabPage6.ResumeLayout(false);
			tableLayoutPanel5.ResumeLayout(false);
			flowLayoutPanel20.ResumeLayout(false);
			flowLayoutPanel20.PerformLayout();
			flowLayoutPanel21.ResumeLayout(false);
			panel7.ResumeLayout(false);
			panel7.PerformLayout();
			panel21.ResumeLayout(false);
			panel21.PerformLayout();
			panel22.ResumeLayout(false);
			panel22.PerformLayout();
			panel23.ResumeLayout(false);
			panel23.PerformLayout();
			panel24.ResumeLayout(false);
			panel24.PerformLayout();
			panel25.ResumeLayout(false);
			panel25.PerformLayout();
			flowLayoutPanel22.ResumeLayout(false);
			flowLayoutPanel23.ResumeLayout(false);
			flowLayoutPanel24.ResumeLayout(false);
			flowLayoutPanel25.ResumeLayout(false);
			flowLayoutPanel25.PerformLayout();
			tabPage7.ResumeLayout(false);
			tableLayoutPanel6.ResumeLayout(false);
			flowLayoutPanel26.ResumeLayout(false);
			flowLayoutPanel26.PerformLayout();
			flowLayoutPanel27.ResumeLayout(false);
			panel26.ResumeLayout(false);
			panel26.PerformLayout();
			panel27.ResumeLayout(false);
			panel27.PerformLayout();
			panel28.ResumeLayout(false);
			panel28.PerformLayout();
			panel29.ResumeLayout(false);
			panel29.PerformLayout();
			panel30.ResumeLayout(false);
			panel30.PerformLayout();
			flowLayoutPanel28.ResumeLayout(false);
			flowLayoutPanel29.ResumeLayout(false);
			flowLayoutPanel30.ResumeLayout(false);
			flowLayoutPanel30.PerformLayout();
			tabPage4.ResumeLayout(false);
			tabPage4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgv_SynchronizeList).EndInit();
			tableLayoutPanel4.ResumeLayout(false);
			panel20.ResumeLayout(false);
			panel20.PerformLayout();
			flowLayoutPanel12.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
