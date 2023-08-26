namespace Hyperar.HPA.Business.XmlFileParser
{
    using System.Collections.Generic;
    using System.Xml;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Common.Enums;
    using Hyperar.HPA.Common.ExtensionMethods;
    using Hyperar.HPA.Domain.Hattrick;
    using Hyperar.HPA.Domain.Hattrick.TeamDetails;

    public class TeamDetails : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string totalItemsAttributeName = "TotalItems";

        private const string maxItemsAttributeName = "MaxItems";

        private const string teamNodeName = "Team";

        private const string indexAttributeName = "Index";

        private const string supporterTeamNodeName = "SupporterTeam";

        private const string mySupportesNodeName = "MySupporters";

        private const string supportedTeamNodeName = "SupportedTeam";

        private const string guestbookNodeName = "Guestbook";

        private const string teamColorsNodeName = "TeamColors";

        private const string nationalTeamNodeName = "NationalTeam";

        private const string pressAnnouncementNodeName = "PressAnnouncement";

        private const string supportedTeamsNodeName = "SupportedTeams";

        private const string awayFlagsNodeName = "AwayFlags";

        private const string homeFlagsNodeName = "HomeFlags";

        private const string flagNodeName = "Flag";

        private const string flagsNodeName = "Flags";

        private const string trophyListNodeName = "TrophyList";

        private const string trophyNodeName = "Trophy";

        private const string subjectNodeName = "Subject";

        private const string pressAnnouncementSubjectNodeName = "PressAnnouncementSubject";

        private const string bodyNodeName = "Body";

        private const string pressAnnouncementBodyNodeName = "PressAnnouncementBody";

        private const string sendDateNodeName = "SendDate";

        private const string pressAnnouncementSendDateNodeName = "PressAnnouncementSendDate";

        private const string imageUrlNodeName = "ImageUrl";

        public override void ParseFileTypeSpecificContent(XmlReader reader, ref XmlFileBase entity)
        {
            HattrickData result = (HattrickData)entity;

            result.User = this.ParseUserNode(reader);
            result.Teams = this.ParseTeamsNode(reader);
        }

        private User ParseUserNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            User result = new User
            {
                UserId = uint.Parse(reader.ReadElementContentAsString()),
                Language = this.ParseLanguageNode(reader),
                SupporterTier = reader.ReadElementContentAsString().ToSupporterTier(),
                LoginName = reader.ReadElementContentAsString(),
                Name = reader.ReadElementContentAsString(),
                Icq = reader.ReadElementContentAsString(),
                SignUpDate = this.ParseDateTimeValue(reader.ReadElementContentAsString()),
                ActivationDate = this.ParseDateTimeValue(reader.ReadElementContentAsString()),
                LastLoginDate = this.ParseDateTimeValue(reader.ReadElementContentAsString()),
                HasManagerLicense = reader.ReadElementContentAsString() == bool.TrueString,
                NationalTeams = this.ParseNationalTeamsNode(reader)
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Language ParseLanguageNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Language result = new Language
            {
                LanguageId = uint.Parse(reader.ReadElementContentAsString()),
                LanguageName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private List<NationalTeam>? ParseNationalTeamsNode(XmlReader reader)
        {
            List<NationalTeam>? result = null;

            if (!reader.IsEmptyElement)
            {
                result = new List<NationalTeam>();

                // Reads opening element.
                reader.Read();

                while (reader.Name == nationalTeamNodeName)
                {
                    result.Add(this.ParseNationalTeamNode(reader));
                }
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private NationalTeam ParseNationalTeamNode(XmlReader reader)
        {
            NationalTeam result = new NationalTeam();

            if (!reader.IsEmptyElement)
            {
                result.Index = uint.Parse(reader.GetAttribute(indexAttributeName) ?? "0");

                // Reads opening element.
                reader.Read();

                result.NationalTeamStaffType = (NationalTeamStaffType)uint.Parse(reader.ReadElementContentAsString());
                result.NationalTeamId = uint.Parse(reader.ReadElementContentAsString());
                result.NationalTeamName = reader.ReadElementContentAsString();
            }

            // Reads opening element.
            reader.Read();

            return result;
        }

        private List<Team> ParseTeamsNode(XmlReader reader)
        {
            List<Team> result = new List<Team>();

            // Reads opening element.
            reader.Read();

            while (reader.Name == teamNodeName)
            {
                result.Add(this.ParseTeamNode(reader));
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Team ParseTeamNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Team result = new Team
            {
                TeamId = uint.Parse(reader.ReadElementContentAsString()),
                TeamName = reader.ReadElementContentAsString(),
                ShortTeamName = reader.ReadElementContentAsString(),
                IsPrimaryClub = reader.ReadElementContentAsString() == bool.TrueString,
                FoundedDate = this.ParseDateTimeValue(reader.ReadElementContentAsString()),
                Arena = this.ParseArenaNode(reader),
                League = this.ParseLeagueNode(reader),
                Country = this.ParseCountryNode(reader),
                Region = this.ParseRegionNode(reader),
                Trainer = this.ParseTrainerNode(reader),
                HomePage = reader.ReadElementContentAsString(),
                DressUri = reader.ReadElementContentAsString(),
                DressAlternateUri = reader.ReadElementContentAsString(),
                LeagueLevelUnit = this.ParseLeagueLevelUnitNode(reader),
                BotStatus = this.ParseBotStatusNode(reader),
                Cup = this.ParseCupNode(reader),
                PowerRating = this.ParsePowerRatingNode(reader)
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

            result.Fanclub = this.ParseFanclubNode(reader);
            result.LogoUrl = reader.ReadElementContentAsString();

            if (reader.Name == guestbookNodeName)
            {
                result.Guestbook = this.ParseGuestbookNode(reader);
            }

            if (reader.Name == pressAnnouncementNodeName)
            {
                result.PressAnnouncement = this.ParsePressAnnouncementNode(reader);
            }

            if (reader.Name == teamColorsNodeName)
            {
                result.TeamColors = this.ParseTeamColorsNode(reader);
            }

            result.YouthTeamId = uint.Parse(reader.ReadElementContentAsString());
            result.YouthTeamName = reader.ReadElementContentAsString();
            result.NumberOfVisits = uint.Parse(reader.ReadElementContentAsString());

            if (reader.Name == flagsNodeName)
            {
                result.Flags = this.ParseFlagsNode(reader);
            }

            if (reader.Name == trophyListNodeName)
            {
                result.TrophyList = this.ParseTrophyListNode(reader);
            }

            if (reader.Name == supportedTeamsNodeName)
            {
                result.SupportedTeams = this.ParseSupportedTeamsNode(reader);
            }

            if (reader.Name == mySupportesNodeName)
            {
                result.MySupporters = this.ParseMySupportersNode(reader);
            }

            result.PossibleToChallengeMidweek = reader.ReadElementContentAsString() == bool.TrueString;
            result.PossibleToChallengeWeekend = reader.ReadElementContentAsString() == bool.TrueString;

            // Reads closing element.
            reader.Read();

            return result;
        }

        private List<Trophy>? ParseTrophyListNode(XmlReader reader)
        {
            List<Trophy>? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                reader.Read();

                result = new List<Trophy>();

                while (reader.Name == trophyNodeName)
                {
                    result.Add(this.ParseTrophyNode(reader));
                }
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private TeamColors? ParseTeamColorsNode(XmlReader reader)
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

        private Trophy ParseTrophyNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Trophy result = new Trophy
            {
                TrophyTypeId = uint.Parse(reader.ReadElementContentAsString()),
                TrophySeason = uint.Parse(reader.ReadElementContentAsString()),
                LeagueLevel = uint.Parse(reader.ReadElementContentAsString()),
                LeagueLevelUnitId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueLevelUnitName = reader.ReadElementContentAsString(),
                GainedDate = this.ParseDateTimeValue(reader.ReadElementContentAsString()),
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

        private SupportedTeam ParseSupportedTeamNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            SupportedTeam result = new SupportedTeam
            {
                UserId = uint.Parse(reader.ReadElementContentAsString()),
                LoginName = reader.ReadElementContentAsString(),
                TeamId = uint.Parse(reader.ReadElementContentAsString()),
                TeamName = reader.ReadElementContentAsString(),
                LeagueId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueName = reader.ReadElementContentAsString(),
                LeagueLevelUnitId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueLevelUnitName = reader.ReadElementContentAsString(),
                LastMatch = this.ParseLastMatchNode(reader),
                NextMatch = this.ParseNextMatchNode(reader),
            };

            if (reader.Name == pressAnnouncementNodeName)
            {
                result.PressAnnouncement = this.ParsePressAnnouncementNode(reader);
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Arena ParseArenaNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Arena result = new Arena
            {
                ArenaId = uint.Parse(reader.ReadElementContentAsString()),
                ArenaName = reader.ReadElementContentAsString(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private League ParseLeagueNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            League result = new League
            {
                LeagueId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueName = reader.ReadElementContentAsString(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private SupportedTeams ParseSupportedTeamsNode(XmlReader reader)
        {
            SupportedTeams result = new SupportedTeams
            {
                TotalItems = uint.Parse(reader.GetAttribute(totalItemsAttributeName) ?? "0"),
                MaxItems = uint.Parse(reader.GetAttribute(maxItemsAttributeName) ?? "0")
            };

            // Reads opening element.
            reader.Read();

            while (reader.Name == supportedTeamNodeName)
            {
                result.SupportedTeamList.Add(this.ParseSupportedTeamNode(reader));
            }

            // Reads opening element.
            reader.Read();

            return result;
        }

        private MySupporters ParseMySupportersNode(XmlReader reader)
        {
            MySupporters result = new MySupporters
            {
                TotalItems = uint.Parse(reader.GetAttribute(totalItemsAttributeName) ?? "0"),
                MaxItems = uint.Parse(reader.GetAttribute(maxItemsAttributeName) ?? "0")
            };

            // Reads opening element.
            reader.Read();

            while (reader.Name == supporterTeamNodeName)
            {
                result.SupporterTeamList.Add(this.ParseSupporterTeamNode(reader));
            }

            // Reads opening element.
            reader.Read();

            return result;
        }

        private Country ParseCountryNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Country result = new Country
            {
                CountryId = uint.Parse(reader.ReadElementContentAsString()),
                CountryName = reader.ReadElementContentAsString(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Region ParseRegionNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Region result = new Region
            {
                RegionId = uint.Parse(reader.ReadElementContentAsString()),
                RegionName = reader.ReadElementContentAsString(),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Trainer ParseTrainerNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Trainer result = new Trainer
            {
                PlayerId = uint.Parse(reader.ReadElementContentAsString()),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private LeagueLevelUnit ParseLeagueLevelUnitNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            LeagueLevelUnit result = new LeagueLevelUnit
            {
                LeagueLevelUnitId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueLevelUnitName = reader.ReadElementContentAsString(),
                LeagueLevel = uint.Parse(reader.ReadElementContentAsString())
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private BotStatus ParseBotStatusNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            BotStatus result = new BotStatus
            {
                IsBot = reader.ReadElementContentAsString() == bool.TrueString
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Cup? ParseCupNode(XmlReader reader)
        {
            Cup? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                reader.Read();

                result = new Cup
                {
                    StillInCup = reader.ReadElementContentAsString() == bool.TrueString
                };

                if (result.StillInCup)
                {
                    result.CupId = uint.Parse(reader.ReadElementContentAsString());
                    result.CupName = reader.ReadElementContentAsString();
                    result.CupLeagueLevel = uint.Parse(reader.ReadElementContentAsString());
                    result.CupLevel = uint.Parse(reader.ReadElementContentAsString());
                    result.CupLeagueLevelIndex = uint.Parse(reader.ReadElementContentAsString());
                    result.MatchRound = uint.Parse(reader.ReadElementContentAsString());
                    result.MatchRoundsLeft = uint.Parse(reader.ReadElementContentAsString());
                }
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Rating ParsePowerRatingNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Rating result = new Rating
            {
                GlobalRanking = uint.Parse(reader.ReadElementContentAsString()),
                LeagueRanking = uint.Parse(reader.ReadElementContentAsString()),
                RegionRanking = uint.Parse(reader.ReadElementContentAsString()),
                PowerRating = uint.Parse(reader.ReadElementContentAsString())
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Fanclub ParseFanclubNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Fanclub result = new Fanclub
            {
                FanclubId = uint.Parse(reader.ReadElementContentAsString()),
                FanclubName = reader.ReadElementContentAsString(),
                FanclubSize = uint.Parse(reader.ReadElementContentAsString())
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Guestbook ParseGuestbookNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Guestbook result = new Guestbook
            {
                NumberOfGuestbookItems = uint.Parse(reader.ReadElementContentAsString()),
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Flags ParseFlagsNode(XmlReader reader)
        {
            Flags result = new Flags();

            // Reads opening element.
            reader.Read();

            if (reader.Name == awayFlagsNodeName)
            {
                result.AwayFlags = this.ParseFlagListNode(reader);
            }

            if (reader.Name == homeFlagsNodeName)
            {
                result.HomeFlags = this.ParseFlagListNode(reader);
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private List<Flag> ParseFlagListNode(XmlReader reader)
        {
            List<Flag> result = new List<Flag>();

            // Reads opening element.
            reader.Read();

            while (reader.Name == flagNodeName)
            {
                result.Add(this.ParseFlagNode(reader));
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Flag ParseFlagNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Flag result = new Flag
            {
                LeagueId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueName = reader.ReadElementContentAsString(),
                CountryCode = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private PressAnnouncement ParsePressAnnouncementNode(XmlReader reader)
        {
            // Since there's multiple PressAnnouncement nodes with the same child elements but in different order, this mehtod is different than the rest.
            PressAnnouncement result = new PressAnnouncement();

            // Reads opening element.
            reader.Read();

            while (reader.Name != pressAnnouncementNodeName && reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.Name)
                {
                    case sendDateNodeName:
                    case pressAnnouncementSendDateNodeName:
                        result.SendDate = this.ParseDateTimeValue(reader.ReadElementContentAsString());
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

        private NextMatch ParseNextMatchNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            NextMatch result = new NextMatch
            {
                NextMatchId = uint.Parse(reader.ReadElementContentAsString()),
                NextMatchDate = this.ParseDateTimeValue(reader.ReadElementContentAsString()),
                NextMatchHomeTeamId = uint.Parse(reader.ReadElementContentAsString()),
                NextMatchHomeTeamName = reader.ReadElementContentAsString(),
                NextMatchAwayTeamId = uint.Parse(reader.ReadElementContentAsString()),
                NextMatchAwayTeamName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private LastMatch ParseLastMatchNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            LastMatch result = new LastMatch
            {
                LastMatchId = uint.Parse(reader.ReadElementContentAsString()),
                LastMatchDate = this.ParseDateTimeValue(reader.ReadElementContentAsString()),
                LastMatchHomeTeamId = uint.Parse(reader.ReadElementContentAsString()),
                LastMatchHomeTeamName = reader.ReadElementContentAsString(),
                LastMatchHomeGoals = uint.Parse(reader.ReadElementContentAsString()),
                LastMatchAwayTeamId = uint.Parse(reader.ReadElementContentAsString()),
                LastMatchAwayTeamName = reader.ReadElementContentAsString(),
                LastMatchAwayGoals = uint.Parse(reader.ReadElementContentAsString())
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private SupporterTeam ParseSupporterTeamNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            SupporterTeam result = new SupporterTeam
            {
                UserId = uint.Parse(reader.ReadElementContentAsString()),
                LoginName = reader.ReadElementContentAsString(),
                TeamId = uint.Parse(reader.ReadElementContentAsString()),
                TeamName = reader.ReadElementContentAsString(),
                LeagueId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueName = reader.ReadElementContentAsString(),
                LeagueLevelUnitId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueLevelUnitName = reader.ReadElementContentAsString()
            };


            // Reads closing element.
            reader.Read();

            return result;
        }
    }
}
