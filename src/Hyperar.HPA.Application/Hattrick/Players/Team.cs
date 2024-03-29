﻿namespace Hyperar.HPA.Application.Hattrick.Players
{
    using System.Collections.Generic;

    public class Team
    {
        public List<Player> PlayerList { get; set; } = new List<Player>();

        public uint TeamId { get; set; }

        public string TeamName { get; set; } = string.Empty;
    }
}