using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public class ucDeliveryOrderInfoDetail : UserControl
	{
		private IContainer components;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column3;

		public ucDeliveryOrderInfoDetail()
		{
			InitializeComponent();
		}

		public void setSellNo(string str)
		{
			Column1.HeaderText = str;
			dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
		}

		public void setCommodityInfo(string str)
		{
			Column3.HeaderText = str;
			dataGridView1.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
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
			dataGridView1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			dataGridView1.BackgroundColor = System.Drawing.Color.White;
			dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 240, 230);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 192);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dataGridView1.ColumnHeadersHeight = 64;
			dataGridView1.Columns.AddRange(Column1, Column3);
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.Location = new System.Drawing.Point(0, 0);
			dataGridView1.Margin = new System.Windows.Forms.Padding(0);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 24;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView1.Size = new System.Drawing.Size(570, 72);
			dataGridView1.TabIndex = 8;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			Column1.DefaultCellStyle = dataGridViewCellStyle2;
			Column1.HeaderText = "商品資訊";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 801;
			Column3.HeaderText = "數量";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column3.Width = 180;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.MenuBar;
			base.Controls.Add(dataGridView1);
			base.Margin = new System.Windows.Forms.Padding(0);
			base.Name = "ucDeliveryOrderInfoDetail";
			base.Size = new System.Drawing.Size(570, 38);
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
		}
	}
}
