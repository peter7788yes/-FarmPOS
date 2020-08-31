using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace POS_Client
{
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	internal sealed class TaftOLP : ApplicationSettingsBase
	{
		private static TaftOLP defaultInstance = (TaftOLP)SettingsBase.Synchronized(new TaftOLP());

		public static TaftOLP Default
		{
			get
			{
				return defaultInstance;
			}
		}

		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string UserSetting
		{
			get
			{
				return (string)this["UserSetting"];
			}
			set
			{
				this["UserSetting"] = value;
			}
		}
	}
}
