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
	public class frmPrintHotKey : Form
	{
		public static class myPrinters
		{
			[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern bool SetDefaultPrinter(string Name);
		}

		private static string _ExePath;

		private bool _member;

		private bool _cash;

		private bool _shop;

		private bool _returnM;

		private string memberStr;

		private string cashStr;

		private string shopStr;

		private string returnM_Str;

		private string _printerType;

		private string _printerName;

		private IContainer components;

		private Button Print;

		private Button PrintShow;

		private WebBrowser webBrowser1;

		private Button btn_close;

		public frmPrintHotKey(bool member, bool cash, bool shop, bool returnM)
		{
			InitializeComponent();
			_member = member;
			_cash = cash;
			_shop = shop;
			_returnM = returnM;
			if (!Directory.Exists("TempBarCode"))
			{
				Directory.CreateDirectory("TempBarCode");
			}
			_ExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string sql = "SELECT BarcodeListType, BarcodeListPrinterName FROM hypos_PrinterManage ";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
			_printerType = dataTable.Rows[0]["BarcodeListType"].ToString();
			_printerName = dataTable.Rows[0]["BarcodeListPrinterName"].ToString();
			if (_member)
			{
				memberStr = "CTRLM";
				CreateBarCode(memberStr);
			}
			if (_cash)
			{
				cashStr = "CTRLO";
				CreateBarCode(cashStr);
			}
			if (_shop)
			{
				shopStr = "CTRLSHIFTS";
				CreateBarCode(shopStr);
			}
			if (_returnM)
			{
				returnM_Str = "CTRLSHIFTR";
				CreateBarCode(returnM_Str);
			}
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			string text = "width:170mm;";
			string text2 = "min-height:244mm;";
			string documentText = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\"/><style>body{width:100%;height:100%;margin:0;padding:0;background-color:#FAFAFA;font-family:\"微軟正黑體\", Microsoft JhengHei;} *{box-sizing: border-box;-moz-box-sizing: border-box;} .page{" + text + text2 + "padding:10mm;margin:10mm auto;border:1px #D3D3D3 solid;border-radius:5px;background:white;box-shadow:0 0 5px rgba(0, 0, 0, 0.1);} .subpage{padding: 1cm;border: 5px red solid;height: 257mm;outline: 2cm #FFEAEA solid;} table{border-collapse:collapse;border-spacing:0;border:1px dotted #666;background-color:#FFF;font-size:1.15em;margin:0 0 20px 0;width:100%;} th{text-align:right;border:1px dotted #666;padding:5px;} td{text-align:left;border:1px dotted #666;padding:5px;} @page{size:A4;margin:0;} @media print{html,body{width:210mm;height:297mm;} .page{border:initial;border-radius:initial;width:initial;min-height:initial;box-shadow:initial;background:initial;}}</style></head><body style=\"margin:0;padding:0;overflow:auto;\"><div class=\"book\">" + divPage() + "</div></body></html>";
			webBrowser1.DocumentText = documentText;
			webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
		}

		private string divPage()
		{
			string str = "<div class=\"page\">常用功能快捷．Page 1/1<table style=\"font-size:.9em;\">";
			List<string> list = new List<string>();
			if (_member)
			{
				list.Add(memberStr);
			}
			if (_cash)
			{
				list.Add(cashStr);
			}
			if (_shop)
			{
				list.Add(shopStr);
			}
			if (_returnM)
			{
				list.Add(returnM_Str);
			}
			for (int i = 0; i < list.Count; i++)
			{
				if ((i + 1) % 2 == 1)
				{
					str += "<tr>";
				}
				str += tableContent(list[i]);
				if ((i + 1) % 2 == 1 && i + 1 == list.Count)
				{
					str += "<td style=\"width:50%;\"><table summary=\"資料表格\"></table></td>";
				}
				if ((i + 1) % 2 == 0 || ((i + 1) % 2 == 1 && i + 1 == list.Count))
				{
					str += "</tr>";
				}
			}
			return str + "</table></div>";
		}

		private string tableContent(string codeStr)
		{
			string text = _ExePath + "\\TempBarCode\\" + codeStr + ".gif";
			string str = "<td style=\"width:50%;\"><table summary=\"資料表格\">";
			string[] array = null;
			if ("CTRLM".Equals(codeStr))
			{
				array = new string[3]
				{
					text,
					"銷售對象(會員)條碼輸入",
					"刷入此功能條碼後可直接進行會員條碼的刷入(需進入銷售功能)"
				};
			}
			if ("CTRLO".Equals(codeStr))
			{
				array = new string[3]
				{
					text,
					"現金收銀(預設全額)",
					"刷入此功能條碼後可直接結帳，預設全額現金付款(需進入銷售功能)"
				};
			}
			if ("CTRLSHIFTS".Equals(codeStr))
			{
				array = new string[3]
				{
					text,
					"銷售作業",
					"刷入此功能條碼後可直接進入銷售主頁(需先登入)"
				};
			}
			if ("CTRLSHIFTR".Equals(codeStr))
			{
				array = new string[3]
				{
					text,
					"退貨作業",
					"刷入此功能條碼後可直接進入退貨主頁(需先登入)"
				};
			}
			for (int i = 0; i < array.Length; i++)
			{
				str = ((i != 0) ? (str + "<tr><td>" + array[i] + "</td></tr>") : (str + "<tr><td style=\"text-align:center;\"><img src=\"" + array[0] + "\" style=\"height:65px;\"/></td></tr>"));
			}
			return str + "</table></td>";
		}

		private void CreateBarCode(string code)
		{
			string str = code + ".gif";
			string filename = "TempBarCode\\" + str;
			Barcode barcode = new Barcode();
			barcode.IncludeLabel = true;
			barcode.LabelFont = new Font("Verdana", 8f);
			barcode.Width = 156;
			barcode.Height = 63;
			barcode.Encode(TYPE.CODE128, code, barcode.Width, barcode.Height).Save(filename, ImageFormat.Gif);
		}

		private void PrintShow_Click(object sender, EventArgs e)
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

		private void Print_Click(object sender, EventArgs e)
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
			Print = new System.Windows.Forms.Button();
			PrintShow = new System.Windows.Forms.Button();
			webBrowser1 = new System.Windows.Forms.WebBrowser();
			btn_close = new System.Windows.Forms.Button();
			SuspendLayout();
			Print.Location = new System.Drawing.Point(22, 12);
			Print.Name = "Print";
			Print.Size = new System.Drawing.Size(75, 38);
			Print.TabIndex = 0;
			Print.Text = "立即列印";
			Print.UseVisualStyleBackColor = true;
			Print.Click += new System.EventHandler(Print_Click);
			PrintShow.Location = new System.Drawing.Point(103, 12);
			PrintShow.Name = "PrintShow";
			PrintShow.Size = new System.Drawing.Size(75, 38);
			PrintShow.TabIndex = 1;
			PrintShow.Text = "預覽列印";
			PrintShow.UseVisualStyleBackColor = true;
			PrintShow.Click += new System.EventHandler(PrintShow_Click);
			webBrowser1.Location = new System.Drawing.Point(22, 56);
			webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			webBrowser1.Name = "webBrowser1";
			webBrowser1.Size = new System.Drawing.Size(974, 698);
			webBrowser1.TabIndex = 2;
			webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
			webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
			btn_close.Location = new System.Drawing.Point(220, 12);
			btn_close.Name = "btn_close";
			btn_close.Size = new System.Drawing.Size(75, 38);
			btn_close.TabIndex = 3;
			btn_close.Text = "關閉視窗";
			btn_close.UseVisualStyleBackColor = true;
			btn_close.Click += new System.EventHandler(btn_close_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1017, 766);
			base.Controls.Add(btn_close);
			base.Controls.Add(webBrowser1);
			base.Controls.Add(PrintShow);
			base.Controls.Add(Print);
			base.Name = "frmPrintHotKey";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "frmPrintHotKey";
			ResumeLayout(false);
		}
	}
}
