namespace Hyperar.HPA.Application.Hattrick.TeamDetails
{
    using System;

    public class PressAnnouncement
    {
        public string Body { get; set; } = string.Empty;

        public DateTime SendDate { get; set; }

        public string Subject { get; set; } = string.Empty;
    }
}