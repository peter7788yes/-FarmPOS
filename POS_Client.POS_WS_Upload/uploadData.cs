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

namespace POS_Client.POS_WS_Upload
{
	[GeneratedCode("System.Web.Services", "4.7.2053.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "PosCUploadDataPortBinding", Namespace = "http://ws.baphiq.jo.hyweb/")]
	public class uploadData : SoapHttpClientProtocol
	{
		private SendOrPostCallback uploadCusDataOperationCompleted;

		private SendOrPostCallback uploadSalesDataOperationCompleted;

		private SendOrPostCallback uploadCountDataOperationCompleted;

		private SendOrPostCallback uploadShipDataOperationCompleted;

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

		public event uploadCusDataCompletedEventHandler uploadCusDataCompleted;

		public event uploadSalesDataCompletedEventHandler uploadSalesDataCompleted;

		public event uploadCountDataCompletedEventHandler uploadCountDataCompleted;

		public event uploadShipDataCompletedEventHandler uploadShipDataCompleted;

		public uploadData()
		{
			Url = Settings.Default.農藥銷售簡易POS_POS_WS_Upload_uploadData;
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

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified)]
		public string uploadCusData([XmlElement(Form = XmlSchemaForm.Unqualified)] string uploadCusXML)
		{
			return (string)Invoke("uploadCusData", new object[1]
			{
				uploadCusXML
			})[0];
		}

		public void uploadCusDataAsync(string uploadCusXML)
		{
			uploadCusDataAsync(uploadCusXML, null);
		}

		public void uploadCusDataAsync(string uploadCusXML, object userState)
		{
			if (uploadCusDataOperationCompleted == null)
			{
				uploadCusDataOperationCompleted = new SendOrPostCallback(OnuploadCusDataOperationCompleted);
			}
			InvokeAsync("uploadCusData", new object[1]
			{
				uploadCusXML
			}, uploadCusDataOperationCompleted, userState);
		}

		private void OnuploadCusDataOperationCompleted(object arg)
		{
			if (this.uploadCusDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.uploadCusDataCompleted(this, new uploadCusDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified)]
		public string uploadSalesData([XmlElement(Form = XmlSchemaForm.Unqualified)] string uploadSalesXML)
		{
			return (string)Invoke("uploadSalesData", new object[1]
			{
				uploadSalesXML
			})[0];
		}

		public void uploadSalesDataAsync(string uploadSalesXML)
		{
			uploadSalesDataAsync(uploadSalesXML, null);
		}

		public void uploadSalesDataAsync(string uploadSalesXML, object userState)
		{
			if (uploadSalesDataOperationCompleted == null)
			{
				uploadSalesDataOperationCompleted = new SendOrPostCallback(OnuploadSalesDataOperationCompleted);
			}
			InvokeAsync("uploadSalesData", new object[1]
			{
				uploadSalesXML
			}, uploadSalesDataOperationCompleted, userState);
		}

		private void OnuploadSalesDataOperationCompleted(object arg)
		{
			if (this.uploadSalesDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.uploadSalesDataCompleted(this, new uploadSalesDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified)]
		public string uploadCountData([XmlElement(Form = XmlSchemaForm.Unqualified)] string uploadCountXML)
		{
			return (string)Invoke("uploadCountData", new object[1]
			{
				uploadCountXML
			})[0];
		}

		public void uploadCountDataAsync(string uploadCountXML)
		{
			uploadCountDataAsync(uploadCountXML, null);
		}

		public void uploadCountDataAsync(string uploadCountXML, object userState)
		{
			if (uploadCountDataOperationCompleted == null)
			{
				uploadCountDataOperationCompleted = new SendOrPostCallback(OnuploadCountDataOperationCompleted);
			}
			InvokeAsync("uploadCountData", new object[1]
			{
				uploadCountXML
			}, uploadCountDataOperationCompleted, userState);
		}

		private void OnuploadCountDataOperationCompleted(object arg)
		{
			if (this.uploadCountDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.uploadCountDataCompleted(this, new uploadCountDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified)]
		public string uploadShipData([XmlElement(Form = XmlSchemaForm.Unqualified)] string uploadShipmentXML)
		{
			return (string)Invoke("uploadShipData", new object[1]
			{
				uploadShipmentXML
			})[0];
		}

		public void uploadShipDataAsync(string uploadShipmentXML)
		{
			uploadShipDataAsync(uploadShipmentXML, null);
		}

		public void uploadShipDataAsync(string uploadShipmentXML, object userState)
		{
			if (uploadShipDataOperationCompleted == null)
			{
				uploadShipDataOperationCompleted = new SendOrPostCallback(OnuploadShipDataOperationCompleted);
			}
			InvokeAsync("uploadShipData", new object[1]
			{
				uploadShipmentXML
			}, uploadShipDataOperationCompleted, userState);
		}

		private void OnuploadShipDataOperationCompleted(object arg)
		{
			if (this.uploadShipDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.uploadShipDataCompleted(this, new uploadShipDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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
