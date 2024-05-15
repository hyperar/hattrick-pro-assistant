namespace Hyperar.HPA.Domain
{
    using System.Collections.Generic;
    using Domain.Interfaces;
    using Domain.Senior;
    using Models = Shared.Models.Hattrick.WorldDetails;

    public class League : HattrickEntityBase, IHattrickEntity
    {
        public League()
        {
            this.Cups = new HashSet<LeagueCup>();
            this.Teams = new HashSet<Team>();

            this.Continent = string.Empty;
            this.EnglishName = string.Empty;
            this.FlagBytes = Array.Empty<byte>();
            this.LanguageName = string.Empty;
            this.Name = string.Empty;
            this.ShortName = string.Empty;
            this.Zone = string.Empty;
        }

        public int ActiveTeams { get; set; }

        public int ActiveUsers { get; set; }

        public string Continent { get; set; }

        public virtual Country? Country { get; set; }

        public virtual ICollection<LeagueCup> Cups { get; set; }

        public string EnglishName { get; set; }

        public byte[] FlagBytes { get; set; }

        public long JuniorNationalTeamId { get; set; }

        public long LanguageId { get; set; }

        public string LanguageName { get; set; }

        public string Name { get; set; }

        public DateTime? NextCupMatchDate { get; set; }

        public DateTime NextEconomyUpdate { get; set; }

        public DateTime NextSeriesMatchDate { get; set; }

        public DateTime NextTrainingUpdate { get; set; }

        public int NumberOfLevels { get; set; }

        public int Season { get; set; }

        public int SeasonOffset { get; set; }

        public long SeniorNationalTeamId { get; set; }

        public string ShortName { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public int WaitingUsers { get; set; }

        public int Week { get; set; }

        public string Zone { get; set; }

        public static League Create(Models.League xmlLeague, byte[] flagImageBytes)
        {
            League league = new League
            {
                ActiveTeams = xmlLeague.ActiveTeams,
                ActiveUsers = xmlLeague.ActiveUsers,
                Continent = xmlLeague.Continent,
                EnglishName = xmlLeague.EnglishName,
                FlagBytes = flagImageBytes,
                HattrickId = xmlLeague.LeagueId,
                JuniorNationalTeamId = xmlLeague.U20TeamId,
                LanguageId = xmlLeague.LeagueId,
                LanguageName = xmlLeague.LeagueName,
                Name = xmlLeague.LeagueName,
                NextCupMatchDate = xmlLeague.CupMatchDate,
                NextEconomyUpdate = xmlLeague.EconomyDate,
                NextSeriesMatchDate = xmlLeague.SeriesMatchDate,
                NextTrainingUpdate = xmlLeague.TrainingDate,
                NumberOfLevels = xmlLeague.NumberOfLevels,
                Season = xmlLeague.Season,
                SeasonOffset = xmlLeague.SeasonOffset,
                SeniorNationalTeamId = xmlLeague.NationalTeamId,
                ShortName = xmlLeague.ShortName,
                WaitingUsers = xmlLeague.WaitingUsers,
                Week = xmlLeague.MatchRound,
                Zone = xmlLeague.ZoneName
            };

            return league;
        }

        public bool HasChanged(Models.League xmlLeague)
        {
            return ActiveTeams != xmlLeague.ActiveTeams
                || ActiveUsers != xmlLeague.ActiveUsers
                || EnglishName != xmlLeague.EnglishName
                || LanguageId != xmlLeague.LeagueId
                || LanguageName != xmlLeague.LeagueName
                || Name != xmlLeague.LeagueName
                || NextCupMatchDate != xmlLeague.CupMatchDate
                || NextEconomyUpdate != xmlLeague.EconomyDate
                || NextSeriesMatchDate != xmlLeague.SeriesMatchDate
                || NextTrainingUpdate != xmlLeague.TrainingDate
                || NumberOfLevels != xmlLeague.NumberOfLevels
                || Season != xmlLeague.Season
                || ShortName != xmlLeague.ShortName
                || WaitingUsers != xmlLeague.WaitingUsers
                || Week != xmlLeague.MatchRound;
        }

        public void Update(Models.League xmlLeague)
        {
            this.ActiveTeams = xmlLeague.ActiveTeams;
            this.ActiveUsers = xmlLeague.ActiveUsers;
            this.EnglishName = xmlLeague.EnglishName;
            this.LanguageId = xmlLeague.LeagueId;
            this.LanguageName = xmlLeague.LeagueName;
            this.Name = xmlLeague.LeagueName;
            this.NextCupMatchDate = xmlLeague.CupMatchDate;
            this.NextEconomyUpdate = xmlLeague.EconomyDate;
            this.NextSeriesMatchDate = xmlLeague.SeriesMatchDate;
            this.NextTrainingUpdate = xmlLeague.TrainingDate;
            this.NumberOfLevels = xmlLeague.NumberOfLevels;
            this.Season = xmlLeague.Season;
            this.ShortName = xmlLeague.ShortName;
            this.WaitingUsers = xmlLeague.WaitingUsers;
            this.Week = xmlLeague.MatchRound;
        }
    }
}