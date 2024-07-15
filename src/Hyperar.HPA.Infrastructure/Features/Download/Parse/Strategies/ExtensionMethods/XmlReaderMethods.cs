namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    public static class XmlReaderMethods
    {
        private const string comma = ",";

        private const string period = ".";

        public static bool CheckNode(this XmlReader reader, params string[] expectedNames)
        {
            return expectedNames.Any(x => x.Equals(reader.Name, StringComparison.OrdinalIgnoreCase));
        }

        public static async Task<bool> ReadXmlValueAsBoolAsync(this XmlReader reader)
        {
            string value = await reader.ReadElementContentAsStringAsync();

            return value.Length == 1
                 ? value == "1"
                 : value.Equals(bool.TrueString, StringComparison.CurrentCultureIgnoreCase);
        }

        public static async Task<DateTime> ReadXmlValueAsDateTimeAsync(this XmlReader reader)
        {
            string value = await reader.ReadElementContentAsStringAsync();

            return DateTime.Parse(value);
        }

        public static async Task<decimal> ReadXmlValueAsDecimalAsync(this XmlReader reader)
        {
            string value = await reader.ReadElementContentAsStringAsync();

            string numberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            return value.Contains(period, StringComparison.CurrentCulture) && numberDecimalSeparator != period
                ? decimal.Parse(value.Replace(period, numberDecimalSeparator))
                : value.Contains(comma, StringComparison.CurrentCulture) && numberDecimalSeparator != comma
                ? decimal.Parse(value.Replace(comma, numberDecimalSeparator))
                : decimal.Parse(value);
        }

        public static async Task<int> ReadXmlValueAsIntAsync(this XmlReader reader)
        {
            return int.Parse(await reader.ReadElementContentAsStringAsync());
        }

        public static async Task<long> ReadXmlValueAsLongAsync(this XmlReader reader)
        {
            return long.Parse(await reader.ReadElementContentAsStringAsync());
        }

        public static async Task<DateTime?> ReadXmlValueAsNullableDateTimeAsync(this XmlReader reader)
        {
            string value = await reader.ReadElementContentAsStringAsync();

            return string.IsNullOrWhiteSpace(value)
                 ? null
                 : DateTime.Parse(value);
        }

        public static async Task<decimal?> ReadXmlValueAsNullableDecimalAsync(this XmlReader reader, decimal? nullValue = null)
        {
            string value = await reader.ReadElementContentAsStringAsync();

            string numberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            decimal decimalValue = value.Contains(period, StringComparison.CurrentCulture) && numberDecimalSeparator != period
                                 ? decimal.Parse(value.Replace(period, numberDecimalSeparator))
                                 : value.Contains(comma, StringComparison.CurrentCulture) && numberDecimalSeparator != comma
                                 ? decimal.Parse(value.Replace(comma, numberDecimalSeparator))
                                 : decimal.Parse(value);

            return nullValue.HasValue && nullValue.Value == decimalValue
                 ? null
                 : decimalValue;
        }

        public static async Task<int?> ReadXmlValueAsNullableIntAsync(this XmlReader reader, int? nullValue = null)
        {
            string value = await reader.ReadElementContentAsStringAsync();

            return value == nullValue?.ToString() || string.IsNullOrWhiteSpace(value) ? null : int.Parse(value);
        }

        public static async Task<long?> ReadXmlValueAsNullableLongAsync(this XmlReader reader, long? nullValue = null)
        {
            string value = await reader.ReadElementContentAsStringAsync();

            return value == nullValue?.ToString() ? null : long.Parse(value);
        }

        public static async Task<string?> ReadXmlValueAsNullableStringAsync(this XmlReader reader)
        {
            string? value = await reader.ReadElementContentAsStringAsync();

            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
    }
}