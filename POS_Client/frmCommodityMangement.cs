using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmCommodityMangement : MasterThinForm
	{
		private ucCommodityInfo[] ucCommoditys;

		private int pageNow = 1;

		public int pageTotal = 1;

		public DataTable dt;

		private string status = "";

		private string type = "";

		private string fromTable = "";

		private string selectString = "hg.GDSNO,hg.barcode,hg.ISWS,hg.CLA1NO,hg.GDNAME,hg.Price,hg.spec,hg.contents,hg.capacity,hg.brandName,hg.CName,hg.formCode,hg.status,hg.SubsidyMoney";

		private string orderByString = "hg.GDSNO,hg.barcode,hg.CreateDate,hg.GDName";

		private bool isWithMoney;

		private IContainer components;

		private ucCommodityInfo uC_Commodity2;

		private ucCommodityInfo uC_Commodity3;

		private ucCommodityInfo uC_Commodity4;

		private ucCommodityInfo uC_Commodity5;

		private ucCommodityInfo uC_Commodity6;

		private ucCommodityInfo uC_Commodity7;

		private ucCommodityInfo uC_Commodity8;

		private Button btn_pageRight;

		private Button btn_pageLeft;

		private Label l_status;

		private Button btn_statusAll;

		private Button btn_statusUse;

		private Button btn_statusNoused;

		private Button btn_AllType;

		private Label l_sortBy;

		private Button btn_type0302;

		private Button btn_type0303;

		private Button btn_memberSearch;

		private Label l_pageInfo;

		private Button btn_firstPage;

		private Button btn_previousPage;

		private Button btn_nextPage;

		private Button btn_lastPage;

		private Label l_pageNow;

		public Label l_memberList;

		private Button btn_type0305;

		private Button btn_type0308;

		private Button btn_statusStop;

		private ucCommodityInfo uC_Commodity1;

		private Button btn_createCommodity;

		private Button btn_viewTempList;

		private Button btn_saveToTemp;

		private Label l_batchPrint;

		private FlowLayoutPanel flowLayoutPanel1;

		private Button btn_statusDenied;

		private Panel type_panel;

		public frmCommodityMangement()
		{
			InitializeComponent();
			ucCommoditys = new ucCommodityInfo[8]
			{
				uC_Commodity1,
				uC_Commodity2,
				uC_Commodity3,
				uC_Commodity4,
				uC_Commodity5,
				uC_Commodity6,
				uC_Commodity7,
				uC_Commodity8
			};
			if (Program.SystemMode == 1)
			{
				setMasterFormName("商品條碼列印");
				btn_createCommodity.Visible = false;
				type_panel.Visible = false;
				fromTable = "hypos_GOODSLST hg, HyLicence as hl";
				type = "hg.CLA1NO ='0302' AND hg.ISWS ='Y' AND hg.licType = hl.licType AND hg.domManufId = hl.licNo AND hl.isDelete='N'";
			}
			else
			{
				setMasterFormName("商品管理");
				fromTable = "hypos_GOODSLST hg left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo";
				type = "((hg.CLA1NO ='0302' and hg.ISWS ='Y' and hg.licType =hl.licType and hg.domManufId =hl.licNo) OR (hg.CLA1NO ='0302' and hg.ISWS ='N') OR hg.CLA1NO ='0303' OR hg.CLA1NO ='0305' OR hg.CLA1NO ='0308') AND (hl.isDelete='N' or hl.isDelete is null)";
			}
			if (Program.RoleType == 1)
			{
				btn_createCommodity.Visible = false;
			}
			ucCommodityInfo[] array = ucCommoditys;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnClickCommodity += new EventHandler(viewCommodityInfo);
			}
		}

		private void frmMemberMangement_Load(object sender, EventArgs e)
		{
			if (Program.SystemMode == 0)
			{
				isWithMoney = true;
				btn_statusUse_Click(sender, e);
			}
			else
			{
				btn_statusNoused_Click(sender, e);
			}
		}

		public void viewCommodityInfo(object barcode, EventArgs s)
		{
			if (Program.SystemMode == 0 && (Program.RoleType == -1 || Program.RoleType == 0) && !hasSelectedCommodity())
			{
				switchForm(new frmEditCommodity(barcode.ToString()));
			}
		}

		private void btn_saveToTemp_Click(object sender, EventArgs e)
		{
			bool flag = false;
			ucCommodityInfo[] array = ucCommoditys;
			foreach (ucCommodityInfo ucCommodityInfo in array)
			{
				if (ucCommodityInfo.isChecked())
				{
					flag = true;
					if (!Program.commodityTemp.Contains(ucCommodityInfo.getGDSNO()))
					{
						Program.commodityTemp.Add(ucCommodityInfo.getGDSNO());
					}
					ucCommodityInfo.checkCommodity(false);
					ucCommodityInfo.BackColor = Color.White;
				}
			}
			if (!flag)
			{
				AutoClosingMessageBox.Show("無勾選商品");
			}
			else
			{
				AutoClosingMessageBox.Show("勾選已存入暫存");
			}
		}

		private void btn_viewTempList_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				if (Program.commodityTemp.Count == 0)
				{
					AutoClosingMessageBox.Show("無暫存商品");
				}
				else
				{
					new frmBatchPrintCommodity().ShowDialog();
				}
			}
		}

		private bool hasSelectedCommodity()
		{
			ucCommodityInfo[] array = ucCommoditys;
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

		public void changePage(int page)
		{
			int num = 0;
			pageNow = page;
			for (int i = (pageNow - 1) * 8; i < pageNow * 8; i++)
			{
				if (i < dt.Rows.Count)
				{
					ucCommoditys[num].setGDSNO(dt.Rows[i]["GDSNO"].ToString());
					ucCommoditys[num].setBarcode(dt.Rows[i]["barcode"].ToString());
					ucCommoditys[num].setPrice(dt.Rows[i]["Price"].ToString());
					ucCommoditys[num].setCommodityName(setCommodityName(dt.Rows[i]));
					if (dt.Rows[i]["CLA1NO"].ToString().Equals("0303") && dt.Rows[i]["ISWS"].ToString().Equals("Y"))
					{
						ucCommoditys[num].setSubsideMoney("補助金額:" + dt.Rows[i]["SubsidyMoney"].ToString() + " 元");
					}
					else
					{
						ucCommoditys[num].setSubsideMoney("");
					}
					if (dt.Rows[num]["CLA1NO"].ToString() == "0302")
					{
						ucCommoditys[num].setCommodityClass(string.Concat("農藥: " + dt.Rows[i]["spec"].ToString(), dt.Rows[i]["contents"].ToString(), dt.Rows[i]["capacity"].ToString()));
					}
					else if (dt.Rows[i]["CLA1NO"].ToString() == "0303")
					{
						ucCommoditys[num].setCommodityClass(string.Concat("肥料: " + dt.Rows[i]["spec"].ToString(), dt.Rows[i]["contents"].ToString(), dt.Rows[i]["capacity"].ToString()));
					}
					else if (dt.Rows[i]["CLA1NO"].ToString() == "0305")
					{
						ucCommoditys[num].setCommodityClass(string.Concat("資材: " + dt.Rows[i]["spec"].ToString(), dt.Rows[i]["contents"].ToString(), dt.Rows[i]["capacity"].ToString()));
					}
					else if (dt.Rows[i]["CLA1NO"].ToString() == "0308")
					{
						ucCommoditys[num].setCommodityClass(string.Concat("其他: " + dt.Rows[i]["spec"].ToString(), dt.Rows[i]["contents"].ToString(), dt.Rows[i]["capacity"].ToString()));
					}
					else
					{
						ucCommoditys[num].setCommodityClass(dt.Rows[i]["spec"].ToString() + dt.Rows[i]["contents"].ToString() + dt.Rows[i]["capacity"].ToString());
					}
					ucCommoditys[num].Visible = true;
				}
				else
				{
					ucCommoditys[num].Visible = false;
				}
				ucCommoditys[num].checkCommodity(false);
				ucCommoditys[num].BackColor = Color.White;
				num++;
			}
			pageTotal = (int)Math.Ceiling((double)dt.Rows.Count / 8.0);
			l_pageNow.Text = string.Format("{0}", pageNow);
			l_pageInfo.Text = string.Format("共{0}筆．{1}頁｜目前在第{2}頁", dt.Rows.Count, Math.Ceiling((double)dt.Rows.Count / 8.0), pageNow);
		}

		private void btn_AllType_Click(object sender, EventArgs e)
		{
			if (hasSelectedCommodity())
			{
				return;
			}
			string strWhereClause = "";
			if (isWithMoney)
			{
				type = "((hg.CLA1NO ='0302' and hg.ISWS ='Y' and hg.licType =hl.licType and hg.domManufId =hl.licNo) OR (hg.CLA1NO ='0302' and hg.ISWS ='N') OR hg.CLA1NO ='0303' OR hg.CLA1NO ='0305' OR hg.CLA1NO ='0308') AND (hl.isDelete='N' or hl.isDelete is null)";
				strWhereClause = ("".Equals(status) ? type : (type + " and " + status));
			}
			else
			{
				type = "";
				if (!status.Equals(""))
				{
					strWhereClause = status;
				}
			}
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectString, fromTable, strWhereClause, orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
			btn_type0302.BackColor = Color.White;
			btn_type0302.ForeColor = Color.FromArgb(247, 106, 45);
			btn_type0303.BackColor = Color.White;
			btn_type0303.ForeColor = Color.FromArgb(247, 106, 45);
			btn_type0305.BackColor = Color.White;
			btn_type0305.ForeColor = Color.FromArgb(247, 106, 45);
			btn_type0308.BackColor = Color.White;
			btn_type0308.ForeColor = Color.FromArgb(247, 106, 45);
			btn_AllType.BackColor = Color.FromArgb(247, 106, 45);
			btn_AllType.ForeColor = Color.White;
		}

		private void btn_type0302_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				if (isWithMoney)
				{
					type = "((hg.CLA1NO ='0302' and hg.ISWS ='Y' and hg.licType =hl.licType and hg.domManufId =hl.licNo) OR (hg.CLA1NO ='0302' and hg.ISWS ='N')) AND (hl.isDelete='N' or hl.isDelete is null)";
				}
				else
				{
					type = "hg.CLA1NO ='0302'";
				}
				string strWhereClause = "".Equals(status) ? type : (type + " and " + status);
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectString, fromTable, strWhereClause, orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_type0302.BackColor = Color.FromArgb(247, 106, 45);
				btn_type0302.ForeColor = Color.White;
				btn_type0303.BackColor = Color.White;
				btn_type0303.ForeColor = Color.FromArgb(247, 106, 45);
				btn_type0305.BackColor = Color.White;
				btn_type0305.ForeColor = Color.FromArgb(247, 106, 45);
				btn_type0308.BackColor = Color.White;
				btn_type0308.ForeColor = Color.FromArgb(247, 106, 45);
				btn_AllType.BackColor = Color.White;
				btn_AllType.ForeColor = Color.FromArgb(247, 106, 45);
			}
		}

		private void btn_type0303_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				type = "hg.CLA1NO ='0303'";
				string strWhereClause = "".Equals(status) ? type : (type + " and " + status);
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectString, fromTable, strWhereClause, orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_type0303.BackColor = Color.FromArgb(247, 106, 45);
				btn_type0303.ForeColor = Color.White;
				btn_type0302.BackColor = Color.White;
				btn_type0302.ForeColor = Color.FromArgb(247, 106, 45);
				btn_type0305.BackColor = Color.White;
				btn_type0305.ForeColor = Color.FromArgb(247, 106, 45);
				btn_type0308.BackColor = Color.White;
				btn_type0308.ForeColor = Color.FromArgb(247, 106, 45);
				btn_AllType.BackColor = Color.White;
				btn_AllType.ForeColor = Color.FromArgb(247, 106, 45);
			}
		}

		private void btn_type0305_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				type = "hg.CLA1NO ='0305'";
				string strWhereClause = "".Equals(status) ? type : (type + " and " + status);
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectString, fromTable, strWhereClause, orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_type0305.BackColor = Color.FromArgb(247, 106, 45);
				btn_type0305.ForeColor = Color.White;
				btn_type0302.BackColor = Color.White;
				btn_type0302.ForeColor = Color.FromArgb(247, 106, 45);
				btn_type0303.BackColor = Color.White;
				btn_type0303.ForeColor = Color.FromArgb(247, 106, 45);
				btn_type0308.BackColor = Color.White;
				btn_type0308.ForeColor = Color.FromArgb(247, 106, 45);
				btn_AllType.BackColor = Color.White;
				btn_AllType.ForeColor = Color.FromArgb(247, 106, 45);
			}
		}

		private void btn_type0308_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				type = "hg.CLA1NO ='0308'";
				string strWhereClause = "".Equals(status) ? type : (type + " and " + status);
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectString, fromTable, strWhereClause, orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_type0308.BackColor = Color.FromArgb(247, 106, 45);
				btn_type0308.ForeColor = Color.White;
				btn_type0302.BackColor = Color.White;
				btn_type0302.ForeColor = Color.FromArgb(247, 106, 45);
				btn_type0303.BackColor = Color.White;
				btn_type0303.ForeColor = Color.FromArgb(247, 106, 45);
				btn_type0305.BackColor = Color.White;
				btn_type0305.ForeColor = Color.FromArgb(247, 106, 45);
				btn_AllType.BackColor = Color.White;
				btn_AllType.ForeColor = Color.FromArgb(247, 106, 45);
			}
		}

		private void btn_statusAll_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				status = "";
				string strWhereClause = "";
				if (!type.Equals(""))
				{
					strWhereClause = type;
				}
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectString, fromTable, strWhereClause, orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_statusAll.ForeColor = Color.White;
				btn_statusAll.BackColor = Color.FromArgb(247, 106, 45);
				btn_statusUse.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusUse.BackColor = Color.White;
				btn_statusNoused.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusNoused.BackColor = Color.White;
				btn_statusStop.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusStop.BackColor = Color.White;
				btn_statusDenied.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusDenied.BackColor = Color.White;
			}
		}

		private void btn_statusUse_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				status = "hg.status='U'";
				string strWhereClause = "".Equals(type) ? status : (type + " and " + status);
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectString, fromTable, strWhereClause, orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_statusUse.ForeColor = Color.White;
				btn_statusUse.BackColor = Color.FromArgb(247, 106, 45);
				btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusAll.BackColor = Color.White;
				btn_statusNoused.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusNoused.BackColor = Color.White;
				btn_statusStop.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusStop.BackColor = Color.White;
				btn_statusDenied.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusDenied.BackColor = Color.White;
			}
		}

		private void btn_statusNoused_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				status = "hg.status='N'";
				string strWhereClause = "".Equals(type) ? status : (type + " and " + status);
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectString, fromTable, strWhereClause, orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_statusNoused.ForeColor = Color.White;
				btn_statusNoused.BackColor = Color.FromArgb(247, 106, 45);
				btn_statusUse.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusUse.BackColor = Color.White;
				btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusAll.BackColor = Color.White;
				btn_statusStop.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusStop.BackColor = Color.White;
				btn_statusDenied.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusDenied.BackColor = Color.White;
			}
		}

		private void btn_statusStop_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				status = "hg.status='S'";
				string strWhereClause = "".Equals(type) ? status : (type + " and " + status);
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectString, fromTable, strWhereClause, orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_statusStop.ForeColor = Color.White;
				btn_statusStop.BackColor = Color.FromArgb(247, 106, 45);
				btn_statusUse.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusUse.BackColor = Color.White;
				btn_statusNoused.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusNoused.BackColor = Color.White;
				btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusAll.BackColor = Color.White;
				btn_statusDenied.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusDenied.BackColor = Color.White;
			}
		}

		private void btn_statusDenied_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				status = "hg.status='D'";
				string strWhereClause = "".Equals(type) ? status : (type + " and " + status);
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectString, fromTable, strWhereClause, orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changePage(1);
				btn_statusDenied.ForeColor = Color.White;
				btn_statusDenied.BackColor = Color.FromArgb(247, 106, 45);
				btn_statusUse.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusUse.BackColor = Color.White;
				btn_statusNoused.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusNoused.BackColor = Color.White;
				btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusAll.BackColor = Color.White;
				btn_statusStop.ForeColor = Color.FromArgb(247, 106, 45);
				btn_statusStop.BackColor = Color.White;
			}
		}

		private void btn_createMember_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				switchForm(new frmNewMember());
			}
		}

		private void btn_pageRight_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity() && pageNow < pageTotal)
			{
				changePage(pageNow + 1);
			}
		}

		private void btn_pageLeft_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity() && pageNow > 1)
			{
				changePage(pageNow - 1);
			}
		}

		private void btn_lastPage_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity() && pageNow < pageTotal)
			{
				changePage(pageTotal);
			}
		}

		private void btn_firstPage_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity() && pageNow > 1)
			{
				changePage(1);
			}
		}

		private void btn_memberSearch_Click(object sender, EventArgs e)
		{
			if (!hasSelectedCommodity())
			{
				new frmSearchCommodity().ShowDialog(this);
			}
		}

		private string setCommodityName(DataRow row)
		{
			string text = row["GDName"].ToString();
			string text2 = row["formCode"].ToString();
			string text3 = row["CName"].ToString();
			string text4 = row["contents"].ToString();
			string text5 = row["brandName"].ToString();
			string[] array = new string[2]
			{
				text3,
				text2
			};
			string[] array2 = new string[2]
			{
				text4,
				text5
			};
			if (!string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text3) || !string.IsNullOrEmpty(text4) || !string.IsNullOrEmpty(text5))
			{
				text += "[";
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i]))
					{
						text = text + array[i] + "-";
					}
				}
				if (text.LastIndexOf("-") > 0)
				{
					text = text.Substring(0, text.LastIndexOf("-")) + " . ";
				}
				for (int j = 0; j < array2.Length; j++)
				{
					if (!string.IsNullOrEmpty(array2[j]))
					{
						text = text + array2[j] + "-";
					}
				}
				text = ((text.LastIndexOf("-") <= 0) ? (text + "]") : (text.Substring(0, text.LastIndexOf("-")) + "]"));
			}
			return text;
		}

		private void btn_createCommodity_Click(object sender, EventArgs e)
		{
			switchForm(new frmNewCommodity());
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
			uC_Commodity2 = new POS_Client.ucCommodityInfo();
			uC_Commodity3 = new POS_Client.ucCommodityInfo();
			uC_Commodity4 = new POS_Client.ucCommodityInfo();
			uC_Commodity5 = new POS_Client.ucCommodityInfo();
			uC_Commodity6 = new POS_Client.ucCommodityInfo();
			uC_Commodity7 = new POS_Client.ucCommodityInfo();
			uC_Commodity8 = new POS_Client.ucCommodityInfo();
			btn_pageRight = new System.Windows.Forms.Button();
			btn_pageLeft = new System.Windows.Forms.Button();
			l_status = new System.Windows.Forms.Label();
			btn_statusAll = new System.Windows.Forms.Button();
			btn_statusUse = new System.Windows.Forms.Button();
			btn_statusNoused = new System.Windows.Forms.Button();
			btn_AllType = new System.Windows.Forms.Button();
			l_sortBy = new System.Windows.Forms.Label();
			btn_type0302 = new System.Windows.Forms.Button();
			btn_type0303 = new System.Windows.Forms.Button();
			l_memberList = new System.Windows.Forms.Label();
			btn_memberSearch = new System.Windows.Forms.Button();
			l_pageInfo = new System.Windows.Forms.Label();
			btn_firstPage = new System.Windows.Forms.Button();
			btn_previousPage = new System.Windows.Forms.Button();
			btn_nextPage = new System.Windows.Forms.Button();
			btn_lastPage = new System.Windows.Forms.Button();
			l_pageNow = new System.Windows.Forms.Label();
			btn_type0305 = new System.Windows.Forms.Button();
			btn_type0308 = new System.Windows.Forms.Button();
			btn_statusStop = new System.Windows.Forms.Button();
			uC_Commodity1 = new POS_Client.ucCommodityInfo();
			btn_createCommodity = new System.Windows.Forms.Button();
			btn_viewTempList = new System.Windows.Forms.Button();
			btn_saveToTemp = new System.Windows.Forms.Button();
			l_batchPrint = new System.Windows.Forms.Label();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			btn_statusDenied = new System.Windows.Forms.Button();
			type_panel = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			flowLayoutPanel1.SuspendLayout();
			type_panel.SuspendLayout();
			SuspendLayout();
			uC_Commodity2.AutoSize = true;
			uC_Commodity2.BackColor = System.Drawing.Color.White;
			uC_Commodity2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity2.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity2.Location = new System.Drawing.Point(495, 175);
			uC_Commodity2.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity2.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity2.Name = "uC_Commodity2";
			uC_Commodity2.Size = new System.Drawing.Size(398, 102);
			uC_Commodity2.TabIndex = 34;
			uC_Commodity3.AutoSize = true;
			uC_Commodity3.BackColor = System.Drawing.Color.White;
			uC_Commodity3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity3.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity3.Location = new System.Drawing.Point(89, 276);
			uC_Commodity3.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity3.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity3.Name = "uC_Commodity3";
			uC_Commodity3.Size = new System.Drawing.Size(398, 102);
			uC_Commodity3.TabIndex = 35;
			uC_Commodity4.AutoSize = true;
			uC_Commodity4.BackColor = System.Drawing.Color.White;
			uC_Commodity4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity4.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity4.Location = new System.Drawing.Point(495, 276);
			uC_Commodity4.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity4.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity4.Name = "uC_Commodity4";
			uC_Commodity4.Size = new System.Drawing.Size(398, 102);
			uC_Commodity4.TabIndex = 36;
			uC_Commodity5.AutoSize = true;
			uC_Commodity5.BackColor = System.Drawing.Color.White;
			uC_Commodity5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity5.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity5.Location = new System.Drawing.Point(89, 377);
			uC_Commodity5.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity5.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity5.Name = "uC_Commodity5";
			uC_Commodity5.Size = new System.Drawing.Size(398, 102);
			uC_Commodity5.TabIndex = 37;
			uC_Commodity6.AutoSize = true;
			uC_Commodity6.BackColor = System.Drawing.Color.White;
			uC_Commodity6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity6.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity6.Location = new System.Drawing.Point(495, 377);
			uC_Commodity6.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity6.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity6.Name = "uC_Commodity6";
			uC_Commodity6.Size = new System.Drawing.Size(398, 102);
			uC_Commodity6.TabIndex = 38;
			uC_Commodity7.AutoSize = true;
			uC_Commodity7.BackColor = System.Drawing.Color.White;
			uC_Commodity7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity7.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity7.Location = new System.Drawing.Point(89, 478);
			uC_Commodity7.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity7.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity7.Name = "uC_Commodity7";
			uC_Commodity7.Size = new System.Drawing.Size(398, 102);
			uC_Commodity7.TabIndex = 39;
			uC_Commodity8.AutoSize = true;
			uC_Commodity8.BackColor = System.Drawing.Color.White;
			uC_Commodity8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity8.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity8.Location = new System.Drawing.Point(495, 478);
			uC_Commodity8.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity8.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity8.Name = "uC_Commodity8";
			uC_Commodity8.Size = new System.Drawing.Size(398, 102);
			uC_Commodity8.TabIndex = 40;
			btn_pageRight.FlatAppearance.BorderSize = 0;
			btn_pageRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageRight.Image = POS_Client.Properties.Resources.right;
			btn_pageRight.Location = new System.Drawing.Point(934, 209);
			btn_pageRight.Name = "btn_pageRight";
			btn_pageRight.Size = new System.Drawing.Size(48, 371);
			btn_pageRight.TabIndex = 41;
			btn_pageRight.UseVisualStyleBackColor = true;
			btn_pageRight.Click += new System.EventHandler(btn_pageRight_Click);
			btn_pageLeft.FlatAppearance.BorderSize = 0;
			btn_pageLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageLeft.Image = POS_Client.Properties.Resources.left;
			btn_pageLeft.Location = new System.Drawing.Point(1, 209);
			btn_pageLeft.Name = "btn_pageLeft";
			btn_pageLeft.Size = new System.Drawing.Size(48, 371);
			btn_pageLeft.TabIndex = 42;
			btn_pageLeft.UseVisualStyleBackColor = true;
			btn_pageLeft.Click += new System.EventHandler(btn_pageLeft_Click);
			l_status.AutoSize = true;
			l_status.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_status.Location = new System.Drawing.Point(52, 135);
			l_status.Name = "l_status";
			l_status.Size = new System.Drawing.Size(41, 20);
			l_status.TabIndex = 43;
			l_status.Text = "狀態";
			btn_statusAll.BackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusAll.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusAll.ForeColor = System.Drawing.Color.White;
			btn_statusAll.Location = new System.Drawing.Point(101, 131);
			btn_statusAll.Name = "btn_statusAll";
			btn_statusAll.Size = new System.Drawing.Size(69, 29);
			btn_statusAll.TabIndex = 44;
			btn_statusAll.Text = "全部";
			btn_statusAll.UseVisualStyleBackColor = false;
			btn_statusAll.Click += new System.EventHandler(btn_statusAll_Click);
			btn_statusUse.BackColor = System.Drawing.Color.White;
			btn_statusUse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusUse.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusUse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusUse.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusUse.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusUse.Location = new System.Drawing.Point(178, 131);
			btn_statusUse.Name = "btn_statusUse";
			btn_statusUse.Size = new System.Drawing.Size(69, 29);
			btn_statusUse.TabIndex = 45;
			btn_statusUse.Text = "使用中";
			btn_statusUse.UseVisualStyleBackColor = false;
			btn_statusUse.Click += new System.EventHandler(btn_statusUse_Click);
			btn_statusNoused.BackColor = System.Drawing.Color.White;
			btn_statusNoused.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusNoused.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusNoused.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusNoused.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusNoused.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusNoused.Location = new System.Drawing.Point(255, 131);
			btn_statusNoused.Name = "btn_statusNoused";
			btn_statusNoused.Size = new System.Drawing.Size(69, 29);
			btn_statusNoused.TabIndex = 46;
			btn_statusNoused.Text = "未使用";
			btn_statusNoused.UseVisualStyleBackColor = false;
			btn_statusNoused.Click += new System.EventHandler(btn_statusNoused_Click);
			btn_AllType.BackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_AllType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_AllType.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_AllType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_AllType.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_AllType.ForeColor = System.Drawing.Color.White;
			btn_AllType.Location = new System.Drawing.Point(72, 5);
			btn_AllType.Name = "btn_AllType";
			btn_AllType.Size = new System.Drawing.Size(75, 29);
			btn_AllType.TabIndex = 48;
			btn_AllType.Text = "全部";
			btn_AllType.UseVisualStyleBackColor = false;
			btn_AllType.Click += new System.EventHandler(btn_AllType_Click);
			l_sortBy.AutoSize = true;
			l_sortBy.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_sortBy.Location = new System.Drawing.Point(25, 9);
			l_sortBy.Name = "l_sortBy";
			l_sortBy.Size = new System.Drawing.Size(41, 20);
			l_sortBy.TabIndex = 47;
			l_sortBy.Text = "類型";
			btn_type0302.BackColor = System.Drawing.Color.White;
			btn_type0302.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0302.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0302.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_type0302.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_type0302.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0302.Location = new System.Drawing.Point(153, 5);
			btn_type0302.Name = "btn_type0302";
			btn_type0302.Size = new System.Drawing.Size(67, 29);
			btn_type0302.TabIndex = 49;
			btn_type0302.Text = "農藥";
			btn_type0302.UseVisualStyleBackColor = false;
			btn_type0302.Click += new System.EventHandler(btn_type0302_Click);
			btn_type0303.BackColor = System.Drawing.Color.White;
			btn_type0303.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0303.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0303.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_type0303.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_type0303.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0303.Location = new System.Drawing.Point(226, 5);
			btn_type0303.Name = "btn_type0303";
			btn_type0303.Size = new System.Drawing.Size(69, 29);
			btn_type0303.TabIndex = 50;
			btn_type0303.Text = "肥料";
			btn_type0303.UseVisualStyleBackColor = false;
			btn_type0303.Click += new System.EventHandler(btn_type0303_Click);
			l_memberList.AutoSize = true;
			l_memberList.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_memberList.Image = POS_Client.Properties.Resources.oblique;
			l_memberList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_memberList.Location = new System.Drawing.Point(85, 48);
			l_memberList.Name = "l_memberList";
			l_memberList.Size = new System.Drawing.Size(101, 24);
			l_memberList.TabIndex = 51;
			l_memberList.Text = "   商品清單";
			l_memberList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			btn_memberSearch.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_memberSearch.FlatAppearance.BorderSize = 0;
			btn_memberSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_memberSearch.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_memberSearch.ForeColor = System.Drawing.Color.White;
			btn_memberSearch.Location = new System.Drawing.Point(610, 3);
			btn_memberSearch.Name = "btn_memberSearch";
			btn_memberSearch.Size = new System.Drawing.Size(88, 30);
			btn_memberSearch.TabIndex = 57;
			btn_memberSearch.Text = "商品查詢";
			btn_memberSearch.UseVisualStyleBackColor = false;
			btn_memberSearch.Click += new System.EventHandler(btn_memberSearch_Click);
			l_pageInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_pageInfo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageInfo.Location = new System.Drawing.Point(23, 8);
			l_pageInfo.Name = "l_pageInfo";
			l_pageInfo.Size = new System.Drawing.Size(487, 20);
			l_pageInfo.TabIndex = 58;
			l_pageInfo.Text = "共{0}筆．{1}頁｜目前在第1頁\r\n";
			l_pageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			btn_firstPage.BackColor = System.Drawing.Color.White;
			btn_firstPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_firstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_firstPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_firstPage.Image = POS_Client.Properties.Resources.first;
			btn_firstPage.Location = new System.Drawing.Point(309, 588);
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
			btn_previousPage.Location = new System.Drawing.Point(384, 588);
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
			btn_nextPage.Location = new System.Drawing.Point(532, 588);
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
			btn_lastPage.Location = new System.Drawing.Point(607, 588);
			btn_lastPage.Name = "btn_lastPage";
			btn_lastPage.Size = new System.Drawing.Size(69, 29);
			btn_lastPage.TabIndex = 63;
			btn_lastPage.UseVisualStyleBackColor = false;
			btn_lastPage.Click += new System.EventHandler(btn_lastPage_Click);
			l_pageNow.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			l_pageNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			l_pageNow.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageNow.ForeColor = System.Drawing.Color.White;
			l_pageNow.Location = new System.Drawing.Point(459, 588);
			l_pageNow.Name = "l_pageNow";
			l_pageNow.Size = new System.Drawing.Size(67, 29);
			l_pageNow.TabIndex = 64;
			l_pageNow.Text = "{0}";
			l_pageNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			btn_type0305.BackColor = System.Drawing.Color.White;
			btn_type0305.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0305.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0305.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_type0305.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_type0305.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0305.Location = new System.Drawing.Point(301, 5);
			btn_type0305.Name = "btn_type0305";
			btn_type0305.Size = new System.Drawing.Size(69, 29);
			btn_type0305.TabIndex = 65;
			btn_type0305.Text = "資材";
			btn_type0305.UseVisualStyleBackColor = false;
			btn_type0305.Click += new System.EventHandler(btn_type0305_Click);
			btn_type0308.BackColor = System.Drawing.Color.White;
			btn_type0308.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0308.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0308.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_type0308.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_type0308.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_type0308.Location = new System.Drawing.Point(376, 5);
			btn_type0308.Name = "btn_type0308";
			btn_type0308.Size = new System.Drawing.Size(69, 29);
			btn_type0308.TabIndex = 66;
			btn_type0308.Text = "其他";
			btn_type0308.UseVisualStyleBackColor = false;
			btn_type0308.Click += new System.EventHandler(btn_type0308_Click);
			btn_statusStop.BackColor = System.Drawing.Color.White;
			btn_statusStop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusStop.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusStop.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusStop.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusStop.Location = new System.Drawing.Point(330, 131);
			btn_statusStop.Name = "btn_statusStop";
			btn_statusStop.Size = new System.Drawing.Size(69, 29);
			btn_statusStop.TabIndex = 67;
			btn_statusStop.Text = "已停用";
			btn_statusStop.UseVisualStyleBackColor = false;
			btn_statusStop.Click += new System.EventHandler(btn_statusStop_Click);
			uC_Commodity1.AutoSize = true;
			uC_Commodity1.BackColor = System.Drawing.Color.White;
			uC_Commodity1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity1.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity1.Location = new System.Drawing.Point(89, 175);
			uC_Commodity1.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity1.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity1.Name = "uC_Commodity1";
			uC_Commodity1.Size = new System.Drawing.Size(398, 102);
			uC_Commodity1.TabIndex = 68;
			btn_createCommodity.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_createCommodity.FlatAppearance.BorderSize = 0;
			btn_createCommodity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_createCommodity.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_createCommodity.ForeColor = System.Drawing.Color.White;
			btn_createCommodity.Location = new System.Drawing.Point(516, 3);
			btn_createCommodity.Name = "btn_createCommodity";
			btn_createCommodity.Size = new System.Drawing.Size(88, 30);
			btn_createCommodity.TabIndex = 57;
			btn_createCommodity.Text = "新建商品";
			btn_createCommodity.UseVisualStyleBackColor = false;
			btn_createCommodity.Click += new System.EventHandler(btn_createCommodity_Click);
			btn_viewTempList.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			btn_viewTempList.FlatAppearance.BorderSize = 0;
			btn_viewTempList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_viewTempList.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_viewTempList.ForeColor = System.Drawing.Color.White;
			btn_viewTempList.Location = new System.Drawing.Point(363, 92);
			btn_viewTempList.Name = "btn_viewTempList";
			btn_viewTempList.Size = new System.Drawing.Size(101, 24);
			btn_viewTempList.TabIndex = 71;
			btn_viewTempList.Text = "檢視暫存清單";
			btn_viewTempList.UseVisualStyleBackColor = false;
			btn_viewTempList.Click += new System.EventHandler(btn_viewTempList_Click);
			btn_saveToTemp.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			btn_saveToTemp.FlatAppearance.BorderSize = 0;
			btn_saveToTemp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_saveToTemp.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_saveToTemp.ForeColor = System.Drawing.Color.White;
			btn_saveToTemp.Location = new System.Drawing.Point(243, 92);
			btn_saveToTemp.Name = "btn_saveToTemp";
			btn_saveToTemp.Size = new System.Drawing.Size(101, 24);
			btn_saveToTemp.TabIndex = 70;
			btn_saveToTemp.Text = "勾選存入暫存";
			btn_saveToTemp.UseVisualStyleBackColor = false;
			btn_saveToTemp.Click += new System.EventHandler(btn_saveToTemp_Click);
			l_batchPrint.AutoSize = true;
			l_batchPrint.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_batchPrint.Location = new System.Drawing.Point(87, 94);
			l_batchPrint.Name = "l_batchPrint";
			l_batchPrint.Size = new System.Drawing.Size(137, 20);
			l_batchPrint.TabIndex = 69;
			l_batchPrint.Text = "商品條碼批次列印";
			flowLayoutPanel1.Controls.Add(btn_memberSearch);
			flowLayoutPanel1.Controls.Add(btn_createCommodity);
			flowLayoutPanel1.Controls.Add(l_pageInfo);
			flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			flowLayoutPanel1.Location = new System.Drawing.Point(192, 46);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(701, 40);
			flowLayoutPanel1.TabIndex = 72;
			btn_statusDenied.BackColor = System.Drawing.Color.White;
			btn_statusDenied.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusDenied.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusDenied.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusDenied.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusDenied.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusDenied.Location = new System.Drawing.Point(405, 131);
			btn_statusDenied.Name = "btn_statusDenied";
			btn_statusDenied.Size = new System.Drawing.Size(69, 29);
			btn_statusDenied.TabIndex = 67;
			btn_statusDenied.Text = "已禁用";
			btn_statusDenied.UseVisualStyleBackColor = false;
			btn_statusDenied.Click += new System.EventHandler(btn_statusDenied_Click);
			type_panel.Controls.Add(l_sortBy);
			type_panel.Controls.Add(btn_AllType);
			type_panel.Controls.Add(btn_type0302);
			type_panel.Controls.Add(btn_type0303);
			type_panel.Controls.Add(btn_type0305);
			type_panel.Controls.Add(btn_type0308);
			type_panel.Location = new System.Drawing.Point(479, 125);
			type_panel.Name = "type_panel";
			type_panel.Size = new System.Drawing.Size(452, 38);
			type_panel.TabIndex = 73;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(type_panel);
			base.Controls.Add(flowLayoutPanel1);
			base.Controls.Add(btn_viewTempList);
			base.Controls.Add(btn_saveToTemp);
			base.Controls.Add(l_batchPrint);
			base.Controls.Add(uC_Commodity1);
			base.Controls.Add(btn_statusDenied);
			base.Controls.Add(btn_statusStop);
			base.Controls.Add(l_pageNow);
			base.Controls.Add(btn_lastPage);
			base.Controls.Add(btn_nextPage);
			base.Controls.Add(btn_previousPage);
			base.Controls.Add(btn_firstPage);
			base.Controls.Add(l_memberList);
			base.Controls.Add(btn_statusNoused);
			base.Controls.Add(btn_statusUse);
			base.Controls.Add(btn_statusAll);
			base.Controls.Add(l_status);
			base.Controls.Add(btn_pageLeft);
			base.Controls.Add(btn_pageRight);
			base.Controls.Add(uC_Commodity8);
			base.Controls.Add(uC_Commodity7);
			base.Controls.Add(uC_Commodity6);
			base.Controls.Add(uC_Commodity5);
			base.Controls.Add(uC_Commodity4);
			base.Controls.Add(uC_Commodity3);
			base.Controls.Add(uC_Commodity2);
			base.Name = "frmCommodityMangement";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmMemberMangement_Load);
			base.Controls.SetChildIndex(uC_Commodity2, 0);
			base.Controls.SetChildIndex(uC_Commodity3, 0);
			base.Controls.SetChildIndex(uC_Commodity4, 0);
			base.Controls.SetChildIndex(uC_Commodity5, 0);
			base.Controls.SetChildIndex(uC_Commodity6, 0);
			base.Controls.SetChildIndex(uC_Commodity7, 0);
			base.Controls.SetChildIndex(uC_Commodity8, 0);
			base.Controls.SetChildIndex(btn_pageRight, 0);
			base.Controls.SetChildIndex(btn_pageLeft, 0);
			base.Controls.SetChildIndex(l_status, 0);
			base.Controls.SetChildIndex(btn_statusAll, 0);
			base.Controls.SetChildIndex(btn_statusUse, 0);
			base.Controls.SetChildIndex(btn_statusNoused, 0);
			base.Controls.SetChildIndex(l_memberList, 0);
			base.Controls.SetChildIndex(btn_firstPage, 0);
			base.Controls.SetChildIndex(btn_previousPage, 0);
			base.Controls.SetChildIndex(btn_nextPage, 0);
			base.Controls.SetChildIndex(btn_lastPage, 0);
			base.Controls.SetChildIndex(l_pageNow, 0);
			base.Controls.SetChildIndex(btn_statusStop, 0);
			base.Controls.SetChildIndex(btn_statusDenied, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(uC_Commodity1, 0);
			base.Controls.SetChildIndex(l_batchPrint, 0);
			base.Controls.SetChildIndex(btn_saveToTemp, 0);
			base.Controls.SetChildIndex(btn_viewTempList, 0);
			base.Controls.SetChildIndex(flowLayoutPanel1, 0);
			base.Controls.SetChildIndex(type_panel, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			flowLayoutPanel1.ResumeLayout(false);
			type_panel.ResumeLayout(false);
			type_panel.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
