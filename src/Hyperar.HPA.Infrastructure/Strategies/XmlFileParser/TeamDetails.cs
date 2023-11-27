namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Collections.Generic;
    using System.Xml;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Hattrick.TeamDetails;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Common.ExtensionMethods;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class TeamDetails : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string awayFlagsNodeName = "AwayFlags";

        private const string bodyNodeName = "Body";

        private const string flagNodeName = "Flag";

        private const string flagsNodeName = "Flags";

        private const string guestbookNodeName = "Guestbook";

        private const string homeFlagsNodeName = "HomeFlags";

        private const string imageUrlNodeName = "ImageUrl";

        private const string indexAttributeName = "Index";

        private const string maxItemsAttributeName = "MaxItems";

        private const string mySupportesNodeName = "MySupporters";

        private const string nationalTeamNodeName = "NationalTeam";

        private const string pressAnnouncementBodyNodeName = "PressAnnouncementBody";

        private const string pressAnnouncementNodeName = "PressAnnouncement";

        private const string pressAnnouncementSendDateNodeName = "PressAnnouncementSendDate";

        private const string pressAnnouncementSubjectNodeName = "PressAnnouncementSubject";

        private const string sendDateNodeName = "SendDate";

        private const string subjectNodeName = "Subject";

        private const string supportedTeamNodeName = "SupportedTeam";

        private const string supportedTeamsNodeName = "SupportedTeams";

        private const string supporterTeamNodeName = "SupporterTeam";

        private const string teamColorsNodeName = "TeamColors";

        private const string teamNodeName = "Team";

        private const string totalItemsAttributeName = "TotalItems";

        private const string trophyListNodeName = "TrophyList";

        private const string trophyNodeName = "Trophy";

        public override void ParseFileTypeSpecificContent(XmlReader reader, ref IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.User = ParseUserNode(reader);
            result.Teams = ParseTeamsNode(reader);
        }

        private static Arena ParseArenaNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Arena
            {
                ArenaId = reader.ReadXmlValueAsUint(),
                ArenaName = reader.ReadElementContentAsString(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static BotStatus ParseBotStatusNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new BotStatus
            {
                IsBot = reader.ReadXmlValueAsBool()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Country ParseCountryNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Country
            {
                CountryId = reader.ReadXmlValueAsUint(),
                CountryName = reader.ReadElementContentAsString(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Cup? ParseCupNode(XmlReader reader)
        {
            Cup? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                reader.Read();

                result = new Cup
                {
                    StillInCup = reader.ReadXmlValueAsBool()
                };

                if (result.StillInCup)
                {
                    result.CupId = reader.ReadXmlValueAsUint();
                    result.CupName = reader.ReadElementContentAsString();
                    result.CupLeagueLevel = reader.ReadXmlValueAsUint();
                    result.CupLevel = reader.ReadXmlValueAsUint();
                    result.CupLeagueLevelIndex = reader.ReadXmlValueAsUint();
                    result.MatchRound = reader.ReadXmlValueAsUint();
                    result.MatchRoundsLeft = reader.ReadXmlValueAsUint();
                }
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Fanclub ParseFanclubNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Fanclub
            {
                FanclubId = reader.ReadXmlValueAsUint(),
                FanclubName = reader.ReadElementContentAsString(),
                FanclubSize = reader.ReadXmlValueAsUint()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static List<Flag> ParseFlagListNode(XmlReader reader)
        {
            var result = new List<Flag>();

            // Reads opening element.
            reader.Read();

            while (reader.Name == flagNodeName)
            {
                result.Add(ParseFlagNode(reader));
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Flag ParseFlagNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Flag
            {
                LeagueId = reader.ReadXmlValueAsUint(),
                LeagueName = reader.ReadElementContentAsString(),
                CountryCode = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Flags ParseFlagsNode(XmlReader reader)
        {
            var result = new Flags();

            // Reads opening element.
            reader.Read();

            if (reader.Name == awayFlagsNodeName)
            {
                result.AwayFlags = ParseFlagListNode(reader);
            }

            if (reader.Name == homeFlagsNodeName)
            {
                result.HomeFlags = ParseFlagListNode(reader);
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Guestbook ParseGuestbookNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Guestbook
            {
                NumberOfGuestbookItems = reader.ReadXmlValueAsUint(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Language ParseLanguageNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Language
            {
                LanguageId = reader.ReadXmlValueAsUint(),
                LanguageName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static LastMatch ParseLastMatchNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new LastMatch
            {
                LastMatchId = reader.ReadXmlValueAsUint(),
                LastMatchDate = reader.ReadXmlValueAsDateTime(),
                LastMatchHomeTeamId = reader.ReadXmlValueAsUint(),
                LastMatchHomeTeamName = reader.ReadElementContentAsString(),
                LastMatchHomeGoals = reader.ReadXmlValueAsUint(),
                LastMatchAwayTeamId = reader.ReadXmlValueAsUint(),
                LastMatchAwayTeamName = reader.ReadElementContentAsString(),
                LastMatchAwayGoals = reader.ReadXmlValueAsUint()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static LeagueLevelUnit ParseLeagueLevelUnitNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new LeagueLevelUnit
            {
                LeagueLevelUnitId = reader.ReadXmlValueAsUint(),
                LeagueLevelUnitName = reader.ReadElementContentAsString(),
                LeagueLevel = reader.ReadXmlValueAsUint()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static League ParseLeagueNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new League
            {
                LeagueId = reader.ReadXmlValueAsUint(),
                LeagueName = reader.ReadElementContentAsString(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static MySupporters ParseMySupportersNode(XmlReader reader)
        {
            var result = new MySupporters
            {
                TotalItems = uint.Parse(reader.GetAttribute(totalItemsAttributeName) ?? "0"),
                MaxItems = uint.Parse(reader.GetAttribute(maxItemsAttributeName) ?? "0")
            };

            // Reads opening element.
            reader.Read();

            while (reader.Name == supporterTeamNodeName)
            {
                result.SupporterTeamList.Add(ParseSupporterTeamNode(reader));
            }

            // Reads opening element.
            reader.Read();

            return result;
        }

        private static NationalTeam ParseNationalTeamNode(XmlReader reader)
        {
            var result = new NationalTeam();

            if (!reader.IsEmptyElement)
            {
                result.Index = uint.Parse(reader.GetAttribute(indexAttributeName) ?? "0");

                // Reads opening element.
                reader.Read();

                result.NationalTeamStaffType = (NationalTeamStaffType)reader.ReadXmlValueAsUint();
                result.NationalTeamId = reader.ReadXmlValueAsUint();
                result.NationalTeamName = reader.ReadElementContentAsString();
            }

            // Reads opening element.
            reader.Read();

            return result;
        }

        private static List<NationalTeam>? ParseNationalTeamsNode(XmlReader reader)
        {
            List<NationalTeam>? result = null;

            if (!reader.IsEmptyElement)
            {
                result = new List<NationalTeam>();

                // Reads opening element.
                reader.Read();

                while (reader.Name == nationalTeamNodeName)
                {
                    result.Add(ParseNationalTeamNode(reader));
                }
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static NextMatch ParseNextMatchNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new NextMatch
            {
                NextMatchId = reader.ReadXmlValueAsUint(),
                NextMatchDate = reader.ReadXmlValueAsDateTime(),
                NextMatchHomeTeamId = reader.ReadXmlValueAsUint(),
                NextMatchHomeTeamName = reader.ReadElementContentAsString(),
                NextMatchAwayTeamId = reader.ReadXmlValueAsUint(),
                NextMatchAwayTeamName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Rating ParsePowerRatingNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Rating
            {
                GlobalRanking = reader.ReadXmlValueAsUint(),
                LeagueRanking = reader.ReadXmlValueAsUint(),
                RegionRanking = reader.ReadXmlValueAsUint(),
                PowerRating = reader.ReadXmlValueAsUint()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static PressAnnouncement ParsePressAnnouncementNode(XmlReader reader)
        {
            // Since there's multiple PressAnnouncement nodes with the same child elements but in different order, this mehtod is different than the rest.
            var result = new PressAnnouncement();

            // Reads opening element.
            reader.Read();

            while (reader.Name != pressAnnouncementNodeName && reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.Name)
                {
                    case sendDateNodeName:
                    case pressAnnouncementSendDateNodeName:
                        result.SendDate = reader.ReadXmlValueAsDateTime();
                        break;

                    case bodyNodeName:
                    case pressAnnouncementBodyNodeName:
                        result.Body = reader.ReadElementContentAsString();
                        break;

                    case subjectNodeName:
                    case pressAnnouncementSubjectNodeName:
                        result.Subject = reader.ReadElementContentAsString();
                        break;

                    default:
                        reader.Read();
                        break;
                }
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Region ParseRegionNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Region
            {
                RegionId = reader.ReadXmlValueAsUint(),
                RegionName = reader.ReadElementContentAsString(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static SupportedTeam ParseSupportedTeamNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new SupportedTeam
            {
                UserId = reader.ReadXmlValueAsUint(),
                LoginName = reader.ReadElementContentAsString(),
                TeamId = reader.ReadXmlValueAsUint(),
                TeamName = reader.ReadElementContentAsString(),
                LeagueId = reader.ReadXmlValueAsUint(),
                LeagueName = reader.ReadElementContentAsString(),
                LeagueLevelUnitId = reader.ReadXmlValueAsUint(),
                LeagueLevelUnitName = reader.ReadElementContentAsString(),
                LastMatch = ParseLastMatchNode(reader),
                NextMatch = ParseNextMatchNode(reader),
            };

            if (reader.Name == pressAnnouncementNodeName)
            {
                result.PressAnnouncement = ParsePressAnnouncementNode(reader);
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static SupportedTeams ParseSupportedTeamsNode(XmlReader reader)
        {
            var result = new SupportedTeams
            {
                TotalItems = uint.Parse(reader.GetAttribute(totalItemsAttributeName) ?? "0"),
                MaxItems = uint.Parse(reader.GetAttribute(maxItemsAttributeName) ?? "0")
            };

            // Reads opening element.
            reader.Read();

            while (reader.Name == supportedTeamNodeName)
            {
                result.SupportedTeamList.Add(ParseSupportedTeamNode(reader));
            }

            // Reads opening element.
            reader.Read();

            return result;
        }

        private static SupporterTeam ParseSupporterTeamNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new SupporterTeam
            {
                UserId = reader.ReadXmlValueAsUint(),
                LoginName = reader.ReadElementContentAsString(),
                TeamId = reader.ReadXmlValueAsUint(),
                TeamName = reader.ReadElementContentAsString(),
                LeagueId = reader.ReadXmlValueAsUint(),
                LeagueName = reader.ReadElementContentAsString(),
                LeagueLevelUnitId = reader.ReadXmlValueAsUint(),
                LeagueLevelUnitName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static TeamColors? ParseTeamColorsNode(XmlReader reader)
        {
            TeamColors? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                reader.Read();

                result = new TeamColors
                {
                    BackgroundColor = reader.ReadElementContentAsString(),
                    Color = reader.ReadElementContentAsString(),
                };
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Team ParseTeamNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Team
            {
                TeamId = reader.ReadXmlValueAsUint(),
                TeamName = reader.ReadElementContentAsString(),
                ShortTeamName = reader.ReadElementContentAsString(),
                IsPrimaryClub = reader.ReadXmlValueAsBool(),
                FoundedDate = reader.ReadXmlValueAsDateTime(),
                Arena = ParseArenaNode(reader),
                League = ParseLeagueNode(reader),
                Country = ParseCountryNode(reader),
                Region = ParseRegionNode(reader),
                Trainer = ParseTrainerNode(reader),
                HomePage = reader.ReadElementContentAsString(),
                DressUri = reader.ReadElementContentAsString(),
                DressAlternateUri = reader.ReadElementContentAsString(),
                LeagueLevelUnit = ParseLeagueLevelUnitNode(reader),
                BotStatus = ParseBotStatusNode(reader),
                Cup = ParseCupNode(reader),
                PowerRating = ParsePowerRatingNode(reader)
            };

            // If the team is from Hattrick Special Leagues, these next elements are empty.
            string auxValue = reader.ReadElementContentAsString();
            result.FriendlyTeamId = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            auxValue = reader.ReadElementContentAsString();
            result.NumberOfVictories = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            auxValue = reader.ReadElementContentAsString();
            result.NumberOfUndefeated = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            auxValue = reader.ReadElementContentAsString();
            result.TeamRank = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            result.Fanclub = ParseFanclubNode(reader);
            result.LogoUrl = reader.ReadElementContentAsString();

            if (reader.Name == guestbookNodeName)
            {
                result.Guestbook = ParseGuestbookNode(reader);
            }

            if (reader.Name == pressAnnouncementNodeName)
            {
                result.PressAnnouncement = ParsePressAnnouncementNode(reader);
            }

            if (reader.Name == teamColorsNodeName)
            {
                result.TeamColors = ParseTeamColorsNode(reader);
            }

            result.YouthTeamId = reader.ReadXmlValueAsUint();
            result.YouthTeamName = reader.ReadElementContentAsString();
            result.NumberOfVisits = reader.ReadXmlValueAsUint();

            if (reader.Name == flagsNodeName)
            {
                result.Flags = ParseFlagsNode(reader);
            }

            if (reader.Name == trophyListNodeName)
            {
                result.TrophyList = ParseTrophyListNode(reader);
            }

            if (reader.Name == supportedTeamsNodeName)
            {
                result.SupportedTeams = ParseSupportedTeamsNode(reader);
            }

            if (reader.Name == mySupportesNodeName)
            {
                result.MySupporters = ParseMySupportersNode(reader);
            }

            result.PossibleToChallengeMidweek = reader.ReadXmlValueAsBool();
            result.PossibleToChallengeWeekend = reader.ReadXmlValueAsBool();

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static List<Team> ParseTeamsNode(XmlReader reader)
        {
            var result = new List<Team>();

            // Reads opening element.
            reader.Read();

            while (reader.Name == teamNodeName)
            {
                result.Add(ParseTeamNode(reader));
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Trainer ParseTrainerNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Trainer
            {
                PlayerId = reader.ReadXmlValueAsUint(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static List<Trophy>? ParseTrophyListNode(XmlReader reader)
        {
            List<Trophy>? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                reader.Read();

                result = new List<Trophy>();

                while (reader.Name == trophyNodeName)
                {
                    result.Add(ParseTrophyNode(reader));
                }
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static Trophy ParseTrophyNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Trophy
            {
                TrophyTypeId = reader.ReadXmlValueAsUint(),
                TrophySeason = reader.ReadXmlValueAsUint(),
                LeagueLevel = reader.ReadXmlValueAsUint(),
                LeagueLevelUnitId = reader.ReadXmlValueAsUint(),
                LeagueLevelUnitName = reader.ReadElementContentAsString(),
                GainedDate = reader.ReadXmlValueAsDateTime(),
            };

            if (reader.Name == imageUrlNodeName)
            {
                result.ImageUrl = reader.ReadElementContentAsString();
            }

            string auxValue = reader.ReadElementContentAsString();
            result.CupLeagueLevel = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            auxValue = reader.ReadElementContentAsString();
            result.CupLevel = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            auxValue = reader.ReadElementContentAsString();
            result.CupLevelIndex = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            // Reads closing element.
            reader.Read();

            return result;
        }

        private static User ParseUserNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new User
            {
                UserId = reader.ReadXmlValueAsUint(),
                Language = ParseLanguageNode(reader),
                SupporterTier = reader.ReadElementContentAsString().ToSupporterTier(),
                LoginName = reader.ReadElementContentAsString(),
                Name = reader.ReadElementContentAsString(),
                Icq = reader.ReadElementContentAsString(),
                SignUpDate = reader.ReadXmlValueAsDateTime(),
                ActivationDate = reader.ReadXmlValueAsDateTime(),
                LastLoginDate = reader.ReadXmlValueAsDateTime(),
                HasManagerLicense = reader.ReadXmlValueAsBool(),
                NationalTeams = ParseNationalTeamsNode(reader)
            };

            // Reads closing element.
            reader.Read();

            return result;
        }
    }
}