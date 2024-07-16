namespace Hyperar.HPA.Domain
{
    using Domain.Interfaces;

    public class Country : HattrickEntityBase, IHattrickEntity
    {
        public string Code { get; set; } = string.Empty;

        public virtual Currency Currency { get; set; } = new Currency();

        public int CurrencyId { get; set; }

        public string DateFormat { get; set; } = string.Empty;

        public virtual ICollection<Junior.Player> JuniorPlayers { get; set; } = new HashSet<Junior.Player>();

        public virtual League League { get; set; } = new League();

        public long LeagueHattrickId { get; set; }

        public virtual ICollection<Manager> Managers { get; set; } = new HashSet<Manager>();

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Senior.MatchReferee> Referees { get; set; } = new HashSet<Senior.MatchReferee>();

        public virtual ICollection<Region> Regions { get; set; } = new HashSet<Region>();

        public virtual ICollection<Senior.Player> SeniorPlayers { get; set; } = new HashSet<Senior.Player>();

        public string TimeFormat { get; set; } = string.Empty;

        public virtual ICollection<Senior.Trainer> Trainers { get; set; } = new HashSet<Senior.Trainer>();
    }
}