namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Parser
{
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Constants;
    using ExtensionMethods;
    using Shared.Models.Hattrick;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.ManagerCompendium;

    public class ManagerCompendium : ParserBase, IFileDownloadTaskStepProcessStrategy
    {
        public ManagerCompendium(IXmlEntityFactory entityFactory) : base(entityFactory)
        {
        }

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.Manager = await ParseManagerNodeAsync(reader);

            return result;
        }

        private static async Task<Avatar?> ParseAvatarNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            Avatar result = new Avatar
            {
                BackgroundImage = await reader.ReadElementContentAsStringAsync()
            };

            while (reader.CheckNode(NodeName.Layer))
            {
                result.Layers.Add(await ParseLayerNodeAsync(reader));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Currency> ParseCurrencyNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Currency result = new Currency
            {
                CurrencyName = await reader.ReadElementContentAsStringAsync(),
                CurrencyRate = await reader.ReadXmlValueAsDecimalAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Layer> ParseLayerNodeAsync(XmlReader reader)
        {
            Layer result = new Layer
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

        private static async Task<League> ParseLeagueNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            League result = new League
            {
                LeagueId = await reader.ReadXmlValueAsLongAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
                Season = await reader.ReadXmlValueAsByteAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Manager> ParseManagerNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Manager result = new Manager
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

            result.Language = await ParseIdNameNodeAsync(reader);
            result.Country = await ParseIdNameNodeAsync(reader);
            result.Currency = await ParseCurrencyNodeAsync(reader);

            if (reader.CheckNode(NodeName.Teams))
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Team))
                {
                    result.Teams.Add(await ParseTeamNodeAsync(reader));
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
                        result.NationalTeamCoach.Add(await ParseIdNameNodeAsync(reader));
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
                        result.NationalTeamCoach.Add(await ParseIdNameNodeAsync(reader));
                    }

                    // Reads closing element.
                    await reader.ReadAsync();
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            if (reader.CheckNode(NodeName.Avatar) && !reader.IsEmptyElement)
            {
                result.Avatar = await ParseAvatarNodeAsync(reader);
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
                Arena = await ParseIdNameNodeAsync(reader),
                League = await ParseLeagueNodeAsync(reader),
                Country = await ParseIdNameNodeAsync(reader),
                LeagueLevelUnit = await ParseIdNameNodeAsync(reader),
                Region = await ParseIdNameNodeAsync(reader),
                YouthTeam = await ParseYouthTeamNodeAsync(reader)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<YouthTeam?> ParseYouthTeamNodeAsync(XmlReader reader)
        {
            YouthTeam? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                result = new YouthTeam
                {
                    YouthTeamId = await reader.ReadXmlValueAsLongAsync(),
                    YouthTeamName = await reader.ReadElementContentAsStringAsync(),
                    YouthLeague = await ParseIdNameNodeAsync(reader)
                };
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}