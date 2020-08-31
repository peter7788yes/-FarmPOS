using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace POS_Client
{
	public class frmDataTransferPrecautions : Form
	{
		private IContainer components;

		private Button btn_mode_1;

		private Button btn_mode_0;

		private Label label3;

		private Label label4;

		private Label label5;

		private LinkLabel linkLabel1;

		private LinkLabel linkLabel2;

		public frmDataTransferPrecautions()
		{
			InitializeComponent();
		}

		private void btn_mode_0_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("轉入程序視電腦效能與資料庫大小而異，可能會需要花費30分~一小時之轉入程序，這段時間內請不要關閉程式或關閉電腦。點選『確定』立即開始進行移轉？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			try
			{
				if (!Directory.Exists("C:\\Hypos"))
				{
					Directory.CreateDirectory("C:\\Hypos");
				}
				using (FileStream stream = new FileStream("C:\\Hypos\\conn_log.txt", FileMode.Create, FileAccess.Write))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Unicode))
					{
						streamWriter.WriteLine("9");
					}
				}
				Process process = Process.Start("BakFileRestore\\BakFileRestore.exe");
				process.WaitForInputIdle();
				process.WaitForExit();
				if (process != null)
				{
					process.Close();
					process.Dispose();
					process = null;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				if (File.Exists("C:\\Hypos\\conn_log.txt"))
				{
					try
					{
						using (StreamReader streamReader = new StreamReader("C:\\\\Hypos\\\\conn_log.txt", Encoding.Unicode))
						{
							string text = streamReader.ReadToEnd();
							if (!"9".Equals(text.Substring(0, 1)))
							{
								Close();
							}
						}
					}
					catch (Exception)
					{
					}
				}
			}
		}

		private void btn_mode_1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://www.microsoft.com/zh-TW/download/details.aspx?id=30437");
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://www.microsoft.com/zh-tw/download/details.aspx?id=3743");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS_Client.frmDataTransferPrecautions));
			btn_mode_1 = new System.Windows.Forms.Button();
			btn_mode_0 = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			linkLabel1 = new System.Windows.Forms.LinkLabel();
			linkLabel2 = new System.Windows.Forms.LinkLabel();
			SuspendLayout();
			btn_mode_1.BackColor = System.Drawing.Color.FromArgb(36, 168, 208);
			btn_mode_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_mode_1.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_mode_1.ForeColor = System.Drawing.Color.White;
			btn_mode_1.Location = new System.Drawing.Point(587, 536);
			btn_mode_1.Name = "btn_mode_1";
			btn_mode_1.Size = new System.Drawing.Size(270, 70);
			btn_mode_1.TabIndex = 34;
			btn_mode_1.Text = "放棄轉入，離開";
			btn_mode_1.UseVisualStyleBackColor = false;
			btn_mode_1.Click += new System.EventHandler(btn_mode_1_Click);
			btn_mode_0.BackColor = System.Drawing.Color.FromArgb(255, 109, 49);
			btn_mode_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_mode_0.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_mode_0.ForeColor = System.Drawing.Color.White;
			btn_mode_0.Location = new System.Drawing.Point(128, 536);
			btn_mode_0.Name = "btn_mode_0";
			btn_mode_0.Size = new System.Drawing.Size(270, 70);
			btn_mode_0.TabIndex = 33;
			btn_mode_0.Text = "同意，開始轉入";
			btn_mode_0.UseVisualStyleBackColor = false;
			btn_mode_0.Click += new System.EventHandler(btn_mode_0_Click);
			label3.Font = new System.Drawing.Font("微軟正黑體", 27.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.Location = new System.Drawing.Point(9, 9);
			label3.Margin = new System.Windows.Forms.Padding(0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(508, 45);
			label3.TabIndex = 35;
			label3.Text = "光碟版POS資料轉入注意事項";
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label4.Location = new System.Drawing.Point(13, 54);
			label4.Margin = new System.Windows.Forms.Padding(0);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(525, 29);
			label4.TabIndex = 35;
			label4.Text = "移轉前請先閱讀重要需知，點選「同意」後即會立即開始進行轉入動作。";
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label5.Location = new System.Drawing.Point(75, 83);
			label5.Margin = new System.Windows.Forms.Padding(0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(830, 450);
			label5.TabIndex = 36;
			label5.Text = resources.GetString("label5.Text");
			linkLabel1.AutoSize = true;
			linkLabel1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			linkLabel1.Location = new System.Drawing.Point(90, 418);
			linkLabel1.Name = "linkLabel1";
			linkLabel1.Size = new System.Drawing.Size(625, 24);
			linkLabel1.TabIndex = 37;
			linkLabel1.TabStop = true;
			linkLabel1.Text = "https://www.microsoft.com/zh-TW/download/details.aspx?id=30437";
			linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
			linkLabel2.AutoSize = true;
			linkLabel2.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			linkLabel2.Location = new System.Drawing.Point(90, 492);
			linkLabel2.Name = "linkLabel2";
			linkLabel2.Size = new System.Drawing.Size(606, 24);
			linkLabel2.TabIndex = 38;
			linkLabel2.TabStop = true;
			linkLabel2.Text = "https://www.microsoft.com/zh-tw/download/details.aspx?id=3743";
			linkLabel2.Visible = false;
			linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel2_LinkClicked);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(linkLabel2);
			base.Controls.Add(linkLabel1);
			base.Controls.Add(label5);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(btn_mode_1);
			base.Controls.Add(btn_mode_0);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmDataTransferPrecautions";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
