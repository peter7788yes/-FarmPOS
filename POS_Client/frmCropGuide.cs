using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmCropGuide : MasterThinForm
	{
		public class ComboBoxItem
		{
			private string _value;

			private string _text;

			public string Value
			{
				get
				{
					return _value;
				}
			}

			public string Text
			{
				get
				{
					return _text;
				}
			}

			public ComboBoxItem(string cValue, string cText)
			{
				_value = cValue;
				_text = cText;
			}
		}

		public string cls1 = "";

		public string cls2 = "";

		public string cls3 = "";

		public string cls4 = "";

		public string cropcode = "";

		public int nowlevel;

		public frmMainShopSimple frs;

		public frmMainShopSimpleWithMoney frsm;

		private IContainer components;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private Panel panel3;

		private Label label3;

		private Panel panel4;

		private Label label4;

		private Panel panel5;

		private Label label5;

		private Label label7;

		private Label cropname;

		private Label label9;

		private Label label10;

		private Label label8;

		private Label label11;

		private DataGridView clslit1;

		private DataGridView clslit2;

		private DataGridView clslit3;

		private DataGridView clslit4;

		private Button button1;

		private DataGridViewTextBoxColumn cls1name;

		private DataGridViewTextBoxColumn cls1code;

		private DataGridViewTextBoxColumn cls2name;

		private DataGridViewTextBoxColumn cls2code;

		private DataGridViewTextBoxColumn cls3name;

		private DataGridViewTextBoxColumn cls3code;

		private DataGridViewTextBoxColumn cls4name;

		private DataGridViewTextBoxColumn cls4code;

		public frmCropGuide()
			: base("收銀作業")
		{
			InitializeComponent();
			string[] strWhereParameterArray = new string[0];
			foreach (DataRow row in ((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "HyCrop", "cat2 = '' and cat3='' and cat4=''", "cat1 asc", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows)
			{
				clslit1.Rows.Add(row["name"].ToString(), row["cat1"].ToString());
			}
		}

		public frmCropGuide(frmMainShopSimple fms)
			: base("收銀作業")
		{
			frs = fms;
			InitializeComponent();
			string[] strWhereParameterArray = new string[0];
			foreach (DataRow row in ((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct hc.cat1 as cat1,hc.name as name", "HyCrop as hc, Hyscope as hs", "hc.cat1 != '' and hc.cat2 = '' and hc.cat3='' and hc.cat4='' and hs.cropId like '%'||hc.cat1||'%' AND hs.isDelete in ('N','') ", "hc.cat1 asc", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows)
			{
				clslit1.Rows.Add(row["name"].ToString(), row["cat1"].ToString());
			}
		}

		public frmCropGuide(frmMainShopSimpleWithMoney fms)
			: base("收銀作業")
		{
			frsm = fms;
			InitializeComponent();
			string[] strWhereParameterArray = new string[0];
			foreach (DataRow row in ((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct hc.cat1 as cat1,hc.name as name", "HyCrop as hc,Hyscope as hs", "hc.cat1 != '' and hc.cat2 = '' and hc.cat3='' and hc.cat4='' and hs.cropId like '%'||hc.cat1||'%' AND hs.isDelete in ('N','') ", "hc.cat1 asc", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows)
			{
				clslit1.Rows.Add(row["name"].ToString(), row["cat1"].ToString());
			}
		}

		private void infolist_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			cls1 = clslit1.CurrentRow.Cells[1].Value.ToString();
			clslit2.Rows.Clear();
			cls2 = "";
			clslit3.Rows.Clear();
			cls3 = "";
			clslit4.Rows.Clear();
			cls4 = "";
			List<string> list = new List<string>();
			string[] strWhereParameterArray = new string[1]
			{
				cls1
			};
			if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
			{
				clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit1.CurrentRow.Cells[1].Value.ToString());
			}
			foreach (DataRow row in ((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct hc.cat2 as cat2, hc.name as name", "HyCrop as hc,Hyscope as hs", "hc.cat1 ={0} and hc.cat2 != '' and hc.cat3 ='' and hc.cat4='' and hs.cropId like '%'||hc.cat1||hc.cat2||'%' AND hs.isDelete in ('N','') ", "hc.cat2 asc", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows)
			{
				bool flag = false;
				foreach (string item in list)
				{
					if (item.Equals(row["cat2"].ToString()))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					clslit2.Rows.Add(row["name"].ToString(), row["cat2"].ToString());
					list.Add(row["cat2"].ToString());
				}
				flag = false;
			}
			clslit2.Columns[0].HeaderText = clslit1.CurrentRow.Cells[0].Value.ToString() + "(" + list.Count + ")";
			cropcode = cls1;
		}

		private void clslit2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			if (clslit2.CurrentRow.Index == 0 && clslit2.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
			{
				cropcode = cls1;
				nowlevel = 1;
				frmPestGuide frmPestGuide = (Program.SystemMode != 1) ? new frmPestGuide(this, frsm) : new frmPestGuide(this, frs);
				frmPestGuide.Location = new Point(base.Location.X, base.Location.Y);
				frmPestGuide.Show();
				Hide();
				return;
			}
			cls2 = clslit2.CurrentRow.Cells[1].Value.ToString();
			clslit3.Rows.Clear();
			clslit3.Columns[0].HeaderText = "請選擇上層作物";
			cls3 = "";
			clslit4.Rows.Clear();
			clslit4.Columns[0].HeaderText = "請選擇上層作物";
			cls4 = "";
			List<string> list = new List<string>();
			string[] strWhereParameterArray = new string[1]
			{
				cls1 + cls2
			};
			string[] strWhereParameterArray2 = new string[2]
			{
				cls1,
				cls2
			};
			if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
			{
				clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit2.CurrentRow.Cells[1].Value.ToString());
			}
			foreach (DataRow row in ((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct hc.cat3 as cat3, hc.name as name", "HyCrop as hc,Hyscope as hs", "hc.cat1 ={0} and hc.cat2 = {1} and hc.cat3 !='' and hc.cat4='' and hs.cropId like '%'||hc.cat1||hc.cat2||hc.cat3||'%' AND hs.isDelete in ('N','') ", "hc.cat3 asc", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable)).Rows)
			{
				bool flag = false;
				foreach (string item in list)
				{
					if (item.Equals(row["cat3"].ToString()))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					clslit3.Rows.Add(row["name"].ToString(), row["cat3"].ToString());
					list.Add(row["cat3"].ToString());
				}
				flag = false;
			}
			clslit3.Columns[0].HeaderText = clslit2.CurrentRow.Cells[0].Value.ToString() + "(" + list.Count + ")";
			cropcode = cls1 + cls2;
		}

		private void clslit3_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			if (clslit3.CurrentRow.Index == 0 && clslit3.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
			{
				cropcode = cls1 + cls2;
				nowlevel = 2;
				frmPestGuide frmPestGuide = (Program.SystemMode != 1) ? new frmPestGuide(this, frsm) : new frmPestGuide(this, frs);
				frmPestGuide.Location = new Point(base.Location.X, base.Location.Y);
				frmPestGuide.Show();
				Hide();
				return;
			}
			cls3 = clslit3.CurrentRow.Cells[1].Value.ToString();
			clslit4.Columns[0].HeaderText = "請選擇上層作物";
			clslit4.Rows.Clear();
			cls4 = "";
			List<string> list = new List<string>();
			string[] strWhereParameterArray = new string[1]
			{
				cls1 + cls2 + cls3
			};
			string[] strWhereParameterArray2 = new string[3]
			{
				cls1,
				cls2,
				cls3
			};
			if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
			{
				clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit3.CurrentRow.Cells[1].Value.ToString());
			}
			foreach (DataRow row in ((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct hc.cat1 as cat1,hc.cat2 as cat2,hc.cat3 as cat3,hc.cat4 as cat4, hc.name as name", "HyCrop as hc,Hyscope as hs", "hc.cat1 ={0} and hc.cat2 = {1} and hc.cat3 ={2} and hc.cat4 !='' and hs.cropId like '%'||hc.cat1||hc.cat2||hc.cat3||hc.cat4||'%' AND hs.isDelete in ('N','') ", "hc.cat4 asc", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable)).Rows)
			{
				bool flag = false;
				foreach (string item in list)
				{
					if (item.Equals(row["cat4"].ToString()))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					clslit4.Rows.Add(row["name"].ToString(), row["cat4"].ToString());
					list.Add(row["cat4"].ToString());
				}
				flag = false;
			}
			clslit4.Columns[0].HeaderText = clslit3.CurrentRow.Cells[0].Value.ToString() + "(" + list.Count + ")";
			cropcode = cls1 + cls2 + cls3;
		}

		private void clslit4_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				if (clslit4.CurrentRow.Index == 0 && clslit4.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
				{
					cropcode = cls1 + cls2 + cls3;
					nowlevel = 3;
					frmPestGuide frmPestGuide = (Program.SystemMode != 1) ? new frmPestGuide(this, frsm) : new frmPestGuide(this, frs);
					frmPestGuide.Location = new Point(base.Location.X, base.Location.Y);
					frmPestGuide.Show();
					Hide();
				}
				else
				{
					cls4 = clslit4.CurrentRow.Cells[1].Value.ToString();
					cropcode = cls1 + cls2 + cls3 + cls4;
					nowlevel = 4;
					frmPestGuide frmPestGuide2 = (Program.SystemMode != 1) ? new frmPestGuide(this, frsm) : new frmPestGuide(this, frs);
					frmPestGuide2.Location = new Point(base.Location.X, base.Location.Y);
					frmPestGuide2.Show();
					Hide();
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (Program.SystemMode == 1)
			{
				frs.Show();
				frs.setfocus();
			}
			else
			{
				frsm.Show();
				frsm.setfocus();
			}
			Dispose();
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			cropname = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			clslit1 = new System.Windows.Forms.DataGridView();
			clslit2 = new System.Windows.Forms.DataGridView();
			clslit3 = new System.Windows.Forms.DataGridView();
			clslit4 = new System.Windows.Forms.DataGridView();
			button1 = new System.Windows.Forms.Button();
			cls1name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls1code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls2name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls2code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls3name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls3code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls4name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls4code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel1.SuspendLayout();
			panel3.SuspendLayout();
			panel4.SuspendLayout();
			panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)clslit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit2).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit3).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit4).BeginInit();
			SuspendLayout();
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 150);
			panel1.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.ForeColor = System.Drawing.Color.White;
			panel1.Location = new System.Drawing.Point(12, 42);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(242, 73);
			panel1.TabIndex = 52;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(78, 12);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(86, 48);
			label2.TabIndex = 1;
			label2.Text = "Step1\r\n作物選擇";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.Location = new System.Drawing.Point(78, 14);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(86, 48);
			label1.TabIndex = 0;
			label1.Text = "Step1\r\n作物選擇";
			panel3.BackColor = System.Drawing.Color.Transparent;
			panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel3.Controls.Add(label3);
			panel3.Location = new System.Drawing.Point(261, 42);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(238, 73);
			panel3.TabIndex = 53;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.Black;
			label3.Location = new System.Drawing.Point(67, 12);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(105, 48);
			label3.TabIndex = 2;
			label3.Text = "Step2\r\n病蟲害選擇";
			panel4.BackColor = System.Drawing.Color.Transparent;
			panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel4.Controls.Add(label4);
			panel4.Location = new System.Drawing.Point(506, 42);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(224, 73);
			panel4.TabIndex = 53;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.Black;
			label4.Location = new System.Drawing.Point(51, 12);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(132, 48);
			label4.TabIndex = 2;
			label4.Text = "Step3\r\n用藥/商品選擇";
			panel5.BackColor = System.Drawing.Color.Transparent;
			panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel5.Controls.Add(label5);
			panel5.Location = new System.Drawing.Point(737, 42);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(231, 73);
			panel5.TabIndex = 53;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Black;
			label5.Location = new System.Drawing.Point(62, 12);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(86, 48);
			label5.TabIndex = 3;
			label5.Text = "End\r\n返回收銀";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label7.Location = new System.Drawing.Point(13, 519);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(86, 24);
			label7.TabIndex = 60;
			label7.Text = "【作物】";
			cropname.AutoSize = true;
			cropname.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cropname.Location = new System.Drawing.Point(90, 519);
			cropname.Name = "cropname";
			cropname.Size = new System.Drawing.Size(86, 24);
			cropname.TabIndex = 62;
			cropname.Text = "作物名稱";
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label9.Location = new System.Drawing.Point(248, 519);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(105, 24);
			label9.TabIndex = 63;
			label9.Text = "【病蟲害】";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label10.Location = new System.Drawing.Point(339, 519);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(105, 24);
			label10.TabIndex = 64;
			label10.Text = "病蟲害名稱";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label8.Location = new System.Drawing.Point(495, 519);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(86, 24);
			label8.TabIndex = 65;
			label8.Text = "【用藥】";
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label11.Location = new System.Drawing.Point(570, 519);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(105, 24);
			label11.TabIndex = 66;
			label11.Text = "用藥害名稱";
			clslit1.AllowUserToAddRows = false;
			clslit1.AllowUserToDeleteRows = false;
			clslit1.AllowUserToResizeColumns = false;
			clslit1.AllowUserToResizeRows = false;
			clslit1.Anchor = System.Windows.Forms.AnchorStyles.None;
			clslit1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			clslit1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			clslit1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			clslit1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 12f);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			clslit1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			clslit1.Columns.AddRange(cls1name, cls1code);
			clslit1.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			clslit1.DefaultCellStyle = dataGridViewCellStyle2;
			clslit1.EnableHeadersVisualStyles = false;
			clslit1.GridColor = System.Drawing.SystemColors.ActiveBorder;
			clslit1.Location = new System.Drawing.Point(12, 171);
			clslit1.MultiSelect = false;
			clslit1.Name = "clslit1";
			clslit1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			clslit1.RowHeadersVisible = false;
			clslit1.RowTemplate.Height = 40;
			clslit1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			clslit1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			clslit1.Size = new System.Drawing.Size(242, 461);
			clslit1.TabIndex = 67;
			clslit1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(infolist_CellContentClick);
			clslit2.AllowUserToAddRows = false;
			clslit2.AllowUserToDeleteRows = false;
			clslit2.AllowUserToResizeColumns = false;
			clslit2.AllowUserToResizeRows = false;
			clslit2.Anchor = System.Windows.Forms.AnchorStyles.None;
			clslit2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			clslit2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			clslit2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			clslit2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 12f);
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			clslit2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			clslit2.Columns.AddRange(cls2name, cls2code);
			clslit2.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			clslit2.DefaultCellStyle = dataGridViewCellStyle5;
			clslit2.EnableHeadersVisualStyles = false;
			clslit2.GridColor = System.Drawing.SystemColors.ActiveBorder;
			clslit2.Location = new System.Drawing.Point(261, 171);
			clslit2.MultiSelect = false;
			clslit2.Name = "clslit2";
			clslit2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit2.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			clslit2.RowHeadersVisible = false;
			clslit2.RowTemplate.Height = 40;
			clslit2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			clslit2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			clslit2.Size = new System.Drawing.Size(238, 461);
			clslit2.TabIndex = 68;
			clslit2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit2_CellContentClick);
			clslit3.AllowUserToAddRows = false;
			clslit3.AllowUserToDeleteRows = false;
			clslit3.AllowUserToResizeColumns = false;
			clslit3.AllowUserToResizeRows = false;
			clslit3.Anchor = System.Windows.Forms.AnchorStyles.None;
			clslit3.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			clslit3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			clslit3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			clslit3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle7.Font = new System.Drawing.Font("微軟正黑體", 12f);
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			clslit3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			clslit3.Columns.AddRange(cls3name, cls3code);
			clslit3.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			clslit3.DefaultCellStyle = dataGridViewCellStyle8;
			clslit3.EnableHeadersVisualStyles = false;
			clslit3.GridColor = System.Drawing.SystemColors.ActiveBorder;
			clslit3.Location = new System.Drawing.Point(506, 171);
			clslit3.MultiSelect = false;
			clslit3.Name = "clslit3";
			clslit3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit3.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			clslit3.RowHeadersVisible = false;
			clslit3.RowTemplate.Height = 40;
			clslit3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			clslit3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			clslit3.Size = new System.Drawing.Size(224, 461);
			clslit3.TabIndex = 69;
			clslit3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit3_CellContentClick);
			clslit4.AllowUserToAddRows = false;
			clslit4.AllowUserToDeleteRows = false;
			clslit4.AllowUserToResizeColumns = false;
			clslit4.AllowUserToResizeRows = false;
			clslit4.Anchor = System.Windows.Forms.AnchorStyles.None;
			clslit4.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			clslit4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			clslit4.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			clslit4.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle10.Font = new System.Drawing.Font("微軟正黑體", 12f);
			dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			clslit4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			clslit4.Columns.AddRange(cls4name, cls4code);
			clslit4.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			clslit4.DefaultCellStyle = dataGridViewCellStyle11;
			clslit4.EnableHeadersVisualStyles = false;
			clslit4.GridColor = System.Drawing.SystemColors.ActiveBorder;
			clslit4.Location = new System.Drawing.Point(737, 171);
			clslit4.MultiSelect = false;
			clslit4.Name = "clslit4";
			clslit4.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit4.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
			clslit4.RowHeadersVisible = false;
			clslit4.RowTemplate.Height = 40;
			clslit4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			clslit4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			clslit4.Size = new System.Drawing.Size(231, 461);
			clslit4.TabIndex = 70;
			clslit4.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit4_CellContentClick);
			button1.Font = new System.Drawing.Font("微軟正黑體", 12.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.Location = new System.Drawing.Point(870, 127);
			button1.Margin = new System.Windows.Forms.Padding(0);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(97, 33);
			button1.TabIndex = 72;
			button1.Text = "回上一步";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			cls1name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			cls1name.DefaultCellStyle = dataGridViewCellStyle13;
			cls1name.HeaderText = "請選擇作物類別";
			cls1name.Name = "cls1name";
			cls1name.ReadOnly = true;
			cls1name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls1code.HeaderText = "代碼";
			cls1code.Name = "cls1code";
			cls1code.Visible = false;
			cls2name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			cls2name.DefaultCellStyle = dataGridViewCellStyle14;
			cls2name.HeaderText = "請選擇上層作物";
			cls2name.Name = "cls2name";
			cls2name.ReadOnly = true;
			cls2name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls2code.HeaderText = "代碼";
			cls2code.Name = "cls2code";
			cls2code.Visible = false;
			cls3name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			cls3name.DefaultCellStyle = dataGridViewCellStyle15;
			cls3name.HeaderText = "請選擇上層作物";
			cls3name.Name = "cls3name";
			cls3name.ReadOnly = true;
			cls3name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls3code.HeaderText = "代碼";
			cls3code.Name = "cls3code";
			cls3code.Visible = false;
			cls4name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			cls4name.DefaultCellStyle = dataGridViewCellStyle16;
			cls4name.HeaderText = "請選擇上層作物";
			cls4name.Name = "cls4name";
			cls4name.ReadOnly = true;
			cls4name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls4code.HeaderText = "代碼";
			cls4code.Name = "cls4code";
			cls4code.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(981, 662);
			base.Controls.Add(button1);
			base.Controls.Add(clslit4);
			base.Controls.Add(clslit3);
			base.Controls.Add(clslit2);
			base.Controls.Add(clslit1);
			base.Controls.Add(label11);
			base.Controls.Add(label8);
			base.Controls.Add(label10);
			base.Controls.Add(label9);
			base.Controls.Add(cropname);
			base.Controls.Add(label7);
			base.Controls.Add(panel5);
			base.Controls.Add(panel4);
			base.Controls.Add(panel3);
			base.Controls.Add(panel1);
			base.Name = "frmCropGuide";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Form3";
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(panel3, 0);
			base.Controls.SetChildIndex(panel4, 0);
			base.Controls.SetChildIndex(panel5, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(cropname, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(clslit1, 0);
			base.Controls.SetChildIndex(clslit2, 0);
			base.Controls.SetChildIndex(clslit3, 0);
			base.Controls.SetChildIndex(clslit4, 0);
			base.Controls.SetChildIndex(button1, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)clslit1).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit2).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit3).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit4).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
