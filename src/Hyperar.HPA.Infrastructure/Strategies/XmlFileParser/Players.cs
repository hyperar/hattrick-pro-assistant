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

        public override void ParseFileTypeSpecificContent(XmlReader reader, ref IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.UserSupporterTier = reader.ReadElementContentAsString().ToSupporterTier();
            result.IsYouth = reader.ReadXmlValueAsBool();
            result.ActionType = reader.ReadElementContentAsString();
            result.IsPlayingMatch = reader.ReadXmlValueAsBool();
            result.Team = this.ParseTeamNode(reader);
        }

        private LastMatch ParseLastMatchNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new LastMatch
            {
                Date = reader.ReadXmlValueAsDateTime(),
                MatchId = reader.ReadXmlValueAsUint(),
                PositionCode = (MatchRole)reader.ReadXmlValueAsUint(),
                PlayedMinutes = reader.ReadXmlValueAsUint(),
                Rating = reader.ReadXmlValueAsDecimal(),
                RatingEndOfMatch = reader.ReadXmlValueAsDecimal()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Player ParsePlayerNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Player
            {
                PlayerId = reader.ReadXmlValueAsUint(),
                FirstName = reader.ReadElementContentAsString(),
                NickName = reader.ReadXmlValueAsNullableString(),
                LastName = reader.ReadElementContentAsString(),
                PlayerNumber = reader.ReadXmlValueAsNullableUint(100),
                Age = reader.ReadXmlValueAsUint(),
                AgeDays = reader.ReadXmlValueAsUint(),
                ArrivalDate = reader.ReadXmlValueAsDateTime(),
                OwnerNotes = reader.ReadXmlValueAsNullableString(),
                Tsi = reader.ReadXmlValueAsUint(),
                PlayerForm = reader.ReadXmlValueAsSkillLevel(),
                Statement = reader.ReadXmlValueAsNullableString(),
                Experience = reader.ReadXmlValueAsSkillLevel(),
                Loyalty = reader.ReadXmlValueAsSkillLevel(),
                MotherClubBonus = reader.ReadXmlValueAsBool(),
                Leadership = reader.ReadXmlValueAsSkillLevel(),
                Salary = reader.ReadXmlValueAsUint(),
                IsAbroad = reader.ReadXmlValueAsBool(),
                Agreeability = (AgreeabilityLevel)reader.ReadXmlValueAsUint(),
                Aggressiveness = (AggressivenessLevel)reader.ReadXmlValueAsUint(),
                Honesty = (HonestyLevel)reader.ReadXmlValueAsUint(),
                LeagueGoals = reader.ReadXmlValueAsInt(),
                CupGoals = reader.ReadXmlValueAsInt(),
                FriendliesGoals = reader.ReadXmlValueAsInt(),
                CareerGoals = reader.ReadXmlValueAsInt(),
                CareerHattricks = reader.ReadXmlValueAsInt(),
                MatchesCurrentTeam = reader.ReadXmlValueAsUint(),
                GoalsCurrentTeam = reader.ReadXmlValueAsInt(),
                Specialty = (Specialty)reader.ReadXmlValueAsUint(),
                TransferListed = reader.ReadXmlValueAsBool(),
                NationalTeamId = reader.ReadXmlValueAsNullableUint(0),
                CountryId = reader.ReadXmlValueAsUint(),
                Caps = reader.ReadXmlValueAsUint(),
                CapsU20 = reader.ReadXmlValueAsUint(),
                Cards = reader.ReadXmlValueAsUint(),
                InjuryLevel = reader.ReadXmlValueAsInt(),
                StaminaSkill = reader.ReadXmlValueAsSkillLevel(),
                KeeperSkill = reader.ReadXmlValueAsSkillLevel(),
                PlaymakerSkill = reader.ReadXmlValueAsSkillLevel(),
                ScorerSkill = reader.ReadXmlValueAsSkillLevel(),
                PassingSkill = reader.ReadXmlValueAsSkillLevel(),
                WingerSkill = reader.ReadXmlValueAsSkillLevel(),
                DefenderSkill = reader.ReadXmlValueAsSkillLevel(),
                SetPiecesSkill = reader.ReadXmlValueAsSkillLevel(),
                PlayerCategoryId = (PlayerCategory)reader.ReadXmlValueAsUint()
            };

            if (reader.Name == trainerDataNodeName)
            {
                result.TrainerData = this.ParseTrainerDataNode(reader);
            }

            if (reader.Name == lastMatchNodeName)
            {
                result.LastMatch = this.ParseLastMatchNode(reader);
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Team ParseTeamNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Team
            {
                TeamId = reader.ReadXmlValueAsUint(),
                TeamName = reader.ReadElementContentAsString(),
            };

            if (reader.Name == playerListNodeName)
            {
                // Reads opening element.
                reader.Read();

                while (reader.Name == playerNodeName)
                {
                    result.PlayerList.Add(
                        this.ParsePlayerNode(reader));
                }

                // Reads closing element.
                reader.Read();
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private TrainerData ParseTrainerDataNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new TrainerData
            {
                TrainerType = (TrainerType)reader.ReadXmlValueAsUint(),
                SkillLevel = reader.ReadXmlValueAsSkillLevel(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }
    }
}