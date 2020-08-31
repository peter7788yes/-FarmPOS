using POS_Client.POS_WS_POS;
using POS_Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Xml;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmSystemSetup : MasterThinForm
	{
		private string receiveType = "";

		private IContainer components;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private TabPage tabPage4;

		private TableLayoutPanel tableLayoutPanel1;

		private TextBox tb_email;

		private TextBox tb_tel;

		private Panel panel11;

		private Label label15;

		private Panel panel9;

		private Label label7;

		private Panel panel7;

		private Label label14;

		private Panel panel6;

		private Label label12;

		private Panel panel3;

		private Label label6;

		private Panel panel1;

		private Label label1;

		private Panel panel4;

		private Label label8;

		private Panel panel5;

		private Label label10;

		private Panel panel10;

		private Label label13;

		private TextBox tb_ShopIdNo;

		private Panel panel14;

		private TextBox tb_zipcode;

		private ComboBox cb_area;

		private ComboBox cb_city;

		private TextBox tb_addr;

		private Panel panel15;

		private Label label23;

		private Label l_licenseCode;

		private Label l_shopName;

		private Label l_siteNo;

		private TextBox tb_nickName;

		private TextBox tb_ParentUnitCode;

		private Panel panel2;

		private Label label2;

		private Panel panel16;

		private Label label3;

		private TableLayoutPanel tableLayoutPanel2;

		private TableLayoutPanel tableLayoutPanel3;

		private TextBox tb_FertilizerPassword;

		private TextBox tb_FertilizerAccount;

		private Panel panel13;

		private Label label16;

		private Panel panel8;

		private Label label9;

		private Panel panel12;

		private Label label11;

		private Panel panel17;

		private TextBox tb_DelarNo;

		private TextBox tb_DutyName;

		private TextBox tb_DutyIdNo;

		private Label label4;

		private Panel panel18;

		private Button btn_shopSave;

		private TextBox tb_fax;

		private Panel panel19;

		private TableLayoutPanel tableLayoutPanel6;

		private Panel panel34;

		private Label label33;

		private Panel panel35;

		private Label label34;

		private Panel panel36;

		private Label label35;

		private TableLayoutPanel tableLayoutPanel5;

		private Panel panel40;

		private Label label38;

		private Panel panel45;

		private Label label42;

		private Panel panel37;

		private Label label32;

		private Panel panel41;

		private Panel panel38;

		private Button button1;

		private Button btn_receiveA4;

		private Button btn_receive80;

		private Button btn_receive60;

		private TableLayoutPanel tableLayoutPanel8;

		private Panel panel49;

		private Label label40;

		private Panel panel50;

		private Label label41;

		private Panel panel51;

		private Label label43;

		private Panel panel52;

		private Panel panel53;

		private TableLayoutPanel tableLayoutPanel7;

		private Panel panel43;

		private Label label36;

		private Panel panel44;

		private Label label37;

		private Panel panel46;

		private Label label39;

		private Panel panel47;

		private Panel panel22;

		private Panel panel20;

		private Label label5;

		private Panel panel21;

		private Label label17;

		private Panel panel24;

		private Panel panel23;

		private Label label18;

		private Panel panel25;

		private Label label19;

		private Button btn_printHotKey;

		private CheckBox cb_shop;

		private CheckBox cb_returnM;

		private CheckBox cb_member;

		private CheckBox cb_cash;

		private Label label24;

		private Label label22;

		private Label label21;

		private Label label20;

		private TableLayoutPanel tableLayoutPanel4;

		private Button btn_createUser;

		private Button btn_batchSuspend;

		private Label label25;

		public TableLayoutPanel tlp_userManage;

		private Label label47;

		private Label label48;

		private Label label27;

		private Label label26;

		private Label label31;

		private Label label28;

		private Label label29;

		private Label label30;

		private Button btn_printerSave;

		private ComboBox cb_listPrinter;

		private ComboBox cb_receivePrinter;

		private TableLayoutPanel tableLayoutPanel10;

		private Panel panel28;

		private Label label50;

		private Panel panel29;

		private Label label51;

		private Panel panel30;

		private Button btn_restoreDataPath;

		private TextBox tb_restoreFilePath;

		private TableLayoutPanel tableLayoutPanel9;

		private Panel panel54;

		private Label label44;

		private Panel panel55;

		private Label label45;

		private Panel panel56;

		private Label label46;

		private Panel panel57;

		private Button btn_AutoBackupPath;

		private TextBox tb_autoPath;

		private Panel panel58;

		private Button btn_ManualBackup;

		private Button btn_ManualBackupPath;

		private TextBox tb_manualPath;

		private Panel panel26;

		private Button btn_executeRestore;

		private TableLayoutPanel tableLayoutPanel11;

		private Panel panel31;

		private Label label52;

		private Panel panel32;

		private Label label53;

		private Panel panel33;

		private Label l_systemVersion;

		private Button btn_downloadDataReset;

		private Button btn_Verification;

		private Button btn_OldDBrestore;

		private CheckBox cbIsRetailer;

		private CheckBox cbIsWholesaler;

		private Panel panel27;

		public frmSystemSetup()
			: base("系統設定")
		{
			InitializeComponent();
		}

		private void frmSystemSetup_Load(object sender, EventArgs e)
		{
			showShopInfoManage();
			showUserManage();
			showPrinterSetup();
			showCommonSetup();
		}

		private void showCommonSetup()
		{
			l_systemVersion.Text = Program.Version;
		}

		private void showPrinterSetup()
		{
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CommonManage", "", "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			tb_autoPath.Text = dataTable.Rows[0]["AutoBackupPath"].ToString();
			tb_manualPath.Text = dataTable.Rows[0]["ManualBackupPath"].ToString();
			tb_restoreFilePath.Text = dataTable.Rows[0]["AutoBackupPath"].ToString();
			if (!Directory.Exists(tb_manualPath.Text))
			{
				Directory.CreateDirectory(tb_manualPath.Text);
			}
			cb_receivePrinter.Items.Add("(系統預設印表機)");
			cb_listPrinter.Items.Add("(系統預設印表機)");
			foreach (object installedPrinter in PrinterSettings.InstalledPrinters)
			{
				cb_receivePrinter.Items.Add(installedPrinter.ToString());
				cb_listPrinter.Items.Add(installedPrinter.ToString());
			}
			DataTable obj = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_PrinterManage", "", "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			string text = obj.Rows[0]["ReceiveType"].ToString();
			string text2 = obj.Rows[0]["ReceivePrinterName"].ToString();
			obj.Rows[0]["BarcodeListType"].ToString();
			string text3 = obj.Rows[0]["BarcodeListPrinterName"].ToString();
			if (string.IsNullOrEmpty(text2))
			{
				cb_receivePrinter.SelectedIndex = 0;
			}
			else
			{
				cb_receivePrinter.SelectedItem = text2;
			}
			if (string.IsNullOrEmpty(text3))
			{
				cb_listPrinter.SelectedIndex = 0;
			}
			else
			{
				cb_listPrinter.SelectedItem = text3;
			}
			switch (text)
			{
			case "A4":
				receiveType = "A4";
				btn_receiveA4_Click(null, null);
				break;
			case "60":
				receiveType = "60";
				btn_receive60_Click(null, null);
				break;
			case "80":
				receiveType = "80";
				btn_receive80_Click(null, null);
				break;
			}
		}

		private void btn_printerSave_Click(object sender, EventArgs e)
		{
			string[,] strFieldArray = new string[3, 2]
			{
				{
					"ReceiveType",
					receiveType
				},
				{
					"ReceivePrinterName",
					cb_receivePrinter.SelectedItem.ToString()
				},
				{
					"BarcodeListPrinterName",
					cb_listPrinter.SelectedItem.ToString()
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_PrinterManage", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			AutoClosingMessageBox.Show("印表機設定已儲存");
		}

		private void showUserManage()
		{
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_User", "Type <> -1", "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			tlp_userManage.RowCount = dataTable.Rows.Count + 1;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				CheckBox checkBox = new CheckBox();
				checkBox.Font = new Font("微軟正黑體", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 136);
				checkBox.ForeColor = Color.Black;
				checkBox.Dock = DockStyle.Fill;
				checkBox.Name = dataTable.Rows[i]["Account"].ToString();
				if (dataTable.Rows[i]["Account"].ToString() == "001")
				{
					checkBox.Enabled = false;
				}
				Label label = new Label();
				label.Dock = DockStyle.Fill;
				label.Text = dataTable.Rows[i]["Account"].ToString();
				label.Anchor = AnchorStyles.Left;
				label.Click += new EventHandler(editUser);
				label.AutoSize = true;
				Label label2 = new Label();
				label2.Dock = DockStyle.Fill;
				label2.Anchor = AnchorStyles.Left;
				label2.Text = dataTable.Rows[i]["Name"].ToString();
				label2.AutoSize = true;
				Label label3 = new Label();
				label3.Dock = DockStyle.Fill;
				label3.Anchor = AnchorStyles.Left;
				label3.Text = ((int.Parse(dataTable.Rows[i]["Type"].ToString()) == 0) ? "管理者" : "使用者");
				label3.AutoSize = true;
				Label label4 = new Label();
				label4.Dock = DockStyle.Fill;
				label4.Anchor = AnchorStyles.Left;
				label4.Text = ((int.Parse(dataTable.Rows[i]["Status"].ToString()) == 0) ? "正常" : "停用");
				label4.AutoSize = true;
				Label label5 = new Label();
				label5.Dock = DockStyle.Fill;
				label5.Anchor = AnchorStyles.Left;
				label5.Text = dataTable.Rows[i]["LastLogin"].ToString();
				label5.AutoSize = true;
				tlp_userManage.Controls.Add(checkBox, 0, i + 1);
				tlp_userManage.Controls.Add(label, 1, i + 1);
				tlp_userManage.Controls.Add(label2, 2, i + 1);
				tlp_userManage.Controls.Add(label3, 3, i + 1);
				tlp_userManage.Controls.Add(label4, 4, i + 1);
				tlp_userManage.Controls.Add(label5, 5, i + 1);
				tlp_userManage.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
			}
		}

		public void editUser(object sender, EventArgs e)
		{
			new frmEditUser((sender as Label).Text).ShowDialog(this);
		}

		private void showShopInfoManage()
		{
			if (string.IsNullOrEmpty(Program.ShopType))
			{
				cbIsRetailer.Checked = false;
				cbIsWholesaler.Checked = false;
			}
			else if (Program.ShopType == "0")
			{
				cbIsRetailer.Checked = true;
				cbIsWholesaler.Checked = true;
			}
			else if (Program.ShopType == "1")
			{
				cbIsRetailer.Checked = true;
				cbIsWholesaler.Checked = false;
			}
			else if (Program.ShopType == "2")
			{
				cbIsRetailer.Checked = false;
				cbIsWholesaler.Checked = true;
			}
			l_licenseCode.Text = Program.LincenseCode;
			l_siteNo.Text = "機台碼：" + Program.SiteNo;
			string sql = "SELECT ShopName FROM hypos_RegisterLicense where isApproved = 'Y' order by CreateDate desc limit 1";
			l_shopName.Text = DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar).ToString();
			DataTable dataSource = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "ADDRCITY", "", "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			cb_city.DisplayMember = "city";
			cb_city.ValueMember = "cityno";
			cb_city.DataSource = dataSource;
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_ShopInfoManage", null, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				tb_ShopIdNo.Text = dataTable.Rows[0]["ShopIdNo"].ToString();
				tb_DutyName.Text = dataTable.Rows[0]["DutyName"].ToString();
				tb_DutyIdNo.Text = dataTable.Rows[0]["DutyIdNo"].ToString();
				tb_ParentUnitCode.Text = dataTable.Rows[0]["ParentUnitCode"].ToString();
				tb_nickName.Text = dataTable.Rows[0]["ShopNickName"].ToString();
				if (!string.IsNullOrEmpty(dataTable.Rows[0]["ShopCity"].ToString()))
				{
					cb_city.SelectedValue = dataTable.Rows[0]["ShopCity"].ToString();
				}
				if (!string.IsNullOrEmpty(dataTable.Rows[0]["ShopArea"].ToString()))
				{
					cb_area.SelectedValue = dataTable.Rows[0]["ShopArea"].ToString();
				}
				tb_addr.Text = dataTable.Rows[0]["ShopAddr"].ToString();
				tb_tel.Text = dataTable.Rows[0]["ShopTelphone"].ToString();
				tb_fax.Text = dataTable.Rows[0]["ShopFax"].ToString();
				tb_email.Text = dataTable.Rows[0]["ShopEmail"].ToString();
				tb_FertilizerAccount.Text = dataTable.Rows[0]["FertilizerAccount"].ToString();
				tb_FertilizerPassword.Text = dataTable.Rows[0]["FertilizerPassword"].ToString();
				tb_DelarNo.Text = dataTable.Rows[0]["DealerNo"].ToString();
			}
		}

		private void cb_city_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cb_city.SelectedValue != null)
			{
				cb_area.DataSource = null;
				DataTable dataSource = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "ADDRAREA", "cityno = {0}", "", null, new string[1]
				{
					cb_city.SelectedValue.ToString()
				}, CommandOperationType.ExecuteReaderReturnDataTable);
				cb_area.DisplayMember = "area";
				cb_area.ValueMember = "zipcode";
				cb_area.DataSource = dataSource;
			}
		}

		private void cb_area_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cb_area.SelectedValue != null)
			{
				tb_zipcode.Text = cb_area.SelectedValue.ToString();
			}
		}

		private void btn_shopSave_Click(object sender, EventArgs e)
		{
			if (CheckVerification().Equals("驗證成功"))
			{
				string[,] strFieldArray = new string[14, 2]
				{
					{
						"ShopIdNo",
						tb_ShopIdNo.Text
					},
					{
						"DutyName",
						tb_DutyName.Text
					},
					{
						"DutyIdNo",
						tb_DutyIdNo.Text
					},
					{
						"ParentUnitCode",
						tb_ParentUnitCode.Text
					},
					{
						"ShopNickName",
						tb_nickName.Text
					},
					{
						"ShopCity",
						cb_city.SelectedValue.ToString()
					},
					{
						"ShopArea",
						cb_area.SelectedValue.ToString()
					},
					{
						"ShopAddr",
						tb_addr.Text
					},
					{
						"ShopTelphone",
						tb_tel.Text
					},
					{
						"ShopFax",
						tb_fax.Text
					},
					{
						"ShopEmail",
						tb_email.Text
					},
					{
						"FertilizerAccount",
						tb_FertilizerAccount.Text
					},
					{
						"FertilizerPassword",
						tb_FertilizerPassword.Text
					},
					{
						"DealerNo",
						tb_DelarNo.Text
					}
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_ShopInfoManage", null, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_ShopInfoManage", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				}
				else
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_ShopInfoManage", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				}
			}
			else
			{
				string[,] strFieldArray2 = new string[13, 2]
				{
					{
						"ShopIdNo",
						tb_ShopIdNo.Text
					},
					{
						"DutyName",
						tb_DutyName.Text
					},
					{
						"DutyIdNo",
						tb_DutyIdNo.Text
					},
					{
						"ParentUnitCode",
						tb_ParentUnitCode.Text
					},
					{
						"ShopNickName",
						tb_nickName.Text
					},
					{
						"ShopCity",
						cb_city.SelectedValue.ToString()
					},
					{
						"ShopArea",
						cb_area.SelectedValue.ToString()
					},
					{
						"ShopAddr",
						tb_addr.Text
					},
					{
						"ShopTelphone",
						tb_tel.Text
					},
					{
						"ShopFax",
						tb_fax.Text
					},
					{
						"ShopEmail",
						tb_email.Text
					},
					{
						"FertilizerAccount",
						tb_FertilizerAccount.Text
					},
					{
						"DealerNo",
						tb_DelarNo.Text
					}
				};
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_ShopInfoManage", null, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_ShopInfoManage", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				}
				else
				{
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_ShopInfoManage", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				}
			}
			AutoClosingMessageBox.Show("店家資訊已儲存。");
		}

		private void tableLayoutPanel10_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			if (e.Row == 0)
			{
				e.Graphics.FillRectangle(Brushes.DarkGray, e.CellBounds);
			}
		}

		private void btn_createUser_Click(object sender, EventArgs e)
		{
			new frmNewUser().ShowDialog(this);
		}

		private void btn_printHotKey_Click(object sender, EventArgs e)
		{
			new frmPrintHotKey(cb_member.Checked, cb_cash.Checked, cb_shop.Checked, cb_returnM.Checked).ShowDialog();
		}

		private void btn_receive60_Click(object sender, EventArgs e)
		{
			receiveType = "60";
			btn_receive60.BackColor = Color.FromArgb(247, 106, 45);
			btn_receive60.ForeColor = Color.White;
			btn_receive80.BackColor = Color.White;
			btn_receive80.ForeColor = Color.FromArgb(247, 106, 45);
			btn_receiveA4.BackColor = Color.White;
			btn_receiveA4.ForeColor = Color.FromArgb(247, 106, 45);
		}

		private void btn_receive80_Click(object sender, EventArgs e)
		{
			receiveType = "80";
			btn_receive80.BackColor = Color.FromArgb(247, 106, 45);
			btn_receive80.ForeColor = Color.White;
			btn_receive60.BackColor = Color.White;
			btn_receive60.ForeColor = Color.FromArgb(247, 106, 45);
			btn_receiveA4.BackColor = Color.White;
			btn_receiveA4.ForeColor = Color.FromArgb(247, 106, 45);
		}

		private void btn_receiveA4_Click(object sender, EventArgs e)
		{
			receiveType = "A4";
			btn_receiveA4.BackColor = Color.FromArgb(247, 106, 45);
			btn_receiveA4.ForeColor = Color.White;
			btn_receive60.BackColor = Color.White;
			btn_receive60.ForeColor = Color.FromArgb(247, 106, 45);
			btn_receive80.BackColor = Color.White;
			btn_receive80.ForeColor = Color.FromArgb(247, 106, 45);
		}

		private void btn_batchSuspend_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			string text = "Account in (";
			int num = 0;
			for (int i = 2; i < tlp_userManage.RowCount; i++)
			{
				CheckBox checkBox = (CheckBox)tlp_userManage.GetControlFromPosition(0, i);
				if (checkBox.Checked)
				{
					text = text + "{" + num + "},";
					list.Add(checkBox.Name);
					(tlp_userManage.GetControlFromPosition(4, i) as Label).Text = "停用";
					num++;
				}
			}
			text = text.Substring(0, text.Length - 1) + ")";
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_User", text, "", new string[1, 2]
			{
				{
					"Status",
					"1"
				}
			}, list.ToArray(), CommandOperationType.ExecuteNonQuery);
		}

		private void btn_AutoBackupPath_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				tb_autoPath.Text = folderBrowserDialog.SelectedPath;
				string[,] strFieldArray = new string[1, 2]
				{
					{
						"AutoBackupPath",
						tb_autoPath.Text
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_CommonManage", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			}
		}

		private void btn_ManualBackupPath_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				tb_manualPath.Text = folderBrowserDialog.SelectedPath;
				string[,] strFieldArray = new string[1, 2]
				{
					{
						"ManualBackupPath",
						tb_manualPath.Text
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_CommonManage", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			}
		}

		private void btn_restoreDataPath_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = tb_restoreFilePath.Text;
			openFileDialog.Filter = "db Files|*.db3";
			openFileDialog.Title = "請選擇還原檔案";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					tb_restoreFilePath.Text = openFileDialog.FileName.ToString();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		private void btn_ManualBackup_Click(object sender, EventArgs e)
		{
			string str = "db_m_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".db3";
			File.Copy(Program.DataPath + "\\db.db3", tb_manualPath.Text + "\\" + str);
			MessageBox.Show("備份成功");
		}

		private void btn_executeRestore_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("將使用「還原檔案完整路徑／檔名」進行資料庫還原。\n請注意，資料一經還原即無法再復原目前狀態。確定進行還原？", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return;
			}
			try
			{
				string connectionString = "Data source=" + tb_restoreFilePath.Text + ";Password=1031;Version=3;Page Size=4096;Cache Size=2000;Synchronous=Full;";
				string sql = "SELECT LicenseCode,ShopName,RegisterCode FROM hypos_RegisterLicense where isApproved = 'Y' order by CreateDate desc limit 1";
				DataTable obj = (DataTable)DataBaseUtilities.DBOperation(connectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
				string text = obj.Rows[0]["LicenseCode"].ToString();
				string text2 = obj.Rows[0]["ShopName"].ToString();
				string text3 = obj.Rows[0]["RegisterCode"].ToString();
				string sql2 = "SELECT SiteNo FROM hypos_SysParam";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(connectionString, sql2, null, CommandOperationType.ExecuteReaderReturnDataTable);
				string text4 = "";
				if (!text.Equals(Program.LincenseCode))
				{
					text4 += "來源店代碼不同，\n";
				}
				if (!text2.Equals(l_shopName.Text))
				{
					text4 += "來源店名不同，\n";
				}
				if (int.Parse(Program.SiteNo) < int.Parse(dataTable.Rows[0]["SiteNo"].ToString()))
				{
					text4 += "來源機台碼必須小於等於當前機台碼，\n";
				}
				if (!string.IsNullOrEmpty(text4))
				{
					text4 += "無法進行還原。";
					MessageBox.Show(text4);
					return;
				}
				string sql3 = "SELECT HardDiskSerialNo,RegisterCode,CreateDate FROM hypos_RegisterLicense where isApproved = 'Y' order by CreateDate desc limit 1";
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql3, null, CommandOperationType.ExecuteReaderReturnDataTable);
				string sql4 = "UPDATE hypos_SysParam set SiteNo = {0}";
				DataBaseUtilities.DBOperation(connectionString, sql4, new string[1]
				{
					Program.SiteNo
				}, CommandOperationType.ExecuteNonQuery);
				string sql5 = "UPDATE hypos_RegisterLicense set LicenseCode = {1}, ShopName = {2},HardDiskSerialNo = {3}, RegisterCode = {4}, CreateDate = {5} where RegisterCode = {0}";
				DataBaseUtilities.DBOperation(connectionString, sql5, new string[6]
				{
					text3,
					Program.LincenseCode,
					l_shopName.Text,
					dataTable2.Rows[0]["HardDiskSerialNo"].ToString(),
					dataTable2.Rows[0]["RegisterCode"].ToString(),
					dataTable2.Rows[0]["CreateDate"].ToString()
				}, CommandOperationType.ExecuteNonQuery);
				File.Copy(tb_restoreFilePath.Text, Program.DataPath + "\\db.db3", true);
				MessageBox.Show("還原成功，系統即將重啟");
				Program.Upgraded = true;
				frmExtendScreen.OnlyInstance.Dispose();
				Application.Exit();
			}
			catch (Exception ex)
			{
				MessageBox.Show("還原失敗:\n" + ex.Message);
			}
		}

		private void btn_downloadDataReset_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("將進行下載資料重置。\n請注意，資料一經重置即無法再復原目前狀態。確定進行?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				string sql = "UPDATE hypos_SysParam set DownloadLastUpdateDate = ''";
				DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteNonQuery);
				MessageBox.Show("重置成功，系統即將重啟");
				Program.Upgraded = true;
				frmExtendScreen.OnlyInstance.Dispose();
				Application.Exit();
			}
		}

		private void btn_Verification_Click(object sender, EventArgs e)
		{
			if (Program.IsFertilizer)
			{
				CheckVerification();
			}
		}

		private string CheckVerification()
		{
			string text = "";
			if (NetworkInterface.GetIsNetworkAvailable())
			{
				POSService pOSService = new POSService();
				pOSService.Url = Program.VerificationURL;
				string xml = pOSService.sendRetailData(Program.LincenseCode, tb_FertilizerAccount.Text.Trim(), tb_FertilizerPassword.Text.Trim(), tb_DelarNo.Text.Trim());
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.LoadXml(xml);
					XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//RetailData");
					if (xmlNodeList.Count > 0)
					{
						for (int i = 0; i < xmlNodeList.Count; i++)
						{
							string innerText = xmlNodeList[i].SelectSingleNode("status").InnerText;
							text = ((!"Y".Equals(innerText)) ? "購肥帳號密碼驗證錯誤" : "驗證成功");
						}
					}
					else
					{
						text = "無WebService驗證回傳資料";
					}
				}
				catch (Exception ex)
				{
					text = "發生錯誤 : " + ex.Message;
				}
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_ShopInfoManage", null, CommandOperationType.ExecuteReaderReturnDataTable);
				if (text.Equals("驗證成功"))
				{
					Program.IsSaleOfFertilizer = true;
					string[,] strFieldArray = new string[4, 2]
					{
						{
							"ShopIdNo",
							tb_ShopIdNo.Text
						},
						{
							"FertilizerAccount",
							tb_FertilizerAccount.Text
						},
						{
							"FertilizerPassword",
							tb_FertilizerPassword.Text
						},
						{
							"DealerNo",
							tb_DelarNo.Text
						}
					};
					if (dataTable.Rows.Count > 0)
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_ShopInfoManage", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
					}
					else
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_ShopInfoManage", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
					}
					AutoClosingMessageBox.Show("驗證成功。");
				}
				else if (text.Equals("購肥帳號密碼驗證錯誤"))
				{
					Program.IsSaleOfFertilizer = false;
					AutoClosingMessageBox.Show("帳號密碼有誤，請重新確認您的帳號密碼。");
					if (dataTable.Rows.Count > 0)
					{
						string[] strParameterArray = new string[1]
						{
							tb_ShopIdNo.Text
						};
						DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_ShopInfoManage SET FertilizerPassword = '' WHERE ShopIdNo = {0} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
					}
				}
			}
			else
			{
				AutoClosingMessageBox.Show("偵測不到網路連線，請確認網路正常後再進行驗證。");
			}
			return text;
		}

		private void btnbtn_OldDBrestore_Click(object sender, EventArgs e)
		{
		}

		public static string getNewGDSNO()
		{
			string sql = "SELECT GDSNO FROM hypos_GOODSLST where GDSNO like 'G" + Program.SiteNo.ToString() + "%' order by GDSNO desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			string text2 = DateTime.Now.Year.ToString().Substring(2, 2);
			if ("-1".Equals(text))
			{
				return string.Format("G{0}{1}00000001", Program.SiteNo, text2);
			}
			string value = text.Substring(3, 2);
			if (!text2.Equals(value))
			{
				return string.Format("G{0}{1}00000001", Program.SiteNo, text2);
			}
			string arg = string.Format("{0:00000000}", int.Parse(text.Substring(5, 8)) + 1);
			return string.Format("G{0}{1}{2}", Program.SiteNo, text2, arg);
		}

		public static string getNewVipNo()
		{
			string sql = "SELECT VipNo FROM hypos_CUST_RTL where VipNo like 'M" + Program.SiteNo.ToString() + "%' order by VipNo desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			string text2 = DateTime.Now.Year.ToString().Substring(2, 2);
			if ("-1".Equals(text))
			{
				return string.Format("M{0}{1}00001", Program.SiteNo.PadLeft(2, '0'), text2);
			}
			string value = text.Substring(3, 2);
			if (!text2.Equals(value))
			{
				return string.Format("M{0}{1}00001", Program.SiteNo, text2);
			}
			string arg = string.Format("{0:00000}", int.Parse(text.Substring(5, 5)) + 1);
			return string.Format("M{0}{1}{2}", Program.SiteNo, text2, arg);
		}

		public static string getSupplierNo()
		{
			string sql = "SELECT SupplierNo FROM hypos_Supplier where SupplierNo like 'S" + Program.SiteNo.ToString() + "%' order by SupplierNo desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			string text2 = DateTime.Now.Year.ToString().Substring(2, 2);
			if ("-1".Equals(text))
			{
				return string.Format("S{0}{1}00001", Program.SiteNo, text2);
			}
			string value = text.Substring(3, 2);
			if (!text2.Equals(value))
			{
				return string.Format("S{0}{1}00001", Program.SiteNo, text2);
			}
			string arg = string.Format("{0:00000}", int.Parse(text.Substring(5, 5)) + 1);
			return string.Format("S{0}{1}{2}", Program.SiteNo, text2, arg);
		}

		public static string getHseqNo()
		{
			string text = "";
			DateTime now = DateTime.Now;
			string text2 = now.ToString("yyyyMMdd");
			string str = now.ToString("yyyy-MM-dd");
			string[] strWhereParameterArray = new string[1]
			{
				text2
			};
			string strWhereClause = "editDate like '%" + str + "%'";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_main_sell", strWhereClause, "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				string text3 = (dataTable.Rows.Count + 1).ToString();
				if (text3.Length == 1)
				{
					return Program.LincenseCode + Program.SiteNo + text2 + "000" + text3;
				}
				if (text3.Length == 2)
				{
					return Program.LincenseCode + Program.SiteNo + text2 + "00" + text3;
				}
				if (text3.Length == 3)
				{
					return Program.LincenseCode + Program.SiteNo + text2 + "0" + text3;
				}
				return Program.LincenseCode + Program.SiteNo + text2 + text3;
			}
			return Program.LincenseCode + Program.SiteNo + text2 + "0001";
		}

		public static string getNewPurchaseNo()
		{
			string sql = "SELECT PurchaseNo FROM hypos_PurchaseGoods_Master where PurchaseNo like '" + Program.SiteNo.ToString() + "%' order by PurchaseNo desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			DateTime now = DateTime.Now;
			string text2 = now.Year.ToString().Substring(2, 2) + string.Format("{0:00}", now.Month);
			if ("-1".Equals(text))
			{
				return string.Format("{0}{1}0001", Program.SiteNo, text2);
			}
			string value = text.Substring(2, 4);
			if (!text2.Equals(value))
			{
				return string.Format("{0}{1}0001", Program.SiteNo, text2);
			}
			string arg = string.Format("{0:0000}", int.Parse(text.Substring(6, 4)) + 1);
			return string.Format("{0}{1}{2}", Program.SiteNo, text2, arg);
		}

		public static void buildMainSell(string SNO)
		{
			string hseqNo = getHseqNo();
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string[] strParameterArray = new string[1]
			{
				SNO
			};
			string sql = "ATTACH DATABASE 'C:\\Hypos\\Old_db.db3' AS [old] KEY '1031'; SELECT * FROM ECRHDHS where {0} = (SHOPNO||'_'||HWSNO||'_'||HDATE||'_'||HSEQNO)";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, strParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			string[] strParameterArray2 = new string[1]
			{
				SNO
			};
			string sql2 = "ATTACH DATABASE 'C:\\Hypos\\Old_db.db3' AS [old] KEY '1031'; SELECT * FROM ECRPYHS where {0} = (SHOPNO||'_'||HWSNO||'_'||HDATE||'_'||HSEQNO)";
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, strParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
			string[] strParameterArray3 = new string[1]
			{
				SNO
			};
			string sql3 = "ATTACH DATABASE 'C:\\Hypos\\Old_db.db3' AS [old] KEY '1031'; SELECT * FROM ECRTRHS where {0} = (SHOPNO||'_'||HWSNO||'_'||HDATE||'_'||HSEQNO)";
			DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql3, strParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
			string text2 = "";
			string[] strParameterArray4 = new string[1]
			{
				dataTable.Rows[0]["HCU_NO"].ToString()
			};
			string sql4 = "ATTACH DATABASE 'C:\\Hypos\\Old_db.db3' AS [old] KEY '1031'; SELECT VipNo FROM hypos_CUST_RTL where oldVIPNO = {0}";
			DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql4, strParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
			text2 = ((dataTable4.Rows.Count <= 0) ? "" : dataTable4.Rows[0]["VipNo"].ToString());
			string text3 = "";
			string[] strParameterArray5 = new string[1]
			{
				SNO
			};
			string sql5 = "ATTACH DATABASE 'C:\\Hypos\\Old_db.db3' AS [old] KEY '1031'; SELECT sum(HQTY) as itemstotal FROM ECRTRHS where(SHOPNO || '_' || HWSNO || '_' || HDATE || '_' || HSEQNO) = {0}";
			DataTable dataTable5 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql5, strParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable);
			text3 = ((dataTable5.Rows.Count <= 0) ? "" : Math.Abs(int.Parse(dataTable5.Rows[0]["itemstotal"].ToString())).ToString());
			string text4 = "";
			string text5 = dataTable.Rows[0]["HHD_VLD"].ToString();
			if (text5 == null || text5.Length != 0)
			{
				if (text5 == "D")
				{
					text4 = "1";
				}
			}
			else
			{
				text4 = "0";
			}
			string str = Convert.ToDateTime(dataTable.Rows[0]["HDATE"].ToString()).ToString("yyyy-MM-dd");
			string text6 = "0";
			string text7 = "0";
			if (dataTable2.Rows.Count > 0)
			{
				if ("01".Equals(dataTable2.Rows[0]["HPYNO"].ToString()))
				{
					text6 = dataTable2.Rows[0]["HPYAMT"].ToString();
				}
				else if ("03".Equals(dataTable2.Rows[0]["HPYNO"].ToString()))
				{
					text7 = dataTable2.Rows[0]["HPYAMT"].ToString();
				}
			}
			string[,] strFieldArray = new string[15, 2]
			{
				{
					"sellNo",
					hseqNo
				},
				{
					"oldECRHDHSNo",
					SNO
				},
				{
					"sellTime",
					str + " " + dataTable.Rows[0]["HTIME"].ToString()
				},
				{
					"memberId",
					text2
				},
				{
					"sum",
					dataTable.Rows[0]["HASTOTAL"].ToString()
				},
				{
					"items",
					dataTable.Rows[0]["HTR_NUM"].ToString()
				},
				{
					"itemstotal",
					text3
				},
				{
					"status",
					text4
				},
				{
					"sumDiscount",
					"0"
				},
				{
					"sumRebate",
					"0"
				},
				{
					"Refund",
					"0"
				},
				{
					"changcount",
					"1"
				},
				{
					"editDate",
					text
				},
				{
					"Cash",
					text6
				},
				{
					"Credit",
					text7
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_main_sell", "", "", strFieldArray, null, CommandOperationType.ExecuteNonQuery);
			if (dataTable3.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable3.Rows.Count; i++)
				{
					string text8 = "";
					string[] strParameterArray6 = new string[1]
					{
						dataTable3.Rows[i]["HGDSNO"].ToString()
					};
					string sql6 = "ATTACH DATABASE 'C:\\Hypos\\Old_db.db3' AS [old] KEY '1031'; SELECT GDSNO FROM hypos_GOODSLST where oldGDSNO = {0}";
					DataTable dataTable6 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql6, strParameterArray6, CommandOperationType.ExecuteReaderReturnDataTable);
					text8 = ((dataTable6.Rows.Count <= 0) ? "" : dataTable6.Rows[0]["GDSNO"].ToString());
					string[,] strFieldArray2 = new string[10, 2]
					{
						{
							"sellNo",
							hseqNo
						},
						{
							"barcode",
							text8
						},
						{
							"fixedPrice",
							dataTable3.Rows[i]["HOPRICE"].ToString()
						},
						{
							"sellingPrice",
							dataTable3.Rows[i]["HSPRICE"].ToString()
						},
						{
							"num",
							dataTable3.Rows[i]["HQTY"].ToString()
						},
						{
							"subtotal",
							(int.Parse(dataTable3.Rows[i]["HQTY"].ToString()) * int.Parse(dataTable3.Rows[i]["HSPRICE"].ToString())).ToString()
						},
						{
							"discount",
							"0"
						},
						{
							"total",
							dataTable3.Rows[i]["HSLTOT"].ToString()
						},
						{
							"PRNO",
							dataTable3.Rows[i]["CropNo"].ToString()
						},
						{
							"BLNO",
							dataTable3.Rows[i]["PestNo"].ToString()
						}
					};
					DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_detail_sell", "", "", strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
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
			tabControl1 = new System.Windows.Forms.TabControl();
			tabPage1 = new System.Windows.Forms.TabPage();
			label4 = new System.Windows.Forms.Label();
			tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			tb_FertilizerPassword = new System.Windows.Forms.TextBox();
			tb_FertilizerAccount = new System.Windows.Forms.TextBox();
			panel13 = new System.Windows.Forms.Panel();
			label16 = new System.Windows.Forms.Label();
			panel8 = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			panel12 = new System.Windows.Forms.Panel();
			label11 = new System.Windows.Forms.Label();
			panel17 = new System.Windows.Forms.Panel();
			btn_Verification = new System.Windows.Forms.Button();
			tb_DelarNo = new System.Windows.Forms.TextBox();
			panel18 = new System.Windows.Forms.Panel();
			btn_shopSave = new System.Windows.Forms.Button();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			tb_nickName = new System.Windows.Forms.TextBox();
			tb_ParentUnitCode = new System.Windows.Forms.TextBox();
			panel2 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			tb_ShopIdNo = new System.Windows.Forms.TextBox();
			l_shopName = new System.Windows.Forms.Label();
			panel19 = new System.Windows.Forms.Panel();
			tb_email = new System.Windows.Forms.TextBox();
			tb_tel = new System.Windows.Forms.TextBox();
			panel11 = new System.Windows.Forms.Panel();
			label15 = new System.Windows.Forms.Label();
			panel9 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			panel7 = new System.Windows.Forms.Panel();
			label14 = new System.Windows.Forms.Label();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label10 = new System.Windows.Forms.Label();
			panel14 = new System.Windows.Forms.Panel();
			tb_zipcode = new System.Windows.Forms.TextBox();
			cb_area = new System.Windows.Forms.ComboBox();
			cb_city = new System.Windows.Forms.ComboBox();
			tb_addr = new System.Windows.Forms.TextBox();
			panel15 = new System.Windows.Forms.Panel();
			label23 = new System.Windows.Forms.Label();
			panel10 = new System.Windows.Forms.Panel();
			label13 = new System.Windows.Forms.Label();
			panel16 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			tb_DutyName = new System.Windows.Forms.TextBox();
			tb_DutyIdNo = new System.Windows.Forms.TextBox();
			l_licenseCode = new System.Windows.Forms.Label();
			tb_fax = new System.Windows.Forms.TextBox();
			panel27 = new System.Windows.Forms.Panel();
			cbIsWholesaler = new System.Windows.Forms.CheckBox();
			cbIsRetailer = new System.Windows.Forms.CheckBox();
			l_siteNo = new System.Windows.Forms.Label();
			tabPage2 = new System.Windows.Forms.TabPage();
			btn_batchSuspend = new System.Windows.Forms.Button();
			tlp_userManage = new System.Windows.Forms.TableLayoutPanel();
			label21 = new System.Windows.Forms.Label();
			label20 = new System.Windows.Forms.Label();
			label24 = new System.Windows.Forms.Label();
			label22 = new System.Windows.Forms.Label();
			label25 = new System.Windows.Forms.Label();
			tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			btn_createUser = new System.Windows.Forms.Button();
			tabPage3 = new System.Windows.Forms.TabPage();
			tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
			panel28 = new System.Windows.Forms.Panel();
			label50 = new System.Windows.Forms.Label();
			panel29 = new System.Windows.Forms.Panel();
			label51 = new System.Windows.Forms.Label();
			panel30 = new System.Windows.Forms.Panel();
			btn_executeRestore = new System.Windows.Forms.Button();
			btn_restoreDataPath = new System.Windows.Forms.Button();
			tb_restoreFilePath = new System.Windows.Forms.TextBox();
			tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
			panel54 = new System.Windows.Forms.Panel();
			label44 = new System.Windows.Forms.Label();
			panel55 = new System.Windows.Forms.Panel();
			label45 = new System.Windows.Forms.Label();
			panel56 = new System.Windows.Forms.Panel();
			label46 = new System.Windows.Forms.Label();
			panel57 = new System.Windows.Forms.Panel();
			btn_AutoBackupPath = new System.Windows.Forms.Button();
			tb_autoPath = new System.Windows.Forms.TextBox();
			panel58 = new System.Windows.Forms.Panel();
			btn_ManualBackup = new System.Windows.Forms.Button();
			btn_ManualBackupPath = new System.Windows.Forms.Button();
			tb_manualPath = new System.Windows.Forms.TextBox();
			panel26 = new System.Windows.Forms.Panel();
			btn_printerSave = new System.Windows.Forms.Button();
			tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			cb_listPrinter = new System.Windows.Forms.ComboBox();
			panel34 = new System.Windows.Forms.Panel();
			label33 = new System.Windows.Forms.Label();
			panel35 = new System.Windows.Forms.Panel();
			label34 = new System.Windows.Forms.Label();
			panel36 = new System.Windows.Forms.Panel();
			label35 = new System.Windows.Forms.Label();
			panel41 = new System.Windows.Forms.Panel();
			button1 = new System.Windows.Forms.Button();
			tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			panel40 = new System.Windows.Forms.Panel();
			label38 = new System.Windows.Forms.Label();
			panel45 = new System.Windows.Forms.Panel();
			label42 = new System.Windows.Forms.Label();
			panel37 = new System.Windows.Forms.Panel();
			label32 = new System.Windows.Forms.Label();
			panel38 = new System.Windows.Forms.Panel();
			btn_receiveA4 = new System.Windows.Forms.Button();
			btn_receive80 = new System.Windows.Forms.Button();
			btn_receive60 = new System.Windows.Forms.Button();
			cb_receivePrinter = new System.Windows.Forms.ComboBox();
			tabPage4 = new System.Windows.Forms.TabPage();
			tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
			panel31 = new System.Windows.Forms.Panel();
			label52 = new System.Windows.Forms.Label();
			panel32 = new System.Windows.Forms.Panel();
			label53 = new System.Windows.Forms.Label();
			panel33 = new System.Windows.Forms.Panel();
			btn_OldDBrestore = new System.Windows.Forms.Button();
			btn_downloadDataReset = new System.Windows.Forms.Button();
			l_systemVersion = new System.Windows.Forms.Label();
			tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
			panel49 = new System.Windows.Forms.Panel();
			label40 = new System.Windows.Forms.Label();
			panel50 = new System.Windows.Forms.Panel();
			label41 = new System.Windows.Forms.Label();
			panel51 = new System.Windows.Forms.Panel();
			label43 = new System.Windows.Forms.Label();
			panel52 = new System.Windows.Forms.Panel();
			cb_shop = new System.Windows.Forms.CheckBox();
			label47 = new System.Windows.Forms.Label();
			panel53 = new System.Windows.Forms.Panel();
			cb_returnM = new System.Windows.Forms.CheckBox();
			label48 = new System.Windows.Forms.Label();
			panel22 = new System.Windows.Forms.Panel();
			btn_printHotKey = new System.Windows.Forms.Button();
			tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			label27 = new System.Windows.Forms.Label();
			panel43 = new System.Windows.Forms.Panel();
			label36 = new System.Windows.Forms.Label();
			panel44 = new System.Windows.Forms.Panel();
			label37 = new System.Windows.Forms.Label();
			panel46 = new System.Windows.Forms.Panel();
			label39 = new System.Windows.Forms.Label();
			panel47 = new System.Windows.Forms.Panel();
			label26 = new System.Windows.Forms.Label();
			cb_member = new System.Windows.Forms.CheckBox();
			panel20 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			panel21 = new System.Windows.Forms.Panel();
			label17 = new System.Windows.Forms.Label();
			panel24 = new System.Windows.Forms.Panel();
			cb_cash = new System.Windows.Forms.CheckBox();
			label31 = new System.Windows.Forms.Label();
			panel23 = new System.Windows.Forms.Panel();
			label18 = new System.Windows.Forms.Label();
			panel25 = new System.Windows.Forms.Panel();
			label19 = new System.Windows.Forms.Label();
			label28 = new System.Windows.Forms.Label();
			label29 = new System.Windows.Forms.Label();
			label30 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			tabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			panel13.SuspendLayout();
			panel8.SuspendLayout();
			panel12.SuspendLayout();
			panel17.SuspendLayout();
			panel18.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			panel2.SuspendLayout();
			panel19.SuspendLayout();
			panel11.SuspendLayout();
			panel9.SuspendLayout();
			panel7.SuspendLayout();
			panel6.SuspendLayout();
			panel3.SuspendLayout();
			panel1.SuspendLayout();
			panel4.SuspendLayout();
			panel5.SuspendLayout();
			panel14.SuspendLayout();
			panel15.SuspendLayout();
			panel10.SuspendLayout();
			panel16.SuspendLayout();
			panel27.SuspendLayout();
			tabPage2.SuspendLayout();
			tlp_userManage.SuspendLayout();
			tableLayoutPanel4.SuspendLayout();
			tabPage3.SuspendLayout();
			tableLayoutPanel10.SuspendLayout();
			panel28.SuspendLayout();
			panel29.SuspendLayout();
			panel30.SuspendLayout();
			tableLayoutPanel9.SuspendLayout();
			panel54.SuspendLayout();
			panel55.SuspendLayout();
			panel56.SuspendLayout();
			panel57.SuspendLayout();
			panel58.SuspendLayout();
			panel26.SuspendLayout();
			tableLayoutPanel6.SuspendLayout();
			panel34.SuspendLayout();
			panel35.SuspendLayout();
			panel36.SuspendLayout();
			panel41.SuspendLayout();
			tableLayoutPanel5.SuspendLayout();
			panel40.SuspendLayout();
			panel45.SuspendLayout();
			panel37.SuspendLayout();
			panel38.SuspendLayout();
			tabPage4.SuspendLayout();
			tableLayoutPanel11.SuspendLayout();
			panel31.SuspendLayout();
			panel32.SuspendLayout();
			panel33.SuspendLayout();
			tableLayoutPanel8.SuspendLayout();
			panel49.SuspendLayout();
			panel50.SuspendLayout();
			panel51.SuspendLayout();
			panel52.SuspendLayout();
			panel53.SuspendLayout();
			panel22.SuspendLayout();
			tableLayoutPanel7.SuspendLayout();
			panel43.SuspendLayout();
			panel44.SuspendLayout();
			panel46.SuspendLayout();
			panel47.SuspendLayout();
			panel20.SuspendLayout();
			panel21.SuspendLayout();
			panel24.SuspendLayout();
			panel23.SuspendLayout();
			panel25.SuspendLayout();
			SuspendLayout();
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 41);
			tabControl1.Controls.Add(tabPage1);
			tabControl1.Controls.Add(tabPage2);
			tabControl1.Controls.Add(tabPage3);
			tabControl1.Controls.Add(tabPage4);
			tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			tabControl1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tabControl1.Location = new System.Drawing.Point(0, 35);
			tabControl1.Margin = new System.Windows.Forms.Padding(0);
			tabControl1.Multiline = true;
			tabControl1.Name = "tabControl1";
			tabControl1.Padding = new System.Drawing.Point(15, 10);
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new System.Drawing.Size(981, 626);
			tabControl1.TabIndex = 33;
			tabPage1.Controls.Add(label4);
			tabPage1.Controls.Add(tableLayoutPanel3);
			tabPage1.Controls.Add(tableLayoutPanel2);
			tabPage1.Controls.Add(tableLayoutPanel1);
			tabPage1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tabPage1.Location = new System.Drawing.Point(4, 47);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new System.Windows.Forms.Padding(3);
			tabPage1.Size = new System.Drawing.Size(973, 575);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "店家資訊管理";
			tabPage1.UseVisualStyleBackColor = true;
			label4.AutoSize = true;
			label4.Image = POS_Client.Properties.Resources.oblique;
			label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label4.Location = new System.Drawing.Point(3, 394);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(153, 20);
			label4.TabIndex = 4;
			label4.Text = "    購肥系統介接資訊";
			tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel3.ColumnCount = 4;
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel3.Controls.Add(tb_FertilizerPassword, 3, 0);
			tableLayoutPanel3.Controls.Add(tb_FertilizerAccount, 1, 0);
			tableLayoutPanel3.Controls.Add(panel13, 0, 1);
			tableLayoutPanel3.Controls.Add(panel8, 0, 0);
			tableLayoutPanel3.Controls.Add(panel12, 2, 0);
			tableLayoutPanel3.Controls.Add(panel17, 1, 1);
			tableLayoutPanel3.Controls.Add(panel18, 0, 2);
			tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			tableLayoutPanel3.Location = new System.Drawing.Point(3, 422);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 3;
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel3.Size = new System.Drawing.Size(967, 150);
			tableLayoutPanel3.TabIndex = 3;
			tb_FertilizerPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_FertilizerPassword.Location = new System.Drawing.Point(654, 10);
			tb_FertilizerPassword.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_FertilizerPassword.Name = "tb_FertilizerPassword";
			tb_FertilizerPassword.Size = new System.Drawing.Size(300, 29);
			tb_FertilizerPassword.TabIndex = 35;
			tb_FertilizerAccount.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_FertilizerAccount.Location = new System.Drawing.Point(172, 10);
			tb_FertilizerAccount.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_FertilizerAccount.Name = "tb_FertilizerAccount";
			tb_FertilizerAccount.Size = new System.Drawing.Size(300, 29);
			tb_FertilizerAccount.TabIndex = 34;
			panel13.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel13.Controls.Add(label16);
			panel13.Dock = System.Windows.Forms.DockStyle.Fill;
			panel13.Location = new System.Drawing.Point(1, 50);
			panel13.Margin = new System.Windows.Forms.Padding(0);
			panel13.Name = "panel13";
			panel13.Size = new System.Drawing.Size(160, 48);
			panel13.TabIndex = 23;
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label16.ForeColor = System.Drawing.Color.White;
			label16.Location = new System.Drawing.Point(84, 15);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(74, 21);
			label16.TabIndex = 0;
			label16.Text = "經銷編號";
			panel8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel8.Controls.Add(label9);
			panel8.Dock = System.Windows.Forms.DockStyle.Fill;
			panel8.Location = new System.Drawing.Point(1, 1);
			panel8.Margin = new System.Windows.Forms.Padding(0);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(160, 48);
			panel8.TabIndex = 21;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label9.ForeColor = System.Drawing.Color.White;
			label9.Location = new System.Drawing.Point(84, 15);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(74, 21);
			label9.TabIndex = 0;
			label9.Text = "登入帳號";
			panel12.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel12.Controls.Add(label11);
			panel12.Dock = System.Windows.Forms.DockStyle.Fill;
			panel12.Location = new System.Drawing.Point(483, 1);
			panel12.Margin = new System.Windows.Forms.Padding(0);
			panel12.Name = "panel12";
			panel12.Size = new System.Drawing.Size(160, 48);
			panel12.TabIndex = 22;
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label11.ForeColor = System.Drawing.Color.White;
			label11.Location = new System.Drawing.Point(84, 15);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(74, 21);
			label11.TabIndex = 0;
			label11.Text = "登入密碼";
			panel17.BackColor = System.Drawing.Color.White;
			tableLayoutPanel3.SetColumnSpan(panel17, 3);
			panel17.Controls.Add(btn_Verification);
			panel17.Controls.Add(tb_DelarNo);
			panel17.Dock = System.Windows.Forms.DockStyle.Fill;
			panel17.Location = new System.Drawing.Point(162, 50);
			panel17.Margin = new System.Windows.Forms.Padding(0);
			panel17.Name = "panel17";
			panel17.Size = new System.Drawing.Size(804, 48);
			panel17.TabIndex = 36;
			btn_Verification.BackColor = System.Drawing.Color.FromArgb(31, 133, 173);
			btn_Verification.FlatAppearance.BorderSize = 0;
			btn_Verification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_Verification.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_Verification.ForeColor = System.Drawing.Color.White;
			btn_Verification.Location = new System.Drawing.Point(328, 8);
			btn_Verification.Name = "btn_Verification";
			btn_Verification.Size = new System.Drawing.Size(149, 33);
			btn_Verification.TabIndex = 59;
			btn_Verification.Text = "立即驗證帳號";
			btn_Verification.UseVisualStyleBackColor = false;
			btn_Verification.Click += new System.EventHandler(btn_Verification_Click);
			tb_DelarNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_DelarNo.Location = new System.Drawing.Point(10, 8);
			tb_DelarNo.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_DelarNo.Name = "tb_DelarNo";
			tb_DelarNo.Size = new System.Drawing.Size(300, 29);
			tb_DelarNo.TabIndex = 36;
			panel18.BackColor = System.Drawing.Color.White;
			tableLayoutPanel3.SetColumnSpan(panel18, 4);
			panel18.Controls.Add(btn_shopSave);
			panel18.Dock = System.Windows.Forms.DockStyle.Fill;
			panel18.Location = new System.Drawing.Point(1, 99);
			panel18.Margin = new System.Windows.Forms.Padding(0);
			panel18.Name = "panel18";
			panel18.Size = new System.Drawing.Size(965, 50);
			panel18.TabIndex = 37;
			btn_shopSave.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_shopSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_shopSave.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_shopSave.ForeColor = System.Drawing.Color.White;
			btn_shopSave.Location = new System.Drawing.Point(432, 7);
			btn_shopSave.Name = "btn_shopSave";
			btn_shopSave.Size = new System.Drawing.Size(103, 32);
			btn_shopSave.TabIndex = 6;
			btn_shopSave.Text = "儲存變更";
			btn_shopSave.UseVisualStyleBackColor = false;
			btn_shopSave.Click += new System.EventHandler(btn_shopSave_Click);
			tableLayoutPanel2.ColumnCount = 1;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel2.Location = new System.Drawing.Point(3, 536);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 1;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel2.Size = new System.Drawing.Size(967, 50);
			tableLayoutPanel2.TabIndex = 2;
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 4;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			tableLayoutPanel1.Controls.Add(tb_nickName, 3, 3);
			tableLayoutPanel1.Controls.Add(tb_ParentUnitCode, 1, 3);
			tableLayoutPanel1.Controls.Add(panel2, 2, 2);
			tableLayoutPanel1.Controls.Add(tb_ShopIdNo, 3, 1);
			tableLayoutPanel1.Controls.Add(l_shopName, 3, 0);
			tableLayoutPanel1.Controls.Add(panel19, 1, 6);
			tableLayoutPanel1.Controls.Add(tb_tel, 1, 5);
			tableLayoutPanel1.Controls.Add(panel11, 2, 5);
			tableLayoutPanel1.Controls.Add(panel9, 2, 0);
			tableLayoutPanel1.Controls.Add(panel7, 0, 6);
			tableLayoutPanel1.Controls.Add(panel6, 0, 5);
			tableLayoutPanel1.Controls.Add(panel3, 0, 2);
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(panel4, 0, 3);
			tableLayoutPanel1.Controls.Add(panel5, 0, 4);
			tableLayoutPanel1.Controls.Add(panel14, 1, 4);
			tableLayoutPanel1.Controls.Add(panel15, 0, 1);
			tableLayoutPanel1.Controls.Add(panel10, 2, 1);
			tableLayoutPanel1.Controls.Add(panel16, 2, 3);
			tableLayoutPanel1.Controls.Add(tb_DutyName, 1, 2);
			tableLayoutPanel1.Controls.Add(tb_DutyIdNo, 3, 2);
			tableLayoutPanel1.Controls.Add(l_licenseCode, 1, 0);
			tableLayoutPanel1.Controls.Add(tb_fax, 3, 5);
			tableLayoutPanel1.Controls.Add(panel27, 1, 1);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 7;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.0737f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel1.Size = new System.Drawing.Size(967, 381);
			tableLayoutPanel1.TabIndex = 1;
			tb_nickName.Location = new System.Drawing.Point(654, 155);
			tb_nickName.Margin = new System.Windows.Forms.Padding(10);
			tb_nickName.Name = "tb_nickName";
			tb_nickName.Size = new System.Drawing.Size(300, 29);
			tb_nickName.TabIndex = 45;
			tb_ParentUnitCode.Location = new System.Drawing.Point(172, 155);
			tb_ParentUnitCode.Margin = new System.Windows.Forms.Padding(10);
			tb_ParentUnitCode.Name = "tb_ParentUnitCode";
			tb_ParentUnitCode.Size = new System.Drawing.Size(300, 29);
			tb_ParentUnitCode.TabIndex = 44;
			panel2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel2.Controls.Add(label2);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Location = new System.Drawing.Point(483, 97);
			panel2.Margin = new System.Windows.Forms.Padding(0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(160, 47);
			panel2.TabIndex = 25;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(20, 18);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(138, 21);
			label2.TabIndex = 0;
			label2.Text = "負責人身分證字號";
			tb_ShopIdNo.Location = new System.Drawing.Point(654, 59);
			tb_ShopIdNo.Margin = new System.Windows.Forms.Padding(10);
			tb_ShopIdNo.Name = "tb_ShopIdNo";
			tb_ShopIdNo.Size = new System.Drawing.Size(300, 29);
			tb_ShopIdNo.TabIndex = 1;
			l_shopName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_shopName.AutoSize = true;
			l_shopName.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_shopName.ForeColor = System.Drawing.Color.Black;
			l_shopName.Location = new System.Drawing.Point(644, 14);
			l_shopName.Margin = new System.Windows.Forms.Padding(0);
			l_shopName.Name = "l_shopName";
			l_shopName.Size = new System.Drawing.Size(32, 21);
			l_shopName.TabIndex = 3;
			l_shopName.Text = "{0}";
			panel19.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.SetColumnSpan(panel19, 3);
			panel19.Controls.Add(tb_email);
			panel19.Dock = System.Windows.Forms.DockStyle.Fill;
			panel19.Location = new System.Drawing.Point(162, 331);
			panel19.Margin = new System.Windows.Forms.Padding(0);
			panel19.Name = "panel19";
			panel19.Size = new System.Drawing.Size(804, 49);
			panel19.TabIndex = 36;
			tb_email.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_email.Location = new System.Drawing.Point(10, 12);
			tb_email.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_email.Name = "tb_email";
			tb_email.Size = new System.Drawing.Size(782, 29);
			tb_email.TabIndex = 35;
			tb_tel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_tel.Location = new System.Drawing.Point(172, 292);
			tb_tel.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_tel.Name = "tb_tel";
			tb_tel.Size = new System.Drawing.Size(300, 29);
			tb_tel.TabIndex = 33;
			panel11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel11.Controls.Add(label15);
			panel11.Dock = System.Windows.Forms.DockStyle.Fill;
			panel11.Location = new System.Drawing.Point(483, 283);
			panel11.Margin = new System.Windows.Forms.Padding(0);
			panel11.Name = "panel11";
			panel11.Size = new System.Drawing.Size(160, 47);
			panel11.TabIndex = 23;
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label15.ForeColor = System.Drawing.Color.White;
			label15.Location = new System.Drawing.Point(84, 15);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(74, 21);
			label15.TabIndex = 0;
			label15.Text = "傳真號碼";
			panel9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel9.Controls.Add(label7);
			panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			panel9.Location = new System.Drawing.Point(483, 1);
			panel9.Margin = new System.Windows.Forms.Padding(0);
			panel9.Name = "panel9";
			panel9.Size = new System.Drawing.Size(160, 47);
			panel9.TabIndex = 20;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(100, 14);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(58, 21);
			label7.TabIndex = 0;
			label7.Text = "店全名";
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(label14);
			panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			panel7.Location = new System.Drawing.Point(1, 331);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(160, 49);
			panel7.TabIndex = 20;
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label14.ForeColor = System.Drawing.Color.White;
			label14.Location = new System.Drawing.Point(84, 15);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(74, 21);
			label14.TabIndex = 0;
			label14.Text = "電子信箱";
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			panel6.Location = new System.Drawing.Point(1, 283);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(160, 47);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(84, 13);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(74, 21);
			label12.TabIndex = 0;
			label12.Text = "營業電話";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label6);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 97);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(160, 47);
			panel3.TabIndex = 21;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(68, 18);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(90, 21);
			label6.TabIndex = 0;
			label6.Text = "負責人姓名";
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(1, 1);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(160, 47);
			panel1.TabIndex = 19;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(100, 14);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(58, 21);
			label1.TabIndex = 0;
			label1.Text = "店代碼";
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label8);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Location = new System.Drawing.Point(1, 145);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(160, 47);
			panel4.TabIndex = 22;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.White;
			label8.Location = new System.Drawing.Point(84, 13);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(74, 21);
			label8.TabIndex = 0;
			label8.Text = "上級代碼";
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label10);
			panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			panel5.Location = new System.Drawing.Point(1, 193);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(160, 89);
			panel5.TabIndex = 23;
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(84, 36);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(74, 21);
			label10.TabIndex = 0;
			label10.Text = "營業地址";
			tableLayoutPanel1.SetColumnSpan(panel14, 3);
			panel14.Controls.Add(tb_zipcode);
			panel14.Controls.Add(cb_area);
			panel14.Controls.Add(cb_city);
			panel14.Controls.Add(tb_addr);
			panel14.Dock = System.Windows.Forms.DockStyle.Fill;
			panel14.Location = new System.Drawing.Point(162, 193);
			panel14.Margin = new System.Windows.Forms.Padding(0);
			panel14.Name = "panel14";
			panel14.Size = new System.Drawing.Size(804, 89);
			panel14.TabIndex = 40;
			tb_zipcode.Cursor = System.Windows.Forms.Cursors.No;
			tb_zipcode.Enabled = false;
			tb_zipcode.Location = new System.Drawing.Point(274, 12);
			tb_zipcode.Margin = new System.Windows.Forms.Padding(0);
			tb_zipcode.Name = "tb_zipcode";
			tb_zipcode.ReadOnly = true;
			tb_zipcode.Size = new System.Drawing.Size(100, 29);
			tb_zipcode.TabIndex = 6;
			cb_area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_area.FormattingEnabled = true;
			cb_area.Location = new System.Drawing.Point(141, 12);
			cb_area.Margin = new System.Windows.Forms.Padding(0);
			cb_area.Name = "cb_area";
			cb_area.Size = new System.Drawing.Size(121, 28);
			cb_area.TabIndex = 5;
			cb_area.SelectedIndexChanged += new System.EventHandler(cb_area_SelectedIndexChanged);
			cb_city.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_city.FormattingEnabled = true;
			cb_city.Location = new System.Drawing.Point(10, 12);
			cb_city.Margin = new System.Windows.Forms.Padding(0);
			cb_city.Name = "cb_city";
			cb_city.Size = new System.Drawing.Size(121, 28);
			cb_city.TabIndex = 4;
			cb_city.SelectedIndexChanged += new System.EventHandler(cb_city_SelectedIndexChanged);
			tb_addr.Location = new System.Drawing.Point(10, 50);
			tb_addr.Margin = new System.Windows.Forms.Padding(0);
			tb_addr.Name = "tb_addr";
			tb_addr.Size = new System.Drawing.Size(782, 29);
			tb_addr.TabIndex = 7;
			panel15.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel15.Controls.Add(label23);
			panel15.Dock = System.Windows.Forms.DockStyle.Fill;
			panel15.Location = new System.Drawing.Point(1, 49);
			panel15.Margin = new System.Windows.Forms.Padding(0);
			panel15.Name = "panel15";
			panel15.Size = new System.Drawing.Size(160, 47);
			panel15.TabIndex = 24;
			label23.AutoSize = true;
			label23.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label23.ForeColor = System.Drawing.Color.White;
			label23.Location = new System.Drawing.Point(100, 14);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(58, 21);
			label23.TabIndex = 0;
			label23.Text = "店資訊";
			panel10.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel10.Controls.Add(label13);
			panel10.Dock = System.Windows.Forms.DockStyle.Fill;
			panel10.Location = new System.Drawing.Point(483, 49);
			panel10.Margin = new System.Windows.Forms.Padding(0);
			panel10.Name = "panel10";
			panel10.Size = new System.Drawing.Size(160, 47);
			panel10.TabIndex = 24;
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.White;
			label13.Location = new System.Drawing.Point(84, 18);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(74, 21);
			label13.TabIndex = 0;
			label13.Text = "統一編號\t";
			panel16.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel16.Controls.Add(label3);
			panel16.Dock = System.Windows.Forms.DockStyle.Fill;
			panel16.Location = new System.Drawing.Point(483, 145);
			panel16.Margin = new System.Windows.Forms.Padding(0);
			panel16.Name = "panel16";
			panel16.Size = new System.Drawing.Size(160, 47);
			panel16.TabIndex = 41;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.White;
			label3.Location = new System.Drawing.Point(61, 13);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(97, 21);
			label3.TabIndex = 0;
			label3.Text = "簡稱/分店名";
			tb_DutyName.Location = new System.Drawing.Point(172, 107);
			tb_DutyName.Margin = new System.Windows.Forms.Padding(10);
			tb_DutyName.Name = "tb_DutyName";
			tb_DutyName.Size = new System.Drawing.Size(300, 29);
			tb_DutyName.TabIndex = 1;
			tb_DutyIdNo.Location = new System.Drawing.Point(654, 107);
			tb_DutyIdNo.Margin = new System.Windows.Forms.Padding(10);
			tb_DutyIdNo.Name = "tb_DutyIdNo";
			tb_DutyIdNo.Size = new System.Drawing.Size(300, 29);
			tb_DutyIdNo.TabIndex = 1;
			l_licenseCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_licenseCode.AutoSize = true;
			l_licenseCode.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_licenseCode.ForeColor = System.Drawing.Color.Black;
			l_licenseCode.Location = new System.Drawing.Point(162, 14);
			l_licenseCode.Margin = new System.Windows.Forms.Padding(0);
			l_licenseCode.Name = "l_licenseCode";
			l_licenseCode.Size = new System.Drawing.Size(32, 21);
			l_licenseCode.TabIndex = 4;
			l_licenseCode.Text = "{0}";
			tb_fax.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_fax.Location = new System.Drawing.Point(654, 292);
			tb_fax.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_fax.Name = "tb_fax";
			tb_fax.Size = new System.Drawing.Size(300, 29);
			tb_fax.TabIndex = 33;
			panel27.Controls.Add(cbIsWholesaler);
			panel27.Controls.Add(cbIsRetailer);
			panel27.Controls.Add(l_siteNo);
			panel27.Dock = System.Windows.Forms.DockStyle.Fill;
			panel27.Location = new System.Drawing.Point(165, 52);
			panel27.Name = "panel27";
			panel27.Size = new System.Drawing.Size(314, 41);
			panel27.TabIndex = 46;
			cbIsWholesaler.AutoCheck = false;
			cbIsWholesaler.AutoSize = true;
			cbIsWholesaler.Enabled = false;
			cbIsWholesaler.Location = new System.Drawing.Point(247, 10);
			cbIsWholesaler.Name = "cbIsWholesaler";
			cbIsWholesaler.Size = new System.Drawing.Size(60, 24);
			cbIsWholesaler.TabIndex = 5;
			cbIsWholesaler.Text = "批發";
			cbIsWholesaler.UseVisualStyleBackColor = true;
			cbIsRetailer.AutoCheck = false;
			cbIsRetailer.AutoSize = true;
			cbIsRetailer.Enabled = false;
			cbIsRetailer.Location = new System.Drawing.Point(181, 11);
			cbIsRetailer.Name = "cbIsRetailer";
			cbIsRetailer.Size = new System.Drawing.Size(60, 24);
			cbIsRetailer.TabIndex = 5;
			cbIsRetailer.Text = "零售";
			cbIsRetailer.UseVisualStyleBackColor = true;
			l_siteNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_siteNo.AutoSize = true;
			l_siteNo.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			l_siteNo.ForeColor = System.Drawing.Color.Black;
			l_siteNo.Location = new System.Drawing.Point(3, 10);
			l_siteNo.Margin = new System.Windows.Forms.Padding(0);
			l_siteNo.Name = "l_siteNo";
			l_siteNo.Size = new System.Drawing.Size(32, 21);
			l_siteNo.TabIndex = 2;
			l_siteNo.Text = "{0}";
			tabPage2.Controls.Add(btn_batchSuspend);
			tabPage2.Controls.Add(tlp_userManage);
			tabPage2.Controls.Add(tableLayoutPanel4);
			tabPage2.ForeColor = System.Drawing.Color.Black;
			tabPage2.Location = new System.Drawing.Point(4, 47);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new System.Windows.Forms.Padding(3);
			tabPage2.Size = new System.Drawing.Size(973, 575);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "使用者管理";
			tabPage2.UseVisualStyleBackColor = true;
			btn_batchSuspend.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_batchSuspend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_batchSuspend.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_batchSuspend.ForeColor = System.Drawing.Color.White;
			btn_batchSuspend.Location = new System.Drawing.Point(435, 508);
			btn_batchSuspend.Name = "btn_batchSuspend";
			btn_batchSuspend.Size = new System.Drawing.Size(103, 32);
			btn_batchSuspend.TabIndex = 38;
			btn_batchSuspend.Text = "批次停用";
			btn_batchSuspend.UseVisualStyleBackColor = false;
			btn_batchSuspend.Click += new System.EventHandler(btn_batchSuspend_Click);
			tlp_userManage.AutoScroll = true;
			tlp_userManage.AutoSize = true;
			tlp_userManage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tlp_userManage.BackColor = System.Drawing.Color.White;
			tlp_userManage.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tlp_userManage.ColumnCount = 6;
			tlp_userManage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72f));
			tlp_userManage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 208f));
			tlp_userManage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 208f));
			tlp_userManage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126f));
			tlp_userManage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91f));
			tlp_userManage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 339f));
			tlp_userManage.Controls.Add(label21, 2, 0);
			tlp_userManage.Controls.Add(label20, 1, 0);
			tlp_userManage.Controls.Add(label24, 5, 0);
			tlp_userManage.Controls.Add(label22, 4, 0);
			tlp_userManage.Controls.Add(label25, 3, 0);
			tlp_userManage.Dock = System.Windows.Forms.DockStyle.Top;
			tlp_userManage.Location = new System.Drawing.Point(3, 54);
			tlp_userManage.Margin = new System.Windows.Forms.Padding(0);
			tlp_userManage.MaximumSize = new System.Drawing.Size(0, 420);
			tlp_userManage.Name = "tlp_userManage";
			tlp_userManage.Padding = new System.Windows.Forms.Padding(1, 1, 1, 20);
			tlp_userManage.RowCount = 1;
			tlp_userManage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120f));
			tlp_userManage.Size = new System.Drawing.Size(967, 143);
			tlp_userManage.TabIndex = 37;
			tlp_userManage.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(tableLayoutPanel10_CellPaint);
			label21.Anchor = System.Windows.Forms.AnchorStyles.None;
			label21.AutoSize = true;
			label21.BackColor = System.Drawing.Color.Transparent;
			label21.ForeColor = System.Drawing.Color.White;
			label21.Location = new System.Drawing.Point(354, 50);
			label21.Margin = new System.Windows.Forms.Padding(0);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(67, 24);
			label21.TabIndex = 0;
			label21.Text = "使用者";
			label20.Anchor = System.Windows.Forms.AnchorStyles.None;
			label20.AutoSize = true;
			label20.BackColor = System.Drawing.Color.Transparent;
			label20.ForeColor = System.Drawing.Color.White;
			label20.Location = new System.Drawing.Point(155, 50);
			label20.Margin = new System.Windows.Forms.Padding(0);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(48, 24);
			label20.TabIndex = 0;
			label20.Text = "帳號";
			label24.Anchor = System.Windows.Forms.AnchorStyles.None;
			label24.AutoSize = true;
			label24.BackColor = System.Drawing.Color.Transparent;
			label24.ForeColor = System.Drawing.Color.White;
			label24.Location = new System.Drawing.Point(838, 50);
			label24.Margin = new System.Windows.Forms.Padding(0);
			label24.Name = "label24";
			label24.Size = new System.Drawing.Size(86, 24);
			label24.TabIndex = 0;
			label24.Text = "最後登入";
			label22.Anchor = System.Windows.Forms.AnchorStyles.None;
			label22.AutoSize = true;
			label22.BackColor = System.Drawing.Color.Transparent;
			label22.ForeColor = System.Drawing.Color.White;
			label22.Location = new System.Drawing.Point(641, 50);
			label22.Margin = new System.Windows.Forms.Padding(0);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(48, 24);
			label22.TabIndex = 0;
			label22.Text = "狀態";
			label25.Anchor = System.Windows.Forms.AnchorStyles.None;
			label25.AutoSize = true;
			label25.BackColor = System.Drawing.Color.Transparent;
			label25.ForeColor = System.Drawing.Color.White;
			label25.Location = new System.Drawing.Point(532, 50);
			label25.Margin = new System.Windows.Forms.Padding(0);
			label25.Name = "label25";
			label25.Size = new System.Drawing.Size(48, 24);
			label25.TabIndex = 0;
			label25.Text = "身分";
			tableLayoutPanel4.ColumnCount = 1;
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel4.Controls.Add(btn_createUser, 0, 0);
			tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.RowCount = 1;
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel4.Size = new System.Drawing.Size(967, 51);
			tableLayoutPanel4.TabIndex = 36;
			btn_createUser.Anchor = System.Windows.Forms.AnchorStyles.Right;
			btn_createUser.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_createUser.FlatAppearance.BorderSize = 0;
			btn_createUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_createUser.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_createUser.ForeColor = System.Drawing.Color.White;
			btn_createUser.Location = new System.Drawing.Point(856, 8);
			btn_createUser.Margin = new System.Windows.Forms.Padding(0);
			btn_createUser.Name = "btn_createUser";
			btn_createUser.Size = new System.Drawing.Size(111, 34);
			btn_createUser.TabIndex = 58;
			btn_createUser.Text = "新建使用者";
			btn_createUser.UseVisualStyleBackColor = false;
			btn_createUser.Click += new System.EventHandler(btn_createUser_Click);
			tabPage3.Controls.Add(tableLayoutPanel10);
			tabPage3.Controls.Add(tableLayoutPanel9);
			tabPage3.Controls.Add(panel26);
			tabPage3.Controls.Add(tableLayoutPanel6);
			tabPage3.Controls.Add(tableLayoutPanel5);
			tabPage3.Location = new System.Drawing.Point(4, 47);
			tabPage3.Name = "tabPage3";
			tabPage3.Padding = new System.Windows.Forms.Padding(3);
			tabPage3.Size = new System.Drawing.Size(973, 575);
			tabPage3.TabIndex = 2;
			tabPage3.Text = "印表機設定";
			tabPage3.UseVisualStyleBackColor = true;
			tableLayoutPanel10.BackColor = System.Drawing.Color.White;
			tableLayoutPanel10.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel10.ColumnCount = 2;
			tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.87371f));
			tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.1263f));
			tableLayoutPanel10.Controls.Add(panel28, 0, 1);
			tableLayoutPanel10.Controls.Add(panel29, 0, 0);
			tableLayoutPanel10.Controls.Add(panel30, 1, 1);
			tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel10.Location = new System.Drawing.Point(3, 399);
			tableLayoutPanel10.Name = "tableLayoutPanel10";
			tableLayoutPanel10.RowCount = 2;
			tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel10.Size = new System.Drawing.Size(967, 90);
			tableLayoutPanel10.TabIndex = 10;
			panel28.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel28.Controls.Add(label50);
			panel28.Dock = System.Windows.Forms.DockStyle.Fill;
			panel28.Location = new System.Drawing.Point(1, 45);
			panel28.Margin = new System.Windows.Forms.Padding(0);
			panel28.Name = "panel28";
			panel28.Size = new System.Drawing.Size(162, 44);
			panel28.TabIndex = 24;
			label50.AutoSize = true;
			label50.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label50.ForeColor = System.Drawing.Color.White;
			label50.Location = new System.Drawing.Point(53, 14);
			label50.Name = "label50";
			label50.Size = new System.Drawing.Size(106, 21);
			label50.TabIndex = 0;
			label50.Text = "還原檔案路徑";
			panel29.BackColor = System.Drawing.Color.White;
			tableLayoutPanel10.SetColumnSpan(panel29, 2);
			panel29.Controls.Add(label51);
			panel29.Dock = System.Windows.Forms.DockStyle.Fill;
			panel29.Location = new System.Drawing.Point(1, 1);
			panel29.Margin = new System.Windows.Forms.Padding(0);
			panel29.Name = "panel29";
			panel29.Size = new System.Drawing.Size(965, 43);
			panel29.TabIndex = 46;
			label51.AutoSize = true;
			label51.Image = POS_Client.Properties.Resources.oblique;
			label51.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label51.Location = new System.Drawing.Point(4, 11);
			label51.Name = "label51";
			label51.Size = new System.Drawing.Size(63, 24);
			label51.TabIndex = 1;
			label51.Text = "   還原";
			panel30.Controls.Add(btn_executeRestore);
			panel30.Controls.Add(btn_restoreDataPath);
			panel30.Controls.Add(tb_restoreFilePath);
			panel30.Dock = System.Windows.Forms.DockStyle.Fill;
			panel30.Location = new System.Drawing.Point(164, 45);
			panel30.Margin = new System.Windows.Forms.Padding(0);
			panel30.Name = "panel30";
			panel30.Size = new System.Drawing.Size(802, 44);
			panel30.TabIndex = 47;
			btn_executeRestore.BackColor = System.Drawing.Color.FromArgb(31, 133, 173);
			btn_executeRestore.FlatAppearance.BorderSize = 0;
			btn_executeRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_executeRestore.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_executeRestore.ForeColor = System.Drawing.Color.White;
			btn_executeRestore.Location = new System.Drawing.Point(575, 5);
			btn_executeRestore.Name = "btn_executeRestore";
			btn_executeRestore.Size = new System.Drawing.Size(149, 33);
			btn_executeRestore.TabIndex = 58;
			btn_executeRestore.Text = "立即還原";
			btn_executeRestore.UseVisualStyleBackColor = false;
			btn_executeRestore.Click += new System.EventHandler(btn_executeRestore_Click);
			btn_restoreDataPath.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_restoreDataPath.FlatAppearance.BorderSize = 0;
			btn_restoreDataPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_restoreDataPath.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_restoreDataPath.ForeColor = System.Drawing.Color.White;
			btn_restoreDataPath.Location = new System.Drawing.Point(404, 5);
			btn_restoreDataPath.Name = "btn_restoreDataPath";
			btn_restoreDataPath.Size = new System.Drawing.Size(149, 33);
			btn_restoreDataPath.TabIndex = 58;
			btn_restoreDataPath.Text = "選擇還原檔案";
			btn_restoreDataPath.UseVisualStyleBackColor = false;
			btn_restoreDataPath.Click += new System.EventHandler(btn_restoreDataPath_Click);
			tb_restoreFilePath.Location = new System.Drawing.Point(12, 5);
			tb_restoreFilePath.Name = "tb_restoreFilePath";
			tb_restoreFilePath.ReadOnly = true;
			tb_restoreFilePath.Size = new System.Drawing.Size(370, 33);
			tb_restoreFilePath.TabIndex = 48;
			tableLayoutPanel9.BackColor = System.Drawing.Color.White;
			tableLayoutPanel9.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel9.ColumnCount = 2;
			tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.87371f));
			tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.1263f));
			tableLayoutPanel9.Controls.Add(panel54, 0, 2);
			tableLayoutPanel9.Controls.Add(panel55, 0, 1);
			tableLayoutPanel9.Controls.Add(panel56, 0, 0);
			tableLayoutPanel9.Controls.Add(panel57, 1, 1);
			tableLayoutPanel9.Controls.Add(panel58, 1, 2);
			tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel9.Location = new System.Drawing.Point(3, 267);
			tableLayoutPanel9.Name = "tableLayoutPanel9";
			tableLayoutPanel9.RowCount = 3;
			tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel9.Size = new System.Drawing.Size(967, 132);
			tableLayoutPanel9.TabIndex = 9;
			panel54.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel54.Controls.Add(label44);
			panel54.Dock = System.Windows.Forms.DockStyle.Fill;
			panel54.Location = new System.Drawing.Point(1, 87);
			panel54.Margin = new System.Windows.Forms.Padding(0);
			panel54.Name = "panel54";
			panel54.Size = new System.Drawing.Size(162, 44);
			panel54.TabIndex = 21;
			label44.AutoSize = true;
			label44.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label44.ForeColor = System.Drawing.Color.White;
			label44.Location = new System.Drawing.Point(85, 12);
			label44.Name = "label44";
			label44.Size = new System.Drawing.Size(74, 21);
			label44.TabIndex = 0;
			label44.Text = "手動備份";
			panel55.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel55.Controls.Add(label45);
			panel55.Dock = System.Windows.Forms.DockStyle.Fill;
			panel55.Location = new System.Drawing.Point(1, 44);
			panel55.Margin = new System.Windows.Forms.Padding(0);
			panel55.Name = "panel55";
			panel55.Size = new System.Drawing.Size(162, 42);
			panel55.TabIndex = 24;
			label45.AutoSize = true;
			label45.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label45.ForeColor = System.Drawing.Color.White;
			label45.Location = new System.Drawing.Point(87, 11);
			label45.Name = "label45";
			label45.Size = new System.Drawing.Size(74, 21);
			label45.TabIndex = 0;
			label45.Text = "自動備份";
			panel56.BackColor = System.Drawing.Color.White;
			tableLayoutPanel9.SetColumnSpan(panel56, 2);
			panel56.Controls.Add(label46);
			panel56.Dock = System.Windows.Forms.DockStyle.Fill;
			panel56.Location = new System.Drawing.Point(1, 1);
			panel56.Margin = new System.Windows.Forms.Padding(0);
			panel56.Name = "panel56";
			panel56.Size = new System.Drawing.Size(965, 42);
			panel56.TabIndex = 46;
			label46.AutoSize = true;
			label46.Image = POS_Client.Properties.Resources.oblique;
			label46.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label46.Location = new System.Drawing.Point(4, 18);
			label46.Name = "label46";
			label46.Size = new System.Drawing.Size(101, 24);
			label46.TabIndex = 1;
			label46.Text = "   備份設定";
			panel57.Controls.Add(btn_AutoBackupPath);
			panel57.Controls.Add(tb_autoPath);
			panel57.Dock = System.Windows.Forms.DockStyle.Fill;
			panel57.Location = new System.Drawing.Point(164, 44);
			panel57.Margin = new System.Windows.Forms.Padding(0);
			panel57.Name = "panel57";
			panel57.Size = new System.Drawing.Size(802, 42);
			panel57.TabIndex = 47;
			btn_AutoBackupPath.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_AutoBackupPath.FlatAppearance.BorderSize = 0;
			btn_AutoBackupPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_AutoBackupPath.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_AutoBackupPath.ForeColor = System.Drawing.Color.White;
			btn_AutoBackupPath.Location = new System.Drawing.Point(404, 3);
			btn_AutoBackupPath.Name = "btn_AutoBackupPath";
			btn_AutoBackupPath.Size = new System.Drawing.Size(149, 34);
			btn_AutoBackupPath.TabIndex = 57;
			btn_AutoBackupPath.Text = "自訂儲存路徑";
			btn_AutoBackupPath.UseVisualStyleBackColor = false;
			btn_AutoBackupPath.Click += new System.EventHandler(btn_AutoBackupPath_Click);
			tb_autoPath.Location = new System.Drawing.Point(12, 4);
			tb_autoPath.Name = "tb_autoPath";
			tb_autoPath.ReadOnly = true;
			tb_autoPath.Size = new System.Drawing.Size(370, 33);
			tb_autoPath.TabIndex = 48;
			panel58.Controls.Add(btn_ManualBackup);
			panel58.Controls.Add(btn_ManualBackupPath);
			panel58.Controls.Add(tb_manualPath);
			panel58.Dock = System.Windows.Forms.DockStyle.Fill;
			panel58.Location = new System.Drawing.Point(164, 87);
			panel58.Margin = new System.Windows.Forms.Padding(0);
			panel58.Name = "panel58";
			panel58.Size = new System.Drawing.Size(802, 44);
			panel58.TabIndex = 47;
			btn_ManualBackup.BackColor = System.Drawing.Color.FromArgb(31, 133, 173);
			btn_ManualBackup.FlatAppearance.BorderSize = 0;
			btn_ManualBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_ManualBackup.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_ManualBackup.ForeColor = System.Drawing.Color.White;
			btn_ManualBackup.Location = new System.Drawing.Point(575, 6);
			btn_ManualBackup.Name = "btn_ManualBackup";
			btn_ManualBackup.Size = new System.Drawing.Size(149, 33);
			btn_ManualBackup.TabIndex = 58;
			btn_ManualBackup.Text = "立即備份";
			btn_ManualBackup.UseVisualStyleBackColor = false;
			btn_ManualBackup.Click += new System.EventHandler(btn_ManualBackup_Click);
			btn_ManualBackupPath.BackColor = System.Drawing.Color.FromArgb(34, 159, 208);
			btn_ManualBackupPath.FlatAppearance.BorderSize = 0;
			btn_ManualBackupPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_ManualBackupPath.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_ManualBackupPath.ForeColor = System.Drawing.Color.White;
			btn_ManualBackupPath.Location = new System.Drawing.Point(404, 6);
			btn_ManualBackupPath.Name = "btn_ManualBackupPath";
			btn_ManualBackupPath.Size = new System.Drawing.Size(149, 33);
			btn_ManualBackupPath.TabIndex = 58;
			btn_ManualBackupPath.Text = "自訂儲存路徑";
			btn_ManualBackupPath.UseVisualStyleBackColor = false;
			btn_ManualBackupPath.Click += new System.EventHandler(btn_ManualBackupPath_Click);
			tb_manualPath.Location = new System.Drawing.Point(12, 6);
			tb_manualPath.Name = "tb_manualPath";
			tb_manualPath.ReadOnly = true;
			tb_manualPath.Size = new System.Drawing.Size(370, 33);
			tb_manualPath.TabIndex = 48;
			panel26.Controls.Add(btn_printerSave);
			panel26.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel26.Location = new System.Drawing.Point(3, 488);
			panel26.Margin = new System.Windows.Forms.Padding(0);
			panel26.Name = "panel26";
			panel26.Size = new System.Drawing.Size(967, 84);
			panel26.TabIndex = 8;
			btn_printerSave.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_printerSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_printerSave.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_printerSave.ForeColor = System.Drawing.Color.White;
			btn_printerSave.Location = new System.Drawing.Point(434, 7);
			btn_printerSave.Name = "btn_printerSave";
			btn_printerSave.Size = new System.Drawing.Size(103, 32);
			btn_printerSave.TabIndex = 7;
			btn_printerSave.Text = "儲存變更";
			btn_printerSave.UseVisualStyleBackColor = false;
			btn_printerSave.Click += new System.EventHandler(btn_printerSave_Click);
			tableLayoutPanel6.BackColor = System.Drawing.Color.White;
			tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel6.ColumnCount = 2;
			tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.08075f));
			tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.91926f));
			tableLayoutPanel6.Controls.Add(cb_listPrinter, 1, 2);
			tableLayoutPanel6.Controls.Add(panel34, 0, 2);
			tableLayoutPanel6.Controls.Add(panel35, 0, 1);
			tableLayoutPanel6.Controls.Add(panel36, 0, 0);
			tableLayoutPanel6.Controls.Add(panel41, 1, 1);
			tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel6.Location = new System.Drawing.Point(3, 135);
			tableLayoutPanel6.Name = "tableLayoutPanel6";
			tableLayoutPanel6.RowCount = 3;
			tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel6.Size = new System.Drawing.Size(967, 132);
			tableLayoutPanel6.TabIndex = 3;
			cb_listPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_listPrinter.FormattingEnabled = true;
			cb_listPrinter.Location = new System.Drawing.Point(176, 93);
			cb_listPrinter.Margin = new System.Windows.Forms.Padding(10, 6, 10, 0);
			cb_listPrinter.Name = "cb_listPrinter";
			cb_listPrinter.Size = new System.Drawing.Size(370, 32);
			cb_listPrinter.TabIndex = 1;
			panel34.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel34.Controls.Add(label33);
			panel34.Dock = System.Windows.Forms.DockStyle.Fill;
			panel34.Location = new System.Drawing.Point(1, 87);
			panel34.Margin = new System.Windows.Forms.Padding(0);
			panel34.Name = "panel34";
			panel34.Size = new System.Drawing.Size(164, 44);
			panel34.TabIndex = 21;
			label33.AutoSize = true;
			label33.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label33.ForeColor = System.Drawing.Color.White;
			label33.Location = new System.Drawing.Point(71, 12);
			label33.Name = "label33";
			label33.Size = new System.Drawing.Size(90, 21);
			label33.TabIndex = 0;
			label33.Text = "預設印表機";
			panel35.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel35.Controls.Add(label34);
			panel35.Dock = System.Windows.Forms.DockStyle.Fill;
			panel35.Location = new System.Drawing.Point(1, 44);
			panel35.Margin = new System.Windows.Forms.Padding(0);
			panel35.Name = "panel35";
			panel35.Size = new System.Drawing.Size(164, 42);
			panel35.TabIndex = 24;
			label34.AutoSize = true;
			label34.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label34.ForeColor = System.Drawing.Color.White;
			label34.Location = new System.Drawing.Point(87, 11);
			label34.Name = "label34";
			label34.Size = new System.Drawing.Size(74, 21);
			label34.TabIndex = 0;
			label34.Text = "預設模式";
			panel36.BackColor = System.Drawing.Color.White;
			tableLayoutPanel6.SetColumnSpan(panel36, 2);
			panel36.Controls.Add(label35);
			panel36.Dock = System.Windows.Forms.DockStyle.Fill;
			panel36.Location = new System.Drawing.Point(1, 1);
			panel36.Margin = new System.Windows.Forms.Padding(0);
			panel36.Name = "panel36";
			panel36.Size = new System.Drawing.Size(965, 42);
			panel36.TabIndex = 46;
			label35.AutoSize = true;
			label35.Image = POS_Client.Properties.Resources.oblique;
			label35.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label35.Location = new System.Drawing.Point(4, 14);
			label35.Name = "label35";
			label35.Size = new System.Drawing.Size(139, 24);
			label35.TabIndex = 0;
			label35.Text = "   條碼清冊列印";
			panel41.Controls.Add(button1);
			panel41.Dock = System.Windows.Forms.DockStyle.Fill;
			panel41.Location = new System.Drawing.Point(166, 44);
			panel41.Margin = new System.Windows.Forms.Padding(0);
			panel41.Name = "panel41";
			panel41.Size = new System.Drawing.Size(800, 42);
			panel41.TabIndex = 47;
			button1.BackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.ForeColor = System.Drawing.Color.White;
			button1.Location = new System.Drawing.Point(10, 7);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(87, 29);
			button1.TabIndex = 48;
			button1.Text = "A4";
			button1.UseVisualStyleBackColor = false;
			tableLayoutPanel5.BackColor = System.Drawing.Color.White;
			tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel5.ColumnCount = 2;
			tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.08075f));
			tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.91926f));
			tableLayoutPanel5.Controls.Add(panel40, 0, 2);
			tableLayoutPanel5.Controls.Add(panel45, 0, 1);
			tableLayoutPanel5.Controls.Add(panel37, 0, 0);
			tableLayoutPanel5.Controls.Add(panel38, 1, 1);
			tableLayoutPanel5.Controls.Add(cb_receivePrinter, 1, 2);
			tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel5.Name = "tableLayoutPanel5";
			tableLayoutPanel5.RowCount = 3;
			tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.65438f));
			tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel5.Size = new System.Drawing.Size(967, 132);
			tableLayoutPanel5.TabIndex = 2;
			panel40.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel40.Controls.Add(label38);
			panel40.Dock = System.Windows.Forms.DockStyle.Fill;
			panel40.Location = new System.Drawing.Point(1, 87);
			panel40.Margin = new System.Windows.Forms.Padding(0);
			panel40.Name = "panel40";
			panel40.Size = new System.Drawing.Size(164, 44);
			panel40.TabIndex = 21;
			label38.AutoSize = true;
			label38.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label38.ForeColor = System.Drawing.Color.White;
			label38.Location = new System.Drawing.Point(71, 12);
			label38.Name = "label38";
			label38.Size = new System.Drawing.Size(90, 21);
			label38.TabIndex = 0;
			label38.Text = "預設印表機";
			panel45.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel45.Controls.Add(label42);
			panel45.Dock = System.Windows.Forms.DockStyle.Fill;
			panel45.Location = new System.Drawing.Point(1, 44);
			panel45.Margin = new System.Windows.Forms.Padding(0);
			panel45.Name = "panel45";
			panel45.Size = new System.Drawing.Size(164, 42);
			panel45.TabIndex = 24;
			label42.AutoSize = true;
			label42.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label42.ForeColor = System.Drawing.Color.White;
			label42.Location = new System.Drawing.Point(87, 11);
			label42.Name = "label42";
			label42.Size = new System.Drawing.Size(74, 21);
			label42.TabIndex = 0;
			label42.Text = "預設模式";
			panel37.BackColor = System.Drawing.Color.White;
			tableLayoutPanel5.SetColumnSpan(panel37, 2);
			panel37.Controls.Add(label32);
			panel37.Dock = System.Windows.Forms.DockStyle.Fill;
			panel37.Location = new System.Drawing.Point(1, 1);
			panel37.Margin = new System.Windows.Forms.Padding(0);
			panel37.Name = "panel37";
			panel37.Size = new System.Drawing.Size(965, 42);
			panel37.TabIndex = 46;
			label32.AutoSize = true;
			label32.Image = POS_Client.Properties.Resources.oblique;
			label32.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label32.Location = new System.Drawing.Point(4, 13);
			label32.Name = "label32";
			label32.Size = new System.Drawing.Size(101, 24);
			label32.TabIndex = 0;
			label32.Text = "   收據列印";
			panel38.Controls.Add(btn_receiveA4);
			panel38.Controls.Add(btn_receive80);
			panel38.Controls.Add(btn_receive60);
			panel38.Dock = System.Windows.Forms.DockStyle.Fill;
			panel38.Location = new System.Drawing.Point(166, 44);
			panel38.Margin = new System.Windows.Forms.Padding(0);
			panel38.Name = "panel38";
			panel38.Size = new System.Drawing.Size(800, 42);
			panel38.TabIndex = 47;
			btn_receiveA4.BackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_receiveA4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_receiveA4.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_receiveA4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_receiveA4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_receiveA4.ForeColor = System.Drawing.Color.White;
			btn_receiveA4.Location = new System.Drawing.Point(226, 6);
			btn_receiveA4.Name = "btn_receiveA4";
			btn_receiveA4.Size = new System.Drawing.Size(87, 29);
			btn_receiveA4.TabIndex = 49;
			btn_receiveA4.Text = "A4";
			btn_receiveA4.UseVisualStyleBackColor = false;
			btn_receiveA4.Click += new System.EventHandler(btn_receiveA4_Click);
			btn_receive80.BackColor = System.Drawing.Color.White;
			btn_receive80.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_receive80.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_receive80.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_receive80.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_receive80.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_receive80.Location = new System.Drawing.Point(118, 6);
			btn_receive80.Name = "btn_receive80";
			btn_receive80.Size = new System.Drawing.Size(87, 29);
			btn_receive80.TabIndex = 48;
			btn_receive80.Text = "80 mm";
			btn_receive80.UseVisualStyleBackColor = false;
			btn_receive80.Click += new System.EventHandler(btn_receive80_Click);
			btn_receive60.BackColor = System.Drawing.Color.White;
			btn_receive60.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_receive60.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_receive60.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_receive60.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_receive60.ForeColor = System.Drawing.Color.FromArgb(247, 106, 45);
			btn_receive60.Location = new System.Drawing.Point(10, 6);
			btn_receive60.Name = "btn_receive60";
			btn_receive60.Size = new System.Drawing.Size(87, 29);
			btn_receive60.TabIndex = 47;
			btn_receive60.Text = "60 mm";
			btn_receive60.UseVisualStyleBackColor = false;
			btn_receive60.Click += new System.EventHandler(btn_receive60_Click);
			cb_receivePrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_receivePrinter.FormattingEnabled = true;
			cb_receivePrinter.Location = new System.Drawing.Point(176, 93);
			cb_receivePrinter.Margin = new System.Windows.Forms.Padding(10, 6, 10, 0);
			cb_receivePrinter.Name = "cb_receivePrinter";
			cb_receivePrinter.Size = new System.Drawing.Size(370, 32);
			cb_receivePrinter.TabIndex = 1;
			tabPage4.Controls.Add(tableLayoutPanel11);
			tabPage4.Controls.Add(tableLayoutPanel8);
			tabPage4.Controls.Add(tableLayoutPanel7);
			tabPage4.Location = new System.Drawing.Point(4, 47);
			tabPage4.Name = "tabPage4";
			tabPage4.Padding = new System.Windows.Forms.Padding(3);
			tabPage4.Size = new System.Drawing.Size(973, 575);
			tabPage4.TabIndex = 3;
			tabPage4.Text = "常用功能(快捷)";
			tabPage4.UseVisualStyleBackColor = true;
			tableLayoutPanel11.BackColor = System.Drawing.Color.White;
			tableLayoutPanel11.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel11.ColumnCount = 2;
			tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel11.Controls.Add(panel31, 0, 1);
			tableLayoutPanel11.Controls.Add(panel32, 0, 0);
			tableLayoutPanel11.Controls.Add(panel33, 1, 1);
			tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel11.Location = new System.Drawing.Point(3, 395);
			tableLayoutPanel11.Name = "tableLayoutPanel11";
			tableLayoutPanel11.RowCount = 2;
			tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel11.Size = new System.Drawing.Size(967, 97);
			tableLayoutPanel11.TabIndex = 5;
			panel31.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel31.Controls.Add(label52);
			panel31.Dock = System.Windows.Forms.DockStyle.Fill;
			panel31.Location = new System.Drawing.Point(1, 49);
			panel31.Margin = new System.Windows.Forms.Padding(0);
			panel31.Name = "panel31";
			panel31.Size = new System.Drawing.Size(162, 47);
			panel31.TabIndex = 24;
			label52.AutoSize = true;
			label52.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label52.ForeColor = System.Drawing.Color.White;
			label52.Location = new System.Drawing.Point(85, 14);
			label52.Name = "label52";
			label52.Size = new System.Drawing.Size(74, 21);
			label52.TabIndex = 0;
			label52.Text = "系統版本";
			panel32.BackColor = System.Drawing.Color.White;
			tableLayoutPanel11.SetColumnSpan(panel32, 2);
			panel32.Controls.Add(label53);
			panel32.Dock = System.Windows.Forms.DockStyle.Fill;
			panel32.Location = new System.Drawing.Point(1, 1);
			panel32.Margin = new System.Windows.Forms.Padding(0);
			panel32.Name = "panel32";
			panel32.Size = new System.Drawing.Size(965, 47);
			panel32.TabIndex = 46;
			label53.AutoSize = true;
			label53.Image = POS_Client.Properties.Resources.oblique;
			label53.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label53.Location = new System.Drawing.Point(4, 11);
			label53.Name = "label53";
			label53.Size = new System.Drawing.Size(101, 24);
			label53.TabIndex = 0;
			label53.Text = "   系統版本";
			panel33.Controls.Add(btn_OldDBrestore);
			panel33.Controls.Add(btn_downloadDataReset);
			panel33.Controls.Add(l_systemVersion);
			panel33.Dock = System.Windows.Forms.DockStyle.Fill;
			panel33.Location = new System.Drawing.Point(164, 49);
			panel33.Margin = new System.Windows.Forms.Padding(0);
			panel33.Name = "panel33";
			panel33.Size = new System.Drawing.Size(802, 47);
			panel33.TabIndex = 47;
			btn_OldDBrestore.BackColor = System.Drawing.Color.FromArgb(255, 109, 49);
			btn_OldDBrestore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			btn_OldDBrestore.FlatAppearance.BorderSize = 0;
			btn_OldDBrestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_OldDBrestore.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_OldDBrestore.ForeColor = System.Drawing.Color.White;
			btn_OldDBrestore.Location = new System.Drawing.Point(608, 3);
			btn_OldDBrestore.Name = "btn_OldDBrestore";
			btn_OldDBrestore.Size = new System.Drawing.Size(149, 41);
			btn_OldDBrestore.TabIndex = 57;
			btn_OldDBrestore.Text = "資料移轉(不使用)";
			btn_OldDBrestore.UseVisualStyleBackColor = false;
			btn_OldDBrestore.Visible = false;
			btn_OldDBrestore.Click += new System.EventHandler(btnbtn_OldDBrestore_Click);
			btn_downloadDataReset.BackColor = System.Drawing.Color.FromArgb(255, 109, 49);
			btn_downloadDataReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			btn_downloadDataReset.FlatAppearance.BorderSize = 0;
			btn_downloadDataReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_downloadDataReset.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_downloadDataReset.ForeColor = System.Drawing.Color.White;
			btn_downloadDataReset.Location = new System.Drawing.Point(245, 3);
			btn_downloadDataReset.Name = "btn_downloadDataReset";
			btn_downloadDataReset.Size = new System.Drawing.Size(149, 41);
			btn_downloadDataReset.TabIndex = 56;
			btn_downloadDataReset.Text = "下載資料重置";
			btn_downloadDataReset.UseVisualStyleBackColor = false;
			btn_downloadDataReset.Click += new System.EventHandler(btn_downloadDataReset_Click);
			l_systemVersion.AutoSize = true;
			l_systemVersion.Location = new System.Drawing.Point(16, 11);
			l_systemVersion.Name = "l_systemVersion";
			l_systemVersion.Size = new System.Drawing.Size(33, 24);
			l_systemVersion.TabIndex = 50;
			l_systemVersion.Text = "{0}";
			tableLayoutPanel8.BackColor = System.Drawing.Color.White;
			tableLayoutPanel8.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel8.ColumnCount = 2;
			tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel8.Controls.Add(panel49, 0, 2);
			tableLayoutPanel8.Controls.Add(panel50, 0, 1);
			tableLayoutPanel8.Controls.Add(panel51, 0, 0);
			tableLayoutPanel8.Controls.Add(panel52, 1, 1);
			tableLayoutPanel8.Controls.Add(panel53, 1, 2);
			tableLayoutPanel8.Controls.Add(panel22, 0, 3);
			tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel8.Location = new System.Drawing.Point(3, 211);
			tableLayoutPanel8.Name = "tableLayoutPanel8";
			tableLayoutPanel8.RowCount = 4;
			tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
			tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
			tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
			tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25f));
			tableLayoutPanel8.Size = new System.Drawing.Size(967, 184);
			tableLayoutPanel8.TabIndex = 4;
			panel49.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel49.Controls.Add(label40);
			panel49.Dock = System.Windows.Forms.DockStyle.Fill;
			panel49.Location = new System.Drawing.Point(1, 91);
			panel49.Margin = new System.Windows.Forms.Padding(0);
			panel49.Name = "panel49";
			panel49.Size = new System.Drawing.Size(162, 44);
			panel49.TabIndex = 21;
			label40.AutoSize = true;
			label40.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label40.ForeColor = System.Drawing.Color.White;
			label40.Location = new System.Drawing.Point(87, 10);
			label40.Name = "label40";
			label40.Size = new System.Drawing.Size(74, 21);
			label40.TabIndex = 0;
			label40.Text = "退貨作業";
			panel50.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel50.Controls.Add(label41);
			panel50.Dock = System.Windows.Forms.DockStyle.Fill;
			panel50.Location = new System.Drawing.Point(1, 46);
			panel50.Margin = new System.Windows.Forms.Padding(0);
			panel50.Name = "panel50";
			panel50.Size = new System.Drawing.Size(162, 44);
			panel50.TabIndex = 24;
			label41.AutoSize = true;
			label41.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label41.ForeColor = System.Drawing.Color.White;
			label41.Location = new System.Drawing.Point(87, 11);
			label41.Name = "label41";
			label41.Size = new System.Drawing.Size(74, 21);
			label41.TabIndex = 0;
			label41.Text = "銷售作業";
			panel51.BackColor = System.Drawing.Color.White;
			tableLayoutPanel8.SetColumnSpan(panel51, 2);
			panel51.Controls.Add(label43);
			panel51.Dock = System.Windows.Forms.DockStyle.Fill;
			panel51.Location = new System.Drawing.Point(1, 1);
			panel51.Margin = new System.Windows.Forms.Padding(0);
			panel51.Name = "panel51";
			panel51.Size = new System.Drawing.Size(965, 44);
			panel51.TabIndex = 46;
			label43.AutoSize = true;
			label43.Image = POS_Client.Properties.Resources.oblique;
			label43.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label43.Location = new System.Drawing.Point(4, 12);
			label43.Name = "label43";
			label43.Size = new System.Drawing.Size(120, 24);
			label43.TabIndex = 0;
			label43.Text = "   主功能選單";
			panel52.Controls.Add(cb_shop);
			panel52.Controls.Add(label47);
			panel52.Dock = System.Windows.Forms.DockStyle.Fill;
			panel52.Location = new System.Drawing.Point(164, 46);
			panel52.Margin = new System.Windows.Forms.Padding(0);
			panel52.Name = "panel52";
			panel52.Size = new System.Drawing.Size(802, 44);
			panel52.TabIndex = 47;
			cb_shop.AutoSize = true;
			cb_shop.Checked = true;
			cb_shop.CheckState = System.Windows.Forms.CheckState.Checked;
			cb_shop.Location = new System.Drawing.Point(196, 7);
			cb_shop.Name = "cb_shop";
			cb_shop.Size = new System.Drawing.Size(124, 28);
			cb_shop.TabIndex = 49;
			cb_shop.Text = "可列印條碼";
			cb_shop.UseVisualStyleBackColor = true;
			label47.AutoSize = true;
			label47.Location = new System.Drawing.Point(16, 11);
			label47.Name = "label47";
			label47.Size = new System.Drawing.Size(146, 24);
			label47.TabIndex = 50;
			label47.Text = "CTRL+SHIFT+S";
			panel53.Controls.Add(cb_returnM);
			panel53.Controls.Add(label48);
			panel53.Dock = System.Windows.Forms.DockStyle.Fill;
			panel53.Location = new System.Drawing.Point(164, 91);
			panel53.Margin = new System.Windows.Forms.Padding(0);
			panel53.Name = "panel53";
			panel53.Size = new System.Drawing.Size(802, 44);
			panel53.TabIndex = 47;
			cb_returnM.AutoSize = true;
			cb_returnM.Checked = true;
			cb_returnM.CheckState = System.Windows.Forms.CheckState.Checked;
			cb_returnM.Location = new System.Drawing.Point(196, 6);
			cb_returnM.Name = "cb_returnM";
			cb_returnM.Size = new System.Drawing.Size(124, 28);
			cb_returnM.TabIndex = 49;
			cb_returnM.Text = "可列印條碼";
			cb_returnM.UseVisualStyleBackColor = true;
			label48.AutoSize = true;
			label48.Location = new System.Drawing.Point(15, 10);
			label48.Name = "label48";
			label48.Size = new System.Drawing.Size(147, 24);
			label48.TabIndex = 50;
			label48.Text = "CTRL+SHIFT+R";
			tableLayoutPanel8.SetColumnSpan(panel22, 2);
			panel22.Controls.Add(btn_printHotKey);
			panel22.Dock = System.Windows.Forms.DockStyle.Fill;
			panel22.Location = new System.Drawing.Point(1, 136);
			panel22.Margin = new System.Windows.Forms.Padding(0);
			panel22.Name = "panel22";
			panel22.Size = new System.Drawing.Size(965, 47);
			panel22.TabIndex = 47;
			btn_printHotKey.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			btn_printHotKey.FlatAppearance.BorderSize = 0;
			btn_printHotKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_printHotKey.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_printHotKey.ForeColor = System.Drawing.Color.White;
			btn_printHotKey.Location = new System.Drawing.Point(408, 4);
			btn_printHotKey.Name = "btn_printHotKey";
			btn_printHotKey.Size = new System.Drawing.Size(149, 40);
			btn_printHotKey.TabIndex = 56;
			btn_printHotKey.Text = "列印功能條碼";
			btn_printHotKey.UseVisualStyleBackColor = false;
			btn_printHotKey.Click += new System.EventHandler(btn_printHotKey_Click);
			tableLayoutPanel7.BackColor = System.Drawing.Color.White;
			tableLayoutPanel7.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel7.ColumnCount = 4;
			tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel7.Controls.Add(label27, 1, 2);
			tableLayoutPanel7.Controls.Add(panel43, 0, 2);
			tableLayoutPanel7.Controls.Add(panel44, 0, 1);
			tableLayoutPanel7.Controls.Add(panel46, 0, 0);
			tableLayoutPanel7.Controls.Add(panel47, 1, 1);
			tableLayoutPanel7.Controls.Add(panel20, 0, 3);
			tableLayoutPanel7.Controls.Add(panel21, 0, 4);
			tableLayoutPanel7.Controls.Add(panel24, 1, 4);
			tableLayoutPanel7.Controls.Add(panel23, 2, 2);
			tableLayoutPanel7.Controls.Add(panel25, 2, 3);
			tableLayoutPanel7.Controls.Add(label28, 1, 3);
			tableLayoutPanel7.Controls.Add(label29, 3, 2);
			tableLayoutPanel7.Controls.Add(label30, 3, 3);
			tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel7.Name = "tableLayoutPanel7";
			tableLayoutPanel7.RowCount = 5;
			tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20f));
			tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20f));
			tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20f));
			tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20f));
			tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20f));
			tableLayoutPanel7.Size = new System.Drawing.Size(967, 208);
			tableLayoutPanel7.TabIndex = 3;
			label27.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label27.AutoSize = true;
			label27.Location = new System.Drawing.Point(194, 91);
			label27.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
			label27.Name = "label27";
			label27.Size = new System.Drawing.Size(24, 24);
			label27.TabIndex = 50;
			label27.Text = "+";
			panel43.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel43.Controls.Add(label36);
			panel43.Dock = System.Windows.Forms.DockStyle.Fill;
			panel43.Location = new System.Drawing.Point(1, 83);
			panel43.Margin = new System.Windows.Forms.Padding(0);
			panel43.Name = "panel43";
			panel43.Size = new System.Drawing.Size(162, 40);
			panel43.TabIndex = 21;
			label36.AutoSize = true;
			label36.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label36.ForeColor = System.Drawing.Color.White;
			label36.Location = new System.Drawing.Point(88, 9);
			label36.Name = "label36";
			label36.Size = new System.Drawing.Size(74, 21);
			label36.TabIndex = 0;
			label36.Text = "數量加一";
			panel44.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel44.Controls.Add(label37);
			panel44.Dock = System.Windows.Forms.DockStyle.Fill;
			panel44.Location = new System.Drawing.Point(1, 42);
			panel44.Margin = new System.Windows.Forms.Padding(0);
			panel44.Name = "panel44";
			panel44.Size = new System.Drawing.Size(162, 40);
			panel44.TabIndex = 24;
			label37.AutoSize = true;
			label37.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label37.ForeColor = System.Drawing.Color.White;
			label37.Location = new System.Drawing.Point(56, 12);
			label37.Name = "label37";
			label37.Size = new System.Drawing.Size(106, 21);
			label37.TabIndex = 0;
			label37.Text = "會員條碼輸入";
			panel46.BackColor = System.Drawing.Color.White;
			tableLayoutPanel7.SetColumnSpan(panel46, 4);
			panel46.Controls.Add(label39);
			panel46.Dock = System.Windows.Forms.DockStyle.Fill;
			panel46.Location = new System.Drawing.Point(1, 1);
			panel46.Margin = new System.Windows.Forms.Padding(0);
			panel46.Name = "panel46";
			panel46.Size = new System.Drawing.Size(965, 40);
			panel46.TabIndex = 46;
			label39.AutoSize = true;
			label39.Image = POS_Client.Properties.Resources.oblique;
			label39.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label39.Location = new System.Drawing.Point(4, 8);
			label39.Name = "label39";
			label39.Size = new System.Drawing.Size(139, 24);
			label39.TabIndex = 0;
			label39.Text = "   銷售快捷設定";
			tableLayoutPanel7.SetColumnSpan(panel47, 3);
			panel47.Controls.Add(label26);
			panel47.Controls.Add(cb_member);
			panel47.Dock = System.Windows.Forms.DockStyle.Fill;
			panel47.Location = new System.Drawing.Point(164, 42);
			panel47.Margin = new System.Windows.Forms.Padding(0);
			panel47.Name = "panel47";
			panel47.Size = new System.Drawing.Size(802, 40);
			panel47.TabIndex = 47;
			label26.AutoSize = true;
			label26.Location = new System.Drawing.Point(16, 9);
			label26.Name = "label26";
			label26.Size = new System.Drawing.Size(88, 24);
			label26.TabIndex = 50;
			label26.Text = "CTRL+M";
			cb_member.AutoSize = true;
			cb_member.Checked = true;
			cb_member.CheckState = System.Windows.Forms.CheckState.Checked;
			cb_member.Location = new System.Drawing.Point(196, 8);
			cb_member.Name = "cb_member";
			cb_member.Size = new System.Drawing.Size(124, 28);
			cb_member.TabIndex = 49;
			cb_member.Text = "可列印條碼";
			cb_member.UseVisualStyleBackColor = true;
			panel20.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel20.Controls.Add(label5);
			panel20.Dock = System.Windows.Forms.DockStyle.Fill;
			panel20.Location = new System.Drawing.Point(1, 124);
			panel20.Margin = new System.Windows.Forms.Padding(0);
			panel20.Name = "panel20";
			panel20.Size = new System.Drawing.Size(162, 40);
			panel20.TabIndex = 21;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.White;
			label5.Location = new System.Drawing.Point(88, 9);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(74, 21);
			label5.TabIndex = 0;
			label5.Text = "上移一筆";
			panel21.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel21.Controls.Add(label17);
			panel21.Dock = System.Windows.Forms.DockStyle.Fill;
			panel21.Location = new System.Drawing.Point(1, 165);
			panel21.Margin = new System.Windows.Forms.Padding(0);
			panel21.Name = "panel21";
			panel21.Size = new System.Drawing.Size(162, 42);
			panel21.TabIndex = 21;
			label17.AutoSize = true;
			label17.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label17.ForeColor = System.Drawing.Color.White;
			label17.Location = new System.Drawing.Point(11, 9);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(150, 21);
			label17.TabIndex = 0;
			label17.Text = "現金收銀(預設全額)";
			tableLayoutPanel7.SetColumnSpan(panel24, 3);
			panel24.Controls.Add(cb_cash);
			panel24.Controls.Add(label31);
			panel24.Dock = System.Windows.Forms.DockStyle.Fill;
			panel24.Location = new System.Drawing.Point(164, 165);
			panel24.Margin = new System.Windows.Forms.Padding(0);
			panel24.Name = "panel24";
			panel24.Size = new System.Drawing.Size(802, 42);
			panel24.TabIndex = 47;
			cb_cash.AutoSize = true;
			cb_cash.Checked = true;
			cb_cash.CheckState = System.Windows.Forms.CheckState.Checked;
			cb_cash.Location = new System.Drawing.Point(196, 8);
			cb_cash.Name = "cb_cash";
			cb_cash.Size = new System.Drawing.Size(124, 28);
			cb_cash.TabIndex = 49;
			cb_cash.Text = "可列印條碼";
			cb_cash.UseVisualStyleBackColor = true;
			label31.AutoSize = true;
			label31.Location = new System.Drawing.Point(16, 9);
			label31.Name = "label31";
			label31.Size = new System.Drawing.Size(85, 24);
			label31.TabIndex = 50;
			label31.Text = "CTRL+O";
			panel23.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel23.Controls.Add(label18);
			panel23.Dock = System.Windows.Forms.DockStyle.Fill;
			panel23.Location = new System.Drawing.Point(484, 83);
			panel23.Margin = new System.Windows.Forms.Padding(0);
			panel23.Name = "panel23";
			panel23.Size = new System.Drawing.Size(162, 40);
			panel23.TabIndex = 21;
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label18.ForeColor = System.Drawing.Color.White;
			label18.Location = new System.Drawing.Point(88, 9);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(74, 21);
			label18.TabIndex = 0;
			label18.Text = "數量減一";
			panel25.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel25.Controls.Add(label19);
			panel25.Dock = System.Windows.Forms.DockStyle.Fill;
			panel25.Location = new System.Drawing.Point(484, 124);
			panel25.Margin = new System.Windows.Forms.Padding(0);
			panel25.Name = "panel25";
			panel25.Size = new System.Drawing.Size(162, 40);
			panel25.TabIndex = 21;
			label19.AutoSize = true;
			label19.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label19.ForeColor = System.Drawing.Color.White;
			label19.Location = new System.Drawing.Point(88, 9);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(74, 21);
			label19.TabIndex = 0;
			label19.Text = "下移一筆";
			label28.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label28.AutoSize = true;
			label28.Location = new System.Drawing.Point(194, 132);
			label28.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
			label28.Name = "label28";
			label28.Size = new System.Drawing.Size(29, 24);
			label28.TabIndex = 50;
			label28.Text = "↑";
			label29.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label29.AutoSize = true;
			label29.Location = new System.Drawing.Point(682, 91);
			label29.Margin = new System.Windows.Forms.Padding(35, 0, 3, 0);
			label29.Name = "label29";
			label29.Size = new System.Drawing.Size(18, 24);
			label29.TabIndex = 50;
			label29.Text = "-";
			label30.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label30.AutoSize = true;
			label30.Location = new System.Drawing.Point(677, 132);
			label30.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
			label30.Name = "label30";
			label30.Size = new System.Drawing.Size(29, 24);
			label30.TabIndex = 50;
			label30.Text = "↓";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(tabControl1);
			base.Name = "frmSystemSetup";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "frmSystemSetup";
			base.Load += new System.EventHandler(frmSystemSetup_Load);
			base.Controls.SetChildIndex(tabControl1, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			tabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tabPage1.PerformLayout();
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			panel13.ResumeLayout(false);
			panel13.PerformLayout();
			panel8.ResumeLayout(false);
			panel8.PerformLayout();
			panel12.ResumeLayout(false);
			panel12.PerformLayout();
			panel17.ResumeLayout(false);
			panel17.PerformLayout();
			panel18.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel19.ResumeLayout(false);
			panel19.PerformLayout();
			panel11.ResumeLayout(false);
			panel11.PerformLayout();
			panel9.ResumeLayout(false);
			panel9.PerformLayout();
			panel7.ResumeLayout(false);
			panel7.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel14.ResumeLayout(false);
			panel14.PerformLayout();
			panel15.ResumeLayout(false);
			panel15.PerformLayout();
			panel10.ResumeLayout(false);
			panel10.PerformLayout();
			panel16.ResumeLayout(false);
			panel16.PerformLayout();
			panel27.ResumeLayout(false);
			panel27.PerformLayout();
			tabPage2.ResumeLayout(false);
			tabPage2.PerformLayout();
			tlp_userManage.ResumeLayout(false);
			tlp_userManage.PerformLayout();
			tableLayoutPanel4.ResumeLayout(false);
			tabPage3.ResumeLayout(false);
			tableLayoutPanel10.ResumeLayout(false);
			panel28.ResumeLayout(false);
			panel28.PerformLayout();
			panel29.ResumeLayout(false);
			panel29.PerformLayout();
			panel30.ResumeLayout(false);
			panel30.PerformLayout();
			tableLayoutPanel9.ResumeLayout(false);
			panel54.ResumeLayout(false);
			panel54.PerformLayout();
			panel55.ResumeLayout(false);
			panel55.PerformLayout();
			panel56.ResumeLayout(false);
			panel56.PerformLayout();
			panel57.ResumeLayout(false);
			panel57.PerformLayout();
			panel58.ResumeLayout(false);
			panel58.PerformLayout();
			panel26.ResumeLayout(false);
			tableLayoutPanel6.ResumeLayout(false);
			panel34.ResumeLayout(false);
			panel34.PerformLayout();
			panel35.ResumeLayout(false);
			panel35.PerformLayout();
			panel36.ResumeLayout(false);
			panel36.PerformLayout();
			panel41.ResumeLayout(false);
			tableLayoutPanel5.ResumeLayout(false);
			panel40.ResumeLayout(false);
			panel40.PerformLayout();
			panel45.ResumeLayout(false);
			panel45.PerformLayout();
			panel37.ResumeLayout(false);
			panel37.PerformLayout();
			panel38.ResumeLayout(false);
			tabPage4.ResumeLayout(false);
			tableLayoutPanel11.ResumeLayout(false);
			panel31.ResumeLayout(false);
			panel31.PerformLayout();
			panel32.ResumeLayout(false);
			panel32.PerformLayout();
			panel33.ResumeLayout(false);
			panel33.PerformLayout();
			tableLayoutPanel8.ResumeLayout(false);
			panel49.ResumeLayout(false);
			panel49.PerformLayout();
			panel50.ResumeLayout(false);
			panel50.PerformLayout();
			panel51.ResumeLayout(false);
			panel51.PerformLayout();
			panel52.ResumeLayout(false);
			panel52.PerformLayout();
			panel53.ResumeLayout(false);
			panel53.PerformLayout();
			panel22.ResumeLayout(false);
			tableLayoutPanel7.ResumeLayout(false);
			tableLayoutPanel7.PerformLayout();
			panel43.ResumeLayout(false);
			panel43.PerformLayout();
			panel44.ResumeLayout(false);
			panel44.PerformLayout();
			panel46.ResumeLayout(false);
			panel46.PerformLayout();
			panel47.ResumeLayout(false);
			panel47.PerformLayout();
			panel20.ResumeLayout(false);
			panel20.PerformLayout();
			panel21.ResumeLayout(false);
			panel21.PerformLayout();
			panel24.ResumeLayout(false);
			panel24.PerformLayout();
			panel23.ResumeLayout(false);
			panel23.PerformLayout();
			panel25.ResumeLayout(false);
			panel25.PerformLayout();
			ResumeLayout(false);
		}
	}
}
