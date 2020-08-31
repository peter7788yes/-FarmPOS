using Newtonsoft.Json;
using POS_Client.POS_WS_Auth;
using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using T00SharedLibraryDotNet20;

namespace POS_Client.WebService
{
	internal class AuthenticationWs
	{
		public PosCService OLWS;

		public AuthenticationWs()
		{
			OLWS = new PosCService();
			OLWS.Url = Program.AuthURL;
		}

		public RegisterResultObject uploadApplySerial(string newRegisterCode)
		{
			Program.Logger.Info("[上傳申請序號] -- 開始");
			RegisterResultObject registerResultObject;
			if (Program.IsDeployClickOnce)
			{
				if (NetworkInterface.GetIsNetworkAvailable())
				{
					try
					{
						Program.Logger.Info("[上傳申請序號] -- 序號上傳至WS 開始");
						Program.Logger.Info("[上傳申請序號] -- 上傳資料\nR:" + newRegisterCode + "\n");
						registerResultObject = JsonConvert.DeserializeObject<RegisterResultObject>(OLWS.uploadApplySerial(newRegisterCode));
						Program.Logger.Info("[上傳申請序號] -- 序號上傳至WS 結束");
						Program.Logger.Info("[上傳申請序號] -- 由WS收到訊息為\nI:" + (string.IsNullOrEmpty(registerResultObject.isSuccess) ? "Null" : registerResultObject.isSuccess) + "\nE:" + (string.IsNullOrEmpty(registerResultObject.errorCode) ? "Null" : registerResultObject.errorCode) + "\nM:" + (string.IsNullOrEmpty(registerResultObject.message) ? "Null" : registerResultObject.message));
						if (string.IsNullOrEmpty(registerResultObject.isSuccess))
						{
							registerResultObject.isSuccess = "false";
							registerResultObject.message = "連線主機失敗";
							return registerResultObject;
						}
						return registerResultObject;
					}
					catch (Exception ex)
					{
						Program.Logger.Fatal("[上傳申請序號] -- 發生例外狀況:" + ex.ToString());
						registerResultObject = new RegisterResultObject();
						registerResultObject.isSuccess = "false";
						registerResultObject.message = "連線主機失敗";
						return registerResultObject;
					}
				}
				Program.Logger.Info("[上傳申請序號] -- 離線中，請檢查網路");
				registerResultObject = new RegisterResultObject();
				registerResultObject.isSuccess = "false";
				registerResultObject.message = "離線中，請檢查網路";
			}
			else
			{
				Program.Logger.Info("[上傳申請序號] -- 非線上更新版本，進入下一步驟");
				registerResultObject = new RegisterResultObject();
				registerResultObject.isSuccess = "true";
				registerResultObject.message = "本機測試:上傳成功";
			}
			return registerResultObject;
		}

		public AuthResultObject hasInUseFirst(string RegisterCode)
		{
			Program.Logger.Info("[第一次審核] -- 開始");
			AuthResultObject authResultObject;
			if (Program.IsDeployClickOnce)
			{
				if (NetworkInterface.GetIsNetworkAvailable())
				{
					try
					{
						Program.Logger.Info("[第一次審核] -- 註冊碼上傳至WS 開始");
						Program.Logger.Info("[第一次審核] -- 上傳資料\nV:" + Program.Version + "\n");
						authResultObject = JsonConvert.DeserializeObject<AuthResultObject>(OLWS.hasInUse(RegisterCode, Program.Version));
						Program.Logger.Info("[第一次審核] -- 註冊碼上傳至WS 結束");
						Program.Logger.Info("[第一次審核] -- 由WS收到訊息為\nI:" + (string.IsNullOrEmpty(authResultObject.inUse) ? "Null" : authResultObject.inUse) + "\nE:" + (string.IsNullOrEmpty(authResultObject.errorCode) ? "Null" : authResultObject.errorCode) + "\nM:" + (string.IsNullOrEmpty(authResultObject.message) ? "Null" : authResultObject.message) + "\nS:" + (string.IsNullOrEmpty(authResultObject.serial) ? "Null" : authResultObject.serial) + "\nST:" + (string.IsNullOrEmpty(authResultObject.shopType) ? "Null" : authResultObject.shopType) + "\n");
						if (string.IsNullOrEmpty(authResultObject.inUse))
						{
							authResultObject.inUse = "false";
							authResultObject.message = "連線主機失敗";
							return authResultObject;
						}
						return authResultObject;
					}
					catch (Exception ex)
					{
						Program.Logger.Fatal("[第一次審核] -- 發生例外狀況:" + ex.ToString());
						authResultObject = new AuthResultObject();
						authResultObject.inUse = "false";
						authResultObject.message = "連線主機失敗";
						return authResultObject;
					}
				}
				Program.Logger.Info("[第一次審核] -- 離線中，請檢查網路");
				authResultObject = new AuthResultObject();
				authResultObject.inUse = "false";
				authResultObject.message = "無法審核:離線中";
			}
			else
			{
				Program.Logger.Info("[第一次審核] -- 非線上更新版本，進入下一步驟");
				authResultObject = new AuthResultObject();
				authResultObject.inUse = "true";
				authResultObject.message = "本機測試:審核成功";
				authResultObject.serial = "01";
				authResultObject.shopType = "0";
			}
			return authResultObject;
		}

