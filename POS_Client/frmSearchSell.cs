using POS_Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmSearchSell : MasterThinForm
	{
		private ucSellInfo[] ucsells;

		public DataTable dt;

		private IContainer components;

		private Button btn_cancel;

		private Button btn_enter;

		private Button btn_reset;

		private Panel panel5;

		private Label label10;

		private Panel panel3;

		private Label label6;

		private Panel panel6;

		private Label label12;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label1;

		private ucSellInfo uC_sellInfo1;

		private Panel panel1;

		private ucSellInfo uC_sellInfo2;

		private ucSellInfo uC_sellInfo3;

		private ucSellInfo uC_sellInfo6;

		private ucSellInfo uC_sellInfo5;

		private ucSellInfo uC_sellInfo4;

		private Panel panel4;

		private Label label3;

		private TextBox tb_MemberName;

		private TextBox sellNo;

		private Panel panel2;

		private DateTimePicker eDate;

		private DateTimePicker sDate;

		private Label label2;

		private TextBox tb_phone;

		private TextBox tb_barcode;

		private Panel panel7;

		private Label label4;

		private Panel panel8;

		public frmSearchSell()
			: base("銷售單|退貨|補印收據")
		{
			InitializeComponent();
			sDate.Value = DateTime.Today.AddDays(-30.0);
			eDate.Value = DateTime.Today;
			sellNo.Focus();
			ucsells = new ucSellInfo[6]
			{
				uC_sellInfo1,
				uC_sellInfo2,
				uC_sellInfo3,
				uC_sellInfo4,
				uC_sellInfo5,
				uC_sellInfo6
			};
			ucSellInfo[] array = ucsells;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnClickMember += new EventHandler(viewMemberInfo);
			}
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hms.sellNo,hms.sellTime,hms.memberId,hms.items,hcr.Name,hcr.Mobile ,hcr.IdNo,hcr.CompanyIdNo,hms.cash,hms.Credit,hms.sum ", "hypos_main_sell as hms left outer join hypos_CUST_RTL as hcr on hms.memberId= hcr.VipNo", "", "hms.editDate desc", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			int num = 0;
			for (int j = 0; j < 6; j++)
			{
				if (j < dt.Rows.Count)
				{
					if (!string.IsNullOrEmpty(dt.Rows[j]["Name"].ToString()))
					{
						ucsells[num].setMemberName(dt.Rows[j]["Name"].ToString());
					}
					else
					{
						ucsells[num].setMemberName("非會員");
					}
					ucsells[num].setsellNo(dt.Rows[j]["sellNo"].ToString());
					ucsells[num].setsellDate("銷售日期: " + dt.Rows[j]["sellTime"].ToString());
					ucsells[num].setcellphone(dt.Rows[j]["Mobile"].ToString());
					ucsells[num].setmemberNo("會員號: " + dt.Rows[j]["memberId"].ToString());
					ucsells[num].setitems("購買品項: " + dt.Rows[j]["items"].ToString());
					if (Program.SystemMode != 1)
					{
						ucsells[num].setSum("消費總額: " + dt.Rows[j]["sum"].ToString());
						ucsells[num].setPayType("付款模式: 現金(" + dt.Rows[j]["cash"].ToString() + ") / 賒帳(" + dt.Rows[j]["Credit"].ToString() + ")");
					}
					ucsells[num].Visible = true;
				}
				else
				{
					ucsells[num].Visible = false;
				}
				ucsells[num].BackColor = Color.White;
				num++;
			}
		}

		public void viewMemberInfo(object sellNumber, EventArgs s)
		{
			switchForm(new frmMainShopSimpleReturn(sellNumber.ToString(), "frmSearchSell", ""));
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			switchForm(new frmMain());
		}

		private void tb_sellNo_Enter(object sender, EventArgs e)
		{
			if (sellNo.Text == "請輸入銷售單號")
			{
				sellNo.Text = "";
			}
		}

		private void tb_sellNo_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(sellNo.Text))
			{
				sellNo.Text = "請輸入銷售單號";
			}
		}

		private void tb_MemberName_Enter(object sender, EventArgs e)
		{
			if (tb_MemberName.Text == "請輸入會員姓名")
			{
				tb_MemberName.Text = "";
			}
		}

		private void tb_MemberName_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_MemberName.Text))
			{
				tb_MemberName.Text = "請輸入會員姓名";
			}
		}

		private void btn_reset_Click(object sender, EventArgs e)
		{
			sellNo.Text = "請輸入銷售單號";
			tb_MemberName.Text = "請輸入會員姓名";
			sDate.Checked = false;
			eDate.Checked = false;
			sDate.Text = "";
			eDate.Text = "";
			tb_barcode.Text = "";
			sellNo.Focus();
		}

		private void tb_SellNo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btn_enter_Click(sender, e);
			}
		}

		private void tb_MemberName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btn_enter_Click(sender, e);
			}
		}

		private void btn_enter_Click(object sender, EventArgs e)
		{
			string text = "";
			if (sDate.Checked && eDate.Checked)
			{
				DateTime t = Convert.ToDateTime(sDate.Value);
				DateTime t2 = Convert.ToDateTime(eDate.Value);
				if (DateTime.Compare(t, t2) > 0)
				{
					text += "起日不可大於迄日，請重新設定\n";
				}
			}
			if (sellNo.Text == "請輸入銷售單號" && tb_MemberName.Text == "請輸入會員姓名" && tb_phone.Text == "請輸入會員電話" && !sDate.Checked && !eDate.Checked)
			{
				text += "必須輸入查詢條件\n";
			}
			if (!"".Equals(text))
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			if (sellNo.Text != "請輸入銷售單號")
			{
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_main_sell", "sellNo={0}", "", null, new string[1]
				{
					sellNo.Text
				}, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dt.Rows.Count > 0)
				{
					switchForm(new frmMainShopSimpleReturn(dt.Rows[0]["sellNo"].ToString(), "frmSearchSell", ""));
				}
				else
				{
					AutoClosingMessageBox.Show("銷售單不存在，請正確輸入銷售單編號");
				}
				return;
			}
			int num = 0;
			List<string> list = new List<string>();
			string text2 = "select distinct hms.sellNo,hms.sellTime,hms.memberId,hms.items,hcr.Name,hcr.Mobile ,hcr.IdNo,hcr.CompanyIdNo,hms.cash,hms.Credit,hms.sum FROM hypos_main_sell as hms left outer join hypos_CUST_RTL as hcr on hms.memberId= hcr.VipNo  join hypos_detail_sell on hms.sellNo = hypos_detail_sell.sellNo WHERE 1=1 ";
			if (tb_MemberName.Text != "請輸入會員姓名")
			{
				if ("非會員".Equals(tb_MemberName.Text))
				{
					text2 += " AND hcr.Name is null";
				}
				else
				{
					text2 = text2 + " AND hcr.Name like {" + num + "}";
					list.Add("%" + tb_MemberName.Text.ToString().Trim() + "%");
					num++;
				}
			}
			if (tb_phone.Text != "請輸入會員電話")
			{
				text2 = text2 + " AND (hcr.Telphone like {" + num + "} or hcr.Mobile like {" + num + "})";
				list.Add("%" + tb_phone.Text.ToString().Trim() + "%");
				num++;
			}
			if (tb_barcode.Text != "請輸入商品編號")
			{
				text2 = text2 + " and  hypos_detail_sell.barcode like {" + num + "}";
				list.Add("%" + tb_barcode.Text.ToString().Trim() + "%");
				num++;
			}
			if (sDate.Checked)
			{
				string str = sDate.Text.ToString();
				str += " 00:00:00";
				text2 = text2 + " AND hms.sellTime >= {" + num + "}";
				list.Add(str);
				num++;
			}
			if (eDate.Checked)
			{
				string str2 = eDate.Text.ToString();
				str2 += " 23:59:59";
				text2 = text2 + " AND hms.sellTime <= {" + num + "}";
				list.Add(str2);
				num++;
			}
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text2, list.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
			switchForm(new frmSearchSellResult(dataTable, "frmSearchSell"));
		}

		private void tb_phone_Enter(object sender, EventArgs e)
		{
			if (tb_phone.Text == "請輸入會員電話")
			{
				tb_phone.Text = "";
			}
		}

		private void tb_phone_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btn_enter_Click(sender, e);
			}
		}

		private void tb_phone_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_phone.Text))
			{
				tb_phone.Text = "請輸入會員電話";
			}
		}

		private void tb_barcode_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_barcode.Text))
			{
				tb_barcode.Text = "請輸入商品編號";
			}
		}

		private void tb_barcode_Enter(object sender, EventArgs e)
		{
			if (tb_barcode.Text == "請輸入商品編號")
			{
				tb_barcode.Text = "";
			}
		}

		private void tb_barcode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btn_enter_Click(sender, e);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS_Client.frmSearchSell));
			btn_cancel = new System.Windows.Forms.Button();
			btn_enter = new System.Windows.Forms.Button();
			btn_reset = new System.Windows.Forms.Button();
			panel5 = new System.Windows.Forms.Panel();
			label10 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			panel4 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			sellNo = new System.Windows.Forms.TextBox();
			panel2 = new System.Windows.Forms.Panel();
			eDate = new System.Windows.Forms.DateTimePicker();
			sDate = new System.Windows.Forms.DateTimePicker();
			label2 = new System.Windows.Forms.Label();
			panel8 = new System.Windows.Forms.Panel();
			tb_phone = new System.Windows.Forms.TextBox();
			tb_MemberName = new System.Windows.Forms.TextBox();
			panel7 = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			tb_barcode = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			uC_sellInfo3 = new POS_Client.ucSellInfo();
			panel1 = new System.Windows.Forms.Panel();
			uC_sellInfo6 = new POS_Client.ucSellInfo();
			uC_sellInfo5 = new POS_Client.ucSellInfo();
			uC_sellInfo4 = new POS_Client.ucSellInfo();
			uC_sellInfo2 = new POS_Client.ucSellInfo();
			uC_sellInfo1 = new POS_Client.ucSellInfo();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel5.SuspendLayout();
			panel3.SuspendLayout();
			panel6.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			panel4.SuspendLayout();
			panel2.SuspendLayout();
			panel8.SuspendLayout();
			panel7.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(553, 276);
			btn_cancel.Name = "btn_cancel";
			btn_cancel.Size = new System.Drawing.Size(75, 35);
			btn_cancel.TabIndex = 43;
			btn_cancel.TabStop = false;
			btn_cancel.Text = "取消";
			btn_cancel.UseVisualStyleBackColor = false;
			btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
			btn_enter.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_enter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_enter.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_enter.ForeColor = System.Drawing.Color.White;
			btn_enter.Location = new System.Drawing.Point(351, 276);
			btn_enter.Name = "btn_enter";
			btn_enter.Size = new System.Drawing.Size(75, 35);
			btn_enter.TabIndex = 1;
			btn_enter.TabStop = false;
			btn_enter.Text = "查詢";
			btn_enter.UseVisualStyleBackColor = false;
			btn_enter.Click += new System.EventHandler(btn_enter_Click);
			btn_reset.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_reset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_reset.ForeColor = System.Drawing.Color.White;
			btn_reset.Location = new System.Drawing.Point(452, 276);
			btn_reset.Name = "btn_reset";
			btn_reset.Size = new System.Drawing.Size(75, 35);
			btn_reset.TabIndex = 42;
			btn_reset.TabStop = false;
			btn_reset.Text = "重設";
			btn_reset.UseVisualStyleBackColor = false;
			btn_reset.Click += new System.EventHandler(btn_reset_Click);
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label10);
			panel5.Location = new System.Drawing.Point(1, 105);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(197, 51);
			panel5.TabIndex = 23;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(37, 15);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(74, 21);
			label10.TabIndex = 0;
			label10.Text = "會員姓名";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label6);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 1);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(197, 51);
			panel3.TabIndex = 21;
			label6.AutoSize = true;
			label6.BackColor = System.Drawing.Color.Transparent;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(38, 15);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(74, 21);
			label6.TabIndex = 0;
			label6.Text = "銷售編號";
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Location = new System.Drawing.Point(295, 0);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(113, 51);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(34, 17);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(74, 21);
			label12.TabIndex = 0;
			label12.Text = "會員電話";
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.63662f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.36338f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel1.Controls.Add(panel4, 0, 3);
			tableLayoutPanel1.Controls.Add(panel3, 0, 0);
			tableLayoutPanel1.Controls.Add(sellNo, 1, 0);
			tableLayoutPanel1.Controls.Add(panel2, 1, 3);
			tableLayoutPanel1.Controls.Add(panel8, 1, 2);
			tableLayoutPanel1.Controls.Add(panel5, 0, 2);
			tableLayoutPanel1.Controls.Add(panel7, 0, 1);
			tableLayoutPanel1.Controls.Add(tb_barcode, 1, 1);
			tableLayoutPanel1.Location = new System.Drawing.Point(42, 53);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 4;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
			tableLayoutPanel1.Size = new System.Drawing.Size(915, 212);
			tableLayoutPanel1.TabIndex = 40;
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label3);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Location = new System.Drawing.Point(1, 157);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(197, 54);
			panel4.TabIndex = 25;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.White;
			label3.Location = new System.Drawing.Point(35, 17);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(74, 21);
			label3.TabIndex = 0;
			label3.Text = "銷售日期";
			sellNo.BackColor = System.Drawing.Color.White;
			sellNo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			sellNo.ForeColor = System.Drawing.Color.DarkGray;
			sellNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
			sellNo.Location = new System.Drawing.Point(214, 16);
			sellNo.Margin = new System.Windows.Forms.Padding(15);
			sellNo.MaxLength = 0;
			sellNo.Name = "sellNo";
			sellNo.Size = new System.Drawing.Size(685, 29);
			sellNo.TabIndex = 1;
			sellNo.Text = "請輸入銷售單號";
			sellNo.Enter += new System.EventHandler(tb_sellNo_Enter);
			sellNo.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_SellNo_KeyDown);
			sellNo.Leave += new System.EventHandler(tb_sellNo_Leave);
			panel2.Controls.Add(eDate);
			panel2.Controls.Add(sDate);
			panel2.Controls.Add(label2);
			panel2.Location = new System.Drawing.Point(202, 160);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(545, 48);
			panel2.TabIndex = 24;
			eDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			eDate.CustomFormat = "yyyy-MM-dd";
			eDate.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			eDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			eDate.Location = new System.Drawing.Point(325, 4);
			eDate.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			eDate.Name = "eDate";
			eDate.ShowCheckBox = true;
			eDate.Size = new System.Drawing.Size(212, 34);
			eDate.TabIndex = 33;
			sDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			sDate.CustomFormat = "yyyy-MM-dd";
			sDate.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			sDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			sDate.Location = new System.Drawing.Point(10, 7);
			sDate.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			sDate.Name = "sDate";
			sDate.ShowCheckBox = true;
			sDate.Size = new System.Drawing.Size(273, 34);
			sDate.TabIndex = 32;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label2.Location = new System.Drawing.Point(291, 15);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(21, 20);
			label2.TabIndex = 1;
			label2.Text = "~";
			panel8.Controls.Add(panel6);
			panel8.Controls.Add(tb_phone);
			panel8.Controls.Add(tb_MemberName);
			panel8.Location = new System.Drawing.Point(202, 108);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(709, 45);
			panel8.TabIndex = 55;
			tb_phone.BackColor = System.Drawing.Color.White;
			tb_phone.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_phone.ForeColor = System.Drawing.Color.DarkGray;
			tb_phone.Location = new System.Drawing.Point(421, 9);
			tb_phone.Margin = new System.Windows.Forms.Padding(15);
			tb_phone.Name = "tb_phone";
			tb_phone.Size = new System.Drawing.Size(276, 29);
			tb_phone.TabIndex = 52;
			tb_phone.Text = "請輸入會員電話";
			tb_phone.Enter += new System.EventHandler(tb_phone_Enter);
			tb_phone.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_phone_KeyDown);
			tb_phone.Leave += new System.EventHandler(tb_phone_Leave);
			tb_MemberName.BackColor = System.Drawing.Color.White;
			tb_MemberName.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_MemberName.ForeColor = System.Drawing.Color.DarkGray;
			tb_MemberName.Location = new System.Drawing.Point(3, 9);
			tb_MemberName.Margin = new System.Windows.Forms.Padding(15);
			tb_MemberName.Name = "tb_MemberName";
			tb_MemberName.Size = new System.Drawing.Size(280, 29);
			tb_MemberName.TabIndex = 2;
			tb_MemberName.Text = "請輸入會員姓名";
			tb_MemberName.Enter += new System.EventHandler(tb_MemberName_Enter);
			tb_MemberName.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_MemberName_KeyDown);
			tb_MemberName.Leave += new System.EventHandler(tb_MemberName_Leave);
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(label4);
			panel7.Location = new System.Drawing.Point(1, 53);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(197, 51);
			panel7.TabIndex = 22;
			label4.AutoSize = true;
			label4.BackColor = System.Drawing.Color.Transparent;
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.White;
			label4.Location = new System.Drawing.Point(36, 18);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(74, 21);
			label4.TabIndex = 0;
			label4.Text = "商品編號";
			tb_barcode.BackColor = System.Drawing.Color.White;
			tb_barcode.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_barcode.ForeColor = System.Drawing.Color.DarkGray;
			tb_barcode.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_barcode.Location = new System.Drawing.Point(214, 68);
			tb_barcode.Margin = new System.Windows.Forms.Padding(15);
			tb_barcode.MaxLength = 0;
			tb_barcode.Name = "tb_barcode";
			tb_barcode.Size = new System.Drawing.Size(685, 29);
			tb_barcode.TabIndex = 54;
			tb_barcode.Text = "請輸入商品編號";
			tb_barcode.Enter += new System.EventHandler(tb_barcode_Enter);
			tb_barcode.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_barcode_KeyDown);
			tb_barcode.Leave += new System.EventHandler(tb_barcode_Leave);
			label1.Font = new System.Drawing.Font("微軟正黑體", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.Image = POS_Client.Properties.Resources.oblique;
			label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label1.Location = new System.Drawing.Point(88, 306);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(127, 23);
			label1.TabIndex = 46;
			label1.Text = "最近銷售單";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			uC_sellInfo3.AutoSize = true;
			uC_sellInfo3.BackColor = System.Drawing.Color.White;
			uC_sellInfo3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sellInfo3.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sellInfo3.Location = new System.Drawing.Point(3, 105);
			uC_sellInfo3.Margin = new System.Windows.Forms.Padding(0);
			uC_sellInfo3.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo3.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo3.Name = "uC_sellInfo3";
			uC_sellInfo3.Size = new System.Drawing.Size(398, 102);
			uC_sellInfo3.TabIndex = 3;
			panel1.Controls.Add(uC_sellInfo6);
			panel1.Controls.Add(uC_sellInfo5);
			panel1.Controls.Add(uC_sellInfo4);
			panel1.Controls.Add(uC_sellInfo3);
			panel1.Controls.Add(uC_sellInfo2);
			panel1.Controls.Add(uC_sellInfo1);
			panel1.Location = new System.Drawing.Point(90, 341);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(801, 312);
			panel1.TabIndex = 45;
			uC_sellInfo6.AutoSize = true;
			uC_sellInfo6.BackColor = System.Drawing.Color.White;
			uC_sellInfo6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sellInfo6.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sellInfo6.Location = new System.Drawing.Point(401, 207);
			uC_sellInfo6.Margin = new System.Windows.Forms.Padding(0);
			uC_sellInfo6.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo6.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo6.Name = "uC_sellInfo6";
			uC_sellInfo6.Size = new System.Drawing.Size(398, 102);
			uC_sellInfo6.TabIndex = 3;
			uC_sellInfo5.AutoSize = true;
			uC_sellInfo5.BackColor = System.Drawing.Color.White;
			uC_sellInfo5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sellInfo5.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sellInfo5.Location = new System.Drawing.Point(3, 207);
			uC_sellInfo5.Margin = new System.Windows.Forms.Padding(0);
			uC_sellInfo5.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo5.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo5.Name = "uC_sellInfo5";
			uC_sellInfo5.Size = new System.Drawing.Size(398, 102);
			uC_sellInfo5.TabIndex = 3;
			uC_sellInfo4.AutoSize = true;
			uC_sellInfo4.BackColor = System.Drawing.Color.White;
			uC_sellInfo4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sellInfo4.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sellInfo4.Location = new System.Drawing.Point(401, 105);
			uC_sellInfo4.Margin = new System.Windows.Forms.Padding(0);
			uC_sellInfo4.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo4.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo4.Name = "uC_sellInfo4";
			uC_sellInfo4.Size = new System.Drawing.Size(398, 102);
			uC_sellInfo4.TabIndex = 3;
			uC_sellInfo2.AutoSize = true;
			uC_sellInfo2.BackColor = System.Drawing.Color.White;
			uC_sellInfo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sellInfo2.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sellInfo2.Location = new System.Drawing.Point(401, 3);
			uC_sellInfo2.Margin = new System.Windows.Forms.Padding(0);
			uC_sellInfo2.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo2.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo2.Name = "uC_sellInfo2";
			uC_sellInfo2.Size = new System.Drawing.Size(398, 102);
			uC_sellInfo2.TabIndex = 1;
			uC_sellInfo1.AutoSize = true;
			uC_sellInfo1.BackColor = System.Drawing.Color.White;
			uC_sellInfo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sellInfo1.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sellInfo1.Location = new System.Drawing.Point(3, 3);
			uC_sellInfo1.Margin = new System.Windows.Forms.Padding(0);
			uC_sellInfo1.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo1.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sellInfo1.Name = "uC_sellInfo1";
			uC_sellInfo1.Size = new System.Drawing.Size(398, 102);
			uC_sellInfo1.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(panel1);
			base.Controls.Add(label1);
			base.Controls.Add(btn_cancel);
			base.Controls.Add(btn_reset);
			base.Controls.Add(btn_enter);
			base.Controls.Add(tableLayoutPanel1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "frmSearchSell";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "會員選擇";
			base.Controls.SetChildIndex(tableLayoutPanel1, 0);
			base.Controls.SetChildIndex(btn_enter, 0);
			base.Controls.SetChildIndex(btn_reset, 0);
			base.Controls.SetChildIndex(btn_cancel, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel8.ResumeLayout(false);
			panel8.PerformLayout();
			panel7.ResumeLayout(false);
			panel7.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}
	}
}
