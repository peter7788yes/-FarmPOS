using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmBatchPrintCommodity_pair : Form
	{
		private string _barCode = "";

		private List<string> _cropList;

		private List<string> _blightList;

		private frmEditCommodity fre;

		private string _scopeName;

		private IContainer components;

		public Label P_title;

		private TableLayoutPanel tableLayoutPanel1;

		private Button button_Print;

		private Button button_Cancel;

		private Button button_UserPair;

		private Label label1;

		private Label label2;

		public frmBatchPrintCommodity_pair(string barCode, List<string> cropList, List<string> blightList, frmEditCommodity fme, string scopeName)
		{
			_barCode = barCode;
			_cropList = cropList;
			_blightList = blightList;
			fre = fme;
			_scopeName = scopeName;
			InitializeComponent();
		}

		private void frmBatchPrintCommodity_Load(object sender, EventArgs e)
		{
			displayTempCommodity();
		}

		private void displayTempCommodity()
		{
			if (_cropList.Count > 0 && _blightList.Count > 0)
			{
				string strSelectField = "code,name";
				string text = "code in (";
				for (int i = 0; i < _cropList.Count; i++)
				{
					text = text + "{" + i + "},";
				}
				text = text.Substring(0, text.Length - 1) + ")";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, strSelectField, "HyCrop", text, "", null, _cropList.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					dictionary.Add(dataTable.Rows[j]["code"].ToString(), dataTable.Rows[j]["name"].ToString());
				}
				string strSelectField2 = "code,name";
				string text2 = "code in (";
				for (int k = 0; k < _blightList.Count; k++)
				{
					text2 = text2 + "{" + k + "},";
				}
				text2 = text2.Substring(0, text2.Length - 1) + ")";
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, strSelectField2, "HyBlight", text2, "", null, _blightList.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
				for (int l = 0; l < dataTable2.Rows.Count; l++)
				{
					dictionary2.Add(dataTable2.Rows[l]["code"].ToString(), dataTable2.Rows[l]["name"].ToString());
				}
				tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
				tableLayoutPanel1.RowCount = _cropList.Count + 1;
				for (int m = 0; m < _cropList.Count; m++)
				{
					CheckBox checkBox = new CheckBox();
					checkBox.AutoSize = false;
					checkBox.CheckAlign = ContentAlignment.MiddleCenter;
					checkBox.Dock = DockStyle.Fill;
					checkBox.Name = m.ToString();
					Label label = new Label();
					label.Text = dictionary[_cropList[m]];
					label.AutoSize = false;
					label.TextAlign = ContentAlignment.MiddleCenter;
					label.Dock = DockStyle.Fill;
					label.BackColor = Color.Transparent;
					label.Anchor = AnchorStyles.None;
					Label label2 = new Label();
					label2.Text = dictionary2[_blightList[m]];
					label2.AutoSize = false;
					label2.TextAlign = ContentAlignment.MiddleCenter;
					label2.Dock = DockStyle.Fill;
					label2.BackColor = Color.Transparent;
					label2.Anchor = AnchorStyles.None;
					tableLayoutPanel1.Controls.Add(checkBox, 0, m + 1);
					tableLayoutPanel1.Controls.Add(label, 1, m + 1);
					tableLayoutPanel1.Controls.Add(label2, 2, m + 1);
					tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
				}
				tableLayoutPanel1.Padding = new Padding(0, 0, 6, 0);
			}
		}

		private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			if (e.Row == 0)
			{
				using (SolidBrush brush = new SolidBrush(Color.FromArgb(102, 102, 102)))
				{
					e.Graphics.FillRectangle(brush, e.CellBounds);
				}
			}
		}

		private void button_Print_Click(object sender, EventArgs e)
		{
			if (_cropList.Count > 0 && _blightList.Count > 0)
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				int num = 0;
				for (int i = 1; i < tableLayoutPanel1.RowCount; i++)
				{
					if (((CheckBox)tableLayoutPanel1.GetControlFromPosition(0, i)).Checked)
					{
						list.Add(_cropList[i - 1]);
						list2.Add(_blightList[i - 1]);
						num++;
					}
				}
				if (num > 0)
				{
					new Commodity_barcode(_barCode, list, list2).ShowDialog();
				}
				else
				{
					AutoClosingMessageBox.Show("無勾選用藥指引配對");
				}
			}
			else
			{
				AutoClosingMessageBox.Show("無用藥指引配對");
			}
		}

		private void button_Cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button_UserPair_Click(object sender, EventArgs e)
		{
			if (Program.IsCropPestRange_NEW)
			{
				frmCropPestRange_NEW frmCropPestRange_NEW = new frmCropPestRange_NEW(fre, 1, _barCode, 3);
				frmCropPestRange_NEW.Location = new Point(fre.Location.X, fre.Location.Y);
				frmCropPestRange_NEW.Show();
				Hide();
				fre.Hide();
			}
			else
			{
				frmCropGuideRange_Mangement frmCropGuideRange_Mangement = new frmCropGuideRange_Mangement(fre, 1, _barCode, _scopeName);
				frmCropGuideRange_Mangement.Location = new Point(fre.Location.X, fre.Location.Y);
				frmCropGuideRange_Mangement.Show();
				Hide();
				fre.Hide();
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
			P_title = new System.Windows.Forms.Label();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			button_Print = new System.Windows.Forms.Button();
			button_Cancel = new System.Windows.Forms.Button();
			button_UserPair = new System.Windows.Forms.Button();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			P_title.AutoSize = true;
			P_title.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			P_title.Location = new System.Drawing.Point(35, 24);
			P_title.Name = "P_title";
			P_title.Size = new System.Drawing.Size(200, 24);
			P_title.TabIndex = 52;
			P_title.Text = "常用用藥配對條碼列印";
			P_title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			tableLayoutPanel1.AutoScroll = true;
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35f));
			tableLayoutPanel1.Controls.Add(label2, 2, 0);
			tableLayoutPanel1.Controls.Add(label1, 1, 0);
			tableLayoutPanel1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
			tableLayoutPanel1.Location = new System.Drawing.Point(39, 68);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel1.MaximumSize = new System.Drawing.Size(829, 371);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30f));
			tableLayoutPanel1.Size = new System.Drawing.Size(829, 30);
			tableLayoutPanel1.TabIndex = 33;
			tableLayoutPanel1.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(tableLayoutPanel1_CellPaint);
			label2.AutoSize = true;
			label2.BackColor = System.Drawing.Color.Transparent;
			label2.Dock = System.Windows.Forms.DockStyle.Fill;
			label2.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(541, 1);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(284, 28);
			label2.TabIndex = 56;
			label2.Text = "病蟲害";
			label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.Dock = System.Windows.Forms.DockStyle.Fill;
			label1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(252, 1);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(282, 28);
			label1.TabIndex = 56;
			label1.Text = "作物";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			button_Print.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			button_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button_Print.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			button_Print.ForeColor = System.Drawing.Color.White;
			button_Print.Location = new System.Drawing.Point(149, 488);
			button_Print.Name = "button_Print";
			button_Print.Size = new System.Drawing.Size(124, 34);
			button_Print.TabIndex = 44;
			button_Print.TabStop = false;
			button_Print.Text = "列印";
			button_Print.UseVisualStyleBackColor = false;
			button_Print.Click += new System.EventHandler(button_Print_Click);
			button_Cancel.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button_Cancel.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			button_Cancel.ForeColor = System.Drawing.Color.White;
			button_Cancel.Location = new System.Drawing.Point(279, 488);
			button_Cancel.Name = "button_Cancel";
			button_Cancel.Size = new System.Drawing.Size(124, 34);
			button_Cancel.TabIndex = 54;
			button_Cancel.TabStop = false;
			button_Cancel.Text = "取消選擇";
			button_Cancel.UseVisualStyleBackColor = false;
			button_Cancel.Click += new System.EventHandler(button_Cancel_Click);
			button_UserPair.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			button_UserPair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button_UserPair.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			button_UserPair.ForeColor = System.Drawing.Color.White;
			button_UserPair.Location = new System.Drawing.Point(531, 488);
			button_UserPair.Name = "button_UserPair";
			button_UserPair.Size = new System.Drawing.Size(124, 34);
			button_UserPair.TabIndex = 55;
			button_UserPair.TabStop = false;
			button_UserPair.Text = "新增使用範圍";
			button_UserPair.UseVisualStyleBackColor = false;
			button_UserPair.Click += new System.EventHandler(button_UserPair_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoScroll = true;
			BackColor = System.Drawing.Color.Silver;
			base.ClientSize = new System.Drawing.Size(904, 576);
			base.ControlBox = false;
			base.Controls.Add(button_UserPair);
			base.Controls.Add(button_Cancel);
			base.Controls.Add(button_Print);
			base.Controls.Add(tableLayoutPanel1);
			base.Controls.Add(P_title);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmBatchPrintCommodity_pair";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "frmBatchPrint";
			base.Load += new System.EventHandler(frmBatchPrintCommodity_Load);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
