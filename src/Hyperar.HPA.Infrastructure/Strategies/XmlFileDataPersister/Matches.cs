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

        private readonly IHattrickRepository<Domain.TeamOverviewMatch> teamOverviewMatchRepository;

        private readonly IHattrickRepository<Domain.Team> teamRepository;

        public Matches(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Team> teamRepository,
            IHattrickRepository<Domain.TeamOverviewMatch> teamOverviewMatchRepository)
        {
            this.databaseContext = databaseContext;
            this.teamRepository = teamRepository;
            this.teamOverviewMatchRepository = teamOverviewMatchRepository;
        }

        public async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData xmlEntity)
                {
                    await this.ProcessMatchesAsync(xmlEntity);
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

        private async Task ProcessMatchAsync(Hattrick.Match xmlMatch, Domain.Team team)
        {
            var storedMatch = await this.teamOverviewMatchRepository.GetByHattrickIdAsync(xmlMatch.MatchId);

            if (storedMatch == null)
            {
                storedMatch = new Domain.TeamOverviewMatch
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
                    Team = team
                };

                await this.teamOverviewMatchRepository.InsertAsync(storedMatch);
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

                this.teamOverviewMatchRepository.Update(storedMatch);
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessMatchesAsync(Hattrick.HattrickData xmlEntity)
        {
            var team = await this.teamRepository.GetByHattrickIdAsync(xmlEntity.Team.TeamId);

            ArgumentNullException.ThrowIfNull(team, nameof(team));

            List<uint> xmlMatchesIds = xmlEntity.Team.MatchList.Select(x => x.MatchId).ToList();

            var teamOverviewMatchesToDelete = await this.teamOverviewMatchRepository.Query(x => x.Team.HattrickId == team.HattrickId
                                                                                             && !xmlMatchesIds.Contains(x.HattrickId)).ToListAsync();

            foreach (var curTeamOverviewMatch in teamOverviewMatchesToDelete)
            {
                await this.teamOverviewMatchRepository.DeleteAsync(curTeamOverviewMatch.HattrickId);
            }

            foreach (var curXmlMatch in xmlEntity.Team.MatchList)
            {
                await this.ProcessMatchAsync(curXmlMatch, team);
            }

            await this.databaseContext.SaveAsync();
        }
    }
}