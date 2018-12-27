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
	/// ���ú��������ض���Ӧ�ó����޹أ�����������á�
	/// </summary>
	/// <remarks>gwd, 2004-04-12</remarks>
	public sealed class Functions
	{

        //��������
        public static string InputPassword()
        {
            string password = "";
            while (true)
            {
                //�洢�û�����İ����������������λ�ò���ʾ�ַ�
                ConsoleKeyInfo ck = Console.ReadKey(true);

                //�ж��û��Ƿ��µ�Enter��
                if (ck.Key != ConsoleKey.Enter)
                {
                    if (ck.Key != ConsoleKey.Backspace)
                    {
                        //���û�������ַ������ַ�����
                        password += ck.KeyChar.ToString();
                        //���û�������ַ��滻Ϊ*
                        Console.Write("*");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(password) && password.Length >= 1)
                        {
                            password = password.Remove(password.Length - 1, 1);
                        }
                        //ɾ��������ַ�
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
        /// ��MD5�����ַ�������ѡ������16λ����32λ�ļ����ַ���
        /// </summary>
        /// <param name="password">�����ܵ��ַ���</param>
        /// <param name="bit">λ����һ��ȡֵ16 �� 32</param>
        /// <returns>���صļ��ܺ���ַ���</returns>
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
                if (bit == 32) return tmp.ToString();//Ĭ�����
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

    #region ������Ч�Լ�������ת��

    /// <summary>
    /// ���һ���ַ��������Ƿ�Ϊnull����string.Empty.
    /// </summary>
    /// <remarks>gwd, 2004-04-12</remarks>
    /// <param name="str">Ҫ�����ַ���</param>
    /// <returns>
    /// true -- null or string.Empty
    /// false -- str.length > 0
    /// </returns>
    public static bool IsValidString(string str)
    {
      return !(str == null || str == string.Empty);
    }

    /// <summary>
    /// ����ַ����Ƿ�Ϊ�գ�������Чֵ��
    /// </summary>
    /// <param name="toCheck">������ַ���</param>
    /// <returns>�����ַ���</returns>
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
    /// ��stringת��Ϊint����������ʱ������ȱʡֵ��
    /// </summary>
    /// <param name="toParse">Ҫת��Ϊ���ֵ��ַ���</param>
    /// <param name="defaultValue">ȱʡ��ֵ</param>
    /// <returns>����</returns>
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
    /// ��stringת��Ϊfloat����������ʱ������ȱʡֵ��
    /// </summary>
    /// <param name="toParse">Ҫת��Ϊ���ֵ��ַ���</param>
    /// <param name="defaultValue">ȱʡ��ֵ</param>
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
    /// ��stringת��Ϊdouble����������ʱ������ȱʡֵ��
    /// </summary>
    /// <param name="toParse">Ҫת��Ϊ���ֵ��ַ���</param>
    /// <param name="defaultValue">ȱʡ��ֵ</param>
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
    /// ��stringת��ΪDateTime����������ʱ������ȱʡֵ��
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
    /// ��stringת��ΪDateTime����������ʱ������DateTime("1900-1-1")��
    /// </summary>
    /// <param name="toParse"></param>
    /// <returns></returns>
    public static DateTime ParseDt(string toParse)
    {
      return Functions.ParseDt(toParse, DateTime.Parse("1900-1-1"));
    }

    /// <summary>
    /// ��bool.TrueString��bool.FalseStringת��Ϊbool������ִ��int.Parse(toParse)>0?true:false����������ʱ������defaultValue��
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
		/// �õ���Ч��·����
		/// </summary>
		/// <param name="directoryName">Ҫ����·����</param>
		/// <returns>��Ч��·����</returns>
		public static string GetValidDirectoryName(string directoryName)
		{
			if (directoryName.EndsWith("/"))
				return directoryName;
			else
				return directoryName + "/";
		}

        /// <summary>
        /// ���ı���Ŀ�ֵת�����㣬���������е�ֵ���ͼ���
        /// </summary>
        /// <param name="controlText">�ı�������</param>
        /// <param name="flag">����Ҫת����int���ͻ���decimal���ͣ�������������flag����1��ֵ��������ת����int</param>
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
		/// ���ı���Ŀ�ֵת�����㣬���������е�ֵ���ͼ���
		/// </summary>
		/// <param name="controlText">�ı�������</param>
		/// <param name="flag">����Ҫת����int���ͻ���decimal���ͣ�������������flagֵ����1.1����decimal��ת����decimal</param>
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
    
    
		#region ��ȡ��appSettings�������
    
		/// <summary>
		/// ��ȡWeb.config�ļ��С�appSettings����stringֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns>
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
		/// ��ȡWeb.config�ļ��С�appSettings����boolֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns>		
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
		/// ��ȡWeb.config�ļ��С�appSettings����intֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns>
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
    
		#endregion ��ȡ��appSettings�������
    
		#region ��ȡ�Զ��������

		/// <summary>
		/// ��ȡWeb.config�ļ����Զ������õ�stringֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">����������</param>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns>    
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
		/// ��ȡWeb.config�ļ����Զ������õ�boolֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">����������</param>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns> 
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
		/// ��ȡWeb.config�ļ����Զ������õ�intֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">����������</param>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns> 
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
    
		#endregion ��ȡ�Զ��������

		#region ���������ֵĲ���������Ȩ�޲�����
		/// <summary>
		/// ��A���Ƿ����B
		/// ��ν����������
		/// A����1��λ��B�ж�Ӧ�Ŀ�����0��1
		/// A����0��λ��B�ж�Ӧ�ı�����0
		/// ע�������ж�Ȩ�޵İ�����ϵ
		/// </summary>
		/// <param name="A">A</param>
		/// <param name="B">B</param>
		/// <returns>������true����������false</returns>
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
		/// ������������λ���򡱵õ��µ�����
		/// ע�����ڽ��ϲ�Ȩ����Ͻ�ɫ
		/// </summary>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <returns></returns>
		public static int GetTotalValue(int A, int B){
			if( A < 0 || B < 0) return 0;
			return A|B;
		}
		/// <summary>
		/// ��A�а�λ��B��ȥ
		/// ��ν����ȥ����
		/// ���A����B�Ļ����ʹ�A�н�B�е�1ȫ����ȥ
		/// </summary>
		/// <param name="A">A</param>
		/// <param name="B">B</param>
		/// <returns></returns>
		public static int SeperateBFromA(int A,int B){
			if (!IsAContainB(A,B)) return A;
			return A-B;
		}
		#endregion ���������ֵĲ���������Ȩ�޲�����


		#region ʱ������

		#region ������ͼ��ȵı�ŷ��ض�Ӧ���ȵ���ֹ����
		/// <summary>
		/// ������ͼ��ȵı�ŷ��ض�Ӧ���ȵ���ֹ����
		/// </summary>
		/// <param name="year">��</param>
		/// <param name="id">����</param>
		/// <param name="Date_s">��ʼʱ��</param>
		/// <param name="Date_e">����ʱ��</param>
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
		/// ������ͼ��ȵı�ŷ��ض�Ӧ���ȵ���ֹ����
		/// </summary>
		/// <param name="year">��</param>
		/// <param name="id">����</param>
		/// <param name="Date_s">��ʼʱ��</param>
		/// <param name="Date_e">����ʱ��</param>
		public static void GetJiduRangByID(int year,int id,out DateTime Date_s,out DateTime Date_e)
		{
			string sDate_s,sDate_e;
			GetJiduRangByID(year,id,out sDate_s,out sDate_e);
            Date_s=Convert.ToDateTime(sDate_s);
			Date_e=Convert.ToDateTime(sDate_e);
		}
		#endregion ������ͼ��ȵı�ŷ��ض�Ӧ���ȵ���ֹ����


		#endregion ʱ������

		#region �������
		
		/// <summary>
		/// �й�����ת��׼������ʽ�ַ���
		/// </summary>
		/// <param name="s">�й�����ֵ�ַ���</param>
		/// <returns></returns>
		public static string ConvertToStandIntTypedString(string s){
			if(s.Equals("")) return "0";
			string[] ss = s.TrimStart('��').Split(',');
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

		#endregion �������


		#region ���ݿ�����͵�ת��
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

        #region html�ַ�����
        /// <summary>
        /// ��html�ı�ת��Ϊ �ı����ݷ���TextNoHTML
        /// </summary>
        /// <param name="Htmlstring">HTML�ı�ֵ</param>
        /// <returns></returns>
        public static string TextNoHTML(string Htmlstring)
        {
            //ɾ���ű�   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //ɾ��HTML   
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
            //�滻�� < �� > ���
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Replace("\r", "");
            Htmlstring = Htmlstring.Replace("\n", "");
            //����ȥ��html��ǵ��ַ���
            return Htmlstring;
        }

        public static string TextSimpleToHTML(string txt)
        {

            //�滻�� < �� > ���
            txt = txt.Replace("<", "&lt;");
            txt = txt.Replace(">", "&gt;");
            txt = txt.Replace("\r\n", "<br>");
            txt = txt.Replace("\r", "<br>");
            txt = txt.Replace("\n", "<br>");
            //����ȥ��html��ǵ��ַ���
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
    /// �ж��ַ������Ƿ���SQL��������
    /// 
    /// �����û��ύ����
    /// true-��ȫ��false-��ע�빥�����У�
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
        return dt.Value.ToString("yyyy��MM��dd��", DateTimeFormatInfo.InvariantInfo);

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
        //string CardID = "���֤����";
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
        string output = ""; //����ַ���  
        if (command != null && !command.Equals(""))
        {
            Process process = new Process();//�������̶���  
            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (! workdir.Trim().Equals(""))
                startInfo.WorkingDirectory = workdir;
            startInfo.FileName = "cmd.exe";//�趨��Ҫִ�е�����  
            startInfo.Arguments = "/C " + command;//��/C����ʾִ��������������˳�  
            //startInfo.FileName = command;
            startInfo.UseShellExecute = false;//��ʹ��ϵͳ��ǳ�������  
            startInfo.RedirectStandardInput = false;//���ض�������  
            startInfo.RedirectStandardOutput = true; //�ض������  
            startInfo.CreateNoWindow = true;//����������  
            process.StartInfo = startInfo;
            try
            {
                if (process.Start())//��ʼ����  
                {
                    if (seconds == 0)
                    {
                        process.WaitForExit();//�������޵ȴ����̽���  
                    }
                    else
                    {
                        process.WaitForExit(seconds); //�ȴ����̽������ȴ�ʱ��Ϊָ���ĺ���  
                    }
                    output = process.StandardOutput.ReadToEnd();//��ȡ���̵����  
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

