using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmChooseVendor : MasterThinForm
	{
		private ucVendorInfo[] ucVendorInfo;

		private frmNewDeliveryOrder frmD;

		private DataTable dt;

		private int pageNow = 1;

		public int pageTotal = 1;

		private bool IsRecentlyDelivery = true;

		[CompilerGenerated]
		private string _003CreturnSupplierNo_003Ek__BackingField;

		[CompilerGenerated]
		private string _003CreturnSupplierName_003Ek__BackingField;

		private IContainer components;

		private TextBox tb_SupplierIdNo;

		private Button search;

		private Label label5;

		private Button cancel;

		private Button reset;

		private Button btn_AddNewVendor;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel3;

		private Label label4;

		private Panel panel2;

		private Label label7;

		private TextBox tb_vendorId;

		private Panel panel1;

		private Label label1;

		private TextBox tb_SupplierName;

		private ucVendorInfo ucVendorInfo1;

		private ucVendorInfo ucVendorInfo2;

		private ucVendorInfo ucVendorInfo3;

		private ucVendorInfo ucVendorInfo4;

		private ucVendorInfo ucVendorInfo5;

		private ucVendorInfo ucVendorInfo6;

		private Button btn_pageLeft;

		private Button btn_pageRight;

		private Label l_pageInfo;

		public string returnSupplierNo
		{
			[CompilerGenerated]
			get
			{
				return _003CreturnSupplierNo_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CreturnSupplierNo_003Ek__BackingField = value;
			}
		}

		public string returnSupplierName
		{
			[CompilerGenerated]
			get
			{
				return _003CreturnSupplierName_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CreturnSupplierName_003Ek__BackingField = value;
			}
		}

		public frmChooseVendor()
			: base("出貨作業")
		{
			InitializeComponent();
			tb_SupplierIdNo.Select();
		}

		public frmChooseVendor(frmNewDeliveryOrder frmD)
			: base("出貨作業")
		{
			this.frmD = frmD;
			InitializeComponent();
			label5.Text = "   最近出貨廠商";
			ucVendorInfo = new ucVendorInfo[6]
			{
				ucVendorInfo1,
				ucVendorInfo2,
				ucVendorInfo3,
				ucVendorInfo4,
				ucVendorInfo5,
				ucVendorInfo6
			};
			ucVendorInfo[] array = ucVendorInfo;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Visible = false;
			}
			try
			{
				string sql = "SELECT s.*, (select d.DeliveryDate from hypos_DeliveryGoods_Master as d where s.SupplierNo=d.vendorNo order by DeliveryDate desc) as DeliveryDate FROM hypos_Supplier as s where DeliveryDate <> '' order by DeliveryDate desc";
				dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
				changepage(1);
			}
			catch (Exception)
			{
			}
		}

		private void commodityName_Enter(object sender, EventArgs e)
		{
			if ("請輸入廠商名稱".Equals(tb_SupplierName.Text))
			{
				tb_SupplierName.Text = "";
			}
		}

		private void commodityName_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_SupplierName.Text))
			{
				tb_SupplierName.Text = "請輸入廠商名稱";
			}
		}

		private void commodityNum_Enter(object sender, EventArgs e)
		{
			if ("請輸入廠商統一編號".Equals(tb_SupplierIdNo.Text))
			{
				tb_SupplierIdNo.Text = "";
			}
		}

		public void infolistCellForInventory(object sender, EventArgs e)
		{
		}

		public void infolistCellForDelivery(object sender, EventArgs e)
		{
			ucVendorInfo ucVendorInfo = sender as ucVendorInfo;
			new dialogAddNewVendor(frmD, this, ucVendorInfo.getVendorNo()).ShowDialog();
		}

		private void commodityNum_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_SupplierIdNo.Text))
			{
				tb_SupplierIdNo.Text = "請輸入廠商統一編號";
			}
		}

		private void search_Click(object sender, EventArgs e)
		{
			if ((tb_SupplierIdNo.Text == "請輸入廠商統一編號" || tb_SupplierIdNo.Text.Trim() == "") && (tb_SupplierName.Text == "請輸入廠商名稱" || tb_SupplierName.Text.Trim() == "") && (tb_vendorId.Text == "請輸入廠商營業執照號碼" || tb_vendorId.Text.Trim() == ""))
			{
				AutoClosingMessageBox.Show("必須輸入查詢條件");
				return;
			}
			IsRecentlyDelivery = false;
			string str = "SELECT * FROM hypos_Supplier WHERE 1=1 and vendorType in ('0','2') and status = 0 ";
			string text = "";
			if (tb_SupplierIdNo.Text.Trim() != "" && tb_SupplierIdNo.Text.Trim() != "請輸入廠商統一編號")
			{
				text = text + " AND SupplierIdNo like '%" + tb_SupplierIdNo.Text.Trim() + "%' ";
			}
			if (tb_SupplierName.Text.Trim() != "" && tb_SupplierName.Text.Trim() != "請輸入廠商名稱")
			{
				text = text + " AND SupplierName like '%" + tb_SupplierName.Text.Trim() + "%' ";
			}
			if (tb_vendorId.Text.Trim() != "" && tb_vendorId.Text.Trim() != "請輸入廠商營業執照號碼")
			{
				text = text + " AND vendorId like '%" + tb_vendorId.Text.Trim() + "%' ";
			}
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, str + text, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changepage(1);
		}

		private void changepage(int page)
		{
			int num = 0;
			pageNow = page;
			for (int i = (pageNow - 1) * 6; i < pageNow * 6; i++)
			{
				if (i < dt.Rows.Count)
				{
					if (IsRecentlyDelivery)
					{
						ucVendorInfo[num].setDate(dt.Rows[i]["DeliveryDate"].ToString());
					}
					else
					{
						ucVendorInfo[num].setDate("");
					}
					ucVendorInfo[num].setPhone(dt.Rows[i]["TelNo"].ToString());
					ucVendorInfo[num].setSupplierName(dt.Rows[i]["SupplierName"].ToString());
					ucVendorInfo[num].setVendorID(dt.Rows[i]["vendorId"].ToString());
					ucVendorInfo[num].setVendorName(dt.Rows[i]["vendorName"].ToString());
					ucVendorInfo[num].setVendorNo(dt.Rows[i]["SupplierNo"].ToString());
					ucVendorInfo[num].Visible = true;
					ucVendorInfo[num].OnClickVendorInfo -= new EventHandler(infolistCellForDelivery);
					ucVendorInfo[num].OnClickVendorInfo += new EventHandler(infolistCellForDelivery);
				}
				else
				{
					ucVendorInfo[num].Visible = false;
				}
				ucVendorInfo[num].BackColor = Color.White;
				num++;
			}
			pageTotal = (int)Math.Ceiling((double)dt.Rows.Count / 6.0);
			l_pageInfo.Text = string.Format("共{0}筆．{1}頁｜目前在第{2}頁", dt.Rows.Count, Math.Ceiling((double)dt.Rows.Count / 6.0), pageNow);
			tb_SupplierIdNo.Select();
		}

		private void tb_CommodityNum_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				search_Click(sender, e);
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		{
			if (frmD != null)
			{
				frmD.Location = new Point(base.Location.X, base.Location.Y);
				frmD.setfocus();
				frmD.Show();
			}
			Hide();
		}

		private void reset_Click(object sender, EventArgs e)
		{
			tb_SupplierIdNo.Text = "請輸入廠商統一編號";
			tb_SupplierName.Text = "請輸入廠商名稱";
			tb_vendorId.Text = "請輸入廠商營業執照號碼";
		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void btn_AddNewVendor_Click(object sender, EventArgs e)
		{
			new dialogAddNewVendor(frmD, this).ShowDialog();
		}

		private void btn_pageRight_Click(object sender, EventArgs e)
		{
			if (pageNow < pageTotal)
			{
				changepage(pageNow + 1);
			}
		}

		private void btn_pageLeft_Click(object sender, EventArgs e)
		{
			if (pageNow > 1)
			{
				changepage(pageNow - 1);
			}
		}

		private void tb_SupplierName_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_SupplierName.Text))
			{
				tb_SupplierName.Text = "請輸入廠商名稱";
			}
		}

		private void tb_vendorId_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_vendorId.Text))
			{
				tb_vendorId.Text = "請輸入廠商營業執照號碼";
			}
		}

		private void tb_SupplierName_Enter(object sender, EventArgs e)
		{
			if ("請輸入廠商名稱".Equals(tb_SupplierName.Text))
			{
				tb_SupplierName.Text = "";
			}
		}

		private void tb_vendorId_Enter(object sender, EventArgs e)
		{
			if ("請輸入廠商營業執照號碼".Equals(tb_vendorId.Text))
			{
				tb_vendorId.Text = "";
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
			btn_AddNewVendor = new System.Windows.Forms.Button();
			cancel = new System.Windows.Forms.Button();
			reset = new System.Windows.Forms.Button();
			search = new System.Windows.Forms.Button();
			tb_SupplierIdNo = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			tb_vendorId = new System.Windows.Forms.TextBox();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			panel2 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			tb_SupplierName = new System.Windows.Forms.TextBox();
			ucVendorInfo1 = new POS_Client.ucVendorInfo();
			ucVendorInfo2 = new POS_Client.ucVendorInfo();
			ucVendorInfo3 = new POS_Client.ucVendorInfo();
			ucVendorInfo4 = new POS_Client.ucVendorInfo();
			ucVendorInfo5 = new POS_Client.ucVendorInfo();
			ucVendorInfo6 = new POS_Client.ucVendorInfo();
			btn_pageLeft = new System.Windows.Forms.Button();
			btn_pageRight = new System.Windows.Forms.Button();
			l_pageInfo = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			panel3.SuspendLayout();
			SuspendLayout();
			btn_AddNewVendor.BackColor = System.Drawing.Color.FromArgb(57, 176, 192);
			btn_AddNewVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_AddNewVendor.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_AddNewVendor.ForeColor = System.Drawing.Color.White;
			btn_AddNewVendor.Location = new System.Drawing.Point(760, 234);
			btn_AddNewVendor.Name = "btn_AddNewVendor";
			btn_AddNewVendor.Size = new System.Drawing.Size(128, 36);
			btn_AddNewVendor.TabIndex = 10;
			btn_AddNewVendor.Text = "新建廠商";
			btn_AddNewVendor.UseVisualStyleBackColor = false;
			btn_AddNewVendor.Click += new System.EventHandler(btn_AddNewVendor_Click);
			cancel.BackColor = System.Drawing.Color.FromArgb(175, 164, 134);
			cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			cancel.ForeColor = System.Drawing.Color.White;
			cancel.Location = new System.Drawing.Point(577, 234);
			cancel.Name = "cancel";
			cancel.Size = new System.Drawing.Size(94, 36);
			cancel.TabIndex = 9;
			cancel.Text = "取消";
			cancel.UseVisualStyleBackColor = false;
			cancel.Click += new System.EventHandler(cancel_Click);
			reset.BackColor = System.Drawing.Color.FromArgb(175, 164, 134);
			reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			reset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			reset.ForeColor = System.Drawing.Color.White;
			reset.Location = new System.Drawing.Point(477, 234);
			reset.Name = "reset";
			reset.Size = new System.Drawing.Size(94, 36);
			reset.TabIndex = 8;
			reset.Text = "重設";
			reset.UseVisualStyleBackColor = false;
			reset.Click += new System.EventHandler(reset_Click);
			search.BackColor = System.Drawing.Color.FromArgb(167, 202, 0);
			search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			search.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			search.ForeColor = System.Drawing.Color.White;
			search.Location = new System.Drawing.Point(343, 234);
			search.Name = "search";
			search.Size = new System.Drawing.Size(128, 36);
			search.TabIndex = 6;
			search.Text = "查詢";
			search.UseVisualStyleBackColor = false;
			search.Click += new System.EventHandler(search_Click);
			tb_SupplierIdNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SupplierIdNo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_SupplierIdNo.ForeColor = System.Drawing.Color.FromArgb(82, 82, 82);
			tb_SupplierIdNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_SupplierIdNo.Location = new System.Drawing.Point(170, 11);
			tb_SupplierIdNo.Margin = new System.Windows.Forms.Padding(10);
			tb_SupplierIdNo.Name = "tb_SupplierIdNo";
			tb_SupplierIdNo.Size = new System.Drawing.Size(615, 29);
			tb_SupplierIdNo.TabIndex = 3;
			tb_SupplierIdNo.Text = "請輸入廠商統一編號";
			tb_SupplierIdNo.Enter += new System.EventHandler(commodityNum_Enter);
			tb_SupplierIdNo.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_CommodityNum_KeyDown);
			tb_SupplierIdNo.Leave += new System.EventHandler(commodityNum_Leave);
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Black;
			label5.Image = POS_Client.Properties.Resources.oblique;
			label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label5.Location = new System.Drawing.Point(87, 292);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(147, 25);
			label5.TabIndex = 35;
			label5.Text = "   最近出貨廠商";
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80f));
			tableLayoutPanel1.Controls.Add(tb_vendorId, 1, 2);
			tableLayoutPanel1.Controls.Add(panel1, 0, 2);
			tableLayoutPanel1.Controls.Add(panel2, 0, 1);
			tableLayoutPanel1.Controls.Add(panel3, 0, 0);
			tableLayoutPanel1.Controls.Add(tb_SupplierIdNo, 1, 0);
			tableLayoutPanel1.Controls.Add(tb_SupplierName, 1, 1);
			tableLayoutPanel1.Location = new System.Drawing.Point(92, 67);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 3;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel1.Size = new System.Drawing.Size(796, 146);
			tableLayoutPanel1.TabIndex = 58;
			tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(tableLayoutPanel1_Paint);
			tb_vendorId.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_vendorId.Font = new System.Drawing.Font("微軟正黑體", 12f);
			tb_vendorId.ForeColor = System.Drawing.Color.FromArgb(82, 82, 82);
			tb_vendorId.Location = new System.Drawing.Point(170, 107);
			tb_vendorId.Margin = new System.Windows.Forms.Padding(10);
			tb_vendorId.Name = "tb_vendorId";
			tb_vendorId.Size = new System.Drawing.Size(615, 29);
			tb_vendorId.TabIndex = 29;
			tb_vendorId.Text = "請輸入廠商營業執照號碼";
			tb_vendorId.Enter += new System.EventHandler(tb_vendorId_Enter);
			tb_vendorId.Leave += new System.EventHandler(tb_vendorId_Leave);
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(1, 97);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(158, 48);
			panel1.TabIndex = 28;
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(42, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(106, 21);
			label1.TabIndex = 0;
			label1.Text = "營業執照號碼";
			panel2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel2.Controls.Add(label7);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Location = new System.Drawing.Point(1, 49);
			panel2.Margin = new System.Windows.Forms.Padding(0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(158, 47);
			panel2.TabIndex = 26;
			label7.AutoSize = true;
			label7.BackColor = System.Drawing.Color.Transparent;
			label7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(75, 13);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(74, 21);
			label7.TabIndex = 0;
			label7.Text = "廠商名稱";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label4);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 1);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(158, 47);
			panel3.TabIndex = 22;
			label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label4.AutoSize = true;
			label4.BackColor = System.Drawing.Color.Transparent;
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.White;
			label4.Location = new System.Drawing.Point(43, 13);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(106, 21);
			label4.TabIndex = 0;
			label4.Text = "廠商統一編號";
			tb_SupplierName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SupplierName.Font = new System.Drawing.Font("微軟正黑體", 12f);
			tb_SupplierName.ForeColor = System.Drawing.Color.FromArgb(82, 82, 82);
			tb_SupplierName.Location = new System.Drawing.Point(170, 59);
			tb_SupplierName.Margin = new System.Windows.Forms.Padding(10);
			tb_SupplierName.Name = "tb_SupplierName";
			tb_SupplierName.Size = new System.Drawing.Size(615, 29);
			tb_SupplierName.TabIndex = 27;
			tb_SupplierName.Text = "請輸入廠商名稱";
			tb_SupplierName.Enter += new System.EventHandler(tb_SupplierName_Enter);
			tb_SupplierName.Leave += new System.EventHandler(tb_SupplierName_Leave);
			ucVendorInfo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			ucVendorInfo1.Cursor = System.Windows.Forms.Cursors.Hand;
			ucVendorInfo1.Location = new System.Drawing.Point(93, 325);
			ucVendorInfo1.Name = "ucVendorInfo1";
			ucVendorInfo1.Size = new System.Drawing.Size(398, 102);
			ucVendorInfo1.TabIndex = 59;
			ucVendorInfo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			ucVendorInfo2.Cursor = System.Windows.Forms.Cursors.Hand;
			ucVendorInfo2.Location = new System.Drawing.Point(491, 325);
			ucVendorInfo2.Name = "ucVendorInfo2";
			ucVendorInfo2.Size = new System.Drawing.Size(398, 102);
			ucVendorInfo2.TabIndex = 60;
			ucVendorInfo3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			ucVendorInfo3.Cursor = System.Windows.Forms.Cursors.Hand;
			ucVendorInfo3.Location = new System.Drawing.Point(93, 427);
			ucVendorInfo3.Name = "ucVendorInfo3";
			ucVendorInfo3.Size = new System.Drawing.Size(398, 102);
			ucVendorInfo3.TabIndex = 61;
			ucVendorInfo4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			ucVendorInfo4.Cursor = System.Windows.Forms.Cursors.Hand;
			ucVendorInfo4.Location = new System.Drawing.Point(491, 427);
			ucVendorInfo4.Name = "ucVendorInfo4";
			ucVendorInfo4.Size = new System.Drawing.Size(398, 102);
			ucVendorInfo4.TabIndex = 62;
			ucVendorInfo5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			ucVendorInfo5.Cursor = System.Windows.Forms.Cursors.Hand;
			ucVendorInfo5.Location = new System.Drawing.Point(93, 529);
			ucVendorInfo5.Name = "ucVendorInfo5";
			ucVendorInfo5.Size = new System.Drawing.Size(398, 102);
			ucVendorInfo5.TabIndex = 63;
			ucVendorInfo6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			ucVendorInfo6.Cursor = System.Windows.Forms.Cursors.Hand;
			ucVendorInfo6.Location = new System.Drawing.Point(491, 529);
			ucVendorInfo6.Name = "ucVendorInfo6";
			ucVendorInfo6.Size = new System.Drawing.Size(398, 102);
			ucVendorInfo6.TabIndex = 64;
			btn_pageLeft.FlatAppearance.BorderSize = 0;
			btn_pageLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageLeft.Image = POS_Client.Properties.Resources.left;
			btn_pageLeft.Location = new System.Drawing.Point(33, 325);
			btn_pageLeft.Name = "btn_pageLeft";
			btn_pageLeft.Size = new System.Drawing.Size(48, 305);
			btn_pageLeft.TabIndex = 65;
			btn_pageLeft.UseVisualStyleBackColor = true;
			btn_pageLeft.Click += new System.EventHandler(btn_pageLeft_Click);
			btn_pageRight.FlatAppearance.BorderSize = 0;
			btn_pageRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageRight.Image = POS_Client.Properties.Resources.right;
			btn_pageRight.Location = new System.Drawing.Point(901, 325);
			btn_pageRight.Name = "btn_pageRight";
			btn_pageRight.Size = new System.Drawing.Size(48, 306);
			btn_pageRight.TabIndex = 66;
			btn_pageRight.UseVisualStyleBackColor = true;
			btn_pageRight.Click += new System.EventHandler(btn_pageRight_Click);
			l_pageInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_pageInfo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageInfo.Location = new System.Drawing.Point(645, 292);
			l_pageInfo.Name = "l_pageInfo";
			l_pageInfo.Size = new System.Drawing.Size(223, 20);
			l_pageInfo.TabIndex = 58;
			l_pageInfo.Text = "共{0}筆．{1}頁｜目前在第1頁\r\n";
			l_pageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(l_pageInfo);
			base.Controls.Add(btn_pageRight);
			base.Controls.Add(btn_pageLeft);
			base.Controls.Add(ucVendorInfo6);
			base.Controls.Add(ucVendorInfo5);
			base.Controls.Add(ucVendorInfo4);
			base.Controls.Add(ucVendorInfo3);
			base.Controls.Add(ucVendorInfo2);
			base.Controls.Add(ucVendorInfo1);
			base.Controls.Add(btn_AddNewVendor);
			base.Controls.Add(cancel);
			base.Controls.Add(reset);
			base.Controls.Add(search);
			base.Controls.Add(label5);
			base.Controls.Add(tableLayoutPanel1);
			base.Name = "frmChooseVendor";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "選擇廠商";
			base.Controls.SetChildIndex(tableLayoutPanel1, 0);
			base.Controls.SetChildIndex(label5, 0);
			base.Controls.SetChildIndex(search, 0);
			base.Controls.SetChildIndex(reset, 0);
			base.Controls.SetChildIndex(cancel, 0);
			base.Controls.SetChildIndex(btn_AddNewVendor, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(ucVendorInfo1, 0);
			base.Controls.SetChildIndex(ucVendorInfo2, 0);
			base.Controls.SetChildIndex(ucVendorInfo3, 0);
			base.Controls.SetChildIndex(ucVendorInfo4, 0);
			base.Controls.SetChildIndex(ucVendorInfo5, 0);
			base.Controls.SetChildIndex(ucVendorInfo6, 0);
			base.Controls.SetChildIndex(btn_pageLeft, 0);
			base.Controls.SetChildIndex(btn_pageRight, 0);
			base.Controls.SetChildIndex(l_pageInfo, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
