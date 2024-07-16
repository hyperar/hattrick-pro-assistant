namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.MatchLineUp;

    public class MatchLineUp : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.MatchId = await reader.ReadXmlValueAsLongAsync();
            result.SourceSystem = await reader.ReadElementContentAsStringAsync();
            result.MatchType = await reader.ReadXmlValueAsIntAsync();
            result.MatchContextId = reader.CheckNode(NodeName.MatchContextId) ? await reader.ReadXmlValueAsLongAsync() : null;
            result.MatchDate = await reader.ReadXmlValueAsDateTimeAsync();
            result.HomeTeam = await ParseIdNameNodeAsync(reader, cancellationToken);
            result.AwayTeam = await ParseIdNameNodeAsync(reader, cancellationToken);
            result.Arena = await ParseArenaNodeAsync(reader, cancellationToken);
            result.Team = await ParseTeamNodeAsync(reader, cancellationToken);
        }

        private static async Task<IdName> ParseArenaNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node
            await reader.ReadAsync();

            IdName result = new IdName
            {
                Id = await reader.ReadXmlValueAsLongAsync(),
                Name = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Player> ParsePlayerNode(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node
            await reader.ReadAsync();

            Player result = new Player
            {
                PlayerId = await reader.ReadXmlValueAsLongAsync(),
                RoleId = await reader.ReadXmlValueAsIntAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadElementContentAsStringAsync(),
                RatingStars = reader.CheckNode(NodeName.RatingStars) ? await reader.ReadXmlValueAsNullableDecimalAsync(0) : null,
                RatingStarsEndOfMatch = reader.CheckNode(NodeName.RatingStarsEndOfMatch) ? await reader.ReadXmlValueAsNullableDecimalAsync(0) : null,
                Behaviour = reader.CheckNode(NodeName.Behaviour) ? await reader.ReadXmlValueAsIntAsync() : null
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<StartingPlayer> ParseStartingPlayerNode(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node
            await reader.ReadAsync();

            StartingPlayer result = new StartingPlayer
            {
                PlayerId = await reader.ReadXmlValueAsLongAsync(),
                RoleId = await reader.ReadXmlValueAsIntAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadElementContentAsStringAsync(),
                Behaviour = reader.CheckNode(NodeName.Behaviour) ? await reader.ReadXmlValueAsIntAsync() : null
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Substitution> ParseSubstitutionNode(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node
            await reader.ReadAsync();

            Substitution result = new Substitution
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                SubjectPlayerId = await reader.ReadXmlValueAsLongAsync(),
                ObjectPlayerId = await reader.ReadXmlValueAsLongAsync(),
                OrderType = await reader.ReadXmlValueAsIntAsync(),
                NewPositionId = await reader.ReadXmlValueAsIntAsync(),
                NewPositionBehaviour = await reader.ReadXmlValueAsIntAsync(),
                MatchMinute = await reader.ReadXmlValueAsIntAsync(),
                MatchPart = await reader.ReadXmlValueAsIntAsync()
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
                ExperienceLevel = await reader.ReadXmlValueAsIntAsync(),
                StyleOfPlay = await reader.ReadXmlValueAsIntAsync(),
            };

            if (reader.CheckNode(NodeName.StartingLineup) && !reader.IsEmptyElement)
            {
                // Reads opening node
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Player))
                {
                    result.StartingLineUp.Add(
                        await ParseStartingPlayerNode(reader, cancellationToken));
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
                        await ParseSubstitutionNode(reader, cancellationToken));
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
                        await ParsePlayerNode(reader, cancellationToken));
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