namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Globalization;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Data.Converters;

    internal class HealthStatusToImageConverter : IValueConverter
    {
        private const string BruisedIconKey = "BruisedIcon";

        private const string InjuredIconKey = "InjuredIcon";

        private const string MissingIconKey = "MissingIcon";

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is short healthStatus)
            {
                return healthStatus switch
                {
                    -1 => null,
                    0 => Application.Current?.FindResource(BruisedIconKey) ?? Application.Current?.FindResource(MissingIconKey),
                    _ => Application.Current?.FindResource(InjuredIconKey) ?? Application.Current?.FindResource(MissingIconKey)
                };
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