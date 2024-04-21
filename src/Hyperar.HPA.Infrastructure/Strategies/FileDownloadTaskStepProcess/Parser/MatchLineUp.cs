namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.MatchLineUp;

    public class MatchLineUp : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public MatchLineUp(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.MatchId = await reader.ReadXmlValueAsLongAsync();
            result.SourceSystem = await reader.ReadElementContentAsStringAsync();
            result.MatchType = await reader.ReadXmlValueAsByteAsync();
            result.MatchContextId = await reader.ReadXmlValueAsLongAsync();
            result.MatchDate = await reader.ReadXmlValueAsDateTimeAsync();
            result.HomeTeam = await ParseIdNameNodeAsync(reader);
            result.AwayTeam = await ParseIdNameNodeAsync(reader);
            result.Arena = await ParseArenaNodeAsync(reader);
            result.Team = await ParseTeamNodeAsync(reader);

            return result;
        }

        private static async Task<Arena> ParseArenaNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            Arena result = new Arena
            {
                ArenaId = await reader.ReadXmlValueAsLongAsync(),
                ArenaName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Player> ParsePlayerNode(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            Player result = new Player
            {
                PlayerId = await reader.ReadXmlValueAsLongAsync(),
                RoleId = await reader.ReadXmlValueAsByteAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadElementContentAsStringAsync(),
                RatingStars = reader.CheckNode(NodeName.RatingStars) ? await reader.ReadXmlValueAsDecimalAsync() : null,
                RatingStarsEndOfMatch = reader.CheckNode(NodeName.RatingStarsEndOfMatch) ? await reader.ReadXmlValueAsDecimalAsync() : null,
                Behaviour = reader.CheckNode(NodeName.Behaviour) ? await reader.ReadXmlValueAsByteAsync() : null
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<StartingPlayer> ParseStartingPlayerNode(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            StartingPlayer result = new StartingPlayer
            {
                PlayerId = await reader.ReadXmlValueAsLongAsync(),
                RoleId = await reader.ReadXmlValueAsByteAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadElementContentAsStringAsync(),
                Behaviour = reader.CheckNode(NodeName.Behaviour) ? await reader.ReadXmlValueAsByteAsync() : null
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Substitution> ParseSubstitutionNode(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            Substitution result = new Substitution
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                SubjectPlayerId = await reader.ReadXmlValueAsLongAsync(),
                ObjectPlayerId = await reader.ReadXmlValueAsLongAsync(),
                OrderType = await reader.ReadXmlValueAsByteAsync(),
                NewPositionId = await reader.ReadXmlValueAsShortAsync(),
                NewPositionBehaviour = await reader.ReadXmlValueAsShortAsync(),
                MatchMinute = await reader.ReadXmlValueAsByteAsync(),
                MatchPart = await reader.ReadXmlValueAsByteAsync()
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
                ExperienceLevel = await reader.ReadXmlValueAsByteAsync(),
                StyleOfPlay = await reader.ReadXmlValueAsShortAsync(),
            };

            if (reader.CheckNode(NodeName.StartingLineup) && !reader.IsEmptyElement)
            {
                // Reads opening node
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Player))
                {
                    result.StartingLineUp.Add(
                        await ParseStartingPlayerNode(reader));
                }
            }

            // Reads closing node.
            await reader.ReadAsync();

            if (reader.CheckNode(NodeName.Substitutions) && !reader.IsEmptyElement)
            {
                // Reads opening node
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Substitution))
                {
                    result.Substitutions.Add(
                        await ParseSubstitutionNode(reader));
                }
            }

            // Reads closing node.
            await reader.ReadAsync();

            if (reader.CheckNode(NodeName.Lineup) && !reader.IsEmptyElement)
            {
                // Reads opening node
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Player))
                {
                    result.LineUp.Add(
                        await ParsePlayerNode(reader));
                }
            }

            // Reads closing node.
            await reader.ReadAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}