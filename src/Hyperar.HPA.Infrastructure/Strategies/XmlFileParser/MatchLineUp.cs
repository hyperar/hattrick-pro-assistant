namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.MatchLineUp;
    using Application.Interfaces;
    using Common.Enums;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class MatchLineUp : XmlFileParserBase, IXmlFileParserStrategy
    {
        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.MatchId = await reader.ReadXmlValueAsUintAsync();
            result.SourceSystem = await reader.ReadElementContentAsStringAsync();
            result.MatchType = (MatchType)await reader.ReadXmlValueAsByteAsync();
            result.MatchContextId = await reader.ReadXmlValueAsUintAsync();
            result.MatchDate = await reader.ReadXmlValueAsDateTimeAsync();
            result.HomeTeam = await ParseHomeTeamNodeAsync(reader);
            result.AwayTeam = await ParseAwayTeamNodeAsync(reader);
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
                ArenaId = await reader.ReadXmlValueAsUintAsync(),
                ArenaName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<AwayTeam> ParseAwayTeamNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            AwayTeam result = new AwayTeam
            {
                AwayTeamId = await reader.ReadXmlValueAsUintAsync(),
                AwayTeamName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<HomeTeam> ParseHomeTeamNodeAsync(XmlReader reader)
        {
            // Reads opening node
            await reader.ReadAsync();

            HomeTeam result = new HomeTeam
            {
                HomeTeamId = await reader.ReadXmlValueAsUintAsync(),
                HomeTeamName = await reader.ReadElementContentAsStringAsync()
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
                PlayerId = await reader.ReadXmlValueAsUintAsync(),
                RoleId = await reader.ReadXmlValueAsByteAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadElementContentAsStringAsync(),
                RatingStars = reader.CheckNode(Constants.NodeName.RatingStars) ? await reader.ReadXmlValueAsDecimalAsync() : null,
                RatingStarsEndOfMatch = reader.CheckNode(Constants.NodeName.RatingStarsEndOfMatch) ? await reader.ReadXmlValueAsDecimalAsync() : null,
                Behaviour = reader.CheckNode(Constants.NodeName.Behaviour) ? (MatchRoleBehavior)await reader.ReadXmlValueAsByteAsync() : null
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
                PlayerId = await reader.ReadXmlValueAsUintAsync(),
                RoleId = await reader.ReadXmlValueAsByteAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadElementContentAsStringAsync(),
                Behavior = reader.CheckNode(Constants.NodeName.Behaviour) ? (MatchRoleBehavior)await reader.ReadXmlValueAsByteAsync() : null
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
                TeamId = await reader.ReadXmlValueAsUintAsync(),
                SubjectPlayerId = await reader.ReadXmlValueAsUintAsync(),
                ObjectPlayerId = await reader.ReadXmlValueAsUintAsync(),
                OrderType = (MatchOrderType)await reader.ReadXmlValueAsByteAsync(),
                NewPositionId = await reader.ReadXmlValueAsUshortAsync(),
                NewPositionBehavior = (MatchRoleBehavior)await reader.ReadXmlValueAsShortAsync(),
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
                TeamId = await reader.ReadXmlValueAsUintAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                ExperienceLevel = (SkillLevel)await reader.ReadXmlValueAsByteAsync(),
                StyleOfPlay = await reader.ReadXmlValueAsIntAsync(),
            };

            if (reader.CheckNode(Constants.NodeName.StartingLineup) && !reader.IsEmptyElement)
            {
                // Reads opening node
                await reader.ReadAsync();

                while (reader.CheckNode(Constants.NodeName.Player))
                {
                    result.StartingLineUp.Add(
                        await ParseStartingPlayerNode(reader));
                }
            }

            // Reads closing node.
            await reader.ReadAsync();

            if (reader.CheckNode(Constants.NodeName.Substitutions) && !reader.IsEmptyElement)
            {
                // Reads opening node
                await reader.ReadAsync();

                while (reader.CheckNode(Constants.NodeName.Substitution))
                {
                    result.Substitutions.Add(
                        await ParseSubstitutionNode(reader));
                }
            }

            // Reads closing node.
            await reader.ReadAsync();

            if (reader.CheckNode(Constants.NodeName.Lineup) && !reader.IsEmptyElement)
            {
                // Reads opening node
                await reader.ReadAsync();

                while (reader.CheckNode(Constants.NodeName.Player))
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