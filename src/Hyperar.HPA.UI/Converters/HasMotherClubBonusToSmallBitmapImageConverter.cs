namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class HasMotherClubBonusToSmallBitmapImageConverter : ResourceImageValueConverter, IValueConverter
    {
        private const string isTransferListedFileName = "mother-club-bonus-16.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool hasMotherClubBonus
                ? ConvertByteArrayToBitMapImage(
                    ReadImageFromResources(
                        hasMotherClubBonus ? isTransferListedFileName : transparentFileName))
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}