namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Hattrick = Application.Hattrick.HallOfFamePlayers;

    public class HallOfFamePlayers : IXmlFileDataPersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.HallOfFamePlayer> hallOfFamePlayersRepository;

        public HallOfFamePlayers(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.HallOfFamePlayer> hallOfFamePlayersRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.hallOfFamePlayersRepository = hallOfFamePlayersRepository;
        }

        public async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData xmlEntity)
                {
                    await this.ProcessHallOfFamePlayersAsync(xmlEntity);
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

        private async Task ProcessHallOfFamePlayersAsync(Hattrick.HattrickData xmlPlayers)
        {
            foreach (var curPlayer in xmlPlayers.PlayerList)
            {
                var hallOfFamePlayer = await this.hallOfFamePlayersRepository.GetByHattrickIdAsync(curPlayer.PlayerId);

                if (hallOfFamePlayer == null)
                {
                    var country = await this.countryRepository.GetByHattrickIdAsync(curPlayer.CountryId);

                    ArgumentNullException.ThrowIfNull(country, nameof(country));

                    hallOfFamePlayer = new Domain.HallOfFamePlayer
                    {
                        HattrickId = curPlayer.PlayerId,
                        FirstName = curPlayer.FirstName,
                        NickName = string.IsNullOrWhiteSpace(curPlayer.NickName) ? null : curPlayer.NickName,
                        LastName = curPlayer.LastName,
                        Age = curPlayer.Age,
                        JoinedTeamOn = curPlayer.ArrivalDate,
                        NextBirthday = curPlayer.NextBirthday,
                        IntroducedToHallOfFameOn = curPlayer.HofDate,
                        ExpertType = curPlayer.ExpertType,
                        Country = country
                    };

                    await this.hallOfFamePlayersRepository.InsertAsync(hallOfFamePlayer);
                }
                else
                {
                    hallOfFamePlayer.Age = curPlayer.Age;
                    hallOfFamePlayer.NextBirthday = curPlayer.NextBirthday;
                    hallOfFamePlayer.ExpertType = curPlayer.ExpertType;

                    this.hallOfFamePlayersRepository.Update(hallOfFamePlayer);
                }
            }

            await this.databaseContext.SaveAsync();
        }
    }
}