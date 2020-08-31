using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace POS_Client
{
	public class frmSearchPeriodTransactions : MasterThinForm
	{
		private DataTable _dt_summary;

		private DataTable _dt_details;

		private HSSFWorkbook wb;

		private HSSFSheet sh;

		private string str_file_location = "\\";

		private string str_file_name = "report_trade_";

		private string str_file_type = ".xls";

		private List<string> lst_Sheet = new List<string>();

		private string _strFromDate = "";

		private string _strToDate = "";

		private string _sales_status = "";

		private string _sales_cash_credit = "";

		private string _sales_total_range = "";

		private IContainer components;

		private Label label1;

		private Label label2;

		private Label label4;

		private DataGridView dataGridView1;

		private DataGridView dataGridView2;

		private Button btn_ExportTradeReport;

		private Button button2;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column7;

		private DataGridViewTextBoxColumn Column8;

		private DataGridViewTextBoxColumn Column10;

		private DataGridViewTextBoxColumn Column11;

		private DataGridViewTextBoxColumn Column12;

		private DataGridViewTextBoxColumn Column9;

		private DataGridViewTextBoxColumn Column13;

		private DataGridViewTextBoxColumn Column14;

		private DataGridViewTextBoxColumn Column16;

		public frmSearchPeriodTransactions(DataTable dt_summary, DataTable dt_details, string strFromDate, string strToDate, string status, string CashCreditMode, string ReportMode, string sales_total_range)
			: base("報表查詢")
		{
			_dt_summary = dt_summary;
			_dt_details = dt_details;
			_strFromDate = strFromDate;
			_strToDate = strToDate;
			_sales_status = status;
			_sales_cash_credit = CashCreditMode;
			_sales_total_range = sales_total_range;
			InitializeComponent();
			label2.Text = "訂單狀態:" + status;
			Label label = label2;
			label.Text = label.Text + "  結帳模式:" + CashCreditMode;
			label4.Text = strFromDate + " ~ " + strToDate + "/" + ReportMode;
			if (_dt_summary.Rows.Count > 0)
			{
				for (int i = 0; i < _dt_summary.Rows.Count; i++)
				{
					int result = 0;
					int result2 = 0;
					int result3 = 0;
					int.TryParse(_dt_summary.Rows[i]["cash"].ToString(), out result);
					int.TryParse(_dt_summary.Rows[i]["Credit"].ToString(), out result2);
					int.TryParse(_dt_summary.Rows[i]["returnChange"].ToString(), out result3);
					dataGridView1.Rows.Add(string.IsNullOrEmpty(_dt_summary.Rows[i]["sum"].ToString()) ? "0" : (_dt_summary.Rows[i]["sum"].ToString() + "(" + (result + result2 - result3) + ")"), result.ToString(), result2.ToString(), result3.ToString(), string.IsNullOrEmpty(_dt_summary.Rows[i]["Refund"].ToString()) ? "0" : _dt_summary.Rows[i]["Refund"].ToString(), string.IsNullOrEmpty(_dt_summary.Rows[i]["consumptionTimes"].ToString()) ? "0" : _dt_summary.Rows[i]["consumptionTimes"].ToString(), string.IsNullOrEmpty(_dt_summary.Rows[i]["itemstotal"].ToString()) ? "0" : _dt_summary.Rows[i]["itemstotal"].ToString());
				}
			}
			if (_dt_details.Rows.Count <= 0)
			{
				return;
			}
			for (int j = 0; j < _dt_details.Rows.Count; j++)
			{
				int result4 = 0;
				int result5 = 0;
				int result6 = 0;
				string text = "";
				int.TryParse(_dt_details.Rows[j]["cash"].ToString(), out result4);
				int.TryParse(_dt_details.Rows[j]["Credit"].ToString(), out result5);
				int.TryParse(_dt_details.Rows[j]["returnChange"].ToString(), out result6);
				switch (ReportMode)
				{
				case "[月報表]":
					text = (string.IsNullOrEmpty(_dt_details.Rows[j]["Time"].ToString()) ? "0" : _dt_details.Rows[j]["Time"].ToString());
					break;
				case "[週報表]":
					text = (string.IsNullOrEmpty(_dt_details.Rows[j]["Time"].ToString()) ? "0" : _dt_details.Rows[j]["Time"].ToString());
					text += "第";
					text += (string.IsNullOrEmpty(_dt_details.Rows[j]["week"].ToString()) ? "0" : _dt_details.Rows[j]["week"].ToString());
					text += "週";
					break;
				case "[日報表]":
					text = (string.IsNullOrEmpty(_dt_details.Rows[j]["Time"].ToString()) ? "0" : _dt_details.Rows[j]["Time"].ToString());
					break;
				}
				dataGridView2.Rows.Add(text, string.IsNullOrEmpty(_dt_details.Rows[j]["sum"].ToString()) ? "0" : (_dt_details.Rows[j]["sum"].ToString() + "(" + (result4 + result5 - result6) + ")"), result4.ToString(), result5.ToString(), result6.ToString(), string.IsNullOrEmpty(_dt_details.Rows[j]["Refund"].ToString()) ? "0" : _dt_details.Rows[j]["Refund"].ToString(), string.IsNullOrEmpty(_dt_details.Rows[j]["consumptionTimes"].ToString()) ? "0" : _dt_details.Rows[j]["consumptionTimes"].ToString(), string.IsNullOrEmpty(_dt_details.Rows[j]["itemstotal"].ToString()) ? "0" : _dt_details.Rows[j]["itemstotal"].ToString());
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			switchForm(new frmStatisticsRecord());
		}

		private void btn_ExportTradeReport_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			string selectedPath = folderBrowserDialog.SelectedPath;
			string text = selectedPath + str_file_location + str_file_name + _strFromDate.Replace("-", "") + "-" + _strToDate.Replace("-", "") + str_file_type;
			FileInfo file = new FileInfo(text);
			int num = 2;
			if (!File.Exists(text))
			{
				wb = HSSFWorkbook.Create(InternalWorkbook.CreateWorkbook());
				sh = (HSSFSheet)wb.CreateSheet("Sheet1");
				for (int i = 0; i < dataGridView2.RowCount + num; i++)
				{
					IRow row = sh.CreateRow(i);
					for (int j = 0; j < dataGridView2.ColumnCount; j++)
					{
						row.CreateCell(j);
					}
				}
				using (FileStream @out = new FileStream(text, FileMode.Create, FileAccess.Write))
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
				int num2 = 0;
				string[] array = new string[9]
				{
					"日期:",
					_strFromDate + "~" + _strToDate,
					"訂單狀態:",
					_sales_status,
					"結帳模式:",
					_sales_cash_credit,
					"銷售單總價範圍:",
					_sales_total_range,
					""
				};
				string[] array2 = new string[9]
				{
					"時間",
					"銷售總額（原始）",
					"現金收款",
					"賒帳金額",
					"找零",
					"退款金額",
					"總客次",
					"銷售數量",
					""
				};
				num2 = ((array.Length > array2.Length) ? array.Length : array2.Length);
				for (; l < num; l++)
				{
					for (int m = 0; m < num2; m++)
					{
						if (sh.GetRow(l).GetCell(m) == null)
						{
							sh.GetRow(l).CreateCell(m);
						}
						if (l == 0)
						{
							sh.GetRow(l).GetCell(m).SetCellValue(array[m]);
						}
						if (l == 1)
						{
							sh.GetRow(l).GetCell(m).SetCellValue(array2[m]);
						}
					}
				}
				for (int n = 0; n < dataGridView2.RowCount; n++)
				{
					if (sh.GetRow(l) == null)
					{
						sh.CreateRow(l);
					}
					for (int num3 = 0; num3 < dataGridView2.ColumnCount; num3++)
					{
						if (sh.GetRow(l).GetCell(num3) == null)
						{
							sh.GetRow(l).CreateCell(num3);
						}
						if (dataGridView2[num3, n].Value != null)
						{
							sh.GetRow(l).GetCell(num3).SetCellValue(dataGridView2[num3, n].Value.ToString());
						}
					}
					l++;
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
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			dataGridView2 = new System.Windows.Forms.DataGridView();
			btn_ExportTradeReport = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
			SuspendLayout();
			pb_virtualKeyBoard.Visible = false;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.Black;
			label1.Location = new System.Drawing.Point(12, 44);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(144, 28);
			label1.TabIndex = 52;
			label1.Text = "期間交易營收";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.Black;
			label2.Location = new System.Drawing.Point(171, 48);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(104, 21);
			label2.TabIndex = 53;
			label2.Text = "訂單狀態:{0};";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.Black;
			label4.Location = new System.Drawing.Point(15, 77);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(271, 21);
			label4.TabIndex = 55;
			label4.Text = "2017-01-01~2017-08-31/{3}報表";
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column6, Column4, Column5, Column7);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.Location = new System.Drawing.Point(0, 101);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 35;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView1.Size = new System.Drawing.Size(981, 72);
			dataGridView1.TabIndex = 61;
			dataGridView2.AllowUserToAddRows = false;
			dataGridView2.AllowUserToDeleteRows = false;
			dataGridView2.AllowUserToResizeColumns = false;
			dataGridView2.AllowUserToResizeRows = false;
			dataGridView2.BackgroundColor = System.Drawing.Color.White;
			dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView2.Columns.AddRange(Column8, Column10, Column11, Column12, Column9, Column13, Column14, Column16);
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView2.DefaultCellStyle = dataGridViewCellStyle4;
			dataGridView2.EnableHeadersVisualStyles = false;
			dataGridView2.Location = new System.Drawing.Point(0, 188);
			dataGridView2.Name = "dataGridView2";
			dataGridView2.ReadOnly = true;
			dataGridView2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridView2.RowHeadersVisible = false;
			dataGridView2.RowTemplate.Height = 40;
			dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView2.Size = new System.Drawing.Size(981, 469);
			dataGridView2.TabIndex = 62;
			btn_ExportTradeReport.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_ExportTradeReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_ExportTradeReport.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_ExportTradeReport.ForeColor = System.Drawing.Color.White;
			btn_ExportTradeReport.Location = new System.Drawing.Point(784, 46);
			btn_ExportTradeReport.Name = "btn_ExportTradeReport";
			btn_ExportTradeReport.Size = new System.Drawing.Size(90, 40);
			btn_ExportTradeReport.TabIndex = 63;
			btn_ExportTradeReport.Text = "匯出報表";
			btn_ExportTradeReport.UseVisualStyleBackColor = false;
			btn_ExportTradeReport.Click += new System.EventHandler(btn_ExportTradeReport_Click);
			button2.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button2.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button2.ForeColor = System.Drawing.Color.White;
			button2.Location = new System.Drawing.Point(880, 46);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(90, 40);
			button2.TabIndex = 64;
			button2.Text = "返回查詢";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(button2_Click);
			Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column1.HeaderText = "銷售總額(原始)";
			Column1.MinimumWidth = 200;
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 200;
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column2.HeaderText = "現金收款";
			Column2.MinimumWidth = 130;
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.Width = 130;
			Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column3.HeaderText = "賒帳金額";
			Column3.MinimumWidth = 130;
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column3.Width = 130;
			Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column6.HeaderText = "找零";
			Column6.Name = "Column6";
			Column6.ReadOnly = true;
			Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column6.Width = 130;
			Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column4.HeaderText = "退款金額";
			Column4.MinimumWidth = 150;
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column4.Width = 160;
			Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column5.HeaderText = "總客次";
			Column5.MinimumWidth = 75;
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column5.Width = 110;
			Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column7.HeaderText = "銷售數量";
			Column7.MinimumWidth = 75;
			Column7.Name = "Column7";
			Column7.ReadOnly = true;
			Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column7.Width = 130;
			Column8.HeaderText = "時間";
			Column8.Name = "Column8";
			Column8.ReadOnly = true;
			Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column8.Width = 150;
			Column10.HeaderText = "銷售總額(原始)";
			Column10.Name = "Column10";
			Column10.ReadOnly = true;
			Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column10.Width = 200;
			Column11.HeaderText = "現金收款";
			Column11.Name = "Column11";
			Column11.ReadOnly = true;
			Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column11.Width = 130;
			Column12.HeaderText = "賒帳金額";
			Column12.Name = "Column12";
			Column12.ReadOnly = true;
			Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column12.Width = 130;
			Column9.HeaderText = "找零";
			Column9.Name = "Column9";
			Column9.ReadOnly = true;
			Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column9.Width = 90;
			Column13.HeaderText = "退款金額";
			Column13.Name = "Column13";
			Column13.ReadOnly = true;
			Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column14.HeaderText = "總客次";
			Column14.Name = "Column14";
			Column14.ReadOnly = true;
			Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column14.Width = 80;
			Column16.HeaderText = "銷售數量";
			Column16.Name = "Column16";
			Column16.ReadOnly = true;
			Column16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(button2);
			base.Controls.Add(btn_ExportTradeReport);
			base.Controls.Add(dataGridView2);
			base.Controls.Add(dataGridView1);
			base.Controls.Add(label4);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Name = "frmSearchPeriodTransactions";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(dataGridView1, 0);
			base.Controls.SetChildIndex(dataGridView2, 0);
			base.Controls.SetChildIndex(btn_ExportTradeReport, 0);
			base.Controls.SetChildIndex(button2, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
