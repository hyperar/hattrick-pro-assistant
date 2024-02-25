namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Application.Hattrick.Interfaces;
    using Application.Hattrick.ManagerCompendium;
    using Application.Interfaces;
    using Common.ExtensionMethods;
    using Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

    public class ManagerCompendium : XmlFileParserBase, IXmlFileParserStrategy
    {
        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            HattrickData result = (HattrickData)entity;

            result.Manager = await ParseManagerNodeAsync(reader);

            return result;
        }

        private static async Task<Arena> ParseArenaNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Arena result = new Arena
            {
                ArenaId = await reader.ReadXmlValueAsUintAsync(),
                ArenaName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

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

            while (reader.CheckNode(Constants.NodeName.Layer))
            {
                result.Layers.Add(await ParseLayerNodeAsync(reader));
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Country> ParseCountryNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Country result = new Country
            {
                CountryId = await reader.ReadXmlValueAsUintAsync(),
                CountryName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing element.
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

        private static async Task<Language> ParseLanguageNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Language result = new Language
            {
                LanguageId = await reader.ReadXmlValueAsUintAsync(),
                LanguageName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Layer> ParseLayerNodeAsync(XmlReader reader)
        {
            Layer result = new Layer
            {
                X = uint.Parse(reader.GetAttribute(Constants.NodeName.X) ?? "0"),
                Y = uint.Parse(reader.GetAttribute(Constants.NodeName.Y) ?? "0"),
            };

            // Reads opening node.
            await reader.ReadAsync();

            result.Image = await reader.ReadElementContentAsStringAsync();

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<LeagueLevelUnit> ParseLeagueLevelUnitNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            LeagueLevelUnit result = new LeagueLevelUnit
            {
                LeagueLevelUnitId = await reader.ReadXmlValueAsUintAsync(),
                LeagueLevelUnitName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<League> ParseLeagueNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            League result = new League
            {
                LeagueId = await reader.ReadXmlValueAsUintAsync(),
                LeagueName = await reader.ReadElementContentAsStringAsync(),
                Season = await reader.ReadXmlValueAsUintAsync()
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
                UserId = await reader.ReadXmlValueAsUintAsync(),
                LoginName = await reader.ReadElementContentAsStringAsync(),
                SupporterTier = (await reader.ReadElementContentAsStringAsync()).ToSupporterTier()
            };

            if (reader.CheckNode(Constants.NodeName.LastLogins))
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(Constants.NodeName.LoginTime))
                {
                    result.LastLogins.Add(await reader.ReadElementContentAsStringAsync());
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            result.Language = await ParseLanguageNodeAsync(reader);
            result.Country = await ParseCountryNodeAsync(reader);
            result.Currency = await ParseCurrencyNodeAsync(reader);

            if (reader.CheckNode(Constants.NodeName.Teams))
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(Constants.NodeName.Team))
                {
                    result.Teams.Add(await ParseTeamNodeAsync(reader));
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            if (reader.CheckNode(Constants.NodeName.NationalTeamCoach))
            {
                if (!reader.IsEmptyElement)
                {
                    // Reads opening element.
                    await reader.ReadAsync();

                    while (reader.CheckNode(Constants.NodeName.NationalTeam))
                    {
                        result.NationalTeamCoach.Add(await ParseNationalTeamNodeAsync(reader));
                    }

                    // Reads closing element.
                    await reader.ReadAsync();
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            if (reader.CheckNode(Constants.NodeName.NationalTeamAssistant))
            {
                if (!reader.IsEmptyElement)
                {
                    // Reads opening element.
                    await reader.ReadAsync();

                    while (reader.CheckNode(Constants.NodeName.NationalTeam))
                    {
                        result.NationalTeamCoach.Add(await ParseNationalTeamNodeAsync(reader));
                    }

                    // Reads closing element.
                    await reader.ReadAsync();
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            if (reader.CheckNode(Constants.NodeName.Avatar) && !reader.IsEmptyElement)
            {
                result.Avatar = await ParseAvatarNodeAsync(reader);
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<NationalTeam> ParseNationalTeamNodeAsync(XmlReader reader)
        {
            // Reads opening node.
            await reader.ReadAsync();

            NationalTeam result = new NationalTeam
            {
                NationalTeamId = await reader.ReadXmlValueAsUintAsync(),
                NationalTeamName = await reader.ReadElementContentAsStringAsync()
            };

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Region> ParseRegionNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Region result = new Region
            {
                RegionId = await reader.ReadXmlValueAsUintAsync(),
                RegionName = await reader.ReadElementContentAsStringAsync()
            };

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
                TeamId = await reader.ReadXmlValueAsUintAsync(),
                TeamName = await reader.ReadElementContentAsStringAsync(),
                Arena = await ParseArenaNodeAsync(reader),
                League = await ParseLeagueNodeAsync(reader),
                Country = await ParseCountryNodeAsync(reader),
                LeagueLevelUnit = await ParseLeagueLevelUnitNodeAsync(reader),
                Region = await ParseRegionNodeAsync(reader),
                YouthTeam = await ParseYouthTeamNodeAsync(reader)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<YouthLeague?> ParseYouthLeagueNodeAsync(XmlReader reader)
        {
            YouthLeague? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                await reader.ReadAsync();

                result = new YouthLeague
                {
                    YouthLeagueId = await reader.ReadXmlValueAsUintAsync(),
                    YouthLeagueName = await reader.ReadElementContentAsStringAsync()
                };
            }

            // Reads closing node.
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
                    YouthTeamId = await reader.ReadXmlValueAsUintAsync(),
                    YouthTeamName = await reader.ReadElementContentAsStringAsync(),
                    YouthLeague = await ParseYouthLeagueNodeAsync(reader)
                };
            }

            // Reads closing node.
            await reader.ReadAsync();

            return result;
        }
    }
}