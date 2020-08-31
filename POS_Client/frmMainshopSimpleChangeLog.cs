using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmMainshopSimpleChangeLog : Form
	{
		private string sellNo;

		private IContainer components;

		public Label l_title;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label1;

		public frmMainshopSimpleChangeLog(string sellNo)
		{
			this.sellNo = sellNo;
			InitializeComponent();
		}

		private void frmBatchPrintMember_Load(object sender, EventArgs e)
		{
			displayChangeLog();
		}

		private void displayChangeLog()
		{
			if (tableLayoutPanel1.HasChildren)
			{
				tableLayoutPanel1.Controls.Clear();
			}
			string[] strWhereParameterArray = new string[1]
			{
				sellNo
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_mainsell_log", "sellNo = {0}", "sellLogId desc", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				tableLayoutPanel1.RowCount = dataTable.Rows.Count;
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					Label label = new Label();
					label.Text = (i + 1).ToString();
					label.BackColor = Color.Transparent;
					label.Anchor = AnchorStyles.None;
					label.Dock = DockStyle.Fill;
					Label label2 = new Label();
					label2.Text = dataTable.Rows[i]["changeDate"].ToString();
					label2.BackColor = Color.Transparent;
					label2.Anchor = AnchorStyles.None;
					label2.Dock = DockStyle.Fill;
					Label label3 = new Label();
					if (dataTable.Rows[i]["iscancel"].ToString() == "1")
					{
						label3.Text = "取消訂單";
					}
					else if (dataTable.Rows[i]["ischange"].ToString() == "1" && dataTable.Rows[i]["isprint"].ToString() == "1")
					{
						string[] strWhereParameterArray2 = new string[2]
						{
							dataTable.Rows[i]["sellLogId"].ToString(),
							sellNo
						};
						int count = ((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_mainsell_log", "isprint = 1 and sellLogId < {0} and sellNo={1} ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count;
						label3.Text = "商品退貨/補印收據(v" + (count + 1) + ")";
						string[] strWhereParameterArray3 = new string[1]
						{
							dataTable.Rows[i]["sellLogId"].ToString()
						};
						DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hdl.barcode as code,hdl.diffNum,gd.*", "hypos_detailsell_log as hdl,hypos_GOODSLST as gd", "hdl.sellLogId = {0} and hdl.barcode = gd.GDSNO ", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
						if (dataTable2.Rows.Count > 0)
						{
							label3.Text += "\r\n";
							foreach (DataRow row in dataTable2.Rows)
							{
								label3.Text = label3.Text + row["code"].ToString() + row["GDName"].ToString() + row["spec"].ToString() + row["capacity"].ToString() + " (-" + row["diffNum"].ToString() + ")\r\n";
							}
						}
					}
					else if (dataTable.Rows[i]["isprint"].ToString() == "1" && !string.IsNullOrEmpty(dataTable.Rows[i]["sum"].ToString()))
					{
						label3.Text = "新建銷售單(v1)\r\n ";
					}
					else if (dataTable.Rows[i]["isprint"].ToString() == "1")
					{
						string[] strWhereParameterArray4 = new string[2]
						{
							dataTable.Rows[i]["sellLogId"].ToString(),
							sellNo
						};
						int count2 = ((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_mainsell_log", "isprint = 1 and sellLogId < {0} and sellNo={1} ", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count;
						label3.Text = "補印收據(v" + (count2 + 1) + ")\r\n ";
					}
					label3.BackColor = Color.Transparent;
					label3.Anchor = AnchorStyles.None;
					label3.Dock = DockStyle.Fill;
					label3.AutoSize = true;
					label3.Padding = new Padding(0, 0, 0, 20);
					tableLayoutPanel1.Controls.Add(label, 0, i);
					tableLayoutPanel1.Controls.Add(label2, 1, i);
					tableLayoutPanel1.Controls.Add(label3, 2, i);
					tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize, 100f));
				}
				if (dataTable.Rows.Count > 1)
				{
					tableLayoutPanel1.AutoScroll = true;
					tableLayoutPanel1.Padding = new Padding(0, 0, 10, 1);
				}
				else
				{
					tableLayoutPanel1.AutoScroll = false;
					tableLayoutPanel1.Padding = new Padding(0, 0, 1, 1);
				}
			}
			else
			{
				Close();
			}
		}

		private void label1_Click(object sender, EventArgs e)
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
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			l_title = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			SuspendLayout();
			tableLayoutPanel1.AutoScroll = true;
			tableLayoutPanel1.AutoScrollMargin = new System.Drawing.Size(10, 0);
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.975162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.81184f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.213f));
			tableLayoutPanel1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
			tableLayoutPanel1.Location = new System.Drawing.Point(39, 57);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel1.MaximumSize = new System.Drawing.Size(751, 260);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.Size = new System.Drawing.Size(751, 260);
			tableLayoutPanel1.TabIndex = 33;
			l_title.AutoSize = true;
			l_title.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_title.Image = POS_Client.Properties.Resources.oblique;
			l_title.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_title.Location = new System.Drawing.Point(35, 24);
			l_title.Name = "l_title";
			l_title.Size = new System.Drawing.Size(158, 24);
			l_title.TabIndex = 52;
			l_title.Text = "   銷售單變更紀錄";
			l_title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(801, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(17, 19);
			label1.TabIndex = 53;
			label1.Text = "X";
			label1.Click += new System.EventHandler(label1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Silver;
			base.ClientSize = new System.Drawing.Size(830, 360);
			base.ControlBox = false;
			base.Controls.Add(label1);
			base.Controls.Add(l_title);
			base.Controls.Add(tableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmMainshopSimpleChangeLog";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "frmSearchMember";
			base.Load += new System.EventHandler(frmBatchPrintMember_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
