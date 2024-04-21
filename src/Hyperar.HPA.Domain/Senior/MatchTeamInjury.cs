namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.MatchDetails;

    public class MatchTeamInjury : EntityBase, IEntity
    {
        public MatchTeamInjury()
        {
            this.MatchTeam = new MatchTeam();

            this.PlayerName = string.Empty;
        }

        public byte Index { get; set; }

        public MatchPart MatchPart { get; set; }

        public virtual MatchTeam MatchTeam { get; set; }

        public int MatchTeamId { get; set; }

        public byte Minute { get; set; }

        public long PlayerHattrickId { get; set; }

        public string PlayerName { get; set; }

        public InjuryType Type { get; set; }

        public static MatchTeamInjury Create(Models.Injury xmlInjury, MatchTeam matchTeam)
        {
            return new MatchTeamInjury
            {
                Index = xmlInjury.Index,
                MatchPart = (MatchPart)xmlInjury.MatchPart,
                MatchTeam = matchTeam,
                Minute = xmlInjury.InjuryMinute,
                PlayerHattrickId = xmlInjury.InjuryPlayerId,
                PlayerName = xmlInjury.InjuryPlayerName,
                Type = (InjuryType)xmlInjury.InjuryType
            };
        }
    }
}