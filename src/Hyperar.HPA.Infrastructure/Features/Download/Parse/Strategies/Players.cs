namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.Players;

    public class Players : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.UserSupporterTier = await reader.ReadElementContentAsStringAsync();
            result.IsYouth = await reader.ReadXmlValueAsBoolAsync();
            result.ActionType = await reader.ReadElementContentAsStringAsync();
            result.IsPlayingMatch = await reader.ReadXmlValueAsBoolAsync();
            result.Team = await ParseTeamNodeAsync(reader, cancellationToken);
        }

        private static async Task<LastMatch> ParseLastMatchNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            LastMatch result = new LastMatch
            {
                Date = await reader.ReadXmlValueAsDateTimeAsync(),
                MatchId = await reader.ReadXmlValueAsLongAsync(),
                PositionCode = await reader.ReadXmlValueAsIntAsync(),
                PlayedMinutes = await reader.ReadXmlValueAsIntAsync(),
                Rating = await reader.ReadXmlValueAsDecimalAsync(),
                RatingEndOfMatch = await reader.ReadXmlValueAsDecimalAsync()
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
                Age = await reader.ReadXmlValueAsIntAsync(),
                AgeDays = await reader.ReadXmlValueAsIntAsync(),
                ArrivalDate = await reader.ReadXmlValueAsDateTimeAsync(),
                OwnerNotes = await reader.ReadXmlValueAsNullableStringAsync(),
                TSI = await reader.ReadXmlValueAsIntAsync(),
                PlayerForm = await reader.ReadXmlValueAsIntAsync(),
                Statement = await reader.ReadXmlValueAsNullableStringAsync(),
                Experience = await reader.ReadXmlValueAsIntAsync(),
                Loyalty = await reader.ReadXmlValueAsIntAsync(),
                MotherClubBonus = await reader.ReadXmlValueAsBoolAsync(),
                Leadership = await reader.ReadXmlValueAsIntAsync(),
                Salary = await reader.ReadXmlValueAsLongAsync(),
                IsAbroad = await reader.ReadXmlValueAsBoolAsync(),
                Agreeability = await reader.ReadXmlValueAsIntAsync(),
                Aggressiveness = await reader.ReadXmlValueAsIntAsync(),
                Honesty = await reader.ReadXmlValueAsIntAsync(),
                LeagueGoals = await reader.ReadXmlValueAsIntAsync(),
                CupGoals = await reader.ReadXmlValueAsIntAsync(),
                FriendliesGoals = await reader.ReadXmlValueAsIntAsync(),
                CareerGoals = await reader.ReadXmlValueAsIntAsync(),
                CareerHattricks = await reader.ReadXmlValueAsIntAsync(),
                MatchesCurrentTeam = await reader.ReadXmlValueAsIntAsync(),
                GoalsCurrentTeam = await reader.ReadXmlValueAsIntAsync(),
                Specialty = await reader.ReadXmlValueAsIntAsync(),
                TransferListed = await reader.ReadXmlValueAsBoolAsync(),
                NationalTeamId = await reader.ReadXmlValueAsNullableLongAsync(0),
                CountryId = await reader.ReadXmlValueAsLongAsync(),
                Caps = await reader.ReadXmlValueAsIntAsync(),
                CapsU20 = await reader.ReadXmlValueAsIntAsync(),
                Cards = await reader.ReadXmlValueAsIntAsync(),
                InjuryLevel = await reader.ReadXmlValueAsIntAsync(),
                StaminaSkill = await reader.ReadXmlValueAsIntAsync(),
                KeeperSkill = await reader.ReadXmlValueAsIntAsync(),
                PlaymakerSkill = await reader.ReadXmlValueAsIntAsync(),
                ScorerSkill = await reader.ReadXmlValueAsIntAsync(),
                PassingSkill = await reader.ReadXmlValueAsIntAsync(),
                WingerSkill = await reader.ReadXmlValueAsIntAsync(),
                DefenderSkill = await reader.ReadXmlValueAsIntAsync(),
                SetPiecesSkill = await reader.ReadXmlValueAsIntAsync(),
                PlayerCategoryId = await reader.ReadXmlValueAsIntAsync()
            };

            if (reader.CheckNode(NodeName.TrainerData))
            {
                result.TrainerData = await ParseTrainerDataNodeAsync(reader, cancellationToken);
            }

            if (reader.CheckNode(NodeName.LastMatch))
            {
                result.LastMatch = await ParseLastMatchNodeAsync(reader, cancellationToken);
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
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
                            reader,
                            cancellationToken));
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<TrainerData> ParseTrainerDataNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            TrainerData result = new TrainerData
            {
                TrainerType = await reader.ReadXmlValueAsIntAsync(),
                SkillLevel = await reader.ReadXmlValueAsIntAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}