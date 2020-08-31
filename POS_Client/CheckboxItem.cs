using System.Runtime.CompilerServices;

namespace POS_Client
{
	public class CheckboxItem
	{
		[CompilerGenerated]
		private string _003CText_003Ek__BackingField;

		[CompilerGenerated]
		private object _003CValue_003Ek__BackingField;

		public string Text
		{
			[CompilerGenerated]
			get
			{
				return _003CText_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CText_003Ek__BackingField = value;
			}
		}

		public object Value
		{
			[CompilerGenerated]
			get
			{
				return _003CValue_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CValue_003Ek__BackingField = value;
			}
		}

		public CheckboxItem(string text, object value)
		{
			Text = text;
			Value = value;
		}

		public override string ToString()
		{
			return Text;
		}
	}
}
