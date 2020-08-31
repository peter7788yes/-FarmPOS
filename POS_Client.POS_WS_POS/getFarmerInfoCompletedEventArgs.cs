using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace POS_Client.POS_WS_POS
{
	[GeneratedCode("System.Web.Services", "4.6.1055.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class getFarmerInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public string Result
		{
			get
			{
				RaiseExceptionIfNecessary();
				return (string)results[0];
			}
		}

		internal getFarmerInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
