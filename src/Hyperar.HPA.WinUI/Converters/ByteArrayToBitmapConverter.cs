namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Globalization;
    using System.IO;
    using Avalonia.Controls;
    using Avalonia.Data.Converters;
    using Avalonia.Media.Imaging;

    internal class ByteArrayToBitmapConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is byte[] imageBytes)
            {
                using (var memoryStream = new MemoryStream(imageBytes))
                {
                    return new Bitmap(memoryStream);
                }
            }
            else if (parameter is Image fallbackImage)
            {
                return fallbackImage.Source;
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}