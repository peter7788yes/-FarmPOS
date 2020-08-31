using POS_Client.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmEditInventory : MasterThinForm
	{
		private string _purchaseNo = "";

		private bool action;

		private bool isFocus;

		private bool islock;

		private vendorInfo vendorINFO = new vendorInfo();

		private string[] InfoSetting = new string[2]
		{
			"",
			""
		};

		public bool CanDo;

		private IContainer components;

		private Panel panel2;

		private Label label1;

		private Label l_supplierInfo;

		private Label label13;

		private TextBox tb_temp;

		private TextBox alertMsg;

		public Button btn_Save;

		public Button btn_purchaseReturn;

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

		private Label l_createDate;

		private Label label7;

		private Label l_purcaseNo;

		private Label label5;

		public DataGridView infolist1;

		private Label label15;

		private Label l_adujstPrice;

		private Label label12;

		private Label l_oldTotal;

		private Label label10;

		private Label l_purchaseDate;

		private Label label8;

		private Label l_PurchaseCustomNo;

		private Label member;

		private Button btn_pre;

		private Button btn_next;

		public Button btn_returnBack;

		public Button btn_clear;

		private Label label3;

		private Label l_newTotal;

		private Label l_status;

		private Label l_updateLog;

		public Button btn_adjustTypeF;

		public Button btn_adjustTypeE;

		private Label l_checkVendorInfo;

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

		private DataGridViewTextBoxColumn hidden_BatchNo;

		private DataGridViewTextBoxColumn hidden_MFGDate;

		private DataGridViewTextBoxColumn hidden_POSBatchNo;

		private DataGridViewTextBoxColumn hidden_NewBatchNo;

		private DataGridViewTextBoxColumn hidden_NewMFGDate;

		private DataGridViewTextBoxColumn hidden_returnType;

		public frmEditInventory(string purchaseNo)
			: base("編修進貨單")
		{
			_purchaseNo = purchaseNo;
			InitializeComponent();
			pb_virtualKeyBoard.Visible = false;
			string strSelectField = "s.SupplierName, s.SupplierIdNo, m.PurchaseCustomNo, m.*";
			string strTableName = "hypos_PurchaseGoods_Master m, hypos_Supplier s";
			string strWhereClause = "m.SupplierNo = s.SupplierNo and m.PurchaseNo = {0}";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, strSelectField, strTableName, strWhereClause, "", null, new string[1]
			{
				purchaseNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (!"".Equals(dataTable.Rows[0]["oldCBNO"].ToString()))
			{
				islock = true;
			}
			if (dataTable.Rows.Count > 0)
			{
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_Supplier where SupplierNo = '" + dataTable.Rows[0]["SupplierNo"].ToString() + "' ", null, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count > 0)
				{
					vendorINFO.SupplierName = dataTable2.Rows[0]["SupplierName"].ToString();
					vendorINFO.SupplierNo = dataTable2.Rows[0]["SupplierNo"].ToString();
					vendorINFO.SupplierIdNo = dataTable2.Rows[0]["SupplierIdNo"].ToString();
					vendorINFO.vendorId = dataTable2.Rows[0]["vendorId"].ToString();
					vendorINFO.vendorName = dataTable2.Rows[0]["vendorName"].ToString();
				}
				string text = dataTable.Rows[0]["Status"].ToString();
				switch (text)
				{
				case "0":
					text = "正常";
					break;
				case "1":
					text = "正常(變更)";
					l_updateLog.Visible = true;
					break;
				case "2":
					text = "取消";
					break;
				}
				l_status.Text = text;
				l_purcaseNo.Text = purchaseNo;
				l_createDate.Text = dataTable.Rows[0]["CreateDate"].ToString();
				l_PurchaseCustomNo.Text = dataTable.Rows[0]["PurchaseCustomNo"].ToString();
				l_purchaseDate.Text = dataTable.Rows[0]["PurchaseDate"].ToString();
				l_oldTotal.Text = dataTable.Rows[0]["Total"].ToString();
				l_newTotal.Text = dataTable.Rows[0]["Total"].ToString();
				l_adujstPrice.Text = "0";
				l_supplierInfo.Text = string.Format("{0} {1}({2})", dataTable.Rows[0]["SupplierNo"].ToString(), dataTable.Rows[0]["SupplierName"].ToString(), dataTable.Rows[0]["SupplierIdNo"].ToString());
			}
			else
			{
				MessageBox.Show("進貨單資訊異常");
				backToPreviousForm();
			}
			strSelectField = "d.*,g.*";
			strTableName = "hypos_PurchaseGoods_Master m, hypos_PurchaseGoods_Detail d,hypos_GOODSLST g";
			strWhereClause = "g.GDSNO = d.GDSNO and m.PurchaseNo = d.PurchaseNo and m.PurchaseNo = {0}";
			dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, strSelectField, strTableName, strWhereClause, "", null, new string[1]
			{
				purchaseNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					int num = int.Parse(row["Cost"].ToString());
					int num2 = int.Parse(row["Quantity"].ToString());
					string text2 = string.IsNullOrEmpty(row["inventory"].ToString()) ? "0" : row["inventory"].ToString();
					CommodityInfo commodityInfo = new CommodityInfo();
					commodityInfo.setMemberIdNo("");
					commodityInfo.setHiddenGDSNO(row["GDSNO"].ToString());
					commodityInfo.setMemberVipNo("店內碼:" + row["GDSNO"].ToString());
					commodityInfo.setCommodityName(setCommodityName(row) + " " + row["spec"].ToString() + " " + row["capacity"].ToString());
					commodityInfo.setCommodityClass("批號:" + row["BatchNo"].ToString() + " 製造日期:" + row["MFGDate"].ToString());
					commodityInfo.setHiddenBatchNo(row["BatchNo"].ToString());
					commodityInfo.setHiddenMFGDate(row["MFGDate"].ToString());
					commodityInfo.setHiddenPOSBatchNo(row["POSBatchNo"].ToString());
					commodityInfo.setlabe1("");
					commodityInfo.BackColor = Color.FromArgb(255, 208, 81);
					infolist1.Rows.Add(0, commodityInfo, num, num2, num * num2, num, num2, "0/0", "+", "-", "X", text2, row["BatchNo"].ToString(), row["MFGDate"].ToString(), row["POSBatchNo"].ToString(), "", "", "");
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
			l_adujstPrice.Text = (int.Parse(l_adujstPrice.Text) + num6 - num5).ToString();
			l_newTotal.Text = (int.Parse(l_newTotal.Text) + num6 - num5).ToString();
			string text = (num > num3) ? ("+" + (num - num3)) : (num - num3).ToString();
			string arg = (num6 > num4) ? ("+" + (num6 - num4)) : (num6 - num4).ToString();
			infolist1.CurrentRow.Cells["adjustInfo"].Value = string.Format("{0}/{1}", text, arg);
			infolist1.CurrentRow.Cells["afterPrice"].Value = num2;
			infolist1.CurrentRow.Cells["afterQuantity"].Value = num;
			string text2 = Regex.Replace(text, "[\\W_]+", "");
			tb_temp.Text = text2;
			tb_temp.Focus();
			tb_temp.SelectionStart = tb_temp.Text.Length;
			if (text.Substring(0, 1) == "+")
			{
				CanDo = true;
			}
			else
			{
				CanDo = false;
			}
		}

		private void adjustPurchaseReturn(object price, object count, string Type)
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
			l_adujstPrice.Text = (int.Parse(l_adujstPrice.Text) + num6 - num5).ToString();
			l_newTotal.Text = (int.Parse(l_newTotal.Text) + num6 - num5).ToString();
			string arg = (num > num3) ? ("+" + (num - num3)) : (num - num3).ToString();
			string arg2 = (num6 > num4) ? ("+" + (num6 - num4)) : (num6 - num4).ToString();
			if (Type.Equals("E"))
			{
				infolist1.CurrentRow.Cells["adjustInfo"].Value = string.Format("{0}/{1}\n(原廠退回)", arg, arg2);
				infolist1.CurrentRow.Cells["adjustInfo"].Style.ForeColor = Color.Blue;
			}
			else if (Type.Equals("F"))
			{
				infolist1.CurrentRow.Cells["adjustInfo"].Value = string.Format("{0}/{1}\n(過期退貨)", arg, arg2);
				infolist1.CurrentRow.Cells["adjustInfo"].Style.ForeColor = Color.Blue;
			}
			else
			{
				infolist1.CurrentRow.Cells["adjustInfo"].Value = string.Format("{0}/{1}", arg, arg2);
			}
			infolist1.CurrentRow.Cells["afterPrice"].Value = num2;
			infolist1.CurrentRow.Cells["afterQuantity"].Value = num;
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
				int result = 0;
				int.TryParse(tb_temp.Text, out result);
				try
				{
					string text = "";
					foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
					{
						string commodityName = (item.Cells["commodity"].Value as CommodityInfo).getCommodityName();
						DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = {0} order by id desc limit 1", new string[1]
						{
							item.Cells["hidden_POSBatchNo"].Value.ToString()
						}, CommandOperationType.ExecuteReaderReturnDataTable);
						int num = int.Parse(item.Cells[3].Value.ToString()) - result;
						if (dataTable.Rows.Count > 0)
						{
							if (num > int.Parse(dataTable.Rows[0][0].ToString()))
							{
								text = commodityName + "\n此批號商品目前剩餘數量為" + dataTable.Rows[0][0].ToString() + "，請重新設定減少數量\n\n";
							}
						}
						else
						{
							text = "";
						}
					}
					infolist1.CurrentRow.Cells["hidden_returnType"].Value = "";
					if (!string.IsNullOrEmpty(text))
					{
						AutoClosingMessageBox.Show(text);
						tb_temp.Text = "";
						return;
					}
				}
				catch (Exception ex)
				{
					AutoClosingMessageBox.Show("批號數量（變更）紀錄檢查錯誤\n" + ex.ToString());
				}
				adjustPurchase(null, result);
				action = true;
				alertMsg.Text = "變更進貨數量";
				tb_temp.Text = "";
			}
			tb_temp.Focus();
			tb_temp.SelectionStart = tb_temp.Text.Length;
		}

		private void btn_enterPrice_Click(object sender, EventArgs e)
		{
			if (!islock)
			{
				if (!infolist1.CurrentRow.Selected)
				{
					AutoClosingMessageBox.Show("請先選擇商品");
				}
				else
				{
					int result = 0;
					int.TryParse(tb_temp.Text, out result);
					adjustPurchase(result, null);
					action = true;
					alertMsg.Text = "變更進貨價格";
					tb_temp.Text = "";
				}
				tb_temp.Focus();
				tb_temp.SelectionStart = tb_temp.Text.Length;
			}
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
				string value = infolist1.CurrentRow.Cells["hidden_returnType"].Value.ToString();
				if ("E".Equals(value) || "F".Equals(value))
				{
					if (MessageBox.Show("此商品已設定原廠退回/過期退貨，是否要變更為一般數量增減？ 點選確定變更為一般增減，點選取消保留原設定。", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						adjustPurchase(null, ++num);
						infolist1.CurrentRow.Cells["hidden_returnType"].Value = "";
					}
				}
				else
				{
					adjustPurchase(null, ++num);
					infolist1.CurrentRow.Cells["hidden_returnType"].Value = "";
				}
			}
			else if (e.ColumnIndex == 9)
			{
				string value2 = infolist1.CurrentRow.Cells["hidden_returnType"].Value.ToString();
				if ("E".Equals(value2) || "F".Equals(value2))
				{
					if (MessageBox.Show("此商品已設定原廠退回/過期退貨，是否要變更為一般數量增減？ 點選確定變更為一般增減，點選取消保留原設定。", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
					{
						return;
					}
					try
					{
						string text = "";
						foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
						{
							string commodityName = (item.Cells["commodity"].Value as CommodityInfo).getCommodityName();
							DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = {0} order by id desc limit 1", new string[1]
							{
								item.Cells["hidden_POSBatchNo"].Value.ToString()
							}, CommandOperationType.ExecuteReaderReturnDataTable);
							int num2 = int.Parse(item.Cells[3].Value.ToString()) - int.Parse(item.Cells[6].Value.ToString());
							if (dataTable.Rows.Count > 0)
							{
								if (num2 + 1 > int.Parse(dataTable.Rows[0][0].ToString()))
								{
									text = commodityName + "\n此批號商品目前剩餘數量為" + dataTable.Rows[0][0].ToString() + "，請重新設定減少數量\n\n";
								}
							}
							else
							{
								text = "";
							}
						}
						infolist1.CurrentRow.Cells["hidden_returnType"].Value = "";
						if (!string.IsNullOrEmpty(text))
						{
							AutoClosingMessageBox.Show(text);
							return;
						}
					}
					catch (Exception ex)
					{
						AutoClosingMessageBox.Show("批號數量（變更）紀錄檢查錯誤\n" + ex.ToString());
					}
					if (num != 0)
					{
						adjustPurchase(null, --num);
					}
					return;
				}
				try
				{
					string text2 = "";
					string commodityName2 = (infolist1.CurrentRow.Cells["commodity"].Value as CommodityInfo).getCommodityName();
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = {0} order by id desc limit 1", new string[1]
					{
						infolist1.CurrentRow.Cells["hidden_POSBatchNo"].Value.ToString()
					}, CommandOperationType.ExecuteReaderReturnDataTable);
					int num3 = int.Parse(infolist1.CurrentRow.Cells[3].Value.ToString()) - int.Parse(infolist1.CurrentRow.Cells[6].Value.ToString());
					if (dataTable2.Rows.Count > 0)
					{
						if (num3 + 1 > int.Parse(dataTable2.Rows[0][0].ToString()))
						{
							text2 = commodityName2 + "\n此批號商品目前剩餘數量為" + dataTable2.Rows[0][0].ToString() + "，請重新設定減少數量\n\n";
						}
					}
					else
					{
						text2 = "";
					}
					infolist1.CurrentRow.Cells["hidden_returnType"].Value = "";
					if (!string.IsNullOrEmpty(text2))
					{
						AutoClosingMessageBox.Show(text2);
						return;
					}
				}
				catch (Exception ex2)
				{
					AutoClosingMessageBox.Show("批號數量（變更）紀錄檢查錯誤\n" + ex2.ToString());
				}
				if (num != 0)
				{
					adjustPurchase(null, --num);
				}
			}
			else
			{
				if (e.ColumnIndex != 10)
				{
					return;
				}
				string value3 = infolist1.CurrentRow.Cells["hidden_returnType"].Value.ToString();
				if ("E".Equals(value3) || "F".Equals(value3))
				{
					if (MessageBox.Show("此商品已設定原廠退回/過期退貨，是否要變更為一般數量增減？ 點選確定變更為一般增減，點選取消保留原設定。", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						adjustPurchase(null, 0);
						infolist1.CurrentRow.Cells["hidden_returnType"].Value = "";
					}
				}
				else
				{
					adjustPurchase(null, 0);
					infolist1.CurrentRow.Cells["hidden_returnType"].Value = "";
				}
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
			try
			{
				string text = "";
				foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
				{
					string commodityName = (item.Cells["commodity"].Value as CommodityInfo).getCommodityName();
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = {0} order by id desc limit 1", new string[1]
					{
						item.Cells["hidden_POSBatchNo"].Value.ToString()
					}, CommandOperationType.ExecuteReaderReturnDataTable);
					int num = int.Parse(item.Cells[3].Value.ToString()) - int.Parse(item.Cells[6].Value.ToString());
					if (dataTable.Rows.Count > 0)
					{
						if (num > int.Parse(dataTable.Rows[0][0].ToString()))
						{
							text = commodityName + "\n此批號商品目前剩餘數量為" + dataTable.Rows[0][0].ToString() + "，請重新設定減少數量\n\n";
						}
					}
					else
					{
						text = "";
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					AutoClosingMessageBox.Show(text);
					return;
				}
			}
			catch (Exception ex)
			{
				AutoClosingMessageBox.Show("批號數量（變更）紀錄檢查錯誤\n" + ex.ToString());
			}
			int num2 = 0;
			foreach (DataGridViewRow item2 in (IEnumerable)infolist1.Rows)
			{
				if (!string.IsNullOrEmpty(item2.Cells["hidden_returnType"].Value.ToString()))
				{
					num2++;
				}
			}
			if (num2 > 0)
			{
				foreach (DataGridViewRow item3 in (IEnumerable)infolist1.Rows)
				{
					string text2 = "";
					string text3 = "";
					string hiddenGDSNO = (item3.Cells["commodity"].Value as CommodityInfo).getHiddenGDSNO();
					string value = item3.Cells["hidden_MFGDate"].Value.ToString();
					string value2 = item3.Cells["hidden_BatchNo"].Value.ToString();
					string value3 = item3.Cells["hidden_NewMFGDate"].Value.ToString();
					string value4 = item3.Cells["hidden_NewBatchNo"].Value.ToString();
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_GOODSLST where GDSNO = {0} ", new string[1]
					{
						hiddenGDSNO
					}, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable2.Rows.Count > 0)
					{
						text2 = dataTable2.Rows[0]["CLA1NO"].ToString();
						text3 = dataTable2.Rows[0]["ISWS"].ToString();
					}
					if (text2.Equals("0302") && text3.Equals("Y"))
					{
						if (!string.IsNullOrEmpty(vendorINFO.vendorId) && string.IsNullOrEmpty(vendorINFO.vendorName))
						{
							AutoClosingMessageBox.Show("請先驗證廠商營業資訊");
							return;
						}
						if (string.IsNullOrEmpty(vendorINFO.vendorId) || string.IsNullOrEmpty(vendorINFO.vendorName))
						{
							AutoClosingMessageBox.Show("執行管制農藥之原廠退回/過期退貨功能，必須驗證進貨廠商營業資訊。請先檢查廠商營業資訊。");
							return;
						}
						if (!string.IsNullOrEmpty(item3.Cells["hidden_returnType"].Value.ToString()) && (string.IsNullOrEmpty(value3) || string.IsNullOrEmpty(value4)) && (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value2)))
						{
							string commodityName2 = (item3.Cells["commodity"].Value as CommodityInfo).getCommodityName();
							AutoClosingMessageBox.Show("請設定商品「" + commodityName2 + "」製造日期與批號");
							return;
						}
					}
				}
			}
			string text4 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string sql = "update hypos_PurchaseGoods_Master set Total = {1}, Status = 1, UpdateDate = {2} where PurchaseNo = {0}";
			DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[3]
			{
				_purchaseNo,
				l_newTotal.Text,
				text4
			}, CommandOperationType.ExecuteNonQuery);
			DataBaseUtilities.DBOperation(Program.ConnectionString, "DELETE FROM hypos_PurchaseGoods_Detail WHERE PurchaseNo = {0}", new string[1]
			{
				_purchaseNo
			}, CommandOperationType.ExecuteNonQuery);
			foreach (DataGridViewRow item4 in (IEnumerable)infolist1.Rows)
			{
				string hiddenGDSNO2 = (item4.Cells["commodity"].Value as CommodityInfo).getHiddenGDSNO();
				int num3 = int.Parse(item4.Cells[5].Value.ToString()) * int.Parse(item4.Cells[6].Value.ToString());
				int num4 = int.Parse(item4.Cells[6].Value.ToString()) - int.Parse(item4.Cells[3].Value.ToString());
				int num5 = int.Parse(item4.Cells[5].Value.ToString()) - int.Parse(item4.Cells[2].Value.ToString());
				string text5 = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT inventory FROM hypos_GOODSLST where GDSNO = {0}", new string[1]
				{
					hiddenGDSNO2
				}, CommandOperationType.ExecuteScalar).ToString();
				text5 = (string.IsNullOrEmpty(text5) ? "0" : text5);
				int num6 = int.Parse(text5) + num4;
				string text6 = item4.Cells["hidden_BatchNo"].Value.ToString();
				string text7 = item4.Cells["hidden_MFGDate"].Value.ToString();
				string text8 = item4.Cells["hidden_POSBatchNo"].Value.ToString();
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST set inventory = {1} WHERE GDSNO = {0}", new string[2]
				{
					hiddenGDSNO2,
					num6.ToString()
				}, CommandOperationType.ExecuteNonQuery);
				string[,] strFieldArray = new string[8, 2]
				{
					{
						"PurchaseNo",
						_purchaseNo
					},
					{
						"GDSNO",
						hiddenGDSNO2
					},
					{
						"Cost",
						item4.Cells["afterPrice"].Value.ToString()
					},
					{
						"Quantity",
						item4.Cells["afterQuantity"].Value.ToString()
					},
					{
						"GoodsTotalCountLog",
						num6.ToString()
					},
					{
						"BatchNo",
						text6
					},
					{
						"MFGDate",
						text7
					},
					{
						"POSBatchNo",
						text8
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_PurchaseGoods_Detail", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				string text9 = "";
				if (!string.IsNullOrEmpty(item4.Cells["hidden_returnType"].Value.ToString()))
				{
					text9 = item4.Cells["hidden_returnType"].Value.ToString();
				}
				string[,] strFieldArray2 = new string[12, 2]
				{
					{
						"PurchaseNo",
						_purchaseNo
					},
					{
						"GDSNO",
						hiddenGDSNO2
					},
					{
						"beforeCost",
						item4.Cells["beforePrice"].Value.ToString()
					},
					{
						"beforeQuantity",
						item4.Cells["beforeQuantity"].Value.ToString()
					},
					{
						"beforeSubTotal",
						item4.Cells["subTotal"].Value.ToString()
					},
					{
						"afterCost",
						item4.Cells["afterPrice"].Value.ToString()
					},
					{
						"afterQuantity",
						item4.Cells["afterQuantity"].Value.ToString()
					},
					{
						"afterSubTotal",
						num3.ToString()
					},
					{
						"adjustQuantity",
						num4.ToString()
					},
					{
						"adjustMoney",
						num5.ToString()
					},
					{
						"updateDate",
						text4
					},
					{
						"adjustType",
						text9
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_PurchaseGoods_Detail_Log", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				string text10 = item4.Cells[6].Value.ToString();
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_PurchaseGoodsBatchNo_log set num = {0} WHERE POSBatchNo = {1}", new string[2]
				{
					text10,
					text8
				}, CommandOperationType.ExecuteNonQuery);
				DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = {0} order by id desc limit 1", new string[1]
				{
					item4.Cells["hidden_POSBatchNo"].Value.ToString()
				}, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable3.Rows.Count > 0)
				{
					string text11 = (int.Parse(dataTable3.Rows[0][0].ToString()) + int.Parse(item4.Cells[6].Value.ToString()) - int.Parse(item4.Cells[3].Value.ToString())).ToString();
					string text12 = (int.Parse(item4.Cells[6].Value.ToString()) - int.Parse(item4.Cells[3].Value.ToString())).ToString();
					string[,] strFieldArray3 = new string[5, 2]
					{
						{
							"POSBatchNo",
							text8
						},
						{
							"barcode",
							hiddenGDSNO2
						},
						{
							"num",
							text12
						},
						{
							"backlogQuantity",
							text11
						},
						{
							"createDate",
							text4
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_BatchNo_log", "", "", strFieldArray3, null, CommandOperationType.ExecuteNonQuery);
				}
				if (text9.Equals("E") || text9.Equals("F"))
				{
					string text13 = item4.Cells["hidden_NewBatchNo"].Value.ToString();
					string text14 = item4.Cells["hidden_NewMFGDate"].Value.ToString();
					string text15 = "";
					string text16 = "";
					text15 = ((!string.IsNullOrEmpty(text13)) ? text13 : text6);
					text16 = ((!string.IsNullOrEmpty(text14)) ? text14 : text7);
					string[,] strFieldArray4 = new string[10, 2]
					{
						{
							"AdjustNo",
							getNewAdjustNo()
						},
						{
							"GDSNO",
							hiddenGDSNO2
						},
						{
							"adjustType",
							text9
						},
						{
							"adjustCount",
							num4.ToString()
						},
						{
							"updateDate",
							text4
						},
						{
							"GoodsTotalCountLog",
							num6.ToString()
						},
						{
							"batchNO",
							text15
						},
						{
							"MFD",
							text16
						},
						{
							"vendorId",
							vendorINFO.vendorId
						},
						{
							"vendorName",
							vendorINFO.vendorName
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_InventoryAdjustment", null, null, strFieldArray4, null, CommandOperationType.ExecuteNonQuery);
				}
				string text17 = "";
				text17 = ((!(item4.Cells["hidden_NewBatchNo"].Value.ToString() != "")) ? item4.Cells["hidden_BatchNo"].Value.ToString() : item4.Cells["hidden_NewBatchNo"].Value.ToString());
				string text18 = "";
				text18 = ((!(item4.Cells["hidden_NewMFGDate"].Value.ToString() != "")) ? item4.Cells["hidden_MFGDate"].Value.ToString() : item4.Cells["hidden_NewMFGDate"].Value.ToString());
				string sql2 = "update hypos_PurchaseGoods_Detail set BatchNo = {0}, MFGDate = {1} where PurchaseNo = {2} and GDSNO = {3}";
				DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, new string[4]
				{
					text17,
					text18,
					_purchaseNo,
					hiddenGDSNO2
				}, CommandOperationType.ExecuteNonQuery);
			}
			if ("frmEditSupplier".Equals(base.Owner.Name))
			{
				frmEditSupplier frmEditSupplier = (frmEditSupplier)base.Owner;
				frmEditSupplier.dataGridView1.CurrentRow.Cells[2].Value = l_newTotal.Text;
				frmEditSupplier.l_Total.Text = (int.Parse(frmEditSupplier.l_Total.Text) - int.Parse(l_oldTotal.Text) + int.Parse(l_newTotal.Text)).ToString();
			}
			if ("frmInventoryMangement".Equals(base.Owner.Name))
			{
				frmInventoryMangement obj = (frmInventoryMangement)base.Owner;
				obj.dataGridView1.CurrentRow.Cells[3].Value = l_newTotal.Text;
				obj.dataGridView1.CurrentRow.Cells[4].Value = "正常(變更)";
			}
			AutoClosingMessageBox.Show("所有變更已儲存");
			switchForm(base.Owner);
		}

		private void btn_purchaseReturn_Click(object sender, EventArgs e)
		{
			if (islock)
			{
				return;
			}
			try
			{
				string text = "";
				foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
				{
					string hiddenGDSNO = (item.Cells["commodity"].Value as CommodityInfo).getHiddenGDSNO();
					string hiddenBatchNo = (item.Cells["commodity"].Value as CommodityInfo).getHiddenBatchNo();
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = {0} order by id desc limit 1", new string[1]
					{
						item.Cells["hidden_POSBatchNo"].Value.ToString()
					}, CommandOperationType.ExecuteReaderReturnDataTable);
					int num = int.Parse(item.Cells[3].Value.ToString());
					if (dataTable.Rows.Count > 0)
					{
						if (num > int.Parse(dataTable.Rows[0][0].ToString()))
						{
							text = "本進貨單之" + hiddenGDSNO + "商品之" + hiddenBatchNo + "已進行過出貨作業，無法取消整筆進貨單\n";
						}
					}
					else
					{
						text = "";
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					AutoClosingMessageBox.Show(text);
					return;
				}
			}
			catch (Exception ex)
			{
				AutoClosingMessageBox.Show("批號數量（變更）紀錄檢查錯誤\n" + ex.ToString());
			}
			if (MessageBox.Show("整筆訂單取消，確認後將無法復原。確定取消？", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return;
			}
			string text2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string sql = "update hypos_PurchaseGoods_Master set Status = 2,Total = 0, UpdateDate = {1} where PurchaseNo = {0}";
			DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[2]
			{
				_purchaseNo,
				DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
			}, CommandOperationType.ExecuteNonQuery);
			DataBaseUtilities.DBOperation(Program.ConnectionString, "DELETE FROM hypos_PurchaseGoods_Detail WHERE PurchaseNo = {0}", new string[1]
			{
				_purchaseNo
			}, CommandOperationType.ExecuteNonQuery);
			foreach (DataGridViewRow item2 in (IEnumerable)infolist1.Rows)
			{
				string hiddenGDSNO2 = (item2.Cells["commodity"].Value as CommodityInfo).getHiddenGDSNO();
				int num2 = 0;
				int num3 = -int.Parse(item2.Cells[3].Value.ToString());
				int num4 = int.Parse(item2.Cells[5].Value.ToString()) - int.Parse(item2.Cells[2].Value.ToString());
				string text3 = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT inventory FROM hypos_GOODSLST where GDSNO = {0}", new string[1]
				{
					hiddenGDSNO2
				}, CommandOperationType.ExecuteScalar).ToString();
				text3 = (string.IsNullOrEmpty(text3) ? "0" : text3);
				int num5 = int.Parse(text3) + num3;
				string text4 = item2.Cells["hidden_BatchNo"].Value.ToString();
				string text5 = item2.Cells["hidden_MFGDate"].Value.ToString();
				string text6 = item2.Cells["hidden_POSBatchNo"].Value.ToString();
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST set inventory = {1} WHERE GDSNO = {0}", new string[2]
				{
					hiddenGDSNO2,
					num5.ToString()
				}, CommandOperationType.ExecuteNonQuery);
				string[,] strFieldArray = new string[8, 2]
				{
					{
						"PurchaseNo",
						_purchaseNo
					},
					{
						"GDSNO",
						hiddenGDSNO2
					},
					{
						"Cost",
						item2.Cells["afterPrice"].Value.ToString()
					},
					{
						"Quantity",
						"0"
					},
					{
						"GoodsTotalCountLog",
						num5.ToString()
					},
					{
						"BatchNo",
						text4
					},
					{
						"MFGDate",
						text5
					},
					{
						"POSBatchNo",
						text6
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_PurchaseGoods_Detail", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				string[,] strFieldArray2 = new string[11, 2]
				{
					{
						"PurchaseNo",
						_purchaseNo
					},
					{
						"GDSNO",
						hiddenGDSNO2
					},
					{
						"beforeCost",
						item2.Cells["beforePrice"].Value.ToString()
					},
					{
						"beforeQuantity",
						item2.Cells["beforeQuantity"].Value.ToString()
					},
					{
						"beforeSubTotal",
						item2.Cells["subTotal"].Value.ToString()
					},
					{
						"afterCost",
						"0"
					},
					{
						"afterQuantity",
						"0"
					},
					{
						"afterSubTotal",
						num2.ToString()
					},
					{
						"adjustQuantity",
						num3.ToString()
					},
					{
						"adjustMoney",
						num4.ToString()
					},
					{
						"updateDate",
						text2
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_PurchaseGoods_Detail_Log", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				string text7 = "0";
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_PurchaseGoodsBatchNo_log set num = {0} WHERE POSBatchNo = {1}", new string[2]
				{
					text7,
					text6
				}, CommandOperationType.ExecuteNonQuery);
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = {0} order by id desc limit 1", new string[1]
				{
					item2.Cells["hidden_POSBatchNo"].Value.ToString()
				}, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count > 0)
				{
					string text8 = "0";
					string text9 = (-int.Parse(dataTable2.Rows[0][0].ToString())).ToString();
					string[,] strFieldArray3 = new string[5, 2]
					{
						{
							"POSBatchNo",
							text6
						},
						{
							"barcode",
							hiddenGDSNO2
						},
						{
							"num",
							text9
						},
						{
							"backlogQuantity",
							text8
						},
						{
							"createDate",
							text2
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_BatchNo_log", "", "", strFieldArray3, null, CommandOperationType.ExecuteNonQuery);
				}
			}
			if ("frmEditSupplier".Equals(base.Owner.Name))
			{
				frmEditSupplier frmEditSupplier = (frmEditSupplier)base.Owner;
				frmEditSupplier.dataGridView1.CurrentRow.Cells[2].Value = "0";
				frmEditSupplier.dataGridView1.CurrentRow.Cells[3].Value = "取消";
				frmEditSupplier.dataGridView1.CurrentRow.Cells["hidden_status"].Value = "2";
				frmEditSupplier.l_Total.Text = (int.Parse(frmEditSupplier.l_Total.Text) - int.Parse(l_oldTotal.Text)).ToString();
			}
			if ("frmInventoryMangement".Equals(base.Owner.Name))
			{
				frmInventoryMangement obj = (frmInventoryMangement)base.Owner;
				obj.dataGridView1.CurrentRow.Cells[3].Value = "0";
				obj.dataGridView1.CurrentRow.Cells[4].Value = "取消";
				obj.dataGridView1.CurrentRow.Cells["hidden_Status"].Value = "2";
			}
			switchForm(base.Owner);
		}

		private void l_updateLog_Click(object sender, EventArgs e)
		{
			new dialogPurchaseUpdateLog(_purchaseNo).ShowDialog();
		}

		private void l_checkVendorInfo_Click(object sender, EventArgs e)
		{
			new dialogCheckVendorInfo(this).ShowDialog();
		}

		public void set_vendorINFO(string vendorId, string vendorName)
		{
			vendorINFO.vendorId = vendorId;
			vendorINFO.vendorName = vendorName;
		}

		public string get_SupplierNo()
		{
			return vendorINFO.SupplierNo;
		}

		public string get_SupplierName()
		{
			return vendorINFO.SupplierName;
		}

		public string get_SupplierIdNo()
		{
			return vendorINFO.SupplierIdNo;
		}

		public string get_vendorId()
		{
			return vendorINFO.vendorId;
		}

		public string get_vendorName()
		{
			return vendorINFO.vendorName;
		}

		public void infolistInfoSetting(string[] data)
		{
			try
			{
				if (InfoSetting.Length == data.Length)
				{
					for (int i = 0; i < InfoSetting.Length; i++)
					{
						InfoSetting[i] = data[i];
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void ResetinfolistInfoSetting()
		{
			try
			{
				for (int i = 0; i < InfoSetting.Length; i++)
				{
					InfoSetting[i] = "";
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static string getNewAdjustNo()
		{
			string sql = "SELECT AdjustNo FROM hypos_InventoryAdjustment order by AdjustNo desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			string text2 = DateTime.Now.Year.ToString().Substring(2, 2) + string.Format("{0:00}", DateTime.Now.Month);
			if ("-1".Equals(text) || string.IsNullOrEmpty(text))
			{
				return string.Format("{0}{1}0001", Program.SiteNo, text2);
			}
			string value = text.Substring(2, 4);
			if (!text2.Equals(value))
			{
				return string.Format("{0}{1}0001", Program.SiteNo, text2);
			}
			string arg = string.Format("{0:0000}", int.Parse(text.Substring(6, 4)) + 1);
			return string.Format("{0}{1}{2}", Program.SiteNo, text2, arg);
		}

		private void btn_adjustTypeE_Click(object sender, EventArgs e)
		{
			if (CanDo)
			{
				MessageBox.Show("『加號「+」不可進行退貨』");
			}
			else
			{
				if (islock)
				{
					return;
				}
				try
				{
					if (!infolist1.CurrentRow.Selected)
					{
						AutoClosingMessageBox.Show("請先選擇商品");
						return;
					}
					if ("".Equals(tb_temp.Text))
					{
						AutoClosingMessageBox.Show("請先輸入欲退貨之數量");
						return;
					}
					int result = 0;
					int.TryParse(tb_temp.Text, out result);
					if (result > int.Parse(infolist1.CurrentRow.Cells["beforeQuantity"].Value.ToString()))
					{
						AutoClosingMessageBox.Show("欲退貨之數量不得大於進貨數量");
						return;
					}
					string value = infolist1.CurrentRow.Cells["hidden_MFGDate"].Value.ToString();
					string value2 = infolist1.CurrentRow.Cells["hidden_BatchNo"].Value.ToString();
					string text = "";
					string text2 = "";
					text = ((!string.IsNullOrEmpty(infolist1.CurrentRow.Cells["hidden_NewMFGDate"].Value.ToString())) ? infolist1.CurrentRow.Cells["hidden_NewMFGDate"].Value.ToString() : "");
					text2 = ((!string.IsNullOrEmpty(infolist1.CurrentRow.Cells["hidden_NewBatchNo"].Value.ToString())) ? infolist1.CurrentRow.Cells["hidden_NewBatchNo"].Value.ToString() : "");
					string text3 = "";
					string text4 = "";
					string hiddenGDSNO = (infolist1.CurrentRow.Cells["commodity"].Value as CommodityInfo).getHiddenGDSNO();
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_GOODSLST where GDSNO = {0} ", new string[1]
					{
						hiddenGDSNO
					}, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable.Rows.Count > 0)
					{
						text3 = dataTable.Rows[0]["CLA1NO"].ToString();
						text4 = dataTable.Rows[0]["ISWS"].ToString();
					}
					if (text3.Equals("0302") && text4.Equals("Y") && (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value2)) && (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2)) && new dialogSetBatchNoAndMFGdate3(this, hiddenGDSNO).ShowDialog() == DialogResult.Cancel)
					{
						return;
					}
					string text5 = "";
					string commodityName = (infolist1.CurrentRow.Cells["commodity"].Value as CommodityInfo).getCommodityName();
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = {0} order by id desc limit 1", new string[1]
					{
						infolist1.CurrentRow.Cells["hidden_POSBatchNo"].Value.ToString()
					}, CommandOperationType.ExecuteReaderReturnDataTable);
					int num = result;
					if (dataTable2.Rows.Count > 0)
					{
						if (num > int.Parse(dataTable2.Rows[0][0].ToString()))
						{
							text5 = commodityName + "\n此批號商品目前剩餘數量為" + dataTable2.Rows[0][0].ToString() + "，請重新設定減少數量\n\n";
						}
					}
					else
					{
						text5 = "";
					}
					if (!string.IsNullOrEmpty(text5))
					{
						AutoClosingMessageBox.Show(text5);
						return;
					}
					infolist1.CurrentRow.Cells["hidden_returnType"].Value = "E";
					infolist1.CurrentRow.Cells["hidden_NewBatchNo"].Value = InfoSetting[0];
					infolist1.CurrentRow.Cells["hidden_NewMFGDate"].Value = InfoSetting[1];
					ResetinfolistInfoSetting();
					adjustPurchaseReturn(null, int.Parse(infolist1.CurrentRow.Cells["beforeQuantity"].Value.ToString()) - result, "E");
				}
				catch (Exception ex)
				{
					AutoClosingMessageBox.Show("原廠退回功能錯誤\n" + ex.ToString());
				}
				finally
				{
					tb_temp.Text = "";
					tb_temp.Focus();
					tb_temp.SelectionStart = tb_temp.Text.Length;
				}
			}
		}

		private void btn_adjustTypeF_Click(object sender, EventArgs e)
		{
			if (CanDo)
			{
				MessageBox.Show("『加號「+」不可進行退貨』");
			}
			else
			{
				if (islock)
				{
					return;
				}
				try
				{
					if (!infolist1.CurrentRow.Selected)
					{
						AutoClosingMessageBox.Show("請先選擇商品");
						return;
					}
					if ("".Equals(tb_temp.Text))
					{
						AutoClosingMessageBox.Show("請先輸入欲退貨之數量");
						return;
					}
					int result = 0;
					int.TryParse(tb_temp.Text, out result);
					if (result > int.Parse(infolist1.CurrentRow.Cells["beforeQuantity"].Value.ToString()))
					{
						AutoClosingMessageBox.Show("欲退貨之數量不得大於進貨數量");
						return;
					}
					string value = infolist1.CurrentRow.Cells["hidden_MFGDate"].Value.ToString();
					string value2 = infolist1.CurrentRow.Cells["hidden_BatchNo"].Value.ToString();
					string text = "";
					string text2 = "";
					text = ((!string.IsNullOrEmpty(infolist1.CurrentRow.Cells["hidden_NewMFGDate"].Value.ToString())) ? infolist1.CurrentRow.Cells["hidden_NewMFGDate"].Value.ToString() : "");
					text2 = ((!string.IsNullOrEmpty(infolist1.CurrentRow.Cells["hidden_NewBatchNo"].Value.ToString())) ? infolist1.CurrentRow.Cells["hidden_NewBatchNo"].Value.ToString() : "");
					string text3 = "";
					string text4 = "";
					string hiddenGDSNO = (infolist1.CurrentRow.Cells["commodity"].Value as CommodityInfo).getHiddenGDSNO();
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_GOODSLST where GDSNO = {0} ", new string[1]
					{
						hiddenGDSNO
					}, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable.Rows.Count > 0)
					{
						text3 = dataTable.Rows[0]["CLA1NO"].ToString();
						text4 = dataTable.Rows[0]["ISWS"].ToString();
					}
					if (text3.Equals("0302") && text4.Equals("Y") && (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value2)) && (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2)) && new dialogSetBatchNoAndMFGdate3(this, hiddenGDSNO).ShowDialog() == DialogResult.Cancel)
					{
						return;
					}
					string text5 = "";
					foreach (DataGridViewRow item in (IEnumerable)infolist1.Rows)
					{
						DataGridViewRow dataGridViewRow = item;
						string commodityName = (infolist1.CurrentRow.Cells["commodity"].Value as CommodityInfo).getCommodityName();
						DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT backlogQuantity FROM hypos_BatchNo_log where POSBatchNo = {0} order by id desc limit 1", new string[1]
						{
							infolist1.CurrentRow.Cells["hidden_POSBatchNo"].Value.ToString()
						}, CommandOperationType.ExecuteReaderReturnDataTable);
						int num = result;
						if (dataTable2.Rows.Count > 0)
						{
							if (num > int.Parse(dataTable2.Rows[0][0].ToString()))
							{
								text5 = commodityName + "\n此批號商品目前剩餘數量為" + dataTable2.Rows[0][0].ToString() + "，請重新設定減少數量\n\n";
							}
						}
						else
						{
							text5 = "";
						}
					}
					if (!string.IsNullOrEmpty(text5))
					{
						AutoClosingMessageBox.Show(text5);
						return;
					}
					infolist1.CurrentRow.Cells["hidden_returnType"].Value = "F";
					infolist1.CurrentRow.Cells["hidden_NewBatchNo"].Value = InfoSetting[0];
					infolist1.CurrentRow.Cells["hidden_NewMFGDate"].Value = InfoSetting[1];
					ResetinfolistInfoSetting();
					adjustPurchaseReturn(null, int.Parse(infolist1.CurrentRow.Cells["beforeQuantity"].Value.ToString()) - result, "F");
				}
				catch (Exception ex)
				{
					AutoClosingMessageBox.Show("過期退貨功能錯誤\n" + ex.ToString());
				}
				finally
				{
					tb_temp.Text = "";
					tb_temp.Focus();
					tb_temp.SelectionStart = tb_temp.Text.Length;
				}
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
			btn_adjustTypeF = new System.Windows.Forms.Button();
			btn_adjustTypeE = new System.Windows.Forms.Button();
			btn_pre = new System.Windows.Forms.Button();
			btn_next = new System.Windows.Forms.Button();
			btn_purchaseReturn = new System.Windows.Forms.Button();
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
			l_supplierInfo = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			l_adujstPrice = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			l_oldTotal = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			l_purchaseDate = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			l_PurchaseCustomNo = new System.Windows.Forms.Label();
			member = new System.Windows.Forms.Label();
			infolist1 = new System.Windows.Forms.DataGridView();
			l_createDate = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			l_purcaseNo = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			alertMsg = new System.Windows.Forms.TextBox();
			tb_temp = new System.Windows.Forms.TextBox();
			label13 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			l_newTotal = new System.Windows.Forms.Label();
			l_status = new System.Windows.Forms.Label();
			l_updateLog = new System.Windows.Forms.Label();
			l_checkVendorInfo = new System.Windows.Forms.Label();
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
			hidden_BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_MFGDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_POSBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_NewBatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_NewMFGDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_returnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)infolist1).BeginInit();
			SuspendLayout();
			pb_virtualKeyBoard.Location = new System.Drawing.Point(975, 640);
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 678);
			pb_virtualKeyBoard.Visible = false;
			panel2.BackgroundImage = POS_Client.Properties.Resources.inside_button;
			panel2.Controls.Add(btn_adjustTypeF);
			panel2.Controls.Add(btn_adjustTypeE);
			panel2.Controls.Add(btn_pre);
			panel2.Controls.Add(btn_next);
			panel2.Controls.Add(btn_purchaseReturn);
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
			btn_adjustTypeF.BackColor = System.Drawing.Color.RoyalBlue;
			btn_adjustTypeF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_adjustTypeF.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_adjustTypeF.ForeColor = System.Drawing.Color.White;
			btn_adjustTypeF.Location = new System.Drawing.Point(615, 99);
			btn_adjustTypeF.Name = "btn_adjustTypeF";
			btn_adjustTypeF.Size = new System.Drawing.Size(102, 90);
			btn_adjustTypeF.TabIndex = 57;
			btn_adjustTypeF.Text = "過期退貨";
			btn_adjustTypeF.UseVisualStyleBackColor = false;
			btn_adjustTypeF.Click += new System.EventHandler(btn_adjustTypeF_Click);
			btn_adjustTypeE.BackColor = System.Drawing.Color.RoyalBlue;
			btn_adjustTypeE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_adjustTypeE.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_adjustTypeE.ForeColor = System.Drawing.Color.White;
			btn_adjustTypeE.Location = new System.Drawing.Point(615, 4);
			btn_adjustTypeE.Name = "btn_adjustTypeE";
			btn_adjustTypeE.Size = new System.Drawing.Size(102, 90);
			btn_adjustTypeE.TabIndex = 56;
			btn_adjustTypeE.Text = "原廠退回";
			btn_adjustTypeE.UseVisualStyleBackColor = false;
			btn_adjustTypeE.Click += new System.EventHandler(btn_adjustTypeE_Click);
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
			btn_purchaseReturn.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btn_purchaseReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_purchaseReturn.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_purchaseReturn.ForeColor = System.Drawing.Color.White;
			btn_purchaseReturn.Location = new System.Drawing.Point(723, 4);
			btn_purchaseReturn.Name = "btn_purchaseReturn";
			btn_purchaseReturn.Size = new System.Drawing.Size(102, 186);
			btn_purchaseReturn.TabIndex = 53;
			btn_purchaseReturn.Text = "取消進貨單";
			btn_purchaseReturn.UseVisualStyleBackColor = false;
			btn_purchaseReturn.Click += new System.EventHandler(btn_purchaseReturn_Click);
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
			btn_enterPrice.Text = "變更進貨價";
			btn_enterPrice.UseVisualStyleBackColor = false;
			btn_enterPrice.Click += new System.EventHandler(btn_enterPrice_Click);
			btn_clear.BackColor = System.Drawing.Color.FromArgb(170, 206, 0);
			btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_clear.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_clear.ForeColor = System.Drawing.Color.White;
			btn_clear.Location = new System.Drawing.Point(203, 4);
			btn_clear.Name = "btn_clear";
			btn_clear.Size = new System.Drawing.Size(102, 90);
			btn_clear.TabIndex = 51;
			btn_clear.Text = "清除輸入";
			btn_clear.UseVisualStyleBackColor = false;
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
			btn_Save.Location = new System.Drawing.Point(831, 3);
			btn_Save.Name = "btn_Save";
			btn_Save.Size = new System.Drawing.Size(102, 186);
			btn_Save.TabIndex = 36;
			btn_Save.Text = "儲存變更";
			btn_Save.UseVisualStyleBackColor = false;
			btn_Save.Click += new System.EventHandler(btn_Save_Click);
			l_supplierInfo.AutoSize = true;
			l_supplierInfo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_supplierInfo.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			l_supplierInfo.Location = new System.Drawing.Point(589, 342);
			l_supplierInfo.Name = "l_supplierInfo";
			l_supplierInfo.Size = new System.Drawing.Size(64, 24);
			l_supplierInfo.TabIndex = 13;
			l_supplierInfo.Text = "label4";
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label15.ForeColor = System.Drawing.Color.Black;
			label15.Location = new System.Drawing.Point(728, 396);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(105, 24);
			label15.TabIndex = 44;
			label15.Text = "進貨單狀態";
			l_adujstPrice.AutoSize = true;
			l_adujstPrice.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_adujstPrice.ForeColor = System.Drawing.Color.Red;
			l_adujstPrice.Location = new System.Drawing.Point(589, 396);
			l_adujstPrice.Name = "l_adujstPrice";
			l_adujstPrice.Size = new System.Drawing.Size(64, 24);
			l_adujstPrice.TabIndex = 43;
			l_adujstPrice.Text = "label4";
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.Black;
			label12.Location = new System.Drawing.Point(496, 396);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(86, 24);
			label12.TabIndex = 42;
			label12.Text = "調整金額";
			l_oldTotal.AutoSize = true;
			l_oldTotal.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_oldTotal.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			l_oldTotal.Location = new System.Drawing.Point(120, 396);
			l_oldTotal.Name = "l_oldTotal";
			l_oldTotal.Size = new System.Drawing.Size(64, 24);
			l_oldTotal.TabIndex = 41;
			l_oldTotal.Text = "label4";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.Black;
			label10.Location = new System.Drawing.Point(28, 396);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(86, 24);
			label10.TabIndex = 40;
			label10.Text = "單據總額";
			l_purchaseDate.AutoSize = true;
			l_purchaseDate.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_purchaseDate.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			l_purchaseDate.Location = new System.Drawing.Point(356, 342);
			l_purchaseDate.Name = "l_purchaseDate";
			l_purchaseDate.Size = new System.Drawing.Size(64, 24);
			l_purchaseDate.TabIndex = 39;
			l_purchaseDate.Text = "label4";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.Black;
			label8.Location = new System.Drawing.Point(264, 342);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(86, 24);
			label8.TabIndex = 38;
			label8.Text = "進貨日期";
			l_PurchaseCustomNo.AutoSize = true;
			l_PurchaseCustomNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_PurchaseCustomNo.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			l_PurchaseCustomNo.Location = new System.Drawing.Point(120, 342);
			l_PurchaseCustomNo.Name = "l_PurchaseCustomNo";
			l_PurchaseCustomNo.Size = new System.Drawing.Size(64, 24);
			l_PurchaseCustomNo.TabIndex = 37;
			l_PurchaseCustomNo.Text = "label4";
			member.AutoSize = true;
			member.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			member.ForeColor = System.Drawing.Color.Black;
			member.Location = new System.Drawing.Point(28, 342);
			member.Name = "member";
			member.Size = new System.Drawing.Size(86, 24);
			member.TabIndex = 36;
			member.Text = "進貨單號";
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
			infolist1.Columns.AddRange(Column1, commodity, beforePrice, beforeQuantity, subTotal, afterPrice, afterQuantity, adjustInfo, plus, minus, zero, hidden_beforeAdjustInventory, hidden_BatchNo, hidden_MFGDate, hidden_POSBatchNo, hidden_NewBatchNo, hidden_NewMFGDate, hidden_returnType);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
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
			l_createDate.AutoSize = true;
			l_createDate.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			l_createDate.ForeColor = System.Drawing.Color.Black;
			l_createDate.Location = new System.Drawing.Point(647, 49);
			l_createDate.Name = "l_createDate";
			l_createDate.Size = new System.Drawing.Size(65, 24);
			l_createDate.TabIndex = 34;
			l_createDate.Text = "label6";
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
			l_purcaseNo.AutoSize = true;
			l_purcaseNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold);
			l_purcaseNo.ForeColor = System.Drawing.Color.Black;
			l_purcaseNo.Location = new System.Drawing.Point(136, 49);
			l_purcaseNo.Name = "l_purcaseNo";
			l_purcaseNo.Size = new System.Drawing.Size(65, 24);
			l_purcaseNo.TabIndex = 32;
			l_purcaseNo.Text = "label4";
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
			alertMsg.Location = new System.Drawing.Point(501, 437);
			alertMsg.Multiline = true;
			alertMsg.Name = "alertMsg";
			alertMsg.ReadOnly = true;
			alertMsg.Size = new System.Drawing.Size(319, 27);
			alertMsg.TabIndex = 30;
			tb_temp.BackColor = System.Drawing.Color.White;
			tb_temp.Font = new System.Drawing.Font("Calibri", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			tb_temp.Location = new System.Drawing.Point(123, 437);
			tb_temp.Name = "tb_temp";
			tb_temp.ReadOnly = true;
			tb_temp.Size = new System.Drawing.Size(359, 26);
			tb_temp.TabIndex = 29;
			tb_temp.Enter += new System.EventHandler(tb_temp_Enter);
			tb_temp.Leave += new System.EventHandler(tb_temp_Leave);
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.Black;
			label13.Location = new System.Drawing.Point(28, 438);
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
			label1.Text = "進貨廠商";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.Black;
			label3.Location = new System.Drawing.Point(245, 396);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(105, 24);
			label3.TabIndex = 38;
			label3.Text = "調整後總價";
			l_newTotal.AutoSize = true;
			l_newTotal.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_newTotal.ForeColor = System.Drawing.Color.Red;
			l_newTotal.Location = new System.Drawing.Point(356, 396);
			l_newTotal.Name = "l_newTotal";
			l_newTotal.Size = new System.Drawing.Size(64, 24);
			l_newTotal.TabIndex = 39;
			l_newTotal.Text = "label4";
			l_status.AutoSize = true;
			l_status.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_status.ForeColor = System.Drawing.Color.FromArgb(61, 156, 74);
			l_status.Location = new System.Drawing.Point(839, 396);
			l_status.Name = "l_status";
			l_status.Size = new System.Drawing.Size(64, 24);
			l_status.TabIndex = 13;
			l_status.Text = "label4";
			l_updateLog.AutoSize = true;
			l_updateLog.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 136);
			l_updateLog.ForeColor = System.Drawing.Color.Blue;
			l_updateLog.Location = new System.Drawing.Point(839, 420);
			l_updateLog.Name = "l_updateLog";
			l_updateLog.Size = new System.Drawing.Size(73, 20);
			l_updateLog.TabIndex = 13;
			l_updateLog.Text = "變更紀錄";
			l_updateLog.Visible = false;
			l_updateLog.Click += new System.EventHandler(l_updateLog_Click);
			l_checkVendorInfo.AutoSize = true;
			l_checkVendorInfo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 136);
			l_checkVendorInfo.ForeColor = System.Drawing.Color.Blue;
			l_checkVendorInfo.Location = new System.Drawing.Point(589, 369);
			l_checkVendorInfo.Name = "l_checkVendorInfo";
			l_checkVendorInfo.Size = new System.Drawing.Size(105, 20);
			l_checkVendorInfo.TabIndex = 52;
			l_checkVendorInfo.Text = "補驗營業資訊";
			l_checkVendorInfo.Click += new System.EventHandler(l_checkVendorInfo_Click);
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
			beforePrice.HeaderText = "進貨價";
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
			afterPrice.HeaderText = "變更進貨價";
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
			hidden_BatchNo.HeaderText = "(隱藏_批號)";
			hidden_BatchNo.Name = "hidden_BatchNo";
			hidden_BatchNo.Visible = false;
			hidden_MFGDate.HeaderText = "(隱藏_製造日期)";
			hidden_MFGDate.Name = "hidden_MFGDate";
			hidden_MFGDate.Visible = false;
			hidden_POSBatchNo.HeaderText = "(隱藏_批號序號)";
			hidden_POSBatchNo.Name = "hidden_POSBatchNo";
			hidden_POSBatchNo.Visible = false;
			hidden_NewBatchNo.HeaderText = "(隱藏_退貨新增批號)";
			hidden_NewBatchNo.Name = "hidden_NewBatchNo";
			hidden_NewBatchNo.Visible = false;
			hidden_NewMFGDate.HeaderText = "(隱藏_退貨新增製造日期)";
			hidden_NewMFGDate.Name = "hidden_NewMFGDate";
			hidden_NewMFGDate.Visible = false;
			hidden_returnType.HeaderText = "(隱藏_退貨類型)";
			hidden_returnType.Name = "hidden_returnType";
			hidden_returnType.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(252, 252, 237);
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(991, 671);
			base.Controls.Add(l_checkVendorInfo);
			base.Controls.Add(l_updateLog);
			base.Controls.Add(l_status);
			base.Controls.Add(l_supplierInfo);
			base.Controls.Add(panel2);
			base.Controls.Add(label15);
			base.Controls.Add(l_adujstPrice);
			base.Controls.Add(label5);
			base.Controls.Add(label12);
			base.Controls.Add(label1);
			base.Controls.Add(l_oldTotal);
			base.Controls.Add(label10);
			base.Controls.Add(l_newTotal);
			base.Controls.Add(l_purchaseDate);
			base.Controls.Add(label13);
			base.Controls.Add(label3);
			base.Controls.Add(label8);
			base.Controls.Add(tb_temp);
			base.Controls.Add(l_PurchaseCustomNo);
			base.Controls.Add(alertMsg);
			base.Controls.Add(member);
			base.Controls.Add(l_purcaseNo);
			base.Controls.Add(infolist1);
			base.Controls.Add(label7);
			base.Controls.Add(l_createDate);
			base.Name = "frmEditInventory";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "frmMainShop";
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmEditInventory_KeyDown);
			base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(frmEditInventory_KeyPress);
			base.Controls.SetChildIndex(l_createDate, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(infolist1, 0);
			base.Controls.SetChildIndex(l_purcaseNo, 0);
			base.Controls.SetChildIndex(member, 0);
			base.Controls.SetChildIndex(alertMsg, 0);
			base.Controls.SetChildIndex(l_PurchaseCustomNo, 0);
			base.Controls.SetChildIndex(tb_temp, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(label13, 0);
			base.Controls.SetChildIndex(l_purchaseDate, 0);
			base.Controls.SetChildIndex(l_newTotal, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(l_oldTotal, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(label12, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(l_adujstPrice, 0);
			base.Controls.SetChildIndex(label15, 0);
			base.Controls.SetChildIndex(panel2, 0);
			base.Controls.SetChildIndex(l_supplierInfo, 0);
			base.Controls.SetChildIndex(l_status, 0);
			base.Controls.SetChildIndex(l_updateLog, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(l_checkVendorInfo, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)infolist1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
