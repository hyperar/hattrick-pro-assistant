namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;

    public class PressAnnouncement
    {
        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public DateTime SendDate { get; set; }
    }
}
