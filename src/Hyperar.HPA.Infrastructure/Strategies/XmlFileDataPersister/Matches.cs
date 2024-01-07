namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Hattrick = Application.Hattrick.Matches;

    public class Matches : IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.SeniorTeamOverviewMatch> seniorTeamOverviewMatchRepository;

        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public Matches(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository,
            IHattrickRepository<Domain.SeniorTeamOverviewMatch> seniorTeamOverviewMatchRepository)
        {
            this.databaseContext = databaseContext;
            this.seniorTeamRepository = seniorTeamRepository;
            this.seniorTeamOverviewMatchRepository = seniorTeamOverviewMatchRepository;
        }

        public async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData entity)
                {
                    await this.ProcessMatchesAsync(entity);
                }
                else
                {
                    throw new ArgumentException(file.GetType().FullName, nameof(file));
                }
            }
            catch
            {
                this.databaseContext.Cancel();

                throw;
            }
        }

        private async Task ProcessMatchAsync(Hattrick.Match xmlMatch, Domain.SeniorTeam seniorTeam)
        {
            var storedMatch = await this.seniorTeamOverviewMatchRepository.GetByHattrickIdAsync(xmlMatch.MatchId);

            if (storedMatch == null)
            {
                storedMatch = new Domain.SeniorTeamOverviewMatch
                {
                    HattrickId = xmlMatch.MatchId,
                    HomeTeamHattrickId = xmlMatch.HomeTeam.HomeTeamId,
                    HomeTeamName = xmlMatch.HomeTeam.HomeTeamName,
                    HomeTeamShortName = xmlMatch.HomeTeam.HomeTeamShortName,
                    HomeGoals = xmlMatch.HomeGoals,
                    AwayTeamHattrickId = xmlMatch.AwayTeam.AwayTeamId,
                    AwayTeamName = xmlMatch.AwayTeam.AwayTeamName,
                    AwayTeamShortName = xmlMatch.AwayTeam.AwayTeamShortName,
                    AwayGoals = xmlMatch.AwayGoals,
                    StartsOn = xmlMatch.MatchDate,
                    Type = xmlMatch.MatchType,
                    CompetitionId = xmlMatch.MatchContextId > 0 ? xmlMatch.MatchContextId : null,
                    Status = xmlMatch.Status,
                    SeniorTeam = seniorTeam
                };

                await this.seniorTeamOverviewMatchRepository.InsertAsync(storedMatch);
            }
            else
            {
                storedMatch.HomeTeamName = xmlMatch.HomeTeam.HomeTeamName;
                storedMatch.HomeTeamShortName = xmlMatch.HomeTeam.HomeTeamShortName;
                storedMatch.HomeGoals = xmlMatch.HomeGoals;
                storedMatch.AwayTeamName = xmlMatch.AwayTeam.AwayTeamName;
                storedMatch.AwayTeamShortName = xmlMatch.AwayTeam.AwayTeamShortName;
                storedMatch.AwayGoals = xmlMatch.AwayGoals;
                storedMatch.StartsOn = xmlMatch.MatchDate;
                storedMatch.Status = xmlMatch.Status;

                this.seniorTeamOverviewMatchRepository.Update(storedMatch);
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessMatchesAsync(Hattrick.HattrickData entity)
        {
            var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(entity.Team.TeamId);

            ArgumentNullException.ThrowIfNull(seniorTeam, nameof(seniorTeam));

            List<uint> xmlMatchesIds = entity.Team.MatchList.Select(x => x.MatchId).ToList();

            var seniorOverviewMatchesToDelete = await this.seniorTeamOverviewMatchRepository.Query(x => x.SeniorTeam.HattrickId == seniorTeam.HattrickId
                                                                                                     && !xmlMatchesIds.Contains(x.HattrickId)).ToListAsync();

            foreach (var curSeniorOverviewMatch in seniorOverviewMatchesToDelete)
            {
                await this.seniorTeamOverviewMatchRepository.DeleteAsync(curSeniorOverviewMatch.HattrickId);
            }

            foreach (var curXmlMatch in entity.Team.MatchList)
            {
                await this.ProcessMatchAsync(curXmlMatch, seniorTeam);
            }

            await this.databaseContext.SaveAsync();
        }
    }
}