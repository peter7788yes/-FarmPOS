using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmCropGuideRangeForSearch : MasterThinForm
	{
		public string cls1 = "";

		public string cls2 = "";

		public string cls3 = "";

		public string cls4 = "";

		public string cropcode = "";

		public int nowlevel;

		public frmMainShopSimple frs;

		public frmMainShopSimpleWithMoney frsm;

		private string barcode;

		private int count;

		private List<string> cropId = new List<string>();

		private List<string> cropcls1 = new List<string>();

		private List<string> cropcls2 = new List<string>();

		private List<string> cropcls3 = new List<string>();

		private List<string> cropcls4 = new List<string>();

		private IContainer components;

		private DataGridView clslit1;

		private DataGridView clslit2;

		private DataGridView clslit3;

		private DataGridView clslit4;

		private Label label6;

		private Panel panel5;

		private Label label5;

		private Panel panel3;

		private Label label3;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private DataGridViewTextBoxColumn cls1name;

		private DataGridViewTextBoxColumn cls1code;

		private Button button1;

		private DataGridViewTextBoxColumn cls2name;

		private DataGridViewTextBoxColumn cls2code;

		private DataGridViewTextBoxColumn cls3name;

		private DataGridViewTextBoxColumn cls3code;

		private DataGridViewTextBoxColumn cls4name;

		private DataGridViewTextBoxColumn cls4code;

		public frmCropGuideRangeForSearch()
			: base("收銀作業")
		{
		}

		public frmCropGuideRangeForSearch(frmMainShopSimple fms, int count, string barcode1)
			: base("收銀作業")
		{
			frs = fms;
			this.count = count;
			barcode = barcode1;
			InitializeComponent();
			string[] strWhereParameterArray = new string[1]
			{
				barcode
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				string[] strWhereParameterArray2 = new string[3]
				{
					row["pesticideId"].ToString(),
					row["formCode"].ToString(),
					row["contents"].ToString()
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cropId", "HyScope", "pesticideId = {0} and formCode ={1} and contents={2} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count <= 0)
				{
					continue;
				}
				foreach (DataRow row2 in dataTable2.Rows)
				{
					cropId.Add(row2["cropId"].ToString());
					bool flag = false;
					string[] strWhereParameterArray3 = new string[1]
					{
						row2["cropId"].ToString()
					};
					DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat1,name", "HyCrop", "{0} like '%'||cat1||'%' and cat2 ='' and cat3 ='' and cat4 =''", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable3.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row3 in dataTable3.Rows)
					{
						foreach (string item in cropcls1)
						{
							if (item.Equals(row3["cat1"].ToString()))
							{
								flag = true;
							}
						}
						if (!flag)
						{
							clslit1.Rows.Add(row3["name"].ToString(), row3["cat1"].ToString());
							cropcls1.Add(row3["cat1"].ToString());
						}
						flag = false;
					}
				}
			}
		}

		public frmCropGuideRangeForSearch(frmMainShopSimpleWithMoney fms, int count, string barcode1)
			: base("收銀作業")
		{
			frsm = fms;
			this.count = count;
			barcode = barcode1;
			InitializeComponent();
			string[] strWhereParameterArray = new string[1]
			{
				barcode
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				string[] strWhereParameterArray2 = new string[3]
				{
					row["pesticideId"].ToString(),
					row["formCode"].ToString(),
					row["contents"].ToString()
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cropId", "HyScope", "pesticideId = {0} and formCode ={1} and contents={2} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count <= 0)
				{
					continue;
				}
				foreach (DataRow row2 in dataTable2.Rows)
				{
					cropId.Add(row2["cropId"].ToString());
					bool flag = false;
					string[] strWhereParameterArray3 = new string[1]
					{
						row2["cropId"].ToString()
					};
					DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat1,name", "HyCrop", "{0} like '%'||cat1||'%' and cat2 ='' and cat3 ='' and cat4 =''", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable3.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row3 in dataTable3.Rows)
					{
						foreach (string item in cropcls1)
						{
							if (item.Equals(row3["cat1"].ToString()))
							{
								flag = true;
							}
						}
						if (!flag)
						{
							clslit1.Rows.Add(row3["name"].ToString(), row3["cat1"].ToString());
							cropcls1.Add(row3["cat1"].ToString());
						}
						flag = false;
					}
				}
			}
		}

		private void infolist_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
			{
				return;
			}
			cls1 = clslit1.CurrentRow.Cells[1].Value.ToString();
			clslit2.Rows.Clear();
			cls2 = "";
			clslit3.Rows.Clear();
			cls3 = "";
			clslit4.Rows.Clear();
			cls4 = "";
			cropcls2.Clear();
			cropcls3.Clear();
			cropcls4.Clear();
			new List<string>();
			foreach (string item in cropId)
			{
				if (item.Equals(cls1))
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」分類", cls1);
				}
			}
			string[] strWhereParameterArray = new string[1]
			{
				barcode
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					string[] strWhereParameterArray2 = new string[3]
					{
						row["pesticideId"].ToString(),
						row["formCode"].ToString(),
						row["contents"].ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cropId", "HyScope", "pesticideId = {0} and formCode ={1} and contents={2} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row2 in dataTable2.Rows)
					{
						if (row2["cropId"].ToString().Length < 3)
						{
							continue;
						}
						bool flag = false;
						string[] strWhereParameterArray3 = new string[2]
						{
							row2["cropId"].ToString(),
							cls1
						};
						DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat2,name", "HyCrop", "{0} like '%'||cat1||cat2||'%' and cat1={1} and cat2 !='' and cat3 ='' and cat4 =''", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
						if (dataTable3.Rows.Count <= 0)
						{
							continue;
						}
						foreach (DataRow row3 in dataTable3.Rows)
						{
							foreach (string item2 in cropcls2)
							{
								if (item2.Equals(row3["cat2"].ToString()))
								{
									flag = true;
								}
							}
							if (!flag)
							{
								clslit2.Rows.Add(row3["name"].ToString(), row3["cat2"].ToString());
								cropcls2.Add(row3["cat2"].ToString());
							}
							flag = false;
						}
					}
				}
			}
			clslit2.Columns[0].HeaderText = clslit1.CurrentRow.Cells[0].Value.ToString() + "(" + cropcls2.Count + ")";
			cropcode = cls1;
		}

		private void clslit2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
			{
				return;
			}
			if (clslit2.CurrentRow.Index == 0 && clslit2.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
			{
				cropcode = cls1;
				nowlevel = 1;
				if (Program.SystemMode == 1)
				{
					frmPestGuideRangeForSearch frmPestGuideRangeForSearch = new frmPestGuideRangeForSearch(this, frs, count, barcode);
					frmPestGuideRangeForSearch.Location = new Point(base.Location.X, base.Location.Y);
					frmPestGuideRangeForSearch.Show();
					Hide();
				}
				else
				{
					frmPestGuideRangeForSearch frmPestGuideRangeForSearch2 = new frmPestGuideRangeForSearch(this, frsm, count, barcode);
					frmPestGuideRangeForSearch2.Location = new Point(base.Location.X, base.Location.Y);
					frmPestGuideRangeForSearch2.Show();
					Hide();
				}
				return;
			}
			cls2 = clslit2.CurrentRow.Cells[1].Value.ToString();
			clslit3.Rows.Clear();
			clslit3.Columns[0].HeaderText = "請選擇上層作物";
			cls3 = "";
			clslit4.Rows.Clear();
			clslit4.Columns[0].HeaderText = "請選擇上層作物";
			cls4 = "";
			cropcls3.Clear();
			cropcls4.Clear();
			new List<string>();
			foreach (string item in cropId)
			{
				if (item.Equals(cls1 + cls2))
				{
					clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」分類", cls1 + cls2);
				}
			}
			string[] strWhereParameterArray = new string[1]
			{
				barcode
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					string[] strWhereParameterArray2 = new string[3]
					{
						row["pesticideId"].ToString(),
						row["formCode"].ToString(),
						row["contents"].ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cropId", "HyScope", "pesticideId = {0} and formCode ={1} and contents={2} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row2 in dataTable2.Rows)
					{
						if (row2["cropId"].ToString().Length < 5)
						{
							continue;
						}
						bool flag = false;
						string[] strWhereParameterArray3 = new string[3]
						{
							row2["cropId"].ToString(),
							cls1,
							cls2
						};
						DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat3,name", "HyCrop", "{0} like '%'||cat1||cat2||cat3||'%' and cat1={1} and cat2 ={2} and cat3 !='' and cat4 =''", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
						if (dataTable3.Rows.Count <= 0)
						{
							continue;
						}
						foreach (DataRow row3 in dataTable3.Rows)
						{
							foreach (string item2 in cropcls3)
							{
								if (item2.Equals(row3["cat3"].ToString()))
								{
									flag = true;
								}
							}
							if (!flag)
							{
								clslit3.Rows.Add(row3["name"].ToString(), row3["cat3"].ToString());
								cropcls3.Add(row3["cat3"].ToString());
							}
							flag = false;
						}
					}
				}
			}
			clslit3.Columns[0].HeaderText = clslit2.CurrentRow.Cells[0].Value.ToString() + "(" + cropcls3.Count + ")";
			cropcode = cls1 + cls2;
		}

		private void clslit3_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
			{
				return;
			}
			if (clslit3.CurrentRow.Index == 0 && clslit3.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
			{
				cropcode = cls1 + cls2;
				nowlevel = 2;
				if (Program.SystemMode == 1)
				{
					frmPestGuideRangeForSearch frmPestGuideRangeForSearch = new frmPestGuideRangeForSearch(this, frs, count, barcode);
					frmPestGuideRangeForSearch.Location = new Point(base.Location.X, base.Location.Y);
					frmPestGuideRangeForSearch.Show();
					Hide();
				}
				else
				{
					frmPestGuideRangeForSearch frmPestGuideRangeForSearch2 = new frmPestGuideRangeForSearch(this, frsm, count, barcode);
					frmPestGuideRangeForSearch2.Location = new Point(base.Location.X, base.Location.Y);
					frmPestGuideRangeForSearch2.Show();
					Hide();
				}
				return;
			}
			cls3 = clslit3.CurrentRow.Cells[1].Value.ToString();
			clslit4.Columns[0].HeaderText = "請選擇上層作物";
			clslit4.Rows.Clear();
			cls4 = "";
			cropcls4.Clear();
			new List<string>();
			foreach (string item in cropId)
			{
				if (item.Equals(cls1 + cls2 + cls3))
				{
					clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」分類", cls1 + cls2 + cls3);
				}
			}
			string[] strWhereParameterArray = new string[1]
			{
				barcode
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					string[] strWhereParameterArray2 = new string[3]
					{
						row["pesticideId"].ToString(),
						row["formCode"].ToString(),
						row["contents"].ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cropId", "HyScope", "pesticideId = {0} and formCode ={1} and contents={2} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row2 in dataTable2.Rows)
					{
						if (row2["cropId"].ToString().Length < 7)
						{
							continue;
						}
						bool flag = false;
						string[] strWhereParameterArray3 = new string[4]
						{
							row2["cropId"].ToString(),
							cls1,
							cls2,
							cls3
						};
						DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat4,name", "HyCrop", "{0} like '%'||cat1||cat2||cat3||cat4||'%' and cat1={1} and cat2 ={2} and cat3 ={3} and cat4 !=''", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
						if (dataTable3.Rows.Count <= 0)
						{
							continue;
						}
						foreach (DataRow row3 in dataTable3.Rows)
						{
							foreach (string item2 in cropcls4)
							{
								if (item2.Equals(row3["cat4"].ToString()))
								{
									flag = true;
								}
							}
							if (!flag)
							{
								clslit4.Rows.Add(row3["name"].ToString(), row3["cat4"].ToString());
								cropcls4.Add(row3["cat4"].ToString());
							}
							flag = false;
						}
					}
				}
			}
			clslit4.Columns[0].HeaderText = clslit3.CurrentRow.Cells[0].Value.ToString() + "(" + cropcls4.Count + ")";
			cropcode = cls1 + cls2 + cls3;
		}

		private void clslit4_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
			{
				return;
			}
			if (clslit4.CurrentRow.Index == 0 && clslit4.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
			{
				cropcode = cls1 + cls2 + cls3;
				nowlevel = 3;
				if (Program.SystemMode == 1)
				{
					frmPestGuideRangeForSearch frmPestGuideRangeForSearch = new frmPestGuideRangeForSearch(this, frs, count, barcode);
					frmPestGuideRangeForSearch.Location = new Point(base.Location.X, base.Location.Y);
					frmPestGuideRangeForSearch.Show();
					Hide();
				}
				else
				{
					frmPestGuideRangeForSearch frmPestGuideRangeForSearch2 = new frmPestGuideRangeForSearch(this, frsm, count, barcode);
					frmPestGuideRangeForSearch2.Location = new Point(base.Location.X, base.Location.Y);
					frmPestGuideRangeForSearch2.Show();
					Hide();
				}
				return;
			}
			cls4 = clslit4.CurrentRow.Cells[1].Value.ToString();
			cropcode = cls1 + cls2 + cls3 + cls4;
			nowlevel = 4;
			if (Program.SystemMode == 1)
			{
				frmPestGuideRangeForSearch frmPestGuideRangeForSearch3 = new frmPestGuideRangeForSearch(this, frs, count, barcode);
				frmPestGuideRangeForSearch3.Location = new Point(base.Location.X, base.Location.Y);
				frmPestGuideRangeForSearch3.Show();
				Hide();
			}
			else
			{
				frmPestGuideRangeForSearch frmPestGuideRangeForSearch4 = new frmPestGuideRangeForSearch(this, frsm, count, barcode);
				frmPestGuideRangeForSearch4.Location = new Point(base.Location.X, base.Location.Y);
				frmPestGuideRangeForSearch4.Show();
				Hide();
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
			clslit1 = new System.Windows.Forms.DataGridView();
			cls1name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls1code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			clslit2 = new System.Windows.Forms.DataGridView();
			cls2name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls2code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			clslit3 = new System.Windows.Forms.DataGridView();
			cls3name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls3code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			clslit4 = new System.Windows.Forms.DataGridView();
			cls4name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls4code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label6 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit2).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit3).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit4).BeginInit();
			panel5.SuspendLayout();
			panel3.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 159);
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
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(175, 164, 134);
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
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(254, 234, 225);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			clslit1.DefaultCellStyle = dataGridViewCellStyle2;
			clslit1.EnableHeadersVisualStyles = false;
			clslit1.GridColor = System.Drawing.SystemColors.ActiveBorder;
			clslit1.Location = new System.Drawing.Point(12, 171);
			clslit1.Name = "clslit1";
			clslit1.ReadOnly = true;
			clslit1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(254, 234, 225);
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
			cls1name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			cls1name.DefaultCellStyle = dataGridViewCellStyle4;
			cls1name.HeaderText = "請選擇作物類別";
			cls1name.Name = "cls1name";
			cls1name.ReadOnly = true;
			cls1name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls1code.HeaderText = "代碼";
			cls1code.Name = "cls1code";
			cls1code.ReadOnly = true;
			cls1code.Visible = false;
			clslit2.AllowUserToAddRows = false;
			clslit2.AllowUserToDeleteRows = false;
			clslit2.AllowUserToResizeColumns = false;
			clslit2.AllowUserToResizeRows = false;
			clslit2.Anchor = System.Windows.Forms.AnchorStyles.None;
			clslit2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			clslit2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			clslit2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			clslit2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 12f);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(175, 164, 134);
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			clslit2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			clslit2.Columns.AddRange(cls2name, cls2code);
			clslit2.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(254, 234, 225);
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			clslit2.DefaultCellStyle = dataGridViewCellStyle6;
			clslit2.EnableHeadersVisualStyles = false;
			clslit2.GridColor = System.Drawing.SystemColors.ActiveBorder;
			clslit2.Location = new System.Drawing.Point(260, 171);
			clslit2.Name = "clslit2";
			clslit2.ReadOnly = true;
			clslit2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(254, 234, 225);
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit2.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
			clslit2.RowHeadersVisible = false;
			clslit2.RowTemplate.Height = 40;
			clslit2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			clslit2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			clslit2.Size = new System.Drawing.Size(238, 461);
			clslit2.TabIndex = 68;
			clslit2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit2_CellContentClick);
			cls2name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			cls2name.DefaultCellStyle = dataGridViewCellStyle8;
			cls2name.HeaderText = "請選擇上層作物";
			cls2name.Name = "cls2name";
			cls2name.ReadOnly = true;
			cls2name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls2code.HeaderText = "代碼";
			cls2code.Name = "cls2code";
			cls2code.ReadOnly = true;
			cls2code.Visible = false;
			clslit3.AllowUserToAddRows = false;
			clslit3.AllowUserToDeleteRows = false;
			clslit3.AllowUserToResizeColumns = false;
			clslit3.AllowUserToResizeRows = false;
			clslit3.Anchor = System.Windows.Forms.AnchorStyles.None;
			clslit3.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			clslit3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			clslit3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			clslit3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle9.Font = new System.Drawing.Font("微軟正黑體", 12f);
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(5);
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(175, 164, 134);
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
			clslit3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			clslit3.Columns.AddRange(cls3name, cls3code);
			clslit3.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(254, 234, 225);
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			clslit3.DefaultCellStyle = dataGridViewCellStyle10;
			clslit3.EnableHeadersVisualStyles = false;
			clslit3.GridColor = System.Drawing.SystemColors.ActiveBorder;
			clslit3.Location = new System.Drawing.Point(508, 171);
			clslit3.Name = "clslit3";
			clslit3.ReadOnly = true;
			clslit3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(254, 234, 225);
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit3.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
			clslit3.RowHeadersVisible = false;
			clslit3.RowTemplate.Height = 40;
			clslit3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			clslit3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			clslit3.Size = new System.Drawing.Size(224, 461);
			clslit3.TabIndex = 69;
			clslit3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit3_CellContentClick);
			cls3name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
			cls3name.DefaultCellStyle = dataGridViewCellStyle12;
			cls3name.HeaderText = "請選擇上層作物";
			cls3name.Name = "cls3name";
			cls3name.ReadOnly = true;
			cls3name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls3code.HeaderText = "代碼";
			cls3code.Name = "cls3code";
			cls3code.ReadOnly = true;
			cls3code.Visible = false;
			clslit4.AllowUserToAddRows = false;
			clslit4.AllowUserToDeleteRows = false;
			clslit4.AllowUserToResizeColumns = false;
			clslit4.AllowUserToResizeRows = false;
			clslit4.Anchor = System.Windows.Forms.AnchorStyles.None;
			clslit4.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			clslit4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			clslit4.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			clslit4.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle13.Font = new System.Drawing.Font("微軟正黑體", 12f);
			dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle13.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(175, 164, 134);
			dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
			clslit4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			clslit4.Columns.AddRange(cls4name, cls4code);
			clslit4.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle14.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(254, 234, 225);
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			clslit4.DefaultCellStyle = dataGridViewCellStyle14;
			clslit4.EnableHeadersVisualStyles = false;
			clslit4.GridColor = System.Drawing.SystemColors.ActiveBorder;
			clslit4.Location = new System.Drawing.Point(738, 171);
			clslit4.Name = "clslit4";
			clslit4.ReadOnly = true;
			clslit4.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle15.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(254, 234, 225);
			dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit4.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
			clslit4.RowHeadersVisible = false;
			clslit4.RowTemplate.Height = 40;
			clslit4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			clslit4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			clslit4.Size = new System.Drawing.Size(231, 461);
			clslit4.TabIndex = 70;
			clslit4.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit4_CellContentClick);
			cls4name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
			cls4name.DefaultCellStyle = dataGridViewCellStyle16;
			cls4name.HeaderText = "請選擇上層作物";
			cls4name.Name = "cls4name";
			cls4name.ReadOnly = true;
			cls4name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls4code.HeaderText = "代碼";
			cls4code.Name = "cls4code";
			cls4code.ReadOnly = true;
			cls4code.Visible = false;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.Location = new System.Drawing.Point(13, 132);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(86, 24);
			label6.TabIndex = 54;
			label6.Text = "作物分類";
			panel5.BackColor = System.Drawing.Color.Transparent;
			panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel5.Controls.Add(label5);
			panel5.Location = new System.Drawing.Point(508, 38);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(231, 73);
			panel5.TabIndex = 73;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Black;
			label5.Location = new System.Drawing.Point(62, 12);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(86, 48);
			label5.TabIndex = 3;
			label5.Text = "End\r\n返回收銀";
			panel3.BackColor = System.Drawing.Color.Transparent;
			panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel3.Controls.Add(label3);
			panel3.Location = new System.Drawing.Point(261, 38);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(238, 73);
			panel3.TabIndex = 72;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.Black;
			label3.Location = new System.Drawing.Point(67, 12);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(105, 48);
			label3.TabIndex = 2;
			label3.Text = "Step2\r\n病蟲害選擇";
			panel1.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.ForeColor = System.Drawing.Color.White;
			panel1.Location = new System.Drawing.Point(12, 38);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(242, 73);
			panel1.TabIndex = 71;
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
			button1.Font = new System.Drawing.Font("微軟正黑體", 12.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.Location = new System.Drawing.Point(872, 123);
			button1.Margin = new System.Windows.Forms.Padding(0);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(97, 33);
			button1.TabIndex = 74;
			button1.Text = "回上一步";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(981, 671);
			base.Controls.Add(button1);
			base.Controls.Add(panel5);
			base.Controls.Add(panel3);
			base.Controls.Add(panel1);
			base.Controls.Add(clslit4);
			base.Controls.Add(clslit3);
			base.Controls.Add(clslit2);
			base.Controls.Add(clslit1);
			base.Controls.Add(label6);
			base.Name = "frmCropGuideRangeForSearch";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Form3";
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(clslit1, 0);
			base.Controls.SetChildIndex(clslit2, 0);
			base.Controls.SetChildIndex(clslit3, 0);
			base.Controls.SetChildIndex(clslit4, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(panel3, 0);
			base.Controls.SetChildIndex(panel5, 0);
			base.Controls.SetChildIndex(button1, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit1).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit2).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit3).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit4).EndInit();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
