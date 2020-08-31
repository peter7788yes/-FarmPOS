using NPOI.HSSF.UserModel;
using POS_Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmMemberMangement : MasterThinForm
	{
		private UC_Member[] ucMembers;

		private int pageNow = 1;

		public int pageTotal = 1;

		public DataTable dt;

		private string status = "Status = 0";

		private string orderBy = "CreateDate DESC";

		private HSSFWorkbook wb1;

		private HSSFSheet sh;

		private string str_file_location = "\\";

		private string str_file_name = "會員清單";

		private string str_file_type = ".xls";

		private string str_file = "";

		private int report_daily_rowcount;

		private int report_DeliveryDaily_rowcount;

		private DataTable dt_details1;

		private DataTable dt_details2;

		private List<string> lst_selmember = new List<string>();

		private List<string> lst_Salesselmember = new List<string>();

		private List<string> lst_commodity = new List<string>();

		private List<string> lst_commodity2 = new List<string>();

		private List<string> lst_selvendor = new List<string>();

		private List<string> lst_selTEMP = new List<string>();

		private IContainer components;

		private UC_Member uC_Member1;

		private UC_Member uC_Member2;

		private UC_Member uC_Member3;

		private UC_Member uC_Member4;

		private UC_Member uC_Member5;

		private UC_Member uC_Member6;

		private UC_Member uC_Member7;

		private UC_Member uC_Member8;

		private Button btn_pageRight;

		private Button btn_pageLeft;

		private Label l_status;

		private Button btn_statusAll;

		private Button btn_statusNormal;

		private Button btn_statusSuspend;

		private Button btn_sortByCreateDate;

		private Label l_sortBy;

		private Button btn_SortByEditDate;

		private Button btn_SortByBuyDate;

		private Label l_batchPrint;

		private Button btn_saveToTemp;

		private Button btn_viewTempList;

		private Button btn_createMember;

		private Button btn_importList;

		private Button btn_memberSearch;

		private Label l_pageInfo;

		private Button btn_firstPage;

		private Button btn_previousPage;

		private Button btn_nextPage;

		private Button btn_lastPage;

		private Label l_pageNow;

		public Label l_memberList;

		private Button btn_downloadTemplate;

		private Button btn_DownLoadVipData;

		private Button btn_ImportCSV;

		public frmMemberMangement()
			: base("會員管理")
		{
			InitializeComponent();
			ucMembers = new UC_Member[8]
			{
				uC_Member1,
				uC_Member2,
				uC_Member3,
				uC_Member4,
				uC_Member5,
				uC_Member6,
				uC_Member7,
				uC_Member8
			};
			UC_Member[] array = ucMembers;
			foreach (UC_Member obj in array)
			{
				obj.OnClickMember += new EventHandler(viewMemberInfo);
				obj.showCancelBtn(true);
			}
		}

		private void btn_saveToTemp_Click(object sender, EventArgs e)
		{
			bool flag = false;
			UC_Member[] array = ucMembers;
			foreach (UC_Member uC_Member in array)
			{
				if (uC_Member.isChecked())
				{
					flag = true;
					if (!Program.membersTemp.Contains(uC_Member.getMemberVipNo()))
					{
						Program.membersTemp.Add(uC_Member.getMemberVipNo());
					}
					uC_Member.checkMember(false);
					uC_Member.BackColor = Color.White;
				}
			}
			if (!flag)
			{
				AutoClosingMessageBox.Show("請先勾選欲放入暫存的資料");
			}
			else
			{
				AutoClosingMessageBox.Show("勾選資料已放入暫存");
			}
		}

		private void btn_viewTempList_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember())
			{
				if (Program.membersTemp.Count == 0)
				{
					AutoClosingMessageBox.Show("無暫存會員");
				}
				else
				{
					new frmBatchPrintMember().ShowDialog();
				}
			}
		}

		public void viewMemberInfo(object vipNo, EventArgs s)
		{
			if (!hasSelectedMember())
			{
				switchForm(new frmEditMember(vipNo.ToString()));
			}
		}

		private void frmMemberMangement_Load(object sender, EventArgs e)
		{
			btn_createMember.Focus();
			reload();
		}

		public void changePage(int page)
		{
			int num = 0;
			pageNow = page;
			for (int i = (pageNow - 1) * 8; i < pageNow * 8; i++)
			{
				if (i < dt.Rows.Count)
				{
					ucMembers[num].setMemberName(dt.Rows[i]["Name"].ToString());
					ucMembers[num].setMemberVipNo(dt.Rows[i]["VipNo"].ToString());
					string memberHTEL = "".Equals(dt.Rows[i]["Mobile"].ToString()) ? dt.Rows[i]["Telphone"].ToString() : dt.Rows[i]["Mobile"].ToString();
					ucMembers[num].setMemberHTEL(memberHTEL);
					ucMembers[num].setMemberIdNo(dt.Rows[i]["IdNo"].ToString());
					ucMembers[num].setCredit(dt.Rows[i]["Credit"].ToString());
					ucMembers[num].setTotal(dt.Rows[i]["Total"].ToString());
					ucMembers[num].Visible = true;
				}
				else
				{
					ucMembers[num].Visible = false;
				}
				ucMembers[num].checkMember(false);
				ucMembers[num].BackColor = Color.White;
				num++;
			}
			l_pageNow.Text = string.Format("{0}", pageNow);
			l_pageInfo.Text = string.Format("共{0}筆．{1}頁｜目前在第{2}頁", dt.Rows.Count, Math.Ceiling((double)dt.Rows.Count / 8.0), pageNow);
		}

		private void btn_sortByCreateDate_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember())
			{
				orderBy = "CreateDate DESC";
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", status, orderBy, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_sortByCreateDate.BackColor = Color.FromArgb(247, 106, 45);
				btn_sortByCreateDate.ForeColor = Color.White;
				btn_SortByBuyDate.BackColor = Color.White;
				btn_SortByBuyDate.ForeColor = Color.FromArgb(247, 106, 45);
				btn_SortByEditDate.BackColor = Color.White;
				btn_SortByEditDate.ForeColor = Color.FromArgb(247, 106, 45);
			}
		}

		private void btn_SortByEditDate_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember())
			{
				orderBy = "UpdateDate DESC";
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", status, orderBy, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_SortByEditDate.BackColor = Color.FromArgb(247, 106, 45);
				btn_SortByEditDate.ForeColor = Color.White;
				btn_SortByBuyDate.BackColor = Color.White;
				btn_SortByBuyDate.ForeColor = Color.FromArgb(247, 106, 45);
				btn_sortByCreateDate.BackColor = Color.White;
				btn_sortByCreateDate.ForeColor = Color.FromArgb(247, 106, 45);
			}
		}

		private void btn_SortByBuyDate_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember())
			{
				orderBy = "BuyDate DESC";
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", status, orderBy, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_SortByBuyDate.BackColor = Color.FromArgb(247, 106, 45);
				btn_SortByBuyDate.ForeColor = Color.White;
				btn_SortByEditDate.BackColor = Color.White;
				btn_SortByEditDate.ForeColor = Color.FromArgb(247, 106, 45);
				btn_sortByCreateDate.BackColor = Color.White;
				btn_sortByCreateDate.ForeColor = Color.FromArgb(247, 106, 45);
			}
		}

		private void btn_statusAll_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember())
			{
				status = "";
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", status, orderBy, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_statusAll.ForeColor = Color.White;
				btn_statusAll.BackColor = Color.FromArgb(247, 106, 45);
				btn_statusNormal.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusNormal.BackColor = Color.White;
				btn_statusSuspend.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusSuspend.BackColor = Color.White;
			}
		}

		private void btn_statusNormal_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember())
			{
				status = "Status = 0";
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", status, orderBy, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusAll.BackColor = Color.White;
				btn_statusNormal.ForeColor = Color.White;
				btn_statusNormal.BackColor = Color.FromArgb(247, 106, 45);
				btn_statusSuspend.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusSuspend.BackColor = Color.White;
			}
		}

		private void btn_statusSuspend_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember())
			{
				status = "Status = 1";
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", status, orderBy, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusAll.BackColor = Color.White;
				btn_statusNormal.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusNormal.BackColor = Color.White;
				btn_statusSuspend.ForeColor = Color.White;
				btn_statusSuspend.BackColor = Color.FromArgb(247, 106, 45);
			}
		}

		private void btn_createMember_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember())
			{
				switchForm(new frmNewMember());
			}
		}

		private bool hasSelectedMember()
		{
			UC_Member[] array = ucMembers;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].isChecked())
				{
					if (MessageBox.Show("勾選的資料尚未放入暫存清單，是否放棄將勾選放入暫存？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						return false;
					}
					return true;
				}
			}
			return false;
		}

		private void btn_pageRight_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember() && pageNow < pageTotal)
			{
				changePage(pageNow + 1);
			}
		}

		private void btn_pageLeft_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember() && pageNow > 1)
			{
				changePage(pageNow - 1);
			}
		}

		private void btn_lastPage_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember() && pageNow < pageTotal)
			{
				changePage(pageTotal);
			}
		}

		private void btn_firstPage_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember() && pageNow > 1)
			{
				changePage(1);
			}
		}

		private void btn_memberSearch_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember())
			{
				new frmSearchMember().ShowDialog(this);
			}
		}

		private void btn_importList_Click(object sender, EventArgs e)
		{
			if (!hasSelectedMember())
			{
				importList();
			}
		}

		private void importList()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "xls Files|*.xls";
			openFileDialog.Title = "請選擇會員檔案";
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			try
			{
				string path = openFileDialog.FileName.ToString();
				new List<string>();
				HSSFWorkbook hSSFWorkbook;
				using (FileStream s = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
				{
					hSSFWorkbook = new HSSFWorkbook(s);
				}
				HSSFSheet hSSFSheet = (HSSFSheet)hSSFWorkbook.GetSheetAt(0);
				int num = 0;
				int num2 = 0;
				string text = "";
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "LicenseCode", "hypos_RegisterLicense", "", "", null, new string[0], CommandOperationType.ExecuteScalar).ToString();
				for (int i = 2; i <= hSSFSheet.LastRowNum; i++)
				{
					HSSFRow hSSFRow = (HSSFRow)hSSFSheet.GetRow(i);
					bool flag = true;
					for (int j = 2; j < 13; j++)
					{
						if (!string.IsNullOrEmpty(hSSFRow.GetCell(j).ToString()))
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						string text2 = "";
						if (string.IsNullOrEmpty(hSSFRow.GetCell(2).ToString()))
						{
							text2 += "會員姓名(必填), ";
						}
						if (string.IsNullOrEmpty(hSSFRow.GetCell(3).ToString()))
						{
							text2 += "會員類型(必填), ";
						}
						if (string.IsNullOrEmpty(hSSFRow.GetCell(5).ToString()))
						{
							text2 += "出生年月日(必填), ";
						}
						if (string.IsNullOrEmpty(hSSFRow.GetCell(8).ToString()))
						{
							text2 += "地址(縣市)(必填), ";
						}
						if (string.IsNullOrEmpty(hSSFRow.GetCell(9).ToString()))
						{
							text2 += "地址(鄉鎮區)(必填), ";
						}
						if (string.IsNullOrEmpty(hSSFRow.GetCell(10).ToString()))
						{
							text2 += "地址(詳細路名)(必填), ";
						}
						if (text2.Length > 1)
						{
							text2 = "第" + (i + 1) + "行:" + text2.Substring(0, text2.Length - 2) + "\r\n";
							num2++;
							text += text2;
						}
						else
						{
							try
							{
								string text3 = hSSFRow.GetCell(9).ToString();
								text3 = text3.Substring(text3.Length - 3);
								string text4 = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "cityno", "ADDRAREA", "zipcode = {0}", "", null, new string[1]
								{
									text3
								}, CommandOperationType.ExecuteScalar).ToString();
								string text5 = "";
								string text6 = "";
								if (string.IsNullOrEmpty(hSSFRow.GetCell(6).ToString()))
								{
									text5 += "電話為空, ";
								}
								text6 = ((text5.Length <= 1) ? hSSFRow.GetCell(6).ToString() : "");
								string a = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Telphone ", " hypos_CUST_RTL", "Telphone = {0}", "", null, new string[1]
								{
									text6
								}, CommandOperationType.ExecuteScalar).ToString();
								string text7 = "";
								string text8 = "";
								if (string.IsNullOrEmpty(hSSFRow.GetCell(7).ToString()))
								{
									text7 += "電話為空, ";
								}
								text8 = ((text7.Length <= 1) ? hSSFRow.GetCell(7).ToString() : "");
								string a2 = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Mobile", " hypos_CUST_RTL", "Mobile = {0}", "", null, new string[1]
								{
									text8
								}, CommandOperationType.ExecuteScalar).ToString();
								string a3 = hSSFRow.GetCell(2).ToString();
								a3 = ((!(a3 != "")) ? "" : hSSFRow.GetCell(2).ToString());
								string a4 = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Name", " hypos_CUST_RTL", "Name = {0}", "", null, new string[1]
								{
									a3
								}, CommandOperationType.ExecuteScalar).ToString();
								DateTime dateCellValue = hSSFRow.GetCell(5).DateCellValue;
								string text9 = string.Format("{0}-{1}-{2}", dateCellValue.Year, dateCellValue.Month, dateCellValue.Day);
								string text10 = "";
								string text11 = dateCellValue.Month.ToString();
								string text12 = dateCellValue.Day.ToString();
								if (text11.Length == 1)
								{
									text11 = text11.Insert(0, "0");
								}
								if (text12.Length == 1)
								{
									text12 = text12.Insert(0, "0");
								}
								text10 = string.Format("{0}-{1}-{2}", dateCellValue.Year, text11, text12);
								string a5 = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "BirthDate", " hypos_CUST_RTL", "BirthDate = {0}", "", null, new string[1]
								{
									text9
								}, CommandOperationType.ExecuteScalar).ToString();
								string a6 = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "BirthDate", " hypos_CUST_RTL", "BirthDate = {0}", "", null, new string[1]
								{
									text10
								}, CommandOperationType.ExecuteScalar).ToString();
								string newVipNo = frmNewMember.getNewVipNo();
								string[,] strFieldArray = new string[14, 2]
								{
									{
										"LicenseCode",
										Program.LincenseCode
									},
									{
										"VipNo",
										newVipNo
									},
									{
										"IdNo",
										hSSFRow.GetCell(4).ToString()
									},
									{
										"Name",
										hSSFRow.GetCell(2).ToString()
									},
									{
										"BirthDate",
										text9
									},
									{
										"Telphone",
										hSSFRow.GetCell(6).ToString()
									},
									{
										"Mobile",
										hSSFRow.GetCell(7).ToString()
									},
									{
										"Type",
										hSSFRow.GetCell(3).ToString().Trim()
											.Substring(0, 1)
									},
									{
										"City",
										text4
									},
									{
										"Area",
										text3
									},
									{
										"Address",
										hSSFRow.GetCell(10).ToString()
									},
									{
										"CompanyIdNo",
										hSSFRow.GetCell(12).ToString()
									},
									{
										"CompanyName",
										hSSFRow.GetCell(11).ToString()
									},
									{
										"Status",
										"0"
									}
								};
								if ((a4 != "-1" && a != "-1" && (a5 != "-1" || a6 != "-1")) || (a4 != "-1" && a2 != "-1" && (a5 != "-1" || a6 != "-1")))
								{
									DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET IdNo = {0}, Name = {1}, BirthDate = {2}, Telphone = {3}, Mobile = {4}, Type = {5}, City = {6}, Area = {7}, Address = {8}, CompanyIdNo = {9}, CompanyName = {10}, Status = {11} where (Name = {12} and Telphone = {13} and BirthDate = {14}) or (Name = {15} and Mobile = {16} and BirthDate = {17})", new string[18]
									{
										hSSFRow.GetCell(4).ToString(),
										hSSFRow.GetCell(2).ToString(),
										text9,
										hSSFRow.GetCell(6).ToString(),
										hSSFRow.GetCell(7).ToString(),
										hSSFRow.GetCell(3).ToString().Trim()
											.Substring(0, 1),
										text4,
										text3,
										hSSFRow.GetCell(10).ToString(),
										hSSFRow.GetCell(12).ToString(),
										hSSFRow.GetCell(11).ToString(),
										"0",
										hSSFRow.GetCell(2).ToString(),
										hSSFRow.GetCell(6).ToString(),
										text9,
										hSSFRow.GetCell(2).ToString(),
										hSSFRow.GetCell(7).ToString(),
										text9
									}, CommandOperationType.ExecuteNonQuery);
								}
								else
								{
									DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_CUST_RTL", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
								}
							}
							catch (Exception)
							{
								num2++;
								text = text + "匯入第" + (i + 1) + " 筆失敗,請重新檢查!\r\n";
							}
						}
						num++;
						continue;
					}
					num = i - 2;
					break;
				}
				reload();
				string text13 = string.Format("成功:{0}筆, 失敗:{1}筆, 總共:{2}筆", num - num2, num2, num);
				if (text.Length > 1)
				{
					text13 = text13 + "\r\n失敗清單:\r\n" + text;
				}
				new frmImportResult(text13).ShowDialog();
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message.ToString());
			}
		}

		private void reload()
		{
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", status, orderBy, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			pageTotal = (int)Math.Ceiling((double)dt.Rows.Count / 8.0);
			changePage(1);
		}

		private void btn_downloadTemplate_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			if (File.Exists(folderBrowserDialog.SelectedPath + "\\會員匯入範例.xls"))
			{
				if (MessageBox.Show("檔案已存在，是否覆蓋?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					File.Copy("會員匯入範例.xls", folderBrowserDialog.SelectedPath + "\\會員匯入範例.xls", true);
				}
			}
			else
			{
				File.Copy("會員匯入範例.xls", folderBrowserDialog.SelectedPath + "\\會員匯入範例.xls", true);
			}
		}

		private bool IsFileLocked(FileInfo file)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
			}
			catch (IOException)
			{
				return true;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return false;
		}

		private void btn_DownLoadVipData_Click(object sender, EventArgs e)
		{
			try
			{
				string sql = "SELECT LicenseCode, VipNo, Name, Type, IdNo, BirthDate, Telphone, Mobile, City, Area, Address, CompanyName, CompanyIdNo FROM hypos_CUST_RTL where Status in ('0', '1') order by CreateDate desc";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				folderBrowserDialog.ShowDialog();
				if (folderBrowserDialog.SelectedPath == "")
				{
					MessageBox.Show("請選擇匯出檔案要儲存的資料夾");
					return;
				}
				string str = folderBrowserDialog.SelectedPath + "\\";
				string text = "會員清單_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
				if (dataTable.Rows.Count > 0)
				{
					StreamWriter streamWriter = new StreamWriter(str + text, true, Encoding.Default);
					streamWriter.WriteLine("門市代號,會員號,會員姓名*必填,會員類型*必填,身分證字號,出生年月日*必填請輸入yyyy/MM/dd格式,市內電話號碼*請先輸入單引號「'」(必輸入)，再輸入市內電話號碼,行動電話號碼*請先輸入單引號「'」(必輸入)，再輸入行動電話號碼,地址(縣市)*必填,地址(鄉鎮)*必填,地址(路名)*必填,公司名稱,統一編號*請先輸入單引號「'」(必輸入)，再輸入統一編號");
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						string text2 = "";
						for (int j = 0; j < dataTable.Columns.Count; j++)
						{
							switch (j)
							{
							case 6:
							case 7:
							case 12:
								text2 = text2 + "'" + dataTable.Rows[i][j].ToString() + ",";
								break;
							case 3:
								if (dataTable.Rows[i][3].ToString() == "1")
								{
									text2 += "1 : 一般會員,";
								}
								else if (dataTable.Rows[i][3].ToString() == "2")
								{
									text2 += "2 : 優惠會員(1),";
								}
								else if (dataTable.Rows[i][3].ToString() == "3")
								{
									text2 += "3 : 優惠會員(2),";
								}
								break;
							case 8:
							{
								string sql3 = "SELECT city FROM ADDRCITY where cityno =" + dataTable.Rows[i][8].ToString() + " limit 1";
								string str2 = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql3, null, CommandOperationType.ExecuteScalar));
								text2 = text2 + str2 + ",";
								break;
							}
							case 9:
							{
								string sql2 = "SELECT area FROM ADDRAREA where zipcode =" + dataTable.Rows[i][9].ToString() + " limit 1";
								string text3 = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteScalar));
								text2 = text2 + text3 + " " + dataTable.Rows[i][9].ToString() + ",";
								break;
							}
							default:
								text2 = ((!string.IsNullOrEmpty(dataTable.Rows[i][j].ToString())) ? (text2 + dataTable.Rows[i][j].ToString() + ",") : (text2 + " ,"));
								break;
							}
						}
						streamWriter.WriteLine(text2.TrimEnd(','));
					}
					streamWriter.Close();
					MessageBox.Show(text + " 匯出成功!", "匯出成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show("無會員資料");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_ImportCSV_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "CSV files (*.csv)|*.csv";
			openFileDialog.Title = "請選擇會員檔案";
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("LicenseCode", typeof(string));
			dataTable.Columns.Add("VipNo", typeof(string));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("Type", typeof(string));
			dataTable.Columns.Add("IdNo", typeof(string));
			dataTable.Columns.Add("BirthDate", typeof(string));
			dataTable.Columns.Add("Telphone", typeof(string));
			dataTable.Columns.Add("Mobile", typeof(string));
			dataTable.Columns.Add("City", typeof(string));
			dataTable.Columns.Add("Area", typeof(string));
			dataTable.Columns.Add("Address", typeof(string));
			dataTable.Columns.Add("CompanyName", typeof(string));
			dataTable.Columns.Add("CompanyIdNo", typeof(string));
			try
			{
				int num = 0;
				int num2 = 0;
				string text = "";
				string[] array = File.ReadAllLines(openFileDialog.FileName, Encoding.GetEncoding("big5"));
				for (int i = 1; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(',');
					DataRow dataRow = dataTable.NewRow();
					dataRow["LicenseCode"] = array2[0];
					dataRow["VipNo"] = array2[1];
					dataRow["Name"] = array2[2];
					dataRow["Type"] = array2[3];
					dataRow["IdNo"] = array2[4];
					dataRow["BirthDate"] = array2[5];
					dataRow["Telphone"] = array2[6];
					dataRow["Mobile"] = array2[7];
					dataRow["City"] = array2[8];
					dataRow["Area"] = array2[9];
					dataRow["Address"] = array2[10];
					dataRow["CompanyName"] = array2[11];
					dataRow["CompanyIdNo"] = array2[12];
					dataTable.Rows.Add(dataRow);
					string text2 = "";
					if (array2[0].ToString() == " ")
					{
						text2 += "門市代號(必填), ";
					}
					if (array2[1].ToString() == " ")
					{
						text2 += "會員號(必填), ";
					}
					if (array2[2].ToString() == " ")
					{
						text2 += "會員姓名(必填), ";
					}
					if (array2[3].ToString() == " ")
					{
						text2 += "會員類型(必填), ";
					}
					if (array2[5].ToString() == " ")
					{
						text2 += "出生年月日(必填), ";
					}
					if (array2[8].ToString() == " ")
					{
						text2 += "地址(縣市)(必填), ";
					}
					if (array2[9].ToString() == " ")
					{
						text2 += "地址(鄉鎮區)(必填), ";
					}
					if (array2[10].ToString() == " ")
					{
						text2 += "地址(詳細路名)(必填), ";
					}
					if (text2.Length > 1)
					{
						text2 = "第" + (i + 1) + "行:" + text2.Substring(0, text2.Length - 2) + "\r\n";
						num2++;
						text += text2;
						MessageBox.Show(text2);
						continue;
					}
					try
					{
						string text3 = array2[9].ToString();
						text3 = text3.Substring(text3.Length - 3);
						string text4 = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "cityno", "ADDRAREA", "zipcode = {0}", "", null, new string[1]
						{
							text3
						}, CommandOperationType.ExecuteScalar).ToString();
						string a = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "LicenseCode", " hypos_CUST_RTL", "LicenseCode = {0}", "", null, new string[1]
						{
							array2[0].ToString()
						}, CommandOperationType.ExecuteScalar).ToString();
						string a2 = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "VipNo", " hypos_CUST_RTL", "VipNo = {0}", "", null, new string[1]
						{
							array2[1].ToString()
						}, CommandOperationType.ExecuteScalar).ToString();
						DateTime dateTime = DateTime.Parse(array2[5].ToString());
						string text5 = string.Format("{0}-{1}-{2}", dateTime.Year, dateTime.Month, dateTime.Day);
						string text6 = array2[6].ToString();
						text6 = ((text6.Length <= 6) ? "" : text6.Remove(0, 2));
						string text7 = array2[7].ToString();
						text7 = ((text7.Length <= 6) ? "" : text7.Remove(0, 2));
						string text8 = array2[7].ToString();
						text8 = ((text8.Length <= 6) ? "" : text8.Remove(0, 2));
						if (a != "-1" && a2 != "-1")
						{
							DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET IdNo = {0}, Name = {1}, BirthDate = {2}, Telphone = {3}, Mobile = {4}, Type = {5}, City = {6}, Area = {7}, Address = {8}, CompanyIdNo = {9}, CompanyName = {10}, Status = {11} where VipNo = {12}", new string[13]
							{
								array2[4].ToString(),
								array2[2].ToString(),
								text5,
								text6,
								text7,
								array2[3].ToString().Trim().Substring(0, 1),
								text4,
								text3,
								array2[10].ToString(),
								text8,
								array2[11].ToString(),
								"0",
								array2[1].ToString()
							}, CommandOperationType.ExecuteNonQuery);
							continue;
						}
						string newVipNo = frmNewMember.getNewVipNo();
						string[,] strFieldArray = new string[14, 2]
						{
							{
								"LicenseCode",
								Program.LincenseCode
							},
							{
								"VipNo",
								newVipNo
							},
							{
								"IdNo",
								array2[4].ToString()
							},
							{
								"Name",
								array2[2].ToString()
							},
							{
								"BirthDate",
								text5
							},
							{
								"Telphone",
								text6
							},
							{
								"Mobile",
								text7
							},
							{
								"Type",
								array2[3].ToString().Trim().Substring(0, 1)
							},
							{
								"City",
								text4
							},
							{
								"Area",
								text3
							},
							{
								"Address",
								array2[10].ToString()
							},
							{
								"CompanyIdNo",
								text8
							},
							{
								"CompanyName",
								array2[11].ToString()
							},
							{
								"Status",
								"0"
							}
						};
						DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_CUST_RTL", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
					}
					catch (Exception ex)
					{
						num2++;
						text = string.Concat(text, "匯入第", i + 1, " 筆失敗,請重新檢查!, ", ex, "\r\n");
						MessageBox.Show(text);
					}
				}
				reload();
				num = array.Length - 1;
				string text9 = string.Format("成功:{0}筆, 失敗:{1}筆, 總共:{2}筆", num - num2, num2, num);
				if (text.Length > 1)
				{
					text9 = text9 + "\r\n失敗清單:\r\n" + text;
				}
				new frmImportResult(text9).ShowDialog();
			}
			catch (Exception ex2)
			{
				MessageBox.Show("匯入錯誤，請確認檔案是否正確！" + ex2.Message, "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
			uC_Member1 = new POS_Client.UC_Member();
			uC_Member2 = new POS_Client.UC_Member();
			uC_Member3 = new POS_Client.UC_Member();
			uC_Member4 = new POS_Client.UC_Member();
			uC_Member5 = new POS_Client.UC_Member();
			uC_Member6 = new POS_Client.UC_Member();
			uC_Member7 = new POS_Client.UC_Member();
			uC_Member8 = new POS_Client.UC_Member();
			btn_pageRight = new System.Windows.Forms.Button();
			btn_pageLeft = new System.Windows.Forms.Button();
			l_status = new System.Windows.Forms.Label();
			btn_statusAll = new System.Windows.Forms.Button();
			btn_statusNormal = new System.Windows.Forms.Button();
			btn_statusSuspend = new System.Windows.Forms.Button();
			btn_sortByCreateDate = new System.Windows.Forms.Button();
			l_sortBy = new System.Windows.Forms.Label();
			btn_SortByEditDate = new System.Windows.Forms.Button();
			btn_SortByBuyDate = new System.Windows.Forms.Button();
			l_memberList = new System.Windows.Forms.Label();
			l_batchPrint = new System.Windows.Forms.Label();
			btn_saveToTemp = new System.Windows.Forms.Button();
			btn_viewTempList = new System.Windows.Forms.Button();
			btn_createMember = new System.Windows.Forms.Button();
			btn_importList = new System.Windows.Forms.Button();
			btn_memberSearch = new System.Windows.Forms.Button();
			l_pageInfo = new System.Windows.Forms.Label();
			btn_firstPage = new System.Windows.Forms.Button();
			btn_previousPage = new System.Windows.Forms.Button();
			btn_nextPage = new System.Windows.Forms.Button();
			btn_lastPage = new System.Windows.Forms.Button();
			l_pageNow = new System.Windows.Forms.Label();
			btn_downloadTemplate = new System.Windows.Forms.Button();
			btn_DownLoadVipData = new System.Windows.Forms.Button();
			btn_ImportCSV = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			SuspendLayout();
			pb_virtualKeyBoard.Location = new System.Drawing.Point(918, 620);
			uC_Member1.AutoSize = true;
			uC_Member1.BackColor = System.Drawing.Color.White;
			uC_Member1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member1.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member1.Location = new System.Drawing.Point(65, 175);
			uC_Member1.Margin = new System.Windows.Forms.Padding(0);
			uC_Member1.Name = "uC_Member1";
			uC_Member1.Size = new System.Drawing.Size(425, 95);
			uC_Member1.TabIndex = 33;
			uC_Member2.AutoSize = true;
			uC_Member2.BackColor = System.Drawing.Color.White;
			uC_Member2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member2.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member2.Location = new System.Drawing.Point(490, 175);
			uC_Member2.Margin = new System.Windows.Forms.Padding(0);
			uC_Member2.Name = "uC_Member2";
			uC_Member2.Size = new System.Drawing.Size(425, 95);
			uC_Member2.TabIndex = 34;
			uC_Member3.AutoSize = true;
			uC_Member3.BackColor = System.Drawing.Color.White;
			uC_Member3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member3.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member3.Location = new System.Drawing.Point(65, 270);
			uC_Member3.Margin = new System.Windows.Forms.Padding(0);
			uC_Member3.Name = "uC_Member3";
			uC_Member3.Size = new System.Drawing.Size(425, 95);
			uC_Member3.TabIndex = 35;
			uC_Member4.AutoSize = true;
			uC_Member4.BackColor = System.Drawing.Color.White;
			uC_Member4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member4.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member4.Location = new System.Drawing.Point(490, 270);
			uC_Member4.Margin = new System.Windows.Forms.Padding(0);
			uC_Member4.Name = "uC_Member4";
			uC_Member4.Size = new System.Drawing.Size(425, 95);
			uC_Member4.TabIndex = 36;
			uC_Member5.AutoSize = true;
			uC_Member5.BackColor = System.Drawing.Color.White;
			uC_Member5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member5.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member5.Location = new System.Drawing.Point(65, 365);
			uC_Member5.Margin = new System.Windows.Forms.Padding(0);
			uC_Member5.Name = "uC_Member5";
			uC_Member5.Size = new System.Drawing.Size(425, 95);
			uC_Member5.TabIndex = 37;
			uC_Member6.AutoSize = true;
			uC_Member6.BackColor = System.Drawing.Color.White;
			uC_Member6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member6.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member6.Location = new System.Drawing.Point(490, 365);
			uC_Member6.Margin = new System.Windows.Forms.Padding(0);
			uC_Member6.Name = "uC_Member6";
			uC_Member6.Size = new System.Drawing.Size(425, 95);
			uC_Member6.TabIndex = 38;
			uC_Member7.AutoSize = true;
			uC_Member7.BackColor = System.Drawing.Color.White;
			uC_Member7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member7.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member7.Location = new System.Drawing.Point(65, 460);
			uC_Member7.Margin = new System.Windows.Forms.Padding(0);
			uC_Member7.Name = "uC_Member7";
			uC_Member7.Size = new System.Drawing.Size(425, 95);
			uC_Member7.TabIndex = 39;
			uC_Member8.AutoSize = true;
			uC_Member8.BackColor = System.Drawing.Color.White;
			uC_Member8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member8.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member8.Location = new System.Drawing.Point(490, 460);
			uC_Member8.Margin = new System.Windows.Forms.Padding(0);
			uC_Member8.Name = "uC_Member8";
			uC_Member8.Size = new System.Drawing.Size(425, 95);
			uC_Member8.TabIndex = 40;
			btn_pageRight.FlatAppearance.BorderSize = 0;
			btn_pageRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageRight.Image = POS_Client.Properties.Resources.right;
			btn_pageRight.Location = new System.Drawing.Point(934, 175);
			btn_pageRight.Name = "btn_pageRight";
			btn_pageRight.Size = new System.Drawing.Size(48, 378);
			btn_pageRight.TabIndex = 41;
			btn_pageRight.UseVisualStyleBackColor = true;
			btn_pageRight.Click += new System.EventHandler(btn_pageRight_Click);
			btn_pageLeft.FlatAppearance.BorderSize = 0;
			btn_pageLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageLeft.Image = POS_Client.Properties.Resources.left;
			btn_pageLeft.Location = new System.Drawing.Point(-2, 175);
			btn_pageLeft.Name = "btn_pageLeft";
			btn_pageLeft.Size = new System.Drawing.Size(48, 378);
			btn_pageLeft.TabIndex = 42;
			btn_pageLeft.UseVisualStyleBackColor = true;
			btn_pageLeft.Click += new System.EventHandler(btn_pageLeft_Click);
			l_status.AutoSize = true;
			l_status.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_status.Location = new System.Drawing.Point(65, 134);
			l_status.Name = "l_status";
			l_status.Size = new System.Drawing.Size(41, 20);
			l_status.TabIndex = 43;
			l_status.Text = "狀態";
			btn_statusAll.BackColor = System.Drawing.Color.White;
			btn_statusAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusAll.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusAll.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.Location = new System.Drawing.Point(120, 130);
			btn_statusAll.Name = "btn_statusAll";
			btn_statusAll.Size = new System.Drawing.Size(69, 29);
			btn_statusAll.TabIndex = 44;
			btn_statusAll.Text = "全部";
			btn_statusAll.UseVisualStyleBackColor = false;
			btn_statusAll.Click += new System.EventHandler(btn_statusAll_Click);
			btn_statusNormal.BackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusNormal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusNormal.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusNormal.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusNormal.ForeColor = System.Drawing.Color.White;
			btn_statusNormal.Location = new System.Drawing.Point(197, 130);
			btn_statusNormal.Name = "btn_statusNormal";
			btn_statusNormal.Size = new System.Drawing.Size(69, 29);
			btn_statusNormal.TabIndex = 45;
			btn_statusNormal.Text = "正常";
			btn_statusNormal.UseVisualStyleBackColor = false;
			btn_statusNormal.Click += new System.EventHandler(btn_statusNormal_Click);
			btn_statusSuspend.BackColor = System.Drawing.Color.White;
			btn_statusSuspend.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusSuspend.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusSuspend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusSuspend.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusSuspend.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusSuspend.Location = new System.Drawing.Point(274, 130);
			btn_statusSuspend.Name = "btn_statusSuspend";
			btn_statusSuspend.Size = new System.Drawing.Size(69, 29);
			btn_statusSuspend.TabIndex = 46;
			btn_statusSuspend.Text = "已停用";
			btn_statusSuspend.UseVisualStyleBackColor = false;
			btn_statusSuspend.Click += new System.EventHandler(btn_statusSuspend_Click);
			btn_sortByCreateDate.BackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_sortByCreateDate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_sortByCreateDate.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_sortByCreateDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_sortByCreateDate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_sortByCreateDate.ForeColor = System.Drawing.Color.White;
			btn_sortByCreateDate.Location = new System.Drawing.Point(432, 130);
			btn_sortByCreateDate.Name = "btn_sortByCreateDate";
			btn_sortByCreateDate.Size = new System.Drawing.Size(156, 29);
			btn_sortByCreateDate.TabIndex = 48;
			btn_sortByCreateDate.Text = "建立日期(新→舊)";
			btn_sortByCreateDate.UseVisualStyleBackColor = false;
			btn_sortByCreateDate.Click += new System.EventHandler(btn_sortByCreateDate_Click);
			l_sortBy.AutoSize = true;
			l_sortBy.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_sortBy.Location = new System.Drawing.Point(367, 134);
			l_sortBy.Name = "l_sortBy";
			l_sortBy.Size = new System.Drawing.Size(57, 20);
			l_sortBy.TabIndex = 47;
			l_sortBy.Text = "排序依";
			btn_SortByEditDate.BackColor = System.Drawing.Color.White;
			btn_SortByEditDate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_SortByEditDate.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_SortByEditDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SortByEditDate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SortByEditDate.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_SortByEditDate.Location = new System.Drawing.Point(596, 130);
			btn_SortByEditDate.Name = "btn_SortByEditDate";
			btn_SortByEditDate.Size = new System.Drawing.Size(156, 29);
			btn_SortByEditDate.TabIndex = 49;
			btn_SortByEditDate.Text = "編修日期(新→舊)";
			btn_SortByEditDate.UseVisualStyleBackColor = false;
			btn_SortByEditDate.Click += new System.EventHandler(btn_SortByEditDate_Click);
			btn_SortByBuyDate.BackColor = System.Drawing.Color.White;
			btn_SortByBuyDate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_SortByBuyDate.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_SortByBuyDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SortByBuyDate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SortByBuyDate.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_SortByBuyDate.Location = new System.Drawing.Point(759, 130);
			btn_SortByBuyDate.Name = "btn_SortByBuyDate";
			btn_SortByBuyDate.Size = new System.Drawing.Size(156, 29);
			btn_SortByBuyDate.TabIndex = 50;
			btn_SortByBuyDate.Text = "消費日期(新→舊)";
			btn_SortByBuyDate.UseVisualStyleBackColor = false;
			btn_SortByBuyDate.Click += new System.EventHandler(btn_SortByBuyDate_Click);
			l_memberList.AutoSize = true;
			l_memberList.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_memberList.Image = POS_Client.Properties.Resources.oblique;
			l_memberList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_memberList.Location = new System.Drawing.Point(65, 47);
			l_memberList.Name = "l_memberList";
			l_memberList.Size = new System.Drawing.Size(101, 24);
			l_memberList.TabIndex = 51;
			l_memberList.Text = "   會員清單";
			l_memberList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			l_batchPrint.AutoSize = true;
			l_batchPrint.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_batchPrint.Location = new System.Drawing.Point(65, 93);
			l_batchPrint.Name = "l_batchPrint";
			l_batchPrint.Size = new System.Drawing.Size(137, 20);
			l_batchPrint.TabIndex = 52;
			l_batchPrint.Text = "會員清冊批次列印";
			btn_saveToTemp.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			btn_saveToTemp.FlatAppearance.BorderSize = 0;
			btn_saveToTemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_saveToTemp.Font = new System.Drawing.Font("微軟正黑體", 12f);
			btn_saveToTemp.ForeColor = System.Drawing.Color.White;
			btn_saveToTemp.Location = new System.Drawing.Point(208, 84);
			btn_saveToTemp.Name = "btn_saveToTemp";
			btn_saveToTemp.Size = new System.Drawing.Size(125, 35);
			btn_saveToTemp.TabIndex = 53;
			btn_saveToTemp.Text = "勾選存入暫存";
			btn_saveToTemp.UseVisualStyleBackColor = false;
			btn_saveToTemp.Click += new System.EventHandler(btn_saveToTemp_Click);
			btn_viewTempList.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			btn_viewTempList.FlatAppearance.BorderSize = 0;
			btn_viewTempList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_viewTempList.Font = new System.Drawing.Font("微軟正黑體", 12f);
			btn_viewTempList.ForeColor = System.Drawing.Color.White;
			btn_viewTempList.Location = new System.Drawing.Point(340, 84);
			btn_viewTempList.Name = "btn_viewTempList";
			btn_viewTempList.Size = new System.Drawing.Size(125, 35);
			btn_viewTempList.TabIndex = 54;
			btn_viewTempList.Text = "檢視暫存清單";
			btn_viewTempList.UseVisualStyleBackColor = false;
			btn_viewTempList.Click += new System.EventHandler(btn_viewTempList_Click);
			btn_createMember.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_createMember.FlatAppearance.BorderSize = 0;
			btn_createMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_createMember.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_createMember.ForeColor = System.Drawing.Color.White;
			btn_createMember.Location = new System.Drawing.Point(670, 44);
			btn_createMember.Name = "btn_createMember";
			btn_createMember.Size = new System.Drawing.Size(88, 30);
			btn_createMember.TabIndex = 55;
			btn_createMember.Text = "新建會員";
			btn_createMember.UseVisualStyleBackColor = false;
			btn_createMember.Click += new System.EventHandler(btn_createMember_Click);
			btn_importList.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_importList.FlatAppearance.BorderSize = 0;
			btn_importList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_importList.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_importList.ForeColor = System.Drawing.Color.White;
			btn_importList.Location = new System.Drawing.Point(572, 44);
			btn_importList.Name = "btn_importList";
			btn_importList.Size = new System.Drawing.Size(88, 30);
			btn_importList.TabIndex = 56;
			btn_importList.Text = "匯入清單";
			btn_importList.UseVisualStyleBackColor = false;
			btn_importList.Click += new System.EventHandler(btn_importList_Click);
			btn_memberSearch.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_memberSearch.FlatAppearance.BorderSize = 0;
			btn_memberSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_memberSearch.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_memberSearch.ForeColor = System.Drawing.Color.White;
			btn_memberSearch.Location = new System.Drawing.Point(769, 44);
			btn_memberSearch.Name = "btn_memberSearch";
			btn_memberSearch.Size = new System.Drawing.Size(88, 30);
			btn_memberSearch.TabIndex = 57;
			btn_memberSearch.Text = "會員查詢";
			btn_memberSearch.UseVisualStyleBackColor = false;
			btn_memberSearch.Click += new System.EventHandler(btn_memberSearch_Click);
			l_pageInfo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageInfo.Location = new System.Drawing.Point(167, 49);
			l_pageInfo.Name = "l_pageInfo";
			l_pageInfo.Size = new System.Drawing.Size(301, 20);
			l_pageInfo.TabIndex = 58;
			l_pageInfo.Text = "共{0}筆．{1}頁｜目前在第1頁\r\n";
			l_pageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			btn_firstPage.BackColor = System.Drawing.Color.White;
			btn_firstPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_firstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_firstPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_firstPage.Image = POS_Client.Properties.Resources.first;
			btn_firstPage.Location = new System.Drawing.Point(309, 575);
			btn_firstPage.Name = "btn_firstPage";
			btn_firstPage.Size = new System.Drawing.Size(69, 29);
			btn_firstPage.TabIndex = 59;
			btn_firstPage.UseVisualStyleBackColor = false;
			btn_firstPage.Click += new System.EventHandler(btn_firstPage_Click);
			btn_previousPage.BackColor = System.Drawing.Color.White;
			btn_previousPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_previousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_previousPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_previousPage.Image = POS_Client.Properties.Resources.prev;
			btn_previousPage.Location = new System.Drawing.Point(384, 575);
			btn_previousPage.Name = "btn_previousPage";
			btn_previousPage.Size = new System.Drawing.Size(69, 29);
			btn_previousPage.TabIndex = 60;
			btn_previousPage.UseVisualStyleBackColor = false;
			btn_previousPage.Click += new System.EventHandler(btn_pageLeft_Click);
			btn_nextPage.BackColor = System.Drawing.Color.White;
			btn_nextPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_nextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_nextPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_nextPage.Image = POS_Client.Properties.Resources.next;
			btn_nextPage.Location = new System.Drawing.Point(532, 575);
			btn_nextPage.Name = "btn_nextPage";
			btn_nextPage.Size = new System.Drawing.Size(69, 29);
			btn_nextPage.TabIndex = 62;
			btn_nextPage.UseVisualStyleBackColor = false;
			btn_nextPage.Click += new System.EventHandler(btn_pageRight_Click);
			btn_lastPage.BackColor = System.Drawing.Color.White;
			btn_lastPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_lastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_lastPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_lastPage.Image = POS_Client.Properties.Resources.last;
			btn_lastPage.Location = new System.Drawing.Point(607, 575);
			btn_lastPage.Name = "btn_lastPage";
			btn_lastPage.Size = new System.Drawing.Size(69, 29);
			btn_lastPage.TabIndex = 63;
			btn_lastPage.UseVisualStyleBackColor = false;
			btn_lastPage.Click += new System.EventHandler(btn_lastPage_Click);
			l_pageNow.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			l_pageNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			l_pageNow.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageNow.ForeColor = System.Drawing.Color.White;
			l_pageNow.Location = new System.Drawing.Point(459, 575);
			l_pageNow.Name = "l_pageNow";
			l_pageNow.Size = new System.Drawing.Size(67, 29);
			l_pageNow.TabIndex = 64;
			l_pageNow.Text = "{0}";
			l_pageNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			btn_downloadTemplate.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_downloadTemplate.FlatAppearance.BorderSize = 0;
			btn_downloadTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_downloadTemplate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_downloadTemplate.ForeColor = System.Drawing.Color.White;
			btn_downloadTemplate.Location = new System.Drawing.Point(474, 44);
			btn_downloadTemplate.Name = "btn_downloadTemplate";
			btn_downloadTemplate.Size = new System.Drawing.Size(88, 30);
			btn_downloadTemplate.TabIndex = 56;
			btn_downloadTemplate.Text = "下載範本";
			btn_downloadTemplate.UseVisualStyleBackColor = false;
			btn_downloadTemplate.Click += new System.EventHandler(btn_downloadTemplate_Click);
			btn_DownLoadVipData.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_DownLoadVipData.FlatAppearance.BorderSize = 0;
			btn_DownLoadVipData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_DownLoadVipData.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_DownLoadVipData.ForeColor = System.Drawing.Color.White;
			btn_DownLoadVipData.Location = new System.Drawing.Point(863, 44);
			btn_DownLoadVipData.Name = "btn_DownLoadVipData";
			btn_DownLoadVipData.Size = new System.Drawing.Size(116, 30);
			btn_DownLoadVipData.TabIndex = 65;
			btn_DownLoadVipData.Text = "下載會員清單";
			btn_DownLoadVipData.UseVisualStyleBackColor = false;
			btn_DownLoadVipData.Click += new System.EventHandler(btn_DownLoadVipData_Click);
			btn_ImportCSV.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_ImportCSV.FlatAppearance.BorderSize = 0;
			btn_ImportCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_ImportCSV.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_ImportCSV.ForeColor = System.Drawing.Color.White;
			btn_ImportCSV.Location = new System.Drawing.Point(769, 83);
			btn_ImportCSV.Name = "btn_ImportCSV";
			btn_ImportCSV.Size = new System.Drawing.Size(209, 30);
			btn_ImportCSV.TabIndex = 66;
			btn_ImportCSV.Text = "匯入會員清單CSV";
			btn_ImportCSV.UseVisualStyleBackColor = false;
			btn_ImportCSV.Visible = false;
			btn_ImportCSV.Click += new System.EventHandler(btn_ImportCSV_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(997, 661);
			base.Controls.Add(btn_ImportCSV);
			base.Controls.Add(btn_DownLoadVipData);
			base.Controls.Add(l_pageNow);
			base.Controls.Add(btn_lastPage);
			base.Controls.Add(btn_nextPage);
			base.Controls.Add(btn_previousPage);
			base.Controls.Add(btn_firstPage);
			base.Controls.Add(l_pageInfo);
			base.Controls.Add(btn_memberSearch);
			base.Controls.Add(btn_downloadTemplate);
			base.Controls.Add(btn_importList);
			base.Controls.Add(btn_createMember);
			base.Controls.Add(btn_viewTempList);
			base.Controls.Add(btn_saveToTemp);
			base.Controls.Add(l_batchPrint);
			base.Controls.Add(l_memberList);
			base.Controls.Add(btn_SortByBuyDate);
			base.Controls.Add(btn_SortByEditDate);
			base.Controls.Add(btn_sortByCreateDate);
			base.Controls.Add(l_sortBy);
			base.Controls.Add(btn_statusSuspend);
			base.Controls.Add(btn_statusNormal);
			base.Controls.Add(btn_statusAll);
			base.Controls.Add(l_status);
			base.Controls.Add(btn_pageLeft);
			base.Controls.Add(btn_pageRight);
			base.Controls.Add(uC_Member8);
			base.Controls.Add(uC_Member7);
			base.Controls.Add(uC_Member6);
			base.Controls.Add(uC_Member5);
			base.Controls.Add(uC_Member4);
			base.Controls.Add(uC_Member3);
			base.Controls.Add(uC_Member2);
			base.Controls.Add(uC_Member1);
			base.Name = "frmMemberMangement";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmMemberMangement_Load);
			base.Controls.SetChildIndex(uC_Member1, 0);
			base.Controls.SetChildIndex(uC_Member2, 0);
			base.Controls.SetChildIndex(uC_Member3, 0);
			base.Controls.SetChildIndex(uC_Member4, 0);
			base.Controls.SetChildIndex(uC_Member5, 0);
			base.Controls.SetChildIndex(uC_Member6, 0);
			base.Controls.SetChildIndex(uC_Member7, 0);
			base.Controls.SetChildIndex(uC_Member8, 0);
			base.Controls.SetChildIndex(btn_pageRight, 0);
			base.Controls.SetChildIndex(btn_pageLeft, 0);
			base.Controls.SetChildIndex(l_status, 0);
			base.Controls.SetChildIndex(btn_statusAll, 0);
			base.Controls.SetChildIndex(btn_statusNormal, 0);
			base.Controls.SetChildIndex(btn_statusSuspend, 0);
			base.Controls.SetChildIndex(l_sortBy, 0);
			base.Controls.SetChildIndex(btn_sortByCreateDate, 0);
			base.Controls.SetChildIndex(btn_SortByEditDate, 0);
			base.Controls.SetChildIndex(btn_SortByBuyDate, 0);
			base.Controls.SetChildIndex(l_memberList, 0);
			base.Controls.SetChildIndex(l_batchPrint, 0);
			base.Controls.SetChildIndex(btn_saveToTemp, 0);
			base.Controls.SetChildIndex(btn_viewTempList, 0);
			base.Controls.SetChildIndex(btn_createMember, 0);
			base.Controls.SetChildIndex(btn_importList, 0);
			base.Controls.SetChildIndex(btn_downloadTemplate, 0);
			base.Controls.SetChildIndex(btn_memberSearch, 0);
			base.Controls.SetChildIndex(l_pageInfo, 0);
			base.Controls.SetChildIndex(btn_firstPage, 0);
			base.Controls.SetChildIndex(btn_previousPage, 0);
			base.Controls.SetChildIndex(btn_nextPage, 0);
			base.Controls.SetChildIndex(btn_lastPage, 0);
			base.Controls.SetChildIndex(l_pageNow, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(btn_DownLoadVipData, 0);
			base.Controls.SetChildIndex(btn_ImportCSV, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
