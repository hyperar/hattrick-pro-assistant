namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.MatchDetails;

    public class MatchDetails : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.UserSupporterTier = await reader.ReadElementContentAsStringAsync();
            result.SourceSystem = await reader.ReadElementContentAsStringAsync();
            result.Match = await ParseMatchNodeAsync(reader, cancellationToken);
        }

        private static async Task<Arena> ParseArenaNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node
            await reader.ReadAsync();

            Arena result = new Arena
            {
                ArenaId = await reader.ReadXmlValueAsLongAsync(),
                ArenaName = await reader.ReadElementContentAsStringAsync(),
                WeatherId = reader.CheckNode(NodeName.WeatherId) ? await reader.ReadXmlValueAsIntAsync() : null,
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

        private static async Task<List<Booking>> ParseBookingListNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            List<Booking> result = new List<Booking>();

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Booking))
                {
                    result.Add(
                        await ParseBookingNodeAsync(reader, cancellationToken));
                }
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Booking> ParseBookingNodeAsync(XmlReader reader, CancellationToken cancellationToken)
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
            result.BookingType = await reader.ReadXmlValueAsIntAsync();
            result.BookingMinute = await reader.ReadXmlValueAsIntAsync();
            result.MatchPart = await reader.ReadXmlValueAsIntAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Event>> ParseEventListNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            List<Event> result = new List<Event>();

            while (reader.CheckNode(NodeName.Event))
            {
                result.Add(
                    await ParseEventNodeAsync(reader, cancellationToken));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Event> ParseEventNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            Event result = new Event
            {
                Index = byte.Parse(reader.GetAttribute(NodeName.Index) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.Minute = await reader.ReadXmlValueAsIntAsync();
            result.SubjectPlayerId = await reader.ReadXmlValueAsLongAsync();
            result.SubjectTeamId = await reader.ReadXmlValueAsLongAsync();
            result.ObjectPlayerId = await reader.ReadXmlValueAsLongAsync();
            result.MatchPart = await reader.ReadXmlValueAsIntAsync();
            result.EventTypeId = await reader.ReadXmlValueAsIntAsync();
            result.EventVariation = await reader.ReadXmlValueAsIntAsync();
            result.EventText = await reader.ReadElementContentAsStringAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Goal>> ParseGoalListNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            List<Goal> result = new List<Goal>();

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Goal))
                {
                    result.Add(
                        await ParseGoalNodeAsync(reader, cancellationToken));
                }
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Goal> ParseGoalNodeAsync(XmlReader reader, CancellationToken cancellationToken)
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
            result.ScorerHomeGoals = await reader.ReadXmlValueAsIntAsync();
            result.ScorerAwayGoals = await reader.ReadXmlValueAsIntAsync();
            result.ScorerMinute = await reader.ReadXmlValueAsIntAsync();
            result.MatchPart = await reader.ReadXmlValueAsIntAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Injury>> ParseInjuryListNodeAsync(XmlReader reader, CancellationToken cancellationToken)
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
            result.InjuryType = await reader.ReadXmlValueAsIntAsync();
            result.InjuryMinute = await reader.ReadXmlValueAsIntAsync();
            result.MatchPart = await reader.ReadXmlValueAsIntAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Match> ParseMatchNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node
            await reader.ReadAsync();

            Match result = new Match
            {
                MatchId = await reader.ReadXmlValueAsLongAsync(),
                MatchType = await reader.ReadXmlValueAsIntAsync(),
                MatchContextId = await reader.ReadXmlValueAsLongAsync(),
                MatchRuleId = await reader.ReadXmlValueAsIntAsync(),
                CupLevel = await reader.ReadXmlValueAsIntAsync(),
                CupLevelIndex = await reader.ReadXmlValueAsIntAsync(),
                MatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                FinishedDate = reader.CheckNode(NodeName.FinishedDate) ? await reader.ReadXmlValueAsDateTimeAsync() : null,
                AddedMinutes = reader.CheckNode(NodeName.AddedMinutes) ? await reader.ReadXmlValueAsIntAsync() : null,
                HomeTeam = await ParseTeamNodeAsync(reader, cancellationToken),
                AwayTeam = await ParseTeamNodeAsync(reader, cancellationToken),
                Arena = await ParseArenaNodeAsync(reader, cancellationToken),
                MatchOfficials = reader.CheckNode(NodeName.MatchOfficials) ? await ParseMatchOfficialsNodeAsync(reader, cancellationToken) : null,
                Scorers = reader.CheckNode(NodeName.Scorers) ? await ParseGoalListNodeAsync(reader, cancellationToken) : null,
                Bookings = reader.CheckNode(NodeName.Bookings) ? await ParseBookingListNodeAsync(reader, cancellationToken) : null,
                Injuries = reader.CheckNode(NodeName.Injuries) ? await ParseInjuryListNodeAsync(reader, cancellationToken) : null,
                PossessionFirstHalfHome = reader.CheckNode(NodeName.PossessionFirstHalfHome) ? await reader.ReadXmlValueAsIntAsync() : null,
                PossessionFirstHalfAway = reader.CheckNode(NodeName.PossessionFirstHalfAway) ? await reader.ReadXmlValueAsIntAsync() : null,
                PossessionSecondHalfHome = reader.CheckNode(NodeName.PossessionSecondHalfHome) ? await reader.ReadXmlValueAsIntAsync() : null,
                PossessionSecondHalfAway = reader.CheckNode(NodeName.PossessionSecondHalfAway) ? await reader.ReadXmlValueAsIntAsync() : null,
                EventList = reader.CheckNode(NodeName.EventList) ? await ParseEventListNodeAsync(reader, cancellationToken) : null
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<MatchOfficials> ParseMatchOfficialsNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node
            await reader.ReadAsync();

            MatchOfficials result = new MatchOfficials
            {
                Referee = await ParseRefereeNodeAsync(reader, cancellationToken),
                RefereeAssistant1 = await ParseRefereeNodeAsync(reader, cancellationToken),
                RefereeAssistant2 = await ParseRefereeNodeAsync(reader, cancellationToken),
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Referee> ParseRefereeNodeAsync(XmlReader reader, CancellationToken cancellationToken)
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

        private static async Task<Team> ParseTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node
            await reader.ReadAsync();

            Team result = new Team
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                DressUri = reader.CheckNode(NodeName.DressURI) ? await reader.ReadElementContentAsStringAsync() : null,
                Formation = reader.CheckNode(NodeName.Formation) ? await reader.ReadElementContentAsStringAsync() : null,
                Goals = reader.CheckNode(NodeName.AwayGoals, NodeName.HomeGoals) ? await reader.ReadXmlValueAsIntAsync() : null,
                TacticType = reader.CheckNode(NodeName.TacticType) ? await reader.ReadXmlValueAsIntAsync() : null,
                TacticSkill = reader.CheckNode(NodeName.TacticSkill) ? await reader.ReadXmlValueAsIntAsync() : null,
                RatingMidfield = reader.CheckNode(NodeName.RatingMidfield) ? await reader.ReadXmlValueAsIntAsync() : null,
                RatingRightDef = reader.CheckNode(NodeName.RatingRightDef) ? await reader.ReadXmlValueAsIntAsync() : null,
                RatingMidDef = reader.CheckNode(NodeName.RatingMidDef) ? await reader.ReadXmlValueAsIntAsync() : null,
                RatingLeftDef = reader.CheckNode(NodeName.RatingLeftDef) ? await reader.ReadXmlValueAsIntAsync() : null,
                RatingRightAtt = reader.CheckNode(NodeName.RatingRightAtt) ? await reader.ReadXmlValueAsIntAsync() : null,
                RatingMidAtt = reader.CheckNode(NodeName.RatingMidAtt) ? await reader.ReadXmlValueAsIntAsync() : null,
                RatingLeftAtt = reader.CheckNode(NodeName.RatingLeftAtt) ? await reader.ReadXmlValueAsIntAsync() : null,
                TeamAttitude = reader.CheckNode(NodeName.TeamAttitude) ? await reader.ReadXmlValueAsIntAsync() : null,
                RatingSetPiecesDef = reader.CheckNode(NodeName.RatingIndirectSetPiecesDef) ? await reader.ReadXmlValueAsIntAsync() : null,
                RatingSetPiecesAtt = reader.CheckNode(NodeName.RatingIndirectSetPiecesAtt) ? await reader.ReadXmlValueAsIntAsync() : null,
                NrOfChancesLeft = reader.CheckNode(NodeName.NrOfChancesLeft) ? await reader.ReadXmlValueAsIntAsync() : null,
                NrOfChancesCenter = reader.CheckNode(NodeName.NrOfChancesCenter) ? await reader.ReadXmlValueAsIntAsync() : null,
                NrOfChancesRight = reader.CheckNode(NodeName.NrOfChancesRight) ? await reader.ReadXmlValueAsIntAsync() : null,
                NrOfChancesSpecialEvents = reader.CheckNode(NodeName.NrOfChancesSpecialEvents) ? await reader.ReadXmlValueAsIntAsync() : null,
                NrOfChancesOther = reader.CheckNode(NodeName.NrOfChancesOther) ? await reader.ReadXmlValueAsIntAsync() : null,
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}