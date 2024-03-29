﻿namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser.ExtensionMethods
{
    using System;
    using System.Globalization;
    using System.Xml;
    using Common.Enums;

    public static class XmlReaderExtensionMethods
    {
        private const string comma = ",";

        private const string period = ".";

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

        public static async Task<string?> ReadXmlValueAsNullableStringAsync(this XmlReader reader)
        {
            string? value = await reader.ReadElementContentAsStringAsync();

            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public static async Task<uint?> ReadXmlValueAsNullableUintAsync(this XmlReader reader, uint? nullValue = null)
        {
            string value = await reader.ReadElementContentAsStringAsync();

            return value == nullValue?.ToString() ? null : uint.Parse(value);
        }

        public static async Task<SkillLevel> ReadXmlValueAsSkillLevelAsync(this XmlReader reader)
        {
            return (SkillLevel)await reader.ReadXmlValueAsIntAsync();
        }

        public static async Task<uint> ReadXmlValueAsUintAsync(this XmlReader reader)
        {
            return uint.Parse(await reader.ReadElementContentAsStringAsync());
        }
    }
}