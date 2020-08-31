using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class ucDeliveryOrderInfo : UserControl
	{
		private IContainer components;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn Column6;

		private DataGridViewTextBoxColumn Column7;

		public ucDeliveryOrderInfo()
		{
			InitializeComponent();
		}

		public void setDeliveryNo(string str)
		{
			Column1.HeaderText = str;
		}

		public void setVendor(string str)
		{
			Column2.HeaderText = str;
		}

		public void setSum(string str)
		{
			Column3.HeaderText = str;
		}

		public void setItems(string str)
		{
			Column5.HeaderText = str;
		}

		public void setNum(string str)
		{
			Column6.HeaderText = str;
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
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column5, Column6, Column7);
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.Location = new System.Drawing.Point(0, 0);
			dataGridView1.Margin = new System.Windows.Forms.Padding(0);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 24;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView1.Size = new System.Drawing.Size(570, 41);
			dataGridView1.TabIndex = 7;
			Column1.HeaderText = "出貨單號";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 210;
			Column2.HeaderText = "業者";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.Width = 360;
			Column3.HeaderText = "出貨總額(原始)";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column3.Width = 160;
			Column5.HeaderText = "品項";
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column5.Width = 70;
			Column6.HeaderText = "數量";
			Column6.Name = "Column6";
			Column6.ReadOnly = true;
			Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column6.Width = 80;
			Column7.HeaderText = "狀態";
			Column7.Name = "Column7";
			Column7.ReadOnly = true;
			Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column7.Width = 80;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.ActiveBorder;
			base.Controls.Add(dataGridView1);
			base.Margin = new System.Windows.Forms.Padding(0);
			base.Name = "ucDeliveryOrderInfo";
			base.Size = new System.Drawing.Size(570, 20);
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
		}
	}
}
