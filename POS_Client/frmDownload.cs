using POS_Client.POS_WS_Download;
using SevenZip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	public class frmDownload : Form
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_0
		{
			public string licenseCode;

			public frmDownload _003C_003E4__this;

			internal void _003CbackgroundWorker1_DoWork_003Eb__0()
			{
				_003C_003E4__this.tb_status.AppendText("店代碼: " + licenseCode + ",上次更新日期: " + ((_003C_003E4__this._TRUE_lastUpdateDate == "") ? "無" : _003C_003E4__this._TRUE_lastUpdateDate) + "\r\n");
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_1
		{
			public string s;

			public _003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals1;

			internal void _003CbackgroundWorker1_DoWork_003Eb__3()
			{
				CS_0024_003C_003E8__locals1._003C_003E4__this.tb_status.AppendText("讀取:" + s + "\r\n");
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_2
		{
			public string s;

			public _003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals2;

			internal void _003CbackgroundWorker1_DoWork_003Eb__4()
			{
				CS_0024_003C_003E8__locals2._003C_003E4__this.tb_status.AppendText("讀取:" + s + "\r\n");
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_3
		{
			public string s;

			public _003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals3;

			internal void _003CbackgroundWorker1_DoWork_003Eb__5()
			{
				CS_0024_003C_003E8__locals3._003C_003E4__this.tb_status.AppendText("讀取:" + s + "\r\n");
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_4
		{
			public string s;

			public _003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals4;

			internal void _003CbackgroundWorker1_DoWork_003Eb__6()
			{
				CS_0024_003C_003E8__locals4._003C_003E4__this.tb_status.AppendText("讀取:" + s + "\r\n");
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_5
		{
			public string s;

			public _003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals5;

			internal void _003CbackgroundWorker1_DoWork_003Eb__7()
			{
				CS_0024_003C_003E8__locals5._003C_003E4__this.tb_status.AppendText("讀取:" + s + "\r\n");
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_6
		{
			public string s;

			public _003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals6;

			internal void _003CbackgroundWorker1_DoWork_003Eb__9()
			{
				CS_0024_003C_003E8__locals6._003C_003E4__this.tb_status.AppendText("讀取:" + s + "\r\n");
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_7
		{
			public Exception ex;

			public _003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals7;

			internal void _003CbackgroundWorker1_DoWork_003Eb__12()
			{
				CS_0024_003C_003E8__locals7._003C_003E4__this.tb_status.AppendText(ex.ToString() + "\r\n");
				CS_0024_003C_003E8__locals7._003C_003E4__this.tb_status.AppendText("下載失敗!!!");
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass7_0
		{
			public List<string> zipFilePathArray;

			public frmDownload _003C_003E4__this;
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass7_1
		{
			public int i;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals1;

			internal void _003CunZip_003Eb__0()
			{
				CS_0024_003C_003E8__locals1._003C_003E4__this.tb_status.AppendText("解壓縮:" + CS_0024_003C_003E8__locals1.zipFilePathArray[i] + "...");
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass8_0
		{
			public string zipFile;

			public frmDownload _003C_003E4__this;

			internal void _003CdownloadZip_003Eb__0()
			{
				_003C_003E4__this.tb_status.AppendText("下載:" + zipFile + "檔案...");
			}
		}

		private string _licenseCode;

		private string _lastUpdateDate;

		private string _TRUE_lastUpdateDate;

		private bool _IsLastUpdateDateEmpty;

		private IContainer components;

		private ProgressBar pbStatus;

		private BackgroundWorker backgroundWorker1;

		private TextBox tb_status;

		private Button btn_close;

		private Label label1;

		public frmDownload(string licenseCode, string lastUpdateDate)
		{
			InitializeComponent();
			_licenseCode = licenseCode;
			if (!lastUpdateDate.Equals(""))
			{
				DateTime dateTime = DateTime.ParseExact(lastUpdateDate, "yyyyMMddHHmmss", null, DateTimeStyles.AllowWhiteSpaces);
				if (DateTime.Compare(DateTime.Now, dateTime.AddDays(20.0)) > 0)
				{
					lastUpdateDate = "";
				}
			}
			if ("".Equals(lastUpdateDate))
			{
				_lastUpdateDate = lastUpdateDate;
				_IsLastUpdateDateEmpty = true;
				return;
			}
			_IsLastUpdateDateEmpty = false;
			DateTime dateTime2 = DateTime.ParseExact(lastUpdateDate, "yyyyMMddHHmmss", null, DateTimeStyles.AllowWhiteSpaces);
			_TRUE_lastUpdateDate = dateTime2.ToString("yyyyMMddHHmmss");
			_lastUpdateDate = dateTime2.AddDays(-1.0).ToString("yyyyMMddHHmmss");
		}

		private void frmDownload_Load(object sender, EventArgs e)
		{
			if (NetworkInterface.GetIsNetworkAvailable())
			{
				backgroundWorker1.RunWorkerAsync();
				return;
			}
			MessageBox.Show("偵測不到網路連線，資料同步將於下次連線進行更新");
			Close();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			_003C_003Ec__DisplayClass6_0 _003C_003Ec__DisplayClass6_ = new _003C_003Ec__DisplayClass6_0();
			_003C_003Ec__DisplayClass6_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass6_.licenseCode = _licenseCode;
			string lastUpdateDate = _lastUpdateDate;
			tb_status.Invoke(new MethodInvoker(_003C_003Ec__DisplayClass6_._003CbackgroundWorker1_DoWork_003Eb__0));
			try
			{
				ExpData expData = new ExpData();
				expData.Url = Program.DownloadURL;
				tb_status.Invoke(new MethodInvoker(_003CbackgroundWorker1_DoWork_003Eb__6_1));
				if (!Directory.Exists("Crop"))
				{
					Directory.CreateDirectory("Crop");
				}
				if (!Directory.Exists("Pest"))
				{
					Directory.CreateDirectory("Pest");
				}
				if (!Directory.Exists("PesticideLic"))
				{
					Directory.CreateDirectory("PesticideLic");
				}
				if (!Directory.Exists("PestCropRelation"))
				{
					Directory.CreateDirectory("PestCropRelation");
				}
				if (!Directory.Exists("BarCode"))
				{
					Directory.CreateDirectory("BarCode");
				}
				if (!Directory.Exists("FerData"))
				{
					Directory.CreateDirectory("FerData");
				}
				tb_status.Invoke(new MethodInvoker(_003CbackgroundWorker1_DoWork_003Eb__6_2));
				backgroundWorker1.ReportProgress(10);
				List<string> zipFilePathArray = new List<string>();
				string @string = Encoding.UTF8.GetString(expData.expCrop(_003C_003Ec__DisplayClass6_.licenseCode, lastUpdateDate, Encrypt.GetMD5(_003C_003Ec__DisplayClass6_.licenseCode + lastUpdateDate + "!baphiq2012$").ToLower()));
				downloadZip(@string, "Crop\\", zipFilePathArray);
				List<string> zipFilePathArray2 = new List<string>();
				@string = Encoding.UTF8.GetString(expData.expPest(_003C_003Ec__DisplayClass6_.licenseCode, lastUpdateDate, Encrypt.GetMD5(_003C_003Ec__DisplayClass6_.licenseCode + lastUpdateDate + "!baphiq2012$").ToLower()));
				downloadZip(@string, "Pest\\", zipFilePathArray2);
				List<string> zipFilePathArray3 = new List<string>();
				@string = Encoding.UTF8.GetString(expData.expPesticideLic(_003C_003Ec__DisplayClass6_.licenseCode, lastUpdateDate, Encrypt.GetMD5(_003C_003Ec__DisplayClass6_.licenseCode + lastUpdateDate + "!baphiq2012$").ToLower()));
				downloadZip(@string, "PesticideLic\\", zipFilePathArray3);
				List<string> zipFilePathArray4 = new List<string>();
				@string = Encoding.UTF8.GetString(expData.expPestCropRelation(_003C_003Ec__DisplayClass6_.licenseCode, lastUpdateDate, Encrypt.GetMD5(_003C_003Ec__DisplayClass6_.licenseCode + lastUpdateDate + "!baphiq2012$").ToLower()));
				downloadZip(@string, "PestCropRelation\\", zipFilePathArray4);
				List<string> zipFilePathArray5 = new List<string>();
				@string = Encoding.UTF8.GetString(expData.expBarCode(_003C_003Ec__DisplayClass6_.licenseCode, lastUpdateDate, Encrypt.GetMD5(_003C_003Ec__DisplayClass6_.licenseCode + lastUpdateDate + "!baphiq2012$").ToLower()));
				downloadZip(@string, "BarCode\\", zipFilePathArray5);
				List<string> zipFilePathArray6 = new List<string>();
				@string = Encoding.UTF8.GetString(expData.expferData(_003C_003Ec__DisplayClass6_.licenseCode, lastUpdateDate, Encrypt.GetMD5(_003C_003Ec__DisplayClass6_.licenseCode + lastUpdateDate + "!baphiq2012$").ToLower()));
				downloadZip(@string, "FerData\\", zipFilePathArray6);
				backgroundWorker1.ReportProgress(20);
				List<string> list = new List<string>();
				unZip(zipFilePathArray, list);
				List<string> list2 = new List<string>();
				unZip(zipFilePathArray2, list2);
				List<string> list3 = new List<string>();
				unZip(zipFilePathArray3, list3);
				List<string> list4 = new List<string>();
				unZip(zipFilePathArray4, list4);
				List<string> list5 = new List<string>();
				unZip(zipFilePathArray5, list5);
				List<string> list6 = new List<string>();
				unZip(zipFilePathArray6, list6);
				backgroundWorker1.ReportProgress(30);
				string value = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT IsCheckHyLicenceAndGoodslst FROM hypos_SysParam", null, CommandOperationType.ExecuteScalar).ToString();
				if (!"Y".Equals(value))
				{
					string sql = "update hypos_GOODSLST set pesticideId = (select pesticideId from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType ),CName = (select pesticideName from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType ),formCode = (select formCode from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType ),contents = (select contents from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType ),GDName = (select brandName from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType),brandName = (select domManufName from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType) where CLA1NO = '0302' and pesticideId is null and formcode is null and contents is null ";
					DataBaseUtilities.DBOperation(Program.ConnectionString, sql, null, CommandOperationType.ExecuteNonQuery);
					DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_SysParam SET IsCheckHyLicenceAndGoodslst = 'Y'", null, CommandOperationType.ExecuteNonQuery);
				}
				if (list.Count > 0)
				{
					using (List<string>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							_003C_003Ec__DisplayClass6_1 _003C_003Ec__DisplayClass6_2 = new _003C_003Ec__DisplayClass6_1();
							_003C_003Ec__DisplayClass6_2.CS_0024_003C_003E8__locals1 = _003C_003Ec__DisplayClass6_;
							_003C_003Ec__DisplayClass6_2.s = enumerator.Current;
							tb_status.Invoke(new MethodInvoker(_003C_003Ec__DisplayClass6_2._003CbackgroundWorker1_DoWork_003Eb__3));
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.Load(_003C_003Ec__DisplayClass6_2.s);
							XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//record");
							string str = "INSERT INTO HyTempCrop(cat1,cat2,cat3,cat4,code,name,lastUpdateDate,isDelete) VALUES ";
							StringBuilder stringBuilder = new StringBuilder();
							HashSet<string> hashSet = new HashSet<string>();
							for (int i = 0; i < xmlNodeList.Count; i++)
							{
								string innerText = xmlNodeList[i].SelectSingleNode("./code").InnerText;
								if (!hashSet.Contains(innerText))
								{
									hashSet.Add(innerText);
									stringBuilder.Append("('" + xmlNodeList[i].SelectSingleNode("./cat1").InnerText + "',");
									stringBuilder.Append("'" + xmlNodeList[i].SelectSingleNode("./cat2").InnerText + "',");
									stringBuilder.Append("'" + xmlNodeList[i].SelectSingleNode("./cat3").InnerText + "',");
									stringBuilder.Append("'" + xmlNodeList[i].SelectSingleNode("./cat4").InnerText + "',");
									stringBuilder.Append("'" + xmlNodeList[i].SelectSingleNode("./code").InnerText + "',");
									stringBuilder.Append("'" + xmlNodeList[i].SelectSingleNode("./name").InnerText + "',");
									stringBuilder.Append("'" + xmlNodeList[i].SelectSingleNode("./lastUpdateDate").InnerText + "',");
									stringBuilder.Append("'" + xmlNodeList[i].SelectSingleNode("./isDelete").InnerText + "'),");
								}
							}
							if (stringBuilder.Length > 0)
							{
								str += stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
								DataBaseUtilities.DBOperation(Program.ConnectionString, str, null, CommandOperationType.ExecuteNonQuery);
								string sql2 = "DELETE FROM HyCrop WHERE EXISTS (select 1 from HyTempCrop WHERE HyCrop.code = HyTempCrop.code)";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql2, null, CommandOperationType.ExecuteNonQuery);
								string sql3 = "INSERT INTO HyCrop SELECT * FROM HyTempCrop";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql3, null, CommandOperationType.ExecuteNonQuery);
								DataBaseUtilities.DBOperation(Program.ConnectionString, "DELETE FROM HyTempCrop", null, CommandOperationType.ExecuteNonQuery);
							}
						}
					}
				}
				backgroundWorker1.ReportProgress(40);
				if (list2.Count > 0)
				{
					using (List<string>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							_003C_003Ec__DisplayClass6_2 _003C_003Ec__DisplayClass6_3 = new _003C_003Ec__DisplayClass6_2();
							_003C_003Ec__DisplayClass6_3.CS_0024_003C_003E8__locals2 = _003C_003Ec__DisplayClass6_;
							_003C_003Ec__DisplayClass6_3.s = enumerator.Current;
							tb_status.Invoke(new MethodInvoker(_003C_003Ec__DisplayClass6_3._003CbackgroundWorker1_DoWork_003Eb__4));
							XmlDocument xmlDocument2 = new XmlDocument();
							xmlDocument2.Load(_003C_003Ec__DisplayClass6_3.s);
							XmlNodeList xmlNodeList2 = xmlDocument2.SelectNodes("//record");
							string str2 = "INSERT INTO HyTempBlight(cat1,cat2,cat3,cat4,code,name,lastUpdateDate,isDelete) VALUES ";
							StringBuilder stringBuilder2 = new StringBuilder();
							HashSet<string> hashSet2 = new HashSet<string>();
							for (int j = 0; j < xmlNodeList2.Count; j++)
							{
								string innerText2 = xmlNodeList2[j].SelectSingleNode("./code").InnerText;
								if (!hashSet2.Contains(innerText2))
								{
									hashSet2.Add(innerText2);
									stringBuilder2.Append("('" + xmlNodeList2[j].SelectSingleNode("./cat1").InnerText + "',");
									stringBuilder2.Append("'" + xmlNodeList2[j].SelectSingleNode("./cat2").InnerText + "',");
									stringBuilder2.Append("'" + xmlNodeList2[j].SelectSingleNode("./cat3").InnerText + "',");
									stringBuilder2.Append("'" + xmlNodeList2[j].SelectSingleNode("./cat4").InnerText + "',");
									stringBuilder2.Append("'" + xmlNodeList2[j].SelectSingleNode("./code").InnerText + "',");
									stringBuilder2.Append("'" + xmlNodeList2[j].SelectSingleNode("./name").InnerText + "',");
									stringBuilder2.Append("'" + xmlNodeList2[j].SelectSingleNode("./lastUpdateDate").InnerText + "',");
									stringBuilder2.Append("'" + xmlNodeList2[j].SelectSingleNode("./isDelete").InnerText + "'),");
								}
							}
							if (stringBuilder2.Length > 0)
							{
								str2 += stringBuilder2.ToString().Substring(0, stringBuilder2.Length - 1);
								DataBaseUtilities.DBOperation(Program.ConnectionString, str2, null, CommandOperationType.ExecuteNonQuery);
								string sql4 = "DELETE FROM HyBlight WHERE EXISTS (select 1 from HyTempBlight WHERE HyBlight.code = HyTempBlight.code)";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql4, null, CommandOperationType.ExecuteNonQuery);
								string sql5 = "INSERT INTO HyBlight SELECT * FROM HyTempBlight";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql5, null, CommandOperationType.ExecuteNonQuery);
								DataBaseUtilities.DBOperation(Program.ConnectionString, "DELETE FROM HyTempBlight", null, CommandOperationType.ExecuteNonQuery);
							}
						}
					}
				}
				backgroundWorker1.ReportProgress(50);
				if (list3.Count > 0)
				{
					bool flag = true;
					using (List<string>.Enumerator enumerator = list3.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							_003C_003Ec__DisplayClass6_3 _003C_003Ec__DisplayClass6_4 = new _003C_003Ec__DisplayClass6_3();
							_003C_003Ec__DisplayClass6_4.CS_0024_003C_003E8__locals3 = _003C_003Ec__DisplayClass6_;
							_003C_003Ec__DisplayClass6_4.s = enumerator.Current;
							if ("PesticideLic\\pesticideLic.xml".Equals(_003C_003Ec__DisplayClass6_4.s))
							{
								flag = false;
							}
							tb_status.Invoke(new MethodInvoker(_003C_003Ec__DisplayClass6_4._003CbackgroundWorker1_DoWork_003Eb__5));
							XmlDocument xmlDocument3 = new XmlDocument();
							xmlDocument3.Load(_003C_003Ec__DisplayClass6_4.s);
							XmlNodeList xmlNodeList3 = xmlDocument3.SelectNodes("//record");
							string str3 = "INSERT INTO HyTempLicence(licType,licNo,domManufId,domManufName,domManufAddr,domManufChg, forManufCoun,forManufName,forManufAddr,pesticideId,cat1Name,cat2Name,pesticideName,pesticideEname,formCode, formName,contents,ctUp,mixNote,signNo,brandName,brandEname,otherComp,contTotal,outlook,expireDate,lastUpdateDate,isDelete) VALUES ";
							StringBuilder stringBuilder3 = new StringBuilder();
							HashSet<string> hashSet3 = new HashSet<string>();
							List<string[]> list7 = new List<string[]>();
							for (int k = 0; k < xmlNodeList3.Count; k++)
							{
								string item = xmlNodeList3[k].SelectSingleNode("./licType").InnerText + xmlNodeList3[k].SelectSingleNode("./licNo").InnerText;
								if (!hashSet3.Contains(item))
								{
									string[] item2 = new string[2]
									{
										"'" + xmlNodeList3[k].SelectSingleNode("./licType").InnerText + "'",
										"'" + xmlNodeList3[k].SelectSingleNode("./licNo").InnerText + "'"
									};
									list7.Add(item2);
									hashSet3.Add(item);
									stringBuilder3.Append("('" + xmlNodeList3[k].SelectSingleNode("./licType").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./licNo").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./domManufId").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./domManufName").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./domManufAddr").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./domManufChg").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./forManufCoun").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./forManufName").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./forManufAddr").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./pesticideId").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./cat1Name").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./cat2Name").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./pesticideName").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./pesticideEname").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./formCode").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./formName").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./contents").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./ctUp").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./mixNote").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./signNo").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./brandName").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./brandEname").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./otherComp").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./contTotal").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./outlook").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./expireDate").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./lastUpdateDate").InnerText + "',");
									stringBuilder3.Append("'" + xmlNodeList3[k].SelectSingleNode("./isDelete").InnerText + "'),");
								}
							}
							if (stringBuilder3.Length > 0)
							{
								str3 += stringBuilder3.ToString().Substring(0, stringBuilder3.Length - 1);
								DataBaseUtilities.DBOperation(Program.ConnectionString, str3, null, CommandOperationType.ExecuteNonQuery);
								string sql6 = "DELETE FROM HyLicence WHERE EXISTS (select 1 from HyTempLicence WHERE HyLicence.licType = HyTempLicence.licType and HyLicence.licNo = HyTempLicence.licNo)";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql6, null, CommandOperationType.ExecuteNonQuery);
								string sql7 = "INSERT INTO HyLicence SELECT * FROM HyTempLicence";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql7, null, CommandOperationType.ExecuteNonQuery);
								DataBaseUtilities.DBOperation(Program.ConnectionString, "DELETE FROM HyTempLicence", null, CommandOperationType.ExecuteNonQuery);
								string text = "";
								string text2 = "";
								string text3 = "update hypos_GOODSLST set pesticideId = (select pesticideId from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType ),CName = (select pesticideName from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType ),formCode = (select formCode from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType ),contents = (select contents from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType ),GDName = (select brandName from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType),brandName = (select domManufName from HyLicence b where hypos_GOODSLST.domManufId = b.licNo and hypos_GOODSLST.licType = b.licType) where CLA1NO = '0302' ";
								if (!_IsLastUpdateDateEmpty && flag)
								{
									for (int l = 0; l < list7.Count; l++)
									{
										try
										{
											string text4 = text3;
											text = list7[l][0];
											text2 = list7[l][1];
											text4 = text4 + "and licType = " + text + "and domManufId = " + text2;
											DataBaseUtilities.DBOperation(Program.ConnectionString, text4, null, CommandOperationType.ExecuteNonQuery);
										}
										catch
										{
										}
									}
								}
								list7.Clear();
							}
							flag = true;
						}
					}
				}
				backgroundWorker1.ReportProgress(60);
				if (list4.Count > 0)
				{
					using (List<string>.Enumerator enumerator = list4.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							_003C_003Ec__DisplayClass6_4 _003C_003Ec__DisplayClass6_5 = new _003C_003Ec__DisplayClass6_4();
							_003C_003Ec__DisplayClass6_5.CS_0024_003C_003E8__locals4 = _003C_003Ec__DisplayClass6_;
							_003C_003Ec__DisplayClass6_5.s = enumerator.Current;
							tb_status.Invoke(new MethodInvoker(_003C_003Ec__DisplayClass6_5._003CbackgroundWorker1_DoWork_003Eb__6));
							XmlDocument xmlDocument4 = new XmlDocument();
							xmlDocument4.Load(_003C_003Ec__DisplayClass6_5.s);
							XmlNodeList xmlNodeList4 = xmlDocument4.SelectNodes("//record");
							string str4 = "INSERT INTO HyTempScope(seq,pesticideId,pesticideName,formCode,contents,mixNote, cropId,pestId,usages,dilute,period,intervals,frequency, recovery,approveDate,regStoreName,notes,direction,banCirculate,lastUpdateDate,isDelete) VALUES ";
							StringBuilder stringBuilder4 = new StringBuilder();
							HashSet<string> hashSet4 = new HashSet<string>();
							for (int m = 0; m < xmlNodeList4.Count; m++)
							{
								string innerText3 = xmlNodeList4[m].SelectSingleNode("./seq").InnerText;
								if (!hashSet4.Contains(innerText3))
								{
									hashSet4.Add(innerText3);
									stringBuilder4.Append("('" + xmlNodeList4[m].SelectSingleNode("./seq").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./pesticideId").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./pesticideName").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./formCode").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./contents").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./mixNote").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./cropId").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./pestId").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./usages").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./dilute").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./period").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./intervals").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./frequency").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./recovery").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./approveDate").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./regStoreName").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./notes").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./direction").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./banCirculate").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./lastUpdateDate").InnerText + "',");
									stringBuilder4.Append("'" + xmlNodeList4[m].SelectSingleNode("./isDelete").InnerText + "'),");
								}
							}
							if (stringBuilder4.Length > 0)
							{
								str4 += stringBuilder4.ToString().Substring(0, stringBuilder4.Length - 1);
								DataBaseUtilities.DBOperation(Program.ConnectionString, str4, null, CommandOperationType.ExecuteNonQuery);
								string sql8 = "DELETE FROM HyScope WHERE EXISTS (select 1 from HyTempScope WHERE HyScope.seq = HyTempScope.seq)";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql8, null, CommandOperationType.ExecuteNonQuery);
								string sql9 = "INSERT INTO HyScope SELECT * FROM HyTempScope";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql9, null, CommandOperationType.ExecuteNonQuery);
								DataBaseUtilities.DBOperation(Program.ConnectionString, "DELETE FROM HyTempScope", null, CommandOperationType.ExecuteNonQuery);
							}
						}
					}
				}
				backgroundWorker1.ReportProgress(70);
				if (list6.Count > 0)
				{
					using (List<string>.Enumerator enumerator = list6.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							_003C_003Ec__DisplayClass6_5 _003C_003Ec__DisplayClass6_6 = new _003C_003Ec__DisplayClass6_5();
							_003C_003Ec__DisplayClass6_6.CS_0024_003C_003E8__locals5 = _003C_003Ec__DisplayClass6_;
							_003C_003Ec__DisplayClass6_6.s = enumerator.Current;
							tb_status.Invoke(new MethodInvoker(_003C_003Ec__DisplayClass6_6._003CbackgroundWorker1_DoWork_003Eb__7));
							XmlDocument xmlDocument5 = new XmlDocument();
							xmlDocument5.Load(_003C_003Ec__DisplayClass6_6.s);
							XmlNodeList xmlNodeList5 = xmlDocument5.SelectNodes("//record");
							string str5 = "INSERT INTO HyFerData(strBarcode, Barcode , ferID, ferName, Subsidy, unitWeight,isWS , Type ,lastUpdateDate ,isDelete) VALUES ";
							StringBuilder stringBuilder5 = new StringBuilder();
							HashSet<string> hashSet5 = new HashSet<string>();
							for (int n = 0; n < xmlNodeList5.Count; n++)
							{
								string innerText4 = xmlNodeList5[n].SelectSingleNode("./strBarcode").InnerText;
								if (!hashSet5.Contains(innerText4))
								{
									hashSet5.Add(innerText4);
									stringBuilder5.Append("('" + xmlNodeList5[n].SelectSingleNode("./strBarcode").InnerText + "',");
									stringBuilder5.Append("'" + xmlNodeList5[n].SelectSingleNode("./Barcode").InnerText + "',");
									stringBuilder5.Append("'" + xmlNodeList5[n].SelectSingleNode("./ferID").InnerText + "',");
									stringBuilder5.Append("'" + xmlNodeList5[n].SelectSingleNode("./ferName").InnerText + "',");
									stringBuilder5.Append("'" + xmlNodeList5[n].SelectSingleNode("./Subsidy").InnerText + "',");
									stringBuilder5.Append("'" + xmlNodeList5[n].SelectSingleNode("./unitWeight").InnerText + "',");
									stringBuilder5.Append("'" + xmlNodeList5[n].SelectSingleNode("./isWS").InnerText + "',");
									stringBuilder5.Append("'" + xmlNodeList5[n].SelectSingleNode("./Type").InnerText + "',");
									stringBuilder5.Append("'" + xmlNodeList5[n].SelectSingleNode("./lastUpdateDate").InnerText + "',");
									stringBuilder5.Append("'" + xmlNodeList5[n].SelectSingleNode("./isDelete").InnerText + "'),");
								}
							}
							if (stringBuilder5.Length > 0)
							{
								tb_status.Invoke(new MethodInvoker(_003CbackgroundWorker1_DoWork_003Eb__6_8));
								str5 += stringBuilder5.ToString().Substring(0, stringBuilder5.Length - 1);
								DataBaseUtilities.DBOperation(Program.ConnectionString, str5, null, CommandOperationType.ExecuteNonQuery);
								string sql10 = "UPDATE hypos_GOODSLST  set status = (select 'D' from HyFerData a where hypos_GOODSLST.GDSNO = a.strBarcode and a.isDelete = 'Y') WHERE EXISTS(select 1 from HyFerData a where hypos_GOODSLST.GDSNO = a.strBarcode and a.isDelete = 'Y')";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql10, null, CommandOperationType.ExecuteNonQuery);
								string sql11 = "UPDATE hypos_GOODSLST  set barcode = (select a.Barcode from HyFerData a where hypos_GOODSLST.GDSNO = a.strBarcode and a.isDelete = 'N'),  GDName = (select a.ferName from HyFerData a where hypos_GOODSLST.GDSNO = a.strBarcode and a.isDelete = 'N'),  CName = (select a.ferName from HyFerData a where hypos_GOODSLST.GDSNO = a.strBarcode and a.isDelete = 'N'), SubsidyMoney = (select a.Subsidy from HyFerData a where hypos_GOODSLST.GDSNO = a.strBarcode and a.isDelete = 'N'),  capacity = (select a.unitWeight from HyFerData a where hypos_GOODSLST.GDSNO = a.strBarcode and a.isDelete = 'N')  WHERE EXISTS(select 1 from HyFerData a where hypos_GOODSLST.GDSNO = a.strBarcode and a.isDelete = 'N')";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql11, null, CommandOperationType.ExecuteNonQuery);
								string sql12 = "DELETE FROM HyFerData WHERE EXISTS (select 1 from hypos_GOODSLST WHERE hypos_GOODSLST.GDSNO = HyFerData.strBarcode)";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql12, null, CommandOperationType.ExecuteNonQuery);
								string sql13 = "INSERT INTO hypos_GOODSLST(GDSNO,barcode,GDName,CName,SubsidyMoney,capacity,ISWS,status,CLA1NO,SubsidyFertilizer)SELECT strBarcode,Barcode,ferName,ferName,Subsidy,unitWeight,isWS,'N','0303','Y' FROM HyFerData";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql13, null, CommandOperationType.ExecuteNonQuery);
								DataBaseUtilities.DBOperation(Program.ConnectionString, "DELETE FROM HyFerData", null, CommandOperationType.ExecuteNonQuery);
							}
						}
					}
				}
				backgroundWorker1.ReportProgress(80);
				if (list5.Count > 0)
				{
					using (List<string>.Enumerator enumerator = list5.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							_003C_003Ec__DisplayClass6_6 _003C_003Ec__DisplayClass6_7 = new _003C_003Ec__DisplayClass6_6();
							_003C_003Ec__DisplayClass6_7.CS_0024_003C_003E8__locals6 = _003C_003Ec__DisplayClass6_;
							_003C_003Ec__DisplayClass6_7.s = enumerator.Current;
							tb_status.Invoke(new MethodInvoker(_003C_003Ec__DisplayClass6_7._003CbackgroundWorker1_DoWork_003Eb__9));
							XmlDocument xmlDocument6 = new XmlDocument();
							xmlDocument6.Load(_003C_003Ec__DisplayClass6_7.s);
							XmlNodeList xmlNodeList6 = xmlDocument6.SelectNodes("//record");
							string str6 = "INSERT INTO HyTempBarcode(licType,licNo,seq,spec,barcode,Unit, Volume,lastUpdateDate,isDelete) VALUES ";
							StringBuilder stringBuilder6 = new StringBuilder();
							HashSet<string> hashSet6 = new HashSet<string>();
							for (int num = 0; num < xmlNodeList6.Count; num++)
							{
								string innerText5 = xmlNodeList6[num].SelectSingleNode("./barcode").InnerText;
								if (!hashSet6.Contains(innerText5))
								{
									hashSet6.Add(innerText5);
									stringBuilder6.Append("('" + xmlNodeList6[num].SelectSingleNode("./licType").InnerText + "',");
									stringBuilder6.Append("'" + xmlNodeList6[num].SelectSingleNode("./licNo").InnerText + "',");
									stringBuilder6.Append("'" + xmlNodeList6[num].SelectSingleNode("./seq").InnerText + "',");
									stringBuilder6.Append("'" + xmlNodeList6[num].SelectSingleNode("./spec").InnerText + "',");
									stringBuilder6.Append("'" + xmlNodeList6[num].SelectSingleNode("./barcode").InnerText + "',");
									stringBuilder6.Append("'" + xmlNodeList6[num].SelectSingleNode("./unit").InnerText + "',");
									stringBuilder6.Append("'" + xmlNodeList6[num].SelectSingleNode("./volume").InnerText + "',");
									stringBuilder6.Append("'" + xmlNodeList6[num].SelectSingleNode("./lastUpdateDate").InnerText + "',");
									stringBuilder6.Append("'" + xmlNodeList6[num].SelectSingleNode("./isDelete").InnerText + "'),");
								}
							}
							if (stringBuilder6.Length > 0)
							{
								tb_status.Invoke(new MethodInvoker(_003CbackgroundWorker1_DoWork_003Eb__6_10));
								str6 += stringBuilder6.ToString().Substring(0, stringBuilder6.Length - 1);
								DataBaseUtilities.DBOperation(Program.ConnectionString, str6, null, CommandOperationType.ExecuteNonQuery);
								DataBaseUtilities.DBOperation(Program.ConnectionString, "update HyTempBarcode set pesticideId = (select pesticideId from HyLicence b where HyTempBarcode.licNo = b.licNo and HyTempBarcode.licType = b.licType ), pesticideName = (select pesticideName from HyLicence b where HyTempBarcode.licNo = b.licNo and HyTempBarcode.licType = b.licType),    formCode = (select formCode from HyLicence b where HyTempBarcode.licNo = b.licNo and HyTempBarcode.licType = b.licType),              contents = (select contents from HyLicence b where HyTempBarcode.licNo = b.licNo and HyTempBarcode.licType = b.licType ),              brandName = (select brandName from HyLicence b where HyTempBarcode.licNo = b.licNo and HyTempBarcode.licType = b.licType),            domManufName = (select domManufName from HyLicence b where HyTempBarcode.licNo = b.licNo and HyTempBarcode.licType = b.licType)", null, CommandOperationType.ExecuteNonQuery);
								DataBaseUtilities.DBOperation(Program.ConnectionString, "update hypos_GOODSLST set  status = (select 'D' from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'Y')  WHERE EXISTS (select 1 from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'Y') ", null, CommandOperationType.ExecuteNonQuery);
								DataBaseUtilities.DBOperation(Program.ConnectionString, "update hypos_GOODSLST set  domManufId = (select licNo from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),     licType = (select licType from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),     pesticideId = (select pesticideId from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),     GDName = (select brandName from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),               brandName = (select domManufName from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),               CName = (select pesticideName from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),               formCode = (select formCode from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),             contents = (select contents from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),             spec = (select spec from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),             capacity = (select Volume||Unit from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N')            WHERE EXISTS (select 1 from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N' and hypos_GOODSLST.status !='D') ", null, CommandOperationType.ExecuteNonQuery);
								DataBaseUtilities.DBOperation(Program.ConnectionString, "update hypos_GOODSLST set  domManufId = (select licNo from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),     licType = (select licType from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),     pesticideId = (select pesticideId from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),     GDName = (select brandName from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),               brandName = (select domManufName from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),               CName = (select pesticideName from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),               formCode = (select formCode from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),             contents = (select contents from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),             spec = (select spec from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N'),             capacity = (select Volume||Unit from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N')             , status = (select 'N' from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N')  WHERE EXISTS (select 1 from HyTempBarcode a where hypos_GOODSLST.GDSNO = a.barcode and a.isDelete = 'N' and hypos_GOODSLST.status ='D') ", null, CommandOperationType.ExecuteNonQuery);
								string sql14 = "DELETE FROM HyTempBarcode WHERE EXISTS (select 1 from hypos_GOODSLST WHERE hypos_GOODSLST.GDSNO = HyTempBarcode.barcode)";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql14, null, CommandOperationType.ExecuteNonQuery);
								string sql15 = "INSERT INTO hypos_GOODSLST(GDSNO,barcode,domManufId,ISWS,GDName,brandName,CName,formCode,contents,spec,capacity,CLA1NO,Cost,Price,status,licType,pesticideId)  select barcode,barcode,licNo,ISWS,brandName,domManufName, pesticideName,formCode,contents,spec,Volume||Unit,cls,0,0,'N',licType,pesticideId from HyTempBarcode ";
								DataBaseUtilities.DBOperation(Program.ConnectionString, sql15, null, CommandOperationType.ExecuteNonQuery);
								DataBaseUtilities.DBOperation(Program.ConnectionString, "DELETE FROM HyTempBarcode", null, CommandOperationType.ExecuteNonQuery);
							}
						}
					}
				}
				DataBaseUtilities.DBOperation(Program.ConnectionString, "UPDATE hypos_SysParam SET DownloadLastUpdateDate = {0} ", new string[1]
				{
					DateTime.Now.ToString("yyyyMMddHHmmss")
				}, CommandOperationType.ExecuteNonQuery);
				string text5 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				string[,] strFieldArray = new string[3, 2]
				{
					{
						"status",
						"0"
					},
					{
						"updateType",
						4.ToString()
					},
					{
						"updateDate",
						text5
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray = new string[3, 2]
				{
					{
						"status",
						"0"
					},
					{
						"updateType",
						5.ToString()
					},
					{
						"updateDate",
						text5
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray = new string[3, 2]
				{
					{
						"status",
						"0"
					},
					{
						"updateType",
						7.ToString()
					},
					{
						"updateDate",
						text5
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray = new string[3, 2]
				{
					{
						"status",
						"0"
					},
					{
						"updateType",
						6.ToString()
					},
					{
						"updateDate",
						text5
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray = new string[3, 2]
				{
					{
						"status",
						"0"
					},
					{
						"updateType",
						3.ToString()
					},
					{
						"updateDate",
						text5
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray = new string[3, 2]
				{
					{
						"status",
						"0"
					},
					{
						"updateType",
						8.ToString()
					},
					{
						"updateDate",
						text5
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray, null, CommandOperationType.ExecuteNonQuery);
				tb_status.Invoke(new MethodInvoker(_003CbackgroundWorker1_DoWork_003Eb__6_11));
				backgroundWorker1.ReportProgress(100);
			}
			catch (Exception ex)
			{
				_003C_003Ec__DisplayClass6_7 _003C_003Ec__DisplayClass6_8 = new _003C_003Ec__DisplayClass6_7();
				_003C_003Ec__DisplayClass6_8.CS_0024_003C_003E8__locals7 = _003C_003Ec__DisplayClass6_;
				Exception ex2 = _003C_003Ec__DisplayClass6_8.ex = ex;
				string text6 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				string[,] strFieldArray2 = new string[3, 2]
				{
					{
						"status",
						"1"
					},
					{
						"updateType",
						4.ToString()
					},
					{
						"updateDate",
						text6
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray2 = new string[3, 2]
				{
					{
						"status",
						"1"
					},
					{
						"updateType",
						5.ToString()
					},
					{
						"updateDate",
						text6
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray2 = new string[3, 2]
				{
					{
						"status",
						"1"
					},
					{
						"updateType",
						7.ToString()
					},
					{
						"updateDate",
						text6
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray2 = new string[3, 2]
				{
					{
						"status",
						"1"
					},
					{
						"updateType",
						6.ToString()
					},
					{
						"updateDate",
						text6
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray2 = new string[3, 2]
				{
					{
						"status",
						"1"
					},
					{
						"updateType",
						3.ToString()
					},
					{
						"updateDate",
						text6
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				strFieldArray2 = new string[3, 2]
				{
					{
						"status",
						"1"
					},
					{
						"updateType",
						8.ToString()
					},
					{
						"updateDate",
						text6
					}
				};
				DataBaseUtilities.DBOperation(Program.ConnectionString, TableOperation.Insert, "", "hypos_Synchronize_main_log", null, null, strFieldArray2, null, CommandOperationType.ExecuteNonQuery);
				tb_status.Invoke(new MethodInvoker(_003C_003Ec__DisplayClass6_8._003CbackgroundWorker1_DoWork_003Eb__12));
				MessageBox.Show("下載資料發生問題");
			}
		}

		private void unZip(List<string> zipFilePathArray, List<string> outputArray)
		{
			_003C_003Ec__DisplayClass7_0 _003C_003Ec__DisplayClass7_ = new _003C_003Ec__DisplayClass7_0();
			_003C_003Ec__DisplayClass7_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass7_.zipFilePathArray = zipFilePathArray;
			_003C_003Ec__DisplayClass7_1 _003C_003Ec__DisplayClass7_2 = new _003C_003Ec__DisplayClass7_1();
			_003C_003Ec__DisplayClass7_2.CS_0024_003C_003E8__locals1 = _003C_003Ec__DisplayClass7_;
			_003C_003Ec__DisplayClass7_2.i = 0;
			while (_003C_003Ec__DisplayClass7_2.i < _003C_003Ec__DisplayClass7_2.CS_0024_003C_003E8__locals1.zipFilePathArray.Count)
			{
				tb_status.Invoke(new MethodInvoker(_003C_003Ec__DisplayClass7_2._003CunZip_003Eb__0));
				string text = _003C_003Ec__DisplayClass7_2.CS_0024_003C_003E8__locals1.zipFilePathArray[_003C_003Ec__DisplayClass7_2.i].Substring(0, _003C_003Ec__DisplayClass7_2.CS_0024_003C_003E8__locals1.zipFilePathArray[_003C_003Ec__DisplayClass7_2.i].IndexOf("\\") + 1);
				using (SevenZipExtractor sevenZipExtractor = new SevenZipExtractor(_003C_003Ec__DisplayClass7_2.CS_0024_003C_003E8__locals1.zipFilePathArray[_003C_003Ec__DisplayClass7_2.i], "!2012Baphiq$"))
				{
					sevenZipExtractor.ExtractArchive(text);
					foreach (string archiveFileName in sevenZipExtractor.ArchiveFileNames)
					{
						outputArray.Add(text + archiveFileName);
					}
				}
				tb_status.Invoke(new MethodInvoker(_003CunZip_003Eb__7_1));
				_003C_003Ec__DisplayClass7_2.i++;
			}
		}

		private void downloadZip(string xmlString, string path, List<string> zipFilePathArray)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xmlString);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//record");
			for (int i = 0; i < xmlNodeList.Count; i++)
			{
				_003C_003Ec__DisplayClass8_0 _003C_003Ec__DisplayClass8_ = new _003C_003Ec__DisplayClass8_0();
				_003C_003Ec__DisplayClass8_._003C_003E4__this = this;
				string innerText = xmlNodeList.Item(i).InnerText;
				_003C_003Ec__DisplayClass8_.zipFile = innerText.Substring(innerText.LastIndexOf("/") + 1, innerText.Length - innerText.LastIndexOf("/") - 1);
				tb_status.Invoke(new MethodInvoker(_003C_003Ec__DisplayClass8_._003CdownloadZip_003Eb__0));
				zipFilePathArray.Add(path + _003C_003Ec__DisplayClass8_.zipFile);
				new WebClient().DownloadFile(innerText, path + _003C_003Ec__DisplayClass8_.zipFile);
				tb_status.Invoke(new MethodInvoker(_003CdownloadZip_003Eb__8_1));
			}
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			pbStatus.Value = e.ProgressPercentage;
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (pbStatus.Value == 100)
			{
				tb_status.AppendText("下載完成!!!");
				MessageBox.Show("下載完成");
			}
			btn_close.Enabled = true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS_Client.frmDownload));
			pbStatus = new System.Windows.Forms.ProgressBar();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			tb_status = new System.Windows.Forms.TextBox();
			btn_close = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			SuspendLayout();
			pbStatus.Location = new System.Drawing.Point(12, 78);
			pbStatus.Name = "pbStatus";
			pbStatus.Size = new System.Drawing.Size(382, 23);
			pbStatus.Step = 1;
			pbStatus.TabIndex = 0;
			backgroundWorker1.WorkerReportsProgress = true;
			backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
			backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
			backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
			tb_status.Location = new System.Drawing.Point(12, 118);
			tb_status.Multiline = true;
			tb_status.Name = "tb_status";
			tb_status.ReadOnly = true;
			tb_status.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			tb_status.Size = new System.Drawing.Size(382, 197);
			tb_status.TabIndex = 1;
			btn_close.Enabled = false;
			btn_close.Location = new System.Drawing.Point(148, 332);
			btn_close.Name = "btn_close";
			btn_close.Size = new System.Drawing.Size(111, 23);
			btn_close.TabIndex = 2;
			btn_close.Text = "完成，啟動系統";
			btn_close.UseVisualStyleBackColor = true;
			btn_close.Click += new System.EventHandler(btn_close_Click);
			label1.Font = new System.Drawing.Font("微軟正黑體", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
			label1.ForeColor = System.Drawing.Color.Red;
			label1.Location = new System.Drawing.Point(24, 19);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(348, 44);
			label1.TabIndex = 3;
			label1.Text = "正在更新資料中（初次啟用請稍待3~5分鐘），請停留在此頁面、不要關閉程式或電腦！";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(406, 400);
			base.ControlBox = false;
			base.Controls.Add(label1);
			base.Controls.Add(btn_close);
			base.Controls.Add(tb_status);
			base.Controls.Add(pbStatus);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmDownload";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "下載資料中，請稍候";
			base.Load += new System.EventHandler(frmDownload_Load);
			ResumeLayout(false);
			PerformLayout();
		}

		[CompilerGenerated]
		private void _003CbackgroundWorker1_DoWork_003Eb__6_1()
		{
			tb_status.AppendText("產生資料夾....");
		}

		[CompilerGenerated]
		private void _003CbackgroundWorker1_DoWork_003Eb__6_2()
		{
			tb_status.AppendText("OK! \r\n");
		}

		[CompilerGenerated]
		private void _003CbackgroundWorker1_DoWork_003Eb__6_8()
		{
			tb_status.AppendText("更新肥料資料:" + DateTime.Now.ToLongTimeString() + " \r\n");
		}

		[CompilerGenerated]
		private void _003CbackgroundWorker1_DoWork_003Eb__6_10()
		{
			tb_status.AppendText("更新農藥資料:" + DateTime.Now.ToLongTimeString() + " \r\n");
		}

		[CompilerGenerated]
		private void _003CbackgroundWorker1_DoWork_003Eb__6_11()
		{
			tb_status.AppendText("更新完成: " + DateTime.Now.ToLongTimeString() + " \r\n");
		}

		[CompilerGenerated]
		private void _003CunZip_003Eb__7_1()
		{
			tb_status.AppendText("OK! \r\n");
		}

		[CompilerGenerated]
		private void _003CdownloadZip_003Eb__8_1()
		{
			tb_status.AppendText("OK! \r\n");
		}
	}
}
