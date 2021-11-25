using System.Drawing; /* Bitmap */
using System.Drawing.Imaging; /* Image Format */
using System.IO; /* MemoryStream */
using System.Windows.Media.Imaging; /* BitmapImage*/
namespace WPFVidRecApp
{
    static class BitmapHandler
    {
        public static BitmapImage convertBitmapToBitmapImage(this Bitmap bitmap)
        {
            BitmapImage bi = new BitmapImage(); /* Provides */ 
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }
    }
}
