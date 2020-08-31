using POS_Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmBatchPrintMember : Form
	{
		private bool tempSelectAll = true;

		private IContainer components;

		private Button btn_cancel;

		private Button btn_printSelected;

		private Button btn_printAll;

		private Button btn_removeSelected;

		private Button btn_selectAll;

		public Label l_title;

		private Label label2;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private RadioButton radioButton3;

		private TableLayoutPanel tableLayoutPanel1;

		public frmBatchPrintMember()
		{
			InitializeComponent();
		}

		private void frmBatchPrintMember_Load(object sender, EventArgs e)
		{
			displayTempMember();
		}

		private void displayTempMember()
		{
			if (tableLayoutPanel1.HasChildren)
			{
				tableLayoutPanel1.Controls.Clear();
			}
			if (Program.membersTemp.Count != 0)
			{
				string text = "VipNo in (";
				for (int i = 0; i < Program.membersTemp.Count; i++)
				{
					text = text + "{" + i + "},";
				}
				text = text.Substring(0, text.Length - 1) + ")";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", text, "", null, Program.membersTemp.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					tableLayoutPanel1.RowCount = Program.membersTemp.Count;
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						CheckBox checkBox = new CheckBox();
						checkBox.Dock = DockStyle.Fill;
						checkBox.Name = dataTable.Rows[j]["VipNo"].ToString();
						checkBox.Text = dataTable.Rows[j]["VipNo"].ToString();
						Label label = new Label();
						label.Text = dataTable.Rows[j]["Name"].ToString();
						label.BackColor = Color.Transparent;
						label.Anchor = AnchorStyles.None;
						Label label2 = new Label();
						label2.Text = dataTable.Rows[j]["Mobile"].ToString();
						label2.BackColor = Color.Transparent;
						label2.Anchor = AnchorStyles.None;
						Label label3 = new Label();
						label3.Text = dataTable.Rows[j]["CompanyIdNo"].ToString();
						label3.BackColor = Color.Transparent;
						label3.Anchor = AnchorStyles.None;
						tableLayoutPanel1.Controls.Add(checkBox, 0, j);
						tableLayoutPanel1.Controls.Add(label, 1, j);
						tableLayoutPanel1.Controls.Add(label2, 2, j);
						tableLayoutPanel1.Controls.Add(label3, 3, j);
						tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 61f));
					}
					if (Program.membersTemp.Count > 6)
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
			}
			else
			{
				MessageBox.Show("已無暫存會員資料");
				Close();
			}
		}

		private void btn_removeSelected_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
			{
				CheckBox checkBox = (CheckBox)tableLayoutPanel1.GetControlFromPosition(0, i);
				if (checkBox.Checked)
				{
					Program.membersTemp.Remove(checkBox.Text);
				}
			}
			displayTempMember();
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btn_printSelected_Click(object sender, EventArgs e)
		{
			int pageRecordCount = 0;
			if (radioButton1.Checked)
			{
				pageRecordCount = 3;
			}
			if (radioButton2.Checked)
			{
				pageRecordCount = 6;
			}
			if (radioButton3.Checked)
			{
				pageRecordCount = 12;
			}
			List<string> list = new List<string>();
			for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
			{
				CheckBox checkBox = (CheckBox)tableLayoutPanel1.GetControlFromPosition(0, i);
				if (checkBox.Checked)
				{
					list.Add(checkBox.Name);
				}
			}
			new Member_barcode(list, pageRecordCount).ShowDialog();
		}

		private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, new Point(e.CellBounds.Right, e.CellBounds.Top));
			e.Graphics.DrawLine(Pens.Black, new Point(e.CellBounds.Left, e.CellBounds.Bottom), new Point(e.CellBounds.Right, e.CellBounds.Bottom));
			if (e.Column == 0)
			{
				e.Graphics.DrawLine(Pens.Black, new Point(e.CellBounds.Left, e.CellBounds.Top), new Point(e.CellBounds.Left, e.CellBounds.Bottom));
			}
			if (e.Column == 3)
			{
				e.Graphics.DrawLine(Pens.Black, new Point(e.CellBounds.Right, e.CellBounds.Top), new Point(e.CellBounds.Right, e.CellBounds.Bottom));
			}
		}

		private void btn_selectAll_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
			{
				((CheckBox)tableLayoutPanel1.GetControlFromPosition(0, i)).Checked = tempSelectAll;
			}
			tempSelectAll = !tempSelectAll;
		}

		private void btn_printAll_Click(object sender, EventArgs e)
		{
			int pageRecordCount = 0;
			if (radioButton1.Checked)
			{
				pageRecordCount = 3;
			}
			if (radioButton2.Checked)
			{
				pageRecordCount = 6;
			}
			if (radioButton3.Checked)
			{
				pageRecordCount = 12;
			}
			new Member_barcode(Program.membersTemp, pageRecordCount).ShowDialog();
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
			btn_cancel = new System.Windows.Forms.Button();
			btn_printSelected = new System.Windows.Forms.Button();
			btn_printAll = new System.Windows.Forms.Button();
			btn_removeSelected = new System.Windows.Forms.Button();
			btn_selectAll = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			radioButton1 = new System.Windows.Forms.RadioButton();
			radioButton2 = new System.Windows.Forms.RadioButton();
			radioButton3 = new System.Windows.Forms.RadioButton();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			l_title = new System.Windows.Forms.Label();
			SuspendLayout();
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(681, 496);
			btn_cancel.Name = "btn_cancel";
			btn_cancel.Size = new System.Drawing.Size(124, 34);
			btn_cancel.TabIndex = 46;
			btn_cancel.TabStop = false;
			btn_cancel.Text = "取消";
			btn_cancel.UseVisualStyleBackColor = false;
			btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
			btn_printSelected.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_printSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_printSelected.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_printSelected.ForeColor = System.Drawing.Color.White;
			btn_printSelected.Location = new System.Drawing.Point(535, 496);
			btn_printSelected.Name = "btn_printSelected";
			btn_printSelected.Size = new System.Drawing.Size(124, 34);
			btn_printSelected.TabIndex = 45;
			btn_printSelected.TabStop = false;
			btn_printSelected.Text = "列印勾選";
			btn_printSelected.UseVisualStyleBackColor = false;
			btn_printSelected.Click += new System.EventHandler(btn_printSelected_Click);
			btn_printAll.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_printAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_printAll.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_printAll.ForeColor = System.Drawing.Color.White;
			btn_printAll.Location = new System.Drawing.Point(393, 496);
			btn_printAll.Name = "btn_printAll";
			btn_printAll.Size = new System.Drawing.Size(124, 34);
			btn_printAll.TabIndex = 44;
			btn_printAll.TabStop = false;
			btn_printAll.Text = "全部列印";
			btn_printAll.UseVisualStyleBackColor = false;
			btn_printAll.Click += new System.EventHandler(btn_printAll_Click);
			btn_removeSelected.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_removeSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_removeSelected.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_removeSelected.ForeColor = System.Drawing.Color.White;
			btn_removeSelected.Location = new System.Drawing.Point(245, 496);
			btn_removeSelected.Name = "btn_removeSelected";
			btn_removeSelected.Size = new System.Drawing.Size(124, 34);
			btn_removeSelected.TabIndex = 45;
			btn_removeSelected.TabStop = false;
			btn_removeSelected.Text = "移除勾選";
			btn_removeSelected.UseVisualStyleBackColor = false;
			btn_removeSelected.Click += new System.EventHandler(btn_removeSelected_Click);
			btn_selectAll.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_selectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_selectAll.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_selectAll.ForeColor = System.Drawing.Color.White;
			btn_selectAll.Location = new System.Drawing.Point(99, 496);
			btn_selectAll.Name = "btn_selectAll";
			btn_selectAll.Size = new System.Drawing.Size(124, 34);
			btn_selectAll.TabIndex = 45;
			btn_selectAll.TabStop = false;
			btn_selectAll.Text = "全選/全不選";
			btn_selectAll.UseVisualStyleBackColor = false;
			btn_selectAll.Click += new System.EventHandler(btn_selectAll_Click);
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label2.Location = new System.Drawing.Point(303, 453);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(105, 20);
			label2.TabIndex = 53;
			label2.Text = "每頁列印筆數";
			radioButton1.AutoSize = true;
			radioButton1.Checked = true;
			radioButton1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			radioButton1.Location = new System.Drawing.Point(424, 451);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new System.Drawing.Size(52, 24);
			radioButton1.TabIndex = 54;
			radioButton1.TabStop = true;
			radioButton1.Text = "3筆";
			radioButton1.UseVisualStyleBackColor = true;
			radioButton2.AutoSize = true;
			radioButton2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			radioButton2.Location = new System.Drawing.Point(484, 451);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new System.Drawing.Size(52, 24);
			radioButton2.TabIndex = 54;
			radioButton2.TabStop = true;
			radioButton2.Text = "6筆";
			radioButton2.UseVisualStyleBackColor = true;
			radioButton3.AutoSize = true;
			radioButton3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			radioButton3.Location = new System.Drawing.Point(544, 451);
			radioButton3.Name = "radioButton3";
			radioButton3.Size = new System.Drawing.Size(61, 24);
			radioButton3.TabIndex = 54;
			radioButton3.TabStop = true;
			radioButton3.Text = "12筆";
			radioButton3.UseVisualStyleBackColor = true;
			tableLayoutPanel1.AutoScrollMargin = new System.Drawing.Size(10, 0);
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
			tableLayoutPanel1.ColumnCount = 4;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.29592f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.70408f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 201f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 246f));
			tableLayoutPanel1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
			tableLayoutPanel1.Location = new System.Drawing.Point(39, 68);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel1.MaximumSize = new System.Drawing.Size(829, 371);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61f));
			tableLayoutPanel1.Size = new System.Drawing.Size(829, 61);
			tableLayoutPanel1.TabIndex = 33;
			tableLayoutPanel1.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(tableLayoutPanel1_CellPaint);
			l_title.AutoSize = true;
			l_title.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_title.Image = POS_Client.Properties.Resources.oblique;
			l_title.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_title.Location = new System.Drawing.Point(35, 24);
			l_title.Name = "l_title";
			l_title.Size = new System.Drawing.Size(101, 24);
			l_title.TabIndex = 52;
			l_title.Text = "   批次列印";
			l_title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Silver;
			base.ClientSize = new System.Drawing.Size(904, 576);
			base.ControlBox = false;
			base.Controls.Add(radioButton3);
			base.Controls.Add(radioButton2);
			base.Controls.Add(radioButton1);
			base.Controls.Add(label2);
			base.Controls.Add(l_title);
			base.Controls.Add(btn_cancel);
			base.Controls.Add(btn_selectAll);
			base.Controls.Add(btn_removeSelected);
			base.Controls.Add(btn_printSelected);
			base.Controls.Add(btn_printAll);
			base.Controls.Add(tableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmBatchPrintMember";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "frmSearchMember";
			base.Load += new System.EventHandler(frmBatchPrintMember_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
