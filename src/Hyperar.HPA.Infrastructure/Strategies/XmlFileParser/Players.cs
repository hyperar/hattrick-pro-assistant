namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Hattrick.Players;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Common.ExtensionMethods;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class Players : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string lastMatchNodeName = "LastMatch";

        private const string playerListNodeName = "PlayerList";

        private const string playerNodeName = "Player";

        private const string trainerDataNodeName = "TrainerData";

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.UserSupporterTier = (await reader.ReadElementContentAsStringAsync()).ToSupporterTier();
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

            var result = new LastMatch
            {
                Date = await reader.ReadXmlValueAsDateTimeAsync(),
                MatchId = await reader.ReadXmlValueAsUintAsync(),
                PositionCode = (MatchRole)await reader.ReadXmlValueAsUintAsync(),
                PlayedMinutes = await reader.ReadXmlValueAsUintAsync(),
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

            var result = new Player
            {
                PlayerId = await reader.ReadXmlValueAsUintAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadXmlValueAsNullableStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                PlayerNumber = await reader.ReadXmlValueAsNullableUintAsync(100),
                Age = await reader.ReadXmlValueAsUintAsync(),
                AgeDays = await reader.ReadXmlValueAsUintAsync(),
                ArrivalDate = await reader.ReadXmlValueAsDateTimeAsync(),
                OwnerNotes = await reader.ReadXmlValueAsNullableStringAsync(),
                Tsi = await reader.ReadXmlValueAsUintAsync(),
                PlayerForm = await reader.ReadXmlValueAsSkillLevelAsync(),
                Statement = await reader.ReadXmlValueAsNullableStringAsync(),
                Experience = await reader.ReadXmlValueAsSkillLevelAsync(),
                Loyalty = await reader.ReadXmlValueAsSkillLevelAsync(),
                MotherClubBonus = await reader.ReadXmlValueAsBoolAsync(),
                Leadership = await reader.ReadXmlValueAsSkillLevelAsync(),
                Salary = await reader.ReadXmlValueAsUintAsync(),
                IsAbroad = await reader.ReadXmlValueAsBoolAsync(),
                Agreeability = (AgreeabilityLevel)await reader.ReadXmlValueAsUintAsync(),
                Aggressiveness = (AggressivenessLevel)await reader.ReadXmlValueAsUintAsync(),
                Honesty = (HonestyLevel)await reader.ReadXmlValueAsUintAsync(),
                LeagueGoals = await reader.ReadXmlValueAsIntAsync(),
                CupGoals = await reader.ReadXmlValueAsIntAsync(),
                FriendliesGoals = await reader.ReadXmlValueAsIntAsync(),
                CareerGoals = await reader.ReadXmlValueAsIntAsync(),
                CareerHattricks = await reader.ReadXmlValueAsIntAsync(),
                MatchesCurrentTeam = await reader.ReadXmlValueAsUintAsync(),
                GoalsCurrentTeam = await reader.ReadXmlValueAsIntAsync(),
                Specialty = (Specialty)await reader.ReadXmlValueAsUintAsync(),
                TransferListed = await reader.ReadXmlValueAsBoolAsync(),
                NationalTeamId = await reader.ReadXmlValueAsNullableUintAsync(0),
                CountryId = await reader.ReadXmlValueAsUintAsync(),
                Caps = await reader.ReadXmlValueAsUintAsync(),
                CapsU20 = await reader.ReadXmlValueAsUintAsync(),
                Cards = await reader.ReadXmlValueAsUintAsync(),
                InjuryLevel = await reader.ReadXmlValueAsIntAsync(),
                StaminaSkill = await reader.ReadXmlValueAsSkillLevelAsync(),
                KeeperSkill = await reader.ReadXmlValueAsSkillLevelAsync(),
                PlaymakerSkill = await reader.ReadXmlValueAsSkillLevelAsync(),
                ScorerSkill = await reader.ReadXmlValueAsSkillLevelAsync(),
                PassingSkill = await reader.ReadXmlValueAsSkillLevelAsync(),
                WingerSkill = await reader.ReadXmlValueAsSkillLevelAsync(),
                DefenderSkill = await reader.ReadXmlValueAsSkillLevelAsync(),
                SetPiecesSkill = await reader.ReadXmlValueAsSkillLevelAsync(),
                PlayerCategoryId = (PlayerCategory)await reader.ReadXmlValueAsUintAsync()
            };

            if (reader.Name == trainerDataNodeName)
            {
                result.TrainerData = await ParseTrainerDataNodeAsync(reader);
            }

            if (reader.Name == lastMatchNodeName)
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

            var result = new Team
            {
                TeamId = await reader.ReadXmlValueAsUintAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
            };

            if (reader.Name == playerListNodeName)
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.Name == playerNodeName)
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

            var result = new TrainerData
            {
                TrainerType = (TrainerType)await reader.ReadXmlValueAsUintAsync(),
                SkillLevel = await reader.ReadXmlValueAsSkillLevelAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}