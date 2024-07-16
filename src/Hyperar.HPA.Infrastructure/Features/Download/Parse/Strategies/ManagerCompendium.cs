namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Models = Shared.Models.Hattrick;

    public class ManagerCompendium : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            Models.ManagerCompendium.HattrickData result = (Models.ManagerCompendium.HattrickData)file;

            result.Manager = await ParseManagerNodeAsync(reader, cancellationToken);
        }

        private static async Task<Models.Avatar?> ParseAvatarNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Models.Avatar result = new Models.Avatar
            {
                BackgroundImage = await reader.ReadElementContentAsStringAsync()
            };

            while (reader.CheckNode(NodeName.Layer))
            {
                result.Layers.Add(await ParseLayerNodeAsync(reader, cancellationToken));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Models.ManagerCompendium.Currency> ParseCurrencyNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Models.ManagerCompendium.Currency result = new Models.ManagerCompendium.Currency
            {
                CurrencyName = await reader.ReadElementContentAsStringAsync(),
                CurrencyRate = await reader.ReadXmlValueAsDecimalAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Models.Layer> ParseLayerNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            Models.Layer result = new Models.Layer
            {
                X = byte.Parse(reader.GetAttribute(NodeName.X) ?? "0"),
                Y = byte.Parse(reader.GetAttribute(NodeName.Y) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.Image = await reader.ReadElementContentAsStringAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Models.ManagerCompendium.League> ParseLeagueNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Models.ManagerCompendium.League result = new Models.ManagerCompendium.League
            {
                LeagueId = await reader.ReadXmlValueAsLongAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
                Season = await reader.ReadXmlValueAsIntAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Models.ManagerCompendium.Manager> ParseManagerNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Models.ManagerCompendium.Manager result = new Models.ManagerCompendium.Manager
            {
                UserId = await reader.ReadXmlValueAsLongAsync(),
                LoginName = await reader.ReadElementContentAsStringAsync(),
                SupporterTier = await reader.ReadElementContentAsStringAsync()
            };

            if (reader.CheckNode(NodeName.LastLogins))
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.LoginTime))
                {
                    result.LastLogins.Add(await reader.ReadElementContentAsStringAsync());
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            result.Language = await ParseIdNameNodeAsync(reader, cancellationToken);
            result.Country = await ParseIdNameNodeAsync(reader, cancellationToken);
            result.Currency = await ParseCurrencyNodeAsync(reader, cancellationToken);

            if (reader.CheckNode(NodeName.Teams))
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Team))
                {
                    result.Teams.Add(await ParseTeamNodeAsync(reader, cancellationToken));
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            if (reader.CheckNode(NodeName.NationalTeamCoach))
            {
                if (!reader.IsEmptyElement)
                {
                    // Reads opening element.
                    await reader.ReadAsync();

                    while (reader.CheckNode(NodeName.NationalTeam))
                    {
                        result.NationalTeamCoach.Add(await ParseIdNameNodeAsync(reader, cancellationToken));
                    }

                    // Reads closing element.
                    await reader.ReadAsync();
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            if (reader.CheckNode(NodeName.NationalTeamAssistant))
            {
                if (!reader.IsEmptyElement)
                {
                    // Reads opening element.
                    await reader.ReadAsync();

                    while (reader.CheckNode(NodeName.NationalTeam))
                    {
                        result.NationalTeamCoach.Add(await ParseIdNameNodeAsync(reader, cancellationToken));
                    }

                    // Reads closing element.
                    await reader.ReadAsync();
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            if (reader.CheckNode(NodeName.Avatar) && !reader.IsEmptyElement)
            {
                result.Avatar = await ParseAvatarNodeAsync(reader, cancellationToken);
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Models.ManagerCompendium.Team> ParseTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Models.ManagerCompendium.Team result = new Models.ManagerCompendium.Team
            {
                TeamId = await reader.ReadXmlValueAsLongAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                Arena = await ParseIdNameNodeAsync(reader, cancellationToken),
                League = await ParseLeagueNodeAsync(reader, cancellationToken),
                Country = await ParseIdNameNodeAsync(reader, cancellationToken),
                LeagueLevelUnit = await ParseIdNameNodeAsync(reader, cancellationToken),
                Region = await ParseIdNameNodeAsync(reader, cancellationToken),
                YouthTeam = await ParseYouthTeamNodeAsync(reader, cancellationToken)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Models.ManagerCompendium.YouthTeam?> ParseYouthTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            Models.ManagerCompendium.YouthTeam? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                result = new Models.ManagerCompendium.YouthTeam
                {
                    YouthTeamId = await reader.ReadXmlValueAsLongAsync(),
                    YouthTeamName = await reader.ReadElementContentAsStringAsync(),
                    YouthLeague = await ParseNullableIdNameNodeAsync(reader, cancellationToken)
                };
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}