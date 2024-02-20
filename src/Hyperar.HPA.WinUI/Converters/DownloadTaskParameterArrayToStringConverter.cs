namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Avalonia.Data.Converters;

    internal class DownloadTaskParameterArrayToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null && value is Dictionary<string, string> dictionary && dictionary.Count > 0)
            {
                List<string> parameters = new List<string>();

                foreach (var item in dictionary)
                {
                    parameters.Add($"{item.Key}: \"{item.Value}\"");
                }

                return string.Join(", ", parameters);
            }
            else
            {
                return string.Empty;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}