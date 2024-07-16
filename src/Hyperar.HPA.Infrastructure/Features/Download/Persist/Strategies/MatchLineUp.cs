namespace Hyperar.HPA.Infrastructure.Features.Download.Persist.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Shared.Enums;
    using Shared.ExtensionMethods;
    using Models = Shared.Models.Hattrick;

    public class MatchLineUp : IPersisterStrategy
    {
        private readonly IAuditableRepository<Domain.Junior.MatchTeamLineUpPlayer> juniorMatchTeamLineUpPlayerRepository;

        private readonly IAuditableRepository<Domain.Junior.MatchTeamLineUp> juniorMatchTeamLineUpRepository;

        private readonly IAuditableRepository<Domain.Junior.MatchTeamLineUpStartingPlayer> juniorMatchTeamLineUpStartingPlayerRepository;

        private readonly IAuditableRepository<Domain.Junior.MatchTeamLineUpSubstitution> juniorMatchTeamLineUpSubstitutionRepository;

        private readonly IAuditableRepository<Domain.Junior.MatchTeam> juniorMatchTeamRepository;

        private readonly IAuditableRepository<Domain.Junior.PlayerMatch> juniorPlayerMatchRepository;

        private readonly IHattrickRepository<Domain.Junior.Player> juniorPlayerRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchTeamLineUpPlayer> seniorMatchTeamLineUpPlayerRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchTeamLineUp> seniorMatchTeamLineUpRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchTeamLineUpStartingPlayer> seniorMatchTeamLineUpStartingPlayerRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchTeamLineUpSubstitution> seniorMatchTeamLineUpSubstitutionRepository;

        private readonly IAuditableRepository<Domain.Senior.MatchTeam> seniorMatchTeamRepository;

        private readonly IAuditableRepository<Domain.Senior.PlayerMatch> seniorPlayerMatchRepository;

        private readonly IHattrickRepository<Domain.Senior.Player> seniorPlayerRepository;

        public MatchLineUp(
            IAuditableRepository<Domain.Junior.MatchTeamLineUpPlayer> juniorMatchTeamLineUpPlayerRepository,
            IAuditableRepository<Domain.Junior.MatchTeamLineUp> juniorMatchTeamLineUpRepository,
            IAuditableRepository<Domain.Junior.MatchTeamLineUpStartingPlayer> juniorMatchTeamLineUpStartingPlayerRepository,
            IAuditableRepository<Domain.Junior.MatchTeamLineUpSubstitution> juniorMatchTeamLineUpSubstitutionRepository,
            IAuditableRepository<Domain.Junior.MatchTeam> juniorMatchTeamRepository,
            IAuditableRepository<Domain.Junior.PlayerMatch> juniorPlayerMatchRepository,
            IHattrickRepository<Domain.Junior.Player> juniorPlayerRepository,
            IAuditableRepository<Domain.Senior.MatchTeamLineUpPlayer> seniorMatchTeamLineUpPlayerRepository,
            IAuditableRepository<Domain.Senior.MatchTeamLineUp> seniorMatchTeamLineUpRepository,
            IAuditableRepository<Domain.Senior.MatchTeamLineUpStartingPlayer> seniorMatchTeamLineUpStartingPlayerRepository,
            IAuditableRepository<Domain.Senior.MatchTeamLineUpSubstitution> seniorMatchTeamLineUpSubstitutionRepository,
            IAuditableRepository<Domain.Senior.MatchTeam> seniorMatchTeamRepository,
            IAuditableRepository<Domain.Senior.PlayerMatch> seniorPlayerMatchRepository,
            IHattrickRepository<Domain.Senior.Player> seniorPlayerRepository)
        {
            this.juniorMatchTeamLineUpPlayerRepository = juniorMatchTeamLineUpPlayerRepository;
            this.juniorMatchTeamLineUpRepository = juniorMatchTeamLineUpRepository;
            this.juniorMatchTeamLineUpStartingPlayerRepository = juniorMatchTeamLineUpStartingPlayerRepository;
            this.juniorMatchTeamLineUpSubstitutionRepository = juniorMatchTeamLineUpSubstitutionRepository;
            this.juniorMatchTeamRepository = juniorMatchTeamRepository;
            this.juniorPlayerMatchRepository = juniorPlayerMatchRepository;
            this.juniorPlayerRepository = juniorPlayerRepository;
            this.seniorMatchTeamLineUpPlayerRepository = seniorMatchTeamLineUpPlayerRepository;
            this.seniorMatchTeamLineUpRepository = seniorMatchTeamLineUpRepository;
            this.seniorMatchTeamLineUpStartingPlayerRepository = seniorMatchTeamLineUpStartingPlayerRepository;
            this.seniorMatchTeamLineUpSubstitutionRepository = seniorMatchTeamLineUpSubstitutionRepository;
            this.seniorMatchTeamRepository = seniorMatchTeamRepository;
            this.seniorPlayerMatchRepository = seniorPlayerMatchRepository;
            this.seniorPlayerRepository = seniorPlayerRepository;
        }

        public async Task PersistAsync(XmlDownloadTask task, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(task.ContextId, nameof(task.ContextId));

            if (task.XmlFile is Models.MatchLineUp.HattrickData file)
            {
                if (file.SourceSystem.ToMatchSystem() == MatchSystem.Youth)
                {
                    var juniorMatchTeam = await this.juniorMatchTeamRepository.Query(x => x.TeamHattrickId == file.Team.TeamId
                                                                                       && x.MatchHattrickId == file.MatchId)
                        .SingleOrDefaultAsync();

                    ArgumentNullException.ThrowIfNull(juniorMatchTeam, nameof(juniorMatchTeam));

                    var lineUp = await this.ProcessMatchTeamLineUpAsync(file.Team, juniorMatchTeam);

                    foreach (var xmlStartingPlayer in file.Team.StartingLineUp)
                    {
                        await this.ProcessMatchTeamLineUpStartingPlayerAsync(xmlStartingPlayer, lineUp);
                    }

                    bool isOwnTeam = task.ContextId.Value == file.Team.TeamId;

                    foreach (var xmlPlayer in file.Team.LineUp)
                    {
                        await this.ProcessMatchTeamLineUpPlayerAsync(xmlPlayer, lineUp, isOwnTeam);
                    }

                    foreach (var xmlSubstitution in file.Team.Substitutions)
                    {
                        await this.ProcessMatchTeamLineUpSubstitutionAsync(xmlSubstitution, lineUp);
                    }
                }
                else
                {
                    var seniorMatchTeam = await this.seniorMatchTeamRepository.Query(x => x.TeamHattrickId == file.Team.TeamId
                                                                                       && x.MatchHattrickId == file.MatchId)
                        .SingleOrDefaultAsync();

                    ArgumentNullException.ThrowIfNull(seniorMatchTeam, nameof(seniorMatchTeam));

                    var lineUp = await this.ProcessMatchTeamLineUpAsync(file.Team, seniorMatchTeam);

                    foreach (var xmlStartingPlayer in file.Team.StartingLineUp)
                    {
                        await this.ProcessMatchTeamLineUpStartingPlayerAsync(xmlStartingPlayer, lineUp);
                    }

                    bool isOwnTeam = task.ContextId.Value == file.Team.TeamId;

                    foreach (var xmlPlayer in file.Team.LineUp)
                    {
                        await this.ProcessMatchTeamLineUpPlayerAsync(xmlPlayer, lineUp, isOwnTeam);
                    }

                    foreach (var xmlSubstitution in file.Team.Substitutions)
                    {
                        await this.ProcessMatchTeamLineUpSubstitutionAsync(xmlSubstitution, lineUp);
                    }
                }
            }
            else
            {
                throw new ArgumentException(nameof(task.XmlFile));
            }
        }

        private async Task<Domain.Senior.MatchTeamLineUp> ProcessMatchTeamLineUpAsync(
            Models.MatchLineUp.Team xmlTeam,
            Domain.Senior.MatchTeam seniorMatchTeam)
        {
            var lineUp = await this.seniorMatchTeamLineUpRepository.Query(x => x.TeamHattrickId == xmlTeam.TeamId
                                                                            && x.MatchHattrickId == seniorMatchTeam.MatchHattrickId)
                .SingleOrDefaultAsync();

            if (lineUp == null)
            {
                lineUp = await this.seniorMatchTeamLineUpRepository.InsertAsync(
                    new Domain.Senior.MatchTeamLineUp
                    {
                        TeamHattrickId = xmlTeam.TeamId,
                        MatchHattrickId = seniorMatchTeam.MatchHattrickId,
                        Experience = (SkillLevel)xmlTeam.ExperienceLevel,
                        PlayStyle = xmlTeam.StyleOfPlay,
                        MatchTeam = seniorMatchTeam
                    });
            }

            return lineUp;
        }

        private async Task<Domain.Junior.MatchTeamLineUp> ProcessMatchTeamLineUpAsync(
            Models.MatchLineUp.Team xmlTeam,
            Domain.Junior.MatchTeam juniorMatchTeam)
        {
            var lineUp = await this.juniorMatchTeamLineUpRepository.Query(x => x.TeamHattrickId == xmlTeam.TeamId
                                                                            && x.MatchHattrickId == juniorMatchTeam.MatchHattrickId)
                .SingleOrDefaultAsync();

            if (lineUp == null)
            {
                lineUp = await this.juniorMatchTeamLineUpRepository.InsertAsync(
                    new Domain.Junior.MatchTeamLineUp
                    {
                        TeamHattrickId = juniorMatchTeam.TeamHattrickId,
                        MatchHattrickId = juniorMatchTeam.MatchHattrickId,
                        Experience = (SkillLevel)xmlTeam.ExperienceLevel,
                        PlayStyle = xmlTeam.StyleOfPlay,
                        MatchTeam = juniorMatchTeam
                    });
            }

            return lineUp;
        }

        private async Task ProcessMatchTeamLineUpPlayerAsync(
                    Models.MatchLineUp.Player xmlPlayer,
                    Domain.Senior.MatchTeamLineUp matchTeamLineUp,
                    bool isOwnTeam)
        {
            var player = await this.seniorMatchTeamLineUpPlayerRepository.Query(x => x.PlayerHattrickId == xmlPlayer.PlayerId
                                                                                  && x.TeamHattrickId == matchTeamLineUp.TeamHattrickId
                                                                                  && x.MatchHattrickId == matchTeamLineUp.MatchHattrickId
                                                                                  && x.Role == (MatchRole)xmlPlayer.RoleId)
                .SingleOrDefaultAsync();

            if (player == null)
            {
                await this.seniorMatchTeamLineUpPlayerRepository.InsertAsync(
                    new Domain.Senior.MatchTeamLineUpPlayer
                    {
                        PlayerHattrickId = xmlPlayer.PlayerId,
                        TeamHattrickId = matchTeamLineUp.TeamHattrickId,
                        MatchHattrickId = matchTeamLineUp.MatchHattrickId,
                        Role = (MatchRole)xmlPlayer.RoleId,
                        FirstName = xmlPlayer.FirstName,
                        NickName = string.IsNullOrWhiteSpace(xmlPlayer.NickName) ? null : xmlPlayer.NickName,
                        LastName = xmlPlayer.LastName,
                        Behavior = (MatchRoleBehavior?)xmlPlayer.Behaviour,
                        AverageRating = xmlPlayer.RatingStars,
                        EndOfMatchRating = xmlPlayer.RatingStarsEndOfMatch,
                        MatchTeamLineUp = matchTeamLineUp
                    });

                var seniorPlayer = await this.seniorPlayerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

                if (isOwnTeam && seniorPlayer != null && xmlPlayer.RatingStars is not null && xmlPlayer.RatingStarsEndOfMatch is not null)
                {
                    ArgumentNullException.ThrowIfNull(xmlPlayer.RatingStars, nameof(xmlPlayer.RatingStars));
                    ArgumentNullException.ThrowIfNull(xmlPlayer.RatingStarsEndOfMatch, nameof(xmlPlayer.RatingStarsEndOfMatch));

                    await this.seniorPlayerMatchRepository.InsertAsync(
                                new Domain.Senior.PlayerMatch
                                {
                                    PlayerHattrickId = xmlPlayer.PlayerId,
                                    MatchHattrickId = matchTeamLineUp.MatchTeam.Match.HattrickId,
                                    Date = matchTeamLineUp.MatchTeam.Match.Date,
                                    Role = (MatchRole)xmlPlayer.RoleId,
                                    AverageRating = xmlPlayer.RatingStars.Value,
                                    EndOfMatchRating = xmlPlayer.RatingStarsEndOfMatch.Value,
                                    Player = seniorPlayer,
                                    Match = matchTeamLineUp.MatchTeam.Match
                                });
                }
            }
        }

        private async Task ProcessMatchTeamLineUpPlayerAsync(
            Models.MatchLineUp.Player xmlPlayer,
            Domain.Junior.MatchTeamLineUp matchTeamLineUp,
            bool isOwnTeam)
        {
            var player = await this.juniorMatchTeamLineUpPlayerRepository.Query(x => x.PlayerHattrickId == xmlPlayer.PlayerId
                                                                                  && x.TeamHattrickId == matchTeamLineUp.TeamHattrickId
                                                                                  && x.MatchHattrickId == matchTeamLineUp.MatchHattrickId
                                                                                  && x.Role == (MatchRole)xmlPlayer.RoleId)
                .SingleOrDefaultAsync();

            if (player == null)
            {
                await this.juniorMatchTeamLineUpPlayerRepository.InsertAsync(
                    new Domain.Junior.MatchTeamLineUpPlayer
                    {
                        PlayerHattrickId = xmlPlayer.PlayerId,
                        TeamHattrickId = matchTeamLineUp.TeamHattrickId,
                        MatchHattrickId = matchTeamLineUp.MatchHattrickId,
                        FirstName = xmlPlayer.FirstName,
                        NickName = string.IsNullOrWhiteSpace(xmlPlayer.NickName) ? null : xmlPlayer.NickName,
                        LastName = xmlPlayer.LastName,
                        Role = (MatchRole)xmlPlayer.RoleId,
                        Behavior = (MatchRoleBehavior?)xmlPlayer.Behaviour,
                        AverageRating = xmlPlayer.RatingStars,
                        MatchTeamLineUp = matchTeamLineUp
                    });

                var juniorPlayer = await this.juniorPlayerRepository.GetByHattrickIdAsync(xmlPlayer.PlayerId);

                if (isOwnTeam && juniorPlayer != null && xmlPlayer.RatingStars is not null)
                {
                    ArgumentNullException.ThrowIfNull(xmlPlayer.RatingStars, nameof(xmlPlayer.RatingStars));

                    await this.juniorPlayerMatchRepository.InsertAsync(
                                new Domain.Junior.PlayerMatch
                                {
                                    PlayerHattrickId = xmlPlayer.PlayerId,
                                    MatchHattrickId = matchTeamLineUp.MatchTeam.Match.HattrickId,
                                    Date = matchTeamLineUp.MatchTeam.Match.Date,
                                    Role = (MatchRole)xmlPlayer.RoleId,
                                    AverageRating = xmlPlayer.RatingStars.Value,
                                    Player = juniorPlayer,
                                    Match = matchTeamLineUp.MatchTeam.Match
                                });
                }
            }
        }

        private async Task ProcessMatchTeamLineUpStartingPlayerAsync(
            Models.MatchLineUp.StartingPlayer xmlStartingPlayer,
            Domain.Senior.MatchTeamLineUp matchTeamLineUp)
        {
            var startingPlayer = await this.seniorMatchTeamLineUpStartingPlayerRepository.Query(x => x.PlayerHattrickId == xmlStartingPlayer.PlayerId
                                                                                                  && x.TeamHattrickId == matchTeamLineUp.TeamHattrickId
                                                                                                  && x.MatchHattrickId == matchTeamLineUp.MatchHattrickId
                                                                                                  && x.Role == (MatchRole)xmlStartingPlayer.RoleId)
                .SingleOrDefaultAsync();

            if (startingPlayer == null)
            {
                await this.seniorMatchTeamLineUpStartingPlayerRepository.InsertAsync(
                    new Domain.Senior.MatchTeamLineUpStartingPlayer
                    {
                        PlayerHattrickId = xmlStartingPlayer.PlayerId,
                        TeamHattrickId = matchTeamLineUp.TeamHattrickId,
                        MatchHattrickId = matchTeamLineUp.MatchHattrickId,
                        FirstName = xmlStartingPlayer.FirstName,
                        NickName = string.IsNullOrWhiteSpace(xmlStartingPlayer.NickName) ? null : xmlStartingPlayer.NickName,
                        LastName = xmlStartingPlayer.LastName,
                        Role = (MatchRole)xmlStartingPlayer.RoleId,
                        // In some cases, the Behavior is not informed on the XML file.
                        Behavior = xmlStartingPlayer.Behaviour != null ? (MatchRoleBehavior)xmlStartingPlayer.Behaviour : MatchRoleBehavior.Normal,
                        MatchTeamLineUp = matchTeamLineUp
                    });
            }
        }

        private async Task ProcessMatchTeamLineUpStartingPlayerAsync(
            Models.MatchLineUp.StartingPlayer xmlStartingPlayer,
            Domain.Junior.MatchTeamLineUp matchTeamLineUp)
        {
            var startingPlayer = await this.juniorMatchTeamLineUpStartingPlayerRepository.Query(x => x.PlayerHattrickId == xmlStartingPlayer.PlayerId
                                                                                                  && x.TeamHattrickId == matchTeamLineUp.TeamHattrickId
                                                                                                  && x.MatchHattrickId == matchTeamLineUp.MatchHattrickId
                                                                                                  && x.Role == (MatchRole)xmlStartingPlayer.RoleId)
                .SingleOrDefaultAsync();

            if (startingPlayer == null)
            {
                await this.juniorMatchTeamLineUpStartingPlayerRepository.InsertAsync(
                    new Domain.Junior.MatchTeamLineUpStartingPlayer
                    {
                        PlayerHattrickId = xmlStartingPlayer.PlayerId,
                        TeamHattrickId = matchTeamLineUp.TeamHattrickId,
                        MatchHattrickId = matchTeamLineUp.MatchHattrickId,
                        FirstName = xmlStartingPlayer.FirstName,
                        NickName = string.IsNullOrWhiteSpace(xmlStartingPlayer.NickName) ? null : xmlStartingPlayer.NickName,
                        LastName = xmlStartingPlayer.LastName,
                        Role = (MatchRole)xmlStartingPlayer.RoleId,
                        // In some cases, the Behavior is not informed on the XML file.
                        Behavior = xmlStartingPlayer.Behaviour != null ? (MatchRoleBehavior)xmlStartingPlayer.Behaviour : MatchRoleBehavior.Normal,
                        MatchTeamLineUp = matchTeamLineUp
                    });
            }
        }

        private async Task ProcessMatchTeamLineUpSubstitutionAsync(
            Models.MatchLineUp.Substitution xmlSubstitution,
            Domain.Senior.MatchTeamLineUp matchTeamLineUp)
        {
            var substitution = await this.seniorMatchTeamLineUpSubstitutionRepository.Query(x => x.OutPlayerHattrickId == xmlSubstitution.SubjectPlayerId
                                                                                              && x.InPlayerHattrickId == xmlSubstitution.ObjectPlayerId
                                                                                              && x.TeamHattrickId == matchTeamLineUp.TeamHattrickId
                                                                                              && x.MatchHattrickId == matchTeamLineUp.MatchHattrickId
                                                                                              && x.NewRole == (MatchRole)xmlSubstitution.NewPositionId
                                                                                              && x.NewBehavior == (MatchRoleBehavior)xmlSubstitution.NewPositionBehaviour)
                .SingleOrDefaultAsync();

            if (substitution == null)
            {
                await this.seniorMatchTeamLineUpSubstitutionRepository.InsertAsync(
                    new Domain.Senior.MatchTeamLineUpSubstitution
                    {
                        Minute = xmlSubstitution.MatchMinute,
                        MatchPart = (MatchPart)xmlSubstitution.MatchPart,
                        OutPlayerHattrickId = xmlSubstitution.SubjectPlayerId,
                        InPlayerHattrickId = xmlSubstitution.ObjectPlayerId,
                        Type = (MatchOrderType)xmlSubstitution.OrderType,
                        NewRole = (MatchRole)xmlSubstitution.NewPositionId,
                        NewBehavior = (MatchRoleBehavior)xmlSubstitution.NewPositionBehaviour,
                        MatchTeamLineUp = matchTeamLineUp
                    });
            }
        }

        private async Task ProcessMatchTeamLineUpSubstitutionAsync(
            Models.MatchLineUp.Substitution xmlSubstitution,
            Domain.Junior.MatchTeamLineUp matchTeamLineUp)
        {
            var substitution = await this.juniorMatchTeamLineUpSubstitutionRepository.Query(x => x.OutPlayerHattrickId == xmlSubstitution.SubjectPlayerId
                                                                                              && x.InPlayerHattrickId == xmlSubstitution.ObjectPlayerId
                                                                                              && x.TeamHattrickId == matchTeamLineUp.TeamHattrickId
                                                                                              && x.MatchHattrickId == matchTeamLineUp.MatchHattrickId
                                                                                              && x.NewRole == (MatchRole)xmlSubstitution.NewPositionId
                                                                                              && x.NewBehavior == (MatchRoleBehavior)xmlSubstitution.NewPositionBehaviour)
                .SingleOrDefaultAsync();

            if (substitution == null)
            {
                await this.juniorMatchTeamLineUpSubstitutionRepository.InsertAsync(
                    new Domain.Junior.MatchTeamLineUpSubstitution
                    {
                        Minute = xmlSubstitution.MatchMinute,
                        MatchPart = (MatchPart)xmlSubstitution.MatchPart,
                        OutPlayerHattrickId = xmlSubstitution.SubjectPlayerId,
                        InPlayerHattrickId = xmlSubstitution.ObjectPlayerId,
                        Type = (MatchOrderType)xmlSubstitution.OrderType,
                        NewRole = (MatchRole)xmlSubstitution.NewPositionId,
                        NewBehavior = (MatchRoleBehavior)xmlSubstitution.NewPositionBehaviour,
                        MatchTeamLineUp = matchTeamLineUp
                    });
            }
        }
    }
}