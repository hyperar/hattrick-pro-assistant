namespace Hyperar.HPA.Shared.Models.Hattrick
{
    using System;
    using Shared.Models.Hattrick.Interfaces;

    public class XmlFileBase : IXmlFile
    {
        public XmlFileBase(string fileName)
        {
            this.FileName = fileName;
        }

        public DateTime FetchedDate { get; set; }

        public string FileName { get; set; }

        public long UserId { get; set; }

        public decimal Version { get; set; }
    }
}