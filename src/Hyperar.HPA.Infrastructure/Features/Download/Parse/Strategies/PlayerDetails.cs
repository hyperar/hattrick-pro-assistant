namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.PlayerDetails;

    public class PlayerDetails : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.UserSupporterTier = await reader.ReadElementContentAsStringAsync();
            result.IsPlayingMatch = await reader.ReadXmlValueAsBoolAsync();
            result.Player = await ParsePlayerNodeAsync(reader, cancellationToken);
        }

        private static async Task<IdName?> ParseBidderTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            IdName? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                await reader.ReadAsync();

                result = new IdName
                {
                    Id = await reader.ReadXmlValueAsLongAsync(),
                    Name = await reader.ReadElementContentAsStringAsync()
                };
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<LastMatch?> ParseLastMatchNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            LastMatch? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                await reader.ReadAsync();

                result = new LastMatch
                {
                    Date = await reader.ReadXmlValueAsDateTimeAsync(),
                    MatchId = await reader.ReadXmlValueAsLongAsync(),
                    PositionCode = await reader.ReadXmlValueAsIntAsync(),
                    PlayedMinutes = await reader.ReadXmlValueAsIntAsync(),
                    Rating = await reader.ReadXmlValueAsDecimalAsync(),
                    RatingEndOfMatch = await reader.ReadXmlValueAsDecimalAsync()
                };

                // Sometimes this node is broken.
                if (result.MatchId == -1)
                {
                    result = null;
                }
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<IdName> ParseMotherClubNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            IdName result = new IdName
            {
                Id = await reader.ReadXmlValueAsLongAsync(),
                Name = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<OwningTeam> ParseOwningTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            OwningTeam result = new OwningTeam
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                LeagueId = await reader.ReadXmlValueAsLongAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Player> ParsePlayerNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Player result = new Player
            {
                PlayerId = await reader.ReadXmlValueAsLongAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadXmlValueAsNullableStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                PlayerNumber = await reader.ReadXmlValueAsNullableIntAsync(100),
                PlayerCategoryId = await reader.ReadXmlValueAsIntAsync(),
                OwnerNotes = await reader.ReadXmlValueAsNullableStringAsync(),
                Age = await reader.ReadXmlValueAsIntAsync(),
                AgeDays = await reader.ReadXmlValueAsIntAsync(),
                NextBirthDay = await reader.ReadXmlValueAsDateTimeAsync(),
                ArrivalDate = await reader.ReadXmlValueAsDateTimeAsync(),
                PlayerForm = await reader.ReadXmlValueAsIntAsync(),
                Cards = await reader.ReadXmlValueAsIntAsync(),
                InjuryLevel = await reader.ReadXmlValueAsIntAsync(),
                Statement = await reader.ReadXmlValueAsNullableStringAsync(),
                PlayerLanguage = await reader.ReadElementContentAsStringAsync(),
                PlayerLanguageId = await reader.ReadXmlValueAsLongAsync(),
                TrainerData = await ParseTrainerDataNodeAsync(reader, cancellationToken),
                OwningTeam = await ParseOwningTeamNodeAsync(reader, cancellationToken),
                Salary = await reader.ReadXmlValueAsLongAsync(),
                IsAbroad = await reader.ReadXmlValueAsBoolAsync(),
                Agreeability = await reader.ReadXmlValueAsIntAsync(),
                Aggressiveness = await reader.ReadXmlValueAsIntAsync(),
                Honesty = await reader.ReadXmlValueAsIntAsync(),
                Experience = await reader.ReadXmlValueAsIntAsync(),
                Loyalty = await reader.ReadXmlValueAsIntAsync(),
                MotherClubBonus = await reader.ReadXmlValueAsBoolAsync(),
                MotherClub = await ParseMotherClubNodeAsync(reader, cancellationToken),
                Leadership = await reader.ReadXmlValueAsIntAsync(),
                Specialty = await reader.ReadXmlValueAsIntAsync(),
                NativeCountryId = await reader.ReadXmlValueAsLongAsync(),
                NativeLeagueId = await reader.ReadXmlValueAsLongAsync(),
                NativeLeagueName = await reader.ReadElementContentAsStringAsync(),
                TSI = await reader.ReadXmlValueAsIntAsync(),
                PlayerSkills = await ParsePlayerSkillsNodeAsync(reader, cancellationToken),
                Caps = await reader.ReadXmlValueAsIntAsync(),
                CapsU20 = await reader.ReadXmlValueAsIntAsync(),
                CareerGoals = await reader.ReadXmlValueAsIntAsync(),
                CareerHattricks = await reader.ReadXmlValueAsIntAsync(),
                LeagueGoals = await reader.ReadXmlValueAsIntAsync(),
                CupGoals = await reader.ReadXmlValueAsIntAsync(),
                FriendliesGoals = await reader.ReadXmlValueAsIntAsync(),
                MatchesCurrentTeam = await reader.ReadXmlValueAsIntAsync(),
                GoalsCurrentTeam = await reader.ReadXmlValueAsIntAsync(),
                TransferListed = await reader.ReadXmlValueAsBoolAsync(),
                TransferDetails = await ParseTransferDetailsNodeAsync(reader, cancellationToken),
                LastMatch = reader.CheckNode(NodeName.LastMatch) ? await ParseLastMatchNodeAsync(reader, cancellationToken) : null
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<PlayerSkills> ParsePlayerSkillsNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            PlayerSkills result = new PlayerSkills
            {
                StaminaSkill = await reader.ReadXmlValueAsIntAsync(),
                KeeperSkill = await reader.ReadXmlValueAsIntAsync(),
                PlaymakerSkill = await reader.ReadXmlValueAsIntAsync(),
                ScorerSkill = await reader.ReadXmlValueAsIntAsync(),
                PassingSkill = await reader.ReadXmlValueAsIntAsync(),
                WingerSkill = await reader.ReadXmlValueAsIntAsync(),
                DefenderSkill = await reader.ReadXmlValueAsIntAsync(),
                SetPiecesSkill = await reader.ReadXmlValueAsIntAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<TrainerData?> ParseTrainerDataNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            TrainerData? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                await reader.ReadAsync();

                result = new TrainerData
                {
                    TrainerType = await reader.ReadXmlValueAsIntAsync(),
                    TrainerSkillLevel = await reader.ReadXmlValueAsIntAsync(),
                };
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<TransferDetails?> ParseTransferDetailsNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            TransferDetails? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                await reader.ReadAsync();

                result = new TransferDetails
                {
                    AskingPrice = await reader.ReadXmlValueAsLongAsync(),
                    Deadline = await reader.ReadXmlValueAsDateTimeAsync(),
                    HighestBid = await reader.ReadXmlValueAsLongAsync(),
                    BidderTeam = await ParseBidderTeamNodeAsync(reader, cancellationToken)
                };
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}