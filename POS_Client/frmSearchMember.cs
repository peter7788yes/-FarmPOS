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
	public class frmSearchMember : Form
	{
		private IContainer components;

		private TableLayoutPanel tableLayoutPanel1;

		private Label label13;

		private Label label11;

		private Label label9;

		private Label label7;

		private Label label5;

		private Label label3;

		private Label label1;

		private TextBox tb_companyIdno;

		private TextBox tb_idno;

		private TextBox tb_name;

		private TextBox tb_phone;

		private TextBox tb_vipNo;

		private ComboBox cb_status;

		private CheckedListBox clb_memberType;

		private Button btn_cancel;

		private Button btn_reset;

		private Button btn_enter;

		public frmSearchMember()
		{
			InitializeComponent();
		}

		private void frmSearchMember_Load(object sender, EventArgs e)
		{
			cb_status.Items.Add(new ComboboxItem("全部", ""));
			cb_status.Items.Add(new ComboboxItem("正常", "0"));
			cb_status.Items.Add(new ComboboxItem("停用", "1"));
			cb_status.SelectedIndex = 0;
			if (Program.SystemMode == 1)
			{
				clb_memberType.Items.Add(new CheckboxItem("一般會員", 1));
				clb_memberType.Items.Add(new CheckboxItem("優惠會員(1)", 2));
				clb_memberType.Items.Add(new CheckboxItem("優惠會員(2)", 3));
			}
			else
			{
				clb_memberType.Items.Add(new CheckboxItem("一般會員", 1));
				clb_memberType.Items.Add(new CheckboxItem("優惠會員(1)", 2));
				clb_memberType.Items.Add(new CheckboxItem("尚有賒帳未還款會員", 5));
				clb_memberType.Items.Add(new CheckboxItem("優惠會員(2)", 3));
			}
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
			tb_phone.Text = "請輸入會員聯絡電話或手機";
			tb_vipNo.Text = "請刷會員卡(條碼)或輸入會員號";
			tb_name.Text = "請輸入會員姓名關鍵字";
			tb_idno.Text = "請輸入會員身分證字號";
			tb_companyIdno.Text = "請輸入統一編號";
			cb_status.SelectedIndex = 0;
			for (int i = 0; i < clb_memberType.Items.Count; i++)
			{
				clb_memberType.SetItemChecked(i, false);
			}
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btn_enter_Click(object sender, EventArgs e)
		{
			int num = 0;
			List<string> list = new List<string>();
			string text = "SELECT * FROM hypos_CUST_RTL WHERE 1 = 1 ";
			if (tb_phone.Text != "請輸入會員聯絡電話或手機")
			{
				text = text + " AND (Telphone like {" + num + "} or Mobile like {" + num + "})";
				list.Add("%" + tb_phone.Text + "%");
				num++;
			}
			if (tb_vipNo.Text != "請刷會員卡(條碼)或輸入會員號")
			{
				text = text + " AND VipNo like {" + num + "}";
				list.Add("%" + tb_vipNo.Text + "%");
				num++;
			}
			if (tb_name.Text != "請輸入會員姓名關鍵字")
			{
				text = text + " AND Name like {" + num + "}";
				list.Add("%" + tb_name.Text + "%");
				num++;
			}
			if (tb_idno.Text != "請輸入會員身分證字號")
			{
				text = text + " AND IdNo like {" + num + "}";
				list.Add("%" + tb_idno.Text + "%");
				num++;
			}
			if (tb_companyIdno.Text != "請輸入統一編號")
			{
				text = text + " AND CompanyIdNo like {" + num + "}";
				list.Add("%" + tb_companyIdno.Text + "%");
				num++;
			}
			if (clb_memberType.CheckedItems.Count > 0)
			{
				bool flag = false;
				bool flag2 = false;
				foreach (CheckboxItem checkedItem in clb_memberType.CheckedItems)
				{
					if (!"4".Equals(checkedItem.Value.ToString()))
					{
						if ("5".Equals(checkedItem.Value.ToString()))
						{
							flag = true;
						}
						else
						{
							flag2 = true;
						}
					}
				}
				if (flag2)
				{
					text += " AND Type in (";
					foreach (CheckboxItem checkedItem2 in clb_memberType.CheckedItems)
					{
						if (!"4".Equals(checkedItem2.Value.ToString()) && !"5".Equals(checkedItem2.Value.ToString()))
						{
							text = text + "{" + num + "},";
							list.Add(checkedItem2.Value.ToString());
							num++;
						}
					}
					text = text.Substring(0, text.Length - 1) + ")";
				}
				if (flag)
				{
					text += " AND Credit > 0";
				}
			}
			string text2 = (cb_status.SelectedItem as ComboboxItem).Value.ToString();
			if (!string.IsNullOrEmpty(text2))
			{
				text = text + " AND status = {" + num + "}";
				list.Add(text2);
			}
			text += " ORDER BY CreateDate DESC";
			frmMemberMangement obj = (frmMemberMangement)base.Owner;
			obj.dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text, list.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
			obj.changePage(1);
			obj.pageTotal = (int)Math.Ceiling((double)obj.dt.Rows.Count / 8.0);
			Close();
		}

		private void tb_phone_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_phone.Text))
			{
				tb_phone.Text = "請輸入會員聯絡電話或手機";
			}
		}

		private void tb_phone_Enter(object sender, EventArgs e)
		{
			if ("請輸入會員聯絡電話或手機".Equals(tb_phone.Text))
			{
				tb_phone.Text = "";
			}
		}

		private void tb_vipNo_Enter(object sender, EventArgs e)
		{
			if ("請刷會員卡(條碼)或輸入會員號".Equals(tb_vipNo.Text))
			{
				tb_vipNo.Text = "";
			}
		}

		private void tb_vipNo_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_vipNo.Text))
			{
				tb_vipNo.Text = "請刷會員卡(條碼)或輸入會員號";
			}
		}

		private void tb_name_Enter(object sender, EventArgs e)
		{
			if ("請輸入會員姓名關鍵字".Equals(tb_name.Text))
			{
				tb_name.Text = "";
			}
		}

		private void tb_name_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_name.Text))
			{
				tb_name.Text = "請輸入會員姓名關鍵字";
			}
		}

		private void tb_idno_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_idno.Text))
			{
				tb_idno.Text = "請輸入會員身分證字號";
			}
		}

		private void tb_idno_Enter(object sender, EventArgs e)
		{
			if ("請輸入會員身分證字號".Equals(tb_idno.Text))
			{
				tb_idno.Text = "";
			}
		}

		private void tb_companyIdno_Enter(object sender, EventArgs e)
		{
			if ("請輸入統一編號".Equals(tb_companyIdno.Text))
			{
				tb_companyIdno.Text = "";
			}
		}

		private void tb_companyIdno_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_companyIdno.Text))
			{
				tb_companyIdno.Text = "請輸入統一編號";
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
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			tb_companyIdno = new System.Windows.Forms.TextBox();
			tb_idno = new System.Windows.Forms.TextBox();
			tb_name = new System.Windows.Forms.TextBox();
			label13 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			tb_phone = new System.Windows.Forms.TextBox();
			tb_vipNo = new System.Windows.Forms.TextBox();
			cb_status = new System.Windows.Forms.ComboBox();
			clb_memberType = new System.Windows.Forms.CheckedListBox();
			btn_cancel = new System.Windows.Forms.Button();
			btn_reset = new System.Windows.Forms.Button();
			btn_enter = new System.Windows.Forms.Button();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.30479f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.69521f));
			tableLayoutPanel1.Controls.Add(tb_companyIdno, 1, 4);
			tableLayoutPanel1.Controls.Add(tb_idno, 1, 3);
			tableLayoutPanel1.Controls.Add(tb_name, 1, 2);
			tableLayoutPanel1.Controls.Add(label13, 0, 6);
			tableLayoutPanel1.Controls.Add(label11, 0, 5);
			tableLayoutPanel1.Controls.Add(label9, 0, 4);
			tableLayoutPanel1.Controls.Add(label7, 0, 3);
			tableLayoutPanel1.Controls.Add(label5, 0, 2);
			tableLayoutPanel1.Controls.Add(label3, 0, 1);
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(tb_phone, 1, 0);
			tableLayoutPanel1.Controls.Add(tb_vipNo, 1, 1);
			tableLayoutPanel1.Controls.Add(cb_status, 1, 6);
			tableLayoutPanel1.Controls.Add(clb_memberType, 1, 5);
			tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
			tableLayoutPanel1.Location = new System.Drawing.Point(41, 23);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 7;
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
			tb_companyIdno.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_companyIdno.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_companyIdno.ForeColor = System.Drawing.Color.Gray;
			tb_companyIdno.Location = new System.Drawing.Point(198, 286);
			tb_companyIdno.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			tb_companyIdno.Name = "tb_companyIdno";
			tb_companyIdno.Size = new System.Drawing.Size(441, 33);
			tb_companyIdno.TabIndex = 17;
			tb_companyIdno.Text = "請輸入統一編號";
			tb_companyIdno.Enter += new System.EventHandler(tb_companyIdno_Enter);
			tb_companyIdno.Leave += new System.EventHandler(tb_companyIdno_Leave);
			tb_idno.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_idno.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_idno.ForeColor = System.Drawing.Color.Gray;
			tb_idno.Location = new System.Drawing.Point(198, 219);
			tb_idno.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			tb_idno.Name = "tb_idno";
			tb_idno.Size = new System.Drawing.Size(441, 33);
			tb_idno.TabIndex = 16;
			tb_idno.Text = "請輸入會員身分證字號";
			tb_idno.Enter += new System.EventHandler(tb_idno_Enter);
			tb_idno.Leave += new System.EventHandler(tb_idno_Leave);
			tb_name.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_name.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_name.ForeColor = System.Drawing.Color.Gray;
			tb_name.Location = new System.Drawing.Point(198, 152);
			tb_name.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			tb_name.Name = "tb_name";
			tb_name.Size = new System.Drawing.Size(441, 33);
			tb_name.TabIndex = 15;
			tb_name.Text = "請輸入會員姓名關鍵字";
			tb_name.Enter += new System.EventHandler(tb_name_Enter);
			tb_name.Leave += new System.EventHandler(tb_name_Leave);
			label13.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			label13.Dock = System.Windows.Forms.DockStyle.Fill;
			label13.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.White;
			label13.Location = new System.Drawing.Point(2, 404);
			label13.Margin = new System.Windows.Forms.Padding(0);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(174, 71);
			label13.TabIndex = 12;
			label13.Text = "會員狀態";
			label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			label11.Dock = System.Windows.Forms.DockStyle.Fill;
			label11.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label11.ForeColor = System.Drawing.Color.White;
			label11.Location = new System.Drawing.Point(2, 337);
			label11.Margin = new System.Windows.Forms.Padding(0);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(174, 65);
			label11.TabIndex = 10;
			label11.Text = "會員類型";
			label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			label9.Dock = System.Windows.Forms.DockStyle.Fill;
			label9.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label9.ForeColor = System.Drawing.Color.White;
			label9.Location = new System.Drawing.Point(2, 270);
			label9.Margin = new System.Windows.Forms.Padding(0);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(174, 65);
			label9.TabIndex = 8;
			label9.Text = "統一編號";
			label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			label7.Dock = System.Windows.Forms.DockStyle.Fill;
			label7.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(2, 203);
			label7.Margin = new System.Windows.Forms.Padding(0);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(174, 65);
			label7.TabIndex = 6;
			label7.Text = "身分證字號";
			label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			label5.Dock = System.Windows.Forms.DockStyle.Fill;
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.White;
			label5.Location = new System.Drawing.Point(2, 136);
			label5.Margin = new System.Windows.Forms.Padding(0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(174, 65);
			label5.TabIndex = 4;
			label5.Text = "會員姓名";
			label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			label3.Dock = System.Windows.Forms.DockStyle.Fill;
			label3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.White;
			label3.Location = new System.Drawing.Point(2, 69);
			label3.Margin = new System.Windows.Forms.Padding(0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(174, 65);
			label3.TabIndex = 2;
			label3.Text = "會員編號";
			label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			label1.Dock = System.Windows.Forms.DockStyle.Fill;
			label1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(2, 2);
			label1.Margin = new System.Windows.Forms.Padding(0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(174, 65);
			label1.TabIndex = 0;
			label1.Text = "聯絡電話";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			tb_phone.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_phone.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_phone.ForeColor = System.Drawing.Color.Gray;
			tb_phone.Location = new System.Drawing.Point(198, 18);
			tb_phone.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			tb_phone.Name = "tb_phone";
			tb_phone.Size = new System.Drawing.Size(441, 33);
			tb_phone.TabIndex = 13;
			tb_phone.Text = "請輸入會員聯絡電話或手機";
			tb_phone.Enter += new System.EventHandler(tb_phone_Enter);
			tb_phone.Leave += new System.EventHandler(tb_phone_Leave);
			tb_vipNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_vipNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_vipNo.ForeColor = System.Drawing.Color.Gray;
			tb_vipNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_vipNo.Location = new System.Drawing.Point(198, 85);
			tb_vipNo.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			tb_vipNo.Name = "tb_vipNo";
			tb_vipNo.Size = new System.Drawing.Size(441, 33);
			tb_vipNo.TabIndex = 14;
			tb_vipNo.Text = "請刷會員卡(條碼)或輸入會員號";
			tb_vipNo.Enter += new System.EventHandler(tb_vipNo_Enter);
			tb_vipNo.Leave += new System.EventHandler(tb_vipNo_Leave);
			cb_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_status.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_status.FormattingEnabled = true;
			cb_status.Location = new System.Drawing.Point(198, 423);
			cb_status.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			cb_status.Name = "cb_status";
			cb_status.Size = new System.Drawing.Size(189, 32);
			cb_status.TabIndex = 18;
			clb_memberType.BackColor = System.Drawing.SystemColors.Control;
			clb_memberType.BorderStyle = System.Windows.Forms.BorderStyle.None;
			clb_memberType.CheckOnClick = true;
			clb_memberType.ColumnWidth = 200;
			clb_memberType.Dock = System.Windows.Forms.DockStyle.Fill;
			clb_memberType.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			clb_memberType.FormattingEnabled = true;
			clb_memberType.IntegralHeight = false;
			clb_memberType.Location = new System.Drawing.Point(198, 337);
			clb_memberType.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
			clb_memberType.MultiColumn = true;
			clb_memberType.Name = "clb_memberType";
			clb_memberType.Size = new System.Drawing.Size(623, 65);
			clb_memberType.TabIndex = 19;
			clb_memberType.UseCompatibleTextRendering = true;
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(541, 516);
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
			btn_enter.Location = new System.Drawing.Point(288, 516);
			btn_enter.Name = "btn_enter";
			btn_enter.Size = new System.Drawing.Size(100, 35);
			btn_enter.TabIndex = 44;
			btn_enter.TabStop = false;
			btn_enter.Text = "查 詢";
			btn_enter.UseVisualStyleBackColor = false;
			btn_enter.Click += new System.EventHandler(btn_enter_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Silver;
			base.ClientSize = new System.Drawing.Size(904, 576);
			base.ControlBox = false;
			base.Controls.Add(btn_cancel);
			base.Controls.Add(btn_reset);
			base.Controls.Add(btn_enter);
			base.Controls.Add(tableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmSearchMember";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "frmSearchMember";
			base.Load += new System.EventHandler(frmSearchMember_Load);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			ResumeLayout(false);
		}
	}
}
