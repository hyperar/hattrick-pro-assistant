namespace Hyperar.HPA.Business
{
    using System.Linq;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Data;
    using Hyperar.HPA.Domain.Database;

    public class WorldService : IWorldService
    {
        private readonly DatabaseContextFactory databaseContextFactory;

        public WorldService(DatabaseContextFactory databaseContextFactory)
        {
            this.databaseContextFactory = databaseContextFactory;
        }

        public void ProcessWorldDetails(Domain.Hattrick.XmlFileBase entity)
        {
            var worldDetails = entity as Domain.Hattrick.WorldDetails.HattrickData ?? throw new ArgumentNullException(nameof(entity));

            using (var context = this.databaseContextFactory.CreateDbContext())
            {
                context.BeginTransaction();

                try
                {
                    foreach (var curLeague in worldDetails.LeagueList)
                    {
                        var league = context.Leagues.Where(x => x.HattrickId == curLeague.LeagueId).SingleOrDefault();

                        if (league == null)
                        {
                            league = this.CreateLeague(curLeague);

                            context.Leagues.Add(league);
                        }
                        else
                        {
                            this.UpdateLeague(curLeague, ref league);

                            context.Leagues.Update(league);
                        }

                        context.SaveChanges();

                        var leagueCalendar = context.LeagueCalendars.Where(x => x.LeagueId == league.Id).SingleOrDefault();

                        if (leagueCalendar == null)
                        {
                            leagueCalendar = this.CreateLeagueCalendar(curLeague, league.Id);

                            context.LeagueCalendars.Add(leagueCalendar);
                        }
                        else
                        {
                            this.UpdateLeagueCalendar(curLeague, ref leagueCalendar);

                            context.LeagueCalendars.Update(leagueCalendar);
                        }

                        context.SaveChanges();

                        foreach (var curCup in curLeague.Cups)
                        {
                            var leagueCup = context.LeagueCups.Where(x => x.HattrickId == curCup.CupId).SingleOrDefault();

                            if (leagueCup == null)
                            {
                                leagueCup = this.CreateLeagueCup(curCup, league.Id);

                                context.LeagueCups.Add(leagueCup);
                            }
                            else
                            {
                                this.UpdateLeagueCup(curCup, ref leagueCup);

                                context.LeagueCups.Update(leagueCup);
                            }
                        }

                        context.SaveChanges();

                        var country = context.Countries.Where(x => x.LeagueId == curLeague.LeagueId).SingleOrDefault();

                        // If current file league has a country.
                        if (curLeague.Country.Available)
                        {
                            // Country not found on the database.
                            if (country == null)
                            {
                                // Tries to create country from file data.
                                country = this.CreateCountry(curLeague.Country, league.Id);

                                if (country != null)
                                {
                                    context.Countries.Add(country);
                                }
                            }
                            else // Country found.
                            {
                                // Updates values.
                                this.UpdateCountry(curLeague.Country, ref country);

                                context.Countries.Update(country);
                            }
                        }
                        else // If not country on file data but country found on database, it removes it.
                        {
                            if (country != null)
                            {
                                context.Countries.Remove(country);
                            }
                        }

                        context.SaveChanges();

                        if (curLeague.Country.Available && curLeague.Country.RegionList != null && curLeague.Country.RegionList.Count > 0)
                        {
                            if (country != null)
                            {
                                foreach (var curRegion in curLeague.Country.RegionList)
                                {
                                    var region = context.Regions.Where(x => x.HattrickId == curRegion.RegionId).SingleOrDefault();

                                    if (region == null)
                                    {
                                        region = this.CreateRegion(curRegion, country.Id);

                                        context.Regions.Add(region);
                                    }
                                    else
                                    {
                                        this.UpdateRegion(curRegion, ref region);

                                        context.Regions.Update(region);
                                    }
                                }

                                context.SaveChanges();
                            }
                        }
                    }
                }
                catch
                {
                    context.Cancel();

                    throw;
                }
                finally
                {
                    context.EndTransaction();
                }
            }
        }

        private League CreateLeague(Domain.Hattrick.WorldDetails.League league)
        {
            return new League
            {

                HattrickId = league.LeagueId,
                Name = league.LeagueName,
                ShortName = league.ShortName,
                EnglishName = league.EnglishName,
                Continent = league.Continent,
                Zone = league.ZoneName,
                Season = league.Season,
                SeasonOffset = league.SeasonOffset,
                CurrentRound = league.MatchRound,
                LanguageId = league.LanguageId,
                LanguageName = league.LanguageName,
                SeniorNationalTeamId = league.NationalTeamId,
                JuniorNationalTeamId = league.U20TeamId,
                ActiveTeams = league.ActiveTeams,
                ActiveUsers = league.ActiveUsers,
                WaitingUsers = league.WaitingUsers,
                NumberOfLevels = league.NumberOfLevels
            };
        }

        private void UpdateLeague(Domain.Hattrick.WorldDetails.League league, ref League dbLeague)
        {
            dbLeague.ActiveTeams = league.ActiveTeams;
            dbLeague.ActiveUsers = league.ActiveUsers;
            dbLeague.WaitingUsers = league.WaitingUsers;
            dbLeague.CurrentRound = league.MatchRound;
            dbLeague.Season = league.Season;
        }

        private LeagueCalendar CreateLeagueCalendar(Domain.Hattrick.WorldDetails.League league, int leagueId)
        {
            return new LeagueCalendar
            {
                LeagueId = leagueId,
                NextTrainingUpdate = league.TrainingDate,
                NextEconomyUpdate = league.EconomyDate,
                NextCupMatchDate = league.CupMatchDate,
                NextSeriesMatchDate = league.SeriesMatchDate,
                FirstWeeklyUpdate = league.Sequence1,
                SecondWeeklyUpdate = league.Sequence2,
                ThirdWeeklyUpdate = league.Sequence3,
                FourthWeeklyUpdate = league.Sequence5,
                FifthWeeklyUpdate = league.Sequence7
            };
        }

        private void UpdateLeagueCalendar(Domain.Hattrick.WorldDetails.League league, ref LeagueCalendar dbLeagueCalendar)
        {
            dbLeagueCalendar.NextTrainingUpdate = league.TrainingDate;
            dbLeagueCalendar.NextEconomyUpdate = league.EconomyDate;
            dbLeagueCalendar.NextCupMatchDate = league.CupMatchDate;
            dbLeagueCalendar.NextSeriesMatchDate = league.SeriesMatchDate;
            dbLeagueCalendar.FirstWeeklyUpdate = league.Sequence1;
            dbLeagueCalendar.SecondWeeklyUpdate = league.Sequence2;
            dbLeagueCalendar.ThirdWeeklyUpdate = league.Sequence3;
            dbLeagueCalendar.FourthWeeklyUpdate = league.Sequence5;
            dbLeagueCalendar.FifthWeeklyUpdate = league.Sequence7;
        }

        private LeagueCup CreateLeagueCup(Domain.Hattrick.WorldDetails.Cup cup, int leagueId)
        {
            return new LeagueCup
            {
                LeagueId = leagueId,
                HattrickId = cup.CupId,
                Name = cup.CupName,
                LeagueLevel = cup.CupLeagueLevel,
                Level = cup.CupLevel,
                LevelIndex = cup.CupLevelIndex,
                CurrentRound = cup.MatchRound,
                RoundsLeft = cup.MatchRoundsLeft
            };
        }

        private void UpdateLeagueCup(Domain.Hattrick.WorldDetails.Cup cup, ref LeagueCup dbLeagueCup)
        {
            dbLeagueCup.Name = cup.CupName;
            dbLeagueCup.LeagueLevel = cup.CupLeagueLevel;
            dbLeagueCup.Level = cup.CupLevel;
            dbLeagueCup.LevelIndex = cup.CupLevelIndex;
            dbLeagueCup.CurrentRound = cup.MatchRound;
            dbLeagueCup.RoundsLeft = cup.MatchRoundsLeft;
        }

        private Country? CreateCountry(Domain.Hattrick.WorldDetails.Country country, int leagueId)
        {
            if (country == null ||
                !country.Available ||
                country.CountryId == null ||
                string.IsNullOrWhiteSpace(country.CountryName) ||
                string.IsNullOrWhiteSpace(country.CurrencyName) ||
                country.CurrencyRate == null ||
                string.IsNullOrWhiteSpace(country.CountryCode) ||
                string.IsNullOrWhiteSpace(country.DateFormat) ||
                string.IsNullOrWhiteSpace(country.TimeFormat))
            {
                return null;
            }

            return new Country
            {
                LeagueId = leagueId,
                HattrickId = country.CountryId.Value,
                Name = country.CountryName,
                CurrencyName = country.CurrencyName,
                CurrencyRate = country.CurrencyRate.Value,
                Code = country.CountryCode,
                DateFormat = country.DateFormat,
                TimeFormat = country.TimeFormat
            };
        }

        private void UpdateCountry(Domain.Hattrick.WorldDetails.Country country, ref Country dbCountry)
        {
            if (country == null ||
                !country.Available ||
                country.CountryId == null ||
                string.IsNullOrWhiteSpace(country.CountryName) ||
                string.IsNullOrWhiteSpace(country.CurrencyName) ||
                country.CurrencyRate == null ||
                string.IsNullOrWhiteSpace(country.CountryCode) ||
                string.IsNullOrWhiteSpace(country.DateFormat) ||
                string.IsNullOrWhiteSpace(country.TimeFormat))
            {
                return;
            }

            dbCountry.Name = country.CountryName;
            dbCountry.CurrencyName = country.CurrencyName;
            dbCountry.CurrencyRate = country.CurrencyRate.Value;
            dbCountry.Code = country.CountryCode;
            dbCountry.DateFormat = country.DateFormat;
            dbCountry.TimeFormat = country.TimeFormat;
        }

        private Region CreateRegion(Domain.Hattrick.WorldDetails.Region region, int countryId)
        {
            var newRegion = new Region
            {
                CountryId = countryId,
                HattrickId = region.RegionId,
                Name = region.RegionName
            };

            return newRegion;
        }

        private void UpdateRegion(Domain.Hattrick.WorldDetails.Region region, ref Region dbRegion)
        {
            dbRegion.Name = region.RegionName;
        }
    }
}
