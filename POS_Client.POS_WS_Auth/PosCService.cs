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

namespace POS_Client.POS_WS_Auth
{
	[GeneratedCode("System.Web.Services", "4.7.2053.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "PosCServicePortBinding", Namespace = "http://ws.baphiq.jo.hyweb/")]
	public class PosCService : SoapHttpClientProtocol
	{
		private SendOrPostCallback hasInUseOperationCompleted;

		private SendOrPostCallback vendorDataOperationCompleted;

		private SendOrPostCallback toRestoreOperationCompleted;

		private SendOrPostCallback uploadApplySerialOperationCompleted;

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

		public event hasInUseCompletedEventHandler hasInUseCompleted;

		public event vendorDataCompletedEventHandler vendorDataCompleted;

		public event toRestoreCompletedEventHandler toRestoreCompleted;

		public event uploadApplySerialCompletedEventHandler uploadApplySerialCompleted;

		public PosCService()
		{
			Url = Settings.Default.農藥銷售簡易POS_POS_WS_Auth_PosCService;
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
		public string hasInUse([XmlElement(Form = XmlSchemaForm.Unqualified)] string applySerial, [XmlElement(Form = XmlSchemaForm.Unqualified)] string version)
		{
			return (string)Invoke("hasInUse", new object[2]
			{
				applySerial,
				version
			})[0];
		}

		public void hasInUseAsync(string applySerial, string version)
		{
			hasInUseAsync(applySerial, version, null);
		}

		public void hasInUseAsync(string applySerial, string version, object userState)
		{
			if (hasInUseOperationCompleted == null)
			{
				hasInUseOperationCompleted = new SendOrPostCallback(OnhasInUseOperationCompleted);
			}
			InvokeAsync("hasInUse", new object[2]
			{
				applySerial,
				version
			}, hasInUseOperationCompleted, userState);
		}

		private void OnhasInUseOperationCompleted(object arg)
		{
			if (this.hasInUseCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.hasInUseCompleted(this, new hasInUseCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified)]
		public string vendorData([XmlElement(Form = XmlSchemaForm.Unqualified)] string storedId, [XmlElement(Form = XmlSchemaForm.Unqualified)] string vendorId)
		{
			return (string)Invoke("vendorData", new object[2]
			{
				storedId,
				vendorId
			})[0];
		}

		public void vendorDataAsync(string storedId, string vendorId)
		{
			vendorDataAsync(storedId, vendorId, null);
		}

		public void vendorDataAsync(string storedId, string vendorId, object userState)
		{
			if (vendorDataOperationCompleted == null)
			{
				vendorDataOperationCompleted = new SendOrPostCallback(OnvendorDataOperationCompleted);
			}
			InvokeAsync("vendorData", new object[2]
			{
				storedId,
				vendorId
			}, vendorDataOperationCompleted, userState);
		}

		private void OnvendorDataOperationCompleted(object arg)
		{
			if (this.vendorDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.vendorDataCompleted(this, new vendorDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified)]
		public string toRestore([XmlElement(Form = XmlSchemaForm.Unqualified)] string verifyData)
		{
			return (string)Invoke("toRestore", new object[1]
			{
				verifyData
			})[0];
		}

		public void toRestoreAsync(string verifyData)
		{
			toRestoreAsync(verifyData, null);
		}

		public void toRestoreAsync(string verifyData, object userState)
		{
			if (toRestoreOperationCompleted == null)
			{
				toRestoreOperationCompleted = new SendOrPostCallback(OntoRestoreOperationCompleted);
			}
			InvokeAsync("toRestore", new object[1]
			{
				verifyData
			}, toRestoreOperationCompleted, userState);
		}

		private void OntoRestoreOperationCompleted(object arg)
		{
			if (this.toRestoreCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.toRestoreCompleted(this, new toRestoreCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified)]
		public string uploadApplySerial([XmlElement(Form = XmlSchemaForm.Unqualified)] string applySerial)
		{
			return (string)Invoke("uploadApplySerial", new object[1]
			{
				applySerial
			})[0];
		}

		public void uploadApplySerialAsync(string applySerial)
		{
			uploadApplySerialAsync(applySerial, null);
		}

		public void uploadApplySerialAsync(string applySerial, object userState)
		{
			if (uploadApplySerialOperationCompleted == null)
			{
				uploadApplySerialOperationCompleted = new SendOrPostCallback(OnuploadApplySerialOperationCompleted);
			}
			InvokeAsync("uploadApplySerial", new object[1]
			{
				applySerial
			}, uploadApplySerialOperationCompleted, userState);
		}

		private void OnuploadApplySerialOperationCompleted(object arg)
		{
			if (this.uploadApplySerialCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.uploadApplySerialCompleted(this, new uploadApplySerialCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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
