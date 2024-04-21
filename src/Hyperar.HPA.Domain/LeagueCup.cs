namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.WorldDetails;

    public class LeagueCup : HattrickEntityBase, IHattrickEntity
    {
        public LeagueCup()
        {
            this.Name = string.Empty;
        }

        public byte CurrentRound { get; set; }

        public virtual League? League { get; set; }

        public long LeagueHattrickId { get; set; }

        public string Name { get; set; }

        public byte RoundsLeft { get; set; }

        public byte? SeriesLevel { get; set; }

        public LeagueCupSubType? SubType { get; set; }

        public LeagueCupType Type { get; set; }

        public static LeagueCup Create(Models.Cup xmlCup, League league)
        {
            return new LeagueCup
            {
                CurrentRound = xmlCup.MatchRound,
                HattrickId = xmlCup.CupId,
                League = league,
                Name = xmlCup.CupName,
                RoundsLeft = xmlCup.MatchRoundsLeft,
                SeriesLevel = xmlCup.CupLeagueLevel == 0 ? null : xmlCup.CupLeagueLevel,
                SubType = xmlCup.CupLevel == 2 ? (LeagueCupSubType)xmlCup.CupLevelIndex : null,
                Type = (LeagueCupType)xmlCup.CupLevel
            };
        }

        public bool HasChanged(Models.Cup xmlLeagueCup)
        {
            return this.Name != xmlLeagueCup.CupName
                || this.CurrentRound != xmlLeagueCup.MatchRound
                || this.RoundsLeft != xmlLeagueCup.MatchRoundsLeft;
        }

        public void Update(Models.Cup xmlLeagueCup)
        {
            this.Name = xmlLeagueCup.CupName;
            this.CurrentRound = xmlLeagueCup.MatchRound;
            this.RoundsLeft = xmlLeagueCup.MatchRoundsLeft;
        }
    }
}