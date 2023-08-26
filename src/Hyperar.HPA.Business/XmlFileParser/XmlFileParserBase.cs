namespace Hyperar.HPA.Business.XmlFileParser
{
    using System;
    using System.Globalization;
    using System.Xml;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Domain.Hattrick;

    public abstract class XmlFileParserBase : IXmlFileParserStrategy
    {
        private const string period = ".";

        private const string comma = ",";

        public void Parse(XmlReader reader, ref XmlFileBase result)
        {
            result.Version = this.ParseDecimalValue(reader.ReadElementContentAsString());
            result.UserId = uint.Parse(reader.ReadElementContentAsString());
            result.FetchedDate = this.ParseDateTimeValue(reader.ReadElementContentAsString());

            this.ParseFileTypeSpecificContent(reader, ref result);
        }

        public abstract void ParseFileTypeSpecificContent(XmlReader reader, ref XmlFileBase entity);

        protected DateTime ParseDateTimeValue(string value)
        {
            return DateTime.Parse(value);
        }

        protected decimal ParseDecimalValue(string value)
        {
            string numberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (value.IndexOf(period) != -1 && numberDecimalSeparator != period)
            {
                return decimal.Parse(value.Replace(period, numberDecimalSeparator));
            }

            if (value.IndexOf(comma) != -1 && numberDecimalSeparator != comma)
            {
                return decimal.Parse(value.Replace(comma, numberDecimalSeparator));
            }

            return decimal.Parse(value);
        }
    }
}
