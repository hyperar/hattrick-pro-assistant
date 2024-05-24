namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick;

    public class MatchLineUp : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpPlayer> matchTeamLineUpPlayerRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUp> matchTeamLineUpRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpStartingPlayer> matchTeamLineUpStartingPlayerRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpSubstitution> matchTeamLineUpSubstitutionRepository;

        private readonly IRepository<Domain.Senior.MatchTeam> matchTeamRepository;

        private readonly IRepository<Domain.Senior.PlayerMatch> playerMatchRepository;

        private readonly IHattrickRepository<Domain.Senior.Player> playerRepository;

        public MatchLineUp(
            IDatabaseContext databaseContext,
            IRepository<Domain.Senior.MatchTeamLineUpPlayer> matchTeamLineUpPlayerRepository,
            IRepository<Domain.Senior.MatchTeamLineUp> matchTeamLineUpRepository,
            IRepository<Domain.Senior.MatchTeamLineUpStartingPlayer> matchTeamLineUpStartingPlayerRepository,
            IRepository<Domain.Senior.MatchTeamLineUpSubstitution> matchTeamLineUpSubstitutionRepository,
            IRepository<Domain.Senior.MatchTeam> matchTeamRepository,
            IHattrickRepository<Domain.Senior.Player> playerRepository,
            IRepository<Domain.Senior.PlayerMatch> playerMatchRepository)
        {
            this.databaseContext = databaseContext;
            this.matchTeamLineUpPlayerRepository = matchTeamLineUpPlayerRepository;
            this.matchTeamLineUpRepository = matchTeamLineUpRepository;
            this.matchTeamLineUpStartingPlayerRepository = matchTeamLineUpStartingPlayerRepository;
            this.matchTeamLineUpSubstitutionRepository = matchTeamLineUpSubstitutionRepository;
            this.matchTeamRepository = matchTeamRepository;
            this.playerRepository = playerRepository;
            this.playerMatchRepository = playerMatchRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.MatchLineUp.HattrickData file)
            {
                ArgumentNullException.ThrowIfNull(fileDownloadTask.ContextId);

                var matchTeam = await this.matchTeamRepository.Query(x => x.MatchHattrickId == file.MatchId
                                                                       && x.HattrickId == file.Team.TeamId)
                                                              .SingleOrDefaultAsync(cancellationToken);

                ArgumentNullException.ThrowIfNull(matchTeam, nameof(matchTeam));

                var matchTeamLineUp = await this.ProcessMatchLineUpAsync(file.Team, matchTeam);

                foreach (var xmlPlayer in file.Team.StartingLineUp)
                {
                    await this.ProcessMatchTeamLineUpStartingPlayerAsync(xmlPlayer, matchTeamLineUp);
                }

                if (file.Team.Substitutions != null)
                {
                    foreach (var xmlPlayer in file.Team.Substitutions)
                    {
                        await this.ProcessMatchTeamLineUpSubstitutionAsync(xmlPlayer, matchTeamLineUp);
                    }
                }

                foreach (var xmlPlayer in file.Team.LineUp)
                {
                    await this.ProcessMatchTeamLineUpPlayerAsync(xmlPlayer, matchTeamLineUp);

                    if (matchTeam.HattrickId == file.Team.TeamId)
                    {
                        await this.ProcessPlayerMatchAsync(
                            xmlPlayer,
                            matchTeamLineUp.MatchTeam.MatchHattrickId,
                            matchTeamLineUp.MatchTeam.Match.StartDate);
                    }
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.MatchLineUp.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task<Domain.Senior.MatchTeamLineUp> ProcessMatchLineUpAsync(Models.MatchLineUp.Team xmlTeam, Domain.Senior.MatchTeam matchTeam)
        {
            var lineUp = await this.matchTeamLineUpRepository.Query(x => x.MatchTeamId == matchTeam.Id)
                                                             .SingleOrDefaultAsync();

            lineUp ??= await this.matchTeamLineUpRepository.InsertAsync(
                    Domain.Senior.MatchTeamLineUp.Create(
                        xmlTeam,
                        matchTeam));

            return lineUp;
        }

        private async Task ProcessMatchTeamLineUpPlayerAsync(Models.MatchLineUp.Player xmlPlayer, Domain.Senior.MatchTeamLineUp matchTeamLineUp)
        {
            var matchLineUpPlayer = await this.matchTeamLineUpPlayerRepository.Query(x => x.MatchTeamLineUpId == matchTeamLineUp.Id
                                                                                       && x.HattrickId == xmlPlayer.PlayerId
                                                                                       && x.Role == (MatchRole)xmlPlayer.RoleId)
                                                                              .SingleOrDefaultAsync();

            if (matchLineUpPlayer == null)
            {
                await this.matchTeamLineUpPlayerRepository.InsertAsync(
                    Domain.Senior.MatchTeamLineUpPlayer.Create(
                        xmlPlayer,
                        matchTeamLineUp));
            }
        }

        private async Task ProcessMatchTeamLineUpStartingPlayerAsync(Models.MatchLineUp.StartingPlayer xmlStartingPlayer, Domain.Senior.MatchTeamLineUp matchTeamLineUp)
        {
            var matchLineUpstartingPlayer = await this.matchTeamLineUpStartingPlayerRepository.Query(x => x.MatchTeamLineUpId == matchTeamLineUp.Id
                                                                                                       && x.HattrickId == xmlStartingPlayer.PlayerId
                                                                                                       && x.Role == (MatchRole)xmlStartingPlayer.RoleId)
                                                                                              .SingleOrDefaultAsync();

            if (matchLineUpstartingPlayer == null)
            {
                await this.matchTeamLineUpStartingPlayerRepository.InsertAsync(
                    Domain.Senior.MatchTeamLineUpStartingPlayer.Create(
                        xmlStartingPlayer,
                        matchTeamLineUp));
            }
        }

        private async Task ProcessMatchTeamLineUpSubstitutionAsync(Models.MatchLineUp.Substitution xmlSubstitution, Domain.Senior.MatchTeamLineUp matchTeamLineUp)
        {
            var matchLineUpSubstitution = await this.matchTeamLineUpSubstitutionRepository.Query(x => x.MatchTeamLineUpId == matchTeamLineUp.Id
                                                                                                   && x.InPlayerHattrickId == xmlSubstitution.ObjectPlayerId
                                                                                                   && x.OutPlayerHattrickId == xmlSubstitution.SubjectPlayerId)
                                                                                              .SingleOrDefaultAsync();

            if (matchLineUpSubstitution == null)
            {
                await this.matchTeamLineUpSubstitutionRepository.InsertAsync(
                    Domain.Senior.MatchTeamLineUpSubstitution.Create(
                        xmlSubstitution,
                        matchTeamLineUp));
            }
        }

        private async Task ProcessPlayerMatchAsync(
            Models.MatchLineUp.Player xmlPlayer,
            long matchId,
            DateTime matchDate)
        {
            if (xmlPlayer.RatingStars == null || xmlPlayer.RatingStarsEndOfMatch == null)
            {
                return;
            }

            var player = await this.playerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

            // Player no longer on team, discard.
            if (player == null)
            {
                return;
            }

            var playerMatch = await this.playerMatchRepository.Query(x => x.PlayerHattrickId == xmlPlayer.PlayerId
                                                                       && x.MatchHattrickId == matchId)
                                                              .SingleOrDefaultAsync();

            if (playerMatch == null)
            {
                string calculatedRating = CalculateRating(xmlPlayer.RatingStars.Value, xmlPlayer.RatingStarsEndOfMatch.Value);

                await this.playerMatchRepository.InsertAsync(
                    Domain.Senior.PlayerMatch.Create(
                        xmlPlayer,
                        player,
                        matchId,
                        matchDate,
                        calculatedRating));
            }
        }
    }
}