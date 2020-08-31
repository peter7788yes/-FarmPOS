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
	public class frmPrint_DeliveryDoc : Form
	{
		public static class myPrinters
		{
			[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern bool SetDefaultPrinter(string Name);
		}

		private static string _ExePath;

		private string _shopName;

		private string _DeliveryNo;

		private string _sellType;

		private DataTable _detailSell;

		private List<string> _detailList;

		private Dictionary<string, int> _barCodeMap;

		private DataTable _barCode;

		private Dictionary<string, int> _cropMap;

		private DataTable _cropList;

		private Dictionary<string, int> _blightMap;

		private DataTable _blightList;

		private string _imagePath;

		private string _printerType;

		private string _printerName;

		private string _version;

		private IContainer components;

		private WebBrowser webBrowser1;

		private Button btnPrint;

		private Button btnPrintView;

		private Button btn_close;

		public frmPrint_DeliveryDoc(string DeliveryNo)
		{
			InitializeComponent();
			_DeliveryNo = DeliveryNo;
			CommonUtilities();
		}

		public frmPrint_DeliveryDoc(string sellNo, int version, string sellType)
		{
			InitializeComponent();
			_version = version.ToString();
			_DeliveryNo = sellNo;
			_sellType = sellType;
			CommonUtilities();
		}

		private void CommonUtilities()
		{
			if (!Directory.Exists("TempBarCode"))
			{
				Directory.CreateDirectory("TempBarCode");
			}
			_ExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string sql = "SELECT ShopName FROM hypos_RegisterLicense where isApproved = 'Y' order by CreateDate desc limit 1";
			_shopName = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			string sql2 = "SELECT barcode, num, sellingPrice, subtotal, BatchNo, MFGDate, POSBatchNo FROM hypos_DeliveryGoods_Detail where DeliveryNo='" + _DeliveryNo + "'";
			DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteReaderReturnDataTable);
			List<string> list = new List<string>();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				list.Add(dataTable.Rows[i]["barcode"].ToString());
			}
			_detailSell = dataTable;
			_detailList = list;
			string strSelectField = "GDSNO,GDName,CName,contents,brandName,spec,capacity,formCode";
			string text = "GDSNO in (";
			for (int j = 0; j < _detailList.Count; j++)
			{
				text = text + "{" + j + "},";
			}
			text = text.Substring(0, text.Length - 1) + ")";
			DataTable dataTable2 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, strSelectField, "hypos_GOODSLST", text, "", null, _detailList.ToArray(), CommandOperationType.ExecuteReaderReturnDataTable);
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			for (int k = 0; k < dataTable2.Rows.Count; k++)
			{
				dictionary.Add(dataTable2.Rows[k]["GDSNO"].ToString(), k);
			}
			_barCode = dataTable2;
			_barCodeMap = dictionary;
			string sql3 = "SELECT ReceiveType, ReceivePrinterName FROM hypos_PrinterManage ";
			DataTable dataTable3 = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql3, null, CommandOperationType.ExecuteReaderReturnDataTable);
			_printerType = "A4";
			_printerName = dataTable3.Rows[0]["ReceivePrinterName"].ToString();
			string str = _DeliveryNo + ".gif";
			string filename = _imagePath = _ExePath + "\\TempBarCode\\" + str;
			Barcode barcode = new Barcode();
			barcode.IncludeLabel = true;
			barcode.LabelFont = new Font("Verdana", 8f);
			barcode.Width = 181;
			barcode.Height = 54;
			barcode.Encode(TYPE.CODE128, _DeliveryNo, barcode.Width, barcode.Height).Save(filename, ImageFormat.Gif);
		}

		private DataTable getMemberList()
		{
			string sql = "SELECT c.SupplierName, c.SupplierIdNo, c.ContactName, c.Mobile, c.CityNo, c.Zipcode, c.Address, c.vendorId, c.vendorName, m.items, m.itemstotal, m.CurSum, m.sumDiscount, m.changcount, m.DeliveryDate FROM hypos_DeliveryGoods_Master as m , hypos_Supplier as c where m.DeliveryNo='" + _DeliveryNo + "'  and (m.vendorNo = c.SupplierNo)";
			return (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
		}

		private DataTable getMainSellList()
		{
			return (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Select, "items,itemstotal,CurSum,sumDiscount,changcount,DeliveryDate,DeliveryCustomNo", "hypos_DeliveryGoods_Master", "DeliveryNo={0}", "", null, new string[1]
			{
				_DeliveryNo
			}, CommandOperationType.ExecuteReaderReturnDataTable);
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			string documentText = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta content=\"text/html;charset=utf-8\" http-equiv=\"Content-Type\"/><style>" + pageSize(_printerType) + "</style></head><body style=\"margin:0;padding:0;overflow:auto;\"><div class=\"book\">" + divPage(_detailSell) + "</div></body></html>";
			webBrowser1.DocumentText = documentText;
			webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
		}

		private string divPage(DataTable dt)
		{
			string result = "";
			int count = dt.Rows.Count;
			if (count > 0)
			{
				int num = count / 14;
				num = ((count <= 6) ? 1 : ((count % 14 != 0) ? (num + 2) : (num + 1)));
				for (int i = 0; i < count; i++)
				{
					int num2 = i + 1;
					int num3 = (num2 - 6) / 8;
					num3 = ((num2 <= 6) ? 1 : ((num2 > 6 && num2 < 15) ? 2 : (((num2 - 6) % 8 != 0) ? (num3 + 2) : (num3 + 1))));
					DataTable memberList = getMemberList();
					result = tableContent_A4(result, dt, i, num, num3, memberList);
				}
			}
			return result;
		}

		private string tableContent_A4(string result, DataTable dt, int i, int un, int up, DataTable member)
		{
			int count = dt.Rows.Count;
			DataTable mainSellList = getMainSellList();
			if (i + 1 == 1 || i + 1 == 7 + (up - 2) * 8)
			{
				result = result + "<div class=\"page\">Page " + up + "/" + un + "<div align=\"center\" style=\"font-size:24px;\"> 出貨單 </div>";
			}
			if (i + 1 == 1)
			{
				string deliveryNo = _DeliveryNo;
				if (!string.IsNullOrEmpty(_version))
				{
					deliveryNo = deliveryNo + "(v" + _version + ")";
				}
				string text = Program.LincenseCode + " " + _shopName;
				string text2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				string text3 = "";
				string text4 = mainSellList.Rows[0]["DeliveryDate"].ToString();
				if (member.Rows.Count > 0)
				{
					getAddress("A4");
					member.Rows[0]["SupplierName"].ToString();
					member.Rows[0]["Mobile"].ToString();
					member.Rows[0]["SupplierIdNo"].ToString();
					text3 = "";
					if (!string.IsNullOrEmpty(text3))
					{
						DateTime dateTime = Convert.ToDateTime(text3);
						(new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks).Days / 365).ToString();
					}
				}
				else
				{
					text3 = "";
				}
				result = result + "<table summary=\"資料表格\" style=\"font-size:1em;\"><tr><td rowspan=\"2\" style=\"width:30%;text-align:center;vertical-align:middle;\"><div style=\"overflow:hidden;height:65px;\"><img src=\"" + _imagePath + "\" style=\"max-width:215px;\"/></div></td><th style=\"width:10%;\">店家</th><td colspan=\"3\">" + text + "</td></tr><tr><th>出貨日期</th><td>" + text4 + "</td><th style=\"width:15%;\">列印日期</th><td>" + text2 + "</td></tr></table>";
			}
			if (i + 1 == 1)
			{
				string text5 = mainSellList.Rows[0]["DeliveryCustomNo"].ToString();
				string text6 = member.Rows[0]["SupplierIdNo"].ToString();
				string text7 = member.Rows[0]["ContactName"].ToString();
				string text8 = member.Rows[0]["vendorId"].ToString();
				string text9 = member.Rows[0]["vendorName"].ToString();
				string text10 = member.Rows[0]["Mobile"].ToString();
				string vendorAddress = getVendorAddress();
				result = result + "<table style=\"width:100%;font-size:1em;\"><tr><th style=\"width:15%;\">出貨單號</th><td style=\"width:18%;\">" + text5 + "</td><th style=\"width:15%;\">客戶名稱</th><td>" + text8 + text9 + "</td></tr><tr><th style=\"width:15%;\">統一編號</th><td style=\"width:18%;\">" + text6 + "</td><th style=\"width:15%;\">聯繫電話</th><td>" + text10 + "</td></tr><tr><th style=\"width:15%;\">聯絡人</th><td style=\"vertical-align:middle;\">" + text7 + "</td><th style=\"width:15%;\">聯繫住址</th><td>" + vendorAddress + "</td></tr></table>";
			}
			if (i + 1 == 1 || i + 1 == 7 + (up - 2) * 8)
			{
				result += "<table style=\"width:100%;font-size:1em;\"><tr>";
				string[] array = null;
				string[] array2 = null;
				array = new string[6]
				{
					"&nbsp;",
					"商品名稱",
					"售價",
					"數量",
					"合計",
					"批號/製造日期"
				};
				array2 = new string[7]
				{
					"width:5%;",
					"text-align:center;",
					"text-align:center;width:7%;font-size:.85em;",
					"text-align:center;width:7%;font-size:.85em;",
					"text-align:center;width:7%;font-size:.85em;",
					"text-align:center;width:7%;font-size:.85em;",
					"text-align:center; width:15%; font-size:.85em;"
				};
				for (int j = 0; j < array.Length; j++)
				{
					result = result + "<th style=\"" + array2[j] + "\">" + array[j] + "</th>";
				}
				result += "</tr>";
			}
			string text11 = dt.Rows[i]["barcode"].ToString();
			string text12 = dt.Rows[i]["num"].ToString();
			string noteMore = getNoteMore(dt, i, text11, "A4");
			int index = int.Parse(_barCodeMap[text11].ToString());
			string text13 = _barCode.Rows[index]["spec"].ToString();
			string text14 = _barCode.Rows[index]["capacity"].ToString();
			string text15 = dt.Rows[i]["sellingPrice"].ToString();
			string text16 = dt.Rows[i]["subtotal"].ToString();
			result = result + "<tr><td class=\"aCenter\">" + (i + 1) + "</td><td class=\"title\"><div class=\"text-overflow of2\"><div class=\"code\">" + text11 + "</div><div class=\"productname\">" + commodityName(text11) + "<br/><span class=\"unit\">" + text13 + text14 + "</span></div></div></td><td class=\"number\">" + text15 + "</td><td class=\"number\">" + text12 + "</td><td class=\"number total\">" + text16 + "</td><td class=\"number total\">" + noteMore + "</td></tr>";
			if (i + 1 == 6 + (up - 1) * 8 || i + 1 == count)
			{
				result += "</table>";
			}
			if (i + 1 == count)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				if (member.Rows.Count > 0)
				{
					num = int.Parse(member.Rows[0]["items"].ToString());
					num2 = int.Parse(member.Rows[0]["itemstotal"].ToString());
					num3 = int.Parse(member.Rows[0]["CurSum"].ToString());
					num4 = int.Parse(member.Rows[0]["sumDiscount"].ToString());
				}
				result = result + "<table style=\"width:100%;font-size:1em;\"><tr><th style=\"width:15%;\">總計</th><td style=\"width:18%;\">" + num3 + "</td><th style=\"width:15%;\">總價折讓</th><td style=\"width:18%;\">" + num4 + "</td><th style=\"width:15%;\" rowspan=\"2\">單據總價</th><td rowspan=\"2\" style=\"vertical-align:middle;\">" + (num3 - num4) + "</td></tr><tr><th>品項</th><td>" + num + "</td><th>數量</th><td>" + num2 + "</td></tr>";
				result += "<tr style=\"height:4.5em;\"><th>備註</th><td colspan=\"5\"></td><tr><th>營業員</th><td colspan=\"1\"></td><th style=\"width:15%;\">客戶簽收</th><td colspan=\"3\"></td></tr></table>";
			}
			if (i + 1 == 6 + (up - 1) * 8 || i + 1 == count)
			{
				result += "</div>";
			}
			return result;
		}

		private string tableContent_80mm(string result, DataTable dt, int i, int un, int up, DataTable member)
		{
			int count = dt.Rows.Count;
			if (i + 1 == 1)
			{
				string text = _DeliveryNo;
				if (!string.IsNullOrEmpty(_version))
				{
					text = text + "(v" + _version + ")";
				}
				string shopName = _shopName;
				string address = getAddress("80");
				string text2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				string text3 = "";
				string text4 = "";
				string text5 = "";
				string text6 = "";
				string text7 = "";
				if (member.Rows.Count > 0)
				{
					text3 = member.Rows[0]["Name"].ToString();
					text4 = member.Rows[0]["BirthDate"].ToString();
					text5 = member.Rows[0]["Mobile"].ToString();
					text6 = getComposite(text3, text4, text5, "80");
					text7 = getCompanyIdNo(member.Rows[0]["CompanyIdNo"].ToString(), "80");
				}
				else
				{
					text6 = getComposite("非會員", "", "", "80");
					text7 = getCompanyIdNo("", "80");
				}
				result = result + "<div class=\"page\"><table summary=\"資料表格\" style=\"font-size:.85em;\"><tr><td colspan=\"3\" style=\"text-align:center;\"><div style=\"overflow:hidden;height:60px;margin:0 0 5px 0;padding:0;\"><img src=\"" + _imagePath + "\"/></div><div style=\"text-align:left\"><div>" + shopName + "</div><div>" + text + "</div><div>" + text2 + "</div></div></td></tr>" + text6 + text7 + address + "</table><table style=\"width:100%;font-size:.75em;\">";
			}
			string text8 = dt.Rows[i]["barcode"].ToString();
			string text9 = dt.Rows[i]["num"].ToString();
			string str = dt.Rows[i]["total"].ToString();
			int index = int.Parse(_barCodeMap[text8].ToString());
			string text10 = _barCode.Rows[index]["spec"].ToString();
			string text11 = _barCode.Rows[index]["capacity"].ToString();
			string noteMore = getNoteMore(dt, i, text8, "80");
			string text12 = "";
			if (Program.SystemMode != 1)
			{
				text12 = "<td class=\"number total\">" + str + "</td>";
			}
			result = result + "<tr><td class=\"aCenter\">" + (i + 1) + "</td><td class=\"title\"><div class=\"text-overflow of2\"><div class=\"code\">" + text8 + "</div><div class=\"productname\">" + commodityName(text8) + "<br/><span class=\"unit\">" + text10 + text11 + "</span></div><span class=\"notemore\">" + noteMore + "</span></div></td><td class=\"number\">" + text9 + "</td>" + text12 + "</tr>";
			if (i + 1 == count)
			{
				result += "</table>";
			}
			if (i + 1 == count)
			{
				string text13 = "0";
				string text14 = "0";
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				if (member.Rows.Count > 0)
				{
					text13 = member.Rows[0]["items"].ToString();
					text14 = member.Rows[0]["itemstotal"].ToString();
					num = int.Parse(member.Rows[0]["sum"].ToString());
					num2 = int.Parse(member.Rows[0]["sumDiscount"].ToString());
					num3 = int.Parse(member.Rows[0]["cash"].ToString());
					num4 = int.Parse(member.Rows[0]["Credit"].ToString());
				}
				else
				{
					foreach (DataRow row in getMainSellList().Rows)
					{
						text13 = row["items"].ToString();
						text14 = row["itemstotal"].ToString();
						num = int.Parse(row["sum"].ToString());
						num2 = int.Parse(row["sumDiscount"].ToString());
						num3 = int.Parse(row["cash"].ToString());
						num4 = int.Parse(row["Credit"].ToString());
					}
				}
				if (Program.SystemMode == 1)
				{
					string[] array = new string[2]
					{
						"品項",
						"數量"
					};
					string[] array2 = new string[2]
					{
						text13,
						text14
					};
					result += "<table style=\"width:100%;font-size:.85em;\"><tr>";
					for (int j = 0; j < array.Length; j++)
					{
						result = result + "<th>" + array[j] + "</th><td>" + array2[j] + "</td>";
					}
					result += "</tr></table></div>";
				}
				else
				{
					string text15 = " style=\"width:15%;\"";
					string text16 = " style=\"width:18%;\"";
					string text17 = "font-size:.85em;";
					result = result + "<table style=\"width:100%;" + text17 + "\"><tr><th " + text15 + ">總計</th><td" + text16 + ">" + num + "</td><th" + text15 + ">總價折讓</th><td" + text16 + ">" + num2 + "</td></tr><tr><th>品項</th><td>" + text13 + "</td><th>數量</th><td>" + text14 + "</td></tr><tr><th>消費總額</th><td>" + (num - num2) + "</td><th>找零</th><td>";
					if ("refund".Equals(_sellType))
					{
						string text18 = "";
						string text19 = "";
						string text20 = "";
						string sql = "select * from hypos_user_consumelog where sellNo='" + _DeliveryNo + "' order by editdate DESC LIMIT 0,1";
						DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
						bool flag = false;
						if (dataTable.Rows.Count > 0)
						{
							if ("1".Equals(dataTable.Rows[0]["sellType"].ToString()))
							{
								text18 = dataTable.Rows[0]["Cash"].ToString();
								text19 = "0";
								text20 = "0";
							}
							else if ("2".Equals(dataTable.Rows[0]["sellType"].ToString()))
							{
								text18 = "0";
								text19 = dataTable.Rows[0]["Credit"].ToString();
								text20 = dataTable.Rows[0]["Cash"].ToString();
							}
							else
							{
								flag = true;
							}
						}
						result = ((!flag) ? (result + text20 + "</td></tr><tr><th>退款模式</th><td colspan=\"3\">現金退款 :" + text18 + "<span " + text17 + ">(賒帳還款：" + text19 + ")</span></td></tr></table></div>") : (result + getZero(num, num2, num3) + "</td></tr><tr><th>收款</th><td colspan=\"3\">現金 :" + num3 + "<span " + text17 + ">(賒帳：" + num4 + ")</span></td></tr></table></div>"));
					}
					else
					{
						result = result + getZero(num, num2, num3) + "</td></tr><tr><th>收款</th><td colspan=\"3\">現金 :" + num3 + "<span " + text17 + ">(賒帳：" + num4 + ")</span></td></tr></table></div>";
					}
				}
			}
			return result;
		}

		private string tableContent_60mm(string result, DataTable dt, int i, int un, int up, DataTable member)
		{
			int count = dt.Rows.Count;
			if (i + 1 == 1)
			{
				string shopName = _shopName;
				string address = getAddress("60");
				string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				string text2 = "";
				string text3 = "";
				string text4 = "";
				string text5 = "";
				string text6 = "";
				if (member.Rows.Count > 0)
				{
					text2 = member.Rows[0]["Name"].ToString();
					text3 = member.Rows[0]["BirthDate"].ToString();
					text4 = member.Rows[0]["Mobile"].ToString();
					text5 = getComposite(text2, text3, text4, "60");
					text6 = getCompanyIdNo(member.Rows[0]["CompanyIdNo"].ToString(), "60");
				}
				else
				{
					text5 = getComposite("非會員", "", "", "60");
					text6 = getCompanyIdNo("", "60");
				}
				result = result + "<div class=\"page\"><table summary=\"資料表格\" style=\"font-size:.65em;\"><tr><td style=\"text-align:center;\"><div style=\"overflow:hidden;height:50px;margin:0 0 5px 0;padding:0;\"><img src=\"" + _imagePath + "\" style =\"width:171px;\"/></div><div style=\"text-align:left;\"><div>" + shopName + "</div><div>" + text + "</div></div></td></tr>" + text5 + text6 + address + "</table><table style=\"width:100%;font-size:.65em;\">";
			}
			string text7 = dt.Rows[i]["barcode"].ToString();
			string text8 = dt.Rows[i]["num"].ToString();
			dt.Rows[i]["sellingPrice"].ToString();
			dt.Rows[i]["discount"].ToString();
			string str = dt.Rows[i]["total"].ToString();
			int index = int.Parse(_barCodeMap[text7].ToString());
			string text9 = _barCode.Rows[index]["spec"].ToString();
			string text10 = _barCode.Rows[index]["capacity"].ToString();
			string noteMore = getNoteMore(dt, i, text7, "60");
			string text11 = "";
			if (Program.SystemMode != 1)
			{
				text11 = "<td class=\"number total\">" + str + "</td>";
			}
			result = result + "<tr><td class=\"title\"><div class=\"text-overflow of2\"><div class=\"code\">" + text7 + "</div><div class=\"productname\">" + commodityName(text7) + "<br/><span class=\"unit\">" + text9 + text10 + "</span></div><span class=\"notemore\">" + noteMore + "</span></div></td><td class=\"number\">" + text8 + "</td>" + text11 + "</tr>";
			if (i + 1 == count)
			{
				result += "</table>";
			}
			if (i + 1 == count)
			{
				string text12 = "0";
				string text13 = "0";
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				if (member.Rows.Count > 0)
				{
					text12 = member.Rows[0]["items"].ToString();
					text13 = member.Rows[0]["itemstotal"].ToString();
					num = int.Parse(member.Rows[0]["sum"].ToString());
					num2 = int.Parse(member.Rows[0]["sumDiscount"].ToString());
					num3 = int.Parse(member.Rows[0]["cash"].ToString());
					num4 = int.Parse(member.Rows[0]["Credit"].ToString());
				}
				else
				{
					foreach (DataRow row in getMainSellList().Rows)
					{
						text12 = row["items"].ToString();
						text13 = row["itemstotal"].ToString();
						num = int.Parse(row["sum"].ToString());
						num2 = int.Parse(row["sumDiscount"].ToString());
						num3 = int.Parse(row["cash"].ToString());
						num4 = int.Parse(row["credit"].ToString());
					}
				}
				if (Program.SystemMode == 1)
				{
					string[] array = new string[2]
					{
						"品項",
						"數量"
					};
					string[] array2 = new string[2]
					{
						text12,
						text13
					};
					result += "<table style=\"width:100%;font-size:.85em;\"><tr>";
					for (int j = 0; j < array.Length; j++)
					{
						result = result + "<th>" + array[j] + "</th><td>" + array2[j] + "</td>";
					}
					result += "</tr></table></div>";
				}
				else
				{
					string text14 = " style=\"width:15%;\"";
					string text15 = " style=\"width:18%;\"";
					string text16 = "font-size:.85em;";
					result = result + "<table style=\"width:100%;" + text16 + "\"><tr><th " + text14 + ">總計</th><td" + text15 + ">" + num + "</td><th" + text14 + ">折讓</th><td" + text15 + ">" + num2 + "</td></tr><tr><th>品項</th><td>" + text12 + "</td><th>數量</th><td>" + text13 + "</td></tr><tr><th>消費總額</th><td>" + (num - num2) + "</td><th>找零</th><td>";
					if ("refund".Equals(_sellType))
					{
						string text17 = "";
						string text18 = "";
						string text19 = "";
						string sql = "select * from hypos_user_consumelog where sellNo='" + _DeliveryNo + "' order by editdate DESC LIMIT 0,1";
						DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteReaderReturnDataTable);
						bool flag = false;
						if (dataTable.Rows.Count > 0)
						{
							if ("1".Equals(dataTable.Rows[0]["sellType"].ToString()))
							{
								text17 = dataTable.Rows[0]["Cash"].ToString();
								text18 = "0";
								text19 = "0";
							}
							else if ("2".Equals(dataTable.Rows[0]["sellType"].ToString()))
							{
								text17 = "0";
								text18 = dataTable.Rows[0]["Credit"].ToString();
								text19 = dataTable.Rows[0]["Cash"].ToString();
							}
							else
							{
								flag = true;
							}
						}
						result = ((!flag) ? (result + text19 + "</td></tr><tr><th>退款模式</th><td colspan=\"3\">現金退款 :" + text17 + "<span " + text16 + "><br/>(賒帳還款：" + text18 + ")</span></td></tr></table></div>") : (result + getZero(num, num2, num3) + "</td></tr><tr><th>收款</th><td colspan=\"3\">現金 :" + num3 + "<span " + text16 + "><br/>(賒帳：" + num4 + ")</span></td></tr></table></div>"));
					}
					else
					{
						result = result + getZero(num, num2, num3) + "</td></tr><tr><th>收款</th><td colspan=\"3\">現金 :" + num3 + "<span " + text16 + "><br/>(賒帳：" + num4 + ")</span></td></tr></table></div>";
					}
				}
			}
			return result;
		}

		private string commodityName(string barCode)
		{
			int index = int.Parse(_barCodeMap[barCode].ToString());
			string text = _barCode.Rows[index]["GDName"].ToString();
			string text2 = _barCode.Rows[index]["CName"].ToString();
			string text3 = _barCode.Rows[index]["contents"].ToString();
			string text4 = _barCode.Rows[index]["brandName"].ToString();
			string text5 = _barCode.Rows[index]["formCode"].ToString();
			if (!string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text3) || !string.IsNullOrEmpty(text4) || !string.IsNullOrEmpty(text5))
			{
				text += "[";
				if (!string.IsNullOrEmpty(text2))
				{
					text = text + text2 + "-";
				}
				if (!string.IsNullOrEmpty(text5))
				{
					text = text + text5 + ((!string.IsNullOrEmpty(text3)) ? "．" : "");
				}
				if (!string.IsNullOrEmpty(text3))
				{
					text = text + text3 + "-";
				}
				if (!string.IsNullOrEmpty(text4))
				{
					text = text + text4 + "-";
				}
				int num = text.LastIndexOf("-");
				if (num > 0)
				{
					text = text.Substring(0, num) + "]";
				}
			}
			return text;
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

		private void btn_close_Click(object sender, EventArgs e)
		{
			Close();
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

		private string pageSize(string cssType)
		{
			return "body{width:100%;height:100%;margin:0;padding:0;background-color:#FAFAFA;font-family:\"微軟正黑體\", Microsoft JhengHei;} *{box-sizing: border-box;-moz-box-sizing: border-box;} .page{width:170mm;min-height:244mm;padding:10mm;margin:10mm auto;border:1px #D3D3D3 solid;border-radius:5px;background:white;box-shadow:0 0 5px rgba(0,0,0,0.1);} .subpage{padding: 1cm;border: 5px red solid;height: 257mm;outline: 2cm #FFEAEA solid;} table{border-collapse:collapse;border-spacing:0;border:1px dotted #666;background-color:#FFF;font-size:1.15em;margin:0 0 20px 0;width:100%;} th{text-align:right;border:1px dotted #666;padding:5px;white-space:nowrap;} td{text-align:left;border:1px dotted #666;padding:5px;vertical-align:top;} @page{size:A4;margin:0;} @media print{html,body{width:210mm;height:297mm;} .page{border:initial;border-radius:initial;width:initial;min-height:initial;box-shadow:initial;background:initial;}}";
		}

		private string getAddress(string type)
		{
			string result = "";
			DataTable memberList = getMemberList();
			if (memberList.Rows.Count > 0)
			{
				string str = string.IsNullOrEmpty(memberList.Rows[0]["CityNo"].ToString()) ? "''" : memberList.Rows[0]["CityNo"].ToString();
				string str2 = string.IsNullOrEmpty(memberList.Rows[0]["Zipcode"].ToString()) ? "''" : memberList.Rows[0]["Zipcode"].ToString();
				string text = string.IsNullOrEmpty(memberList.Rows[0]["Address"].ToString()) ? "''" : memberList.Rows[0]["Address"].ToString();
				string sql = "SELECT city FROM ADDRCITY where cityno =" + str + " limit 1";
				string text2 = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
				string sql2 = "SELECT area FROM ADDRAREA where zipcode =" + str2 + " limit 1";
				string text3 = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteScalar));
				if ("-1".Equals(text2))
				{
					text2 = "";
				}
				if ("-1".Equals(text3))
				{
					text3 = "";
				}
				if ("-1".Equals(text))
				{
					text = "";
				}
				result = text2 + text3 + text;
			}
			return result;
		}

		private string getVendorAddress()
		{
			string result = "";
			DataTable memberList = getMemberList();
			if (memberList.Rows.Count > 0)
			{
				string str = string.IsNullOrEmpty(memberList.Rows[0]["CityNo"].ToString()) ? "''" : memberList.Rows[0]["CityNo"].ToString();
				string str2 = string.IsNullOrEmpty(memberList.Rows[0]["Zipcode"].ToString()) ? "''" : memberList.Rows[0]["Zipcode"].ToString();
				string text = string.IsNullOrEmpty(memberList.Rows[0]["Address"].ToString()) ? "''" : memberList.Rows[0]["Address"].ToString();
				string sql = "SELECT city FROM ADDRCITY where cityno =" + str + " limit 1";
				string text2 = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
				string sql2 = "SELECT area FROM ADDRAREA where zipcode =" + str2 + " limit 1";
				string text3 = Convert.ToString(DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteScalar));
				if ("-1".Equals(text2))
				{
					text2 = "";
				}
				if ("-1".Equals(text3))
				{
					text3 = "";
				}
				if ("''".Equals(text))
				{
					text = "";
				}
				result = text2 + text3 + text;
			}
			return result;
		}

		private string getNoteMore(DataTable dt, int i, string barCode, string type)
		{
			string str = dt.Rows[i]["BatchNo"].ToString();
			string str2 = dt.Rows[i]["MFGDate"].ToString();
			return str + "<br/>" + str2;
		}

		private string getCompanyIdNo(string companyIdNo, string type)
		{
			string result = "";
			if (!string.IsNullOrEmpty(companyIdNo))
			{
				result = "<tr><td>" + companyIdNo + "</td></tr>";
			}
			return result;
		}

		private string getComposite(string name, string birth, string mobile, string type)
		{
			string text = "";
			if (!string.IsNullOrEmpty(name))
			{
				text += name;
			}
			if (!string.IsNullOrEmpty(birth) || !string.IsNullOrEmpty(mobile))
			{
				text += "(";
			}
			if (!string.IsNullOrEmpty(birth))
			{
				DateTime dateTime = Convert.ToDateTime(birth);
				int num = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks).Days / 365;
				text = (string.IsNullOrEmpty(mobile) ? (text + num + "歲 ") : (text + num + "歲 / "));
			}
			if (!string.IsNullOrEmpty(mobile))
			{
				text += mobile;
			}
			if (!string.IsNullOrEmpty(birth) || !string.IsNullOrEmpty(mobile))
			{
				text += ")";
			}
			return text;
		}

		private int getZero(int sum, int sumDiscount, int cash)
		{
			int result = 0;
			if (cash - (sum - sumDiscount) > 0)
			{
				result = cash - (sum - sumDiscount);
			}
			return result;
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
			webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
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
			base.Name = "frmSell_SellNo";
			Text = "frmSell";
			ResumeLayout(false);
		}
	}
}
