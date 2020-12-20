using System;

namespace StringExtension
{
	public static class StringExtension
	{
		public static bool isNumber(this string str)
		{
			double varriable;
			return double.TryParse(str, out varriable);
		}

		public static bool isLetter(this string str)
		{
			for (int i = 0; i < str.Length; i++)
				if (!Char.IsLetter(str[i]))
					return false;

			return true;
		}

		public static bool isUpper(this string str)
		{
			for (int i = 0; i < str.Length; i++)
				if (!Char.IsUpper(str[i]))
					return false;

			return true;
		}
	}
}
