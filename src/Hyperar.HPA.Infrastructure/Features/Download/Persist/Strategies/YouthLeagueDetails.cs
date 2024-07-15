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

    public class YouthLeagueDetails : DownloadTaskStrategyBase, IPersisterStrategy
    {
        private readonly IAuditableRepository<Domain.Junior.Series> juniorSeriesRepository;

        private readonly IAuditableRepository<Domain.Junior.SeriesTeam> juniorSeriesTeamRepository;

        private readonly IHattrickRepository<Domain.Junior.Team> juniorTeamRepository;

        public YouthLeagueDetails(
            IAuditableRepository<Domain.Junior.Series> juniorSeriesRepository,
            IAuditableRepository<Domain.Junior.SeriesTeam> juniorSeriesTeamRepository,
            IHattrickRepository<Domain.Junior.Team> juniorTeamRepository)
        {
            this.juniorSeriesRepository = juniorSeriesRepository;
            this.juniorSeriesTeamRepository = juniorSeriesTeamRepository;
            this.juniorTeamRepository = juniorTeamRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            if (task.XmlFile is Models.YouthLeagueDetails.HattrickData file)
            {
                ArgumentNullException.ThrowIfNull(task.ContextId, nameof(task.ContextId));

                var juniorTeam = await this.juniorTeamRepository.GetByHattrickIdAsync(task.ContextId.Value);

                ArgumentNullException.ThrowIfNull(juniorTeam, nameof(juniorTeam));

                var juniorSeries = await this.ProcessSeriesAsync(file, juniorTeam, cancellationToken);

                foreach (var xmlTeam in file.Teams)
                {
                    await this.ProcessTeamAsync(xmlTeam, juniorSeries, cancellationToken);
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task<Domain.Junior.Series> ProcessSeriesAsync(
            Models.YouthLeagueDetails.HattrickData xmlSeries,
            Domain.Junior.Team juniorTeam,
            CancellationToken cancellationToken)
        {
            var juniorSeries = await this.juniorSeriesRepository.Query(x => x.SeriesHattrickId == xmlSeries.YouthLeagueId
                                                                         && x.Season == xmlSeries.Season)
                .SingleOrDefaultAsync(cancellationToken);

            if (juniorSeries == null)
            {
                juniorSeries = await this.juniorSeriesRepository.InsertAsync(
                    new Domain.Junior.Series
                    {
                        SeriesHattrickId = xmlSeries.YouthLeagueId,
                        Name = xmlSeries.YouthLeagueName,
                        Type = (YouthLeagueType)xmlSeries.YouthLeagueType,
                        Season = xmlSeries.Season,
                        Team = juniorTeam
                    });
            }

            return juniorSeries;
        }

        private async Task ProcessTeamAsync(
            Models.YouthLeagueDetails.Team xmlTeam,
            Domain.Junior.Series juniorSeries,
            CancellationToken cancellationToken)
        {
            var juniorSeriesTeam = await this.juniorSeriesTeamRepository.Query(x => x.HattrickId == xmlTeam.TeamId
                                                                                 && x.SeriesHattrickId == juniorSeries.SeriesHattrickId
                                                                                 && x.TeamHattrickId == juniorSeries.TeamHattrickId)
                .SingleOrDefaultAsync(cancellationToken);

            if (juniorSeriesTeam == null)
            {
                await this.juniorSeriesTeamRepository.InsertAsync(
                    new Domain.Junior.SeriesTeam
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
                        Series = juniorSeries
                    });
            }
            else
            {
                juniorSeriesTeam.Name = xmlTeam.TeamName;
                juniorSeriesTeam.Position = xmlTeam.Position;
                juniorSeriesTeam.Change = (SeriesPositionChange)xmlTeam.PositionChange;
                juniorSeriesTeam.GoalsFor = xmlTeam.GoalsFor;
                juniorSeriesTeam.GoalsAgainst = xmlTeam.GoalsAgainst;
                juniorSeriesTeam.Points = xmlTeam.Points;
                juniorSeriesTeam.Week = xmlTeam.Matches;
                juniorSeriesTeam.WonMatches = xmlTeam.Won;
                juniorSeriesTeam.DrawnMatches = xmlTeam.Draws;
                juniorSeriesTeam.LostMatches = xmlTeam.Lost;

                this.juniorSeriesTeamRepository.Update(juniorSeriesTeam);
            }
        }
    }
}