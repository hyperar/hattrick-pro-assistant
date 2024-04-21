namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.MatchDetails;

    public class MatchDetails : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public MatchDetails(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.UserSupporterTier = await reader.ReadElementContentAsStringAsync();
            result.SourceSystem = await reader.ReadElementContentAsStringAsync();
            result.Match = await ParseMatchNodeAsync(reader);

            return result;
        }

        private static async Task<Arena> ParseArenaNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            Arena result = new Arena
            {
                ArenaId = await reader.ReadXmlValueAsLongAsync(),
                ArenaName = await reader.ReadElementContentAsStringAsync(),
                WeatherId = reader.CheckNode(NodeName.WeatherId) ? await reader.ReadXmlValueAsByteAsync() : null,
                SoldTotal = reader.CheckNode(NodeName.SoldTotal) ? await reader.ReadXmlValueAsIntAsync() : null,
                SoldTerraces = reader.CheckNode(NodeName.SoldTerraces) ? await reader.ReadXmlValueAsIntAsync() : null,
                SoldBasic = reader.CheckNode(NodeName.SoldBasic) ? await reader.ReadXmlValueAsIntAsync() : null,
                SoldRoof = reader.CheckNode(NodeName.SoldRoof) ? await reader.ReadXmlValueAsIntAsync() : null,
                SoldVip = reader.CheckNode(NodeName.SoldVip) ? await reader.ReadXmlValueAsIntAsync() : null
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Booking>> ParseBookingListNodeAsync(XmlReader reader)
        {
            List<Booking> result = new List<Booking>();

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Booking))
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
            Booking result = new Booking
            {
                Index = byte.Parse(reader.GetAttribute(NodeName.Index) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.BookingPlayerId = await reader.ReadXmlValueAsLongAsync();
            result.BookingPlayerName = await reader.ReadElementContentAsStringAsync();
            result.BookingTeamId = await reader.ReadXmlValueAsLongAsync();
            result.BookingType = await reader.ReadXmlValueAsByteAsync();
            result.BookingMinute = await reader.ReadXmlValueAsByteAsync();
            result.MatchPart = await reader.ReadXmlValueAsByteAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Event>> ParseEventListNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            List<Event> result = new List<Event>();

            while (reader.CheckNode(NodeName.Event))
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
            Event result = new Event
            {
                Index = byte.Parse(reader.GetAttribute(NodeName.Index) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.Minute = await reader.ReadXmlValueAsByteAsync();
            result.SubjectPlayerId = await reader.ReadXmlValueAsLongAsync();
            result.SubjectTeamId = await reader.ReadXmlValueAsLongAsync();
            result.ObjectPlayerId = await reader.ReadXmlValueAsLongAsync();
            result.MatchPart = await reader.ReadXmlValueAsByteAsync();
            result.EventTypeId = await reader.ReadXmlValueAsShortAsync();
            result.EventVariation = await reader.ReadXmlValueAsShortAsync();
            result.EventText = await reader.ReadElementContentAsStringAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Goal>> ParseGoalListNodeAsync(XmlReader reader)
        {
            List<Goal> result = new List<Goal>();

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Goal))
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
            Goal result = new Goal
            {
                Index = byte.Parse(reader.GetAttribute(NodeName.Index) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.ScorerPlayerId = await reader.ReadXmlValueAsLongAsync();
            result.ScorerPlayerName = await reader.ReadElementContentAsStringAsync();
            result.ScorerTeamId = await reader.ReadXmlValueAsLongAsync();
            result.ScorerHomeGoals = await reader.ReadXmlValueAsByteAsync();
            result.ScorerAwayGoals = await reader.ReadXmlValueAsByteAsync();
            result.ScorerMinute = await reader.ReadXmlValueAsByteAsync();
            result.MatchPart = await reader.ReadXmlValueAsByteAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Injury>> ParseInjuryListNodeAsync(XmlReader reader)
        {
            List<Injury> result = new List<Injury>();

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Injury))
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
            Injury result = new Injury
            {
                Index = byte.Parse(reader.GetAttribute(NodeName.Index) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.InjuryPlayerId = await reader.ReadXmlValueAsLongAsync();
            result.InjuryPlayerName = await reader.ReadElementContentAsStringAsync();
            result.InjuryTeamId = await reader.ReadXmlValueAsLongAsync();
            result.InjuryType = await reader.ReadXmlValueAsByteAsync();
            result.InjuryMinute = await reader.ReadXmlValueAsByteAsync();
            result.MatchPart = await reader.ReadXmlValueAsByteAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Match> ParseMatchNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            Match result = new Match
            {
                MatchId = await reader.ReadXmlValueAsLongAsync(),
                MatchType = await reader.ReadXmlValueAsByteAsync(),
                MatchContextId = await reader.ReadXmlValueAsLongAsync(),
                MatchRuleId = await reader.ReadXmlValueAsByteAsync(),
                CupLevel = await reader.ReadXmlValueAsByteAsync(),
                CupLevelIndex = await reader.ReadXmlValueAsByteAsync(),
                MatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                FinishedDate = reader.CheckNode(NodeName.FinishedDate) ? await reader.ReadXmlValueAsDateTimeAsync() : null,
                AddedMinutes = reader.CheckNode(NodeName.AddedMinutes) ? await reader.ReadXmlValueAsByteAsync() : null,
                HomeTeam = await ParseTeamNodeAsync(reader),
                AwayTeam = await ParseTeamNodeAsync(reader),
                Arena = await ParseArenaNodeAsync(reader),
                MatchOfficials = reader.CheckNode(NodeName.MatchOfficials) ? await ParseMatchOfficialsNodeAsync(reader) : null,
                Scorers = reader.CheckNode(NodeName.Scorers) ? await ParseGoalListNodeAsync(reader) : null,
                Bookings = reader.CheckNode(NodeName.Bookings) ? await ParseBookingListNodeAsync(reader) : null,
                Injuries = reader.CheckNode(NodeName.Injuries) ? await ParseInjuryListNodeAsync(reader) : null,
                PossessionFirstHalfHome = reader.CheckNode(NodeName.PossessionFirstHalfHome) ? await reader.ReadXmlValueAsByteAsync() : null,
                PossessionFirstHalfAway = reader.CheckNode(NodeName.PossessionFirstHalfAway) ? await reader.ReadXmlValueAsByteAsync() : null,
                PossessionSecondHalfHome = reader.CheckNode(NodeName.PossessionSecondHalfHome) ? await reader.ReadXmlValueAsByteAsync() : null,
                PossessionSecondHalfAway = reader.CheckNode(NodeName.PossessionSecondHalfAway) ? await reader.ReadXmlValueAsByteAsync() : null,
                EventList = reader.CheckNode(NodeName.EventList) ? await ParseEventListNodeAsync(reader) : null
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<MatchOfficials> ParseMatchOfficialsNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            MatchOfficials result = new MatchOfficials
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

            Referee result = new Referee
            {
                RefereeId = await reader.ReadXmlValueAsLongAsync(),
                RefereeName = await reader.ReadElementContentAsStringAsync(),
                RefereeCountryId = await reader.ReadXmlValueAsLongAsync(),
                RefereeCountryName = await reader.ReadElementContentAsStringAsync(),
                RefereeTeamId = await reader.ReadXmlValueAsLongAsync(),
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

            Team result = new Team
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                DressUri = reader.CheckNode(NodeName.DressURI) ? await reader.ReadElementContentAsStringAsync() : null,
                Formation = reader.CheckNode(NodeName.Formation) ? await reader.ReadElementContentAsStringAsync() : null,
                Goals = reader.CheckNode(NodeName.AwayGoals, NodeName.HomeGoals) ? await reader.ReadXmlValueAsByteAsync() : null,
                TacticType = reader.CheckNode(NodeName.TacticType) ? await reader.ReadXmlValueAsByteAsync() : null,
                TacticSkill = reader.CheckNode(NodeName.TacticSkill) ? await reader.ReadXmlValueAsByteAsync() : null,
                RatingMidfield = reader.CheckNode(NodeName.RatingMidfield) ? await reader.ReadXmlValueAsByteAsync() : null,
                RatingRightDef = reader.CheckNode(NodeName.RatingRightDef) ? await reader.ReadXmlValueAsByteAsync() : null,
                RatingMidDef = reader.CheckNode(NodeName.RatingMidDef) ? await reader.ReadXmlValueAsByteAsync() : null,
                RatingLeftDef = reader.CheckNode(NodeName.RatingLeftDef) ? await reader.ReadXmlValueAsByteAsync() : null,
                RatingRightAtt = reader.CheckNode(NodeName.RatingRightAtt) ? await reader.ReadXmlValueAsByteAsync() : null,
                RatingMidAtt = reader.CheckNode(NodeName.RatingMidAtt) ? await reader.ReadXmlValueAsByteAsync() : null,
                RatingLeftAtt = reader.CheckNode(NodeName.RatingLeftAtt) ? await reader.ReadXmlValueAsByteAsync() : null,
                TeamAttitude = reader.CheckNode(NodeName.TeamAttitude) ? await reader.ReadXmlValueAsByteAsync() : null,
                RatingSetPiecesDef = reader.CheckNode(NodeName.RatingIndirectSetPiecesDef) ? await reader.ReadXmlValueAsByteAsync() : null,
                RatingSetPiecesAtt = reader.CheckNode(NodeName.RatingIndirectSetPiecesAtt) ? await reader.ReadXmlValueAsByteAsync() : null,
                NrOfChancesLeft = reader.CheckNode(NodeName.NrOfChancesLeft) ? await reader.ReadXmlValueAsByteAsync() : null,
                NrOfChancesCenter = reader.CheckNode(NodeName.NrOfChancesCenter) ? await reader.ReadXmlValueAsByteAsync() : null,
                NrOfChancesRight = reader.CheckNode(NodeName.NrOfChancesRight) ? await reader.ReadXmlValueAsByteAsync() : null,
                NrOfChancesSpecialEvents = reader.CheckNode(NodeName.NrOfChancesSpecialEvents) ? await reader.ReadXmlValueAsByteAsync() : null,
                NrOfChancesOther = reader.CheckNode(NodeName.NrOfChancesOther) ? await reader.ReadXmlValueAsByteAsync() : null,
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}