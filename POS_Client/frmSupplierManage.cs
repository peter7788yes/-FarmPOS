using POS_Client.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmSupplierManage : MasterThinForm
	{
		private int pageSize = 8;

		private int pageNow = 1;

		public int pageTotal = 1;

		public DataTable dt;

		private string status = " status = 0 ";

		private string orderByString = " SupplierNo desc";

		private IContainer components;

		private Button btn_pageRight;

		private Button btn_pageLeft;

		private Label l_status;

		private Button btn_statusAll;

		private Button btn_statusNormal;

		private Button btn_statusSuspend;

		private Label l_pageInfo;

		private Button btn_firstPage;

		private Button btn_previousPage;

		private Button btn_nextPage;

		private Button btn_lastPage;

		private Label l_pageNow;

		public Label l_memberList;

		private Button btn_createSupplier;

		private TextBox tb_quickSearch;

		private Label label1;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewLinkColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column5;

		private Button button1;

		public frmSupplierManage()
			: base("廠商管理")
		{
			InitializeComponent();
		}

		public void editSupplier(object sender, EventArgs s)
		{
			switchForm(new frmEditSupplier((sender as Label).Text));
		}

		private void frmSupplierManage_Load(object sender, EventArgs e)
		{
			btn_statusAll_Click(sender, e);
		}

		private string getWhereString()
		{
			if (!string.IsNullOrEmpty(status) && !"請輸入名稱或統一編號".Equals(tb_quickSearch.Text))
			{
				return status + " and (SupplierIdNo like '%" + tb_quickSearch.Text + "%' or SupplierName like '%" + tb_quickSearch.Text + "%')";
			}
			if (!string.IsNullOrEmpty(status))
			{
				return status;
			}
			if (!"請輸入名稱或統一編號".Equals(tb_quickSearch.Text))
			{
				return "SupplierIdNo like '%" + tb_quickSearch.Text + "%' or SupplierName like '%" + tb_quickSearch.Text + "%'";
			}
			return "";
		}

		private void btn_createSupplier_Click(object sender, EventArgs e)
		{
			switchForm(new frmNewSupplier());
		}

		public void changePage(int page)
		{
			dataGridView1.Rows.Clear();
			int num = 0;
			pageNow = page;
			for (int i = (pageNow - 1) * pageSize; i < pageNow * pageSize; i++)
			{
				if (i < dt.Rows.Count)
				{
					string text = DateTime.ParseExact(dt.Rows[i]["CreateDate"].ToString(), "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.AllowWhiteSpaces).ToString("yyyy-MM-dd");
					string text2 = "0".Equals(dt.Rows[i]["Status"].ToString()) ? "正常" : "停用";
					dataGridView1.Rows.Insert(num, text, dt.Rows[i]["SupplierNo"].ToString(), dt.Rows[i]["SupplierName"].ToString(), dt.Rows[i]["SupplierIdNo"].ToString(), text2);
					num++;
				}
			}
			foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
			{
				item.Height = 48;
			}
			pageTotal = (int)Math.Ceiling((double)dt.Rows.Count / (double)pageSize);
			l_pageNow.Text = string.Format("{0}", pageNow);
			l_pageInfo.Text = string.Format("共{0}筆．{1}頁｜目前在第{2}頁", dt.Rows.Count, Math.Ceiling((double)dt.Rows.Count / (double)pageSize), pageNow);
		}

		private void btn_statusAll_Click(object sender, EventArgs e)
		{
			status = "";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "CreateDate,SupplierNo,SupplierName,SupplierIdNo,Status", "hypos_Supplier", getWhereString(), orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
			btn_statusAll.ForeColor = Color.White;
			btn_statusAll.BackColor = Color.FromArgb(247, 106, 45);
			btn_statusNormal.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusNormal.BackColor = Color.White;
			btn_statusSuspend.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusSuspend.BackColor = Color.White;
		}

		private void btn_statusNormal_Click(object sender, EventArgs e)
		{
			status = " status = 0 ";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "CreateDate,SupplierNo,SupplierName,SupplierIdNo,Status", "hypos_Supplier", getWhereString(), orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
			btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusAll.BackColor = Color.White;
			btn_statusNormal.ForeColor = Color.White;
			btn_statusNormal.BackColor = Color.FromArgb(247, 106, 45);
			btn_statusSuspend.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusSuspend.BackColor = Color.White;
		}

		private void btn_statusSuspend_Click(object sender, EventArgs e)
		{
			status = " status = 1 ";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "CreateDate,SupplierNo,SupplierName,SupplierIdNo,Status", "hypos_Supplier", getWhereString(), orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
			btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusAll.BackColor = Color.White;
			btn_statusNormal.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusNormal.BackColor = Color.White;
			btn_statusSuspend.ForeColor = Color.White;
			btn_statusSuspend.BackColor = Color.FromArgb(247, 106, 45);
		}

		private void btn_pageRight_Click(object sender, EventArgs e)
		{
			if (pageNow < pageTotal)
			{
				changePage(pageNow + 1);
			}
		}

		private void btn_pageLeft_Click(object sender, EventArgs e)
		{
			if (pageNow > 1)
			{
				changePage(pageNow - 1);
			}
		}

		private void btn_lastPage_Click(object sender, EventArgs e)
		{
			if (pageNow < pageTotal)
			{
				changePage(pageTotal);
			}
		}

		private void btn_firstPage_Click(object sender, EventArgs e)
		{
			if (pageNow > 1)
			{
				changePage(1);
			}
		}

		private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			if (e.Row == 0)
			{
				e.Graphics.FillRectangle(Brushes.DarkGray, e.CellBounds);
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1 && e.RowIndex >= 0)
			{
				string supplierNo = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
				switchForm(new frmEditSupplier(supplierNo));
			}
		}

		private void l_quickSearch_Click(object sender, EventArgs e)
		{
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "CreateDate,SupplierNo,SupplierName,SupplierIdNo,Status", "hypos_Supplier", getWhereString(), orderByString, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
		}

		private void tb_quickSearch_Enter(object sender, EventArgs e)
		{
			if ("請輸入名稱或統一編號".Equals(tb_quickSearch.Text))
			{
				tb_quickSearch.Text = "";
			}
		}

		private void tb_quickSearch_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_quickSearch.Text))
			{
				tb_quickSearch.Text = "請輸入名稱或統一編號";
			}
		}

		private void tb_quickSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				l_quickSearch_Click(sender, e);
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
			btn_pageRight = new System.Windows.Forms.Button();
			btn_pageLeft = new System.Windows.Forms.Button();
			l_status = new System.Windows.Forms.Label();
			btn_statusAll = new System.Windows.Forms.Button();
			btn_statusNormal = new System.Windows.Forms.Button();
			btn_statusSuspend = new System.Windows.Forms.Button();
			l_memberList = new System.Windows.Forms.Label();
			l_pageInfo = new System.Windows.Forms.Label();
			btn_firstPage = new System.Windows.Forms.Button();
			btn_previousPage = new System.Windows.Forms.Button();
			btn_nextPage = new System.Windows.Forms.Button();
			btn_lastPage = new System.Windows.Forms.Button();
			l_pageNow = new System.Windows.Forms.Label();
			btn_createSupplier = new System.Windows.Forms.Button();
			tb_quickSearch = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewLinkColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			btn_pageRight.FlatAppearance.BorderSize = 0;
			btn_pageRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageRight.Image = POS_Client.Properties.Resources.right;
			btn_pageRight.Location = new System.Drawing.Point(932, 186);
			btn_pageRight.Name = "btn_pageRight";
			btn_pageRight.Size = new System.Drawing.Size(48, 371);
			btn_pageRight.TabIndex = 41;
			btn_pageRight.UseVisualStyleBackColor = true;
			btn_pageRight.Click += new System.EventHandler(btn_pageRight_Click);
			btn_pageLeft.FlatAppearance.BorderSize = 0;
			btn_pageLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageLeft.Image = POS_Client.Properties.Resources.left;
			btn_pageLeft.Location = new System.Drawing.Point(0, 186);
			btn_pageLeft.Name = "btn_pageLeft";
			btn_pageLeft.Size = new System.Drawing.Size(48, 378);
			btn_pageLeft.TabIndex = 42;
			btn_pageLeft.UseVisualStyleBackColor = true;
			btn_pageLeft.Click += new System.EventHandler(btn_pageLeft_Click);
			l_status.AutoSize = true;
			l_status.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_status.Location = new System.Drawing.Point(55, 91);
			l_status.Name = "l_status";
			l_status.Size = new System.Drawing.Size(41, 20);
			l_status.TabIndex = 43;
			l_status.Text = "狀態";
			btn_statusAll.BackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusAll.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusAll.ForeColor = System.Drawing.Color.White;
			btn_statusAll.Location = new System.Drawing.Point(104, 87);
			btn_statusAll.Name = "btn_statusAll";
			btn_statusAll.Size = new System.Drawing.Size(69, 29);
			btn_statusAll.TabIndex = 44;
			btn_statusAll.Text = "全部";
			btn_statusAll.UseVisualStyleBackColor = false;
			btn_statusAll.Click += new System.EventHandler(btn_statusAll_Click);
			btn_statusNormal.BackColor = System.Drawing.Color.White;
			btn_statusNormal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusNormal.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusNormal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusNormal.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusNormal.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusNormal.Location = new System.Drawing.Point(181, 87);
			btn_statusNormal.Name = "btn_statusNormal";
			btn_statusNormal.Size = new System.Drawing.Size(69, 29);
			btn_statusNormal.TabIndex = 45;
			btn_statusNormal.Text = "正常";
			btn_statusNormal.UseVisualStyleBackColor = false;
			btn_statusNormal.Click += new System.EventHandler(btn_statusNormal_Click);
			btn_statusSuspend.BackColor = System.Drawing.Color.White;
			btn_statusSuspend.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusSuspend.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusSuspend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusSuspend.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusSuspend.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusSuspend.Location = new System.Drawing.Point(258, 87);
			btn_statusSuspend.Name = "btn_statusSuspend";
			btn_statusSuspend.Size = new System.Drawing.Size(69, 29);
			btn_statusSuspend.TabIndex = 46;
			btn_statusSuspend.Text = "停用";
			btn_statusSuspend.UseVisualStyleBackColor = false;
			btn_statusSuspend.Click += new System.EventHandler(btn_statusSuspend_Click);
			l_memberList.AutoSize = true;
			l_memberList.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_memberList.Image = POS_Client.Properties.Resources.oblique;
			l_memberList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_memberList.Location = new System.Drawing.Point(55, 47);
			l_memberList.Name = "l_memberList";
			l_memberList.Size = new System.Drawing.Size(82, 24);
			l_memberList.TabIndex = 51;
			l_memberList.Text = "   供應商";
			l_memberList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			l_pageInfo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageInfo.Location = new System.Drawing.Point(427, 51);
			l_pageInfo.Name = "l_pageInfo";
			l_pageInfo.Size = new System.Drawing.Size(386, 20);
			l_pageInfo.TabIndex = 58;
			l_pageInfo.Text = "共{0}筆．{1}頁｜目前在第1頁\r\n";
			l_pageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			btn_firstPage.BackColor = System.Drawing.Color.White;
			btn_firstPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_firstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_firstPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_firstPage.Image = POS_Client.Properties.Resources.first;
			btn_firstPage.Location = new System.Drawing.Point(309, 588);
			btn_firstPage.Name = "btn_firstPage";
			btn_firstPage.Size = new System.Drawing.Size(69, 29);
			btn_firstPage.TabIndex = 59;
			btn_firstPage.UseVisualStyleBackColor = false;
			btn_firstPage.Click += new System.EventHandler(btn_firstPage_Click);
			btn_previousPage.BackColor = System.Drawing.Color.White;
			btn_previousPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_previousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_previousPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_previousPage.Image = POS_Client.Properties.Resources.prev;
			btn_previousPage.Location = new System.Drawing.Point(384, 588);
			btn_previousPage.Name = "btn_previousPage";
			btn_previousPage.Size = new System.Drawing.Size(69, 29);
			btn_previousPage.TabIndex = 60;
			btn_previousPage.UseVisualStyleBackColor = false;
			btn_previousPage.Click += new System.EventHandler(btn_pageLeft_Click);
			btn_nextPage.BackColor = System.Drawing.Color.White;
			btn_nextPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_nextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_nextPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_nextPage.Image = POS_Client.Properties.Resources.next;
			btn_nextPage.Location = new System.Drawing.Point(532, 588);
			btn_nextPage.Name = "btn_nextPage";
			btn_nextPage.Size = new System.Drawing.Size(69, 29);
			btn_nextPage.TabIndex = 62;
			btn_nextPage.UseVisualStyleBackColor = false;
			btn_nextPage.Click += new System.EventHandler(btn_pageRight_Click);
			btn_lastPage.BackColor = System.Drawing.Color.White;
			btn_lastPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_lastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_lastPage.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_lastPage.Image = POS_Client.Properties.Resources.last;
			btn_lastPage.Location = new System.Drawing.Point(607, 588);
			btn_lastPage.Name = "btn_lastPage";
			btn_lastPage.Size = new System.Drawing.Size(69, 29);
			btn_lastPage.TabIndex = 63;
			btn_lastPage.UseVisualStyleBackColor = false;
			btn_lastPage.Click += new System.EventHandler(btn_lastPage_Click);
			l_pageNow.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			l_pageNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			l_pageNow.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageNow.ForeColor = System.Drawing.Color.White;
			l_pageNow.Location = new System.Drawing.Point(459, 588);
			l_pageNow.Name = "l_pageNow";
			l_pageNow.Size = new System.Drawing.Size(67, 29);
			l_pageNow.TabIndex = 64;
			l_pageNow.Text = "{0}";
			l_pageNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			btn_createSupplier.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_createSupplier.FlatAppearance.BorderSize = 0;
			btn_createSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_createSupplier.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_createSupplier.ForeColor = System.Drawing.Color.White;
			btn_createSupplier.Location = new System.Drawing.Point(823, 47);
			btn_createSupplier.Name = "btn_createSupplier";
			btn_createSupplier.Size = new System.Drawing.Size(102, 30);
			btn_createSupplier.TabIndex = 57;
			btn_createSupplier.Text = "新建供應商";
			btn_createSupplier.UseVisualStyleBackColor = false;
			btn_createSupplier.Click += new System.EventHandler(btn_createSupplier_Click);
			tb_quickSearch.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_quickSearch.ForeColor = System.Drawing.Color.DarkGray;
			tb_quickSearch.Location = new System.Drawing.Point(484, 87);
			tb_quickSearch.Name = "tb_quickSearch";
			tb_quickSearch.Size = new System.Drawing.Size(209, 29);
			tb_quickSearch.TabIndex = 68;
			tb_quickSearch.Text = "請輸入名稱或統一編號";
			tb_quickSearch.Enter += new System.EventHandler(tb_quickSearch_Enter);
			tb_quickSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_quickSearch_KeyDown);
			tb_quickSearch.Leave += new System.EventHandler(tb_quickSearch_Leave);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.Location = new System.Drawing.Point(357, 91);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(121, 20);
			label1.TabIndex = 69;
			label1.Text = "供應商快速搜尋";
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.BackgroundColor = System.Drawing.Color.White;
			dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column4, Column5);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.Location = new System.Drawing.Point(59, 142);
			dataGridView1.Margin = new System.Windows.Forms.Padding(0);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 40;
			dataGridView1.Size = new System.Drawing.Size(866, 422);
			dataGridView1.TabIndex = 70;
			dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
			Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			Column1.DefaultCellStyle = dataGridViewCellStyle4;
			Column1.HeaderText = "建置日期";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Blue;
			Column2.DefaultCellStyle = dataGridViewCellStyle5;
			Column2.HeaderText = "系統編號";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("微軟正黑體", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
			Column3.DefaultCellStyle = dataGridViewCellStyle6;
			Column3.FillWeight = 200f;
			Column3.HeaderText = "進貨(供應)廠商名稱";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("微軟正黑體", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
			Column4.DefaultCellStyle = dataGridViewCellStyle7;
			Column4.HeaderText = "統一編號";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("微軟正黑體", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
			Column5.DefaultCellStyle = dataGridViewCellStyle8;
			Column5.FillWeight = 60f;
			Column5.HeaderText = "狀態";
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			button1.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.ForeColor = System.Drawing.Color.White;
			button1.Location = new System.Drawing.Point(708, 85);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(58, 30);
			button1.TabIndex = 46;
			button1.Text = "GO";
			button1.UseVisualStyleBackColor = false;
			button1.Click += new System.EventHandler(l_quickSearch_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(dataGridView1);
			base.Controls.Add(label1);
			base.Controls.Add(tb_quickSearch);
			base.Controls.Add(btn_createSupplier);
			base.Controls.Add(l_pageNow);
			base.Controls.Add(btn_lastPage);
			base.Controls.Add(btn_nextPage);
			base.Controls.Add(btn_previousPage);
			base.Controls.Add(btn_firstPage);
			base.Controls.Add(l_pageInfo);
			base.Controls.Add(l_memberList);
			base.Controls.Add(button1);
			base.Controls.Add(btn_statusSuspend);
			base.Controls.Add(btn_statusNormal);
			base.Controls.Add(btn_statusAll);
			base.Controls.Add(l_status);
			base.Controls.Add(btn_pageLeft);
			base.Controls.Add(btn_pageRight);
			base.Name = "frmSupplierManage";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmSupplierManage_Load);
			base.Controls.SetChildIndex(btn_pageRight, 0);
			base.Controls.SetChildIndex(btn_pageLeft, 0);
			base.Controls.SetChildIndex(l_status, 0);
			base.Controls.SetChildIndex(btn_statusAll, 0);
			base.Controls.SetChildIndex(btn_statusNormal, 0);
			base.Controls.SetChildIndex(btn_statusSuspend, 0);
			base.Controls.SetChildIndex(button1, 0);
			base.Controls.SetChildIndex(l_memberList, 0);
			base.Controls.SetChildIndex(l_pageInfo, 0);
			base.Controls.SetChildIndex(btn_firstPage, 0);
			base.Controls.SetChildIndex(btn_previousPage, 0);
			base.Controls.SetChildIndex(btn_nextPage, 0);
			base.Controls.SetChildIndex(btn_lastPage, 0);
			base.Controls.SetChildIndex(l_pageNow, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(btn_createSupplier, 0);
			base.Controls.SetChildIndex(tb_quickSearch, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(dataGridView1, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
