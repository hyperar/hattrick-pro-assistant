namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.IO;

    internal abstract class ResourceImageValueConverter : ByteArrayToImageConverterBase
    {
        protected const string transparentFileName = "transparent.gif";

        private const string resourcesFolderName = "pack://application:,,,/Resources/";

        protected static byte[] ReadImageFromResources(string path)
        {
            string fullPath = Path.Combine(
                resourcesFolderName,
                path);

            Uri uri = new Uri(fullPath);

            if (System.Windows.Application.GetResourceStream(uri).Stream is UnmanagedMemoryStream memoryStream)
            {
                byte[] content = Array.Empty<byte>();

                using (var binaryReader = new BinaryReader(memoryStream))
                {
                    content = new byte[memoryStream.Length];

                    binaryReader.Read(content, 0, content.Length);
                }

                return content;
            }
            else
            {
                throw new FileNotFoundException(path);
            }
        }
    }
}