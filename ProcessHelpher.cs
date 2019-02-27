using System.Diagnostics;
using System;

//ref: https://blog.csdn.net/pan_junbiao/article/details/78504994

/*
 * 
public static void Unzip()
{
    string zipExe = @"C:\Program Files\7-Zip\7z.exe";   //7-Zip工具的运行程序
    string zipFileName = @"E:\MyZip\Report.zip";        //需要被解压的Zip文件
    string unZipPath = @"E:\MyZip\";                    //解压后文件存放目录
    string pwd = "123";                                 //解压密码
 
    //执行解压命令
    string cmd = String.Format("\"{0}\" e \"{1}\" -p{2} -o\"{3}\"", zipExe, zipFileName, pwd, unZipPath);
    ProcessHelper.ExecCommand(cmd);
)
 */
namespace com.kissmett.Common
{
    public class ProcessHelper
    {
        public static string[] ExecCommand(string commands,string workdir="")
        {
            //msg[0]执行结果;msg[1]错误结果
            string[] msg = new string[2];
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                if(!workdir.Equals(""))
                    proc.StartInfo.WorkingDirectory = workdir;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                /*
                proc.StartInfo.UserName = "thinkpad";
                string password = "kissmett";
                System.Security.SecureString spassword = new System.Security.SecureString();
                char[] passArr = password.ToCharArray();
                for (int i = 0; i < passArr.Length; i++)
                {
                    spassword.AppendChar(passArr[i]);
                }
                proc.StartInfo.Password = spassword;
                proc.StartInfo.Domain = @"\";
                */
                proc.Start();

                proc.StandardInput.WriteLine(commands);
                proc.StandardInput.WriteLine("exit");

                //执行结果
                msg[0] = proc.StandardOutput.ReadToEnd();
                proc.StandardOutput.Close();

                //出错结果
                msg[1] = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();

                //超时等待
                int maxWaitCount = 10;
                while (proc.HasExited == false && maxWaitCount > 0)
                {
                    proc.WaitForExit(1000);
                    maxWaitCount--;
                }
                if (maxWaitCount == 0)
                {
                    msg[1] = "操作执行超时";
                    proc.Kill();
                }
                return msg;
            }
            catch (Exception ex)
            {
                msg[1] = "进程创建失败:";
                msg[1] += ex.Message.ToString();
                msg[1] += ex.StackTrace.ToString();
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return msg;
        }
    }
}