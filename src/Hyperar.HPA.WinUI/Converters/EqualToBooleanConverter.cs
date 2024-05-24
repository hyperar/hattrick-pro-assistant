namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;

    internal class EqualToBooleanConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                return object.Equals(value, parameter);
            }
            else
            {
                return false;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}