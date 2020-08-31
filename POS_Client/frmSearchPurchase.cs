using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmSearchPurchase : MasterThinForm
	{
		private IContainer components;

		private Panel panel5;

		private Label label10;

		private Panel panel4;

		private Label label8;

		private Panel panel1;

		private Label label1;

		private Panel panel2;

		private Label label4;

		private Panel panel3;

		private Label label6;

		private Panel panel6;

		private Label label12;

		private TextBox tb_SupplierName;

		private TableLayoutPanel tableLayoutPanel1;

		private TextBox tb_PurchaseCustomNo;

		private Button btn_Search;

		private Button btn_reset;

		private Button btn_chooseMultiSupplier;

		private FlowLayoutPanel flowLayoutPanel1;

		private DateTimePicker dt_PuschaseDateStart;

		private Label label2;

		private DateTimePicker dt_PuschaseDateEnd;

		private TextBox tb_PurchaseNo;

		private DataGridView dataGridView1;

		private FlowLayoutPanel flowLayoutPanel2;

		private Button btn_Cancel;

		private DataGridViewTextBoxColumn DGV_SupplierName;

		private DataGridViewButtonColumn DVG_Status;

		private DataGridViewTextBoxColumn hiddenGDSNO;

		private FlowLayoutPanel flowLayoutPanel3;

		private MyCheckBox cb_Normal;

		private MyCheckBox cb_Update;

		private MyCheckBox cb_Cancel;

		public frmSearchPurchase()
			: base("進貨單查詢")
		{
			InitializeComponent();
		}

		private void btn_chooseMultiSupplier_Click(object sender, EventArgs e)
		{
			using (dialogMultiChooseSupplier dialogMultiChooseSupplier = new dialogMultiChooseSupplier())
			{
				if (dialogMultiChooseSupplier.ShowDialog() == DialogResult.OK)
				{
					for (int i = 0; i < dialogMultiChooseSupplier.returnSupplierNo.Count; i++)
					{
						dataGridView1.Rows.Add(dialogMultiChooseSupplier.returnSupplierName[i], "X", dialogMultiChooseSupplier.returnSupplierNo[i]);
					}
				}
			}
		}

		protected override void l_ReturnMain_Click(object sender, EventArgs e)
		{
			base.Owner.Close();
			switchForm(new frmMain());
		}

		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			switchForm(base.Owner);
		}

		private void tb_SupplierName_Enter(object sender, EventArgs e)
		{
			if ("請輸入廠商名稱關鍵字".Equals(tb_SupplierName.Text))
			{
				tb_SupplierName.Text = "";
			}
			tb_SupplierName.ForeColor = Color.Black;
			tb_SupplierName.Refresh();
		}

		private void tb_SupplierName_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_SupplierName.Text))
			{
				tb_SupplierName.Text = "請輸入廠商名稱關鍵字";
				tb_SupplierName.ForeColor = Color.Gray;
			}
			tb_SupplierName.Refresh();
		}

		private void tb_PurchaseNo_Enter(object sender, EventArgs e)
		{
			if ("請輸入系統編號".Equals(tb_PurchaseNo.Text))
			{
				tb_PurchaseNo.Text = "";
			}
			tb_PurchaseNo.ForeColor = Color.Black;
			tb_PurchaseNo.Refresh();
		}

		private void tb_PurchaseNo_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_PurchaseNo.Text))
			{
				tb_PurchaseNo.Text = "請輸入系統編號";
				tb_PurchaseNo.ForeColor = Color.Gray;
			}
			tb_PurchaseNo.Refresh();
		}

		private void tb_PurchaseCustomNo_Enter(object sender, EventArgs e)
		{
			if ("請輸入進貨編號".Equals(tb_PurchaseCustomNo.Text))
			{
				tb_PurchaseCustomNo.Text = "";
			}
			tb_PurchaseCustomNo.ForeColor = Color.Black;
			tb_PurchaseCustomNo.Refresh();
		}

		private void tb_PurchaseCustomNo_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tb_PurchaseCustomNo.Text))
			{
				tb_PurchaseCustomNo.Text = "請輸入進貨編號";
				tb_PurchaseCustomNo.ForeColor = Color.Gray;
			}
			tb_PurchaseCustomNo.Refresh();
		}

		private void btn_reset_Click(object sender, EventArgs e)
		{
			tb_PurchaseCustomNo.Text = "請輸入進貨編號";
			tb_PurchaseCustomNo.ForeColor = Color.Gray;
			tb_PurchaseNo.Text = "請輸入系統編號";
			tb_PurchaseNo.ForeColor = Color.Gray;
			tb_SupplierName.Text = "請輸入廠商名稱關鍵字";
			tb_SupplierName.ForeColor = Color.Gray;
			cb_Normal.Checked = false;
			cb_Update.Checked = false;
			cb_Cancel.Checked = false;
			dataGridView1.Rows.Clear();
			dt_PuschaseDateStart.ResetText();
			dt_PuschaseDateStart.Checked = false;
			dt_PuschaseDateEnd.ResetText();
			dt_PuschaseDateEnd.Checked = false;
		}

		private void btn_Search_Click(object sender, EventArgs e)
		{
			string text = " a.SupplierNo = b.SupplierNo ";
			int num = 0;
			List<string> list = new List<string>();
			if (dt_PuschaseDateStart.Checked)
			{
				text = text + " and a.PurchaseDate between {" + num + "} and {" + (num + 1) + "} ";
				list.Add(dt_PuschaseDateStart.Value.ToString("yyyy-MM-dd"));
				list.Add(dt_PuschaseDateEnd.Value.AddDays(1.0).AddSeconds(-1.0).ToString("yyyy-MM-dd"));
				num += 2;
			}
			if (dataGridView1.Rows.Count > 0)
			{
				text += "and a.SupplierNo in (";
				foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
				{
					text = text + "{" + num + "},";
					list.Add(item2.Cells["hiddenGDSNO"].Value.ToString());
					num++;
				}
				text = text.Substring(0, text.Length - 1) + ")";
			}
			if (tb_PurchaseCustomNo.Text != "請輸入進貨編號")
			{
				text = text + " and a.PurchaseCustomNo like {" + num + "}";
				list.Add("%" + tb_PurchaseCustomNo.Text.Trim() + "%");
				num++;
			}
			if (tb_PurchaseNo.Text != "請輸入系統編號")
			{
				text = text + " and a.PurchaseNo like {" + num + "}";
				list.Add("%" + tb_PurchaseNo.Text.Trim() + "%");
				num++;
			}
			if (tb_SupplierName.Text != "請輸入廠商名稱關鍵字")
			{
				text = text + " and b.SupplierName like {" + num + "}";
				list.Add("%" + tb_SupplierName.Text + "%");
				num++;
			}
			string text2 = "";
			if (cb_Normal.Checked)
			{
				text2 += "0,";
			}
			if (cb_Update.Checked)
			{
				text2 += "1,";
			}
			if (cb_Cancel.Checked)
			{
				text2 += "2,";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text += " and a.Status in (";
				text2 = text2.Substring(0, text2.Length - 1);
				string[] array = text2.Split(',');
				foreach (string item in array)
				{
					text = text + "{" + num + "},";
					list.Add(item);
					num++;
				}
				text = text.Substring(0, text.Length - 1) + ")";
			}
			string strSelectField = "a.UpdateDate,a.Status,a.PurchaseNo,a.Total,a.PurchaseDate,b.SupplierName";
			(base.Owner as frmInventoryMangement).dt = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, strSelectField, "hypos_PurchaseGoods_Master a, hypos_Supplier b", text, "", null, list.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
			(base.Owner as frmInventoryMangement).changePage(1);
			switchForm(base.Owner);
		}

		private void dt_PuschaseDateStart_ValueChanged(object sender, EventArgs e)
		{
			dt_PuschaseDateEnd.MinDate = DateTime.Parse(dt_PuschaseDateStart.Text);
		}

		private void dt_PuschaseDateEnd_ValueChanged(object sender, EventArgs e)
		{
			dt_PuschaseDateStart.MaxDate = DateTime.Parse(dt_PuschaseDateEnd.Text);
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == 1)
			{
				dataGridView1.Rows.RemoveAt(e.RowIndex);
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
			panel5 = new System.Windows.Forms.Panel();
			label10 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			panel2 = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			tb_SupplierName = new System.Windows.Forms.TextBox();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			tb_PurchaseCustomNo = new System.Windows.Forms.TextBox();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			dt_PuschaseDateStart = new System.Windows.Forms.DateTimePicker();
			label2 = new System.Windows.Forms.Label();
			dt_PuschaseDateEnd = new System.Windows.Forms.DateTimePicker();
			tb_PurchaseNo = new System.Windows.Forms.TextBox();
			flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			DGV_SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			DVG_Status = new System.Windows.Forms.DataGridViewButtonColumn();
			hiddenGDSNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
			btn_chooseMultiSupplier = new System.Windows.Forms.Button();
			flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			cb_Normal = new POS_Client.MyCheckBox();
			cb_Update = new POS_Client.MyCheckBox();
			cb_Cancel = new POS_Client.MyCheckBox();
			btn_Search = new System.Windows.Forms.Button();
			btn_reset = new System.Windows.Forms.Button();
			btn_Cancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel5.SuspendLayout();
			panel4.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			panel3.SuspendLayout();
			panel6.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			flowLayoutPanel3.SuspendLayout();
			SuspendLayout();
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label10);
			panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			panel5.Location = new System.Drawing.Point(1, 241);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(162, 47);
			panel5.TabIndex = 23;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(80, 16);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(74, 21);
			label10.TabIndex = 0;
			label10.Text = "進貨編號";
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label8);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Location = new System.Drawing.Point(1, 193);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(162, 47);
			panel4.TabIndex = 22;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.White;
			label8.Location = new System.Drawing.Point(48, 16);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(106, 21);
			label8.TabIndex = 0;
			label8.Text = "系統訂單編號";
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(1, 1);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(162, 47);
			panel1.TabIndex = 19;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(48, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(106, 21);
			label1.TabIndex = 0;
			label1.Text = "進貨日期區間";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			panel2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel2.Controls.Add(label4);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Location = new System.Drawing.Point(1, 49);
			panel2.Margin = new System.Windows.Forms.Padding(0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(162, 47);
			panel2.TabIndex = 20;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.White;
			label4.Location = new System.Drawing.Point(80, 13);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(74, 21);
			label4.TabIndex = 0;
			label4.Text = "廠商名稱";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label6);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 97);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(162, 95);
			panel3.TabIndex = 21;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(80, 38);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(74, 21);
			label6.TabIndex = 0;
			label6.Text = "選擇廠商";
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			panel6.Location = new System.Drawing.Point(1, 289);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(162, 50);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(80, 17);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(74, 21);
			label12.TabIndex = 0;
			label12.Text = "訂單狀態";
			tb_SupplierName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_SupplierName.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_SupplierName.ForeColor = System.Drawing.Color.Gray;
			tb_SupplierName.Location = new System.Drawing.Point(179, 56);
			tb_SupplierName.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
			tb_SupplierName.Name = "tb_SupplierName";
			tb_SupplierName.Size = new System.Drawing.Size(416, 33);
			tb_SupplierName.TabIndex = 1;
			tb_SupplierName.Text = "請輸入廠商名稱關鍵字";
			tb_SupplierName.Enter += new System.EventHandler(tb_SupplierName_Enter);
			tb_SupplierName.Leave += new System.EventHandler(tb_SupplierName_Leave);
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel1.Controls.Add(tb_PurchaseCustomNo, 1, 4);
			tableLayoutPanel1.Controls.Add(panel6, 0, 5);
			tableLayoutPanel1.Controls.Add(panel3, 0, 2);
			tableLayoutPanel1.Controls.Add(panel2, 0, 1);
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(panel4, 0, 3);
			tableLayoutPanel1.Controls.Add(panel5, 0, 4);
			tableLayoutPanel1.Controls.Add(tb_SupplierName, 1, 1);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
			tableLayoutPanel1.Controls.Add(tb_PurchaseNo, 1, 3);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 1, 2);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 1, 5);
			tableLayoutPanel1.Location = new System.Drawing.Point(12, 54);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 6;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.27478f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.27478f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.62609f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.27478f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.27478f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.27478f));
			tableLayoutPanel1.Size = new System.Drawing.Size(957, 340);
			tableLayoutPanel1.TabIndex = 1;
			tb_PurchaseCustomNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_PurchaseCustomNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_PurchaseCustomNo.ForeColor = System.Drawing.Color.Gray;
			tb_PurchaseCustomNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_PurchaseCustomNo.Location = new System.Drawing.Point(179, 248);
			tb_PurchaseCustomNo.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
			tb_PurchaseCustomNo.Name = "tb_PurchaseCustomNo";
			tb_PurchaseCustomNo.Size = new System.Drawing.Size(416, 33);
			tb_PurchaseCustomNo.TabIndex = 42;
			tb_PurchaseCustomNo.Text = "請輸入進貨編號";
			tb_PurchaseCustomNo.Enter += new System.EventHandler(tb_PurchaseCustomNo_Enter);
			tb_PurchaseCustomNo.Leave += new System.EventHandler(tb_PurchaseCustomNo_Leave);
			flowLayoutPanel1.Controls.Add(dt_PuschaseDateStart);
			flowLayoutPanel1.Controls.Add(label2);
			flowLayoutPanel1.Controls.Add(dt_PuschaseDateEnd);
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel1.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(792, 47);
			flowLayoutPanel1.TabIndex = 45;
			dt_PuschaseDateStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dt_PuschaseDateStart.CalendarFont = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dt_PuschaseDateStart.Checked = false;
			dt_PuschaseDateStart.CustomFormat = "yyyy-MM-dd";
			dt_PuschaseDateStart.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dt_PuschaseDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dt_PuschaseDateStart.Location = new System.Drawing.Point(15, 8);
			dt_PuschaseDateStart.Margin = new System.Windows.Forms.Padding(15, 8, 3, 3);
			dt_PuschaseDateStart.Name = "dt_PuschaseDateStart";
			dt_PuschaseDateStart.ShowCheckBox = true;
			dt_PuschaseDateStart.Size = new System.Drawing.Size(188, 33);
			dt_PuschaseDateStart.TabIndex = 4;
			dt_PuschaseDateStart.Value = new System.DateTime(2016, 10, 11, 0, 0, 0, 0);
			dt_PuschaseDateStart.ValueChanged += new System.EventHandler(dt_PuschaseDateStart_ValueChanged);
			label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.Location = new System.Drawing.Point(211, 11);
			label2.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(22, 21);
			label2.TabIndex = 44;
			label2.Text = "~";
			dt_PuschaseDateEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dt_PuschaseDateEnd.CalendarFont = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dt_PuschaseDateEnd.Checked = false;
			dt_PuschaseDateEnd.CustomFormat = "yyyy-MM-dd";
			dt_PuschaseDateEnd.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dt_PuschaseDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dt_PuschaseDateEnd.Location = new System.Drawing.Point(243, 8);
			dt_PuschaseDateEnd.Margin = new System.Windows.Forms.Padding(10, 8, 3, 3);
			dt_PuschaseDateEnd.Name = "dt_PuschaseDateEnd";
			dt_PuschaseDateEnd.ShowCheckBox = true;
			dt_PuschaseDateEnd.Size = new System.Drawing.Size(188, 33);
			dt_PuschaseDateEnd.TabIndex = 4;
			dt_PuschaseDateEnd.Value = new System.DateTime(2016, 10, 11, 0, 0, 0, 0);
			dt_PuschaseDateEnd.ValueChanged += new System.EventHandler(dt_PuschaseDateEnd_ValueChanged);
			tb_PurchaseNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_PurchaseNo.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_PurchaseNo.ForeColor = System.Drawing.Color.Gray;
			tb_PurchaseNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_PurchaseNo.Location = new System.Drawing.Point(179, 200);
			tb_PurchaseNo.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
			tb_PurchaseNo.Name = "tb_PurchaseNo";
			tb_PurchaseNo.Size = new System.Drawing.Size(416, 33);
			tb_PurchaseNo.TabIndex = 42;
			tb_PurchaseNo.Text = "請輸入系統編號";
			tb_PurchaseNo.Enter += new System.EventHandler(tb_PurchaseNo_Enter);
			tb_PurchaseNo.Leave += new System.EventHandler(tb_PurchaseNo_Leave);
			flowLayoutPanel2.Controls.Add(dataGridView1);
			flowLayoutPanel2.Controls.Add(btn_chooseMultiSupplier);
			flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel2.Location = new System.Drawing.Point(164, 97);
			flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel2.Name = "flowLayoutPanel2";
			flowLayoutPanel2.Size = new System.Drawing.Size(792, 95);
			flowLayoutPanel2.TabIndex = 46;
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
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(DGV_SupplierName, DVG_Status, hiddenGDSNO);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.Location = new System.Drawing.Point(15, 0);
			dataGridView1.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 32;
			dataGridView1.Size = new System.Drawing.Size(416, 95);
			dataGridView1.TabIndex = 72;
			dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
			DGV_SupplierName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			DGV_SupplierName.DefaultCellStyle = dataGridViewCellStyle4;
			DGV_SupplierName.FillWeight = 80f;
			DGV_SupplierName.HeaderText = "供應商名稱";
			DGV_SupplierName.Name = "DGV_SupplierName";
			DGV_SupplierName.ReadOnly = true;
			DGV_SupplierName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			DGV_SupplierName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			DVG_Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
			DVG_Status.DefaultCellStyle = dataGridViewCellStyle5;
			DVG_Status.FillWeight = 60f;
			DVG_Status.HeaderText = "移除";
			DVG_Status.Name = "DVG_Status";
			DVG_Status.ReadOnly = true;
			DVG_Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			DVG_Status.Text = "X";
			DVG_Status.Width = 80;
			hiddenGDSNO.HeaderText = "";
			hiddenGDSNO.Name = "hiddenGDSNO";
			hiddenGDSNO.ReadOnly = true;
			hiddenGDSNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			hiddenGDSNO.Visible = false;
			btn_chooseMultiSupplier.Anchor = System.Windows.Forms.AnchorStyles.Left;
			btn_chooseMultiSupplier.BackColor = System.Drawing.Color.FromArgb(41, 162, 198);
			btn_chooseMultiSupplier.FlatAppearance.BorderSize = 0;
			btn_chooseMultiSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_chooseMultiSupplier.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_chooseMultiSupplier.ForeColor = System.Drawing.Color.White;
			btn_chooseMultiSupplier.Location = new System.Drawing.Point(446, 32);
			btn_chooseMultiSupplier.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
			btn_chooseMultiSupplier.Name = "btn_chooseMultiSupplier";
			btn_chooseMultiSupplier.Size = new System.Drawing.Size(71, 31);
			btn_chooseMultiSupplier.TabIndex = 33;
			btn_chooseMultiSupplier.Text = "加入";
			btn_chooseMultiSupplier.UseVisualStyleBackColor = false;
			btn_chooseMultiSupplier.Click += new System.EventHandler(btn_chooseMultiSupplier_Click);
			flowLayoutPanel3.Controls.Add(cb_Normal);
			flowLayoutPanel3.Controls.Add(cb_Update);
			flowLayoutPanel3.Controls.Add(cb_Cancel);
			flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel3.Location = new System.Drawing.Point(164, 289);
			flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel3.Name = "flowLayoutPanel3";
			flowLayoutPanel3.Size = new System.Drawing.Size(792, 50);
			flowLayoutPanel3.TabIndex = 47;
			cb_Normal.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_Normal.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_Normal.Location = new System.Drawing.Point(15, 8);
			cb_Normal.Margin = new System.Windows.Forms.Padding(15, 8, 3, 3);
			cb_Normal.Name = "cb_Normal";
			cb_Normal.Size = new System.Drawing.Size(82, 35);
			cb_Normal.TabIndex = 47;
			cb_Normal.Text = "正常";
			cb_Normal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			cb_Normal.UseVisualStyleBackColor = true;
			cb_Update.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_Update.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_Update.Location = new System.Drawing.Point(115, 8);
			cb_Update.Margin = new System.Windows.Forms.Padding(15, 8, 3, 3);
			cb_Update.Name = "cb_Update";
			cb_Update.Size = new System.Drawing.Size(135, 35);
			cb_Update.TabIndex = 47;
			cb_Update.Text = "正常(變更)";
			cb_Update.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			cb_Update.UseVisualStyleBackColor = true;
			cb_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cb_Cancel.Location = new System.Drawing.Point(268, 8);
			cb_Cancel.Margin = new System.Windows.Forms.Padding(15, 8, 3, 3);
			cb_Cancel.Name = "cb_Cancel";
			cb_Cancel.Size = new System.Drawing.Size(80, 35);
			cb_Cancel.TabIndex = 47;
			cb_Cancel.Text = "取消";
			cb_Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			cb_Cancel.UseVisualStyleBackColor = true;
			btn_Search.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_Search.FlatAppearance.BorderSize = 0;
			btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_Search.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_Search.ForeColor = System.Drawing.Color.White;
			btn_Search.Location = new System.Drawing.Point(320, 413);
			btn_Search.Name = "btn_Search";
			btn_Search.Size = new System.Drawing.Size(139, 38);
			btn_Search.TabIndex = 33;
			btn_Search.Text = "查詢";
			btn_Search.UseVisualStyleBackColor = false;
			btn_Search.Click += new System.EventHandler(btn_Search_Click);
			btn_reset.BackColor = System.Drawing.Color.Gray;
			btn_reset.FlatAppearance.BorderSize = 0;
			btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_reset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_reset.ForeColor = System.Drawing.Color.White;
			btn_reset.Location = new System.Drawing.Point(482, 413);
			btn_reset.Name = "btn_reset";
			btn_reset.Size = new System.Drawing.Size(83, 38);
			btn_reset.TabIndex = 33;
			btn_reset.Text = "重設";
			btn_reset.UseVisualStyleBackColor = false;
			btn_reset.Click += new System.EventHandler(btn_reset_Click);
			btn_Cancel.BackColor = System.Drawing.Color.Gray;
			btn_Cancel.FlatAppearance.BorderSize = 0;
			btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_Cancel.ForeColor = System.Drawing.Color.White;
			btn_Cancel.Location = new System.Drawing.Point(588, 413);
			btn_Cancel.Name = "btn_Cancel";
			btn_Cancel.Size = new System.Drawing.Size(83, 38);
			btn_Cancel.TabIndex = 33;
			btn_Cancel.Text = "取消";
			btn_Cancel.UseVisualStyleBackColor = false;
			btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(btn_Cancel);
			base.Controls.Add(btn_reset);
			base.Controls.Add(btn_Search);
			base.Controls.Add(tableLayoutPanel1);
			base.Name = "frmSearchPurchase";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "";
			base.Controls.SetChildIndex(tableLayoutPanel1, 0);
			base.Controls.SetChildIndex(btn_Search, 0);
			base.Controls.SetChildIndex(btn_reset, 0);
			base.Controls.SetChildIndex(btn_Cancel, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			flowLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			flowLayoutPanel3.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
