using POS_Client.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmNewInventory : MasterThinForm
	{
		private string supplierNo = "";

		private string newPurchaseNo = "";

		public CommodityInfo uc;

		private TextBox lastFocused;

		private string[] InfoSetting = new string[2]
		{
			"",
			""
		};

		private bool action;

		private bool isFocusA;

		private bool isFocusB;

		private IContainer components;

		private TextBox tb_PurchaseCustomNo;

		private TextBox tb_quantity;

		private Label l_sysTime;

		private DataGridView infolist;

		private Button addone;

		private Button subone;

		private Button numone;

		private Button pressEnter;

		private Panel panel1;

		private Button backspace;

		private Button pre;

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

		private Button btn_enterPrice;

		private Button btn_enterCount;

		private Button Checkout;

		private Label label1;

		private Button btn_commoditySearch;

		private Label label3;

		private TextBox tb_subtotal;

		private TextBox tb_cost;

		private Label label4;

		private TextBox tb_supplierName;

		private Label l_totalprice;

		private Label label6;

		private Label label5;

		private Label label2;

		private Button btn_chooseSupplier;

		private TextBox tb_GDSNO;

		private DateTimePicker dt_PurchaseDate;

		private DataGridViewTextBoxColumn Column1;

		private frmMainShopSimple.CustomColumn commodityName;

		private DataGridViewTextBoxColumn cost;

		private DataGridViewTextBoxColumn quantity;

		private DataGridViewTextBoxColumn subTotal;

		private DataGridViewTextBoxColumn hidden_inventory;

		public frmNewInventory()
		{
			InitializeComponent();
		}

		private void frmNewInventory_Load(object sender, EventArgs e)
		{
			pb_virtualKeyBoard.Visible = false;
			newPurchaseNo = getNewPurchaseNo();
			setMasterFormName("進貨作業 | 單號: " + newPurchaseNo);
			tb_GDSNO.Select();
			dt_PurchaseDate.Value = DateTime.Now;
			TextBox[] array = new TextBox[2]
			{
				tb_GDSNO,
				tb_PurchaseCustomNo
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i].LostFocus += new EventHandler(textBoxFocusLost);
			}
		}

		private void textBoxFocusLost(object sender, EventArgs e)
		{
			lastFocused = (TextBox)sender;
		}

		private void btn_chooseSupplier_Click(object sender, EventArgs e)
		{
			using (dialogChooseSupplier dialogChooseSupplier = new dialogChooseSupplier())
			{
				if (dialogChooseSupplier.ShowDialog() == DialogResult.OK)
				{
					tb_supplierName.Text = dialogChooseSupplier.returnSupplierName;
					supplierNo = dialogChooseSupplier.returnSupplierNo;
					tb_GDSNO.Select();
				}
			}
		}

		public static string getNewPurchaseNo()
		{
			string sql = "SELECT PurchaseNo FROM hypos_PurchaseGoods_Master where PurchaseNo like '" + Program.SiteNo.ToString() + "%' order by PurchaseNo desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			DateTime now = DateTime.Now;
			string text2 = now.Year.ToString().Substring(2, 2) + string.Format("{0:00}", now.Month);
			if ("-1".Equals(text))
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

		public static string getBatchNo()
		{
			string sql = "SELECT POSBatchNo FROM hypos_PurchaseGoodsBatchNo_log where POSBatchNo like 'B" + Program.SiteNo.ToString() + "%' order by POSBatchNo desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			DateTime dateTime = DateTime.Now.AddDays(-1.0);
			string text2 = dateTime.Year.ToString().Substring(2, 2);
			string text3 = dateTime.Month.ToString().PadLeft(2, '0');
			if ("-1".Equals(text))
			{
				return string.Format("B{0}{1}{2}00001", Program.SiteNo, text2, text3);
			}
			string value = text.Substring(3, 2);
			if (!text2.Equals(value))
			{
				return string.Format("B{0}{1}{2}00001", Program.SiteNo, text2, text3);
			}
			string text4 = string.Format("{0:00000}", ConvertToInt(text.Substring(7, 5)) + 1);
			return string.Format("B{0}{1}{2}{3}", Program.SiteNo, text2, text3, text4);
		}

		private void frmNewInventory_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!isFocusA && !isFocusB)
			{
				if ("請刷商品條碼或輸入條碼".Equals(tb_GDSNO.Text))
				{
					tb_GDSNO.Text = "";
				}
				tb_GDSNO.Text += e.KeyChar;
				tb_GDSNO.Focus();
				tb_GDSNO.SelectionStart = tb_GDSNO.Text.Length;
			}
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (isFocusA || isFocusB)
			{
				return;
			}
			if (action)
			{
				alertMsg.Text = "";
				action = false;
			}
			if (e.KeyCode == Keys.Add)
			{
				if (infolist.Rows.Count > 0)
				{
					int num = int.Parse(infolist.CurrentRow.Cells[2].Value.ToString()) + 1;
					infolist.CurrentRow.Cells[2].Value = num.ToString();
					computetotalmoney();
					alertMsg.Text = "數量加1";
					action = true;
				}
			}
			else if (e.KeyCode == Keys.Subtract && infolist.Rows.Count > 0 && int.Parse(infolist.CurrentRow.Cells[3].Value.ToString()) > 0)
			{
				int num2 = int.Parse(infolist.CurrentRow.Cells[3].Value.ToString()) - 1;
				if (num2 > 0)
				{
					infolist.CurrentRow.Cells[3].Value = num2.ToString();
				}
				else
				{
					infolist.Rows.RemoveAt(infolist.CurrentRow.Index);
				}
				computetotalmoney();
				alertMsg.Text = "數量減1";
				action = true;
			}
		}

		private void num_Down(object sender, MouseEventArgs e)
		{
			if (action)
			{
				alertMsg.Text = "";
				action = false;
			}
			if (lastFocused.Equals(tb_PurchaseCustomNo))
			{
				tb_PurchaseCustomNo.Text += (sender as Button).Text;
				tb_PurchaseCustomNo.Focus();
				tb_PurchaseCustomNo.SelectionStart = tb_PurchaseCustomNo.Text.Length;
			}
			else if (lastFocused.Equals(tb_GDSNO))
			{
				if ("請刷商品條碼或輸入條碼".Equals(tb_GDSNO.Text))
				{
					tb_GDSNO.Text = "";
				}
				tb_GDSNO.Text += (sender as Button).Text;
				tb_GDSNO.Focus();
				tb_GDSNO.SelectionStart = tb_GDSNO.Text.Length;
			}
		}

		private void pressEnter_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("此進貨單尚未儲存，確定放棄？\n點選確定放棄儲存進貨單、點選取消繼續新增此進貨單", "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
			{
				AutoClosingMessageBox.Show("放棄儲存");
				switchForm(new frmMain());
			}
		}

		private void backspace_Click(object sender, EventArgs e)
		{
			if (lastFocused.Equals(tb_PurchaseCustomNo) && tb_PurchaseCustomNo.Text.Length > 0)
			{
				tb_PurchaseCustomNo.Text = tb_PurchaseCustomNo.Text.Remove(tb_PurchaseCustomNo.Text.Length - 1);
				tb_PurchaseCustomNo.Focus();
				tb_PurchaseCustomNo.SelectionStart = tb_PurchaseCustomNo.Text.Length;
			}
			else if (lastFocused.Equals(tb_GDSNO) && tb_GDSNO.Text.Length > 0)
			{
				tb_GDSNO.Text = tb_GDSNO.Text.Remove(tb_GDSNO.Text.Length - 1);
				tb_GDSNO.Focus();
				tb_GDSNO.SelectionStart = tb_GDSNO.Text.Length;
			}
		}

		private void pre_Click(object sender, EventArgs e)
		{
			if (infolist.CurrentRow == null)
			{
				AutoClosingMessageBox.Show("請選擇商品");
			}
			else if (infolist.CurrentRow.Index > 0)
			{
				int index = infolist.CurrentRow.Index;
				infolist.CurrentCell = infolist.Rows[index - 1].Cells[0];
				infolist_SelectionChanged(sender, e);
			}
			else
			{
				AutoClosingMessageBox.Show("已經是第一筆商品");
			}
		}

		private void next_Click(object sender, EventArgs e)
		{
			if (infolist.CurrentRow == null)
			{
				AutoClosingMessageBox.Show("請選擇商品");
				return;
			}
			try
			{
				if (infolist.CurrentRow.Index < infolist.Rows.Count - 1)
				{
					int index = infolist.CurrentRow.Index;
					infolist.CurrentCell = infolist.Rows[index + 1].Cells[0];
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

		private void addone_Click(object sender, EventArgs e)
		{
			if (infolist.CurrentRow == null)
			{
				AutoClosingMessageBox.Show("請選擇商品");
				return;
			}
			action = true;
			int num = int.Parse(infolist.CurrentRow.Cells[3].Value.ToString()) + 1;
			infolist.CurrentRow.Cells[3].Value = num.ToString();
			computetotalmoney();
			alertMsg.Text = "數量加1";
		}

		private void subone_Click(object sender, EventArgs e)
		{
			if (infolist.CurrentRow == null)
			{
				AutoClosingMessageBox.Show("請選擇商品");
			}
			else if (int.Parse(infolist.CurrentRow.Cells[3].Value.ToString()) > 0)
			{
				action = true;
				int num = int.Parse(infolist.CurrentRow.Cells[3].Value.ToString()) - 1;
				if (num > 0)
				{
					infolist.CurrentRow.Cells[3].Value = num.ToString();
				}
				else
				{
					infolist.Rows.RemoveAt(infolist.CurrentRow.Index);
				}
				computetotalmoney();
				alertMsg.Text = "數量減1";
			}
		}

		private void oneremove_Click(object sender, EventArgs e)
		{
			if (infolist.CurrentRow == null)
			{
				AutoClosingMessageBox.Show("請選擇商品");
				return;
			}
			action = true;
			infolist.Rows.RemoveAt(infolist.CurrentRow.Index);
			computetotalmoney();
			alertMsg.Text = "移除選擇商品";
		}

		private void removeall_Click(object sender, EventArgs e)
		{
			if (infolist.RowCount == 0)
			{
				AutoClosingMessageBox.Show("已無商品");
				return;
			}
			action = true;
			infolist.Rows.Clear();
			infolist.Refresh();
			computetotalmoney();
			alertMsg.Text = "移除全部商品";
		}

		private void computetotalmoney()
		{
			int num = 0;
			foreach (DataGridViewRow item in (IEnumerable)infolist.Rows)
			{
				num += int.Parse(item.Cells["subTotal"].Value.ToString());
			}
			l_totalprice.Text = num.ToString();
			setfocus();
		}

		private void clearenter_Click(object sender, EventArgs e)
		{
			if (lastFocused.Equals(tb_GDSNO))
			{
				tb_GDSNO.Text = "";
				tb_GDSNO.Focus();
				tb_GDSNO.SelectionStart = tb_GDSNO.Text.Length;
			}
			if (lastFocused.Equals(tb_PurchaseCustomNo))
			{
				tb_PurchaseCustomNo.Text = "";
				tb_PurchaseCustomNo.Focus();
				tb_PurchaseCustomNo.SelectionStart = tb_PurchaseCustomNo.Text.Length;
			}
			alertMsg.Text = "";
		}

		private void commoditySearch_Click(object sender, EventArgs e)
		{
			frmCommoditySearch frmCommoditySearch = new frmCommoditySearch(this);
			frmCommoditySearch.Location = new Point(base.Location.X, base.Location.Y);
			frmCommoditySearch.Show();
			Hide();
		}

		private void Checkout_Click(object sender, EventArgs e)
		{
			string text = "";
			List<string> list = new List<string>();
			foreach (DataGridViewRow item in (IEnumerable)infolist.Rows)
			{
				string hiddenGDSNO = (item.Cells[1].Value as CommodityInfo).getHiddenGDSNO();
				string hiddenBatchNo = (item.Cells[1].Value as CommodityInfo).getHiddenBatchNo();
				if (!"".Equals(hiddenBatchNo))
				{
					list.Add(hiddenGDSNO + "," + hiddenBatchNo);
				}
			}
			int count = list.Count;
			list = Enumerable.ToList(Enumerable.Distinct(list));
			int count2 = list.Count;
			if (count != count2)
			{
				text += "同一進貨單商品批號重複設定，請移除重複項目\n";
			}
			if (string.IsNullOrEmpty(supplierNo))
			{
				text += "請選擇進貨廠商\n";
			}
			if (infolist.Rows.Count == 0)
			{
				text += "請選擇進貨商品\n";
			}
			if (!string.IsNullOrEmpty(text))
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			string text2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string[,] strFieldArray = new string[8, 2]
			{
				{
					"PurchaseNo",
					newPurchaseNo
				},
				{
					"PurchaseCustomNo",
					tb_PurchaseCustomNo.Text
				},
				{
					"SupplierNo",
					supplierNo
				},
				{
					"Total",
					l_totalprice.Text
				},
				{
					"Status",
					"0"
				},
				{
					"PurchaseDate",
					dt_PurchaseDate.Text
				},
				{
					"CreateDate",
					text2
				},
				{
					"UpdateDate",
					text2
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_PurchaseGoods_Master", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			foreach (DataGridViewRow item2 in (IEnumerable)infolist.Rows)
			{
				string hiddenGDSNO2 = (item2.Cells[1].Value as CommodityInfo).getHiddenGDSNO();
				string batchNo = getBatchNo();
				string text3 = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT inventory FROM hypos_GOODSLST where GDSNO = {0}", new string[1]
				{
					hiddenGDSNO2
				}, CommandOperationType.ExecuteScalar).ToString();
				text3 = (string.IsNullOrEmpty(text3) ? "0" : text3);
				int num = int.Parse(text3) + int.Parse(item2.Cells[3].Value.ToString());
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_GOODSLST set inventory = {1} WHERE GDSNO = {0}", new string[2]
				{
					hiddenGDSNO2,
					num.ToString()
				}, CommandOperationType.ExecuteNonQuery);
				strFieldArray = new string[8, 2]
				{
					{
						"PurchaseNo",
						newPurchaseNo
					},
					{
						"GDSNO",
						hiddenGDSNO2
					},
					{
						"Cost",
						item2.Cells[2].Value.ToString()
					},
					{
						"Quantity",
						item2.Cells[3].Value.ToString()
					},
					{
						"GoodsTotalCountLog",
						num.ToString()
					},
					{
						"BatchNo",
						(item2.Cells[1].Value as CommodityInfo).getHiddenBatchNo()
					},
					{
						"MFGDate",
						(item2.Cells[1].Value as CommodityInfo).getHiddenMFGDate()
					},
					{
						"POSBatchNo",
						batchNo
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_PurchaseGoods_Detail", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray = new string[6, 2]
				{
					{
						"POSBatchNo",
						batchNo
					},
					{
						"BatchNo",
						(item2.Cells[1].Value as CommodityInfo).getHiddenBatchNo()
					},
					{
						"MFGDate",
						(item2.Cells[1].Value as CommodityInfo).getHiddenMFGDate()
					},
					{
						"barcode",
						hiddenGDSNO2
					},
					{
						"num",
						item2.Cells[3].Value.ToString()
					},
					{
						"PurchaseNo",
						newPurchaseNo
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_PurchaseGoodsBatchNo_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray = new string[5, 2]
				{
					{
						"POSBatchNo",
						batchNo
					},
					{
						"barcode",
						hiddenGDSNO2
					},
					{
						"num",
						"0"
					},
					{
						"backlogQuantity",
						item2.Cells[3].Value.ToString()
					},
					{
						"createDate",
						text2
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_BatchNo_log", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			}
			AutoClosingMessageBox.Show("新增成功");
			switchForm(new frmInventoryMangement());
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

		private void btn_enterPrice_Click(object sender, EventArgs e)
		{
			if (infolist.CurrentRow == null)
			{
				AutoClosingMessageBox.Show("請先選擇商品");
			}
			else
			{
				int result = 0;
				int.TryParse(tb_GDSNO.Text, out result);
				tb_cost.Text = result.ToString();
				tb_subtotal.Text = (result * int.Parse(infolist.CurrentRow.Cells[3].Value.ToString())).ToString();
				infolist.CurrentRow.Cells[2].Value = result;
				infolist.CurrentRow.Cells[4].Value = result * int.Parse(infolist.CurrentRow.Cells[3].Value.ToString());
				computetotalmoney();
				action = true;
				alertMsg.Text = "變更進貨價格";
				tb_GDSNO.Text = "";
			}
			tb_GDSNO.Focus();
			tb_GDSNO.SelectionStart = tb_GDSNO.Text.Length;
		}

		private void btn_enterCount_Click(object sender, EventArgs e)
		{
			if (infolist.CurrentRow == null)
			{
				AutoClosingMessageBox.Show("請先選擇商品");
			}
			else
			{
				int result = 0;
				int.TryParse(tb_GDSNO.Text, out result);
				tb_quantity.Text = result.ToString();
				tb_subtotal.Text = (result * int.Parse(infolist.CurrentRow.Cells[2].Value.ToString())).ToString();
				infolist.CurrentRow.Cells[3].Value = result;
				infolist.CurrentRow.Cells[4].Value = result * int.Parse(infolist.CurrentRow.Cells[2].Value.ToString());
				computetotalmoney();
				action = true;
				alertMsg.Text = "變更進貨數量";
				tb_GDSNO.Text = "";
			}
			tb_GDSNO.Focus();
			tb_GDSNO.SelectionStart = tb_GDSNO.Text.Length;
		}

		private void digitOnly_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b') && !char.IsUpper(e.KeyChar) && !char.IsLower(e.KeyChar));
		}

		private void tb_GDSNO_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_GDSNO.Text))
			{
				tb_GDSNO.Text = "請刷商品條碼或輸入條碼";
			}
			isFocusA = false;
		}

		private void tb_GDSNO_Enter(object sender, EventArgs e)
		{
			if ("請刷商品條碼或輸入條碼".Equals(tb_GDSNO.Text))
			{
				tb_GDSNO.Text = "";
			}
			isFocusA = true;
		}

		private void tb_GDSNO_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				string strTableName = "hypos_GOODSLST as hg left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo";
				string strWhereClause = "hg.GDSNO ={0} AND ((hg.ISWS ='Y' and hg.CLA1NO ='0302' and hg.licType = hl.licType and hg.domManufId = hl.licNo) OR (hg.ISWS ='N' and hg.CLA1NO ='0302') OR hg.CLA1NO ='0303' OR hg.CLA1NO ='0305' OR hg.CLA1NO ='0308') AND (hl.isDelete='N' or hl.isDelete is null) ";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hg.inventory,hg.GDSNO,hg.spec,hg.capacity,hg.GDName,hg.formCode,hg.CName,hg.contents,hg.brandName,hg.CLA1NO,hg.ISWS", strTableName, strWhereClause, "", null, new string[1]
				{
					tb_GDSNO.Text
				}, CommandOperationType.ExecuteReaderReturnDataTable);
				DialogResult dialogResult = DialogResult.None;
				if (dataTable.Rows.Count > 0 && "0302".Equals(dataTable.Rows[0]["CLA1NO"].ToString()) && "Y".Equals(dataTable.Rows[0]["ISWS"].ToString()))
				{
					dialogResult = new dialogSetBatchNoAndMFGdate(this, tb_GDSNO.Text).ShowDialog();
				}
				else
				{
					addNewCommodity(sender, e);
				}
				if (dialogResult == DialogResult.Yes)
				{
					addNewCommodity(sender, e);
				}
				else
				{
					tb_GDSNO.Text = "";
				}
			}
		}

		private void addNewCommodity(object sender, EventArgs e)
		{
			string strTableName = "hypos_GOODSLST as hg left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo";
			string strWhereClause = "hg.GDSNO ={0} AND ((hg.ISWS ='Y' and hg.CLA1NO ='0302' and hg.licType = hl.licType and hg.domManufId = hl.licNo) OR (hg.ISWS ='N' and hg.CLA1NO ='0302') OR hg.CLA1NO ='0303' OR hg.CLA1NO ='0305' OR hg.CLA1NO ='0308') AND (hl.isDelete='N' or hl.isDelete is null) ";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hg.inventory,hg.GDSNO,hg.spec,hg.capacity,hg.GDName,hg.formCode,hg.CName,hg.contents,hg.brandName,hg.CLA1NO,hg.ISWS", strTableName, strWhereClause, "", null, new string[1]
			{
				tb_GDSNO.Text
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				bool flag = true;
				string text = "購肥商品暫時不開放";
				if ("0303".Equals(dataTable.Rows[0]["CLA1NO"].ToString()) && "Y".Equals(dataTable.Rows[0]["ISWS"].ToString()))
				{
					flag = Program.IsFertilizer;
				}
				if (flag)
				{
					alertMsg.Text = "選入商品";
					foreach (DataRow row in dataTable.Rows)
					{
						string text2 = string.IsNullOrEmpty(row["inventory"].ToString()) ? "0" : row["inventory"].ToString();
						uc = new CommodityInfo();
						uc.setMemberIdNo("");
						uc.setHiddenGDSNO(row["GDSNO"].ToString());
						uc.setMemberVipNo("店內碼:" + row["GDSNO"].ToString());
						uc.setCommodityName(setCommodityName(row) + " " + row["spec"].ToString() + " " + row["capacity"].ToString());
						if (InfoSetting[0].Equals("") && InfoSetting[1].Equals(""))
						{
							uc.setCommodityClass("");
						}
						else
						{
							uc.setCommodityClass("批號:" + InfoSetting[0].PadRight(20, ' ') + " 製造日期:" + InfoSetting[1]);
						}
						uc.setHiddenBatchNo(InfoSetting[0]);
						uc.setHiddenMFGDate(InfoSetting[1]);
						ResetinfolistInfoSetting();
						uc.setlabe1("");
						uc.BackColor = Color.FromArgb(255, 208, 81);
						infolist.Rows.Add(0, uc, "0", "1", "0", text2);
						infolist.CurrentCell = infolist.Rows[infolist.RowCount - 1].Cells[0];
						infolist_SelectionChanged(sender, e);
					}
					foreach (DataGridViewRow item in (IEnumerable)infolist.Rows)
					{
						item.Height = 100;
					}
					computetotalmoney();
				}
				else
				{
					AutoClosingMessageBox.Show(text);
				}
			}
			else
			{
				alertMsg.Text = "商品不存在，請重新輸入";
				AutoClosingMessageBox.Show(alertMsg.Text);
			}
			tb_GDSNO.Text = "";
			action = true;
		}

		public void addOnecommodity(object sender, EventArgs e, string barcode)
		{
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "inventory,GDSNO,spec,capacity,GDName,formCode,CName,contents,brandName,CLA1NO,ISWS", "hypos_GOODSLST", "GDSNO = {0} ", "", null, new string[1]
			{
				barcode
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				bool flag = true;
				string text = "購肥商品暫時不開放";
				if ("0303".Equals(dataTable.Rows[0]["CLA1NO"].ToString()) && "Y".Equals(dataTable.Rows[0]["ISWS"].ToString()))
				{
					flag = Program.IsFertilizer;
				}
				if (flag)
				{
					alertMsg.Text = "選入商品";
					foreach (DataRow row in dataTable.Rows)
					{
						string text2 = string.IsNullOrEmpty(row["inventory"].ToString()) ? "0" : row["inventory"].ToString();
						uc = new CommodityInfo();
						uc.setMemberIdNo("");
						uc.setHiddenGDSNO(row["GDSNO"].ToString());
						uc.setMemberVipNo("店內碼:" + row["GDSNO"].ToString());
						uc.setCommodityName(setCommodityName(row) + " " + row["spec"].ToString() + " " + row["capacity"].ToString());
						if (InfoSetting[0].Equals("") && InfoSetting[1].Equals(""))
						{
							uc.setCommodityClass("");
						}
						else
						{
							uc.setCommodityClass("批號:" + InfoSetting[0].PadRight(20, ' ') + " 製造日期:" + InfoSetting[1]);
						}
						uc.setHiddenBatchNo(InfoSetting[0]);
						uc.setHiddenMFGDate(InfoSetting[1]);
						ResetinfolistInfoSetting();
						uc.setlabe1("");
						uc.BackColor = Color.FromArgb(255, 208, 81);
						infolist.Rows.Add(0, uc, "0", "1", "0", text2);
						infolist.CurrentCell = infolist.Rows[infolist.RowCount - 1].Cells[0];
						infolist_SelectionChanged(sender, e);
					}
					foreach (DataGridViewRow item in (IEnumerable)infolist.Rows)
					{
						item.Height = 100;
					}
					computetotalmoney();
				}
				else
				{
					setfocus();
					AutoClosingMessageBox.Show(text);
				}
			}
			else
			{
				alertMsg.Text = "商品不存在，請重新輸入";
				AutoClosingMessageBox.Show(alertMsg.Text);
			}
			tb_GDSNO.Text = "";
			action = true;
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

		private void infolist_SelectionChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < infolist.RowCount; i++)
			{
				(infolist[1, i].Value as CommodityInfo).BackColor = Color.White;
			}
			tb_cost.Text = "";
			tb_quantity.Text = "";
			tb_subtotal.Text = "";
			if (infolist.CurrentRow != null)
			{
				tb_cost.Text = infolist.CurrentRow.Cells[2].Value.ToString();
				tb_quantity.Text = infolist.CurrentRow.Cells[3].Value.ToString();
				tb_subtotal.Text = (int.Parse(tb_quantity.Text) * int.Parse(tb_cost.Text)).ToString();
				(infolist.CurrentRow.Cells[1].Value as CommodityInfo).BackColor = Color.FromArgb(255, 208, 81);
				infolist.Refresh();
			}
		}

		private void infolist_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			infolist.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
		}

		private void tb_PurchaseCustomNo_Leave(object sender, EventArgs e)
		{
			isFocusB = false;
		}

		private void tb_PurchaseCustomNo_Enter(object sender, EventArgs e)
		{
			isFocusB = true;
		}

		public void setfocus()
		{
			tb_GDSNO.Select();
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
			tb_PurchaseCustomNo = new System.Windows.Forms.TextBox();
			tb_quantity = new System.Windows.Forms.TextBox();
			l_sysTime = new System.Windows.Forms.Label();
			infolist = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			commodityName = new POS_Client.frmMainShopSimple.CustomColumn();
			cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
			quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			subTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_inventory = new System.Windows.Forms.DataGridViewTextBoxColumn();
			addone = new System.Windows.Forms.Button();
			subone = new System.Windows.Forms.Button();
			numone = new System.Windows.Forms.Button();
			pressEnter = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			dt_PurchaseDate = new System.Windows.Forms.DateTimePicker();
			l_totalprice = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			btn_chooseSupplier = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			tb_subtotal = new System.Windows.Forms.TextBox();
			tb_cost = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			tb_supplierName = new System.Windows.Forms.TextBox();
			alertMsg = new System.Windows.Forms.TextBox();
			tb_GDSNO = new System.Windows.Forms.TextBox();
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
			btn_enterPrice = new System.Windows.Forms.Button();
			btn_enterCount = new System.Windows.Forms.Button();
			Checkout = new System.Windows.Forms.Button();
			btn_commoditySearch = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			((System.ComponentModel.ISupportInitialize)infolist).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			pb_virtualKeyBoard.Location = new System.Drawing.Point(898, 620);
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 7);
			tb_PurchaseCustomNo.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			tb_PurchaseCustomNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_PurchaseCustomNo.Location = new System.Drawing.Point(100, 69);
			tb_PurchaseCustomNo.MaxLength = 20;
			tb_PurchaseCustomNo.Multiline = true;
			tb_PurchaseCustomNo.Name = "tb_PurchaseCustomNo";
			tb_PurchaseCustomNo.Size = new System.Drawing.Size(165, 33);
			tb_PurchaseCustomNo.TabIndex = 2;
			tb_PurchaseCustomNo.Enter += new System.EventHandler(tb_PurchaseCustomNo_Enter);
			tb_PurchaseCustomNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(digitOnly_KeyPress);
			tb_PurchaseCustomNo.Leave += new System.EventHandler(tb_PurchaseCustomNo_Leave);
			tb_quantity.BackColor = System.Drawing.SystemColors.Control;
			tb_quantity.Enabled = false;
			tb_quantity.Font = new System.Drawing.Font("Calibri", 15.75f);
			tb_quantity.Location = new System.Drawing.Point(509, 62);
			tb_quantity.Multiline = true;
			tb_quantity.Name = "tb_quantity";
			tb_quantity.Size = new System.Drawing.Size(70, 40);
			tb_quantity.TabIndex = 3;
			l_sysTime.AutoSize = true;
			l_sysTime.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_sysTime.Location = new System.Drawing.Point(473, 70);
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
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(157, 157, 157);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			infolist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			infolist.Columns.AddRange(Column1, commodityName, cost, quantity, subTotal, hidden_inventory);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			infolist.DefaultCellStyle = dataGridViewCellStyle2;
			infolist.EnableHeadersVisualStyles = false;
			infolist.GridColor = System.Drawing.SystemColors.ActiveBorder;
			infolist.Location = new System.Drawing.Point(21, 222);
			infolist.MultiSelect = false;
			infolist.Name = "infolist";
			infolist.ReadOnly = true;
			infolist.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			infolist.RowHeadersVisible = false;
			infolist.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			infolist.RowTemplate.Height = 102;
			infolist.RowTemplate.ReadOnly = true;
			infolist.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			infolist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			infolist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			infolist.Size = new System.Drawing.Size(754, 433);
			infolist.TabIndex = 9;
			infolist.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(infolist_RowPostPaint);
			infolist.SelectionChanged += new System.EventHandler(infolist_SelectionChanged);
			Column1.HeaderText = "項次";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 62;
			commodityName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			commodityName.HeaderText = "商品名稱";
			commodityName.Name = "commodityName";
			commodityName.ReadOnly = true;
			commodityName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			cost.DefaultCellStyle = dataGridViewCellStyle4;
			cost.HeaderText = "進貨價";
			cost.Name = "cost";
			cost.ReadOnly = true;
			cost.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			cost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			cost.Width = 76;
			quantity.HeaderText = "進貨數量";
			quantity.Name = "quantity";
			quantity.ReadOnly = true;
			quantity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			quantity.Width = 120;
			subTotal.HeaderText = "小計";
			subTotal.Name = "subTotal";
			subTotal.ReadOnly = true;
			subTotal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			subTotal.Width = 76;
			hidden_inventory.HeaderText = "(隱藏_調整前庫存)";
			hidden_inventory.Name = "hidden_inventory";
			hidden_inventory.ReadOnly = true;
			hidden_inventory.Visible = false;
			addone.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			addone.Location = new System.Drawing.Point(790, 325);
			addone.Name = "addone";
			addone.Size = new System.Drawing.Size(40, 40);
			addone.TabIndex = 10;
			addone.Text = "+1";
			addone.UseVisualStyleBackColor = true;
			addone.Click += new System.EventHandler(addone_Click);
			subone.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			subone.Location = new System.Drawing.Point(790, 372);
			subone.Name = "subone";
			subone.Size = new System.Drawing.Size(40, 40);
			subone.TabIndex = 11;
			subone.Text = "-1";
			subone.UseVisualStyleBackColor = true;
			subone.Click += new System.EventHandler(subone_Click);
			numone.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numone.Location = new System.Drawing.Point(837, 419);
			numone.Name = "numone";
			numone.Size = new System.Drawing.Size(40, 40);
			numone.TabIndex = 12;
			numone.Text = "1";
			numone.UseVisualStyleBackColor = true;
			numone.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			pressEnter.BackColor = System.Drawing.Color.FromArgb(167, 202, 0);
			pressEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			pressEnter.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			pressEnter.ForeColor = System.Drawing.Color.White;
			pressEnter.Location = new System.Drawing.Point(885, 50);
			pressEnter.Name = "pressEnter";
			pressEnter.Size = new System.Drawing.Size(88, 77);
			pressEnter.TabIndex = 13;
			pressEnter.Text = "結束\r\n作業";
			pressEnter.UseVisualStyleBackColor = false;
			pressEnter.Click += new System.EventHandler(pressEnter_Click);
			panel1.BackColor = System.Drawing.Color.White;
			panel1.Controls.Add(dt_PurchaseDate);
			panel1.Controls.Add(tb_PurchaseCustomNo);
			panel1.Controls.Add(l_totalprice);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(btn_chooseSupplier);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(tb_subtotal);
			panel1.Controls.Add(tb_cost);
			panel1.Controls.Add(tb_quantity);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(l_sysTime);
			panel1.Controls.Add(tb_supplierName);
			panel1.Controls.Add(alertMsg);
			panel1.Controls.Add(tb_GDSNO);
			panel1.Location = new System.Drawing.Point(23, 50);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(752, 166);
			panel1.TabIndex = 14;
			dt_PurchaseDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dt_PurchaseDate.CalendarFont = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dt_PurchaseDate.Checked = false;
			dt_PurchaseDate.CustomFormat = "yyyy-MM-dd";
			dt_PurchaseDate.Font = new System.Drawing.Font("微軟正黑體", 14.25f);
			dt_PurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dt_PurchaseDate.Location = new System.Drawing.Point(100, 115);
			dt_PurchaseDate.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			dt_PurchaseDate.Name = "dt_PurchaseDate";
			dt_PurchaseDate.Size = new System.Drawing.Size(165, 33);
			dt_PurchaseDate.TabIndex = 54;
			dt_PurchaseDate.Value = new System.DateTime(2016, 11, 24, 15, 23, 29, 0);
			l_totalprice.AutoSize = true;
			l_totalprice.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_totalprice.ForeColor = System.Drawing.Color.Red;
			l_totalprice.Location = new System.Drawing.Point(282, 107);
			l_totalprice.Name = "l_totalprice";
			l_totalprice.Size = new System.Drawing.Size(31, 34);
			l_totalprice.TabIndex = 53;
			l_totalprice.Text = "0";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label6.Location = new System.Drawing.Point(17, 121);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(89, 20);
			label6.TabIndex = 53;
			label6.Text = "進貨日期：";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label5.Location = new System.Drawing.Point(17, 73);
			label5.Margin = new System.Windows.Forms.Padding(0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(89, 20);
			label5.TabIndex = 53;
			label5.Text = "進貨單號：";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label2.Location = new System.Drawing.Point(284, 72);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(73, 20);
			label2.TabIndex = 53;
			label2.Text = "單據總價";
			btn_chooseSupplier.BackColor = System.Drawing.Color.White;
			btn_chooseSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_chooseSupplier.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_chooseSupplier.ForeColor = System.Drawing.Color.Black;
			btn_chooseSupplier.Image = POS_Client.Properties.Resources.ic_toc_black_24dp_1x;
			btn_chooseSupplier.Location = new System.Drawing.Point(283, 15);
			btn_chooseSupplier.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
			btn_chooseSupplier.Name = "btn_chooseSupplier";
			btn_chooseSupplier.Size = new System.Drawing.Size(54, 32);
			btn_chooseSupplier.TabIndex = 52;
			btn_chooseSupplier.UseVisualStyleBackColor = false;
			btn_chooseSupplier.Click += new System.EventHandler(btn_chooseSupplier_Click);
			label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			label3.Location = new System.Drawing.Point(369, 0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(2, 191);
			label3.TabIndex = 12;
			label3.Text = "label3";
			label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			label1.Location = new System.Drawing.Point(275, 72);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(1, 90);
			label1.TabIndex = 11;
			tb_subtotal.BackColor = System.Drawing.SystemColors.Control;
			tb_subtotal.Enabled = false;
			tb_subtotal.Font = new System.Drawing.Font("Calibri", 15.75f);
			tb_subtotal.Location = new System.Drawing.Point(616, 60);
			tb_subtotal.Multiline = true;
			tb_subtotal.Name = "tb_subtotal";
			tb_subtotal.Size = new System.Drawing.Size(119, 40);
			tb_subtotal.TabIndex = 3;
			tb_cost.BackColor = System.Drawing.SystemColors.Control;
			tb_cost.Enabled = false;
			tb_cost.Font = new System.Drawing.Font("Calibri", 15.75f);
			tb_cost.Location = new System.Drawing.Point(397, 63);
			tb_cost.Multiline = true;
			tb_cost.Name = "tb_cost";
			tb_cost.Size = new System.Drawing.Size(70, 40);
			tb_cost.TabIndex = 3;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label4.Location = new System.Drawing.Point(585, 70);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(25, 20);
			label4.TabIndex = 5;
			label4.Text = "＝";
			tb_supplierName.Font = new System.Drawing.Font("微軟正黑體", 12.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_supplierName.Location = new System.Drawing.Point(17, 17);
			tb_supplierName.Multiline = true;
			tb_supplierName.Name = "tb_supplierName";
			tb_supplierName.ReadOnly = true;
			tb_supplierName.Size = new System.Drawing.Size(254, 28);
			tb_supplierName.TabIndex = 6;
			tb_supplierName.Text = "請選擇供應商";
			tb_supplierName.Click += new System.EventHandler(btn_chooseSupplier_Click);
			alertMsg.Font = new System.Drawing.Font("微軟正黑體", 15.75f);
			alertMsg.Location = new System.Drawing.Point(397, 120);
			alertMsg.Multiline = true;
			alertMsg.Name = "alertMsg";
			alertMsg.ReadOnly = true;
			alertMsg.Size = new System.Drawing.Size(339, 28);
			alertMsg.TabIndex = 6;
			tb_GDSNO.Font = new System.Drawing.Font("微軟正黑體", 12.75f);
			tb_GDSNO.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_GDSNO.Location = new System.Drawing.Point(397, 14);
			tb_GDSNO.Name = "tb_GDSNO";
			tb_GDSNO.Size = new System.Drawing.Size(339, 30);
			tb_GDSNO.TabIndex = 2;
			tb_GDSNO.Text = "請刷商品條碼或輸入條碼";
			tb_GDSNO.Enter += new System.EventHandler(tb_GDSNO_Enter);
			tb_GDSNO.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_GDSNO_KeyDown);
			tb_GDSNO.Leave += new System.EventHandler(tb_GDSNO_Leave);
			backspace.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			backspace.Location = new System.Drawing.Point(884, 466);
			backspace.Name = "backspace";
			backspace.Size = new System.Drawing.Size(88, 40);
			backspace.TabIndex = 15;
			backspace.Text = "backspace";
			backspace.UseVisualStyleBackColor = true;
			backspace.Click += new System.EventHandler(backspace_Click);
			pre.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			pre.Location = new System.Drawing.Point(790, 419);
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
			oneremove.Location = new System.Drawing.Point(789, 230);
			oneremove.Name = "oneremove";
			oneremove.Size = new System.Drawing.Size(88, 88);
			oneremove.TabIndex = 18;
			oneremove.Text = "單筆\r\n移除";
			oneremove.UseVisualStyleBackColor = false;
			oneremove.Click += new System.EventHandler(oneremove_Click);
			next.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			next.Location = new System.Drawing.Point(790, 466);
			next.Name = "next";
			next.Size = new System.Drawing.Size(40, 40);
			next.TabIndex = 22;
			next.Text = "↓";
			next.UseVisualStyleBackColor = true;
			next.Click += new System.EventHandler(next_Click);
			numzero.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numzero.Location = new System.Drawing.Point(837, 466);
			numzero.Name = "numzero";
			numzero.Size = new System.Drawing.Size(40, 40);
			numzero.TabIndex = 23;
			numzero.Text = "0";
			numzero.UseVisualStyleBackColor = true;
			numzero.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numtwo.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numtwo.Location = new System.Drawing.Point(884, 419);
			numtwo.Name = "numtwo";
			numtwo.Size = new System.Drawing.Size(40, 40);
			numtwo.TabIndex = 24;
			numtwo.Text = "2";
			numtwo.UseVisualStyleBackColor = true;
			numtwo.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numthree.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numthree.Location = new System.Drawing.Point(931, 419);
			numthree.Name = "numthree";
			numthree.Size = new System.Drawing.Size(40, 40);
			numthree.TabIndex = 25;
			numthree.Text = "3";
			numthree.UseVisualStyleBackColor = true;
			numthree.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numfour.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfour.Location = new System.Drawing.Point(837, 372);
			numfour.Name = "numfour";
			numfour.Size = new System.Drawing.Size(40, 40);
			numfour.TabIndex = 26;
			numfour.Text = "4";
			numfour.UseVisualStyleBackColor = true;
			numfour.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numsix.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numsix.Location = new System.Drawing.Point(931, 372);
			numsix.Name = "numsix";
			numsix.Size = new System.Drawing.Size(40, 40);
			numsix.TabIndex = 27;
			numsix.Text = "6";
			numsix.UseVisualStyleBackColor = true;
			numsix.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numfive.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numfive.Location = new System.Drawing.Point(884, 372);
			numfive.Name = "numfive";
			numfive.Size = new System.Drawing.Size(40, 40);
			numfive.TabIndex = 28;
			numfive.Text = "5";
			numfive.UseVisualStyleBackColor = true;
			numfive.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numseven.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numseven.Location = new System.Drawing.Point(837, 325);
			numseven.Name = "numseven";
			numseven.Size = new System.Drawing.Size(40, 40);
			numseven.TabIndex = 29;
			numseven.Text = "7";
			numseven.UseVisualStyleBackColor = true;
			numseven.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numeight.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numeight.Location = new System.Drawing.Point(884, 325);
			numeight.Name = "numeight";
			numeight.Size = new System.Drawing.Size(40, 40);
			numeight.TabIndex = 30;
			numeight.Text = "8";
			numeight.UseVisualStyleBackColor = true;
			numeight.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			numnine.Font = new System.Drawing.Font("Calibri", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			numnine.Location = new System.Drawing.Point(931, 325);
			numnine.Name = "numnine";
			numnine.Size = new System.Drawing.Size(40, 40);
			numnine.TabIndex = 31;
			numnine.Text = "9";
			numnine.UseVisualStyleBackColor = true;
			numnine.MouseDown += new System.Windows.Forms.MouseEventHandler(num_Down);
			removeall.BackColor = System.Drawing.Color.FromArgb(162, 162, 162);
			removeall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			removeall.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			removeall.ForeColor = System.Drawing.Color.White;
			removeall.Location = new System.Drawing.Point(885, 230);
			removeall.Name = "removeall";
			removeall.Size = new System.Drawing.Size(88, 88);
			removeall.TabIndex = 32;
			removeall.Text = "全部\r\n移除";
			removeall.UseVisualStyleBackColor = false;
			removeall.Click += new System.EventHandler(removeall_Click);
			clearenter.BackColor = System.Drawing.Color.FromArgb(192, 182, 154);
			clearenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			clearenter.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			clearenter.ForeColor = System.Drawing.Color.White;
			clearenter.Location = new System.Drawing.Point(789, 513);
			clearenter.Name = "clearenter";
			clearenter.Size = new System.Drawing.Size(184, 63);
			clearenter.TabIndex = 33;
			clearenter.Text = "清除\r\n輸入";
			clearenter.UseVisualStyleBackColor = false;
			clearenter.Click += new System.EventHandler(clearenter_Click);
			btn_enterPrice.BackColor = System.Drawing.Color.FromArgb(192, 137, 17);
			btn_enterPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_enterPrice.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_enterPrice.ForeColor = System.Drawing.Color.White;
			btn_enterPrice.Location = new System.Drawing.Point(790, 135);
			btn_enterPrice.Name = "btn_enterPrice";
			btn_enterPrice.Size = new System.Drawing.Size(88, 88);
			btn_enterPrice.TabIndex = 34;
			btn_enterPrice.Text = "進貨\r\n價格";
			btn_enterPrice.UseVisualStyleBackColor = false;
			btn_enterPrice.Click += new System.EventHandler(btn_enterPrice_Click);
			btn_enterCount.BackColor = System.Drawing.Color.FromArgb(192, 137, 17);
			btn_enterCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_enterCount.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_enterCount.ForeColor = System.Drawing.Color.White;
			btn_enterCount.Location = new System.Drawing.Point(885, 135);
			btn_enterCount.Name = "btn_enterCount";
			btn_enterCount.Size = new System.Drawing.Size(88, 88);
			btn_enterCount.TabIndex = 35;
			btn_enterCount.Text = "進貨\r\n數量";
			btn_enterCount.UseVisualStyleBackColor = false;
			btn_enterCount.Click += new System.EventHandler(btn_enterCount_Click);
			Checkout.BackColor = System.Drawing.Color.FromArgb(250, 87, 0);
			Checkout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			Checkout.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			Checkout.ForeColor = System.Drawing.Color.White;
			Checkout.Location = new System.Drawing.Point(790, 583);
			Checkout.Name = "Checkout";
			Checkout.Size = new System.Drawing.Size(183, 73);
			Checkout.TabIndex = 36;
			Checkout.Text = "編修\r\n存檔";
			Checkout.UseVisualStyleBackColor = false;
			Checkout.Click += new System.EventHandler(Checkout_Click);
			btn_commoditySearch.BackColor = System.Drawing.Color.FromArgb(45, 152, 165);
			btn_commoditySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_commoditySearch.Font = new System.Drawing.Font("微軟正黑體", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			btn_commoditySearch.ForeColor = System.Drawing.Color.White;
			btn_commoditySearch.Location = new System.Drawing.Point(789, 50);
			btn_commoditySearch.Name = "btn_commoditySearch";
			btn_commoditySearch.Size = new System.Drawing.Size(89, 78);
			btn_commoditySearch.TabIndex = 37;
			btn_commoditySearch.Text = "商品\r查詢";
			btn_commoditySearch.UseVisualStyleBackColor = false;
			btn_commoditySearch.Click += new System.EventHandler(commoditySearch_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(252, 252, 237);
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(981, 671);
			base.Controls.Add(Checkout);
			base.Controls.Add(btn_commoditySearch);
			base.Controls.Add(btn_enterCount);
			base.Controls.Add(btn_enterPrice);
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
			base.Name = "frmNewInventory";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "建立進貨單";
			base.Load += new System.EventHandler(frmNewInventory_Load);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(Form1_KeyDown);
			base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(frmNewInventory_KeyPress);
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
			base.Controls.SetChildIndex(btn_enterPrice, 0);
			base.Controls.SetChildIndex(btn_enterCount, 0);
			base.Controls.SetChildIndex(btn_commoditySearch, 0);
			base.Controls.SetChildIndex(Checkout, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			((System.ComponentModel.ISupportInitialize)infolist).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}
	}
}
