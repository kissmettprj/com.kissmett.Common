using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Archive.SevenZip;
using SharpCompress.Common;

namespace com.kissmett.Common
{
    public class MyZip
    {
        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="targetFile">压缩文件夹路径</param>
        /// <param name="zipFile">压缩后路径</param>
        public static void Zip(string targetFile, string zipFile)
        {
            using (var archive = ZipArchive.Create())
            {
                ZipRecursion(targetFile, archive);
                FileStream fs_scratchPath = new FileStream(zipFile, FileMode.OpenOrCreate, FileAccess.Write);
                archive.SaveTo(fs_scratchPath, CompressionType.Deflate);
                fs_scratchPath.Close();
            }
        }
        /// <summary>
        /// 压缩递归
        /// </summary>
        /// <param name="fullName">压缩文件夹目录</param>
        /// <param name="archive">压缩实体</param>
        public static void ZipRecursion(string fullName, ZipArchive archive)
        {
            DirectoryInfo di = new DirectoryInfo(fullName);//获取需要压缩的文件夹信息
            foreach (var fi in di.GetDirectories())
            {
                if (Directory.Exists(fi.FullName))
                {
                    ZipRecursion(fi.FullName, archive);
                }
            }
            foreach (var f in di.GetFiles())
            {
                archive.AddEntry(f.FullName, f.OpenRead());//添加文件夹中的文件
            }
        }


        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="zipFile">解压文件路径</param>
        /// <param name="targetPath">解压文件后路径</param>
        /// 对.7z文件解压有问题;
        public static void UnZip(string zipFile, string targetPath)
        {
            var archive = ArchiveFactory.Open(zipFile);
            foreach (var entry in archive.Entries)
            {
                if (!entry.IsDirectory)
                {
                    entry.WriteToDirectory(targetPath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                }
            }
        }

        /// <summary>
        /// 解压Zip文件
        /// </summary>
        /// 需要在config中配置 7zexe
        public static void UnZip2(string zipFile, string targetPath)
        {
            string zipExe = Functions.GetAppConfigString("7zexe", string.Empty);// @"D:\Program Files\7-Zip\7z.exe";   //7-Zip工具的运行程序
            //string zipFileName = @"E:\MyZip\Report.zip";        //需要被解压的Zip文件
            //string unZipPath = @"E:\MyZip\";                    //解压后文件存放目录
            string pwd = "";                                 //解压密码

            //执行解压命令
            string cmd = String.Format("\"{0}\" x \"{1}\" -p{2} -o\"{3}\"", zipExe, zipFile, pwd, targetPath);
            ProcessHelper.ExecCommand(cmd);
        }

    }
}
