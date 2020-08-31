using POS_Client.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmMainShopSimpleReturnWithMoney : MasterThinForm
	{
		public int listindex = 1;

		public int columnOfFocus;

		private int oldTotal;

		private string temp1 = "";

		private List<string> barcodelist = new List<string>();

		private string sellno;

		private List<string> changeId = new List<string>();

		private List<string> changeBarcode = new List<string>();

		private List<int> changeNum = new List<int>();

		private bool isover;

		private bool isBarcodeNotExist;

		private string _returnType;

		private string vipNo;

		private string _GDSNO;

		private string _sum;

		private int _distotal;

		private int _disSum;

		private IContainer components;

		private Panel panel2;

		private Label label_totalCost;

		private Label label_credit;

		public Button backto;

		private Label text_totalCost;

		private Label text_credit;

		private Label label13;

		private TextBox paymoney;

		public Button buttonPrint;

		private TextBox alertMsg;

		public Button Checkout;

		public Button btnCancel;

		public Button btnReturn;

		public Button tempClear;

		private Button numnine;

		private Button numeight;

		private Button numseven;

		private Button numfive;

		private Button numsix;

		private Button numfour;

		private Button numthree;

		private Button numtwo;

		private Button numzero;

		private Button backspace;

		private Button numone;

		private Label label_sellDate;

		private Label label7;

		private Label label_sellNo;

		private Label label5;

		public DataGridView infolist1;

		private Label changeLog;

		private Label text_sellstate;

		private Label label_sellstate;

		private Label text_sum;

		private Label label_sum;

		private Label text_uID;

		private Label label_uID;

		private Label text_memberName;

		private Label label_member;

		private Button btnPrevious;

		private Button btnNext;

		private Label label_refund;

		private Label text_refund;

		private Label label_sumDiscount;

		private Label text_sumDiscount;

		private Label label_SdControl;

		private Label label_originalSum;

		private Label text_originalSum;

		private Label label_CashCredit;

		private Label text_CashCredit;

		private TextBox textBox_sumDiscount;

		private TextBox textBox_refund;

		private Label label_refundControl;

		private DataGridViewTextBoxColumn Column1;

		private frmMainShopSimpleWithMoney.CustomColumn commodity;

		private DataGridViewTextBoxColumn commodityName;

		private DataGridViewTextBoxColumn fixedPrice;

		private DataGridViewTextBoxColumn sellingPrice;

		private DataGridViewTextBoxColumn quantity;

		private DataGridViewTextBoxColumn subtotal;

		private DataGridViewTextBoxColumn discount;

		private DataGridViewTextBoxColumn total;

		private DataGridViewTextBoxColumn ReturnNum;

		private DataGridViewTextBoxColumn ReturnPrice;

		private DataGridViewTextBoxColumn barcode;

		private DataGridViewTextBoxColumn detailId;

		private Button btn_enter;

		private Button btn_back;

		public frmMainShopSimpleReturnWithMoney(string sellno, string returnType, string GDSNO)
			: base("銷售單|退貨|補印收據")
		{
			_returnType = returnType;
			this.sellno = sellno;
			_GDSNO = GDSNO;
			InitializeComponent();
			pb_virtualKeyBoard.Visible = false;
			string[] strWhereParameterArray = new string[1]
			{
				sellno
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "memberId,status,sellTime,sellNo,sum,sumDiscount,cash,Credit,itemstotal,sum,oldECRHDHSNo", "hypos_main_sell", "sellNo = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				string text = dataTable.Rows[0]["memberId"].ToString();
				string value = dataTable.Rows[0]["status"].ToString();
				string text2 = dataTable.Rows[0]["sellTime"].ToString();
				string text3 = dataTable.Rows[0]["sellNo"].ToString();
				string text4 = dataTable.Rows[0]["sum"].ToString();
				string text5 = dataTable.Rows[0]["sumDiscount"].ToString();
				string str = dataTable.Rows[0]["cash"].ToString();
				string str2 = dataTable.Rows[0]["Credit"].ToString();
				vipNo = text;
				oldTotal = int.Parse(dataTable.Rows[0]["itemstotal"].ToString());
				_sum = text4;
				bool flag = false;
				if ("1".Equals(value))
				{
					flag = true;
				}
				DateTime dateTime = DateTime.Now.AddMonths(-12);
				DateTime now = DateTime.Now;
				DateTime t = new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0);
				if (DateTime.Compare(Convert.ToDateTime(text2), t) < 0 || flag || !"".Equals(dataTable.Rows[0]["oldECRHDHSNo"].ToString()))
				{
					isover = true;
				}
				string[] strWhereParameterArray2 = new string[1]
				{
					text3
				};
				DataTable obj = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "barcode,num,sellDeatialId,fixedPrice,sellingPrice,subtotal,discount,total", "hypos_detail_sell", "sellNo = {0} ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				int num = 1;
				foreach (DataRow row in obj.Rows)
				{
					string text6 = row["barcode"].ToString();
					string text7 = row["num"].ToString();
					string text8 = row["sellDeatialId"].ToString();
					string text9 = row["fixedPrice"].ToString();
					string text10 = row["sellingPrice"].ToString();
					string text11 = row["subtotal"].ToString();
					string text12 = row["discount"].ToString();
					string text13 = row["total"].ToString();
					string[] strWhereParameterArray3 = new string[1]
					{
						text6
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDSNO,GDName,CName,formCode,contents,brandName,spec,capacity", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count <= 0)
					{
						isBarcodeNotExist = true;
						continue;
					}
					CommodityInfo commodityInfo = new CommodityInfo();
					commodityInfo.setMemberIdNo("");
					commodityInfo.setHiddenGDSNO(dataTable2.Rows[0]["GDSNO"].ToString());
					commodityInfo.setMemberVipNo("店內碼:" + dataTable2.Rows[0]["GDSNO"].ToString());
					commodityInfo.setCommodityName(setCommodityName(dataTable2.Rows[0]));
					commodityInfo.setCommodityClass(dataTable2.Rows[0]["spec"].ToString() + " " + dataTable2.Rows[0]["capacity"].ToString());
					commodityInfo.setlabe1("");
					commodityInfo.BackColor = Color.FromArgb(255, 250, 231);
					infolist1.Rows.Add(num.ToString(), commodityInfo, setCommodityName(dataTable2.Rows[0]), text9, text10, text7, text11, text12, text13, "0", "0", text6, text8);
					infolist1.Rows[0].Selected = false;
					bool flag2 = false;
					foreach (string item in barcodelist)
					{
						if (item.Equals(text6))
						{
							flag2 = true;
						}
					}
					if (!flag2)
					{
						barcodelist.Add(text6);
					}
					flag2 = false;
					num++;
				}
				foreach (DataGridViewRow item2 in (IEnumerable)infolist1.Rows)
				{
					item2.Height = 100;
				}
				if (!isover)
				{
					DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
					dataGridViewButtonColumn.Text = "-";
					dataGridViewButtonColumn.Name = "btn1";
					dataGridViewButtonColumn.HeaderText = "";
					dataGridViewButtonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
					dataGridViewButtonColumn.UseColumnTextForButtonValue = true;
					infolist1.Columns.Add(dataGridViewButtonColumn);
					DataGridViewButtonColumn dataGridViewButtonColumn2 = new DataGridViewButtonColumn();
					dataGridViewButtonColumn2.Text = "x";
					dataGridViewButtonColumn2.Name = "btn2";
					dataGridViewButtonColumn2.HeaderText = "";
					dataGridViewButtonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
					dataGridViewButtonColumn2.UseColumnTextForButtonValue = true;
					infolist1.Columns.Add(dataGridViewButtonColumn2);
				}
				label_sellNo.Text = sellno;
				label_sellDate.Text = text2;
				string[] strWhereParameterArray4 = new string[1]
				{
					text
				};
				DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Name,IdNo,Total,Credit", "hypos_CUST_RTL", "VipNo = {0}", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable3.Rows.Count > 0)
				{
					text_memberName.Text = dataTable3.Rows[0]["Name"].ToString();
					text_uID.Text = dataTable3.Rows[0]["IdNo"].ToString();
					text_totalCost.Text = dataTable3.Rows[0]["Total"].ToString();
					text_credit.Text = dataTable3.Rows[0]["Credit"].ToString();
				}
				else
				{
					text_memberName.Text = "非會員";
					text_uID.Text = "";
					text_totalCost.Text = "0";
					text_credit.Text = "0";
				}
				text_sum.Text = text4;
				text_refund.Text = "0";
				text_sumDiscount.Text = text5;
				textBox_sumDiscount.Text = "0";
				textBox_refund.Text = "0";
				text_originalSum.Text = (int.Parse(text4) - int.Parse(text5)).ToString();
				text_CashCredit.Text = str + "(" + str2 + ")";
				if ("0".Equals(value))
				{
					text_sellstate.Text = "正常";
				}
				else if ("1".Equals(value))
				{
					text_sellstate.Text = "取消";
				}
				else
				{
					text_sellstate.Text = "變更";
				}
				if (isBarcodeNotExist)
				{
					AutoClosingMessageBox.Show("部分商品已不存在");
				}
			}
			if ("frmStatisticsRecord".Equals(_returnType))
			{
				btn_back.Visible = true;
			}
			else if ("frmEditMember".Equals(_returnType))
			{
				btn_back.Visible = true;
			}
			else if ("frmSearchSell_Return".Equals(_returnType))
			{
				btn_back.Visible = false;
			}
			else
			{
				btn_back.Visible = true;
			}
		}

		private void backto_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("確定放棄所有編修離開。點選「確定」放棄目前編修狀態（已編修之退貨退款將不會儲存）？", "結束編修", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				if ("frmSearchSell_Return".Equals(_returnType))
				{
					switchForm(new frmSearchSell_Return());
				}
				else if ("frmEditCommodity".Equals(_returnType))
				{
					switchForm(new frmEditCommodity(_GDSNO));
				}
				else if ("frmEditMember".Equals(_returnType))
				{
					Dispose();
					Close();
				}
				else if ("frmStatisticsRecord".Equals(_returnType))
				{
					Dispose();
					Close();
				}
				else if ("frmSearchSellResult".Equals(_returnType))
				{
					Dispose();
					Close();
				}
			}
		}

		private void Checkout_Click(object sender, EventArgs e)
		{
			if (isover)
			{
				return;
			}
			bool flag = true;
			int num = 0;
			int num2 = 0;
			foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
			{
				num2 += int.Parse(item.Cells["ReturnNum"].Value.ToString());
			}
			if (num2 == 0)
			{
				return;
			}
			foreach (DataGridViewRow item2 in (IEnumerable)infolist1.Rows)
			{
				if ("0".Equals(item2.Cells["quantity"].Value.ToString()))
				{
					num++;
				}
			}
			if (infolist1.Rows.Count == num)
			{
				flag = false;
			}
			int num3 = int.Parse(text_sumDiscount.Text) + int.Parse(textBox_sumDiscount.Text);
			int num4 = int.Parse(text_refund.Text);
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			if (flag)
			{
				string[] array = new string[6]
				{
					sellno,
					_distotal.ToString(),
					text,
					num4.ToString(),
					(-_disSum).ToString(),
					num3.ToString()
				};
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Refund", "hypos_main_sell", "sellNo = {0}", "", null, array, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					string value = dataTable.Rows[0]["Refund"].ToString();
					string text2 = "";
					text2 = (string.IsNullOrEmpty(value) ? "UPDATE hypos_main_sell SET itemstotal =itemstotal+{1},editDate ={2},sum =sum+{3},Refund ={4},sumDiscount ={5},changcount =changcount+1,status =2 where sellNo ={0}" : "UPDATE hypos_main_sell SET itemstotal =itemstotal+{1},editDate ={2},sum =sum+{3},Refund =Refund+{4},sumDiscount ={5},changcount =changcount+1,status =2 where sellNo ={0}");
					DataBaseUtilities.DBOperation(Program.ConnectionString, text2, array, CommandOperationType.ExecuteNonQuery);
				}
				int num5 = 0;
				string[,] strFieldArray = new string[5, 2]
				{
					{
						"sellNo",
						sellno
					},
					{
						"changeDate",
						text
					},
					{
						"isprint",
						"1"
					},
					{
						"ischange",
						"1"
					},
					{
						"sum",
						(int.Parse(_sum) - num3 + num4).ToString()
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_mainsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				string sql = "select sellLogId from hypos_mainsell_log order by sellLogId desc LIMIT 0,1";
				string text3 = DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar).ToString();
				foreach (string item3 in changeId)
				{
					string[] strWhereParameterArray = new string[2]
					{
						sellno,
						item3
					};
					DataTable obj = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_detail_sell", "sellNo = {0} and sellDeatialId={1}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
					string text4 = obj.Rows[0]["fixedPrice"].ToString();
					string text5 = obj.Rows[0]["sellingPrice"].ToString();
					string text6 = obj.Rows[0]["num"].ToString();
					string text7 = obj.Rows[0]["discount"].ToString();
					string text8 = obj.Rows[0]["PRNO"].ToString();
					string text9 = obj.Rows[0]["BLNO"].ToString();
					int num6 = int.Parse(obj.Rows[0]["subtotal"].ToString());
					int num7 = int.Parse(obj.Rows[0]["total"].ToString());
					int num8 = 0;
					string text10 = changeNum[num5].ToString();
					int num9 = 0;
					int num10 = 0;
					foreach (DataGridViewRow item4 in (IEnumerable)infolist1.Rows)
					{
						if (item3.Equals(item4.Cells["detailId"].Value.ToString()))
						{
							num8 = int.Parse(item4.Cells["ReturnPrice"].Value.ToString());
							num9 = num6 + num8;
							num10 = num7 + num8;
						}
					}
					string[] strParameterArray = new string[5]
					{
						text10,
						num9.ToString(),
						num10.ToString(),
						sellno,
						item3
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_detail_sell SET num = num-{0}, subtotal ={1}, total ={2} where sellNo = {3} and sellDeatialId={4} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
					strFieldArray = new string[13, 2]
					{
						{
							"sellLogId",
							text3
						},
						{
							"barcode",
							changeBarcode[num5]
						},
						{
							"num",
							text6
						},
						{
							"diffNum",
							text10
						},
						{
							"fixedPrice",
							text4
						},
						{
							"sellingPrice",
							text5
						},
						{
							"diffSellingPrice",
							(-num8).ToString()
						},
						{
							"discount",
							text7
						},
						{
							"subtotal",
							num9.ToString()
						},
						{
							"total",
							num10.ToString()
						},
						{
							"PRNO",
							text8
						},
						{
							"BLNO",
							text9
						},
						{
							"sellDetailId",
							item3
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_detailsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
					string[] strWhereParameterArray2 = new string[1]
					{
						changeBarcode[num5]
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDSNO, inventory", "hypos_GOODSLST", "GDSNO = {0} ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
					string text11 = text10;
					if (!string.IsNullOrEmpty(dataTable2.Rows[0]["inventory"].ToString()))
					{
						text11 = (int.Parse(dataTable2.Rows[0]["inventory"].ToString()) + int.Parse(text11)).ToString();
					}
					string[] strParameterArray2 = new string[2]
					{
						text11,
						changeBarcode[num5]
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST SET inventory ={0} where GDSNO ={1} ", strParameterArray2, CommandOperationType.ExecuteNonQuery);
					num5++;
				}
				DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Total,Credit", "hypos_CUST_RTL", "VipNo = {0}", "", null, new string[1]
				{
					vipNo
				}, CommandOperationType.ExecuteReaderReturnDataTable);
				int num11 = 0;
				int num12 = 0;
				if (dataTable3.Rows.Count > 0)
				{
					num11 = int.Parse(dataTable3.Rows[0]["Credit"].ToString());
					num12 = int.Parse(dataTable3.Rows[0]["Total"].ToString());
				}
				if (int.Parse(text_credit.Text) > 0 && _disSum != 0)
				{
					if (MessageBox.Show("會員尚有賒帳金額「" + num11 + "」。退款款項是否自賒帳金額中扣除？點選「確定」自賒帳金額中扣除，點選「取消」現金退款", "賒帳扣款", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						int num13 = 0;
						int num14 = 0;
						int num15 = num11 + _disSum;
						if (num15 > 0)
						{
							num13 = _disSum;
							AutoClosingMessageBox.Show("自賒帳金額中扣除「" + -num13 + "」，目前賒帳金額尚餘「" + num15 + "」元");
						}
						else
						{
							num13 = -num11;
							num14 = -num15;
							AutoClosingMessageBox.Show("自賒帳金額中扣除「" + num11 + "」，尚需找零「" + num14 + "」元");
						}
						DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Credit=Credit+{1}, RepayDate ={2} where VipNo={0} ", new string[3]
						{
							vipNo,
							num13.ToString(),
							text
						}, CommandOperationType.ExecuteNonQuery);
						strFieldArray = new string[7, 2]
						{
							{
								"memberId",
								vipNo
							},
							{
								"sellNo",
								sellno
							},
							{
								"editdate",
								text
							},
							{
								"sellType",
								"2"
							},
							{
								"Cash",
								num14.ToString()
							},
							{
								"Credit",
								(-num13).ToString()
							},
							{
								"status",
								"2"
							}
						};
						DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
					}
					else
					{
						strFieldArray = new string[7, 2]
						{
							{
								"memberId",
								vipNo
							},
							{
								"sellNo",
								sellno
							},
							{
								"editdate",
								text
							},
							{
								"sellType",
								"1"
							},
							{
								"Cash",
								(-_disSum).ToString()
							},
							{
								"Credit",
								"0"
							},
							{
								"status",
								"2"
							}
						};
						DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
						AutoClosingMessageBox.Show("現金退款「" + -_disSum + "」點選「確定」直接列印收據、返回銷售單查詢頁");
					}
				}
				else
				{
					strFieldArray = new string[7, 2]
					{
						{
							"memberId",
							vipNo
						},
						{
							"sellNo",
							sellno
						},
						{
							"editdate",
							text
						},
						{
							"sellType",
							"1"
						},
						{
							"Cash",
							(-_disSum).ToString()
						},
						{
							"Credit",
							"0"
						},
						{
							"status",
							"2"
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
					AutoClosingMessageBox.Show("現金退款「" + -_disSum + "」點選「確定」直接列印收據、返回銷售單查詢頁");
				}
				int num16 = 0;
				if (num12 > 0)
				{
					num16 = _disSum;
				}
				if (!"".Equals(vipNo))
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Total=Total+{1} where VipNo={0} ", new string[2]
					{
						vipNo,
						num16.ToString()
					}, CommandOperationType.ExecuteNonQuery);
				}
				string[] strWhereParameterArray3 = new string[1]
				{
					sellno
				};
				DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "changcount", "hypos_main_sell", "sellNo = {0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
				frmSell_SellNo frmSell_SellNo = new frmSell_SellNo(sellno, int.Parse(dataTable4.Rows[0]["changcount"].ToString()), "refund");
				if ("frmEditCommodity".Equals(_returnType))
				{
					switchForm(new frmEditCommodity(_GDSNO));
				}
				else if ("frmEditMember".Equals(_returnType))
				{
					Dispose();
					Close();
				}
				else if ("frmStatisticsRecord".Equals(_returnType))
				{
					Dispose();
					Close();
				}
				else if ("frmSearchSellResult".Equals(_returnType))
				{
					Dispose();
					Close();
				}
				else
				{
					switchForm(new frmSearchSell_Return());
				}
				frmSell_SellNo.Show();
				return;
			}
			int num17 = int.Parse(_sum) - num3;
			string[] array2 = new string[4]
			{
				sellno,
				text,
				num3.ToString(),
				num17.ToString()
			};
			DataTable dataTable5 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Refund", "hypos_main_sell", "sellNo = {0}", "", null, array2, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable5.Rows.Count > 0)
			{
				string text12 = "";
				text12 = (string.IsNullOrEmpty(dataTable5.Rows[0]["Refund"].ToString()) ? "UPDATE hypos_main_sell SET status =1,editDate ={1},sumDiscount ={2},Refund ={3} where sellNo ={0}" : "UPDATE hypos_main_sell SET status =1,editDate ={1},sumDiscount ={2},Refund =Refund+{3} where sellNo ={0}");
				DataBaseUtilities.DBOperation(Program.ConnectionString, text12, array2, CommandOperationType.ExecuteNonQuery);
			}
			string[,] strFieldArray2 = new string[4, 2]
			{
				{
					"sellNo",
					sellno
				},
				{
					"changeDate",
					text
				},
				{
					"iscancel",
					"1"
				},
				{
					"sum",
					num17.ToString()
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_mainsell_log", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
			string sql2 = "select sellLogId from hypos_mainsell_log order by sellLogId desc LIMIT 0,1";
			string text13 = DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteScalar).ToString();
			array2 = new string[1]
			{
				sellno
			};
			DataTable dataTable6 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_detail_sell", "sellNo ={0} and num > 0", "", null, array2, CommandOperationType.ExecuteReaderReturnDataTable);
			for (int i = 0; i < dataTable6.Rows.Count; i++)
			{
				strFieldArray2 = new string[13, 2]
				{
					{
						"sellLogId",
						text13
					},
					{
						"barcode",
						dataTable6.Rows[i]["barcode"].ToString()
					},
					{
						"num",
						dataTable6.Rows[i]["num"].ToString()
					},
					{
						"diffNum",
						dataTable6.Rows[i]["num"].ToString()
					},
					{
						"fixedPrice",
						dataTable6.Rows[i]["fixedPrice"].ToString()
					},
					{
						"sellingPrice",
						dataTable6.Rows[i]["sellingPrice"].ToString()
					},
					{
						"diffSellingPrice",
						dataTable6.Rows[i]["total"].ToString()
					},
					{
						"discount",
						dataTable6.Rows[i]["discount"].ToString()
					},
					{
						"subtotal",
						"0"
					},
					{
						"total",
						"0"
					},
					{
						"PRNO",
						dataTable6.Rows[i]["PRNO"].ToString()
					},
					{
						"BLNO",
						dataTable6.Rows[i]["BLNO"].ToString()
					},
					{
						"sellDetailId",
						dataTable6.Rows[i]["sellDeatialId"].ToString()
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_detailsell_log", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
			}
			string[] strWhereParameterArray4 = new string[1]
			{
				sellno
			};
			DataTable dataTable7 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "barcode, num", "hypos_detail_sell", "sellNo = {0}", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
			for (int j = 0; j < dataTable7.Rows.Count; j++)
			{
				string text14 = dataTable7.Rows[j]["barcode"].ToString();
				string text15 = dataTable7.Rows[j]["num"].ToString();
				string[] strWhereParameterArray5 = new string[1]
				{
					text14
				};
				DataTable dataTable8 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDSNO, inventory", "hypos_GOODSLST", "GDSNO = {0} ", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable);
				string text16 = text15;
				if (!string.IsNullOrEmpty(dataTable8.Rows[0]["inventory"].ToString()))
				{
					text16 = (int.Parse(dataTable8.Rows[0]["inventory"].ToString()) + int.Parse(text15)).ToString();
				}
				string[] strParameterArray3 = new string[2]
				{
					text16,
					text14
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST SET inventory ={0} where GDSNO ={1} ", strParameterArray3, CommandOperationType.ExecuteNonQuery);
			}
			DataTable dataTable9 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Total,Credit", "hypos_CUST_RTL", "VipNo = {0}", "", null, new string[1]
			{
				vipNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			int num18 = 0;
			int num19 = 0;
			if (dataTable9.Rows.Count > 0)
			{
				num18 = int.Parse(dataTable9.Rows[0]["Credit"].ToString());
				num19 = int.Parse(dataTable9.Rows[0]["Total"].ToString());
			}
			int num20 = -int.Parse(text_originalSum.Text);
			if (int.Parse(text_credit.Text) > 0)
			{
				if (MessageBox.Show("會員尚有賒帳金額「" + num18 + "」。退款款項是否自賒帳金額中扣除？點選「確定」自賒帳金額中扣除，點選「取消」現金退款", "賒帳扣款", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					int num21 = 0;
					int num22 = 0;
					int num23 = num18 + num20;
					if (num23 > 0)
					{
						num21 = num20;
						AutoClosingMessageBox.Show("自賒帳金額中扣除「" + -num21 + "」，目前賒帳金額尚餘「" + num23 + "」元");
					}
					else
					{
						num21 = -num18;
						num22 = -num23;
						AutoClosingMessageBox.Show("自賒帳金額中扣除「" + num18 + "」，尚需找零「" + num22 + "」元");
					}
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Credit=Credit+{1}, RepayDate ={2} where VipNo={0} ", new string[3]
					{
						vipNo,
						num21.ToString(),
						text
					}, CommandOperationType.ExecuteNonQuery);
					strFieldArray2 = new string[7, 2]
					{
						{
							"memberId",
							vipNo
						},
						{
							"sellNo",
							sellno
						},
						{
							"editdate",
							text
						},
						{
							"sellType",
							"2"
						},
						{
							"Cash",
							num22.ToString()
						},
						{
							"Credit",
							(-num21).ToString()
						},
						{
							"status",
							"1"
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				}
				else
				{
					strFieldArray2 = new string[7, 2]
					{
						{
							"memberId",
							vipNo
						},
						{
							"sellNo",
							sellno
						},
						{
							"editdate",
							text
						},
						{
							"sellType",
							"1"
						},
						{
							"Cash",
							(-num20).ToString()
						},
						{
							"Credit",
							"0"
						},
						{
							"status",
							"1"
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
					AutoClosingMessageBox.Show("現金退款「" + -num20 + "」點選「確定」訂單取消完成、返回銷售單查詢頁");
				}
			}
			else
			{
				strFieldArray2 = new string[7, 2]
				{
					{
						"memberId",
						vipNo
					},
					{
						"sellNo",
						sellno
					},
					{
						"editdate",
						text
					},
					{
						"sellType",
						"1"
					},
					{
						"Cash",
						(-num20).ToString()
					},
					{
						"Credit",
						"0"
					},
					{
						"status",
						"1"
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				AutoClosingMessageBox.Show("現金退款「" + -num20 + "」點選「確定」訂單取消完成、返回銷售單查詢頁");
			}
			int num24 = 0;
			if (num19 > 0)
			{
				num24 = num20;
			}
			if (!"".Equals(vipNo))
			{
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Total=Total+{1} where VipNo={0} ", new string[2]
				{
					vipNo,
					num24.ToString()
				}, CommandOperationType.ExecuteNonQuery);
			}
			if ("frmEditCommodity".Equals(_returnType))
			{
				switchForm(new frmEditCommodity(_GDSNO));
			}
			else if ("frmEditMember".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else if ("frmStatisticsRecord".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else if ("frmSearchSellResult".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else
			{
				switchForm(new frmSearchSell_Return());
			}
		}

		private void buttonPrint_Click(object sender, EventArgs e)
		{
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string[,] strFieldArray = new string[4, 2]
			{
				{
					"sellNo",
					sellno
				},
				{
					"changeDate",
					text
				},
				{
					"isprint",
					"1"
				},
				{
					"sum",
					(int.Parse(text_sum.Text) - int.Parse(text_sumDiscount.Text)).ToString()
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_mainsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			string[] strWhereParameterArray = new string[1]
			{
				sellno
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "changcount", "hypos_main_sell", "sellNo = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			frmSell_SellNo frmSell_SellNo = new frmSell_SellNo(sellno, int.Parse(dataTable.Rows[0]["changcount"].ToString()), "refund");
			AutoClosingMessageBox.Show("收據補印完成");
			if ("frmEditCommodity".Equals(_returnType))
			{
				switchForm(new frmEditCommodity(_GDSNO));
			}
			else if ("frmEditMember".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else if ("frmStatisticsRecord".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else if ("frmSearchSellResult".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else
			{
				switchForm(new frmSearchSell_Return());
			}
			frmSell_SellNo.Show();
		}

		private void numone_Click(object sender, EventArgs e)
		{
			temp1 += "1";
		}

		private void numtwo_Click(object sender, EventArgs e)
		{
			temp1 += "2";
		}

		private void numthree_Click(object sender, EventArgs e)
		{
			temp1 += "3";
		}

		private void numfour_Click(object sender, EventArgs e)
		{
			temp1 += "4";
		}

		private void numfive_Click(object sender, EventArgs e)
		{
			temp1 += "5";
		}

		private void numsix_Click(object sender, EventArgs e)
		{
			temp1 += "6";
		}

		private void numseven_Click(object sender, EventArgs e)
		{
			temp1 += "7";
		}

		private void numeight_Click(object sender, EventArgs e)
		{
			temp1 += "8";
		}

		private void numnine_Click(object sender, EventArgs e)
		{
			temp1 += "9";
		}

		private void numzero_Click(object sender, EventArgs e)
		{
			temp1 += "0";
		}

		private void backspace_Click(object sender, EventArgs e)
		{
			if (temp1.Length > 0)
			{
				temp1 = temp1.Remove(temp1.Length - 1);
			}
		}

		private void tempClear_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
			{
				int num = int.Parse(item.Cells["ReturnNum"].Value.ToString());
				int num2 = int.Parse(item.Cells["quantity"].Value.ToString());
				if (-num > 0)
				{
					item.Cells["quantity"].Value = num2 - num;
					item.Cells["ReturnNum"].Value = "0";
				}
				item.Cells["ReturnPrice"].Value = "0";
			}
			changeId.Clear();
			changeNum.Clear();
			changeBarcode.Clear();
			temp1 = "";
			for (int i = 0; i < infolist1.Rows.Count; i++)
			{
				infolist1.Rows[i].Selected = false;
			}
			textBox_sumDiscount.Text = "0";
			textBox_refund.Text = "0";
			text_refund.Text = "0";
		}

		private void btnReturn_Click(object sender, EventArgs e)
		{
			if (!isover)
			{
				bool flag = false;
				for (int i = 0; i < infolist1.Rows.Count; i++)
				{
					if (infolist1.Rows[i].Selected)
					{
						flag = true;
					}
				}
				if (flag)
				{
					if (temp1 != null)
					{
						if (temp1.Length > 0)
						{
							if (int.Parse(infolist1.CurrentRow.Cells["quantity"].Value.ToString()) >= int.Parse(temp1))
							{
								bool flag2 = false;
								int num = 0;
								foreach (string item in changeId)
								{
									if (item.Equals(infolist1.CurrentRow.Cells["detailId"].Value.ToString()))
									{
										flag2 = true;
										changeNum[num] += int.Parse(temp1);
									}
									num++;
								}
								if (!flag2)
								{
									changeId.Add(infolist1.CurrentRow.Cells["detailId"].Value.ToString());
									changeNum.Add(int.Parse(temp1));
									changeBarcode.Add(infolist1.CurrentRow.Cells["barcode"].Value.ToString());
								}
								infolist1.CurrentRow.Cells["quantity"].Value = (int.Parse(infolist1.CurrentRow.Cells["quantity"].Value.ToString()) - int.Parse(temp1)).ToString();
								if (infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString() == "" || infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString() == string.Empty)
								{
									infolist1.CurrentRow.Cells["ReturnNum"].Value = "0";
								}
								infolist1.CurrentRow.Cells["ReturnNum"].Value = (int.Parse(infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString()) - int.Parse(temp1)).ToString();
								if (infolist1.CurrentRow.Cells["quantity"].Value.ToString() == "0")
								{
									infolist1.CurrentRow.Cells["ReturnPrice"].Value = "-" + infolist1.CurrentRow.Cells["total"].Value.ToString();
									computetotal();
								}
								else
								{
									infolist1.CurrentRow.Cells["ReturnPrice"].Value = (int.Parse(infolist1.CurrentRow.Cells["sellingPrice"].Value.ToString()) * int.Parse(infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString())).ToString();
									computetotal();
								}
							}
						}
						else
						{
							AutoClosingMessageBox.Show("請先輸入數字後再執行變更動作");
						}
					}
					else
					{
						AutoClosingMessageBox.Show("請先輸入數字後再執行變更動作");
					}
				}
				else
				{
					AutoClosingMessageBox.Show("請先選擇商品");
				}
			}
			temp1 = "";
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (isover || MessageBox.Show("整筆訂單取消，確認後將無法復原。確定取消？", "整筆取消", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return;
			}
			int num = int.Parse(text_sumDiscount.Text) + int.Parse(textBox_sumDiscount.Text);
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			int num2 = int.Parse(_sum) - num;
			string[] array = new string[3]
			{
				sellno,
				text,
				num2.ToString()
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Refund", "hypos_main_sell", "sellNo ={0}", "", null, array, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				string text2 = "";
				text2 = (string.IsNullOrEmpty(dataTable.Rows[0]["Refund"].ToString()) ? "UPDATE hypos_main_sell SET status =1,editDate ={1},Refund ={2} where sellNo ={0}" : "UPDATE hypos_main_sell SET status =1,editDate ={1},Refund =Refund+{2} where sellNo ={0}");
				DataBaseUtilities.DBOperation(Program.ConnectionString, text2, array, CommandOperationType.ExecuteNonQuery);
			}
			string[,] strFieldArray = new string[4, 2]
			{
				{
					"sellNo",
					sellno
				},
				{
					"changeDate",
					text
				},
				{
					"iscancel",
					"1"
				},
				{
					"sum",
					num2.ToString()
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_mainsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			string sql = "select sellLogId from hypos_mainsell_log order by sellLogId desc LIMIT 0,1";
			string text3 = DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar).ToString();
			array = new string[1]
			{
				sellno
			};
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_detail_sell", "sellNo ={0} and num > 0", "", null, array, CommandOperationType.ExecuteReaderReturnDataTable);
			for (int i = 0; i < dataTable2.Rows.Count; i++)
			{
				strFieldArray = new string[13, 2]
				{
					{
						"sellLogId",
						text3
					},
					{
						"barcode",
						dataTable2.Rows[i]["barcode"].ToString()
					},
					{
						"num",
						dataTable2.Rows[i]["num"].ToString()
					},
					{
						"diffNum",
						dataTable2.Rows[i]["num"].ToString()
					},
					{
						"fixedPrice",
						dataTable2.Rows[i]["fixedPrice"].ToString()
					},
					{
						"sellingPrice",
						dataTable2.Rows[i]["sellingPrice"].ToString()
					},
					{
						"diffSellingPrice",
						dataTable2.Rows[i]["total"].ToString()
					},
					{
						"discount",
						dataTable2.Rows[i]["discount"].ToString()
					},
					{
						"subtotal",
						"0"
					},
					{
						"total",
						"0"
					},
					{
						"PRNO",
						dataTable2.Rows[i]["PRNO"].ToString()
					},
					{
						"BLNO",
						dataTable2.Rows[i]["BLNO"].ToString()
					},
					{
						"sellDetailId",
						dataTable2.Rows[i]["sellDeatialId"].ToString()
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_detailsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			}
			string[] strWhereParameterArray = new string[1]
			{
				sellno
			};
			DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "barcode, num", "hypos_detail_sell", "sellNo = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			for (int j = 0; j < dataTable3.Rows.Count; j++)
			{
				string text4 = dataTable3.Rows[j]["barcode"].ToString();
				string text5 = dataTable3.Rows[j]["num"].ToString();
				string[] strWhereParameterArray2 = new string[1]
				{
					text4
				};
				DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDSNO, inventory", "hypos_GOODSLST", "GDSNO = {0} ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				string text6 = text5;
				if (!string.IsNullOrEmpty(dataTable4.Rows[0]["inventory"].ToString()))
				{
					text6 = (int.Parse(dataTable4.Rows[0]["inventory"].ToString()) + int.Parse(text5)).ToString();
				}
				string[] strParameterArray = new string[2]
				{
					text6,
					text4
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST SET inventory ={0} where GDSNO ={1} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
			}
			DataTable dataTable5 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Total,Credit", "hypos_CUST_RTL", "VipNo = {0}", "", null, new string[1]
			{
				vipNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			int num3 = 0;
			int num4 = 0;
			if (dataTable5.Rows.Count > 0)
			{
				num3 = int.Parse(dataTable5.Rows[0]["Credit"].ToString());
				num4 = int.Parse(dataTable5.Rows[0]["Total"].ToString());
			}
			int num5 = -int.Parse(text_originalSum.Text);
			if (int.Parse(text_credit.Text) > 0)
			{
				if (MessageBox.Show("會員尚有賒帳金額「" + num3 + "」。退款款項是否自賒帳金額中扣除？點選「確定」自賒帳金額中扣除，點選「取消」現金退款", "賒帳扣款", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					int num6 = 0;
					int num7 = 0;
					int num8 = num3 + num5;
					if (num8 > 0)
					{
						num6 = num5;
						AutoClosingMessageBox.Show("自賒帳金額中扣除「" + -num6 + "」，目前賒帳金額尚餘「" + num8 + "」元");
					}
					else
					{
						num6 = -num3;
						num7 = -num8;
						AutoClosingMessageBox.Show("自賒帳金額中扣除「" + num3 + "」，尚需找零「" + num7 + "」元");
					}
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Credit=Credit+{1}, RepayDate ={2} where VipNo={0} ", new string[3]
					{
						vipNo,
						num6.ToString(),
						text
					}, CommandOperationType.ExecuteNonQuery);
					strFieldArray = new string[7, 2]
					{
						{
							"memberId",
							vipNo
						},
						{
							"sellNo",
							sellno
						},
						{
							"editdate",
							text
						},
						{
							"sellType",
							"2"
						},
						{
							"Cash",
							num7.ToString()
						},
						{
							"Credit",
							(-num6).ToString()
						},
						{
							"status",
							"1"
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				}
				else
				{
					strFieldArray = new string[7, 2]
					{
						{
							"memberId",
							vipNo
						},
						{
							"sellNo",
							sellno
						},
						{
							"editdate",
							text
						},
						{
							"sellType",
							"1"
						},
						{
							"Cash",
							(-num5).ToString()
						},
						{
							"Credit",
							"0"
						},
						{
							"status",
							"1"
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
					AutoClosingMessageBox.Show("現金退款「" + -num5 + "」點選「確定」訂單取消完成、返回銷售單查詢頁");
				}
			}
			else
			{
				strFieldArray = new string[7, 2]
				{
					{
						"memberId",
						vipNo
					},
					{
						"sellNo",
						sellno
					},
					{
						"editdate",
						text
					},
					{
						"sellType",
						"1"
					},
					{
						"Cash",
						(-num5).ToString()
					},
					{
						"Credit",
						"0"
					},
					{
						"status",
						"1"
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				AutoClosingMessageBox.Show("現金退款「" + -num5 + "」點選「確定」訂單取消完成、返回銷售單查詢頁");
			}
			int num9 = 0;
			if (num4 > 0)
			{
				num9 = num5;
			}
			if (!"".Equals(vipNo))
			{
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Total=Total+{1} where VipNo={0} ", new string[2]
				{
					vipNo,
					num9.ToString()
				}, CommandOperationType.ExecuteNonQuery);
			}
			if ("frmEditCommodity".Equals(_returnType))
			{
				switchForm(new frmEditCommodity(_GDSNO));
			}
			else if ("frmEditMember".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else if ("frmStatisticsRecord".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else if ("frmSearchSellResult".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else
			{
				switchForm(new frmSearchSell_Return());
			}
		}

		private void infolist1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 13)
			{
				bool flag = false;
				bool flag2 = false;
				if (int.Parse(infolist1.CurrentRow.Cells["quantity"].Value.ToString()) <= 0)
				{
					return;
				}
				if (int.Parse(infolist1.CurrentRow.Cells["quantity"].Value.ToString()) - 1 == 0)
				{
					flag2 = true;
					if (MessageBox.Show("商品數量將為0，點選「確定」進行退貨、點選「取消」保留數量", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						flag = true;
					}
				}
				if (!(!flag2 || flag))
				{
					return;
				}
				bool flag3 = false;
				int num = 0;
				foreach (string item in changeId)
				{
					if (item.Equals(infolist1.CurrentRow.Cells["detailId"].Value.ToString()))
					{
						flag3 = true;
						changeNum[num] += 1;
					}
					num++;
				}
				if (!flag3)
				{
					changeId.Add(infolist1.CurrentRow.Cells["detailId"].Value.ToString());
					changeNum.Add(1);
					changeBarcode.Add(infolist1.CurrentRow.Cells["barcode"].Value.ToString());
				}
				infolist1.CurrentRow.Cells["quantity"].Value = (int.Parse(infolist1.CurrentRow.Cells["quantity"].Value.ToString()) - 1).ToString();
				if (infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString() == "" || infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString() == string.Empty)
				{
					infolist1.CurrentRow.Cells["ReturnNum"].Value = "0";
				}
				if (flag2)
				{
					infolist1.CurrentRow.Cells["ReturnNum"].Value = (int.Parse(infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString()) - 1).ToString();
					infolist1.CurrentRow.Cells["ReturnPrice"].Value = "-" + infolist1.CurrentRow.Cells["total"].Value.ToString();
				}
				else
				{
					infolist1.CurrentRow.Cells["ReturnNum"].Value = (int.Parse(infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString()) - 1).ToString();
					infolist1.CurrentRow.Cells["ReturnPrice"].Value = (int.Parse(infolist1.CurrentRow.Cells["sellingPrice"].Value.ToString()) * int.Parse(infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString())).ToString();
				}
				computetotal();
			}
			else
			{
				if (e.ColumnIndex != 14)
				{
					return;
				}
				bool flag4 = false;
				int num2 = 0;
				foreach (string item2 in changeId)
				{
					if (item2.Equals(infolist1.CurrentRow.Cells["detailId"].Value.ToString()))
					{
						flag4 = true;
						changeNum[num2] += int.Parse(infolist1.CurrentRow.Cells["quantity"].Value.ToString());
					}
					num2++;
				}
				if (!flag4)
				{
					changeId.Add(infolist1.CurrentRow.Cells["detailId"].Value.ToString());
					changeNum.Add(int.Parse(infolist1.CurrentRow.Cells["quantity"].Value.ToString()));
					changeBarcode.Add(infolist1.CurrentRow.Cells["barcode"].Value.ToString());
				}
				string s = infolist1.CurrentRow.Cells["quantity"].Value.ToString();
				infolist1.CurrentRow.Cells["quantity"].Value = "0";
				if (infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString() == "" || infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString() == string.Empty)
				{
					infolist1.CurrentRow.Cells["ReturnNum"].Value = "0";
				}
				infolist1.CurrentRow.Cells["ReturnNum"].Value = (int.Parse(infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString()) - int.Parse(s)).ToString();
				infolist1.CurrentRow.Cells["ReturnPrice"].Value = "-" + infolist1.CurrentRow.Cells["total"].Value.ToString();
				computetotal();
				infolist1.CurrentRow.Cells["btn2"].ReadOnly = true;
				infolist1.CurrentRow.Cells["btn1"].ReadOnly = true;
			}
		}

		private void computetotal()
		{
			barcodelist.Clear();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
			{
				bool flag = false;
				foreach (string item2 in barcodelist)
				{
					if (item2.Equals(item.Cells["barcode"].Value.ToString()))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					barcodelist.Add(item.Cells["barcode"].Value.ToString());
				}
				flag = false;
				num += int.Parse(item.Cells["quantity"].Value.ToString());
				num2 += int.Parse(item.Cells["ReturnPrice"].Value.ToString());
				num3 += int.Parse(item.Cells["total"].Value.ToString());
			}
			_distotal = num - oldTotal;
			_disSum = num2;
			text_refund.Text = num2.ToString();
			textBox_refund.Text = (-num2).ToString();
		}

		private void infolist1_SelectionChanged(object sender, EventArgs e)
		{
			if (infolist1.RowCount > 0)
			{
				paymoney.Text = infolist1.CurrentRow.Cells["barcode"].Value.ToString() + infolist1.CurrentRow.Cells["commodityName"].Value.ToString();
				for (int i = 0; i < infolist1.RowCount; i++)
				{
					(infolist1[1, i].Value as CommodityInfo).BackColor = Color.White;
				}
				if (infolist1.CurrentCell != null)
				{
					(infolist1.CurrentRow.Cells[1].Value as CommodityInfo).BackColor = Color.FromArgb(255, 208, 81);
					infolist1.Refresh();
				}
			}
		}

		private void alertMessage(string msg)
		{
			alertMsg.Text = msg;
		}

		private void btnPrevious_Click(object sender, EventArgs e)
		{
			if (infolist1.CurrentRow.Index > 0)
			{
				int index = infolist1.CurrentRow.Index;
				infolist1.Rows[infolist1.CurrentRow.Index - 1].Selected = true;
				infolist1.Rows[index].Selected = false;
				infolist1.CurrentCell = infolist1.Rows[index - 1].Cells[0];
				alertMessage("pre");
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (infolist1.CurrentRow.Index < infolist1.Rows.Count - 1)
			{
				int index = infolist1.CurrentRow.Index;
				infolist1.Rows[index + 1].Selected = true;
				infolist1.Rows[index].Selected = false;
				infolist1.CurrentCell = infolist1.Rows[index + 1].Cells[0];
				alertMessage("next");
			}
		}

		private void changeLog_Click(object sender, EventArgs e)
		{
			new frmMainshopSimpleChangeLogWithMoney(sellno).ShowDialog();
		}

		private void digitOnly_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b'));
		}

		private void tb_newDiscount_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == 13)
			{
				updateDiscount();
			}
		}

		private void updateDiscount()
		{
			int.Parse(text_sumDiscount.Text);
			int num = int.Parse(text_refund.Text);
			int num2 = -num / 2;
			int num3 = int.Parse(textBox_refund.Text);
			int num4 = num + num3;
			if (num3 > 0)
			{
				if (num3 > -num)
				{
					AutoClosingMessageBox.Show("輸入的金額不可大於退款小計金額");
				}
				else if (num3 < num2)
				{
					AutoClosingMessageBox.Show("請注意，輸入的金額小於退款小計之一半金額");
					textBox_sumDiscount.Text = num4.ToString();
					_disSum = -num3;
				}
				else
				{
					textBox_sumDiscount.Text = num4.ToString();
					_disSum = -num3;
				}
			}
			else
			{
				AutoClosingMessageBox.Show("輸入的金額不可小於零");
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
						text = text + array[i] + " ";
					}
				}
				text += " ． ";
				for (int j = 0; j < array2.Length; j++)
				{
					if (!string.IsNullOrEmpty(array2[j]))
					{
						text = text + array2[j] + " ";
					}
				}
				text += "]";
			}
			return text;
		}

		private void btn_enter_Click(object sender, EventArgs e)
		{
			if ("".Equals(textBox_refund.Text))
			{
				AutoClosingMessageBox.Show("請輸入退款金額");
			}
			else
			{
				updateDiscount();
			}
		}

		private void btn_back_Click(object sender, EventArgs e)
		{
			if ("frmSearchSell_Return".Equals(_returnType))
			{
				switchForm(new frmSearchSell_Return());
			}
			else if ("frmEditCommodity".Equals(_returnType))
			{
				switchForm(new frmEditCommodity(_GDSNO));
			}
			else if ("frmEditMember".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else if ("frmSearchSellResult".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else
			{
				Dispose();
				Close();
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
			panel2 = new System.Windows.Forms.Panel();
			btnPrevious = new System.Windows.Forms.Button();
			btnNext = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			btnReturn = new System.Windows.Forms.Button();
			tempClear = new System.Windows.Forms.Button();
			numnine = new System.Windows.Forms.Button();
			numeight = new System.Windows.Forms.Button();
			numseven = new System.Windows.Forms.Button();
			numfive = new System.Windows.Forms.Button();
			numsix = new System.Windows.Forms.Button();
			numfour = new System.Windows.Forms.Button();
			numthree = new System.Windows.Forms.Button();
			numtwo = new System.Windows.Forms.Button();
			numzero = new System.Windows.Forms.Button();
			backspace = new System.Windows.Forms.Button();
			numone = new System.Windows.Forms.Button();
			buttonPrint = new System.Windows.Forms.Button();
			backto = new System.Windows.Forms.Button();
			Checkout = new System.Windows.Forms.Button();
			text_totalCost = new System.Windows.Forms.Label();
			text_credit = new System.Windows.Forms.Label();
			changeLog = new System.Windows.Forms.Label();
			text_sellstate = new System.Windows.Forms.Label();
			label_sellstate = new System.Windows.Forms.Label();
			text_sum = new System.Windows.Forms.Label();
			label_sum = new System.Windows.Forms.Label();
			text_uID = new System.Windows.Forms.Label();
			label_uID = new System.Windows.Forms.Label();
			text_memberName = new System.Windows.Forms.Label();
			label_member = new System.Windows.Forms.Label();
			infolist1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			commodity = new POS_Client.frmMainShopSimpleWithMoney.CustomColumn();
			commodityName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			fixedPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			sellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			total = new System.Windows.Forms.DataGridViewTextBoxColumn();
			ReturnNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
			ReturnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			detailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label_sellDate = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label_sellNo = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			alertMsg = new System.Windows.Forms.TextBox();
			paymoney = new System.Windows.Forms.TextBox();
			label13 = new System.Windows.Forms.Label();
			label_totalCost = new System.Windows.Forms.Label();
			label_credit = new System.Windows.Forms.Label();
			label_refund = new System.Windows.Forms.Label();
			text_refund = new System.Windows.Forms.Label();
			label_sumDiscount = new System.Windows.Forms.Label();
			text_sumDiscount = new System.Windows.Forms.Label();
			label_SdControl = new System.Windows.Forms.Label();
			label_originalSum = new System.Windows.Forms.Label();
			text_originalSum = new System.Windows.Forms.Label();
			label_CashCredit = new System.Windows.Forms.Label();
			text_CashCredit = new System.Windows.Forms.Label();
			textBox_sumDiscount = new System.Windows.Forms.TextBox();
			textBox_refund = new System.Windows.Forms.TextBox();
			label_refundControl = new System.Windows.Forms.Label();
			btn_enter = new System.Windows.Forms.Button();
			btn_back = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)infolist1).BeginInit();
			SuspendLayout();
			pb_virtualKeyBoard.Location = new System.Drawing.Point(975, 640);
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 678);
			pb_virtualKeyBoard.Visible = false;
			panel2.BackgroundImage = POS_Client.Properties.Resources.inside_button;
			panel2.Controls.Add(btnPrevious);
			panel2.Controls.Add(btnNext);
			panel2.Controls.Add(btnCancel);
			panel2.Controls.Add(btnReturn);
			panel2.Controls.Add(tempClear);
			panel2.Controls.Add(numnine);
			panel2.Controls.Add(numeight);
			panel2.Controls.Add(numseven);
			panel2.Controls.Add(numfive);
			panel2.Controls.Add(numsix);
			panel2.Controls.Add(numfour);
			panel2.Controls.Add(numthree);
			panel2.Controls.Add(numtwo);
			panel2.Controls.Add(numzero);
			panel2.Controls.Add(backspace);
			panel2.Controls.Add(numone);
			panel2.Controls.Add(buttonPrint);
			panel2.Controls.Add(backto);
			panel2.Controls.Add(Checkout);
			panel2.Location = new System.Drawing.Point(12, 491);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(966, 182);
			panel2.TabIndex = 38;
			btnPrevious.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btnPrevious.Location = new System.Drawing.Point(526, 11);
			btnPrevious.Name = "btnPrevious";
			btnPrevious.Size = new System.Drawing.Size(42, 78);
			btnPrevious.TabIndex = 55;
			btnPrevious.Text = "↑";
			btnPrevious.UseVisualStyleBackColor = true;
			btnPrevious.Click += new System.EventHandler(btnPrevious_Click);
			btnNext.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btnNext.Location = new System.Drawing.Point(526, 93);
			btnNext.Name = "btnNext";
			btnNext.Size = new System.Drawing.Size(42, 78);
			btnNext.TabIndex = 54;
			btnNext.Text = "↓";
			btnNext.UseVisualStyleBackColor = true;
			btnNext.Click += new System.EventHandler(btnNext_Click);
			btnCancel.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnCancel.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btnCancel.ForeColor = System.Drawing.Color.White;
			btnCancel.Location = new System.Drawing.Point(682, 11);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(102, 163);
			btnCancel.TabIndex = 53;
			btnCancel.Text = "整筆取消";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += new System.EventHandler(btnCancel_Click);
			btnReturn.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnReturn.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btnReturn.ForeColor = System.Drawing.Color.White;
			btnReturn.Location = new System.Drawing.Point(574, 93);
			btnReturn.Name = "btnReturn";
			btnReturn.Size = new System.Drawing.Size(102, 80);
			btnReturn.TabIndex = 52;
			btnReturn.Text = "退貨";
			btnReturn.UseVisualStyleBackColor = false;
			btnReturn.Click += new System.EventHandler(btnReturn_Click);
			tempClear.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			tempClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			tempClear.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			tempClear.ForeColor = System.Drawing.Color.White;
			tempClear.Location = new System.Drawing.Point(574, 11);
			tempClear.Name = "tempClear";
			tempClear.Size = new System.Drawing.Size(102, 80);
			tempClear.TabIndex = 51;
			tempClear.Text = "清除輸入";
			tempClear.UseVisualStyleBackColor = false;
			tempClear.Click += new System.EventHandler(tempClear_Click);
			numnine.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numnine.Location = new System.Drawing.Point(478, 11);
			numnine.Name = "numnine";
			numnine.Size = new System.Drawing.Size(42, 35);
			numnine.TabIndex = 50;
			numnine.Text = "9";
			numnine.UseVisualStyleBackColor = true;
			numnine.Click += new System.EventHandler(numnine_Click);
			numeight.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numeight.Location = new System.Drawing.Point(427, 11);
			numeight.Name = "numeight";
			numeight.Size = new System.Drawing.Size(42, 35);
			numeight.TabIndex = 49;
			numeight.Text = "8";
			numeight.UseVisualStyleBackColor = true;
			numeight.Click += new System.EventHandler(numeight_Click);
			numseven.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numseven.Location = new System.Drawing.Point(379, 11);
			numseven.Name = "numseven";
			numseven.Size = new System.Drawing.Size(42, 35);
			numseven.TabIndex = 48;
			numseven.Text = "7";
			numseven.UseVisualStyleBackColor = true;
			numseven.Click += new System.EventHandler(numseven_Click);
			numfive.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfive.Location = new System.Drawing.Point(427, 52);
			numfive.Name = "numfive";
			numfive.Size = new System.Drawing.Size(42, 35);
			numfive.TabIndex = 47;
			numfive.Text = "5";
			numfive.UseVisualStyleBackColor = true;
			numfive.Click += new System.EventHandler(numfive_Click);
			numsix.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numsix.Location = new System.Drawing.Point(478, 52);
			numsix.Name = "numsix";
			numsix.Size = new System.Drawing.Size(42, 35);
			numsix.TabIndex = 46;
			numsix.Text = "6";
			numsix.UseVisualStyleBackColor = true;
			numsix.Click += new System.EventHandler(numsix_Click);
			numfour.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfour.Location = new System.Drawing.Point(379, 52);
			numfour.Name = "numfour";
			numfour.Size = new System.Drawing.Size(42, 35);
			numfour.TabIndex = 45;
			numfour.Text = "4";
			numfour.UseVisualStyleBackColor = true;
			numfour.Click += new System.EventHandler(numfour_Click);
			numthree.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numthree.Location = new System.Drawing.Point(478, 93);
			numthree.Name = "numthree";
			numthree.Size = new System.Drawing.Size(42, 35);
			numthree.TabIndex = 44;
			numthree.Text = "3";
			numthree.UseVisualStyleBackColor = true;
			numthree.Click += new System.EventHandler(numthree_Click);
			numtwo.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numtwo.Location = new System.Drawing.Point(427, 93);
			numtwo.Name = "numtwo";
			numtwo.Size = new System.Drawing.Size(42, 35);
			numtwo.TabIndex = 43;
			numtwo.Text = "2";
			numtwo.UseVisualStyleBackColor = true;
			numtwo.Click += new System.EventHandler(numtwo_Click);
			numzero.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numzero.Location = new System.Drawing.Point(379, 136);
			numzero.Name = "numzero";
			numzero.Size = new System.Drawing.Size(42, 35);
			numzero.TabIndex = 42;
			numzero.Text = "0";
			numzero.UseVisualStyleBackColor = true;
			numzero.Click += new System.EventHandler(numzero_Click);
			backspace.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			backspace.Location = new System.Drawing.Point(427, 134);
			backspace.Name = "backspace";
			backspace.Size = new System.Drawing.Size(93, 36);
			backspace.TabIndex = 41;
			backspace.Text = "backspace";
			backspace.UseVisualStyleBackColor = true;
			backspace.Click += new System.EventHandler(backspace_Click);
			numone.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numone.Location = new System.Drawing.Point(379, 93);
			numone.Name = "numone";
			numone.Size = new System.Drawing.Size(42, 35);
			numone.TabIndex = 40;
			numone.Text = "1";
			numone.UseVisualStyleBackColor = true;
			numone.Click += new System.EventHandler(numone_Click);
			buttonPrint.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			buttonPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			buttonPrint.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			buttonPrint.ForeColor = System.Drawing.Color.White;
			buttonPrint.Location = new System.Drawing.Point(282, 11);
			buttonPrint.Name = "buttonPrint";
			buttonPrint.Size = new System.Drawing.Size(91, 160);
			buttonPrint.TabIndex = 39;
			buttonPrint.Text = "補印收據";
			buttonPrint.UseVisualStyleBackColor = false;
			buttonPrint.Click += new System.EventHandler(buttonPrint_Click);
			backto.BackColor = System.Drawing.Color.FromArgb(56, 175, 190);
			backto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			backto.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			backto.ForeColor = System.Drawing.Color.White;
			backto.Location = new System.Drawing.Point(183, 11);
			backto.Name = "backto";
			backto.Size = new System.Drawing.Size(93, 160);
			backto.TabIndex = 37;
			backto.Text = "結束編修";
			backto.UseVisualStyleBackColor = false;
			backto.Click += new System.EventHandler(backto_Click);
			Checkout.BackColor = System.Drawing.Color.FromArgb(250, 87, 0);
			Checkout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			Checkout.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			Checkout.ForeColor = System.Drawing.Color.White;
			Checkout.Location = new System.Drawing.Point(790, 10);
			Checkout.Name = "Checkout";
			Checkout.Size = new System.Drawing.Size(102, 163);
			Checkout.TabIndex = 36;
			Checkout.Text = "儲存變更";
			Checkout.UseVisualStyleBackColor = false;
			Checkout.Click += new System.EventHandler(Checkout_Click);
			text_totalCost.AutoSize = true;
			text_totalCost.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_totalCost.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			text_totalCost.Location = new System.Drawing.Point(344, 342);
			text_totalCost.Name = "text_totalCost";
			text_totalCost.Size = new System.Drawing.Size(33, 24);
			text_totalCost.TabIndex = 14;
			text_totalCost.Text = "{3}";
			text_credit.AutoSize = true;
			text_credit.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_credit.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			text_credit.Location = new System.Drawing.Point(595, 342);
			text_credit.Name = "text_credit";
			text_credit.Size = new System.Drawing.Size(33, 24);
			text_credit.TabIndex = 13;
			text_credit.Text = "{5}";
			changeLog.AutoSize = true;
			changeLog.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			changeLog.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
			changeLog.Location = new System.Drawing.Point(887, 410);
			changeLog.Name = "changeLog";
			changeLog.Size = new System.Drawing.Size(73, 20);
			changeLog.TabIndex = 44;
			changeLog.Text = "變更記錄";
			changeLog.Click += new System.EventHandler(changeLog_Click);
			text_sellstate.AutoSize = true;
			text_sellstate.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_sellstate.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			text_sellstate.Location = new System.Drawing.Point(820, 407);
			text_sellstate.Name = "text_sellstate";
			text_sellstate.Size = new System.Drawing.Size(33, 24);
			text_sellstate.TabIndex = 43;
			text_sellstate.Text = "{9}";
			label_sellstate.AutoSize = true;
			label_sellstate.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_sellstate.ForeColor = System.Drawing.Color.Black;
			label_sellstate.Location = new System.Drawing.Point(709, 407);
			label_sellstate.Name = "label_sellstate";
			label_sellstate.Size = new System.Drawing.Size(105, 24);
			label_sellstate.TabIndex = 42;
			label_sellstate.Text = "銷售單狀態";
			text_sum.AutoSize = true;
			text_sum.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_sum.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			text_sum.Location = new System.Drawing.Point(120, 375);
			text_sum.Name = "text_sum";
			text_sum.Size = new System.Drawing.Size(33, 24);
			text_sum.TabIndex = 41;
			text_sum.Text = "{1}";
			label_sum.AutoSize = true;
			label_sum.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_sum.ForeColor = System.Drawing.Color.Black;
			label_sum.Location = new System.Drawing.Point(28, 375);
			label_sum.Name = "label_sum";
			label_sum.Size = new System.Drawing.Size(86, 24);
			label_sum.TabIndex = 40;
			label_sum.Text = "售價總計";
			text_uID.AutoSize = true;
			text_uID.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_uID.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			text_uID.Location = new System.Drawing.Point(820, 342);
			text_uID.Name = "text_uID";
			text_uID.Size = new System.Drawing.Size(33, 24);
			text_uID.TabIndex = 39;
			text_uID.Text = "{7}";
			label_uID.AutoSize = true;
			label_uID.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_uID.ForeColor = System.Drawing.Color.Black;
			label_uID.Location = new System.Drawing.Point(728, 342);
			label_uID.Name = "label_uID";
			label_uID.Size = new System.Drawing.Size(86, 24);
			label_uID.TabIndex = 38;
			label_uID.Text = "統一編號";
			text_memberName.AutoSize = true;
			text_memberName.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_memberName.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			text_memberName.Location = new System.Drawing.Point(120, 342);
			text_memberName.Name = "text_memberName";
			text_memberName.Size = new System.Drawing.Size(33, 24);
			text_memberName.TabIndex = 37;
			text_memberName.Text = "{0}";
			label_member.AutoSize = true;
			label_member.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_member.ForeColor = System.Drawing.Color.Black;
			label_member.Location = new System.Drawing.Point(28, 342);
			label_member.Name = "label_member";
			label_member.Size = new System.Drawing.Size(48, 24);
			label_member.TabIndex = 36;
			label_member.Text = "會員";
			infolist1.AllowUserToAddRows = false;
			infolist1.AllowUserToDeleteRows = false;
			infolist1.AllowUserToResizeColumns = false;
			infolist1.AllowUserToResizeRows = false;
			infolist1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			infolist1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			infolist1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			infolist1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			infolist1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			infolist1.Columns.AddRange(Column1, commodity, commodityName, fixedPrice, sellingPrice, quantity, subtotal, discount, total, ReturnNum, ReturnPrice, barcode, detailId);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			infolist1.DefaultCellStyle = dataGridViewCellStyle2;
			infolist1.EnableHeadersVisualStyles = false;
			infolist1.GridColor = System.Drawing.SystemColors.ActiveBorder;
			infolist1.Location = new System.Drawing.Point(28, 91);
			infolist1.Name = "infolist1";
			infolist1.ReadOnly = true;
			infolist1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ScrollBar;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			infolist1.RowHeadersVisible = false;
			infolist1.RowTemplate.Height = 24;
			infolist1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			infolist1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			infolist1.Size = new System.Drawing.Size(932, 237);
			infolist1.TabIndex = 35;
			infolist1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(infolist1_CellContentClick);
			infolist1.SelectionChanged += new System.EventHandler(infolist1_SelectionChanged);
			Column1.HeaderText = "";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 25;
			commodity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			commodity.DefaultCellStyle = dataGridViewCellStyle4;
			commodity.HeaderText = "商品名稱";
			commodity.Name = "commodity";
			commodity.ReadOnly = true;
			commodity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			commodityName.HeaderText = "商品名稱";
			commodityName.Name = "commodityName";
			commodityName.ReadOnly = true;
			commodityName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			commodityName.Visible = false;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			fixedPrice.DefaultCellStyle = dataGridViewCellStyle5;
			fixedPrice.HeaderText = "定價";
			fixedPrice.Name = "fixedPrice";
			fixedPrice.ReadOnly = true;
			fixedPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			fixedPrice.Width = 70;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			sellingPrice.DefaultCellStyle = dataGridViewCellStyle6;
			sellingPrice.HeaderText = "售價";
			sellingPrice.Name = "sellingPrice";
			sellingPrice.ReadOnly = true;
			sellingPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			sellingPrice.Width = 70;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			quantity.DefaultCellStyle = dataGridViewCellStyle7;
			quantity.HeaderText = "數量";
			quantity.Name = "quantity";
			quantity.ReadOnly = true;
			quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			quantity.Width = 70;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			subtotal.DefaultCellStyle = dataGridViewCellStyle8;
			subtotal.HeaderText = "小計";
			subtotal.Name = "subtotal";
			subtotal.ReadOnly = true;
			subtotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			subtotal.Width = 70;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			discount.DefaultCellStyle = dataGridViewCellStyle9;
			discount.HeaderText = "折讓";
			discount.Name = "discount";
			discount.ReadOnly = true;
			discount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			discount.Width = 70;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			total.DefaultCellStyle = dataGridViewCellStyle10;
			total.HeaderText = "合計";
			total.Name = "total";
			total.ReadOnly = true;
			total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			total.Width = 70;
			ReturnNum.HeaderText = "退貨(數量)";
			ReturnNum.Name = "ReturnNum";
			ReturnNum.ReadOnly = true;
			ReturnNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			ReturnNum.Width = 95;
			ReturnPrice.HeaderText = "退貨(金額)";
			ReturnPrice.Name = "ReturnPrice";
			ReturnPrice.ReadOnly = true;
			ReturnPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			ReturnPrice.Width = 95;
			barcode.HeaderText = "條碼";
			barcode.Name = "barcode";
			barcode.ReadOnly = true;
			barcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			barcode.Visible = false;
			detailId.HeaderText = "紀錄id";
			detailId.Name = "detailId";
			detailId.ReadOnly = true;
			detailId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			detailId.Visible = false;
			label_sellDate.AutoSize = true;
			label_sellDate.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label_sellDate.ForeColor = System.Drawing.Color.Black;
			label_sellDate.Location = new System.Drawing.Point(647, 49);
			label_sellDate.Name = "label_sellDate";
			label_sellDate.Size = new System.Drawing.Size(98, 24);
			label_sellDate.TabIndex = 34;
			label_sellDate.Text = "{sellDate}";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label7.ForeColor = System.Drawing.Color.Black;
			label7.Image = POS_Client.Properties.Resources.oblique;
			label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label7.Location = new System.Drawing.Point(497, 49);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(144, 24);
			label7.TabIndex = 33;
			label7.Text = "   銷售日期時間:";
			label_sellNo.AutoSize = true;
			label_sellNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label_sellNo.ForeColor = System.Drawing.Color.Black;
			label_sellNo.Location = new System.Drawing.Point(136, 49);
			label_sellNo.Name = "label_sellNo";
			label_sellNo.Size = new System.Drawing.Size(82, 24);
			label_sellNo.TabIndex = 32;
			label_sellNo.Text = "{sellNo}";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label5.ForeColor = System.Drawing.Color.Black;
			label5.Image = POS_Client.Properties.Resources.oblique;
			label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label5.Location = new System.Drawing.Point(24, 49);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(106, 24);
			label5.TabIndex = 31;
			label5.Text = "   銷售單號:";
			alertMsg.Font = new System.Drawing.Font("微軟正黑體", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			alertMsg.Location = new System.Drawing.Point(500, 453);
			alertMsg.Multiline = true;
			alertMsg.Name = "alertMsg";
			alertMsg.ReadOnly = true;
			alertMsg.Size = new System.Drawing.Size(319, 27);
			alertMsg.TabIndex = 30;
			paymoney.Font = new System.Drawing.Font("Calibri", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			paymoney.Location = new System.Drawing.Point(120, 453);
			paymoney.Name = "paymoney";
			paymoney.ReadOnly = true;
			paymoney.Size = new System.Drawing.Size(359, 26);
			paymoney.TabIndex = 29;
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.Black;
			label13.Location = new System.Drawing.Point(28, 451);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(86, 24);
			label13.TabIndex = 28;
			label13.Text = "執行動作";
			label_totalCost.AutoSize = true;
			label_totalCost.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_totalCost.ForeColor = System.Drawing.Color.Black;
			label_totalCost.Location = new System.Drawing.Point(252, 342);
			label_totalCost.Name = "label_totalCost";
			label_totalCost.Size = new System.Drawing.Size(86, 24);
			label_totalCost.TabIndex = 11;
			label_totalCost.Text = "消費累積";
			label_credit.AutoSize = true;
			label_credit.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_credit.ForeColor = System.Drawing.Color.Black;
			label_credit.Location = new System.Drawing.Point(503, 342);
			label_credit.Name = "label_credit";
			label_credit.Size = new System.Drawing.Size(86, 24);
			label_credit.TabIndex = 10;
			label_credit.Text = "賒帳金額";
			label_refund.AutoSize = true;
			label_refund.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_refund.ForeColor = System.Drawing.Color.Black;
			label_refund.Location = new System.Drawing.Point(28, 407);
			label_refund.Name = "label_refund";
			label_refund.Size = new System.Drawing.Size(86, 24);
			label_refund.TabIndex = 52;
			label_refund.Text = "退款小計";
			text_refund.AutoSize = true;
			text_refund.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_refund.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			text_refund.Location = new System.Drawing.Point(120, 407);
			text_refund.Name = "text_refund";
			text_refund.Size = new System.Drawing.Size(33, 24);
			text_refund.TabIndex = 53;
			text_refund.Text = "{2}";
			label_sumDiscount.AutoSize = true;
			label_sumDiscount.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_sumDiscount.ForeColor = System.Drawing.Color.Black;
			label_sumDiscount.Location = new System.Drawing.Point(252, 375);
			label_sumDiscount.Name = "label_sumDiscount";
			label_sumDiscount.Size = new System.Drawing.Size(86, 24);
			label_sumDiscount.TabIndex = 54;
			label_sumDiscount.Text = "總價折讓";
			text_sumDiscount.AutoSize = true;
			text_sumDiscount.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_sumDiscount.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			text_sumDiscount.Location = new System.Drawing.Point(344, 375);
			text_sumDiscount.Name = "text_sumDiscount";
			text_sumDiscount.Size = new System.Drawing.Size(33, 24);
			text_sumDiscount.TabIndex = 55;
			text_sumDiscount.Text = "{4}";
			label_SdControl.AutoSize = true;
			label_SdControl.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_SdControl.ForeColor = System.Drawing.Color.Black;
			label_SdControl.Location = new System.Drawing.Point(240, 407);
			label_SdControl.Name = "label_SdControl";
			label_SdControl.Size = new System.Drawing.Size(98, 24);
			label_SdControl.TabIndex = 56;
			label_SdControl.Text = "折讓(調整)";
			label_originalSum.AutoSize = true;
			label_originalSum.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_originalSum.ForeColor = System.Drawing.Color.Black;
			label_originalSum.Location = new System.Drawing.Point(503, 375);
			label_originalSum.Name = "label_originalSum";
			label_originalSum.Size = new System.Drawing.Size(86, 24);
			label_originalSum.TabIndex = 57;
			label_originalSum.Text = "銷售總額";
			text_originalSum.AutoSize = true;
			text_originalSum.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_originalSum.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			text_originalSum.Location = new System.Drawing.Point(595, 375);
			text_originalSum.Name = "text_originalSum";
			text_originalSum.Size = new System.Drawing.Size(33, 24);
			text_originalSum.TabIndex = 58;
			text_originalSum.Text = "{6}";
			label_CashCredit.AutoSize = true;
			label_CashCredit.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_CashCredit.ForeColor = System.Drawing.Color.Black;
			label_CashCredit.Location = new System.Drawing.Point(716, 375);
			label_CashCredit.Name = "label_CashCredit";
			label_CashCredit.Size = new System.Drawing.Size(98, 24);
			label_CashCredit.TabIndex = 60;
			label_CashCredit.Text = "結帳(賒帳)";
			text_CashCredit.AutoSize = true;
			text_CashCredit.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_CashCredit.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			text_CashCredit.Location = new System.Drawing.Point(820, 375);
			text_CashCredit.Name = "text_CashCredit";
			text_CashCredit.Size = new System.Drawing.Size(33, 24);
			text_CashCredit.TabIndex = 61;
			text_CashCredit.Text = "{8}";
			textBox_sumDiscount.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			textBox_sumDiscount.Location = new System.Drawing.Point(348, 407);
			textBox_sumDiscount.Name = "textBox_sumDiscount";
			textBox_sumDiscount.ReadOnly = true;
			textBox_sumDiscount.Size = new System.Drawing.Size(75, 27);
			textBox_sumDiscount.TabIndex = 62;
			textBox_refund.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			textBox_refund.ImeMode = System.Windows.Forms.ImeMode.Disable;
			textBox_refund.Location = new System.Drawing.Point(581, 407);
			textBox_refund.Name = "textBox_refund";
			textBox_refund.Size = new System.Drawing.Size(75, 27);
			textBox_refund.TabIndex = 63;
			textBox_refund.KeyPress += new System.Windows.Forms.KeyPressEventHandler(digitOnly_KeyPress);
			textBox_refund.KeyUp += new System.Windows.Forms.KeyEventHandler(tb_newDiscount_KeyUp);
			label_refundControl.AutoSize = true;
			label_refundControl.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_refundControl.ForeColor = System.Drawing.Color.Black;
			label_refundControl.Location = new System.Drawing.Point(469, 407);
			label_refundControl.Name = "label_refundControl";
			label_refundControl.Size = new System.Drawing.Size(111, 24);
			label_refundControl.TabIndex = 64;
			label_refundControl.Text = "退款(調整) -";
			btn_enter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_enter.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_enter.Image = POS_Client.Properties.Resources.ic_input_black_24dp_1x;
			btn_enter.Location = new System.Drawing.Point(659, 407);
			btn_enter.Name = "btn_enter";
			btn_enter.Size = new System.Drawing.Size(48, 27);
			btn_enter.TabIndex = 65;
			btn_enter.UseVisualStyleBackColor = true;
			btn_enter.Click += new System.EventHandler(btn_enter_Click);
			btn_back.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_back.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_back.ForeColor = System.Drawing.Color.White;
			btn_back.Location = new System.Drawing.Point(866, 40);
			btn_back.Name = "btn_back";
			btn_back.Size = new System.Drawing.Size(115, 47);
			btn_back.TabIndex = 67;
			btn_back.Text = "返回前頁";
			btn_back.UseVisualStyleBackColor = false;
			btn_back.Visible = false;
			btn_back.Click += new System.EventHandler(btn_back_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(252, 252, 237);
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(991, 671);
			base.Controls.Add(btn_back);
			base.Controls.Add(btn_enter);
			base.Controls.Add(label_refundControl);
			base.Controls.Add(textBox_refund);
			base.Controls.Add(textBox_sumDiscount);
			base.Controls.Add(text_CashCredit);
			base.Controls.Add(label_CashCredit);
			base.Controls.Add(text_originalSum);
			base.Controls.Add(label_originalSum);
			base.Controls.Add(label_SdControl);
			base.Controls.Add(text_sumDiscount);
			base.Controls.Add(label_sumDiscount);
			base.Controls.Add(text_refund);
			base.Controls.Add(label_refund);
			base.Controls.Add(text_totalCost);
			base.Controls.Add(text_credit);
			base.Controls.Add(panel2);
			base.Controls.Add(changeLog);
			base.Controls.Add(text_sellstate);
			base.Controls.Add(label5);
			base.Controls.Add(label_sellstate);
			base.Controls.Add(label_credit);
			base.Controls.Add(text_sum);
			base.Controls.Add(label_totalCost);
			base.Controls.Add(label_sum);
			base.Controls.Add(text_uID);
			base.Controls.Add(label13);
			base.Controls.Add(label_uID);
			base.Controls.Add(paymoney);
			base.Controls.Add(text_memberName);
			base.Controls.Add(alertMsg);
			base.Controls.Add(label_member);
			base.Controls.Add(label_sellNo);
			base.Controls.Add(infolist1);
			base.Controls.Add(label7);
			base.Controls.Add(label_sellDate);
			base.Name = "frmMainShopSimpleReturnWithMoney";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "frmMainShop";
			base.Controls.SetChildIndex(label_sellDate, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(infolist1, 0);
			base.Controls.SetChildIndex(label_sellNo, 0);
			base.Controls.SetChildIndex(label_member, 0);
			base.Controls.SetChildIndex(alertMsg, 0);
			base.Controls.SetChildIndex(text_memberName, 0);
			base.Controls.SetChildIndex(paymoney, 0);
			base.Controls.SetChildIndex(label_uID, 0);
			base.Controls.SetChildIndex(label13, 0);
			base.Controls.SetChildIndex(text_uID, 0);
			base.Controls.SetChildIndex(label_sum, 0);
			base.Controls.SetChildIndex(label_totalCost, 0);
			base.Controls.SetChildIndex(text_sum, 0);
			base.Controls.SetChildIndex(label_credit, 0);
			base.Controls.SetChildIndex(label_sellstate, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(text_sellstate, 0);
			base.Controls.SetChildIndex(changeLog, 0);
			base.Controls.SetChildIndex(panel2, 0);
			base.Controls.SetChildIndex(text_credit, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(text_totalCost, 0);
			base.Controls.SetChildIndex(label_refund, 0);
			base.Controls.SetChildIndex(text_refund, 0);
			base.Controls.SetChildIndex(label_sumDiscount, 0);
			base.Controls.SetChildIndex(text_sumDiscount, 0);
			base.Controls.SetChildIndex(label_SdControl, 0);
			base.Controls.SetChildIndex(label_originalSum, 0);
			base.Controls.SetChildIndex(text_originalSum, 0);
			base.Controls.SetChildIndex(label_CashCredit, 0);
			base.Controls.SetChildIndex(text_CashCredit, 0);
			base.Controls.SetChildIndex(textBox_sumDiscount, 0);
			base.Controls.SetChildIndex(textBox_refund, 0);
			base.Controls.SetChildIndex(label_refundControl, 0);
			base.Controls.SetChildIndex(btn_enter, 0);
			base.Controls.SetChildIndex(btn_back, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)infolist1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
