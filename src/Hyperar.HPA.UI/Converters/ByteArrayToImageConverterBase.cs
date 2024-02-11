namespace Hyperar.HPA.UI.Converters
{
    using System.IO;
    using System.Windows.Media.Imaging;

    internal abstract class ByteArrayToImageConverterBase
    {
        protected static BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();

            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = memStream;
                img.EndInit();
                img.Freeze();
            }

            return img;
        }
    }
}