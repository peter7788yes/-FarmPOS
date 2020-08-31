using POS_Client.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmMainShopCheckout : MasterThinForm
	{
		public int listindex = 1;

		public int columnOfFocus;

		private string HseqNo = "";

		private frmMainShopSimpleWithMoney fms;

		private DataGridView temp;

		private string vipNo = "";

		private bool _isOK;

		private string _payCash = "";

		private string _credit = "";

		private IContainer components;

		private Panel panel2;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private Label label3;

		public Button backto;

		private Label total;

		private Label items;

		private Label credit;

		private Label label14;

		private Label cash;

		private Label label11;

		private Label label10;

		private Label summoney;

		private Label label8;

		private Label discountmoney;

		private Label label6;

		private Label totalmoney;

		private Label label4;

		private DataGridView infolist1;

		private Label label13;

		private TextBox paymoney;

		public Button button2;

		public Button button1;

		private Label change;

		private Label label16;

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

		private Label idnum;

		private Label label7;

		private Label money;

		private Label label12;

		private Label memberName;

		private Label label17;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewTextBoxColumn commodity;

		private DataGridViewTextBoxColumn setprice;

		private DataGridViewTextBoxColumn sellingprice;

		private DataGridViewTextBoxColumn quantity;

		private DataGridViewTextBoxColumn subtotal;

		private DataGridViewTextBoxColumn discount;

		private DataGridViewTextBoxColumn sum;

		private DataGridViewTextBoxColumn barcode;

		private DataGridViewTextBoxColumn cropId;

		private DataGridViewTextBoxColumn pestId;

		public frmMainShopCheckout(frmMainShopSimpleWithMoney fms, DataGridView temp, string VipNo)
			: base("銷售作業")
		{
			this.fms = fms;
			infolist1 = new DataGridView();
			vipNo = VipNo;
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
			if (vipNo != "" && vipNo != null)
			{
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hcr.*,ara.area as ae,adr.city as ac", "hypos_CUST_RTL as hcr,ADDRAREA as ara,ADDRCITY as adr", "hcr.VipNo = {0} and adr.cityno = hcr.City and ara.zipcode = hcr.Area  ", "", null, new string[1]
				{
					vipNo
				}, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count > 0)
				{
					_credit = dataTable2.Rows[0]["Credit"].ToString();
					memberName.Text = dataTable2.Rows[0]["Name"].ToString();
					if (dataTable2.Rows[0]["Telphone"].ToString() != "" && dataTable2.Rows[0]["Telphone"].ToString() != null)
					{
						memberName.Text = memberName.Text + "(" + dataTable2.Rows[0]["Telphone"].ToString() + ")";
					}
					money.Text = dataTable2.Rows[0]["Total"].ToString() + "/" + dataTable2.Rows[0]["Credit"].ToString();
					idnum.Text = dataTable2.Rows[0]["IdNo"].ToString();
				}
			}
			else
			{
				memberName.Text = "非會員";
				money.Text = "";
				idnum.Text = "";
			}
			if (temp.Rows.Count > 0)
			{
				for (int i = 0; i < temp.Rows.Count; i++)
				{
					string[] strWhereParameterArray2 = new string[1]
					{
						temp.Rows[i].Cells[8].Value.ToString()
					};
					DataTable commodityName = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_GOODSLST", "GDSNO = {0}  ", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
					infolist1.Rows.Add(temp.Rows[i].Cells[0].Value, setCommodityName(commodityName), temp.Rows[i].Cells[2].Value, temp.Rows[i].Cells[3].Value, temp.Rows[i].Cells[4].Value, temp.Rows[i].Cells[5].Value, temp.Rows[i].Cells[6].Value, temp.Rows[i].Cells[7].Value, temp.Rows[i].Cells[8].Value, temp.Rows[i].Cells[9].Value, temp.Rows[i].Cells[10].Value);
				}
				_isOK = true;
			}
			foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
			{
				item.Height = 30;
			}
			totalmoney.Text = fms.gettotalprice();
			discountmoney.Text = fms.gettotalpriceDiscount();
			summoney.Text = fms.gettotalspending();
			items.Text = fms.getsumitems();
			total.Text = fms.gettotalitems();
			cash.Text = fms.gettotalspending();
			change.Text = "0";
			if (_isOK)
			{
				credit.Text = "0";
			}
			else
			{
				credit.Text = _credit;
			}
		}

		private void backto_Click(object sender, EventArgs e)
		{
			fms.Show();
			Hide();
		}

		private void Checkout_Click(object sender, EventArgs e)
		{
			Checkout.Enabled = false;
			try
			{
				if (_isOK)
				{
					if (int.Parse(credit.Text.ToString()) > 0 && vipNo == "")
					{
						AutoClosingMessageBox.Show("賒帳必須選擇會員。若需要使用賒帳功能請返回前一步選擇會員");
						return;
					}
					frmExtendScreen.RemoveAll();
					if (paymoney.Text.ToString() == "")
					{
						switch (MessageBox.Show("是否以" + cash.Text + "全額現金收款？", "提示訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
						{
						case DialogResult.Yes:
							AutoClosingMessageBox.Show("收銀完成，列印收據");
							checkOutDataSave(getHseqNo(), fms.gettotalprice(), fms.gettotalpriceDiscount(), cash.Text, credit.Text, fms.getsumitems(), fms.gettotalitems(), string.IsNullOrEmpty(change.Text) ? "0" : change.Text);
							Program.goodsWithMoneyTemp.Clear();
							switchForm(new frmMainShopSimpleWithMoney());
							new frmSell_SellNo(HseqNo).Show();
							Close();
							break;
						}
					}
					else
					{
						AutoClosingMessageBox.Show("現金" + cash.Text + "、賒帳" + credit.Text + "、找零" + change.Text + "收銀完成，列印收據");
						checkOutDataSave(getHseqNo(), fms.gettotalprice(), fms.gettotalpriceDiscount(), cash.Text, credit.Text, fms.getsumitems(), fms.gettotalitems(), string.IsNullOrEmpty(change.Text) ? "0" : change.Text);
						Program.goodsWithMoneyTemp.Clear();
						switchForm(new frmMainShopSimpleWithMoney());
						new frmSell_SellNo(HseqNo).Show();
						Close();
					}
				}
				else
				{
					AutoClosingMessageBox.Show("尚無可收銀結帳之商品，請先選入商品");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				Checkout.Enabled = true;
			}
		}

		public void checkOutDataSave(string sellNo, string sum, string sumDiscount, string cash, string Credit, string items, string itemstotal, string change)
		{
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string[,] strFieldArray = new string[14, 2]
			{
				{
					"sellNo",
					sellNo
				},
				{
					"sellTime",
					text
				},
				{
					"memberId",
					vipNo
				},
				{
					"sum",
					sum
				},
				{
					"sumDiscount",
					sumDiscount
				},
				{
					"sumRebate",
					"0"
				},
				{
					"cash",
					cash
				},
				{
					"Credit",
					Credit
				},
				{
					"items",
					items
				},
				{
					"itemstotal",
					itemstotal
				},
				{
					"status",
					"0"
				},
				{
					"editDate",
					text
				},
				{
					"changcount",
					"1"
				},
				{
					"returnChange",
					change
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_main_sell", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			strFieldArray = new string[4, 2]
			{
				{
					"sellNo",
					sellNo
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
					(int.Parse(sum) - int.Parse(sumDiscount)).ToString()
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_mainsell_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			for (int i = 0; i < infolist1.Rows.Count; i++)
			{
				string text2 = infolist1.Rows[i].Cells["quantity"].Value.ToString();
				string text3 = infolist1.Rows[i].Cells["barcode"].Value.ToString();
				string text4 = infolist1.Rows[i].Cells["cropId"].Value.ToString();
				string text5 = infolist1.Rows[i].Cells["pestId"].Value.ToString();
				strFieldArray = new string[10, 2]
				{
					{
						"sellNo",
						sellNo
					},
					{
						"barcode",
						text3
					},
					{
						"fixedPrice",
						infolist1.Rows[i].Cells["setprice"].Value.ToString()
					},
					{
						"sellingPrice",
						infolist1.Rows[i].Cells["sellingprice"].Value.ToString()
					},
					{
						"num",
						text2
					},
					{
						"discount",
						infolist1.Rows[i].Cells["discount"].Value.ToString()
					},
					{
						"subtotal",
						infolist1.Rows[i].Cells["subtotal"].Value.ToString()
					},
					{
						"total",
						infolist1.Rows[i].Cells["sum"].Value.ToString()
					},
					{
						"PRNO",
						text4
					},
					{
						"BLNO",
						text5
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_detail_sell", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				string[] strWhereParameterArray = new string[1]
				{
					text3
				};
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "GDSNO, inventory", "hypos_GOODSLST", "GDSNO = {0} ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count <= 0)
				{
					continue;
				}
				if (!"".Equals(text4) && !"".Equals(text5))
				{
					string[] array = new string[4]
					{
						vipNo,
						text3,
						text4,
						text5
					};
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "total", "hypos_user_pair", " barcode ={1} and cropId={2} and pestId={3} ", "", null, array, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_user_pair SET total = total+1 where  barcode ={1} and cropId={2} and pestId={3} ", array, CommandOperationType.ExecuteNonQuery);
					}
					else
					{
						strFieldArray = new string[4, 2]
						{
							{
								"barcode",
								text3
							},
							{
								"total",
								"1"
							},
							{
								"cropId",
								text4
							},
							{
								"pestId",
								text5
							}
						};
						DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_pair", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
					}
				}
				int num = -int.Parse(text2);
				if (!string.IsNullOrEmpty(dataTable.Rows[0]["inventory"].ToString()))
				{
					num = int.Parse(dataTable.Rows[0]["inventory"].ToString()) - int.Parse(text2);
				}
				string[] strParameterArray = new string[2]
				{
					num.ToString(),
					text3
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST SET inventory ={0} where GDSNO ={1} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
			}
			string[] array2 = new string[3]
			{
				vipNo,
				summoney.Text,
				Credit
			};
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "Total, Credit", "hypos_CUST_RTL", "VipNo={0}", "", null, array2, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable2.Rows.Count > 0)
			{
				string value = dataTable2.Rows[0]["Total"].ToString();
				string value2 = dataTable2.Rows[0]["Credit"].ToString();
				if (!string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(value2))
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Total=Total+{1}, Credit=Credit+{2} where VipNo={0} ", array2, CommandOperationType.ExecuteNonQuery);
				}
				else
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Total={1}, Credit={2} where VipNo={0} ", array2, CommandOperationType.ExecuteNonQuery);
				}
			}
			strFieldArray = new string[7, 2]
			{
				{
					"memberId",
					vipNo
				},
				{
					"sellNo",
					sellNo
				},
				{
					"editdate",
					text
				},
				{
					"sellType",
					"0"
				},
				{
					"Cash",
					cash
				},
				{
					"Credit",
					Credit
				},
				{
					"status",
					"0"
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET BuyDate = {1} WHERE VipNo = {0}", new string[2]
			{
				vipNo,
				text
			}, CommandOperationType.ExecuteNonQuery);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (_isOK)
			{
				if (MessageBox.Show("尚有未結帳之項目，確定放棄銷售單、結束收銀？", "結束收銀", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					frmExtendScreen.RemoveAll();
					switchForm(new frmMainShopSimpleWithMoney());
				}
			}
			else
			{
				switchForm(new frmMainShopSimpleWithMoney());
			}
		}

		private void numone_Click(object sender, EventArgs e)
		{
			paymoney.Text += "1";
		}

		private void numtwo_Click(object sender, EventArgs e)
		{
			paymoney.Text += "2";
		}

		private void numthree_Click(object sender, EventArgs e)
		{
			paymoney.Text += "3";
		}

		private void numfour_Click(object sender, EventArgs e)
		{
			paymoney.Text += "4";
		}

		private void numfive_Click(object sender, EventArgs e)
		{
			paymoney.Text += "5";
		}

		private void numsix_Click(object sender, EventArgs e)
		{
			paymoney.Text += "6";
		}

		private void numseven_Click(object sender, EventArgs e)
		{
			paymoney.Text += "7";
		}

		private void numeight_Click(object sender, EventArgs e)
		{
			paymoney.Text += "8";
		}

		private void numnine_Click(object sender, EventArgs e)
		{
			paymoney.Text += "9";
		}

		private void numzero_Click(object sender, EventArgs e)
		{
			paymoney.Text += "0";
		}

		private void backspace_Click(object sender, EventArgs e)
		{
			if (paymoney.Text.Length > 0)
			{
				paymoney.Text = paymoney.Text.Remove(paymoney.Text.Length - 1);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			paymoney.Text = "";
			cash.Text = fms.gettotalspending();
			credit.Text = "0";
			change.Text = "0";
		}

		private void button4_Click(object sender, EventArgs e)
		{
			try
			{
				if (paymoney.Text.ToString().Length > 0)
				{
					if (int.Parse(paymoney.Text) > 0)
					{
						if (int.Parse(summoney.Text.ToString()) > int.Parse(cash.Text.ToString()) && vipNo == "")
						{
							AutoClosingMessageBox.Show("賒帳必須選擇會員。若需要使用賒帳功能請返回前一步選擇會員");
							return;
						}
						cash.Text = paymoney.Text;
						_payCash = paymoney.Text;
						if (_isOK)
						{
							if (int.Parse(cash.Text.ToString()) >= int.Parse(summoney.Text.ToString()))
							{
								credit.Text = "0";
								change.Text = (int.Parse(cash.Text.ToString()) - int.Parse(summoney.Text.ToString())).ToString();
							}
							else
							{
								credit.Text = (int.Parse(summoney.Text.ToString()) - int.Parse(cash.Text.ToString())).ToString();
								change.Text = "0";
							}
							return;
						}
						int num = int.Parse(_payCash.Trim());
						int num2 = int.Parse(_credit);
						if (num >= num2)
						{
							credit.Text = "0";
							change.Text = (num - num2).ToString();
						}
						if (num < num2)
						{
							credit.Text = (num2 - num).ToString();
							change.Text = "0";
						}
					}
					else
					{
						AutoClosingMessageBox.Show("收款金額需大於零");
					}
				}
				else
				{
					AutoClosingMessageBox.Show("請先輸入收款現金數字");
				}
			}
			catch (Exception)
			{
				AutoClosingMessageBox.Show("收款金額錯誤");
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			if (_isOK)
			{
				if (vipNo == "")
				{
					AutoClosingMessageBox.Show("賒帳必須選擇會員。若需要使用賒帳功能請返回前一步選擇會員");
				}
				else if (paymoney.Text.ToString() == "")
				{
					frmExtendScreen.RemoveAll();
					credit.Text = summoney.Text;
					cash.Text = "0";
					AutoClosingMessageBox.Show("銷售單全額賒帳");
					Checkout_Click(this, null);
					AutoClosingMessageBox.Show("收銀完成，列印收據");
				}
				else if (MessageBox.Show("已設有現金收款輸入值，是否改用全額賒帳？", "全額賒帳", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					frmExtendScreen.RemoveAll();
					credit.Text = summoney.Text;
					cash.Text = "0";
					AutoClosingMessageBox.Show("銷售單全額賒帳");
					Checkout_Click(this, null);
					AutoClosingMessageBox.Show("收銀完成，列印收據");
				}
			}
			else
			{
				AutoClosingMessageBox.Show("尚無可收銀結帳之商品，請先選入商品");
			}
		}

		private string getHseqNo()
		{
			return HseqNo;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			if (_isOK)
			{
				AutoClosingMessageBox.Show("目前仍有未結帳之商品，請先收銀結帳後、再進行賒帳還款");
				return;
			}
			int num = int.Parse(_credit);
			if (num == 0)
			{
				AutoClosingMessageBox.Show("此會員目前無賒帳金額");
			}
			else if (!"".Equals(_payCash.Trim()))
			{
				int num2 = int.Parse(_payCash.Trim());
				int num3 = 0;
				string text2 = "0";
				string sql = "UPDATE hypos_CUST_RTL SET Credit=Credit+{1} where VipNo={0} ";
				if (num2 >= num)
				{
					num3 = num;
					text2 = (num2 - num).ToString();
					AutoClosingMessageBox.Show("賒帳還款「" + num2 + "」，還款後賒帳金額為＂0＂，需找零＂" + text2 + "＂");
				}
				if (num2 < num)
				{
					num3 = num2;
					AutoClosingMessageBox.Show("賒帳還款「" + num2 + "」，還款後賒帳金額為＂" + (num - num2) + "＂");
				}
				DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[2]
				{
					vipNo,
					(-num3).ToString()
				}, CommandOperationType.ExecuteNonQuery);
				string[,] strFieldArray = new string[7, 2]
				{
					{
						"memberId",
						vipNo
					},
					{
						"sellNo",
						""
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
						text2
					},
					{
						"Credit",
						num3.ToString()
					},
					{
						"status",
						"0"
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET RepayDate = {1} WHERE VipNo = {0}", new string[2]
				{
					vipNo,
					text
				}, CommandOperationType.ExecuteNonQuery);
				frmExtendScreen.RemoveAll();
				switchForm(new frmMainShopSimpleWithMoney());
			}
			else
			{
				AutoClosingMessageBox.Show("請先輸入賒帳還款數字");
			}
		}

		private void digitOnly_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b'));
		}

		private string setCommodityName(DataTable dt)
		{
			string text = dt.Rows[0]["GDName"].ToString();
			string text2 = dt.Rows[0]["formCode"].ToString();
			string text3 = dt.Rows[0]["CName"].ToString();
			string text4 = dt.Rows[0]["contents"].ToString();
			string text5 = dt.Rows[0]["brandName"].ToString();
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
				text += "．";
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
			button1 = new System.Windows.Forms.Button();
			backto = new System.Windows.Forms.Button();
			Checkout = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			idnum = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			money = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			memberName = new System.Windows.Forms.Label();
			label17 = new System.Windows.Forms.Label();
			change = new System.Windows.Forms.Label();
			label16 = new System.Windows.Forms.Label();
			alertMsg = new System.Windows.Forms.TextBox();
			paymoney = new System.Windows.Forms.TextBox();
			label13 = new System.Windows.Forms.Label();
			credit = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			cash = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			summoney = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			discountmoney = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			totalmoney = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			infolist1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			commodity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			setprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			sellingprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
			barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cropId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			pestId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			total = new System.Windows.Forms.Label();
			items = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel2.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)infolist1).BeginInit();
			SuspendLayout();
			pb_virtualKeyBoard.Visible = false;
			panel2.BackgroundImage = POS_Client.Properties.Resources.inside_button;
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
			panel2.Controls.Add(button1);
			panel2.Controls.Add(backto);
			panel2.Controls.Add(Checkout);
			panel2.Location = new System.Drawing.Point(12, 473);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(969, 219);
			panel2.TabIndex = 38;
			button5.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button5.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button5.ForeColor = System.Drawing.Color.White;
			button5.Location = new System.Drawing.Point(675, 12);
			button5.Name = "button5";
			button5.Size = new System.Drawing.Size(102, 96);
			button5.TabIndex = 53;
			button5.Text = "整筆賒帳";
			button5.UseVisualStyleBackColor = false;
			button5.Click += new System.EventHandler(button5_Click);
			button4.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button4.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button4.ForeColor = System.Drawing.Color.White;
			button4.Location = new System.Drawing.Point(570, 112);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(102, 96);
			button4.TabIndex = 52;
			button4.Text = "現金收款";
			button4.UseVisualStyleBackColor = false;
			button4.Click += new System.EventHandler(button4_Click);
			button3.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button3.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button3.ForeColor = System.Drawing.Color.White;
			button3.Location = new System.Drawing.Point(570, 12);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(102, 96);
			button3.TabIndex = 51;
			button3.Text = "清除輸入";
			button3.UseVisualStyleBackColor = false;
			button3.Click += new System.EventHandler(button3_Click);
			numnine.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numnine.Location = new System.Drawing.Point(517, 12);
			numnine.Name = "numnine";
			numnine.Size = new System.Drawing.Size(47, 47);
			numnine.TabIndex = 50;
			numnine.Text = "9";
			numnine.UseVisualStyleBackColor = true;
			numnine.Click += new System.EventHandler(numnine_Click);
			numeight.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numeight.Location = new System.Drawing.Point(468, 12);
			numeight.Name = "numeight";
			numeight.Size = new System.Drawing.Size(47, 47);
			numeight.TabIndex = 49;
			numeight.Text = "8";
			numeight.UseVisualStyleBackColor = true;
			numeight.Click += new System.EventHandler(numeight_Click);
			numseven.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numseven.Location = new System.Drawing.Point(417, 12);
			numseven.Name = "numseven";
			numseven.Size = new System.Drawing.Size(47, 47);
			numseven.TabIndex = 48;
			numseven.Text = "7";
			numseven.UseVisualStyleBackColor = true;
			numseven.Click += new System.EventHandler(numseven_Click);
			numfive.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfive.Location = new System.Drawing.Point(468, 61);
			numfive.Name = "numfive";
			numfive.Size = new System.Drawing.Size(47, 47);
			numfive.TabIndex = 47;
			numfive.Text = "5";
			numfive.UseVisualStyleBackColor = true;
			numfive.Click += new System.EventHandler(numfive_Click);
			numsix.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numsix.Location = new System.Drawing.Point(519, 61);
			numsix.Name = "numsix";
			numsix.Size = new System.Drawing.Size(47, 47);
			numsix.TabIndex = 46;
			numsix.Text = "6";
			numsix.UseVisualStyleBackColor = true;
			numsix.Click += new System.EventHandler(numsix_Click);
			numfour.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfour.Location = new System.Drawing.Point(417, 61);
			numfour.Name = "numfour";
			numfour.Size = new System.Drawing.Size(47, 47);
			numfour.TabIndex = 45;
			numfour.Text = "4";
			numfour.UseVisualStyleBackColor = true;
			numfour.Click += new System.EventHandler(numfour_Click);
			numthree.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numthree.Location = new System.Drawing.Point(519, 111);
			numthree.Name = "numthree";
			numthree.Size = new System.Drawing.Size(47, 47);
			numthree.TabIndex = 44;
			numthree.Text = "3";
			numthree.UseVisualStyleBackColor = true;
			numthree.Click += new System.EventHandler(numthree_Click);
			numtwo.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numtwo.Location = new System.Drawing.Point(468, 111);
			numtwo.Name = "numtwo";
			numtwo.Size = new System.Drawing.Size(47, 47);
			numtwo.TabIndex = 43;
			numtwo.Text = "2";
			numtwo.UseVisualStyleBackColor = true;
			numtwo.Click += new System.EventHandler(numtwo_Click);
			numzero.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numzero.Location = new System.Drawing.Point(417, 160);
			numzero.Name = "numzero";
			numzero.Size = new System.Drawing.Size(47, 47);
			numzero.TabIndex = 42;
			numzero.Text = "0";
			numzero.UseVisualStyleBackColor = true;
			numzero.Click += new System.EventHandler(numzero_Click);
			backspace.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			backspace.Location = new System.Drawing.Point(468, 160);
			backspace.Name = "backspace";
			backspace.Size = new System.Drawing.Size(98, 47);
			backspace.TabIndex = 41;
			backspace.Text = "backspace";
			backspace.UseVisualStyleBackColor = true;
			backspace.Click += new System.EventHandler(backspace_Click);
			numone.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numone.Location = new System.Drawing.Point(417, 111);
			numone.Name = "numone";
			numone.Size = new System.Drawing.Size(47, 47);
			numone.TabIndex = 40;
			numone.Text = "1";
			numone.UseVisualStyleBackColor = true;
			numone.Click += new System.EventHandler(numone_Click);
			button2.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button2.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button2.ForeColor = System.Drawing.Color.White;
			button2.Location = new System.Drawing.Point(311, 111);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(102, 96);
			button2.TabIndex = 39;
			button2.Text = "結束收銀";
			button2.UseVisualStyleBackColor = false;
			button2.Click += new System.EventHandler(button2_Click);
			button1.BackColor = System.Drawing.Color.FromArgb(55, 169, 183);
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button1.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			button1.ForeColor = System.Drawing.Color.White;
			button1.Location = new System.Drawing.Point(206, 12);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(102, 195);
			button1.TabIndex = 38;
			button1.Text = "賒帳還款";
			button1.UseVisualStyleBackColor = false;
			button1.Click += new System.EventHandler(button1_Click);
			backto.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			backto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			backto.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			backto.ForeColor = System.Drawing.Color.White;
			backto.Location = new System.Drawing.Point(311, 12);
			backto.Name = "backto";
			backto.Size = new System.Drawing.Size(102, 96);
			backto.TabIndex = 37;
			backto.Text = "返回編修";
			backto.UseVisualStyleBackColor = false;
			backto.Click += new System.EventHandler(backto_Click);
			Checkout.BackColor = System.Drawing.Color.FromArgb(250, 87, 0);
			Checkout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			Checkout.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			Checkout.ForeColor = System.Drawing.Color.White;
			Checkout.Location = new System.Drawing.Point(675, 112);
			Checkout.Name = "Checkout";
			Checkout.Size = new System.Drawing.Size(102, 96);
			Checkout.TabIndex = 36;
			Checkout.Text = "收銀付款";
			Checkout.UseVisualStyleBackColor = false;
			Checkout.Click += new System.EventHandler(Checkout_Click);
			panel1.BackColor = System.Drawing.Color.White;
			panel1.Controls.Add(idnum);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(money);
			panel1.Controls.Add(label12);
			panel1.Controls.Add(memberName);
			panel1.Controls.Add(label17);
			panel1.Controls.Add(change);
			panel1.Controls.Add(label16);
			panel1.Controls.Add(alertMsg);
			panel1.Controls.Add(paymoney);
			panel1.Controls.Add(label13);
			panel1.Controls.Add(credit);
			panel1.Controls.Add(label14);
			panel1.Controls.Add(cash);
			panel1.Controls.Add(label11);
			panel1.Controls.Add(label10);
			panel1.Controls.Add(summoney);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(discountmoney);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(totalmoney);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(infolist1);
			panel1.Controls.Add(total);
			panel1.Controls.Add(items);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Location = new System.Drawing.Point(29, 48);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(937, 419);
			panel1.TabIndex = 37;
			idnum.AutoSize = true;
			idnum.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			idnum.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			idnum.Location = new System.Drawing.Point(732, 5);
			idnum.Name = "idnum";
			idnum.Size = new System.Drawing.Size(64, 26);
			idnum.TabIndex = 38;
			idnum.Text = "label4";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label7.Location = new System.Drawing.Point(617, 6);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(101, 27);
			label7.TabIndex = 37;
			label7.Text = "統一編號:";
			money.AutoSize = true;
			money.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			money.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			money.Location = new System.Drawing.Point(451, 5);
			money.Name = "money";
			money.Size = new System.Drawing.Size(64, 26);
			money.TabIndex = 36;
			money.Text = "label4";
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label12.Location = new System.Drawing.Point(289, 6);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(152, 27);
			label12.TabIndex = 35;
			label12.Text = "消費/賒帳金額:";
			memberName.AutoSize = true;
			memberName.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			memberName.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			memberName.Location = new System.Drawing.Point(87, 5);
			memberName.Name = "memberName";
			memberName.Size = new System.Drawing.Size(64, 26);
			memberName.TabIndex = 34;
			memberName.Text = "label4";
			label17.AutoSize = true;
			label17.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label17.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label17.Location = new System.Drawing.Point(8, 6);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(59, 27);
			label17.TabIndex = 33;
			label17.Text = "會員:";
			change.AutoSize = true;
			change.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			change.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			change.Location = new System.Drawing.Point(696, 363);
			change.Name = "change";
			change.Size = new System.Drawing.Size(64, 26);
			change.TabIndex = 32;
			change.Text = "label4";
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label16.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label16.Location = new System.Drawing.Point(636, 362);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(54, 27);
			label16.TabIndex = 31;
			label16.Text = "找零";
			alertMsg.Font = new System.Drawing.Font("微軟正黑體", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			alertMsg.Location = new System.Drawing.Point(282, 361);
			alertMsg.Multiline = true;
			alertMsg.Name = "alertMsg";
			alertMsg.ReadOnly = true;
			alertMsg.Size = new System.Drawing.Size(294, 27);
			alertMsg.TabIndex = 30;
			paymoney.Font = new System.Drawing.Font("Calibri", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			paymoney.ImeMode = System.Windows.Forms.ImeMode.Disable;
			paymoney.Location = new System.Drawing.Point(104, 362);
			paymoney.Name = "paymoney";
			paymoney.Size = new System.Drawing.Size(153, 26);
			paymoney.TabIndex = 29;
			paymoney.KeyPress += new System.Windows.Forms.KeyPressEventHandler(digitOnly_KeyPress);
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label13.Location = new System.Drawing.Point(44, 362);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(54, 27);
			label13.TabIndex = 28;
			label13.Text = "收款";
			credit.AutoSize = true;
			credit.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			credit.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			credit.Location = new System.Drawing.Point(849, 327);
			credit.Name = "credit";
			credit.Size = new System.Drawing.Size(64, 26);
			credit.TabIndex = 27;
			credit.Text = "label4";
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label14.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label14.Location = new System.Drawing.Point(791, 327);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(63, 27);
			label14.TabIndex = 26;
			label14.Text = "/賒帳";
			cash.AutoSize = true;
			cash.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			cash.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			cash.Location = new System.Drawing.Point(733, 327);
			cash.Name = "cash";
			cash.Size = new System.Drawing.Size(64, 26);
			cash.TabIndex = 24;
			cash.Text = "label4";
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label11.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label11.Location = new System.Drawing.Point(686, 329);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(54, 27);
			label11.TabIndex = 23;
			label11.Text = "現金";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label10.Location = new System.Drawing.Point(594, 329);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(96, 27);
			label10.TabIndex = 22;
			label10.Text = "收款方式";
			summoney.AutoSize = true;
			summoney.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			summoney.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			summoney.Location = new System.Drawing.Point(709, 289);
			summoney.Name = "summoney";
			summoney.Size = new System.Drawing.Size(64, 26);
			summoney.TabIndex = 21;
			summoney.Text = "label4";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label8.Location = new System.Drawing.Point(594, 290);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(96, 27);
			label8.TabIndex = 20;
			label8.Text = "消費總額";
			discountmoney.AutoSize = true;
			discountmoney.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			discountmoney.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			discountmoney.Location = new System.Drawing.Point(428, 289);
			discountmoney.Name = "discountmoney";
			discountmoney.Size = new System.Drawing.Size(64, 26);
			discountmoney.TabIndex = 19;
			discountmoney.Text = "label4";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label6.Location = new System.Drawing.Point(316, 290);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(96, 27);
			label6.TabIndex = 18;
			label6.Text = "總價折讓";
			totalmoney.AutoSize = true;
			totalmoney.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			totalmoney.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			totalmoney.Location = new System.Drawing.Point(104, 290);
			totalmoney.Name = "totalmoney";
			totalmoney.Size = new System.Drawing.Size(64, 26);
			totalmoney.TabIndex = 17;
			totalmoney.Text = "label4";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label4.Location = new System.Drawing.Point(8, 289);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(96, 27);
			label4.TabIndex = 16;
			label4.Text = "售價統計";
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
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(3);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 255);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			infolist1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			infolist1.Columns.AddRange(Column1, commodity, setprice, sellingprice, quantity, subtotal, discount, sum, barcode, cropId, pestId);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			infolist1.DefaultCellStyle = dataGridViewCellStyle2;
			infolist1.Enabled = false;
			infolist1.EnableHeadersVisualStyles = false;
			infolist1.GridColor = System.Drawing.SystemColors.ActiveBorder;
			infolist1.Location = new System.Drawing.Point(49, 36);
			infolist1.MultiSelect = false;
			infolist1.Name = "infolist1";
			infolist1.ReadOnly = true;
			infolist1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ScrollBar;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 255);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			infolist1.RowHeadersVisible = false;
			infolist1.RowTemplate.Height = 24;
			infolist1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			infolist1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			infolist1.Size = new System.Drawing.Size(854, 231);
			infolist1.TabIndex = 15;
			Column1.HeaderText = "";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.Width = 20;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			commodity.DefaultCellStyle = dataGridViewCellStyle4;
			commodity.HeaderText = "商品名稱";
			commodity.Name = "commodity";
			commodity.ReadOnly = true;
			commodity.Width = 347;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
			setprice.DefaultCellStyle = dataGridViewCellStyle5;
			setprice.HeaderText = "定價";
			setprice.Name = "setprice";
			setprice.ReadOnly = true;
			setprice.Width = 75;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			sellingprice.DefaultCellStyle = dataGridViewCellStyle6;
			sellingprice.HeaderText = "售價";
			sellingprice.Name = "sellingprice";
			sellingprice.ReadOnly = true;
			sellingprice.Width = 75;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			quantity.DefaultCellStyle = dataGridViewCellStyle7;
			quantity.HeaderText = "數量";
			quantity.Name = "quantity";
			quantity.ReadOnly = true;
			quantity.Width = 75;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			subtotal.DefaultCellStyle = dataGridViewCellStyle8;
			subtotal.HeaderText = "小計";
			subtotal.Name = "subtotal";
			subtotal.ReadOnly = true;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			discount.DefaultCellStyle = dataGridViewCellStyle9;
			discount.HeaderText = "折讓";
			discount.Name = "discount";
			discount.ReadOnly = true;
			discount.Width = 75;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			sum.DefaultCellStyle = dataGridViewCellStyle10;
			sum.HeaderText = "合計";
			sum.Name = "sum";
			sum.ReadOnly = true;
			barcode.HeaderText = "條碼";
			barcode.Name = "barcode";
			barcode.ReadOnly = true;
			barcode.Visible = false;
			cropId.HeaderText = "作物id";
			cropId.Name = "cropId";
			cropId.ReadOnly = true;
			cropId.Visible = false;
			pestId.HeaderText = "蟲害id";
			pestId.Name = "pestId";
			pestId.ReadOnly = true;
			pestId.Visible = false;
			total.AutoSize = true;
			total.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			total.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			total.Location = new System.Drawing.Point(428, 329);
			total.Name = "total";
			total.Size = new System.Drawing.Size(64, 26);
			total.TabIndex = 14;
			total.Text = "label4";
			items.AutoSize = true;
			items.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			items.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			items.Location = new System.Drawing.Point(104, 328);
			items.Name = "items";
			items.Size = new System.Drawing.Size(64, 26);
			items.TabIndex = 13;
			items.Text = "label4";
			label3.BackColor = System.Drawing.Color.FromArgb(196, 214, 96);
			label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			label3.ForeColor = System.Drawing.Color.FromArgb(196, 214, 96);
			label3.Location = new System.Drawing.Point(3, 401);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(900, 2);
			label3.TabIndex = 12;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label2.Location = new System.Drawing.Point(358, 329);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(54, 27);
			label2.TabIndex = 11;
			label2.Text = "數量";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.FromArgb(139, 128, 100);
			label1.Location = new System.Drawing.Point(44, 328);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(54, 27);
			label1.TabIndex = 10;
			label1.Text = "品項";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(252, 252, 237);
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(1004, 704);
			base.Controls.Add(panel2);
			base.Controls.Add(panel1);
			base.Name = "frmMainShopCheckout";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "frmMainShop";
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(panel2, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel2.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)infolist1).EndInit();
			ResumeLayout(false);
		}
	}
}
