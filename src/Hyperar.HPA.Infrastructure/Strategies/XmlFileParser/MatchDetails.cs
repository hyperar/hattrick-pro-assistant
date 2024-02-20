namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.MatchDetails;
    using Application.Interfaces;
    using Common.Enums;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class MatchDetails : XmlFileParserBase, IXmlFileParserStrategy
    {
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
                WeatherId = reader.CheckNode(Constants.NodeName.WeatherId) ? (Weather)await reader.ReadXmlValueAsByteAsync() : null,
                SoldTotal = reader.CheckNode(Constants.NodeName.SoldTotal) ? await reader.ReadXmlValueAsUintAsync() : null,
                SoldTerraces = reader.CheckNode(Constants.NodeName.SoldTerraces) ? await reader.ReadXmlValueAsUintAsync() : null,
                SoldBasic = reader.CheckNode(Constants.NodeName.SoldBasic) ? await reader.ReadXmlValueAsUintAsync() : null,
                SoldRoof = reader.CheckNode(Constants.NodeName.SoldRoof) ? await reader.ReadXmlValueAsUintAsync() : null,
                SoldVip = reader.CheckNode(Constants.NodeName.SoldVip) ? await reader.ReadXmlValueAsUintAsync() : null
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

                while (reader.CheckNode(Constants.NodeName.Booking))
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
                Index = uint.Parse(reader.GetAttribute(Constants.NodeName.Index) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.BookingPlayerId = await reader.ReadXmlValueAsUintAsync();
            result.BookingPlayerName = await reader.ReadElementContentAsStringAsync();
            result.BookingTeamId = await reader.ReadXmlValueAsUintAsync();
            result.BookingType = (BookingType)await reader.ReadXmlValueAsByteAsync();
            result.BookingMinute = await reader.ReadXmlValueAsUintAsync();
            result.MatchPart = (MatchPart)await reader.ReadXmlValueAsByteAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<Event>> ParseEventListNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            var result = new List<Event>();

            while (reader.CheckNode(Constants.NodeName.Event))
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
                Index = uint.Parse(reader.GetAttribute(Constants.NodeName.Index) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.Minute = await reader.ReadXmlValueAsUintAsync();
            result.SubjectPlayerId = await reader.ReadXmlValueAsUintAsync();
            result.SubjectTeamId = await reader.ReadXmlValueAsUintAsync();
            result.ObjectPlayerId = await reader.ReadXmlValueAsUintAsync();
            result.MatchPart = (MatchPart)await reader.ReadXmlValueAsByteAsync();
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

                while (reader.CheckNode(Constants.NodeName.Goal))
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
                Index = uint.Parse(reader.GetAttribute(Constants.NodeName.Index) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.ScorerPlayerId = await reader.ReadXmlValueAsUintAsync();
            result.ScorerPlayerName = await reader.ReadElementContentAsStringAsync();
            result.ScorerTeamId = await reader.ReadXmlValueAsUintAsync();
            result.ScorerHomeGoals = await reader.ReadXmlValueAsUintAsync();
            result.ScorerAwayGoals = await reader.ReadXmlValueAsUintAsync();
            result.ScorerMinute = await reader.ReadXmlValueAsUintAsync();
            result.MatchPart = (MatchPart)await reader.ReadXmlValueAsByteAsync();

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

                while (reader.CheckNode(Constants.NodeName.Injury))
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
                Index = uint.Parse(reader.GetAttribute(Constants.NodeName.Index) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.InjuryPlayerId = await reader.ReadXmlValueAsUintAsync();
            result.InjuryPlayerName = await reader.ReadElementContentAsStringAsync();
            result.InjuryTeamId = await reader.ReadXmlValueAsUintAsync();
            result.InjuryType = (InjuryType)await reader.ReadXmlValueAsByteAsync();
            result.InjuryMinute = await reader.ReadXmlValueAsUintAsync();
            result.MatchPart = (MatchPart)await reader.ReadXmlValueAsByteAsync();

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
                MatchType = (MatchType)await reader.ReadXmlValueAsByteAsync(),
                MatchContextId = await reader.ReadXmlValueAsUintAsync(),
                MatchRuleId = (MatchRule)await reader.ReadXmlValueAsByteAsync(),
                CupLevel = await reader.ReadXmlValueAsUintAsync(),
                CupLevelIndex = await reader.ReadXmlValueAsUintAsync(),
                MatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                FinishedDate = reader.CheckNode(Constants.NodeName.FinishedDate) ? await reader.ReadXmlValueAsDateTimeAsync() : null,
                AddedMinutes = reader.CheckNode(Constants.NodeName.AddedMinutes) ? await reader.ReadXmlValueAsUintAsync() : null,
                HomeTeam = await ParseTeamNodeAsync(reader),
                AwayTeam = await ParseTeamNodeAsync(reader),
                Arena = await ParseArenaNodeAsync(reader),
                MatchOfficials = reader.CheckNode(Constants.NodeName.MatchOfficials) ? await ParseMatchOfficialsNodeAsync(reader) : null,
                Scorers = reader.CheckNode(Constants.NodeName.Scorers) ? await ParseGoalListNodeAsync(reader) : null,
                Bookings = reader.CheckNode(Constants.NodeName.Bookings) ? await ParseBookingListNodeAsync(reader) : null,
                Injuries = reader.CheckNode(Constants.NodeName.Injuries) ? await ParseInjuryListNodeAsync(reader) : null,
                PossessionFirstHalfHome = reader.CheckNode(Constants.NodeName.PossessionFirstHalfHome) ? await reader.ReadXmlValueAsUintAsync() : null,
                PossessionFirstHalfAway = reader.CheckNode(Constants.NodeName.PossessionFirstHalfAway) ? await reader.ReadXmlValueAsUintAsync() : null,
                PossessionSecondHalfHome = reader.CheckNode(Constants.NodeName.PossessionSecondHalfHome) ? await reader.ReadXmlValueAsUintAsync() : null,
                PossessionSecondHalfAway = reader.CheckNode(Constants.NodeName.PossessionSecondHalfAway) ? await reader.ReadXmlValueAsUintAsync() : null,
                EventList = reader.CheckNode(Constants.NodeName.EventList) ? await ParseEventListNodeAsync(reader) : null
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
                DressUri = reader.CheckNode(Constants.NodeName.DressURI) ? await reader.ReadElementContentAsStringAsync() : null,
                Formation = reader.CheckNode(Constants.NodeName.Formation) ? await reader.ReadElementContentAsStringAsync() : null,
                Goals = reader.CheckNode(Constants.NodeName.AwayGoals, Constants.NodeName.HomeGoals) ? await reader.ReadXmlValueAsUintAsync() : null,
                TacticType = reader.CheckNode(Constants.NodeName.TacticType) ? (MatchTacticType)await reader.ReadXmlValueAsByteAsync() : null,
                TacticSkill = reader.CheckNode(Constants.NodeName.TacticSkill) ? (SkillLevel)await reader.ReadXmlValueAsByteAsync() : null,
                RatingMidfield = reader.CheckNode(Constants.NodeName.RatingMidfield) ? (MatchSectorRating)await reader.ReadXmlValueAsByteAsync() : null,
                RatingRightDef = reader.CheckNode(Constants.NodeName.RatingRightDef) ? (MatchSectorRating)await reader.ReadXmlValueAsByteAsync() : null,
                RatingMidDef = reader.CheckNode(Constants.NodeName.RatingMidDef) ? (MatchSectorRating)await reader.ReadXmlValueAsByteAsync() : null,
                RatingLeftDef = reader.CheckNode(Constants.NodeName.RatingLeftDef) ? (MatchSectorRating)await reader.ReadXmlValueAsByteAsync() : null,
                RatingRightAtt = reader.CheckNode(Constants.NodeName.RatingRightAtt) ? (MatchSectorRating)await reader.ReadXmlValueAsByteAsync() : null,
                RatingMidAtt = reader.CheckNode(Constants.NodeName.RatingMidAtt) ? (MatchSectorRating)await reader.ReadXmlValueAsByteAsync() : null,
                RatingLeftAtt = reader.CheckNode(Constants.NodeName.RatingLeftAtt) ? (MatchSectorRating)await reader.ReadXmlValueAsByteAsync() : null,
                TeamAttitude = reader.CheckNode(Constants.NodeName.TeamAttitude) ? (MatchTeamAttitude)await reader.ReadXmlValueAsByteAsync() : null,
                RatingSetPiecesDef = reader.CheckNode(Constants.NodeName.RatingIndirectSetPiecesDef) ? (MatchSectorRating)await reader.ReadXmlValueAsByteAsync() : null,
                RatingSetPiecesAtt = reader.CheckNode(Constants.NodeName.RatingIndirectSetPiecesAtt) ? (MatchSectorRating)await reader.ReadXmlValueAsByteAsync() : null,
                NrOfChancesLeft = reader.CheckNode(Constants.NodeName.NrOfChancesLeft) ? await reader.ReadXmlValueAsUintAsync() : null,
                NrOfChancesCenter = reader.CheckNode(Constants.NodeName.NrOfChancesCenter) ? await reader.ReadXmlValueAsUintAsync() : null,
                NrOfChancesRight = reader.CheckNode(Constants.NodeName.NrOfChancesRight) ? await reader.ReadXmlValueAsUintAsync() : null,
                NrOfChancesSpecialEvents = reader.CheckNode(Constants.NodeName.NrOfChancesSpecialEvents) ? await reader.ReadXmlValueAsUintAsync() : null,
                NrOfChancesOther = reader.CheckNode(Constants.NodeName.NrOfChancesOther) ? await reader.ReadXmlValueAsUintAsync() : null,
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}