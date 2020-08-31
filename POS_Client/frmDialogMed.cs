using KeyboardClassLibrary;
using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmDialogMed : Form
	{
		private string _vipNo;

		private frmMainShopSimple frs;

		private frmMainShopSimpleWithMoney frsm;

		private int count;

		private string barcodeid;

		private IContainer components;

		private Button btn_back;

		private Button btn_useRange;

		private PictureBox pictureBox1;

		private Panel panel17;

		private Button btn_down;

		private Button btn_top;

		private PictureBox pictureBox2;

		private Keyboardcontrol keyboardcontrol1;

		private DataGridView infolist;

		private Label label1;

		private DataGridViewTextBoxColumn commodity;

		private DataGridViewTextBoxColumn quantity;

		private DataGridViewTextBoxColumn barcode;

		private DataGridViewTextBoxColumn cropId;

		private DataGridViewTextBoxColumn pestid;

		public frmDialogMed(frmMainShopSimple frs, int count, string barcode)
		{
			InitializeComponent();
			this.frs = frs;
			this.count = count;
			barcodeid = barcode;
			string[] strWhereParameterArray = new string[1]
			{
				barcodeid
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDName", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			label1.Text = "【" + dataTable.Rows[0]["GDName"].ToString() + "】用藥範圍設定";
			string sql = "select cropId,pestId from hypos_user_pair where barcode = '" + barcodeid + "' order by total desc";
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable2.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable2.Rows)
			{
				string[] strWhereParameterArray2 = new string[1]
				{
					row["cropId"].ToString()
				};
				DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "code,name", "HyCrop", "code = {0}", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				string[] strWhereParameterArray3 = new string[1]
				{
					row["pestId"].ToString()
				};
				DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "code,name", "HyBlight", "code = {0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
				infolist.Rows.Add(dataTable3.Rows[0]["name"].ToString(), dataTable4.Rows[0]["name"].ToString(), barcodeid, dataTable3.Rows[0]["code"].ToString(), dataTable4.Rows[0]["code"].ToString());
				DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
				dataGridViewButtonColumn.Text = "選擇";
				dataGridViewButtonColumn.Name = "btn";
				dataGridViewButtonColumn.HeaderText = "選取";
				dataGridViewButtonColumn.UseColumnTextForButtonValue = true;
				infolist.Columns.Add(dataGridViewButtonColumn);
			}
		}

		public frmDialogMed(frmMainShopSimpleWithMoney frs, int count, string barcode)
		{
			InitializeComponent();
			frsm = frs;
			this.count = count;
			barcodeid = barcode;
			string[] strWhereParameterArray = new string[1]
			{
				barcodeid
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDName", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			label1.Text = "【" + dataTable.Rows[0]["GDName"].ToString() + "】用藥範圍設定";
			string sql = "select cropId,pestId from hypos_user_pair where barcode = '" + barcodeid + "' order by total desc";
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable2.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable2.Rows)
			{
				string[] strWhereParameterArray2 = new string[1]
				{
					row["cropId"].ToString()
				};
				DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "code,name", "HyCrop", "code = {0}", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				string[] strWhereParameterArray3 = new string[1]
				{
					row["pestId"].ToString()
				};
				DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "code,name", "HyBlight", "code = {0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
				infolist.Rows.Add(dataTable3.Rows[0]["name"].ToString(), dataTable4.Rows[0]["name"].ToString(), barcodeid, dataTable3.Rows[0]["code"].ToString(), dataTable4.Rows[0]["code"].ToString());
				DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
				dataGridViewButtonColumn.Text = "選擇";
				dataGridViewButtonColumn.Name = "btn";
				dataGridViewButtonColumn.HeaderText = "選取";
				dataGridViewButtonColumn.UseColumnTextForButtonValue = true;
				infolist.Columns.Add(dataGridViewButtonColumn);
			}
		}

		private void btn_useRange_Click(object sender, EventArgs e)
		{
			frmCropGuideRange frmCropGuideRange = (Program.SystemMode != 1) ? new frmCropGuideRange(frsm, count, barcodeid) : new frmCropGuideRange(frs, count, barcodeid);
			frmCropGuideRange.Location = new Point(base.Location.X, base.Location.Y);
			frmCropGuideRange.Show();
			Hide();
			if (Program.SystemMode == 1)
			{
				frs.Hide();
			}
			else
			{
				frsm.Hide();
			}
		}

		private void infolist_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 5)
			{
				if (Program.SystemMode == 1)
				{
					frs.addCropAndPest(count, infolist.CurrentRow.Cells[3].Value.ToString(), infolist.CurrentRow.Cells[4].Value.ToString());
					frs.Hide();
					frs.setfocus();
					frs.Show();
					Hide();
				}
				else
				{
					frsm.addCropAndPest(count, infolist.CurrentRow.Cells[3].Value.ToString(), infolist.CurrentRow.Cells[4].Value.ToString());
					frsm.Hide();
					frsm.setfocus();
					frsm.Show();
					Hide();
				}
			}
		}

		private void btn_back_Click(object sender, EventArgs e)
		{
			frs.RemoveLast();
			frs.setfocus();
			Close();
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
			btn_back = new System.Windows.Forms.Button();
			btn_useRange = new System.Windows.Forms.Button();
			panel17 = new System.Windows.Forms.Panel();
			btn_down = new System.Windows.Forms.Button();
			btn_top = new System.Windows.Forms.Button();
			pictureBox2 = new System.Windows.Forms.PictureBox();
			keyboardcontrol1 = new KeyboardClassLibrary.Keyboardcontrol();
			infolist = new System.Windows.Forms.DataGridView();
			label1 = new System.Windows.Forms.Label();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			commodity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cropId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			pestid = new System.Windows.Forms.DataGridViewTextBoxColumn();
			panel17.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)infolist).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			btn_back.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_back.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_back.ForeColor = System.Drawing.Color.White;
			btn_back.Location = new System.Drawing.Point(457, 555);
			btn_back.Name = "btn_back";
			btn_back.Size = new System.Drawing.Size(113, 51);
			btn_back.TabIndex = 0;
			btn_back.Text = "取消選擇";
			btn_back.UseVisualStyleBackColor = false;
			btn_back.Click += new System.EventHandler(btn_back_Click);
			btn_useRange.BackColor = System.Drawing.Color.FromArgb(255, 109, 49);
			btn_useRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_useRange.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_useRange.ForeColor = System.Drawing.Color.White;
			btn_useRange.Location = new System.Drawing.Point(296, 555);
			btn_useRange.Name = "btn_useRange";
			btn_useRange.Size = new System.Drawing.Size(128, 51);
			btn_useRange.TabIndex = 42;
			btn_useRange.Text = "使用範圍";
			btn_useRange.UseVisualStyleBackColor = false;
			btn_useRange.Click += new System.EventHandler(btn_useRange_Click);
			panel17.BackColor = System.Drawing.Color.FromArgb(51, 51, 51);
			panel17.Controls.Add(btn_down);
			panel17.Controls.Add(btn_top);
			panel17.Controls.Add(pictureBox2);
			panel17.Controls.Add(keyboardcontrol1);
			panel17.Location = new System.Drawing.Point(953, 367);
			panel17.Margin = new System.Windows.Forms.Padding(0);
			panel17.Name = "panel17";
			panel17.Size = new System.Drawing.Size(949, 269);
			panel17.TabIndex = 53;
			btn_down.Location = new System.Drawing.Point(862, 112);
			btn_down.Name = "btn_down";
			btn_down.Size = new System.Drawing.Size(58, 40);
			btn_down.TabIndex = 52;
			btn_down.Text = "Down";
			btn_down.UseVisualStyleBackColor = true;
			btn_top.Location = new System.Drawing.Point(862, 55);
			btn_top.Name = "btn_top";
			btn_top.Size = new System.Drawing.Size(58, 40);
			btn_top.TabIndex = 51;
			btn_top.Text = "Top";
			btn_top.UseVisualStyleBackColor = true;
			pictureBox2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom);
			pictureBox2.BackColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
			pictureBox2.Image = POS_Client.Properties.Resources.keyboard_close;
			pictureBox2.Location = new System.Drawing.Point(842, 7);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new System.Drawing.Size(59, 34);
			pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			pictureBox2.TabIndex = 50;
			pictureBox2.TabStop = false;
			keyboardcontrol1.KeyboardType = KeyboardClassLibrary.BoW.Standard;
			keyboardcontrol1.Location = new System.Drawing.Point(12, 6);
			keyboardcontrol1.Name = "keyboardcontrol1";
			keyboardcontrol1.Size = new System.Drawing.Size(816, 260);
			keyboardcontrol1.TabIndex = 0;
			infolist.AllowUserToAddRows = false;
			infolist.AllowUserToDeleteRows = false;
			infolist.AllowUserToResizeColumns = false;
			infolist.AllowUserToResizeRows = false;
			infolist.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			infolist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			infolist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			infolist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 255);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			infolist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			infolist.Columns.AddRange(commodity, quantity, barcode, cropId, pestid);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 250, 231);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			infolist.DefaultCellStyle = dataGridViewCellStyle2;
			infolist.EnableHeadersVisualStyles = false;
			infolist.GridColor = System.Drawing.SystemColors.ActiveBorder;
			infolist.Location = new System.Drawing.Point(77, 117);
			infolist.Name = "infolist";
			infolist.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ScrollBar;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 255);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			infolist.RowHeadersVisible = false;
			infolist.RowTemplate.Height = 24;
			infolist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			infolist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			infolist.Size = new System.Drawing.Size(771, 216);
			infolist.TabIndex = 54;
			infolist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(infolist_CellContentClick);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.Location = new System.Drawing.Point(217, 83);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(81, 30);
			label1.TabIndex = 55;
			label1.Text = "label1";
			pictureBox1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom);
			pictureBox1.BackColor = System.Drawing.Color.Silver;
			pictureBox1.Image = POS_Client.Properties.Resources.keyboard;
			pictureBox1.Location = new System.Drawing.Point(874, 588);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(70, 46);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			pictureBox1.TabIndex = 52;
			pictureBox1.TabStop = false;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			commodity.DefaultCellStyle = dataGridViewCellStyle4;
			commodity.HeaderText = "作物";
			commodity.Name = "commodity";
			commodity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			commodity.Width = 330;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			quantity.DefaultCellStyle = dataGridViewCellStyle5;
			quantity.HeaderText = "病蟲害";
			quantity.Name = "quantity";
			quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			quantity.Width = 330;
			barcode.HeaderText = "條碼";
			barcode.Name = "barcode";
			barcode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			barcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			barcode.Visible = false;
			cropId.HeaderText = "作物id";
			cropId.Name = "cropId";
			cropId.Visible = false;
			pestid.HeaderText = "病蟲害id";
			pestid.Name = "pestid";
			pestid.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Silver;
			base.ClientSize = new System.Drawing.Size(949, 636);
			base.Controls.Add(label1);
			base.Controls.Add(infolist);
			base.Controls.Add(panel17);
			base.Controls.Add(pictureBox1);
			base.Controls.Add(btn_useRange);
			base.Controls.Add(btn_back);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmDialogMed";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "選擇會員 / 會員編修";
			panel17.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)infolist).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
