using POS_Client.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmSearchSellResult_View : MasterThinForm
	{
		private ucSellInfo[] ucsells;

		private int pageNow = 1;

		public int pageTotal = 1;

		public DataTable dt;

		private string status = "";

		private IContainer components;

		private ucSellInfo uC_sell2;

		private ucSellInfo uC_sell3;

		private ucSellInfo uC_sell4;

		private ucSellInfo uC_sell5;

		private ucSellInfo uC_sell6;

		private ucSellInfo uC_sell7;

		private ucSellInfo uC_sell8;

		private Button btn_pageRight;

		private Button btn_pageLeft;

		private Label l_status;

		private Button btn_statusAll;

		private Button btn_today;

		private Button btn_memberSearch;

		private Label l_pageInfo;

		private Button btn_firstPage;

		private Button btn_previousPage;

		private Button btn_nextPage;

		private Button btn_lastPage;

		private Label l_pageNow;

		public Label l_memberList;

		private ucSellInfo uC_sell1;

		private Button btn_month;

		private Button btn_week;

		private Button btn_lastMonth;

		private Label label_salesDate;

		private Label text_salesDate;

		public frmSearchSellResult_View(DataTable dt)
			: base("銷售單|退貨|補印收據")
		{
			InitializeComponent();
			ucsells = new ucSellInfo[8]
			{
				uC_sell1,
				uC_sell2,
				uC_sell3,
				uC_sell4,
				uC_sell5,
				uC_sell6,
				uC_sell7,
				uC_sell8
			};
			this.dt = dt;
			ucSellInfo[] array = ucsells;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnClickMember += new EventHandler(viewMemberInfo);
			}
		}

		public void viewMemberInfo(object sellNumber, EventArgs s)
		{
			frmMainShopSimpleReturnWithMoney frmMainShopSimpleReturnWithMoney = new frmMainShopSimpleReturnWithMoney(sellNumber.ToString(), "frmSearchSellResult", "");
			frmMainShopSimpleReturnWithMoney.frmName = base.Name;
			frmMainShopSimpleReturnWithMoney.Location = new Point(base.Location.X, base.Location.Y);
			frmMainShopSimpleReturnWithMoney.Show();
			frmMainShopSimpleReturnWithMoney.Focus();
		}

		private void frmMemberMangement_Load(object sender, EventArgs e)
		{
			pageTotal = (int)Math.Ceiling((double)dt.Rows.Count / 8.0);
			changePage(1);
		}

		public void changePage(int page)
		{
			int num = 0;
			pageNow = page;
			for (int i = (pageNow - 1) * 8; i < pageNow * 8; i++)
			{
				if (i < dt.Rows.Count)
				{
					if (!string.IsNullOrEmpty(dt.Rows[i]["Name"].ToString()))
					{
						ucsells[num].setMemberName(dt.Rows[i]["Name"].ToString());
					}
					else
					{
						ucsells[num].setMemberName("非會員");
					}
					ucsells[num].setsellNo(dt.Rows[i]["sellNo"].ToString());
					ucsells[num].setsellDate("銷售日期: " + dt.Rows[i]["sellTime"].ToString());
					ucsells[num].setcellphone(dt.Rows[i]["Mobile"].ToString());
					ucsells[num].setmemberNo("會員號: " + dt.Rows[i]["memberId"].ToString());
					ucsells[num].setitems("購買品項: " + dt.Rows[i]["items"].ToString());
					ucsells[num].setSum("消費總額: " + dt.Rows[i]["sum"].ToString());
					int num2 = 0;
					num2 = ((dt.Rows[i]["Credit"].ToString() != null || dt.Rows[i]["Credit"].ToString() != "") ? int.Parse(dt.Rows[i]["Credit"].ToString()) : 0);
					if (num2 >= 1)
					{
						ucsells[num].setPayType("付款模式: 賒帳(" + dt.Rows[i]["Credit"].ToString() + ")");
					}
					else
					{
						ucsells[num].setPayType("付款模式: 現金(" + dt.Rows[i]["cash"].ToString() + ")");
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
			l_pageNow.Text = string.Format("{0}", pageNow);
			l_pageInfo.Text = string.Format("共{0}筆．{1}頁｜目前在第{2}頁", dt.Rows.Count, Math.Ceiling((double)dt.Rows.Count / 8.0), pageNow);
			DateTime dateTime = DateTime.Now.AddMonths(-2);
			DateTime now = DateTime.Now;
			string text = new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
			string text2 = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");
			string text3 = text.Substring(0, 10);
			string text4 = text2.Substring(0, 10);
			text_salesDate.Text = text3.Replace("-", "/") + "~" + text4.Replace("-", "/");
			text_salesDate.ForeColor = Color.Red;
		}

		private void btn_all_Click(object sender, EventArgs e)
		{
			DateTime dateTime = DateTime.Now.AddMonths(-2);
			DateTime now = DateTime.Now;
			string text = new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
			string text2 = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");
			status = "sellTime >='" + text + "' and sellTime <='" + text2 + "'";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hms.sellNo,hms.sellTime,hms.memberId,hms.items,hcr.Name,hcr.Mobile ,hcr.IdNo,hcr.CompanyIdNo,hms.cash,hms.Credit,hms.sum ", "hypos_main_sell as hms left outer join hypos_CUST_RTL as hcr on hms.memberId= hcr.VipNo", status, "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
			btn_statusAll.ForeColor = Color.White;
			btn_statusAll.BackColor = Color.FromArgb(247, 106, 45);
			btn_today.ForeColor = Color.FromArgb(247, 106, 45);
			btn_today.BackColor = Color.White;
			btn_week.ForeColor = Color.FromArgb(247, 106, 45);
			btn_week.BackColor = Color.White;
			btn_month.ForeColor = Color.FromArgb(247, 106, 45);
			btn_month.BackColor = Color.White;
			btn_lastMonth.ForeColor = Color.FromArgb(247, 106, 45);
			btn_lastMonth.BackColor = Color.White;
		}

		private void btn_tody_Click(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			string text = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
			string text2 = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");
			status = "hms.sellTime >= '" + text + "' and hms.sellTime <= '" + text2 + "'";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hms.sellNo,hms.sellTime,hms.memberId,hms.items,hcr.Name,hcr.Mobile ,hcr.IdNo,hcr.CompanyIdNo,hms.cash,hms.Credit,hms.sum ", "hypos_main_sell as hms left outer join hypos_CUST_RTL as hcr on hms.memberId= hcr.VipNo", status, "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
			btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusAll.BackColor = Color.White;
			btn_today.ForeColor = Color.White;
			btn_today.BackColor = Color.FromArgb(247, 106, 45);
			btn_week.ForeColor = Color.FromArgb(247, 106, 45);
			btn_week.BackColor = Color.White;
			btn_month.ForeColor = Color.FromArgb(247, 106, 45);
			btn_month.BackColor = Color.White;
			btn_lastMonth.ForeColor = Color.FromArgb(247, 106, 45);
			btn_lastMonth.BackColor = Color.White;
		}

		private void btn_week_Click(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			int num = (int)now.DayOfWeek;
			if (num == 0)
			{
				num = 7;
			}
			int num2 = 7 - num;
			int num3 = num - 1;
			DateTime dateTime = now.AddDays(-num3);
			DateTime dateTime2 = now.AddDays(num2);
			string text = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
			string text2 = new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");
			status = "hms.sellTime >= '" + text + "' and hms.sellTime <= '" + text2 + "'";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hms.sellNo,hms.sellTime,hms.memberId,hms.items,hcr.Name,hcr.Mobile ,hcr.IdNo,hcr.CompanyIdNo,hms.cash,hms.Credit,hms.sum ", "hypos_main_sell as hms left outer join hypos_CUST_RTL as hcr on hms.memberId= hcr.VipNo", status, "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
			btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusAll.BackColor = Color.White;
			btn_today.ForeColor = Color.FromArgb(247, 106, 45);
			btn_today.BackColor = Color.White;
			btn_week.ForeColor = Color.White;
			btn_week.BackColor = Color.FromArgb(247, 106, 45);
			btn_month.ForeColor = Color.FromArgb(247, 106, 45);
			btn_month.BackColor = Color.White;
			btn_lastMonth.ForeColor = Color.FromArgb(247, 106, 45);
			btn_lastMonth.BackColor = Color.White;
		}

		private void btn_month_Click(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			int year = now.Year;
			int month = now.Month;
			string text = new DateTime(year, month, 1, 0, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
			string text2 = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");
			status = "hms.sellTime >= '" + text + "' and hms.sellTime <= '" + text2 + "'";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hms.sellNo,hms.sellTime,hms.memberId,hms.items,hcr.Name,hcr.Mobile ,hcr.IdNo,hcr.CompanyIdNo,hms.cash,hms.Credit,hms.sum ", "hypos_main_sell as hms left outer join hypos_CUST_RTL as hcr on hms.memberId= hcr.VipNo", status, "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
			btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusAll.BackColor = Color.White;
			btn_today.ForeColor = Color.FromArgb(247, 106, 45);
			btn_today.BackColor = Color.White;
			btn_week.ForeColor = Color.FromArgb(247, 106, 45);
			btn_week.BackColor = Color.White;
			btn_month.ForeColor = Color.White;
			btn_month.BackColor = Color.FromArgb(247, 106, 45);
			btn_lastMonth.ForeColor = Color.FromArgb(247, 106, 45);
			btn_lastMonth.BackColor = Color.White;
		}

		private void btn_lastMonth_Click(object sender, EventArgs e)
		{
			DateTime dateTime = DateTime.Now.AddMonths(-1);
			int year = dateTime.Year;
			int month = dateTime.Month;
			string text = new DateTime(year, month, 1, 0, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
			string text2 = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");
			Console.WriteLine("date1 : " + text);
			Console.WriteLine("date2 : " + text2);
			status = "hms.sellTime >= '" + text + "' and hms.sellTime <= '" + text2 + "'";
			dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hms.sellNo,hms.sellTime,hms.memberId,hms.items,hcr.Name,hcr.Mobile ,hcr.IdNo,hcr.CompanyIdNo,hms.cash,hms.Credit,hms.sum ", "hypos_main_sell as hms left outer join hypos_CUST_RTL as hcr on hms.memberId= hcr.VipNo", status, "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			changePage(1);
			btn_statusAll.ForeColor = Color.FromArgb(247, 106, 45);
			btn_statusAll.BackColor = Color.White;
			btn_today.ForeColor = Color.FromArgb(247, 106, 45);
			btn_today.BackColor = Color.White;
			btn_week.ForeColor = Color.FromArgb(247, 106, 45);
			btn_week.BackColor = Color.White;
			btn_month.ForeColor = Color.FromArgb(247, 106, 45);
			btn_month.BackColor = Color.White;
			btn_lastMonth.ForeColor = Color.White;
			btn_lastMonth.BackColor = Color.FromArgb(247, 106, 45);
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

		private void btn_memberSearch_Click(object sender, EventArgs e)
		{
			switchForm(new frmSearchSell_Return());
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
			uC_sell2 = new POS_Client.ucSellInfo();
			uC_sell3 = new POS_Client.ucSellInfo();
			uC_sell4 = new POS_Client.ucSellInfo();
			uC_sell5 = new POS_Client.ucSellInfo();
			uC_sell6 = new POS_Client.ucSellInfo();
			uC_sell7 = new POS_Client.ucSellInfo();
			uC_sell8 = new POS_Client.ucSellInfo();
			btn_pageRight = new System.Windows.Forms.Button();
			btn_pageLeft = new System.Windows.Forms.Button();
			l_status = new System.Windows.Forms.Label();
			btn_statusAll = new System.Windows.Forms.Button();
			btn_today = new System.Windows.Forms.Button();
			l_memberList = new System.Windows.Forms.Label();
			btn_memberSearch = new System.Windows.Forms.Button();
			l_pageInfo = new System.Windows.Forms.Label();
			btn_firstPage = new System.Windows.Forms.Button();
			btn_previousPage = new System.Windows.Forms.Button();
			btn_nextPage = new System.Windows.Forms.Button();
			btn_lastPage = new System.Windows.Forms.Button();
			l_pageNow = new System.Windows.Forms.Label();
			uC_sell1 = new POS_Client.ucSellInfo();
			btn_month = new System.Windows.Forms.Button();
			btn_week = new System.Windows.Forms.Button();
			btn_lastMonth = new System.Windows.Forms.Button();
			label_salesDate = new System.Windows.Forms.Label();
			text_salesDate = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			SuspendLayout();
			uC_sell2.AutoSize = true;
			uC_sell2.BackColor = System.Drawing.Color.White;
			uC_sell2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sell2.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sell2.Location = new System.Drawing.Point(495, 175);
			uC_sell2.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sell2.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sell2.Name = "uC_sell2";
			uC_sell2.Size = new System.Drawing.Size(398, 102);
			uC_sell2.TabIndex = 34;
			uC_sell3.AutoSize = true;
			uC_sell3.BackColor = System.Drawing.Color.White;
			uC_sell3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sell3.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sell3.Location = new System.Drawing.Point(89, 276);
			uC_sell3.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sell3.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sell3.Name = "uC_sell3";
			uC_sell3.Size = new System.Drawing.Size(398, 102);
			uC_sell3.TabIndex = 35;
			uC_sell4.AutoSize = true;
			uC_sell4.BackColor = System.Drawing.Color.White;
			uC_sell4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sell4.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sell4.Location = new System.Drawing.Point(495, 276);
			uC_sell4.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sell4.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sell4.Name = "uC_sell4";
			uC_sell4.Size = new System.Drawing.Size(398, 102);
			uC_sell4.TabIndex = 36;
			uC_sell5.AutoSize = true;
			uC_sell5.BackColor = System.Drawing.Color.White;
			uC_sell5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sell5.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sell5.Location = new System.Drawing.Point(89, 377);
			uC_sell5.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sell5.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sell5.Name = "uC_sell5";
			uC_sell5.Size = new System.Drawing.Size(398, 102);
			uC_sell5.TabIndex = 37;
			uC_sell6.AutoSize = true;
			uC_sell6.BackColor = System.Drawing.Color.White;
			uC_sell6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sell6.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sell6.Location = new System.Drawing.Point(495, 377);
			uC_sell6.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sell6.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sell6.Name = "uC_sell6";
			uC_sell6.Size = new System.Drawing.Size(398, 102);
			uC_sell6.TabIndex = 38;
			uC_sell7.AutoSize = true;
			uC_sell7.BackColor = System.Drawing.Color.White;
			uC_sell7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sell7.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sell7.Location = new System.Drawing.Point(89, 478);
			uC_sell7.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sell7.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sell7.Name = "uC_sell7";
			uC_sell7.Size = new System.Drawing.Size(398, 102);
			uC_sell7.TabIndex = 39;
			uC_sell8.AutoSize = true;
			uC_sell8.BackColor = System.Drawing.Color.White;
			uC_sell8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sell8.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sell8.Location = new System.Drawing.Point(495, 478);
			uC_sell8.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sell8.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sell8.Name = "uC_sell8";
			uC_sell8.Size = new System.Drawing.Size(398, 102);
			uC_sell8.TabIndex = 40;
			btn_pageRight.FlatAppearance.BorderSize = 0;
			btn_pageRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageRight.Image = POS_Client.Properties.Resources.right;
			btn_pageRight.Location = new System.Drawing.Point(934, 209);
			btn_pageRight.Name = "btn_pageRight";
			btn_pageRight.Size = new System.Drawing.Size(48, 371);
			btn_pageRight.TabIndex = 41;
			btn_pageRight.UseVisualStyleBackColor = true;
			btn_pageRight.Click += new System.EventHandler(btn_pageRight_Click);
			btn_pageLeft.FlatAppearance.BorderSize = 0;
			btn_pageLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_pageLeft.Image = POS_Client.Properties.Resources.left;
			btn_pageLeft.Location = new System.Drawing.Point(1, 209);
			btn_pageLeft.Name = "btn_pageLeft";
			btn_pageLeft.Size = new System.Drawing.Size(48, 371);
			btn_pageLeft.TabIndex = 42;
			btn_pageLeft.UseVisualStyleBackColor = true;
			btn_pageLeft.Click += new System.EventHandler(btn_pageLeft_Click);
			l_status.AutoSize = true;
			l_status.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_status.Location = new System.Drawing.Point(87, 134);
			l_status.Name = "l_status";
			l_status.Size = new System.Drawing.Size(73, 20);
			l_status.TabIndex = 43;
			l_status.Text = "日期篩選";
			btn_statusAll.BackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_statusAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_statusAll.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_statusAll.ForeColor = System.Drawing.Color.White;
			btn_statusAll.Location = new System.Drawing.Point(166, 130);
			btn_statusAll.Name = "btn_statusAll";
			btn_statusAll.Size = new System.Drawing.Size(69, 29);
			btn_statusAll.TabIndex = 44;
			btn_statusAll.Text = "全部";
			btn_statusAll.UseVisualStyleBackColor = false;
			btn_statusAll.Click += new System.EventHandler(btn_all_Click);
			btn_today.BackColor = System.Drawing.Color.White;
			btn_today.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_today.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_today.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_today.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_today.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_today.Location = new System.Drawing.Point(241, 130);
			btn_today.Name = "btn_today";
			btn_today.Size = new System.Drawing.Size(69, 29);
			btn_today.TabIndex = 45;
			btn_today.Text = "今日";
			btn_today.UseVisualStyleBackColor = false;
			btn_today.Click += new System.EventHandler(btn_tody_Click);
			l_memberList.AutoSize = true;
			l_memberList.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_memberList.Image = POS_Client.Properties.Resources.oblique;
			l_memberList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l_memberList.Location = new System.Drawing.Point(87, 52);
			l_memberList.Name = "l_memberList";
			l_memberList.Size = new System.Drawing.Size(105, 24);
			l_memberList.TabIndex = 51;
			l_memberList.Text = "銷售單查詢";
			l_memberList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			btn_memberSearch.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_memberSearch.FlatAppearance.BorderSize = 0;
			btn_memberSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_memberSearch.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_memberSearch.ForeColor = System.Drawing.Color.White;
			btn_memberSearch.Location = new System.Drawing.Point(820, 46);
			btn_memberSearch.Name = "btn_memberSearch";
			btn_memberSearch.Size = new System.Drawing.Size(88, 30);
			btn_memberSearch.TabIndex = 57;
			btn_memberSearch.Text = "重新查詢";
			btn_memberSearch.UseVisualStyleBackColor = false;
			btn_memberSearch.Click += new System.EventHandler(btn_memberSearch_Click);
			l_pageInfo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_pageInfo.Location = new System.Drawing.Point(410, 51);
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
			uC_sell1.AutoSize = true;
			uC_sell1.BackColor = System.Drawing.Color.White;
			uC_sell1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			uC_sell1.Cursor = System.Windows.Forms.Cursors.Hand;
			uC_sell1.Location = new System.Drawing.Point(89, 175);
			uC_sell1.MaximumSize = new System.Drawing.Size(398, 102);
			uC_sell1.MinimumSize = new System.Drawing.Size(398, 102);
			uC_sell1.Name = "uC_sell1";
			uC_sell1.Size = new System.Drawing.Size(398, 102);
			uC_sell1.TabIndex = 68;
			btn_month.BackColor = System.Drawing.Color.White;
			btn_month.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_month.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_month.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_month.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_month.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_month.Location = new System.Drawing.Point(391, 130);
			btn_month.Name = "btn_month";
			btn_month.Size = new System.Drawing.Size(69, 29);
			btn_month.TabIndex = 67;
			btn_month.Text = "本月";
			btn_month.UseVisualStyleBackColor = false;
			btn_month.Click += new System.EventHandler(btn_month_Click);
			btn_week.BackColor = System.Drawing.Color.White;
			btn_week.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_week.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_week.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_week.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_week.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_week.Location = new System.Drawing.Point(316, 130);
			btn_week.Name = "btn_week";
			btn_week.Size = new System.Drawing.Size(69, 29);
			btn_week.TabIndex = 45;
			btn_week.Text = "本週";
			btn_week.UseVisualStyleBackColor = false;
			btn_week.Click += new System.EventHandler(btn_week_Click);
			btn_lastMonth.BackColor = System.Drawing.Color.White;
			btn_lastMonth.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_lastMonth.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_lastMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_lastMonth.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_lastMonth.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_lastMonth.Location = new System.Drawing.Point(466, 130);
			btn_lastMonth.Name = "btn_lastMonth";
			btn_lastMonth.Size = new System.Drawing.Size(69, 29);
			btn_lastMonth.TabIndex = 69;
			btn_lastMonth.Text = "上月";
			btn_lastMonth.UseVisualStyleBackColor = false;
			btn_lastMonth.Click += new System.EventHandler(btn_lastMonth_Click);
			label_salesDate.AutoSize = true;
			label_salesDate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label_salesDate.Location = new System.Drawing.Point(237, 51);
			label_salesDate.Name = "label_salesDate";
			label_salesDate.Size = new System.Drawing.Size(81, 20);
			label_salesDate.TabIndex = 70;
			label_salesDate.Text = "銷售區間: ";
			text_salesDate.AutoSize = true;
			text_salesDate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			text_salesDate.Location = new System.Drawing.Point(312, 51);
			text_salesDate.Name = "text_salesDate";
			text_salesDate.Size = new System.Drawing.Size(28, 20);
			text_salesDate.TabIndex = 71;
			text_salesDate.Text = "{0}";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(text_salesDate);
			base.Controls.Add(label_salesDate);
			base.Controls.Add(btn_lastMonth);
			base.Controls.Add(uC_sell1);
			base.Controls.Add(btn_month);
			base.Controls.Add(l_pageNow);
			base.Controls.Add(btn_lastPage);
			base.Controls.Add(btn_nextPage);
			base.Controls.Add(btn_previousPage);
			base.Controls.Add(btn_firstPage);
			base.Controls.Add(l_pageInfo);
			base.Controls.Add(btn_memberSearch);
			base.Controls.Add(l_memberList);
			base.Controls.Add(btn_week);
			base.Controls.Add(btn_today);
			base.Controls.Add(btn_statusAll);
			base.Controls.Add(l_status);
			base.Controls.Add(btn_pageLeft);
			base.Controls.Add(btn_pageRight);
			base.Controls.Add(uC_sell8);
			base.Controls.Add(uC_sell7);
			base.Controls.Add(uC_sell6);
			base.Controls.Add(uC_sell5);
			base.Controls.Add(uC_sell4);
			base.Controls.Add(uC_sell3);
			base.Controls.Add(uC_sell2);
			base.Name = "frmSearchSellResult_View";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmMemberMangement_Load);
			base.Controls.SetChildIndex(uC_sell2, 0);
			base.Controls.SetChildIndex(uC_sell3, 0);
			base.Controls.SetChildIndex(uC_sell4, 0);
			base.Controls.SetChildIndex(uC_sell5, 0);
			base.Controls.SetChildIndex(uC_sell6, 0);
			base.Controls.SetChildIndex(uC_sell7, 0);
			base.Controls.SetChildIndex(uC_sell8, 0);
			base.Controls.SetChildIndex(btn_pageRight, 0);
			base.Controls.SetChildIndex(btn_pageLeft, 0);
			base.Controls.SetChildIndex(l_status, 0);
			base.Controls.SetChildIndex(btn_statusAll, 0);
			base.Controls.SetChildIndex(btn_today, 0);
			base.Controls.SetChildIndex(btn_week, 0);
			base.Controls.SetChildIndex(l_memberList, 0);
			base.Controls.SetChildIndex(btn_memberSearch, 0);
			base.Controls.SetChildIndex(l_pageInfo, 0);
			base.Controls.SetChildIndex(btn_firstPage, 0);
			base.Controls.SetChildIndex(btn_previousPage, 0);
			base.Controls.SetChildIndex(btn_nextPage, 0);
			base.Controls.SetChildIndex(btn_lastPage, 0);
			base.Controls.SetChildIndex(l_pageNow, 0);
			base.Controls.SetChildIndex(btn_month, 0);
			base.Controls.SetChildIndex(uC_sell1, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(btn_lastMonth, 0);
			base.Controls.SetChildIndex(label_salesDate, 0);
			base.Controls.SetChildIndex(text_salesDate, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
