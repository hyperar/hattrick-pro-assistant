﻿namespace Hyperar.HPA.Domain.Hattrick.TeamDetails
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot("BotStatus")]
    public class BotStatus
    {
        [XmlElement("IsBot")]
        public string IsBotString { get; set; } = string.Empty;

        [XmlIgnore]
        public bool IsBot
        {
            get
            {
                return bool.Parse(this.IsBotString.ToLower());
            }
        }
    }
}