namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class HealthIntToTranslatedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int healthLevel)
            {
                return healthLevel switch
                {
                    -1 => Globalization.Strings.Healthy,
                    0 => Globalization.Strings.Brusied,
                    _ => string.Format(Globalization.Strings.InjuredWeeks, healthLevel)
                };
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}