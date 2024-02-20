namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using System.Threading.Tasks;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Hattrick = Application.Hattrick.MatchLineUp;

    public class MatchLineUp : XmlFileDataPersisterBase, IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpPlayer> lineUpPlayerRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUp> lineUpRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpStartingPlayer> lineUpStartingPlayerRepository;

        private readonly IRepository<Domain.Senior.MatchTeamLineUpSubstitution> lineUpSubstitutionRepository;

        private readonly IRepository<Domain.Senior.MatchTeam> matchTeamRepository;

        public MatchLineUp(
            IDatabaseContext databaseContext,
            IRepository<Domain.Senior.MatchTeamLineUpPlayer> lineUpPlayerRepository,
            IRepository<Domain.Senior.MatchTeamLineUp> lineUpRepository,
            IRepository<Domain.Senior.MatchTeamLineUpStartingPlayer> lineUpStartingPlayerRepository,
            IRepository<Domain.Senior.MatchTeamLineUpSubstitution> lineUpSubstitutionRepository,
            IRepository<Domain.Senior.MatchTeam> matchTeamRepository)
        {
            this.databaseContext = databaseContext;
            this.lineUpPlayerRepository = lineUpPlayerRepository;
            this.lineUpRepository = lineUpRepository;
            this.lineUpStartingPlayerRepository = lineUpStartingPlayerRepository;
            this.lineUpSubstitutionRepository = lineUpSubstitutionRepository;
            this.matchTeamRepository = matchTeamRepository;
        }

        public override async Task PersistDataAsync(IXmlFile file)
        {
            if (file is Hattrick.HattrickData xmlEntity)
            {
                var matchTeam = await this.matchTeamRepository.Query(x => x.Match.HattrickId == xmlEntity.MatchId
                                                                       && x.HattrickId == xmlEntity.Team.TeamId)
                                                              .SingleOrDefaultAsync();

                ArgumentNullException.ThrowIfNull(matchTeam, nameof(matchTeam));

                await this.PersistMatchLineUp(xmlEntity.Team, matchTeam);

                await this.databaseContext.SaveAsync();
            }
            else
            {
                throw new ArgumentException(file.GetType().FullName, nameof(file));
            }
        }

        private async Task PersistMatchLineUp(Hattrick.Team xmlTeam, Domain.Senior.MatchTeam matchTeam)
        {
            var lineUp = await this.lineUpRepository.Query(x => x.Team.HattrickId == xmlTeam.TeamId
                                                             && x.Team.Match.HattrickId == matchTeam.Match.HattrickId)
                                                    .SingleOrDefaultAsync();

            lineUp ??= await this.lineUpRepository.InsertAsync(
                new Domain.Senior.MatchTeamLineUp
                {
                    Experience = xmlTeam.ExperienceLevel,
                    Style = xmlTeam.StyleOfPlay,
                    Team = matchTeam
                });

            await this.databaseContext.SaveAsync();

            foreach (var xmlStartingPlayer in xmlTeam.StartingLineUp)
            {
                await this.PersistStartingPlayerAsync(xmlStartingPlayer, lineUp);
            }

            foreach (var xmlPlayer in xmlTeam.LineUp)
            {
                await this.PersistPlayerAsync(xmlPlayer, lineUp);
            }

            foreach (var xmlSubstitution in xmlTeam.Substitutions)
            {
                await this.PersistSubstitutionAsync(xmlSubstitution, lineUp);
            }
        }

        private async Task PersistPlayerAsync(Hattrick.Player xmlPlayer, Domain.Senior.MatchTeamLineUp lineUp)
        {
            var player = await this.lineUpPlayerRepository.Query(x => x.HattrickId == xmlPlayer.PlayerId
                                                                   && x.Role == xmlPlayer.RoleId
                                                                   && x.LineUp.Id == lineUp.Id)
                                                          .SingleOrDefaultAsync();

            player ??= await this.lineUpPlayerRepository.InsertAsync(
                new Domain.Senior.MatchTeamLineUpPlayer
                {
                    HattrickId = xmlPlayer.PlayerId,
                    FirstName = xmlPlayer.FirstName,
                    NickName = !string.IsNullOrWhiteSpace(xmlPlayer.NickName) ? xmlPlayer.NickName : null,
                    LastName = xmlPlayer.LastName,
                    Role = xmlPlayer.RoleId,
                    Behavior = xmlPlayer.Behaviour,
                    Rating = xmlPlayer.RatingStars,
                    EndRating = xmlPlayer.RatingStarsEndOfMatch,
                    LineUp = lineUp
                });
        }

        private async Task PersistStartingPlayerAsync(Hattrick.StartingPlayer xmlStartingPlayer, Domain.Senior.MatchTeamLineUp lineUp)
        {
            var startingPlayer = await this.lineUpStartingPlayerRepository.Query(x => x.HattrickId == xmlStartingPlayer.PlayerId
                                                                                   && x.Role == xmlStartingPlayer.RoleId
                                                                                   && x.LineUp.Id == lineUp.Id)
                                                                          .SingleOrDefaultAsync();

            startingPlayer ??= await this.lineUpStartingPlayerRepository.InsertAsync(
                new Domain.Senior.MatchTeamLineUpStartingPlayer
                {
                    HattrickId = xmlStartingPlayer.PlayerId,
                    FirstName = xmlStartingPlayer.FirstName,
                    NickName = !string.IsNullOrWhiteSpace(xmlStartingPlayer.NickName) ? xmlStartingPlayer.NickName : null,
                    LastName = xmlStartingPlayer.LastName,
                    Role = xmlStartingPlayer.RoleId,
                    Behavior = xmlStartingPlayer.Behavior,
                    LineUp = lineUp
                });
        }

        private async Task PersistSubstitutionAsync(Hattrick.Substitution xmlSubstitution, Domain.Senior.MatchTeamLineUp lineUp)
        {
            var substitution = await this.lineUpSubstitutionRepository.Query(x => x.LineUp.Id == lineUp.Id)
                                                                      .SingleOrDefaultAsync();

            substitution ??= await this.lineUpSubstitutionRepository.InsertAsync(
                new Domain.Senior.MatchTeamLineUpSubstitution
                {
                    OrderType = xmlSubstitution.OrderType,
                    NewRole = xmlSubstitution.NewPositionId,
                    NewRoleBehavior = xmlSubstitution.NewPositionBehavior,
                    Minute = xmlSubstitution.MatchMinute,
                    MatchPart = xmlSubstitution.MatchPart,
                    InPlayerHattrickId = xmlSubstitution.ObjectPlayerId,
                    OutPlayerHattrickId = xmlSubstitution.SubjectPlayerId,
                    LineUp = lineUp
                });
        }
    }
}