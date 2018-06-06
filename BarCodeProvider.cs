using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
// http://q.cnblogs.com/q/15253/
namespace com.kissmett.Common
{
    public class BarCodeProvider
    {
        #region 单例
        private static BarCodeProvider _Intance;
        public static BarCodeProvider Intance
        {
            get
            {
                if (_Intance == null)
                {
                    _Intance = new BarCodeProvider();
                }
                return _Intance;
            }
        }
        #endregion

        #region Size
        /// <summary>
        /// 图片宽度
        /// </summary>
        private int _Width = 200;
        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }


        /// <summary>
        /// 图片高度
        /// </summary>
        private int _Height = 80;
        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        /// <summary>
        /// 明文高度
        /// </summary>
        private int _TextHeight = 25;
        public int TextHeight
        {
            get { return _TextHeight; }
            set { _TextHeight = value; }
        }

        #endregion

        #region 边距
        private int _Margin_Top = 5;

        /// <summary>
        /// 上边距
        /// </summary>
        public int Margin_Top
        {
            get { return _Margin_Top; }
            set { _Margin_Top = value; }
        }

        private int _Margin_Left = 5;

        /// <summary>
        /// 左边距
        /// </summary>
        public int Margin_Left
        {
            get { return _Margin_Left; }
            set { _Margin_Left = value; }
        }


        private int _Margin_Right = 5;

        /// <summary>
        /// 右边距
        /// </summary>
        public int Margin_Right
        {
            get { return _Margin_Right; }
            set { _Margin_Right = value; }
        }

        private int _Margin_Bottom = 5;
        /// <summary>
        /// 下边距
        /// </summary>
        public int Margin_Bottom
        {
            get { return _Margin_Bottom; }
            set { _Margin_Bottom = value; }
        }
        #endregion

        private Font _TextFont = new Font("宋体", 12);

        /// <summary>
        /// 明文字体
        /// </summary>
        public Font TextFont
        {
            get { return _TextFont; }
            set { _TextFont = value; }
        }

        private Pen _BlackPen = new Pen(Brushes.Black);
        private Pen _WhitePen = new Pen(Brushes.White);

        /// <summary>
        /// 将明文装换为编码
        /// </summary>
        /// <param name="text">明文内容</param>
        /// <returns>编码</returns>
        private string ConvertToBarCode(string text)
        {
            string code = string.Empty;
            foreach (char item in text)
            {
                int itemValue = item;
                code += Convert.ToString(itemValue, 2) + ",";
            }
            return code;
        }

        /// <summary>
        /// 将字符串生成条形图片
        /// </summary>
        /// <param name="text">明文内容</param>
        /// <returns></returns>
        public Bitmap CreateBarCodeImage(string text,string filename)
        {
            Bitmap map = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(map);
            try
            {
                string code = ConvertToBarCode(text);
                code = string.Format("101{0}101", code);
                //code = text;//xx
                char[] array = code.ToCharArray();
                char[] textArray = text.ToCharArray();
                int lineWidth = (Width - Margin_Left - Margin_Right) / (array.Length - textArray.Length);
                int lineHeight = Height - TextHeight - Margin_Bottom;

                _BlackPen.Width = lineWidth;
                _WhitePen.Width = lineWidth;

                int x = 5;
                int topY = 5;
                int bottonY = Height - Margin_Bottom - TextHeight;

                int index = 0;
                char pItem = ' ';
                Pen pen = null;
                foreach (char item in array)
                {

                    if (item == ',')
                    {
                        string t = textArray[index].ToString();
                        g.DrawString(t, TextFont, Brushes.Black, new PointF(x - (lineWidth * 5), bottonY + 3));
                        index++;
                    }
                    else
                    {
                        if (pItem == ' ')
                        {
                            pen = _BlackPen;
                        }
                        else
                        {
                            if (item != pItem)
                            {
                                pen = pen == _BlackPen ? _WhitePen : _BlackPen;
                            }
                        }
                        pItem = item;
                        g.DrawLine(pen, new Point(x, topY), new Point(x, bottonY));
                    }
                    x += lineWidth;
                }
            }
            catch (Exception ex)
            {
                g.Clear(Color.White);
                g.DrawString(ex.Message, TextFont, Brushes.Black, new PointF(0, 0));
            }
            g.Save();
            map.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
            return map;
        }
    }
}