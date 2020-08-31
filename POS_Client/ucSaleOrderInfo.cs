using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class ucSaleOrderInfo : UserControl
	{
		private IContainer components;

		private DataGridView dataGridView1;

		private DataGridViewLinkColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewTextBoxColumn Column7;

		public ucSaleOrderInfo()
		{
			InitializeComponent();
		}

		public void setSellNo(string str)
		{
			Column1.HeaderText = str;
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			dataGridViewCellStyle.Font = new Font("微軟正黑體", 12f, FontStyle.Underline, GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = Color.Blue;
			dataGridView1.Columns[0].HeaderCell.Style = dataGridViewCellStyle;
		}

		public void setName(string str)
		{
			Column2.HeaderText = str;
		}

		public void setSum(string str)
		{
			Column3.HeaderText = str;
		}

		public void setCashCredit(string str)
		{
			Column4.HeaderText = str;
		}

		public void setItems(string str)
		{
			Column6.HeaderText = str;
		}

		public void setNum(string str)
		{
			Column5.HeaderText = str;
		}

		public void setStatus(string str)
		{
			Column7.HeaderText = str;
		}

		public void setBackColor(string str)
		{
			if (str == "odd_cellstyle")
			{
				DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
				dataGridViewCellStyle = dataGridView1.ColumnHeadersDefaultCellStyle;
				dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridViewCellStyle.BackColor = Color.FromArgb(255, 240, 150);
				dataGridViewCellStyle.Font = new Font("微軟正黑體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
				dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
				dataGridViewCellStyle.Padding = new Padding(6);
				dataGridViewCellStyle.SelectionBackColor = Color.FromArgb(255, 192, 192);
				dataGridViewCellStyle.SelectionForeColor = Color.Black;
				dataGridViewCellStyle.WrapMode = DataGridViewTriState.False;
				dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			}
			else if (str == "even_cellstyle")
			{
				DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
				dataGridViewCellStyle2 = dataGridView1.ColumnHeadersDefaultCellStyle;
				dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 228, 181);
				dataGridViewCellStyle2.Font = new Font("微軟正黑體", 12f, FontStyle.Regular, GraphicsUnit.Point, 136);
				dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
				dataGridViewCellStyle2.Padding = new Padding(6);
				dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(255, 240, 230);
				dataGridViewCellStyle2.SelectionForeColor = Color.Black;
				dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
				dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			}
		}

		public void setForeColor(Color color)
		{
		}

		public void setTitleTextAlign()
		{
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == 0 && e.RowIndex == -1)
				{
					string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText;
					string sql = "SELECT ms.sellNo as sellNo, ms.sellTime, cr.Name as Name, ms.memberId as memberId, (ms.sum-ms.sumDiscount) as sum, ms.cash as cash, ms.Credit as Credit, ms.items as items, ms.itemstotal as itemstotal, ms.status as status, ds.sellNoCount as sellNoCount, ms.returnChange as returnChange FROM hypos_main_sell as ms LEFT JOIN hypos_CUST_RTL as cr on cr.VipNo = ms.memberId INNER JOIN ( SELECT ds.sellNo as sellNo, count(ds.sellNo) as sellNoCount FROM hypos_detail_sell as ds GROUP BY ds.sellNo ) as ds on ms.sellNo = ds.sellNo WHERE ms.sellTime  ='" + headerText + "'  ORDER BY ms.sellTime desc";
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
					string sellno = "";
					if (dataTable.Rows.Count > 0)
					{
						sellno = (string.IsNullOrEmpty(dataTable.Rows[0]["sellNo"].ToString()) ? "0" : dataTable.Rows[0]["sellNo"].ToString());
					}
					if (Program.SystemMode == 1)
					{
						frmMainShopSimpleReturn frmMainShopSimpleReturn = new frmMainShopSimpleReturn(sellno, "frmStatisticsRecord", "");
						frmMainShopSimpleReturn.frmName = base.Name;
						frmMainShopSimpleReturn.Location = new Point(base.Location.X, base.Location.Y);
						frmMainShopSimpleReturn.Show();
						frmMainShopSimpleReturn.Focus();
					}
					else
					{
						frmMainShopSimpleReturnWithMoney frmMainShopSimpleReturnWithMoney = new frmMainShopSimpleReturnWithMoney(sellno, "frmStatisticsRecord", "");
						frmMainShopSimpleReturnWithMoney.frmName = base.Name;
						frmMainShopSimpleReturnWithMoney.Location = new Point(base.Location.X, base.Location.Y);
						frmMainShopSimpleReturnWithMoney.Show();
						frmMainShopSimpleReturnWithMoney.Focus();
					}
				}
			}
			catch (Exception ex)
			{
				ex.ToString();
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
			dataGridView1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewLinkColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.BackgroundColor = System.Drawing.Color.White;
			dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 228, 181);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column4, Column5, Column6, Column7);
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.Location = new System.Drawing.Point(0, 0);
			dataGridView1.Margin = new System.Windows.Forms.Padding(0);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 24;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView1.Size = new System.Drawing.Size(570, 41);
			dataGridView1.TabIndex = 7;
			dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
			Column1.HeaderText = "銷售單號";
			Column1.Name = "Column1";
			Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column1.Width = 210;
			Column2.HeaderText = "購買會員";
			Column2.Name = "Column2";
			Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.Width = 200;
			Column3.HeaderText = "銷售總額(原始)";
			Column3.Name = "Column3";
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column3.Width = 160;
			Column4.HeaderText = "付款模式總計";
			Column4.Name = "Column4";
			Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column4.Width = 200;
			Column5.HeaderText = "數量";
			Column5.Name = "Column5";
			Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column5.Width = 60;
			Column6.HeaderText = "品項";
			Column6.Name = "Column6";
			Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column6.Width = 60;
			Column7.HeaderText = "狀態";
			Column7.Name = "Column7";
			Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column7.Width = 60;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.ActiveBorder;
			base.Controls.Add(dataGridView1);
			base.Margin = new System.Windows.Forms.Padding(0);
			base.Name = "ucSaleOrderInfo";
			base.Size = new System.Drawing.Size(570, 20);
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
		}
	}
}
