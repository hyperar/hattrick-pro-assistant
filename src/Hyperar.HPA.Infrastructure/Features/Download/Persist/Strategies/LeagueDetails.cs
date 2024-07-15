namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick;

    public class LeagueDetails : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private readonly IAuditableRepository<Domain.Senior.Series> seniorSeriesRepository;

        private readonly IAuditableRepository<Domain.Senior.SeriesTeam> seniorSeriesTeamRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> seniorTeamRepository;

        public LeagueDetails(
            IAuditableRepository<Domain.Senior.Series> seniorSeriesRepository,
            IAuditableRepository<Domain.Senior.SeriesTeam> seniorSeriesTeamRepository,
            IHattrickRepository<Domain.Senior.Team> seniorTeamRepository)
        {
            this.seniorSeriesRepository = seniorSeriesRepository;
            this.seniorSeriesTeamRepository = seniorSeriesTeamRepository;
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.LeagueDetails.HattrickData file)
            {
                ArgumentNullException.ThrowIfNull(task.ContextId, nameof(task.ContextId));

                var seniorTeam = await this.seniorTeamRepository.GetByHattrickIdAsync(task.ContextId.Value);

                ArgumentNullException.ThrowIfNull(seniorTeam, nameof(seniorTeam));

                var seniorSeries = await this.ProcessSeriesAsync(file, seniorTeam, cancellationToken);

                foreach (var xmlTeam in file.Teams)
                {
                    await this.ProcessTeamAsync(xmlTeam, seniorSeries, cancellationToken);
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task<Domain.Senior.Series> ProcessSeriesAsync(
            Models.LeagueDetails.HattrickData xmlSeries,
            Domain.Senior.Team seniorTeam,
            CancellationToken cancellationToken)
        {
            var seniorSeries = await this.seniorSeriesRepository.Query(x => x.SeriesHattrickId == xmlSeries.LeagueLevelUnitId
                                                                         && x.TeamHattrickId == seniorTeam.HattrickId
                                                                         && x.Season == seniorTeam.League.Season)
                .SingleOrDefaultAsync(cancellationToken);

            if (seniorSeries == null)
            {
                seniorSeries = await this.seniorSeriesRepository.InsertAsync(
                    new Domain.Senior.Series
                    {
                        SeriesHattrickId = xmlSeries.LeagueLevelUnitId,
                        TeamHattrickId = seniorTeam.HattrickId,
                        Name = xmlSeries.LeagueLevelUnitName,
                        Level = xmlSeries.LeagueLevel,
                        Rank = xmlSeries.Rank,
                        Season = seniorTeam.League.Season,
                        Team = seniorTeam
                    });
            }
            else
            {
                seniorSeries.Rank = xmlSeries.Rank;

                this.seniorSeriesRepository.Update(seniorSeries);
            }

            return seniorSeries;
        }

        private async Task ProcessTeamAsync(
            Models.LeagueDetails.Team xmlTeam,
            Domain.Senior.Series seniorSeries,
            CancellationToken cancellationToken)
        {
            var seniorSeriesTeam = await this.seniorSeriesTeamRepository.Query(x => x.HattrickId == xmlTeam.TeamId
                                                                                 && x.SeriesHattrickId == seniorSeries.SeriesHattrickId
                                                                                 && x.TeamHattrickId == seniorSeries.TeamHattrickId)
                .SingleOrDefaultAsync(cancellationToken);

            if (seniorSeriesTeam == null)
            {
                await this.seniorSeriesTeamRepository.InsertAsync(
                    new Domain.Senior.SeriesTeam
                    {
                        HattrickId = xmlTeam.TeamId,
                        Name = xmlTeam.TeamName,
                        Position = xmlTeam.Position,
                        Change = (SeriesPositionChange)xmlTeam.PositionChange,
                        GoalsFor = xmlTeam.GoalsFor,
                        GoalsAgainst = xmlTeam.GoalsAgainst,
                        Points = xmlTeam.Points,
                        Week = xmlTeam.Matches,
                        WonMatches = xmlTeam.Won,
                        DrawnMatches = xmlTeam.Draws,
                        LostMatches = xmlTeam.Lost,
                        Series = seniorSeries
                    });
            }
            else
            {
                seniorSeriesTeam.Name = xmlTeam.TeamName;
                seniorSeriesTeam.Position = xmlTeam.Position;
                seniorSeriesTeam.Change = (SeriesPositionChange)xmlTeam.PositionChange;
                seniorSeriesTeam.GoalsFor = xmlTeam.GoalsFor;
                seniorSeriesTeam.GoalsAgainst = xmlTeam.GoalsAgainst;
                seniorSeriesTeam.Points = xmlTeam.Points;
                seniorSeriesTeam.Week = xmlTeam.Matches;
                seniorSeriesTeam.WonMatches = xmlTeam.Won;
                seniorSeriesTeam.DrawnMatches = xmlTeam.Draws;
                seniorSeriesTeam.LostMatches = xmlTeam.Lost;

                this.seniorSeriesTeamRepository.Update(seniorSeriesTeam);
            }
        }
    }
}