namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class HealthToLargeBitmapImageConverter : ResourceImageValueConverter, IValueConverter
    {
        private const string bandageFileName = "bandage-30.png";

        private const string clinicFileName = "clinic-30.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fileName = string.Empty;

            if (value is int health)
            {
                fileName = health switch
                {
                    -1 => transparentFileName,
                    0 => bandageFileName,
                    _ => clinicFileName
                };
            }

            return ConvertByteArrayToBitMapImage(
                    ReadImageFromResources(
                        fileName));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}