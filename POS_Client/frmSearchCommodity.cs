using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmSearchCommodity : Form
	{
		private IContainer components;

		private Button btn_cancel;

		private Button btn_reset;

		private Button btn_enter;

		private TextBox Cm_word;

		private TextBox CommodityNum;

		private Label label1;

		private Label label3;

		private Label label5;

		private Label label7;

		private Label label9;

		private Label label11;

		private TableLayoutPanel tableLayoutPanel1;

		private CheckedListBox clb_datatype;

		private CheckedListBox clb_datafrom;

		private CheckedListBox clb_datastatus;

		private TextBox brandName;

		private CheckBox cb_SubsidyisnotZero;

		public frmSearchCommodity()
		{
			InitializeComponent();
		}

		private void frmSearchMember_Load(object sender, EventArgs e)
		{
			clb_datafrom.Items.Add(new CheckboxItem("介接", "Y"));
			if (Program.SystemMode == 0)
			{
				clb_datafrom.Items.Add(new CheckboxItem("自建", "N"));
			}
			clb_datatype.Items.Add(new CheckboxItem("農藥", "0302"));
			if (Program.SystemMode == 0)
			{
				clb_datatype.Items.Add(new CheckboxItem("肥料", "0303"));
				clb_datatype.Items.Add(new CheckboxItem("資材", "0305"));
				clb_datatype.Items.Add(new CheckboxItem("其他", "0308"));
			}
			clb_datastatus.Items.Add(new CheckboxItem("使用中", "U"));
			clb_datastatus.Items.Add(new CheckboxItem("未使用", "N"));
			clb_datastatus.Items.Add(new CheckboxItem("已停用", "S"));
			cb_SubsidyisnotZero.Checked = false;
		}

		private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			Graphics graphic = e.Graphics;
			Rectangle cellBounds = e.CellBounds;
			using (Pen pen = new Pen(Color.White, 1f))
			{
				pen.Alignment = PenAlignment.Center;
				pen.DashStyle = DashStyle.Solid;
				if (e.Row == tableLayoutPanel1.RowCount - 1)
				{
					cellBounds.Height--;
				}
				if (e.Column == tableLayoutPanel1.ColumnCount - 1)
				{
					cellBounds.Width--;
				}
				e.Graphics.DrawRectangle(pen, cellBounds);
			}
		}

		private void btn_reset_Click(object sender, EventArgs e)
		{
			CommodityNum.Text = "請刷商品條碼或輸入條碼";
			Cm_word.Text = "請輸入商品名稱關鍵字";
			clb_datafrom.SelectedIndex = 0;
			brandName.Text = "請輸入廠商名稱關鍵字";
			for (int i = 0; i < clb_datafrom.Items.Count; i++)
			{
				clb_datafrom.SetItemChecked(i, false);
			}
			for (int j = 0; j < clb_datatype.Items.Count; j++)
			{
				clb_datatype.SetItemChecked(j, false);
			}
			for (int k = 0; k < clb_datastatus.Items.Count; k++)
			{
				clb_datastatus.SetItemChecked(k, false);
			}
			cb_SubsidyisnotZero.Checked = false;
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btn_enter_Click(object sender, EventArgs e)
		{
			int num = 0;
			List<string> list = new List<string>();
			string str = "";
			string value = "";
			bool flag = false;
			if (Program.SystemMode == 1)
			{
				str += " SELECT hg.* FROM hypos_GOODSLST as hg, HyLicence as hl WHERE hg.CLA1NO ='0302' and hg.ISWS ='Y' and hg.licType = hl.licType and hg.domManufId = hl.licNo and hl.isDelete='N' and hg.status !='D'";
			}
			else
			{
				str += " SELECT hg.* FROM hypos_GOODSLST as hg left outer join HyLicence as hl on hg.licType =hl.licType and hg.domManufId =hl.licNo WHERE (hl.isDelete='N' or hl.isDelete is null) AND hg.status !='D'";
				value = "All";
				flag = true;
			}
			if (CommodityNum.Text != "請刷商品條碼或輸入條碼")
			{
				str = str + " AND hg.GDSNO = {" + num + "}";
				list.Add(CommodityNum.Text);
				num++;
			}
			if (Cm_word.Text != "請輸入商品名稱關鍵字")
			{
				str = str + " AND hg.GDName like {" + num + "}";
				list.Add("%" + Cm_word.Text + "%");
				num++;
			}
			if (brandName.Text != "請輸入廠商名稱關鍵字")
			{
				str = str + " AND hg.brandName like {" + num + "}";
				list.Add("%" + brandName.Text + "%");
				num++;
			}
			if (clb_datafrom.CheckedItems.Count > 0)
			{
				str += " AND hg.ISWS in (";
				foreach (CheckboxItem checkedItem in clb_datafrom.CheckedItems)
				{
					str = str + "{" + num + "},";
					list.Add(checkedItem.Value.ToString());
					num++;
				}
				str = str.Substring(0, str.Length - 1) + ")";
			}
			if (clb_datatype.CheckedItems.Count > 0 && flag)
			{
				string str2 = "";
				str += " AND (";
				foreach (CheckboxItem checkedItem2 in clb_datatype.CheckedItems)
				{
					if ("0302".Equals(checkedItem2.Value.ToString()))
					{
						str += " (hg.CLA1NO ='0302' and hg.ISWS ='Y' and hg.licType =hl.licType and hg.domManufId =hl.licNo) OR (hg.ISWS ='N' and hg.CLA1NO ='0302')";
						str2 = " OR";
					}
					if ("0303".Equals(checkedItem2.Value.ToString()))
					{
						str = str + str2 + " hg.CLA1NO ='0303'";
						str2 = " OR";
					}
					if ("0305".Equals(checkedItem2.Value.ToString()))
					{
						str = str + str2 + " hg.CLA1NO ='0305'";
						str2 = " OR";
					}
					if ("0308".Equals(checkedItem2.Value.ToString()))
					{
						str = str + str2 + " hg.CLA1NO ='0308'";
					}
				}
				str += " )";
				value = "CLA1NO";
			}
			if (clb_datastatus.CheckedItems.Count > 0)
			{
				str += " AND hg.status in (";
				foreach (CheckboxItem checkedItem3 in clb_datastatus.CheckedItems)
				{
					str = str + "{" + num + "},";
					list.Add(checkedItem3.Value.ToString());
					num++;
				}
				str = str.Substring(0, str.Length - 1) + ")";
			}
			if ("All".Equals(value) && flag)
			{
				str += " AND ((hg.CLA1NO ='0302' and hg.ISWS ='Y' and hg.licType =hl.licType and hg.domManufId =hl.licNo) OR (hg.ISWS ='N' and hg.CLA1NO ='0302') OR hg.CLA1NO ='0303' OR hg.CLA1NO ='0305' OR hg.CLA1NO ='0308')";
			}
			if (cb_SubsidyisnotZero.Checked)
			{
				str += " AND hg.SubsidyMoney > 0 ";
			}
			frmCommodityMangement obj = (frmCommodityMangement)base.Owner;
			obj.dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, str, list.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
			obj.changePage(1);
			obj.pageTotal = (int)Math.Ceiling((double)obj.dt.Rows.Count / 8.0);
			Close();
		}

		private void tb_phone_Leave(object sender, EventArgs e)
		{
			if ("".Equals(CommodityNum.Text))
			{
				CommodityNum.Text = "請刷商品條碼或輸入條碼";
			}
		}

		private void tb_phone_Enter(object sender, EventArgs e)
		{
			if ("請刷商品條碼或輸入條碼".Equals(CommodityNum.Text))
			{
				CommodityNum.Text = "";
			}
		}

		private void tb_vipNo_Enter(object sender, EventArgs e)
		{
			if ("請輸入商品名稱關鍵字".Equals(Cm_word.Text))
			{
				Cm_word.Text = "";
			}
		}

		private void tb_vipNo_Leave(object sender, EventArgs e)
		{
			if ("".Equals(Cm_word.Text))
			{
				Cm_word.Text = "請輸入商品名稱關鍵字";
			}
		}

		private void barndname_Enter(object sender, EventArgs e)
		{
			if ("請輸入廠商名稱關鍵字".Equals(brandName.Text))
			{
				brandName.Text = "";
			}
		}

		private void barndname_Leave(object sender, EventArgs e)
		{
			if ("".Equals(brandName.Text))
			{
				brandName.Text = "請輸入廠商名稱關鍵字";
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
			btn_cancel = new System.Windows.Forms.Button();
			btn_reset = new System.Windows.Forms.Button();
			btn_enter = new System.Windows.Forms.Button();
			Cm_word = new System.Windows.Forms.TextBox();
			CommodityNum = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			clb_datatype = new System.Windows.Forms.CheckedListBox();
			clb_datafrom = new System.Windows.Forms.CheckedListBox();
			clb_datastatus = new System.Windows.Forms.CheckedListBox();
			brandName = new System.Windows.Forms.TextBox();
			cb_SubsidyisnotZero = new System.Windows.Forms.CheckBox();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(528, 516);
			btn_cancel.Name = "btn_cancel";
			btn_cancel.Size = new System.Drawing.Size(75, 35);
			btn_cancel.TabIndex = 46;
			btn_cancel.TabStop = false;
			btn_cancel.Text = "關閉";
			btn_cancel.UseVisualStyleBackColor = false;
			btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
			btn_reset.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_reset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_reset.ForeColor = System.Drawing.Color.White;
			btn_reset.Location = new System.Drawing.Point(427, 516);
			btn_reset.Name = "btn_reset";
			btn_reset.Size = new System.Drawing.Size(75, 35);
			btn_reset.TabIndex = 45;
			btn_reset.TabStop = false;
			btn_reset.Text = "重設";
			btn_reset.UseVisualStyleBackColor = false;
			btn_reset.Click += new System.EventHandler(btn_reset_Click);
			btn_enter.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_enter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_enter.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_enter.ForeColor = System.Drawing.Color.White;
			btn_enter.Location = new System.Drawing.Point(301, 516);
			btn_enter.Name = "btn_enter";
			btn_enter.Size = new System.Drawing.Size(100, 35);
			btn_enter.TabIndex = 44;
			btn_enter.TabStop = false;
			btn_enter.Text = "查 詢";
			btn_enter.UseVisualStyleBackColor = false;
			btn_enter.Click += new System.EventHandler(btn_enter_Click);
			Cm_word.Anchor = System.Windows.Forms.AnchorStyles.Left;
			Cm_word.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			Cm_word.ForeColor = System.Drawing.Color.Gray;
			Cm_word.Location = new System.Drawing.Point(198, 103);
			Cm_word.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			Cm_word.Name = "Cm_word";
			Cm_word.Size = new System.Drawing.Size(441, 33);
			Cm_word.TabIndex = 14;
			Cm_word.Text = "請輸入商品名稱關鍵字";
			Cm_word.Enter += new System.EventHandler(tb_vipNo_Enter);
			Cm_word.Leave += new System.EventHandler(tb_vipNo_Leave);
			CommodityNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
			CommodityNum.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			CommodityNum.ForeColor = System.Drawing.Color.Gray;
			CommodityNum.ImeMode = System.Windows.Forms.ImeMode.Disable;
			CommodityNum.Location = new System.Drawing.Point(198, 24);
			CommodityNum.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			CommodityNum.Name = "CommodityNum";
			CommodityNum.Size = new System.Drawing.Size(441, 33);
			CommodityNum.TabIndex = 13;
			CommodityNum.Text = "請刷商品條碼或輸入條碼";
			CommodityNum.Enter += new System.EventHandler(tb_phone_Enter);
			CommodityNum.Leave += new System.EventHandler(tb_phone_Leave);
			label1.BackColor = System.Drawing.Color.FromArgb(41, 162, 198);
			label1.Dock = System.Windows.Forms.DockStyle.Fill;
			label1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(2, 2);
			label1.Margin = new System.Windows.Forms.Padding(0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(174, 77);
			label1.TabIndex = 0;
			label1.Text = "商品編號";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label3.BackColor = System.Drawing.Color.FromArgb(41, 162, 198);
			label3.Dock = System.Windows.Forms.DockStyle.Fill;
			label3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.White;
			label3.Location = new System.Drawing.Point(2, 81);
			label3.Margin = new System.Windows.Forms.Padding(0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(174, 77);
			label3.TabIndex = 2;
			label3.Text = "商品名稱";
			label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label5.BackColor = System.Drawing.Color.FromArgb(41, 162, 198);
			label5.Dock = System.Windows.Forms.DockStyle.Fill;
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.White;
			label5.Location = new System.Drawing.Point(2, 160);
			label5.Margin = new System.Windows.Forms.Padding(0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(174, 77);
			label5.TabIndex = 4;
			label5.Text = "資料模式";
			label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label7.BackColor = System.Drawing.Color.FromArgb(41, 162, 198);
			label7.Dock = System.Windows.Forms.DockStyle.Fill;
			label7.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(2, 239);
			label7.Margin = new System.Windows.Forms.Padding(0);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(174, 77);
			label7.TabIndex = 6;
			label7.Text = "商品類型";
			label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label9.BackColor = System.Drawing.Color.FromArgb(41, 162, 198);
			label9.Dock = System.Windows.Forms.DockStyle.Fill;
			label9.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label9.ForeColor = System.Drawing.Color.White;
			label9.Location = new System.Drawing.Point(2, 318);
			label9.Margin = new System.Windows.Forms.Padding(0);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(174, 77);
			label9.TabIndex = 8;
			label9.Text = "商品狀態";
			label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label11.BackColor = System.Drawing.Color.FromArgb(41, 162, 198);
			label11.Dock = System.Windows.Forms.DockStyle.Fill;
			label11.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label11.ForeColor = System.Drawing.Color.White;
			label11.Location = new System.Drawing.Point(2, 397);
			label11.Margin = new System.Windows.Forms.Padding(0);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(174, 78);
			label11.TabIndex = 10;
			label11.Text = "廠商名稱";
			label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.30479f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.69521f));
			tableLayoutPanel1.Controls.Add(clb_datatype, 1, 3);
			tableLayoutPanel1.Controls.Add(label11, 0, 5);
			tableLayoutPanel1.Controls.Add(label9, 0, 4);
			tableLayoutPanel1.Controls.Add(label7, 0, 3);
			tableLayoutPanel1.Controls.Add(label5, 0, 2);
			tableLayoutPanel1.Controls.Add(label3, 0, 1);
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(CommodityNum, 1, 0);
			tableLayoutPanel1.Controls.Add(Cm_word, 1, 1);
			tableLayoutPanel1.Controls.Add(clb_datafrom, 1, 2);
			tableLayoutPanel1.Controls.Add(clb_datastatus, 1, 4);
			tableLayoutPanel1.Controls.Add(brandName, 1, 5);
			tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
			tableLayoutPanel1.Location = new System.Drawing.Point(41, 23);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 6;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel1.Size = new System.Drawing.Size(823, 477);
			tableLayoutPanel1.TabIndex = 33;
			tableLayoutPanel1.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(tableLayoutPanel1_CellPaint);
			clb_datatype.BackColor = System.Drawing.SystemColors.Control;
			clb_datatype.BorderStyle = System.Windows.Forms.BorderStyle.None;
			clb_datatype.CheckOnClick = true;
			clb_datatype.ColumnWidth = 200;
			clb_datatype.Dock = System.Windows.Forms.DockStyle.Fill;
			clb_datatype.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			clb_datatype.FormattingEnabled = true;
			clb_datatype.IntegralHeight = false;
			clb_datatype.Location = new System.Drawing.Point(198, 239);
			clb_datatype.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			clb_datatype.MultiColumn = true;
			clb_datatype.Name = "clb_datatype";
			clb_datatype.Size = new System.Drawing.Size(623, 77);
			clb_datatype.TabIndex = 21;
			clb_datatype.UseCompatibleTextRendering = true;
			clb_datafrom.BackColor = System.Drawing.SystemColors.Control;
			clb_datafrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
			clb_datafrom.CheckOnClick = true;
			clb_datafrom.ColumnWidth = 200;
			clb_datafrom.Dock = System.Windows.Forms.DockStyle.Fill;
			clb_datafrom.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			clb_datafrom.FormattingEnabled = true;
			clb_datafrom.IntegralHeight = false;
			clb_datafrom.Location = new System.Drawing.Point(198, 160);
			clb_datafrom.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			clb_datafrom.MultiColumn = true;
			clb_datafrom.Name = "clb_datafrom";
			clb_datafrom.Size = new System.Drawing.Size(623, 77);
			clb_datafrom.TabIndex = 20;
			clb_datafrom.UseCompatibleTextRendering = true;
			clb_datastatus.BackColor = System.Drawing.SystemColors.Control;
			clb_datastatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
			clb_datastatus.CheckOnClick = true;
			clb_datastatus.ColumnWidth = 200;
			clb_datastatus.Dock = System.Windows.Forms.DockStyle.Fill;
			clb_datastatus.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			clb_datastatus.FormattingEnabled = true;
			clb_datastatus.IntegralHeight = false;
			clb_datastatus.Location = new System.Drawing.Point(198, 318);
			clb_datastatus.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			clb_datastatus.MultiColumn = true;
			clb_datastatus.Name = "clb_datastatus";
			clb_datastatus.Size = new System.Drawing.Size(623, 77);
			clb_datastatus.TabIndex = 22;
			clb_datastatus.UseCompatibleTextRendering = true;
			brandName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			brandName.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			brandName.ForeColor = System.Drawing.Color.Gray;
			brandName.Location = new System.Drawing.Point(198, 419);
			brandName.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			brandName.Name = "brandName";
			brandName.Size = new System.Drawing.Size(441, 33);
			brandName.TabIndex = 23;
			brandName.Text = "請輸入廠商名稱關鍵字";
			brandName.Enter += new System.EventHandler(barndname_Enter);
			brandName.Leave += new System.EventHandler(barndname_Leave);
			cb_SubsidyisnotZero.AutoSize = true;
			cb_SubsidyisnotZero.BackColor = System.Drawing.SystemColors.Control;
			cb_SubsidyisnotZero.Font = new System.Drawing.Font("微軟正黑體", 14.25f);
			cb_SubsidyisnotZero.Location = new System.Drawing.Point(440, 369);
			cb_SubsidyisnotZero.Name = "cb_SubsidyisnotZero";
			cb_SubsidyisnotZero.Size = new System.Drawing.Size(154, 28);
			cb_SubsidyisnotZero.TabIndex = 47;
			cb_SubsidyisnotZero.Text = "補助金額不為0";
			cb_SubsidyisnotZero.UseVisualStyleBackColor = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Silver;
			base.ClientSize = new System.Drawing.Size(904, 576);
			base.ControlBox = false;
			base.Controls.Add(cb_SubsidyisnotZero);
			base.Controls.Add(btn_cancel);
			base.Controls.Add(btn_reset);
			base.Controls.Add(btn_enter);
			base.Controls.Add(tableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmSearchCommodity";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "frmSearchMember";
			base.Load += new System.EventHandler(frmSearchMember_Load);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
