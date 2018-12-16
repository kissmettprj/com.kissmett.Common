using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;


namespace com.kissmett.Common
{
    /// <summary>
    ///     HttpService 的摘要说明
    /// </summary>
    public class HttpService
    {
        public static string Get(string url)
        {
            using (var c = new WebClient())
            {
                try
                {
                    c.Encoding = Encoding.UTF8;
                    return c.DownloadString(url);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public static string GetPost(string url, string parameters)
        {
            var ret = "";
            using (var c = new WebClient())
            {
                try
                {
                    c.Encoding = Encoding.UTF8;
                    var data = Encoding.UTF8.GetBytes(parameters);
                    c.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    c.Headers.Add("ContentLength", parameters.Length.ToString());
                    ret = Encoding.UTF8.GetString(c.UploadData(url, "POST", data));
                    return ret;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public static string GetPost(string url, NameValueCollection nv)
        {
            using (var c = new WebClient())
            {
                try
                {
                    c.Encoding = Encoding.UTF8;
                    return Encoding.UTF8.GetString(c.UploadValues(url, "POST", nv));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}