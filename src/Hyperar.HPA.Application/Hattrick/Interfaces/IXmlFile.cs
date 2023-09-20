namespace Hyperar.HPA.Application.Hattrick.Interfaces
{
    using System;

    public interface IXmlFile
    {
        DateTime FetchedDate { get; set; }

        string FileName { get; set; }

        uint UserId { get; set; }

        decimal Version { get; set; }
    }
}