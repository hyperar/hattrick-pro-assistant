namespace Hyperar.HPA.Application.Hattrick
{
    using System;
    using Hyperar.HPA.Application.Hattrick.Interfaces;

    public class XmlFileBase : IXmlFile
    {
        public XmlFileBase(string fileName)
        {
            this.FileName = fileName;
        }

        public DateTime FetchedDate { get; set; }

        public string FileName { get; set; }

        public uint UserId { get; set; }

        public decimal Version { get; set; }
    }
}