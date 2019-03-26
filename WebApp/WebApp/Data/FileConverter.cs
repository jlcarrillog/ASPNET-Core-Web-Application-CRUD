using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.IO;

namespace WebApp
{
    public class FileConverter
    {
        public static byte[] ConvertBinary(IFormFile file, int width)
        {
            var meSt = new MemoryStream();
            file.CopyTo(meSt);
            meSt = ResizeImage(meSt, width);
            return meSt.ToArray();
        }
        public static byte[] ConvertBinary(IFormFile file)
        {
            var meSt = new MemoryStream();
            file.CopyTo(meSt);
            return meSt.ToArray();
        }
        public static MemoryStream ResizeImage(MemoryStream ms, int w)
        {
            Image img = Image.FromStream(ms);
            int h = Convert.ToInt32(w * img.Height / img.Width);
            Image imgN = img.GetThumbnailImage(w, h, null, IntPtr.Zero);
            MemoryStream res = new MemoryStream();
            imgN.Save(res, img.RawFormat);
            return res;
        }
    }
}
