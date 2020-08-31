using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmDialogMedNew : Form
	{
		private string _vipNo;

		private frmMainShopSimple frs;

		private frmMainShopSimpleWithMoney frsm;

		private int count;

		private string barcodeid;

		private string _pesticideId;

		private string _formCode;

		private string _content;

		private const int WS_SYSMENU = 524288;

		private IContainer components;

		private Label hyscope;

		private DataGridView infolist;

		private Button usescope;

		private Button button1;

		private DataGridViewTextBoxColumn commodity;

		private DataGridViewTextBoxColumn quantity;

		private DataGridViewButtonColumn Column1;

		private DataGridViewButtonColumn Column2;

		private DataGridViewTextBoxColumn barcode;

		private DataGridViewTextBoxColumn cropId;

		private DataGridViewTextBoxColumn pestId;

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style &= -524289;
				return createParams;
			}
		}

		public frmDialogMedNew(frmMainShopSimple frs, int count, string barcode)
		{
			InitializeComponent();
			this.frs = frs;
			this.count = count;
			barcodeid = barcode;
			string[] strWhereParameterArray = new string[1]
			{
				barcodeid
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			_pesticideId = dataTable.Rows[0]["pesticideId"].ToString();
			_formCode = dataTable.Rows[0]["formCode"].ToString();
			_content = dataTable.Rows[0]["contents"].ToString();
			hyscope.Text = "【" + getCommodityName(dataTable.Rows[0]) + "】用藥範圍設定";
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
				infolist.Rows.Add(dataTable3.Rows[0]["name"].ToString(), dataTable4.Rows[0]["name"].ToString(), "選擇", "用藥說明", barcodeid, dataTable3.Rows[0]["code"].ToString(), dataTable4.Rows[0]["code"].ToString());
			}
		}

		public frmDialogMedNew(frmMainShopSimpleWithMoney frs, int count, string barcode)
		{
			InitializeComponent();
			frsm = frs;
			this.count = count;
			barcodeid = barcode;
			string[] strWhereParameterArray = new string[1]
			{
				barcodeid
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			_pesticideId = dataTable.Rows[0]["pesticideId"].ToString();
			_formCode = dataTable.Rows[0]["formCode"].ToString();
			_content = dataTable.Rows[0]["contents"].ToString();
			hyscope.Text = "【" + getCommodityName(dataTable.Rows[0]) + "】用藥範圍設定";
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
				infolist.Rows.Add(dataTable3.Rows[0]["name"].ToString(), dataTable4.Rows[0]["name"].ToString(), "選擇", "用藥說明", barcodeid, dataTable3.Rows[0]["code"].ToString(), dataTable4.Rows[0]["code"].ToString());
			}
		}

		private string getCommodityName(DataRow row)
		{
			string text = row["GDName"].ToString();
			string text2 = row["formCode"].ToString();
			string text3 = row["CName"].ToString();
			string text4 = row["contents"].ToString();
			string text5 = row["brandName"].ToString();
			string[] array = new string[2]
			{
				text3,
				text2
			};
			string[] array2 = new string[2]
			{
				text4,
				text5
			};
			if (!string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text3) || !string.IsNullOrEmpty(text4) || !string.IsNullOrEmpty(text5))
			{
				text += "[";
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i]))
					{
						text = text + array[i] + "-";
					}
				}
				if (text.LastIndexOf("-") > 0)
				{
					text = text.Substring(0, text.LastIndexOf("-")) + " . ";
				}
				for (int j = 0; j < array2.Length; j++)
				{
					if (!string.IsNullOrEmpty(array2[j]))
					{
						text = text + array2[j] + "-";
					}
				}
				if (text.LastIndexOf("-") > 0)
				{
					text = text.Substring(0, text.LastIndexOf("-")) + "]";
				}
			}
			return text;
		}

		private void btn_useRange_Click(object sender, EventArgs e)
		{
			if (Program.SystemMode == 1)
			{
				if (Program.IsCropPestRange_NEW)
				{
					frmCropPestRange_NEW frmCropPestRange_NEW = new frmCropPestRange_NEW(frs, count, barcodeid, 1);
					frmCropPestRange_NEW.Location = new Point(frs.Location.X, frs.Location.Y);
					frmCropPestRange_NEW.Show();
					Hide();
				}
				else
				{
					frmCropGuideRange frmCropGuideRange = new frmCropGuideRange(frs, count, barcodeid);
					frmCropGuideRange.Location = new Point(frs.Location.X, frs.Location.Y);
					frmCropGuideRange.Show();
					Hide();
				}
			}
			else if (Program.IsCropPestRange_NEW)
			{
				frmCropPestRange_NEW frmCropPestRange_NEW2 = new frmCropPestRange_NEW(frsm, count, barcodeid, 1);
				frmCropPestRange_NEW2.Location = new Point(frsm.Location.X, frsm.Location.Y);
				frmCropPestRange_NEW2.Show();
				Hide();
			}
			else
			{
				frmCropGuideRange frmCropGuideRange2 = new frmCropGuideRange(frsm, count, barcodeid);
				frmCropGuideRange2.Location = new Point(frsm.Location.X, frsm.Location.Y);
				frmCropGuideRange2.Show();
				Hide();
			}
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
			if (e.RowIndex < 0)
			{
				return;
			}
			if (e.ColumnIndex == 2)
			{
				string[] strWhereParameterArray = new string[5]
				{
					_pesticideId,
					_formCode,
					_content,
					infolist.CurrentRow.Cells["cropId"].Value.ToString(),
					infolist.CurrentRow.Cells["pestId"].Value.ToString()
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "HyScope", " pesticideId = {0} AND formCode = {1} AND contents = {2} AND cropId = {3} AND pestId = {4} AND isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					if (Program.SystemMode == 1)
					{
						frs.addCropAndPest(count, infolist.CurrentRow.Cells["cropId"].Value.ToString(), infolist.CurrentRow.Cells["pestId"].Value.ToString());
						frs.Hide();
						frs.setfocus();
						frs.Show();
						Hide();
					}
					else
					{
						frsm.addCropAndPest(count, infolist.CurrentRow.Cells["cropId"].Value.ToString(), infolist.CurrentRow.Cells["pestId"].Value.ToString());
						frsm.Hide();
						frsm.setfocus();
						frsm.Show();
						Hide();
					}
				}
				else
				{
					MessageBox.Show(string.Format("此配對已不存在，請選擇其他配對。此配對將自您的常用配對紀錄中移除。", Application.ProductName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					try
					{
						string[] strParameterArray = new string[3]
						{
							barcodeid,
							infolist.CurrentRow.Cells["cropId"].Value.ToString(),
							infolist.CurrentRow.Cells["pestId"].Value.ToString()
						};
						string sql = " DELETE FROM hypos_user_pair WHERE barcode = {0} AND cropId = {1} AND pestId = {2} ";
						DataBaseUtilities.DBOperation(Program.ConnectionString, sql, strParameterArray, CommandOperationType.ExecuteNonQuery);
						infolist.Rows.RemoveAt(infolist.CurrentRow.Index);
					}
					catch (Exception)
					{
						throw;
					}
				}
			}
			if (e.ColumnIndex == 3)
			{
				new dialogMedDescription(_pesticideId, infolist.CurrentRow.Cells["cropId"].Value.ToString(), infolist.CurrentRow.Cells["pestId"].Value.ToString(), _formCode, _content).ShowDialog();
			}
		}

		private void btn_back_Click(object sender, EventArgs e)
		{
			if (Program.SystemMode == 1)
			{
				frs.RemoveLast();
				frs.setfocus();
			}
			else
			{
				frsm.RemoveLast();
				frsm.setfocus();
			}
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
			hyscope = new System.Windows.Forms.Label();
			infolist = new System.Windows.Forms.DataGridView();
			commodity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
			Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
			barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cropId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			pestId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			usescope = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)infolist).BeginInit();
			SuspendLayout();
			hyscope.AutoSize = true;
			hyscope.Font = new System.Drawing.Font("微軟正黑體", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			hyscope.Location = new System.Drawing.Point(6, 22);
			hyscope.Name = "hyscope";
			hyscope.Size = new System.Drawing.Size(157, 30);
			hyscope.TabIndex = 0;
			hyscope.Text = "用藥範圍設定";
			hyscope.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			infolist.AllowUserToAddRows = false;
			infolist.AllowUserToDeleteRows = false;
			infolist.AllowUserToResizeColumns = false;
			infolist.AllowUserToResizeRows = false;
			infolist.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			infolist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			infolist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			infolist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			infolist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			infolist.Columns.AddRange(commodity, quantity, Column1, Column2, barcode, cropId, pestId);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Pink;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			infolist.DefaultCellStyle = dataGridViewCellStyle2;
			infolist.EnableHeadersVisualStyles = false;
			infolist.GridColor = System.Drawing.SystemColors.ActiveBorder;
			infolist.Location = new System.Drawing.Point(12, 66);
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
			infolist.RowTemplate.Height = 40;
			infolist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			infolist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			infolist.Size = new System.Drawing.Size(771, 216);
			infolist.TabIndex = 10;
			infolist.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(infolist_CellContentClick);
			commodity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			commodity.DefaultCellStyle = dataGridViewCellStyle4;
			commodity.HeaderText = "作物";
			commodity.MinimumWidth = 150;
			commodity.Name = "commodity";
			commodity.ReadOnly = true;
			commodity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			quantity.DefaultCellStyle = dataGridViewCellStyle5;
			quantity.HeaderText = "病蟲害";
			quantity.MinimumWidth = 150;
			quantity.Name = "quantity";
			quantity.ReadOnly = true;
			quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column1.HeaderText = "選取";
			Column1.MinimumWidth = 100;
			Column1.Name = "Column1";
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column2.HeaderText = "用藥說明";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column2.Width = 73;
			barcode.HeaderText = "條碼";
			barcode.Name = "barcode";
			barcode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			barcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			barcode.Visible = false;
			cropId.HeaderText = "作物id";
			cropId.Name = "cropId";
			cropId.Visible = false;
			pestId.HeaderText = "病蟲害id";
			pestId.Name = "pestId";
			pestId.Visible = false;
			usescope.BackColor = System.Drawing.Color.FromArgb(255, 102, 0);
			usescope.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			usescope.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			usescope.ForeColor = System.Drawing.Color.White;
			usescope.Location = new System.Drawing.Point(411, 296);
			usescope.Name = "usescope";
			usescope.Size = new System.Drawing.Size(170, 36);
			usescope.TabIndex = 11;
			usescope.Text = "選擇使用範圍";
			usescope.UseVisualStyleBackColor = false;
			usescope.Click += new System.EventHandler(btn_useRange_Click);
			button1.BackColor = System.Drawing.Color.FromArgb(190, 180, 152);
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button1.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.ForeColor = System.Drawing.Color.White;
			button1.Location = new System.Drawing.Point(260, 296);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(102, 36);
			button1.TabIndex = 12;
			button1.Text = "取消選擇";
			button1.UseVisualStyleBackColor = false;
			button1.Click += new System.EventHandler(btn_back_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(241, 240, 213);
			base.ClientSize = new System.Drawing.Size(814, 344);
			base.Controls.Add(button1);
			base.Controls.Add(usescope);
			base.Controls.Add(infolist);
			base.Controls.Add(hyscope);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmDialogMedNew";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			((System.ComponentModel.ISupportInitialize)infolist).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
