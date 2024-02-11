namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class DateTimeToLocaleShortDateTimeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is DateTime dateTime
                ? $"{dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}"
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(nameof(this.ConvertBack));
        }
    }
}