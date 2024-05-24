namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;

    internal class EnumToTranslatedStringAndValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            object underlyingValue = System.Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));

            string translatedString = Globalization.Translations.ResourceManager.GetString($"{value.GetType().Name}_{value}") ?? string.Empty;

            return $"{translatedString} ({underlyingValue})";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}