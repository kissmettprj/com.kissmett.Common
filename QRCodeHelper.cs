using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Drawing;

using ThoughtWorks.QRCode.Codec;

// http://blog.csdn.net/lybwwp/article/details/18444369

namespace com.kissmett.Common
{
    public class QRCodeHelper
    {

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="codeNumber">要生成二维码的字符串</param>     
        /// <param name="size">大小尺寸</param>
        /// <returns>二维码图片</returns>
        public static Bitmap Create_ImgCode(string codeNumber, int size)
        {
            //创建二维码生成类
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeNumber);
            return image;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="strPath">保存路径.png</param>
        /// <param name="img">图片</param>
        public static void SaveImg(string filename, Bitmap img)
        {
            //保存图片到目录
            string strPath = Path.GetDirectoryName(filename);
            if (Directory.Exists(strPath))
            {
                //文件名称
                //string guid = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                img.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                //当前目录不存在，则创建
                Directory.CreateDirectory(strPath);
            }
        }
    }
}
