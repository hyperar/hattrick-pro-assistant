namespace Hyperar.HPA.Domain.Hattrick
{
    using System;

    public class XmlFileBase
    {
        public XmlFileBase(string fileName)
        {
            this.FileName = fileName;
        }

        public string FileName { get; set; }

        public decimal Version { get; set; }

        public uint UserId { get; set; }

        public DateTime FetchedDate { get; set; }
    }
}
