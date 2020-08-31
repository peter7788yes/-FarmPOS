using log4net;
using log4net.Config;
using POS_Client.Utils;
using POS_Client.WebService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client
{
	internal static class Program
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

			public static Comparison<FileInfo> _003C_003E9__108_0;

			public static Comparison<FileInfo> _003C_003E9__108_1;

			internal int _003CAutoBackup_003Eb__108_0(FileInfo x, FileInfo y)
			{
				return y.CreationTime.CompareTo(x.CreationTime);
			}

			internal int _003CAutoBackup_003Eb__108_1(FileInfo x, FileInfo y)
			{
				return y.CreationTime.CompareTo(x.CreationTime);
			}
		}

		private static bool _isDeployClickOnce = true;

		private static bool _isHyweb = false;

		private static string _ws = "posadmin";

		private static bool _isFertilizer = true;

		private static bool _isCropPestRange_NEW = true;

		private static bool _isDataTransfer = false;

		private static int _DBSchemaV = 13;

		public static List<GoodObject> goodsTemp = new List<GoodObject>();

		public static List<GoodObjectWithMoney> goodsWithMoneyTemp = new List<GoodObjectWithMoney>();

		public static List<string> membersTemp = new List<string>();

		public static List<string> commodityTemp = new List<string>();

		private static string _version = _isDeployClickOnce ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4) : "1.0";

		private static int _SystemMode = 1;

		private static int _RoleType;

		private static readonly ILog _Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private static string _sysLastUpdate = _isDeployClickOnce ? ApplicationDeployment.CurrentDeployment.TimeOfLastUpdateCheck.ToLongDateString() : "2016/09/06 08:00:00";

		private static string _sysFolder = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 3);

		private static string _DBName = "db.db3";

		private static string _DataPath;

		private static string _ConnString;

		private static string _LincenseCode;

		private static string _SiteNo;

		private static string _ShopType;

		private static string _UploadDataURL = "http://" + (_isHyweb ? "10.10.4.161:8888" : "posadmin.baphiq.gov.tw") + "/mPosCService/uploadData?wsdl";

		private static string _DownloadDataURL = "http://" + (_isHyweb ? "10.10.4.161:8888" : "posadmin.baphiq.gov.tw") + "/mPosCService/ExpData?wsdl";

		private static string _AuthURL = "http://" + (_isHyweb ? "10.10.4.161:8888" : "posadmin.baphiq.gov.tw") + "/mPosCService/PosCService?wsdl";

		private static string _VerificationURL = "http://" + (_isHyweb ? "10.10.4.161:8888" : "posadmin.baphiq.gov.tw") + "/mPosMiddleware/POSService?wsdl";

		private static string _casher;

		private static string _CRCFile;

		private static string _HardDiskSerialNo;

		private static string _RegisterCode;

		private static string _crcStatus;

		private static string _printerName;

		private static bool _isSaleOfFertilizer = false;

		private static bool _isProgramUpgraded = false;

		public static ILog Logger
		{
			get
			{
				return _Logger;
			}
		}

		public static int RoleType
		{
			get
			{
				return _RoleType;
			}
			set
			{
				_RoleType = value;
			}
		}

		public static string ShopType
		{
			get
			{
				return _ShopType;
			}
			set
			{
				_ShopType = value;
			}
		}

		public static string AuthURL
		{
			get
			{
				return _AuthURL;
			}
		}

		public static string DownloadURL
		{
			get
			{
				return _DownloadDataURL;
			}
		}

		public static string UploadDataURL
		{
			get
			{
				return _UploadDataURL;
			}
		}

		public static string VerificationURL
		{
			get
			{
				return _VerificationURL;
			}
		}

		public static bool IsDataTransfer
		{
			get
			{
				return _isDataTransfer;
			}
		}

		public static bool IsCropPestRange_NEW
		{
			get
			{
				return _isCropPestRange_NEW;
			}
		}

		public static bool IsHyweb
		{
			get
			{
				return _isHyweb;
			}
		}

		public static bool IsFertilizer
		{
			get
			{
				return _isFertilizer;
			}
		}

		public static string Version
		{
			get
			{
				return _version;
			}
		}

		public static int SystemMode
		{
			get
			{
				return _SystemMode;
			}
			set
			{
				_SystemMode = value;
			}
		}

		public static string SiteNo
		{
			get
			{
				if (string.IsNullOrEmpty(_SiteNo))
				{
					return "";
				}
				return _SiteNo.PadLeft(2, '0');
			}
			set
			{
				_SiteNo = value;
			}
		}

		public static string LincenseCode
		{
			get
			{
				return _LincenseCode;
			}
			set
			{
				_LincenseCode = value;
			}
		}

		public static string Casher
		{
			get
			{
				return _casher;
			}
			set
			{
				_casher = value;
			}
		}

		public static string ConnectionString
		{
			get
			{
				return _ConnString;
			}
			set
			{
			}
		}

		public static string CRCStatus
		{
			get
			{
				return _crcStatus;
			}
			set
			{
				_crcStatus = value;
			}
		}

		public static string DataPath
		{
			get
			{
				return _DataPath;
			}
			set
			{
			}
		}

		public static string HardDiskSerialNo
		{
			get
			{
				return _HardDiskSerialNo;
			}
			set
			{
			}
		}

		public static string RegisterCode
		{
			get
			{
				return _RegisterCode;
			}
			set
			{
			}
		}

		public static string PrinterName
		{
			get
			{
				return _printerName;
			}
			set
			{
				_printerName = value;
			}
		}

		public static bool IsDeployClickOnce
		{
			get
			{
				return _isDeployClickOnce;
			}
			set
			{
			}
		}

		public static bool IsSaleOfFertilizer
		{
			get
			{
				return _isSaleOfFertilizer;
			}
			set
			{
				_isSaleOfFertilizer = value;
			}
		}

		public static bool Upgraded
		{
			get
			{
				return _isProgramUpgraded;
			}
			set
			{
				_isProgramUpgraded = value;
			}
		}

		public static string SysLastUpdate
		{
			get
			{
				return _sysLastUpdate;
			}
			set
			{
			}
		}

		public static string systemFolder
		{
			get
			{
				return _sysFolder;
			}
			set
			{
				_sysFolder = value;
			}
		}

		[STAThread]
		private static void Main()
		{
			XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
			_Logger.Info("POS_CLIENT 程式啟動 !!");
			switch (_ws)
			{
			case "test":
				_UploadDataURL = "http://10.10.4.161:8888/mPosCService/uploadData?wsdl";
				_DownloadDataURL = "http://10.10.4.161:8888/mPosCService/ExpData?wsdl";
				_AuthURL = "http://10.10.4.161:8888/mPosCService/PosCService?wsdl";
				_VerificationURL = "http://10.10.4.161:8888/mPosMiddleware/POSService?wsdl";
				break;
			case "posadmin":
				_UploadDataURL = "http://posadmin.baphiq.gov.tw/mPosCService/uploadData?wsdl";
				_DownloadDataURL = "http://posadmin.baphiq.gov.tw/mPosCService/ExpData?wsdl";
				_AuthURL = "http://posadmin.baphiq.gov.tw/mPosCService/PosCService?wsdl";
				_VerificationURL = "http://posadmin.baphiq.gov.tw/mPosMiddlewareDisable/POSService?wsdl";
				break;
			case "crop":
				_UploadDataURL = "http://crop.baphiq.gov.tw/mPosCService/uploadData?wsdl";
				_DownloadDataURL = "http://posadmin.baphiq.gov.tw/mPosCService/ExpData?wsdl";
				_AuthURL = "http://crop.baphiq.gov.tw/mPosCService/PosCService?wsdl";
				_VerificationURL = "http://crop.baphiq.gov.tw/mPosMiddleware/POSService?wsdl";
				break;
			}
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				bool createdNew;
				Mutex mutex = new Mutex(true, Application.ProductName, out createdNew);
				if (createdNew)
				{
					new frmUpdate().ShowDialog();
					if (!_isProgramUpgraded && InitSystemParams())
					{
						string dBHardDiskSerialNo = GetDBHardDiskSerialNo();
						if (dBHardDiskSerialNo == "")
						{
							new frmInitSysParam().Show();
							Application.Run();
						}
						else if (dBHardDiskSerialNo != _HardDiskSerialNo)
						{
							new frmInitSysParam().Show();
							Application.Run();
						}
						else
						{
							string sql = "SELECT RegisterCode FROM hypos_RegisterLicense where isApproved = 'Y' order by CreateDate desc limit 1";
							_RegisterCode = Convert.ToString(DataBaseUtilities.DBOperation(ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
							if ("-1".Equals(_RegisterCode))
							{
								new frmInitSysParam().Show();
								Application.Run();
							}
							else
							{
								AuthResultObject authResultObject = new AuthenticationWs().hasInUseRetry(_RegisterCode);
								if (bool.Parse(authResultObject.inUse))
								{
									string sql2 = "SELECT LicenseCode FROM hypos_RegisterLicense where isApproved = 'Y' order by CreateDate desc limit 1";
									_LincenseCode = Convert.ToString(DataBaseUtilities.DBOperation(ConnectionString, sql2, null, CommandOperationType.ExecuteScalar));
									string lastUpdateDate = DataBaseUtilities.DBOperation(ConnectionString, "SELECT DownloadLastUpdateDate FROM hypos_SysParam", null, CommandOperationType.ExecuteScalar).ToString();
									new frmDownload(_LincenseCode, lastUpdateDate).ShowDialog();
									_SiteNo = authResultObject.serial;
									string sql3 = "update hypos_SysParam set SiteNo = {0} ";
									DataBaseUtilities.DBOperation(ConnectionString, sql3, new string[1]
									{
										_SiteNo
									}, CommandOperationType.ExecuteNonQuery);
									ShopType = authResultObject.shopType;
									if (!string.IsNullOrEmpty(ShopType))
									{
										string sql4 = "SELECT ShopIdNo FROM hypos_ShopInfoManage";
										if (((DataTable)DataBaseUtilities.DBOperation(ConnectionString, sql4, null, CommandOperationType.ExecuteReaderReturnDataTable)).Rows.Count > 0)
										{
											if (ShopType.Equals("0"))
											{
												DataBaseUtilities.DBOperation(ConnectionString, "UPDATE hypos_ShopInfoManage SET IsRetailer = {0}, IsWholesaler = {1} ", new string[2]
												{
													"ON",
													"ON"
												}, CommandOperationType.ExecuteNonQuery);
											}
											else if (ShopType.Equals("1"))
											{
												DataBaseUtilities.DBOperation(ConnectionString, "UPDATE hypos_ShopInfoManage SET IsRetailer = {0}, IsWholesaler = {1} ", new string[2]
												{
													"ON",
													"OFF"
												}, CommandOperationType.ExecuteNonQuery);
											}
											else if (ShopType.Equals("2"))
											{
												DataBaseUtilities.DBOperation(ConnectionString, "UPDATE hypos_ShopInfoManage SET IsRetailer = {0}, IsWholesaler = {1} ", new string[2]
												{
													"OFF",
													"ON"
												}, CommandOperationType.ExecuteNonQuery);
											}
										}
										else if (ShopType.Equals("0"))
										{
											DataBaseUtilities.DBOperation(ConnectionString, "INSERT INTO hypos_ShopInfoManage ( IsRetailer, IsWholesaler) VALUES( {0}, {1})", new string[2]
											{
												"ON",
												"ON"
											}, CommandOperationType.ExecuteNonQuery);
										}
										else if (ShopType.Equals("1"))
										{
											DataBaseUtilities.DBOperation(ConnectionString, "INSERT INTO hypos_ShopInfoManage ( IsRetailer, IsWholesaler) VALUES( {0}, {1}) ", new string[2]
											{
												"ON",
												"OFF"
											}, CommandOperationType.ExecuteNonQuery);
										}
										else if (ShopType.Equals("2"))
										{
											DataBaseUtilities.DBOperation(ConnectionString, "INSERT INTO hypos_ShopInfoManage ( IsRetailer, IsWholesaler) VALUES( {0}, {1}) ", new string[2]
											{
												"OFF",
												"ON"
											}, CommandOperationType.ExecuteNonQuery);
										}
									}
									if (IsDataTransfer)
									{
										oldPOS_DataTransfer();
									}
									new frmLogin().Show();
									Application.Run();
								}
								else
								{
									MessageBox.Show(authResultObject.message);
									string[] strParameterArray = new string[2]
									{
										_RegisterCode,
										DateTime.Now.ToString()
									};
									DataBaseUtilities.DBOperation(_ConnString, "UPDATE hypos_RegisterLicense SET isApproved = 'N', ApproveDate = {1} where RegisterCode = {0} ", strParameterArray, CommandOperationType.ExecuteNonQuery);
									new frmInitSysParam().Show();
									Application.Run();
								}
							}
						}
						if (NetworkInterface.GetIsNetworkAvailable())
						{
							try
							{
								new frmUploadData().ShowDialog();
							}
							catch (Exception ex)
							{
								_Logger.Fatal("上傳資料程序發生例外狀況:" + ex.ToString());
								Console.WriteLine("Error Message : " + ex.Message);
								MessageBox.Show("上傳失敗:網路異常");
							}
						}
						AutoBackup();
					}
					_Logger.Info("POS_CLIENT 程式關閉 。");
					mutex.ReleaseMutex();
					if (_isProgramUpgraded)
					{
						Application.Restart();
					}
				}
				else
				{
					MessageBox.Show(string.Format(" \"{0}\" 執行中", Application.ProductName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			catch (Exception ex2)
			{
				_Logger.Fatal("Main發生例外狀況:" + ex2.ToString());
				MessageBox.Show(string.Format("Main發生例外狀況:「{0}」", ex2.ToString()));
			}
		}

		private static string CheckShopType()
		{
			if (!NetworkInterface.GetIsNetworkAvailable())
			{
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(ConnectionString, "SELECT IsRetailer, IsWholesaler FROM hypos_ShopInfoManage", null, CommandOperationType.ExecuteReaderReturnDataTable);
				string text = string.IsNullOrEmpty(dataTable.Rows[0]["IsRetailer"].ToString()) ? "NULL" : dataTable.Rows[0]["IsRetailer"].ToString();
				string text2 = string.IsNullOrEmpty(dataTable.Rows[0]["IsWholesaler"].ToString()) ? "NULL" : dataTable.Rows[0]["IsWholesaler"].ToString();
				if (text.Equals("ON") && text2.Equals("ON"))
				{
					ShopType = "0";
				}
				else if (text.Equals("ON") && (text2.Equals("OFF") || text2.Equals("NULL")))
				{
					ShopType = "1";
				}
				else if ((text.Equals("OFF") || text.Equals("NULL")) && text2.Equals("ON"))
				{
					ShopType = "2";
				}
				else
				{
					ShopType = "-1";
				}
			}
			return "";
		}

		private static void AutoBackup()
		{
			_Logger.Info("DB自動備份 -- 開始");
			string text = DataBaseUtilities.DBOperation(ConnectionString, TableOperation.Select, "AutoBackupPath", "hypos_CommonManage", "", "", null, null, CommandOperationType.ExecuteScalar).ToString();
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string str = "db_a_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".db3";
			File.Copy(DataPath + "\\db.db3", text + "\\" + str);
			_Logger.Info("DB自動備份 -- 完成");
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(text);
				FileInfo[] files = directoryInfo.GetFiles("db_a_*.db3");
				Array.Sort(files, _003C_003Ec._003C_003E9__108_0 ?? (_003C_003Ec._003C_003E9__108_0 = new Comparison<FileInfo>(_003C_003Ec._003C_003E9._003CAutoBackup_003Eb__108_0)));
				int num = 0;
				List<string> list = new List<string>();
				for (num = 0; num < files.Length; num++)
				{
					list.Add(files[num].ToString().Substring(0, 13));
				}
				list = Enumerable.ToList(Enumerable.Distinct(list));
				for (num = 7; num < list.Count; num++)
				{
					list.RemoveAt(num);
				}
				List<int> list2 = new List<int>();
				int num2 = 0;
				for (num2 = 0; num2 < list.Count; num2++)
				{
					int num3 = 0;
					for (num = 0; num < files.Length; num++)
					{
						if (files[num].ToString().Contains(list[num2]))
						{
							num3++;
						}
						if (num3 > 1 && files[num].ToString().Contains(list[num2]))
						{
							list2.Add(num);
						}
					}
				}
				list2 = Enumerable.ToList(Enumerable.Distinct(list2));
				for (int i = 0; i < list2.Count; i++)
				{
					files[list2[i]].Delete();
				}
				files = directoryInfo.GetFiles("db_a_*.db3");
				Array.Sort(files, _003C_003Ec._003C_003E9__108_1 ?? (_003C_003Ec._003C_003E9__108_1 = new Comparison<FileInfo>(_003C_003Ec._003C_003E9._003CAutoBackup_003Eb__108_1)));
				for (num = 7; num < files.Length; num++)
				{
					files[num].Delete();
				}
			}
			catch (Exception ex)
			{
				_Logger.Fatal("清除舊資料時發生例外狀況:" + ex.ToString());
			}
		}

		private static void AutoBackupDT()
		{
			string text = DataBaseUtilities.DBOperation(ConnectionString, TableOperation.Select, "AutoBackupPath", "hypos_CommonManage", "", "", null, null, CommandOperationType.ExecuteScalar).ToString();
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string str = "db_DT_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".db3";
			File.Copy(DataPath + "\\db.db3", text + "\\" + str);
		}

		private static void oldPOS_DataTransfer()
		{
			string value = DataBaseUtilities.DBOperation(ConnectionString, "SELECT SystemMode FROM hypos_SysParam", null, CommandOperationType.ExecuteScalar).ToString();
			string value2 = DataBaseUtilities.DBOperation(ConnectionString, "SELECT IsDataTransfer FROM hypos_SysParam", null, CommandOperationType.ExecuteScalar).ToString();
			if ("Y".Equals(value2) || !"".Equals(value))
			{
				return;
			}
			new frmDataTransfer().ShowDialog();
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
								DataBaseUtilities.DBOperation(ConnectionString, "UPDATE hypos_SysParam SET IsDataTransfer = 'Y'", null, CommandOperationType.ExecuteNonQuery);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("dbDataTransfer發生例外狀況:「{0}」", ex.ToString()));
							}
						}
						else if ("2".Equals(text.Substring(0, 1)))
						{
							DataBaseUtilities.DBOperation(ConnectionString, "UPDATE hypos_SysParam SET IsDataTransfer = 'Y'", null, CommandOperationType.ExecuteNonQuery);
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
			else
			{
				if (!File.Exists("C:\\Hypos\\conn_log.txt"))
				{
					return;
				}
				try
				{
					using (StreamReader streamReader2 = new StreamReader("C:\\\\Hypos\\\\conn_log.txt", Encoding.Unicode))
					{
						string text2 = streamReader2.ReadToEnd();
						if (!"2".Equals(text2.Substring(0, 1)))
						{
							MessageBox.Show("移轉程序失敗並結束。");
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		private static bool InitSystemParams()
		{
			try
			{
				_DataPath = (_isDeployClickOnce ? ApplicationDeployment.CurrentDeployment.DataDirectory : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
				_ConnString = "Data source=" + _DataPath + "\\db.db3;Password=1031;Version=3;Page Size=4096;Cache Size=2000;Synchronous=Full;";
				_CRCFile = _DataPath + "\\Conn.log";
				_HardDiskSerialNo = Encrypt(HardwareInfo.GetHDDSignature());
				CheckDBSchema();
				return true;
			}
			catch (Exception ex)
			{
				_Logger.Fatal("初始化程式參數過程中發生例外狀況 --" + ex.ToString());
				MessageBox.Show(string.Format("初始化程式參數過程中發生例外狀況:「{0}」\r\n\r\n程式執行路徑為:「{1}」", ex.ToString(), DataPath), "例外訊息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return false;
		}

		private static string GetDBHardDiskSerialNo()
		{
			string sql = "SELECT HardDiskSerialNo FROM hypos_RegisterLicense where isApproved = 'Y' order by CreateDate desc limit 1";
			string text = Convert.ToString(DataBaseUtilities.DBOperation(ConnectionString, sql, null, CommandOperationType.ExecuteScalar));
			if ("-1".Equals(text))
			{
				return "";
			}
			return text;
		}

		public static int GetDBVersion()
		{
			try
			{
				DataTable dataTable = (DataTable)DataBaseUtilities.DBOperation(ConnectionString, TableOperation.Select, "*", "hypos_SysParam", "", "", null, null, CommandOperationType.ExecuteReaderReturnDataTable);
				foreach (DataColumn column in dataTable.Columns)
				{
					if (column.Caption == "DBSchemaVersion")
					{
						return Convert.ToInt32(dataTable.Rows[0]["DBSchemaVersion"]);
					}
				}
				return 0;
			}
			catch (Exception ex)
			{
				_Logger.Fatal("取得DB版本過程中發生例外狀況 --" + ex.ToString());
				return -2;
			}
		}

		private static void CheckDBSchema()
		{
			int num = GetDBVersion();
			if (num == -2 || num == _DBSchemaV)
			{
				return;
			}
			try
			{
				if (num < _DBSchemaV && num < 1)
				{
					string sql = "ALTER TABLE hypos_CUST_RTL ADD Verification TEXT";
					string sql2 = "ALTER TABLE hypos_CUST_RTL ADD LastVerificationTime TEXT";
					DataBaseUtilities.DBOperation(ConnectionString, sql, null, CommandOperationType.ExecuteNonQuery);
					DataBaseUtilities.DBOperation(ConnectionString, sql2, null, CommandOperationType.ExecuteNonQuery);
					num = 1;
				}
				if (num < _DBSchemaV && num < 2)
				{
					string sql3 = "ALTER TABLE hypos_GOODSLST ADD oldGDSNO TEXT";
					string sql4 = "ALTER TABLE hypos_CUST_RTL ADD oldVIPNO TEXT; ALTER TABLE hypos_CUST_RTL ADD note TEXT;";
					string sql5 = "ALTER TABLE hypos_Supplier ADD oldVENDO TEXT";
					string sql6 = "ALTER TABLE hypos_main_sell ADD oldECRHDHSNo TEXT";
					string sql7 = "ALTER TABLE hypos_PurchaseGoods_Master ADD oldCBNO TEXT";
					string sql8 = "ALTER TABLE hypos_SysParam ADD IsDataTransfer TEXT";
					DataBaseUtilities.DBOperation(ConnectionString, sql3, null, CommandOperationType.ExecuteNonQuery);
					DataBaseUtilities.DBOperation(ConnectionString, sql4, null, CommandOperationType.ExecuteNonQuery);
					DataBaseUtilities.DBOperation(ConnectionString, sql5, null, CommandOperationType.ExecuteNonQuery);
					DataBaseUtilities.DBOperation(ConnectionString, sql6, null, CommandOperationType.ExecuteNonQuery);
					DataBaseUtilities.DBOperation(ConnectionString, sql7, null, CommandOperationType.ExecuteNonQuery);
					DataBaseUtilities.DBOperation(ConnectionString, sql8, null, CommandOperationType.ExecuteNonQuery);
					num = 2;
				}
				if (num < _DBSchemaV && num < 3)
				{
					string sql9 = "ALTER TABLE hypos_main_sell ADD returnChange INTEGER";
					DataBaseUtilities.DBOperation(ConnectionString, sql9, null, CommandOperationType.ExecuteNonQuery);
					num = 3;
				}
				if (num < _DBSchemaV && num < 4)
				{
					string sql10 = "ALTER TABLE hypos_SysParam ADD IsCheckHyLicenceAndGoodslst TEXT";
					DataBaseUtilities.DBOperation(ConnectionString, sql10, null, CommandOperationType.ExecuteNonQuery);
					num = 4;
				}
				if (num < _DBSchemaV && num < 5)
				{
					string sql11 = "ALTER TABLE hypos_detailsell_log ADD sellDetailId INTEGER";
					DataBaseUtilities.DBOperation(ConnectionString, sql11, null, CommandOperationType.ExecuteNonQuery);
					num = 5;
				}
				if (num < _DBSchemaV && num < 6)
				{
					string[] array = new string[36]
					{
						"ALTER TABLE hypos_Form ADD Retailer INTEGER",
						"ALTER TABLE hypos_Form ADD Wholesaler INTEGER",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 0,ShowOrder = 1 WHERE FormID = 1",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 0,FormColor = 'B',ShowOrder = 2 WHERE FormID = 2",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 0,FormColor = 'B',ShowOrder = 3 WHERE FormID = 3",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 0,ShowOrder = 4 WHERE FormID = 4",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 1,FormColor = 'P',ShowOrder = 6 WHERE FormID = 5",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 0 WHERE FormID = 6",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 0,ShowOrder = 2 WHERE FormID = 7",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 0,ShowOrder = 1 WHERE FormID = 9",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 1,FormColor = 'P',ShowOrder = 8 WHERE FormID = 10",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 1,FormName = '進貨管理',FormColor = 'B' WHERE FormID = 11",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 1,FormName = '廠商管理',FormColor = 'B',ShowOrder = 6 WHERE FormID = 12",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 1,FormColor = 'P',ShowOrder = 7 WHERE FormID = 13",
						"UPDATE hypos_Form SET Retailer = 1,Wholesaler = 1,ShowOrder = 3 WHERE FormID = 14",
						"INSERT INTO hypos_Form ( FormID,FormName,FormClass,FormColor,ShowOrder,FormType,Retailer,Wholesaler)  VALUES( 15,'庫存管理','frmInventoryQuickEdit','B',5,0,1,1)",
						"INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 15,0)",
						"INSERT INTO hypos_Form ( FormID,FormName,FormClass,FormColor,ShowOrder,FormType,Retailer,Wholesaler)  VALUES( 16,'出貨管理','frmDeliveryMangement','O',9,0,0,1)",
						"INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 16,0)",
						"INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 16,1)",
						"INSERT INTO hypos_Form ( FormID,FormName,FormClass,FormColor,ShowOrder,FormType,Retailer,Wholesaler)  VALUES( 17,'庫存管理','frmInventoryQuickEdit','B',5,1,1,1)",
						"INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 17,0)",
						"ALTER TABLE hypos_ShopInfoManage ADD IsRetailer TEXT",
						"ALTER TABLE hypos_ShopInfoManage ADD IsWholesaler TEXT",
						"ALTER TABLE hypos_Supplier ADD vendorId TEXT",
						"ALTER TABLE hypos_Supplier ADD vendorName TEXT",
						"ALTER TABLE hypos_Supplier ADD vendorType TEXT default '0'",
						"ALTER TABLE hypos_PurchaseGoods_Detail ADD BatchNo TEXT",
						"ALTER TABLE hypos_PurchaseGoods_Detail ADD MFGDate TEXT",
						"ALTER TABLE hypos_PurchaseGoods_Detail ADD POSBatchNo TEXT",
						"CREATE TABLE hypos_DeliveryGoods_Master( DeliveryId INTEGER PRIMARY KEY AUTOINCREMENT,   DeliveryNo TEXT NOT NULL, DeliveryCustomNo TEXT, sumDiscount INT,   OriSum INT,   CurSum INT,  items INT,   itemstotal INT,   BusinessName TEXT,   CustomerLicNo TEXT, vendorNo TEXT,  ShopType TEXT,   changcount INT,   status INT,   DeliveryDate TEXT,   editDate TEXT); ",
						"CREATE TABLE hypos_DeliveryGoods_Master_log( DeliveryLogId INTEGER PRIMARY KEY AUTOINCREMENT,   DeliveryNo TEXT,   changeDate TEXT,   isprint TEXT,   iscancel INT,   ischange INT,   sum INT,   sumDiscount INT); ",
						"CREATE TABLE hypos_DeliveryGoods_Detail( DeliveryDeatialId INTEGER PRIMARY KEY AUTOINCREMENT,   DeliveryNo TEXT,   barcode TEXT,   sellingPrice INT,   num INT,   subtotal INT,   total INT,   DeliveryDate TEXT,   editDate TEXT,   BatchNo TEXT,   MFGDate TEXT,   POSBatchNo TEXT); ",
						"CREATE TABLE hypos_DeliveryGoods_Detail_Log( DeliveryDetailLogId INTEGER PRIMARY KEY AUTOINCREMENT,   DeliveryLogId INT,   DeliveryNo TEXT,   barcode TEXT,   sellingPrice INT, diffSellingPrice INT,  num INT,   diffNum INT,   subtotal INT,   total INT,   editDate TEXT, DeliveryDeatialId INT); ",
						"CREATE TABLE hypos_PurchaseGoodsBatchNo_log( id INTEGER PRIMARY KEY AUTOINCREMENT,   POSBatchNo TEXT NOT NULL,   BatchNo TEXT,   MFGDate TEXT,   barcode TEXT,   num INT,   PurchaseNo TEXT); ",
						"CREATE TABLE hypos_BatchNo_log( id INTEGER PRIMARY KEY AUTOINCREMENT,   POSBatchNo TEXT,   barcode TEXT,   num INT,   backlogQuantity INT,   createDate TEXT); "
					};
					for (int i = 0; i < array.Length; i++)
					{
						DataBaseUtilities.DBOperation(ConnectionString, array[i], null, CommandOperationType.ExecuteNonQuery);
					}
					num = 6;
				}
				if (num < _DBSchemaV && num < 7)
				{
					string[] array2 = new string[4]
					{
						"ALTER TABLE hypos_GOODSLST ADD DeliveryPrice INT default 0",
						"ALTER TABLE hypos_GOODSLST ADD DeliveryPriceSetType TEXT default '0'",
						"ALTER TABLE hypos_GOODSLST ADD DeliveryOpenPrice INT default 0",
						"CREATE TABLE hypos_DeliveryPrice_log(   id INTEGER PRIMARY KEY AUTOINCREMENT,   GDSNO TEXT,   price TEXT,   Account TEXT,   editDate TEXT,   status TEXT); "
					};
					for (int j = 0; j < array2.Length; j++)
					{
						DataBaseUtilities.DBOperation(ConnectionString, array2[j], null, CommandOperationType.ExecuteNonQuery);
					}
					num = 7;
				}
				if (num < _DBSchemaV && num < 8)
				{
					string[] array3 = new string[1]
					{
						"ALTER TABLE hypos_DeliveryGoods_Detail ADD IsDeliveryOnly TEXT"
					};
					for (int k = 0; k < array3.Length; k++)
					{
						DataBaseUtilities.DBOperation(ConnectionString, array3[k], null, CommandOperationType.ExecuteNonQuery);
					}
					num = 8;
				}
				if (num < _DBSchemaV && num < 9)
				{
					string[] array4 = new string[5]
					{
						"ALTER TABLE hypos_DeliveryGoods_Detail ADD GoodsTotalCountLog INT",
						"ALTER TABLE hypos_DeliveryGoods_Master ADD CreateDate TEXT",
						"ALTER TABLE hypos_SysParam ADD UploadCountLastUpdateDate TEXT",
						"ALTER TABLE hypos_SysParam ADD UploadShipLastUpdateDate TEXT",
						"CREATE TABLE hypos_Sync_ship_detail_log( shipId INTEGER PRIMARY KEY AUTOINCREMENT, mainLogId INT, DeliveryNo TEXT, status TEXT, shipDateTime TEXT, vendorId TEXT, vendorName TEXT, vendorNO TEXT, itemNO INT, barcode TEXT, itemType TEXT, batchNO TEXT, MFD TEXT, shipQTY INT, salesTYPE TEXT, differNUM TEXT, dataType TEXT, dataName TEXT);"
					};
					for (int l = 0; l < array4.Length; l++)
					{
						DataBaseUtilities.DBOperation(ConnectionString, array4[l], null, CommandOperationType.ExecuteNonQuery);
					}
					num = 9;
				}
				if (num < _DBSchemaV && num < 10)
				{
					string[] array5 = new string[1]
					{
						"ALTER TABLE hypos_PurchaseGoods_Detail_Log ADD adjustType TEXT"
					};
					for (int m = 0; m < array5.Length; m++)
					{
						DataBaseUtilities.DBOperation(ConnectionString, array5[m], null, CommandOperationType.ExecuteNonQuery);
					}
					num = 10;
				}
				if (num < _DBSchemaV && num < 11)
				{
					string[] array6 = new string[4]
					{
						"ALTER TABLE hypos_InventoryAdjustment ADD batchNO TEXT",
						"ALTER TABLE hypos_InventoryAdjustment ADD MFD TEXT",
						"ALTER TABLE hypos_InventoryAdjustment ADD vendorId TEXT",
						"ALTER TABLE hypos_InventoryAdjustment ADD vendorName TEXT"
					};
					for (int n = 0; n < array6.Length; n++)
					{
						DataBaseUtilities.DBOperation(ConnectionString, array6[n], null, CommandOperationType.ExecuteNonQuery);
					}
					num = 11;
				}
				if (num < _DBSchemaV && num < 12)
				{
					string[] array7 = new string[7]
					{
						"INSERT INTO hypos_Form ( FormID,FormName,FormClass,FormColor,ShowOrder,FormType,Retailer,Wholesaler)  VALUES( 18,'無銷售回報','frmNoSaleReport','O',10,0,1,1)",
						"INSERT INTO hypos_Form ( FormID,FormName,FormClass,FormColor,ShowOrder,FormType,Retailer,Wholesaler)  VALUES( 19,'無銷售回報','frmNoSaleReport','O',7,1,1,1)",
						"INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 18,0)",
						"INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 18,1)",
						"INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 19,0)",
						"INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 19,1)",
						"ALTER TABLE hypos_GOODSLST ADD hot_key TEXT"
					};
					for (int num2 = 0; num2 < array7.Length; num2++)
					{
						DataBaseUtilities.DBOperation(ConnectionString, array7[num2], null, CommandOperationType.ExecuteNonQuery);
					}
					num = 12;
				}
				if (num < _DBSchemaV && num < 13)
				{
					string[] array8 = new string[6]
					{
						" INSERT INTO hypos_Form ( FormID,FormName,FormClass,FormColor,ShowOrder,FormType,Retailer,Wholesaler)  VALUES( 20,'劣農藥查詢','frmBadPesticide','Coffee',11,0,1,1)",
						" INSERT INTO hypos_Form ( FormID,FormName,FormClass,FormColor,ShowOrder,FormType,Retailer,Wholesaler)  VALUES( 21,'劣農藥查詢','frmBadPesticide','Coffee',8,1,1,1)",
						" INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 20,0) ",
						" INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 20,1) ",
						" INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 21,0) ",
						" INSERT INTO hypos_ACL ( FormID,UserType)  VALUES( 21,1) "
					};
					for (int num3 = 0; num3 < array8.Length; num3++)
					{
						DataBaseUtilities.DBOperation(ConnectionString, array8[num3], null, CommandOperationType.ExecuteNonQuery);
					}
					num = 13;
				}
				DataBaseUtilities.DBOperation(ConnectionString, "Update hypos_SysParam set DBSchemaVersion = '" + _DBSchemaV + "'", null, CommandOperationType.ExecuteNonQuery);
			}
			catch (Exception ex)
			{
				_Logger.Fatal("確認及更新 DB Schema 過程中發生例外狀況 --" + ex.ToString());
			}
		}

		private static RijndaelManaged GetRijndaelManaged(string secretKey)
		{
			byte[] array = new byte[16];
			byte[] bytes = Encoding.UTF8.GetBytes(secretKey);
			Array.Copy(bytes, array, Math.Min(array.Length, bytes.Length));
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Mode = CipherMode.CBC;
			rijndaelManaged.Padding = PaddingMode.PKCS7;
			rijndaelManaged.KeySize = 128;
			rijndaelManaged.BlockSize = 128;
			rijndaelManaged.Key = array;
			rijndaelManaged.IV = array;
			return rijndaelManaged;
		}

		private static byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
		{
			return rijndaelManaged.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
		}

		public static string Encrypt(string plainText)
		{
			return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(plainText), GetRijndaelManaged("Hyweb@POS#E00T00")));
		}
	}
}
