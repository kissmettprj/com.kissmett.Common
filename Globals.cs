using System;



namespace com.kissmett.Common
{
	/// <summary>
	/// ��Ӧ�ó�������Ĺ������������ԡ�
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
		/// ���ݿ������ַ�����
		/// </summary>
		public static string ConnectionString
		{
			get { return Functions.GetAppConfigString("ConnectionString", string.Empty); }
		}
		/// <summary>
		/// Ӧ�ó�����
		/// </summary>
		public static string AppTitle
		{
			get { return Functions.GetAppConfigString("AppTitle", string.Empty); }
		}



		/// <summary>
		/// Ȩ��ģ���б�
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
		/// �õ��豸�����ļ��ϴ�·��
		/// </summary>
		public static string EqiConfigFileRoot
		{
			get{return (Functions.GetValidDirectoryName(Functions.GetAppConfigString("EqiConfigFileRoot", "/"))).TrimEnd(new char[]{'/'}); }
		}

		/// <summary>
		/// �õ����ʺ�ͬ�ļ��ϴ�·��
		/// </summary>
		public static string MatContractFileRoot
		{
			get{return (Functions.GetValidDirectoryName(Functions.GetAppConfigString("MatContractFileRoot", "/"))).TrimEnd(new char[]{'/'}); }
		}

		/// <summary>
		/// �õ����������ļ��ϴ�·��
		/// </summary>
		public static string MatConsumptionFileRoot
		{
			get{return (Functions.GetValidDirectoryName(Functions.GetAppConfigString("MatConsumptionRoot", "/"))).TrimEnd(new char[]{'/'}); }
		}

		/// <summary>
		/// �õ�����·������
		/// </summary>
		public static string AppVirtualDictionaryName
		{
			get{return Functions.GetValidDirectoryName(Functions.GetAppConfigString("AppVirtualDictionaryName", "/web")); }
		}
		/// <summary>
		/// Ӧ�ó����Ŀ¼
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
        
				// ��ȫ�������ļ�����ϵͳ�ĸ�Ŀ¼��
				return Functions.GetValidDirectoryName(Functions.GetAppConfigString("AppRoot", "/"));
			}
		}
		/// <summary>
		/// Ƥ����ʽ��δ���ã�
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
    //      page.GetAlertMsg(string.Format("����ȷ��д{0}������ĸ�ʽ:{1}��", title1, Globals.FormatDate(DateTime.Now)));
    //      page.SetFocus("", d1.ClientID);
    //      return false;
    //    }
    //  }
      
    //  if(d2.Text.Trim().Length > 0)
    //  {
    //    date2 = Functions.ParseDt(d2.Text);
    //    if(date2.Year <= 1990)
    //    {
    //      page.GetAlertMsg(string.Format("����ȷ��д{0}������ĸ�ʽ: {1}��", title2, Globals.FormatDate(DateTime.Now)));
    //      page.SetFocus("", d2.ClientID);
    //      return false;
    //    }
    //    else if(date2.CompareTo(date1) < 0)
    //    {
    //      page.GetAlertMsg(string.Format("{0}Ӧ����{1}֮��", title2, title1));
    //      page.SetFocus("", d2.ClientID);
    //      return false;
    //    }
    //  }

    //  return true;
    //}
		
	}
}
