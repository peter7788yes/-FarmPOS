using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmRepayDialog : Form
	{
		private int _credit;

		private string _vipNo = "";

		private frmEditMember _frmEM;

		private IContainer components;

		private Button btn_close;

		private Button btn_repay;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel1;

		private Label label1;

		private TextBox tb_repay;

		public frmRepayDialog(int credit, string vipNo, frmEditMember frmEM)
		{
			InitializeComponent();
			_credit = credit;
			_vipNo = vipNo;
			_frmEM = frmEM;
		}

		private void frmRepayDialog_Load(object sender, EventArgs e)
		{
			tb_repay.Text = "請輸入還款金額";
		}

		private void btn_close_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btn_repay_Click(object sender, EventArgs e)
		{
			if (_credit == 0)
			{
				AutoClosingMessageBox.Show("此會員目前無賒帳金額");
				return;
			}
			if ("請輸入還款金額".Equals(tb_repay.Text))
			{
				AutoClosingMessageBox.Show("請先輸入賒帳還款數字");
				return;
			}
			int num = int.Parse(tb_repay.Text);
			string text = "0";
			string text2 = "0";
			string sql = "";
			if (_credit <= num)
			{
				sql = "UPDATE hypos_CUST_RTL SET Credit={1} where VipNo={0} ";
				text2 = (num - _credit).ToString();
				AutoClosingMessageBox.Show("賒帳還款「" + num + "」，還款後賒帳金額為＂0＂，需找零＂" + text2 + "＂");
			}
			if (_credit > num)
			{
				sql = "UPDATE hypos_CUST_RTL SET Credit=Credit+{1} where VipNo={0} ";
				text = (-num).ToString();
				AutoClosingMessageBox.Show("賒帳還款「" + num + "」，還款後賒帳金額為＂" + (_credit - num) + "＂");
			}
			DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[2]
			{
				_vipNo,
				text
			}, CommandOperationType.ExecuteNonQuery);
			string text3 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string[,] strFieldArray = new string[7, 2]
			{
				{
					"memberId",
					_vipNo
				},
				{
					"sellNo",
					""
				},
				{
					"editdate",
					text3
				},
				{
					"sellType",
					"2"
				},
				{
					"Cash",
					text2
				},
				{
					"Credit",
					num.ToString()
				},
				{
					"status",
					"0"
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_user_consumelog", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET RepayDate = {1} WHERE VipNo = {0}", new string[2]
			{
				_vipNo,
				text3
			}, CommandOperationType.ExecuteNonQuery);
			_frmEM.repayChange();
			Hide();
			_frmEM.Show();
		}

		private void tb_repay_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
			{
				e.Handled = true;
			}
			if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
			{
				e.Handled = true;
			}
		}

		private void tb_repay_Leave(object sender, EventArgs e)
		{
			if ("".Equals(tb_repay.Text))
			{
				tb_repay.Text = "請輸入還款金額";
			}
		}

		private void tb_repay_Enter(object sender, EventArgs e)
		{
			if ("請輸入還款金額".Equals(tb_repay.Text))
			{
				tb_repay.Text = "";
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
			btn_close = new System.Windows.Forms.Button();
			btn_repay = new System.Windows.Forms.Button();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			tb_repay = new System.Windows.Forms.TextBox();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			btn_close.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_close.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_close.ForeColor = System.Drawing.Color.White;
			btn_close.Location = new System.Drawing.Point(463, 149);
			btn_close.Name = "btn_close";
			btn_close.Size = new System.Drawing.Size(124, 34);
			btn_close.TabIndex = 45;
			btn_close.TabStop = false;
			btn_close.Text = "關閉";
			btn_close.UseVisualStyleBackColor = false;
			btn_close.Click += new System.EventHandler(btn_close_Click);
			btn_repay.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_repay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_repay.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_repay.ForeColor = System.Drawing.Color.White;
			btn_repay.Location = new System.Drawing.Point(317, 149);
			btn_repay.Name = "btn_repay";
			btn_repay.Size = new System.Drawing.Size(124, 34);
			btn_repay.TabIndex = 45;
			btn_repay.TabStop = false;
			btn_repay.Text = "賒帳還款";
			btn_repay.UseVisualStyleBackColor = false;
			btn_repay.Click += new System.EventHandler(btn_repay_Click);
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.79005f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.20995f));
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(tb_repay, 1, 0);
			tableLayoutPanel1.Location = new System.Drawing.Point(93, 71);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.Size = new System.Drawing.Size(724, 44);
			tableLayoutPanel1.TabIndex = 46;
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label1);
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(164, 44);
			panel1.TabIndex = 0;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(55, 13);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(106, 21);
			label1.TabIndex = 0;
			label1.Text = "輸入還款金額";
			tb_repay.Anchor = System.Windows.Forms.AnchorStyles.None;
			tb_repay.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tb_repay.Location = new System.Drawing.Point(178, 7);
			tb_repay.Name = "tb_repay";
			tb_repay.Size = new System.Drawing.Size(531, 29);
			tb_repay.TabIndex = 1;
			tb_repay.Enter += new System.EventHandler(tb_repay_Enter);
			tb_repay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tb_repay_KeyPress);
			tb_repay.Leave += new System.EventHandler(tb_repay_Leave);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Silver;
			base.ClientSize = new System.Drawing.Size(904, 238);
			base.ControlBox = false;
			base.Controls.Add(tableLayoutPanel1);
			base.Controls.Add(btn_repay);
			base.Controls.Add(btn_close);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmRepayDialog";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "frmSearchMember";
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
		}
	}
}
