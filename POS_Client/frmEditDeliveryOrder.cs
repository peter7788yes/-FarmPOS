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
	public class frmEditDeliveryOrder : MasterThinForm
	{
		private string _DeliveryNo = "";

		private string newTotal = "0";

		private bool action;

		private bool isFocus;

		private bool islock;

		private IContainer components;

		private Panel panel2;

		private Label label1;

		private Label l_vendorInfo;

		private Label label13;

		private TextBox tb_temp;

		private TextBox alertMsg;

		public Button btn_Save;

		public Button btn_DeliveryCancel;

		public Button btn_enterPrice;

		public Button btn_enterCount;

		private Button numnine;

		private Button numeight;

		private Button numseven;

		private Button numfive;

		private Button numsix;

		private Button numfour;

		private Button numthree;

		private Button numtwo;

		private Button numzero;

		private Button btn_backspace;

		private Button numone;

		private Label l_DeliveryCreateDate;

		private Label label7;

		private Label l_DeliveryNo;

		private Label label5;

		public DataGridView infolist1;

		private Label label15;

		private Label l_adujstPrice;

		private Label label12;

		private Label l_OriSum;

		private Label label10;

		private Label l_DeliveryDate;

		private Label label8;

		private Label l_DeliveryCustomNo;

		private Label member;

		private Button btn_pre;

		private Button btn_next;

		public Button btn_returnBack;

		public Button btn_clear;

		private Label label3;

		private Label l_newTotal;

		private Label l_status;

		private Label l_updateLog;

		public Button btn_PrintDoc;

		public Button btn_changeDiscount2;

		private Label label2;

		private Label l_diff;

		private Label label6;

		private TextBox tb_changeDiscount;

		private Button btn_changeDiscount;

		private Label label9;

		private Label l_Total;

		private DataGridViewTextBoxColumn Column1;

		private frmMainShopSimple.CustomColumn commodity;

		private DataGridViewTextBoxColumn beforePrice;

		private DataGridViewTextBoxColumn beforeQuantity;

		private DataGridViewTextBoxColumn subTotal;

		private DataGridViewTextBoxColumn afterPrice;

		private DataGridViewTextBoxColumn afterQuantity;

		private DataGridViewTextBoxColumn adjustInfo;

		private DataGridViewButtonColumn plus;

		private DataGridViewButtonColumn minus;

		private DataGridViewButtonColumn zero;

		private DataGridViewTextBoxColumn hidden_beforeAdjustInventory;

		private DataGridViewTextBoxColumn hidden_detailID;

		public frmEditDeliveryOrder(string DeliveryNo)
			: base("編修出貨單")
		{
			_DeliveryNo = DeliveryNo;
			InitializeComponent();
			infolist1.Columns["plus"].Visible = false;
			pb_virtualKeyBoard.Visible = false;
			string strSelectField = "s.SupplierName, s.SupplierIdNo, s.vendorId, s.vendorName, m.*";
			string strTableName = "hypos_DeliveryGoods_Master m, hypos_Supplier s";
			string strWhereClause = "m.vendorNo = s.SupplierNo and m.DeliveryNo = {0}";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, strSelectField, strTableName, strWhereClause, "", null, new string[1]
			{
				DeliveryNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				string text = dataTable.Rows[0]["status"].ToString();
				switch (text)
				{
				case "0":
					text = "正常";
					break;
				case "2":
					text = "正常(變更)";
					l_updateLog.Visible = true;
					break;
				case "1":
					text = "取消";
					islock = true;
					l_updateLog.Visible = true;
					break;
				}
				l_status.Text = text;
				l_DeliveryNo.Text = DeliveryNo;
				l_DeliveryCreateDate.Text = dataTable.Rows[0]["CreateDate"].ToString();
				l_DeliveryCustomNo.Text = dataTable.Rows[0]["DeliveryCustomNo"].ToString();
				l_DeliveryDate.Text = dataTable.Rows[0]["DeliveryDate"].ToString();
				l_Total.Text = dataTable.Rows[0]["CurSum"].ToString();
				l_OriSum.Text = (int.Parse(dataTable.Rows[0]["CurSum"].ToString()) - int.Parse(dataTable.Rows[0]["sumDiscount"].ToString())).ToString();
				l_adujstPrice.Text = (int.Parse(dataTable.Rows[0]["CurSum"].ToString()) - int.Parse(dataTable.Rows[0]["sumDiscount"].ToString())).ToString();
				l_newTotal.Text = dataTable.Rows[0]["CurSum"].ToString();
				newTotal = dataTable.Rows[0]["CurSum"].ToString();
				tb_changeDiscount.Text = (string.IsNullOrEmpty(dataTable.Rows[0]["sumDiscount"].ToString()) ? "0" : dataTable.Rows[0]["sumDiscount"].ToString());
				l_diff.Text = "0";
				l_vendorInfo.Text = string.Format("{0}-{1}({2})({3})", dataTable.Rows[0]["vendorId"].ToString(), dataTable.Rows[0]["vendorName"].ToString(), dataTable.Rows[0]["SupplierName"].ToString(), dataTable.Rows[0]["SupplierIdNo"].ToString());
			}
			else
			{
				MessageBox.Show("出貨單資訊異常");
				backToPreviousForm();
			}
			strSelectField = "d.*,g.*";
			strTableName = "hypos_DeliveryGoods_Master m, hypos_DeliveryGoods_Detail d,hypos_GOODSLST g";
			strWhereClause = "g.GDSNO = d.barcode and m.DeliveryNo = d.DeliveryNo and m.DeliveryNo = {0}";
			dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, strSelectField, strTableName, strWhereClause, "", null, new string[1]
			{
				DeliveryNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					int num = int.Parse(string.IsNullOrEmpty(row["sellingPrice"].ToString()) ? "0" : row["sellingPrice"].ToString());
					int num2 = int.Parse(string.IsNullOrEmpty(row["num"].ToString()) ? "0" : row["num"].ToString());
					string text2 = string.IsNullOrEmpty(row["inventory"].ToString()) ? "0" : row["inventory"].ToString();
					CommodityInfo commodityInfo = new CommodityInfo();
					commodityInfo = new CommodityInfo();
					commodityInfo.setMemberIdNo("");
					commodityInfo.setHiddenGDSNO(row["GDSNO"].ToString());
					commodityInfo.setMemberVipNo("店內碼:" + row["GDSNO"].ToString());
					commodityInfo.setCommodityName(setCommodityName(row) + " " + row["spec"].ToString() + " " + row["capacity"].ToString());
					if (row["BatchNo"].ToString().Equals("") && row["MFGDate"].ToString().Equals(""))
					{
						commodityInfo.setCommodityClass("");
					}
					else
					{
						commodityInfo.setCommodityClass("批號:" + row["BatchNo"].ToString() + " 製造日期:" + row["MFGDate"].ToString());
					}
					commodityInfo.setHiddenBatchNo(row["BatchNo"].ToString());
					commodityInfo.setHiddenMFGDate(row["MFGDate"].ToString());
					commodityInfo.setHiddenPOSBatchNo(row["POSBatchNo"].ToString());
					commodityInfo.setlabe1("");
					commodityInfo.BackColor = Color.FromArgb(255, 208, 81);
					infolist1.Rows.Add(0, commodityInfo, num, num2, num * num2, num, num2, "0/0", "+", "-", "X", text2, row["DeliveryDeatialId"].ToString());
					infolist1.CurrentCell = infolist1.Rows[infolist1.RowCount - 1].Cells[0];
					infolist_SelectionChanged(null, null);
				}
				foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
				{
					item.Height = 100;
				}
			}
			else
			{
				MessageBox.Show("商品已不存在");
				backToPreviousForm();
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

		private void num_Down(object sender, MouseEventArgs e)
		{
			tb_temp.Text += (sender as Button).Text;
			tb_temp.Focus();
			tb_temp.SelectionStart = tb_temp.Text.Length;
		}

		private void infolist1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			infolist1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
		}

		private void infolist_SelectionChanged(object sender, EventArgs e)
		{
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

		private void btn_clear_Click(object sender, EventArgs e)
		{
			if (!islock)
			{
				tb_temp.Text = "";
				tb_temp.Focus();
				tb_temp.SelectionStart = tb_temp.Text.Length;
				if (infolist1.CurrentCell != null)
				{
					infolist1.ClearSelection();
					(infolist1.CurrentRow.Cells[1].Value as CommodityInfo).BackColor = Color.White;
					infolist1.CurrentRow.Selected = false;
					infolist1.Refresh();
				}
			}
		}

		private void btn_returnBack_Click(object sender, EventArgs e)
		{
			switchForm(base.Owner);
		}

		private void adjustPurchase(object price, object count)
		{
			int num = int.Parse(infolist1.CurrentRow.Cells["afterQuantity"].Value.ToString());
			int num2 = int.Parse(infolist1.CurrentRow.Cells["afterPrice"].Value.ToString());
			int num3 = int.Parse(infolist1.CurrentRow.Cells["beforeQuantity"].Value.ToString());
			int.Parse(infolist1.CurrentRow.Cells["beforePrice"].Value.ToString());
			int num4 = int.Parse(infolist1.CurrentRow.Cells["subTotal"].Value.ToString());
			int num5 = num * num2;
			if (count != null)
			{
				num = int.Parse(count.ToString());
			}
			if (price != null)
			{
				num2 = int.Parse(price.ToString());
			}
			int num6 = num2 * num;
			l_adujstPrice.Text = (int.Parse(newTotal) - int.Parse(tb_changeDiscount.Text) + num6 - num5).ToString();
			l_newTotal.Text = (int.Parse(l_newTotal.Text) + num6 - num5).ToString();
			newTotal = l_newTotal.Text;
			string arg = (num > num3) ? ("+" + (num - num3)) : (num - num3).ToString();
			string arg2 = (num6 > num4) ? ("+" + (num6 - num4)) : (num6 - num4).ToString();
			infolist1.CurrentRow.Cells["adjustInfo"].Value = string.Format("{0}/{1}", arg, arg2);
			infolist1.CurrentRow.Cells["afterPrice"].Value = num2;
			infolist1.CurrentRow.Cells["afterQuantity"].Value = num;
			l_diff.Text = (int.Parse(l_OriSum.Text) - int.Parse(l_adujstPrice.Text)).ToString();
		}

		private void btn_enterCount_Click(object sender, EventArgs e)
		{
			if (islock)
			{
				return;
			}
			if (!infolist1.CurrentRow.Selected)
			{
				AutoClosingMessageBox.Show("請先選擇商品");
			}
			else
			{
				if (tb_temp.Text.Trim().Equals(""))
				{
					AutoClosingMessageBox.Show("請先輸入數字");
					return;
				}
				string hiddenPOSBatchNo = (infolist1.CurrentRow.Cells[1].Value as CommodityInfo).getHiddenPOSBatchNo();
				string text = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT IsDeliveryOnly FROM hypos_DeliveryGoods_Detail where POSBatchNo = '" + hiddenPOSBatchNo + "' limit 1", null, CommandOperationType.ExecuteScalar).ToString();
				int result = 0;
				int.TryParse(tb_temp.Text, out result);
				if (result > int.Parse(infolist1.CurrentRow.Cells[3].Value.ToString()))
				{
					result = int.Parse(infolist1.CurrentRow.Cells[3].Value.ToString());
				}
				if (text.Equals("Y"))
				{
					int num = result - int.Parse(infolist1.CurrentRow.Cells[3].Value.ToString());
					string hiddenGDSNO = (infolist1.CurrentRow.Cells[1].Value as CommodityInfo).getHiddenGDSNO();
					string text2 = "0";
					string sql = "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = '" + hiddenPOSBatchNo + "' and barcode = '" + hiddenGDSNO + "' order by id desc limit 1";
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable.Rows.Count > 0)
					{
						text2 = (string.IsNullOrEmpty(dataTable.Rows[0]["backlogQuantity"].ToString()) ? "0" : dataTable.Rows[0]["backlogQuantity"].ToString());
						if (num > int.Parse(text2))
						{
							tb_temp.Text = "";
							AutoClosingMessageBox.Show("出貨數量不可高於剩餘數量\n此商品剩餘庫存為 " + text2 + " ");
							return;
						}
					}
				}
				adjustPurchase(null, result);
				action = true;
				alertMsg.Text = "變更出貨數量";
				tb_temp.Text = "";
			}
			tb_temp.Focus();
			tb_temp.SelectionStart = tb_temp.Text.Length;
		}

		private void btn_enterPrice_Click(object sender, EventArgs e)
		{
			if (islock)
			{
				return;
			}
			if (!infolist1.CurrentRow.Selected)
			{
				AutoClosingMessageBox.Show("請先選擇商品");
			}
			else
			{
				if (tb_temp.Text.Trim().Equals(""))
				{
					AutoClosingMessageBox.Show("請先輸入數字");
					return;
				}
				int result = 0;
				int.TryParse(tb_temp.Text, out result);
				adjustPurchase(result, null);
				action = true;
				alertMsg.Text = "變更出貨價格";
				tb_temp.Text = "";
			}
			tb_temp.Focus();
			tb_temp.SelectionStart = tb_temp.Text.Length;
		}

		private void infolist1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (islock || e.RowIndex < 0)
			{
				return;
			}
			int num = int.Parse(infolist1["afterQuantity", e.RowIndex].Value.ToString());
			if (e.ColumnIndex == 8)
			{
				string hiddenPOSBatchNo = (infolist1.CurrentRow.Cells[1].Value as CommodityInfo).getHiddenPOSBatchNo();
				if (DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT IsDeliveryOnly FROM hypos_DeliveryGoods_Detail where POSBatchNo = '" + hiddenPOSBatchNo + "' limit 1", null, CommandOperationType.ExecuteScalar).ToString().Equals("Y"))
				{
					int num2 = int.Parse(infolist1.CurrentRow.Cells[6].Value.ToString()) + 1 - int.Parse(infolist1.CurrentRow.Cells[3].Value.ToString());
					string hiddenGDSNO = (infolist1.CurrentRow.Cells[1].Value as CommodityInfo).getHiddenGDSNO();
					string text = "0";
					string sql = "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = '" + hiddenPOSBatchNo + "' and barcode = '" + hiddenGDSNO + "' order by id desc limit 1";
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable.Rows.Count > 0)
					{
						text = (string.IsNullOrEmpty(dataTable.Rows[0]["backlogQuantity"].ToString()) ? "0" : dataTable.Rows[0]["backlogQuantity"].ToString());
						if (num2 > int.Parse(text))
						{
							tb_temp.Text = "";
							AutoClosingMessageBox.Show("出貨數量不可高於剩餘數量\n此商品剩餘庫存為 " + text + " ");
							return;
						}
					}
				}
				adjustPurchase(null, ++num);
			}
			else if (e.ColumnIndex == 9)
			{
				if (num != 0)
				{
					adjustPurchase(null, --num);
				}
			}
			else if (e.ColumnIndex == 10)
			{
				adjustPurchase(null, 0);
			}
		}

		private void btn_pre_Click(object sender, EventArgs e)
		{
			if (!islock)
			{
				if (!infolist1.CurrentRow.Selected)
				{
					AutoClosingMessageBox.Show("請選擇商品");
				}
				else if (infolist1.CurrentRow.Index > 0)
				{
					int index = infolist1.CurrentRow.Index;
					infolist1.CurrentCell = infolist1.Rows[index - 1].Cells[0];
					infolist_SelectionChanged(sender, e);
				}
				else
				{
					AutoClosingMessageBox.Show("已經是第一筆商品");
				}
			}
		}

		private void btn_next_Click(object sender, EventArgs e)
		{
			if (islock)
			{
				return;
			}
			if (!infolist1.CurrentRow.Selected)
			{
				AutoClosingMessageBox.Show("請選擇商品");
				return;
			}
			try
			{
				if (infolist1.CurrentRow.Index < infolist1.Rows.Count - 1)
				{
					int index = infolist1.CurrentRow.Index;
					infolist1.CurrentCell = infolist1.Rows[index + 1].Cells[0];
					infolist_SelectionChanged(sender, e);
				}
				else
				{
					AutoClosingMessageBox.Show("已經是最後一筆商品");
				}
			}
			catch (Exception)
			{
			}
		}

		private void btn_backspace_Click(object sender, EventArgs e)
		{
			if (!islock && tb_temp.Text.Length > 0)
			{
				tb_temp.Text = tb_temp.Text.Remove(tb_temp.Text.Length - 1);
				tb_temp.Focus();
				tb_temp.SelectionStart = tb_temp.Text.Length;
			}
		}

		private void frmEditInventory_KeyDown(object sender, KeyEventArgs e)
		{
			if (action)
			{
				alertMsg.Text = "";
				action = false;
			}
		}

		private void frmEditInventory_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!isFocus)
			{
				tb_temp.Text += e.KeyChar;
				tb_temp.Focus();
				tb_temp.SelectionStart = tb_temp.Text.Length;
			}
		}

		private void tb_temp_Leave(object sender, EventArgs e)
		{
			isFocus = false;
		}

		private void tb_temp_Enter(object sender, EventArgs e)
		{
			isFocus = true;
		}

		private void btn_Save_Click(object sender, EventArgs e)
		{
			if (islock)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
			{
				if (int.Parse(item.Cells[6].Value.ToString()) > 0)
				{
					num++;
					num2 += int.Parse(item.Cells[6].Value.ToString());
				}
			}
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string text2 = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT changcount FROM hypos_DeliveryGoods_Master where DeliveryNo = {0}", new string[1]
			{
				_DeliveryNo
			}, CommandOperationType.ExecuteScalar).ToString();
			string str = "2";
			if (num2 <= 0)
			{
				str = "1";
			}
			else
			{
				text2 = (int.Parse(text2) + 1).ToString();
			}
			string sql = "update hypos_DeliveryGoods_Master set CurSum = {1}, status = " + str + ", editDate = {2}, sumDiscount = {3}, items = {4}, itemstotal = {5}, changcount = {6} where DeliveryNo = {0}";
			DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[7]
			{
				_DeliveryNo,
				l_newTotal.Text,
				text,
				tb_changeDiscount.Text,
				num.ToString(),
				num2.ToString(),
				text2
			}, CommandOperationType.ExecuteNonQuery);
			string text3 = "";
			string text4 = "";
			string text5 = "";
			if (num2 <= 0)
			{
				text3 = "1";
				text4 = "0";
			}
			else
			{
				text5 = "1";
				text4 = "1";
			}
			string[,] strFieldArray = new string[7, 2]
			{
				{
					"DeliveryNo",
					_DeliveryNo
				},
				{
					"changeDate",
					text
				},
				{
					"isprint",
					text4
				},
				{
					"iscancel",
					text3
				},
				{
					"ischange",
					text5
				},
				{
					"sum",
					l_newTotal.Text
				},
				{
					"sumDiscount",
					tb_changeDiscount.Text
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_DeliveryGoods_Master_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			string text6 = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT DeliveryLogId FROM hypos_DeliveryGoods_Master_log order by DeliveryLogId desc", null, CommandOperationType.ExecuteScalar).ToString();
			foreach (DataGridViewRow item2 in (IEnumerable)infolist1.Rows)
			{
				string hiddenGDSNO = (item2.Cells["commodity"].Value as CommodityInfo).getHiddenGDSNO();
				int num3 = int.Parse(item2.Cells[5].Value.ToString()) * int.Parse(item2.Cells[6].Value.ToString());
				int num4 = int.Parse(item2.Cells[6].Value.ToString()) - int.Parse(item2.Cells[3].Value.ToString());
				int num5 = int.Parse(item2.Cells[5].Value.ToString()) - int.Parse(item2.Cells[2].Value.ToString());
				string text7 = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT inventory FROM hypos_GOODSLST where GDSNO = {0}", new string[1]
				{
					hiddenGDSNO
				}, CommandOperationType.ExecuteScalar).ToString();
				text7 = (string.IsNullOrEmpty(text7) ? "0" : text7);
				int num6 = int.Parse(text7) + int.Parse(item2.Cells[3].Value.ToString()) - int.Parse(item2.Cells[6].Value.ToString());
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST set inventory = {1} WHERE GDSNO = {0}", new string[2]
				{
					hiddenGDSNO,
					num6.ToString()
				}, CommandOperationType.ExecuteNonQuery);
				string[,] strFieldArray2 = new string[7, 2]
				{
					{
						"DeliveryNo",
						_DeliveryNo
					},
					{
						"barcode",
						hiddenGDSNO
					},
					{
						"sellingPrice",
						item2.Cells["afterPrice"].Value.ToString()
					},
					{
						"subtotal",
						num3.ToString()
					},
					{
						"num",
						item2.Cells["afterQuantity"].Value.ToString()
					},
					{
						"editDate",
						text
					},
					{
						"GoodsTotalCountLog",
						num6.ToString()
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_DeliveryGoods_Detail", "DeliveryNo = {0} and DeliveryDeatialId = {1}", "", strFieldArray2, new string[2]
				{
					_DeliveryNo,
					item2.Cells["hidden_detailID"].Value.ToString()
				}, CommandOperationType.ExecuteNonQuery);
				string[,] strFieldArray3 = new string[11, 2]
				{
					{
						"DeliveryLogId",
						text6
					},
					{
						"DeliveryNo",
						_DeliveryNo
					},
					{
						"barcode",
						hiddenGDSNO
					},
					{
						"sellingPrice",
						item2.Cells["afterPrice"].Value.ToString()
					},
					{
						"diffSellingPrice",
						num5.ToString()
					},
					{
						"num",
						item2.Cells["afterQuantity"].Value.ToString()
					},
					{
						"diffNum",
						num4.ToString()
					},
					{
						"subtotal",
						num3.ToString()
					},
					{
						"total",
						""
					},
					{
						"editDate",
						text
					},
					{
						"DeliveryDeatialId",
						item2.Cells["hidden_detailID"].Value.ToString()
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_DeliveryGoods_Detail_Log", "", "", strFieldArray3, null, CommandOperationType.ExecuteNonQuery);
				string text8 = item2.Cells[6].Value.ToString();
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_PurchaseGoodsBatchNo_log set num = {0} WHERE POSBatchNo = {1}", new string[2]
				{
					text8,
					(item2.Cells["commodity"].Value as CommodityInfo).getHiddenPOSBatchNo()
				}, CommandOperationType.ExecuteNonQuery);
				string s = "0";
				string sql2 = "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = '" + (item2.Cells["commodity"].Value as CommodityInfo).getHiddenPOSBatchNo() + "' and barcode = '" + hiddenGDSNO + "' order by id desc limit 1";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					s = dataTable.Rows[0][0].ToString();
				}
				string[,] strFieldArray4 = new string[5, 2]
				{
					{
						"POSBatchNo",
						(item2.Cells["commodity"].Value as CommodityInfo).getHiddenPOSBatchNo()
					},
					{
						"barcode",
						hiddenGDSNO
					},
					{
						"num",
						(int.Parse(item2.Cells[3].Value.ToString()) - int.Parse(item2.Cells[6].Value.ToString())).ToString()
					},
					{
						"backlogQuantity",
						(int.Parse(item2.Cells[3].Value.ToString()) - int.Parse(item2.Cells[6].Value.ToString()) + int.Parse(s)).ToString()
					},
					{
						"createDate",
						text
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_BatchNo_log", "", "", strFieldArray4, null, CommandOperationType.ExecuteNonQuery);
			}
			if ("1".Equals(text3))
			{
				AutoClosingMessageBox.Show("出貨單已取消");
			}
			else
			{
				AutoClosingMessageBox.Show("所有變更已儲存");
			}
			if (base.Owner is frmDeliveryMangement)
			{
				(base.Owner as frmDeliveryMangement).UpdateDeliveryMangemnetPage();
			}
			else if (base.Owner is frmEditSupplier)
			{
				(base.Owner as frmEditSupplier).showDeliveryLog();
			}
			switchForm(base.Owner);
		}

		private void btn_purchaseReturn_Click(object sender, EventArgs e)
		{
			if (islock || MessageBox.Show("整筆出貨單取消，確認後將無法復原。確定取消？", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return;
			}
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string str = "1";
			string sql = "update hypos_DeliveryGoods_Master set CurSum = {1}, status = " + str + ", editDate = {2}, sumDiscount = {3}, items = {4}, itemstotal = {5} where DeliveryNo = {0}";
			DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[6]
			{
				_DeliveryNo,
				"0",
				text,
				"0",
				"0",
				"0"
			}, CommandOperationType.ExecuteNonQuery);
			string text2 = "1";
			string[,] strFieldArray = new string[6, 2]
			{
				{
					"DeliveryNo",
					_DeliveryNo
				},
				{
					"changeDate",
					text
				},
				{
					"isprint",
					"0"
				},
				{
					"iscancel",
					text2
				},
				{
					"sum",
					"0"
				},
				{
					"sumDiscount",
					"0"
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_DeliveryGoods_Master_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			string text3 = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT DeliveryLogId FROM hypos_DeliveryGoods_Master_log order by DeliveryLogId desc", null, CommandOperationType.ExecuteScalar).ToString();
			foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
			{
				string hiddenGDSNO = (item.Cells["commodity"].Value as CommodityInfo).getHiddenGDSNO();
				int num = 0;
				int num2 = int.Parse(item.Cells[3].Value.ToString());
				int num3 = int.Parse(item.Cells[5].Value.ToString()) - int.Parse(item.Cells[2].Value.ToString());
				string text4 = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT inventory FROM hypos_GOODSLST where GDSNO = {0}", new string[1]
				{
					hiddenGDSNO
				}, CommandOperationType.ExecuteScalar).ToString();
				text4 = (string.IsNullOrEmpty(text4) ? "0" : text4);
				int num4 = int.Parse(text4) + num2;
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST set inventory = {1} WHERE GDSNO = {0}", new string[2]
				{
					hiddenGDSNO,
					num4.ToString()
				}, CommandOperationType.ExecuteNonQuery);
				string[,] strFieldArray2 = new string[7, 2]
				{
					{
						"DeliveryNo",
						_DeliveryNo
					},
					{
						"barcode",
						hiddenGDSNO
					},
					{
						"sellingPrice",
						item.Cells["afterPrice"].Value.ToString()
					},
					{
						"subtotal",
						num.ToString()
					},
					{
						"num",
						"0"
					},
					{
						"editDate",
						text
					},
					{
						"GoodsTotalCountLog",
						num4.ToString()
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_DeliveryGoods_Detail", "DeliveryNo = {0} and DeliveryDeatialId = {1}", "", strFieldArray2, new string[2]
				{
					_DeliveryNo,
					item.Cells["hidden_detailID"].Value.ToString()
				}, CommandOperationType.ExecuteNonQuery);
				string[,] strFieldArray3 = new string[10, 2]
				{
					{
						"DeliveryLogId",
						text3
					},
					{
						"DeliveryNo",
						_DeliveryNo
					},
					{
						"barcode",
						hiddenGDSNO
					},
					{
						"sellingPrice",
						item.Cells["afterPrice"].Value.ToString()
					},
					{
						"diffSellingPrice",
						num3.ToString()
					},
					{
						"num",
						"0"
					},
					{
						"diffNum",
						"-" + num2
					},
					{
						"subtotal",
						num.ToString()
					},
					{
						"editDate",
						text
					},
					{
						"DeliveryDeatialId",
						item.Cells["hidden_detailID"].Value.ToString()
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_DeliveryGoods_Detail_Log", "", "", strFieldArray3, null, CommandOperationType.ExecuteNonQuery);
				string text5 = "0";
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_PurchaseGoodsBatchNo_log set num = {0} WHERE POSBatchNo = {1}", new string[2]
				{
					text5,
					(item.Cells["commodity"].Value as CommodityInfo).getHiddenPOSBatchNo()
				}, CommandOperationType.ExecuteNonQuery);
				string s = "0";
				string sql2 = "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = '" + (item.Cells["commodity"].Value as CommodityInfo).getHiddenPOSBatchNo() + "' and barcode = '" + hiddenGDSNO + "' order by id desc limit 1";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					s = dataTable.Rows[0][0].ToString();
				}
				string[,] strFieldArray4 = new string[5, 2]
				{
					{
						"POSBatchNo",
						(item.Cells["commodity"].Value as CommodityInfo).getHiddenPOSBatchNo()
					},
					{
						"barcode",
						hiddenGDSNO
					},
					{
						"num",
						num2.ToString()
					},
					{
						"backlogQuantity",
						(int.Parse(item.Cells[3].Value.ToString()) + int.Parse(s)).ToString()
					},
					{
						"createDate",
						text
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_BatchNo_log", "", "", strFieldArray4, null, CommandOperationType.ExecuteNonQuery);
			}
			if (base.Owner is frmDeliveryMangement)
			{
				(base.Owner as frmDeliveryMangement).UpdateDeliveryMangemnetPage();
			}
			else if (base.Owner is frmEditSupplier)
			{
				(base.Owner as frmEditSupplier).showDeliveryLog();
			}
			switchForm(base.Owner);
		}

		public static int ConvertToInt(string value)
		{
			try
			{
				return (int)float.Parse(value);
			}
			catch (Exception)
			{
				return 0;
			}
		}

		private void l_updateLog_Click(object sender, EventArgs e)
		{
			new dialogDeliveryUpdateLog(_DeliveryNo).ShowDialog();
		}

		private void btn_changeDiscount_Click(object sender, EventArgs e)
		{
			if (tb_temp.Text.Trim().Equals(""))
			{
				AutoClosingMessageBox.Show("請先輸入數字");
				return;
			}
			tb_changeDiscount.Text = ConvertToInt(tb_temp.Text).ToString();
			adjustPurchase(null, null);
			tb_temp.Text = "";
		}

		private void btn_PrintDoc_Click(object sender, EventArgs e)
		{
			new frmPrint_DeliveryDoc(_DeliveryNo).Show();
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
			btn_changeDiscount2 = new System.Windows.Forms.Button();
			btn_PrintDoc = new System.Windows.Forms.Button();
			btn_pre = new System.Windows.Forms.Button();
			btn_next = new System.Windows.Forms.Button();
			btn_DeliveryCancel = new System.Windows.Forms.Button();
			btn_returnBack = new System.Windows.Forms.Button();
			btn_enterPrice = new System.Windows.Forms.Button();
			btn_clear = new System.Windows.Forms.Button();
			btn_enterCount = new System.Windows.Forms.Button();
			numnine = new System.Windows.Forms.Button();
			numeight = new System.Windows.Forms.Button();
			numseven = new System.Windows.Forms.Button();
			numfive = new System.Windows.Forms.Button();
			numsix = new System.Windows.Forms.Button();
			numfour = new System.Windows.Forms.Button();
			numthree = new System.Windows.Forms.Button();
			numtwo = new System.Windows.Forms.Button();
			numzero = new System.Windows.Forms.Button();
			btn_backspace = new System.Windows.Forms.Button();
			numone = new System.Windows.Forms.Button();
			btn_Save = new System.Windows.Forms.Button();
			l_vendorInfo = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			l_adujstPrice = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			l_OriSum = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			l_DeliveryDate = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			l_DeliveryCustomNo = new System.Windows.Forms.Label();
			member = new System.Windows.Forms.Label();
			infolist1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			commodity = new POS_Client.frmMainShopSimple.CustomColumn();
			beforePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			beforeQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			subTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			afterPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
			afterQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			adjustInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			plus = new System.Windows.Forms.DataGridViewButtonColumn();
			minus = new System.Windows.Forms.DataGridViewButtonColumn();
			zero = new System.Windows.Forms.DataGridViewButtonColumn();
			hidden_beforeAdjustInventory = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_detailID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			l_DeliveryCreateDate = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			l_DeliveryNo = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			alertMsg = new System.Windows.Forms.TextBox();
			tb_temp = new System.Windows.Forms.TextBox();
			label13 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			l_newTotal = new System.Windows.Forms.Label();
			l_status = new System.Windows.Forms.Label();
			l_updateLog = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			l_diff = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			tb_changeDiscount = new System.Windows.Forms.TextBox();
			btn_changeDiscount = new System.Windows.Forms.Button();
			label9 = new System.Windows.Forms.Label();
			l_Total = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)infolist1).BeginInit();
			SuspendLayout();
			pb_virtualKeyBoard.Location = new System.Drawing.Point(975, 640);
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 678);
			pb_virtualKeyBoard.Visible = false;
			panel2.BackgroundImage = POS_Client.Properties.Resources.inside_button;
			panel2.Controls.Add(btn_changeDiscount2);
			panel2.Controls.Add(btn_PrintDoc);
			panel2.Controls.Add(btn_pre);
			panel2.Controls.Add(btn_next);
			panel2.Controls.Add(btn_DeliveryCancel);
			panel2.Controls.Add(btn_returnBack);
			panel2.Controls.Add(btn_enterPrice);
			panel2.Controls.Add(btn_clear);
			panel2.Controls.Add(btn_enterCount);
			panel2.Controls.Add(numnine);
			panel2.Controls.Add(numeight);
			panel2.Controls.Add(numseven);
			panel2.Controls.Add(numfive);
			panel2.Controls.Add(numsix);
			panel2.Controls.Add(numfour);
			panel2.Controls.Add(numthree);
			panel2.Controls.Add(numtwo);
			panel2.Controls.Add(numzero);
			panel2.Controls.Add(btn_backspace);
			panel2.Controls.Add(numone);
			panel2.Controls.Add(btn_Save);
			panel2.Location = new System.Drawing.Point(12, 473);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(966, 200);
			panel2.TabIndex = 38;
			btn_changeDiscount2.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btn_changeDiscount2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_changeDiscount2.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_changeDiscount2.ForeColor = System.Drawing.Color.White;
			btn_changeDiscount2.Location = new System.Drawing.Point(616, 4);
			btn_changeDiscount2.Name = "btn_changeDiscount2";
			btn_changeDiscount2.Size = new System.Drawing.Size(102, 90);
			btn_changeDiscount2.TabIndex = 57;
			btn_changeDiscount2.Text = "變更折讓";
			btn_changeDiscount2.UseVisualStyleBackColor = false;
			btn_changeDiscount2.Click += new System.EventHandler(btn_changeDiscount_Click);
			btn_PrintDoc.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btn_PrintDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_PrintDoc.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_PrintDoc.ForeColor = System.Drawing.Color.White;
			btn_PrintDoc.Location = new System.Drawing.Point(203, 4);
			btn_PrintDoc.Name = "btn_PrintDoc";
			btn_PrintDoc.Size = new System.Drawing.Size(102, 90);
			btn_PrintDoc.TabIndex = 56;
			btn_PrintDoc.Text = "列印出貨單";
			btn_PrintDoc.UseVisualStyleBackColor = false;
			btn_PrintDoc.Click += new System.EventHandler(btn_PrintDoc_Click);
			btn_pre.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_pre.Location = new System.Drawing.Point(458, 4);
			btn_pre.Name = "btn_pre";
			btn_pre.Size = new System.Drawing.Size(42, 90);
			btn_pre.TabIndex = 55;
			btn_pre.Text = "↑";
			btn_pre.UseVisualStyleBackColor = true;
			btn_pre.Click += new System.EventHandler(btn_pre_Click);
			btn_next.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_next.Location = new System.Drawing.Point(458, 98);
			btn_next.Name = "btn_next";
			btn_next.Size = new System.Drawing.Size(42, 91);
			btn_next.TabIndex = 54;
			btn_next.Text = "↓";
			btn_next.UseVisualStyleBackColor = true;
			btn_next.Click += new System.EventHandler(btn_next_Click);
			btn_DeliveryCancel.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btn_DeliveryCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_DeliveryCancel.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_DeliveryCancel.ForeColor = System.Drawing.Color.White;
			btn_DeliveryCancel.Location = new System.Drawing.Point(616, 99);
			btn_DeliveryCancel.Name = "btn_DeliveryCancel";
			btn_DeliveryCancel.Size = new System.Drawing.Size(102, 90);
			btn_DeliveryCancel.TabIndex = 53;
			btn_DeliveryCancel.Text = "取消出貨單";
			btn_DeliveryCancel.UseVisualStyleBackColor = false;
			btn_DeliveryCancel.Click += new System.EventHandler(btn_purchaseReturn_Click);
			btn_returnBack.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btn_returnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_returnBack.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_returnBack.ForeColor = System.Drawing.Color.White;
			btn_returnBack.Location = new System.Drawing.Point(203, 99);
			btn_returnBack.Name = "btn_returnBack";
			btn_returnBack.Size = new System.Drawing.Size(102, 90);
			btn_returnBack.TabIndex = 52;
			btn_returnBack.Text = "結束編修";
			btn_returnBack.UseVisualStyleBackColor = false;
			btn_returnBack.Click += new System.EventHandler(btn_returnBack_Click);
			btn_enterPrice.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btn_enterPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_enterPrice.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_enterPrice.ForeColor = System.Drawing.Color.White;
			btn_enterPrice.Location = new System.Drawing.Point(507, 99);
			btn_enterPrice.Name = "btn_enterPrice";
			btn_enterPrice.Size = new System.Drawing.Size(102, 90);
			btn_enterPrice.TabIndex = 52;
			btn_enterPrice.Text = "變更出貨價";
			btn_enterPrice.UseVisualStyleBackColor = false;
			btn_enterPrice.Click += new System.EventHandler(btn_enterPrice_Click);
			btn_clear.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_clear.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_clear.ForeColor = System.Drawing.Color.White;
			btn_clear.Location = new System.Drawing.Point(20, 94);
			btn_clear.Name = "btn_clear";
			btn_clear.Size = new System.Drawing.Size(102, 90);
			btn_clear.TabIndex = 51;
			btn_clear.Text = "清除輸入";
			btn_clear.UseVisualStyleBackColor = false;
			btn_clear.Visible = false;
			btn_clear.Click += new System.EventHandler(btn_clear_Click);
			btn_enterCount.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btn_enterCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_enterCount.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_enterCount.ForeColor = System.Drawing.Color.White;
			btn_enterCount.Location = new System.Drawing.Point(507, 4);
			btn_enterCount.Name = "btn_enterCount";
			btn_enterCount.Size = new System.Drawing.Size(102, 90);
			btn_enterCount.TabIndex = 51;
			btn_enterCount.Text = "變更數量";
			btn_enterCount.UseVisualStyleBackColor = false;
			btn_enterCount.Click += new System.EventHandler(btn_enterCount_Click);
			numnine.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numnine.Location = new System.Drawing.Point(409, 4);
			numnine.Name = "numnine";
			numnine.Size = new System.Drawing.Size(42, 42);
			numnine.TabIndex = 50;
			numnine.Text = "9";
			numnine.UseVisualStyleBackColor = true;
			numnine.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numeight.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numeight.Location = new System.Drawing.Point(360, 4);
			numeight.Name = "numeight";
			numeight.Size = new System.Drawing.Size(42, 42);
			numeight.TabIndex = 49;
			numeight.Text = "8";
			numeight.UseVisualStyleBackColor = true;
			numeight.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numseven.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numseven.Location = new System.Drawing.Point(311, 4);
			numseven.Name = "numseven";
			numseven.Size = new System.Drawing.Size(42, 42);
			numseven.TabIndex = 48;
			numseven.Text = "7";
			numseven.UseVisualStyleBackColor = true;
			numseven.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numfive.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfive.Location = new System.Drawing.Point(360, 52);
			numfive.Name = "numfive";
			numfive.Size = new System.Drawing.Size(42, 42);
			numfive.TabIndex = 47;
			numfive.Text = "5";
			numfive.UseVisualStyleBackColor = true;
			numfive.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numsix.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numsix.Location = new System.Drawing.Point(409, 52);
			numsix.Name = "numsix";
			numsix.Size = new System.Drawing.Size(42, 42);
			numsix.TabIndex = 46;
			numsix.Text = "6";
			numsix.UseVisualStyleBackColor = true;
			numsix.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numfour.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfour.Location = new System.Drawing.Point(311, 52);
			numfour.Name = "numfour";
			numfour.Size = new System.Drawing.Size(42, 42);
			numfour.TabIndex = 45;
			numfour.Text = "4";
			numfour.UseVisualStyleBackColor = true;
			numfour.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numthree.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numthree.Location = new System.Drawing.Point(409, 100);
			numthree.Name = "numthree";
			numthree.Size = new System.Drawing.Size(42, 42);
			numthree.TabIndex = 44;
			numthree.Text = "3";
			numthree.UseVisualStyleBackColor = true;
			numthree.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numtwo.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numtwo.Location = new System.Drawing.Point(360, 100);
			numtwo.Name = "numtwo";
			numtwo.Size = new System.Drawing.Size(42, 42);
			numtwo.TabIndex = 43;
			numtwo.Text = "2";
			numtwo.UseVisualStyleBackColor = true;
			numtwo.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numzero.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numzero.Location = new System.Drawing.Point(311, 147);
			numzero.Name = "numzero";
			numzero.Size = new System.Drawing.Size(42, 42);
			numzero.TabIndex = 42;
			numzero.Text = "0";
			numzero.UseVisualStyleBackColor = true;
			numzero.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			btn_backspace.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_backspace.Location = new System.Drawing.Point(359, 147);
			btn_backspace.Name = "btn_backspace";
			btn_backspace.Size = new System.Drawing.Size(93, 42);
			btn_backspace.TabIndex = 41;
			btn_backspace.Text = "backspace";
			btn_backspace.UseVisualStyleBackColor = true;
			btn_backspace.Click += new System.EventHandler(btn_backspace_Click);
			numone.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numone.Location = new System.Drawing.Point(311, 99);
			numone.Name = "numone";
			numone.Size = new System.Drawing.Size(42, 42);
			numone.TabIndex = 40;
			numone.Text = "1";
			numone.UseVisualStyleBackColor = true;
			numone.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			btn_Save.BackColor = System.Drawing.Color.FromArgb(250, 87, 0);
			btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_Save.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_Save.ForeColor = System.Drawing.Color.White;
			btn_Save.Location = new System.Drawing.Point(724, 4);
			btn_Save.Name = "btn_Save";
			btn_Save.Size = new System.Drawing.Size(102, 186);
			btn_Save.TabIndex = 36;
			btn_Save.Text = "儲存變更";
			btn_Save.UseVisualStyleBackColor = false;
			btn_Save.Click += new System.EventHandler(btn_Save_Click);
			l_vendorInfo.AutoSize = true;
			l_vendorInfo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_vendorInfo.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			l_vendorInfo.Location = new System.Drawing.Point(589, 342);
			l_vendorInfo.Name = "l_vendorInfo";
			l_vendorInfo.Size = new System.Drawing.Size(64, 24);
			l_vendorInfo.TabIndex = 13;
			l_vendorInfo.Text = "label4";
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label15.ForeColor = System.Drawing.Color.Black;
			label15.Location = new System.Drawing.Point(687, 407);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(105, 24);
			label15.TabIndex = 44;
			label15.Text = "出貨單狀態";
			l_adujstPrice.AutoSize = true;
			l_adujstPrice.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_adujstPrice.ForeColor = System.Drawing.Color.Red;
			l_adujstPrice.Location = new System.Drawing.Point(589, 407);
			l_adujstPrice.Name = "l_adujstPrice";
			l_adujstPrice.Size = new System.Drawing.Size(64, 24);
			l_adujstPrice.TabIndex = 43;
			l_adujstPrice.Text = "label4";
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.Black;
			label12.Location = new System.Drawing.Point(478, 407);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(105, 24);
			label12.TabIndex = 42;
			label12.Text = "調整後總價";
			l_OriSum.AutoSize = true;
			l_OriSum.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_OriSum.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			l_OriSum.Location = new System.Drawing.Point(120, 374);
			l_OriSum.Name = "l_OriSum";
			l_OriSum.Size = new System.Drawing.Size(64, 24);
			l_OriSum.TabIndex = 41;
			l_OriSum.Text = "label4";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.Black;
			label10.Location = new System.Drawing.Point(10, 374);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(105, 24);
			label10.TabIndex = 40;
			label10.Text = "原單據總額";
			l_DeliveryDate.AutoSize = true;
			l_DeliveryDate.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_DeliveryDate.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			l_DeliveryDate.Location = new System.Drawing.Point(356, 342);
			l_DeliveryDate.Name = "l_DeliveryDate";
			l_DeliveryDate.Size = new System.Drawing.Size(64, 24);
			l_DeliveryDate.TabIndex = 39;
			l_DeliveryDate.Text = "label4";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.Black;
			label8.Location = new System.Drawing.Point(264, 342);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(86, 24);
			label8.TabIndex = 38;
			label8.Text = "出貨日期";
			l_DeliveryCustomNo.AutoSize = true;
			l_DeliveryCustomNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_DeliveryCustomNo.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			l_DeliveryCustomNo.Location = new System.Drawing.Point(120, 342);
			l_DeliveryCustomNo.Name = "l_DeliveryCustomNo";
			l_DeliveryCustomNo.Size = new System.Drawing.Size(64, 24);
			l_DeliveryCustomNo.TabIndex = 37;
			l_DeliveryCustomNo.Text = "label4";
			member.AutoSize = true;
			member.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			member.ForeColor = System.Drawing.Color.Black;
			member.Location = new System.Drawing.Point(28, 342);
			member.Name = "member";
			member.Size = new System.Drawing.Size(86, 24);
			member.TabIndex = 36;
			member.Text = "出貨單號";
			infolist1.AllowUserToAddRows = false;
			infolist1.AllowUserToDeleteRows = false;
			infolist1.AllowUserToResizeColumns = false;
			infolist1.AllowUserToResizeRows = false;
			infolist1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			infolist1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			infolist1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			infolist1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
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
			infolist1.Columns.AddRange(Column1, commodity, beforePrice, beforeQuantity, subTotal, afterPrice, afterQuantity, adjustInfo, plus, minus, zero, hidden_beforeAdjustInventory, hidden_detailID);
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
			infolist1.Location = new System.Drawing.Point(12, 76);
			infolist1.Name = "infolist1";
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
			infolist1.RowTemplate.Height = 40;
			infolist1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			infolist1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			infolist1.Size = new System.Drawing.Size(966, 252);
			infolist1.TabIndex = 35;
			infolist1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(infolist1_CellContentClick);
			infolist1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(infolist1_RowPostPaint);
			infolist1.SelectionChanged += new System.EventHandler(infolist_SelectionChanged);
			Column1.HeaderText = "";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			beforePrice.DefaultCellStyle = dataGridViewCellStyle5;
			beforePrice.HeaderText = "出貨價";
			beforePrice.Name = "beforePrice";
			beforePrice.ReadOnly = true;
			beforePrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			beforePrice.Width = 75;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			beforeQuantity.DefaultCellStyle = dataGridViewCellStyle6;
			beforeQuantity.HeaderText = "數量";
			beforeQuantity.Name = "beforeQuantity";
			beforeQuantity.ReadOnly = true;
			beforeQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			beforeQuantity.Width = 60;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Red;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Red;
			subTotal.DefaultCellStyle = dataGridViewCellStyle7;
			subTotal.HeaderText = "小計";
			subTotal.Name = "subTotal";
			subTotal.ReadOnly = true;
			subTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			subTotal.Width = 70;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Red;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Red;
			afterPrice.DefaultCellStyle = dataGridViewCellStyle8;
			afterPrice.HeaderText = "變更出貨價";
			afterPrice.Name = "afterPrice";
			afterPrice.ReadOnly = true;
			afterPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			afterPrice.Width = 105;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Red;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Red;
			afterQuantity.DefaultCellStyle = dataGridViewCellStyle9;
			afterQuantity.HeaderText = "變更數量";
			afterQuantity.Name = "afterQuantity";
			afterQuantity.ReadOnly = true;
			afterQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			afterQuantity.Width = 90;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Red;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Red;
			adjustInfo.DefaultCellStyle = dataGridViewCellStyle10;
			adjustInfo.HeaderText = "調整(數量/金額)";
			adjustInfo.Name = "adjustInfo";
			adjustInfo.ReadOnly = true;
			adjustInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			adjustInfo.Width = 135;
			plus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			plus.HeaderText = "+";
			plus.MinimumWidth = 20;
			plus.Name = "plus";
			plus.ReadOnly = true;
			plus.Text = "+";
			plus.UseColumnTextForButtonValue = true;
			plus.Width = 37;
			minus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			minus.HeaderText = "-";
			minus.MinimumWidth = 20;
			minus.Name = "minus";
			minus.ReadOnly = true;
			minus.Text = "-";
			minus.UseColumnTextForButtonValue = true;
			minus.Width = 32;
			zero.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			zero.HeaderText = "x";
			zero.MinimumWidth = 20;
			zero.Name = "zero";
			zero.ReadOnly = true;
			zero.Text = "x";
			zero.UseColumnTextForButtonValue = true;
			zero.Width = 33;
			hidden_beforeAdjustInventory.HeaderText = "(隱藏_調整前庫存)";
			hidden_beforeAdjustInventory.Name = "hidden_beforeAdjustInventory";
			hidden_beforeAdjustInventory.Visible = false;
			hidden_detailID.HeaderText = "(隱藏_detailID)";
			hidden_detailID.Name = "hidden_detailID";
			hidden_detailID.Visible = false;
			l_DeliveryCreateDate.AutoSize = true;
			l_DeliveryCreateDate.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			l_DeliveryCreateDate.ForeColor = System.Drawing.Color.Black;
			l_DeliveryCreateDate.Location = new System.Drawing.Point(647, 49);
			l_DeliveryCreateDate.Name = "l_DeliveryCreateDate";
			l_DeliveryCreateDate.Size = new System.Drawing.Size(65, 24);
			l_DeliveryCreateDate.TabIndex = 34;
			l_DeliveryCreateDate.Text = "label6";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label7.ForeColor = System.Drawing.Color.Black;
			label7.Image = POS_Client.Properties.Resources.oblique;
			label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label7.Location = new System.Drawing.Point(497, 49);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(144, 24);
			label7.TabIndex = 33;
			label7.Text = "   建置日期時間:";
			l_DeliveryNo.AutoSize = true;
			l_DeliveryNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			l_DeliveryNo.ForeColor = System.Drawing.Color.Black;
			l_DeliveryNo.Location = new System.Drawing.Point(136, 49);
			l_DeliveryNo.Name = "l_DeliveryNo";
			l_DeliveryNo.Size = new System.Drawing.Size(65, 24);
			l_DeliveryNo.TabIndex = 32;
			l_DeliveryNo.Text = "label4";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			label5.ForeColor = System.Drawing.Color.Black;
			label5.Image = POS_Client.Properties.Resources.oblique;
			label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label5.Location = new System.Drawing.Point(24, 49);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(106, 24);
			label5.TabIndex = 31;
			label5.Text = "   系統單號:";
			alertMsg.Font = new System.Drawing.Font("微軟正黑體", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			alertMsg.Location = new System.Drawing.Point(501, 440);
			alertMsg.Multiline = true;
			alertMsg.Name = "alertMsg";
			alertMsg.ReadOnly = true;
			alertMsg.Size = new System.Drawing.Size(319, 27);
			alertMsg.TabIndex = 30;
			tb_temp.BackColor = System.Drawing.Color.White;
			tb_temp.Font = new System.Drawing.Font("Calibri", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			tb_temp.Location = new System.Drawing.Point(123, 440);
			tb_temp.Name = "tb_temp";
			tb_temp.ReadOnly = true;
			tb_temp.Size = new System.Drawing.Size(359, 26);
			tb_temp.TabIndex = 29;
			tb_temp.Enter += new System.EventHandler(tb_temp_Enter);
			tb_temp.Leave += new System.EventHandler(tb_temp_Leave);
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.Black;
			label13.Location = new System.Drawing.Point(28, 441);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(86, 24);
			label13.TabIndex = 28;
			label13.Text = "執行動作";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.Black;
			label1.Location = new System.Drawing.Point(497, 342);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(86, 24);
			label1.TabIndex = 10;
			label1.Text = "出貨廠商";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.Black;
			label3.Location = new System.Drawing.Point(10, 407);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(105, 24);
			label3.TabIndex = 38;
			label3.Text = "調整後總計";
			l_newTotal.AutoSize = true;
			l_newTotal.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_newTotal.ForeColor = System.Drawing.Color.Red;
			l_newTotal.Location = new System.Drawing.Point(120, 407);
			l_newTotal.Name = "l_newTotal";
			l_newTotal.Size = new System.Drawing.Size(64, 24);
			l_newTotal.TabIndex = 39;
			l_newTotal.Text = "label4";
			l_status.AutoSize = true;
			l_status.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_status.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			l_status.Location = new System.Drawing.Point(798, 407);
			l_status.Name = "l_status";
			l_status.Size = new System.Drawing.Size(64, 24);
			l_status.TabIndex = 13;
			l_status.Text = "label4";
			l_updateLog.AutoSize = true;
			l_updateLog.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 136);
			l_updateLog.ForeColor = System.Drawing.Color.Blue;
			l_updateLog.Location = new System.Drawing.Point(868, 410);
			l_updateLog.Name = "l_updateLog";
			l_updateLog.Size = new System.Drawing.Size(73, 20);
			l_updateLog.TabIndex = 13;
			l_updateLog.Text = "變更紀錄";
			l_updateLog.Click += new System.EventHandler(l_updateLog_Click);
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.Black;
			label2.Location = new System.Drawing.Point(264, 407);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(86, 24);
			label2.TabIndex = 52;
			label2.Text = "調整價差";
			l_diff.AutoSize = true;
			l_diff.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_diff.ForeColor = System.Drawing.Color.Red;
			l_diff.Location = new System.Drawing.Point(356, 407);
			l_diff.Name = "l_diff";
			l_diff.Size = new System.Drawing.Size(64, 24);
			l_diff.TabIndex = 53;
			l_diff.Text = "label4";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.Black;
			label6.Location = new System.Drawing.Point(226, 374);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(124, 24);
			label6.TabIndex = 54;
			label6.Text = "總價折讓調整";
			tb_changeDiscount.Font = new System.Drawing.Font("微軟正黑體", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			tb_changeDiscount.Location = new System.Drawing.Point(356, 376);
			tb_changeDiscount.Multiline = true;
			tb_changeDiscount.Name = "tb_changeDiscount";
			tb_changeDiscount.ReadOnly = true;
			tb_changeDiscount.Size = new System.Drawing.Size(126, 27);
			tb_changeDiscount.TabIndex = 55;
			btn_changeDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_changeDiscount.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_changeDiscount.Image = POS_Client.Properties.Resources.ic_input_black_24dp_1x;
			btn_changeDiscount.Location = new System.Drawing.Point(488, 376);
			btn_changeDiscount.Name = "btn_changeDiscount";
			btn_changeDiscount.Size = new System.Drawing.Size(48, 27);
			btn_changeDiscount.TabIndex = 66;
			btn_changeDiscount.UseVisualStyleBackColor = true;
			btn_changeDiscount.Click += new System.EventHandler(btn_changeDiscount_Click);
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label9.ForeColor = System.Drawing.Color.Black;
			label9.Location = new System.Drawing.Point(668, 376);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(124, 24);
			label9.TabIndex = 67;
			label9.Text = "原出貨單總價";
			l_Total.AutoSize = true;
			l_Total.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_Total.ForeColor = System.Drawing.Color.Red;
			l_Total.Location = new System.Drawing.Point(798, 374);
			l_Total.Name = "l_Total";
			l_Total.Size = new System.Drawing.Size(64, 24);
			l_Total.TabIndex = 68;
			l_Total.Text = "label4";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(252, 252, 237);
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(991, 671);
			base.Controls.Add(l_Total);
			base.Controls.Add(label9);
			base.Controls.Add(btn_changeDiscount);
			base.Controls.Add(tb_changeDiscount);
			base.Controls.Add(label6);
			base.Controls.Add(l_diff);
			base.Controls.Add(label2);
			base.Controls.Add(l_updateLog);
			base.Controls.Add(l_status);
			base.Controls.Add(l_vendorInfo);
			base.Controls.Add(panel2);
			base.Controls.Add(label15);
			base.Controls.Add(l_adujstPrice);
			base.Controls.Add(label5);
			base.Controls.Add(label12);
			base.Controls.Add(label1);
			base.Controls.Add(l_OriSum);
			base.Controls.Add(label10);
			base.Controls.Add(l_newTotal);
			base.Controls.Add(l_DeliveryDate);
			base.Controls.Add(label13);
			base.Controls.Add(label3);
			base.Controls.Add(label8);
			base.Controls.Add(tb_temp);
			base.Controls.Add(l_DeliveryCustomNo);
			base.Controls.Add(alertMsg);
			base.Controls.Add(member);
			base.Controls.Add(l_DeliveryNo);
			base.Controls.Add(infolist1);
			base.Controls.Add(label7);
			base.Controls.Add(l_DeliveryCreateDate);
			base.Name = "frmEditDeliveryOrder";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "frmMainShop";
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmEditInventory_KeyDown);
			base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(frmEditInventory_KeyPress);
			base.Controls.SetChildIndex(l_DeliveryCreateDate, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(infolist1, 0);
			base.Controls.SetChildIndex(l_DeliveryNo, 0);
			base.Controls.SetChildIndex(member, 0);
			base.Controls.SetChildIndex(alertMsg, 0);
			base.Controls.SetChildIndex(l_DeliveryCustomNo, 0);
			base.Controls.SetChildIndex(tb_temp, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(label13, 0);
			base.Controls.SetChildIndex(l_DeliveryDate, 0);
			base.Controls.SetChildIndex(l_newTotal, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(l_OriSum, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(l_adujstPrice, 0);
			base.Controls.SetChildIndex(label15, 0);
			base.Controls.SetChildIndex(panel2, 0);
			base.Controls.SetChildIndex(l_vendorInfo, 0);
			base.Controls.SetChildIndex(l_status, 0);
			base.Controls.SetChildIndex(l_updateLog, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(label2, 0);
			base.Controls.SetChildIndex(l_diff, 0);
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(tb_changeDiscount, 0);
			base.Controls.SetChildIndex(btn_changeDiscount, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(l_Total, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)infolist1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
