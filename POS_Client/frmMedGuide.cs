using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmMedGuide : MasterThinForm
	{
		public class CustomColumnMed : DataGridViewColumn
		{
			public override DataGridViewCell CellTemplate
			{
				get
				{
					return base.CellTemplate;
				}
				set
				{
					if (value != null && !value.GetType().IsAssignableFrom(typeof(CustomeCellMed)))
					{
						throw new InvalidCastException("It should be a custom Cell");
					}
					base.CellTemplate = value;
				}
			}

			public CustomColumnMed()
				: base(new CustomeCellMed())
			{
			}
		}

		public class CustomeCellMed : DataGridViewCell
		{
			public override Type ValueType
			{
				get
				{
					return typeof(frmMedUserControl);
				}
			}

			protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
			{
				frmMedUserControl frmMedUserControl = (frmMedUserControl)value;
				Bitmap bitmap = new Bitmap(cellBounds.Width, cellBounds.Height);
				frmMedUserControl.DrawToBitmap(bitmap, new Rectangle(0, 0, frmMedUserControl.Width, frmMedUserControl.Height));
				graphics.DrawImage(bitmap, cellBounds.Location);
			}
		}

		private class MedInfo
		{
			[CompilerGenerated]
			private string _003C_pesticideId_003Ek__BackingField;

			[CompilerGenerated]
			private string _003C_formCode_003Ek__BackingField;

			[CompilerGenerated]
			private string _003C_contents_003Ek__BackingField;

			public string _pesticideId
			{
				[CompilerGenerated]
				get
				{
					return _003C_pesticideId_003Ek__BackingField;
				}
				[CompilerGenerated]
				set
				{
					_003C_pesticideId_003Ek__BackingField = value;
				}
			}

			public string _formCode
			{
				[CompilerGenerated]
				get
				{
					return _003C_formCode_003Ek__BackingField;
				}
				[CompilerGenerated]
				set
				{
					_003C_formCode_003Ek__BackingField = value;
				}
			}

			public string _contents
			{
				[CompilerGenerated]
				get
				{
					return _003C_contents_003Ek__BackingField;
				}
				[CompilerGenerated]
				set
				{
					_003C_contents_003Ek__BackingField = value;
				}
			}

			public MedInfo(string pesticideId, string formCode, string contents)
			{
				_pesticideId = pesticideId;
				_formCode = formCode;
				_contents = contents;
			}
		}

		public string cls1 = "";

		public string cls2 = "";

		public string cls3 = "";

		public string cls4 = "";

		public string cropcode = "";

		private frmMainShopSimple frs;

		private frmMainShopSimpleWithMoney frsm;

		private frmCropGuide fcg;

		private frmPestGuide fpg;

		private List<string> Medlist = new List<string>();

		private List<string> barcodelist = new List<string>();

		private IContainer components;

		private Label label6;

		private Button button1;

		private DataGridView clslit4;

		private DataGridView clslit3;

		private Label label8;

		private Label label11;

		private Label label9;

		private Label label10;

		private Label label7;

		private Label cropname;

		private Panel panel5;

		private Label label5;

		private Panel panel4;

		private Label label4;

		private Panel panel3;

		private Label label3;

		private Panel panel1;

		private Label label2;

		private Label label1;

		private DataGridViewTextBoxColumn cls4name;

		private DataGridViewTextBoxColumn cls4code;

		private DataGridViewTextBoxColumn cropGuideId;

		private DataGridViewTextBoxColumn pestGuideId;

		private DataGridViewTextBoxColumn pesticideId;

		private DataGridViewTextBoxColumn formcode1;

		private DataGridViewTextBoxColumn content;

		private DataGridViewTextBoxColumn MedName;

		private DataGridViewButtonColumn Column1;

		private DataGridViewTextBoxColumn code;

		private DataGridViewTextBoxColumn cropId;

		private DataGridViewTextBoxColumn pestId;

		private DataGridViewTextBoxColumn formcode;

		private DataGridViewTextBoxColumn Contents;

		public frmMedGuide(frmCropGuide fcg, frmMainShopSimple fms, frmPestGuide fpg)
			: base("收銀作業")
		{
			InitializeComponent();
			this.fcg = fcg;
			frs = fms;
			this.fpg = fpg;
			addMed(this.fpg.realcropcode, this.fpg.pestcode);
		}

		public frmMedGuide(frmCropGuide fcg, frmMainShopSimpleWithMoney fms, frmPestGuide fpg)
			: base("收銀作業")
		{
			InitializeComponent();
			this.fcg = fcg;
			frsm = fms;
			this.fpg = fpg;
			addMed(this.fpg.realcropcode, this.fpg.pestcode);
		}

		private void addMed(string cropId, string pestId)
		{
			List<MedInfo> list = new List<MedInfo>();
			string text = DateTime.Now.ToString("yyyyMMdd");
			string[] strWhereParameterArray = new string[3]
			{
				cropId,
				pestId,
				text
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hs.*", "HyScope as hs", "hs.cropId={0} and hs.pestId={1} AND hs.isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				bool flag = false;
				foreach (MedInfo item in list)
				{
					if (item._pesticideId.Equals(row["pesticideId"].ToString()) && item._contents.Equals(row["contents"].ToString()) && item._formCode.Equals(row["formCode"].ToString()))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					string[] strWhereParameterArray2 = new string[3]
					{
						row["pesticideId"].ToString(),
						row["formCode"].ToString(),
						row["contents"].ToString()
					};
					DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hl.pesticideEname,hl.licType,hl.licNo", "hypos_GOODSLST as hg, HyLicence as hl", "hg.licType = hl.licType and hg.domManufId = hl.licNo and hl.pesticideId ={0} and hl.formCode={1} and hl.contents={2} and hl.isDelete='N' and hg.status!='D'", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
					list.Add(new MedInfo(row["pesticideId"].ToString(), row["formCode"].ToString(), row["contents"].ToString()));
					if (dataTable2.Rows.Count > 0)
					{
						clslit3.Rows.Add(row["pesticideName"].ToString() + "\r\n" + dataTable2.Rows[0]["pesticideEname"].ToString() + " | " + row["formcode"].ToString() + " " + row["contents"].ToString(), "用藥說明", row["pesticideId"].ToString(), cropId, pestId, row["formCode"].ToString(), row["contents"].ToString());
					}
				}
			}
		}

		private void clslit3_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				string text = clslit3["code", e.RowIndex].Value.ToString();
				string text2 = clslit3["cropId", e.RowIndex].Value.ToString();
				string text3 = clslit3["pestId", e.RowIndex].Value.ToString();
				string formCode = clslit3["formcode", e.RowIndex].Value.ToString();
				string text4 = clslit3["Contents", e.RowIndex].Value.ToString();
				new dialogMedDescription(text, text2, text3, formCode, text4).ShowDialog();
				return;
			}
			barcodelist.Clear();
			clslit4.Rows.Clear();
			string text5 = clslit3.CurrentRow.Cells["cropId"].Value.ToString();
			string text6 = clslit3.CurrentRow.Cells["pestId"].Value.ToString();
			string text7 = clslit3.CurrentRow.Cells["code"].Value.ToString();
			string text8 = clslit3.CurrentRow.Cells["formcode"].Value.ToString();
			string text9 = clslit3.CurrentRow.Cells["Contents"].Value.ToString();
			string[] strWhereParameterArray = new string[3]
			{
				text7,
				text8,
				text9
			};
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "licType,licNo,pesticideName,domManufId,domManufName", "HyLicence", "pesticideId={0} and formCode={1} and contents={2} and isDelete='N'", "licNo", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dataTable.Rows)
			{
				string[] strWhereParameterArray2 = new string[2]
				{
					row["licNo"].ToString(),
					row["licType"].ToString()
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "spec,GDSNO,capacity,GDName", "hypos_GOODSLST", "domManufId={0} and licType ={1} and status !='D' ", "barcode", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				if (dataTable2.Rows.Count <= 0)
				{
					continue;
				}
				foreach (DataRow row2 in dataTable2.Rows)
				{
					clslit4.Rows.Add(row2["GDName"].ToString() + "[" + row["domManufName"].ToString() + "] \r\n" + row2["spec"].ToString() + " " + row2["capacity"].ToString(), row2["GDSNO"].ToString(), text5, text6, text7, text8, text9);
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			fpg.Show();
			Hide();
		}

		private void clslit4_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			bool flag = false;
			string text = DateTime.Now.ToString("yyyyMMdd");
			string text2 = clslit4.CurrentRow.Cells["cropGuideId"].Value.ToString();
			string text3 = clslit4.CurrentRow.Cells["pestGuideId"].Value.ToString();
			string text4 = clslit4.CurrentRow.Cells["pesticideId"].Value.ToString();
			string text5 = clslit4.CurrentRow.Cells["formcode1"].Value.ToString();
			string text6 = clslit4.CurrentRow.Cells["content"].Value.ToString();
			string[] strWhereParameterArray = new string[6]
			{
				text2,
				text3,
				text4,
				text5,
				text6,
				text
			};
			if (((DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hs.*", "HyScope as hs", "hs.cropId={0} and hs.pestId={1} and hs.pesticideId={2} and hs.formCode={3} and hs.contents={4}  and hs.regStoreName =''  and hs.isDelete in ('N','') ", "", null, strWhereParameterArray, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
			{
				string[] strWhereParameterArray2 = new string[1]
				{
					fpg.realcropcode
				};
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyCrop", "code={0}", "", null, strWhereParameterArray2, CommandOperationType.ExecuteReaderReturnDataTable);
				string[] strWhereParameterArray3 = new string[1]
				{
					fpg.pestcode
				};
				DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyBlight", "code={0}", "", null, strWhereParameterArray3, CommandOperationType.ExecuteReaderReturnDataTable);
				if (Program.SystemMode == 1)
				{
					frs.addOnecommodity(clslit4.CurrentRow.Cells[1].Value.ToString(), fpg.realcropcode, fpg.pestcode, dataTable.Rows[0]["name"].ToString(), dataTable2.Rows[0]["name"].ToString());
					frs.setfocus();
					frs.Show();
				}
				else
				{
					frsm.addOnecommodity(clslit4.CurrentRow.Cells[1].Value.ToString(), fpg.realcropcode, fpg.pestcode, dataTable.Rows[0]["name"].ToString(), dataTable2.Rows[0]["name"].ToString());
					frsm.setfocus();
					frsm.Show();
				}
				Hide();
				return;
			}
			string[] strWhereParameterArray4 = new string[6]
			{
				text2,
				text3,
				text4,
				text5,
				text6,
				text
			};
			DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hs.*", "HyScope as hs", "hs.cropId={0} and hs.pestId={1} and hs.pesticideId={2} and hs.formCode={3} and hs.contents={4}  and hs.approveDate != ''  and (hs.approveDate + 19190000) >= CAST ({5} as INTEGER)  and hs.regStoreName !=''  and hs.regStoreName !='99999999'  and hs.isDelete in ('N','') ", "", null, strWhereParameterArray4, CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable3.Rows.Count > 0)
			{
				flag = true;
				foreach (DataRow row in dataTable3.Rows)
				{
					string[] strWhereParameterArray5 = new string[4]
					{
						row["pesticideId"].ToString(),
						row["formCode"].ToString(),
						row["contents"].ToString(),
						row["regStoreName"].ToString()
					};
					DataTable dataTable4 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "hg.GDSNO", "HyLicence as hl,hypos_GOODSLST as hg", "hl.pesticideId ={0} and hl.formCode={1} and hl.contents={2} and hl.licType = hg.licType and hl.licNo = hg.domManufId and hl.domManufId in (" + row["regStoreName"].ToString() + ") and hl.isDelete='N' and hg.status !='D' ", "", null, strWhereParameterArray5, CommandOperationType.ExecuteReaderReturnDataTable);
					if (dataTable4.Rows.Count <= 0)
					{
						continue;
					}
					foreach (DataRow row2 in dataTable4.Rows)
					{
						barcodelist.Add(row2["GDSNO"].ToString());
					}
				}
			}
			if (flag && barcodelist.Count > 0)
			{
				bool flag2 = false;
				foreach (string item in barcodelist)
				{
					if (item.Equals(clslit4.CurrentRow.Cells[1].Value.ToString()))
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					string[] strWhereParameterArray6 = new string[1]
					{
						fpg.realcropcode
					};
					DataTable dataTable5 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyCrop", "code={0}", "", null, strWhereParameterArray6, CommandOperationType.ExecuteReaderReturnDataTable);
					string[] strWhereParameterArray7 = new string[1]
					{
						fpg.pestcode
					};
					DataTable dataTable6 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyBlight", "code={0}", "", null, strWhereParameterArray7, CommandOperationType.ExecuteReaderReturnDataTable);
					if (Program.SystemMode == 1)
					{
						frs.addOnecommodity(clslit4.CurrentRow.Cells[1].Value.ToString(), fpg.realcropcode, fpg.pestcode, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
						frs.setfocus();
						frs.Show();
					}
					else
					{
						frsm.addOnecommodity(clslit4.CurrentRow.Cells[1].Value.ToString(), fpg.realcropcode, fpg.pestcode, dataTable5.Rows[0]["name"].ToString(), dataTable6.Rows[0]["name"].ToString());
						frsm.setfocus();
						frsm.Show();
					}
					Hide();
					return;
				}
				List<string> list = new List<string>();
				foreach (string item2 in barcodelist)
				{
					string[] strWhereParameterArray8 = new string[1]
					{
						item2
					};
					DataTable dataTable7 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "brandName", "hypos_GOODSLST", "GDSNO={0}", "", null, strWhereParameterArray8, CommandOperationType.ExecuteReaderReturnDataTable);
					list.Add(dataTable7.Rows[0]["brandName"].ToString());
				}
				list = Enumerable.ToList(Enumerable.Distinct(list));
				string text7 = "";
				foreach (string item3 in list)
				{
					text7 = text7 + "[" + item3 + "]";
				}
				AutoClosingMessageBox.Show("此用藥配對尚於資料保護期間，僅可選擇" + text7 + "用藥");
			}
			else
			{
				string[] strWhereParameterArray9 = new string[1]
				{
					fpg.realcropcode
				};
				DataTable dataTable8 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyCrop", "code={0}", "", null, strWhereParameterArray9, CommandOperationType.ExecuteReaderReturnDataTable);
				string[] strWhereParameterArray10 = new string[1]
				{
					fpg.pestcode
				};
				DataTable dataTable9 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "name", "HyBlight", "code={0}", "", null, strWhereParameterArray10, CommandOperationType.ExecuteReaderReturnDataTable);
				if (Program.SystemMode == 1)
				{
					frs.addOnecommodity(clslit4.CurrentRow.Cells[1].Value.ToString(), fpg.realcropcode, fpg.pestcode, dataTable8.Rows[0]["name"].ToString(), dataTable9.Rows[0]["name"].ToString());
					frs.setfocus();
					frs.Show();
				}
				else
				{
					frsm.addOnecommodity(clslit4.CurrentRow.Cells[1].Value.ToString(), fpg.realcropcode, fpg.pestcode, dataTable8.Rows[0]["name"].ToString(), dataTable9.Rows[0]["name"].ToString());
					frsm.setfocus();
					frsm.Show();
				}
				Hide();
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
			label6 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			clslit4 = new System.Windows.Forms.DataGridView();
			clslit3 = new System.Windows.Forms.DataGridView();
			label8 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			cropname = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			panel4 = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			panel3 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			MedName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
			code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cropId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			pestId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			formcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
			Contents = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls4name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cls4code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			cropGuideId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			pestGuideId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			pesticideId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			formcode1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			content = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit4).BeginInit();
			((System.ComponentModel.ISupportInitialize)clslit3).BeginInit();
			panel5.SuspendLayout();
			panel4.SuspendLayout();
			panel3.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			pb_virtualKeyBoard.Size = new System.Drawing.Size(70, 150);
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label6.Location = new System.Drawing.Point(13, 146);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(94, 24);
			label6.TabIndex = 54;
			label6.Text = "用藥/商品";
			button1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			button1.Location = new System.Drawing.Point(872, 143);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(97, 33);
			button1.TabIndex = 71;
			button1.Text = "回上一步";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			clslit4.AllowUserToAddRows = false;
			clslit4.AllowUserToDeleteRows = false;
			clslit4.AllowUserToResizeColumns = false;
			clslit4.AllowUserToResizeRows = false;
			clslit4.Anchor = System.Windows.Forms.AnchorStyles.None;
			clslit4.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			clslit4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			clslit4.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			clslit4.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle.Font = new System.Drawing.Font("微軟正黑體", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			clslit4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			clslit4.Columns.AddRange(cls4name, cls4code, cropGuideId, pestGuideId, pesticideId, formcode1, content);
			clslit4.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			clslit4.DefaultCellStyle = dataGridViewCellStyle2;
			clslit4.EnableHeadersVisualStyles = false;
			clslit4.GridColor = System.Drawing.SystemColors.ActiveBorder;
			clslit4.Location = new System.Drawing.Point(499, 192);
			clslit4.MultiSelect = false;
			clslit4.Name = "clslit4";
			clslit4.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit4.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			clslit4.RowHeadersVisible = false;
			clslit4.RowTemplate.Height = 40;
			clslit4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			clslit4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			clslit4.Size = new System.Drawing.Size(479, 422);
			clslit4.TabIndex = 70;
			clslit4.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit4_CellContentClick);
			clslit3.AllowUserToAddRows = false;
			clslit3.AllowUserToDeleteRows = false;
			clslit3.AllowUserToResizeColumns = false;
			clslit3.AllowUserToResizeRows = false;
			clslit3.Anchor = System.Windows.Forms.AnchorStyles.None;
			clslit3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			clslit3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			clslit3.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			clslit3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			clslit3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			clslit3.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
			dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 12f);
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(6);
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			clslit3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			clslit3.Columns.AddRange(MedName, Column1, code, cropId, pestId, formcode, Contents);
			clslit3.Cursor = System.Windows.Forms.Cursors.Hand;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 15f);
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(163, 151, 117);
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			clslit3.DefaultCellStyle = dataGridViewCellStyle5;
			clslit3.EnableHeadersVisualStyles = false;
			clslit3.GridColor = System.Drawing.SystemColors.ActiveBorder;
			clslit3.Location = new System.Drawing.Point(17, 192);
			clslit3.MultiSelect = false;
			clslit3.Name = "clslit3";
			clslit3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(255, 208, 81);
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			clslit3.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			clslit3.RowHeadersVisible = false;
			clslit3.RowTemplate.Height = 40;
			clslit3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			clslit3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			clslit3.Size = new System.Drawing.Size(481, 422);
			clslit3.TabIndex = 69;
			clslit3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(clslit3_CellContentClick);
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label8.Location = new System.Drawing.Point(495, 519);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(86, 24);
			label8.TabIndex = 65;
			label8.Text = "【用藥】";
			label11.AutoSize = true;
			label11.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label11.Location = new System.Drawing.Point(570, 519);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(105, 24);
			label11.TabIndex = 66;
			label11.Text = "用藥害名稱";
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label9.Location = new System.Drawing.Point(248, 519);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(105, 24);
			label9.TabIndex = 63;
			label9.Text = "【病蟲害】";
			label10.AutoSize = true;
			label10.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label10.Location = new System.Drawing.Point(339, 519);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(105, 24);
			label10.TabIndex = 64;
			label10.Text = "病蟲害名稱";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label7.Location = new System.Drawing.Point(13, 519);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(86, 24);
			label7.TabIndex = 60;
			label7.Text = "【作物】";
			cropname.AutoSize = true;
			cropname.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			cropname.Location = new System.Drawing.Point(90, 519);
			cropname.Name = "cropname";
			cropname.Size = new System.Drawing.Size(86, 24);
			cropname.TabIndex = 62;
			cropname.Text = "作物名稱";
			panel5.BackColor = System.Drawing.Color.Transparent;
			panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel5.Controls.Add(label5);
			panel5.Location = new System.Drawing.Point(742, 38);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(231, 73);
			panel5.TabIndex = 77;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label5.ForeColor = System.Drawing.Color.Black;
			label5.Location = new System.Drawing.Point(62, 12);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(86, 48);
			label5.TabIndex = 3;
			label5.Text = "End\r\n返回收銀";
			panel4.BackColor = System.Drawing.Color.FromArgb(125, 156, 35);
			panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel4.Controls.Add(label4);
			panel4.Location = new System.Drawing.Point(511, 38);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(224, 73);
			panel4.TabIndex = 78;
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label4.ForeColor = System.Drawing.Color.White;
			label4.Location = new System.Drawing.Point(51, 12);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(132, 48);
			label4.TabIndex = 2;
			label4.Text = "Step3\r\n用藥/商品選擇";
			panel3.BackColor = System.Drawing.Color.White;
			panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel3.Controls.Add(label3);
			panel3.ForeColor = System.Drawing.Color.White;
			panel3.Location = new System.Drawing.Point(266, 38);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(238, 73);
			panel3.TabIndex = 79;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label3.ForeColor = System.Drawing.Color.Black;
			label3.Location = new System.Drawing.Point(67, 12);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(105, 48);
			label3.TabIndex = 2;
			label3.Text = "Step2\r\n病蟲害選擇";
			panel1.BackColor = System.Drawing.Color.White;
			panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.ForeColor = System.Drawing.Color.Black;
			panel1.Location = new System.Drawing.Point(17, 38);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(242, 73);
			panel1.TabIndex = 76;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label2.ForeColor = System.Drawing.Color.Black;
			label2.Location = new System.Drawing.Point(78, 12);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(86, 48);
			label2.TabIndex = 1;
			label2.Text = "Step1\r\n作物選擇";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("微軟正黑體", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 136);
			label1.Location = new System.Drawing.Point(78, 14);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(86, 48);
			label1.TabIndex = 0;
			label1.Text = "Step1\r\n作物選擇";
			MedName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			MedName.DefaultCellStyle = dataGridViewCellStyle7;
			MedName.HeaderText = "請選擇用藥";
			MedName.Name = "MedName";
			MedName.ReadOnly = true;
			MedName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			MedName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			Column1.HeaderText = "用藥說明";
			Column1.Name = "Column1";
			Column1.Width = 89;
			code.HeaderText = "農藥代號";
			code.Name = "code";
			code.Visible = false;
			cropId.HeaderText = "作物Id";
			cropId.Name = "cropId";
			cropId.Visible = false;
			pestId.HeaderText = "病蟲害id";
			pestId.Name = "pestId";
			pestId.Visible = false;
			formcode.HeaderText = "濟型";
			formcode.Name = "formcode";
			formcode.Visible = false;
			Contents.HeaderText = "含量";
			Contents.Name = "Contents";
			Contents.Visible = false;
			cls4name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(89, 124, 14);
			cls4name.DefaultCellStyle = dataGridViewCellStyle8;
			cls4name.HeaderText = "";
			cls4name.Name = "cls4name";
			cls4name.ReadOnly = true;
			cls4code.HeaderText = "代碼";
			cls4code.Name = "cls4code";
			cls4code.Visible = false;
			cropGuideId.HeaderText = "作物id";
			cropGuideId.Name = "cropGuideId";
			cropGuideId.Visible = false;
			pestGuideId.HeaderText = "病蟲害id";
			pestGuideId.Name = "pestGuideId";
			pestGuideId.Visible = false;
			pesticideId.HeaderText = "農藥id";
			pesticideId.Name = "pesticideId";
			pesticideId.Visible = false;
			formcode1.HeaderText = "濟型";
			formcode1.Name = "formcode1";
			formcode1.Visible = false;
			content.HeaderText = "含量";
			content.Name = "content";
			content.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(981, 662);
			base.Controls.Add(panel5);
			base.Controls.Add(panel4);
			base.Controls.Add(panel3);
			base.Controls.Add(panel1);
			base.Controls.Add(button1);
			base.Controls.Add(clslit4);
			base.Controls.Add(clslit3);
			base.Controls.Add(label11);
			base.Controls.Add(label8);
			base.Controls.Add(label10);
			base.Controls.Add(label9);
			base.Controls.Add(cropname);
			base.Controls.Add(label7);
			base.Controls.Add(label6);
			base.Name = "frmMedGuide";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Form3";
			base.Controls.SetChildIndex(label6, 0);
			base.Controls.SetChildIndex(label7, 0);
			base.Controls.SetChildIndex(cropname, 0);
			base.Controls.SetChildIndex(label9, 0);
			base.Controls.SetChildIndex(label10, 0);
			base.Controls.SetChildIndex(label8, 0);
			base.Controls.SetChildIndex(label11, 0);
			base.Controls.SetChildIndex(clslit3, 0);
			base.Controls.SetChildIndex(clslit4, 0);
			base.Controls.SetChildIndex(button1, 0);
			base.Controls.SetChildIndex(pb_virtualKeyBoard, 0);
			base.Controls.SetChildIndex(panel1, 0);
			base.Controls.SetChildIndex(panel3, 0);
			base.Controls.SetChildIndex(panel4, 0);
			base.Controls.SetChildIndex(panel5, 0);
			((System.ComponentModel.ISupportInitialize)pb_virtualKeyBoard).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit4).EndInit();
			((System.ComponentModel.ISupportInitialize)clslit3).EndInit();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
