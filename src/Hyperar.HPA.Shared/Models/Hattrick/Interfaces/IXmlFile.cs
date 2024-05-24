namespace Hyperar.HPA.Shared.Models.Hattrick.Interfaces
{
    using System;

    public interface IXmlFile
    {
        DateTime FetchedDate { get; set; }

        string FileName { get; set; }

        long UserId { get; set; }

        decimal Version { get; set; }
    }
}