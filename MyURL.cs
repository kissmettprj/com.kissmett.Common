﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Collections.Specialized;

namespace com.kissmett.Common
{
    public class MyURL
    {

        //ref: https://www.cnblogs.com/gaizai/archive/2010/05/27/1743485.html
/// 测试.
/// </summary>
public void Test()
{
    string pageURL = "http://www.google.com.hk/search?hl=zh-CN&source=hp&q=%E5%8D%9A%E6%B1%87%E6%95%B0%E7%A0%81&aq=f&aqi=g2&aql=&oq=&gs_rfai=";
    Uri uri = new Uri(pageURL);
    string queryString = uri.Query;
    NameValueCollection col = GetQueryStringNVC(queryString);
    string searchKey = col["q"];
    //结果 searchKey = "博汇数码"
}    

/// <summary>
/// 将查询字符串解析转换为名值集合.
/// </summary>
/// <param name="queryString"></param>
/// <returns></returns>
public static NameValueCollection GetQueryStringNVC(string queryString)
{
    return GetQueryStringNVC(queryString, null, true);
}

/// <summary>
/// 将查询字符串解析转换为名值集合.
/// </summary>
/// <param name="queryString"></param>
/// <param name="encoding"></param>
/// <param name="isEncoded"></param>
/// <returns></returns>
public static NameValueCollection GetQueryStringNVC(string queryString, Encoding encoding, bool isEncoded)
{
    queryString = queryString.Replace("?", "");
    NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
    if (!string.IsNullOrEmpty(queryString))
    {
        int count = queryString.Length;
        for (int i = 0; i < count; i++)
        {
            int startIndex = i;
            int index = -1;
            while (i < count)
            {
                char item = queryString[i];
                if (item == '=')
                {
                    if (index < 0)
                    {
                        index = i;
                    }
                }
                else if (item == '&')
                {
                    break;
                }
                i++;
            }
            string key = null;
            string value = null;
            if (index >= 0)
            {
                key = queryString.Substring(startIndex, index - startIndex);
                value = queryString.Substring(index + 1, (i - index) - 1);
            }
            else
            {
                key = queryString.Substring(startIndex, i - startIndex);
            }
            if (isEncoded)
            {
                result[MyUrlDeCode(key, encoding)] = MyUrlDeCode(value, encoding);                    
            }
            else
            {
                result[key] = value;
            }
            if ((i == (count - 1)) && (queryString[i] == '&'))
            {
                result[key] = string.Empty;
            }
        }
    }
    return result;
}

/// <summary>
/// 解码URL.
/// </summary>
/// <param name="encoding">null为自动选择编码</param>
/// <param name="str"></param>
/// <returns></returns>
public static string MyUrlDeCode(string str, Encoding encoding)
{
    if (encoding == null)
    {
        Encoding utf8 = Encoding.UTF8;
        //首先用utf-8进行解码                     
        string code = HttpUtility.UrlDecode(str.ToUpper(), utf8);
        //将已经解码的字符再次进行编码.
        string encode = HttpUtility.UrlEncode(code, utf8).ToUpper();
        if (str == encode)
            encoding = Encoding.UTF8;
        else
            encoding = Encoding.GetEncoding("gb2312");
    }
    return HttpUtility.UrlDecode(str, encoding);
}
    }
}
