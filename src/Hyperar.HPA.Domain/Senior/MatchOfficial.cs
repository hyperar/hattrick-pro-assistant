namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick.MatchDetails;

    public class MatchOfficial : EntityBase, IEntity
    {
        public MatchOfficial()
        {
            this.Country = new Country();
            this.Match = new Match();

            this.Name = string.Empty;
        }

        public virtual Country Country { get; set; }

        public long CountryHattrickId { get; set; }

        public long HattrickId { get; set; }

        public virtual Match Match { get; set; }

        public long MatchHattrickId { get; set; }

        public string Name { get; set; }

        public static MatchOfficial Create(Models.Referee xmlReferee, Match match, Country country)
        {
            return new MatchOfficial
            {
                Country = country,
                HattrickId = xmlReferee.RefereeId,
                Match = match,
                Name = xmlReferee.RefereeName
            };
        }
    }
}