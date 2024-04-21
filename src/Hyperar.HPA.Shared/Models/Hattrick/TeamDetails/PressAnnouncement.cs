namespace Hyperar.HPA.Shared.Models.Hattrick.TeamDetails
{
    using System;

    public class PressAnnouncement
    {
        public PressAnnouncement()
        {
            this.Body = string.Empty;
            this.Subject = string.Empty;
        }

        public string Body { get; set; }

        public DateTime SendDate { get; set; }

        public string Subject { get; set; }
    }
}