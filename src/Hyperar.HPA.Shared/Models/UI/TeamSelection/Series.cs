﻿namespace Hyperar.HPA.Shared.Models.UI.TeamSelection
{
    public class Series
    {
        public Series()
        {
            this.Name = string.Empty;
        }

        public long HattrickId { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}