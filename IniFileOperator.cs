using System;
using System.Collections.Generic;
using System.Text;


using System.Runtime.InteropServices;
using System.IO;


namespace com.kissmett.Common
{
    public class IniFileOperator
    {
        [DllImport("kernel32.dll")]
        //参数说明:(ini文件中的段落,INI中的关键字,INI中关键字的数值,INI文件的完整路径)
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32.dll")]
        //参数说明:(INI文件中的段落,INI中的关键字,INI无法读取时的缺省值,读取数值,数值大小,INI文件的完整路径)
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        public static long Write(string section, string key, string val, string filePath)
        {
            //写INI文件,如果事存在这个关键字的话,程序会自动的添加一个相对应的值.
            long res = 0;
            if (File.GetAttributes(filePath) == FileAttributes.ReadOnly)
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                res = WritePrivateProfileString(section, key, val, filePath);
                File.SetAttributes(filePath, FileAttributes.ReadOnly);
            }
            else
            {
                //修改数据
                res = WritePrivateProfileString(section, key, val, filePath);
            }
            return res;
        }

        public static string Read(string section, string key, string def, StringBuilder sb, int size, string filePath)
        {
            //读INI文件
            int i = GetPrivateProfileString(section, key, def, sb, size,filePath);
            return sb.ToString();

        }


        //-------------------------------- test ------------------------------
        public static void TestGet()
        {
            //读INI文件
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString("Recording", "Position", "无法显示", sb, 255, @"e:\kissmett.ini");
            //MessageBox.Show(i.ToString());
        }

        public static void TestWrite()
        {
            try
            {
                //写INI文件,如果事存在这个关键字的话,程序会自动的添加一个相对应的值.
                if (File.GetAttributes(@"e:\kissmett.ini") == FileAttributes.ReadOnly)
                {
                    File.SetAttributes(@"e:\kissmett.ini", FileAttributes.Normal);
                    string strval = "1017";
                    WritePrivateProfileString("Recording", "Channels", strval, @"e:\kissmett.ini");
                    //MessageBox.Show("完成操作!!!");
                    File.SetAttributes(@"e:\kissmett.ini", FileAttributes.ReadOnly);
                }
                else
                {
                    string strval = "1017";
                    //修改数据
                    //WritePrivateProfileString("Recording", "Channels", strval, @"e:\zjw.ini");
                    //添加一条记录
                    //WritePrivateProfileString("Recording", "Channels1", strval, @"e:\zjw.ini");
                    //删除一条记录
                    //WritePrivateProfileString("Recording", "Channels1",null, @"e:\zjw.ini");
                    //删除一个section下所有的记录
                    WritePrivateProfileString("Recording", null, null, @"e:\zjw.ini");
                    //MessageBox.Show("完成操作!!!");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
           
        }

    }
}
