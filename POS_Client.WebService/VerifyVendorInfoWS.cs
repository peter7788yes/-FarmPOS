using POS_Client.POS_WS_Auth;
using System;
using System.Net.NetworkInformation;
using System.Xml;

namespace POS_Client.WebService
{
	internal class VerifyVendorInfoWS
	{
		public PosCService OLWS;

		public VerifyVendorInfoWS()
		{
			OLWS = new PosCService();
			OLWS.Url = Program.AuthURL;
		}

		public VendorResultObject vendorData(string vendorId)
		{
			Program.Logger.Info("[檢查執照號碼，取得商業名稱] -- 開始");
			VendorResultObject vendorResultObject;
			if (NetworkInterface.GetIsNetworkAvailable())
			{
				try
				{
					Program.Logger.Info("[檢查執照號碼，取得商業名稱] -- 執照號碼上傳至WS 開始");
					Program.Logger.Info("[檢查執照號碼，取得商業名稱] -- 上傳資料\nL:" + Program.LincenseCode + "\nV:" + vendorId + "\n");
					string xml = OLWS.vendorData(Program.LincenseCode, vendorId);
					Program.Logger.Info("[檢查執照號碼，取得商業名稱] -- 執照號碼上傳至WS 結束");
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(xml);
					XmlNode xmlNode = xmlDocument.SelectSingleNode("/vendorData/success");
					XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/vendorData/vendorId");
					XmlNode xmlNode3 = xmlDocument.SelectSingleNode("/vendorData/vendorName");
					XmlNode xmlNode4 = xmlDocument.SelectSingleNode("/vendorData/errorCode");
					XmlNode xmlNode5 = xmlDocument.SelectSingleNode("/vendorData/message");
					vendorResultObject = new VendorResultObject();
					if (xmlNode != null)
					{
						vendorResultObject.success = xmlNode.InnerText;
					}
					if (xmlNode2 != null)
					{
						vendorResultObject.vendorId = xmlNode2.InnerText;
					}
					if (xmlNode3 != null)
					{
						vendorResultObject.vendorName = xmlNode3.InnerText;
					}
					if (xmlNode4 != null)
					{
						vendorResultObject.errorCode = xmlNode4.InnerText;
					}
					if (xmlNode5 != null)
					{
						vendorResultObject.message = xmlNode5.InnerText;
					}
					Program.Logger.Info("[檢查執照號碼，取得商業名稱] -- 由WS收到訊息為\nS:" + (string.IsNullOrEmpty(vendorResultObject.success) ? "Null" : vendorResultObject.success) + "\nVI:" + (string.IsNullOrEmpty(vendorResultObject.vendorId) ? "Null" : vendorResultObject.vendorId) + "\nVN:" + (string.IsNullOrEmpty(vendorResultObject.vendorName) ? "Null" : vendorResultObject.vendorName) + "\nE:" + (string.IsNullOrEmpty(vendorResultObject.errorCode) ? "Null" : vendorResultObject.errorCode) + "\nM:" + (string.IsNullOrEmpty(vendorResultObject.message) ? "Null" : vendorResultObject.message) + "\n");
					return vendorResultObject;
				}
				catch (Exception ex)
				{
					Program.Logger.Fatal("[檢查執照號碼，取得商業名稱] -- 發生例外狀況:" + ex.ToString());
					vendorResultObject = new VendorResultObject();
					vendorResultObject.success = "N";
					vendorResultObject.message = "連線主機失敗";
					return vendorResultObject;
				}
			}
			Program.Logger.Info("[檢查執照號碼，取得商業名稱] -- 離線中，請檢查網路");
			vendorResultObject = new VendorResultObject();
			vendorResultObject.success = "N";
			vendorResultObject.message = "離線中，請檢查網路";
			return vendorResultObject;
		}
	}
}
