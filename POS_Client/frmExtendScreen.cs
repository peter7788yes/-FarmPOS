using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Client
{
	public sealed class frmExtendScreen : Form
	{
		private static bool IsShown;

		private static readonly frmExtendScreen MyInstance;

		private IContainer components;

		private DataGridView dataGridView1;

		private TextBox textBox1;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		public static frmExtendScreen OnlyInstance
		{
			get
			{
				return MyInstance;
			}
		}

		private frmExtendScreen()
		{
			InitializeComponent();
			dataGridView1.RowTemplate.Height = 80;
			dataGridView1.Height -= textBox1.Height;
			int num = Screen.AllScreens.Length;
			Screen[] allScreens = Screen.AllScreens;
			for (int i = 0; i < allScreens.Length; i++)
			{
				Console.WriteLine(allScreens[i]);
			}
			base.FormBorderStyle = FormBorderStyle.None;
			base.WindowState = FormWindowState.Maximized;
			base.TopMost = false;
			base.StartPosition = FormStartPosition.Manual;
			base.DesktopLocation = new Point(Screen.PrimaryScreen.Bounds.Width, 0);
		}

		static frmExtendScreen()
		{
			IsShown = false;
			MyInstance = new frmExtendScreen();
			OnlyInstance.FormClosing += new FormClosingEventHandler(OnlyInstance_FormClosing);
		}

		public new void Show()
		{
			if (IsShown)
			{
				base.Show();
				return;
			}
			base.Show();
			IsShown = true;
		}

		private static void OnlyInstance_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			IsShown = false;
			OnlyInstance.Hide();
		}

		public static void setCommodityInfo(string[] data)
		{
			OnlyInstance.dataGridView1.Rows.Add(data[0], data[1], data[2], data[3]);
			OnlyInstance.dataGridView1.FirstDisplayedScrollingRowIndex = OnlyInstance.dataGridView1.RowCount - 1;
		}

		public static void RemoveAt(int idx)
		{
			OnlyInstance.dataGridView1.Rows.RemoveAt(idx);
		}

		public static void RemoveAll()
		{
			OnlyInstance.dataGridView1.Rows.Clear();
			OnlyInstance.dataGridView1.Refresh();
			OnlyInstance.textBox1.Text = "總計：0 元";
		}

		public static void setTotal(string total)
		{
			OnlyInstance.textBox1.Text = "總計：" + total + " 元";
		}

		public static void CommodityAddOne(int idx, string sum)
		{
			int num = int.Parse(OnlyInstance.dataGridView1.Rows[idx].Cells["Column3"].Value.ToString()) + 1;
			OnlyInstance.dataGridView1.Rows[idx].Cells["Column3"].Value = num.ToString();
			OnlyInstance.dataGridView1.Rows[idx].Cells["Column4"].Value = sum;
		}

		public static void CommoditySubOne(int idx, string sum)
		{
			int num = int.Parse(OnlyInstance.dataGridView1.Rows[idx].Cells["Column3"].Value.ToString()) - 1;
			OnlyInstance.dataGridView1.Rows[idx].Cells["Column3"].Value = num.ToString();
			OnlyInstance.dataGridView1.Rows[idx].Cells["Column4"].Value = sum;
		}

		public static void setCommodityQuantity(int idx, string Quantity, string sum)
		{
			OnlyInstance.dataGridView1.Rows[idx].Cells["Column3"].Value = Quantity;
			OnlyInstance.dataGridView1.Rows[idx].Cells["Column4"].Value = sum;
		}

		public static void setCommodityPrice(int idx, string price, string subtotal)
		{
			OnlyInstance.dataGridView1.Rows[idx].Cells["Column2"].Value = price;
			OnlyInstance.dataGridView1.Rows[idx].Cells["Column4"].Value = subtotal;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS_Client.frmExtendScreen));
			dataGridView1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			textBox1 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			dataGridView1.BackgroundColor = System.Drawing.Color.White;
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
			dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column4);
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
			dataGridView1.Location = new System.Drawing.Point(0, 0);
			dataGridView1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 93);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 35;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView1.Size = new System.Drawing.Size(479, 288);
			dataGridView1.TabIndex = 0;
			Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 27.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			Column1.DefaultCellStyle = dataGridViewCellStyle3;
			Column1.FillWeight = 300f;
			Column1.HeaderText = "商品名稱";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 90;
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 27.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			Column2.DefaultCellStyle = dataGridViewCellStyle4;
			Column2.HeaderText = "商品單價";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.Width = 90;
			Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 27.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			Column3.DefaultCellStyle = dataGridViewCellStyle5;
			Column3.HeaderText = "數量";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column3.Width = 58;
			Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("微軟正黑體", 27.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			Column4.DefaultCellStyle = dataGridViewCellStyle6;
			Column4.HeaderText = "價格";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column4.Width = 58;
			textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			textBox1.Font = new System.Drawing.Font("微軟正黑體", 48f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			textBox1.Location = new System.Drawing.Point(0, 195);
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(479, 93);
			textBox1.TabIndex = 1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(479, 288);
			base.Controls.Add(textBox1);
			base.Controls.Add(dataGridView1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "frmExtendScreen";
			Text = "農藥銷售簡易POS";
			base.TopMost = true;
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
