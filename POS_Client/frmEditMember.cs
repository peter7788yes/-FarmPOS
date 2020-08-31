using POS_Client.WebService;
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
	public class frmEditMember : MasterThinForm
	{
		private string vipNo = "";

		private string _idNo = "";

		private int _credit;

		private string _returnType;

		private int comboBox1SelectedIndex = 99;

		private IContainer components;

		private Label label3;

		private Label label4;

		private Button btn_back;

		private Button btn_printBarcode;

		private Panel panel2;

		private TabControl tabControl;

		private TabPage BasicData;

		private Button btn_cancel;

		private Button btn_save;

		private TableLayoutPanel tableLayoutPanel1;

		private ComboBox cb_status;

		private TextBox tb_fax;

		private TextBox tb_companyTel;

		private TextBox tb_companyIdno;

		private TextBox tb_companyName;

		private TextBox tb_email;

		private TextBox tb_name;

		private FlowLayoutPanel flowLayoutPanel4;

		private Label label20;

		private TextBox tb_tel;

		private Label label22;

		private TextBox tb_mobile;

		private Panel panel11;

		private Label label15;

		private Panel panel9;

		private Label label5;

		private Label label7;

		private Panel panel8;

		private Label label16;

		private Panel panel7;

		private Label label14;

		private Panel panel6;

		private Label label12;

		private Panel panel3;

		private Label label6;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private Panel panel4;

		private Label label8;

		private Panel panel5;

		private Label label9;

		private Label label10;

		private Panel panel10;

		private Label label11;

		private Label label13;

		private Panel panel12;

		private Label label17;

		private Panel panel13;

		private Label label18;

		private FlowLayoutPanel flowLayoutPanel1;

		private TextBox tb_licenseCode;

		private TextBox tb_vipNo;

		private FlowLayoutPanel flowLayoutPanel3;

		private TextBox tb_idno;

		private Button btn_check;

		private Label l_checkLowIncome;

		private DateTimePicker dt_birthDate;

		private Panel panel14;

		private TextBox tb_zipcode;

		private ComboBox cb_area;

		private ComboBox cb_city;

		private TextBox tb_addr;

		private ComboBox cb_type;

		private Panel panel15;

		private Label label21;

		private Label label23;

		private TabPage ConsumeRecord;

		private TableLayoutPanel tableLayoutPanel2;

		private Panel panel17;

		private Label label27;

		private Panel panel22;

		private Label label33;

		private Panel panel25;

		private Label label38;

		private Panel panel29;

		private Label label43;

		private Label label19;

		private Label l_Total;

		private Label l_BuyDate;

		private Label l_RepayDate;

		private Label l_Credit;

		private Panel panel16;

		private Button btn_repay;

		private Label label_status;

		private ComboBox comboBox1;

		private DataGridView infolist;

		private DateTimePicker spDate;

		private Button btn_SelectDate;

		private DataGridViewTextBoxColumn editdate;

		private DataGridViewTextBoxColumn sellType;

		private DataGridViewTextBoxColumn payType;

		private DataGridViewLinkColumn sellNo;

		private DataGridViewTextBoxColumn sum;

		private DataGridViewTextBoxColumn status;

		private DateTimePicker spDate2;

		private Label label49;

		private Button btn_DeleteMember;

		private TabPage Cost_Detail;

		private TableLayoutPanel tableLayoutPanel4;

		private Panel panel30;

		private Label label34;

		private Panel panel32;

		private Label label36;

		private Panel panel33;

		private Panel panel18;

		private DateTimePicker dateTimePicker1;

		private DateTimePicker dateTimePicker2;

		private Label label24;

		private Panel panel21;

		private Panel panel23;

		private TextBox tb_productname;

		private Panel panel24;

		private Label label26;

		private TextBox tb_barcode;

		private Panel panel19;

		private Panel panel20;

		private DateTimePicker dateTimePicker3;

		private DateTimePicker dateTimePicker4;

		private Label label25;

		private DataGridView dataGridView1;

		private Button btn_reset;

		private Button btn_enter;

		private Label label28;

		private DataGridViewTextBoxColumn Column1;

		private DataGridViewLinkColumn Column2;

		private DataGridViewTextBoxColumn Column3;

		private DataGridViewTextBoxColumn Column4;

		private DataGridViewTextBoxColumn Column5;

		public frmEditMember(string vipNo)
			: base("會員管理")
		{
			InitializeComponent();
			this.vipNo = vipNo;
			tb_vipNo.Text = vipNo;
		}

		public frmEditMember(string vipNo, string form)
			: base("會員管理")
		{
			InitializeComponent();
			this.vipNo = vipNo;
			tb_vipNo.Text = vipNo;
			_returnType = form;
			tabControl.SelectedIndex = 1;
		}

		private void frmEditMember_Load(object sender, EventArgs e)
		{
			DataTable dataSource = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "ADDRCITY", "", "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
			cb_city.DisplayMember = "city";
			cb_city.ValueMember = "cityno";
			cb_city.DataSource = dataSource;
			ComboboxItem[] array = new ComboboxItem[2]
			{
				new ComboboxItem("正常", "0"),
				new ComboboxItem("停用", "1")
			};
			cb_status.Items.AddRange(array);
			cb_status.SelectedIndex = 0;
			ComboboxItem[] array2 = new ComboboxItem[3]
			{
				new ComboboxItem("一般會員", "1"),
				new ComboboxItem("優惠會員(1)", "2"),
				new ComboboxItem("優惠會員(2)", "3")
			};
			cb_type.Items.AddRange(array2);
			cb_type.SelectedIndex = 0;
			spDate.Value = DateTime.Today.AddDays(-30.0);
			spDate2.Value = DateTime.Today;
			dateTimePicker3.Value = DateTime.Today.AddDays(-30.0);
			dateTimePicker4.Value = DateTime.Today;
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", "VipNo = {0}", "", null, new string[1]
			{
				vipNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			try
			{
				if (dataTable.Rows.Count > 0)
				{
					_idNo = dataTable.Rows[0]["IdNo"].ToString();
					tb_licenseCode.Text = dataTable.Rows[0]["LicenseCode"].ToString();
					tb_name.Text = dataTable.Rows[0]["Name"].ToString();
					tb_idno.Text = dataTable.Rows[0]["IdNo"].ToString();
					tb_tel.Text = dataTable.Rows[0]["Telphone"].ToString();
					tb_mobile.Text = dataTable.Rows[0]["Mobile"].ToString();
					tb_email.Text = dataTable.Rows[0]["EMail"].ToString();
					tb_companyName.Text = dataTable.Rows[0]["CompanyName"].ToString();
					tb_companyTel.Text = dataTable.Rows[0]["CompanyTel"].ToString();
					tb_fax.Text = dataTable.Rows[0]["Fax"].ToString();
					tb_companyIdno.Text = dataTable.Rows[0]["CompanyIdNo"].ToString();
					if (!"".Equals(dataTable.Rows[0]["City"].ToString()))
					{
						cb_city.SelectedValue = dataTable.Rows[0]["City"].ToString();
						cb_city_SelectedIndexChanged(sender, e);
					}
					else
					{
						cb_city.SelectedValue = 0;
					}
					if (!"".Equals(dataTable.Rows[0]["Area"].ToString()))
					{
						cb_area.SelectedValue = dataTable.Rows[0]["Area"].ToString();
						cb_area_SelectedIndexChanged(sender, e);
					}
					else
					{
						cb_area.SelectedValue = 0;
					}
					tb_addr.Text = dataTable.Rows[0]["Address"].ToString();
					ComboboxItem[] array3 = array;
					foreach (ComboboxItem comboboxItem in array3)
					{
						if (comboboxItem.Value.Equals(dataTable.Rows[0]["Status"].ToString()))
						{
							cb_status.SelectedItem = comboboxItem;
						}
					}
					array3 = array2;
					foreach (ComboboxItem comboboxItem2 in array3)
					{
						if (comboboxItem2.Value.Equals(dataTable.Rows[0]["Type"].ToString()))
						{
							cb_type.SelectedItem = comboboxItem2;
						}
					}
					cb_type.SelectedValue = dataTable.Rows[0]["Type"].ToString();
					string text = dataTable.Rows[0]["BirthDate"].ToString();
					string value = "-";
					if (text.Length >= 8)
					{
						if (text.Length == 8)
						{
							if (text.IndexOf(value) > -1)
							{
								dt_birthDate.Text = dataTable.Rows[0]["BirthDate"].ToString();
							}
							else
							{
								string text2 = text.Insert(4, "-").Insert(7, "-");
								dt_birthDate.Text = text2;
							}
						}
						else
						{
							dt_birthDate.Text = dataTable.Rows[0]["BirthDate"].ToString();
						}
					}
					else
					{
						string text3 = text.Insert(4, "-").Insert(7, "-");
						dt_birthDate.Text = text3;
					}
				}
				else
				{
					MessageBox.Show("會員號碼錯誤!");
					backToPreviousForm();
				}
				if (Program.SystemMode == 1)
				{
					l_checkLowIncome.Visible = false;
					btn_check.Visible = false;
					changeMoneyAndDate(dataTable);
					ComboboxItem[] items = new ComboboxItem[2]
					{
						new ComboboxItem("銷售", "0"),
						new ComboboxItem("退貨", "1")
					};
					comboBox1.Items.AddRange(items);
					comboBox1.SelectedIndex = 0;
					btn_repay.Visible = false;
				}
				else
				{
					changeMoneyAndDate(dataTable);
					ComboboxItem[] items2 = new ComboboxItem[4]
					{
						new ComboboxItem("全部", "All"),
						new ComboboxItem("銷售", "0"),
						new ComboboxItem("退貨", "1"),
						new ComboboxItem("賒帳歸還", "2")
					};
					comboBox1.Items.AddRange(items2);
					comboBox1.SelectedIndex = 1;
				}
			}
			catch (Exception)
			{
				MessageBox.Show("會員資料異常，請回報廠商支援!");
				backToPreviousForm();
			}
			if (Program.SystemMode == 1)
			{
				dataGridView1.Columns["Column3"].Visible = false;
				dataGridView1.Columns["Column5"].Visible = false;
			}
			tabledataGridView1();
		}

		private void tableConsumeRecord(int recordIndex, string spDate, string spDate2)
		{
			try
			{
				infolist.Rows.Clear();
				infolist.Refresh();
				infolist.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 13f, FontStyle.Regular);
				infolist.DefaultCellStyle.Font = new Font("微軟正黑體", 13f, FontStyle.Regular);
				if (Program.SystemMode == 1)
				{
					infolist.Columns["payType"].Visible = false;
					infolist.Columns["sum"].Visible = false;
				}
				string text = "";
				string text2 = "";
				if (!"".Equals(spDate))
				{
					text = spDate + " 00:00:00";
					text2 = spDate2 + " 23:59:59";
				}
				else
				{
					DateTime now = DateTime.Now;
					text = now.AddDays(-30.0).ToString("yyyy-MM-dd 00:00:00");
					text2 = now.ToString("yyyy-MM-dd 23:59:59");
				}
				string str = "SELECT * FROM hypos_user_consumelog WHERE memberId ={0} AND editdate >={1} AND editdate <={2}";
				if (Program.SystemMode == 1)
				{
					switch (recordIndex)
					{
					case 1:
						str += " and sellType ='0'";
						break;
					case 2:
						str += " and sellType ='1'";
						break;
					}
					str += " and sellType !='2' ";
				}
				else
				{
					switch (recordIndex)
					{
					case 1:
						str += " and sellType ='0'";
						break;
					case 2:
						str += " and sellType ='1'";
						break;
					case 3:
						str += " and sellType ='2'";
						break;
					}
				}
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, str + " ORDER BY editdate desc", new string[3]
				{
					vipNo,
					text,
					text2
				}, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count <= 0)
				{
					return;
				}
				List<string> list = new List<string>();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					list.Add(dataTable.Rows[i]["editdate"].ToString());
				}
				string text3 = "changeDate in (";
				for (int j = 0; j < list.Count; j++)
				{
					text3 = text3 + "{" + j + "},";
				}
				text3 = text3.Substring(0, text3.Length - 1) + ")";
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "changeDate,sum", "hypos_mainsell_log", text3, "", null, list.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				if (dataTable2.Rows.Count > 0)
				{
					for (int k = 0; k < dataTable2.Rows.Count; k++)
					{
						if (!dictionary.ContainsKey(dataTable2.Rows[k]["changeDate"].ToString()))
						{
							dictionary.Add(dataTable2.Rows[k]["changeDate"].ToString(), dataTable2.Rows[k]["sum"].ToString());
						}
					}
				}
				for (int l = 0; l < dataTable.Rows.Count; l++)
				{
					string text4 = dataTable.Rows[l]["sellNo"].ToString();
					string text5 = dataTable.Rows[l]["editdate"].ToString();
					string text6 = "";
					switch (dataTable.Rows[l]["status"].ToString())
					{
					case "0":
						text6 = "正常";
						break;
					case "1":
						text6 = "取消";
						break;
					case "2":
						text6 = "正常(變更)";
						break;
					}
					string text7 = "";
					switch (dataTable.Rows[l]["sellType"].ToString())
					{
					case "0":
						text7 = "銷售(購物)";
						break;
					case "1":
						text7 = "退貨(含取消)";
						break;
					case "2":
						text7 = "賒帳還款";
						break;
					}
					string text8 = "";
					string text9 = dataTable.Rows[l]["Cash"].ToString();
					string text10 = dataTable.Rows[l]["Credit"].ToString();
					if ("賒帳還款".Equals(text7))
					{
						if (!string.IsNullOrEmpty(text9) && !string.IsNullOrEmpty(text10))
						{
							text8 = ((!"正常".Equals(text6)) ? ("退貨還款(" + string.Format("{0:n0}", int.Parse(text10)) + ")") : ("現金還款(" + string.Format("{0:n0}", int.Parse(text10)) + ")"));
							text8 += ((int.Parse(text9) > 0) ? ("/找零(" + string.Format("{0:n0}", int.Parse(text9)) + ")") : "");
						}
					}
					else if ("銷售(購物)".Equals(text7))
					{
						if (!string.IsNullOrEmpty(text9) && !string.IsNullOrEmpty(text10))
						{
							DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "sum", "hypos_mainsell_log", "sellNo={0}", "", null, new string[1]
							{
								dataTable.Rows[l]["sellNo"].ToString()
							}, CommandOperationType.ExecuteReaderReturnDataTable);
							int num = int.Parse(text9) - int.Parse(dataTable3.Rows[0]["sum"].ToString());
							if (num <= 0)
							{
								num = 0;
							}
							text8 = "現金(" + string.Format("{0:n0}", int.Parse(text9)) + ")";
							text8 += ((int.Parse(text10) > 0) ? ("/賒帳(" + string.Format("{0:n0}", int.Parse(text10)) + ")") : "");
							text8 += ((num > 0) ? ("/找零(" + num + ")") : "");
						}
					}
					else if ("退貨(含取消)".Equals(text7) && !string.IsNullOrEmpty(text9) && !string.IsNullOrEmpty(text10))
					{
						text8 = "現金退款(" + string.Format("{0:n0}", int.Parse(text9)) + ")";
					}
					string text11 = "";
					if (!string.IsNullOrEmpty(text4))
					{
						try
						{
							text11 = string.Format("{0:n0}", int.Parse(dictionary[text5]));
						}
						catch (Exception)
						{
							text4 = "";
							text11 = "";
						}
					}
					infolist.Rows.Add(text5, text7, text8, text4, text11, text6);
				}
				foreach (DataGridViewRow item in (IEnumerable)infolist.Rows)
				{
					item.Height = 35;
				}
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message);
			}
		}

		private void checkSell(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 3 && e.RowIndex >= 0)
			{
				string sellno = infolist[e.ColumnIndex, e.RowIndex].Value.ToString();
				if (Program.SystemMode == 1)
				{
					frmMainShopSimpleReturn frmMainShopSimpleReturn = new frmMainShopSimpleReturn(sellno, "frmEditMember", "");
					frmMainShopSimpleReturn.frmName = base.Name;
					frmMainShopSimpleReturn.Location = new Point(base.Location.X, base.Location.Y);
					frmMainShopSimpleReturn.Show();
					frmMainShopSimpleReturn.Focus();
				}
				else
				{
					frmMainShopSimpleReturnWithMoney frmMainShopSimpleReturnWithMoney = new frmMainShopSimpleReturnWithMoney(sellno, "frmEditMember", "");
					frmMainShopSimpleReturnWithMoney.frmName = base.Name;
					frmMainShopSimpleReturnWithMoney.Location = new Point(base.Location.X, base.Location.Y);
					frmMainShopSimpleReturnWithMoney.Show();
					frmMainShopSimpleReturnWithMoney.Focus();
				}
			}
		}

		private void btn_back_Click(object sender, EventArgs e)
		{
			if ("frmDialogMember".Equals(_returnType))
			{
				Dispose();
				Close();
			}
			else
			{
				switchForm(new frmMemberMangement());
			}
		}

		private void btn_cancel_Click(object sender, EventArgs e)
		{
			switchForm(new frmMemberMangement());
		}

		private void btn_save_Click(object sender, EventArgs e)
		{
			string text = "";
			if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "IdNo", "hypos_CUST_RTL", "IdNo ={0}", "", null, new string[1]
			{
				tb_idno.Text.Trim()
			}, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0 && !"".Equals(tb_idno.Text.Trim()) && !_idNo.Equals(tb_idno.Text.Trim()))
			{
				AutoClosingMessageBox.Show("此身分證已使用");
				return;
			}
			if (cb_city.SelectedValue == null)
			{
				text += "請輸入會員所在城市\n";
			}
			if (cb_area.SelectedValue == null)
			{
				text += "請輸入會員所在區域\n";
			}
			if (!text.Equals(""))
			{
				AutoClosingMessageBox.Show(text);
				return;
			}
			string[,] strFieldArray = new string[17, 2]
			{
				{
					"LicenseCode",
					tb_licenseCode.Text
				},
				{
					"IdNo",
					tb_idno.Text.Trim()
				},
				{
					"Name",
					tb_name.Text
				},
				{
					"BirthDate",
					dt_birthDate.Text
				},
				{
					"Telphone",
					tb_tel.Text
				},
				{
					"Mobile",
					tb_mobile.Text
				},
				{
					"Type",
					(cb_type.SelectedItem as ComboboxItem).Value.ToString()
				},
				{
					"City",
					cb_city.SelectedValue.ToString()
				},
				{
					"Area",
					cb_area.SelectedValue.ToString()
				},
				{
					"Address",
					tb_addr.Text
				},
				{
					"EMail",
					tb_email.Text
				},
				{
					"UpdateDate",
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
				},
				{
					"CompanyIdNo",
					tb_companyIdno.Text
				},
				{
					"CompanyName",
					tb_companyName.Text
				},
				{
					"CompanyTel",
					tb_companyTel.Text
				},
				{
					"Status",
					(cb_status.SelectedItem as ComboboxItem).Value.ToString()
				},
				{
					"Fax",
					tb_fax.Text
				}
			};
			DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Update, "", "hypos_CUST_RTL", "VipNo = {0}", "", strFieldArray, new string[1]
			{
				vipNo
			}, CommandOperationType.ExecuteNonQuery);
			AutoClosingMessageBox.Show("會員修改完成");
			backToPreviousForm();
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

		private void btn_printBarcode_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			list.Add(vipNo);
			new Member_barcode(list, 3).ShowDialog();
		}

		private void tlp_consumeRecord_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			if (e.Row == 0)
			{
				using (SolidBrush brush = new SolidBrush(Color.FromArgb(102, 102, 102)))
				{
					e.Graphics.FillRectangle(brush, e.CellBounds);
				}
			}
		}

		private void btn_repay_Click(object sender, EventArgs e)
		{
			new frmRepayDialog(_credit, vipNo, this).ShowDialog();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int num = comboBox1.SelectedIndex;
			if (Program.SystemMode == 1)
			{
				num++;
			}
			if (comboBox1SelectedIndex == 99 || comboBox1SelectedIndex != num)
			{
				comboBox1SelectedIndex = num;
				tableConsumeRecord(num, spDate.Text.ToString(), spDate2.Text.ToString());
			}
		}

		private void changeMoneyAndDate(DataTable dt_member)
		{
			if (Program.SystemMode == 1)
			{
				l_BuyDate.Text = dt_member.Rows[0]["BuyDate"].ToString();
				l_RepayDate.Text = "";
				l_Credit.Text = "";
				l_Total.Text = "";
				return;
			}
			l_Total.Text = (string.IsNullOrEmpty(dt_member.Rows[0]["Total"].ToString()) ? "0" : string.Format("{0:n0}", int.Parse(dt_member.Rows[0]["Total"].ToString())));
			if (string.IsNullOrEmpty(dt_member.Rows[0]["Credit"].ToString()))
			{
				l_Credit.Text = "0";
			}
			else
			{
				int num = int.Parse(dt_member.Rows[0]["Credit"].ToString());
				l_Credit.Text = string.Format("{0:n0}", num);
				_credit = num;
			}
			if ("0".Equals(l_Credit.Text))
			{
				btn_repay.Visible = false;
			}
			l_BuyDate.Text = dt_member.Rows[0]["BuyDate"].ToString();
			l_RepayDate.Text = dt_member.Rows[0]["RepayDate"].ToString();
		}

		public void repayChange()
		{
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", "VipNo = {0}", "", null, new string[1]
			{
				vipNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0)
			{
				changeMoneyAndDate(dataTable);
			}
			tableConsumeRecord(3, "", "");
			comboBox1.SelectedIndex = 3;
		}

		private void btn_check_Click(object sender, EventArgs e)
		{
			int dBVersion = Program.GetDBVersion();
			string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			if (dBVersion == 0)
			{
				AutoClosingMessageBox.Show(new UploadVerification().farmerInfo(tb_idno.Text));
			}
			else
			{
				if (dBVersion < 1 || !Program.IsFertilizer)
				{
					return;
				}
				string text2 = new UploadVerification().farmerInfo(tb_idno.Text);
				if (tb_idno.Text.Equals(""))
				{
					AutoClosingMessageBox.Show("請輸入身分證字號");
					return;
				}
				if (tb_idno.Text.Length != 10 || !Verification.checkIDNo(tb_idno.Text))
				{
					AutoClosingMessageBox.Show("身分證格式錯誤，請檢查輸入值");
					return;
				}
				string[] strParameterArray = new string[4]
				{
					tb_name.Text,
					tb_idno.Text,
					text,
					vipNo
				};
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT * FROM hypos_CUST_RTL ", null, CommandOperationType.ExecuteReaderReturnDataTable);
				if (text2.Equals("符合補助資格"))
				{
					if (dataTable.Rows.Count > 0)
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Verification = 'Y', IdNo = {1}, LastVerificationTime = {2} WHERE VipNo = {3}", strParameterArray, CommandOperationType.ExecuteNonQuery);
					}
					else
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_CUST_RTL ( Name, IdNo, Verification, LastVerificationTime) VALUES( {0}, {1}, 'Y', {2}) ", strParameterArray, CommandOperationType.ExecuteNonQuery);
					}
					AutoClosingMessageBox.Show("驗證成功。");
				}
				else if (text2.Equals("購肥帳號密碼驗證錯誤"))
				{
					AutoClosingMessageBox.Show("帳號密碼有誤，請重新確認您的帳號密碼。");
					if (dataTable.Rows.Count > 0)
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Verification = 'N', IdNo = {1}, LastVerificationTime = {2} WHERE VipNo = {3} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
					}
					else
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_CUST_RTL ( Name, IdNo, Verification, LastVerificationTime) VALUES( {0}, {1}, 'N', {2}) ", strParameterArray, CommandOperationType.ExecuteNonQuery);
					}
				}
				else if (text2.Equals("不符合補助資格"))
				{
					AutoClosingMessageBox.Show("不符合補助資格");
					if (dataTable.Rows.Count > 0)
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_CUST_RTL SET Verification = 'N', IdNo = {1}, LastVerificationTime = {2} WHERE VipNo = {3} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
					}
					else
					{
						DataBaseUtilities.DBOperation(Program.ConnectionString, "INSERT INTO hypos_CUST_RTL ( Name, IdNo, Verification, LastVerificationTime) VALUES( {0}, {1}, 'N', {2}) ", strParameterArray, CommandOperationType.ExecuteNonQuery);
					}
				}
			}
		}

		private void specifyTheDate_Click(object sender, EventArgs e)
		{
			if (spDate.Checked)
			{
				tableConsumeRecord(0, spDate.Text.ToString(), spDate2.Text.ToString());
			}
		}

		private void tb_idno_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals('\b') && !char.IsUpper(e.KeyChar));
		}

		private void spDate_ValueChanged(object sender, EventArgs e)
		{
			int selectedIndex = comboBox1.SelectedIndex;
			if (Program.SystemMode == 1)
			{
				selectedIndex++;
				tableConsumeRecord(selectedIndex, spDate.Text.ToString(), spDate2.Text.ToString());
			}
			else
			{
				tableConsumeRecord(comboBox1.SelectedIndex, spDate.Text.ToString(), spDate2.Text.ToString());
			}
		}

		private void spDate2_ValueChanged(object sender, EventArgs e)
		{
			int selectedIndex = comboBox1.SelectedIndex;
			if (Program.SystemMode == 1)
			{
				selectedIndex++;
				tableConsumeRecord(selectedIndex, spDate.Text.ToString(), spDate2.Text.ToString());
			}
			else
			{
				tableConsumeRecord(comboBox1.SelectedIndex, spDate.Text.ToString(), spDate2.Text.ToString());
			}
		}

		private void btn_DeleteMember_Click(object sender, EventArgs e)
		{
			int num = 0;
			string text = "1999-01-01";
			string str = DateTime.Now.ToString("D");
			try
			{
				infolist.Rows.Clear();
				infolist.Refresh();
				infolist.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 13f, FontStyle.Regular);
				infolist.DefaultCellStyle.Font = new Font("微軟正黑體", 13f, FontStyle.Regular);
				string text2 = "";
				string text3 = "";
				if (!"".Equals(text))
				{
					text2 = text + " 00:00:00";
					text3 = str + " 23:59:59";
				}
				else
				{
					DateTime now = DateTime.Now;
					text2 = now.AddDays(-7.0).ToString("yyyy-MM-dd 00:00:00");
					text2 = now.AddDays(-30.0).ToString("yyyy-MM-dd 00:00:00");
					text3 = now.ToString("yyyy-MM-dd 23:59:59");
				}
				string str2 = "SELECT * FROM hypos_user_consumelog WHERE memberId ={0} AND editdate >={1} AND editdate <={2}";
				switch (num)
				{
				case 1:
					str2 += " and sellType ='0'";
					break;
				case 2:
					str2 += " and sellType ='1'";
					break;
				case 3:
					str2 += " and sellType ='2'";
					break;
				}
				if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, str2 + " ORDER BY editdate desc", new string[3]
				{
					vipNo,
					text2,
					text3
				}, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
				{
					MessageBox.Show("會員已有消費紀錄，不可刪除");
					return;
				}
				DialogResult dialogResult = MessageBox.Show("確定刪除?", "會員號: " + vipNo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					string sql = "DELETE FROM hypos_CUST_RTL WHERE VipNo ={0}";
					DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, new string[1]
					{
						vipNo
					}, CommandOperationType.ExecuteReaderReturnDataTable);
					switchForm(new frmMemberMangement());
				}
				else
				{
					int num2 = 7;
				}
			}
			catch (Exception)
			{
			}
		}

		private void btn_enter_Click(object sender, EventArgs e)
		{
			try
			{
				dataGridView1.Rows.Clear();
				dataGridView1.Refresh();
				dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 13f, FontStyle.Regular);
				dataGridView1.DefaultCellStyle.Font = new Font("微軟正黑體", 13f, FontStyle.Regular);
				string text = " select hms.sellNo,hms.sellTime,hcr.Name, hypos_GOODSLST.barcode ,hypos_GOODSLST.GDName, hypos_GOODSLST.CName, hypos_GOODSLST.formCode, hypos_GOODSLST.contents, hypos_GOODSLST.brandName ,hypos_GOODSLST.spec,hypos_GOODSLST.capacity,hypos_GOODSLST.Cost,hypos_GOODSLST.Price ,hypos_detail_sell.num, hypos_detail_sell.discount ,hypos_GOODSLST.SpecialPrice1,hypos_GOODSLST.SpecialPrice2 ,hcr.Type , hypos_detail_sell.sellingPrice as 'HDS_SellPrice', hypos_detail_sell.num as 'HDS_num' , hypos_detail_sell.total as 'HDS_total'  FROM hypos_main_sell as hms  left outer join hypos_CUST_RTL as hcr on hms.memberId= hcr.VipNo join hypos_detail_sell on hms.sellNo = hypos_detail_sell.sellNo join hypos_GOODSLST on hypos_detail_sell.barcode = hypos_GOODSLST.barcode WHERE 1=1  and name = '" + tb_name.Text.ToString() + "'";
				if (tb_barcode.Text != "")
				{
					text = text + " AND  hypos_detail_sell.barcode ='" + tb_barcode.Text.ToString().Trim() + "'";
				}
				if (tb_productname.Text != "")
				{
					text = text + " AND hypos_GOODSLST.GDName  ='" + tb_productname.Text.ToString() + "'";
				}
				if (dateTimePicker3.Checked)
				{
					string str = dateTimePicker3.Text.ToString();
					str += " 00:00:00";
					text = text + " AND hms.sellTime >= '" + str + "'";
				}
				if (dateTimePicker4.Checked)
				{
					string str2 = dateTimePicker4.Text.ToString();
					str2 += " 23:59:59";
					text = text + " AND hms.sellTime <= '" + str2 + "'";
				}
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text, new string[0], CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						dataGridView1.Rows.Add(dataTable.Rows[i]["sellTime"].ToString(), dataTable.Rows[i]["barcode"].ToString() + " " + dataTable.Rows[i]["GDName"].ToString() + "[" + dataTable.Rows[i]["CName"].ToString() + "-" + dataTable.Rows[i]["formCode"].ToString() + " . " + dataTable.Rows[i]["contents"].ToString() + "-" + dataTable.Rows[i]["brandName"].ToString() + "](" + dataTable.Rows[i]["spec"].ToString() + " " + dataTable.Rows[i]["capacity"].ToString() + ")", dataTable.Rows[i]["HDS_SellPrice"].ToString(), dataTable.Rows[i]["HDS_num"].ToString(), dataTable.Rows[i]["HDS_total"].ToString());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_reset_Click(object sender, EventArgs e)
		{
			tb_barcode.Text = "";
			tb_productname.Text = "";
			dateTimePicker3.Value = DateTime.Today.AddDays(-30.0);
			dateTimePicker4.Value = DateTime.Today;
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != 1 || e.RowIndex < 0)
			{
				return;
			}
			dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
			int rowIndex = e.RowIndex;
			try
			{
				string text = " select hms.sellNo,hms.sellTime,hcr.Name, hypos_GOODSLST.barcode ,hypos_GOODSLST.GDName, hypos_GOODSLST.CName, hypos_GOODSLST.formCode, hypos_GOODSLST.contents, hypos_GOODSLST.brandName ,hypos_GOODSLST.spec,hypos_GOODSLST.capacity,hypos_GOODSLST.Cost,hypos_GOODSLST.Price ,hypos_detail_sell.num, hypos_detail_sell.discount ,hypos_GOODSLST.SpecialPrice1,hypos_GOODSLST.SpecialPrice2 ,hcr.Type  FROM hypos_main_sell as hms  left outer join hypos_CUST_RTL as hcr on hms.memberId= hcr.VipNo join hypos_detail_sell on hms.sellNo = hypos_detail_sell.sellNo join hypos_GOODSLST on hypos_detail_sell.barcode = hypos_GOODSLST.barcode WHERE 1=1  and name = '" + tb_name.Text.ToString() + "'";
				if (tb_barcode.Text != "")
				{
					text = text + " AND  hypos_detail_sell.barcode ='" + tb_barcode.Text.ToString().Trim() + "'";
				}
				if (tb_productname.Text != "")
				{
					text = text + " AND hypos_GOODSLST.GDName  ='" + tb_productname.Text.ToString() + "'";
				}
				if (dateTimePicker3.Checked)
				{
					string str = dateTimePicker3.Text.ToString();
					str += " 00:00:00";
					text = text + " AND hms.sellTime >= '" + str + "'";
				}
				if (dateTimePicker4.Checked)
				{
					string str2 = dateTimePicker4.Text.ToString();
					str2 += " 23:59:59";
					text = text + " AND hms.sellTime <= '" + str2 + "'";
				}
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, text, new string[0], CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					string sellno = dataTable.Rows[rowIndex]["sellNo"].ToString();
					if (Program.SystemMode == 1)
					{
						frmMainShopSimpleReturn frmMainShopSimpleReturn = new frmMainShopSimpleReturn(sellno, "frmEditMember", "");
						frmMainShopSimpleReturn.frmName = base.Name;
						frmMainShopSimpleReturn.Location = new Point(base.Location.X, base.Location.Y);
						frmMainShopSimpleReturn.Show();
						frmMainShopSimpleReturn.Focus();
					}
					else
					{
						frmMainShopSimpleReturnWithMoney frmMainShopSimpleReturnWithMoney = new frmMainShopSimpleReturnWithMoney(sellno, "frmEditMember", "");
						frmMainShopSimpleReturnWithMoney.frmName = base.Name;
						frmMainShopSimpleReturnWithMoney.Location = new Point(base.Location.X, base.Location.Y);
						frmMainShopSimpleReturnWithMoney.Show();
						frmMainShopSimpleReturnWithMoney.Focus();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void tabledataGridView1()
		{
			try
			{
				dataGridView1.Rows.Clear();
				dataGridView1.Refresh();
				dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微軟正黑體", 13f, FontStyle.Regular);
				dataGridView1.DefaultCellStyle.Font = new Font("微軟正黑體", 13f, FontStyle.Regular);
				string str = " select hms.sellNo,hms.sellTime,hcr.Name, hypos_GOODSLST.barcode ,hypos_GOODSLST.GDName, hypos_GOODSLST.CName, hypos_GOODSLST.formCode, hypos_GOODSLST.contents, hypos_GOODSLST.brandName ,hypos_GOODSLST.spec,hypos_GOODSLST.capacity,hypos_GOODSLST.Cost,hypos_GOODSLST.Price ,hypos_detail_sell.num, hypos_detail_sell.discount ,hypos_GOODSLST.SpecialPrice1,hypos_GOODSLST.SpecialPrice2 ,hcr.Type , hypos_detail_sell.sellingPrice as 'HDS_SellPrice', hypos_detail_sell.num as 'HDS_num' , hypos_detail_sell.total as 'HDS_total'  FROM hypos_main_sell as hms  left outer join hypos_CUST_RTL as hcr on hms.memberId= hcr.VipNo join hypos_detail_sell on hms.sellNo = hypos_detail_sell.sellNo join hypos_GOODSLST on hypos_detail_sell.barcode = hypos_GOODSLST.barcode WHERE 1=1  and name = '" + tb_name.Text.ToString() + "'";
				DateTime now = DateTime.Now;
				string str2 = now.AddDays(-30.0).ToString("yyyy-MM-dd 00:00:00");
				str = str + " AND hms.sellTime >= '" + str2 + "'";
				string str3 = now.ToString("yyyy-MM-dd 23:59:59");
				str = str + " AND hms.sellTime <= '" + str3 + "'";
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, str, new string[0], CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable.Rows.Count > 0)
				{
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						dataGridView1.Rows.Add(dataTable.Rows[i]["sellTime"].ToString(), dataTable.Rows[i]["barcode"].ToString() + " " + dataTable.Rows[i]["GDName"].ToString() + "[" + dataTable.Rows[i]["CName"].ToString() + "-" + dataTable.Rows[i]["formCode"].ToString() + " . " + dataTable.Rows[i]["contents"].ToString() + "-" + dataTable.Rows[i]["brandName"].ToString() + "](" + dataTable.Rows[i]["spec"].ToString() + " " + dataTable.Rows[i]["capacity"].ToString() + ")", dataTable.Rows[i]["HDS_SellPrice"].ToString(), dataTable.Rows[i]["HDS_num"].ToString(), dataTable.Rows[i]["HDS_total"].ToString());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			panel2 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			btn_back = new System.Windows.Forms.Button();
			btn_printBarcode = new System.Windows.Forms.Button();
			tabControl = new System.Windows.Forms.TabControl();
			BasicData = new System.Windows.Forms.TabPage();
			btn_DeleteMember = new System.Windows.Forms.Button();
			btn_cancel = new System.Windows.Forms.Button();
			btn_save = new System.Windows.Forms.Button();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			cb_status = new System.Windows.Forms.ComboBox();
			tb_fax = new System.Windows.Forms.TextBox();
			tb_companyTel = new System.Windows.Forms.TextBox();
			tb_companyIdno = new System.Windows.Forms.TextBox();
			tb_companyName = new System.Windows.Forms.TextBox();
			tb_email = new System.Windows.Forms.TextBox();
			tb_name = new System.Windows.Forms.TextBox();
			flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			label20 = new System.Windows.Forms.Label();
			tb_tel = new System.Windows.Forms.TextBox();
			label22 = new System.Windows.Forms.Label();
			tb_mobile = new System.Windows.Forms.TextBox();
			panel11 = new System.Windows.Forms.Panel();
			label15 = new System.Windows.Forms.Label();
			panel9 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			panel8 = new System.Windows.Forms.Panel();
			label16 = new System.Windows.Forms.Label();
			panel7 = new System.Windows.Forms.Panel();
			label14 = new System.Windows.Forms.Label();
			panel6 = new System.Windows.Forms.Panel();
			label12 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			panel10 = new System.Windows.Forms.Panel();
			label11 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			panel12 = new System.Windows.Forms.Panel();
			label17 = new System.Windows.Forms.Label();
			panel13 = new System.Windows.Forms.Panel();
			label18 = new System.Windows.Forms.Label();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			tb_licenseCode = new System.Windows.Forms.TextBox();
			tb_vipNo = new System.Windows.Forms.TextBox();
			flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			tb_idno = new System.Windows.Forms.TextBox();
			btn_check = new System.Windows.Forms.Button();
			l_checkLowIncome = new System.Windows.Forms.Label();
			dt_birthDate = new System.Windows.Forms.DateTimePicker();
			panel14 = new System.Windows.Forms.Panel();
			tb_zipcode = new System.Windows.Forms.TextBox();
			cb_area = new System.Windows.Forms.ComboBox();
			cb_city = new System.Windows.Forms.ComboBox();
			tb_addr = new System.Windows.Forms.TextBox();
			cb_type = new System.Windows.Forms.ComboBox();
			panel15 = new System.Windows.Forms.Panel();
			label21 = new System.Windows.Forms.Label();
			label23 = new System.Windows.Forms.Label();
			ConsumeRecord = new System.Windows.Forms.TabPage();
			spDate = new System.Windows.Forms.DateTimePicker();
			spDate2 = new System.Windows.Forms.DateTimePicker();
			label49 = new System.Windows.Forms.Label();
			btn_SelectDate = new System.Windows.Forms.Button();
			comboBox1 = new System.Windows.Forms.ComboBox();
			label_status = new System.Windows.Forms.Label();
			label19 = new System.Windows.Forms.Label();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			panel17 = new System.Windows.Forms.Panel();
			label27 = new System.Windows.Forms.Label();
			panel22 = new System.Windows.Forms.Panel();
			label33 = new System.Windows.Forms.Label();
			panel25 = new System.Windows.Forms.Panel();
			label38 = new System.Windows.Forms.Label();
			panel29 = new System.Windows.Forms.Panel();
			label43 = new System.Windows.Forms.Label();
			l_Total = new System.Windows.Forms.Label();
			l_BuyDate = new System.Windows.Forms.Label();
			l_RepayDate = new System.Windows.Forms.Label();
			panel16 = new System.Windows.Forms.Panel();
			btn_repay = new System.Windows.Forms.Button();
			l_Credit = new System.Windows.Forms.Label();
			infolist = new System.Windows.Forms.DataGridView();
			editdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			sellType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			payType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			sellNo = new System.Windows.Forms.DataGridViewLinkColumn();
			sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
			status = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Cost_Detail = new System.Windows.Forms.TabPage();
			label28 = new System.Windows.Forms.Label();
			dataGridView1 = new System.Windows.Forms.DataGridView();
			Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column2 = new System.Windows.Forms.DataGridViewLinkColumn();
			Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			btn_reset = new System.Windows.Forms.Button();
			btn_enter = new System.Windows.Forms.Button();
			tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			panel21 = new System.Windows.Forms.Panel();
			panel23 = new System.Windows.Forms.Panel();
			tb_productname = new System.Windows.Forms.TextBox();
			panel24 = new System.Windows.Forms.Panel();
			label26 = new System.Windows.Forms.Label();
			tb_barcode = new System.Windows.Forms.TextBox();
			panel30 = new System.Windows.Forms.Panel();
			label34 = new System.Windows.Forms.Label();
			panel32 = new System.Windows.Forms.Panel();
			label36 = new System.Windows.Forms.Label();
			panel33 = new System.Windows.Forms.Panel();
			panel19 = new System.Windows.Forms.Panel();
			panel20 = new System.Windows.Forms.Panel();
			dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
			dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
			label25 = new System.Windows.Forms.Label();
			panel18 = new System.Windows.Forms.Panel();
			dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			label24 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			panel2.SuspendLayout();
			tabControl.SuspendLayout();
			BasicData.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			flowLayoutPanel4.SuspendLayout();
			panel11.SuspendLayout();
			panel9.SuspendLayout();
			panel8.SuspendLayout();
			panel7.SuspendLayout();
			panel6.SuspendLayout();
			panel3.SuspendLayout();
			panel1.SuspendLayout();
			panel4.SuspendLayout();
			panel5.SuspendLayout();
			panel10.SuspendLayout();
			panel12.SuspendLayout();
			panel13.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			flowLayoutPanel3.SuspendLayout();
			panel14.SuspendLayout();
			panel15.SuspendLayout();
			ConsumeRecord.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			panel17.SuspendLayout();
			panel22.SuspendLayout();
			panel25.SuspendLayout();
			panel29.SuspendLayout();
			panel16.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)infolist).BeginInit();
			Cost_Detail.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			tableLayoutPanel4.SuspendLayout();
			panel21.SuspendLayout();
			panel23.SuspendLayout();
			panel24.SuspendLayout();
			panel30.SuspendLayout();
			panel32.SuspendLayout();
			panel33.SuspendLayout();
			panel19.SuspendLayout();
			panel20.SuspendLayout();
			panel18.SuspendLayout();
			SuspendLayout();
			panel2.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel2.Controls.Add(label3);
			panel2.Controls.Add(label4);
			panel2.Location = new System.Drawing.Point(1, 60);
			panel2.Margin = new System.Windows.Forms.Padding(0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(156, 58);
			panel2.TabIndex = 20;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.Red;
			label3.Location = new System.Drawing.Point(67, 24);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(17, 21);
			label3.TabIndex = 1;
			label3.Text = "*";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.White;
			label4.Location = new System.Drawing.Point(80, 24);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(74, 21);
			label4.TabIndex = 0;
			label4.Text = "會員姓名";
			btn_back.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_back.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_back.ForeColor = System.Drawing.Color.White;
			btn_back.Location = new System.Drawing.Point(890, 39);
			btn_back.Name = "btn_back";
			btn_back.Size = new System.Drawing.Size(77, 28);
			btn_back.TabIndex = 3;
			btn_back.Text = "返回前頁";
			btn_back.UseVisualStyleBackColor = false;
			btn_back.Click += new System.EventHandler(btn_back_Click);
			btn_printBarcode.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_printBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_printBarcode.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_printBarcode.ForeColor = System.Drawing.Color.White;
			btn_printBarcode.Location = new System.Drawing.Point(779, 39);
			btn_printBarcode.Name = "btn_printBarcode";
			btn_printBarcode.Size = new System.Drawing.Size(102, 28);
			btn_printBarcode.TabIndex = 35;
			btn_printBarcode.Text = "列印會員條碼";
			btn_printBarcode.UseVisualStyleBackColor = false;
			btn_printBarcode.Click += new System.EventHandler(btn_printBarcode_Click);
			tabControl.Controls.Add(BasicData);
			tabControl.Controls.Add(ConsumeRecord);
			tabControl.Controls.Add(Cost_Detail);
			tabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
			tabControl.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			tabControl.Location = new System.Drawing.Point(0, 39);
			tabControl.Name = "tabControl";
			tabControl.Padding = new System.Drawing.Point(15, 10);
			tabControl.SelectedIndex = 0;
			tabControl.Size = new System.Drawing.Size(981, 622);
			tabControl.TabIndex = 54;
			BasicData.Controls.Add(btn_DeleteMember);
			BasicData.Controls.Add(btn_cancel);
			BasicData.Controls.Add(btn_save);
			BasicData.Controls.Add(tableLayoutPanel1);
			BasicData.Location = new System.Drawing.Point(4, 47);
			BasicData.Name = "BasicData";
			BasicData.Padding = new System.Windows.Forms.Padding(3);
			BasicData.Size = new System.Drawing.Size(973, 571);
			BasicData.TabIndex = 0;
			BasicData.Text = "基本資料";
			BasicData.UseVisualStyleBackColor = true;
			btn_DeleteMember.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_DeleteMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_DeleteMember.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_DeleteMember.ForeColor = System.Drawing.Color.White;
			btn_DeleteMember.Location = new System.Drawing.Point(646, 518);
			btn_DeleteMember.Name = "btn_DeleteMember";
			btn_DeleteMember.Size = new System.Drawing.Size(103, 32);
			btn_DeleteMember.TabIndex = 55;
			btn_DeleteMember.Text = "刪除會員";
			btn_DeleteMember.UseVisualStyleBackColor = false;
			btn_DeleteMember.Click += new System.EventHandler(btn_DeleteMember_Click);
			btn_cancel.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_cancel.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_cancel.ForeColor = System.Drawing.Color.White;
			btn_cancel.Location = new System.Drawing.Point(544, 518);
			btn_cancel.Name = "btn_cancel";
			btn_cancel.Size = new System.Drawing.Size(70, 32);
			btn_cancel.TabIndex = 2;
			btn_cancel.Text = "取消";
			btn_cancel.UseVisualStyleBackColor = false;
			btn_cancel.Click += new System.EventHandler(btn_cancel_Click);
			btn_save.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_save.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_save.ForeColor = System.Drawing.Color.White;
			btn_save.Location = new System.Drawing.Point(408, 519);
			btn_save.Name = "btn_save";
			btn_save.Size = new System.Drawing.Size(103, 32);
			btn_save.TabIndex = 1;
			btn_save.Text = "儲存變更";
			btn_save.UseVisualStyleBackColor = false;
			btn_save.Click += new System.EventHandler(btn_save_Click);
			tableLayoutPanel1.BackColor = System.Drawing.Color.White;
			tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel1.ColumnCount = 4;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.Controls.Add(cb_status, 3, 5);
			tableLayoutPanel1.Controls.Add(tb_fax, 3, 7);
			tableLayoutPanel1.Controls.Add(tb_companyTel, 1, 7);
			tableLayoutPanel1.Controls.Add(tb_companyIdno, 3, 6);
			tableLayoutPanel1.Controls.Add(tb_companyName, 1, 6);
			tableLayoutPanel1.Controls.Add(tb_email, 1, 5);
			tableLayoutPanel1.Controls.Add(tb_name, 1, 1);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel4, 1, 3);
			tableLayoutPanel1.Controls.Add(panel11, 2, 5);
			tableLayoutPanel1.Controls.Add(panel9, 2, 0);
			tableLayoutPanel1.Controls.Add(panel8, 0, 7);
			tableLayoutPanel1.Controls.Add(panel7, 0, 6);
			tableLayoutPanel1.Controls.Add(panel6, 0, 5);
			tableLayoutPanel1.Controls.Add(panel3, 0, 2);
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(panel4, 0, 3);
			tableLayoutPanel1.Controls.Add(panel5, 0, 4);
			tableLayoutPanel1.Controls.Add(panel10, 2, 1);
			tableLayoutPanel1.Controls.Add(panel12, 2, 6);
			tableLayoutPanel1.Controls.Add(panel13, 2, 7);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 1, 2);
			tableLayoutPanel1.Controls.Add(dt_birthDate, 3, 1);
			tableLayoutPanel1.Controls.Add(panel14, 1, 4);
			tableLayoutPanel1.Controls.Add(cb_type, 3, 0);
			tableLayoutPanel1.Controls.Add(panel15, 0, 1);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 8;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23292f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.36951f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel1.Size = new System.Drawing.Size(967, 503);
			tableLayoutPanel1.TabIndex = 0;
			cb_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_status.FormattingEnabled = true;
			cb_status.Location = new System.Drawing.Point(657, 348);
			cb_status.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			cb_status.Name = "cb_status";
			cb_status.Size = new System.Drawing.Size(299, 32);
			cb_status.TabIndex = 39;
			tb_fax.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_fax.Location = new System.Drawing.Point(657, 456);
			tb_fax.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_fax.Name = "tb_fax";
			tb_fax.Size = new System.Drawing.Size(299, 33);
			tb_fax.TabIndex = 38;
			tb_companyTel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_companyTel.Location = new System.Drawing.Point(174, 456);
			tb_companyTel.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_companyTel.Name = "tb_companyTel";
			tb_companyTel.Size = new System.Drawing.Size(299, 33);
			tb_companyTel.TabIndex = 37;
			tb_companyIdno.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_companyIdno.Location = new System.Drawing.Point(657, 398);
			tb_companyIdno.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_companyIdno.Name = "tb_companyIdno";
			tb_companyIdno.Size = new System.Drawing.Size(299, 33);
			tb_companyIdno.TabIndex = 36;
			tb_companyName.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_companyName.Location = new System.Drawing.Point(174, 398);
			tb_companyName.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_companyName.Name = "tb_companyName";
			tb_companyName.Size = new System.Drawing.Size(299, 33);
			tb_companyName.TabIndex = 35;
			tb_email.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_email.Location = new System.Drawing.Point(174, 342);
			tb_email.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			tb_email.Name = "tb_email";
			tb_email.Size = new System.Drawing.Size(299, 33);
			tb_email.TabIndex = 33;
			tb_name.Anchor = System.Windows.Forms.AnchorStyles.Left;
			tb_name.Location = new System.Drawing.Point(174, 68);
			tb_name.Margin = new System.Windows.Forms.Padding(10);
			tb_name.Name = "tb_name";
			tb_name.Size = new System.Drawing.Size(299, 33);
			tb_name.TabIndex = 32;
			tableLayoutPanel1.SetColumnSpan(flowLayoutPanel4, 3);
			flowLayoutPanel4.Controls.Add(label20);
			flowLayoutPanel4.Controls.Add(tb_tel);
			flowLayoutPanel4.Controls.Add(label22);
			flowLayoutPanel4.Controls.Add(tb_mobile);
			flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel4.Location = new System.Drawing.Point(164, 169);
			flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel4.Name = "flowLayoutPanel4";
			flowLayoutPanel4.Size = new System.Drawing.Size(802, 55);
			flowLayoutPanel4.TabIndex = 28;
			label20.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label20.AutoSize = true;
			label20.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label20.ForeColor = System.Drawing.Color.Black;
			label20.Location = new System.Drawing.Point(10, 16);
			label20.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(73, 20);
			label20.TabIndex = 3;
			label20.Text = "室內電話";
			tb_tel.Location = new System.Drawing.Point(96, 10);
			tb_tel.Margin = new System.Windows.Forms.Padding(10);
			tb_tel.Name = "tb_tel";
			tb_tel.Size = new System.Drawing.Size(284, 33);
			tb_tel.TabIndex = 1;
			label22.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label22.AutoSize = true;
			label22.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label22.ForeColor = System.Drawing.Color.Black;
			label22.Location = new System.Drawing.Point(393, 16);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(73, 20);
			label22.TabIndex = 5;
			label22.Text = "行動電話";
			tb_mobile.Location = new System.Drawing.Point(479, 10);
			tb_mobile.Margin = new System.Windows.Forms.Padding(10);
			tb_mobile.Name = "tb_mobile";
			tb_mobile.Size = new System.Drawing.Size(299, 33);
			tb_mobile.TabIndex = 4;
			panel11.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel11.Controls.Add(label15);
			panel11.Dock = System.Windows.Forms.DockStyle.Fill;
			panel11.Location = new System.Drawing.Point(484, 331);
			panel11.Margin = new System.Windows.Forms.Padding(0);
			panel11.Name = "panel11";
			panel11.Size = new System.Drawing.Size(162, 55);
			panel11.TabIndex = 23;
			label15.AutoSize = true;
			label15.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label15.ForeColor = System.Drawing.Color.White;
			label15.Location = new System.Drawing.Point(98, 22);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(42, 21);
			label15.TabIndex = 0;
			label15.Text = "狀態";
			panel9.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel9.Controls.Add(label5);
			panel9.Controls.Add(label7);
			panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			panel9.Location = new System.Drawing.Point(484, 1);
			panel9.Margin = new System.Windows.Forms.Padding(0);
			panel9.Name = "panel9";
			panel9.Size = new System.Drawing.Size(162, 55);
			panel9.TabIndex = 20;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Red;
			label5.Location = new System.Drawing.Point(53, 23);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(17, 21);
			label5.TabIndex = 1;
			label5.Text = "*";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label7.ForeColor = System.Drawing.Color.White;
			label7.Location = new System.Drawing.Point(66, 23);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(74, 21);
			label7.TabIndex = 0;
			label7.Text = "會員類型";
			panel8.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel8.Controls.Add(label16);
			panel8.Dock = System.Windows.Forms.DockStyle.Fill;
			panel8.Location = new System.Drawing.Point(1, 443);
			panel8.Margin = new System.Windows.Forms.Padding(0);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(162, 59);
			panel8.TabIndex = 20;
			label16.AutoSize = true;
			label16.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label16.ForeColor = System.Drawing.Color.White;
			label16.Location = new System.Drawing.Point(83, 23);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(74, 21);
			label16.TabIndex = 0;
			label16.Text = "公司電話";
			panel7.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel7.Controls.Add(label14);
			panel7.Dock = System.Windows.Forms.DockStyle.Fill;
			panel7.Location = new System.Drawing.Point(1, 387);
			panel7.Margin = new System.Windows.Forms.Padding(0);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(162, 55);
			panel7.TabIndex = 20;
			label14.AutoSize = true;
			label14.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label14.ForeColor = System.Drawing.Color.White;
			label14.Location = new System.Drawing.Point(80, 23);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(74, 21);
			label14.TabIndex = 0;
			label14.Text = "公司名稱";
			panel6.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel6.Controls.Add(label12);
			panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			panel6.Location = new System.Drawing.Point(1, 331);
			panel6.Margin = new System.Windows.Forms.Padding(0);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(162, 55);
			panel6.TabIndex = 20;
			label12.AutoSize = true;
			label12.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label12.ForeColor = System.Drawing.Color.White;
			label12.Location = new System.Drawing.Point(83, 22);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(74, 21);
			label12.TabIndex = 0;
			label12.Text = "電子信箱";
			panel3.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel3.Controls.Add(label6);
			panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(1, 113);
			panel3.Margin = new System.Windows.Forms.Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(162, 55);
			panel3.TabIndex = 21;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.ForeColor = System.Drawing.Color.White;
			label6.Location = new System.Drawing.Point(64, 24);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(90, 21);
			label6.TabIndex = 0;
			label6.Text = "身分證字號";
			panel1.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(1, 1);
			panel1.Margin = new System.Windows.Forms.Padding(0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(162, 55);
			panel1.TabIndex = 19;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.Red;
			label2.Location = new System.Drawing.Point(36, 23);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(17, 21);
			label2.TabIndex = 1;
			label2.Text = "*";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(49, 23);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(105, 21);
			label1.TabIndex = 0;
			label1.Text = "門市 / 會員號";
			panel4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel4.Controls.Add(label8);
			panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			panel4.Location = new System.Drawing.Point(1, 169);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(162, 55);
			panel4.TabIndex = 22;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label8.ForeColor = System.Drawing.Color.White;
			label8.Location = new System.Drawing.Point(80, 22);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(74, 21);
			label8.TabIndex = 0;
			label8.Text = "連絡電話";
			panel5.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel5.Controls.Add(label9);
			panel5.Controls.Add(label10);
			panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			panel5.Location = new System.Drawing.Point(1, 225);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(162, 105);
			panel5.TabIndex = 23;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label9.ForeColor = System.Drawing.Color.Red;
			label9.Location = new System.Drawing.Point(99, 48);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(17, 21);
			label9.TabIndex = 1;
			label9.Text = "*";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label10.ForeColor = System.Drawing.Color.White;
			label10.Location = new System.Drawing.Point(112, 48);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(42, 21);
			label10.TabIndex = 0;
			label10.Text = "地址";
			panel10.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel10.Controls.Add(label11);
			panel10.Controls.Add(label13);
			panel10.Dock = System.Windows.Forms.DockStyle.Fill;
			panel10.Location = new System.Drawing.Point(484, 57);
			panel10.Margin = new System.Windows.Forms.Padding(0);
			panel10.Name = "panel10";
			panel10.Size = new System.Drawing.Size(162, 55);
			panel10.TabIndex = 24;
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label11.ForeColor = System.Drawing.Color.Red;
			label11.Location = new System.Drawing.Point(43, 23);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(17, 21);
			label11.TabIndex = 1;
			label11.Text = "*";
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label13.ForeColor = System.Drawing.Color.White;
			label13.Location = new System.Drawing.Point(53, 23);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(90, 21);
			label13.TabIndex = 0;
			label13.Text = "出生年月日";
			panel12.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel12.Controls.Add(label17);
			panel12.Dock = System.Windows.Forms.DockStyle.Fill;
			panel12.Location = new System.Drawing.Point(484, 387);
			panel12.Margin = new System.Windows.Forms.Padding(0);
			panel12.Name = "panel12";
			panel12.Size = new System.Drawing.Size(162, 55);
			panel12.TabIndex = 24;
			label17.AutoSize = true;
			label17.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label17.ForeColor = System.Drawing.Color.White;
			label17.Location = new System.Drawing.Point(66, 23);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(74, 21);
			label17.TabIndex = 0;
			label17.Text = "統一編號";
			panel13.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel13.Controls.Add(label18);
			panel13.Dock = System.Windows.Forms.DockStyle.Fill;
			panel13.Location = new System.Drawing.Point(484, 443);
			panel13.Margin = new System.Windows.Forms.Padding(0);
			panel13.Name = "panel13";
			panel13.Size = new System.Drawing.Size(162, 59);
			panel13.TabIndex = 24;
			label18.AutoSize = true;
			label18.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label18.ForeColor = System.Drawing.Color.White;
			label18.Location = new System.Drawing.Point(66, 22);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(74, 21);
			label18.TabIndex = 0;
			label18.Text = "傳真機號";
			flowLayoutPanel1.Controls.Add(tb_licenseCode);
			flowLayoutPanel1.Controls.Add(tb_vipNo);
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel1.Location = new System.Drawing.Point(164, 1);
			flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(319, 55);
			flowLayoutPanel1.TabIndex = 25;
			tb_licenseCode.Cursor = System.Windows.Forms.Cursors.No;
			tb_licenseCode.Enabled = false;
			tb_licenseCode.Location = new System.Drawing.Point(10, 10);
			tb_licenseCode.Margin = new System.Windows.Forms.Padding(10, 10, 3, 10);
			tb_licenseCode.Name = "tb_licenseCode";
			tb_licenseCode.ReadOnly = true;
			tb_licenseCode.Size = new System.Drawing.Size(109, 33);
			tb_licenseCode.TabIndex = 0;
			tb_vipNo.Cursor = System.Windows.Forms.Cursors.No;
			tb_vipNo.Enabled = false;
			tb_vipNo.Location = new System.Drawing.Point(132, 10);
			tb_vipNo.Margin = new System.Windows.Forms.Padding(10);
			tb_vipNo.Name = "tb_vipNo";
			tb_vipNo.ReadOnly = true;
			tb_vipNo.Size = new System.Drawing.Size(175, 33);
			tb_vipNo.TabIndex = 1;
			tableLayoutPanel1.SetColumnSpan(flowLayoutPanel3, 3);
			flowLayoutPanel3.Controls.Add(tb_idno);
			flowLayoutPanel3.Controls.Add(btn_check);
			flowLayoutPanel3.Controls.Add(l_checkLowIncome);
			flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel3.Location = new System.Drawing.Point(164, 113);
			flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel3.Name = "flowLayoutPanel3";
			flowLayoutPanel3.Size = new System.Drawing.Size(802, 55);
			flowLayoutPanel3.TabIndex = 27;
			tb_idno.ImeMode = System.Windows.Forms.ImeMode.Disable;
			tb_idno.Location = new System.Drawing.Point(10, 10);
			tb_idno.Margin = new System.Windows.Forms.Padding(10);
			tb_idno.MaxLength = 10;
			tb_idno.Name = "tb_idno";
			tb_idno.Size = new System.Drawing.Size(299, 33);
			tb_idno.TabIndex = 1;
			tb_idno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tb_idno_KeyPress);
			btn_check.Anchor = System.Windows.Forms.AnchorStyles.Left;
			btn_check.BackColor = System.Drawing.Color.FromArgb(153, 153, 153);
			btn_check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_check.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			btn_check.ForeColor = System.Drawing.Color.White;
			btn_check.Location = new System.Drawing.Point(322, 9);
			btn_check.Name = "btn_check";
			btn_check.Size = new System.Drawing.Size(86, 35);
			btn_check.TabIndex = 2;
			btn_check.Text = "檢查輔助";
			btn_check.UseVisualStyleBackColor = false;
			btn_check.Click += new System.EventHandler(btn_check_Click);
			l_checkLowIncome.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_checkLowIncome.AutoSize = true;
			l_checkLowIncome.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			l_checkLowIncome.ForeColor = System.Drawing.Color.Red;
			l_checkLowIncome.Location = new System.Drawing.Point(414, 18);
			l_checkLowIncome.Name = "l_checkLowIncome";
			l_checkLowIncome.Size = new System.Drawing.Size(242, 17);
			l_checkLowIncome.TabIndex = 3;
			l_checkLowIncome.Text = "若需驗證補助身分請務必填寫身分證字號";
			dt_birthDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dt_birthDate.CustomFormat = "yyyy-MM-dd";
			dt_birthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dt_birthDate.Location = new System.Drawing.Point(657, 68);
			dt_birthDate.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			dt_birthDate.Name = "dt_birthDate";
			dt_birthDate.Size = new System.Drawing.Size(299, 33);
			dt_birthDate.TabIndex = 31;
			tableLayoutPanel1.SetColumnSpan(panel14, 3);
			panel14.Controls.Add(tb_zipcode);
			panel14.Controls.Add(cb_area);
			panel14.Controls.Add(cb_city);
			panel14.Controls.Add(tb_addr);
			panel14.Dock = System.Windows.Forms.DockStyle.Fill;
			panel14.Location = new System.Drawing.Point(164, 225);
			panel14.Margin = new System.Windows.Forms.Padding(0);
			panel14.Name = "panel14";
			panel14.Size = new System.Drawing.Size(802, 105);
			panel14.TabIndex = 40;
			tb_zipcode.Cursor = System.Windows.Forms.Cursors.No;
			tb_zipcode.Enabled = false;
			tb_zipcode.Location = new System.Drawing.Point(290, 16);
			tb_zipcode.Name = "tb_zipcode";
			tb_zipcode.ReadOnly = true;
			tb_zipcode.Size = new System.Drawing.Size(100, 33);
			tb_zipcode.TabIndex = 6;
			cb_area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_area.FormattingEnabled = true;
			cb_area.Location = new System.Drawing.Point(152, 17);
			cb_area.Name = "cb_area";
			cb_area.Size = new System.Drawing.Size(121, 32);
			cb_area.TabIndex = 5;
			cb_area.SelectedIndexChanged += new System.EventHandler(cb_area_SelectedIndexChanged);
			cb_city.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_city.FormattingEnabled = true;
			cb_city.Location = new System.Drawing.Point(14, 17);
			cb_city.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			cb_city.Name = "cb_city";
			cb_city.Size = new System.Drawing.Size(121, 32);
			cb_city.TabIndex = 4;
			cb_city.SelectedIndexChanged += new System.EventHandler(cb_city_SelectedIndexChanged);
			tb_addr.Location = new System.Drawing.Point(14, 64);
			tb_addr.Margin = new System.Windows.Forms.Padding(0);
			tb_addr.Name = "tb_addr";
			tb_addr.Size = new System.Drawing.Size(778, 33);
			tb_addr.TabIndex = 7;
			cb_type.Anchor = System.Windows.Forms.AnchorStyles.Left;
			cb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cb_type.FormattingEnabled = true;
			cb_type.Location = new System.Drawing.Point(657, 18);
			cb_type.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
			cb_type.Name = "cb_type";
			cb_type.Size = new System.Drawing.Size(299, 32);
			cb_type.TabIndex = 30;
			panel15.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel15.Controls.Add(label21);
			panel15.Controls.Add(label23);
			panel15.Dock = System.Windows.Forms.DockStyle.Fill;
			panel15.Location = new System.Drawing.Point(1, 57);
			panel15.Margin = new System.Windows.Forms.Padding(0);
			panel15.Name = "panel15";
			panel15.Size = new System.Drawing.Size(162, 55);
			panel15.TabIndex = 24;
			label21.AutoSize = true;
			label21.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label21.ForeColor = System.Drawing.Color.Red;
			label21.Location = new System.Drawing.Point(64, 24);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(17, 21);
			label21.TabIndex = 1;
			label21.Text = "*";
			label23.AutoSize = true;
			label23.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label23.ForeColor = System.Drawing.Color.White;
			label23.Location = new System.Drawing.Point(83, 24);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(74, 21);
			label23.TabIndex = 0;
			label23.Text = "會員姓名";
			ConsumeRecord.Controls.Add(spDate);
			ConsumeRecord.Controls.Add(spDate2);
			ConsumeRecord.Controls.Add(label49);
			ConsumeRecord.Controls.Add(btn_SelectDate);
			ConsumeRecord.Controls.Add(comboBox1);
			ConsumeRecord.Controls.Add(label_status);
			ConsumeRecord.Controls.Add(label19);
			ConsumeRecord.Controls.Add(tableLayoutPanel2);
			ConsumeRecord.Controls.Add(infolist);
			ConsumeRecord.Location = new System.Drawing.Point(4, 47);
			ConsumeRecord.Name = "ConsumeRecord";
			ConsumeRecord.Padding = new System.Windows.Forms.Padding(3);
			ConsumeRecord.Size = new System.Drawing.Size(973, 571);
			ConsumeRecord.TabIndex = 1;
			ConsumeRecord.Text = "消費紀錄";
			ConsumeRecord.UseVisualStyleBackColor = true;
			spDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			spDate.CustomFormat = "yyyy-MM-dd";
			spDate.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			spDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			spDate.ImeMode = System.Windows.Forms.ImeMode.Disable;
			spDate.Location = new System.Drawing.Point(285, 92);
			spDate.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			spDate.Name = "spDate";
			spDate.ShowCheckBox = true;
			spDate.Size = new System.Drawing.Size(181, 29);
			spDate.TabIndex = 33;
			spDate.ValueChanged += new System.EventHandler(spDate_ValueChanged);
			spDate2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			spDate2.CustomFormat = "yyyy-MM-dd";
			spDate2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			spDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			spDate2.ImeMode = System.Windows.Forms.ImeMode.Disable;
			spDate2.Location = new System.Drawing.Point(488, 92);
			spDate2.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			spDate2.Name = "spDate2";
			spDate2.ShowCheckBox = true;
			spDate2.Size = new System.Drawing.Size(181, 29);
			spDate2.TabIndex = 52;
			spDate2.ValueChanged += new System.EventHandler(spDate2_ValueChanged);
			label49.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label49.AutoSize = true;
			label49.Location = new System.Drawing.Point(465, 95);
			label49.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label49.Name = "label49";
			label49.Size = new System.Drawing.Size(24, 24);
			label49.TabIndex = 51;
			label49.Text = "~";
			btn_SelectDate.BackColor = System.Drawing.Color.SandyBrown;
			btn_SelectDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_SelectDate.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_SelectDate.ForeColor = System.Drawing.Color.WhiteSmoke;
			btn_SelectDate.Location = new System.Drawing.Point(674, 93);
			btn_SelectDate.Name = "btn_SelectDate";
			btn_SelectDate.Size = new System.Drawing.Size(80, 25);
			btn_SelectDate.TabIndex = 34;
			btn_SelectDate.Text = "選擇時間";
			btn_SelectDate.UseVisualStyleBackColor = false;
			btn_SelectDate.Visible = false;
			btn_SelectDate.Click += new System.EventHandler(specifyTheDate_Click);
			comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox1.Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new System.Drawing.Point(847, 92);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new System.Drawing.Size(121, 25);
			comboBox1.TabIndex = 4;
			comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
			label_status.AutoSize = true;
			label_status.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label_status.Location = new System.Drawing.Point(755, 96);
			label_status.Name = "label_status";
			label_status.Size = new System.Drawing.Size(86, 21);
			label_status.TabIndex = 3;
			label_status.Text = "篩選類型 : ";
			label19.AutoSize = true;
			label19.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label19.ForeColor = System.Drawing.Color.Black;
			label19.Location = new System.Drawing.Point(6, 97);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(138, 21);
			label19.TabIndex = 0;
			label19.Text = "近期消費記錄一覽";
			tableLayoutPanel2.BackColor = System.Drawing.Color.White;
			tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel2.ColumnCount = 4;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel2.Controls.Add(panel17, 2, 0);
			tableLayoutPanel2.Controls.Add(panel22, 0, 0);
			tableLayoutPanel2.Controls.Add(panel25, 2, 1);
			tableLayoutPanel2.Controls.Add(panel29, 0, 1);
			tableLayoutPanel2.Controls.Add(l_Total, 1, 0);
			tableLayoutPanel2.Controls.Add(l_BuyDate, 3, 0);
			tableLayoutPanel2.Controls.Add(l_RepayDate, 3, 1);
			tableLayoutPanel2.Controls.Add(panel16, 1, 1);
			tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 2;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23292f));
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel2.Size = new System.Drawing.Size(967, 86);
			tableLayoutPanel2.TabIndex = 1;
			panel17.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel17.Controls.Add(label27);
			panel17.Dock = System.Windows.Forms.DockStyle.Fill;
			panel17.Location = new System.Drawing.Point(484, 1);
			panel17.Margin = new System.Windows.Forms.Padding(0);
			panel17.Name = "panel17";
			panel17.Size = new System.Drawing.Size(162, 41);
			panel17.TabIndex = 20;
			label27.AutoSize = true;
			label27.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label27.ForeColor = System.Drawing.Color.White;
			label27.Location = new System.Drawing.Point(53, 14);
			label27.Name = "label27";
			label27.Size = new System.Drawing.Size(106, 21);
			label27.TabIndex = 0;
			label27.Text = "最近消費日期";
			panel22.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel22.Controls.Add(label33);
			panel22.Dock = System.Windows.Forms.DockStyle.Fill;
			panel22.Location = new System.Drawing.Point(1, 1);
			panel22.Margin = new System.Windows.Forms.Padding(0);
			panel22.Name = "panel22";
			panel22.Size = new System.Drawing.Size(162, 41);
			panel22.TabIndex = 19;
			label33.AutoSize = true;
			label33.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label33.ForeColor = System.Drawing.Color.White;
			label33.Location = new System.Drawing.Point(67, 14);
			label33.Name = "label33";
			label33.Size = new System.Drawing.Size(90, 21);
			label33.TabIndex = 0;
			label33.Text = "總消費金額";
			panel25.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel25.Controls.Add(label38);
			panel25.Dock = System.Windows.Forms.DockStyle.Fill;
			panel25.Location = new System.Drawing.Point(484, 43);
			panel25.Margin = new System.Windows.Forms.Padding(0);
			panel25.Name = "panel25";
			panel25.Size = new System.Drawing.Size(162, 42);
			panel25.TabIndex = 24;
			label38.AutoSize = true;
			label38.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label38.ForeColor = System.Drawing.Color.White;
			label38.Location = new System.Drawing.Point(53, 12);
			label38.Name = "label38";
			label38.Size = new System.Drawing.Size(106, 21);
			label38.TabIndex = 0;
			label38.Text = "最近還款日期";
			panel29.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel29.Controls.Add(label43);
			panel29.Location = new System.Drawing.Point(1, 43);
			panel29.Margin = new System.Windows.Forms.Padding(0);
			panel29.Name = "panel29";
			panel29.Size = new System.Drawing.Size(162, 42);
			panel29.TabIndex = 24;
			label43.AutoSize = true;
			label43.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label43.ForeColor = System.Drawing.Color.White;
			label43.Location = new System.Drawing.Point(83, 12);
			label43.Name = "label43";
			label43.Size = new System.Drawing.Size(74, 21);
			label43.TabIndex = 0;
			label43.Text = "賒帳金額";
			l_Total.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_Total.AutoSize = true;
			l_Total.Location = new System.Drawing.Point(167, 9);
			l_Total.Name = "l_Total";
			l_Total.Size = new System.Drawing.Size(33, 24);
			l_Total.TabIndex = 25;
			l_Total.Text = "{0}";
			l_BuyDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_BuyDate.AutoSize = true;
			l_BuyDate.Location = new System.Drawing.Point(650, 9);
			l_BuyDate.Name = "l_BuyDate";
			l_BuyDate.Size = new System.Drawing.Size(33, 24);
			l_BuyDate.TabIndex = 25;
			l_BuyDate.Text = "{0}";
			l_RepayDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			l_RepayDate.AutoSize = true;
			l_RepayDate.Location = new System.Drawing.Point(650, 52);
			l_RepayDate.Name = "l_RepayDate";
			l_RepayDate.Size = new System.Drawing.Size(33, 24);
			l_RepayDate.TabIndex = 25;
			l_RepayDate.Text = "{0}";
			panel16.BackColor = System.Drawing.Color.White;
			panel16.Controls.Add(btn_repay);
			panel16.Controls.Add(l_Credit);
			panel16.Dock = System.Windows.Forms.DockStyle.Fill;
			panel16.Location = new System.Drawing.Point(164, 43);
			panel16.Margin = new System.Windows.Forms.Padding(0);
			panel16.Name = "panel16";
			panel16.Size = new System.Drawing.Size(319, 42);
			panel16.TabIndex = 24;
			btn_repay.BackColor = System.Drawing.Color.FromArgb(255, 109, 49);
			btn_repay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_repay.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_repay.ForeColor = System.Drawing.Color.WhiteSmoke;
			btn_repay.Location = new System.Drawing.Point(225, 6);
			btn_repay.Name = "btn_repay";
			btn_repay.Size = new System.Drawing.Size(86, 30);
			btn_repay.TabIndex = 26;
			btn_repay.Text = "賒帳還款";
			btn_repay.UseVisualStyleBackColor = false;
			btn_repay.Click += new System.EventHandler(btn_repay_Click);
			l_Credit.Anchor = System.Windows.Forms.AnchorStyles.None;
			l_Credit.AutoSize = true;
			l_Credit.Location = new System.Drawing.Point(3, 9);
			l_Credit.Name = "l_Credit";
			l_Credit.Size = new System.Drawing.Size(33, 24);
			l_Credit.TabIndex = 25;
			l_Credit.Text = "{0}";
			infolist.AllowUserToAddRows = false;
			infolist.AllowUserToDeleteRows = false;
			infolist.AllowUserToResizeColumns = false;
			infolist.AllowUserToResizeRows = false;
			infolist.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			infolist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			infolist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle.Padding = new System.Windows.Forms.Padding(3);
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 255);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			infolist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			infolist.Columns.AddRange(editdate, sellType, payType, sellNo, sum, status);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			infolist.DefaultCellStyle = dataGridViewCellStyle2;
			infolist.EnableHeadersVisualStyles = false;
			infolist.GridColor = System.Drawing.SystemColors.ActiveBorder;
			infolist.Location = new System.Drawing.Point(3, 123);
			infolist.MultiSelect = false;
			infolist.Name = "infolist";
			infolist.ReadOnly = true;
			infolist.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ScrollBar;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 255);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			infolist.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			infolist.RowHeadersVisible = false;
			infolist.RowTemplate.Height = 24;
			infolist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			infolist.Size = new System.Drawing.Size(967, 448);
			infolist.TabIndex = 5;
			infolist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(checkSell);
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			editdate.DefaultCellStyle = dataGridViewCellStyle4;
			editdate.HeaderText = "銷售/編修日期時間";
			editdate.Name = "editdate";
			editdate.ReadOnly = true;
			editdate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			editdate.Width = 200;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			sellType.DefaultCellStyle = dataGridViewCellStyle5;
			sellType.HeaderText = "類型";
			sellType.Name = "sellType";
			sellType.ReadOnly = true;
			sellType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			sellType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			payType.DefaultCellStyle = dataGridViewCellStyle6;
			payType.HeaderText = "付款模式";
			payType.Name = "payType";
			payType.ReadOnly = true;
			payType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			payType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			payType.Width = 200;
			sellNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Blue;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Blue;
			sellNo.DefaultCellStyle = dataGridViewCellStyle7;
			sellNo.HeaderText = "銷售單號";
			sellNo.Name = "sellNo";
			sellNo.ReadOnly = true;
			sellNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			sum.DefaultCellStyle = dataGridViewCellStyle8;
			sum.HeaderText = "消費單總額";
			sum.Name = "sum";
			sum.ReadOnly = true;
			sum.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			sum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			sum.Width = 120;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			status.DefaultCellStyle = dataGridViewCellStyle9;
			status.HeaderText = "銷售單狀態";
			status.Name = "status";
			status.ReadOnly = true;
			status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			status.Width = 120;
			Cost_Detail.Controls.Add(label28);
			Cost_Detail.Controls.Add(dataGridView1);
			Cost_Detail.Controls.Add(btn_reset);
			Cost_Detail.Controls.Add(btn_enter);
			Cost_Detail.Controls.Add(tableLayoutPanel4);
			Cost_Detail.Location = new System.Drawing.Point(4, 47);
			Cost_Detail.Name = "Cost_Detail";
			Cost_Detail.Padding = new System.Windows.Forms.Padding(3);
			Cost_Detail.Size = new System.Drawing.Size(973, 571);
			Cost_Detail.TabIndex = 2;
			Cost_Detail.Text = "消費明細";
			Cost_Detail.UseVisualStyleBackColor = true;
			label28.AutoSize = true;
			label28.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label28.ForeColor = System.Drawing.Color.Black;
			label28.Location = new System.Drawing.Point(17, 100);
			label28.Name = "label28";
			label28.Size = new System.Drawing.Size(106, 21);
			label28.TabIndex = 46;
			label28.Text = "近期消費明細";
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle10.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(3);
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 255);
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column4, Column5);
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			dataGridView1.DefaultCellStyle = dataGridViewCellStyle11;
			dataGridView1.EnableHeadersVisualStyles = false;
			dataGridView1.GridColor = System.Drawing.SystemColors.ActiveBorder;
			dataGridView1.Location = new System.Drawing.Point(3, 134);
			dataGridView1.MultiSelect = false;
			dataGridView1.Name = "dataGridView1";
			dataGridView1.ReadOnly = true;
			dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.ScrollBar;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(255, 192, 255);
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.RowTemplate.Height = 24;
			dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			dataGridView1.Size = new System.Drawing.Size(967, 448);
			dataGridView1.TabIndex = 45;
			dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			Column1.DefaultCellStyle = dataGridViewCellStyle13;
			Column1.HeaderText = "銷售日期";
			Column1.Name = "Column1";
			Column1.ReadOnly = true;
			Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.Width = 193;
			Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			Column2.DefaultCellStyle = dataGridViewCellStyle14;
			Column2.HeaderText = "商品名稱";
			Column2.Name = "Column2";
			Column2.ReadOnly = true;
			Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			Column3.DefaultCellStyle = dataGridViewCellStyle15;
			Column3.HeaderText = "售價";
			Column3.Name = "Column3";
			Column3.ReadOnly = true;
			Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			Column4.DefaultCellStyle = dataGridViewCellStyle16;
			Column4.HeaderText = "數量";
			Column4.Name = "Column4";
			Column4.ReadOnly = true;
			Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			Column5.DefaultCellStyle = dataGridViewCellStyle17;
			Column5.HeaderText = "合計";
			Column5.Name = "Column5";
			Column5.ReadOnly = true;
			Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			btn_reset.BackColor = System.Drawing.Color.FromArgb(175, 175, 175);
			btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_reset.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_reset.ForeColor = System.Drawing.Color.White;
			btn_reset.Location = new System.Drawing.Point(499, 92);
			btn_reset.Name = "btn_reset";
			btn_reset.Size = new System.Drawing.Size(75, 35);
			btn_reset.TabIndex = 44;
			btn_reset.TabStop = false;
			btn_reset.Text = "重設";
			btn_reset.UseVisualStyleBackColor = false;
			btn_reset.Click += new System.EventHandler(btn_reset_Click);
			btn_enter.BackColor = System.Drawing.Color.FromArgb(157, 189, 59);
			btn_enter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btn_enter.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			btn_enter.ForeColor = System.Drawing.Color.White;
			btn_enter.Location = new System.Drawing.Point(398, 92);
			btn_enter.Name = "btn_enter";
			btn_enter.Size = new System.Drawing.Size(75, 35);
			btn_enter.TabIndex = 43;
			btn_enter.TabStop = false;
			btn_enter.Text = "查詢";
			btn_enter.UseVisualStyleBackColor = false;
			btn_enter.Click += new System.EventHandler(btn_enter_Click);
			tableLayoutPanel4.BackColor = System.Drawing.Color.White;
			tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			tableLayoutPanel4.ColumnCount = 2;
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162f));
			tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel4.Controls.Add(panel21, 1, 0);
			tableLayoutPanel4.Controls.Add(panel30, 0, 0);
			tableLayoutPanel4.Controls.Add(panel32, 0, 1);
			tableLayoutPanel4.Controls.Add(panel33, 1, 1);
			tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
			tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.RowCount = 2;
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23292f));
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.23293f));
			tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			tableLayoutPanel4.Size = new System.Drawing.Size(967, 86);
			tableLayoutPanel4.TabIndex = 2;
			panel21.BackColor = System.Drawing.Color.White;
			panel21.Controls.Add(panel23);
			panel21.Dock = System.Windows.Forms.DockStyle.Fill;
			panel21.Location = new System.Drawing.Point(164, 1);
			panel21.Margin = new System.Windows.Forms.Padding(0);
			panel21.Name = "panel21";
			panel21.Size = new System.Drawing.Size(802, 41);
			panel21.TabIndex = 25;
			panel23.Controls.Add(tb_productname);
			panel23.Controls.Add(panel24);
			panel23.Controls.Add(tb_barcode);
			panel23.Location = new System.Drawing.Point(3, 0);
			panel23.Name = "panel23";
			panel23.Size = new System.Drawing.Size(803, 42);
			panel23.TabIndex = 0;
			tb_productname.Location = new System.Drawing.Point(530, 5);
			tb_productname.Name = "tb_productname";
			tb_productname.Size = new System.Drawing.Size(270, 33);
			tb_productname.TabIndex = 21;
			panel24.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel24.Controls.Add(label26);
			panel24.Location = new System.Drawing.Point(365, 1);
			panel24.Margin = new System.Windows.Forms.Padding(0);
			panel24.Name = "panel24";
			panel24.Size = new System.Drawing.Size(162, 41);
			panel24.TabIndex = 20;
			label26.AutoSize = true;
			label26.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label26.ForeColor = System.Drawing.Color.White;
			label26.Location = new System.Drawing.Point(67, 14);
			label26.Name = "label26";
			label26.Size = new System.Drawing.Size(74, 21);
			label26.TabIndex = 0;
			label26.Text = "商品名稱";
			tb_barcode.Location = new System.Drawing.Point(3, 6);
			tb_barcode.Name = "tb_barcode";
			tb_barcode.Size = new System.Drawing.Size(358, 33);
			tb_barcode.TabIndex = 0;
			panel30.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel30.Controls.Add(label34);
			panel30.Location = new System.Drawing.Point(1, 1);
			panel30.Margin = new System.Windows.Forms.Padding(0);
			panel30.Name = "panel30";
			panel30.Size = new System.Drawing.Size(162, 41);
			panel30.TabIndex = 19;
			label34.AutoSize = true;
			label34.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label34.ForeColor = System.Drawing.Color.White;
			label34.Location = new System.Drawing.Point(67, 14);
			label34.Name = "label34";
			label34.Size = new System.Drawing.Size(74, 21);
			label34.TabIndex = 0;
			label34.Text = "商品編號";
			panel32.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			panel32.Controls.Add(label36);
			panel32.Location = new System.Drawing.Point(1, 43);
			panel32.Margin = new System.Windows.Forms.Padding(0);
			panel32.Name = "panel32";
			panel32.Size = new System.Drawing.Size(162, 42);
			panel32.TabIndex = 24;
			label36.AutoSize = true;
			label36.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label36.ForeColor = System.Drawing.Color.White;
			label36.Location = new System.Drawing.Point(50, 12);
			label36.Name = "label36";
			label36.Size = new System.Drawing.Size(106, 21);
			label36.TabIndex = 0;
			label36.Text = "交易日期區間";
			panel33.BackColor = System.Drawing.Color.White;
			panel33.Controls.Add(panel19);
			panel33.Controls.Add(panel18);
			panel33.Dock = System.Windows.Forms.DockStyle.Fill;
			panel33.Location = new System.Drawing.Point(164, 43);
			panel33.Margin = new System.Windows.Forms.Padding(0);
			panel33.Name = "panel33";
			panel33.Size = new System.Drawing.Size(802, 42);
			panel33.TabIndex = 24;
			panel19.BackColor = System.Drawing.Color.White;
			panel19.Controls.Add(panel20);
			panel19.Dock = System.Windows.Forms.DockStyle.Fill;
			panel19.Location = new System.Drawing.Point(0, 0);
			panel19.Margin = new System.Windows.Forms.Padding(0);
			panel19.Name = "panel19";
			panel19.Size = new System.Drawing.Size(802, 42);
			panel19.TabIndex = 25;
			panel20.Controls.Add(dateTimePicker3);
			panel20.Controls.Add(dateTimePicker4);
			panel20.Controls.Add(label25);
			panel20.Location = new System.Drawing.Point(3, 0);
			panel20.Name = "panel20";
			panel20.Size = new System.Drawing.Size(453, 42);
			panel20.TabIndex = 0;
			dateTimePicker3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker3.CustomFormat = "yyyy-MM-dd";
			dateTimePicker3.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker3.ImeMode = System.Windows.Forms.ImeMode.Disable;
			dateTimePicker3.Location = new System.Drawing.Point(39, 7);
			dateTimePicker3.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			dateTimePicker3.Name = "dateTimePicker3";
			dateTimePicker3.ShowCheckBox = true;
			dateTimePicker3.Size = new System.Drawing.Size(181, 29);
			dateTimePicker3.TabIndex = 53;
			dateTimePicker4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker4.CustomFormat = "yyyy-MM-dd";
			dateTimePicker4.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker4.ImeMode = System.Windows.Forms.ImeMode.Disable;
			dateTimePicker4.Location = new System.Drawing.Point(242, 7);
			dateTimePicker4.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			dateTimePicker4.Name = "dateTimePicker4";
			dateTimePicker4.ShowCheckBox = true;
			dateTimePicker4.Size = new System.Drawing.Size(181, 29);
			dateTimePicker4.TabIndex = 55;
			label25.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label25.AutoSize = true;
			label25.Location = new System.Drawing.Point(219, 10);
			label25.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label25.Name = "label25";
			label25.Size = new System.Drawing.Size(24, 24);
			label25.TabIndex = 54;
			label25.Text = "~";
			panel18.Controls.Add(dateTimePicker1);
			panel18.Controls.Add(dateTimePicker2);
			panel18.Controls.Add(label24);
			panel18.Location = new System.Drawing.Point(3, 0);
			panel18.Name = "panel18";
			panel18.Size = new System.Drawing.Size(453, 42);
			panel18.TabIndex = 0;
			dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			dateTimePicker1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker1.ImeMode = System.Windows.Forms.ImeMode.Disable;
			dateTimePicker1.Location = new System.Drawing.Point(39, 7);
			dateTimePicker1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			dateTimePicker1.Name = "dateTimePicker1";
			dateTimePicker1.ShowCheckBox = true;
			dateTimePicker1.Size = new System.Drawing.Size(181, 29);
			dateTimePicker1.TabIndex = 53;
			dateTimePicker2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			dateTimePicker2.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePicker2.ImeMode = System.Windows.Forms.ImeMode.Disable;
			dateTimePicker2.Location = new System.Drawing.Point(242, 7);
			dateTimePicker2.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
			dateTimePicker2.Name = "dateTimePicker2";
			dateTimePicker2.ShowCheckBox = true;
			dateTimePicker2.Size = new System.Drawing.Size(181, 29);
			dateTimePicker2.TabIndex = 55;
			label24.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label24.AutoSize = true;
			label24.Location = new System.Drawing.Point(219, 10);
			label24.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			label24.Name = "label24";
			label24.Size = new System.Drawing.Size(24, 24);
			label24.TabIndex = 54;
			label24.Text = "~";
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 17f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(981, 661);
			base.Controls.Add(btn_printBarcode);
			base.Controls.Add(btn_back);
			base.Controls.Add(tabControl);
			Font = new System.Drawing.Font("微軟正黑體", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "frmEditMember";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "農委會防檢局POS系統";
			base.Load += new System.EventHandler(frmEditMember_Load);
			base.Controls.SetChildIndex(tabControl, 0);
			base.Controls.SetChildIndex(btn_back, 0);
			base.Controls.SetChildIndex(btn_printBarcode, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			tabControl.ResumeLayout(false);
			BasicData.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			flowLayoutPanel4.ResumeLayout(false);
			flowLayoutPanel4.PerformLayout();
			panel11.ResumeLayout(false);
			panel11.PerformLayout();
			panel9.ResumeLayout(false);
			panel9.PerformLayout();
			panel8.ResumeLayout(false);
			panel8.PerformLayout();
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
			panel10.ResumeLayout(false);
			panel10.PerformLayout();
			panel12.ResumeLayout(false);
			panel12.PerformLayout();
			panel13.ResumeLayout(false);
			panel13.PerformLayout();
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			flowLayoutPanel3.ResumeLayout(false);
			flowLayoutPanel3.PerformLayout();
			panel14.ResumeLayout(false);
			panel14.PerformLayout();
			panel15.ResumeLayout(false);
			panel15.PerformLayout();
			ConsumeRecord.ResumeLayout(false);
			ConsumeRecord.PerformLayout();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			panel17.ResumeLayout(false);
			panel17.PerformLayout();
			panel22.ResumeLayout(false);
			panel22.PerformLayout();
			panel25.ResumeLayout(false);
			panel25.PerformLayout();
			panel29.ResumeLayout(false);
			panel29.PerformLayout();
			panel16.ResumeLayout(false);
			panel16.PerformLayout();
			((System.ComponentModel.ISupportInitialize)infolist).EndInit();
			Cost_Detail.ResumeLayout(false);
			Cost_Detail.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			tableLayoutPanel4.ResumeLayout(false);
			panel21.ResumeLayout(false);
			panel23.ResumeLayout(false);
			panel23.PerformLayout();
			panel24.ResumeLayout(false);
			panel24.PerformLayout();
			panel30.ResumeLayout(false);
			panel30.PerformLayout();
			panel32.ResumeLayout(false);
			panel32.PerformLayout();
			panel33.ResumeLayout(false);
			panel19.ResumeLayout(false);
			panel20.ResumeLayout(false);
			panel20.PerformLayout();
			panel18.ResumeLayout(false);
			panel18.PerformLayout();
			ResumeLayout(false);
		}
	}
}
