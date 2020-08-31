using System;
using System.Collections.Generic;

namespace POS_Client
{
	internal class Verification
	{
		public static bool checkIDNo(string id)
		{
			List<string> list = new List<string>();
			list.Add("A");
			list.Add("B");
			list.Add("C");
			list.Add("D");
			list.Add("E");
			list.Add("F");
			list.Add("G");
			list.Add("H");
			list.Add("J");
			list.Add("K");
			list.Add("L");
			list.Add("M");
			list.Add("N");
			list.Add("P");
			list.Add("Q");
			list.Add("R");
			list.Add("S");
			list.Add("T");
			list.Add("U");
			list.Add("V");
			list.Add("X");
			list.Add("Y");
			list.Add("W");
			list.Add("Z");
			list.Add("I");
			list.Add("O");
			List<string> list2 = list;
			byte b = Convert.ToByte(id.Trim().Substring(1, 1).ToCharArray()[0]);
			if (id.Trim().Length == 10)
			{
				if (b > 50 || b < 49)
				{
					return false;
				}
				id = id.ToUpper();
				int i;
				for (i = 0; i < list2.Count && !(id.Substring(0, 1) == list2[i]); i++)
				{
				}
				if (i > 25)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
