using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmDataTransfer : MasterForm
	{
		private IContainer components;

		private Button btn_mode_1;

		private Button btn_mode_0;

		private Label label3;

		private Label label4;

		public frmDataTransfer()
		{
			InitializeComponent();
		}

		private void btn_mode_0_Click(object sender, EventArgs e)
		{
			frmDataTransferPrecautions frmDataTransferPrecautions = new frmDataTransferPrecautions();
			frmDataTransferPrecautions.Location = new Point(base.Location.X, base.Location.Y);
			frmDataTransferPrecautions.ShowDialog();
			try
			{
			}
			finally
			{
				if (File.Exists("C:\\Hypos\\Old_db.db3") && File.Exists("C:\\Hypos\\conn_log.txt"))
				{
					try
					{
						using (StreamReader streamReader = new StreamReader("C:\\\\Hypos\\\\conn_log.txt", Encoding.Unicode))
						{
							string text = streamReader.ReadToEnd();
							if ("1".Equals(text.Substring(0, 1)))
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
			if (MessageBox.Show("轉入功能只能在初次安裝時進行一次，若放棄轉入之後無法進行補轉入，確定放棄轉入？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
			{
				return;
			}
			try
			{
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_SysParam SET IsDataTransfer = 'Y'", null, CommandOperationType.ExecuteNonQuery);
				if (!Directory.Exists("C:\\Hypos"))
				{
					Directory.CreateDirectory("C:\\Hypos");
				}
				using (FileStream stream = new FileStream("C:\\Hypos\\conn_log.txt", FileMode.Create, FileAccess.Write))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Unicode))
					{
						streamWriter.WriteLine("2");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				Close();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS_Client.frmDataTransfer));
			btn_mode_1 = new System.Windows.Forms.Button();
			btn_mode_0 = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			SuspendLayout();
			btn_mode_1.BackColor = System.Drawing.Color.FromArgb(36, 168, 208);
			btn_mode_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_mode_1.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_mode_1.ForeColor = System.Drawing.Color.White;
			btn_mode_1.Location = new System.Drawing.Point(580, 172);
			btn_mode_1.Name = "btn_mode_1";
			btn_mode_1.Size = new System.Drawing.Size(258, 163);
			btn_mode_1.TabIndex = 34;
			btn_mode_1.Text = "不需轉入開始使用";
			btn_mode_1.UseVisualStyleBackColor = false;
			btn_mode_1.Click += new System.EventHandler(btn_mode_1_Click);
			btn_mode_0.BackColor = System.Drawing.Color.FromArgb(255, 109, 49);
			btn_mode_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_mode_0.Font = new System.Drawing.Font("微軟正黑體", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_mode_0.ForeColor = System.Drawing.Color.White;
			btn_mode_0.Location = new System.Drawing.Point(146, 172);
			btn_mode_0.Name = "btn_mode_0";
			btn_mode_0.Size = new System.Drawing.Size(258, 163);
			btn_mode_0.TabIndex = 33;
			btn_mode_0.Text = "光碟版資料轉入";
			btn_mode_0.UseVisualStyleBackColor = false;
			btn_mode_0.Click += new System.EventHandler(btn_mode_0_Click);
			label3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.Location = new System.Drawing.Point(146, 356);
			label3.Margin = new System.Windows.Forms.Padding(0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(265, 104);
			label3.TabIndex = 35;
			label3.Text = "原使用光碟版POS之商家，可使用此功能轉入原光碟版POS所編修之商品、會員、廠商、銷售單、以及進貨單等資料。";
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label4.Location = new System.Drawing.Point(576, 356);
			label4.Margin = new System.Windows.Forms.Padding(0);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(271, 104);
			label4.TabIndex = 35;
			label4.Text = "第一次使用POS不需要進行任何轉入的商家，立即開始使用簡易版農藥陳報POS系統。";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(btn_mode_1);
			base.Controls.Add(btn_mode_0);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "frmDataTransfer";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			base.Controls.SetChildIndex(btn_mode_0, 0);
			base.Controls.SetChildIndex(btn_mode_1, 0);
			base.Controls.SetChildIndex(label3, 0);
			base.Controls.SetChildIndex(label4, 0);
			ResumeLayout(false);
		}
	}
}
