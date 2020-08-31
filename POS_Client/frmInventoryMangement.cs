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
	public class frmInventoryMangement : MasterThinForm
	{
		private int pageSize = 8;

		private int pageNow = 1;

		public int pageTotal = 1;

		public DataTable dt;

		private string status = "";

		private string selectStr = "a.UpdateDate,a.Status,a.PurchaseNo,a.Total,a.PurchaseDate,b.SupplierName";

		private string orderByStr = "a.CreateDate DESC";

		private IContainer components;

		private Button btn_pageRight;

		private Button btn_pageLeft;

		private Label l_status;

		private Button btn_statusAll;

		private Button btn_statusNormal;

		private Button btn_statusCancel;

		private Button btn_quickManager;

		private Label l_pageInfo;

		private Button btn_firstPage;

		private Button btn_previousPage;

		private Button btn_nextPage;

		private Button btn_lastPage;

		private Label l_pageNow;

		public Label l_memberList;

		private Button btn_SearchPurchase;

		private Button btn_CreatePurchase;

		private Label label1;

		private TextBox tb_supplierKeyword;

		private Button btn_supplierFilter;

		public DataGridView dataGridView1;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewLinkColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column5;

		private DataGridViewTextBoxColumn hidden_Status;

		public frmInventoryMangement()
			: base("進貨管理")
		{
			InitializeComponent();
		}

		private string getWhereStr()
		{
			string text = tb_supplierKeyword.Text;
			if (!string.IsNullOrEmpty(status) && !"請輸入名稱或統一編號".Equals(text))
			{
				return string.Format("a.SupplierNo = b.SupplierNo and a.status in ({0}) and (b.SupplierName like {1} or b.SupplierIdNo like {1})", status, "'%" + text + "%'");
			}
			if (!string.IsNullOrEmpty(status))
			{
				return string.Format("a.SupplierNo = b.SupplierNo and a.status in ({0})", status);
			}
			if (!"請輸入名稱或統一編號".Equals(text))
			{
				return string.Format("a.SupplierNo = b.SupplierNo and (b.SupplierName like {0} or b.SupplierIdNo like {0})", "'%" + text + "%'");
			}
			return "a.SupplierNo = b.SupplierNo";
		}

		private void frmInventoryMangement_Load(object sender, EventArgs e)
		{
			btn_statusAll_Click(sender, e);
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
					string text = dt.Rows[i]["Status"].ToString();
					switch (text)
					{
					case "0":
						text = "正常";
						break;
					case "1":
						text = "正常(變更)";
						break;
					case "2":
						text = "取消";
						break;
					}
					dataGridView1.Rows.Insert(num, dt.Rows[i]["PurchaseDate"].ToString(), dt.Rows[i]["PurchaseNo"].ToString(), dt.Rows[i]["SupplierName"].ToString(), dt.Rows[i]["Total"].ToString(), text, dt.Rows[i]["Status"].ToString());
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
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectStr, "hypos_PurchaseGoods_Master a, hypos_Supplier b", getWhereStr(), orderByStr, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			pageTotal = (int)Math.Ceiling((double)dt.Rows.Count / (double)pageSize);
			changePage(1);
			btn_statusAll.ForeColor = Color.White;
			btn_statusAll.BackColor = Color.FromArgb(247, 106, 45);
			btn_statusNormal.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusNormal.BackColor = Color.White;
			btn_statusCancel.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusCancel.BackColor = Color.White;
		}

		private void btn_statusNormal_Click(object sender, EventArgs e)
		{
			status = "'0','1'";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectStr, "hypos_PurchaseGoods_Master a, hypos_Supplier b", getWhereStr(), orderByStr, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			pageTotal = (int)Math.Ceiling((double)dt.Rows.Count / (double)pageSize);
			changePage(1);
			btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusAll.BackColor = Color.White;
			btn_statusNormal.ForeColor = Color.White;
			btn_statusNormal.BackColor = Color.FromArgb(247, 106, 45);
			btn_statusCancel.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusCancel.BackColor = Color.White;
		}

		private void btn_statusSuspend_Click(object sender, EventArgs e)
		{
			status = "'2'";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectStr, "hypos_PurchaseGoods_Master a, hypos_Supplier b", getWhereStr(), orderByStr, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
			btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusAll.BackColor = Color.White;
			btn_statusNormal.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusNormal.BackColor = Color.White;
			btn_statusCancel.ForeColor = Color.White;
			btn_statusCancel.BackColor = Color.FromArgb(247, 106, 45);
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

		private void btn_createCommodity_Click(object sender, EventArgs e)
		{
			switchForm(new frmNewInventory());
		}

		private void tb_supplierKeyword_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_supplierKeyword.Text))
			{
				tb_supplierKeyword.Text = "請輸入名稱或統一編號";
			}
		}

		private void tb_supplierKeyword_Enter(object sender, EventArgs e)
		{
			if ("請輸入名稱或統一編號".Equals(tb_supplierKeyword.Text))
			{
				tb_supplierKeyword.Text = "";
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1 && e.RowIndex >= 0)
			{
				string purchaseNo = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
				string value = dataGridView1["hidden_Status", e.RowIndex].Value.ToString();
				if ("2".Equals(value))
				{
					MessageBox.Show("進貨單已取消，無法編修");
				}
				else
				{
					switchForm(new frmEditInventory(purchaseNo), this);
				}
			}
		}

		private void tb_supplierKeyword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				btn_supplierFilter_Click(sender, e);
			}
		}

		private void btn_supplierFilter_Click(object sender, EventArgs e)
		{
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, selectStr, "hypos_PurchaseGoods_Master a, hypos_Supplier b", getWhereStr(), orderByStr, null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			pageTotal = (int)Math.Ceiling((double)dt.Rows.Count / (double)pageSize);
			changePage(1);
		}

		private void btn_quickManager_Click(object sender, EventArgs e)
		{
			switchForm(new frmInventoryQuickEdit());
		}

		private void btn_SearchPurchase_Click(object sender, EventArgs e)
		{
			tb_supplierKeyword.Text = "請輸入名稱或統一編號";
			btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusAll.BackColor = Color.White;
			btn_statusNormal.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusNormal.BackColor = Color.White;
			btn_statusCancel.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusCancel.BackColor = Color.White;
			switchForm(new frmSearchPurchase(), this);
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
			btn_statusCancel = new System.Windows.Forms.Button();
			l_memberList = new System.Windows.Forms.Label();
			btn_quickManager = new System.Windows.Forms.Button();
			l_pageInfo = new System.Windows.Forms.Label();
			btn_firstPage = new System.Windows.Forms.Button();
			btn_previousPage = new System.Windows.Forms.Button();
			btn_nextPage = new System.Windows.Forms.Button();
			btn_lastPage = new System.Windows.Forms.Button();
			l_pageNow = new System.Windows.Forms.Label();
			btn_SearchPurchase = new System.Windows.Forms.Button();
			btn_CreatePurchase = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			tb_supplierKeyword = new System.Windows.Forms.TextBox();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewLinkColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			hidden_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
			btn_supplierFilter = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();
			btn_pageRight.FlatAppearance.BorderSize = 0;
			btn_pageRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageRight.Image = POS_Client.Properties.Resources.right;
			btn_pageRight.Location = new System.Drawing.Point(933, 185);
			btn_pageRight.Name = "btn_pageRight";
			btn_pageRight.Size = new System.Drawing.Size(48, 371);
			btn_pageRight.TabIndex = 41;
			btn_pageRight.UseVisualStyleBackColor = true;
			btn_pageRight.Click += new System.EventHandler(btn_pageRight_Click);
			btn_pageLeft.FlatAppearance.BorderSize = 0;
			btn_pageLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageLeft.Image = POS_Client.Properties.Resources.left;
			btn_pageLeft.Location = new System.Drawing.Point(0, 185);
			btn_pageLeft.Name = "btn_pageLeft";
			btn_pageLeft.Size = new System.Drawing.Size(48, 378);
			btn_pageLeft.TabIndex = 42;
			btn_pageLeft.UseVisualStyleBackColor = true;
			btn_pageLeft.Click += new System.EventHandler(btn_pageLeft_Click);
			l_status.AutoSize = true;
			l_status.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_status.Location = new System.Drawing.Point(47, 94);
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
			btn_statusAll.Location = new System.Drawing.Point(96, 90);
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
			btn_statusNormal.Location = new System.Drawing.Point(173, 90);
			btn_statusNormal.Name = "btn_statusNormal";
			btn_statusNormal.Size = new System.Drawing.Size(69, 29);
			btn_statusNormal.TabIndex = 45;
			btn_statusNormal.Text = "正常";
			btn_statusNormal.UseVisualStyleBackColor = false;
			btn_statusNormal.Click += new System.EventHandler(btn_statusNormal_Click);
			btn_statusCancel.BackColor = System.Drawing.Color.White;
			btn_statusCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusCancel.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusCancel.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusCancel.Location = new System.Drawing.Point(250, 90);
			btn_statusCancel.Name = "btn_statusCancel";
			btn_statusCancel.Size = new System.Drawing.Size(69, 29);
			btn_statusCancel.TabIndex = 46;
			btn_statusCancel.Text = "已取消";
			btn_statusCancel.UseVisualStyleBackColor = false;
			btn_statusCancel.Click += new System.EventHandler(btn_statusSuspend_Click);
			l_memberList.AutoSize = true;
			l_memberList.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_memberList.Image = POS_Client.Properties.Resources.oblique;
			l_memberList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_memberList.Location = new System.Drawing.Point(47, 49);
			l_memberList.Name = "l_memberList";
			l_memberList.Size = new System.Drawing.Size(82, 24);
			l_memberList.TabIndex = 51;
			l_memberList.Text = "   進貨單";
			l_memberList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			btn_quickManager.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_quickManager.FlatAppearance.BorderSize = 0;
			btn_quickManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_quickManager.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_quickManager.ForeColor = System.Drawing.Color.White;
			btn_quickManager.Location = new System.Drawing.Point(805, 91);
			btn_quickManager.Name = "btn_quickManager";
			btn_quickManager.Size = new System.Drawing.Size(115, 30);
			btn_quickManager.TabIndex = 57;
			btn_quickManager.Text = "庫存快速管理";
			btn_quickManager.UseVisualStyleBackColor = false;
			btn_quickManager.Visible = false;
			btn_quickManager.Click += new System.EventHandler(btn_quickManager_Click);
			l_pageInfo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageInfo.Location = new System.Drawing.Point(302, 52);
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
			btn_SearchPurchase.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_SearchPurchase.FlatAppearance.BorderSize = 0;
			btn_SearchPurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SearchPurchase.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SearchPurchase.ForeColor = System.Drawing.Color.White;
			btn_SearchPurchase.Location = new System.Drawing.Point(818, 47);
			btn_SearchPurchase.Name = "btn_SearchPurchase";
			btn_SearchPurchase.Size = new System.Drawing.Size(102, 30);
			btn_SearchPurchase.TabIndex = 57;
			btn_SearchPurchase.Text = "進貨單查詢";
			btn_SearchPurchase.UseVisualStyleBackColor = false;
			btn_SearchPurchase.Click += new System.EventHandler(btn_SearchPurchase_Click);
			btn_CreatePurchase.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_CreatePurchase.FlatAppearance.BorderSize = 0;
			btn_CreatePurchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_CreatePurchase.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_CreatePurchase.ForeColor = System.Drawing.Color.White;
			btn_CreatePurchase.Location = new System.Drawing.Point(702, 47);
			btn_CreatePurchase.Name = "btn_CreatePurchase";
			btn_CreatePurchase.Size = new System.Drawing.Size(102, 30);
			btn_CreatePurchase.TabIndex = 57;
			btn_CreatePurchase.Text = "新建進貨單";
			btn_CreatePurchase.UseVisualStyleBackColor = false;
			btn_CreatePurchase.Click += new System.EventHandler(btn_createCommodity_Click);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.Location = new System.Drawing.Point(337, 94);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(89, 20);
			label1.TabIndex = 43;
			label1.Text = "供應商篩選";
			tb_supplierKeyword.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_supplierKeyword.Location = new System.Drawing.Point(433, 90);
			tb_supplierKeyword.Name = "tb_supplierKeyword";
			tb_supplierKeyword.Size = new System.Drawing.Size(209, 29);
			tb_supplierKeyword.TabIndex = 66;
			tb_supplierKeyword.Text = "請輸入名稱或統一編號";
			tb_supplierKeyword.Enter += new System.EventHandler(tb_supplierKeyword_Enter);
			tb_supplierKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_supplierKeyword_KeyDown);
			tb_supplierKeyword.Leave += new System.EventHandler(tb_supplierKeyword_Leave);
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
			dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column4, Column5, hidden_Status);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("新細明體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.Location = new System.Drawing.Point(54, 141);
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
			dataGridView1.TabIndex = 71;
			dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
			Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			Column1.DefaultCellStyle = dataGridViewCellStyle4;
			Column1.FillWeight = 100.7162f;
			Column1.HeaderText = "進貨日期";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Blue;
			Column2.DefaultCellStyle = dataGridViewCellStyle5;
			Column2.FillWeight = 100.7162f;
			Column2.HeaderText = "系統單號";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("微軟正黑體", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
			Column3.DefaultCellStyle = dataGridViewCellStyle6;
			Column3.FillWeight = 201.4324f;
			Column3.HeaderText = "進貨(供應)廠商名稱";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("微軟正黑體", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
			Column4.DefaultCellStyle = dataGridViewCellStyle7;
			Column4.FillWeight = 83.22656f;
			Column4.HeaderText = "單據總價";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("微軟正黑體", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
			Column5.DefaultCellStyle = dataGridViewCellStyle8;
			Column5.FillWeight = 73.90863f;
			Column5.HeaderText = "狀態";
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			hidden_Status.HeaderText = "(隱藏_訂單狀態)";
			hidden_Status.Name = "hidden_Status";
			hidden_Status.ReadOnly = true;
			hidden_Status.Visible = false;
			btn_supplierFilter.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_supplierFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_supplierFilter.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_supplierFilter.ForeColor = System.Drawing.Color.White;
			btn_supplierFilter.Location = new System.Drawing.Point(648, 90);
			btn_supplierFilter.Name = "btn_supplierFilter";
			btn_supplierFilter.Size = new System.Drawing.Size(48, 29);
			btn_supplierFilter.TabIndex = 72;
			btn_supplierFilter.Text = "GO";
			btn_supplierFilter.UseVisualStyleBackColor = false;
			btn_supplierFilter.Click += new System.EventHandler(btn_supplierFilter_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(btn_supplierFilter);
			base.Controls.Add(dataGridView1);
			base.Controls.Add(tb_supplierKeyword);
			base.Controls.Add(btn_CreatePurchase);
			base.Controls.Add(btn_SearchPurchase);
			base.Controls.Add(btn_quickManager);
			base.Controls.Add(l_pageNow);
			base.Controls.Add(btn_lastPage);
			base.Controls.Add(btn_nextPage);
			base.Controls.Add(btn_previousPage);
			base.Controls.Add(btn_firstPage);
			base.Controls.Add(l_pageInfo);
			base.Controls.Add(l_memberList);
			base.Controls.Add(btn_statusCancel);
			base.Controls.Add(btn_statusNormal);
			base.Controls.Add(btn_statusAll);
			base.Controls.Add(label1);
			base.Controls.Add(l_status);
			base.Controls.Add(btn_pageLeft);
			base.Controls.Add(btn_pageRight);
			base.Name = "frmInventoryMangement";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmInventoryMangement_Load);
			base.Controls.SetChildIndex(btn_pageRight, 0);
			base.Controls.SetChildIndex(btn_pageLeft, 0);
			base.Controls.SetChildIndex(l_status, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(btn_statusAll, 0);
			base.Controls.SetChildIndex(btn_statusNormal, 0);
			base.Controls.SetChildIndex(btn_statusCancel, 0);
			base.Controls.SetChildIndex(l_memberList, 0);
			base.Controls.SetChildIndex(l_pageInfo, 0);
			base.Controls.SetChildIndex(btn_firstPage, 0);
			base.Controls.SetChildIndex(btn_previousPage, 0);
			base.Controls.SetChildIndex(btn_nextPage, 0);
			base.Controls.SetChildIndex(btn_lastPage, 0);
			base.Controls.SetChildIndex(l_pageNow, 0);
			base.Controls.SetChildIndex(btn_quickManager, 0);
			base.Controls.SetChildIndex(btn_SearchPurchase, 0);
			base.Controls.SetChildIndex(btn_CreatePurchase, 0);
			base.Controls.SetChildIndex(tb_supplierKeyword, 0);
			base.Controls.SetChildIndex(dataGridView1, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(btn_supplierFilter, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
