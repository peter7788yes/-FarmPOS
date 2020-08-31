using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmPestGuide : MasterThinForm
	{
		public string cls1 = "";

		public string cls2 = "";

		public string cls3 = "";

		public string cls4 = "";

		public string cropcode = "";

		public string pestcode = "";

		public string realcropcode = "";

		private frmMainShopSimple frs;

		private frmCropGuide fcg;

		private frmMainShopSimpleWithMoney frsm;

		public int nowlevel;

		private List<string> pestidcat1list = new List<string>();

		private List<string> pestidcat2list = new List<string>();

		private List<string> pestidcat3list = new List<string>();

		private List<string> pestidcat4list = new List<string>();

		private IContainer components;

		private Label label6;

		private Label label9;

		private Label label10;

		private Label label8;

		private Label label11;

		private DataGridView clslit1;

		private DataGridView clslit2;

		private DataGridView clslit3;

		private DataGridView clslit4;

		private Button button1;

		private Panel panel5;

		private Label label5;

		private Panel panel4;

		private Label label4;

		private Panel panel3;

		private Label label3;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private DataGridViewTextBoxColumn cls1name;

		private DataGridViewTextBoxColumn cls1code;

		private DataGridViewTextBoxColumn cropId;

		private DataGridViewTextBoxColumn cls2name;

		private DataGridViewTextBoxColumn cls2code;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn cls3name;

		private DataGridViewTextBoxColumn cls3code;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn cls4name;

		private DataGridViewTextBoxColumn cls4code;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		public frmPestGuide(frmCropGuide fcg, frmMainShopSimple fms)
			: base("收銀作業")
		{
			InitializeComponent();
			this.fcg = fcg;
			frs = fms;
			if (this.fcg.nowlevel == 4)
			{
				if (this.fcg.cls4 != "")
				{
					cropcode = this.fcg.cls1 + this.fcg.cls2 + this.fcg.cls3 + this.fcg.cls4;
					setPest(cropcode, clslit1, false, 1);
					cropcode = this.fcg.cls1 + this.fcg.cls2 + this.fcg.cls3;
					setPest(cropcode, clslit1, true, 1);
					cropcode = this.fcg.cls1 + this.fcg.cls2;
					setPest(cropcode, clslit1, true, 1);
					cropcode = this.fcg.cls1;
					setPest(cropcode, clslit1, true, 1);
				}
			}
			else if (this.fcg.nowlevel == 3)
			{
				if (this.fcg.cls3 != "")
				{
					string cropid = this.fcg.cls1 + this.fcg.cls2 + this.fcg.cls3;
					setPest(cropid, clslit1, false, 1);
					cropid = this.fcg.cls1 + this.fcg.cls2;
					setPest(cropid, clslit1, true, 1);
					cropid = this.fcg.cls1;
					setPest(cropid, clslit1, true, 1);
				}
			}
			else if (this.fcg.nowlevel == 2)
			{
				cropcode = this.fcg.cls1 + this.fcg.cls2;
				setPest(cropcode, clslit1, false, 1);
				cropcode = this.fcg.cls1;
				setPest(cropcode, clslit1, true, 1);
			}
			else
			{
				cropcode = this.fcg.cls1;
				setPest(cropcode, clslit1, false, 1);
			}
		}

		public frmPestGuide(frmCropGuide fcg, frmMainShopSimpleWithMoney fms)
			: base("收銀作業")
		{
			InitializeComponent();
			this.fcg = fcg;
			frsm = fms;
			if (this.fcg.nowlevel == 4)
			{
				if (this.fcg.cls4 != "")
				{
					cropcode = this.fcg.cls1 + this.fcg.cls2 + this.fcg.cls3 + this.fcg.cls4;
					setPest(cropcode, clslit1, false, 1);
					cropcode = this.fcg.cls1 + this.fcg.cls2 + this.fcg.cls3;
					setPest(cropcode, clslit1, true, 1);
					cropcode = this.fcg.cls1 + this.fcg.cls2;
					setPest(cropcode, clslit1, true, 1);
					cropcode = this.fcg.cls1;
					setPest(cropcode, clslit1, true, 1);
				}
			}
			else if (this.fcg.nowlevel == 3)
			{
				if (this.fcg.cls3 != "")
				{
					string cropid = this.fcg.cls1 + this.fcg.cls2 + this.fcg.cls3;
					setPest(cropid, clslit1, false, 1);
					cropid = this.fcg.cls1 + this.fcg.cls2;
					setPest(cropid, clslit1, true, 1);
					cropid = this.fcg.cls1;
					setPest(cropid, clslit1, true, 1);
				}
			}
			else if (this.fcg.nowlevel == 2)
			{
				cropcode = this.fcg.cls1 + this.fcg.cls2;
				setPest(cropcode, clslit1, false, 1);
				cropcode = this.fcg.cls1;
				setPest(cropcode, clslit1, true, 1);
			}
			else
			{
				cropcode = this.fcg.cls1;
				setPest(cropcode, clslit1, false, 1);
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
			pestidcat2list.Clear();
			pestidcat3list.Clear();
			pestidcat4list.Clear();
			bool flag = false;
			if (fcg.nowlevel == 4)
			{
				string text = fcg.cls1 + fcg.cls2 + fcg.cls3 + fcg.cls4;
				string[] strWhereParameterArray = new string[2]
				{
					text,
					cls1
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit1.CurrentRow.Cells[1].Value.ToString(), text);
					flag = true;
				}
				text = fcg.cls1 + fcg.cls2 + fcg.cls3 + fcg.cls4;
				setPest(text, clslit2, false, 2);
				text = fcg.cls1 + fcg.cls2 + fcg.cls3;
				string[] strWhereParameterArray2 = new string[2]
				{
					text,
					cls1
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit1.CurrentRow.Cells[1].Value.ToString());
					flag = true;
				}
				setPest(text, clslit2, true, 2);
				text = fcg.cls1 + fcg.cls2;
				string[] strWhereParameterArray3 = new string[2]
				{
					text,
					cls1
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit1.CurrentRow.Cells[1].Value.ToString());
					flag = true;
				}
				setPest(text, clslit2, true, 2);
				text = fcg.cls1;
				string[] strWhereParameterArray4 = new string[2]
				{
					text,
					cls1
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit1.CurrentRow.Cells[1].Value.ToString());
					flag = true;
				}
				setPest(text, clslit2, true, 2);
				clslit2.Columns[0].HeaderText = clslit1.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat2list.Count + ")";
			}
			else if (fcg.nowlevel == 3)
			{
				string text2 = fcg.cls1 + fcg.cls2 + fcg.cls3;
				string[] strWhereParameterArray5 = new string[2]
				{
					text2,
					cls1
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit1.CurrentRow.Cells[1].Value.ToString(), text2);
					flag = true;
				}
				setPest(text2, clslit2, false, 2);
				text2 = fcg.cls1 + fcg.cls2;
				string[] strWhereParameterArray6 = new string[2]
				{
					text2,
					cls1
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray6, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit1.CurrentRow.Cells[1].Value.ToString());
					flag = true;
				}
				setPest(text2, clslit2, true, 2);
				text2 = fcg.cls1;
				string[] strWhereParameterArray7 = new string[2]
				{
					text2,
					cls1
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray7, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit1.CurrentRow.Cells[1].Value.ToString());
					flag = true;
				}
				setPest(text2, clslit2, true, 2);
				clslit2.Columns[0].HeaderText = clslit1.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat2list.Count + ")";
			}
			else if (fcg.nowlevel == 2)
			{
				string text3 = fcg.cls1 + fcg.cls2;
				string[] strWhereParameterArray8 = new string[2]
				{
					text3,
					cls1
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray8, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit1.CurrentRow.Cells[1].Value.ToString(), text3);
					flag = true;
				}
				setPest(text3, clslit2, false, 2);
				text3 = fcg.cls1;
				string[] strWhereParameterArray9 = new string[2]
				{
					text3,
					cls1
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray9, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit1.CurrentRow.Cells[1].Value.ToString());
					flag = true;
				}
				setPest(text3, clslit2, true, 2);
				clslit2.Columns[0].HeaderText = clslit1.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat2list.Count + ")";
			}
			else
			{
				string text4 = fcg.cls1;
				string[] strWhereParameterArray10 = new string[2]
				{
					text4,
					cls1
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray10, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit1.CurrentRow.Cells[1].Value.ToString(), text4);
				}
				setPest(text4, clslit2, false, 2);
				clslit2.Columns[0].HeaderText = clslit1.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat2list.Count + ")";
			}
		}

		private void clslit2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			if (clslit2.CurrentRow.Index == 0 && clslit2.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
			{
				pestcode = cls1;
				realcropcode = clslit2.CurrentRow.Cells[2].Value.ToString();
				nowlevel = 1;
				frmMedGuide frmMedGuide = (Program.SystemMode != 1) ? new frmMedGuide(fcg, frsm, this) : new frmMedGuide(fcg, frs, this);
				frmMedGuide.Location = new Point(base.Location.X, base.Location.Y);
				frmMedGuide.Show();
				Hide();
			}
			else
			{
				pestidcat3list.Clear();
				pestidcat4list.Clear();
				cls2 = clslit2.CurrentRow.Cells[1].Value.ToString();
				clslit3.Rows.Clear();
				clslit3.Columns[0].HeaderText = "請選擇上層病蟲害";
				cls3 = "";
				clslit4.Rows.Clear();
				clslit4.Columns[0].HeaderText = "請選擇上層病蟲害";
				cls4 = "";
				bool flag = false;
				if (fcg.nowlevel == 4)
				{
					string text = fcg.cls1 + fcg.cls2 + fcg.cls3 + fcg.cls4;
					string[] strWhereParameterArray = new string[2]
					{
						text,
						cls1 + cls2
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit2.CurrentRow.Cells[1].Value.ToString(), text);
						flag = true;
					}
					text = fcg.cls1 + fcg.cls2 + fcg.cls3 + fcg.cls4;
					setPest(text, clslit3, false, 3);
					text = fcg.cls1 + fcg.cls2 + fcg.cls3;
					string[] strWhereParameterArray2 = new string[2]
					{
						text,
						cls1 + cls2
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit2.CurrentRow.Cells[1].Value.ToString(), text);
						flag = true;
					}
					text = fcg.cls1 + fcg.cls2 + fcg.cls3;
					setPest(text, clslit3, true, 3);
					text = fcg.cls1 + fcg.cls2;
					string[] strWhereParameterArray3 = new string[2]
					{
						text,
						cls1 + cls2
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit2.CurrentRow.Cells[1].Value.ToString(), text);
						flag = true;
					}
					text = fcg.cls1 + fcg.cls2;
					setPest(text, clslit3, true, 3);
					text = fcg.cls1;
					string[] strWhereParameterArray4 = new string[2]
					{
						text,
						cls1 + cls2
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit2.CurrentRow.Cells[1].Value.ToString(), text);
						flag = true;
					}
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct pestId", "HyScope", "cropId={0} AND isDelete in ('N','') ", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
					text = fcg.cls1;
					setPest(text, clslit3, true, 3);
					clslit3.Columns[0].HeaderText = clslit2.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat3list.Count + ")";
				}
				else if (fcg.nowlevel == 3)
				{
					string text2 = fcg.cls1 + fcg.cls2 + fcg.cls3;
					string[] strWhereParameterArray5 = new string[2]
					{
						text2,
						cls1 + cls2
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit2.CurrentRow.Cells[1].Value.ToString(), text2);
						flag = true;
					}
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct pestId", "HyScope", "cropId={0} AND isDelete in ('N','') ", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable);
					setPest(text2, clslit3, false, 3);
					text2 = fcg.cls1 + fcg.cls2;
					string[] strWhereParameterArray6 = new string[2]
					{
						text2,
						cls1 + cls2
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray6, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit2.CurrentRow.Cells[1].Value.ToString(), text2);
						flag = true;
					}
					setPest(text2, clslit3, false, 3);
					text2 = fcg.cls1;
					string[] strWhereParameterArray7 = new string[2]
					{
						text2,
						cls1 + cls2
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray7, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit2.CurrentRow.Cells[1].Value.ToString(), text2);
						flag = true;
					}
					setPest(text2, clslit3, true, 3);
					clslit3.Columns[0].HeaderText = clslit2.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat3list.Count + ")";
				}
				else if (fcg.nowlevel == 2)
				{
					string text3 = fcg.cls1 + fcg.cls2;
					string[] strWhereParameterArray8 = new string[2]
					{
						text3,
						cls1 + cls2
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray8, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit2.CurrentRow.Cells[1].Value.ToString(), text3);
						flag = true;
					}
					setPest(text3, clslit3, false, 3);
					text3 = fcg.cls1;
					string[] strWhereParameterArray9 = new string[2]
					{
						text3,
						cls1 + cls2
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray9, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit2.CurrentRow.Cells[1].Value.ToString(), text3);
						flag = true;
					}
					setPest(text3, clslit3, true, 3);
					clslit3.Columns[0].HeaderText = clslit2.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat3list.Count + ")";
				}
				else
				{
					string text4 = fcg.cls1;
					string[] strWhereParameterArray10 = new string[2]
					{
						text4,
						cls1 + cls2
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray10, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit2.CurrentRow.Cells[1].Value.ToString(), text4);
					}
					setPest(text4, clslit3, false, 3);
					clslit3.Columns[0].HeaderText = clslit2.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat3list.Count + ")";
				}
			}
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
				pestcode = cls1 + cls2;
				realcropcode = clslit3.CurrentRow.Cells[2].Value.ToString();
				nowlevel = 2;
				frmMedGuide frmMedGuide = (Program.SystemMode != 1) ? new frmMedGuide(fcg, frsm, this) : new frmMedGuide(fcg, frs, this);
				frmMedGuide.Location = new Point(base.Location.X, base.Location.Y);
				frmMedGuide.Show();
				Hide();
			}
			else
			{
				pestidcat4list.Clear();
				cls3 = clslit3.CurrentRow.Cells[1].Value.ToString();
				clslit4.Columns[0].HeaderText = "請選擇上層作物";
				clslit4.Rows.Clear();
				cls4 = "";
				bool flag = false;
				if (fcg.nowlevel == 4)
				{
					string text = fcg.cls1 + fcg.cls2 + fcg.cls3 + fcg.cls4;
					string[] strWhereParameterArray = new string[2]
					{
						text,
						cls1 + cls2 + cls3
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit3.CurrentRow.Cells[1].Value.ToString(), text);
						flag = true;
					}
					setPest(text, clslit4, false, 4);
					text = fcg.cls1 + fcg.cls2 + fcg.cls3;
					string[] strWhereParameterArray2 = new string[2]
					{
						text,
						cls1 + cls2 + cls3
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit3.CurrentRow.Cells[1].Value.ToString(), text);
						flag = true;
					}
					setPest(text, clslit4, true, 4);
					text = fcg.cls1 + fcg.cls2;
					string[] strWhereParameterArray3 = new string[2]
					{
						text,
						cls1 + cls2 + cls3
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit3.CurrentRow.Cells[1].Value.ToString(), text);
						flag = true;
					}
					setPest(text, clslit4, true, 4);
					text = fcg.cls1;
					string[] strWhereParameterArray4 = new string[2]
					{
						text,
						cls1 + cls2 + cls3
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit3.CurrentRow.Cells[1].Value.ToString(), text);
						flag = true;
					}
					setPest(text, clslit4, true, 4);
					clslit4.Columns[0].HeaderText = clslit3.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat4list.Count + ")";
				}
				else if (fcg.nowlevel == 3)
				{
					string text2 = fcg.cls1 + fcg.cls2 + fcg.cls3;
					string[] strWhereParameterArray5 = new string[2]
					{
						text2,
						cls1 + cls2 + cls3
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit3.CurrentRow.Cells[1].Value.ToString(), text2);
						flag = true;
					}
					setPest(text2, clslit4, false, 4);
					text2 = fcg.cls1 + fcg.cls2;
					string[] strWhereParameterArray6 = new string[2]
					{
						text2,
						cls1 + cls2 + cls3
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray6, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit3.CurrentRow.Cells[1].Value.ToString(), text2);
						flag = true;
					}
					setPest(text2, clslit4, true, 4);
					text2 = fcg.cls1;
					string[] strWhereParameterArray7 = new string[2]
					{
						text2,
						cls1 + cls2 + cls3
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray7, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit3.CurrentRow.Cells[1].Value.ToString(), text2);
						flag = true;
					}
					setPest(text2, clslit4, true, 4);
					clslit4.Columns[0].HeaderText = clslit3.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat4list.Count + ")";
				}
				else if (fcg.nowlevel == 2)
				{
					string text3 = fcg.cls1 + fcg.cls2;
					string[] strWhereParameterArray8 = new string[2]
					{
						text3,
						cls1 + cls2 + cls3
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray8, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit3.CurrentRow.Cells[1].Value.ToString(), text3);
						flag = true;
					}
					setPest(text3, clslit4, false, 4);
					text3 = fcg.cls1;
					string[] strWhereParameterArray9 = new string[2]
					{
						text3,
						cls1 + cls2 + cls3
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray9, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !flag)
					{
						clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」擴大分類", clslit3.CurrentRow.Cells[1].Value.ToString(), text3);
						flag = true;
					}
					setPest(text3, clslit4, true, 4);
					clslit4.Columns[0].HeaderText = clslit3.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat4list.Count + ")";
				}
				else
				{
					string text4 = fcg.cls1;
					string[] strWhereParameterArray10 = new string[2]
					{
						text4,
						cls1 + cls2 + cls3
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "Hyscope", "cropId ={0} and pestId = {1} AND isDelete in ('N','') ", "", null, strWhereParameterArray10, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit3.CurrentRow.Cells[1].Value.ToString(), text4);
					}
					setPest(text4, clslit4, false, 4);
					clslit4.Columns[0].HeaderText = clslit1.CurrentRow.Cells[0].Value.ToString() + "(" + pestidcat4list.Count + ")";
				}
			}
			cropcode = cls1 + cls2 + cls3;
		}

		private void clslit4_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				if (clslit4.CurrentRow.Index == 0 && clslit4.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
				{
					pestcode = cls1 + cls2 + cls3;
					realcropcode = clslit4.CurrentRow.Cells[2].Value.ToString();
					nowlevel = 3;
					frmMedGuide frmMedGuide = (Program.SystemMode != 1) ? new frmMedGuide(fcg, frsm, this) : new frmMedGuide(fcg, frs, this);
					frmMedGuide.Location = new Point(base.Location.X, base.Location.Y);
					frmMedGuide.Show();
					Hide();
				}
				else
				{
					cls4 = clslit4.CurrentRow.Cells[1].Value.ToString();
					pestcode = cls1 + cls2 + cls3 + cls4;
					realcropcode = clslit4.CurrentRow.Cells[2].Value.ToString();
					nowlevel = 4;
					frmMedGuide frmMedGuide2 = (Program.SystemMode != 1) ? new frmMedGuide(fcg, frsm, this) : new frmMedGuide(fcg, frs, this);
					frmMedGuide2.Location = new Point(base.Location.X, base.Location.Y);
					frmMedGuide2.Show();
					Hide();
				}
			}
		}

		private void setPest(string cropid, DataGridView templist, bool extend, int level)
		{
			new List<string>();
			(new string[1])[0] = cropid;
			DataTable dataTable = new DataTable();
			bool flag = false;
			string[] strWhereParameterArray = new string[5]
			{
				cropid,
				cls1,
				cls2,
				cls3,
				cls4
			};
			switch (level)
			{
			case 1:
				dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat1,name", "HyBlight as hb,HyScope as hs,HyLicence as hl ", "hs.cropId={0} and  hs.pestId like '%'||hb.cat1||'%' and hb.cat2 ='' and hb.cat3 ='' and hb.cat4 ='' and hs.formCode = hl.formCode and hs.contents = hl.contents and  hs.pesticideId = hl.pesticideId and hl.isDelete='N' AND hs.isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				break;
			case 2:
				dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat2,name", "HyBlight as hb,HyScope as hs,HyLicence as hl", "hs.cropId={0} and  hs.pestId like '%'||hb.cat1||hb.cat2||'%' and hb.cat1={1} and hb.cat2 !='' and hb.cat3 ='' and hb.cat4 ='' and hs.formCode = hl.formCode and hs.contents = hl.contents and  hs.pesticideId = hl.pesticideId and hl.isDelete='N' AND hs.isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				break;
			case 3:
				dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat3,name", "HyBlight as hb,HyScope as hs,HyLicence as hl", "hs.cropId={0} and  hs.pestId like '%'||hb.cat1||hb.cat2||hb.cat3||'%' and hb.cat1={1} and hb.cat2={2} and hb.cat3 !='' and hb.cat4 ='' and hs.formCode = hl.formCode and hs.contents = hl.contents and  hs.pesticideId = hl.pesticideId and hl.isDelete='N' AND hs.isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				break;
			case 4:
				dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat4,name", "HyBlight as hb,HyScope as hs,HyLicence as hl", "hs.cropId={0} and  hs.pestId like '%'||hb.cat1||hb.cat2||hb.cat3||hb.cat4||'%'  and hb.cat1={1} and hb.cat2={2} and hb.cat3={3} and hb.cat4 !='' and hs.formCode = hl.formCode and hs.contents = hl.contents and  hs.pesticideId = hl.pesticideId and hl.isDelete='N' AND hs.isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				break;
			}
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				switch (level)
				{
				case 1:
					foreach (string item in pestidcat1list)
					{
						if (item.Equals(row["cat1"].ToString()))
						{
							flag = true;
						}
					}
					if (!flag)
					{
						if (!extend)
						{
							clslit1.Rows.Add(row["name"].ToString(), row["cat1"].ToString(), cropid);
							pestidcat1list.Add(row["cat1"].ToString());
						}
						else
						{
							clslit1.Rows.Add(row["name"].ToString() + "(擴大)", row["cat1"].ToString(), cropid);
							pestidcat1list.Add(row["cat1"].ToString());
						}
					}
					flag = false;
					break;
				case 2:
					foreach (string item2 in pestidcat2list)
					{
						if (item2.Equals(row["cat2"].ToString()))
						{
							flag = true;
						}
					}
					if (!flag)
					{
						if (!extend)
						{
							clslit2.Rows.Add(row["name"].ToString(), row["cat2"].ToString(), cropid);
							pestidcat2list.Add(row["cat2"].ToString());
						}
						else
						{
							clslit2.Rows.Add(row["name"].ToString() + "(擴大)", row["cat2"].ToString(), cropid);
							pestidcat2list.Add(row["cat2"].ToString());
						}
					}
					flag = false;
					break;
				case 3:
					foreach (string item3 in pestidcat3list)
					{
						if (item3.Equals(row["cat3"].ToString()))
						{
							flag = true;
						}
					}
					if (!flag)
					{
						if (!extend)
						{
							clslit3.Rows.Add(row["name"].ToString(), row["cat3"].ToString(), cropid);
							pestidcat3list.Add(row["cat3"].ToString());
						}
						else
						{
							clslit3.Rows.Add(row["name"].ToString() + "(擴大)", row["cat3"].ToString(), cropid);
							pestidcat3list.Add(row["cat3"].ToString());
						}
					}
					flag = false;
					break;
				case 4:
					foreach (string item4 in pestidcat4list)
					{
						if (item4.Equals(row["cat4"].ToString()))
						{
							flag = true;
						}
					}
					if (!flag)
					{
						if (!extend)
						{
							clslit4.Rows.Add(row["name"].ToString(), row["cat4"].ToString(), cropid);
							pestidcat4list.Add(row["cat4"].ToString());
						}
						else
						{
							clslit4.Rows.Add(row["name"].ToString() + "(擴大)", row["cat4"].ToString(), cropid);
							pestidcat4list.Add(row["cat4"].ToString());
						}
					}
					flag = false;
					break;
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			fcg.Show();
			Hide();
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
			label6 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			clslit1 = new System.Windows.Forms.DataGridView();
			clslit2 = new System.Windows.Forms.DataGridView();
			clslit3 = new System.Windows.Forms.DataGridView();
			clslit4 = new System.Windows.Forms.DataGridView();
			button1 = new System.Windows.Forms.Button();
			panel5 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			cls1name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls1code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cropId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls2name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls2code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls3name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls3code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls4name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls4code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit2).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit3).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit4).BeginInit();
			panel5.SuspendLayout();
			panel4.SuspendLayout();
			panel3.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 150);
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.Location = new System.Drawing.Point(13, 132);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(105, 24);
			label6.TabIndex = 54;
			label6.Text = "病蟲害分類";
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
			clslit1.Columns.AddRange(cls1name, cls1code, cropId);
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
			clslit2.Columns.AddRange(cls2name, cls2code, dataGridViewTextBoxColumn1);
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
			clslit2.Location = new System.Drawing.Point(260, 171);
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
			clslit3.Columns.AddRange(cls3name, cls3code, dataGridViewTextBoxColumn2);
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
			clslit4.Columns.AddRange(cls4name, cls4code, dataGridViewTextBoxColumn3);
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
			clslit4.Location = new System.Drawing.Point(738, 171);
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
			button1.Location = new System.Drawing.Point(871, 128);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(97, 33);
			button1.TabIndex = 71;
			button1.Text = "回上一步";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			panel5.BackColor = System.Drawing.Color.Transparent;
			panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel5.Controls.Add(label5);
			panel5.Location = new System.Drawing.Point(737, 42);
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
			panel4.BackColor = System.Drawing.Color.Transparent;
			panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel4.Controls.Add(label4);
			panel4.Location = new System.Drawing.Point(506, 42);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(224, 73);
			panel4.TabIndex = 74;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.Black;
			label4.Location = new System.Drawing.Point(51, 12);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(132, 48);
			label4.TabIndex = 2;
			label4.Text = "Step3\r\n用藥/商品選擇";
			panel3.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel3.Controls.Add(label3);
			panel3.ForeColor = System.Drawing.Color.White;
			panel3.Location = new System.Drawing.Point(261, 42);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(238, 73);
			panel3.TabIndex = 75;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.White;
			label3.Location = new System.Drawing.Point(67, 12);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(105, 48);
			label3.TabIndex = 2;
			label3.Text = "Step2\r\n病蟲害選擇";
			panel1.BackColor = System.Drawing.Color.White;
			panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.ForeColor = System.Drawing.Color.Black;
			panel1.Location = new System.Drawing.Point(12, 42);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(242, 73);
			panel1.TabIndex = 72;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.Black;
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
			cls1name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			cls1name.DefaultCellStyle = dataGridViewCellStyle13;
			cls1name.HeaderText = "請選擇病蟲害類別";
			cls1name.Name = "cls1name";
			cls1name.ReadOnly = true;
			cls1name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls1code.HeaderText = "代碼";
			cls1code.Name = "cls1code";
			cls1code.Visible = false;
			cropId.HeaderText = "作物id";
			cropId.Name = "cropId";
			cropId.Visible = false;
			cls2name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			cls2name.DefaultCellStyle = dataGridViewCellStyle14;
			cls2name.HeaderText = "請選擇上層病蟲害";
			cls2name.Name = "cls2name";
			cls2name.ReadOnly = true;
			cls2name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls2code.HeaderText = "代碼";
			cls2code.Name = "cls2code";
			cls2code.Visible = false;
			dataGridViewTextBoxColumn1.HeaderText = "作物id";
			dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			dataGridViewTextBoxColumn1.Visible = false;
			cls3name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			cls3name.DefaultCellStyle = dataGridViewCellStyle15;
			cls3name.HeaderText = "請選擇上層病蟲害";
			cls3name.Name = "cls3name";
			cls3name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls3code.HeaderText = "代碼";
			cls3code.Name = "cls3code";
			cls3code.Visible = false;
			dataGridViewTextBoxColumn2.HeaderText = "作物id";
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.Visible = false;
			cls4name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			cls4name.DefaultCellStyle = dataGridViewCellStyle16;
			cls4name.HeaderText = "請選擇上層病蟲害";
			cls4name.Name = "cls4name";
			cls4name.ReadOnly = true;
			cls4name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls4code.HeaderText = "代碼";
			cls4code.Name = "cls4code";
			cls4code.Visible = false;
			dataGridViewTextBoxColumn3.HeaderText = "作物id";
			dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			dataGridViewTextBoxColumn3.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(981, 662);
			base.Controls.Add(panel5);
			base.Controls.Add(panel4);
			base.Controls.Add(panel3);
			base.Controls.Add(panel1);
			base.Controls.Add(button1);
			base.Controls.Add(clslit4);
			base.Controls.Add(clslit3);
			base.Controls.Add(clslit2);
			base.Controls.Add(clslit1);
			base.Controls.Add(label11);
			base.Controls.Add(label8);
			base.Controls.Add(label10);
			base.Controls.Add(label9);
			base.Controls.Add(label6);
			base.Name = "frmPestGuide";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Form3";
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(clslit1, 0);
			base.Controls.SetChildIndex(clslit2, 0);
			base.Controls.SetChildIndex(clslit3, 0);
			base.Controls.SetChildIndex(clslit4, 0);
			base.Controls.SetChildIndex(button1, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(panel3, 0);
			base.Controls.SetChildIndex(panel4, 0);
			base.Controls.SetChildIndex(panel5, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit1).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit2).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit3).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit4).EndInit();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
