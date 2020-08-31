using POS_Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmSearchDelivery : MasterThinForm
	{
		private frmDeliveryMangement _fdm;

		private List<string> lst_vendor = new List<string>();

		private List<string> lst_selTEMP = new List<string>();

		private string sql = "select a.editDate,a.status,a.DeliveryNo,a.CurSum,a.DeliveryDate,a.DeliveryCustomNo,a.sumDiscount,b.SupplierName from hypos_DeliveryGoods_Master as a join hypos_Supplier as b on a.vendorNo = b.SupplierNo where 1=1 ";

		private IContainer components;

		private TableLayoutPanel tableLayoutPanel3;

		private FlowLayoutPanel flowLayoutPanel13;

		private DateTimePicker dateTimePicker4;

		private Label label23;

		private DateTimePicker dateTimePicker5;

		private FlowLayoutPanel flowLayoutPanel14;

		private Button btn_SelectVendors;

		private Panel panel14;

		private Label label15;

		private Panel panel16;

		private Label label17;

		private Panel panel17;

		private Label label18;

		private Panel panel18;

		private Label label19;

		private Panel panel19;

		private Label label20;

		private Panel panel1;

		private TextBox tb_DeliveryNo;

		private Panel panel2;

		private TextBox tb_DeliveryCustomNo;

		private Panel panel3;

		private TextBox tb_vendorId;

		private TextBox tb_SupplierNo;

		private Button btn_reset;

		private Button btn_SearchDelivery;

		private Button btn_cancel;

		public frmSearchDelivery(frmDeliveryMangement fdm)
			: base("出貨單查詢")
		{
			InitializeComponent();
			_fdm = fdm;
			dateTimePicker4.Value = DateTime.Today.AddDays(-30.0);
			dateTimePicker5.Value = DateTime.Today;
			tb_DeliveryNo.Select();
		}

		private void btn_SelectVendors_Click(object sender, EventArgs e)
		{
			if (flowLayoutPanel14.Controls.Count > 1)
			{
				for (int num = flowLayoutPanel14.Controls.Count - 1; num > 0; num--)
				{
					flowLayoutPanel14.Controls.RemoveAt(num);
				}
			}
			lst_selTEMP.Clear();
			new dialogChooseVendors(lst_selTEMP).ShowDialog(this);
			lst_vendor.AddRange(lst_selTEMP);
			lst_vendor = Enumerable.ToList(Enumerable.Distinct(lst_vendor));
			if (lst_vendor.Count > 0)
			{
				for (int i = 0; i < lst_vendor.Count; i++)
				{
					ucShowString ucShowString = new ucShowString(flowLayoutPanel14, lst_vendor[i]);
					ucShowString.OnClickRemove += new EventHandler(vendorRemove);
					flowLayoutPanel14.Controls.Add(ucShowString);
				}
			}
		}

		public void vendorRemove(object Name, EventArgs s)
		{
			string text = Name as string;
			if (text != null)
			{
				lst_vendor.Remove(text);
			}
		}

		private void btn_SearchDelivery_Click(object sender, EventArgs e)
		{
			string text = "";
			if (DateTime.Compare(dateTimePicker4.Value, dateTimePicker5.Value) > 0)
			{
				MessageBox.Show("起日不可大於迄日，請重新設定");
				return;
			}
			if (!"".Equals(tb_DeliveryNo.Text) && !"請輸入或刷入系統單號".Equals(tb_DeliveryNo.Text))
			{
				text = text + " and a.DeliveryNo like '%" + tb_DeliveryNo.Text.Trim() + "%'";
			}
			if (!"".Equals(tb_DeliveryCustomNo.Text) && !"請輸入自設出貨單號".Equals(tb_DeliveryCustomNo.Text))
			{
				text = text + " and a.DeliveryCustomNo like '%" + tb_DeliveryCustomNo.Text.Trim() + "%'";
			}
			if (lst_vendor.Count > 0)
			{
				for (int i = 0; i < lst_vendor.Count; i++)
				{
					string[] array = lst_vendor[i].Split(',');
					text = ((i != 0) ? (text + " , '" + array[0] + "'") : (text + " and b.SupplierNo IN ( '" + array[0] + "' "));
				}
				text += ") ";
			}
			if (!"".Equals(tb_SupplierNo.Text) && !"請輸入廠商系統編號".Equals(tb_SupplierNo.Text))
			{
				text = text + " and b.upplierNo like '%" + tb_SupplierNo.Text.Trim() + "%'";
			}
			if (!"".Equals(tb_vendorId.Text) && !"請輸入廠商營業執照號碼".Equals(tb_vendorId.Text))
			{
				text = text + " and b.vendorId like '%" + tb_vendorId.Text.Trim() + "%'";
			}
			text = text + " and a.DeliveryDate between '" + dateTimePicker4.Value.ToString("yyyy-MM-dd") + "' and datetime(date( '" + dateTimePicker5.Value.ToString("yyyy-MM-dd") + "' ), '+1 days')";
			_fdm.UpdateDeliveryMangemnet(sql + text);
			Close();
			_fdm.Show();
		}

		private void btn_reset_Click(object sender, EventArgs e)
		{
			tb_DeliveryNo.Text = "請輸入或刷入系統單號";
			tb_DeliveryCustomNo.Text = "請輸入自設出貨單號";
			tb_SupplierNo.Text = "請輸入廠商系統編號";
			tb_vendorId.Text = "請輸入廠商營業執照號碼";
			dateTimePicker4.Value = DateTime.Today.AddDays(30.0);
			dateTimePicker5.Value = DateTime.Today;
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			_fdm.Show();
			Close();
		}

		private void tb_DeliveryNo_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_DeliveryNo.Text))
			{
				tb_DeliveryNo.Text = "請輸入或刷入系統單號";
			}
		}

		private void tb_DeliveryNo_Enter(object sender, EventArgs e)
		{
			if ("請輸入或刷入系統單號".Equals(tb_DeliveryNo.Text))
			{
				tb_DeliveryNo.Text = "";
			}
		}

		private void tb_DeliveryCustomNo_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_DeliveryCustomNo.Text))
			{
				tb_DeliveryCustomNo.Text = "請輸入自設出貨單號";
			}
		}

		private void tb_DeliveryCustomNo_Enter(object sender, EventArgs e)
		{
			if ("請輸入自設出貨單號".Equals(tb_DeliveryCustomNo.Text))
			{
				tb_DeliveryCustomNo.Text = "";
			}
		}

		private void tb_SupplierNo_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_SupplierNo.Text))
			{
				tb_SupplierNo.Text = "請輸入廠商系統編號";
			}
		}

		private void tb_SupplierNo_Enter(object sender, EventArgs e)
		{
			if ("請輸入廠商系統編號".Equals(tb_SupplierNo.Text))
			{
				tb_SupplierNo.Text = "";
			}
		}

		private void tb_vendorId_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_vendorId.Text))
			{
				tb_vendorId.Text = "請輸入廠商營業執照號碼";
			}
		}

		private void tb_vendorId_Enter(object sender, EventArgs e)
		{
			if ("請輸入廠商營業執照號碼".Equals(tb_vendorId.Text))
			{
				tb_vendorId.Text = "";
			}
		}

		private void tb_DeliveryNo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && tb_DeliveryNo.Text.Length == 20)
			{
				string text = "SELECT DeliveryNo FROM hypos_DeliveryGoods_Master where DeliveryNo = '" + tb_DeliveryNo.Text + "'";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text, null, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					string deliveryNo = dataTable.Rows[0]["DeliveryNo"].ToString();
					Close();
					switchForm(new frmEditDeliveryOrder(deliveryNo), _fdm);
				}
				else
				{
					MessageBox.Show("出貨單不存在，請正確輸入出貨單編號");
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
			tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			panel14 = new System.Windows.Forms.Panel();
			label15 = new System.Windows.Forms.Label();
			panel17 = new System.Windows.Forms.Panel();
			label18 = new System.Windows.Forms.Label();
			panel16 = new System.Windows.Forms.Panel();
			label17 = new System.Windows.Forms.Label();
			flowLayoutPanel14 = new System.Windows.Forms.FlowLayoutPanel();
			btn_SelectVendors = new System.Windows.Forms.Button();
			panel18 = new System.Windows.Forms.Panel();
			label19 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			tb_DeliveryCustomNo = new System.Windows.Forms.TextBox();
			flowLayoutPanel13 = new System.Windows.Forms.FlowLayoutPanel();
			dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
			label23 = new System.Windows.Forms.Label();
			dateTimePicker5 = new System.Windows.Forms.DateTimePicker();
			panel2 = new System.Windows.Forms.Panel();
			tb_DeliveryNo = new System.Windows.Forms.TextBox();
			panel19 = new System.Windows.Forms.Panel();
			label20 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			tb_vendorId = new System.Windows.Forms.TextBox();
			tb_SupplierNo = new System.Windows.Forms.TextBox();
			btn_reset = new System.Windows.Forms.Button();
			btn_SearchDelivery = new System.Windows.Forms.Button();
			btn_cancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			tableLayoutPanel3.SuspendLayout();
			panel14.SuspendLayout();
			panel17.SuspendLayout();
			panel16.SuspendLayout();
			flowLayoutPanel14.SuspendLayout();
			panel18.SuspendLayout();
			panel1.SuspendLayout();
			flowLayoutPanel13.SuspendLayout();
			panel2.SuspendLayout();
			panel19.SuspendLayout();
			panel3.SuspendLayout();
			SuspendLayout();
			tableLayoutPanel3.BackColor = System.Drawing.Color.White;
			tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel3.ColumnCount = 2;
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel3.Controls.Add(panel14, 0, 5);
			tableLayoutPanel3.Controls.Add(panel17, 0, 0);
			tableLayoutPanel3.Controls.Add(panel16, 0, 2);
			tableLayoutPanel3.Controls.Add(flowLayoutPanel14, 1, 2);
			tableLayoutPanel3.Controls.Add(panel18, 0, 1);
			tableLayoutPanel3.Controls.Add(panel1, 1, 1);
			tableLayoutPanel3.Controls.Add(flowLayoutPanel13, 1, 5);
			tableLayoutPanel3.Controls.Add(panel2, 1, 0);
			tableLayoutPanel3.Controls.Add(panel19, 0, 3);
			tableLayoutPanel3.Controls.Add(panel3, 1, 3);
			tableLayoutPanel3.Location = new System.Drawing.Point(0, 50);
			tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 6;
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.1579f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.51196f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.78947f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571f));
			tableLayoutPanel3.Size = new System.Drawing.Size(981, 419);
			tableLayoutPanel3.TabIndex = 52;
			panel14.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel14.Controls.Add(label15);
			panel14.Dock = System.Windows.Forms.DockStyle.Fill;
			panel14.Location = new System.Drawing.Point(1, 357);
			panel14.Margin = new System.Windows.Forms.Padding(0);
			panel14.Name = "panel14";
			panel14.Size = new System.Drawing.Size(162, 61);
			panel14.TabIndex = 20;
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label15.ForeColor = System.Drawing.Color.White;
			label15.Location = new System.Drawing.Point(48, 21);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(106, 21);
			label15.TabIndex = 0;
			label15.Text = "出貨日期區間";
			panel17.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel17.Controls.Add(label18);
			panel17.Dock = System.Windows.Forms.DockStyle.Fill;
			panel17.Location = new System.Drawing.Point(1, 1);
			panel17.Margin = new System.Windows.Forms.Padding(0);
			panel17.Name = "panel17";
			panel17.Size = new System.Drawing.Size(162, 59);
			panel17.TabIndex = 19;
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label18.ForeColor = System.Drawing.Color.White;
			label18.Location = new System.Drawing.Point(80, 22);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(74, 21);
			label18.TabIndex = 0;
			label18.Text = "系統單號";
			label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			panel16.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel16.Controls.Add(label17);
			panel16.Dock = System.Windows.Forms.DockStyle.Fill;
			panel16.Location = new System.Drawing.Point(1, 116);
			panel16.Margin = new System.Windows.Forms.Padding(0);
			panel16.Name = "panel16";
			panel16.Size = new System.Drawing.Size(162, 114);
			panel16.TabIndex = 20;
			label17.AutoSize = true;
			label17.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label17.ForeColor = System.Drawing.Color.White;
			label17.Location = new System.Drawing.Point(48, 49);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(106, 21);
			label17.TabIndex = 0;
			label17.Text = "選擇出貨廠商";
			flowLayoutPanel14.AutoScroll = true;
			flowLayoutPanel14.Controls.Add(btn_SelectVendors);
			flowLayoutPanel14.Location = new System.Drawing.Point(164, 116);
			flowLayoutPanel14.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel14.Name = "flowLayoutPanel14";
			flowLayoutPanel14.Size = new System.Drawing.Size(816, 113);
			flowLayoutPanel14.TabIndex = 27;
			btn_SelectVendors.BackColor = System.Drawing.Color.White;
			btn_SelectVendors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SelectVendors.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_SelectVendors.ForeColor = System.Drawing.Color.Black;
			btn_SelectVendors.Image = POS_Client.Properties.Resources.ic_toc_black_24dp_1x;
			btn_SelectVendors.Location = new System.Drawing.Point(10, 10);
			btn_SelectVendors.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
			btn_SelectVendors.Name = "btn_SelectVendors";
			btn_SelectVendors.Size = new System.Drawing.Size(35, 33);
			btn_SelectVendors.TabIndex = 4;
			btn_SelectVendors.UseVisualStyleBackColor = false;
			btn_SelectVendors.Click += new System.EventHandler(btn_SelectVendors_Click);
			panel18.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel18.Controls.Add(label19);
			panel18.Location = new System.Drawing.Point(1, 61);
			panel18.Margin = new System.Windows.Forms.Padding(0);
			panel18.Name = "panel18";
			panel18.Size = new System.Drawing.Size(162, 54);
			panel18.TabIndex = 22;
			label19.AutoSize = true;
			label19.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label19.ForeColor = System.Drawing.Color.White;
			label19.Location = new System.Drawing.Point(80, 19);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(74, 21);
			label19.TabIndex = 0;
			label19.Text = "出貨單號";
			panel1.Controls.Add(tb_DeliveryCustomNo);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(167, 64);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(810, 48);
			panel1.TabIndex = 28;
			tb_DeliveryCustomNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_DeliveryCustomNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f);
			tb_DeliveryCustomNo.ForeColor = System.Drawing.Color.Gray;
			tb_DeliveryCustomNo.Location = new System.Drawing.Point(7, 10);
			tb_DeliveryCustomNo.Name = "tb_DeliveryCustomNo";
			tb_DeliveryCustomNo.Size = new System.Drawing.Size(405, 33);
			tb_DeliveryCustomNo.TabIndex = 1;
			tb_DeliveryCustomNo.Text = "請輸入自設出貨單號";
			tb_DeliveryCustomNo.Enter += new System.EventHandler(tb_DeliveryCustomNo_Enter);
			tb_DeliveryCustomNo.Leave += new System.EventHandler(tb_DeliveryCustomNo_Leave);
			flowLayoutPanel13.Controls.Add(dateTimePicker4);
			flowLayoutPanel13.Controls.Add(label23);
			flowLayoutPanel13.Controls.Add(dateTimePicker5);
			flowLayoutPanel13.Location = new System.Drawing.Point(164, 357);
			flowLayoutPanel13.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel13.Name = "flowLayoutPanel13";
			flowLayoutPanel13.Size = new System.Drawing.Size(816, 58);
			flowLayoutPanel13.TabIndex = 27;
			dateTimePicker4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker4.CustomFormat = "yyyy-MM-dd";
			dateTimePicker4.Font = new System.Drawing.Font("微軟正黑體", 14.25f);
			dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker4.Location = new System.Drawing.Point(10, 13);
			dateTimePicker4.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker4.Name = "dateTimePicker4";
			dateTimePicker4.ShowCheckBox = true;
			dateTimePicker4.Size = new System.Drawing.Size(181, 33);
			dateTimePicker4.TabIndex = 9;
			dateTimePicker4.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			label23.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label23.AutoSize = true;
			label23.Font = new System.Drawing.Font("微軟正黑體", 14.25f);
			label23.Location = new System.Drawing.Point(197, 17);
			label23.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(24, 24);
			label23.TabIndex = 11;
			label23.Text = "~";
			dateTimePicker5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker5.CustomFormat = "yyyy-MM-dd";
			dateTimePicker5.Font = new System.Drawing.Font("微軟正黑體", 14.25f);
			dateTimePicker5.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker5.Location = new System.Drawing.Point(234, 13);
			dateTimePicker5.Margin = new System.Windows.Forms.Padding(10, 13, 3, 3);
			dateTimePicker5.Name = "dateTimePicker5";
			dateTimePicker5.ShowCheckBox = true;
			dateTimePicker5.Size = new System.Drawing.Size(181, 33);
			dateTimePicker5.TabIndex = 10;
			dateTimePicker5.Value = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			panel2.Controls.Add(tb_DeliveryNo);
			panel2.Location = new System.Drawing.Point(167, 4);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(810, 53);
			panel2.TabIndex = 29;
			tb_DeliveryNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_DeliveryNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f);
			tb_DeliveryNo.ForeColor = System.Drawing.Color.Gray;
			tb_DeliveryNo.Location = new System.Drawing.Point(7, 13);
			tb_DeliveryNo.Name = "tb_DeliveryNo";
			tb_DeliveryNo.Size = new System.Drawing.Size(405, 33);
			tb_DeliveryNo.TabIndex = 0;
			tb_DeliveryNo.Text = "請輸入或刷入系統單號";
			tb_DeliveryNo.Enter += new System.EventHandler(tb_DeliveryNo_Enter);
			tb_DeliveryNo.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_DeliveryNo_KeyDown);
			tb_DeliveryNo.Leave += new System.EventHandler(tb_DeliveryNo_Leave);
			panel19.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel19.Controls.Add(label20);
			panel19.Location = new System.Drawing.Point(1, 231);
			panel19.Margin = new System.Windows.Forms.Padding(0);
			panel19.Name = "panel19";
			tableLayoutPanel3.SetRowSpan(panel19, 2);
			panel19.Size = new System.Drawing.Size(162, 125);
			panel19.TabIndex = 23;
			label20.AutoSize = true;
			label20.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label20.ForeColor = System.Drawing.Color.White;
			label20.Location = new System.Drawing.Point(80, 54);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(74, 21);
			label20.TabIndex = 0;
			label20.Text = "廠商資訊";
			panel3.Controls.Add(tb_vendorId);
			panel3.Controls.Add(tb_SupplierNo);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(167, 234);
			panel3.Name = "panel3";
			tableLayoutPanel3.SetRowSpan(panel3, 2);
			panel3.Size = new System.Drawing.Size(810, 119);
			panel3.TabIndex = 30;
			tb_vendorId.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_vendorId.Font = new System.Drawing.Font("微軟正黑體", 14.25f);
			tb_vendorId.ForeColor = System.Drawing.Color.Gray;
			tb_vendorId.Location = new System.Drawing.Point(7, 68);
			tb_vendorId.Name = "tb_vendorId";
			tb_vendorId.Size = new System.Drawing.Size(405, 33);
			tb_vendorId.TabIndex = 3;
			tb_vendorId.Text = "請輸入廠商營業執照號碼";
			tb_vendorId.Enter += new System.EventHandler(tb_vendorId_Enter);
			tb_vendorId.Leave += new System.EventHandler(tb_vendorId_Leave);
			tb_SupplierNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SupplierNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f);
			tb_SupplierNo.ForeColor = System.Drawing.Color.Gray;
			tb_SupplierNo.Location = new System.Drawing.Point(7, 20);
			tb_SupplierNo.Name = "tb_SupplierNo";
			tb_SupplierNo.Size = new System.Drawing.Size(405, 33);
			tb_SupplierNo.TabIndex = 2;
			tb_SupplierNo.Text = "請輸入廠商系統編號";
			tb_SupplierNo.Enter += new System.EventHandler(tb_SupplierNo_Enter);
			tb_SupplierNo.Leave += new System.EventHandler(tb_SupplierNo_Leave);
			btn_reset.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_reset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_reset.ForeColor = System.Drawing.Color.White;
			btn_reset.Location = new System.Drawing.Point(461, 493);
			btn_reset.Name = "btn_reset";
			btn_reset.Size = new System.Drawing.Size(113, 35);
			btn_reset.TabIndex = 54;
			btn_reset.TabStop = false;
			btn_reset.Text = "重設";
			btn_reset.UseVisualStyleBackColor = false;
			btn_reset.Click += new System.EventHandler(btn_reset_Click);
			btn_SearchDelivery.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_SearchDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SearchDelivery.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_SearchDelivery.ForeColor = System.Drawing.Color.White;
			btn_SearchDelivery.Location = new System.Drawing.Point(320, 493);
			btn_SearchDelivery.Name = "btn_SearchDelivery";
			btn_SearchDelivery.Size = new System.Drawing.Size(113, 35);
			btn_SearchDelivery.TabIndex = 53;
			btn_SearchDelivery.TabStop = false;
			btn_SearchDelivery.Text = "查詢";
			btn_SearchDelivery.UseVisualStyleBackColor = false;
			btn_SearchDelivery.Click += new System.EventHandler(btn_SearchDelivery_Click);
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(600, 493);
			btn_cancel.Name = "btn_cancel";
			btn_cancel.Size = new System.Drawing.Size(113, 35);
			btn_cancel.TabIndex = 55;
			btn_cancel.TabStop = false;
			btn_cancel.Text = "取消";
			btn_cancel.UseVisualStyleBackColor = false;
			btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(btn_cancel);
			base.Controls.Add(btn_reset);
			base.Controls.Add(btn_SearchDelivery);
			base.Controls.Add(tableLayoutPanel3);
			base.Name = "frmSearchDelivery";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "frmSearchDelivery";
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(tableLayoutPanel3, 0);
			base.Controls.SetChildIndex(btn_SearchDelivery, 0);
			base.Controls.SetChildIndex(btn_reset, 0);
			base.Controls.SetChildIndex(btn_cancel, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			tableLayoutPanel3.ResumeLayout(false);
			panel14.ResumeLayout(false);
			panel14.PerformLayout();
			panel17.ResumeLayout(false);
			panel17.PerformLayout();
			panel16.ResumeLayout(false);
			panel16.PerformLayout();
			flowLayoutPanel14.ResumeLayout(false);
			panel18.ResumeLayout(false);
			panel18.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			flowLayoutPanel13.ResumeLayout(false);
			flowLayoutPanel13.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel19.ResumeLayout(false);
			panel19.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			ResumeLayout(false);
		}
	}
}
