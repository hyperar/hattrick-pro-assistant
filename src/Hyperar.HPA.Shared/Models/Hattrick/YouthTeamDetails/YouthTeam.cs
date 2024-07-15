namespace Hyperar.HPA.Shared.Models.Hattrick.YouthTeamDetails
{
    using System;
    using System.Collections.Generic;

    public class YouthTeam
    {
        public IdName Country { get; set; } = new IdName();

        public DateTime CreatedDate { get; set; }

        public DateTime NextTrainingMatchDate { get; set; }

        public IdName? OwningTeam { get; set; } = new IdName();

        public IdName Region { get; set; } = new IdName();

        public List<Scout> ScoutList { get; set; } = new List<Scout>();

        public string ShortTeamName { get; set; } = string.Empty;

        public long UserId { get; set; }

        public IdName YouthArena { get; set; } = new IdName();

        public YouthLeague? YouthLeague { get; set; }

        public long YouthTeamId { get; set; }

        public string YouthTeamName { get; set; } = string.Empty;

        public YouthTrainer YouthTrainer { get; set; } = new YouthTrainer();
    }
}