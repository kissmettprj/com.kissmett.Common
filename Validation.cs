using System.Text.RegularExpressions;
using System;

namespace com.kissmett.Common
{
	/// <summary>
	/// Ｃ＃的正则表达式验证类
	/// </summary>
	public class Validation
	{
		public Validation()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		// Function to test for Positive Integers.
        /// <summary>
        /// 验证正整数
        /// </summary>
        /// <param name="strNumber">待验证的字符串</param>
        /// <returns>如果验证为真，返回true;否则,返回false</returns>
		public static bool IsNaturalNumber(String strNumber)
		{
			Regex objNotNaturalPattern=new Regex("[^0-9]");
			Regex objNaturalPattern=new Regex("0*[1-9][0-9]*");

			return !objNotNaturalPattern.IsMatch(strNumber) &&
				objNaturalPattern.IsMatch(strNumber);
		}

		// Function to test for Positive Integers with zero inclusive
		/// <summary>
		/// 验证非负整数
		/// </summary>
		/// <param name="strNumber">待验证的字符串</param>
		/// <returns>如果验证为真，返回true;否则,返回false</returns>
		public static bool IsWholeNumber(String strNumber)
		{
			Regex objNotWholePattern=new Regex("[^0-9]");

			return !objNotWholePattern.IsMatch(strNumber);
		}

		// Function to Test for Integers both Positive & Negative
		/// <summary>
		/// 验证整数
		/// </summary>
		/// <param name="strNumber">待验证的字符串</param>
		/// <returns>如果验证为真，返回true;否则,返回false</returns>
		public static bool IsInteger(String strNumber)
		{
			Regex objNotIntPattern=new Regex("[^0-9-]");
			Regex objIntPattern=new Regex("^-[0-9]+$|^[0-9]+$");

			return !objNotIntPattern.IsMatch(strNumber) &&
				objIntPattern.IsMatch(strNumber);
		}

		// Function to Test for Positive Number both Integer & Real
		/// <summary>
		/// 验证正数
		/// </summary>
		/// <param name="strNumber">待验证的字符串</param>
		/// <returns>如果验证为真，返回true;否则,返回false</returns>
		public static bool IsPositiveNumber(String strNumber)
		{
			Regex objNotPositivePattern=new Regex("[^0-9.]");
			Regex objPositivePattern=new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
			Regex objTwoDotPattern=new Regex("[0-9]*[.][0-9]*[.][0-9]*");

			return !objNotPositivePattern.IsMatch(strNumber) &&
				objPositivePattern.IsMatch(strNumber) &&
				!objTwoDotPattern.IsMatch(strNumber);
		}

		// Function to test whether the string is valid number or not
		/// <summary>
		/// 验证数字
		/// </summary>
		/// <param name="strNumber">待验证的字符串</param>
		/// <returns>如果验证为真，返回true;否则,返回false</returns>
		public static bool IsNumber(String strNumber)
		{
			Regex objNotNumberPattern=new Regex("[^0-9.-]");
			Regex objTwoDotPattern=new Regex("[0-9]*[.][0-9]*[.][0-9]*");
			Regex objTwoMinusPattern=new Regex("[0-9]*[-][0-9]*[-][0-9]*");
			String strValidRealPattern="^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
			String strValidIntegerPattern="^([-]|[0-9])[0-9]*$";
			Regex objNumberPattern =new Regex("(" + strValidRealPattern +")|(" + strValidIntegerPattern + ")");

			return !objNotNumberPattern.IsMatch(strNumber) &&
				!objTwoDotPattern.IsMatch(strNumber) &&
				!objTwoMinusPattern.IsMatch(strNumber) &&
				objNumberPattern.IsMatch(strNumber);
		}

		// Function To test for Alphabets.
		/// <summary>
		/// 验证英文字母
		/// </summary>
		/// <param name="strNumber">待验证的字符串</param>
		/// <returns>如果验证为真，返回true;否则,返回false</returns>
		public static bool IsAlpha(String strToCheck)
		{
			Regex objAlphaPattern=new Regex("[^a-zA-Z]");

			return !objAlphaPattern.IsMatch(strToCheck);
		}

		// Function to Check for AlphaNumeric.
		/// <summary>
		/// 验证英文字母和数字
		/// </summary>
		/// <param name="strNumber">待验证的字符串</param>
		/// <returns>如果验证为真，返回true;否则,返回false</returns>
		public static bool IsAlphaNumeric(String strToCheck)
		{
			Regex objAlphaNumericPattern=new Regex("[^a-zA-Z0-9]");

			return !objAlphaNumericPattern.IsMatch(strToCheck); 
		}
	}
}
