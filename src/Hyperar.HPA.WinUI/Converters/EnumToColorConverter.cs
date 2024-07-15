namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;
    using Avalonia.Media;
    using Shared.Enums;

    internal class EnumToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is BookingStatus bookingStatus)
            {
                var color = bookingStatus switch
                {
                    BookingStatus.NoBookings => Color.FromArgb(0, 0, 0, 0),
                    BookingStatus.Suspended => Color.FromRgb(255, 51, 51),
                    _ => Color.FromRgb(255, 204, 0)
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