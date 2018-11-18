using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.IO;

namespace com.kissmett.Common
{
    public class MyFile
    {

        /*
         * ext: ".pdf"
         */
        public static  void DeleteFileByExt(string path,string ext)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            System.Collections.Stack s = new Stack();
            s.Push(di);
            DirectoryInfo[] dii = di.GetDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo dix in dii)
            {
                s.Push(dix);
            }
            while (0 != s.Count)
            {
                DirectoryInfo dixx = (DirectoryInfo)s.Pop();
                FileInfo[] fi = dixx.GetFiles();
                foreach (FileInfo xx in fi)
                {
                    if (xx.Extension == ext )
                    {
                        File.Delete(path+@"\" + xx);
                    }
                }
            }
        }

        public static void DeleteFile(string srcFilePath)
        {
            if (!File.Exists(srcFilePath)) return;
            File.Delete(srcFilePath);

        }
        /*
         调用DelectDir方法前可以先判断文件夹是否存在

        if(Directory.Exists(srcPath))
        {
            DelectDir(srcPath);
        }
         * 
         * */
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
                Directory.Delete(srcPath);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 移动文件夹中的所有文件夹与文件到另一个文件夹  //转载请注明来自 http://www.shang11.com
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        public static void MoveFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    //string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    string destFile = Path.Combine( destPath, Path.GetFileName(c) );
                    //覆盖模式
                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }
                    File.Move(c, destFile);
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));

                folders.ForEach(c =>
                {
                    //string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    string destDir = Path.Combine( destPath, Path.GetFileName(c) );
                    //Directory.Move必须要在同一个根目录下移动才有效，不能在不同卷中移动。
                    //Directory.Move(c, destDir);

                    //采用递归的方法实现
                    MoveFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }




        public static string fileToString(String filePath)
        {
            string strData = "";
            try
            {
                string line;
                // 创建一个 StreamReader 的实例来读取文件 ,using 语句也能关闭 StreamReader
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                {
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        strData += line+"\r\n";
                    }
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return strData;
        }


        public static void stringToFile(string data,string filepath)
        {
            
            //文件覆盖方式添加内容
            System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, false);
            //保存数据到文件
            file.Write(data);
            //关闭文件
            file.Close();
            //释放对象
            file.Dispose();
           
        }


        //读取txt文件中总行数的方法
        public static int getTxtFileLineCount(string fileName)
        {

            var path = fileName;
            int lines = 0;

            //按行读取
          
            using (var sr = new StreamReader(path))
            {
                var ls = "";
                while ((ls = sr.ReadLine()) != null)
                {
                    lines++;
                }
            }
       
            return lines;
        }

    }
}
