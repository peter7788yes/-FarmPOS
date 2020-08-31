using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmCommoditySearch : MasterThinForm
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

		private CommodityInfoforSearch[] ucCommodities;

		private frmMainShopSimple fms;

		private frmMainShopSimpleWithMoney frms;

		private frmNewInventory frmI;

		private frmNewDeliveryOrder frmD;

		private IContainer components;

		private TextBox commodityNum;

		private Button search;

		private Label label5;

		private Button cancel;

		private Button reset;

		private Button guide;

		private CommodityInfoforSearch uC_Commodity1;

		private CommodityInfoforSearch uC_Commodity2;

		private CommodityInfoforSearch uC_Commodity3;

		private CommodityInfoforSearch uC_Commodity4;

		private CommodityInfoforSearch uC_Commodity5;

		private CommodityInfoforSearch uC_Commodity6;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel3;

		private Label label4;

		private ComboBox commodityClass;

		private Panel panel2;

		private Label label7;

		private TextBox commodityName;

		private Panel panel1;

		private Label label1;

		private Panel panel4;

		private Label label2;

		private FlowLayoutPanel flowLayoutPanel1;

		private MyCheckBox myCheckBox1;

		private MyCheckBox myCheckBox2;

		private MyCheckBox myCheckBox3;

		public frmCommoditySearch()
			: base("收銀作業")
		{
			InitializeComponent();
		}

		public frmCommoditySearch(frmMainShopSimple fms)
			: base("收銀作業")
		{
			this.fms = fms;
			InitializeComponent();
			commodityClass.Items.Clear();
			commodityClass.Items.Add("全部");
			commodityClass.Items.Add("農藥");
			commodityClass.SelectedIndex = 0;
			ucCommodities = new CommodityInfoforSearch[6]
			{
				uC_Commodity1,
				uC_Commodity2,
				uC_Commodity3,
				uC_Commodity4,
				uC_Commodity5,
				uC_Commodity6
			};
			CommodityInfoforSearch[] array = ucCommodities;
			foreach (CommodityInfoforSearch obj in array)
			{
				obj.OnClickMember += new EventHandler(viewCommodityInfo);
				obj.Visible = false;
			}
			int num = 0;
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct ds.barcode", "hypos_detail_sell as ds left outer join hypos_GOODSLST as hg,HyLicence as hl on ds.barcode = hg.GDSNO", "hg.ISWS ='Y' and hg.CLA1NO ='0302' and hg.licType = hl.licType and hg.domManufId = hl.licNo and hl.isDelete='N' and hg.status !='D'", "ds.sellDeatialId desc limit 6", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				string[] strWhereParameterArray = new string[1]
				{
					row["barcode"].ToString()
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_GOODSLST", "GDSNO = {0} ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count <= 0)
				{
					continue;
				}
				foreach (DataRow row2 in dataTable2.Rows)
				{
					ucCommodities[num].setMemberIdNo("");
					ucCommodities[num].setMemberVipNo("店內碼:" + row2["GDSNO"].ToString());
					ucCommodities[num].setCommodityName(setCommodityName(row2));
					ucCommodities[num].setCommodityClass(row2["spec"].ToString() + row2["capacity"].ToString());
					ucCommodities[num].Visible = true;
					ucCommodities[num].barcode = row2["GDSNO"].ToString();
					ucCommodities[num].setfms(this.fms);
					ucCommodities[num].Visible = true;
					num++;
				}
			}
		}

		public frmCommoditySearch(frmMainShopSimpleWithMoney frms)
			: base("收銀作業")
		{
			this.frms = frms;
			InitializeComponent();
			commodityClass.SelectedIndex = 0;
			ucCommodities = new CommodityInfoforSearch[6]
			{
				uC_Commodity1,
				uC_Commodity2,
				uC_Commodity3,
				uC_Commodity4,
				uC_Commodity5,
				uC_Commodity6
			};
			CommodityInfoforSearch[] array = ucCommodities;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Visible = false;
			}
			int num = 0;
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "distinct barcode", "hypos_detail_sell", "", "sellDeatialId desc limit 50", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			int num2 = 1;
			foreach (DataRow row in dataTable.Rows)
			{
				if (num2 < 7)
				{
					string[] strWhereParameterArray = new string[1]
					{
						row["barcode"].ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hg.*", "hypos_GOODSLST as hg left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo", "hg.GDSNO ={0} and (hl.isDelete='N' or hl.isDelete is null) and hg.status !='D'", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count > 0)
					{
						foreach (DataRow row2 in dataTable2.Rows)
						{
							ucCommodities[num].setMemberIdNo("");
							ucCommodities[num].setMemberVipNo("店內碼:" + row2["GDSNO"].ToString());
							ucCommodities[num].setCommodityName(setCommodityName(row2));
							if (row2["CLA1NO"].ToString().Equals("0303") && row2["ISWS"].ToString().Equals("Y"))
							{
								ucCommodities[num].setSubsideMoney("補助金額:" + row2["SubsidyMoney"].ToString() + " 元");
							}
							else
							{
								ucCommodities[num].setSubsideMoney("");
							}
							ucCommodities[num].setCommodityClass(row2["spec"].ToString() + row2["capacity"].ToString());
							ucCommodities[num].Visible = true;
							ucCommodities[num].barcode = row2["GDSNO"].ToString();
							ucCommodities[num].Visible = true;
							if ("0302".Equals(row2["CLA1NO"].ToString()) && "Y".Equals(row2["ISWS"].ToString()))
							{
								ucCommodities[num].OnClickMember += new EventHandler(viewCommodityInfo);
							}
							else
							{
								ucCommodities[num].OnClickMember += new EventHandler(infolistCell);
							}
							if (Program.SystemMode != 1)
							{
								ucCommodities[num].setcommodityPrice("單位售價:" + row2["Price"].ToString() + " 元");
							}
							num++;
						}
					}
				}
				if (num2 == 6)
				{
					break;
				}
				num2++;
			}
		}

		public frmCommoditySearch(frmNewInventory frmI)
			: base("進貨作業")
		{
			this.frmI = frmI;
			InitializeComponent();
			commodityClass.SelectedIndex = 0;
			label5.Text = "   最近進貨商品";
			guide.Hide();
			ucCommodities = new CommodityInfoforSearch[6]
			{
				uC_Commodity1,
				uC_Commodity2,
				uC_Commodity3,
				uC_Commodity4,
				uC_Commodity5,
				uC_Commodity6
			};
			CommodityInfoforSearch[] array = ucCommodities;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Visible = false;
			}
			int num = 0;
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "pd.GDSNO,pm.CreateDate", "hypos_PurchaseGoods_Detail as pd left outer join hypos_PurchaseGoods_Master as pm on pd.PurchaseNo = pm.PurchaseNo", "", "pm.CreateDate desc limit 10", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			int num2 = 1;
			foreach (DataRow row in dataTable.Rows)
			{
				if (num2 < 7)
				{
					string[] strWhereParameterArray = new string[1]
					{
						row["GDSNO"].ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hg.*", "hypos_GOODSLST as hg left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo", "hg.GDSNO ={0} and (hl.isDelete='N' or hl.isDelete is null) and hg.status !='D'", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count > 0)
					{
						foreach (DataRow row2 in dataTable2.Rows)
						{
							ucCommodities[num].setMemberIdNo("");
							ucCommodities[num].setMemberVipNo("店內碼:" + row2["GDSNO"].ToString());
							ucCommodities[num].setCommodityName(setCommodityName(row2));
							ucCommodities[num].setCommodityClass(row2["spec"].ToString() + row2["capacity"].ToString());
							ucCommodities[num].Visible = true;
							ucCommodities[num].barcode = row2["GDSNO"].ToString();
							ucCommodities[num].Visible = true;
							ucCommodities[num].OnClickMember += new EventHandler(infolistCellForInventory);
							num++;
						}
					}
				}
				if (num2 == 6)
				{
					break;
				}
				num2++;
			}
		}

		public frmCommoditySearch(frmNewDeliveryOrder frmD)
			: base("出貨作業")
		{
			this.frmD = frmD;
			InitializeComponent();
			commodityClass.SelectedIndex = 0;
			label5.Text = "   最近出貨商品";
			guide.Hide();
			ucCommodities = new CommodityInfoforSearch[6]
			{
				uC_Commodity1,
				uC_Commodity2,
				uC_Commodity3,
				uC_Commodity4,
				uC_Commodity5,
				uC_Commodity6
			};
			CommodityInfoforSearch[] array = ucCommodities;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Visible = false;
			}
			int num = 0;
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "barcode,DeliveryDate", "hypos_DeliveryGoods_Detail", "", "DeliveryDate desc limit 10", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			int num2 = 1;
			foreach (DataRow row in dataTable.Rows)
			{
				if (num2 < 7)
				{
					string[] strWhereParameterArray = new string[1]
					{
						row["barcode"].ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hg.*", "hypos_GOODSLST as hg left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo", "hg.GDSNO ={0} and (hl.isDelete='N' or hl.isDelete is null) and hg.status !='D'", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count > 0)
					{
						foreach (DataRow row2 in dataTable2.Rows)
						{
							ucCommodities[num].setMemberIdNo("");
							ucCommodities[num].setMemberVipNo("店內碼:" + row2["GDSNO"].ToString());
							ucCommodities[num].setCommodityName(setCommodityName(row2));
							ucCommodities[num].setCommodityClass(row2["spec"].ToString() + row2["capacity"].ToString());
							ucCommodities[num].Visible = true;
							ucCommodities[num].barcode = row2["GDSNO"].ToString();
							ucCommodities[num].Visible = true;
							ucCommodities[num].OnClickMember += new EventHandler(infolistCellForDelivery);
							num++;
						}
					}
				}
				if (num2 == 6)
				{
					break;
				}
				num2++;
			}
		}

		private void commodityName_Enter(object sender, EventArgs e)
		{
			if ("請輸入商品名稱關鍵字".Equals(commodityName.Text))
			{
				commodityName.Text = "";
			}
		}

		private void commodityName_Leave(object sender, EventArgs e)
		{
			if ("".Equals(commodityName.Text))
			{
				commodityName.Text = "請輸入商品名稱關鍵字";
			}
		}

		private void commodityNum_Enter(object sender, EventArgs e)
		{
			if ("請刷商品條碼或輸入條碼".Equals(commodityNum.Text))
			{
				commodityNum.Text = "";
			}
		}

		public void viewCommodityInfo(object sender, EventArgs s)
		{
			CommodityInfoforSearch commodityInfoforSearch = sender as CommodityInfoforSearch;
			if (Program.SystemMode == 1)
			{
				frshyscorp frshyscorp = new frshyscorp(fms, commodityInfoforSearch.barcode, commodityInfoforSearch.getcommodityName());
				frshyscorp.Location = new Point(base.Location.X, base.Location.Y);
				frshyscorp.setSearch(this);
				frshyscorp.ShowDialog();
			}
			else
			{
				frshyscorp frshyscorp2 = new frshyscorp(frms, commodityInfoforSearch.barcode, commodityInfoforSearch.getcommodityName());
				frshyscorp2.Location = new Point(base.Location.X, base.Location.Y);
				frshyscorp2.setSearch(this);
				frshyscorp2.ShowDialog();
			}
		}

		public void infolistCell(object sender, EventArgs e)
		{
			CommodityInfoforSearch commodityInfoforSearch = sender as CommodityInfoforSearch;
			string[] strWhereParameterArray = new string[1]
			{
				commodityInfoforSearch.barcode
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDSNO,CLA1NO,ISWS", "hypos_GOODSLST", "GDSNO = {0} ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0 && "0303".Equals(dataTable.Rows[0]["CLA1NO"].ToString()) && "Y".Equals(dataTable.Rows[0]["ISWS"].ToString()))
			{
				if (Program.IsSaleOfFertilizer && Program.IsFertilizer)
				{
					frms.addOnecommodity(commodityInfoforSearch.barcode, "", "", "", "");
					frms.Show();
					Close();
				}
				else
				{
					AutoClosingMessageBox.Show("店家無法販賣介接肥料。");
				}
			}
			else
			{
				frms.addOnecommodity(commodityInfoforSearch.barcode, "", "", "", "");
				frms.Show();
				Close();
			}
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

		public void infolistCellForDelivery(object sender, EventArgs e)
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

		private void commodityNum_Leave(object sender, EventArgs e)
		{
			if ("".Equals(commodityNum.Text))
			{
				commodityNum.Text = "請刷商品條碼或輸入條碼";
			}
		}

		private void search_Click(object sender, EventArgs e)
		{
			if ((commodityNum.Text == "請刷商品條碼或輸入條碼" || commodityNum.Text == "") && (commodityName.Text == "請輸入商品名稱關鍵字" || commodityName.Text == "") && commodityClass.SelectedIndex == 0)
			{
				AutoClosingMessageBox.Show("必須輸入查詢條件");
				return;
			}
			string[] strSearchConditionCommodityStatus = new string[3]
			{
				myCheckBox1.Checked.ToString(),
				myCheckBox2.Checked.ToString(),
				myCheckBox3.Checked.ToString()
			};
			if (frmI != null)
			{
				switchForm(new frmCommdityList(frmI, commodityName.Text.ToString(), commodityNum.Text.ToString(), commodityClass.SelectedIndex.ToString(), strSearchConditionCommodityStatus));
			}
			else if (fms != null || frms != null)
			{
				if (Program.SystemMode == 1)
				{
					switchForm(new frmCommdityList(fms, commodityName.Text.ToString(), commodityNum.Text.ToString(), commodityClass.SelectedIndex.ToString(), strSearchConditionCommodityStatus));
				}
				else
				{
					switchForm(new frmCommdityList(frms, commodityName.Text.ToString(), commodityNum.Text.ToString(), commodityClass.SelectedIndex.ToString(), strSearchConditionCommodityStatus));
				}
			}
			else
			{
				switchForm(new frmCommdityList(frmD, commodityName.Text.ToString(), commodityNum.Text.ToString(), commodityClass.SelectedIndex.ToString(), strSearchConditionCommodityStatus));
			}
		}

		private void tb_CommodityNum_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && commodityNum.Text.Length >= 13)
			{
				search_Click(sender, e);
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			if (frmI != null)
			{
				frmI.Location = new Point(base.Location.X, base.Location.Y);
				frmI.setfocus();
				frmI.Show();
			}
			else if (fms != null || frms != null)
			{
				if (Program.SystemMode == 1)
				{
					fms.Location = new Point(base.Location.X, base.Location.Y);
					fms.setfocus();
					fms.Show();
				}
				else
				{
					frms.Location = new Point(base.Location.X, base.Location.Y);
					frms.setfocus();
					frms.Show();
				}
			}
			else
			{
				frmD.Location = new Point(base.Location.X, base.Location.Y);
				frmD.setfocus();
				frmD.Show();
			}
			Hide();
		}

		private void reset_Click(object sender, EventArgs e)
		{
			commodityNum.Text = "請刷商品條碼或輸入條碼";
			commodityName.Text = "請輸入商品名稱關鍵字";
			commodityClass.SelectedIndex = 0;
			myCheckBox1.Checked = false;
			myCheckBox2.Checked = false;
			myCheckBox3.Checked = false;
		}

		private void guide_Click(object sender, EventArgs e)
		{
			if (Program.SystemMode == 1)
			{
				frmCropGuide frmCropGuide = new frmCropGuide(fms);
				frmCropGuide.Location = new Point(base.Location.X, base.Location.Y);
				frmCropGuide.Show();
			}
			else
			{
				frmCropGuide frmCropGuide2 = new frmCropGuide(frms);
				frmCropGuide2.Location = new Point(base.Location.X, base.Location.Y);
				frmCropGuide2.Show();
			}
			Hide();
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

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void commodityName_Enter_1(object sender, EventArgs e)
		{
			if ("請輸入商品名稱關鍵字".Equals(commodityName.Text))
			{
				commodityName.Text = "";
			}
		}

		private void commodityName_Leave_1(object sender, EventArgs e)
		{
			if ("".Equals(commodityName.Text))
			{
				commodityName.Text = "請輸入商品名稱關鍵字";
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
			guide = new System.Windows.Forms.Button();
			cancel = new System.Windows.Forms.Button();
			reset = new System.Windows.Forms.Button();
			search = new System.Windows.Forms.Button();
			commodityNum = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			uC_Commodity1 = new POS_Client.CommodityInfoforSearch();
			uC_Commodity2 = new POS_Client.CommodityInfoforSearch();
			uC_Commodity3 = new POS_Client.CommodityInfoforSearch();
			uC_Commodity4 = new POS_Client.CommodityInfoforSearch();
			uC_Commodity5 = new POS_Client.CommodityInfoforSearch();
			uC_Commodity6 = new POS_Client.CommodityInfoforSearch();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			panel4 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			commodityClass = new System.Windows.Forms.ComboBox();
			panel2 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			commodityName = new System.Windows.Forms.TextBox();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			myCheckBox1 = new POS_Client.MyCheckBox();
			myCheckBox2 = new POS_Client.MyCheckBox();
			myCheckBox3 = new POS_Client.MyCheckBox();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			tableLayoutPanel1.SuspendLayout();
			panel4.SuspendLayout();
			panel2.SuspendLayout();
			panel1.SuspendLayout();
			panel3.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			SuspendLayout();
			guide.BackColor = System.Drawing.Color.FromArgb(57, 176, 192);
			guide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			guide.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			guide.ForeColor = System.Drawing.Color.White;
			guide.Location = new System.Drawing.Point(773, 234);
			guide.Name = "guide";
			guide.Size = new System.Drawing.Size(102, 36);
			guide.TabIndex = 10;
			guide.Text = "用藥指引";
			guide.UseVisualStyleBackColor = false;
			guide.Click += new System.EventHandler(guide_Click);
			cancel.BackColor = System.Drawing.Color.FromArgb(175, 164, 134);
			cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			cancel.ForeColor = System.Drawing.Color.White;
			cancel.Location = new System.Drawing.Point(543, 234);
			cancel.Name = "cancel";
			cancel.Size = new System.Drawing.Size(121, 36);
			cancel.TabIndex = 9;
			cancel.Text = "返回前頁";
			cancel.UseVisualStyleBackColor = false;
			cancel.Click += new System.EventHandler(cancel_Click);
			reset.BackColor = System.Drawing.Color.FromArgb(175, 164, 134);
			reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			reset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			reset.ForeColor = System.Drawing.Color.White;
			reset.Location = new System.Drawing.Point(443, 234);
			reset.Name = "reset";
			reset.Size = new System.Drawing.Size(94, 36);
			reset.TabIndex = 8;
			reset.Text = "重設";
			reset.UseVisualStyleBackColor = false;
			reset.Click += new System.EventHandler(reset_Click);
			search.BackColor = System.Drawing.Color.FromArgb(167, 202, 0);
			search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			search.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			search.ForeColor = System.Drawing.Color.White;
			search.Location = new System.Drawing.Point(343, 234);
			search.Name = "search";
			search.Size = new System.Drawing.Size(94, 36);
			search.TabIndex = 6;
			search.Text = "查詢";
			search.UseVisualStyleBackColor = false;
			search.Click += new System.EventHandler(search_Click);
			commodityNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
			commodityNum.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			commodityNum.ForeColor = System.Drawing.Color.FromArgb(82, 82, 82);
			commodityNum.ImeMode = System.Windows.Forms.ImeMode.Disable;
			commodityNum.Location = new System.Drawing.Point(130, 29);
			commodityNum.Margin = new System.Windows.Forms.Padding(10);
			commodityNum.Name = "commodityNum";
			commodityNum.Size = new System.Drawing.Size(256, 29);
			commodityNum.TabIndex = 3;
			commodityNum.Text = "請刷商品條碼或輸入條碼";
			commodityNum.Enter += new System.EventHandler(commodityNum_Enter);
			commodityNum.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_CommodityNum_KeyDown);
			commodityNum.Leave += new System.EventHandler(commodityNum_Leave);
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Black;
			label5.Image = POS_Client.Properties.Resources.oblique;
			label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label5.Location = new System.Drawing.Point(87, 292);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(147, 25);
			label5.TabIndex = 35;
			label5.Text = "   最近賣出商品";
			uC_Commodity1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity1.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity1.Location = new System.Drawing.Point(92, 329);
			uC_Commodity1.Margin = new System.Windows.Forms.Padding(0);
			uC_Commodity1.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity1.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity1.Name = "uC_Commodity1";
			uC_Commodity1.Size = new System.Drawing.Size(398, 102);
			uC_Commodity1.TabIndex = 52;
			uC_Commodity2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity2.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity2.Location = new System.Drawing.Point(490, 329);
			uC_Commodity2.Margin = new System.Windows.Forms.Padding(0);
			uC_Commodity2.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity2.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity2.Name = "uC_Commodity2";
			uC_Commodity2.Size = new System.Drawing.Size(398, 102);
			uC_Commodity2.TabIndex = 53;
			uC_Commodity3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity3.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity3.Location = new System.Drawing.Point(92, 428);
			uC_Commodity3.Margin = new System.Windows.Forms.Padding(0);
			uC_Commodity3.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity3.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity3.Name = "uC_Commodity3";
			uC_Commodity3.Size = new System.Drawing.Size(398, 102);
			uC_Commodity3.TabIndex = 54;
			uC_Commodity4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity4.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity4.Location = new System.Drawing.Point(490, 428);
			uC_Commodity4.Margin = new System.Windows.Forms.Padding(0);
			uC_Commodity4.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity4.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity4.Name = "uC_Commodity4";
			uC_Commodity4.Size = new System.Drawing.Size(398, 102);
			uC_Commodity4.TabIndex = 55;
			uC_Commodity5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity5.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity5.Location = new System.Drawing.Point(92, 530);
			uC_Commodity5.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity5.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity5.Name = "uC_Commodity5";
			uC_Commodity5.Size = new System.Drawing.Size(398, 102);
			uC_Commodity5.TabIndex = 56;
			uC_Commodity6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_Commodity6.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_Commodity6.Location = new System.Drawing.Point(490, 530);
			uC_Commodity6.Margin = new System.Windows.Forms.Padding(0);
			uC_Commodity6.MaximumSize = new System.Drawing.Size(398, 102);
			uC_Commodity6.MinimumSize = new System.Drawing.Size(398, 102);
			uC_Commodity6.Name = "uC_Commodity6";
			uC_Commodity6.Size = new System.Drawing.Size(398, 102);
			uC_Commodity6.TabIndex = 57;
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 4;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35f));
			tableLayoutPanel1.Controls.Add(panel4, 2, 1);
			tableLayoutPanel1.Controls.Add(commodityClass, 1, 1);
			tableLayoutPanel1.Controls.Add(panel2, 0, 1);
			tableLayoutPanel1.Controls.Add(commodityName, 3, 0);
			tableLayoutPanel1.Controls.Add(panel1, 2, 0);
			tableLayoutPanel1.Controls.Add(panel3, 0, 0);
			tableLayoutPanel1.Controls.Add(commodityNum, 1, 0);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 3, 1);
			tableLayoutPanel1.Location = new System.Drawing.Point(92, 44);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 2;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel1.Size = new System.Drawing.Size(796, 174);
			tableLayoutPanel1.TabIndex = 58;
			tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(tableLayoutPanel1_Paint);
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label2);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Location = new System.Drawing.Point(397, 87);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(118, 86);
			panel4.TabIndex = 30;
			label2.AutoSize = true;
			label2.BackColor = System.Drawing.Color.Transparent;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(25, 32);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(74, 21);
			label2.TabIndex = 0;
			label2.Text = "商品狀態";
			commodityClass.Anchor = System.Windows.Forms.AnchorStyles.Left;
			commodityClass.DisplayMember = "Text";
			commodityClass.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			commodityClass.ForeColor = System.Drawing.Color.FromArgb(82, 82, 82);
			commodityClass.FormattingEnabled = true;
			commodityClass.Items.AddRange(new object[4]
			{
				"全部",
				"農藥",
				"肥料",
				"資材/其他"
			});
			commodityClass.Location = new System.Drawing.Point(130, 116);
			commodityClass.Margin = new System.Windows.Forms.Padding(10);
			commodityClass.Name = "commodityClass";
			commodityClass.Size = new System.Drawing.Size(144, 28);
			commodityClass.TabIndex = 27;
			panel2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel2.Controls.Add(label7);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Location = new System.Drawing.Point(1, 87);
			panel2.Margin = new System.Windows.Forms.Padding(0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(118, 86);
			panel2.TabIndex = 26;
			label7.AutoSize = true;
			label7.BackColor = System.Drawing.Color.Transparent;
			label7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(23, 34);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(74, 21);
			label7.TabIndex = 0;
			label7.Text = "商品類型";
			commodityName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			commodityName.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			commodityName.ForeColor = System.Drawing.Color.FromArgb(82, 82, 82);
			commodityName.Location = new System.Drawing.Point(526, 29);
			commodityName.Margin = new System.Windows.Forms.Padding(10);
			commodityName.Name = "commodityName";
			commodityName.Size = new System.Drawing.Size(259, 29);
			commodityName.TabIndex = 25;
			commodityName.Text = "請輸入商品名稱關鍵字";
			commodityName.Enter += new System.EventHandler(commodityName_Enter_1);
			commodityName.Leave += new System.EventHandler(commodityName_Leave_1);
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(397, 1);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(118, 85);
			panel1.TabIndex = 24;
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(25, 35);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(74, 21);
			label1.TabIndex = 0;
			label1.Text = "商品名稱";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label4);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 1);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(118, 85);
			panel3.TabIndex = 22;
			label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label4.AutoSize = true;
			label4.BackColor = System.Drawing.Color.Transparent;
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.White;
			label4.Location = new System.Drawing.Point(23, 33);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(74, 21);
			label4.TabIndex = 0;
			label4.Text = "商品編號";
			flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			flowLayoutPanel1.Controls.Add(myCheckBox1);
			flowLayoutPanel1.Controls.Add(myCheckBox2);
			flowLayoutPanel1.Controls.Add(myCheckBox3);
			flowLayoutPanel1.Location = new System.Drawing.Point(519, 114);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(273, 32);
			flowLayoutPanel1.TabIndex = 31;
			myCheckBox1.Font = new System.Drawing.Font("微軟正黑體", 12f);
			myCheckBox1.Location = new System.Drawing.Point(3, 3);
			myCheckBox1.Name = "myCheckBox1";
			myCheckBox1.Size = new System.Drawing.Size(77, 24);
			myCheckBox1.TabIndex = 0;
			myCheckBox1.Text = "使用中";
			myCheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			myCheckBox1.UseVisualStyleBackColor = true;
			myCheckBox2.Font = new System.Drawing.Font("微軟正黑體", 12f);
			myCheckBox2.Location = new System.Drawing.Point(86, 3);
			myCheckBox2.Name = "myCheckBox2";
			myCheckBox2.Size = new System.Drawing.Size(77, 24);
			myCheckBox2.TabIndex = 1;
			myCheckBox2.Text = "未使用";
			myCheckBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			myCheckBox2.UseVisualStyleBackColor = true;
			myCheckBox3.Font = new System.Drawing.Font("微軟正黑體", 12f);
			myCheckBox3.Location = new System.Drawing.Point(169, 3);
			myCheckBox3.Name = "myCheckBox3";
			myCheckBox3.Size = new System.Drawing.Size(76, 24);
			myCheckBox3.TabIndex = 2;
			myCheckBox3.Text = "已停用";
			myCheckBox3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			myCheckBox3.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(guide);
			base.Controls.Add(cancel);
			base.Controls.Add(reset);
			base.Controls.Add(uC_Commodity6);
			base.Controls.Add(search);
			base.Controls.Add(uC_Commodity5);
			base.Controls.Add(uC_Commodity4);
			base.Controls.Add(uC_Commodity3);
			base.Controls.Add(uC_Commodity2);
			base.Controls.Add(uC_Commodity1);
			base.Controls.Add(label5);
			base.Controls.Add(tableLayoutPanel1);
			base.Name = "frmCommoditySearch";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Form3";
			base.Controls.SetChildIndex(tableLayoutPanel1, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(uC_Commodity1, 0);
			base.Controls.SetChildIndex(uC_Commodity2, 0);
			base.Controls.SetChildIndex(uC_Commodity3, 0);
			base.Controls.SetChildIndex(uC_Commodity4, 0);
			base.Controls.SetChildIndex(uC_Commodity5, 0);
			base.Controls.SetChildIndex(search, 0);
			base.Controls.SetChildIndex(uC_Commodity6, 0);
			base.Controls.SetChildIndex(reset, 0);
			base.Controls.SetChildIndex(cancel, 0);
			base.Controls.SetChildIndex(guide, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			flowLayoutPanel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
