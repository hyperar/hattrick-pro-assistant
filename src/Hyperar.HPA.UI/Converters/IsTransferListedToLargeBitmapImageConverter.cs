namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Common.Enums;

    internal class IsTransferListedToLargeBitmapImageConverter : ResourceImageValueConverter, IValueConverter
    {
        private const string isTransferListedFileName = "price-tag-30.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool isTransferListed
                ? ConvertByteArrayToBitMapImage(
                    ReadImageFromResources(
                        isTransferListed ? isTransferListedFileName : transparentFileName))
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}