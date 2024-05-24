namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.TeamDetails;

    public class TeamDetails : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public TeamDetails(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.User = await ParseUserNodeAsync(reader);
            result.Teams = await ParseTeamsNodeAsync(reader);

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
                    result.CupId = await reader.ReadXmlValueAsLongAsync();
                    result.CupName = await reader.ReadElementContentAsStringAsync();
                    result.CupLeagueLevel = await reader.ReadXmlValueAsIntAsync();
                    result.CupLevel = await reader.ReadXmlValueAsIntAsync();
                    result.CupLeagueLevelIndex = await reader.ReadXmlValueAsIntAsync();
                    result.MatchRound = await reader.ReadXmlValueAsIntAsync();
                    result.MatchRoundsLeft = await reader.ReadXmlValueAsIntAsync();
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
                FanclubId = await reader.ReadXmlValueAsLongAsync(),
                FanclubName = await reader.ReadElementContentAsStringAsync(),
                FanclubSize = await reader.ReadXmlValueAsIntAsync()
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

            while (reader.CheckNode(NodeName.Flag))
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
                LeagueId = await reader.ReadXmlValueAsLongAsync(),
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

            if (reader.CheckNode(NodeName.AwayFlags))
            {
                result.AwayFlags = await ParseFlagListNodeAsync(reader);
            }

            if (reader.CheckNode(NodeName.HomeFlags))
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
                NumberOfGuestbookItems = await reader.ReadXmlValueAsIntAsync(),
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
                LastMatchId = await reader.ReadXmlValueAsLongAsync(),
                LastMatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                LastMatchHomeTeamId = await reader.ReadXmlValueAsLongAsync(),
                LastMatchHomeTeamName = await reader.ReadElementContentAsStringAsync(),
                LastMatchHomeGoals = await reader.ReadXmlValueAsIntAsync(),
                LastMatchAwayTeamId = await reader.ReadXmlValueAsLongAsync(),
                LastMatchAwayTeamName = await reader.ReadElementContentAsStringAsync(),
                LastMatchAwayGoals = await reader.ReadXmlValueAsIntAsync()
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
                LeagueLevelUnitId = await reader.ReadXmlValueAsLongAsync(),
                LeagueLevelUnitName = await reader.ReadElementContentAsStringAsync(),
                LeagueLevel = await reader.ReadXmlValueAsIntAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<MySupporters> ParseMySupportersNodeAsync(XmlReader reader)
        {
            MySupporters result = new MySupporters
            {
                TotalItems = byte.Parse(reader.GetAttribute(NodeName.TotalItems) ?? "0"),
                MaxItems = byte.Parse(reader.GetAttribute(NodeName.MaxItems) ?? "0")
            };

            // Reads opening element.
            await reader.ReadAsync();

            while (reader.CheckNode(NodeName.SupporterTeam))
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
                result.Index = byte.Parse(reader.GetAttribute(NodeName.Index) ?? "0");

                // Reads opening element.
                await reader.ReadAsync();

                result.NationalTeamStaffType = await reader.ReadXmlValueAsIntAsync();
                result.NationalTeamId = await reader.ReadXmlValueAsLongAsync();
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

                while (reader.CheckNode(NodeName.NationalTeam))
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
                NextMatchId = await reader.ReadXmlValueAsLongAsync(),
                NextMatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                NextMatchHomeTeamId = await reader.ReadXmlValueAsLongAsync(),
                NextMatchHomeTeamName = await reader.ReadElementContentAsStringAsync(),
                NextMatchAwayTeamId = await reader.ReadXmlValueAsLongAsync(),
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
                GlobalRanking = await reader.ReadXmlValueAsIntAsync(),
                LeagueRanking = await reader.ReadXmlValueAsIntAsync(),
                RegionRanking = await reader.ReadXmlValueAsIntAsync(),
                PowerRating = await reader.ReadXmlValueAsIntAsync()
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

            while (reader.Name != NodeName.PressAnnouncement && reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.Name)
                {
                    case NodeName.SendDate:
                    case NodeName.PressAnnouncementSendDate:
                        result.SendDate = await reader.ReadXmlValueAsDateTimeAsync();
                        break;

                    case NodeName.Body:
                    case NodeName.PressAnnouncementBody:
                        result.Body = await reader.ReadElementContentAsStringAsync();
                        break;

                    case NodeName.Subject:
                    case NodeName.PressAnnouncementSubject:
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

        private static async Task<SupportedTeam> ParseSupportedTeamNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            SupportedTeam result = new SupportedTeam
            {
                UserId = await reader.ReadXmlValueAsLongAsync(),
                LoginName = await reader.ReadElementContentAsStringAsync(),
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                LeagueId = await reader.ReadXmlValueAsLongAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
                LeagueLevelUnitId = await reader.ReadXmlValueAsLongAsync(),
                LeagueLevelUnitName = await reader.ReadElementContentAsStringAsync(),
                LastMatch = await ParseLastMatchNodeAsync(reader),
                NextMatch = await ParseNextMatchNodeAsync(reader),
            };

            if (reader.CheckNode(NodeName.PressAnnouncement))
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
                TotalItems = int.Parse(reader.GetAttribute(NodeName.TotalItems) ?? "0"),
                MaxItems = int.Parse(reader.GetAttribute(NodeName.MaxItems) ?? "0")
            };

            // Reads opening element.
            await reader.ReadAsync();

            while (reader.CheckNode(NodeName.SupportedTeam))
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
                UserId = await reader.ReadXmlValueAsLongAsync(),
                LoginName = await reader.ReadElementContentAsStringAsync(),
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                LeagueId = await reader.ReadXmlValueAsLongAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
                LeagueLevelUnitId = await reader.ReadXmlValueAsLongAsync(),
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
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                ShortTeamName = await reader.ReadElementContentAsStringAsync(),
                IsPrimaryClub = await reader.ReadXmlValueAsBoolAsync(),
                FoundedDate = await reader.ReadXmlValueAsDateTimeAsync(),
                Arena = await ParseIdNameNodeAsync(reader),
                League = await ParseIdNameNodeAsync(reader),
                Country = await ParseIdNameNodeAsync(reader),
                Region = await ParseIdNameNodeAsync(reader),
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
            result.FriendlyTeamId = string.IsNullOrWhiteSpace(auxValue) ? null : long.Parse(auxValue);

            auxValue = await reader.ReadElementContentAsStringAsync();
            result.NumberOfVictories = string.IsNullOrWhiteSpace(auxValue) ? null : short.Parse(auxValue);

            auxValue = await reader.ReadElementContentAsStringAsync();
            result.NumberOfUndefeated = string.IsNullOrWhiteSpace(auxValue) ? null : short.Parse(auxValue);

            auxValue = await reader.ReadElementContentAsStringAsync();
            result.TeamRank = string.IsNullOrWhiteSpace(auxValue) ? null : int.Parse(auxValue);

            result.Fanclub = await ParseFanclubNodeAsync(reader);
            result.LogoUrl = await reader.ReadElementContentAsStringAsync();

            if (reader.CheckNode(NodeName.Guestbook))
            {
                result.Guestbook = await ParseGuestbookNodeAsync(reader);
            }

            if (reader.CheckNode(NodeName.PressAnnouncement))
            {
                result.PressAnnouncement = await ParsePressAnnouncementNodeAsync(reader);
            }

            if (reader.CheckNode(NodeName.TeamColors))
            {
                result.TeamColors = await ParseTeamColorsNodeAsync(reader);
            }

            result.YouthTeamId = await reader.ReadXmlValueAsLongAsync();
            result.YouthTeamName = await reader.ReadElementContentAsStringAsync();
            result.NumberOfVisits = await reader.ReadXmlValueAsIntAsync();

            if (reader.CheckNode(NodeName.Flags))
            {
                result.Flags = await ParseFlagsNodeAsync(reader);
            }

            if (reader.CheckNode(NodeName.TrophyList))
            {
                result.TrophyList = await ParseTrophyListNodeAsync(reader);
            }

            if (reader.CheckNode(NodeName.SupportedTeams))
            {
                result.SupportedTeams = await ParseSupportedTeamsNodeAsync(reader);
            }

            if (reader.CheckNode(NodeName.MySupportes))
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

            while (reader.CheckNode(NodeName.Team))
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
                PlayerId = await reader.ReadXmlValueAsLongAsync(),
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

                while (reader.CheckNode(NodeName.Trophy))
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
                TrophyTypeId = await reader.ReadXmlValueAsLongAsync(),
                TrophySeason = await reader.ReadXmlValueAsIntAsync(),
                LeagueLevel = await reader.ReadXmlValueAsIntAsync(),
                LeagueLevelUnitId = await reader.ReadXmlValueAsLongAsync(),
                LeagueLevelUnitName = await reader.ReadElementContentAsStringAsync(),
                GainedDate = await reader.ReadXmlValueAsDateTimeAsync(),
            };

            if (reader.CheckNode(NodeName.ImageUrl))
            {
                result.ImageUrl = await reader.ReadElementContentAsStringAsync();
            }

            string auxValue = await reader.ReadElementContentAsStringAsync();
            result.CupLeagueLevel = string.IsNullOrWhiteSpace(auxValue) ? null : byte.Parse(auxValue);

            auxValue = await reader.ReadElementContentAsStringAsync();
            result.CupLevel = string.IsNullOrWhiteSpace(auxValue) ? null : byte.Parse(auxValue);

            auxValue = await reader.ReadElementContentAsStringAsync();
            result.CupLevelIndex = string.IsNullOrWhiteSpace(auxValue) ? null : byte.Parse(auxValue);

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
                UserId = await reader.ReadXmlValueAsLongAsync(),
                Language = await ParseIdNameNodeAsync(reader),
                SupporterTier = await reader.ReadElementContentAsStringAsync(),
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