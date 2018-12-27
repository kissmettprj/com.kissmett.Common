using System;
using System.Configuration;
using System.Collections.Specialized;

using System.Text;
using System.Text.RegularExpressions;

using System.Data;
using System.Globalization;

using System.Diagnostics;

using System.Security.Cryptography;

//using System.Web;
//using System.Web.SessionState;


namespace com.kissmett.Common
{
	/// <summary>
	/// 常用函数，与特定的应用程序无关，方便代码重用。
	/// </summary>
	/// <remarks>gwd, 2004-04-12</remarks>
	public sealed class Functions
	{

        //输入密码
        public static string InputPassword()
        {
            string password = "";
            while (true)
            {
                //存储用户输入的按键，并且在输入的位置不显示字符
                ConsoleKeyInfo ck = Console.ReadKey(true);

                //判断用户是否按下的Enter键
                if (ck.Key != ConsoleKey.Enter)
                {
                    if (ck.Key != ConsoleKey.Backspace)
                    {
                        //将用户输入的字符存入字符串中
                        password += ck.KeyChar.ToString();
                        //将用户输入的字符替换为*
                        Console.Write("*");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(password) && password.Length >= 1)
                        {
                            password = password.Remove(password.Length - 1, 1);
                        }
                        //删除错误的字符
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    Console.WriteLine();

                    break;
                }
            }
            return password;
        }

        public static string GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }

