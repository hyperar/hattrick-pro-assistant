namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Avalonia;
    using Avalonia.Data.Converters;

    public class MoneyConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            // This is due to an bug in Avalonia.
            if (values.Any(x => x is UnsetValueType))
            {
                return false;
            }

            if (values.Count == 3 && values[0] is long value &&
                values[1] is decimal rate
                && values[2] is string name)
            {
                string calculatedValue = (value / rate).ToString("N0");

                return string.Format(
                    Globalization.Translations.WeeklySalaryMask,
                    calculatedValue,
                    name);
            }

            return "Converter Error";
        }
    }
}
