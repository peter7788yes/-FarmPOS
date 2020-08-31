using POS_Client.Properties;
using POS_Client.Utils;
using POS_Client.WebService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmMainShopSimpleWithMoney : MasterThinForm
	{
		public class CustomColumn : DataGridViewColumn
		{
			public override DataGridViewCell CellTemplate
			{
				get
				{
					return base.CellTemplate;
				}
				set
				{
					if (value != null && !value.GetType().IsAssignableFrom(typeof(CustomeCell)))
					{
						throw new InvalidCastException("It should be a custom Cell");
					}
					base.CellTemplate = value;
				}
			}

			public CustomColumn()
				: base(new CustomeCell())
			{
			}
		}

		public class CustomeCell : DataGridViewCell
		{
			public override Type ValueType
			{
				get
				{
					return typeof(CommodityInfo);
				}
			}

			protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
			{
				CommodityInfo commodityInfo = (CommodityInfo)value;
				Bitmap bitmap = new Bitmap(cellBounds.Width, cellBounds.Height);
				commodityInfo.DrawToBitmap(bitmap, new Rectangle(0, 0, commodityInfo.Width, commodityInfo.Height));
				graphics.DrawImage(bitmap, cellBounds.Location);
			}
		}

		public int columnOfFocus;

		private int totalmoney;

		public int dismoney;

		public double aftertotalmoney;

		public double tempdistotalprice;

		public int usediscount;

		private string HseqNo = "";

		private string _memberType = "";

		private string _idNO = "";

		public string vipNo = "";

		public string sumitems;

		public string totalitems;

		private List<string> barcodelist = new List<string>();

		private bool same;

		private bool memberSubsidy;

		public CommodityInfo uc;

		private string tempKeyCode = "";

		private IContainer components;

		private TextBox textBox1;

		private TextBox num;

		private Label l_sysTime;

		private DataGridView infolist;

		private Button addone;

		private Button subone;

		private Button numone;

		private Button pressEnter;

		private Panel panel1;

		private Button backspace;

		private Button pre;

		private Label totalspending;

		private Label totalpriceDiscount;

		private Label totalpriceDiscountView;

		private Label totalprice;

		private Label totalpriceview;

		private Button oneremove;

		private Button next;

		private Button numzero;

		private Button numtwo;

		private Button numthree;

		private Button numfour;

		private Button numsix;

		private Button numfive;

		private Button numseven;

		private Button numeight;

		private Button numnine;

		private Button removeall;

		private Button clearenter;

		private TextBox alertMsg;

		private Button commoditySearch;

		private Button cureGuide;

		private Button Checkout;

		private FlowLayoutPanel flp_chooseMember;

		private Label label2;

		private Label label1;

		private Label label3;

		private PictureBox pictureBox1;

		private Button onesubcount;

		private Button disodd;

		private Button distotalprice;

		private Button onediscount;

		private Label label4;

		private Label summoney;

		private TextBox textBox3;

		private Label label5;

		private TextBox textBox2;

		private Button button1;

		private DataGridViewTextBoxColumn Column1;

		private frmMainShopSimple.CustomColumn commodity;

		private DataGridViewTextBoxColumn setprice;

		private DataGridViewTextBoxColumn sellingprice;

		private DataGridViewTextBoxColumn quantity;

		private DataGridViewTextBoxColumn subtotal;

		private DataGridViewTextBoxColumn discount;

		private DataGridViewTextBoxColumn sum;

		private DataGridViewTextBoxColumn barcode;

		private DataGridViewTextBoxColumn cropId;

		private DataGridViewTextBoxColumn pestId;

		private DataGridViewTextBoxColumn SpecialPrice1;

		private DataGridViewTextBoxColumn SpecialPrice2;

		private DataGridViewTextBoxColumn openPrice;

		private DataGridViewTextBoxColumn subsidyFertilizer;

		private DataGridViewTextBoxColumn subsidyMoney;

		private DataGridViewTextBoxColumn ISWS;

		private DataGridViewTextBoxColumn CLA1NO;

		public frmMainShopSimpleWithMoney(string vipNo)
			: base("銷售作業")
		{
			init();
			this.vipNo = vipNo;
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hcr.*,ara.area as ae,adr.city as ac", "hypos_CUST_RTL as hcr,ADDRAREA as ara,ADDRCITY as adr", "hcr.VipNo = {0} and adr.cityno = hcr.City and ara.zipcode = hcr.Area  ", "", null, new string[1]
			{
				vipNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				_memberType = dataTable.Rows[0]["Type"].ToString();
				_idNO = dataTable.Rows[0]["idNo"].ToString();
				label2.Text = "會員:" + dataTable.Rows[0]["Name"].ToString();
				if (dataTable.Rows[0]["Telphone"].ToString() != "")
				{
					label2.Text = label2.Text + "\r\n電話:" + dataTable.Rows[0]["Telphone"].ToString();
				}
				else
				{
					label2.Text = label2.Text + "\r\n電話:" + dataTable.Rows[0]["Mobile"].ToString();
				}
				label2.Text = label2.Text + "\r\n地址:" + dataTable.Rows[0]["ac"].ToString() + dataTable.Rows[0]["ae"].ToString();
				label2.Font = new Font("微軟正黑體", 14f, FontStyle.Bold, GraphicsUnit.Point, 136);
				int dBVersion = Program.GetDBVersion();
				if (dBVersion == 0)
				{
					string value = new UploadVerification().farmerInfo(_idNO);
					if ("符合補助資格".Equals(value))
					{
						memberSubsidy = true;
					}
				}
				else if (dBVersion >= 1)
				{
					string[] strParameterArray = new string[1]
					{
						_idNO
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_CUST_RTL WHERE IdNo = {0} ", strParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count > 0 && "Y".Equals(dataTable2.Rows[0]["Verification"].ToString()))
					{
						memberSubsidy = true;
					}
				}
				computetotalmoney("", true);
			}
			else
			{
				AutoClosingMessageBox.Show("會員號碼錯誤!");
				Close();
			}
		}

		public frmMainShopSimpleWithMoney()
			: base("銷售作業")
		{
			init();
		}

		private void init()
		{
			pb_virtualKeyBoard.Visible = false;
			DateTime now = DateTime.Now;
			string text = now.ToString("yyyyMMdd");
			string str = now.ToString("yyyy-MM-dd");
			string[] strWhereParameterArray = new string[1]
			{
				text
			};
			string strWhereClause = "sellTime like '%" + str + "%'";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_main_sell", strWhereClause, "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				string text2 = (dataTable.Rows.Count + 1).ToString();
				if (text2.Length == 1)
				{
					HseqNo = Program.LincenseCode + Program.SiteNo + text + "000" + text2;
				}
				else if (text2.Length == 2)
				{
					HseqNo = Program.LincenseCode + Program.SiteNo + text + "00" + text2;
				}
				else if (text2.Length == 3)
				{
					HseqNo = Program.LincenseCode + Program.SiteNo + text + "0" + text2;
				}
				else
				{
					HseqNo = Program.LincenseCode + Program.SiteNo + text + text2;
				}
			}
			else
			{
				HseqNo = Program.LincenseCode + Program.SiteNo + text + "0001";
			}
			setMasterFormName("銷售作業 | 單號: " + HseqNo);
			InitializeComponent();
			if (Program.goodsWithMoneyTemp.Count > 0)
			{
				foreach (GoodObjectWithMoney item in Program.goodsWithMoneyTemp)
				{
					infolist.Rows.Add(item._index, item._GDSName, item._setprice, item._sellingprice, item._number, item._subtotal, item._discount, item._sum, item._barcode, item._cropId, item._pestId, item._specialPrice1, item._specialPrice2, item._openPrice, item._subsidyFertilizer, item._subsidyMoney, item._ISWS, item._CLA1NO);
					foreach (string item2 in barcodelist)
					{
						if (item2.Equals(textBox1.Text))
						{
							same = true;
						}
					}
					infolist.CurrentCell = infolist.Rows[item._index - 1].Cells[0];
					if (infolist.CurrentCell != null)
					{
						infolist.ClearSelection();
						(infolist.CurrentRow.Cells[1].Value as CommodityInfo).BackColor = Color.White;
						infolist.CurrentRow.Selected = false;
						infolist.Refresh();
					}
					if (!same)
					{
						barcodelist.Add(textBox1.Text);
					}
					same = false;
				}
			}
			computetotalmoney("", true);
			textBox1.Select();
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (columnOfFocus == 3)
			{
				if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
				{
					textBox1.Text = textBox1.Text.Trim() + "0";
				}
				else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
				{
					textBox1.Text = textBox1.Text.Trim() + "1";
				}
				else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
				{
					textBox1.Text = textBox1.Text.Trim() + "2";
				}
				else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
				{
					textBox1.Text = textBox1.Text.Trim() + "3";
				}
				else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
				{
					textBox1.Text = textBox1.Text.Trim() + "4";
				}
				else if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
				{
					textBox1.Text = textBox1.Text.Trim() + "5";
				}
				else if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
				{
					textBox1.Text = textBox1.Text.Trim() + "6";
				}
				else if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
				{
					textBox1.Text = textBox1.Text.Trim() + "7";
				}
				else if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
				{
					textBox1.Text = textBox1.Text.Trim() + "8";
				}
				else if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
				{
					textBox1.Text = textBox1.Text.Trim() + "9";
				}
				else if (e.KeyCode == Keys.Back && textBox1.Text.Length > 0)
				{
					textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
				}
			}
			if (e.KeyCode == Keys.Add)
			{
				if (infolist.Rows.Count > 0 && infolist.CurrentRow.Selected)
				{
					int num = int.Parse(infolist.CurrentRow.Cells["quantity"].Value.ToString()) + 1;
					infolist.CurrentRow.Cells["quantity"].Value = num.ToString();
					int num2 = 0;
					num2 = num * int.Parse(infolist.CurrentRow.Cells["sellingprice"].Value.ToString());
					infolist.CurrentRow.Cells["subtotal"].Value = num2.ToString();
					infolist.CurrentRow.Cells["sum"].Value = (num2 - int.Parse(infolist.CurrentRow.Cells["discount"].Value.ToString())).ToString();
					computetotalmoney("", false);
					alertMessage("數量加1");
					setfocus();
				}
			}
			else if (e.KeyCode == Keys.Subtract)
			{
				if (infolist.Rows.Count > 0 && infolist.CurrentRow.Selected && int.Parse(infolist.CurrentRow.Cells["quantity"].Value.ToString()) > 0)
				{
					int num3 = int.Parse(infolist.CurrentRow.Cells["quantity"].Value.ToString()) - 1;
					if (num3 > 0)
					{
						infolist.CurrentRow.Cells["quantity"].Value = num3.ToString();
						int num4 = 0;
						num4 = num3 * int.Parse(infolist.CurrentRow.Cells["sellingprice"].Value.ToString());
						infolist.CurrentRow.Cells["subtotal"].Value = num4.ToString();
						infolist.CurrentRow.Cells["sum"].Value = (num4 - int.Parse(infolist.CurrentRow.Cells["discount"].Value.ToString())).ToString();
					}
					else
					{
						infolist.Rows.RemoveAt(infolist.CurrentRow.Index);
						barcodelist.Clear();
						for (int i = 0; i < infolist.Rows.Count; i++)
						{
							foreach (string item in barcodelist)
							{
								if (item.Equals(infolist.Rows[i].Cells["barcode"].Value.ToString()))
								{
									same = true;
								}
							}
							if (!same)
							{
								barcodelist.Add(infolist.Rows[i].Cells["barcode"].Value.ToString());
							}
							same = false;
						}
					}
					computetotalmoney("", false);
					alertMessage("數量減1");
					setfocus();
				}
			}
			else if (e.Control && e.KeyCode == Keys.M)
			{
				Program.goodsWithMoneyTemp.Clear();
				for (int j = 0; j < infolist.Rows.Count; j++)
				{
					Program.goodsWithMoneyTemp.Add(new GoodObjectWithMoney(int.Parse(infolist.Rows[j].Cells["Column1"].Value.ToString()), (CommodityInfo)infolist.Rows[j].Cells["commodity"].Value, infolist.Rows[j].Cells["setprice"].Value.ToString(), infolist.Rows[j].Cells["sellingprice"].Value.ToString(), infolist.Rows[j].Cells["quantity"].Value.ToString(), infolist.Rows[j].Cells["subtotal"].Value.ToString(), infolist.Rows[j].Cells["discount"].Value.ToString(), infolist.Rows[j].Cells["sum"].Value.ToString(), infolist.Rows[j].Cells["barcode"].Value.ToString(), infolist.Rows[j].Cells["cropId"].Value.ToString(), infolist.Rows[j].Cells["pestId"].Value.ToString(), infolist.Rows[j].Cells["SpecialPrice1"].Value.ToString(), infolist.Rows[j].Cells["SpecialPrice2"].Value.ToString(), infolist.Rows[j].Cells["openPrice"].Value.ToString(), infolist.Rows[j].Cells["subsidyFertilizer"].Value.ToString(), infolist.Rows[j].Cells["subsidyMoney"].Value.ToString(), infolist.Rows[j].Cells["ISWS"].Value.ToString(), infolist.Rows[j].Cells["CLA1NO"].Value.ToString()));
				}
				switchForm(new frmChooseMember());
			}
			else if (e.KeyCode == Keys.C)
			{
				tempKeyCode += "C";
			}
			else if (e.KeyCode == Keys.T)
			{
				tempKeyCode += "T";
			}
			else if (e.KeyCode == Keys.R)
			{
				tempKeyCode += "R";
			}
			else if (e.KeyCode == Keys.L)
			{
				tempKeyCode += "L";
			}
			else if (e.KeyCode == Keys.M)
			{
				tempKeyCode += "M";
			}
			else if (e.KeyCode == Keys.O)
			{
				tempKeyCode += "O";
			}
			if (e.KeyCode == Keys.Return && !string.IsNullOrEmpty(textBox1.Text) && columnOfFocus == 1)
			{
				if (tempKeyCode.Length >= 5)
				{
					if ("CTRLM".Equals(tempKeyCode))
					{
						Program.goodsWithMoneyTemp.Clear();
						for (int k = 0; k < infolist.Rows.Count; k++)
						{
							Program.goodsWithMoneyTemp.Add(new GoodObjectWithMoney(int.Parse(infolist.Rows[k].Cells["Column1"].Value.ToString()), (CommodityInfo)infolist.Rows[k].Cells["commodity"].Value, infolist.Rows[k].Cells["setprice"].Value.ToString(), infolist.Rows[k].Cells["sellingprice"].Value.ToString(), infolist.Rows[k].Cells["quantity"].Value.ToString(), infolist.Rows[k].Cells["subtotal"].Value.ToString(), infolist.Rows[k].Cells["discount"].Value.ToString(), infolist.Rows[k].Cells["sum"].Value.ToString(), infolist.Rows[k].Cells["barcode"].Value.ToString(), infolist.Rows[k].Cells["cropId"].Value.ToString(), infolist.Rows[k].Cells["pestId"].Value.ToString(), infolist.Rows[k].Cells["SpecialPrice1"].Value.ToString(), infolist.Rows[k].Cells["SpecialPrice2"].Value.ToString(), infolist.Rows[k].Cells["openPrice"].Value.ToString(), infolist.Rows[k].Cells["subsidyFertilizer"].Value.ToString(), infolist.Rows[k].Cells["subsidyMoney"].Value.ToString(), infolist.Rows[k].Cells["ISWS"].Value.ToString(), infolist.Rows[k].Cells["CLA1NO"].Value.ToString()));
						}
						switchForm(new frmChooseMember());
					}
					else if ("CTRLO".Equals(tempKeyCode))
					{
						CheckOutPrint();
					}
				}
				else
				{
					commodityEnter();
				}
				tempKeyCode = "";
			}
			else if (e.KeyCode == Keys.Return && !string.IsNullOrEmpty(textBox1.Text) && columnOfFocus == 3)
			{
				if (tempKeyCode.Length >= 5)
				{
					if ("CTRLM".Equals(tempKeyCode))
					{
						Program.goodsWithMoneyTemp.Clear();
						for (int l = 0; l < infolist.Rows.Count; l++)
						{
							Program.goodsWithMoneyTemp.Add(new GoodObjectWithMoney(int.Parse(infolist.Rows[l].Cells["Column1"].Value.ToString()), (CommodityInfo)infolist.Rows[l].Cells["commodity"].Value, infolist.Rows[l].Cells["setprice"].Value.ToString(), infolist.Rows[l].Cells["sellingprice"].Value.ToString(), infolist.Rows[l].Cells["quantity"].Value.ToString(), infolist.Rows[l].Cells["subtotal"].Value.ToString(), infolist.Rows[l].Cells["discount"].Value.ToString(), infolist.Rows[l].Cells["sum"].Value.ToString(), infolist.Rows[l].Cells["barcode"].Value.ToString(), infolist.Rows[l].Cells["cropId"].Value.ToString(), infolist.Rows[l].Cells["pestId"].Value.ToString(), infolist.Rows[l].Cells["SpecialPrice1"].Value.ToString(), infolist.Rows[l].Cells["SpecialPrice2"].Value.ToString(), infolist.Rows[l].Cells["openPrice"].Value.ToString(), infolist.Rows[l].Cells["subsidyFertilizer"].Value.ToString(), infolist.Rows[l].Cells["subsidyMoney"].Value.ToString(), infolist.Rows[l].Cells["ISWS"].Value.ToString(), infolist.Rows[l].Cells["CLA1NO"].Value.ToString()));
						}
						switchForm(new frmChooseMember());
					}
					else if ("CTRLO".Equals(tempKeyCode))
					{
						CheckOutPrint();
					}
				}
				else
				{
					try
					{
						int num5 = int.Parse(textBox1.Text);
						if (num5 == 0)
						{
							infolist.Rows.RemoveAt(infolist.CurrentRow.Index);
						}
						else
						{
							infolist.CurrentRow.Cells["quantity"].Value = num5.ToString();
						}
						textBox1.Text = "";
						computetotalmoney("", false);
					}
					catch (Exception)
					{
						AutoClosingMessageBox.Show("輸入數量錯誤");
						textBox1.Text = "";
					}
				}
				int count = infolist.Rows.Count;
				for (int m = 0; m < count; m++)
				{
					infolist.Rows[m].Selected = true;
					infolist.CurrentCell = infolist.Rows[m].Cells[0];
				}
				if (infolist.CurrentCell != null)
				{
					(infolist.CurrentRow.Cells[1].Value as CommodityInfo).BackColor = Color.White;
					infolist.Refresh();
				}
				setfocus();
				tempKeyCode = "";
			}
			else
			{
				if (e.KeyCode != Keys.Return)
				{
					return;
				}
				if (tempKeyCode.Length >= 5)
				{
					if ("CTRLM".Equals(tempKeyCode))
					{
						Program.goodsWithMoneyTemp.Clear();
						for (int n = 0; n < infolist.Rows.Count; n++)
						{
							Program.goodsWithMoneyTemp.Add(new GoodObjectWithMoney(int.Parse(infolist.Rows[n].Cells["Column1"].Value.ToString()), (CommodityInfo)infolist.Rows[n].Cells["commodity"].Value, infolist.Rows[n].Cells["setprice"].Value.ToString(), infolist.Rows[n].Cells["sellingprice"].Value.ToString(), infolist.Rows[n].Cells["quantity"].Value.ToString(), infolist.Rows[n].Cells["subtotal"].Value.ToString(), infolist.Rows[n].Cells["discount"].Value.ToString(), infolist.Rows[n].Cells["sum"].Value.ToString(), infolist.Rows[n].Cells["barcode"].Value.ToString(), infolist.Rows[n].Cells["cropId"].Value.ToString(), infolist.Rows[n].Cells["pestId"].Value.ToString(), infolist.Rows[n].Cells["SpecialPrice1"].Value.ToString(), infolist.Rows[n].Cells["SpecialPrice2"].Value.ToString(), infolist.Rows[n].Cells["openPrice"].Value.ToString(), infolist.Rows[n].Cells["subsidyFertilizer"].Value.ToString(), infolist.Rows[n].Cells["subsidyMoney"].Value.ToString(), infolist.Rows[n].Cells["ISWS"].Value.ToString(), infolist.Rows[n].Cells["CLA1NO"].Value.ToString()));
						}
						switchForm(new frmChooseMember());
					}
					else if ("CTRLO".Equals(tempKeyCode))
					{
						CheckOutPrint();
					}
				}
				tempKeyCode = "";
			}
		}

		private void numEnter(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Return || columnOfFocus != 2)
			{
				return;
			}
			string[] strWhereParameterArray = new string[1]
			{
				textBox1.Text
			};
			foreach (DataRow row in ((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows)
			{
				infolist.Rows.Add(infolist.RowCount + 1, row["GDSNAME"].ToString(), num.Text, textBox1.Text);
				infolist.Rows[0].Selected = false;
				foreach (string item in barcodelist)
				{
					if (item.Equals(textBox1.Text))
					{
						same = true;
					}
				}
				if (!same)
				{
					barcodelist.Add(textBox1.Text);
				}
				same = false;
			}
			computetotalmoney("", true);
			textBox1.Text = "";
		}

		private void textBox1_Enter(object sender, EventArgs e)
		{
			textBox1.Focus();
			if (infolist.CurrentCell != null)
			{
				infolist.ClearSelection();
				(infolist.CurrentRow.Cells[1].Value as CommodityInfo).BackColor = Color.White;
				infolist.CurrentRow.Selected = false;
				infolist.Refresh();
			}
			columnOfFocus = 1;
		}

		private void num_Focus(object sender, EventArgs e)
		{
			columnOfFocus = 2;
		}

		private void infolist_SelectionChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < infolist.RowCount; i++)
			{
				(infolist[1, i].Value as CommodityInfo).BackColor = Color.White;
			}
			if (infolist.CurrentRow != null && infolist.CurrentRow.Selected)
			{
				num.Text = infolist.CurrentRow.Cells["quantity"].Value.ToString();
				textBox2.Text = infolist.CurrentRow.Cells["sellingprice"].Value.ToString();
				textBox3.Text = infolist.CurrentRow.Cells["subtotal"].Value.ToString();
				(infolist.CurrentRow.Cells[1].Value as CommodityInfo).BackColor = Color.FromArgb(255, 208, 81);
				infolist.Refresh();
			}
			columnOfFocus = 3;
		}

		private void number_Click(object sender, EventArgs e)
		{
			textBox1.Text = textBox1.Text.Trim() + (sender as Button).Text;
		}

		private void pressEnter_Click(object sender, EventArgs e)
		{
			if (columnOfFocus == 3)
			{
				if (!string.IsNullOrEmpty(textBox1.Text))
				{
					try
					{
						int num = int.Parse(textBox1.Text);
						if (num == 0)
						{
							infolist.Rows.RemoveAt(infolist.CurrentRow.Index);
							frmExtendScreen.RemoveAt(infolist.CurrentRow.Index);
						}
						else
						{
							int num2 = 0;
							num2 = num * int.Parse(infolist.CurrentRow.Cells[3].Value.ToString());
							infolist.CurrentRow.Cells["subtotal"].Value = num2.ToString();
							infolist.CurrentRow.Cells["quantity"].Value = num.ToString();
							infolist.CurrentRow.Cells["sum"].Value = (num2 - int.Parse(infolist.CurrentRow.Cells["discount"].Value.ToString())).ToString();
							frmExtendScreen.setCommodityQuantity(infolist.CurrentRow.Index, num.ToString(), (num2 - int.Parse(infolist.CurrentRow.Cells["discount"].Value.ToString())).ToString());
						}
						textBox1.Text = "";
						computetotalmoney("", true);
					}
					catch (Exception)
					{
						AutoClosingMessageBox.Show("輸入數量錯誤");
						textBox1.Text = "";
					}
				}
			}
			else if (columnOfFocus == 1)
			{
				commodityEnter();
			}
			setfocus();
		}

		private void backspace_Click(object sender, EventArgs e)
		{
			if (textBox1.Text.Length > 0)
			{
				textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
			}
		}

		private void pre_Click(object sender, EventArgs e)
		{
			if (columnOfFocus == 3 && infolist.CurrentRow != null)
			{
				if (infolist.CurrentRow.Index > 0)
				{
					int index = infolist.CurrentRow.Index;
					infolist.Rows[infolist.CurrentRow.Index - 1].Selected = true;
					infolist.Rows[index].Selected = false;
					infolist.CurrentCell = infolist.Rows[index - 1].Cells["Column1"];
					infolist_SelectionChanged(sender, e);
				}
				else
				{
					AutoClosingMessageBox.Show("已經是第一筆商品");
				}
			}
		}

		private void addone_Click(object sender, EventArgs e)
		{
			if (columnOfFocus == 3 && infolist.CurrentRow != null)
			{
				int num = int.Parse(infolist.CurrentRow.Cells["quantity"].Value.ToString()) + 1;
				infolist.CurrentRow.Cells["quantity"].Value = num.ToString();
				int num2 = 0;
				num2 = num * int.Parse(infolist.CurrentRow.Cells["sellingprice"].Value.ToString());
				infolist.CurrentRow.Cells["subtotal"].Value = num2.ToString();
				infolist.CurrentRow.Cells["sum"].Value = (num2 - int.Parse(infolist.CurrentRow.Cells["discount"].Value.ToString())).ToString();
				frmExtendScreen.CommodityAddOne(infolist.CurrentRow.Index, (num2 - int.Parse(infolist.CurrentRow.Cells["discount"].Value.ToString())).ToString());
				computetotalmoney("", true);
				alertMessage("數量加1");
				setfocus();
			}
		}

		private void subone_Click(object sender, EventArgs e)
		{
			if (columnOfFocus != 3 || infolist.CurrentRow == null || int.Parse(infolist.CurrentRow.Cells["quantity"].Value.ToString()) <= 0)
			{
				return;
			}
			int num = int.Parse(infolist.CurrentRow.Cells["quantity"].Value.ToString()) - 1;
			if (num > 0)
			{
				infolist.CurrentRow.Cells["quantity"].Value = num.ToString();
				int num2 = 0;
				num2 = num * int.Parse(infolist.CurrentRow.Cells["sellingprice"].Value.ToString());
				infolist.CurrentRow.Cells["subtotal"].Value = num2.ToString();
				infolist.CurrentRow.Cells["sum"].Value = (num2 - int.Parse(infolist.CurrentRow.Cells["discount"].Value.ToString())).ToString();
				frmExtendScreen.CommoditySubOne(infolist.CurrentRow.Index, (num2 - int.Parse(infolist.CurrentRow.Cells["discount"].Value.ToString())).ToString());
			}
			else
			{
				infolist.Rows.RemoveAt(infolist.CurrentRow.Index);
				frmExtendScreen.RemoveAt(infolist.CurrentRow.Index);
				barcodelist.Clear();
				for (int i = 0; i < infolist.Rows.Count; i++)
				{
					foreach (string item in barcodelist)
					{
						if (item.Equals(infolist.Rows[i].Cells["barcode"].Value.ToString()))
						{
							same = true;
						}
					}
					if (!same)
					{
						barcodelist.Add(infolist.Rows[i].Cells["barcode"].Value.ToString());
					}
					same = false;
				}
			}
			computetotalmoney("", true);
			alertMessage("數量減1");
			setfocus();
		}

		private void oneremove_Click(object sender, EventArgs e)
		{
			if (columnOfFocus != 3)
			{
				return;
			}
			if (infolist.CurrentRow == null)
			{
				AutoClosingMessageBox.Show("請選擇商品");
				return;
			}
			frmExtendScreen.RemoveAt(infolist.CurrentRow.Index);
			infolist.Rows.RemoveAt(infolist.CurrentRow.Index);
			computetotalmoney("", true);
			if (infolist.Rows.Count == 0)
			{
				totalmoney = 0;
				dismoney = 0;
				tempdistotalprice = 0.0;
				aftertotalmoney = 0.0;
				totalpriceDiscount.Text = "0";
			}
			alertMessage("移除選擇商品");
		}

		private void removeall_Click(object sender, EventArgs e)
		{
			totalmoney = 0;
			dismoney = 0;
			tempdistotalprice = 0.0;
			aftertotalmoney = 0.0;
			barcodelist.Clear();
			infolist.Rows.Clear();
			infolist.Refresh();
			computetotalmoney("", true);
			totalpriceDiscount.Text = "0";
			alertMessage("移除全部商品");
			frmExtendScreen.RemoveAll();
		}

		private void computetotalmoney(string isEnter, bool select)
		{
			totalmoney = 0;
			int num = 0;
			barcodelist.Clear();
			for (int i = 0; i < infolist.Rows.Count; i++)
			{
				string text = memberType(_memberType);
				string text2 = "";
				if (memberSubsidy)
				{
					if ("Y".Equals(infolist.Rows[i].Cells["subsidyFertilizer"].Value.ToString()))
					{
						if (!"".Equals(infolist.Rows[i].Cells["setprice"].Value.ToString().Trim()))
						{
							int num2 = int.Parse(infolist.Rows[i].Cells["setprice"].Value.ToString());
							int num3 = int.Parse(infolist.Rows[i].Cells["subsidyMoney"].Value.ToString());
							text2 = ((num2 - num3 <= 0) ? "0" : (num2 - num3).ToString());
						}
						else
						{
							text2 = "0";
						}
					}
					else
					{
						text2 = infolist.Rows[i].Cells[text].Value.ToString();
					}
				}
				else
				{
					text2 = infolist.Rows[i].Cells[text].Value.ToString();
				}
				if ("setprice".Equals(text))
				{
					if ("".Equals(text2.Trim()))
					{
						text2 = "0";
					}
				}
				else if ("SpecialPrice1".Equals(text) || "SpecialPrice2".Equals(text))
				{
					if ("".Equals(infolist.Rows[i].Cells["setprice"].Value.ToString()) && "".Equals(text2.Trim()))
					{
						text2 = "0";
					}
					else if ("".Equals(text2.Trim()))
					{
						text2 = infolist.Rows[i].Cells["setprice"].Value.ToString();
					}
				}
				if (!"".Equals(infolist.Rows[i].Cells["openPrice"].Value.ToString().Trim()))
				{
					text2 = infolist.Rows[i].Cells["openPrice"].Value.ToString();
				}
				infolist.Rows[i].Cells["sellingprice"].Value = text2;
				int num4 = int.Parse(text2) * int.Parse(infolist.Rows[i].Cells["quantity"].Value.ToString());
				infolist.Rows[i].Cells["subtotal"].Value = num4;
				int num5 = num4 - int.Parse(infolist.Rows[i].Cells["discount"].Value.ToString());
				if (num5 < 0)
				{
					num5 = 0;
				}
				infolist.Rows[i].Cells["sum"].Value = num5;
				frmExtendScreen.setCommodityPrice(i, text2, num5.ToString());
				foreach (string item in barcodelist)
				{
					if (item.Equals(infolist.Rows[i].Cells["barcode"].Value.ToString()))
					{
						same = true;
					}
				}
				if (!same)
				{
					barcodelist.Add(infolist.Rows[i].Cells["barcode"].Value.ToString());
				}
				same = false;
				infolist.Rows[i].Selected = false;
			}
			foreach (DataGridViewRow item2 in (IEnumerable)infolist.Rows)
			{
				totalmoney += int.Parse(item2.Cells["sum"].Value.ToString());
				num += int.Parse(item2.Cells["quantity"].Value.ToString());
			}
			if ("Enter".Equals(isEnter))
			{
				this.num.Text = "1";
			}
			else
			{
				this.num.Text = "";
				textBox2.Text = "";
				textBox3.Text = "";
			}
			if (usediscount == 2)
			{
				if (totalmoney - dismoney < 0)
				{
					aftertotalmoney = 0.0;
				}
				else
				{
					aftertotalmoney = totalmoney - dismoney;
				}
				totalpriceDiscount.Text = dismoney.ToString();
			}
			else if (usediscount == 1)
			{
				aftertotalmoney = Math.Round(Convert.ToDouble(totalmoney) * tempdistotalprice, 0);
				totalpriceDiscount.Text = tempdistotalprice.ToString();
			}
			else
			{
				if (totalmoney - dismoney < 0)
				{
					aftertotalmoney = 0.0;
				}
				else
				{
					aftertotalmoney = totalmoney - dismoney;
				}
				totalpriceDiscount.Text = dismoney.ToString();
			}
			totalprice.Text = totalmoney.ToString();
			summoney.Text = aftertotalmoney.ToString();
			sumitems = barcodelist.Count.ToString();
			totalitems = num.ToString();
			frmExtendScreen.setTotal(aftertotalmoney.ToString());
			if (select)
			{
				textBox1.Select();
			}
		}

		private void next_Click(object sender, EventArgs e)
		{
			if (columnOfFocus == 3 && infolist.CurrentRow != null)
			{
				if (infolist.CurrentRow.Index < infolist.Rows.Count - 1)
				{
					int index = infolist.CurrentRow.Index;
					infolist.Rows[index + 1].Selected = true;
					infolist.Rows[index].Selected = false;
					infolist.CurrentCell = infolist.Rows[index + 1].Cells["Column1"];
					infolist_SelectionChanged(sender, e);
				}
				else
				{
					AutoClosingMessageBox.Show("已經是最後一筆商品");
				}
			}
		}

		private void clearenter_Click(object sender, EventArgs e)
		{
			textBox1.Text = "";
			setfocus();
		}

		private void alertMessage(string msg)
		{
			alertMsg.Text = msg;
		}

		private void flp_chooseMember_Click(object sender, EventArgs e)
		{
			Program.goodsWithMoneyTemp.Clear();
			for (int i = 0; i < infolist.Rows.Count; i++)
			{
				Program.goodsWithMoneyTemp.Add(new GoodObjectWithMoney(int.Parse(infolist.Rows[i].Cells["Column1"].Value.ToString()), (CommodityInfo)infolist.Rows[i].Cells["commodity"].Value, infolist.Rows[i].Cells["setprice"].Value.ToString(), infolist.Rows[i].Cells["sellingprice"].Value.ToString(), infolist.Rows[i].Cells["quantity"].Value.ToString(), infolist.Rows[i].Cells["subtotal"].Value.ToString(), infolist.Rows[i].Cells["discount"].Value.ToString(), infolist.Rows[i].Cells["sum"].Value.ToString(), infolist.Rows[i].Cells["barcode"].Value.ToString(), infolist.Rows[i].Cells["cropId"].Value.ToString(), infolist.Rows[i].Cells["pestId"].Value.ToString(), infolist.Rows[i].Cells["SpecialPrice1"].Value.ToString(), infolist.Rows[i].Cells["SpecialPrice2"].Value.ToString(), infolist.Rows[i].Cells["openPrice"].Value.ToString(), infolist.Rows[i].Cells["subsidyFertilizer"].Value.ToString(), infolist.Rows[i].Cells["subsidyMoney"].Value.ToString(), infolist.Rows[i].Cells["ISWS"].Value.ToString(), infolist.Rows[i].Cells["CLA1NO"].Value.ToString()));
			}
			switchForm(new frmChooseMember());
		}

		private void commoditySearch_Click(object sender, EventArgs e)
		{
			frmCommoditySearch frmCommoditySearch = new frmCommoditySearch(this);
			frmCommoditySearch.Location = new Point(base.Location.X, base.Location.Y);
			frmCommoditySearch.Show();
			Hide();
		}

		public void addOnecommodity(string barcode, string cropId, string pestId, string cropName, string pestName)
		{
			try
			{
				string[] strWhereParameterArray = new string[1]
				{
					barcode
				};
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDSNO,spec,capacity,CLA1NO,Price,SpecialPrice1,SpecialPrice2,CLA1NO,ISWS,GDName,formCode,CName,contents,brandName,SubsidyFertilizer,SubsidyMoney", "hypos_GOODSLST", "GDSNO = {0} ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				bool flag = true;
				string text = "購肥商品暫時不開放";
				if (dataTable.Rows.Count > 0 && "0303".Equals(dataTable.Rows[0]["CLA1NO"].ToString()) && "Y".Equals(dataTable.Rows[0]["ISWS"].ToString()))
				{
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_ShopInfoManage", null, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count > 0 && !dataTable2.Rows[0]["FertilizerPassword"].ToString().Equals(""))
					{
						Program.IsSaleOfFertilizer = true;
					}
					flag = (Program.IsFertilizer & Program.IsSaleOfFertilizer);
				}
				if (flag)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						uc = new CommodityInfo();
						uc.setMemberIdNo("");
						uc.setMemberVipNo("店內碼:" + row["GDSNO"].ToString());
						uc.setCommodityName(setCommodityName(row));
						uc.setCommodityClass(row["spec"].ToString() + " " + row["capacity"].ToString());
						if ("0302".Equals(row["CLA1NO"].ToString()) && "Y".Equals(row["ISWS"].ToString()))
						{
							uc.setlabe1("作物: " + cropName + "|病蟲害: " + pestName);
						}
						else if ("0303".Equals(row["CLA1NO"].ToString()) && "Y".Equals(dataTable.Rows[0]["ISWS"].ToString()) && "Y".Equals(dataTable.Rows[0]["SubsidyFertilizer"].ToString()))
						{
							uc.setlabe1("補助金額: " + dataTable.Rows[0]["SubsidyMoney"].ToString());
						}
						else
						{
							uc.setlabe1("");
						}
						string text2 = row["Price"].ToString();
						if (string.IsNullOrEmpty(text2))
						{
							text2 = "0";
						}
						infolist.Rows.Add(infolist.RowCount + 1, uc, text2, text2, "1", text2, "0", text2, barcode, cropId, pestId, row["SpecialPrice1"].ToString(), row["SpecialPrice2"].ToString(), "", row["SubsidyFertilizer"].ToString(), row["SubsidyMoney"].ToString(), row["ISWS"].ToString(), row["CLA1NO"].ToString());
						frmExtendScreen.setCommodityInfo(new string[4]
						{
							setCommodityName(row),
							text2,
							"1",
							text2
						});
						infolist.Rows[0].Selected = false;
						foreach (string item in barcodelist)
						{
							if (item.Equals(barcode))
							{
								same = true;
							}
						}
						if (!same)
						{
							barcodelist.Add(barcode);
						}
						same = false;
					}
					computetotalmoney("", true);
					AutoClosingMessageBox.Show("商品已選入");
				}
				else
				{
					setfocus();
					AutoClosingMessageBox.Show(text);
				}
			}
			catch (Exception)
			{
			}
		}

		private void Checkout_Click(object sender, EventArgs e)
		{
			if (vipNo != "")
			{
				if (infolist.CurrentRow != null)
				{
					bool flag = false;
					for (int i = 0; i < infolist.Rows.Count; i++)
					{
						if ("Y".Equals(infolist.Rows[i].Cells["ISWS"].Value.ToString()) && "0303".Equals(infolist.Rows[i].Cells["CLA1NO"].Value.ToString()) && "Y".Equals(infolist.Rows[i].Cells["subsidyFertilizer"].Value.ToString()))
						{
							int dBVersion = Program.GetDBVersion();
							string value = new UploadVerification().farmerInfo(_idNO);
							if ("符合補助資格".Equals(value))
							{
								frmMainShopCheckout();
							}
							else if ("偵測不到網路連線，請確認網路正常後再使用檢查補助".Equals(value) && dBVersion >= 1 && Program.IsFertilizer)
							{
								string[] strParameterArray = new string[1]
								{
									vipNo.ToString()
								};
								DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_CUST_RTL WHERE VipNo = {0} ", strParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
								if (dataTable.Rows.Count > 0)
								{
									if ("Y".Equals(dataTable.Rows[0]["Verification"].ToString()))
									{
										AutoClosingMessageBox.Show("會員符合購肥補助資格");
										frmMainShopCheckout();
									}
									else if (MessageBox.Show("會員不符合補助身分、補助肥料不予補助，確定進行結帳？", "收銀結帳", MessageBoxButtons.YesNo) == DialogResult.Yes)
									{
										frmMainShopCheckout();
									}
								}
								else if (MessageBox.Show("會員不符合補助身分、補助肥料不予補助，確定進行結帳？", "收銀結帳", MessageBoxButtons.YesNo) == DialogResult.Yes)
								{
									frmMainShopCheckout();
								}
							}
							else if (MessageBox.Show("會員不符合補助身分、補助肥料不予補助，確定進行結帳？", "收銀結帳", MessageBoxButtons.YesNo) == DialogResult.Yes)
							{
								frmMainShopCheckout();
							}
							flag = false;
							break;
						}
						flag = true;
					}
					if (flag)
					{
						frmMainShopCheckout();
					}
				}
				else
				{
					frmMainShopCheckout();
				}
			}
			else if ("".Equals(vipNo) && infolist.CurrentRow != null)
			{
				string value2 = "";
				for (int j = 0; j < infolist.Rows.Count; j++)
				{
					string value3 = infolist.Rows[j].Cells["ISWS"].Value.ToString();
					string value4 = infolist.Rows[j].Cells["CLA1NO"].Value.ToString();
					if ("Y".Equals(value3) && "0302".Equals(value4))
					{
						AutoClosingMessageBox.Show("銷售管制農藥商品必須選擇會員");
						return;
					}
					if ("Y".Equals(value3) && "0303".Equals(value4) && "Y".Equals(infolist.Rows[j].Cells["subsidyFertilizer"].Value.ToString()))
					{
						value2 = "0303";
					}
				}
				if ("0303".Equals(value2))
				{
					if (MessageBox.Show("未選擇會員則補助肥料不予補助，確定進行結帳？", "收銀結帳", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						frmMainShopCheckout();
					}
				}
				else
				{
					frmMainShopCheckout();
				}
			}
			else
			{
				AutoClosingMessageBox.Show("尚無可收銀結帳之商品，請先選入商品若要進行賒帳還款，請先選擇會員");
			}
		}

		private void frmMainShopCheckout()
		{
			frmMainShopCheckout frmMainShopCheckout = new frmMainShopCheckout(this, infolist, vipNo);
			frmMainShopCheckout.Location = new Point(base.Location.X, base.Location.Y);
			frmMainShopCheckout.Show();
			Hide();
		}

		private void Checkout_Click()
		{
			if (vipNo == "")
			{
				AutoClosingMessageBox.Show("會員尚未選擇");
			}
			else if (infolist.RowCount == 0)
			{
				AutoClosingMessageBox.Show("無銷售商品");
			}
			else
			{
				switchForm(new frmMainShopCheckout(this, infolist, vipNo));
			}
		}

		private void CheckOutPrint()
		{
			if (vipNo == "")
			{
				AutoClosingMessageBox.Show("會員尚未選擇");
				return;
			}
			if (infolist.RowCount == 0)
			{
				AutoClosingMessageBox.Show("無銷售商品");
				return;
			}
			new frmMainShopCheckout(this, infolist, vipNo).checkOutDataSave(getHseqNo(), gettotalprice(), gettotalpriceDiscount(), gettotalspending(), "0", getsumitems(), gettotalitems(), "0");
			Program.goodsWithMoneyTemp.Clear();
			switchForm(new frmMainShopSimpleWithMoney());
			new frmSell_SellNo(HseqNo).Show();
			Close();
		}

		public string gettotalprice()
		{
			return totalprice.Text;
		}

		public string gettotalpriceDiscount()
		{
			return dismoney.ToString();
		}

		public string getHseqNo()
		{
			return HseqNo;
		}

		private void cureGuide_Click(object sender, EventArgs e)
		{
			frmCropGuide frmCropGuide = new frmCropGuide(this);
			frmCropGuide.Location = new Point(base.Location.X, base.Location.Y);
			frmCropGuide.Show();
			Hide();
		}

		public void addCropAndPest(int index, string crop, string pest)
		{
			infolist.Rows[index - 1].Cells["cropId"].Value = crop;
			infolist.Rows[index - 1].Cells["pestId"].Value = pest;
			string[] strWhereParameterArray = new string[1]
			{
				crop
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyCrop", "code = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			string[] strWhereParameterArray2 = new string[1]
			{
				pest
			};
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyBlight", "code = {0}", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
			uc.setlabe1("作物: " + dataTable.Rows[0]["name"].ToString() + "|病蟲害: " + dataTable2.Rows[0]["name"].ToString());
		}

		public void setfocus()
		{
			textBox1.Select();
		}

		public void RemoveLast()
		{
			infolist.Rows.RemoveAt(infolist.Rows.Count - 1);
			computetotalmoney("", true);
			setfocus();
		}

		private void panal1_Paint(object sender, PaintEventArgs e)
		{
			MatchCollection matchCollection = Regex.Matches("#d9d9d9", "([0-9A-Fa-f]{2})");
			if (matchCollection.Count == 3)
			{
				int red = Convert.ToInt32(matchCollection[0].Groups[0].Value, 16);
				int green = Convert.ToInt32(matchCollection[1].Groups[0].Value, 16);
				int blue = Convert.ToInt32(matchCollection[2].Groups[0].Value, 16);
				ControlPaint.DrawBorder(e.Graphics, panel1.ClientRectangle, Color.FromArgb(red, green, blue), 1, ButtonBorderStyle.Solid, Color.FromArgb(red, green, blue), 1, ButtonBorderStyle.Solid, Color.FromArgb(red, green, blue), 1, ButtonBorderStyle.Solid, Color.FromArgb(red, green, blue), 1, ButtonBorderStyle.Solid);
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

		private void distotalprice_Click(object sender, EventArgs e)
		{
			try
			{
				usediscount = 1;
				string text = "";
				int num = int.Parse(textBox1.Text);
				text = ((textBox1.Text.Length != 1) ? ("0." + num) : ("0.0" + num));
				double num2 = Convert.ToDouble(text);
				double value = Math.Round(Convert.ToDouble(totalmoney) * num2, 0);
				totalpriceDiscount.Text = totalmoney - Convert.ToInt32(value) + "(" + text.ToString() + ")";
				dismoney = totalmoney - Convert.ToInt32(value);
				tempdistotalprice = num2;
				summoney.Text = value.ToString();
				aftertotalmoney = value;
				alertMessage("總價折扣");
			}
			catch (Exception)
			{
				AutoClosingMessageBox.Show("輸入總價折扣數錯誤");
				textBox1.Text = "";
			}
			textBox1.Text = "";
			setfocus();
		}

		private void disodd_Click(object sender, EventArgs e)
		{
			try
			{
				usediscount = 2;
				dismoney = int.Parse(textBox1.Text);
				totalpriceDiscount.Text = dismoney.ToString();
				int num = 0;
				if (totalmoney - dismoney > 0)
				{
					num = totalmoney - dismoney;
				}
				aftertotalmoney = num;
				summoney.Text = aftertotalmoney.ToString();
				alertMessage("總價折讓");
			}
			catch
			{
				AutoClosingMessageBox.Show("輸入總價折讓數錯誤");
				textBox1.Text = "";
			}
			textBox1.Text = "";
			setfocus();
		}

		private void onediscount_Click(object sender, EventArgs e)
		{
			if (infolist.CurrentRow != null)
			{
				string[] strWhereParameterArray = new string[1]
				{
					infolist.CurrentRow.Cells[8].Value.ToString()
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "OpenPrice", "hypos_GOODSLST", "GDSNO = {0}  ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows[0]["OpenPrice"].ToString().Equals("1"))
				{
					try
					{
						infolist.CurrentRow.Cells["openPrice"].Value = int.Parse(textBox1.Text);
						int num = 0;
						num = int.Parse(infolist.CurrentRow.Cells["quantity"].Value.ToString()) * int.Parse(infolist.CurrentRow.Cells["sellingprice"].Value.ToString());
						infolist.CurrentRow.Cells["subtotal"].Value = num.ToString();
						infolist.CurrentRow.Cells["sum"].Value = (num - int.Parse(infolist.CurrentRow.Cells["discount"].Value.ToString())).ToString();
						textBox1.Text = "";
						computetotalmoney("", true);
						alertMessage("開放售價");
					}
					catch (Exception)
					{
						AutoClosingMessageBox.Show("輸入開放售價數錯誤");
						textBox1.Text = "";
						setfocus();
					}
				}
				else
				{
					AutoClosingMessageBox.Show("此商品未開放售價");
					setfocus();
				}
			}
			else
			{
				AutoClosingMessageBox.Show("未選擇商品");
				setfocus();
			}
		}

		private void onesubcount_Click(object sender, EventArgs e)
		{
			if (infolist.CurrentRow != null)
			{
				string[] strWhereParameterArray = new string[1]
				{
					infolist.CurrentRow.Cells[8].Value.ToString()
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "OpenPrice", "hypos_GOODSLST", "GDSNO = {0}  ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows[0]["OpenPrice"].ToString().Equals("1"))
				{
					try
					{
						int num = int.Parse(textBox1.Text);
						if (int.Parse(infolist.CurrentRow.Cells["subtotal"].Value.ToString()) - num > 0)
						{
							int.Parse(infolist.CurrentRow.Cells["subtotal"].Value.ToString());
						}
						infolist.CurrentRow.Cells["discount"].Value = num.ToString();
						textBox1.Text = "";
						computetotalmoney("", true);
						alertMessage("單項折讓");
					}
					catch (Exception)
					{
						AutoClosingMessageBox.Show("輸入單項折讓數錯誤");
						textBox1.Text = "";
						setfocus();
					}
				}
				else
				{
					AutoClosingMessageBox.Show("此商品未開放單項折讓");
					setfocus();
				}
			}
			else
			{
				AutoClosingMessageBox.Show("未選擇商品");
				setfocus();
			}
		}

		public string gettotalspending()
		{
			return summoney.Text;
		}

		public string gettotalitems()
		{
			return totalitems;
		}

		public string getsumitems()
		{
			return sumitems;
		}

		private string memberType(string type)
		{
			string text = "";
			if ("2".Equals(type))
			{
				return "SpecialPrice1";
			}
			if ("3".Equals(type))
			{
				return "SpecialPrice2";
			}
			return "setprice";
		}

		private void commodityEnter()
		{
			try
			{
				if (textBox1.Text.Trim().Length != 0)
				{
					alertMessage("選入商品");
					bool flag = false;
					string text = textBox1.Text.Trim();
					string[] strWhereParameterArray;
					if (text.Length > 13)
					{
						strWhereParameterArray = new string[1]
						{
							text.Substring(0, 13)
						};
						flag = true;
					}
					else
					{
						strWhereParameterArray = new string[1]
						{
							text
						};
					}
					string strTableName = "hypos_GOODSLST as hg left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo";
					string strWhereClause = "(hg.GDSNO ={0} or hg.hot_key ={0}) AND ((hg.ISWS ='Y' and hg.CLA1NO ='0302' and hg.licType = hl.licType and hg.domManufId = hl.licNo) OR (hg.ISWS ='N' and hg.CLA1NO ='0302') OR hg.CLA1NO ='0303' OR hg.CLA1NO ='0305' OR hg.CLA1NO ='0308') AND (hl.isDelete='N' or hl.isDelete is null)   ";
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hg.*", strTableName, strWhereClause, "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
					string text2 = "";
					if (dataTable.Rows.Count > 0)
					{
						text2 = dataTable.Rows[0]["GDSNO"].ToString();
					}
					text = text2;
					if (dataTable.Rows.Count > 0)
					{
						string[] array = new string[5]
						{
							"0",
							"0",
							"0",
							"0",
							"0"
						};
						string[] array2 = text.Split(new string[1]
						{
							"-"
						}, StringSplitOptions.RemoveEmptyEntries);
						DataTable dataTable2 = null;
						if (flag && array2.Length > 2)
						{
							array[0] = dataTable.Rows[0]["pesticideId"].ToString();
							array[1] = dataTable.Rows[0]["formCode"].ToString();
							array[2] = dataTable.Rows[0]["contents"].ToString();
							array[3] = array2[1];
							array[4] = array2[2];
							dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "HyScope", " pesticideId = {0} AND formCode = {1} AND contents = {2} AND cropId = {3} AND pestId = {4} AND isDelete in ('N','') ", "", null, array, CommandOperationType.ExecuteReaderReturnDataTable);
						}
						else
						{
							array[0] = dataTable.Rows[0]["pesticideId"].ToString();
							array[1] = dataTable.Rows[0]["formCode"].ToString();
							array[2] = dataTable.Rows[0]["contents"].ToString();
							dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "HyScope", " pesticideId = {0} AND formCode = {1} AND contents = {2} ", "", null, array, CommandOperationType.ExecuteReaderReturnDataTable);
						}
						if (dataTable2.Rows.Count > 0 || "N".Equals(dataTable.Rows[0]["ISWS"].ToString()) || ("0303".Equals(dataTable.Rows[0]["CLA1NO"].ToString()) && "Y".Equals(dataTable.Rows[0]["ISWS"].ToString())))
						{
							bool flag2 = true;
							string text3 = "購肥商品暫時不開放";
							if ("0303".Equals(dataTable.Rows[0]["CLA1NO"].ToString()) && "Y".Equals(dataTable.Rows[0]["ISWS"].ToString()))
							{
								flag2 = (Program.IsFertilizer & Program.IsSaleOfFertilizer);
							}
							if (!"D".Equals(dataTable.Rows[0]["status"].ToString()) && flag2)
							{
								string value = "";
								string value2 = "";
								foreach (DataRow row in dataTable.Rows)
								{
									value = row["CLA1NO"].ToString();
									value2 = row["ISWS"].ToString();
									string text4 = row["Price"].ToString();
									if (string.IsNullOrEmpty(text4))
									{
										text4 = "0";
									}
									int num = 0;
									num = int.Parse(text4);
									uc = new CommodityInfo();
									uc.setMemberIdNo("");
									uc.setMemberVipNo("店內碼:" + row["GDSNO"].ToString());
									uc.setCommodityName(setCommodityName(row));
									uc.setCommodityClass(row["spec"].ToString() + " " + row["capacity"].ToString());
									if ("N".Equals(value2))
									{
										uc.setlabe1("");
									}
									else if ("0303".Equals(row["CLA1NO"].ToString()) && "Y".Equals(row["ISWS"].ToString()) && "Y".Equals(row["SubsidyFertilizer"].ToString()))
									{
										uc.setlabe1("補助金額: " + row["SubsidyMoney"].ToString());
									}
									if (!flag)
									{
										string text5 = row["Price"].ToString();
										if (string.IsNullOrEmpty(text5))
										{
											text5 = "0";
										}
										infolist.Rows.Add(infolist.RowCount + 1, uc, text5, text5, "1", num.ToString(), "0", num.ToString(), text, "", "", row["SpecialPrice1"].ToString(), row["SpecialPrice2"].ToString(), "", row["SubsidyFertilizer"].ToString(), row["SubsidyMoney"].ToString(), row["ISWS"].ToString(), row["CLA1NO"].ToString());
										frmExtendScreen.setCommodityInfo(new string[4]
										{
											setCommodityName(row),
											text5,
											"1",
											text5
										});
										foreach (DataGridViewRow item in (IEnumerable)infolist.Rows)
										{
											item.Height = 100;
										}
										infolist.Rows[0].Selected = false;
									}
									foreach (string item2 in barcodelist)
									{
										if (item2.Equals(text))
										{
											same = true;
										}
									}
									if (!same)
									{
										barcodelist.Add(text);
									}
									same = false;
								}
								computetotalmoney("Enter", true);
								if ("0302".Equals(value) && "Y".Equals(value2))
								{
									int count = infolist.Rows.Count;
									if (flag)
									{
										try
										{
											if (textBox1.Text.Trim().Length != 0)
											{
												text = textBox1.Text.Trim();
												string[] array3 = text.Split(new string[1]
												{
													"-"
												}, StringSplitOptions.RemoveEmptyEntries);
												combinationBarcode(dataTable, array3[1], array3[2]);
											}
										}
										catch (Exception)
										{
											AutoClosingMessageBox.Show("商品不存在，請重新輸入");
											this.num.Text = "";
										}
									}
									else
									{
										new frmDialogMedNew(this, count, text).ShowDialog();
									}
								}
								else if (flag)
								{
									AutoClosingMessageBox.Show("商品不存在，請重新輸入");
									this.num.Text = "";
								}
							}
							else
							{
								if (flag2)
								{
									AutoClosingMessageBox.Show("商品已禁賣，請重新輸入");
								}
								else
								{
									AutoClosingMessageBox.Show(text3);
								}
								this.num.Text = "";
							}
						}
						else
						{
							string[] array4 = text.Split(new string[1]
							{
								"-"
							}, StringSplitOptions.RemoveEmptyEntries);
							if (array4.Length > 2)
							{
								string[] strWhereParameterArray2 = new string[1]
								{
									array4[1]
								};
								DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyCrop", " code = {0}  ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
								string[] strWhereParameterArray3 = new string[1]
								{
									array4[2]
								};
								DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyBlight", " code = {0}  ", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
								if (dataTable3.Rows.Count > 0 && dataTable4.Rows.Count > 0)
								{
									MessageBox.Show("此[" + dataTable3.Rows[0]["name"].ToString() + " x " + dataTable4.Rows[0]["name"].ToString() + "]配對已不存在，請選擇其他配對。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								}
								else
								{
									AutoClosingMessageBox.Show("商品不存在，請重新輸入");
								}
							}
							else
							{
								AutoClosingMessageBox.Show("商品不存在，請重新輸入");
							}
						}
					}
					else
					{
						AutoClosingMessageBox.Show("商品不存在，請重新輸入");
						this.num.Text = "";
					}
					textBox1.Text = textBox1.Text.Replace(Environment.NewLine, "");
					textBox1.Text = "";
					columnOfFocus = 1;
				}
				else
				{
					AutoClosingMessageBox.Show("商品不存在，請重新輸入");
				}
			}
			catch (Exception)
			{
			}
		}

		private void combinationBarcode(DataTable dt, string cropCode, string pestCode)
		{
			string text = DateTime.Now.ToString("yyyyMMdd");
			bool flag = false;
			string[] strWhereParameterArray = new string[6]
			{
				dt.Rows[0]["pesticideId"].ToString(),
				dt.Rows[0]["formCode"].ToString(),
				dt.Rows[0]["contents"].ToString(),
				cropCode,
				pestCode,
				text
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hl.licType,hl.licNo,hl.domManufName", "HyScope as hs,HyLicence as hl", "hs.pesticideId = {0} and hl.pesticideId = hs.pesticideId  and hs.formCode ={1} and hl.formCode = hs.formCode and hl.contents = hs.contents  and hs.contents={2} and hs.cropId ={3} and hs.pestId={4} and hs.approveDate != '' and (hs.approveDate +19190000) >=CAST ({5} as INTEGER) and hs.regStoreName !='' and  hs.regStoreName !='99999999' and hl.domManufId = hs.regStoreName ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			string[] strWhereParameterArray2 = new string[1]
			{
				cropCode
			};
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyCrop", "code = {0}", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
			strWhereParameterArray2 = new string[1]
			{
				pestCode
			};
			DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyBlight", "code = {0}", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					if (row["licType"].ToString().Equals(dt.Rows[0]["licType"].ToString()) && row["licNo"].ToString().Equals(dt.Rows[0]["domManufId"].ToString()))
					{
						flag = true;
					}
				}
				if (flag)
				{
					addOnecommodity(dt.Rows[0]["GDSNO"].ToString(), cropCode, pestCode, dataTable2.Rows[0]["name"].ToString(), dataTable3.Rows[0]["name"].ToString());
				}
				else
				{
					AutoClosingMessageBox.Show("此用藥配對尚於資料保護期間");
				}
			}
			else
			{
				addOnecommodity(dt.Rows[0]["GDSNO"].ToString(), cropCode, pestCode, dataTable2.Rows[0]["name"].ToString(), dataTable3.Rows[0]["name"].ToString());
			}
		}

		private void infolist_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			infolist.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (frmExtendScreen.OnlyInstance.Visible)
			{
				if (frmExtendScreen.OnlyInstance != null)
				{
					frmExtendScreen.OnlyInstance.Hide();
				}
			}
			else if (frmExtendScreen.OnlyInstance != null)
			{
				frmExtendScreen.OnlyInstance.Show();
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
			textBox1 = new System.Windows.Forms.TextBox();
			num = new System.Windows.Forms.TextBox();
			l_sysTime = new System.Windows.Forms.Label();
			infolist = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			commodity = new POS_Client.frmMainShopSimple.CustomColumn();
			setprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			sellingprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
			barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cropId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			pestId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			SpecialPrice1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			SpecialPrice2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			openPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			subsidyFertilizer = new System.Windows.Forms.DataGridViewTextBoxColumn();
			subsidyMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
			ISWS = new System.Windows.Forms.DataGridViewTextBoxColumn();
			CLA1NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
			addone = new System.Windows.Forms.Button();
			subone = new System.Windows.Forms.Button();
			numone = new System.Windows.Forms.Button();
			pressEnter = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			textBox3 = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			textBox2 = new System.Windows.Forms.TextBox();
			summoney = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			flp_chooseMember = new System.Windows.Forms.FlowLayoutPanel();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			label2 = new System.Windows.Forms.Label();
			alertMsg = new System.Windows.Forms.TextBox();
			totalspending = new System.Windows.Forms.Label();
			totalpriceDiscount = new System.Windows.Forms.Label();
			totalpriceDiscountView = new System.Windows.Forms.Label();
			totalprice = new System.Windows.Forms.Label();
			totalpriceview = new System.Windows.Forms.Label();
			backspace = new System.Windows.Forms.Button();
			pre = new System.Windows.Forms.Button();
			oneremove = new System.Windows.Forms.Button();
			next = new System.Windows.Forms.Button();
			numzero = new System.Windows.Forms.Button();
			numtwo = new System.Windows.Forms.Button();
			numthree = new System.Windows.Forms.Button();
			numfour = new System.Windows.Forms.Button();
			numsix = new System.Windows.Forms.Button();
			numfive = new System.Windows.Forms.Button();
			numseven = new System.Windows.Forms.Button();
			numeight = new System.Windows.Forms.Button();
			numnine = new System.Windows.Forms.Button();
			removeall = new System.Windows.Forms.Button();
			clearenter = new System.Windows.Forms.Button();
			commoditySearch = new System.Windows.Forms.Button();
			cureGuide = new System.Windows.Forms.Button();
			Checkout = new System.Windows.Forms.Button();
			onesubcount = new System.Windows.Forms.Button();
			disodd = new System.Windows.Forms.Button();
			distotalprice = new System.Windows.Forms.Button();
			onediscount = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			((System.ComponentModel.ISupportInitialize)infolist).BeginInit();
			panel1.SuspendLayout();
			flp_chooseMember.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			pb_virtualKeyBoard.Location = new System.Drawing.Point(898, 620);
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 7);
			textBox1.Font = new System.Drawing.Font("Calibri", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			textBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
			textBox1.Location = new System.Drawing.Point(318, 14);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(405, 41);
			textBox1.TabIndex = 2;
			textBox1.Enter += new System.EventHandler(textBox1_Enter);
			num.Font = new System.Drawing.Font("Calibri", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			num.Location = new System.Drawing.Point(318, 73);
			num.Multiline = true;
			num.Name = "num";
			num.ReadOnly = true;
			num.Size = new System.Drawing.Size(111, 42);
			num.TabIndex = 3;
			num.Enter += new System.EventHandler(num_Focus);
			num.KeyDown += new System.Windows.Forms.KeyEventHandler(numEnter);
			l_sysTime.AutoSize = true;
			l_sysTime.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_sysTime.Location = new System.Drawing.Point(435, 83);
			l_sysTime.Name = "l_sysTime";
			l_sysTime.Size = new System.Drawing.Size(19, 20);
			l_sysTime.TabIndex = 5;
			l_sysTime.Text = "X";
			infolist.AllowUserToAddRows = false;
			infolist.AllowUserToDeleteRows = false;
			infolist.AllowUserToResizeColumns = false;
			infolist.AllowUserToResizeRows = false;
			infolist.Anchor = System.Windows.Forms.AnchorStyles.None;
			infolist.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			infolist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			infolist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			infolist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(175, 164, 134);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			infolist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			infolist.Columns.AddRange(Column1, commodity, setprice, sellingprice, quantity, subtotal, discount, sum, barcode, cropId, pestId, SpecialPrice1, SpecialPrice2, openPrice, subsidyFertilizer, subsidyMoney, ISWS, CLA1NO);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			infolist.DefaultCellStyle = dataGridViewCellStyle2;
			infolist.EnableHeadersVisualStyles = false;
			infolist.GridColor = System.Drawing.SystemColors.ActiveBorder;
			infolist.Location = new System.Drawing.Point(21, 247);
			infolist.MultiSelect = false;
			infolist.Name = "infolist";
			infolist.ReadOnly = true;
			infolist.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(254, 234, 225);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			infolist.RowHeadersVisible = false;
			infolist.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			infolist.RowTemplate.Height = 102;
			infolist.RowTemplate.ReadOnly = true;
			infolist.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			infolist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			infolist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			infolist.Size = new System.Drawing.Size(754, 408);
			infolist.TabIndex = 9;
			infolist.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(infolist_RowPostPaint);
			infolist.SelectionChanged += new System.EventHandler(infolist_SelectionChanged);
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			Column1.DefaultCellStyle = dataGridViewCellStyle4;
			Column1.HeaderText = "項次";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 43;
			commodity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
			commodity.DefaultCellStyle = dataGridViewCellStyle5;
			commodity.HeaderText = "商品名稱";
			commodity.Name = "commodity";
			commodity.ReadOnly = true;
			commodity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			setprice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			setprice.DefaultCellStyle = dataGridViewCellStyle6;
			setprice.HeaderText = "售價";
			setprice.Name = "setprice";
			setprice.ReadOnly = true;
			setprice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			setprice.Width = 43;
			sellingprice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			sellingprice.DefaultCellStyle = dataGridViewCellStyle7;
			sellingprice.HeaderText = "優惠價";
			sellingprice.Name = "sellingprice";
			sellingprice.ReadOnly = true;
			sellingprice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			sellingprice.Width = 58;
			quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			quantity.DefaultCellStyle = dataGridViewCellStyle8;
			quantity.HeaderText = "數量";
			quantity.Name = "quantity";
			quantity.ReadOnly = true;
			quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			quantity.Width = 43;
			subtotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			subtotal.DefaultCellStyle = dataGridViewCellStyle9;
			subtotal.HeaderText = "小計";
			subtotal.Name = "subtotal";
			subtotal.ReadOnly = true;
			subtotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			subtotal.Width = 43;
			discount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			discount.DefaultCellStyle = dataGridViewCellStyle10;
			discount.HeaderText = "折讓";
			discount.Name = "discount";
			discount.ReadOnly = true;
			discount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			discount.Width = 43;
			sum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			sum.DefaultCellStyle = dataGridViewCellStyle11;
			sum.HeaderText = "合計";
			sum.Name = "sum";
			sum.ReadOnly = true;
			sum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			sum.Width = 43;
			barcode.HeaderText = "條碼";
			barcode.Name = "barcode";
			barcode.ReadOnly = true;
			barcode.Visible = false;
			barcode.Width = 62;
			cropId.HeaderText = "作物id";
			cropId.Name = "cropId";
			cropId.ReadOnly = true;
			cropId.Visible = false;
			cropId.Width = 76;
			pestId.HeaderText = "蟲害id";
			pestId.Name = "pestId";
			pestId.ReadOnly = true;
			pestId.Visible = false;
			pestId.Width = 76;
			SpecialPrice1.HeaderText = "優惠價1";
			SpecialPrice1.Name = "SpecialPrice1";
			SpecialPrice1.ReadOnly = true;
			SpecialPrice1.Visible = false;
			SpecialPrice2.HeaderText = "優惠價2";
			SpecialPrice2.Name = "SpecialPrice2";
			SpecialPrice2.ReadOnly = true;
			SpecialPrice2.Visible = false;
			openPrice.HeaderText = "開放售價";
			openPrice.Name = "openPrice";
			openPrice.ReadOnly = true;
			openPrice.Visible = false;
			subsidyFertilizer.HeaderText = "補助肥料";
			subsidyFertilizer.Name = "subsidyFertilizer";
			subsidyFertilizer.ReadOnly = true;
			subsidyFertilizer.Visible = false;
			subsidyMoney.HeaderText = "補助(金額)";
			subsidyMoney.Name = "subsidyMoney";
			subsidyMoney.ReadOnly = true;
			subsidyMoney.Visible = false;
			ISWS.HeaderText = "介接";
			ISWS.Name = "ISWS";
			ISWS.ReadOnly = true;
			ISWS.Visible = false;
			CLA1NO.HeaderText = "商品類型";
			CLA1NO.Name = "CLA1NO";
			CLA1NO.ReadOnly = true;
			CLA1NO.Visible = false;
			addone.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			addone.Location = new System.Drawing.Point(790, 333);
			addone.Name = "addone";
			addone.Size = new System.Drawing.Size(40, 40);
			addone.TabIndex = 10;
			addone.Text = "+1";
			addone.UseVisualStyleBackColor = true;
			addone.Click += new System.EventHandler(addone_Click);
			subone.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			subone.Location = new System.Drawing.Point(790, 379);
			subone.Name = "subone";
			subone.Size = new System.Drawing.Size(40, 40);
			subone.TabIndex = 11;
			subone.Text = "-1";
			subone.UseVisualStyleBackColor = true;
			subone.Click += new System.EventHandler(subone_Click);
			numone.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numone.Location = new System.Drawing.Point(837, 425);
			numone.Name = "numone";
			numone.Size = new System.Drawing.Size(40, 40);
			numone.TabIndex = 12;
			numone.Text = "1";
			numone.UseVisualStyleBackColor = true;
			numone.Click += new System.EventHandler(number_Click);
			pressEnter.BackColor = System.Drawing.Color.FromArgb(167, 202, 0);
			pressEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			pressEnter.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			pressEnter.ForeColor = System.Drawing.Color.White;
			pressEnter.Location = new System.Drawing.Point(883, 517);
			pressEnter.Name = "pressEnter";
			pressEnter.Size = new System.Drawing.Size(88, 63);
			pressEnter.TabIndex = 13;
			pressEnter.Text = "確認\r\n輸入";
			pressEnter.UseVisualStyleBackColor = false;
			pressEnter.Click += new System.EventHandler(pressEnter_Click);
			panel1.BackColor = System.Drawing.Color.White;
			panel1.Controls.Add(textBox3);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(textBox2);
			panel1.Controls.Add(summoney);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(flp_chooseMember);
			panel1.Controls.Add(alertMsg);
			panel1.Controls.Add(totalspending);
			panel1.Controls.Add(totalpriceDiscount);
			panel1.Controls.Add(totalpriceDiscountView);
			panel1.Controls.Add(totalprice);
			panel1.Controls.Add(totalpriceview);
			panel1.Controls.Add(num);
			panel1.Controls.Add(textBox1);
			panel1.Controls.Add(l_sysTime);
			panel1.Location = new System.Drawing.Point(23, 50);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(752, 191);
			panel1.TabIndex = 14;
			panel1.Paint += new System.Windows.Forms.PaintEventHandler(panal1_Paint);
			textBox3.Font = new System.Drawing.Font("Calibri", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			textBox3.Location = new System.Drawing.Point(612, 73);
			textBox3.Multiline = true;
			textBox3.Name = "textBox3";
			textBox3.ReadOnly = true;
			textBox3.Size = new System.Drawing.Size(111, 42);
			textBox3.TabIndex = 17;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label5.Location = new System.Drawing.Point(582, 80);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(24, 24);
			label5.TabIndex = 16;
			label5.Text = "=";
			textBox2.Font = new System.Drawing.Font("Calibri", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			textBox2.Location = new System.Drawing.Point(465, 73);
			textBox2.Multiline = true;
			textBox2.Name = "textBox2";
			textBox2.ReadOnly = true;
			textBox2.Size = new System.Drawing.Size(111, 42);
			textBox2.TabIndex = 15;
			summoney.AutoSize = true;
			summoney.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			summoney.Location = new System.Drawing.Point(212, 142);
			summoney.Name = "summoney";
			summoney.Size = new System.Drawing.Size(24, 27);
			summoney.TabIndex = 14;
			summoney.Text = "0";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			label4.Location = new System.Drawing.Point(209, 115);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(64, 18);
			label4.TabIndex = 13;
			label4.Text = "消費總額";
			label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			label3.Location = new System.Drawing.Point(298, 0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(2, 191);
			label3.TabIndex = 12;
			label3.Text = "label3";
			label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			label1.Location = new System.Drawing.Point(3, 113);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(280, 2);
			label1.TabIndex = 11;
			label1.Text = "label1";
			flp_chooseMember.Controls.Add(pictureBox1);
			flp_chooseMember.Controls.Add(label2);
			flp_chooseMember.Cursor = System.Windows.Forms.Cursors.Hand;
			flp_chooseMember.Location = new System.Drawing.Point(3, 1);
			flp_chooseMember.Margin = new System.Windows.Forms.Padding(0);
			flp_chooseMember.Name = "flp_chooseMember";
			flp_chooseMember.Size = new System.Drawing.Size(292, 105);
			flp_chooseMember.TabIndex = 10;
			flp_chooseMember.Click += new System.EventHandler(flp_chooseMember_Click);
			pictureBox1.Image = POS_Client.Properties.Resources.more;
			pictureBox1.Location = new System.Drawing.Point(0, 0);
			pictureBox1.Margin = new System.Windows.Forms.Padding(0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(61, 65);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			pictureBox1.TabIndex = 2;
			pictureBox1.TabStop = false;
			pictureBox1.Click += new System.EventHandler(flp_chooseMember_Click);
			label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.Location = new System.Drawing.Point(71, 15);
			label2.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(123, 35);
			label2.TabIndex = 3;
			label2.Text = "選擇會員";
			label2.Click += new System.EventHandler(flp_chooseMember_Click);
			alertMsg.Font = new System.Drawing.Font("微軟正黑體", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			alertMsg.Location = new System.Drawing.Point(318, 135);
			alertMsg.Multiline = true;
			alertMsg.Name = "alertMsg";
			alertMsg.ReadOnly = true;
			alertMsg.Size = new System.Drawing.Size(405, 41);
			alertMsg.TabIndex = 6;
			totalspending.AutoSize = true;
			totalspending.Font = new System.Drawing.Font("Arial", 32f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			totalspending.ForeColor = System.Drawing.Color.Orange;
			totalspending.Location = new System.Drawing.Point(206, 135);
			totalspending.Name = "totalspending";
			totalspending.Size = new System.Drawing.Size(0, 36);
			totalspending.TabIndex = 5;
			totalpriceDiscount.AutoSize = true;
			totalpriceDiscount.Font = new System.Drawing.Font("Arial", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			totalpriceDiscount.Location = new System.Drawing.Point(93, 145);
			totalpriceDiscount.Name = "totalpriceDiscount";
			totalpriceDiscount.Size = new System.Drawing.Size(0, 24);
			totalpriceDiscount.TabIndex = 3;
			totalpriceDiscountView.AutoSize = true;
			totalpriceDiscountView.Font = new System.Drawing.Font("微軟正黑體", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			totalpriceDiscountView.Location = new System.Drawing.Point(94, 115);
			totalpriceDiscountView.Name = "totalpriceDiscountView";
			totalpriceDiscountView.Size = new System.Drawing.Size(86, 18);
			totalpriceDiscountView.TabIndex = 2;
			totalpriceDiscountView.Text = "總價折讓(扣)";
			totalprice.AutoSize = true;
			totalprice.Font = new System.Drawing.Font("Arial", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			totalprice.Location = new System.Drawing.Point(2, 144);
			totalprice.Name = "totalprice";
			totalprice.Size = new System.Drawing.Size(0, 24);
			totalprice.TabIndex = 1;
			totalpriceview.AutoSize = true;
			totalpriceview.Font = new System.Drawing.Font("微軟正黑體", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			totalpriceview.Location = new System.Drawing.Point(13, 115);
			totalpriceview.Name = "totalpriceview";
			totalpriceview.Size = new System.Drawing.Size(36, 18);
			totalpriceview.TabIndex = 0;
			totalpriceview.Text = "總價";
			backspace.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			backspace.Location = new System.Drawing.Point(884, 471);
			backspace.Name = "backspace";
			backspace.Size = new System.Drawing.Size(88, 40);
			backspace.TabIndex = 15;
			backspace.Text = "backspace";
			backspace.UseVisualStyleBackColor = true;
			backspace.Click += new System.EventHandler(backspace_Click);
			pre.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			pre.Location = new System.Drawing.Point(790, 425);
			pre.Name = "pre";
			pre.Size = new System.Drawing.Size(40, 40);
			pre.TabIndex = 16;
			pre.Text = "↑";
			pre.UseVisualStyleBackColor = true;
			pre.Click += new System.EventHandler(pre_Click);
			oneremove.BackColor = System.Drawing.Color.FromArgb(162, 162, 162);
			oneremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			oneremove.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			oneremove.ForeColor = System.Drawing.Color.Transparent;
			oneremove.Location = new System.Drawing.Point(791, 257);
			oneremove.Name = "oneremove";
			oneremove.Size = new System.Drawing.Size(88, 63);
			oneremove.TabIndex = 18;
			oneremove.Text = "單筆\r\n移除";
			oneremove.UseVisualStyleBackColor = false;
			oneremove.Click += new System.EventHandler(oneremove_Click);
			next.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			next.Location = new System.Drawing.Point(790, 471);
			next.Name = "next";
			next.Size = new System.Drawing.Size(40, 40);
			next.TabIndex = 22;
			next.Text = "↓";
			next.UseVisualStyleBackColor = true;
			next.Click += new System.EventHandler(next_Click);
			numzero.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numzero.Location = new System.Drawing.Point(837, 471);
			numzero.Name = "numzero";
			numzero.Size = new System.Drawing.Size(40, 40);
			numzero.TabIndex = 23;
			numzero.Text = "0";
			numzero.UseVisualStyleBackColor = true;
			numzero.Click += new System.EventHandler(number_Click);
			numtwo.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numtwo.Location = new System.Drawing.Point(884, 425);
			numtwo.Name = "numtwo";
			numtwo.Size = new System.Drawing.Size(40, 40);
			numtwo.TabIndex = 24;
			numtwo.Text = "2";
			numtwo.UseVisualStyleBackColor = true;
			numtwo.Click += new System.EventHandler(number_Click);
			numthree.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numthree.Location = new System.Drawing.Point(931, 425);
			numthree.Name = "numthree";
			numthree.Size = new System.Drawing.Size(40, 40);
			numthree.TabIndex = 25;
			numthree.Text = "3";
			numthree.UseVisualStyleBackColor = true;
			numthree.Click += new System.EventHandler(number_Click);
			numfour.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfour.Location = new System.Drawing.Point(837, 379);
			numfour.Name = "numfour";
			numfour.Size = new System.Drawing.Size(40, 40);
			numfour.TabIndex = 26;
			numfour.Text = "4";
			numfour.UseVisualStyleBackColor = true;
			numfour.Click += new System.EventHandler(number_Click);
			numsix.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numsix.Location = new System.Drawing.Point(931, 379);
			numsix.Name = "numsix";
			numsix.Size = new System.Drawing.Size(40, 40);
			numsix.TabIndex = 27;
			numsix.Text = "6";
			numsix.UseVisualStyleBackColor = true;
			numsix.Click += new System.EventHandler(number_Click);
			numfive.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfive.Location = new System.Drawing.Point(884, 379);
			numfive.Name = "numfive";
			numfive.Size = new System.Drawing.Size(40, 40);
			numfive.TabIndex = 28;
			numfive.Text = "5";
			numfive.UseVisualStyleBackColor = true;
			numfive.Click += new System.EventHandler(number_Click);
			numseven.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numseven.Location = new System.Drawing.Point(837, 333);
			numseven.Name = "numseven";
			numseven.Size = new System.Drawing.Size(40, 40);
			numseven.TabIndex = 29;
			numseven.Text = "7";
			numseven.UseVisualStyleBackColor = true;
			numseven.Click += new System.EventHandler(number_Click);
			numeight.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numeight.Location = new System.Drawing.Point(884, 333);
			numeight.Name = "numeight";
			numeight.Size = new System.Drawing.Size(40, 40);
			numeight.TabIndex = 30;
			numeight.Text = "8";
			numeight.UseVisualStyleBackColor = true;
			numeight.Click += new System.EventHandler(number_Click);
			numnine.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numnine.Location = new System.Drawing.Point(931, 333);
			numnine.Name = "numnine";
			numnine.Size = new System.Drawing.Size(40, 40);
			numnine.TabIndex = 31;
			numnine.Text = "9";
			numnine.UseVisualStyleBackColor = true;
			numnine.Click += new System.EventHandler(number_Click);
			removeall.BackColor = System.Drawing.Color.FromArgb(162, 162, 162);
			removeall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			removeall.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			removeall.ForeColor = System.Drawing.Color.White;
			removeall.Location = new System.Drawing.Point(887, 257);
			removeall.Name = "removeall";
			removeall.Size = new System.Drawing.Size(88, 63);
			removeall.TabIndex = 32;
			removeall.Text = "全部\r\n移除";
			removeall.UseVisualStyleBackColor = false;
			removeall.Click += new System.EventHandler(removeall_Click);
			clearenter.BackColor = System.Drawing.Color.FromArgb(192, 182, 154);
			clearenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			clearenter.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			clearenter.ForeColor = System.Drawing.Color.White;
			clearenter.Location = new System.Drawing.Point(790, 517);
			clearenter.Name = "clearenter";
			clearenter.Size = new System.Drawing.Size(88, 63);
			clearenter.TabIndex = 33;
			clearenter.Text = "清除\r\n輸入";
			clearenter.UseVisualStyleBackColor = false;
			clearenter.Click += new System.EventHandler(clearenter_Click);
			commoditySearch.BackColor = System.Drawing.Color.FromArgb(156, 187, 58);
			commoditySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			commoditySearch.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			commoditySearch.ForeColor = System.Drawing.Color.White;
			commoditySearch.Location = new System.Drawing.Point(792, 50);
			commoditySearch.Name = "commoditySearch";
			commoditySearch.Size = new System.Drawing.Size(89, 66);
			commoditySearch.TabIndex = 34;
			commoditySearch.Text = "商品\r\n查詢";
			commoditySearch.UseVisualStyleBackColor = false;
			commoditySearch.Click += new System.EventHandler(commoditySearch_Click);
			cureGuide.BackColor = System.Drawing.Color.FromArgb(156, 187, 58);
			cureGuide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cureGuide.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			cureGuide.ForeColor = System.Drawing.Color.White;
			cureGuide.Location = new System.Drawing.Point(887, 50);
			cureGuide.Name = "cureGuide";
			cureGuide.Size = new System.Drawing.Size(89, 66);
			cureGuide.TabIndex = 35;
			cureGuide.Text = "用藥\r\n指引";
			cureGuide.UseVisualStyleBackColor = false;
			cureGuide.Click += new System.EventHandler(cureGuide_Click);
			Checkout.BackColor = System.Drawing.Color.FromArgb(250, 87, 0);
			Checkout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			Checkout.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			Checkout.ForeColor = System.Drawing.Color.White;
			Checkout.Location = new System.Drawing.Point(790, 586);
			Checkout.Name = "Checkout";
			Checkout.Size = new System.Drawing.Size(183, 73);
			Checkout.TabIndex = 36;
			Checkout.Text = "收銀\r\n結帳";
			Checkout.UseVisualStyleBackColor = false;
			Checkout.Click += new System.EventHandler(Checkout_Click);
			onesubcount.BackColor = System.Drawing.Color.FromArgb(89, 124, 14);
			onesubcount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			onesubcount.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			onesubcount.ForeColor = System.Drawing.Color.White;
			onesubcount.Location = new System.Drawing.Point(887, 192);
			onesubcount.Name = "onesubcount";
			onesubcount.Size = new System.Drawing.Size(88, 63);
			onesubcount.TabIndex = 55;
			onesubcount.Text = "單項\r\n折讓";
			onesubcount.UseVisualStyleBackColor = false;
			onesubcount.Click += new System.EventHandler(onesubcount_Click);
			disodd.BackColor = System.Drawing.Color.FromArgb(89, 124, 14);
			disodd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			disodd.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			disodd.ForeColor = System.Drawing.Color.White;
			disodd.Location = new System.Drawing.Point(887, 123);
			disodd.Name = "disodd";
			disodd.Size = new System.Drawing.Size(88, 63);
			disodd.TabIndex = 54;
			disodd.Text = "總價\r\n折讓";
			disodd.UseVisualStyleBackColor = false;
			disodd.Click += new System.EventHandler(disodd_Click);
			distotalprice.BackColor = System.Drawing.Color.FromArgb(89, 124, 14);
			distotalprice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			distotalprice.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			distotalprice.ForeColor = System.Drawing.Color.White;
			distotalprice.Location = new System.Drawing.Point(792, 123);
			distotalprice.Name = "distotalprice";
			distotalprice.Size = new System.Drawing.Size(88, 63);
			distotalprice.TabIndex = 53;
			distotalprice.Text = "總價\r\n折扣";
			distotalprice.UseVisualStyleBackColor = false;
			distotalprice.Click += new System.EventHandler(distotalprice_Click);
			onediscount.BackColor = System.Drawing.Color.FromArgb(89, 124, 14);
			onediscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			onediscount.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			onediscount.ForeColor = System.Drawing.Color.White;
			onediscount.Location = new System.Drawing.Point(792, 192);
			onediscount.Name = "onediscount";
			onediscount.Size = new System.Drawing.Size(88, 63);
			onediscount.TabIndex = 52;
			onediscount.Text = "開放\r\n售價";
			onediscount.UseVisualStyleBackColor = false;
			onediscount.Click += new System.EventHandler(onediscount_Click);
			button1.BackColor = System.Drawing.Color.DarkGray;
			button1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			button1.ForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
			button1.Location = new System.Drawing.Point(601, 0);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(174, 36);
			button1.TabIndex = 56;
			button1.Text = "開啟/隱藏子螢幕";
			button1.UseVisualStyleBackColor = false;
			button1.Visible = false;
			button1.Click += new System.EventHandler(button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(252, 252, 237);
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(981, 671);
			base.Controls.Add(button1);
			base.Controls.Add(onesubcount);
			base.Controls.Add(disodd);
			base.Controls.Add(distotalprice);
			base.Controls.Add(onediscount);
			base.Controls.Add(Checkout);
			base.Controls.Add(cureGuide);
			base.Controls.Add(commoditySearch);
			base.Controls.Add(clearenter);
			base.Controls.Add(removeall);
			base.Controls.Add(numnine);
			base.Controls.Add(numeight);
			base.Controls.Add(numseven);
			base.Controls.Add(numfive);
			base.Controls.Add(numsix);
			base.Controls.Add(numfour);
			base.Controls.Add(numthree);
			base.Controls.Add(numtwo);
			base.Controls.Add(numzero);
			base.Controls.Add(next);
			base.Controls.Add(oneremove);
			base.Controls.Add(pre);
			base.Controls.Add(backspace);
			base.Controls.Add(pressEnter);
			base.Controls.Add(numone);
			base.Controls.Add(subone);
			base.Controls.Add(addone);
			base.Controls.Add(infolist);
			base.Controls.Add(panel1);
			base.Name = "frmMainShopSimpleWithMoney";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "frmMainShop";
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(Form1_KeyDown);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(infolist, 0);
			base.Controls.SetChildIndex(addone, 0);
			base.Controls.SetChildIndex(subone, 0);
			base.Controls.SetChildIndex(numone, 0);
			base.Controls.SetChildIndex(pressEnter, 0);
			base.Controls.SetChildIndex(backspace, 0);
			base.Controls.SetChildIndex(pre, 0);
			base.Controls.SetChildIndex(oneremove, 0);
			base.Controls.SetChildIndex(next, 0);
			base.Controls.SetChildIndex(numzero, 0);
			base.Controls.SetChildIndex(numtwo, 0);
			base.Controls.SetChildIndex(numthree, 0);
			base.Controls.SetChildIndex(numfour, 0);
			base.Controls.SetChildIndex(numsix, 0);
			base.Controls.SetChildIndex(numfive, 0);
			base.Controls.SetChildIndex(numseven, 0);
			base.Controls.SetChildIndex(numeight, 0);
			base.Controls.SetChildIndex(numnine, 0);
			base.Controls.SetChildIndex(removeall, 0);
			base.Controls.SetChildIndex(clearenter, 0);
			base.Controls.SetChildIndex(commoditySearch, 0);
			base.Controls.SetChildIndex(cureGuide, 0);
			base.Controls.SetChildIndex(Checkout, 0);
			base.Controls.SetChildIndex(onediscount, 0);
			base.Controls.SetChildIndex(distotalprice, 0);
			base.Controls.SetChildIndex(disodd, 0);
			base.Controls.SetChildIndex(onesubcount, 0);
			base.Controls.SetChildIndex(button1, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			((System.ComponentModel.ISupportInitialize)infolist).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			flp_chooseMember.ResumeLayout(false);
			flp_chooseMember.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}
	}
}