            return byte2String;
        }


        /// <summary>
        /// 用MD5加密字符串，可选择生成16位或者32位的加密字符串
        /// </summary>
        /// <param name="password">待加密的字符串</param>
        /// <param name="bit">位数，一般取值16 或 32</param>
        /// <returns>返回的加密后的字符串</returns>
        public static string MD5Encrypt(string password, int bit)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(password));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            if (bit == 16)
                return tmp.ToString().Substring(8, 16);
            else
                if (bit == 32) return tmp.ToString();//默认情况
                else return string.Empty;
        }

	
		/// <summary>
		/// Since this class provides only static methods, make the default constructor private to prevent 
		/// instances from being created with "new Functions()"
		/// </summary>
		private Functions() {}

		public static void Debug(string msg)
		{
			System.Diagnostics.Debug.WriteLine(msg);
		}
    
		#region property
    
        //public static string ApplicationDirectory
        //{
        //    get 
        //    {
        //        return Functions.GetValidDirectoryName(HttpContext.Current.Request.ApplicationPath);
        //    }
        //}
    
		#endregion propery

    #region 数据有效性检查和类型转换

    /// <summary>
    /// 检查一个字符串看其是否为null或者string.Empty.
    /// </summary>
    /// <remarks>gwd, 2004-04-12</remarks>
    /// <param name="str">要检查的字符串</param>
    /// <returns>
    /// true -- null or string.Empty
    /// false -- str.length > 0
    /// </returns>
    public static bool IsValidString(string str)
    {
      return !(str == null || str == string.Empty);
    }

    /// <summary>
    /// 检查字符串是否为空，返回有效值。
    /// </summary>
    /// <param name="toCheck">待检查字符串</param>
    /// <returns>返回字符串</returns>
    public static string ParseStr(object toCheck, string defaultValue)
    {
      if(toCheck == null)
        return defaultValue;
      else
        return toCheck.ToString();
    }
    public static string ParseStr(object toCheck)
    {
      return Functions.ParseStr(toCheck, "");
    }
    
    /// <summary>
    /// 将string转化为int，发生错误时，返回缺省值。
    /// </summary>
    /// <param name="toParse">要转化为数字的字符串</param>
    /// <param name="defaultValue">缺省数值</param>
    /// <returns>数字</returns>
    public static int ParseInt(string toParse, int defaultValue)
    {
      try
      {
        return int.Parse(toParse);
      }
      catch
      {
        return defaultValue;
      }
    }
    public static int ParseInt(string toParse)
    {
      return Functions.ParseInt(toParse, 0);
    }

    public static int ParseInt(object toParse, int defaultValue)
    {
        try
        {
            return int.Parse(ParseStr(toParse, "0"));
        }
        catch
        {
            return defaultValue;
        }
    }

    public static long ParseLong(object toParse, long defaultValue)
    {
        try
        {
            return long.Parse(ParseStr(toParse, "0"));
        }
        catch
        {
            return defaultValue;
        }
    }
    /// <summary>
    /// 将string转化为float，发生错误时，返回缺省值。
    /// </summary>
    /// <param name="toParse">要转化为数字的字符串</param>
    /// <param name="defaultValue">缺省数值</param>
    /// <returns>float</returns>
    public static float ParseFlt(string toParse, float defaultValue)
    {
      try
      {
        return float.Parse(toParse);
      }
      catch
      {
        return defaultValue;
      }
    }
    public static float ParseFlt(string toParse)
    {
      return Functions.ParseFlt(toParse, 0F);
    }

    /// <summary>
    /// 将string转化为double，发生错误时，返回缺省值。
    /// </summary>
    /// <param name="toParse">要转化为数字的字符串</param>
    /// <param name="defaultValue">缺省数值</param>
    /// <returns>double</returns>
    public static double ParseDbl(string toParse, double defaultValue)
    {
      try
      {
        return double.Parse(toParse);
      }
      catch
      {
        return defaultValue;
      }
    }
    public static double ParseDbl(string toParse)
    {
      return Functions.ParseDbl(toParse, 0D);
    }

    /// <summary>
    /// 将string转换为DateTime，发生错误时，返回缺省值。
    /// </summary>
    /// <param name="toParse"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static DateTime ParseDt(string toParse, DateTime defaultValue)
    {
      try
      {
        return DateTime.Parse(toParse);
      }
      catch
      {
        return defaultValue;
      }
    }
    /// <summary>
    /// 将string转换为DateTime，发生错误时，返回DateTime("1900-1-1")。
    /// </summary>
    /// <param name="toParse"></param>
    /// <returns></returns>
    public static DateTime ParseDt(string toParse)
    {
      return Functions.ParseDt(toParse, DateTime.Parse("1900-1-1"));
    }

    /// <summary>
    /// 将bool.TrueString和bool.FalseString转换为bool，或者执行int.Parse(toParse)>0?true:false，发生错误时，返回defaultValue。
    /// </summary>
    /// <param name="toParse"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static bool ParseBln(string toParse, bool defaultValue)
    {
      if(toParse.Trim().ToLower()==bool.TrueString.ToLower() || toParse.Trim().ToLower()==bool.FalseString.ToLower())
        return bool.Parse(toParse);
      else
      {
        try
        {
          return (int.Parse(toParse)>0?true:false);
        }
        catch
        {
          return defaultValue;
        }
      }
    }
    public static bool ParseBln(string toParse)
    {
      return Functions.ParseBln(toParse, true);
    }

    #endregion

    public static float calcRatio(object basenum, object compnum)
    {
        float b = ParseFlt(ParseStr(basenum, "0"));
        float c = ParseFlt(ParseStr(compnum, "0"));
        if (c == 0) return 0;
        return c / b -1;

    }

		/// <summary>
		/// 得到有效的路径名
		/// </summary>
		/// <param name="directoryName">要检查的路径名</param>
		/// <returns>有效的路径名</returns>
		public static string GetValidDirectoryName(string directoryName)
		{
			if (directoryName.EndsWith("/"))
				return directoryName;
			else
				return directoryName + "/";
		}

        /// <summary>
        /// 将文本框的空值转换成零，以与数据列的值类型兼容
        /// </summary>
        /// <param name="controlText">文本框内容</param>
        /// <param name="flag">看是要转换成int类型还是decimal类型，如果任意输入的flag（如1）值是整数则转换成int</param>
        /// <returns></returns>
		public static int ConvertTextValue(string controlText,int flag)
		{
			try
			{
				if(controlText=="")
				{
					return 0;
				}
				else
				{
						return Convert.ToInt32(controlText);
				}
			}
			catch
			{
				return 0;
			}
		}

		/// <summary>
		/// 将文本框的空值转换成零，以与数据列的值类型兼容
		/// </summary>
		/// <param name="controlText">文本框内容</param>
		/// <param name="flag">看是要转换成int类型还是decimal类型，如果任意输入的flag值（如1.1）是decimal则转换成decimal</param>
		/// <returns></returns>
		public static decimal ConvertTextValue(string controlText,double flag)
		{
			try
			{
				if(controlText=="")
				{
					return 0;
				}
				else
				{
						return Convert.ToDecimal(controlText);
				}
			}
			catch
			{
				return 0;
			}
		}
    
    
		#region 读取“appSettings”配置项。
    
		/// <summary>
		/// 获取Web.config文件中“appSettings”的string值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns>
		public static string GetAppConfigString(string keyName, string defaultValue)
		{
			NameValueCollection appSettings = ConfigurationSettings.AppSettings;
			if (appSettings != null)
			{
				string keyValue = appSettings[keyName];
				if (Functions.IsValidString(keyValue))
					return keyValue;
			}
		  
			return defaultValue;
		}

		/// <summary>
		/// 获取Web.config文件中“appSettings”的bool值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns>		
		public static bool GetAppConfigBool(string keyName, bool defaultValue)
		{
			NameValueCollection appSettings = ConfigurationSettings.AppSettings;
			if (appSettings != null)
			{
				try
				{
					bool keyValue = bool.Parse(appSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
		
		/// <summary>
		/// 获取Web.config文件中“appSettings”的int值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns>
		public static int GetAppConfigInt(string keyName, int defaultValue)
		{
			NameValueCollection appSettings = ConfigurationSettings.AppSettings;
			if (appSettings != null)
			{
				try
				{
					int keyValue = int.Parse(appSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
    
		#endregion 读取“appSettings”配置项。
    
		#region 读取自定义配置项。

		/// <summary>
		/// 获取Web.config文件中自定义配置的string值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">配置项名称</param>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns>    
		public static string GetConfigString(string configName, string keyName, string defaultValue)
		{
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configName) as NameValueCollection;
			if (configSettings != null)
			{
				string keyValue = configSettings[keyName];
				if (Functions.IsValidString(keyValue))
					return keyValue;
			}
      
			return defaultValue;
		}
    
		/// <summary>
		/// 获取Web.config文件中自定义配置的bool值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">配置项名称</param>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns> 
		public static bool GetConfigBool(string configName, string keyName, bool defaultValue)
		{
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configName) as NameValueCollection;
			if (configSettings != null)
			{
				try
				{
					bool keyValue = bool.Parse(configSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
		
		/// <summary>
		/// 获取Web.config文件中自定义配置的int值。
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">配置项名称</param>
		/// <param name="keyName">键名称</param>
		/// <param name="defaultValue">键的默认值</param>
		/// <returns>键值</returns> 
		public static int GetConfigInt(string configName, string keyName, int defaultValue)
		{
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configName) as NameValueCollection;
			if (configSettings != null)
			{
				try
				{
					int keyValue = int.Parse(configSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
    
		#endregion 读取自定义配置项。

		#region 二进制数字的操作（用于权限操作）
		/// <summary>
		/// 看A中是否包含B
		/// 所谓“包含”：
		/// A中是1的位，B中对应的可以是0或1
		/// A中是0的位，B中对应的必须是0
		/// 注：用于判断权限的包含关系
		/// </summary>
		/// <param name="A">A</param>
		/// <param name="B">B</param>
		/// <returns>包含，true；不包含，false</returns>
		public static bool IsAContainB(int A,int B)
		{
			if ( A< B ) return false;
			for (int i=1;i<=A;i<<=1)
			{
				if( ((B&i)!=0) && ((A&i)==0) ) return false;
			}
			return true;
		}
		/// <summary>
		/// 将两个整数按位“或”得到新的整数
		/// 注：用于将合并权限组合角色
		/// </summary>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <returns></returns>
		public static int GetTotalValue(int A, int B){
			if( A < 0 || B < 0) return 0;
			return A|B;
		}
		/// <summary>
		/// 从A中按位将B除去
		/// 所谓“除去”：
		/// 如果A包含B的话，就从A中将B中的1全部除去
		/// </summary>
		/// <param name="A">A</param>
		/// <param name="B">B</param>
		/// <returns></returns>
		public static int SeperateBFromA(int A,int B){
			if (!IsAContainB(A,B)) return A;
			return A-B;
		}
		#endregion 二进制数字的操作（用于权限操作）


		#region 时间日期

		#region 根据年和季度的编号返回对应季度的起止日期
		/// <summary>
		/// 根据年和季度的编号返回对应季度的起止日期
		/// </summary>
		/// <param name="year">年</param>
		/// <param name="id">季度</param>
		/// <param name="Date_s">起始时间</param>
		/// <param name="Date_e">结束时间</param>
		public static void GetJiduRangByID(int year,int id,out string Date_s,out string Date_e)
		{
			if (year==0)
			{
				Date_s="1900-1-1";
				Date_e="1900-1-1";
				return;
			}

			switch (id)
			{
				case 1:{
					Date_s=Convert.ToString(year)+"-1-1";
					Date_e=Convert.ToString(year)+"-4-1";
					break;
				}
				case 2:{
					Date_s=Convert.ToString(year)+"-4-1";
					Date_e=Convert.ToString(year)+"-7-1";
					break;
				}
				case 3:{
					Date_s=Convert.ToString(year)+"-7-1";
					Date_e=Convert.ToString(year)+"-10-1";
					break;
				}
				case 4:{
					Date_s=Convert.ToString(year)+"-10-1";
					Date_e=Convert.ToString(year+1)+"-1-1";
					break;					
				}
				default:{
					Date_s=Convert.ToString(year)+"-1-1";
					Date_e=Convert.ToString(year)+"-4-1";
					break;				
				}				
			}//switch			
			
			
		}
		
		/// <summary>
		/// 根据年和季度的编号返回对应季度的起止日期
		/// </summary>
		/// <param name="year">年</param>
		/// <param name="id">季度</param>
		/// <param name="Date_s">起始时间</param>
		/// <param name="Date_e">结束时间</param>
		public static void GetJiduRangByID(int year,int id,out DateTime Date_s,out DateTime Date_e)
		{
			string sDate_s,sDate_e;
			GetJiduRangByID(year,id,out sDate_s,out sDate_e);
            Date_s=Convert.ToDateTime(sDate_s);
			Date_e=Convert.ToDateTime(sDate_e);
		}
		#endregion 根据年和季度的编号返回对应季度的起止日期


		#endregion 时间日期

		#region 货币相关
		
		/// <summary>
		/// 中国货币转标准数字形式字符串
		/// </summary>
		/// <param name="s">中国货币值字符串</param>
		/// <returns></returns>
		public static string ConvertToStandIntTypedString(string s){
			if(s.Equals("")) return "0";
			string[] ss = s.TrimStart('￥').Split(',');
			System.Collections.IEnumerator ie = ss.GetEnumerator();
			string res = "";
			while (ie.MoveNext())
			{
				res +=ie.Current.ToString();
			}
			return res;			
		}
        public static string FormatMoneyString(string s)
        {
            Double d = Convert.ToDouble(s);
            return d.ToString("#.##");
        }
        public static string FormatMoneyString(object s)
        {
            if (s == null) return "0.00";
            Double d = Convert.ToDouble(s);
            return d.ToString("#.##");
        }
        internal static string FormatRmb(double rmb, string format)  
    {
      return rmb.ToString(format);
    }

    internal static string FormatRmb(float rmb, string format)
    {
      return rmb.ToString(format);
    }

		#endregion 货币相关


		#region 数据库空类型的转换
		public static int CleanDBInt(object drvalue)
		{
			return drvalue==DBNull.Value?0:(Int32)drvalue;
		}
		public static string CleanDBString(object drvalue)
		{
			return drvalue==DBNull.Value?"":(String)drvalue;
		}
		public static double CleanDBDouble(object drvalue)
		{
			return drvalue==DBNull.Value?0:(Double)drvalue;
		}

        public static decimal CleanDBDecimail(object drvalue)
        {
            return drvalue == DBNull.Value ? 0 : (Decimal)drvalue;
        }
		public static DateTime? CleanDBDateTime(object drvalue)
		{
            //return drvalue == DBNull.Value ? Convert.ToDateTime("1900-1-1") : (DateTime)drvalue;
            return drvalue == DBNull.Value ? null : (DateTime?)drvalue;
		}
		#endregion 

        #region html字符处理
        /// <summary>
        /// 将html文本转化为 文本内容方法TextNoHTML
        /// </summary>
        /// <param name="Htmlstring">HTML文本值</param>
        /// <returns></returns>
        public static string TextNoHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([/r/n])[/s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "/", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "/xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "/xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "/xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "/xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(/d+);", "", RegexOptions.IgnoreCase);
            //替换掉 < 和 > 标记
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Replace("\r", "");
            Htmlstring = Htmlstring.Replace("\n", "");
            //返回去掉html标记的字符串
            return Htmlstring;
        }

        public static string TextSimpleToHTML(string txt)
        {

            //替换掉 < 和 > 标记
            txt = txt.Replace("<", "&lt;");
            txt = txt.Replace(">", "&gt;");
            txt = txt.Replace("\r\n", "<br>");
            txt = txt.Replace("\r", "<br>");
            txt = txt.Replace("\n", "<br>");
            //返回去掉html标记的字符串
            return txt;
        }
        #endregion


        public static string Base64Encode(string s){
        byte[] bytes=Encoding.Default.GetBytes(s);
        return Convert.ToBase64String(bytes);
    }

    public static string Base64Decode(string s){
        
        byte[] outputb = Convert.FromBase64String(s);
        return Encoding.Default.GetString(outputb);
    }


    /// 
    /// 判断字符串中是否有SQL攻击代码
    /// 
    /// 传入用户提交数据
    /// true-安全；false-有注入攻击现有；
    /// ref: http://www.cnblogs.com/luluping/archive/2009/07/24/1530591.html
    public static bool ProcessSqlStr(string inputString)
    {
        string SqlStr = @"and|or|exec|execute|insert|select|delete|update|alter|create|drop|count|\*|chr|char|asc|mid|substring|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";
        try
        {
            if ((inputString != null) && (inputString != String.Empty))
            {
                string str_Regex = @"\b(" + SqlStr + @")\b";

                Regex Regex = new Regex(str_Regex, RegexOptions.IgnoreCase);
                //string s = Regex.Match(inputString).Value; 
                if (true == Regex.IsMatch(inputString))
                    return false;

            }
        }
        catch
        {
            return false;
        }
        return true;
    }

        public static string ConvertSQLTextValue(string v)
        {
            string res;
            res = v.Replace("'","''");
            return res;
        }


        /// <summary>
        /// dt2 - dt1
        /// </summary>
        /// <param name="dt1">from date</param>
        /// <param name="dt2">to date</param>
        /// <returns></returns>
        public static int GetMonthDiff(DateTime dt1, DateTime dt2)
        {
            return dt2.Year * 12 + dt2.Month - dt1.Year * 12 - dt1.Month;
        }

    public static int CompareTwoDate(DateTime d1, DateTime d2)
    {
      string s1 = d1.ToString("yyyy-MM-dd HH:mm:ss");
      string s2 = d2.ToString("yyyy-MM-dd HH:mm:ss");

      if(s1 == s2) return 0;

      s1 = s1.Replace("00:00:00", "23:59:59");

      return DateTime.Parse(s1).CompareTo(DateTime.Parse(s2));
    }

    public static string GetShortDateStringFromNullableDateTime(DateTime? dt)
    {

        if (dt == null) return "";
        return dt.Value.ToShortDateString();

    }
    public static string GetShortDateStringFromNullableDateTime_CN(DateTime? dt)
    {

        if (dt == null) return "";
        return dt.Value.ToString("yyyy年MM月dd日", DateTimeFormatInfo.InvariantInfo);

    }

    public static string GetFieldValue(DataSet ds, string fieldname, string defaultvalue )
    {
        if (ds == null)
        {
            return defaultvalue;
        }
        if (!ds.Tables[0].Columns.Contains(fieldname))
        {
            return defaultvalue;
        }
        if (ds.Tables[0].Rows.Count <= 0)
        {
            return defaultvalue;
        }
        if (ds.Tables[0].Rows[0][fieldname].GetType().FullName.Equals("System.DateTime"))
        {
            return ((DateTime)ds.Tables[0].Rows[0][fieldname]).ToShortDateString();
        }
        return ds.Tables[0].Rows[0][fieldname].ToString();
    }

    public static DateTime? GetBirthDayFromIDNumber(string CardID)
    {
        //string CardID = "身份证号码";
        string year = CardID.Substring(6, 4);
        string month = CardID.Substring(10, 2);
        string date = CardID.Substring(12, 2);
        string result = year + "-" + month + "-" + date;
        try
        {
            DateTime mydate = Convert.ToDateTime(result);
            return mydate;
        }
        catch
        {
            return null;
        }
         
    }


    public static string Execute(string command,string workdir, int seconds)
    {
        string output = ""; //输出字符串  
        if (command != null && !command.Equals(""))
        {
            Process process = new Process();//创建进程对象  
            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (! workdir.Trim().Equals(""))
                startInfo.WorkingDirectory = workdir;
            startInfo.FileName = "cmd.exe";//设定需要执行的命令  
            startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
            //startInfo.FileName = command;
            startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
            startInfo.RedirectStandardInput = false;//不重定向输入  
            startInfo.RedirectStandardOutput = true; //重定向输出  
            startInfo.CreateNoWindow = true;//不创建窗口  
            process.StartInfo = startInfo;
            try
            {
                if (process.Start())//开始进程  
                {
                    if (seconds == 0)
                    {
                        process.WaitForExit();//这里无限等待进程结束  
                    }
                    else
                    {
                        process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒  
                    }
                    output = process.StandardOutput.ReadToEnd();//读取进程的输出  
                }
            }
            catch
            {
            }
            finally
            {
                if (process != null)
                    process.Close();
            }
        }
        return output;
    } 

    
    }  
}

