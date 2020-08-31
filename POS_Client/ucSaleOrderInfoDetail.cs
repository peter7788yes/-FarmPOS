using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class ucSaleOrderInfoDetail : UserControl
	{
		private IContainer components;

		private DataGridView dataGridView1;

		private DataGridViewLinkColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		public ucSaleOrderInfoDetail()
		{
			InitializeComponent();
		}

		public void setSellNo(string str)
		{
			Column1.HeaderText = str;
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			dataGridViewCellStyle.Font = new Font("微軟正黑體", 11f, FontStyle.Underline, GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = Color.Blue;
			dataGridView1.Columns[0].HeaderCell.Style = dataGridViewCellStyle;
		}

		public void setCommodityInfo(string str)
		{
			Column2.HeaderText = str;
		}

		public void setNum(string str)
		{
			Column3.HeaderText = str;
		}

		public void setBackColor(string str)
		{
			if (str == "odd_cellstyle")
			{
				DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
				dataGridViewCellStyle = dataGridView1.ColumnHeadersDefaultCellStyle;
				dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
				dataGridViewCellStyle.BackColor = Color.FromArgb(255, 240, 150);
				dataGridViewCellStyle.Font = new Font("微軟正黑體", 11f, FontStyle.Regular, GraphicsUnit.Point, 136);
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
				dataGridViewCellStyle2.Font = new Font("微軟正黑體", 11f, FontStyle.Regular, GraphicsUnit.Point, 136);
				dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
				dataGridViewCellStyle2.Padding = new Padding(6);
				dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(255, 240, 230);
				dataGridViewCellStyle2.SelectionForeColor = Color.Black;
				dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
				dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == 0 && e.RowIndex == -1)
				{
					string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText;
					string sql = "SELECT ds1.barcode as barcode, ds1.sellNo as sellNo, sellTime, ds1.sellTime, gl.GDName as GDName, gl.CName as CName, gl.formCode as formCode, gl.contents as contents, gl.brandName as brandName, gl.spec as spec, gl.capacity as capacity, ds1.num as num FROM ( SELECT ds.* , ms.sellTime FROM hypos_detail_sell as ds INNER JOIN hypos_main_sell as ms on ds.sellNo = ms.sellNo WHERE ms.sellTime ='" + headerText + "' ORDER BY ms.sellTime desc, ds.sellDeatialId desc  ) as ds1 LEFT JOIN hypos_GOODSLST as gl on ds1.barcode = gl.GDSNO";
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewLinkColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 240, 230);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(Column1, Column2, Column3);
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.Location = new System.Drawing.Point(0, 0);
			dataGridView1.Margin = new System.Windows.Forms.Padding(0);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 24;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView1.Size = new System.Drawing.Size(570, 41);
			dataGridView1.TabIndex = 8;
			dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			Column1.DefaultCellStyle = dataGridViewCellStyle2;
			Column1.HeaderText = "銷售單號";
			Column1.Name = "Column1";
			Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column1.Width = 210;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			Column2.DefaultCellStyle = dataGridViewCellStyle3;
			Column2.HeaderText = "商品名稱[農藥普通名稱-劑型．含量-商品廠商] 容器 容量";
			Column2.Name = "Column2";
			Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.Width = 560;
			Column3.HeaderText = "數量";
			Column3.Name = "Column3";
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column3.Width = 190;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.MenuBar;
			base.Controls.Add(dataGridView1);
			base.Margin = new System.Windows.Forms.Padding(0);
			base.Name = "ucSaleOrderInfoDetail";
			base.Size = new System.Drawing.Size(570, 20);
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
		}
	}
}
