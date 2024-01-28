namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class DecimalAndCurrencyToCaltulatedTranslatedStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 && values[0] is uint value && values[1] is decimal rate && values[2] is string name)
            {
                string calculatedValue = (value / rate).ToString("N0");

                return string.Format(
                    Globalization.Strings.WeeklySalary,
                    calculatedValue,
                    name);
            }

            return "Converter Error";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}