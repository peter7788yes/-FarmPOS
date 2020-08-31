using Newtonsoft.Json;
using POS_Client.WebService;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmInitSysParam : MasterForm
	{
		private string RegisterCode = "";

		private AuthenticationWs ws;

		private IContainer components;

		private Label l_info2;

		private Label l_info1;

		private Label label1;

		private TextBox txtLicenseCode;

		private Button btnUploadSysSN;

		private Button btnReset;

		private Button btnSubmit;

		private TextBox txtShopName;

		private Label label17;

		private Label label8;

		public frmInitSysParam()
		{
			InitializeComponent();
			ws = new AuthenticationWs();
		}

		private void frmInitSysParam_Load(object sender, EventArgs e)
		{
			btnUploadSysSN.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_RegisterLicense order by CreateDate desc limit 1 ", null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				txtLicenseCode.Text = dataTable.Rows[0]["LicenseCode"].ToString();
				txtShopName.Text = dataTable.Rows[0]["ShopName"].ToString();
				string text = dataTable.Rows[0]["isApproved"].ToString();
				if ("N".Equals(text))
				{
					l_info1.Text = "申請失敗，請再重新申請一次";
					RegisterCode = "";
				}
				else if (text == null || "".Equals(text))
				{
					btnSubmit.Enabled = true;
					btnUploadSysSN.Enabled = false;
					btnReset.Enabled = false;
					txtLicenseCode.Enabled = false;
					txtShopName.Enabled = false;
					btnSubmit.BackColor = Color.FromArgb(0, 153, 204);
					RegisterCode = dataTable.Rows[0]["RegisterCode"].ToString();
				}
			}
			else if (!Program.IsDeployClickOnce)
			{
				txtLicenseCode.Text = "N00019";
				txtShopName.Text = "興農股份有限公司臺東營業所";
			}
		}

		private void btnUploadSysSN_Click(object sender, EventArgs e)
		{
			string text = "";
			if (txtLicenseCode.Text == "請輸入販賣執照證號")
			{
				text += "請輸入販賣執照證號 \n";
			}
			if (txtShopName.Text == "請輸入商家商業名稱")
			{
				text += "請輸入商家商業名稱";
			}
			if (text != "")
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			string text2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
			RegisterObject registerObject = new RegisterObject();
			registerObject.license = txtLicenseCode.Text;
			registerObject.name = txtShopName.Text;
			registerObject.hddSerialId = Program.HardDiskSerialNo;
			registerObject.applyTime = text2;
			string text3 = Program.Encrypt(JsonConvert.SerializeObject(registerObject, Formatting.Indented));
			string[,] strFieldArray = new string[5, 2]
			{
				{
					"RegisterCode",
					text3
				},
				{
					"LicenseCode",
					registerObject.license
				},
				{
					"ShopName",
					registerObject.name
				},
				{
					"HardDiskSerialNo",
					registerObject.hddSerialId
				},
				{
					"CreateDate",
					text2
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_RegisterLicense", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			RegisterResultObject registerResultObject = ws.uploadApplySerial(text3);
			if (bool.Parse(registerResultObject.isSuccess))
			{
				AutoClosingMessageBox.Show(registerResultObject.message);
				btnSubmit.Enabled = true;
				btnUploadSysSN.Enabled = false;
				btnReset.Enabled = false;
				txtLicenseCode.Enabled = false;
				txtShopName.Enabled = false;
				btnSubmit.BackColor = Color.FromArgb(0, 153, 204);
				RegisterCode = text3;
			}
			else
			{
				AutoClosingMessageBox.Show("上傳失敗:\n" + registerResultObject.message);
			}
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			AuthResultObject authResultObject = ws.hasInUseFirst(RegisterCode);
			string[] strParameterArray = new string[2]
			{
				RegisterCode,
				DateTime.Now.ToString()
			};
			if (bool.Parse(authResultObject.inUse))
			{
				AutoClosingMessageBox.Show(authResultObject.message);
				Program.LincenseCode = txtLicenseCode.Text;
				Program.SiteNo = authResultObject.serial.PadLeft(2, '0');
				Program.ShopType = authResultObject.shopType;
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_RegisterLicense SET isApproved = 'Y', ApproveDate = {1} where RegisterCode = {0} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_SysParam SET SiteNo = {0} ", new string[1]
				{
					authResultObject.serial.PadLeft(2, '0')
				}, CommandOperationType.ExecuteNonQuery);
				if (!string.IsNullOrEmpty(Program.ShopType))
				{
					string sql = "SELECT ShopIdNo FROM hypos_ShopInfoManage";
					if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
					{
						if (Program.ShopType.Equals("0"))
						{
							DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_ShopInfoManage SET IsRetailer = {0}, IsWholesaler = {1} ", new string[2]
							{
								"ON",
								"ON"
							}, CommandOperationType.ExecuteNonQuery);
						}
						else if (Program.ShopType.Equals("1"))
						{
							DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_ShopInfoManage SET IsRetailer = {0}, IsWholesaler = {1} ", new string[2]
							{
								"ON",
								"OFF"
							}, CommandOperationType.ExecuteNonQuery);
						}
						else if (Program.ShopType.Equals("2"))
						{
							DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_ShopInfoManage SET IsRetailer = {0}, IsWholesaler = {1} ", new string[2]
							{
								"OFF",
								"ON"
							}, CommandOperationType.ExecuteNonQuery);
						}
					}
					else if (Program.ShopType.Equals("0"))
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_ShopInfoManage ( IsRetailer, IsWholesaler) VALUES( {0}, {1})", new string[2]
						{
							"ON",
							"ON"
						}, CommandOperationType.ExecuteNonQuery);
					}
					else if (Program.ShopType.Equals("1"))
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_ShopInfoManage ( IsRetailer, IsWholesaler) VALUES( {0}, {1}) ", new string[2]
						{
							"ON",
							"OFF"
						}, CommandOperationType.ExecuteNonQuery);
					}
					else if (Program.ShopType.Equals("2"))
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_ShopInfoManage ( IsRetailer, IsWholesaler) VALUES( {0}, {1}) ", new string[2]
						{
							"OFF",
							"ON"
						}, CommandOperationType.ExecuteNonQuery);
					}
				}
				new frmDownload(Program.LincenseCode, "").ShowDialog();
				if (Program.IsDataTransfer)
				{
					string value = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT SystemMode FROM hypos_SysParam", null, CommandOperationType.ExecuteScalar).ToString();
					string value2 = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT IsDataTransfer FROM hypos_SysParam", null, CommandOperationType.ExecuteScalar).ToString();
					if (!"Y".Equals(value2) && "".Equals(value))
					{
						frmDataTransfer frmDataTransfer = new frmDataTransfer();
						frmDataTransfer.Location = new Point(base.Location.X, base.Location.Y);
						frmDataTransfer.ShowDialog();
						if (File.Exists("C:\\Hypos\\Old_db.db3") && File.Exists("C:\\Hypos\\conn_log.txt"))
						{
							try
							{
								using (StreamReader streamReader = new StreamReader("C:\\\\Hypos\\\\conn_log.txt", Encoding.Unicode))
								{
									string text = streamReader.ReadToEnd();
									if ("1".Equals(text.Substring(0, 1)))
									{
										try
										{
											AutoBackupDT();
											new dbDataTransfer().ShowDialog();
											DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_SysParam SET IsDataTransfer = 'Y'", null, CommandOperationType.ExecuteNonQuery);
										}
										catch (Exception ex)
										{
											MessageBox.Show(string.Format("dbDataTransfer發生例外狀況:「{0}」", ex.ToString()));
										}
									}
									else if ("2".Equals(text.Substring(0, 1)))
									{
										DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_SysParam SET IsDataTransfer = 'Y'", null, CommandOperationType.ExecuteNonQuery);
										MessageBox.Show("舊POS資料移轉程序已取消");
									}
									else
									{
										MessageBox.Show("舊POS系統資料匯入SQLite失敗。");
									}
								}
							}
							catch (Exception ex2)
							{
								MessageBox.Show(string.Format("開啟紀錄檔發生例外狀況:「{0}」", ex2.ToString()));
							}
						}
						else if (File.Exists("C:\\Hypos\\conn_log.txt"))
						{
							try
							{
								using (StreamReader streamReader2 = new StreamReader("C:\\\\Hypos\\\\conn_log.txt", Encoding.Unicode))
								{
									string text2 = streamReader2.ReadToEnd();
									if (!"2".Equals(text2.Substring(0, 1)))
									{
										MessageBox.Show("移轉程序失敗並結束。");
										return;
									}
								}
							}
							catch (Exception)
							{
							}
						}
					}
				}
				switchForm(new frmLogin());
			}
			else
			{
				AutoClosingMessageBox.Show(authResultObject.message);
				if ("ERROR_BAN".Equals(authResultObject.errorCode) || "ERROR_UNUSE".Equals(authResultObject.errorCode) || "ERROR_EMPTY".Equals(authResultObject.errorCode))
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_RegisterLicense SET isApproved = 'N', ApproveDate = {1} where RegisterCode = {0} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
					btnSubmit.Enabled = false;
					btnUploadSysSN.Enabled = true;
					btnReset.Enabled = true;
					txtLicenseCode.Enabled = true;
					txtShopName.Enabled = true;
					btnSubmit.BackColor = Color.DimGray;
				}
			}
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			txtLicenseCode.Text = "請輸入販賣執照證號";
			txtShopName.Text = "請輸入商家商業名稱";
		}

		private void txtLicenseCode_Enter(object sender, EventArgs e)
		{
			if (txtLicenseCode.Text == "請輸入販賣執照證號")
			{
				txtLicenseCode.Text = "";
			}
		}

		private void txtLicenseCode_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtLicenseCode.Text))
			{
				txtLicenseCode.Text = "請輸入販賣執照證號";
				return;
			}
			txtLicenseCode.Text = txtLicenseCode.Text.ToUpper();
			if (!new Regex("[A-Z]\\d{5}").IsMatch(txtLicenseCode.Text))
			{
				AutoClosingMessageBox.Show("販賣執照證號格式錯誤\n請檢閱證書上的販賣業執照號碼，包含一碼英文+五碼數字，如A00005、B00012");
				txtLicenseCode.Focus();
			}
		}

		private void txtShopName_Enter(object sender, EventArgs e)
		{
			if (txtShopName.Text == "請輸入商家商業名稱")
			{
				txtShopName.Text = "";
			}
		}

		private void txtShopName_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtShopName.Text))
			{
				txtShopName.Text = "請輸入商家商業名稱";
			}
		}

		private static void AutoBackupDT()
		{
			string text = DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "AutoBackupPath", "hypos_CommonManage", "", "", null, null, CommandOperationType.ExecuteScalar).ToString();
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string str = "db_DT_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".db3";
			File.Copy(Program.DataPath + "\\db.db3", text + "\\" + str);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS_Client.frmInitSysParam));
			l_info2 = new System.Windows.Forms.Label();
			l_info1 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			txtLicenseCode = new System.Windows.Forms.TextBox();
			btnUploadSysSN = new System.Windows.Forms.Button();
			btnReset = new System.Windows.Forms.Button();
			btnSubmit = new System.Windows.Forms.Button();
			txtShopName = new System.Windows.Forms.TextBox();
			label17 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			SuspendLayout();
			l_info2.AutoSize = true;
			l_info2.Font = new System.Drawing.Font("微軟正黑體", 10f);
			l_info2.Location = new System.Drawing.Point(351, 403);
			l_info2.Name = "l_info2";
			l_info2.Size = new System.Drawing.Size(274, 18);
			l_info2.TabIndex = 38;
			l_info2.Text = "開通需經過審核，若有疑問請連繫系統客服";
			l_info1.AutoSize = true;
			l_info1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_info1.Location = new System.Drawing.Point(323, 361);
			l_info1.Name = "l_info1";
			l_info1.Size = new System.Drawing.Size(330, 21);
			l_info1.TabIndex = 33;
			l_info1.Text = "尚未申請過使用系統使用權者，請先進行申請";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 20f, System.Drawing.FontStyle.Bold);
			label1.Location = new System.Drawing.Point(292, 122);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(393, 35);
			label1.TabIndex = 29;
			label1.Text = "立即申請：請輸入商家驗證資訊";
			txtLicenseCode.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			txtLicenseCode.ForeColor = System.Drawing.Color.DarkGray;
			txtLicenseCode.ImeMode = System.Windows.Forms.ImeMode.Disable;
			txtLicenseCode.Location = new System.Drawing.Point(452, 188);
			txtLicenseCode.MaxLength = 6;
			txtLicenseCode.Name = "txtLicenseCode";
			txtLicenseCode.Size = new System.Drawing.Size(318, 29);
			txtLicenseCode.TabIndex = 1;
			txtLicenseCode.Text = "請輸入販賣執照證號";
			txtLicenseCode.Enter += new System.EventHandler(txtLicenseCode_Enter);
			txtLicenseCode.Leave += new System.EventHandler(txtLicenseCode_Leave);
			btnUploadSysSN.BackColor = System.Drawing.Color.FromArgb(255, 105, 47);
			btnUploadSysSN.FlatAppearance.BorderSize = 0;
			btnUploadSysSN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnUploadSysSN.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btnUploadSysSN.ForeColor = System.Drawing.Color.White;
			btnUploadSysSN.Location = new System.Drawing.Point(363, 279);
			btnUploadSysSN.Name = "btnUploadSysSN";
			btnUploadSysSN.Size = new System.Drawing.Size(110, 43);
			btnUploadSysSN.TabIndex = 3;
			btnUploadSysSN.Text = "送出申請";
			btnUploadSysSN.UseVisualStyleBackColor = false;
			btnUploadSysSN.Click += new System.EventHandler(btnUploadSysSN_Click);
			btnReset.BackColor = System.Drawing.Color.FromArgb(170, 170, 170);
			btnReset.FlatAppearance.BorderSize = 0;
			btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnReset.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btnReset.ForeColor = System.Drawing.Color.White;
			btnReset.Location = new System.Drawing.Point(500, 279);
			btnReset.Name = "btnReset";
			btnReset.Size = new System.Drawing.Size(110, 43);
			btnReset.TabIndex = 34;
			btnReset.Text = "重新輸入";
			btnReset.UseVisualStyleBackColor = false;
			btnReset.Click += new System.EventHandler(btnReset_Click);
			btnSubmit.BackColor = System.Drawing.Color.DimGray;
			btnSubmit.Enabled = false;
			btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnSubmit.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btnSubmit.ForeColor = System.Drawing.Color.White;
			btnSubmit.Location = new System.Drawing.Point(394, 463);
			btnSubmit.Name = "btnSubmit";
			btnSubmit.Size = new System.Drawing.Size(188, 72);
			btnSubmit.TabIndex = 4;
			btnSubmit.Text = "立即開通";
			btnSubmit.UseVisualStyleBackColor = false;
			btnSubmit.Click += new System.EventHandler(btnSubmit_Click);
			txtShopName.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			txtShopName.ForeColor = System.Drawing.Color.DarkGray;
			txtShopName.Location = new System.Drawing.Point(452, 221);
			txtShopName.MaxLength = 50;
			txtShopName.Name = "txtShopName";
			txtShopName.Size = new System.Drawing.Size(318, 29);
			txtShopName.TabIndex = 2;
			txtShopName.Text = "請輸入商家商業名稱";
			txtShopName.Enter += new System.EventHandler(txtShopName_Enter);
			txtShopName.Leave += new System.EventHandler(txtShopName_Leave);
			label17.AutoSize = true;
			label17.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			label17.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label17.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			label17.Location = new System.Drawing.Point(206, 221);
			label17.MaximumSize = new System.Drawing.Size(200, 0);
			label17.MinimumSize = new System.Drawing.Size(240, 0);
			label17.Name = "label17";
			label17.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			label17.Size = new System.Drawing.Size(240, 30);
			label17.TabIndex = 35;
			label17.Text = "商家商業名稱";
			label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			label8.AutoSize = true;
			label8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			label8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			label8.Location = new System.Drawing.Point(206, 188);
			label8.MaximumSize = new System.Drawing.Size(200, 0);
			label8.MinimumSize = new System.Drawing.Size(240, 0);
			label8.Name = "label8";
			label8.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
			label8.Size = new System.Drawing.Size(240, 30);
			label8.TabIndex = 34;
			label8.Text = "販賣執照證號";
			label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(977, 657);
			base.Controls.Add(l_info2);
			base.Controls.Add(l_info1);
			base.Controls.Add(label1);
			base.Controls.Add(txtLicenseCode);
			base.Controls.Add(btnUploadSysSN);
			base.Controls.Add(btnReset);
			base.Controls.Add(btnSubmit);
			base.Controls.Add(txtShopName);
			base.Controls.Add(label17);
			base.Controls.Add(label8);
			Cursor = System.Windows.Forms.Cursors.Default;
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "frmInitSysParam";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			base.Load += new System.EventHandler(frmInitSysParam_Load);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label17, 0);
			base.Controls.SetChildIndex(txtShopName, 0);
			base.Controls.SetChildIndex(btnSubmit, 0);
			base.Controls.SetChildIndex(btnReset, 0);
			base.Controls.SetChildIndex(btnUploadSysSN, 0);
			base.Controls.SetChildIndex(txtLicenseCode, 0);
			base.Controls.SetChildIndex(label1, 0);
			base.Controls.SetChildIndex(l_info1, 0);
			base.Controls.SetChildIndex(l_info2, 0);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
