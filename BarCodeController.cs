using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using  WSBarCode.Barcodes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
using System.IO;

namespace com.kissmett.Common
{
    public class BarCodeController
    {

        string _code = "";
        string _title = "";
        int _barSize = 30;
        WSBarCode.Barcodes.Code39 _c39 = new WSBarCode.Barcodes.Code39();

        public BarCodeController(string code, string title, int barSize)
        {
            this._code = code;
            this._title = title;
            this._barSize = barSize;

            //c39.FontFamilyName = @"Free 3 of 9"; 
            //c39.FontFileName = @"D:\ws\liuxue\dangan\src\fonts\Free3of9.ttf"; 
            _c39.FontFamilyName = ConfigurationSettings.AppSettings["BarCodeFontFamily"];
            _c39.FontFileName = ConfigurationSettings.AppSettings["BarCodeFontFile"];

            _c39.FontSize = _barSize; //30
            _c39.ShowCodeString = true;// showCodeString;
            if (_title + "" != "") _c39.Title = _title;
        }

        public void GenerateBarCodeImageFile( string outputimgfile)
        { 

            Bitmap objBitmap = _c39.GenerateBarcode(_code);
            //objBitmap.Save(ms, ImageFormat.Png); 
            objBitmap.Save(outputimgfile, ImageFormat.Png);


        }
        public byte[] GetBarCodeImageBytes()
        {

            // Create stream....
            MemoryStream ms = new MemoryStream();
            Bitmap objBitmap = _c39.GenerateBarcode(_code);
            objBitmap.Save(ms, ImageFormat.Png);

            byte[] by = new byte[ms.Length];
            ms.Seek(0, SeekOrigin.Begin);
            ms.Read(by, 0, by.Length);
            ms.Close();
            ms.Dispose();
            return by;

        }

        public MemoryStream GetBarCodeImageMemoryStream()
        {
            // Create stream....
            MemoryStream ms = new MemoryStream();
            Bitmap objBitmap = _c39.GenerateBarcode(_code);
            objBitmap.Save(ms, ImageFormat.Png);

            return ms;

        }
    }
}
