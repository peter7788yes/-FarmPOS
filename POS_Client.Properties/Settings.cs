using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace POS_Client.Properties
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return defaultInstance;
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[SpecialSetting(SpecialSetting.WebServiceUrl)]
		[DefaultSettingValue("http://10.10.4.161:80/mPosCService/ExpData")]
		public string 行政院農委會防檢局POS系統_POS_WS_Download_ExpData
		{
			get
			{
				return (string)this["行政院農委會防檢局POS系統_POS_WS_Download_ExpData"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[SpecialSetting(SpecialSetting.WebServiceUrl)]
		[DefaultSettingValue("http://10.10.4.161:8888/mPosMiddleware/POSService")]
		public string 農藥銷售簡易POS_POS_WS_POS_POSService
		{
			get
			{
				return (string)this["農藥銷售簡易POS_POS_WS_POS_POSService"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[SpecialSetting(SpecialSetting.WebServiceUrl)]
		[DefaultSettingValue("http://10.10.4.161:8888/mPosCService/PosCService")]
		public string 農藥銷售簡易POS_POS_WS_Auth_PosCService
		{
			get
			{
				return (string)this["農藥銷售簡易POS_POS_WS_Auth_PosCService"];
			}
		}

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[SpecialSetting(SpecialSetting.WebServiceUrl)]
		[DefaultSettingValue("http://10.10.4.161:8888/mPosCService/uploadData")]
		public string 農藥銷售簡易POS_POS_WS_Upload_uploadData
		{
			get
			{
				return (string)this["農藥銷售簡易POS_POS_WS_Upload_uploadData"];
			}
		}
	}
}
