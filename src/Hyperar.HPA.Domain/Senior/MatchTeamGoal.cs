namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.MatchDetails;

    public class MatchTeamGoal : EntityBase, IEntity
    {
        public MatchTeamGoal()
        {
            this.MatchTeam = new MatchTeam();

            this.PlayerName = string.Empty;
        }

        public byte AwayTeamScore { get; set; }

        public byte HomeTeamScore { get; set; }

        public byte Index { get; set; }

        public MatchPart MatchPart { get; set; }

        public virtual MatchTeam MatchTeam { get; set; }

        public int MatchTeamId { get; set; }

        public byte Minute { get; set; }

        public long PlayerHattrickId { get; set; }

        public string PlayerName { get; set; }

        public static MatchTeamGoal Create(Models.Goal xmlGoal, MatchTeam matchTeam)
        {
            return new MatchTeamGoal
            {
                AwayTeamScore = xmlGoal.ScorerAwayGoals,
                HomeTeamScore = xmlGoal.ScorerHomeGoals,
                Index = xmlGoal.Index,
                MatchPart = (MatchPart)xmlGoal.MatchPart,
                MatchTeam = matchTeam,
                Minute = xmlGoal.ScorerMinute,
                PlayerHattrickId = xmlGoal.ScorerPlayerId,
                PlayerName = xmlGoal.ScorerPlayerName
            };
        }
    }
}