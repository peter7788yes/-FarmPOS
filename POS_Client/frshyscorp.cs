using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frshyscorp : Form
	{
		private frmMainShopSimple fms;

		private frmMainShopSimpleWithMoney frsm;

		private frmCommoditySearch search;

		private frmCommdityList fcd;

		private string barcodeNum = "";

		private DataTable dt;

		private DataTable dtHyScope;

		private string _pesticideId = "";

		private string _formCode = "";

		private string _contents = "";

		private string _cropId = "";

		private string _pestId = "";

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

		public frshyscorp()
		{
			InitializeComponent();
			loaddata();
		}

		public frshyscorp(frmMainShopSimple fms, string barcodeNum, string name)
		{
			this.fms = fms;
			this.barcodeNum = barcodeNum;
			InitializeComponent();
			string[] strWhereParameterArray = new string[1]
			{
				barcodeNum
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			hyscope.Text = "【" + getCommodityName(dataTable.Rows[0]) + "】" + hyscope.Text.ToString();
			_pesticideId = dataTable.Rows[0]["pesticideId"].ToString();
			_formCode = dataTable.Rows[0]["formCode"].ToString();
			_contents = dataTable.Rows[0]["contents"].ToString();
			loaddata();
		}

		public frshyscorp(frmMainShopSimpleWithMoney frsm, string barcodeNum, string name)
		{
			this.frsm = frsm;
			this.barcodeNum = barcodeNum;
			InitializeComponent();
			string[] strWhereParameterArray = new string[1]
			{
				barcodeNum
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			hyscope.Text = "【" + getCommodityName(dataTable.Rows[0]) + "】" + hyscope.Text.ToString();
			_pesticideId = dataTable.Rows[0]["pesticideId"].ToString();
			_formCode = dataTable.Rows[0]["formCode"].ToString();
			_contents = dataTable.Rows[0]["contents"].ToString();
			loaddata();
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

		private void loaddata()
		{
			string strWhereClause = " hypos_user_pair.barcode = '" + barcodeNum + "' and hypos_user_pair.cropId = HyCrop.code and hypos_user_pair.pestId = HyBlight.code  ";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "HyBlight.name as Blname,HyCrop.name as cropname,hypos_user_pair.cropId as hcropid,hypos_user_pair.pestId as hpestId ", "hypos_user_pair,HyBlight,HyCrop", strWhereClause, "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			foreach (DataRow row in dt.Rows)
			{
				infolist.Rows.Add(row["cropname"].ToString(), row["Blname"].ToString(), "選擇", "用藥說明", barcodeNum, row["hcropid"].ToString(), row["hpestId"].ToString());
				infolist.Rows[0].Selected = false;
			}
		}

		public void setSearch(frmCommoditySearch f)
		{
			search = f;
		}

		public void setSearchResult(frmCommdityList fcd)
		{
			this.fcd = fcd;
		}

		private void infolist_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			if (e.ColumnIndex == 2)
			{
				string[] strWhereParameterArray = new string[1]
				{
					barcodeNum
				};
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				string[] strWhereParameterArray2 = new string[5]
				{
					dataTable.Rows[0]["pesticideId"].ToString(),
					dataTable.Rows[0]["formCode"].ToString(),
					dataTable.Rows[0]["contents"].ToString(),
					infolist.CurrentRow.Cells["cropId"].Value.ToString(),
					infolist.CurrentRow.Cells["pestId"].Value.ToString()
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "HyScope", " pesticideId = {0} AND formCode = {1} AND contents = {2} AND cropId = {3} AND pestId = {4} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					if (Program.SystemMode == 1)
					{
						fms.addOnecommodity(barcodeNum, infolist.CurrentRow.Cells["cropId"].Value.ToString(), infolist.CurrentRow.Cells["pestId"].Value.ToString(), infolist.CurrentRow.Cells["commodity"].Value.ToString(), infolist.CurrentRow.Cells["quantity"].Value.ToString());
						fms.Show();
						Close();
						if (fcd != null)
						{
							fcd.Close();
						}
						if (search != null)
						{
							search.Close();
						}
					}
					else
					{
						frsm.addOnecommodity(barcodeNum, infolist.CurrentRow.Cells["cropId"].Value.ToString(), infolist.CurrentRow.Cells["pestId"].Value.ToString(), infolist.CurrentRow.Cells["commodity"].Value.ToString(), infolist.CurrentRow.Cells["quantity"].Value.ToString());
						frsm.Show();
						Close();
						if (fcd != null)
						{
							fcd.Close();
						}
						if (search != null)
						{
							search.Close();
						}
					}
				}
				else
				{
					MessageBox.Show(string.Format("此配對已不存在，請選擇其他配對。此配對將自您的常用配對紀錄中移除。", Application.ProductName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					try
					{
						string[] strParameterArray = new string[3]
						{
							barcodeNum,
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
				new dialogMedDescription(barcodeNum, infolist.CurrentRow.Cells["cropId"].Value.ToString(), infolist.CurrentRow.Cells["pestId"].Value.ToString()).ShowDialog();
			}
		}

		private void usescope_Click(object sender, EventArgs e)
		{
			if (Program.SystemMode == 1)
			{
				if (Program.IsCropPestRange_NEW)
				{
					frmCropPestRange_NEW frmCropPestRange_NEW = new frmCropPestRange_NEW(fms, 0, barcodeNum, 2);
					if (fcd != null)
					{
						frmCropPestRange_NEW.Location = new Point(fcd.Location.X, fcd.Location.Y);
						fcd.Close();
					}
					if (search != null)
					{
						frmCropPestRange_NEW.Location = new Point(search.Location.X, search.Location.Y);
						search.Close();
					}
					Close();
					frmCropPestRange_NEW.Show();
				}
				else
				{
					frmCropGuideRangeForSearch frmCropGuideRangeForSearch = new frmCropGuideRangeForSearch(fms, 0, barcodeNum);
					if (fcd != null)
					{
						frmCropGuideRangeForSearch.Location = new Point(fcd.Location.X, fcd.Location.Y);
						fcd.Close();
					}
					if (search != null)
					{
						frmCropGuideRangeForSearch.Location = new Point(search.Location.X, search.Location.Y);
						search.Close();
					}
					Close();
					frmCropGuideRangeForSearch.Show();
				}
			}
			else if (Program.IsCropPestRange_NEW)
			{
				frmCropPestRange_NEW frmCropPestRange_NEW = new frmCropPestRange_NEW(frsm, 0, barcodeNum, 2);
				if (fcd != null)
				{
					frmCropPestRange_NEW.Location = new Point(fcd.Location.X, fcd.Location.Y);
					fcd.Close();
				}
				if (search != null)
				{
					frmCropPestRange_NEW.Location = new Point(search.Location.X, search.Location.Y);
					search.Close();
				}
				Close();
				frmCropPestRange_NEW.Show();
			}
			else
			{
				frmCropGuideRangeForSearch frmCropGuideRangeForSearch2 = new frmCropGuideRangeForSearch(frsm, 0, barcodeNum);
				if (fcd != null)
				{
					frmCropGuideRangeForSearch2.Location = new Point(fcd.Location.X, fcd.Location.Y);
					fcd.Close();
				}
				if (search != null)
				{
					frmCropGuideRangeForSearch2.Location = new Point(search.Location.X, search.Location.Y);
					search.Close();
				}
				Close();
				frmCropGuideRangeForSearch2.Show();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
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
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(187, 82, 51);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			infolist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			infolist.Columns.AddRange(commodity, quantity, Column1, Column2, barcode, cropId, pestId);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(187, 82, 51);
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
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(187, 82, 51);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			infolist.RowHeadersVisible = false;
			infolist.RowTemplate.Height = 40;
			infolist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			infolist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			infolist.Size = new System.Drawing.Size(771, 216);
			infolist.TabIndex = 10;
			infolist.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(infolist_CellClick);
			commodity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			commodity.DefaultCellStyle = dataGridViewCellStyle4;
			commodity.HeaderText = "作物";
			commodity.Name = "commodity";
			commodity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			quantity.DefaultCellStyle = dataGridViewCellStyle5;
			quantity.HeaderText = "病蟲害";
			quantity.Name = "quantity";
			quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column1.HeaderText = "選取";
			Column1.MinimumWidth = 100;
			Column1.Name = "Column1";
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column2.HeaderText = "用藥說明";
			Column2.MinimumWidth = 100;
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
			usescope.Location = new System.Drawing.Point(413, 296);
			usescope.Name = "usescope";
			usescope.Size = new System.Drawing.Size(165, 36);
			usescope.TabIndex = 11;
			usescope.Text = "選擇使用範圍";
			usescope.UseVisualStyleBackColor = false;
			usescope.Click += new System.EventHandler(usescope_Click);
			button1.BackColor = System.Drawing.Color.FromArgb(190, 180, 152);
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button1.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.ForeColor = System.Drawing.Color.White;
			button1.Location = new System.Drawing.Point(265, 296);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(102, 36);
			button1.TabIndex = 12;
			button1.Text = "取消選擇";
			button1.UseVisualStyleBackColor = false;
			button1.Click += new System.EventHandler(button1_Click);
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
			base.Name = "frshyscorp";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			((System.ComponentModel.ISupportInitialize)infolist).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
