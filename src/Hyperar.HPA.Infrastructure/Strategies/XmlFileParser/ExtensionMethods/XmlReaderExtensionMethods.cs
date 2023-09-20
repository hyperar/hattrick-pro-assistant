namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser.ExtensionMethods
{
    using System;
    using System.Globalization;
    using System.Xml;
    using Hyperar.HPA.Common.Enums;

    public static class XmlReaderExtensionMethods
    {
        private const string comma = ",";

        private const string period = ".";

        public static bool ReadXmlValueAsBool(this XmlReader reader)
        {
            string value = reader.ReadElementContentAsString();

            return value.Length == 1
                 ? value == "1"
                 : value.ToLower() == bool.TrueString.ToLower();
        }

        public static DateTime ReadXmlValueAsDateTime(this XmlReader reader)
        {
            string value = reader.ReadElementContentAsString();

            return DateTime.Parse(value);
        }

        public static decimal ReadXmlValueAsDecimal(this XmlReader reader)
        {
            string value = reader.ReadElementContentAsString();

            string numberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            return value.IndexOf(period) != -1 && numberDecimalSeparator != period
                ? decimal.Parse(value.Replace(period, numberDecimalSeparator))
                : value.IndexOf(comma) != -1 && numberDecimalSeparator != comma
                ? decimal.Parse(value.Replace(comma, numberDecimalSeparator))
                : decimal.Parse(value);
        }

        public static int ReadXmlValueAsInt(this XmlReader reader)
        {
            return int.Parse(reader.ReadElementContentAsString());
        }

        public static string? ReadXmlValueAsNullableString(this XmlReader reader)
        {
            string? value = reader.ReadElementContentAsString();

            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public static uint? ReadXmlValueAsNullableUint(this XmlReader reader, uint? nullValue = null)
        {
            string value = reader.ReadElementContentAsString();

            return value == nullValue?.ToString() ? null : uint.Parse(value);
        }

        public static SkillLevel ReadXmlValueAsSkillLevel(this XmlReader reader)
        {
            return (SkillLevel)reader.ReadXmlValueAsInt();
        }

        public static uint ReadXmlValueAsUint(this XmlReader reader)
        {
            return uint.Parse(reader.ReadElementContentAsString());
        }
    }
}