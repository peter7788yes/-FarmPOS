using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmCommdityList : MasterThinForm
	{
		private CommodityInfoforSearch[] ucMembers;

		private DataTable dt;

		private frmMainShopSimple frs;

		private frmMainShopSimpleWithMoney frsm;

		private frmNewInventory frmI;

		private frmNewDeliveryOrder frmD;

		private string commodityName = "";

		private string commodityNum = "";

		private string CommodityClass = "";

		private string[] _strSearchConditionCommodityStatus;

		private int pageNow = 1;

		private int pageTotal = 1;

		private IContainer components;

		private CommodityInfoforSearch uC_Member1;

		private CommodityInfoforSearch uC_Member2;

		private CommodityInfoforSearch uC_Member3;

		private CommodityInfoforSearch uC_Member4;

		private CommodityInfoforSearch uC_Member5;

		private CommodityInfoforSearch uC_Member6;

		private CommodityInfoforSearch uC_Member7;

		private CommodityInfoforSearch uC_Member8;

		private Button btn_pageRight;

		private Button btn_pageLeft;

		private Label l_memberList;

		private Label l_pageInfo;

		private Button btn_firstPage;

		private Button btn_previousPage;

		private Button btn_nextPage;

		private Button btn_lastPage;

		private Label l_pageNow;

		private CommodityInfoforSearch uC_Member9;

		private CommodityInfoforSearch uC_Member10;

		private Label conditionBarcode;

		private Label conditionName;

		private Label conditionClass;

		private Button search;

		public frmCommdityList(frmMainShopSimple frs, string commodityName, string commodityNum, string CommodityClass, string[] strSearchConditionCommodityStatus)
			: base("銷售作業")
		{
			InitializeComponent();
			ucMembers = new CommodityInfoforSearch[10]
			{
				uC_Member1,
				uC_Member2,
				uC_Member3,
				uC_Member4,
				uC_Member5,
				uC_Member6,
				uC_Member7,
				uC_Member8,
				uC_Member9,
				uC_Member10
			};
			this.frs = frs;
			this.commodityName = commodityName;
			this.commodityNum = commodityNum;
			this.CommodityClass = CommodityClass;
			_strSearchConditionCommodityStatus = strSearchConditionCommodityStatus;
		}

		public frmCommdityList(frmMainShopSimpleWithMoney frsm, string commodityName, string commodityNum, string CommodityClass, string[] strSearchConditionCommodityStatus)
			: base("銷售作業")
		{
			InitializeComponent();
			ucMembers = new CommodityInfoforSearch[10]
			{
				uC_Member1,
				uC_Member2,
				uC_Member3,
				uC_Member4,
				uC_Member5,
				uC_Member6,
				uC_Member7,
				uC_Member8,
				uC_Member9,
				uC_Member10
			};
			this.frsm = frsm;
			this.commodityName = commodityName;
			this.commodityNum = commodityNum;
			this.CommodityClass = CommodityClass;
			_strSearchConditionCommodityStatus = strSearchConditionCommodityStatus;
		}

		public frmCommdityList(frmNewInventory frmI, string commodityName, string commodityNum, string CommodityClass, string[] strSearchConditionCommodityStatus)
			: base("進貨作業")
		{
			InitializeComponent();
			ucMembers = new CommodityInfoforSearch[10]
			{
				uC_Member1,
				uC_Member2,
				uC_Member3,
				uC_Member4,
				uC_Member5,
				uC_Member6,
				uC_Member7,
				uC_Member8,
				uC_Member9,
				uC_Member10
			};
			this.frmI = frmI;
			this.commodityName = commodityName;
			this.commodityNum = commodityNum;
			this.CommodityClass = CommodityClass;
			_strSearchConditionCommodityStatus = strSearchConditionCommodityStatus;
		}

		public frmCommdityList(frmNewDeliveryOrder frmD, string commodityName, string commodityNum, string CommodityClass, string[] strSearchConditionCommodityStatus)
			: base("進貨作業")
		{
			InitializeComponent();
			ucMembers = new CommodityInfoforSearch[10]
			{
				uC_Member1,
				uC_Member2,
				uC_Member3,
				uC_Member4,
				uC_Member5,
				uC_Member6,
				uC_Member7,
				uC_Member8,
				uC_Member9,
				uC_Member10
			};
			this.frmD = frmD;
			this.commodityName = commodityName;
			this.commodityNum = commodityNum;
			this.CommodityClass = CommodityClass;
			_strSearchConditionCommodityStatus = strSearchConditionCommodityStatus;
		}

		private void changePage(int page)
		{
			int num = 0;
			pageNow = page;
			for (int i = (pageNow - 1) * 10; i < pageNow * 10; i++)
			{
				if (i < dt.Rows.Count)
				{
					ucMembers[num].setMemberIdNo("");
					ucMembers[num].setMemberVipNo("店內碼:" + dt.Rows[i]["GDSNO"].ToString());
					if (Program.SystemMode != 1)
					{
						ucMembers[num].setcommodityPrice("單位售價:" + dt.Rows[num]["Price"].ToString() + " 元");
					}
					ucMembers[num].setCommodityName(setCommodityName(dt.Rows[i]));
					if (dt.Rows[num]["CLA1NO"].ToString().Equals("0303") && dt.Rows[num]["ISWS"].ToString().Equals("Y"))
					{
						ucMembers[num].setSubsideMoney("補助金額:" + dt.Rows[num]["SubsidyMoney"].ToString() + " 元");
					}
					else
					{
						ucMembers[num].setSubsideMoney("");
					}
					if (dt.Rows[num]["CLA1NO"].ToString() == "0302")
					{
						ucMembers[num].setCommodityClass(string.Concat("農藥: " + dt.Rows[i]["spec"].ToString(), dt.Rows[i]["capacity"].ToString()));
					}
					else if (dt.Rows[num]["CLA1NO"].ToString() == "0303")
					{
						ucMembers[num].setCommodityClass(string.Concat("肥料: " + dt.Rows[i]["spec"].ToString(), dt.Rows[i]["capacity"].ToString()));
					}
					else if (dt.Rows[num]["CLA1NO"].ToString() == "0305" || dt.Rows[i]["CLA1NO"].ToString() == "0308")
					{
						ucMembers[num].setCommodityClass(string.Concat("資材/其他: " + dt.Rows[i]["spec"].ToString(), dt.Rows[i]["capacity"].ToString()));
					}
					else
					{
						ucMembers[num].setCommodityClass(dt.Rows[i]["spec"].ToString() + dt.Rows[i]["capacity"].ToString());
					}
					ucMembers[num].Visible = true;
					ucMembers[num].barcode = dt.Rows[i]["GDSNO"].ToString();
					ucMembers[num].OnClickMember -= new EventHandler(infolistCellForInventory);
					ucMembers[num].OnClickMember -= new EventHandler(infolistCellForNewDeliveryOrder);
					ucMembers[num].OnClickMember -= new EventHandler(viewMemberInfo);
					ucMembers[num].OnClickMember -= new EventHandler(infolistCell);
					if (frmI != null)
					{
						ucMembers[num].OnClickMember += new EventHandler(infolistCellForInventory);
					}
					else if (frmD != null)
					{
						ucMembers[num].OnClickMember += new EventHandler(infolistCellForNewDeliveryOrder);
					}
					else if ("0302".Equals(dt.Rows[i]["CLA1NO"].ToString()) && "Y".Equals(dt.Rows[i]["ISWS"].ToString()))
					{
						ucMembers[num].OnClickMember += new EventHandler(viewMemberInfo);
					}
					else
					{
						ucMembers[num].OnClickMember += new EventHandler(infolistCell);
					}
				}
				else
				{
					ucMembers[num].Visible = false;
				}
				ucMembers[num].BackColor = Control.DefaultBackColor;
				num++;
			}
			l_pageNow.Text = string.Format("{0}", pageNow);
			l_pageInfo.Text = string.Format("共{0}筆．{1}頁｜目前在第{2}頁", dt.Rows.Count, Math.Ceiling((double)dt.Rows.Count / 10.0), pageNow);
		}

		public void viewMemberInfo(object sender, EventArgs s)
		{
			CommodityInfoforSearch commodityInfoforSearch = sender as CommodityInfoforSearch;
			if (Program.SystemMode == 1)
			{
				frshyscorp frshyscorp = new frshyscorp(frs, commodityInfoforSearch.barcode, commodityInfoforSearch.getcommodityName());
				frshyscorp.Location = new Point(base.Location.X, base.Location.Y);
				frshyscorp.setSearchResult(this);
				frshyscorp.ShowDialog();
			}
			else
			{
				frshyscorp frshyscorp2 = new frshyscorp(frsm, commodityInfoforSearch.barcode, commodityInfoforSearch.getcommodityName());
				frshyscorp2.Location = new Point(base.Location.X, base.Location.Y);
				frshyscorp2.setSearchResult(this);
				frshyscorp2.ShowDialog();
			}
		}

		public void infolistCell(object sender, EventArgs e)
		{
			CommodityInfoforSearch commodityInfoforSearch = sender as CommodityInfoforSearch;
			frsm.addOnecommodity(commodityInfoforSearch.barcode, "", "", "", "");
			frsm.Show();
			Close();
		}

		public void infolistCellForInventory(object sender, EventArgs e)
		{
			CommodityInfoforSearch commodityInfoforSearch = sender as CommodityInfoforSearch;
			string strTableName = "hypos_GOODSLST as hg left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo";
			string strWhereClause = "hg.GDSNO ={0} AND ((hg.ISWS ='Y' and hg.CLA1NO ='0302' and hg.licType = hl.licType and hg.domManufId = hl.licNo) OR (hg.ISWS ='N' and hg.CLA1NO ='0302') OR hg.CLA1NO ='0303' OR hg.CLA1NO ='0305' OR hg.CLA1NO ='0308') AND (hl.isDelete='N' or hl.isDelete is null) ";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hg.inventory,hg.GDSNO,hg.spec,hg.capacity,hg.GDName,hg.formCode,hg.CName,hg.contents,hg.brandName,hg.CLA1NO,hg.ISWS", strTableName, strWhereClause, "", null, new string[1]
			{
				commodityInfoforSearch.barcode
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			DialogResult dialogResult = DialogResult.None;
			if (dataTable.Rows.Count > 0 && "0302".Equals(dataTable.Rows[0]["CLA1NO"].ToString()) && "Y".Equals(dataTable.Rows[0]["ISWS"].ToString()))
			{
				dialogResult = new dialogSetBatchNoAndMFGdate(frmI, commodityInfoforSearch.barcode).ShowDialog();
			}
			else
			{
				frmI.addOnecommodity(sender, e, commodityInfoforSearch.barcode);
				frmI.Show();
				Close();
			}
			if (dialogResult == DialogResult.Yes)
			{
				frmI.addOnecommodity(sender, e, commodityInfoforSearch.barcode);
				frmI.Show();
				Close();
			}
		}

		public void infolistCellForNewDeliveryOrder(object sender, EventArgs e)
		{
			CommodityInfoforSearch commodityInfoforSearch = sender as CommodityInfoforSearch;
			string strTableName = "hypos_GOODSLST as hg left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo";
			string strWhereClause = "hg.GDSNO ={0} AND ((hg.ISWS ='Y' and hg.CLA1NO ='0302' and hg.licType = hl.licType and hg.domManufId = hl.licNo) OR (hg.ISWS ='N' and hg.CLA1NO ='0302') OR hg.CLA1NO ='0303' OR hg.CLA1NO ='0305' OR hg.CLA1NO ='0308') AND (hl.isDelete='N' or hl.isDelete is null) ";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hg.inventory,hg.GDSNO,hg.spec,hg.capacity,hg.GDName,hg.formCode,hg.CName,hg.contents,hg.brandName,hg.CLA1NO,hg.ISWS", strTableName, strWhereClause, "", null, new string[1]
			{
				commodityInfoforSearch.barcode
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			DialogResult dialogResult = DialogResult.None;
			if (dataTable.Rows.Count > 0 && "0302".Equals(dataTable.Rows[0]["CLA1NO"].ToString()) && "Y".Equals(dataTable.Rows[0]["ISWS"].ToString()))
			{
				dialogResult = new dialogSetBatchNoAndMFGdate2(frmD, commodityInfoforSearch.barcode).ShowDialog();
			}
			else
			{
				frmD.addOnecommodity(sender, e, commodityInfoforSearch.barcode);
				frmD.Show();
				Close();
			}
			if (dialogResult == DialogResult.Yes)
			{
				frmD.addOnecommodity(sender, e, commodityInfoforSearch.barcode);
				frmD.Show();
				Close();
			}
		}

		private void frmMemberMangement_Load(object sender, EventArgs e)
		{
			try
			{
				string text = " 1=1";
				string text2 = "hypos_GOODSLST as hg";
				if (commodityName != "" && commodityName != null && commodityName != "請輸入商品名稱關鍵字")
				{
					text = text + " and (hg.GDName like '%" + commodityName + "%' or hg.CName like '%" + commodityName + "%')";
					conditionName.Text = conditionName.Text + commodityName + "|";
				}
				if (commodityNum != "" && commodityNum != null && commodityNum != "請刷商品條碼或輸入條碼")
				{
					text = text + " and hg.GDSNO = '" + commodityNum + "'";
					conditionBarcode.Text = conditionBarcode.Text + commodityNum + " |";
				}
				if (Program.SystemMode == 1)
				{
					if (!string.IsNullOrEmpty(CommodityClass))
					{
						text2 += ",HyLicence as hl";
						text += " AND hg.CLA1NO ='0302' and hg.ISWS ='Y' and hg.licType =hl.licType and hg.domManufId =hl.licNo and hl.isDelete='N' and hg.status !='D'";
						if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True") || _strSearchConditionCommodityStatus[2].Equals("True"))
						{
							text += " AND hg.status in ( ";
						}
						if (_strSearchConditionCommodityStatus[0].Equals("True"))
						{
							text += "'U'";
						}
						if (_strSearchConditionCommodityStatus[1].Equals("True"))
						{
							if (_strSearchConditionCommodityStatus[0].Equals("True"))
							{
								text += ",";
							}
							text += "'N'";
						}
						if (_strSearchConditionCommodityStatus[2].Equals("True"))
						{
							if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True"))
							{
								text += ",";
							}
							text += "'S'";
						}
						if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True") || _strSearchConditionCommodityStatus[2].Equals("True"))
						{
							text += " ) ";
						}
						if ("0".Equals(CommodityClass))
						{
							conditionClass.Text += "全部";
						}
						else if ("1".Equals(CommodityClass))
						{
							conditionClass.Text += "農藥";
						}
					}
				}
				else if (!string.IsNullOrEmpty(CommodityClass))
				{
					if ("0".Equals(CommodityClass))
					{
						text2 += " left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo ";
						text += " AND ((hg.CLA1NO ='0302' and hg.ISWS ='Y' and hg.licType =hl.licType and hg.domManufId =hl.licNo) OR (hg.ISWS ='N' and hg.CLA1NO ='0302') OR hg.CLA1NO ='0303' OR hg.CLA1NO ='0305' OR hg.CLA1NO ='0308' ) AND (hl.isDelete='N' or hl.isDelete is null) AND hg.status !='D'";
						if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True") || _strSearchConditionCommodityStatus[2].Equals("True"))
						{
							text += " AND hg.status in ( ";
						}
						if (_strSearchConditionCommodityStatus[0].Equals("True"))
						{
							text += "'U'";
						}
						if (_strSearchConditionCommodityStatus[1].Equals("True"))
						{
							if (_strSearchConditionCommodityStatus[0].Equals("True"))
							{
								text += ",";
							}
							text += "'N'";
						}
						if (_strSearchConditionCommodityStatus[2].Equals("True"))
						{
							if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True"))
							{
								text += ",";
							}
							text += "'S'";
						}
						if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True") || _strSearchConditionCommodityStatus[2].Equals("True"))
						{
							text += " ) ";
						}
						conditionClass.Text += "全部";
					}
					else if ("1".Equals(CommodityClass))
					{
						text2 += " left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo ";
						text += " AND ((hg.CLA1NO ='0302' and hg.ISWS ='Y' and hg.licType =hl.licType and hg.domManufId =hl.licNo) OR (hg.CLA1NO ='0302' and hg.ISWS ='N') ) AND (hl.isDelete='N' or hl.isDelete is null) AND hg.status !='D'";
						text += " AND ( 1=1 ";
						if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True") || _strSearchConditionCommodityStatus[2].Equals("True"))
						{
							text += " AND hg.status in ( ";
						}
						if (_strSearchConditionCommodityStatus[0].Equals("True"))
						{
							text += "'U'";
						}
						if (_strSearchConditionCommodityStatus[1].Equals("True"))
						{
							if (_strSearchConditionCommodityStatus[0].Equals("True"))
							{
								text += ",";
							}
							text += "'N'";
						}
						if (_strSearchConditionCommodityStatus[2].Equals("True"))
						{
							if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True"))
							{
								text += ",";
							}
							text += "'S'";
						}
						if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True") || _strSearchConditionCommodityStatus[2].Equals("True"))
						{
							text += " ) ";
						}
						text += " ) ";
						conditionClass.Text += "農藥";
					}
					else if ("2".Equals(CommodityClass))
					{
						text += " AND hg.CLA1NO ='0303' AND hg.status !='D'";
						if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True") || _strSearchConditionCommodityStatus[2].Equals("True"))
						{
							text += " AND hg.status in ( ";
						}
						if (_strSearchConditionCommodityStatus[0].Equals("True"))
						{
							text += "'U'";
						}
						if (_strSearchConditionCommodityStatus[1].Equals("True"))
						{
							if (_strSearchConditionCommodityStatus[0].Equals("True"))
							{
								text += ",";
							}
							text += "'N'";
						}
						if (_strSearchConditionCommodityStatus[2].Equals("True"))
						{
							if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True"))
							{
								text += ",";
							}
							text += "'S'";
						}
						if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True") || _strSearchConditionCommodityStatus[2].Equals("True"))
						{
							text += " ) ";
						}
						conditionClass.Text += "肥料";
					}
					else if ("3".Equals(CommodityClass))
					{
						text += " AND (hg.CLA1NO ='0305' or hg.CLA1NO ='0308') AND hg.status !='D'";
						if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True") || _strSearchConditionCommodityStatus[2].Equals("True"))
						{
							text += " AND hg.status in ( ";
						}
						if (_strSearchConditionCommodityStatus[0].Equals("True"))
						{
							text += "'U'";
						}
						if (_strSearchConditionCommodityStatus[1].Equals("True"))
						{
							if (_strSearchConditionCommodityStatus[0].Equals("True"))
							{
								text += ",";
							}
							text += "'N'";
						}
						if (_strSearchConditionCommodityStatus[2].Equals("True"))
						{
							if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True"))
							{
								text += ",";
							}
							text += "'S'";
						}
						if (_strSearchConditionCommodityStatus[0].Equals("True") || _strSearchConditionCommodityStatus[1].Equals("True") || _strSearchConditionCommodityStatus[2].Equals("True"))
						{
							text += " ) ";
						}
						conditionClass.Text += "資材/其他";
					}
				}
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hg.GDSNO,hg.CLA1NO,hg.spec,hg.capacity,hg.GDName,hg.formCode,hg.contents,hg.CName,hg.brandName,hg.Price,hg.ISWS,hg.SubsidyMoney", text2, text, "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				pageTotal = (int)Math.Ceiling((double)dt.Rows.Count / 10.0);
				changePage(1);
			}
			catch (Exception)
			{
			}
		}

		private void search_Click(object sender, EventArgs e)
		{
			if (frmI != null)
			{
				switchForm(new frmCommoditySearch(frmI));
			}
			else if (frs != null || frsm != null)
			{
				if (Program.SystemMode == 1)
				{
					switchForm(new frmCommoditySearch(frs));
				}
				else
				{
					switchForm(new frmCommoditySearch(frsm));
				}
			}
			else
			{
				switchForm(new frmCommoditySearch(frmD));
			}
		}

		private void btn_pageRight_Click(object sender, EventArgs e)
		{
			if (pageNow < pageTotal)
			{
				changePage(pageNow + 1);
			}
		}

		private void btn_pageLeft_Click(object sender, EventArgs e)
		{
			if (pageNow > 1)
			{
				changePage(pageNow - 1);
			}
		}

		private void btn_firstPage_Click(object sender, EventArgs e)
		{
			if (pageNow > 1)
			{
				changePage(1);
			}
		}

		private void btn_lastPage_Click(object sender, EventArgs e)
		{
			if (pageNow < pageTotal)
			{
				changePage(pageTotal);
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
				if (text.LastIndexOf("-") > 0)
				{
					text = text.Substring(0, text.LastIndexOf("-")) + "]";
				}
			}
			return text;
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
			uC_Member1 = new POS_Client.CommodityInfoforSearch();
			uC_Member2 = new POS_Client.CommodityInfoforSearch();
			uC_Member3 = new POS_Client.CommodityInfoforSearch();
			uC_Member4 = new POS_Client.CommodityInfoforSearch();
			uC_Member5 = new POS_Client.CommodityInfoforSearch();
			uC_Member6 = new POS_Client.CommodityInfoforSearch();
			uC_Member7 = new POS_Client.CommodityInfoforSearch();
			uC_Member8 = new POS_Client.CommodityInfoforSearch();
			btn_pageRight = new System.Windows.Forms.Button();
			btn_pageLeft = new System.Windows.Forms.Button();
			l_memberList = new System.Windows.Forms.Label();
			l_pageInfo = new System.Windows.Forms.Label();
			btn_firstPage = new System.Windows.Forms.Button();
			btn_previousPage = new System.Windows.Forms.Button();
			btn_nextPage = new System.Windows.Forms.Button();
			btn_lastPage = new System.Windows.Forms.Button();
			l_pageNow = new System.Windows.Forms.Label();
			uC_Member9 = new POS_Client.CommodityInfoforSearch();
			uC_Member10 = new POS_Client.CommodityInfoforSearch();
			conditionBarcode = new System.Windows.Forms.Label();
			conditionName = new System.Windows.Forms.Label();
			conditionClass = new System.Windows.Forms.Label();
			search = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			SuspendLayout();
			uC_Member1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member1.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member1.Location = new System.Drawing.Point(89, 88);
			uC_Member1.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Member1.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Member1.Name = "uC_Member1";
			uC_Member1.Size = new System.Drawing.Size(398, 102);
			uC_Member1.TabIndex = 33;
			uC_Member2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member2.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member2.Location = new System.Drawing.Point(495, 88);
			uC_Member2.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Member2.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Member2.Name = "uC_Member2";
			uC_Member2.Size = new System.Drawing.Size(398, 102);
			uC_Member2.TabIndex = 34;
			uC_Member3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member3.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member3.Location = new System.Drawing.Point(89, 189);
			uC_Member3.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Member3.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Member3.Name = "uC_Member3";
			uC_Member3.Size = new System.Drawing.Size(398, 102);
			uC_Member3.TabIndex = 35;
			uC_Member4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member4.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member4.Location = new System.Drawing.Point(495, 189);
			uC_Member4.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Member4.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Member4.Name = "uC_Member4";
			uC_Member4.Size = new System.Drawing.Size(398, 102);
			uC_Member4.TabIndex = 36;
			uC_Member5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member5.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member5.Location = new System.Drawing.Point(89, 290);
			uC_Member5.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Member5.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Member5.Name = "uC_Member5";
			uC_Member5.Size = new System.Drawing.Size(398, 102);
			uC_Member5.TabIndex = 37;
			uC_Member6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member6.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member6.Location = new System.Drawing.Point(495, 290);
			uC_Member6.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Member6.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Member6.Name = "uC_Member6";
			uC_Member6.Size = new System.Drawing.Size(398, 102);
			uC_Member6.TabIndex = 38;
			uC_Member7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member7.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member7.Location = new System.Drawing.Point(89, 391);
			uC_Member7.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Member7.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Member7.Name = "uC_Member7";
			uC_Member7.Size = new System.Drawing.Size(398, 102);
			uC_Member7.TabIndex = 39;
			uC_Member8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member8.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member8.Location = new System.Drawing.Point(495, 391);
			uC_Member8.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Member8.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Member8.Name = "uC_Member8";
			uC_Member8.Size = new System.Drawing.Size(398, 102);
			uC_Member8.TabIndex = 40;
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
			l_memberList.AutoSize = true;
			l_memberList.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_memberList.Image = POS_Client.Properties.Resources.oblique;
			l_memberList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_memberList.Location = new System.Drawing.Point(12, 52);
			l_memberList.Name = "l_memberList";
			l_memberList.Size = new System.Drawing.Size(101, 24);
			l_memberList.TabIndex = 51;
			l_memberList.Text = "   商品查詢";
			l_pageInfo.AutoSize = true;
			l_pageInfo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageInfo.Location = new System.Drawing.Point(658, 52);
			l_pageInfo.Name = "l_pageInfo";
			l_pageInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			l_pageInfo.Size = new System.Drawing.Size(216, 20);
			l_pageInfo.TabIndex = 58;
			l_pageInfo.Text = "共{0}筆．{1}頁｜目前在第1頁\r\n";
			l_pageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			btn_firstPage.BackColor = System.Drawing.Color.White;
			btn_firstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_firstPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_firstPage.Location = new System.Drawing.Point(308, 598);
			btn_firstPage.Name = "btn_firstPage";
			btn_firstPage.Size = new System.Drawing.Size(69, 29);
			btn_firstPage.TabIndex = 59;
			btn_firstPage.Text = "｜＜＜";
			btn_firstPage.UseVisualStyleBackColor = false;
			btn_firstPage.Click += new System.EventHandler(btn_firstPage_Click);
			btn_previousPage.BackColor = System.Drawing.Color.White;
			btn_previousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_previousPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_previousPage.Location = new System.Drawing.Point(383, 598);
			btn_previousPage.Name = "btn_previousPage";
			btn_previousPage.Size = new System.Drawing.Size(69, 29);
			btn_previousPage.TabIndex = 60;
			btn_previousPage.Text = "上一頁";
			btn_previousPage.UseVisualStyleBackColor = false;
			btn_previousPage.Click += new System.EventHandler(btn_pageLeft_Click);
			btn_nextPage.BackColor = System.Drawing.Color.White;
			btn_nextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_nextPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_nextPage.Location = new System.Drawing.Point(554, 598);
			btn_nextPage.Name = "btn_nextPage";
			btn_nextPage.Size = new System.Drawing.Size(69, 29);
			btn_nextPage.TabIndex = 62;
			btn_nextPage.Text = "下一頁";
			btn_nextPage.UseVisualStyleBackColor = false;
			btn_nextPage.Click += new System.EventHandler(btn_pageRight_Click);
			btn_lastPage.BackColor = System.Drawing.Color.White;
			btn_lastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_lastPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_lastPage.Location = new System.Drawing.Point(629, 598);
			btn_lastPage.Name = "btn_lastPage";
			btn_lastPage.Size = new System.Drawing.Size(69, 29);
			btn_lastPage.TabIndex = 63;
			btn_lastPage.Text = "＞＞｜";
			btn_lastPage.UseVisualStyleBackColor = false;
			btn_lastPage.Click += new System.EventHandler(btn_lastPage_Click);
			l_pageNow.BackColor = System.Drawing.Color.Red;
			l_pageNow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			l_pageNow.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageNow.ForeColor = System.Drawing.Color.White;
			l_pageNow.Location = new System.Drawing.Point(458, 598);
			l_pageNow.Name = "l_pageNow";
			l_pageNow.Size = new System.Drawing.Size(90, 29);
			l_pageNow.TabIndex = 64;
			l_pageNow.Text = "第{0}頁";
			l_pageNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			uC_Member9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member9.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member9.Location = new System.Drawing.Point(89, 492);
			uC_Member9.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Member9.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Member9.Name = "uC_Member9";
			uC_Member9.Size = new System.Drawing.Size(398, 102);
			uC_Member9.TabIndex = 65;
			uC_Member10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Member10.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Member10.Location = new System.Drawing.Point(495, 491);
			uC_Member10.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Member10.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Member10.Name = "uC_Member10";
			uC_Member10.Size = new System.Drawing.Size(398, 102);
			uC_Member10.TabIndex = 66;
			conditionBarcode.AutoSize = true;
			conditionBarcode.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			conditionBarcode.Location = new System.Drawing.Point(127, 52);
			conditionBarcode.Name = "conditionBarcode";
			conditionBarcode.Size = new System.Drawing.Size(97, 25);
			conditionBarcode.TabIndex = 67;
			conditionBarcode.Text = "商品條碼:";
			conditionName.AutoSize = true;
			conditionName.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			conditionName.Location = new System.Drawing.Point(340, 51);
			conditionName.Name = "conditionName";
			conditionName.Size = new System.Drawing.Size(97, 25);
			conditionName.TabIndex = 68;
			conditionName.Text = "商品名稱:";
			conditionClass.AutoSize = true;
			conditionClass.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			conditionClass.Location = new System.Drawing.Point(502, 52);
			conditionClass.Name = "conditionClass";
			conditionClass.Size = new System.Drawing.Size(97, 25);
			conditionClass.TabIndex = 69;
			conditionClass.Text = "商品類型:";
			search.BackColor = System.Drawing.Color.FromArgb(54, 168, 182);
			search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			search.Font = new System.Drawing.Font("微軟正黑體", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			search.ForeColor = System.Drawing.Color.White;
			search.Location = new System.Drawing.Point(880, 42);
			search.Name = "search";
			search.Size = new System.Drawing.Size(94, 36);
			search.TabIndex = 70;
			search.Text = "重新查詢";
			search.UseVisualStyleBackColor = false;
			search.Click += new System.EventHandler(search_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(search);
			base.Controls.Add(conditionClass);
			base.Controls.Add(conditionName);
			base.Controls.Add(conditionBarcode);
			base.Controls.Add(uC_Member10);
			base.Controls.Add(uC_Member9);
			base.Controls.Add(l_pageNow);
			base.Controls.Add(btn_lastPage);
			base.Controls.Add(btn_nextPage);
			base.Controls.Add(btn_previousPage);
			base.Controls.Add(btn_firstPage);
			base.Controls.Add(l_pageInfo);
			base.Controls.Add(l_memberList);
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
			base.Name = "frmCommdityList";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmMemberMangement_Load);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
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
			base.Controls.SetChildIndex(l_memberList, 0);
			base.Controls.SetChildIndex(l_pageInfo, 0);
			base.Controls.SetChildIndex(btn_firstPage, 0);
			base.Controls.SetChildIndex(btn_previousPage, 0);
			base.Controls.SetChildIndex(btn_nextPage, 0);
			base.Controls.SetChildIndex(btn_lastPage, 0);
			base.Controls.SetChildIndex(l_pageNow, 0);
			base.Controls.SetChildIndex(uC_Member9, 0);
			base.Controls.SetChildIndex(uC_Member10, 0);
			base.Controls.SetChildIndex(conditionBarcode, 0);
			base.Controls.SetChildIndex(conditionName, 0);
			base.Controls.SetChildIndex(conditionClass, 0);
			base.Controls.SetChildIndex(search, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
