namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Collections.Generic;
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.TeamDetails;
    using Application.Interfaces;
    using Common.Enums;
    using Common.ExtensionMethods;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class TeamDetails : XmlFileParserBase, IXmlFileParserStrategy
    {
        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.User = await ParseUserNodeAsync(reader);
            result.Teams = await ParseTeamsNodeAsync(reader);

            return result;
        }

        private static async Task<Arena> ParseArenaNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Arena result = new Arena
            {
                ArenaId = await reader.ReadXmlValueAsUintAsync(),
                ArenaName = await reader.ReadElementContentAsStringAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<BotStatus> ParseBotStatusNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            BotStatus result = new BotStatus
            {
                IsBot = await reader.ReadXmlValueAsBoolAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Country> ParseCountryNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Country result = new Country
            {
                CountryId = await reader.ReadXmlValueAsUintAsync(),
                CountryName = await reader.ReadElementContentAsStringAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Cup?> ParseCupNodeAsync(XmlReader reader)
        {
            Cup? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                await reader.ReadAsync();

                result = new Cup
                {
                    StillInCup = await reader.ReadXmlValueAsBoolAsync()
                };

                if (result.StillInCup)
                {
                    result.CupId = await reader.ReadXmlValueAsUintAsync();
                    result.CupName = await reader.ReadElementContentAsStringAsync();
                    result.CupLeagueLevel = await reader.ReadXmlValueAsUintAsync();
                    result.CupLevel = await reader.ReadXmlValueAsUintAsync();
                    result.CupLeagueLevelIndex = await reader.ReadXmlValueAsUintAsync();
                    result.MatchRound = await reader.ReadXmlValueAsUintAsync();
                    result.MatchRoundsLeft = await reader.ReadXmlValueAsUintAsync();
                }
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Fanclub> ParseFanclubNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Fanclub result = new Fanclub
            {
                FanclubId = await reader.ReadXmlValueAsUintAsync(),
                FanclubName = await reader.ReadElementContentAsStringAsync(),
                FanclubSize = await reader.ReadXmlValueAsUintAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Flag>> ParseFlagListNodeAsync(XmlReader reader)
        {
            List<Flag> result = new List<Flag>();

            // Reads opening element.
            await reader.ReadAsync();

            while (reader.CheckNode(Constants.NodeName.Flag))
            {
                result.Add(await ParseFlagNodeAsync(reader));
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Flag> ParseFlagNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Flag result = new Flag
            {
                LeagueId = await reader.ReadXmlValueAsUintAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
                CountryCode = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Flags> ParseFlagsNodeAsync(XmlReader reader)
        {
            Flags result = new Flags();

            // Reads opening element.
            await reader.ReadAsync();

            if (reader.CheckNode(Constants.NodeName.AwayFlags))
            {
                result.AwayFlags = await ParseFlagListNodeAsync(reader);
            }

            if (reader.CheckNode(Constants.NodeName.HomeFlags))
            {
                result.HomeFlags = await ParseFlagListNodeAsync(reader);
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Guestbook> ParseGuestbookNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Guestbook result = new Guestbook
            {
                NumberOfGuestbookItems = await reader.ReadXmlValueAsUintAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Language> ParseLanguageNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Language result = new Language
            {
                LanguageId = await reader.ReadXmlValueAsUintAsync(),
                LanguageName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<LastMatch> ParseLastMatchNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            LastMatch result = new LastMatch
            {
                LastMatchId = await reader.ReadXmlValueAsUintAsync(),
                LastMatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                LastMatchHomeTeamId = await reader.ReadXmlValueAsUintAsync(),
                LastMatchHomeTeamName = await reader.ReadElementContentAsStringAsync(),
                LastMatchHomeGoals = await reader.ReadXmlValueAsUintAsync(),
                LastMatchAwayTeamId = await reader.ReadXmlValueAsUintAsync(),
                LastMatchAwayTeamName = await reader.ReadElementContentAsStringAsync(),
                LastMatchAwayGoals = await reader.ReadXmlValueAsUintAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<LeagueLevelUnit> ParseLeagueLevelUnitNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            LeagueLevelUnit result = new LeagueLevelUnit
            {
                LeagueLevelUnitId = await reader.ReadXmlValueAsUintAsync(),
                LeagueLevelUnitName = await reader.ReadElementContentAsStringAsync(),
                LeagueLevel = await reader.ReadXmlValueAsUintAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<League> ParseLeagueNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            League result = new League
            {
                LeagueId = await reader.ReadXmlValueAsUintAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<MySupporters> ParseMySupportersNodeAsync(XmlReader reader)
        {
            MySupporters result = new MySupporters
            {
                TotalItems = uint.Parse(reader.GetAttribute(Constants.NodeName.TotalItems) ?? "0"),
                MaxItems = uint.Parse(reader.GetAttribute(Constants.NodeName.MaxItems) ?? "0")
            };

            // Reads opening element.
            await reader.ReadAsync();

            while (reader.CheckNode(Constants.NodeName.SupporterTeam))
            {
                result.SupporterTeamList.Add(await ParseSupporterTeamNodeAsync(reader));
            }

            // Reads opening element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<NationalTeam> ParseNationalTeamNodeAsync(XmlReader reader)
        {
            NationalTeam result = new NationalTeam();

            if (!reader.IsEmptyElement)
            {
                result.Index = uint.Parse(reader.GetAttribute(Constants.NodeName.Index) ?? "0");

                // Reads opening element.
                await reader.ReadAsync();

                result.NationalTeamStaffType = (NationalTeamStaffType)await reader.ReadXmlValueAsUintAsync();
                result.NationalTeamId = await reader.ReadXmlValueAsUintAsync();
                result.NationalTeamName = await reader.ReadElementContentAsStringAsync();
            }

            // Reads opening element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<NationalTeam>?> ParseNationalTeamsNodeAsync(XmlReader reader)
        {
            List<NationalTeam>? result = null;

            if (!reader.IsEmptyElement)
            {
                result = new List<NationalTeam>();

                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(Constants.NodeName.NationalTeam))
                {
                    result.Add(await ParseNationalTeamNodeAsync(reader));
                }
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<NextMatch> ParseNextMatchNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            NextMatch result = new NextMatch
            {
                NextMatchId = await reader.ReadXmlValueAsUintAsync(),
                NextMatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                NextMatchHomeTeamId = await reader.ReadXmlValueAsUintAsync(),
                NextMatchHomeTeamName = await reader.ReadElementContentAsStringAsync(),
                NextMatchAwayTeamId = await reader.ReadXmlValueAsUintAsync(),
                NextMatchAwayTeamName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Rating> ParsePowerRatingNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Rating result = new Rating
            {
                GlobalRanking = await reader.ReadXmlValueAsUintAsync(),
                LeagueRanking = await reader.ReadXmlValueAsUintAsync(),
                RegionRanking = await reader.ReadXmlValueAsUintAsync(),
                PowerRating = await reader.ReadXmlValueAsUintAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<PressAnnouncement> ParsePressAnnouncementNodeAsync(XmlReader reader)
        {
            // Since there's multiple PressAnnouncement nodes with the same child elements but in different order, this mehtod is different than the rest.
            PressAnnouncement result = new PressAnnouncement();

            // Reads opening element.
            await reader.ReadAsync();

            while (reader.Name != Constants.NodeName.PressAnnouncement && reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.Name)
                {
                    case Constants.NodeName.SendDate:
                    case Constants.NodeName.PressAnnouncementSendDate:
                        result.SendDate = await reader.ReadXmlValueAsDateTimeAsync();
                        break;

                    case Constants.NodeName.Body:
                    case Constants.NodeName.PressAnnouncementBody:
                        result.Body = await reader.ReadElementContentAsStringAsync();
                        break;

                    case Constants.NodeName.Subject:
                    case Constants.NodeName.PressAnnouncementSubject:
                        result.Subject = await reader.ReadElementContentAsStringAsync();
                        break;

                    default:
                        await reader.ReadAsync();
                        break;
                }
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Region> ParseRegionNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Region result = new Region
            {
                RegionId = await reader.ReadXmlValueAsUintAsync(),
                RegionName = await reader.ReadElementContentAsStringAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<SupportedTeam> ParseSupportedTeamNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            SupportedTeam result = new SupportedTeam
            {
                UserId = await reader.ReadXmlValueAsUintAsync(),
                LoginName = await reader.ReadElementContentAsStringAsync(),
                TeamId = await reader.ReadXmlValueAsUintAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                LeagueId = await reader.ReadXmlValueAsUintAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
                LeagueLevelUnitId = await reader.ReadXmlValueAsUintAsync(),
                LeagueLevelUnitName = await reader.ReadElementContentAsStringAsync(),
                LastMatch = await ParseLastMatchNodeAsync(reader),
                NextMatch = await ParseNextMatchNodeAsync(reader),
            };

            if (reader.CheckNode(Constants.NodeName.PressAnnouncement))
            {
                result.PressAnnouncement = await ParsePressAnnouncementNodeAsync(reader);
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<SupportedTeams> ParseSupportedTeamsNodeAsync(XmlReader reader)
        {
            SupportedTeams result = new SupportedTeams
            {
                TotalItems = uint.Parse(reader.GetAttribute(Constants.NodeName.TotalItems) ?? "0"),
                MaxItems = uint.Parse(reader.GetAttribute(Constants.NodeName.MaxItems) ?? "0")
            };

            // Reads opening element.
            await reader.ReadAsync();

            while (reader.CheckNode(Constants.NodeName.SupportedTeam))
            {
                result.SupportedTeamList.Add(await ParseSupportedTeamNodeAsync(reader));
            }

            // Reads opening element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<SupporterTeam> ParseSupporterTeamNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            SupporterTeam result = new SupporterTeam
            {
                UserId = await reader.ReadXmlValueAsUintAsync(),
                LoginName = await reader.ReadElementContentAsStringAsync(),
                TeamId = await reader.ReadXmlValueAsUintAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                LeagueId = await reader.ReadXmlValueAsUintAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
                LeagueLevelUnitId = await reader.ReadXmlValueAsUintAsync(),
                LeagueLevelUnitName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<TeamColors?> ParseTeamColorsNodeAsync(XmlReader reader)
        {
            TeamColors? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                await reader.ReadAsync();

                result = new TeamColors
                {
                    BackgroundColor = await reader.ReadElementContentAsStringAsync(),
                    Color = await reader.ReadElementContentAsStringAsync(),
                };
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
                TeamId = await reader.ReadXmlValueAsUintAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                ShortTeamName = await reader.ReadElementContentAsStringAsync(),
                IsPrimaryClub = await reader.ReadXmlValueAsBoolAsync(),
                FoundedDate = await reader.ReadXmlValueAsDateTimeAsync(),
                Arena = await ParseArenaNodeAsync(reader),
                League = await ParseLeagueNodeAsync(reader),
                Country = await ParseCountryNodeAsync(reader),
                Region = await ParseRegionNodeAsync(reader),
                Trainer = await ParseTrainerNodeAsync(reader),
                HomePage = await reader.ReadElementContentAsStringAsync(),
                DressUri = await reader.ReadElementContentAsStringAsync(),
                DressAlternateUri = await reader.ReadElementContentAsStringAsync(),
                LeagueLevelUnit = await ParseLeagueLevelUnitNodeAsync(reader),
                BotStatus = await ParseBotStatusNodeAsync(reader),
                Cup = await ParseCupNodeAsync(reader),
                PowerRating = await ParsePowerRatingNodeAsync(reader)
            };

            // If the team is from Hattrick Special Leagues, these next elements are empty.
            string auxValue = await reader.ReadElementContentAsStringAsync();
            result.FriendlyTeamId = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            auxValue = await reader.ReadElementContentAsStringAsync();
            result.NumberOfVictories = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            auxValue = await reader.ReadElementContentAsStringAsync();
            result.NumberOfUndefeated = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            auxValue = await reader.ReadElementContentAsStringAsync();
            result.TeamRank = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            result.Fanclub = await ParseFanclubNodeAsync(reader);
            result.LogoUrl = await reader.ReadElementContentAsStringAsync();

            if (reader.CheckNode(Constants.NodeName.Guestbook))
            {
                result.Guestbook = await ParseGuestbookNodeAsync(reader);
            }

            if (reader.CheckNode(Constants.NodeName.PressAnnouncement))
            {
                result.PressAnnouncement = await ParsePressAnnouncementNodeAsync(reader);
            }

            if (reader.CheckNode(Constants.NodeName.TeamColors))
            {
                result.TeamColors = await ParseTeamColorsNodeAsync(reader);
            }

            result.YouthTeamId = await reader.ReadXmlValueAsUintAsync();
            result.YouthTeamName = await reader.ReadElementContentAsStringAsync();
            result.NumberOfVisits = await reader.ReadXmlValueAsUintAsync();

            if (reader.CheckNode(Constants.NodeName.Flags))
            {
                result.Flags = await ParseFlagsNodeAsync(reader);
            }

            if (reader.CheckNode(Constants.NodeName.TrophyList))
            {
                result.TrophyList = await ParseTrophyListNodeAsync(reader);
            }

            if (reader.CheckNode(Constants.NodeName.SupportedTeams))
            {
                result.SupportedTeams = await ParseSupportedTeamsNodeAsync(reader);
            }

            if (reader.CheckNode(Constants.NodeName.MySupportes))
            {
                result.MySupporters = await ParseMySupportersNodeAsync(reader);
            }

            result.PossibleToChallengeMidweek = await reader.ReadXmlValueAsBoolAsync();
            result.PossibleToChallengeWeekend = await reader.ReadXmlValueAsBoolAsync();

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Team>> ParseTeamsNodeAsync(XmlReader reader)
        {
            List<Team> result = new List<Team>();

            // Reads opening element.
            await reader.ReadAsync();

            while (reader.CheckNode(Constants.NodeName.Team))
            {
                result.Add(await ParseTeamNodeAsync(reader));
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Trainer> ParseTrainerNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Trainer result = new Trainer
            {
                PlayerId = await reader.ReadXmlValueAsUintAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Trophy>?> ParseTrophyListNodeAsync(XmlReader reader)
        {
            List<Trophy>? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                await reader.ReadAsync();

                result = new List<Trophy>();

                while (reader.CheckNode(Constants.NodeName.Trophy))
                {
                    result.Add(await ParseTrophyNodeAsync(reader));
                }
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Trophy> ParseTrophyNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Trophy result = new Trophy
            {
                TrophyTypeId = await reader.ReadXmlValueAsUintAsync(),
                TrophySeason = await reader.ReadXmlValueAsUintAsync(),
                LeagueLevel = await reader.ReadXmlValueAsUintAsync(),
                LeagueLevelUnitId = await reader.ReadXmlValueAsUintAsync(),
                LeagueLevelUnitName = await reader.ReadElementContentAsStringAsync(),
                GainedDate = await reader.ReadXmlValueAsDateTimeAsync(),
            };

            if (reader.CheckNode(Constants.NodeName.ImageUrl))
            {
                result.ImageUrl = await reader.ReadElementContentAsStringAsync();
            }

            string auxValue = await reader.ReadElementContentAsStringAsync();
            result.CupLeagueLevel = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            auxValue = await reader.ReadElementContentAsStringAsync();
            result.CupLevel = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            auxValue = await reader.ReadElementContentAsStringAsync();
            result.CupLevelIndex = string.IsNullOrWhiteSpace(auxValue) ? null : uint.Parse(auxValue);

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<User> ParseUserNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            User result = new User
            {
                UserId = await reader.ReadXmlValueAsUintAsync(),
                Language = await ParseLanguageNodeAsync(reader),
                SupporterTier = (await reader.ReadElementContentAsStringAsync()).ToSupporterTier(),
                LoginName = await reader.ReadElementContentAsStringAsync(),
                Name = await reader.ReadElementContentAsStringAsync(),
                Icq = await reader.ReadElementContentAsStringAsync(),
                SignUpDate = await reader.ReadXmlValueAsDateTimeAsync(),
                ActivationDate = await reader.ReadXmlValueAsDateTimeAsync(),
                LastLoginDate = await reader.ReadXmlValueAsDateTimeAsync(),
                HasManagerLicense = await reader.ReadXmlValueAsBoolAsync(),
                NationalTeams = await ParseNationalTeamsNodeAsync(reader)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}