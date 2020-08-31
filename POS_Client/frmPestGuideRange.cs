using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmPestGuideRange : MasterThinForm
	{
		public string cls1 = "";

		public string cls2 = "";

		public string cls3 = "";

		public string cls4 = "";

		public string cropcode = "";

		private frmMainShopSimple frs;

		private frmMainShopSimpleWithMoney frsm;

		private frmCropGuideRange fcgr;

		private string barcode;

		private int count;

		public int nowlevel;

		private List<string> pestId = new List<string>();

		private List<string> pestcls1 = new List<string>();

		private List<string> pestcls2 = new List<string>();

		private List<string> pestcls3 = new List<string>();

		private List<string> pestcls4 = new List<string>();

		private int level2length;

		private int level3length;

		private int level4length;

		private IContainer components;

		private Label label6;

		private Label label7;

		private Label cropname;

		private Label label9;

		private Label label10;

		private DataGridView clslit1;

		private DataGridView clslit2;

		private DataGridView clslit3;

		private DataGridView clslit4;

		private Button button1;

		private Panel panel5;

		private Label label5;

		private Panel panel3;

		private Label label3;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private DataGridViewTextBoxColumn cls1name;

		private DataGridViewTextBoxColumn cls1code;

		private DataGridViewTextBoxColumn cls2name;

		private DataGridViewTextBoxColumn cls2code;

		private DataGridViewTextBoxColumn cls3name;

		private DataGridViewTextBoxColumn cls3code;

		private DataGridViewTextBoxColumn cls4name;

		private DataGridViewTextBoxColumn cls4code;

		public frmPestGuideRange(frmCropGuideRange fcgr, frmMainShopSimple fms, int count, string barcode)
			: base("收銀作業")
		{
			InitializeComponent();
			if (Program.IsHyweb)
			{
				level2length = 3;
				level3length = 5;
				level4length = 7;
			}
			else
			{
				level2length = 3;
				level3length = 5;
				level4length = 7;
			}
			this.fcgr = fcgr;
			frs = fms;
			this.barcode = barcode;
			this.count = count;
			new List<string>();
			cropcode = this.fcgr.cropcode;
			string[] strWhereParameterArray = new string[1]
			{
				barcode
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				string[] strWhereParameterArray2 = new string[4]
				{
					row["pesticideId"].ToString(),
					cropcode,
					row["formCode"].ToString(),
					row["contents"].ToString()
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct pestId", "HyScope", "pesticideId={0} and cropId={1} and formCode={2} and contents={3} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count <= 0)
				{
					continue;
				}
				foreach (DataRow row2 in dataTable2.Rows)
				{
					pestId.Add(row2["pestId"].ToString());
					bool flag = false;
					string[] strWhereParameterArray3 = new string[1]
					{
						row2["pestId"].ToString()
					};
					DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat1,name", "HyBlight", "{0} like '%'||cat1||'%' and cat2 ='' and cat3 ='' and cat4 =''", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable3.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row3 in dataTable3.Rows)
					{
						foreach (string item in pestcls1)
						{
							if (item.Equals(row3["cat1"].ToString()))
							{
								flag = true;
							}
						}
						if (!flag)
						{
							clslit1.Rows.Add(row3["name"].ToString(), row3["cat1"].ToString());
							pestcls1.Add(row3["cat1"].ToString());
						}
						flag = false;
					}
				}
			}
		}

		public frmPestGuideRange(frmCropGuideRange fcgr, frmMainShopSimpleWithMoney fms, int count, string barcode)
			: base("收銀作業")
		{
			InitializeComponent();
			if (Program.IsHyweb)
			{
				level2length = 3;
				level3length = 5;
				level4length = 7;
			}
			else
			{
				level2length = 3;
				level3length = 5;
				level4length = 7;
			}
			this.fcgr = fcgr;
			frsm = fms;
			this.barcode = barcode;
			this.count = count;
			new List<string>();
			cropcode = this.fcgr.cropcode;
			string[] strWhereParameterArray = new string[1]
			{
				barcode
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				string[] strWhereParameterArray2 = new string[4]
				{
					row["pesticideId"].ToString(),
					cropcode,
					row["formCode"].ToString(),
					row["contents"].ToString()
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct pestId", "HyScope", "pesticideId={0} and cropId={1} and formCode={2} and contents={3} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count <= 0)
				{
					continue;
				}
				foreach (DataRow row2 in dataTable2.Rows)
				{
					pestId.Add(row2["pestId"].ToString());
					bool flag = false;
					string[] strWhereParameterArray3 = new string[1]
					{
						row2["pestId"].ToString()
					};
					DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat1,name", "HyBlight", "{0} like '%'||cat1||'%' and cat2 ='' and cat3 ='' and cat4 =''", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable3.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row3 in dataTable3.Rows)
					{
						foreach (string item in pestcls1)
						{
							if (item.Equals(row3["cat1"].ToString()))
							{
								flag = true;
							}
						}
						if (!flag)
						{
							clslit1.Rows.Add(row3["name"].ToString(), row3["cat1"].ToString());
							pestcls1.Add(row3["cat1"].ToString());
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
			pestcls2.Clear();
			pestcls3.Clear();
			pestcls4.Clear();
			new List<string>();
			foreach (string item in pestId)
			{
				if (item.Equals(cls1))
				{
					clslit2.Rows.Add("選入「" + clslit1.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit1.CurrentRow.Cells[1].Value.ToString());
				}
			}
			string[] strWhereParameterArray = new string[1]
			{
				barcode
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				string[] strWhereParameterArray2 = new string[4]
				{
					row["pesticideId"].ToString(),
					cropcode,
					row["formCode"].ToString(),
					row["contents"].ToString()
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct pestId", "HyScope", "pesticideId={0} and cropId={1} and formCode={2} and contents={3} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count <= 0)
				{
					continue;
				}
				foreach (DataRow row2 in dataTable2.Rows)
				{
					if (row2["pestId"].ToString().Length < level2length)
					{
						continue;
					}
					bool flag = false;
					string[] strWhereParameterArray3 = new string[2]
					{
						row2["pestId"].ToString(),
						cls1
					};
					DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat2,name", "HyBlight", "{0} like '%'||cat1||cat2||'%' and cat1={1} and cat2 !='' and cat3 ='' and cat4 =''", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable3.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row3 in dataTable3.Rows)
					{
						foreach (string item2 in pestcls2)
						{
							if (item2.Equals(row3["cat2"].ToString()))
							{
								flag = true;
							}
						}
						if (!flag)
						{
							clslit2.Rows.Add(row3["name"].ToString(), row3["cat2"].ToString());
							pestcls2.Add(row3["cat2"].ToString());
						}
						flag = false;
					}
				}
			}
		}

		private void clslit2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
			{
				return;
			}
			if (clslit2.CurrentRow.Index == 0 && clslit2.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
			{
				string text = DateTime.Now.ToString("yyyyMMdd");
				string[] strWhereParameterArray = new string[1]
				{
					barcode
				};
				bool flag = false;
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count <= 0)
				{
					return;
				}
				string[] strWhereParameterArray2 = new string[6]
				{
					dataTable.Rows[0]["pesticideId"].ToString(),
					dataTable.Rows[0]["formCode"].ToString(),
					dataTable.Rows[0]["contents"].ToString(),
					cropcode,
					cls1 + cls2,
					text
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hl.licType,hl.licNo,hl.domManufName", "HyScope as hs,HyLicence as hl", "hs.pesticideId = {0} and hl.pesticideId = hs.pesticideId  and hs.formCode ={1} and hl.formCode = hs.formCode and hl.contents = hs.contents  and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName !='' and  hs.regStoreName !='99999999' and hl.domManufId = hs.regStoreName AND hs.isDelete in ('N','')  ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count > 0)
				{
					string str = "Non";
					DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId", "hypos_GOODSLST", "GDSNO={0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
					foreach (DataRow row in dataTable2.Rows)
					{
						if (row["licType"].ToString().Equals(dataTable3.Rows[0]["licType"].ToString()) && row["licNo"].ToString().Equals(dataTable3.Rows[0]["domManufId"].ToString()))
						{
							flag = true;
						}
						else
						{
							str = row["domManufName"].ToString();
						}
					}
					if (flag)
					{
						if (Program.SystemMode == 1)
						{
							frs.addCropAndPest(count, cropcode, cls1);
							Hide();
							frs.setfocus();
							frs.Show();
						}
						else
						{
							frsm.addCropAndPest(count, cropcode, cls1);
							Hide();
							frsm.setfocus();
							frsm.Show();
						}
					}
					else
					{
						AutoClosingMessageBox.Show("此用藥配對尚於資料保護期間，僅可選擇[" + str + "]用藥");
					}
				}
				else if (Program.SystemMode == 1)
				{
					frs.addCropAndPest(count, cropcode, cls1);
					Hide();
					frs.setfocus();
					frs.Show();
				}
				else
				{
					frsm.addCropAndPest(count, cropcode, cls1);
					Hide();
					frsm.setfocus();
					frsm.Show();
				}
				return;
			}
			cls2 = clslit2.CurrentRow.Cells[1].Value.ToString();
			clslit3.Rows.Clear();
			clslit3.Columns[0].HeaderText = "請選擇上層病蟲害";
			cls3 = "";
			clslit4.Rows.Clear();
			clslit4.Columns[0].HeaderText = "請選擇上層病蟲害";
			cls4 = "";
			pestcls3.Clear();
			pestcls4.Clear();
			new List<string>();
			foreach (string item in pestId)
			{
				if (item.Equals(cls1 + cls2))
				{
					clslit3.Rows.Add("選入「" + clslit2.CurrentRow.Cells[0].Value.ToString() + "」分類", cls1 + cls2);
				}
			}
			string[] strWhereParameterArray3 = new string[1]
			{
				barcode
			};
			DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable4.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row2 in dataTable4.Rows)
			{
				string[] strWhereParameterArray4 = new string[4]
				{
					row2["pesticideId"].ToString(),
					cropcode,
					row2["formCode"].ToString(),
					row2["contents"].ToString()
				};
				DataTable dataTable5 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct pestId", "HyScope", "pesticideId={0} and cropId={1} and formCode={2} and contents={3} AND isDelete in ('N','') ", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable5.Rows.Count <= 0)
				{
					continue;
				}
				foreach (DataRow row3 in dataTable5.Rows)
				{
					if (row3["pestId"].ToString().Length < level3length)
					{
						continue;
					}
					bool flag2 = false;
					string[] strWhereParameterArray5 = new string[3]
					{
						row3["pestId"].ToString(),
						cls1,
						cls2
					};
					DataTable dataTable6 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat3,name", "HyBlight", "{0} like '%'||cat1||cat2||cat3||'%' and cat1={1} and cat2 ={2} and cat3 !='' and cat4 =''", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable6.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row4 in dataTable6.Rows)
					{
						foreach (string item2 in pestcls3)
						{
							if (item2.Equals(row4["cat3"].ToString()))
							{
								flag2 = true;
							}
						}
						if (!flag2)
						{
							clslit3.Rows.Add(row4["name"].ToString(), row4["cat3"].ToString());
							pestcls3.Add(row4["cat3"].ToString());
						}
						flag2 = false;
					}
				}
			}
		}

		private void clslit3_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
			{
				return;
			}
			if (clslit3.CurrentRow.Index == 0 && clslit3.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
			{
				string text = DateTime.Now.ToString("yyyyMMdd");
				string[] strWhereParameterArray = new string[1]
				{
					barcode
				};
				bool flag = false;
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count <= 0)
				{
					return;
				}
				string[] strWhereParameterArray2 = new string[6]
				{
					dataTable.Rows[0]["pesticideId"].ToString(),
					dataTable.Rows[0]["formCode"].ToString(),
					dataTable.Rows[0]["contents"].ToString(),
					cropcode,
					cls1 + cls2,
					text
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hl.licType,hl.licNo,hl.domManufName", "HyScope as hs,HyLicence as hl", "hs.pesticideId = {0} and hl.pesticideId = hs.pesticideId  and hs.formCode ={1} and hl.formCode = hs.formCode and hl.contents = hs.contents  and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName !='' and  hs.regStoreName !='99999999' and hl.domManufId = hs.regStoreName AND hs.isDelete in ('N','')  ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count > 0)
				{
					string str = "Non";
					DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId", "hypos_GOODSLST", "GDSNO={0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
					foreach (DataRow row in dataTable2.Rows)
					{
						if (row["licType"].ToString().Equals(dataTable3.Rows[0]["licType"].ToString()) && row["licNo"].ToString().Equals(dataTable3.Rows[0]["domManufId"].ToString()))
						{
							flag = true;
						}
						else
						{
							str = row["domManufName"].ToString();
						}
					}
					if (flag)
					{
						if (Program.SystemMode == 1)
						{
							frs.addCropAndPest(count, cropcode, cls1 + cls2);
							Hide();
							frs.setfocus();
							frs.Show();
						}
						else
						{
							frsm.addCropAndPest(count, cropcode, cls1 + cls2);
							Hide();
							frsm.setfocus();
							frsm.Show();
						}
					}
					else
					{
						AutoClosingMessageBox.Show("此用藥配對尚於資料保護期間，僅可選擇[" + str + "]用藥");
					}
				}
				else if (Program.SystemMode == 1)
				{
					frs.addCropAndPest(count, cropcode, cls1 + cls2);
					Hide();
					frs.setfocus();
					frs.Show();
				}
				else
				{
					frsm.addCropAndPest(count, cropcode, cls1 + cls2);
					Hide();
					frsm.setfocus();
					frsm.Show();
				}
				return;
			}
			cls3 = clslit3.CurrentRow.Cells[1].Value.ToString();
			clslit4.Columns[0].HeaderText = "請選擇上層作物";
			clslit4.Rows.Clear();
			cls4 = "";
			pestcls4.Clear();
			new List<string>();
			foreach (string item in pestId)
			{
				if (item.Equals(cls1 + cls2 + cls3))
				{
					clslit4.Rows.Add("選入「" + clslit3.CurrentRow.Cells[0].Value.ToString() + "」分類", clslit3.CurrentRow.Cells[1].Value.ToString());
				}
			}
			string[] strWhereParameterArray3 = new string[1]
			{
				barcode
			};
			DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable4.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row2 in dataTable4.Rows)
			{
				string[] strWhereParameterArray4 = new string[4]
				{
					row2["pesticideId"].ToString(),
					cropcode,
					row2["formCode"].ToString(),
					row2["contents"].ToString()
				};
				DataTable dataTable5 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct pestId", "HyScope", "pesticideId={0} and cropId={1} and formCode={2} and contents={3} AND isDelete in ('N','') ", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable5.Rows.Count <= 0)
				{
					continue;
				}
				foreach (DataRow row3 in dataTable5.Rows)
				{
					if (row3["pestId"].ToString().Length < level4length)
					{
						continue;
					}
					bool flag2 = false;
					string[] strWhereParameterArray5 = new string[4]
					{
						row3["pestId"].ToString(),
						cls1,
						cls2,
						cls3
					};
					DataTable dataTable6 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat4,name", "HyBlight", "{0} like '%'||cat1||cat2||cat3||cat4||'%' and cat1={1} and cat2 ={2} and cat3={3} and cat4 !=''", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable6.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row4 in dataTable6.Rows)
					{
						foreach (string item2 in pestcls4)
						{
							if (item2.Equals(row4["cat4"].ToString()))
							{
								flag2 = true;
							}
						}
						if (!flag2)
						{
							clslit4.Rows.Add(row4["name"].ToString(), row4["cat4"].ToString());
							pestcls4.Add(row4["cat4"].ToString());
						}
						flag2 = false;
					}
				}
			}
		}

		private void clslit4_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
			{
				return;
			}
			if (clslit4.CurrentRow.Index == 0 && clslit4.CurrentRow.Cells[0].Value.ToString().Contains("選入"))
			{
				string text = DateTime.Now.ToString("yyyyMMdd");
				string[] strWhereParameterArray = new string[1]
				{
					barcode
				};
				bool flag = false;
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count <= 0)
				{
					return;
				}
				string[] strWhereParameterArray2 = new string[6]
				{
					dataTable.Rows[0]["pesticideId"].ToString(),
					dataTable.Rows[0]["formCode"].ToString(),
					dataTable.Rows[0]["contents"].ToString(),
					cropcode,
					cls1 + cls2 + cls3,
					text
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hl.licType,hl.licNo,hl.domManufName", "HyScope as hs,HyLicence as hl", "hs.pesticideId = {0} and hl.pesticideId = hs.pesticideId  and hs.formCode ={1} and hl.formCode = hs.formCode and hl.contents = hs.contents  and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName !='' and  hs.regStoreName !='99999999' and hl.domManufId = hs.regStoreName AND hs.isDelete in ('N','')  ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count > 0)
				{
					string str = "Non";
					DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId", "hypos_GOODSLST", "GDSNO={0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
					foreach (DataRow row in dataTable2.Rows)
					{
						if (row["licType"].ToString().Equals(dataTable3.Rows[0]["licType"].ToString()) && row["licNo"].ToString().Equals(dataTable3.Rows[0]["domManufId"].ToString()))
						{
							flag = true;
						}
						else
						{
							str = row["domManufName"].ToString();
						}
					}
					if (flag)
					{
						if (Program.SystemMode == 1)
						{
							frs.addCropAndPest(count, cropcode, cls1 + cls2 + cls3);
							Hide();
							frs.setfocus();
							frs.Show();
						}
						else
						{
							frsm.addCropAndPest(count, cropcode, cls1 + cls2 + cls3);
							Hide();
							frsm.setfocus();
							frsm.Show();
						}
					}
					else
					{
						AutoClosingMessageBox.Show("此用藥配對尚於資料保護期間，僅可選擇[" + str + "]用藥");
					}
				}
				else if (Program.SystemMode == 1)
				{
					frs.addCropAndPest(count, cropcode, cls1 + cls2 + cls3);
					Hide();
					frs.setfocus();
					frs.Show();
				}
				else
				{
					frsm.addCropAndPest(count, cropcode, cls1 + cls2 + cls3);
					Hide();
					frsm.setfocus();
					frsm.Show();
				}
				return;
			}
			cls4 = clslit4.CurrentRow.Cells[1].Value.ToString();
			string text2 = DateTime.Now.ToString("yyyyMMdd");
			string[] strWhereParameterArray3 = new string[1]
			{
				barcode
			};
			bool flag2 = false;
			DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable4.Rows.Count <= 0)
			{
				return;
			}
			string[] strWhereParameterArray4 = new string[6]
			{
				dataTable4.Rows[0]["pesticideId"].ToString(),
				dataTable4.Rows[0]["formCode"].ToString(),
				dataTable4.Rows[0]["contents"].ToString(),
				cropcode,
				cls1 + cls2 + cls3 + cls4,
				text2
			};
			DataTable dataTable5 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hl.licType,hl.licNo,hl.domManufName", "HyScope as hs,HyLicence as hl", "hs.pesticideId = {0} and hl.pesticideId = hs.pesticideId  and hs.formCode ={1} and hl.formCode = hs.formCode and hl.contents = hs.contents  and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName !='' and  hs.regStoreName !='99999999' and hl.domManufId = hs.regStoreName AND hs.isDelete in ('N','')  ", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable5.Rows.Count > 0)
			{
				string str2 = "Non";
				DataTable dataTable6 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId", "hypos_GOODSLST", "GDSNO={0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
				foreach (DataRow row2 in dataTable5.Rows)
				{
					if (row2["licType"].ToString().Equals(dataTable6.Rows[0]["licType"].ToString()) && row2["licNo"].ToString().Equals(dataTable6.Rows[0]["domManufId"].ToString()))
					{
						flag2 = true;
					}
					else
					{
						str2 = row2["domManufName"].ToString();
					}
				}
				if (flag2)
				{
					if (Program.SystemMode == 1)
					{
						frs.addCropAndPest(count, cropcode, cls1 + cls2 + cls3 + cls4);
						Hide();
						frs.setfocus();
						frs.Show();
					}
					else
					{
						frsm.addCropAndPest(count, cropcode, cls1 + cls2 + cls3 + cls4);
						Hide();
						frsm.setfocus();
						frsm.Show();
					}
				}
				else
				{
					AutoClosingMessageBox.Show("此用藥配對尚於資料保護期間，僅可選擇[" + str2 + "]用藥");
				}
			}
			else if (Program.SystemMode == 1)
			{
				frs.addCropAndPest(count, cropcode, cls1 + cls2 + cls3 + cls4);
				Hide();
				frs.setfocus();
				frs.Show();
			}
			else
			{
				frsm.addCropAndPest(count, cropcode, cls1 + cls2 + cls3 + cls4);
				Hide();
				frsm.setfocus();
				frsm.Show();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			fcgr.Show();
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
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			cropname = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
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
			button1 = new System.Windows.Forms.Button();
			panel5 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
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
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.Location = new System.Drawing.Point(13, 132);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(105, 24);
			label6.TabIndex = 54;
			label6.Text = "病蟲害分類";
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
			clslit1.Location = new System.Drawing.Point(12, 169);
			clslit1.MultiSelect = false;
			clslit1.Name = "clslit1";
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
			clslit1.Size = new System.Drawing.Size(242, 463);
			clslit1.TabIndex = 67;
			clslit1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(infolist_CellContentClick);
			cls1name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			cls1name.DefaultCellStyle = dataGridViewCellStyle4;
			cls1name.HeaderText = "請選擇病蟲害類別";
			cls1name.Name = "cls1name";
			cls1name.ReadOnly = true;
			cls1name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls1code.HeaderText = "代碼";
			cls1code.Name = "cls1code";
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
			clslit2.Location = new System.Drawing.Point(260, 169);
			clslit2.MultiSelect = false;
			clslit2.Name = "clslit2";
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
			clslit2.Size = new System.Drawing.Size(238, 463);
			clslit2.TabIndex = 68;
			clslit2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit2_CellContentClick);
			cls2name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			cls2name.DefaultCellStyle = dataGridViewCellStyle8;
			cls2name.HeaderText = "請選擇上層病蟲害";
			cls2name.Name = "cls2name";
			cls2name.ReadOnly = true;
			cls2name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls2code.HeaderText = "代碼";
			cls2code.Name = "cls2code";
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
			dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(6);
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
			clslit3.Location = new System.Drawing.Point(508, 169);
			clslit3.MultiSelect = false;
			clslit3.Name = "clslit3";
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
			clslit3.Size = new System.Drawing.Size(224, 463);
			clslit3.TabIndex = 69;
			clslit3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit3_CellContentClick);
			cls3name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
			cls3name.DefaultCellStyle = dataGridViewCellStyle12;
			cls3name.HeaderText = "請選擇上層病蟲害";
			cls3name.Name = "cls3name";
			cls3name.ReadOnly = true;
			cls3name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls3code.HeaderText = "代碼";
			cls3code.Name = "cls3code";
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
			clslit4.Location = new System.Drawing.Point(738, 169);
			clslit4.MultiSelect = false;
			clslit4.Name = "clslit4";
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
			clslit4.Size = new System.Drawing.Size(231, 463);
			clslit4.TabIndex = 70;
			clslit4.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit4_CellContentClick);
			cls4name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
			cls4name.DefaultCellStyle = dataGridViewCellStyle16;
			cls4name.HeaderText = "請選擇上層病蟲害";
			cls4name.Name = "cls4name";
			cls4name.ReadOnly = true;
			cls4name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cls4code.HeaderText = "代碼";
			cls4code.Name = "cls4code";
			cls4code.Visible = false;
			button1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.Location = new System.Drawing.Point(872, 123);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(97, 33);
			button1.TabIndex = 71;
			button1.Text = "回上一步";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
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
			panel3.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel3.Controls.Add(label3);
			panel3.Location = new System.Drawing.Point(261, 38);
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
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.ForeColor = System.Drawing.Color.White;
			panel1.Location = new System.Drawing.Point(12, 38);
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
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(981, 671);
			base.Controls.Add(panel5);
			base.Controls.Add(panel3);
			base.Controls.Add(panel1);
			base.Controls.Add(button1);
			base.Controls.Add(clslit4);
			base.Controls.Add(clslit3);
			base.Controls.Add(clslit2);
			base.Controls.Add(clslit1);
			base.Controls.Add(label10);
			base.Controls.Add(label9);
			base.Controls.Add(cropname);
			base.Controls.Add(label7);
			base.Controls.Add(label6);
			base.Name = "frmPestGuideRange";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Form3";
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(cropname, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(clslit1, 0);
			base.Controls.SetChildIndex(clslit2, 0);
			base.Controls.SetChildIndex(clslit3, 0);
			base.Controls.SetChildIndex(clslit4, 0);
			base.Controls.SetChildIndex(button1, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(panel3, 0);
			base.Controls.SetChildIndex(panel5, 0);
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
