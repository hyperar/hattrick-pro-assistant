namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.MatchDetails;
    using Application.Interfaces;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class MatchDetails : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string addedMinutesNodeName = "AddedMinutes";

        private const string awayGoalsNodeName = "AwayGoals";

        private const string bookingNodeName = "Booking";

        private const string bookingsNodeName = "Bookings";

        private const string dressURINodeName = "DressURI";

        private const string eventListNodeName = "EventList";

        private const string eventNodeName = "Event";

        private const string finishedDateNodeName = "FinishedDate";

        private const string formationNodeName = "Formation";

        private const string goalNodeName = "Goal";

        private const string homeGoalsNodeName = "HomeGoals";

        private const string indexAttributeName = "Index";

        private const string injuriesNodeName = "Injuries";

        private const string injuryNodeName = "Injury";

        private const string matchOfficialsNodeName = "MatchOfficials";

        private const string nrOfChancesCenterNodeName = "NrOfChancesCenter";

        private const string nrOfChancesLeftNodeName = "NrOfChancesLeft";

        private const string nrOfChancesOtherNodeName = "NrOfChancesOther";

        private const string nrOfChancesRightNodeName = "NrOfChancesRight";

        private const string nrOfChancesSpecialEventsNodeName = "NrOfChancesSpecialEvents";

        private const string possessionFirstHalfAwayNodeName = "PossessionFirstHalfAway";

        private const string possessionFirstHalfHomeNodeName = "PossessionFirstHalfHome";

        private const string possessionSecondHalfAwayNodeName = "PossessionSecondHalfAway";

        private const string possessionSecondHalfHomeNodeName = "PossessionSecondHalfHome";

        private const string ratingIndirectSetPiecesAttNodeName = "RatingIndirectSetPiecesAtt";

        private const string ratingIndirectSetPiecesDefNodeName = "RatingIndirectSetPiecesDef";

        private const string ratingLeftAttNodeName = "RatingLeftAtt";

        private const string ratingLeftDefNodeName = "RatingLeftDef";

        private const string ratingMidAttNodeName = "RatingMidAtt";

        private const string ratingMidDefNodeName = "RatingMidDef";

        private const string ratingMidfieldNodeName = "RatingMidfield";

        private const string ratingRightAttNodeName = "RatingRightAtt";

        private const string ratingRightDefNodeName = "RatingRightDef";

        private const string scorersNodeName = "Scorers";

        private const string soldBasicNodeName = "SoldBasic";

        private const string soldRoofNodeName = "SoldRoof";

        private const string soldTerracesNodeName = "SoldTerraces";

        private const string soldTotalNodeName = "SoldTotal";

        private const string soldVipNodeName = "SoldVip";

        private const string tacticSkillNodeName = "TacticSkill";

        private const string tacticTypeNodeName = "TacticType";

        private const string teamAttitudeNodeName = "TeamAttitude";

        private const string weatherIdNodeName = "WeatherId";

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.UserSupporterTier = await reader.ReadElementContentAsStringAsync();
            result.SourceSystem = await reader.ReadElementContentAsStringAsync();
            result.Match = await ParseMatchNodeAsync(reader);

            return result;
        }

        private static async Task<Arena> ParseArenaNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            var result = new Arena
            {
                ArenaId = await reader.ReadXmlValueAsUintAsync(),
                ArenaName = await reader.ReadElementContentAsStringAsync(),
                WeatherId = reader.Name == weatherIdNodeName ? await reader.ReadXmlValueAsWeatherAsync() : null,
                SoldTotal = reader.Name == soldTotalNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                SoldTerraces = reader.Name == soldTerracesNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                SoldBasic = reader.Name == soldBasicNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                SoldRoof = reader.Name == soldRoofNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                SoldVip = reader.Name == soldVipNodeName ? await reader.ReadXmlValueAsUintAsync() : null
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Booking>> ParseBookingListNodeAsync(XmlReader reader)
        {
            var result = new List<Booking>();

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                while (reader.Name == bookingNodeName)
                {
                    result.Add(
                        await ParseBookingNodeAsync(reader));
                }
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Booking> ParseBookingNodeAsync(XmlReader reader)
        {
            var result = new Booking
            {
                Index = uint.Parse(reader.GetAttribute(indexAttributeName) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.BookingPlayerId = await reader.ReadXmlValueAsUintAsync();
            result.BookingPlayerName = await reader.ReadElementContentAsStringAsync();
            result.BookingTeamId = await reader.ReadXmlValueAsUintAsync();
            result.BookingType = await reader.ReadXmlValueAsBookingTypeAsync();
            result.BookingMinute = await reader.ReadXmlValueAsUintAsync();
            result.MatchPart = await reader.ReadXmlValueAsMatchPartAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Event>> ParseEventListNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new List<Event>();

            while (reader.Name == eventNodeName)
            {
                result.Add(
                    await ParseEventNodeAsync(reader));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Event> ParseEventNodeAsync(XmlReader reader)
        {
            var result = new Event
            {
                Index = uint.Parse(reader.GetAttribute(indexAttributeName) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.Minute = await reader.ReadXmlValueAsUintAsync();
            result.SubjectPlayerId = await reader.ReadXmlValueAsUintAsync();
            result.SubjectTeamId = await reader.ReadXmlValueAsUintAsync();
            result.ObjectPlayerId = await reader.ReadXmlValueAsUintAsync();
            result.MatchPart = await reader.ReadXmlValueAsMatchPartAsync();
            result.EventTypeId = await reader.ReadXmlValueAsUintAsync();
            result.EventVariation = await reader.ReadXmlValueAsUintAsync();
            result.EventText = await reader.ReadElementContentAsStringAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Goal>> ParseGoalListNodeAsync(XmlReader reader)
        {
            var result = new List<Goal>();

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                while (reader.Name == goalNodeName)
                {
                    result.Add(
                        await ParseGoalNodeAsync(reader));
                }
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Goal> ParseGoalNodeAsync(XmlReader reader)
        {
            var result = new Goal
            {
                Index = uint.Parse(reader.GetAttribute(indexAttributeName) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.ScorerPlayerId = await reader.ReadXmlValueAsUintAsync();
            result.ScorerPlayerName = await reader.ReadElementContentAsStringAsync();
            result.ScorerTeamId = await reader.ReadXmlValueAsUintAsync();
            result.ScorerHomeGoals = await reader.ReadXmlValueAsUintAsync();
            result.ScorerAwayGoals = await reader.ReadXmlValueAsUintAsync();
            result.ScorerMinute = await reader.ReadXmlValueAsUintAsync();
            result.MatchPart = await reader.ReadXmlValueAsMatchPartAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Injury>> ParseInjuryListNodeAsync(XmlReader reader)
        {
            var result = new List<Injury>();

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                while (reader.Name == injuryNodeName)
                {
                    result.Add(
                        await ParseInjuryNodeAsync(reader));
                }
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Injury> ParseInjuryNodeAsync(XmlReader reader)
        {
            var result = new Injury
            {
                Index = uint.Parse(reader.GetAttribute(indexAttributeName) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.InjuryPlayerId = await reader.ReadXmlValueAsUintAsync();
            result.InjuryPlayerName = await reader.ReadElementContentAsStringAsync();
            result.InjuryTeamId = await reader.ReadXmlValueAsUintAsync();
            result.InjuryType = await reader.ReadXmlValueAsInjuryTypeAsync();
            result.InjuryMinute = await reader.ReadXmlValueAsUintAsync();
            result.MatchPart = await reader.ReadXmlValueAsMatchPartAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Match> ParseMatchNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            var result = new Match
            {
                MatchId = await reader.ReadXmlValueAsUintAsync(),
                MatchType = await reader.ReadXmlValueAsMatchTypeAsync(),
                MatchContextId = await reader.ReadXmlValueAsUintAsync(),
                MatchRuleId = await reader.ReadXmlValueAsMatchRuleAsync(),
                CupLevel = await reader.ReadXmlValueAsUintAsync(),
                CupLevelIndex = await reader.ReadXmlValueAsUintAsync(),
                MatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                FinishedDate = reader.Name == finishedDateNodeName ? await reader.ReadXmlValueAsDateTimeAsync() : null,
                AddedMinutes = reader.Name == addedMinutesNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                HomeTeam = await ParseTeamNodeAsync(reader),
                AwayTeam = await ParseTeamNodeAsync(reader),
                Arena = await ParseArenaNodeAsync(reader),
                MatchOfficials = reader.Name == matchOfficialsNodeName ? await ParseMatchOfficialsNodeAsync(reader) : null,
                Scorers = reader.Name == scorersNodeName ? await ParseGoalListNodeAsync(reader) : null,
                Bookings = reader.Name == bookingsNodeName ? await ParseBookingListNodeAsync(reader) : null,
                Injuries = reader.Name == injuriesNodeName ? await ParseInjuryListNodeAsync(reader) : null,
                PossessionFirstHalfHome = reader.Name == possessionFirstHalfHomeNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                PossessionFirstHalfAway = reader.Name == possessionFirstHalfAwayNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                PossessionSecondHalfHome = reader.Name == possessionSecondHalfHomeNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                PossessionSecondHalfAway = reader.Name == possessionSecondHalfAwayNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                EventList = reader.Name == eventListNodeName ? await ParseEventListNodeAsync(reader) : null
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<MatchOfficials> ParseMatchOfficialsNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            var result = new MatchOfficials
            {
                Referee = await ParseRefereeNodeAsync(reader),
                RefereeAssistant1 = await ParseRefereeNodeAsync(reader),
                RefereeAssistant2 = await ParseRefereeNodeAsync(reader),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Referee> ParseRefereeNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            var result = new Referee
            {
                RefereeId = await reader.ReadXmlValueAsUintAsync(),
                RefereeName = await reader.ReadElementContentAsStringAsync(),
                RefereeCountryId = await reader.ReadXmlValueAsUintAsync(),
                RefereeCountryName = await reader.ReadElementContentAsStringAsync(),
                RefereeTeamId = await reader.ReadXmlValueAsUintAsync(),
                RefereeTeamName = await reader.ReadElementContentAsStringAsync(),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            var result = new Team
            {
                TeamId = await reader.ReadXmlValueAsUintAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                DressUri = reader.Name == dressURINodeName ? await reader.ReadElementContentAsStringAsync() : null,
                Formation = reader.Name == formationNodeName ? await reader.ReadElementContentAsStringAsync() : null,
                Goals = reader.Name == awayGoalsNodeName || reader.Name == homeGoalsNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                TacticType = reader.Name == tacticTypeNodeName ? await reader.ReadXmlValueAsMatchTacticTypeAsync() : null,
                TacticSkill = reader.Name == tacticSkillNodeName ? await reader.ReadXmlValueAsSkillLevelAsync() : null,
                RatingMidfield = reader.Name == ratingMidfieldNodeName ? await reader.ReadXmlValueAsMatchSectorRatingAsync() : null,
                RatingRightDef = reader.Name == ratingRightDefNodeName ? await reader.ReadXmlValueAsMatchSectorRatingAsync() : null,
                RatingMidDef = reader.Name == ratingMidDefNodeName ? await reader.ReadXmlValueAsMatchSectorRatingAsync() : null,
                RatingLeftDef = reader.Name == ratingLeftDefNodeName ? await reader.ReadXmlValueAsMatchSectorRatingAsync() : null,
                RatingRightAtt = reader.Name == ratingRightAttNodeName ? await reader.ReadXmlValueAsMatchSectorRatingAsync() : null,
                RatingMidAtt = reader.Name == ratingMidAttNodeName ? await reader.ReadXmlValueAsMatchSectorRatingAsync() : null,
                RatingLeftAtt = reader.Name == ratingLeftAttNodeName ? await reader.ReadXmlValueAsMatchSectorRatingAsync() : null,
                TeamAttitude = reader.Name == teamAttitudeNodeName ? await reader.ReadXmlValueAsMatchTeamAttitudeAsync() : null,
                RatingSetPiecesDef = reader.Name == ratingIndirectSetPiecesDefNodeName ? await reader.ReadXmlValueAsMatchSectorRatingAsync() : null,
                RatingSetPiecesAtt = reader.Name == ratingIndirectSetPiecesAttNodeName ? await reader.ReadXmlValueAsMatchSectorRatingAsync() : null,
                NrOfChancesLeft = reader.Name == nrOfChancesLeftNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                NrOfChancesCenter = reader.Name == nrOfChancesCenterNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                NrOfChancesRight = reader.Name == nrOfChancesRightNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                NrOfChancesSpecialEvents = reader.Name == nrOfChancesSpecialEventsNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
                NrOfChancesOther = reader.Name == nrOfChancesOtherNodeName ? await reader.ReadXmlValueAsUintAsync() : null,
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}