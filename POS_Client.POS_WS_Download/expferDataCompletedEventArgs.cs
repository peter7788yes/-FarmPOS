using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace POS_Client.POS_WS_Download
{
	[GeneratedCode("System.Web.Services", "4.6.1055.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class expferDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public byte[] Result
		{
			get
			{
				RaiseExceptionIfNecessary();
				return (byte[])results[0];
			}
		}

		internal expferDataCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}
