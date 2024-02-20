namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.Players;
    using Application.Interfaces;
    using Common.Enums;
    using Common.ExtensionMethods;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class Players : XmlFileParserBase, IXmlFileParserStrategy
    {
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
                PositionCode = await reader.ReadXmlValueAsUshortAsync(),
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
                PlayerForm = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                Statement = await reader.ReadXmlValueAsNullableStringAsync(),
                Experience = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                Loyalty = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                MotherClubBonus = await reader.ReadXmlValueAsBoolAsync(),
                Leadership = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
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
                StaminaSkill = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                KeeperSkill = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                PlaymakerSkill = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                ScorerSkill = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                PassingSkill = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                WingerSkill = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                DefenderSkill = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                SetPiecesSkill = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                PlayerCategoryId = (PlayerCategory)await reader.ReadXmlValueAsUintAsync()
            };

            if (reader.CheckNode(Constants.NodeName.TrainerData))
            {
                result.TrainerData = await ParseTrainerDataNodeAsync(reader);
            }

            if (reader.CheckNode(Constants.NodeName.LastMatch))
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

            if (reader.CheckNode(Constants.NodeName.PlayerList))
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(Constants.NodeName.Player))
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
                SkillLevel = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}