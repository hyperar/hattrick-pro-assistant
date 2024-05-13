namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using System.Collections.Generic;
    using Domain.Interfaces;
    using Shared.Enums;
    using Shared.ExtensionMethods;
    using Models = Shared.Models.Hattrick.MatchDetails;

    public class Match : HattrickEntityBase, IHattrickEntity
    {
        public Match()
        {
            this.Events = new HashSet<MatchEvent>();
            this.Officials = new HashSet<MatchOfficial>();
            this.Team = new Team();
            this.Teams = new HashSet<MatchTeam>();
        }

        public byte? AddedMinutes { get; set; }

        public virtual MatchArena? Arena { get; set; }

        public long? CompetitionId { get; set; }

        public virtual ICollection<MatchEvent> Events { get; set; }

        public DateTime? FinishDate { get; set; }

        public virtual ICollection<MatchOfficial> Officials { get; set; }

        public MatchResult? Result { get; set; }

        public MatchRule Rules { get; set; }

        public DateTime StartDate { get; set; }

        public MatchSystem System { get; set; }

        public virtual Team Team { get; set; }

        public long TeamHattrickId { get; set; }

        public virtual ICollection<MatchTeam> Teams { get; set; }

        public MatchType Type { get; set; }

        public Weather? Weather { get; set; }

        public static Match Create(Models.Match xmlMatch, string system, MatchResult? result, Team team)
        {
            return new Match
            {
                AddedMinutes = xmlMatch.AddedMinutes,
                CompetitionId = xmlMatch.MatchContextId != 0 ? xmlMatch.MatchContextId : null,
                FinishDate = xmlMatch.FinishedDate,
                HattrickId = xmlMatch.MatchId,
                Result = result,
                Rules = (MatchRule)xmlMatch.MatchRuleId,
                StartDate = xmlMatch.MatchDate,
                System = system.ToMatchSystem(),
                Team = team,
                Type = (MatchType)xmlMatch.MatchType,
                Weather = xmlMatch.Arena.WeatherId != null ? (Weather)xmlMatch.Arena.WeatherId : null,
            };
        }

        public bool HasChanged(Models.Match xmlMatch, MatchResult? result)
        {
            return this.AddedMinutes != xmlMatch.AddedMinutes
                || this.FinishDate != xmlMatch.FinishedDate
                || this.Result != result
                || this.Weather != (xmlMatch.Arena.WeatherId != null ? (Weather)xmlMatch.Arena.WeatherId : null);
        }

        public void Update(Models.Match xmlMatch, MatchResult? result)
        {
            this.AddedMinutes = xmlMatch.AddedMinutes;
            this.FinishDate = xmlMatch.FinishedDate;
            this.Result = result;
            this.Weather = xmlMatch.Arena.WeatherId != null ? (Weather)xmlMatch.Arena.WeatherId : null;
        }
    }
}