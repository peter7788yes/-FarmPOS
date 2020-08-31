using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmCropPestRange_NEW : MasterThinForm
	{
		public class CustomColumnMed : DataGridViewColumn
		{
			public override DataGridViewCell CellTemplate
			{
				get
				{
					return base.CellTemplate;
				}
				set
				{
					if (value != null && !value.GetType().IsAssignableFrom(typeof(CustomeCellMed)))
					{
						throw new InvalidCastException("It should be a custom Cell");
					}
					base.CellTemplate = value;
				}
			}

			public CustomColumnMed()
				: base(new CustomeCellMed())
			{
			}
		}

		public class CustomeCellMed : DataGridViewCell
		{
			public override Type ValueType
			{
				get
				{
					return typeof(frmMedUserControl);
				}
			}

			protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
			{
				frmMedUserControl frmMedUserControl = (frmMedUserControl)value;
				Bitmap bitmap = new Bitmap(cellBounds.Width, cellBounds.Height);
				frmMedUserControl.DrawToBitmap(bitmap, new Rectangle(0, 0, frmMedUserControl.Width, frmMedUserControl.Height));
				graphics.DrawImage(bitmap, cellBounds.Location);
			}
		}

		private class MedInfo
		{
			[CompilerGenerated]
			private string _003C_pesticideId_003Ek__BackingField;

			[CompilerGenerated]
			private string _003C_formCode_003Ek__BackingField;

			[CompilerGenerated]
			private string _003C_contents_003Ek__BackingField;

			public string _pesticideId
			{
				[CompilerGenerated]
				get
				{
					return _003C_pesticideId_003Ek__BackingField;
				}
				[CompilerGenerated]
				set
				{
					_003C_pesticideId_003Ek__BackingField = value;
				}
			}

			public string _formCode
			{
				[CompilerGenerated]
				get
				{
					return _003C_formCode_003Ek__BackingField;
				}
				[CompilerGenerated]
				set
				{
					_003C_formCode_003Ek__BackingField = value;
				}
			}

			public string _contents
			{
				[CompilerGenerated]
				get
				{
					return _003C_contents_003Ek__BackingField;
				}
				[CompilerGenerated]
				set
				{
					_003C_contents_003Ek__BackingField = value;
				}
			}

			public MedInfo(string pesticideId, string formCode, string contents)
			{
				_pesticideId = pesticideId;
				_formCode = formCode;
				_contents = contents;
			}
		}

		private string _barcode = "";

		private int _WhereRUGo;

		private int _count;

		public string lastCropID = "";

		public string lastPestID = "";

		private frmMainShopSimple frs;

		private frmMainShopSimpleWithMoney frsm;

		private frmEditCommodity frec;

		private List<HyScopeInfo> MedInfoList = new List<HyScopeInfo>();

		private List<string> PestList = new List<string>();

		private IContainer components;

		private Label label6;

		private Button button1;

		private DataGridView dgv_pestList;

		private DataGridView dgv_cropList;

		private Label label8;

		private Label label11;

		private Panel panel5;

		private Label label5;

		private Panel panel3;

		private Label label3;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private Label label7;

		private Label label9;

		private Label label10;

		private Label label4;

		private Label label12;

		private DataGridViewTextBoxColumn PestList1;

		private DataGridViewTextBoxColumn PestList2;

		private DataGridViewTextBoxColumn CropList1;

		private DataGridViewTextBoxColumn CropList2;

		public frmCropPestRange_NEW(frmMainShopSimple fms, int count, string barcode, int WhereAreUGo)
			: base("收銀作業")
		{
			InitializeComponent();
			frs = fms;
			_barcode = barcode;
			_count = count;
			_WhereRUGo = WhereAreUGo;
			MedInfoList.Clear();
			PestList.Clear();
			Initialize(barcode);
		}

		public frmCropPestRange_NEW(frmMainShopSimpleWithMoney fmss, int count, string barcode, int WhereAreUGo)
			: base("收銀作業")
		{
			InitializeComponent();
			frsm = fmss;
			_barcode = barcode;
			_count = count;
			_WhereRUGo = WhereAreUGo;
			MedInfoList.Clear();
			PestList.Clear();
			Initialize(barcode);
		}

		public frmCropPestRange_NEW(frmEditCommodity fmec, int count, string barcode, int WhereAreUGo)
			: base("收銀作業")
		{
			InitializeComponent();
			frec = fmec;
			_barcode = barcode;
			_count = count;
			_WhereRUGo = WhereAreUGo;
			MedInfoList.Clear();
			PestList.Clear();
			Initialize(barcode);
		}

		public void Initialize(string barcode)
		{
			try
			{
				Step1Control_Setting();
				dgv_cropList.Rows.Clear();
				dgv_cropList.Refresh();
				_barcode = barcode;
				string[] strWhereParameterArray = new string[1]
				{
					barcode
				};
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDName,CName,licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count <= 0)
				{
					return;
				}
				label6.Text = "【" + dataTable.Rows[0]["GDName"].ToString() + "-" + dataTable.Rows[0]["CName"].ToString() + "】用藥範圍設定";
				label10.Text = "【用藥】:" + dataTable.Rows[0]["GDName"].ToString() + "-" + dataTable.Rows[0]["CName"].ToString();
				foreach (DataRow row in dataTable.Rows)
				{
					string[] strWhereParameterArray2 = new string[3]
					{
						row["pesticideId"].ToString(),
						row["formCode"].ToString(),
						row["contents"].ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cropId,pesticideId,formCode,contents", "HyScope", "pesticideId = {0} and formCode ={1} and contents={2} AND isDelete in ('N','') ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count <= 0)
					{
						continue;
					}
					for (int i = 0; i < dataTable2.Rows.Count; i++)
					{
						string[] strWhereParameterArray3 = new string[1]
						{
							dataTable2.Rows[i]["cropId"].ToString()
						};
						DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat1,name", "HyCrop", " code = {0} ", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
						if (dataTable3.Rows.Count > 0)
						{
							if (i % 2 == 0)
							{
								dgv_cropList.Rows.Add();
							}
							int index = i / 2;
							if (i % 2 == 0)
							{
								dgv_cropList.Rows[index].Cells["CropList1"].Value = dataTable3.Rows[0]["name"].ToString();
							}
							if (i % 2 == 1)
							{
								dgv_cropList.Rows[index].Cells["CropList2"].Value = dataTable3.Rows[0]["name"].ToString();
							}
							HyScopeInfo hyScopeInfo = new HyScopeInfo();
							hyScopeInfo.contents = dataTable2.Rows[i]["contents"].ToString();
							hyScopeInfo.cropId = dataTable2.Rows[i]["cropId"].ToString();
							hyScopeInfo.cropName = dataTable3.Rows[0]["name"].ToString();
							hyScopeInfo.formCode = dataTable2.Rows[i]["formCode"].ToString();
							hyScopeInfo.pesticideId = dataTable2.Rows[i]["pesticideId"].ToString();
							MedInfoList.Add(hyScopeInfo);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void addMed(string cropId, string pestId)
		{
			List<MedInfo> list = new List<MedInfo>();
			string text = DateTime.Now.ToString("yyyyMMdd");
			string[] strWhereParameterArray = new string[3]
			{
				cropId,
				pestId,
				text
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hs.*", "HyScope as hs", "hs.cropId={0} and hs.pestId={1} AND hs.isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				bool flag = false;
				foreach (MedInfo item in list)
				{
					if (item._pesticideId.Equals(row["pesticideId"].ToString()) && item._contents.Equals(row["contents"].ToString()) && item._formCode.Equals(row["formCode"].ToString()))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					string[] strWhereParameterArray2 = new string[3]
					{
						row["pesticideId"].ToString(),
						row["formCode"].ToString(),
						row["contents"].ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hl.pesticideEname,hl.licType,hl.licNo", "hypos_GOODSLST as hg, HyLicence as hl", "hg.licType = hl.licType and hg.domManufId = hl.licNo and hl.pesticideId ={0} and hl.formCode={1} and hl.contents={2} and hl.isDelete='N' and hg.status!='D'", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
					list.Add(new MedInfo(row["pesticideId"].ToString(), row["formCode"].ToString(), row["contents"].ToString()));
					if (dataTable2.Rows.Count > 0)
					{
						dgv_cropList.Rows.Add(row["pesticideName"].ToString() + "\r\n" + dataTable2.Rows[0]["pesticideEname"].ToString() + " | " + row["formcode"].ToString() + " " + row["contents"].ToString(), "用藥說明", row["pesticideId"].ToString(), cropId, pestId, row["formCode"].ToString(), row["contents"].ToString());
					}
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (_WhereRUGo == 1)
			{
				if (Program.SystemMode == 1)
				{
					frs.RemoveLast();
					frs.setfocus();
					frs.Show();
				}
				else
				{
					frsm.RemoveLast();
					frsm.setfocus();
					frsm.Show();
				}
				Close();
			}
			if (_WhereRUGo == 2)
			{
				if (Program.SystemMode == 1)
				{
					Hide();
					frs.setfocus();
					frs.Show();
				}
				else
				{
					Hide();
					frsm.setfocus();
					frsm.Show();
				}
			}
			if (_WhereRUGo == 3)
			{
				Hide();
				frec.Show();
			}
		}

		private void dgv_cropList_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0)
			{
				if (dgv_cropList.Rows[e.RowIndex].Cells["CropList1"].Value == null)
				{
					return;
				}
			}
			else if (dgv_cropList.Rows[e.RowIndex].Cells["CropList2"].Value == null)
			{
				return;
			}
			Step2Control_Setting();
			dgv_pestList.Rows.Clear();
			dgv_pestList.Refresh();
			int index = e.RowIndex * 2 + e.ColumnIndex;
			label7.Text = "【作物】:" + MedInfoList[index].cropName;
			label9.Text = "【病蟲害】:";
			lastCropID = MedInfoList[index].cropId;
			string[] strWhereParameterArray = new string[4]
			{
				MedInfoList[index].pesticideId,
				MedInfoList[index].cropId,
				MedInfoList[index].formCode,
				MedInfoList[index].contents
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct pestId", "HyScope", "pesticideId={0} and cropId={1} and formCode={2} and contents={3} AND isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			PestList.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string[] strWhereParameterArray2 = new string[1]
				{
					dataTable.Rows[i]["pestId"].ToString()
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct cat1,name", "HyBlight", "code={0}", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count > 0)
				{
					if (i % 2 == 0)
					{
						dgv_pestList.Rows.Add();
					}
					int index2 = i / 2;
					if (i % 2 == 0)
					{
						dgv_pestList.Rows[index2].Cells["PestList1"].Value = dataTable2.Rows[0]["name"].ToString();
						PestList.Add(dataTable.Rows[i]["pestId"].ToString());
					}
					if (i % 2 == 1)
					{
						dgv_pestList.Rows[index2].Cells["PestList2"].Value = dataTable2.Rows[0]["name"].ToString();
						PestList.Add(dataTable.Rows[i]["pestId"].ToString());
					}
				}
			}
		}

		private void dgv_pestList_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0)
			{
				if (dgv_pestList.Rows[e.RowIndex].Cells["PestList1"].Value == null)
				{
					return;
				}
			}
			else if (dgv_pestList.Rows[e.RowIndex].Cells["PestList2"].Value == null)
			{
				return;
			}
			int index = e.RowIndex * 2 + e.ColumnIndex;
			label9.Text = "【病蟲害】:" + dgv_pestList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
			AutoClosingMessageBox.Show("選入用藥配對\n" + label10.Text + "\n" + label7.Text + "\n" + label9.Text);
			lastPestID = PestList[index];
			ReturnSource();
		}

		private void ReturnSource()
		{
			try
			{
				if (_WhereRUGo == 1)
				{
					string text = DateTime.Now.ToString("yyyyMMdd");
					string[] strWhereParameterArray = new string[1]
					{
						_barcode
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
						lastCropID,
						lastPestID,
						text
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hs.regStoreName", "HyScope as hs", "hs.pesticideId = {0} and hs.formCode ={1} and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName ='' AND hs.isDelete in ('N','')  ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						if (Program.SystemMode == 1)
						{
							frs.addCropAndPest(_count, lastCropID, lastPestID);
							Hide();
							frs.setfocus();
							frs.Show();
						}
						else
						{
							frsm.addCropAndPest(_count, lastCropID, lastPestID);
							Hide();
							frsm.setfocus();
							frsm.Show();
						}
						return;
					}
					string[] strWhereParameterArray3 = new string[6]
					{
						dataTable.Rows[0]["pesticideId"].ToString(),
						dataTable.Rows[0]["formCode"].ToString(),
						dataTable.Rows[0]["contents"].ToString(),
						lastCropID,
						lastPestID,
						text
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hs.regStoreName", "HyScope as hs", "hs.pesticideId = {0} and hs.formCode ={1} and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName !='' and hs.regStoreName !='99999999' AND hs.isDelete in ('N','')  ", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
					string text2 = "";
					if (dataTable2.Rows.Count > 0)
					{
						foreach (DataRow row in dataTable2.Rows)
						{
							text2 = text2 + "," + row["regStoreName"].ToString();
						}
						text2 = text2.Remove(0, 1);
						string[] strWhereParameterArray4 = new string[3]
						{
							dataTable.Rows[0]["pesticideId"].ToString(),
							dataTable.Rows[0]["formCode"].ToString(),
							dataTable.Rows[0]["contents"].ToString()
						};
						DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hl.licType,hl.licNo,hl.domManufName", "HyLicence as hl", "hl.pesticideId = {0} and hl.formCode ={1} and hl.contents={2} and hl.domManufId in (" + text2 + ")", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
						if (dataTable3.Rows.Count > 0)
						{
							List<string> list = new List<string>();
							string text3 = "";
							DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId", "hypos_GOODSLST", "GDSNO={0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
							foreach (DataRow row2 in dataTable3.Rows)
							{
								if (row2["licType"].ToString().Equals(dataTable4.Rows[0]["licType"].ToString()) && row2["licNo"].ToString().Equals(dataTable4.Rows[0]["domManufId"].ToString()))
								{
									flag = true;
								}
								else
								{
									list.Add(row2["domManufName"].ToString());
								}
							}
							if (flag)
							{
								if (Program.SystemMode == 1)
								{
									frs.addCropAndPest(_count, lastCropID, lastPestID);
									Hide();
									frs.setfocus();
									frs.Show();
								}
								else
								{
									frsm.addCropAndPest(_count, lastCropID, lastPestID);
									Hide();
									frsm.setfocus();
									frsm.Show();
								}
								return;
							}
							list = Enumerable.ToList(Enumerable.Distinct(list));
							foreach (string item in list)
							{
								text3 = text3 + "[" + item + "]";
							}
							AutoClosingMessageBox.Show("此用藥配對尚於資料保護期間，僅可選擇" + text3 + "用藥");
						}
						else if (Program.SystemMode == 1)
						{
							frs.addCropAndPest(_count, lastCropID, lastPestID);
							Hide();
							frs.setfocus();
							frs.Show();
						}
						else
						{
							frsm.addCropAndPest(_count, lastCropID, lastPestID);
							Hide();
							frsm.setfocus();
							frsm.Show();
						}
					}
					else if (Program.SystemMode == 1)
					{
						frs.addCropAndPest(_count, lastCropID, lastPestID);
						Hide();
						frs.setfocus();
						frs.Show();
					}
					else
					{
						frsm.addCropAndPest(_count, lastCropID, lastPestID);
						Hide();
						frsm.setfocus();
						frsm.Show();
					}
					return;
				}
				if (_WhereRUGo == 2)
				{
					string text4 = DateTime.Now.ToString("yyyyMMdd");
					string[] strWhereParameterArray5 = new string[1]
					{
						_barcode
					};
					bool flag2 = false;
					string[] strWhereParameterArray6 = new string[1]
					{
						lastCropID
					};
					DataTable dataTable5 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyCrop", "code = {0}", "", null, strWhereParameterArray6, CommandOperationType.ExecuteReaderReturnDataTable);
					string[] strWhereParameterArray7 = new string[1]
					{
						lastPestID
					};
					DataTable dataTable6 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyBlight", "code = {0}", "", null, strWhereParameterArray7, CommandOperationType.ExecuteReaderReturnDataTable);
					DataTable dataTable7 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable7.Rows.Count > 0)
					{
						string[] strWhereParameterArray8 = new string[6]
						{
							dataTable7.Rows[0]["pesticideId"].ToString(),
							dataTable7.Rows[0]["formCode"].ToString(),
							dataTable7.Rows[0]["contents"].ToString(),
							lastCropID,
							lastPestID,
							text4
						};
						if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hs.regStoreName", "HyScope as hs", "hs.pesticideId = {0} and hs.formCode ={1} and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName ='' AND hs.isDelete in ('N','')  ", "", null, strWhereParameterArray8, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
						{
							if (Program.SystemMode == 1)
							{
								frs.addOnecommodity(_barcode, lastCropID, lastPestID, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
								Hide();
								frs.setfocus();
								frs.Show();
							}
							else
							{
								frsm.addOnecommodity(_barcode, lastCropID, lastPestID, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
								Hide();
								frsm.setfocus();
								frsm.Show();
							}
							return;
						}
						string[] strWhereParameterArray9 = new string[6]
						{
							dataTable7.Rows[0]["pesticideId"].ToString(),
							dataTable7.Rows[0]["formCode"].ToString(),
							dataTable7.Rows[0]["contents"].ToString(),
							lastCropID,
							lastPestID,
							text4
						};
						DataTable dataTable8 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hs.regStoreName", "HyScope as hs", "hs.pesticideId = {0} and hs.formCode ={1} and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName !='' and hs.regStoreName !='99999999' AND hs.isDelete in ('N','')  ", "", null, strWhereParameterArray9, CommandOperationType.ExecuteReaderReturnDataTable);
						string text5 = "";
						if (dataTable8.Rows.Count > 0)
						{
							foreach (DataRow row3 in dataTable8.Rows)
							{
								text5 = text5 + "," + row3["regStoreName"].ToString();
							}
							text5 = text5.Remove(0, 1);
							string[] strWhereParameterArray10 = new string[3]
							{
								dataTable7.Rows[0]["pesticideId"].ToString(),
								dataTable7.Rows[0]["formCode"].ToString(),
								dataTable7.Rows[0]["contents"].ToString()
							};
							DataTable dataTable9 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hl.licType,hl.licNo,hl.domManufName", "HyLicence as hl", "hl.pesticideId = {0} and hl.formCode ={1} and hl.contents={2} and hl.domManufId in (" + text5 + ")", "", null, strWhereParameterArray10, CommandOperationType.ExecuteReaderReturnDataTable);
							if (dataTable9.Rows.Count > 0)
							{
								List<string> list2 = new List<string>();
								string text6 = "";
								DataTable dataTable10 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId", "hypos_GOODSLST", "GDSNO={0}", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable);
								foreach (DataRow row4 in dataTable9.Rows)
								{
									if (row4["licType"].ToString().Equals(dataTable10.Rows[0]["licType"].ToString()) && row4["licNo"].ToString().Equals(dataTable10.Rows[0]["domManufId"].ToString()))
									{
										flag2 = true;
									}
									else
									{
										list2.Add(row4["domManufName"].ToString());
									}
								}
								if (flag2)
								{
									if (Program.SystemMode == 1)
									{
										frs.addOnecommodity(_barcode, lastCropID, lastPestID, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
										Hide();
										frs.setfocus();
										frs.Show();
									}
									else
									{
										frsm.addOnecommodity(_barcode, lastCropID, lastPestID, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
										Hide();
										frsm.setfocus();
										frsm.Show();
									}
									return;
								}
								list2 = Enumerable.ToList(Enumerable.Distinct(list2));
								foreach (string item2 in list2)
								{
									text6 = text6 + "[" + item2 + "]";
								}
								AutoClosingMessageBox.Show("此用藥配對尚於資料保護期間，僅可選擇" + text6 + "用藥");
							}
							else if (Program.SystemMode == 1)
							{
								frs.addOnecommodity(_barcode, lastCropID, lastPestID, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
								Hide();
								frs.setfocus();
								frs.Show();
							}
							else
							{
								frsm.addOnecommodity(_barcode, lastCropID, lastPestID, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
								Hide();
								frsm.setfocus();
								frsm.Show();
							}
						}
						else if (Program.SystemMode == 1)
						{
							frs.addOnecommodity(_barcode, lastCropID, lastPestID, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
							Hide();
							frs.setfocus();
							frs.Show();
						}
						else
						{
							frsm.addOnecommodity(_barcode, lastCropID, lastPestID, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
							Hide();
							frsm.setfocus();
							frsm.Show();
						}
					}
					else if (Program.SystemMode == 1)
					{
						frs.addOnecommodity(_barcode, lastCropID, lastPestID, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
						Hide();
						frs.setfocus();
						frs.Show();
					}
					else
					{
						frsm.addOnecommodity(_barcode, lastCropID, lastPestID, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
						Hide();
						frsm.setfocus();
						frsm.Show();
					}
				}
				else
				{
					if (_WhereRUGo != 3)
					{
						return;
					}
					string text7 = DateTime.Now.ToString("yyyyMMdd");
					string[] strWhereParameterArray11 = new string[1]
					{
						_barcode
					};
					bool flag3 = false;
					DataTable dataTable11 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId,pesticideId,formCode,contents", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray11, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable11.Rows.Count > 0)
					{
						string[] strWhereParameterArray12 = new string[6]
						{
							dataTable11.Rows[0]["pesticideId"].ToString(),
							dataTable11.Rows[0]["formCode"].ToString(),
							dataTable11.Rows[0]["contents"].ToString(),
							lastCropID,
							lastPestID,
							text7
						};
						if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hs.regStoreName", "HyScope as hs", "hs.pesticideId = {0} and hs.formCode ={1} and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName ='' AND hs.isDelete in ('N','')  ", "", null, strWhereParameterArray12, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
						{
							addUserPair(dataTable11.Rows[0]["pesticideId"].ToString(), dataTable11.Rows[0]["formCode"].ToString(), dataTable11.Rows[0]["contents"].ToString(), lastCropID, lastPestID);
							Hide();
							frec.Show();
							return;
						}
						string[] strWhereParameterArray13 = new string[6]
						{
							dataTable11.Rows[0]["pesticideId"].ToString(),
							dataTable11.Rows[0]["formCode"].ToString(),
							dataTable11.Rows[0]["contents"].ToString(),
							lastCropID,
							lastPestID,
							text7
						};
						DataTable dataTable12 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hs.regStoreName", "HyScope as hs", "hs.pesticideId = {0} and hs.formCode ={1} and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName !='' and hs.regStoreName !='99999999' AND hs.isDelete in ('N','')  ", "", null, strWhereParameterArray13, CommandOperationType.ExecuteReaderReturnDataTable);
						string text8 = "";
						if (dataTable12.Rows.Count > 0)
						{
							foreach (DataRow row5 in dataTable12.Rows)
							{
								text8 = text8 + "," + row5["regStoreName"].ToString();
							}
							text8 = text8.Remove(0, 1);
							string[] strWhereParameterArray14 = new string[3]
							{
								dataTable11.Rows[0]["pesticideId"].ToString(),
								dataTable11.Rows[0]["formCode"].ToString(),
								dataTable11.Rows[0]["contents"].ToString()
							};
							DataTable dataTable13 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hl.licType,hl.licNo,hl.domManufName", "HyLicence as hl", "hl.pesticideId = {0} and hl.formCode ={1} and hl.contents={2} and hl.domManufId in (" + text8 + ")", "", null, strWhereParameterArray14, CommandOperationType.ExecuteReaderReturnDataTable);
							if (dataTable13.Rows.Count <= 0)
							{
								return;
							}
							List<string> list3 = new List<string>();
							string text9 = "";
							DataTable dataTable14 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,domManufId", "hypos_GOODSLST", "GDSNO={0}", "", null, strWhereParameterArray11, CommandOperationType.ExecuteReaderReturnDataTable);
							foreach (DataRow row6 in dataTable13.Rows)
							{
								if (row6["licType"].ToString().Equals(dataTable14.Rows[0]["licType"].ToString()) && row6["licNo"].ToString().Equals(dataTable14.Rows[0]["domManufId"].ToString()))
								{
									flag3 = true;
								}
								else
								{
									list3.Add(row6["domManufName"].ToString());
								}
							}
							if (flag3)
							{
								addUserPair(dataTable11.Rows[0]["pesticideId"].ToString(), dataTable11.Rows[0]["formCode"].ToString(), dataTable11.Rows[0]["contents"].ToString(), lastCropID, lastPestID);
								Hide();
								frec.Show();
								return;
							}
							list3 = Enumerable.ToList(Enumerable.Distinct(list3));
							foreach (string item3 in list3)
							{
								text9 = text9 + "[" + item3 + "]";
							}
							AutoClosingMessageBox.Show("此用藥配對尚於資料保護期間，僅可選擇" + text9 + "用藥");
						}
						else
						{
							addUserPair(dataTable11.Rows[0]["pesticideId"].ToString(), dataTable11.Rows[0]["formCode"].ToString(), dataTable11.Rows[0]["contents"].ToString(), lastCropID, lastPestID);
							Hide();
							frec.Show();
						}
					}
					else
					{
						addUserPair(dataTable11.Rows[0]["pesticideId"].ToString(), dataTable11.Rows[0]["formCode"].ToString(), dataTable11.Rows[0]["contents"].ToString(), lastCropID, lastPestID);
						Hide();
						frec.Show();
					}
					return;
				}
			}
			catch (Exception)
			{
			}
		}

		private void addUserPair(string pesticideId, string formCode, string contents, string cropCode, string pestCode)
		{
			string[] array = new string[3]
			{
				_barcode,
				cropCode,
				pestCode
			};
			string[,] strFieldArray = new string[5, 2]
			{
				{
					"VipNo",
					""
				},
				{
					"barcode",
					_barcode
				},
				{
					"total",
					"1"
				},
				{
					"cropId",
					cropCode
				},
				{
					"pestId",
					pestCode
				}
			};
			if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "total", "hypos_user_pair", " barcode ={0} and cropId={1} and pestId={2} ", "", null, array, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
			{
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_user_pair SET total = total+1 where  barcode ={0} and cropId={1} and pestId={2} ", array, CommandOperationType.ExecuteNonQuery);
			}
			else
			{
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_pair", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			}
		}

		private void Step1Control_Setting()
		{
			label2.ForeColor = Color.White;
			panel1.BackColor = Color.FromArgb(125, 156, 35);
			label3.ForeColor = Color.Black;
			panel3.BackColor = Color.White;
			label5.ForeColor = Color.Black;
			panel5.BackColor = Color.White;
		}

		private void Step2Control_Setting()
		{
			label2.ForeColor = Color.Black;
			panel1.BackColor = Color.White;
			label3.ForeColor = Color.White;
			panel3.BackColor = Color.FromArgb(125, 156, 35);
			label5.ForeColor = Color.Black;
			panel5.BackColor = Color.White;
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
			label6 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			dgv_pestList = new System.Windows.Forms.DataGridView();
			dgv_cropList = new System.Windows.Forms.DataGridView();
			label8 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			CropList1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			CropList2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			PestList1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			PestList2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgv_pestList).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgv_cropList).BeginInit();
			panel5.SuspendLayout();
			panel3.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 150);
			pb_virtualKeyBoard.Visible = false;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.Location = new System.Drawing.Point(17, 129);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(124, 24);
			label6.TabIndex = 54;
			label6.Text = "用藥範圍設定";
			button1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.Location = new System.Drawing.Point(808, 56);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(119, 43);
			button1.TabIndex = 71;
			button1.Text = "回上一步";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			dgv_pestList.AllowUserToAddRows = false;
			dgv_pestList.AllowUserToDeleteRows = false;
			dgv_pestList.AllowUserToResizeColumns = false;
			dgv_pestList.AllowUserToResizeRows = false;
			dgv_pestList.Anchor = System.Windows.Forms.AnchorStyles.None;
			dgv_pestList.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			dgv_pestList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dgv_pestList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dgv_pestList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgv_pestList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dgv_pestList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv_pestList.ColumnHeadersVisible = false;
			dgv_pestList.Columns.AddRange(PestList1, PestList2);
			dgv_pestList.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgv_pestList.DefaultCellStyle = dataGridViewCellStyle2;
			dgv_pestList.EnableHeadersVisualStyles = false;
			dgv_pestList.GridColor = System.Drawing.SystemColors.ActiveBorder;
			dgv_pestList.Location = new System.Drawing.Point(492, 192);
			dgv_pestList.MultiSelect = false;
			dgv_pestList.Name = "dgv_pestList";
			dgv_pestList.ReadOnly = true;
			dgv_pestList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgv_pestList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dgv_pestList.RowHeadersVisible = false;
			dgv_pestList.RowTemplate.Height = 40;
			dgv_pestList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgv_pestList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			dgv_pestList.Size = new System.Drawing.Size(479, 394);
			dgv_pestList.TabIndex = 70;
			dgv_pestList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_pestList_CellContentClick);
			dgv_cropList.AllowUserToAddRows = false;
			dgv_cropList.AllowUserToDeleteRows = false;
			dgv_cropList.AllowUserToResizeColumns = false;
			dgv_cropList.AllowUserToResizeRows = false;
			dgv_cropList.Anchor = System.Windows.Forms.AnchorStyles.None;
			dgv_cropList.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			dgv_cropList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dgv_cropList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dgv_cropList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 12f);
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgv_cropList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			dgv_cropList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv_cropList.ColumnHeadersVisible = false;
			dgv_cropList.Columns.AddRange(CropList1, CropList2);
			dgv_cropList.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dgv_cropList.DefaultCellStyle = dataGridViewCellStyle5;
			dgv_cropList.EnableHeadersVisualStyles = false;
			dgv_cropList.GridColor = System.Drawing.SystemColors.ActiveBorder;
			dgv_cropList.Location = new System.Drawing.Point(10, 192);
			dgv_cropList.MultiSelect = false;
			dgv_cropList.Name = "dgv_cropList";
			dgv_cropList.ReadOnly = true;
			dgv_cropList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dgv_cropList.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			dgv_cropList.RowHeadersVisible = false;
			dgv_cropList.RowTemplate.Height = 40;
			dgv_cropList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dgv_cropList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			dgv_cropList.Size = new System.Drawing.Size(476, 394);
			dgv_cropList.TabIndex = 69;
			dgv_cropList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_cropList_CellContentClick);
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
			panel5.BackColor = System.Drawing.Color.Transparent;
			panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel5.Controls.Add(label5);
			panel5.Location = new System.Drawing.Point(510, 38);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(231, 73);
			panel5.TabIndex = 77;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Black;
			label5.Location = new System.Drawing.Point(62, 12);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(86, 48);
			label5.TabIndex = 3;
			label5.Text = "End\r\n返回收銀";
			panel3.BackColor = System.Drawing.Color.White;
			panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel3.Controls.Add(label3);
			panel3.ForeColor = System.Drawing.Color.White;
			panel3.Location = new System.Drawing.Point(266, 38);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(238, 73);
			panel3.TabIndex = 79;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.Black;
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
			panel1.Location = new System.Drawing.Point(17, 38);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(242, 73);
			panel1.TabIndex = 76;
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
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label7.Location = new System.Drawing.Point(13, 605);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(86, 24);
			label7.TabIndex = 80;
			label7.Text = "【作物】";
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label9.Location = new System.Drawing.Point(317, 605);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(105, 24);
			label9.TabIndex = 81;
			label9.Text = "【病蟲害】";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label10.Location = new System.Drawing.Point(639, 605);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(86, 24);
			label10.TabIndex = 82;
			label10.Text = "【用藥】";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label4.Location = new System.Drawing.Point(195, 165);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(105, 24);
			label4.TabIndex = 83;
			label4.Text = "請選擇作物";
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label12.Location = new System.Drawing.Point(673, 165);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(124, 24);
			label12.TabIndex = 84;
			label12.Text = "請選擇病蟲害";
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			CropList1.DefaultCellStyle = dataGridViewCellStyle7;
			CropList1.HeaderText = "CropList1";
			CropList1.Name = "CropList1";
			CropList1.ReadOnly = true;
			CropList1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			CropList1.Width = 237;
			dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			CropList2.DefaultCellStyle = dataGridViewCellStyle8;
			CropList2.HeaderText = "CropList2";
			CropList2.Name = "CropList2";
			CropList2.ReadOnly = true;
			CropList2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			CropList2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			CropList2.Width = 237;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			PestList1.DefaultCellStyle = dataGridViewCellStyle9;
			PestList1.HeaderText = "";
			PestList1.Name = "PestList1";
			PestList1.ReadOnly = true;
			PestList1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			PestList1.Width = 237;
			dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			PestList2.DefaultCellStyle = dataGridViewCellStyle10;
			PestList2.HeaderText = "";
			PestList2.Name = "PestList2";
			PestList2.ReadOnly = true;
			PestList2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			PestList2.Width = 237;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(981, 662);
			base.Controls.Add(label12);
			base.Controls.Add(label4);
			base.Controls.Add(label10);
			base.Controls.Add(label9);
			base.Controls.Add(label7);
			base.Controls.Add(dgv_cropList);
			base.Controls.Add(panel5);
			base.Controls.Add(panel3);
			base.Controls.Add(panel1);
			base.Controls.Add(button1);
			base.Controls.Add(dgv_pestList);
			base.Controls.Add(label11);
			base.Controls.Add(label8);
			base.Controls.Add(label6);
			base.Name = "frmCropPestRange_NEW";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "";
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(dgv_pestList, 0);
			base.Controls.SetChildIndex(button1, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(panel3, 0);
			base.Controls.SetChildIndex(panel5, 0);
			base.Controls.SetChildIndex(dgv_cropList, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(label4, 0);
			base.Controls.SetChildIndex(label12, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			((System.ComponentModel.ISupportInitialize)dgv_pestList).EndInit();
			((System.ComponentModel.ISupportInitialize)dgv_cropList).EndInit();
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
