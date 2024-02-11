namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class ByteArrayToFlagBitmapImageConverter : ByteArrayToImageConverterBase, IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is byte[] byteArray
                ? ConvertByteArrayToBitMapImage(byteArray)
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(nameof(this.ConvertBack));
        }
    }
}