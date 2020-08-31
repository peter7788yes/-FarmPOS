using POS_Client.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace POS_Client.POS_WS_POS
{
	[GeneratedCode("System.Web.Services", "4.6.1055.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "POSServicePortBinding", Namespace = "http://posm.jo.hyweb/")]
	public class POSService : SoapHttpClientProtocol
	{
		private SendOrPostCallback getFarmerInfoOperationCompleted;

		private SendOrPostCallback sendRetailDataOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if (IsLocalFileSystemWebService(base.Url) && !useDefaultCredentialsSetExplicitly && !IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				useDefaultCredentialsSetExplicitly = true;
			}
		}

		public event getFarmerInfoCompletedEventHandler getFarmerInfoCompleted;

		public event sendRetailDataCompletedEventHandler sendRetailDataCompleted;

		public POSService()
		{
			Url = Settings.Default.農藥銷售簡易POS_POS_WS_POS_POSService;
			if (IsLocalFileSystemWebService(Url))
			{
				UseDefaultCredentials = true;
				useDefaultCredentialsSetExplicitly = false;
			}
			else
			{
				useDefaultCredentialsSetExplicitly = true;
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://posm.jo.hyweb/", ResponseNamespace = "http://posm.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified)]
		public string getFarmerInfo([XmlElement(Form = XmlSchemaForm.Unqualified)] string storeId, [XmlElement(Form = XmlSchemaForm.Unqualified)] string strACC, [XmlElement(Form = XmlSchemaForm.Unqualified)] string strPW, [XmlElement(Form = XmlSchemaForm.Unqualified)] string cardNO)
		{
			return (string)Invoke("getFarmerInfo", new object[4]
			{
				storeId,
				strACC,
				strPW,
				cardNO
			})[0];
		}

		public void getFarmerInfoAsync(string storeId, string strACC, string strPW, string cardNO)
		{
			getFarmerInfoAsync(storeId, strACC, strPW, cardNO, null);
		}

		public void getFarmerInfoAsync(string storeId, string strACC, string strPW, string cardNO, object userState)
		{
			if (getFarmerInfoOperationCompleted == null)
			{
				getFarmerInfoOperationCompleted = new SendOrPostCallback(OngetFarmerInfoOperationCompleted);
			}
			InvokeAsync("getFarmerInfo", new object[4]
			{
				storeId,
				strACC,
				strPW,
				cardNO
			}, getFarmerInfoOperationCompleted, userState);
		}

		private void OngetFarmerInfoOperationCompleted(object arg)
		{
			if (this.getFarmerInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.getFarmerInfoCompleted(this, new getFarmerInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://posm.jo.hyweb/", ResponseNamespace = "http://posm.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified)]
		public string sendRetailData([XmlElement(Form = XmlSchemaForm.Unqualified)] string storeId, [XmlElement(Form = XmlSchemaForm.Unqualified)] string strACC, [XmlElement(Form = XmlSchemaForm.Unqualified)] string strPW, [XmlElement(Form = XmlSchemaForm.Unqualified)] string storeNO)
		{
			return (string)Invoke("sendRetailData", new object[4]
			{
				storeId,
				strACC,
				strPW,
				storeNO
			})[0];
		}

		public void sendRetailDataAsync(string storeId, string strACC, string strPW, string storeNO)
		{
			sendRetailDataAsync(storeId, strACC, strPW, storeNO, null);
		}

		public void sendRetailDataAsync(string storeId, string strACC, string strPW, string storeNO, object userState)
		{
			if (sendRetailDataOperationCompleted == null)
			{
				sendRetailDataOperationCompleted = new SendOrPostCallback(OnsendRetailDataOperationCompleted);
			}
			InvokeAsync("sendRetailData", new object[4]
			{
				storeId,
				strACC,
				strPW,
				storeNO
			}, sendRetailDataOperationCompleted, userState);
		}

		private void OnsendRetailDataOperationCompleted(object arg)
		{
			if (this.sendRetailDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.sendRetailDataCompleted(this, new sendRetailDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			if (url == null || url == string.Empty)
			{
				return false;
			}
			Uri uri = new Uri(url);
			if (uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			return false;
		}
	}
}
