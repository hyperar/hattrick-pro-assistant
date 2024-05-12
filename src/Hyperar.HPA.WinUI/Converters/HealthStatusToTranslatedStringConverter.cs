namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;

    internal class HealthStatusToTranslatedStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is short healthStatus)
            {
                return healthStatus switch
                {
                    -1 => null,
                    0 => Globalization.Translations.BruisedTooltip,
                    _ => string.Format(Globalization.Translations.InjuredTooltip, healthStatus)
                };
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}