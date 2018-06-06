using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.IO;

namespace com.kissmett.Common
{
    public class URLOperator
    {
        public static string getURLContent(String url)
        {

            //String url = "http://www.baidu.com";
            HttpWebRequest webrequest = null;
            HttpWebResponse webresponse = null;

            try
            {

                webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webrequest.Timeout = -1;
                webresponse = (HttpWebResponse)webrequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    return "[URLOperator.ERROR] URLOperator.getURLContent.WebException.Status Code:" + ((HttpWebResponse)e.Response).StatusCode;
                }

            }
            catch (Exception e)
            {
                return "[URLOperator.ERROR] URLOperator.getURLContent.Exception:" + e.ToString();
            }

            if (webresponse == null)
            {
                return "[URLOperator.ERROR] URLOperator.getURLContent.var.webresponse is null";
            }

            Stream stream = webresponse.GetResponseStream();
            byte[] rsByte = new Byte[webresponse.ContentLength];  //save data in the stream
            try
            {
                stream.Read(rsByte, 0, (int)webresponse.ContentLength);
                return System.Text.Encoding.UTF8.GetString(rsByte, 0, rsByte.Length).ToString();
            }
            catch (Exception exp)
            {
                return "[URLOperator.ERROR] " + exp.ToString();
            }
        }
    }

}
