namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Enums;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.Players;

    public class Players : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public Players(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.UserSupporterTier = await reader.ReadElementContentAsStringAsync();
            result.IsYouth = await reader.ReadXmlValueAsBoolAsync();
            result.ActionType = await reader.ReadElementContentAsStringAsync();
            result.IsPlayingMatch = await reader.ReadXmlValueAsBoolAsync();
            result.Team = await ParseTeamNodeAsync(reader);

            return result;
        }

        private static async Task<LastMatch> ParseLastMatchNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            LastMatch result = new LastMatch
            {
                Date = await reader.ReadXmlValueAsDateTimeAsync(),
                MatchId = await reader.ReadXmlValueAsLongAsync(),
                PositionCode = await reader.ReadXmlValueAsShortAsync(),
                PlayedMinutes = await reader.ReadXmlValueAsByteAsync(),
                Rating = await reader.ReadXmlValueAsDecimalAsync(),
                RatingEndOfMatch = await reader.ReadXmlValueAsDecimalAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Player> ParsePlayerNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Player result = new Player
            {
                PlayerId = await reader.ReadXmlValueAsLongAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadXmlValueAsNullableStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                PlayerNumber = await reader.ReadXmlValueAsNullableByteAsync(100),
                Age = await reader.ReadXmlValueAsByteAsync(),
                AgeDays = await reader.ReadXmlValueAsByteAsync(),
                ArrivalDate = await reader.ReadXmlValueAsDateTimeAsync(),
                OwnerNotes = await reader.ReadXmlValueAsNullableStringAsync(),
                Tsi = await reader.ReadXmlValueAsIntAsync(),
                PlayerForm = await reader.ReadXmlValueAsByteAsync(),
                Statement = await reader.ReadXmlValueAsNullableStringAsync(),
                Experience = await reader.ReadXmlValueAsByteAsync(),
                Loyalty = await reader.ReadXmlValueAsByteAsync(),
                MotherClubBonus = await reader.ReadXmlValueAsBoolAsync(),
                Leadership = await reader.ReadXmlValueAsByteAsync(),
                Salary = await reader.ReadXmlValueAsLongAsync(),
                IsAbroad = await reader.ReadXmlValueAsBoolAsync(),
                Agreeability = await reader.ReadXmlValueAsByteAsync(),
                Aggressiveness = await reader.ReadXmlValueAsByteAsync(),
                Honesty = await reader.ReadXmlValueAsByteAsync(),
                LeagueGoals = await reader.ReadXmlValueAsShortAsync(),
                CupGoals = await reader.ReadXmlValueAsShortAsync(),
                FriendliesGoals = await reader.ReadXmlValueAsShortAsync(),
                CareerGoals = await reader.ReadXmlValueAsShortAsync(),
                CareerHattricks = await reader.ReadXmlValueAsShortAsync(),
                MatchesCurrentTeam = await reader.ReadXmlValueAsShortAsync(),
                GoalsCurrentTeam = await reader.ReadXmlValueAsShortAsync(),
                Specialty = await reader.ReadXmlValueAsByteAsync(),
                TransferListed = await reader.ReadXmlValueAsBoolAsync(),
                NationalTeamId = await reader.ReadXmlValueAsNullableLongAsync(0),
                CountryId = await reader.ReadXmlValueAsLongAsync(),
                Caps = await reader.ReadXmlValueAsShortAsync(),
                CapsU20 = await reader.ReadXmlValueAsShortAsync(),
                Cards = await reader.ReadXmlValueAsShortAsync(),
                InjuryLevel = await reader.ReadXmlValueAsShortAsync(),
                StaminaSkill = await reader.ReadXmlValueAsByteAsync(),
                KeeperSkill = await reader.ReadXmlValueAsByteAsync(),
                PlaymakerSkill = await reader.ReadXmlValueAsByteAsync(),
                ScorerSkill = await reader.ReadXmlValueAsByteAsync(),
                PassingSkill = await reader.ReadXmlValueAsByteAsync(),
                WingerSkill = await reader.ReadXmlValueAsByteAsync(),
                DefenderSkill = await reader.ReadXmlValueAsByteAsync(),
                SetPiecesSkill = await reader.ReadXmlValueAsByteAsync(),
                PlayerCategoryId = await reader.ReadXmlValueAsByteAsync()
            };

            if (reader.CheckNode(NodeName.TrainerData))
            {
                result.TrainerData = await ParseTrainerDataNodeAsync(reader);
            }

            if (reader.CheckNode(NodeName.LastMatch))
            {
                result.LastMatch = await ParseLastMatchNodeAsync(reader);
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Team result = new Team
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
            };

            if (reader.CheckNode(NodeName.PlayerList))
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Player))
                {
                    result.PlayerList.Add(
                        await ParsePlayerNodeAsync(
                            reader));
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<TrainerData> ParseTrainerDataNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            TrainerData result = new TrainerData
            {
                TrainerType = (TrainerType)await reader.ReadXmlValueAsLongAsync(),
                SkillLevel = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}