		public AuthResultObject hasInUseRetry(string RegisterCode)
		{
			Program.Logger.Info("[再次審查是否授權中] -- 開始");
			AuthResultObject authResultObject;
			if (Program.IsDeployClickOnce)
			{
				if (NetworkInterface.GetIsNetworkAvailable())
				{
					try
					{
						Program.Logger.Info("[再次審查是否授權中] -- 註冊碼上傳至WS 開始");
						Program.Logger.Info("[再次審查是否授權中] -- 上傳資料\nV:" + Program.Version + "\n");
						authResultObject = JsonConvert.DeserializeObject<AuthResultObject>(OLWS.hasInUse(RegisterCode, Program.Version));
						Program.Logger.Info("[再次審查是否授權中] -- 註冊碼上傳至WS 結束");
						Program.Logger.Info("[再次審查是否授權中] -- 由WS收到訊息為\nI:" + (string.IsNullOrEmpty(authResultObject.inUse) ? "Null" : authResultObject.inUse) + "\nE:" + (string.IsNullOrEmpty(authResultObject.errorCode) ? "Null" : authResultObject.errorCode) + "\nM:" + (string.IsNullOrEmpty(authResultObject.message) ? "Null" : authResultObject.message) + "\nS:" + (string.IsNullOrEmpty(authResultObject.serial) ? "Null" : authResultObject.serial) + "\nST:" + (string.IsNullOrEmpty(authResultObject.shopType) ? "Null" : authResultObject.shopType) + "\n");
						if (string.IsNullOrEmpty(authResultObject.inUse))
						{
							authResultObject.inUse = "true";
							authResultObject.message = "連線主機失敗";
							authResultObject.serial = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT SiteNo FROM hypos_SysParam", null, CommandOperationType.ExecuteScalar).ToString();
							return authResultObject;
						}
						return authResultObject;
					}
					catch (Exception ex)
					{
						Program.Logger.Fatal("[再次審查是否授權中] -- 發生例外狀況:" + ex.ToString());
						authResultObject = new AuthResultObject();
						authResultObject.inUse = "true";
						authResultObject.message = "連線主機失敗";
						authResultObject.serial = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT SiteNo FROM hypos_SysParam", null, CommandOperationType.ExecuteScalar).ToString();
						MessageBox.Show("連線主機失敗");
						return authResultObject;
					}
				}
				Program.Logger.Info("[再次審查是否授權中] -- 離線中，請檢查網路");
				authResultObject = new AuthResultObject();
				authResultObject.inUse = "true";
				authResultObject.message = "無法審核:離線中";
				authResultObject.serial = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT SiteNo FROM hypos_SysParam", null, CommandOperationType.ExecuteScalar).ToString();
				string text = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT IsRetailer FROM hypos_ShopInfoManage", null, CommandOperationType.ExecuteScalar).ToString();
				string text2 = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT IsWholesaler FROM hypos_ShopInfoManage", null, CommandOperationType.ExecuteScalar).ToString();
				if (text.Equals("ON") && text2.Equals("ON"))
				{
					authResultObject.shopType = "0";
				}
				else if (text.Equals("ON") && !text2.Equals("ON"))
				{
					authResultObject.shopType = "1";
				}
				else if (!text.Equals("ON") && text2.Equals("ON"))
				{
					authResultObject.shopType = "2";
				}
				MessageBox.Show("離線中");
			}
			else
			{
				Program.Logger.Info("[再次審查是否授權中] -- 非線上更新版本，進入下一步驟");
				authResultObject = new AuthResultObject();
				authResultObject.inUse = "true";
				authResultObject.message = "本機測試:審核成功";
				authResultObject.serial = DataBaseUtilities.DBOperation(Program.ConnectionString, "SELECT SiteNo FROM hypos_SysParam", null, CommandOperationType.ExecuteScalar).ToString();
				authResultObject.shopType = "0";
			}
			return authResultObject;
		}
	}
}
