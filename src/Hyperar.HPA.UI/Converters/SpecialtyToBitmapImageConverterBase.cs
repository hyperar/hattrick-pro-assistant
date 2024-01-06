namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Hyperar.HPA.Common.Enums;

    internal abstract class SpecialtyToBitmapImageConverterBase : ResourceImageValueConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Specialty specialty
                ? ConvertByteArrayToBitMapImage(
                    ReadImageFromResources(
                        GetFileNameFromSpecialty(specialty)))
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected abstract string GetFileNameFromSpecialty(Specialty specialty);
    }
}