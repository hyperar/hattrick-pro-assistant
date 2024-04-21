namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Hyperar.HPA.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class MatchLineUp : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpPlayer> matchTeamLineUpPlayerRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUp> matchTeamLineUpRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpStartingPlayer> matchTeamLineUpStartingPlayerRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpSubstitution> matchTeamLineUpSubstitutionRepository;

        private readonly IRepository<Domain.Senior.MatchTeam> matchTeamRepository;

        public MatchLineUp(
            IDatabaseContext databaseContext,
            IRepository<Domain.Senior.MatchTeamLineUpPlayer> matchTeamLineUpPlayerRepository,
            IRepository<Domain.Senior.MatchTeamLineUp> matchTeamLineUpRepository,
            IRepository<Domain.Senior.MatchTeamLineUpStartingPlayer> matchTeamLineUpStartingPlayerRepository,
            IRepository<Domain.Senior.MatchTeamLineUpSubstitution> matchTeamLineUpSubstitutionRepository,
            IRepository<Domain.Senior.MatchTeam> matchTeamRepository)
        {
            this.databaseContext = databaseContext;
            this.matchTeamLineUpPlayerRepository = matchTeamLineUpPlayerRepository;
            this.matchTeamLineUpRepository = matchTeamLineUpRepository;
            this.matchTeamLineUpStartingPlayerRepository = matchTeamLineUpStartingPlayerRepository;
            this.matchTeamLineUpSubstitutionRepository = matchTeamLineUpSubstitutionRepository;
            this.matchTeamRepository = matchTeamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.MatchLineUp.HattrickData file)
            {
                ArgumentNullException.ThrowIfNull(fileDownloadTask.ContextId);

                var matchTeam = await this.matchTeamRepository.Query(x => x.MatchHattrickId == file.MatchId
                                                                       && x.HattrickId == file.Team.TeamId)
                                                              .SingleOrDefaultAsync();

                ArgumentNullException.ThrowIfNull(matchTeam, nameof(matchTeam));

                var matchTeamLineUp = await this.ProcessMatchLineUpAsync(file.Team, matchTeam);

                foreach (var xmlPlayer in file.Team.StartingLineUp)
                {
                    await this.ProcessMatchTeamLineUpStartingPlayer(xmlPlayer, matchTeamLineUp);
                }

                if (file.Team.Substitutions != null)
                {
                    foreach (var xmlPlayer in file.Team.Substitutions)
                    {
                        await this.ProcessMatchTeamLineUpSubstitution(xmlPlayer, matchTeamLineUp);
                    }
                }

                foreach (var xmlPlayer in file.Team.LineUp)
                {
                    await this.ProcessMatchTeamLineUpPlayer(xmlPlayer, matchTeamLineUp);
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

        private async Task ProcessMatchTeamLineUpPlayer(Models.MatchLineUp.Player xmlPlayer, Domain.Senior.MatchTeamLineUp matchTeamLineUp)
        {
            var matchLineUpPlayer = await this.matchTeamLineUpPlayerRepository.Query(x => x.MatchTeamLineUpId == matchTeamLineUp.Id
                                                                                       && x.HattrickId == xmlPlayer.PlayerId)
                                                                              .SingleOrDefaultAsync();

            if (matchLineUpPlayer == null)
            {
                await this.matchTeamLineUpPlayerRepository.InsertAsync(
                    Domain.Senior.MatchTeamLineUpPlayer.Create(
                        xmlPlayer,
                        matchTeamLineUp));
            }
        }

        private async Task ProcessMatchTeamLineUpStartingPlayer(Models.MatchLineUp.StartingPlayer xmlStartingPlayer, Domain.Senior.MatchTeamLineUp matchTeamLineUp)
        {
            var matchLineUpstartingPlayer = await this.matchTeamLineUpStartingPlayerRepository.Query(x => x.MatchTeamLineUpId == matchTeamLineUp.Id
                                                                                                       && x.HattrickId == xmlStartingPlayer.PlayerId)
                                                                                              .SingleOrDefaultAsync();

            if (matchLineUpstartingPlayer == null)
            {
                await this.matchTeamLineUpStartingPlayerRepository.InsertAsync(
                    Domain.Senior.MatchTeamLineUpStartingPlayer.Create(
                        xmlStartingPlayer,
                        matchTeamLineUp));
            }
        }

        private async Task ProcessMatchTeamLineUpSubstitution(Models.MatchLineUp.Substitution xmlSubstitution, Domain.Senior.MatchTeamLineUp matchTeamLineUp)
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
    }
}