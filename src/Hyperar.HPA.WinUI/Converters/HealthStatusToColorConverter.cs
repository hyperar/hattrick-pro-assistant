namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;
    using Avalonia.Media;

    internal class HealthStatusToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int healthStatus)
            {
                var color = healthStatus switch
                {
                    -1 => Color.FromArgb(0, 0, 0, 0),
                    0 => Color.FromRgb(247, 195, 176),
                    _ => Color.FromRgb(255, 51, 51)
                };

                return new SolidColorBrush(color);
            }
            else
            {
                return null;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}