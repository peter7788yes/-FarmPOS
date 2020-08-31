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
	public class frmMainShopSimpleReturn : MasterThinForm
	{
		public int listindex = 1;

		public int columnOfFocus;

		private int olditem;

		private int oldtotal;

		private string temp1;

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

		private IContainer components;

		private Panel panel2;

		private Label label2;

		private Label label1;

		public Button backto;

		private Label total;

		private Label items;

		private Label label13;

		private TextBox paymoney;

		public Button button2;

		private TextBox alertMsg;

		public Button Checkout;

		public Button button5;

		public Button button4;

		public Button button3;

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

		private Label sellDate;

		private Label label7;

		private Label sellNo;

		private Label label5;

		public DataGridView infolist1;

		private Label label15;

		private Label sellstate;

		private Label label12;

		private Label address;

		private Label label10;

		private Label uID;

		private Label label8;

		private Label memberName;

		private Label member;

		private Label distotal;

		private Label disitem;

		private Button button1;

		private Button button6;

		private DataGridViewTextBoxColumn Column1;

		private frmMainShopSimpleWithMoney.CustomColumn commodity;

		private DataGridViewTextBoxColumn quantity;

		private DataGridViewTextBoxColumn ReturnNum;

		private DataGridViewTextBoxColumn barcode;

		private DataGridViewTextBoxColumn detailId;

		private Button btn_back;

		public frmMainShopSimpleReturn(string sellno, string returnType, string GDSNO)
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
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_main_sell", "sellNo = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				vipNo = dataTable.Rows[0]["memberId"].ToString();
				bool flag = false;
				if ("1".Equals(dataTable.Rows[0]["status"].ToString()))
				{
					flag = true;
				}
				DateTime dateTime = DateTime.Now.AddMonths(-12);
				DateTime now = DateTime.Now;
				DateTime t = new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0);
				if (DateTime.Compare(Convert.ToDateTime(dataTable.Rows[0]["sellTime"].ToString()), t) < 0 || flag || !"".Equals(dataTable.Rows[0]["oldECRHDHSNo"].ToString()))
				{
					isover = true;
				}
				string[] strWhereParameterArray2 = new string[1]
				{
					dataTable.Rows[0]["sellNo"].ToString()
				};
				DataTable obj = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_detail_sell", "sellNo = {0} ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				int num = 1;
				foreach (DataRow row in obj.Rows)
				{
					string[] strWhereParameterArray3 = new string[1]
					{
						row["barcode"].ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_GOODSLST", "GDSNO = {0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
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
					commodityInfo.setWidth(730);
					infolist1.Rows.Add(num.ToString(), commodityInfo, row["num"].ToString(), "0", row["barcode"].ToString(), row["sellDeatialId"].ToString());
					infolist1.Rows[0].Selected = false;
					num++;
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
				foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
				{
					item.Height = 100;
				}
				sellNo.Text = sellno;
				sellDate.Text = dataTable.Rows[0]["sellTime"].ToString();
				string[] strWhereParameterArray4 = new string[1]
				{
					dataTable.Rows[0]["memberId"].ToString()
				};
				DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hcr.*,ara.area as ae,ara.zipcode as az,adr.city as ac", "hypos_CUST_RTL as hcr,ADDRAREA as ara,ADDRCITY as adr", "VipNo ={0} and adr.cityno =hcr.City and ara.zipcode =hcr.Area", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable3.Rows.Count > 0)
				{
					memberName.Text = dataTable3.Rows[0]["Name"].ToString() + "(" + dataTable3.Rows[0]["Mobile"].ToString() + ")";
					address.Text = dataTable3.Rows[0]["az"].ToString() + dataTable3.Rows[0]["ac"].ToString() + dataTable3.Rows[0]["ae"].ToString() + dataTable3.Rows[0]["Address"].ToString();
					uID.Text = dataTable3.Rows[0]["IdNo"].ToString();
				}
				else
				{
					memberName.Text = "非會員";
					address.Text = "";
					uID.Text = "";
				}
				if (dataTable.Rows[0]["status"].ToString() == "0")
				{
					sellstate.Text = "正常";
				}
				else if (dataTable.Rows[0]["status"].ToString() == "1")
				{
					sellstate.Text = "取消";
				}
				else
				{
					sellstate.Text = "變更";
				}
				items.Text = "(" + dataTable.Rows[0]["items"].ToString() + ")";
				olditem = int.Parse(dataTable.Rows[0]["items"].ToString());
				total.Text = "(" + dataTable.Rows[0]["itemstotal"].ToString() + ")";
				oldtotal = int.Parse(dataTable.Rows[0]["itemstotal"].ToString());
				disitem.Text = "";
				distotal.Text = "";
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
			else if ("frmSearchSell".Equals(_returnType))
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
				else
				{
					switchForm(new frmSearchSell());
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
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			if (flag)
			{
				if (disitem.Text.ToString() == "")
				{
					disitem.Text = "0";
				}
				if (distotal.Text.ToString() == "")
				{
					distotal.Text = "0";
				}
				string[] strParameterArray = new string[4]
				{
					disitem.Text.ToString(),
					distotal.Text.ToString(),
					text,
					sellno
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_main_sell SET items = items+{0}, itemstotal= itemstotal+{1},editDate ={2},changcount = changcount+1,status = 2  where sellNo = {3}  ", strParameterArray, CommandOperationType.ExecuteNonQuery);
				int num3 = 0;
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
						"ischange",
						"1"
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_mainsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
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
						"0"
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
				string sql = "select sellLogId from hypos_mainsell_log order by sellLogId desc LIMIT 0,1";
				string text2 = DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar).ToString();
				foreach (string item3 in changeId)
				{
					string[] strWhereParameterArray = new string[2]
					{
						sellno,
						item3
					};
					DataTable obj = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "num,PRNO,BLNO", "hypos_detail_sell", "sellNo = {0} and sellDeatialId={1}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
					string text3 = obj.Rows[0]["num"].ToString();
					string text4 = obj.Rows[0]["PRNO"].ToString();
					string text5 = obj.Rows[0]["BLNO"].ToString();
					string text6 = changeNum[num3].ToString();
					string[] strParameterArray2 = new string[3]
					{
						text6,
						sellno,
						item3
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_detail_sell SET num = num-{0} where sellNo = {1} and sellDeatialId={2} ", strParameterArray2, CommandOperationType.ExecuteNonQuery);
					strFieldArray = new string[7, 2]
					{
						{
							"sellLogId",
							text2
						},
						{
							"barcode",
							changeBarcode[num3]
						},
						{
							"num",
							text3
						},
						{
							"diffNum",
							text6
						},
						{
							"PRNO",
							text4
						},
						{
							"BLNO",
							text5
						},
						{
							"sellDetailId",
							item3
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_detailsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
					string[] strWhereParameterArray2 = new string[1]
					{
						changeBarcode[num3]
					};
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDSNO, inventory", "hypos_GOODSLST", "GDSNO = {0} ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
					string text7 = changeNum[num3].ToString();
					if (!string.IsNullOrEmpty(dataTable.Rows[0]["inventory"].ToString()))
					{
						text7 = (int.Parse(dataTable.Rows[0]["inventory"].ToString()) + int.Parse(text7)).ToString();
					}
					string[] strParameterArray3 = new string[2]
					{
						text7,
						changeBarcode[num3]
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST SET inventory ={0} where GDSNO ={1} ", strParameterArray3, CommandOperationType.ExecuteNonQuery);
					num3++;
				}
				AutoClosingMessageBox.Show("點選「確定」直接列印收據、返回銷售單查詢頁");
				string[] strWhereParameterArray3 = new string[1]
				{
					sellno
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "changcount", "hypos_main_sell", "sellNo = {0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
				frmSell_SellNo frmSell_SellNo = new frmSell_SellNo(sellno, int.Parse(dataTable2.Rows[0]["changcount"].ToString()), "");
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
				else
				{
					switchForm(new frmSearchSell());
				}
				frmSell_SellNo.Show();
				return;
			}
			string[] strParameterArray4 = new string[2]
			{
				sellno,
				text
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_main_sell SET status = 1,editDate ={1} where sellNo = {0}", strParameterArray4, CommandOperationType.ExecuteNonQuery);
			string[,] strFieldArray2 = new string[3, 2]
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
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_mainsell_log", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
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
					"0"
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
			string[] strWhereParameterArray4 = new string[1]
			{
				sellno
			};
			DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "barcode, num", "hypos_detail_sell", "sellNo = {0}", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
			for (int i = 0; i < dataTable3.Rows.Count; i++)
			{
				string text8 = dataTable3.Rows[i]["barcode"].ToString();
				string text9 = dataTable3.Rows[i]["num"].ToString();
				string[] strWhereParameterArray5 = new string[1]
				{
					text8
				};
				DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDSNO, inventory", "hypos_GOODSLST", "GDSNO = {0} ", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable);
				string text10 = text9;
				if (!string.IsNullOrEmpty(dataTable4.Rows[0]["inventory"].ToString()))
				{
					text10 = (int.Parse(dataTable4.Rows[0]["inventory"].ToString()) + int.Parse(text9)).ToString();
				}
				string[] strParameterArray5 = new string[2]
				{
					text10,
					text8
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST SET inventory ={0} where GDSNO ={1} ", strParameterArray5, CommandOperationType.ExecuteNonQuery);
			}
			AutoClosingMessageBox.Show("訂單取消完成");
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
			else
			{
				switchForm(new frmSearchSell());
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string[,] strFieldArray = new string[3, 2]
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
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_mainsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			string[] strWhereParameterArray = new string[1]
			{
				sellno
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "changcount", "hypos_main_sell", "sellNo = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			frmSell_SellNo frmSell_SellNo = new frmSell_SellNo(sellno, int.Parse(dataTable.Rows[0]["changcount"].ToString()), "");
			AutoClosingMessageBox.Show("收據補印完成");
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
			else
			{
				switchForm(new frmSearchSell());
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

		private void button3_Click(object sender, EventArgs e)
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
			}
			distotal.Text = "";
			changeId.Clear();
			changeNum.Clear();
			changeBarcode.Clear();
			temp1 = "";
			for (int i = 0; i < infolist1.Rows.Count; i++)
			{
				infolist1.Rows[i].Selected = false;
			}
		}

		private void button4_Click(object sender, EventArgs e)
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
									computetotal();
								}
								else
								{
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

		private void button5_Click(object sender, EventArgs e)
		{
			if (isover || MessageBox.Show("整筆訂單取消，確認後將無法復原。確定取消？", "整筆取消", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return;
			}
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string[] strParameterArray = new string[2]
			{
				sellno,
				text
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_main_sell SET status = 1,editDate ={1} where sellNo = {0}", strParameterArray, CommandOperationType.ExecuteNonQuery);
			string[,] strFieldArray = new string[3, 2]
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
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_mainsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			string sql = "select sellLogId from hypos_mainsell_log order by sellLogId desc LIMIT 0,1";
			string text2 = DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar).ToString();
			strParameterArray = new string[1]
			{
				sellno
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_detail_sell", "sellNo ={0} and num > 0", "", null, strParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				strFieldArray = new string[7, 2]
				{
					{
						"sellLogId",
						text2
					},
					{
						"barcode",
						dataTable.Rows[i]["barcode"].ToString()
					},
					{
						"num",
						dataTable.Rows[i]["num"].ToString()
					},
					{
						"diffNum",
						dataTable.Rows[i]["num"].ToString()
					},
					{
						"PRNO",
						dataTable.Rows[i]["PRNO"].ToString()
					},
					{
						"BLNO",
						dataTable.Rows[i]["BLNO"].ToString()
					},
					{
						"sellDetailId",
						dataTable.Rows[i]["sellDeatialId"].ToString()
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_detailsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			}
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
					"0"
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
			string[] strWhereParameterArray = new string[1]
			{
				sellno
			};
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "barcode, num", "hypos_detail_sell", "sellNo = {0}", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			for (int j = 0; j < dataTable2.Rows.Count; j++)
			{
				string text3 = dataTable2.Rows[j]["barcode"].ToString();
				string text4 = dataTable2.Rows[j]["num"].ToString();
				string[] strWhereParameterArray2 = new string[1]
				{
					text3
				};
				DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDSNO, inventory", "hypos_GOODSLST", "GDSNO = {0} ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				string text5 = text4;
				if (!string.IsNullOrEmpty(dataTable3.Rows[0]["inventory"].ToString()))
				{
					text5 = (int.Parse(dataTable3.Rows[0]["inventory"].ToString()) + int.Parse(text4)).ToString();
				}
				string[] strParameterArray2 = new string[2]
				{
					text5,
					text3
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST SET inventory ={0} where GDSNO ={1} ", strParameterArray2, CommandOperationType.ExecuteNonQuery);
			}
			AutoClosingMessageBox.Show("訂單取消完成");
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
			else
			{
				switchForm(new frmSearchSell());
			}
		}

		private void infolist1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 6)
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
				infolist1.CurrentRow.Cells["ReturnNum"].Value = (int.Parse(infolist1.CurrentRow.Cells["ReturnNum"].Value.ToString()) - 1).ToString();
				computetotal();
			}
			else
			{
				if (e.ColumnIndex != 7)
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
				computetotal();
				infolist1.CurrentRow.Cells["btn2"].ReadOnly = true;
				infolist1.CurrentRow.Cells["btn1"].ReadOnly = true;
			}
		}

		private void computetotal()
		{
			int num = 0;
			foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
			{
				num += int.Parse(item.Cells["quantity"].Value.ToString());
			}
			distotal.Text = (num - oldtotal).ToString();
		}

		private void infolist1_SelectionChanged(object sender, EventArgs e)
		{
			if (infolist1.Rows.Count > 0)
			{
				paymoney.Text = infolist1.CurrentRow.Cells["barcode"].Value.ToString();
			}
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

		private void alertMessage(string msg)
		{
			alertMsg.Text = msg;
		}

		private void button1_Click(object sender, EventArgs e)
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

		private void button6_Click(object sender, EventArgs e)
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

		private void label15_Click(object sender, EventArgs e)
		{
			new frmMainshopSimpleChangeLog(sellno).ShowDialog();
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
			panel2 = new System.Windows.Forms.Panel();
			button1 = new System.Windows.Forms.Button();
			button6 = new System.Windows.Forms.Button();
			button5 = new System.Windows.Forms.Button();
			button4 = new System.Windows.Forms.Button();
			button3 = new System.Windows.Forms.Button();
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
			button2 = new System.Windows.Forms.Button();
			backto = new System.Windows.Forms.Button();
			Checkout = new System.Windows.Forms.Button();
			total = new System.Windows.Forms.Label();
			distotal = new System.Windows.Forms.Label();
			items = new System.Windows.Forms.Label();
			disitem = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			sellstate = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			address = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			uID = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			memberName = new System.Windows.Forms.Label();
			member = new System.Windows.Forms.Label();
			infolist1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			commodity = new POS_Client.frmMainShopSimpleWithMoney.CustomColumn();
			quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			ReturnNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
			barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			detailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			sellDate = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			sellNo = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			alertMsg = new System.Windows.Forms.TextBox();
			paymoney = new System.Windows.Forms.TextBox();
			label13 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			btn_back = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)infolist1).BeginInit();
			SuspendLayout();
			pb_virtualKeyBoard.Location = new System.Drawing.Point(975, 640);
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 678);
			pb_virtualKeyBoard.Visible = false;
			panel2.BackgroundImage = POS_Client.Properties.Resources.inside_button;
			panel2.Controls.Add(button1);
			panel2.Controls.Add(button6);
			panel2.Controls.Add(button5);
			panel2.Controls.Add(button4);
			panel2.Controls.Add(button3);
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
			panel2.Controls.Add(button2);
			panel2.Controls.Add(backto);
			panel2.Controls.Add(Checkout);
			panel2.Location = new System.Drawing.Point(12, 473);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(966, 200);
			panel2.TabIndex = 38;
			button1.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button1.Location = new System.Drawing.Point(527, 8);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(42, 90);
			button1.TabIndex = 55;
			button1.Text = "↑";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			button6.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button6.Location = new System.Drawing.Point(527, 102);
			button6.Name = "button6";
			button6.Size = new System.Drawing.Size(42, 91);
			button6.TabIndex = 54;
			button6.Text = "↓";
			button6.UseVisualStyleBackColor = true;
			button6.Click += new System.EventHandler(button6_Click);
			button5.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button5.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button5.ForeColor = System.Drawing.Color.White;
			button5.Location = new System.Drawing.Point(685, 8);
			button5.Name = "button5";
			button5.Size = new System.Drawing.Size(102, 186);
			button5.TabIndex = 53;
			button5.Text = "整筆取消";
			button5.UseVisualStyleBackColor = false;
			button5.Click += new System.EventHandler(button5_Click);
			button4.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button4.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button4.ForeColor = System.Drawing.Color.White;
			button4.Location = new System.Drawing.Point(576, 103);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(102, 90);
			button4.TabIndex = 52;
			button4.Text = "退貨";
			button4.UseVisualStyleBackColor = false;
			button4.Click += new System.EventHandler(button4_Click);
			button3.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button3.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button3.ForeColor = System.Drawing.Color.White;
			button3.Location = new System.Drawing.Point(576, 8);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(102, 90);
			button3.TabIndex = 51;
			button3.Text = "清除輸入";
			button3.UseVisualStyleBackColor = false;
			button3.Click += new System.EventHandler(button3_Click);
			numnine.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numnine.Location = new System.Drawing.Point(478, 8);
			numnine.Name = "numnine";
			numnine.Size = new System.Drawing.Size(42, 42);
			numnine.TabIndex = 50;
			numnine.Text = "9";
			numnine.UseVisualStyleBackColor = true;
			numnine.Click += new System.EventHandler(numnine_Click);
			numeight.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numeight.Location = new System.Drawing.Point(429, 8);
			numeight.Name = "numeight";
			numeight.Size = new System.Drawing.Size(42, 42);
			numeight.TabIndex = 49;
			numeight.Text = "8";
			numeight.UseVisualStyleBackColor = true;
			numeight.Click += new System.EventHandler(numeight_Click);
			numseven.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numseven.Location = new System.Drawing.Point(380, 8);
			numseven.Name = "numseven";
			numseven.Size = new System.Drawing.Size(42, 42);
			numseven.TabIndex = 48;
			numseven.Text = "7";
			numseven.UseVisualStyleBackColor = true;
			numseven.Click += new System.EventHandler(numseven_Click);
			numfive.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfive.Location = new System.Drawing.Point(429, 56);
			numfive.Name = "numfive";
			numfive.Size = new System.Drawing.Size(42, 42);
			numfive.TabIndex = 47;
			numfive.Text = "5";
			numfive.UseVisualStyleBackColor = true;
			numfive.Click += new System.EventHandler(numfive_Click);
			numsix.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numsix.Location = new System.Drawing.Point(478, 56);
			numsix.Name = "numsix";
			numsix.Size = new System.Drawing.Size(42, 42);
			numsix.TabIndex = 46;
			numsix.Text = "6";
			numsix.UseVisualStyleBackColor = true;
			numsix.Click += new System.EventHandler(numsix_Click);
			numfour.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfour.Location = new System.Drawing.Point(380, 56);
			numfour.Name = "numfour";
			numfour.Size = new System.Drawing.Size(42, 42);
			numfour.TabIndex = 45;
			numfour.Text = "4";
			numfour.UseVisualStyleBackColor = true;
			numfour.Click += new System.EventHandler(numfour_Click);
			numthree.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numthree.Location = new System.Drawing.Point(478, 104);
			numthree.Name = "numthree";
			numthree.Size = new System.Drawing.Size(42, 42);
			numthree.TabIndex = 44;
			numthree.Text = "3";
			numthree.UseVisualStyleBackColor = true;
			numthree.Click += new System.EventHandler(numthree_Click);
			numtwo.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numtwo.Location = new System.Drawing.Point(429, 104);
			numtwo.Name = "numtwo";
			numtwo.Size = new System.Drawing.Size(42, 42);
			numtwo.TabIndex = 43;
			numtwo.Text = "2";
			numtwo.UseVisualStyleBackColor = true;
			numtwo.Click += new System.EventHandler(numtwo_Click);
			numzero.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numzero.Location = new System.Drawing.Point(380, 151);
			numzero.Name = "numzero";
			numzero.Size = new System.Drawing.Size(42, 42);
			numzero.TabIndex = 42;
			numzero.Text = "0";
			numzero.UseVisualStyleBackColor = true;
			numzero.Click += new System.EventHandler(numzero_Click);
			backspace.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			backspace.Location = new System.Drawing.Point(428, 151);
			backspace.Name = "backspace";
			backspace.Size = new System.Drawing.Size(93, 42);
			backspace.TabIndex = 41;
			backspace.Text = "backspace";
			backspace.UseVisualStyleBackColor = true;
			backspace.Click += new System.EventHandler(backspace_Click);
			numone.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numone.Location = new System.Drawing.Point(380, 103);
			numone.Name = "numone";
			numone.Size = new System.Drawing.Size(42, 42);
			numone.TabIndex = 40;
			numone.Text = "1";
			numone.UseVisualStyleBackColor = true;
			numone.Click += new System.EventHandler(numone_Click);
			button2.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button2.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button2.ForeColor = System.Drawing.Color.White;
			button2.Location = new System.Drawing.Point(282, 8);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(91, 183);
			button2.TabIndex = 39;
			button2.Text = "補印收據";
			button2.UseVisualStyleBackColor = false;
			button2.Click += new System.EventHandler(button2_Click);
			backto.BackColor = System.Drawing.Color.FromArgb(56, 175, 190);
			backto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			backto.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			backto.ForeColor = System.Drawing.Color.White;
			backto.Location = new System.Drawing.Point(182, 8);
			backto.Name = "backto";
			backto.Size = new System.Drawing.Size(93, 183);
			backto.TabIndex = 37;
			backto.Text = "結束編修";
			backto.UseVisualStyleBackColor = false;
			backto.Click += new System.EventHandler(backto_Click);
			Checkout.BackColor = System.Drawing.Color.FromArgb(250, 87, 0);
			Checkout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			Checkout.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			Checkout.ForeColor = System.Drawing.Color.White;
			Checkout.Location = new System.Drawing.Point(794, 8);
			Checkout.Name = "Checkout";
			Checkout.Size = new System.Drawing.Size(102, 186);
			Checkout.TabIndex = 36;
			Checkout.Text = "儲存變更";
			Checkout.UseVisualStyleBackColor = false;
			Checkout.Click += new System.EventHandler(Checkout_Click);
			total.AutoSize = true;
			total.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			total.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			total.Location = new System.Drawing.Point(820, 342);
			total.Name = "total";
			total.Size = new System.Drawing.Size(64, 24);
			total.TabIndex = 14;
			total.Text = "label4";
			distotal.AutoSize = true;
			distotal.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			distotal.ForeColor = System.Drawing.Color.Red;
			distotal.Location = new System.Drawing.Point(890, 342);
			distotal.Name = "distotal";
			distotal.Size = new System.Drawing.Size(64, 24);
			distotal.TabIndex = 46;
			distotal.Text = "label6";
			items.AutoSize = true;
			items.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			items.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			items.Location = new System.Drawing.Point(607, 342);
			items.Name = "items";
			items.Size = new System.Drawing.Size(64, 24);
			items.TabIndex = 13;
			items.Text = "label4";
			disitem.AutoSize = true;
			disitem.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			disitem.ForeColor = System.Drawing.Color.Red;
			disitem.Location = new System.Drawing.Point(666, 342);
			disitem.Name = "disitem";
			disitem.Size = new System.Drawing.Size(64, 24);
			disitem.TabIndex = 45;
			disitem.Text = "label4";
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label15.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
			label15.Location = new System.Drawing.Point(728, 384);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(86, 24);
			label15.TabIndex = 44;
			label15.Text = "變更記錄";
			label15.Click += new System.EventHandler(label15_Click);
			sellstate.AutoSize = true;
			sellstate.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			sellstate.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			sellstate.Location = new System.Drawing.Point(607, 384);
			sellstate.Name = "sellstate";
			sellstate.Size = new System.Drawing.Size(64, 24);
			sellstate.TabIndex = 43;
			sellstate.Text = "label4";
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.Black;
			label12.Location = new System.Drawing.Point(496, 384);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(105, 24);
			label12.TabIndex = 42;
			label12.Text = "銷售單狀態";
			address.AutoSize = true;
			address.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			address.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			address.Location = new System.Drawing.Point(120, 384);
			address.Name = "address";
			address.Size = new System.Drawing.Size(64, 24);
			address.TabIndex = 41;
			address.Text = "label4";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.Black;
			label10.Location = new System.Drawing.Point(28, 384);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(86, 24);
			label10.TabIndex = 40;
			label10.Text = "會員地址";
			uID.AutoSize = true;
			uID.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			uID.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			uID.Location = new System.Drawing.Point(356, 342);
			uID.Name = "uID";
			uID.Size = new System.Drawing.Size(64, 24);
			uID.TabIndex = 39;
			uID.Text = "label4";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.Black;
			label8.Location = new System.Drawing.Point(264, 342);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(86, 24);
			label8.TabIndex = 38;
			label8.Text = "統一編號";
			memberName.AutoSize = true;
			memberName.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			memberName.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			memberName.Location = new System.Drawing.Point(120, 342);
			memberName.Name = "memberName";
			memberName.Size = new System.Drawing.Size(64, 24);
			memberName.TabIndex = 37;
			memberName.Text = "label4";
			member.AutoSize = true;
			member.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			member.ForeColor = System.Drawing.Color.Black;
			member.Location = new System.Drawing.Point(66, 342);
			member.Name = "member";
			member.Size = new System.Drawing.Size(48, 24);
			member.TabIndex = 36;
			member.Text = "會員";
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
			infolist1.Columns.AddRange(Column1, commodity, quantity, ReturnNum, barcode, detailId);
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
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			quantity.DefaultCellStyle = dataGridViewCellStyle5;
			quantity.HeaderText = "數量";
			quantity.Name = "quantity";
			quantity.ReadOnly = true;
			quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			quantity.Width = 75;
			ReturnNum.HeaderText = "退貨";
			ReturnNum.Name = "ReturnNum";
			ReturnNum.ReadOnly = true;
			ReturnNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
			sellDate.AutoSize = true;
			sellDate.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			sellDate.ForeColor = System.Drawing.Color.Black;
			sellDate.Location = new System.Drawing.Point(647, 49);
			sellDate.Name = "sellDate";
			sellDate.Size = new System.Drawing.Size(65, 24);
			sellDate.TabIndex = 34;
			sellDate.Text = "label6";
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
			sellNo.AutoSize = true;
			sellNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			sellNo.ForeColor = System.Drawing.Color.Black;
			sellNo.Location = new System.Drawing.Point(136, 49);
			sellNo.Name = "sellNo";
			sellNo.Size = new System.Drawing.Size(65, 24);
			sellNo.TabIndex = 32;
			sellNo.Text = "label4";
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
			alertMsg.Location = new System.Drawing.Point(501, 425);
			alertMsg.Multiline = true;
			alertMsg.Name = "alertMsg";
			alertMsg.ReadOnly = true;
			alertMsg.Size = new System.Drawing.Size(319, 27);
			alertMsg.TabIndex = 30;
			paymoney.Font = new System.Drawing.Font("Calibri", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			paymoney.Location = new System.Drawing.Point(123, 425);
			paymoney.Name = "paymoney";
			paymoney.ReadOnly = true;
			paymoney.Size = new System.Drawing.Size(359, 26);
			paymoney.TabIndex = 29;
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.Black;
			label13.Location = new System.Drawing.Point(28, 426);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(86, 24);
			label13.TabIndex = 28;
			label13.Text = "執行動作";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.Black;
			label2.Location = new System.Drawing.Point(766, 342);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(48, 24);
			label2.TabIndex = 11;
			label2.Text = "數量";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.Black;
			label1.Location = new System.Drawing.Point(553, 342);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(48, 24);
			label1.TabIndex = 10;
			label1.Text = "品項";
			btn_back.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_back.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_back.ForeColor = System.Drawing.Color.White;
			btn_back.Location = new System.Drawing.Point(857, 38);
			btn_back.Name = "btn_back";
			btn_back.Size = new System.Drawing.Size(124, 47);
			btn_back.TabIndex = 68;
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
			base.Controls.Add(total);
			base.Controls.Add(distotal);
			base.Controls.Add(items);
			base.Controls.Add(disitem);
			base.Controls.Add(panel2);
			base.Controls.Add(label15);
			base.Controls.Add(sellstate);
			base.Controls.Add(label5);
			base.Controls.Add(label12);
			base.Controls.Add(label1);
			base.Controls.Add(address);
			base.Controls.Add(label2);
			base.Controls.Add(label10);
			base.Controls.Add(uID);
			base.Controls.Add(label13);
			base.Controls.Add(label8);
			base.Controls.Add(paymoney);
			base.Controls.Add(memberName);
			base.Controls.Add(alertMsg);
			base.Controls.Add(member);
			base.Controls.Add(sellNo);
			base.Controls.Add(infolist1);
			base.Controls.Add(label7);
			base.Controls.Add(sellDate);
			base.Name = "frmMainShopSimpleReturn";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "frmMainShop";
			base.Controls.SetChildIndex(sellDate, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(infolist1, 0);
			base.Controls.SetChildIndex(sellNo, 0);
			base.Controls.SetChildIndex(member, 0);
			base.Controls.SetChildIndex(alertMsg, 0);
			base.Controls.SetChildIndex(memberName, 0);
			base.Controls.SetChildIndex(paymoney, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label13, 0);
			base.Controls.SetChildIndex(uID, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(address, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(sellstate, 0);
			base.Controls.SetChildIndex(label15, 0);
			base.Controls.SetChildIndex(panel2, 0);
			base.Controls.SetChildIndex(disitem, 0);
			base.Controls.SetChildIndex(items, 0);
			base.Controls.SetChildIndex(distotal, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(total, 0);
			base.Controls.SetChildIndex(btn_back, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)infolist1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
