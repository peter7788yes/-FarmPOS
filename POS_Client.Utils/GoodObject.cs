using System.Runtime.CompilerServices;

namespace POS_Client.Utils
{
	public class GoodObject
	{
		[CompilerGenerated]
		private int _003C_index_003Ek__BackingField;

		[CompilerGenerated]
		private CommodityInfo _003C_GDSName_003Ek__BackingField;

		[CompilerGenerated]
		private string _003C_number_003Ek__BackingField;

		[CompilerGenerated]
		private string _003C_barcode_003Ek__BackingField;

		[CompilerGenerated]
		private string _003C_cropId_003Ek__BackingField;

		[CompilerGenerated]
		private string _003C_pestId_003Ek__BackingField;

		public int _index
		{
			[CompilerGenerated]
			get
			{
				return _003C_index_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003C_index_003Ek__BackingField = value;
			}
		}

		public CommodityInfo _GDSName
		{
			[CompilerGenerated]
			get
			{
				return _003C_GDSName_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003C_GDSName_003Ek__BackingField = value;
			}
		}

		public string _number
		{
			[CompilerGenerated]
			get
			{
				return _003C_number_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003C_number_003Ek__BackingField = value;
			}
		}

		public string _barcode
		{
			[CompilerGenerated]
			get
			{
				return _003C_barcode_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003C_barcode_003Ek__BackingField = value;
			}
		}

		public string _cropId
		{
			[CompilerGenerated]
			get
			{
				return _003C_cropId_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003C_cropId_003Ek__BackingField = value;
			}
		}

		public string _pestId
		{
			[CompilerGenerated]
			get
			{
				return _003C_pestId_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003C_pestId_003Ek__BackingField = value;
			}
		}

		public GoodObject(int index, CommodityInfo GDSName, string number, string barcode, string cropId, string pestId)
		{
			_index = index;
			_GDSName = GDSName;
			_number = number;
			_barcode = barcode;
			_cropId = cropId;
			_pestId = pestId;
		}
	}
}
