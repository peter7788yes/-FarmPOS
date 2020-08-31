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

namespace POS_Client.POS_WS_Download
{
	[GeneratedCode("System.Web.Services", "4.6.1055.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "PosCExpDataPortBinding", Namespace = "http://ws.baphiq.jo.hyweb/")]
	public class ExpData : SoapHttpClientProtocol
	{
		private SendOrPostCallback expPesticideLicOperationCompleted;

		private SendOrPostCallback expPestCropRelationOperationCompleted;

		private SendOrPostCallback expCropOperationCompleted;

		private SendOrPostCallback expStoreOperationCompleted;

		private SendOrPostCallback expBarCodeOperationCompleted;

		private SendOrPostCallback expPestOperationCompleted;

		private SendOrPostCallback expferDataOperationCompleted;

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

		public event expPesticideLicCompletedEventHandler expPesticideLicCompleted;

		public event expPestCropRelationCompletedEventHandler expPestCropRelationCompleted;

		public event expCropCompletedEventHandler expCropCompleted;

		public event expStoreCompletedEventHandler expStoreCompleted;

		public event expBarCodeCompletedEventHandler expBarCodeCompleted;

		public event expPestCompletedEventHandler expPestCompleted;

		public event expferDataCompletedEventHandler expferDataCompleted;

		public ExpData()
		{
			Url = Settings.Default.行政院農委會防檢局POS系統_POS_WS_Download_ExpData;
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
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", IsNullable = true)]
		public byte[] expPesticideLic([XmlElement(Form = XmlSchemaForm.Unqualified)] string storeId, [XmlElement(Form = XmlSchemaForm.Unqualified)] string lastUpdateDate, [XmlElement(Form = XmlSchemaForm.Unqualified)] string key)
		{
			return (byte[])Invoke("expPesticideLic", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			})[0];
		}

		public void expPesticideLicAsync(string storeId, string lastUpdateDate, string key)
		{
			expPesticideLicAsync(storeId, lastUpdateDate, key, null);
		}

		public void expPesticideLicAsync(string storeId, string lastUpdateDate, string key, object userState)
		{
			if (expPesticideLicOperationCompleted == null)
			{
				expPesticideLicOperationCompleted = new SendOrPostCallback(OnexpPesticideLicOperationCompleted);
			}
			InvokeAsync("expPesticideLic", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			}, expPesticideLicOperationCompleted, userState);
		}

		private void OnexpPesticideLicOperationCompleted(object arg)
		{
			if (this.expPesticideLicCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.expPesticideLicCompleted(this, new expPesticideLicCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", IsNullable = true)]
		public byte[] expPestCropRelation([XmlElement(Form = XmlSchemaForm.Unqualified)] string storeId, [XmlElement(Form = XmlSchemaForm.Unqualified)] string lastUpdateDate, [XmlElement(Form = XmlSchemaForm.Unqualified)] string key)
		{
			return (byte[])Invoke("expPestCropRelation", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			})[0];
		}

		public void expPestCropRelationAsync(string storeId, string lastUpdateDate, string key)
		{
			expPestCropRelationAsync(storeId, lastUpdateDate, key, null);
		}

		public void expPestCropRelationAsync(string storeId, string lastUpdateDate, string key, object userState)
		{
			if (expPestCropRelationOperationCompleted == null)
			{
				expPestCropRelationOperationCompleted = new SendOrPostCallback(OnexpPestCropRelationOperationCompleted);
			}
			InvokeAsync("expPestCropRelation", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			}, expPestCropRelationOperationCompleted, userState);
		}

		private void OnexpPestCropRelationOperationCompleted(object arg)
		{
			if (this.expPestCropRelationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.expPestCropRelationCompleted(this, new expPestCropRelationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", IsNullable = true)]
		public byte[] expCrop([XmlElement(Form = XmlSchemaForm.Unqualified)] string storeId, [XmlElement(Form = XmlSchemaForm.Unqualified)] string lastUpdateDate, [XmlElement(Form = XmlSchemaForm.Unqualified)] string key)
		{
			return (byte[])Invoke("expCrop", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			})[0];
		}

		public void expCropAsync(string storeId, string lastUpdateDate, string key)
		{
			expCropAsync(storeId, lastUpdateDate, key, null);
		}

		public void expCropAsync(string storeId, string lastUpdateDate, string key, object userState)
		{
			if (expCropOperationCompleted == null)
			{
				expCropOperationCompleted = new SendOrPostCallback(OnexpCropOperationCompleted);
			}
			InvokeAsync("expCrop", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			}, expCropOperationCompleted, userState);
		}

		private void OnexpCropOperationCompleted(object arg)
		{
			if (this.expCropCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.expCropCompleted(this, new expCropCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", IsNullable = true)]
		public byte[] expStore([XmlElement(Form = XmlSchemaForm.Unqualified)] string storeId, [XmlElement(Form = XmlSchemaForm.Unqualified)] string lastUpdateDate, [XmlElement(Form = XmlSchemaForm.Unqualified)] string key)
		{
			return (byte[])Invoke("expStore", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			})[0];
		}

		public void expStoreAsync(string storeId, string lastUpdateDate, string key)
		{
			expStoreAsync(storeId, lastUpdateDate, key, null);
		}

		public void expStoreAsync(string storeId, string lastUpdateDate, string key, object userState)
		{
			if (expStoreOperationCompleted == null)
			{
				expStoreOperationCompleted = new SendOrPostCallback(OnexpStoreOperationCompleted);
			}
			InvokeAsync("expStore", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			}, expStoreOperationCompleted, userState);
		}

		private void OnexpStoreOperationCompleted(object arg)
		{
			if (this.expStoreCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.expStoreCompleted(this, new expStoreCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", IsNullable = true)]
		public byte[] expBarCode([XmlElement(Form = XmlSchemaForm.Unqualified)] string storeId, [XmlElement(Form = XmlSchemaForm.Unqualified)] string lastUpdateDate, [XmlElement(Form = XmlSchemaForm.Unqualified)] string key)
		{
			return (byte[])Invoke("expBarCode", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			})[0];
		}

		public void expBarCodeAsync(string storeId, string lastUpdateDate, string key)
		{
			expBarCodeAsync(storeId, lastUpdateDate, key, null);
		}

		public void expBarCodeAsync(string storeId, string lastUpdateDate, string key, object userState)
		{
			if (expBarCodeOperationCompleted == null)
			{
				expBarCodeOperationCompleted = new SendOrPostCallback(OnexpBarCodeOperationCompleted);
			}
			InvokeAsync("expBarCode", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			}, expBarCodeOperationCompleted, userState);
		}

		private void OnexpBarCodeOperationCompleted(object arg)
		{
			if (this.expBarCodeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.expBarCodeCompleted(this, new expBarCodeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", IsNullable = true)]
		public byte[] expPest([XmlElement(Form = XmlSchemaForm.Unqualified)] string storeId, [XmlElement(Form = XmlSchemaForm.Unqualified)] string lastUpdateDate, [XmlElement(Form = XmlSchemaForm.Unqualified)] string key)
		{
			return (byte[])Invoke("expPest", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			})[0];
		}

		public void expPestAsync(string storeId, string lastUpdateDate, string key)
		{
			expPestAsync(storeId, lastUpdateDate, key, null);
		}

		public void expPestAsync(string storeId, string lastUpdateDate, string key, object userState)
		{
			if (expPestOperationCompleted == null)
			{
				expPestOperationCompleted = new SendOrPostCallback(OnexpPestOperationCompleted);
			}
			InvokeAsync("expPest", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			}, expPestOperationCompleted, userState);
		}

		private void OnexpPestOperationCompleted(object arg)
		{
			if (this.expPestCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.expPestCompleted(this, new expPestCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		[SoapDocumentMethod("", RequestNamespace = "http://ws.baphiq.jo.hyweb/", ResponseNamespace = "http://ws.baphiq.jo.hyweb/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement("return", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary", IsNullable = true)]
		public byte[] expferData([XmlElement(Form = XmlSchemaForm.Unqualified)] string storeId, [XmlElement(Form = XmlSchemaForm.Unqualified)] string lastUpdateDate, [XmlElement(Form = XmlSchemaForm.Unqualified)] string key)
		{
			return (byte[])Invoke("expferData", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			})[0];
		}

		public void expferDataAsync(string storeId, string lastUpdateDate, string key)
		{
			expferDataAsync(storeId, lastUpdateDate, key, null);
		}

		public void expferDataAsync(string storeId, string lastUpdateDate, string key, object userState)
		{
			if (expferDataOperationCompleted == null)
			{
				expferDataOperationCompleted = new SendOrPostCallback(OnexpferDataOperationCompleted);
			}
			InvokeAsync("expferData", new object[3]
			{
				storeId,
				lastUpdateDate,
				key
			}, expferDataOperationCompleted, userState);
		}

		private void OnexpferDataOperationCompleted(object arg)
		{
			if (this.expferDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.expferDataCompleted(this, new expferDataCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
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
