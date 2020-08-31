using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace POS_Client.POS_WS_Auth
{
	[GeneratedCode("System.Web.Services", "4.7.2053.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class toRestoreCompletedEventArgs : AsyncCompletedEventArgs
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

		internal toRestoreCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
