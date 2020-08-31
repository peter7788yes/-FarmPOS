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
	public class frmSearchVendorDeliverySummary : MasterThinForm
	{
		private DataTable _dt_summary;

		private DataTable _dt_details;

		private List<string> _lst_selmember = new List<string>();

		private HSSFWorkbook wb;

		private HSSFSheet sh;

		private string str_file_location = "\\";

		private string str_file_name = "report_VendorDelivery_";

		private string str_file_type = ".xls";

		private List<string> lst_Sheet = new List<string>();

		private string _strFromDate = "";

		private string _strToDate = "";

		private string _member_name = "";

		private string _member_type = "";

		private string _member_status = "";

		private IContainer components;

		private Label label1;

		private Label label2;

		private Label label3;

		private DataGridView dataGridView1;

		private DataGridView dataGridView2;

		private Button btn_ExportCustflowReport;

		private Button button2;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column16;

		private DataGridViewTextBoxColumn Column8;

		private DataGridViewTextBoxColumn Column9;

		private DataGridViewTextBoxColumn Column10;

		private DataGridViewTextBoxColumn Column11;

		private DataGridViewTextBoxColumn Column17;

		public frmSearchVendorDeliverySummary(DataTable dt_summary, DataTable dt_details, string strFromDate, string strToDate, string member_name, string member_type, string member_status)
			: base("報表查詢")
		{
			_dt_details = dt_details;
			_strFromDate = strFromDate;
			_strToDate = strToDate;
			_member_name = member_name;
			_member_type = member_type;
			_member_status = member_status;
			string text = "出貨廠商:";
			InitializeComponent();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			label3.Text = strFromDate + " ~ " + strToDate;
			if (_dt_details.Rows.Count > 0)
			{
				for (int i = 0; i < _dt_details.Rows.Count; i++)
				{
					int result = 0;
					int.TryParse(_dt_details.Rows[i]["OriSum"].ToString(), out result);
					num += result;
					int.TryParse(_dt_details.Rows[i]["CurSum"].ToString(), out result);
					num2 += result;
					int.TryParse(_dt_details.Rows[i]["DeliveryCount"].ToString(), out result);
					num3 += result;
					int.TryParse(_dt_details.Rows[i]["items"].ToString(), out result);
					num4 += result;
					int.TryParse(_dt_details.Rows[i]["itemstotal"].ToString(), out result);
					num5 += result;
					text = text + "[" + (string.IsNullOrEmpty(_dt_details.Rows[i]["SupplierName"].ToString()) ? "" : _dt_details.Rows[i]["SupplierName"].ToString()) + "]";
					string text2 = _dt_details.Rows[i]["SupplierName"].ToString() + "(" + _dt_details.Rows[i]["vendorId"].ToString() + "/" + _dt_details.Rows[i]["vendorName"].ToString() + ")";
					string text3 = _dt_details.Rows[i]["CurSum"].ToString() + "(" + _dt_details.Rows[i]["OriSum"].ToString() + ")";
					dataGridView2.Rows.Add(text2, text3, string.IsNullOrEmpty(_dt_details.Rows[i]["DeliveryCount"].ToString()) ? "" : _dt_details.Rows[i]["DeliveryCount"].ToString(), string.IsNullOrEmpty(_dt_details.Rows[i]["items"].ToString()) ? "" : _dt_details.Rows[i]["items"].ToString(), string.IsNullOrEmpty(_dt_details.Rows[i]["itemstotal"].ToString()) ? "" : _dt_details.Rows[i]["itemstotal"].ToString());
				}
			}
			label2.Text = text;
			string text4 = num2 + "(" + num + ")";
			dataGridView1.Rows.Add(text4, num3.ToString(), num4.ToString(), num5.ToString());
		}

		private void button2_Click(object sender, EventArgs e)
		{
			switchForm(new frmStatisticsRecord());
		}

		private void btn_ExportCustflowReport_Click(object sender, EventArgs e)
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
				string[] array = new string[9]
				{
					"日期:",
					_strFromDate + "~" + _strToDate,
					"特定廠商:",
					_member_name,
					"廠商類型:",
					_member_type,
					"廠商狀態:",
					_member_status,
					""
				};
				string[] array2 = new string[9]
				{
					"出貨對象(廠商)",
					"出貨總額（原始）",
					"出貨次",
					"品項數",
					"出貨數量",
					" ",
					" ",
					" ",
					" "
				};
				int num2 = (array.Length > array2.Length) ? array.Length : array2.Length;
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
			label3 = new System.Windows.Forms.Label();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			dataGridView2 = new System.Windows.Forms.DataGridView();
			btn_ExportCustflowReport = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			label1.Text = "廠商出貨統計";
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.Black;
			label2.Location = new System.Drawing.Point(162, 49);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(616, 21);
			label2.TabIndex = 53;
			label2.Text = "出貨對象:";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.Black;
			label3.Location = new System.Drawing.Point(13, 77);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(210, 21);
			label3.TabIndex = 54;
			label3.Text = "2017-01-01~2017-08-30";
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
			dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column16);
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
			dataGridView2.Columns.AddRange(Column8, Column9, Column10, Column11, Column17);
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
			btn_ExportCustflowReport.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_ExportCustflowReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_ExportCustflowReport.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_ExportCustflowReport.ForeColor = System.Drawing.Color.White;
			btn_ExportCustflowReport.Location = new System.Drawing.Point(784, 46);
			btn_ExportCustflowReport.Name = "btn_ExportCustflowReport";
			btn_ExportCustflowReport.Size = new System.Drawing.Size(90, 40);
			btn_ExportCustflowReport.TabIndex = 63;
			btn_ExportCustflowReport.Text = "匯出報表";
			btn_ExportCustflowReport.UseVisualStyleBackColor = false;
			btn_ExportCustflowReport.Click += new System.EventHandler(btn_ExportCustflowReport_Click);
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
			Column1.HeaderText = "出貨總額(原始)";
			Column1.MinimumWidth = 200;
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 300;
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column2.HeaderText = "總出貨次數";
			Column2.MinimumWidth = 150;
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.Width = 240;
			Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column3.HeaderText = "總出貨品項數";
			Column3.MinimumWidth = 150;
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column3.Width = 240;
			Column16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			Column16.HeaderText = "總出貨商品數量";
			Column16.Name = "Column16";
			Column16.ReadOnly = true;
			Column16.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column16.Width = 200;
			Column8.HeaderText = "出貨對象(廠商)";
			Column8.Name = "Column8";
			Column8.ReadOnly = true;
			Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column8.Width = 400;
			Column9.HeaderText = "出貨總額(原始)";
			Column9.Name = "Column9";
			Column9.ReadOnly = true;
			Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column9.Width = 250;
			Column10.HeaderText = "出貨次";
			Column10.Name = "Column10";
			Column10.ReadOnly = true;
			Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column10.Width = 110;
			Column11.HeaderText = "品項數";
			Column11.Name = "Column11";
			Column11.ReadOnly = true;
			Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column11.Width = 110;
			Column17.HeaderText = "出貨數量";
			Column17.Name = "Column17";
			Column17.ReadOnly = true;
			Column17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column17.Width = 110;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(button2);
			base.Controls.Add(btn_ExportCustflowReport);
			base.Controls.Add(dataGridView2);
			base.Controls.Add(dataGridView1);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Name = "frmSearchVendorDeliverySummary";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(dataGridView1, 0);
			base.Controls.SetChildIndex(dataGridView2, 0);
			base.Controls.SetChildIndex(btn_ExportCustflowReport, 0);
			base.Controls.SetChildIndex(button2, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
