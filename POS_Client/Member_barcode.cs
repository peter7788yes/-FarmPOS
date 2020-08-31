using BarcodeLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class Member_barcode : Form
	{
		public static class myPrinters
		{
			[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern bool SetDefaultPrinter(string Name);
		}

		private static string _ExePath;

		private int _pageRecordCount;

		private List<string> _vipNoList;

		private string _printerType;

		private string _printerName;

		private IContainer components;

		private WebBrowser webBrowser1;

		private Button btnPrint;

		private Button btnPrintView;

		private Button btn_close;

		public Member_barcode(List<string> vipNoList, int pageRecordCount)
		{
			InitializeComponent();
			if (!Directory.Exists("TempBarCode"))
			{
				Directory.CreateDirectory("TempBarCode");
			}
			_pageRecordCount = pageRecordCount;
			_vipNoList = vipNoList;
			_ExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string sql = "SELECT BarcodeListType, BarcodeListPrinterName FROM hypos_PrinterManage ";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
			_printerType = dataTable.Rows[0]["BarcodeListType"].ToString();
			_printerName = dataTable.Rows[0]["BarcodeListPrinterName"].ToString();
			for (int i = 0; i < _vipNoList.Count; i++)
			{
				string text = _vipNoList[i].Trim();
				string str = text + ".gif";
				string imagePath = _ExePath + "\\TempBarCode\\" + str;
				Barcode barcode = new Barcode();
				barcode.IncludeLabel = true;
				barcode.LabelFont = new Font("Verdana", 8f);
				barcode.Width = 156;
				barcode.Height = 63;
				SaveImage(barcode.Encode(TYPE.CODE128, text, barcode.Width, barcode.Height), imagePath);
			}
		}

		private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			string text = "width:170mm;";
			string text2 = "min-height:244mm;";
			string text3 = "font-size: 1.15em;";
			if (_pageRecordCount == 6 || _pageRecordCount == 12)
			{
				text3 = "";
			}
			string documentText = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\"/><style>body{width:100%;height:100%;margin:0;padding:0;background-color:#FAFAFA;font-family:\"微軟正黑體\", Microsoft JhengHei;} *{box-sizing: border-box;-moz-box-sizing: border-box;} .page{" + text + text2 + "padding:10mm;margin:10mm auto;border:1px #D3D3D3 solid;border-radius:5px;background:white;box-shadow:0 0 5px rgba(0, 0, 0, 0.1);} .subpage{padding: 1cm;border: 5px red solid;height: 257mm;outline: 2cm #FFEAEA solid;} table{border-collapse:collapse;border-spacing:0;border:1px dotted #666;background-color:#FFF;" + text3 + "margin:0 0 20px 0;width:100%;} th{text-align:right;border:1px dotted #666;padding:5px;} td{text-align:left;border:1px dotted #666;padding:5px;} @page{size:A4;margin:0;} @media print{html,body{width:210mm;height:297mm;} .page{border:initial;border-radius:initial;width:initial;min-height:initial;box-shadow:initial;background:initial;}}</style></head><body style=\"margin:0;padding:0;overflow:auto;\"><div class=\"book\">" + divPage() + "</body></html>";
			webBrowser1.DocumentText = documentText;
			webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted_1);
		}

		private string divPage()
		{
			string text = "";
			string text2 = "VipNo in (";
			for (int i = 0; i < _vipNoList.Count; i++)
			{
				text2 = text2 + "{" + i + "},";
			}
			text2 = text2.Substring(0, text2.Length - 1) + ")";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "*", "hypos_CUST_RTL", text2, "", null, _vipNoList.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
			if (dataTable.Rows.Count > 0 && (_pageRecordCount == 3 || _pageRecordCount == 6 || _pageRecordCount == 12))
			{
				int num = _vipNoList.Count / _pageRecordCount;
				num = ((_vipNoList.Count <= _pageRecordCount) ? 1 : (num + 1));
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					int num2 = (j + 1) / _pageRecordCount;
					if ((j + 1) % _pageRecordCount != 0 || j < _pageRecordCount - 1)
					{
						num2++;
					}
					text = tableConent(text, _pageRecordCount, dataTable, j, num2, num);
				}
				text += "</div>";
			}
			return text;
		}

		private string tableConent(string result, int pageRecordCount, DataTable dt, int i, int up, int un)
		{
			if (pageRecordCount == 3)
			{
				Console.WriteLine("10 ::: " + (pageRecordCount == 3));
				if (i == (up - 1) * pageRecordCount)
				{
					result = result + "<div class=\"page\">會員清冊．Page " + up + "/" + un;
				}
				result = result + "<table summary=\"資料表格\">" + content3(dt, i) + "</table>";
				if (i + 1 == dt.Rows.Count || i == up * pageRecordCount - 1)
				{
					result += "</div>";
				}
			}
			if (pageRecordCount == 6)
			{
				if (i == (up - 1) * pageRecordCount)
				{
					result = result + "<div class=\"page\">會員清冊．Page " + up + "/" + un + "<table style=\"font-size:.9em;\">";
				}
				if ((i + 1) % 2 == 1)
				{
					result += "<tr>";
				}
				result = result + "<td style=\"width:50%;\"><table summary=\"資料表格\">" + content6(dt, i) + "</table></td>";
				if (i + 1 == dt.Rows.Count && dt.Rows.Count % 2 == 1)
				{
					result += "<td style=\"width:50%;\"></td>";
				}
				if ((i + 1) % 2 == 0 || i + 1 == dt.Rows.Count)
				{
					result += "</tr>";
				}
				if (i + 1 == dt.Rows.Count || i == up * pageRecordCount - 1)
				{
					result += "</table></div>";
				}
			}
			if (pageRecordCount == 12)
			{
				if (i == (up - 1) * pageRecordCount)
				{
					result = result + "<div class=\"page\">會員清冊．Page " + up + "/" + un + "<table style=\"font-size:.75em;\">";
				}
				string text = "";
				if ((i + 1) % 3 == 1)
				{
					text = " style=\"width:33%;vertical-align:top;\"";
					result += "<tr>";
				}
				if ((i + 1) % 3 == 2)
				{
					text = " style=\"width:33%;\"";
				}
				result = result + "<td" + text + "><table summary=\"資料表格\">" + content12(dt, i) + "</table></td>";
				if (i + 1 == dt.Rows.Count && (dt.Rows.Count % 3 == 1 || dt.Rows.Count % 3 == 2))
				{
					result += "<td style=\"width:33%;vertical-align:top;\"></td><td style=\"width:33%;\"></td><td></td>";
				}
				if ((i + 1) % 3 == 0 || i + 1 == dt.Rows.Count)
				{
					result += "</tr>";
				}
				if (i + 1 == dt.Rows.Count || i == up * pageRecordCount - 1)
				{
					result += "</table></div>";
				}
			}
			return result;
		}

		private string content3(DataTable dt, int i)
		{
			string text = dt.Rows[i]["LicenseCode"].ToString();
			string text2 = dt.Rows[i]["VipNo"].ToString();
			string str = text2 + ".gif";
			string text3 = dt.Rows[i]["Name"].ToString();
			string text4 = dt.Rows[i]["BirthDate"].ToString();
			string text5 = dt.Rows[i]["Mobile"].ToString();
			string text6 = dt.Rows[i]["CompanyIdNo"].ToString();
			string[] array = new string[5]
			{
				"會員號",
				"會員姓名",
				"出生日期",
				"電話號碼",
				"統一編號"
			};
			string[] array2 = new string[5]
			{
				text2,
				text3,
				text4,
				text5,
				text6
			};
			string text7 = _ExePath + "\\TempBarCode\\" + str;
			string text8 = "<tr><td rowspan=\"7\" style=\"width:15%;\"><img src=\"" + text7 + "\" style=\"height:75px;\"/></td><th style=\"width:25%;\">門市代號</th><td>" + text + "</td></tr>";
			for (int j = 0; j < array.Length; j++)
			{
				text8 = text8 + "<tr><th>" + array[j] + "</th><td>" + array2[j] + "</td></tr>";
			}
			return text8;
		}

		private string content6(DataTable dt, int i)
		{
			string text = dt.Rows[i]["VipNo"].ToString();
			string str = text + ".gif";
			string text2 = dt.Rows[i]["Name"].ToString();
			string text3 = dt.Rows[i]["Mobile"].ToString();
			string text4 = dt.Rows[i]["CompanyIdNo"].ToString();
			string[] array = new string[4]
			{
				"會員號",
				"會員姓名",
				"電話號碼",
				"統一編號"
			};
			string[] array2 = new string[4]
			{
				text,
				text2,
				text3,
				text4
			};
			string str2 = _ExePath + "\\TempBarCode\\" + str;
			string text5 = "<tr><td colspan=\"2\" style=\"text-align:center;height:150px;\"><img src=\"" + str2 + "\" style=\"width:200px;\"/></td></tr>";
			for (int j = 0; j < array.Length; j++)
			{
				string text6 = "";
				if (j == 0)
				{
					text6 = " style=\"width:35%;\"";
				}
				text5 = text5 + "<tr><th" + text6 + ">" + array[j] + "</th><td>" + array2[j] + "</td></tr>";
			}
			return text5;
		}

		private string content12(DataTable dt, int i)
		{
			string str = dt.Rows[i]["VipNo"].ToString() + ".gif";
			string text = dt.Rows[i]["Name"].ToString();
			string text2 = dt.Rows[i]["Mobile"].ToString();
			string text3 = dt.Rows[i]["CompanyIdNo"].ToString();
			string[] array = new string[3]
			{
				"會員姓名",
				"電話號碼",
				"統一編號"
			};
			string[] array2 = new string[3]
			{
				text,
				text2,
				text3
			};
			string str2 = _ExePath + "\\TempBarCode\\" + str;
			string text4 = "<tr><td colspan=\"2\" style=\"text-align:center;height:110px;\"><img src=\"" + str2 + "\" style=\"width:150px;\"/></td></tr>";
			for (int j = 0; j < array.Length; j++)
			{
				string text5 = "";
				if (j == 0)
				{
					text5 = " style=\"width:33%;\"";
				}
				text4 = text4 + "<tr><th" + text5 + ">" + array[j] + "</th><td>" + array2[j] + "</td></tr>";
			}
			return text4;
		}

		private static void SaveImage(Image image, string imagePath)
		{
			image.Save(imagePath, ImageFormat.Gif);
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			string text = null;
			try
			{
				text = new PrintDocument().PrinterSettings.PrinterName;
				if (!string.IsNullOrEmpty(_printerName))
				{
					myPrinters.SetDefaultPrinter(_printerName);
				}
				IEPageSetup();
				webBrowser1.Print();
				AutoClosingMessageBox.Show("列印完成", 1000);
			}
			catch (Exception ex)
			{
				Console.WriteLine("列印錯誤 ::: " + ex.Message);
			}
			finally
			{
				if (!string.IsNullOrEmpty(text))
				{
					myPrinters.SetDefaultPrinter(text);
				}
			}
		}

		private void btnPrintView_Click(object sender, EventArgs e)
		{
			string text = null;
			try
			{
				text = new PrintDocument().PrinterSettings.PrinterName;
				if (!string.IsNullOrEmpty(_printerName))
				{
					myPrinters.SetDefaultPrinter(_printerName);
				}
				IEPageSetup();
				webBrowser1.ShowPrintPreviewDialog();
				AutoClosingMessageBox.Show("列印預覽完成", 1000);
			}
			catch (Exception ex)
			{
				Console.WriteLine("列印預覽錯誤 ::: " + ex.Message);
			}
			finally
			{
				if (!string.IsNullOrEmpty(text))
				{
					myPrinters.SetDefaultPrinter(text);
				}
			}
		}

		private void IEPageSetup()
		{
			string name = "Software\\Microsoft\\Internet Explorer\\PageSetup";
			bool writable = true;
			object obj = "";
			object obj2 = "0.395670";
			object obj3 = "0.166540";
			string[] array = new string[6]
			{
				"footer",
				"header",
				"margin_bottom",
				"margin_left",
				"margin_right",
				"margin_top"
			};
			object[] array2 = new object[6]
			{
				obj,
				obj,
				obj2,
				obj3,
				obj3,
				obj3
			};
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name, writable);
			for (int i = 0; i < array.Length; i++)
			{
				registryKey.SetValue(array[i], array2[i]);
			}
			registryKey.Close();
		}

		private void btn_close_Click(object sender, EventArgs e)
		{
			Close();
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
			webBrowser1 = new System.Windows.Forms.WebBrowser();
			btnPrint = new System.Windows.Forms.Button();
			btnPrintView = new System.Windows.Forms.Button();
			btn_close = new System.Windows.Forms.Button();
			SuspendLayout();
			webBrowser1.Location = new System.Drawing.Point(22, 56);
			webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			webBrowser1.Name = "webBrowser1";
			webBrowser1.Size = new System.Drawing.Size(974, 698);
			webBrowser1.TabIndex = 2;
			webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
			webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted_1);
			btnPrint.Location = new System.Drawing.Point(22, 12);
			btnPrint.Name = "btnPrint";
			btnPrint.Size = new System.Drawing.Size(75, 38);
			btnPrint.TabIndex = 3;
			btnPrint.Text = "立即列印";
			btnPrint.UseVisualStyleBackColor = true;
			btnPrint.Click += new System.EventHandler(btnPrint_Click);
			btnPrintView.Location = new System.Drawing.Point(103, 12);
			btnPrintView.Name = "btnPrintView";
			btnPrintView.Size = new System.Drawing.Size(75, 38);
			btnPrintView.TabIndex = 4;
			btnPrintView.Text = "預覽列印";
			btnPrintView.UseVisualStyleBackColor = true;
			btnPrintView.Click += new System.EventHandler(btnPrintView_Click);
			btn_close.Location = new System.Drawing.Point(220, 12);
			btn_close.Name = "btn_close";
			btn_close.Size = new System.Drawing.Size(75, 38);
			btn_close.TabIndex = 5;
			btn_close.Text = "關閉視窗";
			btn_close.UseVisualStyleBackColor = true;
			btn_close.Click += new System.EventHandler(btn_close_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1017, 766);
			base.Controls.Add(btn_close);
			base.Controls.Add(btnPrintView);
			base.Controls.Add(btnPrint);
			base.Controls.Add(webBrowser1);
			base.Name = "Member_barcode";
			Text = "Member_BarCode";
			ResumeLayout(false);
		}
	}
}
