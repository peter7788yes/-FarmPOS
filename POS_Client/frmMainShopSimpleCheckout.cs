using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmMainShopSimpleCheckout : MasterThinForm
	{
		public int listindex = 1;

		public int columnOfFocus;

		private string HseqNo = "";

		private frmMainShopSimple fms;

		private DataGridView temp;

		private IContainer components;

		public Button Checkout;

		private Panel panel2;

		public DataGridView infolist1;

		private Panel panel1;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn commodity;

		private DataGridViewTextBoxColumn quantity;

		private DataGridViewTextBoxColumn barcode;

		private Label label2;

		private Label label1;

		private Label label3;

		public Button backto;

		private Label total;

		private Label items;

		public frmMainShopSimpleCheckout(frmMainShopSimple fms, DataGridView temp)
			: base("銷售作業")
		{
			this.fms = fms;
			infolist1 = new DataGridView();
			string text = DateTime.Now.ToString("yyyyMMdd");
			string[] strWhereParameterArray = new string[1]
			{
				text
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "ECRHDHS", "Hdate = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				string text2 = dataTable.Rows.Count.ToString();
				if (text2.Length == 1)
				{
					HseqNo = text + "00" + text2;
				}
				else if (text2.Length == 2)
				{
					HseqNo = text + "0" + text2;
				}
				else
				{
					HseqNo = text + text2;
				}
			}
			else
			{
				HseqNo = text + "001";
			}
			setMasterFormName("銷售作業 | 單號: " + HseqNo);
			InitializeComponent();
			for (int i = 0; i < temp.Rows.Count; i++)
			{
				infolist1.Rows.Add(temp.Rows[i].Cells[0].Value, temp.Rows[i].Cells[1].Value, temp.Rows[i].Cells[2].Value, temp.Rows[i].Cells[3].Value);
				items.Text = fms.gettotalprice();
				total.Text = fms.gettotalpriceDiscount();
			}
		}

		private void backto_Click(object sender, EventArgs e)
		{
			fms.Show();
			Hide();
		}

		private void Checkout_Click(object sender, EventArgs e)
		{
			string text = DateTime.Now.ToString("yyyyMMdd");
			string[,] strFieldArray = new string[11, 2]
			{
				{
					"sellNo",
					HseqNo
				},
				{
					"sellTime",
					text
				},
				{
					"memberId",
					"test"
				},
				{
					"sum",
					"0"
				},
				{
					"sumDiscount",
					"0"
				},
				{
					"sumRebate",
					"0"
				},
				{
					"cash",
					"0"
				},
				{
					"Credit",
					"0"
				},
				{
					"items",
					items.Text
				},
				{
					"itemstotal",
					total.Text
				},
				{
					"status",
					"1"
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_main_sell", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			for (int i = 0; i < infolist1.Rows.Count; i++)
			{
				strFieldArray = new string[10, 2]
				{
					{
						"sellNo",
						HseqNo
					},
					{
						"barcode",
						infolist1.Rows[i].Cells[3].Value.ToString()
					},
					{
						"fixedPrice",
						"0"
					},
					{
						"sellingPrice",
						"0"
					},
					{
						"num",
						infolist1.Rows[i].Cells[2].Value.ToString()
					},
					{
						"discount",
						"0"
					},
					{
						"subtotal",
						"0"
					},
					{
						"total",
						"0"
					},
					{
						"PRNO",
						"test"
					},
					{
						"BLNO",
						"test2"
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_detail_sell", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			}
			MessageBox.Show("新增成功");
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
			Checkout = new System.Windows.Forms.Button();
			panel2 = new System.Windows.Forms.Panel();
			backto = new System.Windows.Forms.Button();
			infolist1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			commodity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			panel1 = new System.Windows.Forms.Panel();
			total = new System.Windows.Forms.Label();
			items = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)infolist1).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			Checkout.BackColor = System.Drawing.Color.FromArgb(250, 87, 0);
			Checkout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			Checkout.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			Checkout.ForeColor = System.Drawing.Color.White;
			Checkout.Location = new System.Drawing.Point(574, 93);
			Checkout.Name = "Checkout";
			Checkout.Size = new System.Drawing.Size(183, 84);
			Checkout.TabIndex = 36;
			Checkout.Text = "收銀\r\n結帳";
			Checkout.UseVisualStyleBackColor = false;
			Checkout.Click += new System.EventHandler(Checkout_Click);
			panel2.BackgroundImage = POS_Client.Properties.Resources.inside_button;
			panel2.Controls.Add(backto);
			panel2.Controls.Add(Checkout);
			panel2.Location = new System.Drawing.Point(12, 473);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(969, 219);
			panel2.TabIndex = 38;
			backto.BackColor = System.Drawing.Color.FromArgb(250, 87, 0);
			backto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			backto.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			backto.ForeColor = System.Drawing.Color.White;
			backto.Location = new System.Drawing.Point(267, 93);
			backto.Name = "backto";
			backto.Size = new System.Drawing.Size(183, 84);
			backto.TabIndex = 37;
			backto.Text = "返回\r\n編修";
			backto.UseVisualStyleBackColor = false;
			backto.Click += new System.EventHandler(backto_Click);
			infolist1.AllowUserToAddRows = false;
			infolist1.AllowUserToDeleteRows = false;
			infolist1.AllowUserToResizeColumns = false;
			infolist1.AllowUserToResizeRows = false;
			infolist1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			infolist1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			infolist1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			infolist1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 255);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			infolist1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			infolist1.Columns.AddRange(Column1, commodity, quantity, barcode);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Pink;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			infolist1.DefaultCellStyle = dataGridViewCellStyle2;
			infolist1.GridColor = System.Drawing.SystemColors.ActiveBorder;
			infolist1.Location = new System.Drawing.Point(71, 77);
			infolist1.Name = "infolist1";
			infolist1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ScrollBar;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 255);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			infolist1.RowHeadersVisible = false;
			infolist1.RowTemplate.Height = 24;
			infolist1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			infolist1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			infolist1.Size = new System.Drawing.Size(750, 232);
			infolist1.TabIndex = 9;
			Column1.HeaderText = "";
			Column1.Name = "Column1";
			Column1.Width = 25;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			commodity.DefaultCellStyle = dataGridViewCellStyle4;
			commodity.HeaderText = "商品名稱";
			commodity.Name = "commodity";
			commodity.Width = 650;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			quantity.DefaultCellStyle = dataGridViewCellStyle5;
			quantity.HeaderText = "數量";
			quantity.Name = "quantity";
			quantity.Width = 75;
			barcode.HeaderText = "條碼";
			barcode.Name = "barcode";
			barcode.Visible = false;
			panel1.BackColor = System.Drawing.Color.White;
			panel1.Controls.Add(total);
			panel1.Controls.Add(items);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(infolist1);
			panel1.Location = new System.Drawing.Point(29, 48);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(937, 419);
			panel1.TabIndex = 37;
			total.AutoSize = true;
			total.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			total.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			total.Location = new System.Drawing.Point(552, 341);
			total.Name = "total";
			total.Size = new System.Drawing.Size(64, 26);
			total.TabIndex = 14;
			total.Text = "label4";
			items.AutoSize = true;
			items.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			items.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			items.Location = new System.Drawing.Point(150, 342);
			items.Name = "items";
			items.Size = new System.Drawing.Size(64, 26);
			items.TabIndex = 13;
			items.Text = "label4";
			label3.BackColor = System.Drawing.Color.FromArgb(196, 214, 96);
			label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			label3.ForeColor = System.Drawing.Color.FromArgb(196, 214, 96);
			label3.Location = new System.Drawing.Point(13, 396);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(900, 2);
			label3.TabIndex = 12;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label2.Location = new System.Drawing.Point(424, 341);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(54, 27);
			label2.TabIndex = 11;
			label2.Text = "數量";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label1.Location = new System.Drawing.Point(36, 341);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(54, 27);
			label1.TabIndex = 10;
			label1.Text = "品項";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(252, 252, 237);
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(1004, 704);
			base.Controls.Add(panel2);
			base.Controls.Add(panel1);
			base.Name = "frmMainShopSimpleCheckout";
			Text = "frmMainShop";
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(panel2, 0);
			panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)infolist1).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}
	}
}
