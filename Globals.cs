using System;



namespace com.kissmett.Common
{
	/// <summary>
	/// 本应用程序所需的公共函数和属性。
	/// </summary>
	/// <remarks>gwd, 2004-04-12</remarks>
	public sealed class Globals
	{
		/// <summary>
		/// Since this class provides only static methods, make the default constructor private to prevent 
		/// instances from being created with "new Globals()"
		/// </summary>
		private Globals() {}
		
		#region private functions
		
		#endregion private functions
		
		#region properties
		
		/// <summary>
		/// 数据库连接字符串。
		/// </summary>
		public static string ConnectionString
		{
			get { return Functions.GetAppConfigString("ConnectionString", string.Empty); }
		}
		/// <summary>
		/// 应用程序名
		/// </summary>
		public static string AppTitle
		{
			get { return Functions.GetAppConfigString("AppTitle", string.Empty); }
		}



		/// <summary>
		/// 权限模块列表
		/// </summary>
		public static string[] Rights{
			get{
				string s = Functions.GetValidDirectoryName(Functions.GetAppConfigString("Rights",""));
				s = s.TrimEnd(new char[]{'/'});
				string[] ss = s.Split(",".ToCharArray());
				return ss;
			}
		}
		/// <summary>
		/// 得到设备配置文件上传路径
		/// </summary>
		public static string EqiConfigFileRoot
		{
			get{return (Functions.GetValidDirectoryName(Functions.GetAppConfigString("EqiConfigFileRoot", "/"))).TrimEnd(new char[]{'/'}); }
		}

		/// <summary>
		/// 得到物资合同文件上传路径
		/// </summary>
		public static string MatContractFileRoot
		{
			get{return (Functions.GetValidDirectoryName(Functions.GetAppConfigString("MatContractFileRoot", "/"))).TrimEnd(new char[]{'/'}); }
		}

		/// <summary>
		/// 得到物资消耗文件上传路径
		/// </summary>
		public static string MatConsumptionFileRoot
		{
			get{return (Functions.GetValidDirectoryName(Functions.GetAppConfigString("MatConsumptionRoot", "/"))).TrimEnd(new char[]{'/'}); }
		}

		/// <summary>
		/// 得到虚拟路径名称
		/// </summary>
		public static string AppVirtualDictionaryName
		{
			get{return Functions.GetValidDirectoryName(Functions.GetAppConfigString("AppVirtualDictionaryName", "/web")); }
		}
		/// <summary>
		/// 应用程序根目录
		/// </summary>
		public static string AppRoot
		{
			get 
			{
				/*
				string path = Functions.ApplicationDirectory;
				if (path == "/")
				  return Functions.GetValidDirectoryName(Functions.GetAppConfigString("AppRoot", "/"));
				else
				  return path;
				*/
        
				// 完全由配置文件决定系统的根目录。
				return Functions.GetValidDirectoryName(Functions.GetAppConfigString("AppRoot", "/"));
			}
		}
		/// <summary>
		/// 皮肤样式（未采用）
		/// </summary>
		public static string Skin
		{
			get { return Functions.GetAppConfigString("Skin", "default"); }
		}
 
		#endregion properties

    public static string FormatRmb(double rmb)
    {
      return Functions.FormatRmb(rmb, "F");
    }

    public static string FormatRmb(float rmb)
    {
      return Functions.FormatRmb(rmb, "F");
    }

    public static string FormatDate(DateTime date)
    {
      return date.ToString("yyyy-MM-dd");
    }

    public static string FormatDate(string date)
    {
      return Convert.ToDateTime(date).ToString("yyyy-MM-dd");
    }

    public static string FormatDateTime(DateTime date)
    {
      return date.ToString("yyyy-MM-dd HH:mm");
    }

    public static string FormatDateTime(string date)
    {
      return Convert.ToDateTime(date).ToString("yyyy-MM-dd HH:mm");
    }


    //public static bool CheckTwoDate(System.Web.UI.WebControls.TextBox d1, System.Web.UI.WebControls.TextBox d2, string title1, string title2, PageBase page)
    //{
    //  DateTime date1, date2;

    //  date1 = DateTime.Parse("1900-1-1");
    //  if(d1.Text.Trim().Length > 0)
    //  {
    //    date1 = Functions.ParseDt(d1.Text);
    //    if(date1.Year <= 1990)
    //    {
    //      page.GetAlertMsg(string.Format("请正确填写{0}，建议的格式:{1}！", title1, Globals.FormatDate(DateTime.Now)));
    //      page.SetFocus("", d1.ClientID);
    //      return false;
    //    }
    //  }
      
    //  if(d2.Text.Trim().Length > 0)
    //  {
    //    date2 = Functions.ParseDt(d2.Text);
    //    if(date2.Year <= 1990)
    //    {
    //      page.GetAlertMsg(string.Format("请正确填写{0}，建议的格式: {1}！", title2, Globals.FormatDate(DateTime.Now)));
    //      page.SetFocus("", d2.ClientID);
    //      return false;
    //    }
    //    else if(date2.CompareTo(date1) < 0)
    //    {
    //      page.GetAlertMsg(string.Format("{0}应该在{1}之后！", title2, title1));
    //      page.SetFocus("", d2.ClientID);
    //      return false;
    //    }
    //  }

    //  return true;
    //}
		
	}
}
