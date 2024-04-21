namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser.ExtensionMethods
{
    using System;
    using System.Globalization;
    using System.Xml;

    public static class XmlReaderExtensionMethods
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

        public static async Task<byte> ReadXmlValueAsByteAsync(this XmlReader reader)
        {
            return byte.Parse(await reader.ReadElementContentAsStringAsync());
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

        public static async Task<byte?> ReadXmlValueAsNullableByteAsync(this XmlReader reader, byte? nullValue = null)
        {
            string value = await reader.ReadElementContentAsStringAsync();

            return value == nullValue?.ToString() ? null : byte.Parse(value);
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

        public static async Task<short> ReadXmlValueAsShortAsync(this XmlReader reader)
        {
            return short.Parse(await reader.ReadElementContentAsStringAsync());
        }
    }
}