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
        private const string avatarNodeName = "Avatar";

        private const string lastLoginsNodeName = "LastLogins";

        private const string layerNodeName = "Layer";

        private const string loginTimeNodeName = "LoginTime";

        private const string nationalTeamAssistantNodeName = "NationalTeamAssistant";

        private const string nationalTeamCoachNodeName = "NationalTeamCoach";

        private const string nationalTeamNodeName = "NationalTeam";

        private const string teamNodeName = "Team";

        private const string teamsNodeName = "Teams";

        private const string xAttributeName = "x";

        private const string yAttributeName = "y";

        public override async Task<IXmlFile> ParseFileTypeSpecificContentAsync(XmlReader reader, IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.Manager = await ParseManagerNodeAsync(reader);

            return result;
        }

        private static async Task<Arena> ParseArenaNodeAsync(XmlReader reader)
        {
            // Reads opening element.
            await reader.ReadAsync();

            var result = new Arena
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

            var result = new Avatar
            {
                BackgroundImage = await reader.ReadElementContentAsStringAsync()
            };

            while (reader.Name == layerNodeName)
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

            var result = new Country
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

            var result = new Currency
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

            var result = new Language
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
            var result = new Layer
            {
                X = int.Parse(reader.GetAttribute(xAttributeName) ?? "0"),
                Y = int.Parse(reader.GetAttribute(yAttributeName) ?? "0"),
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

            var result = new LeagueLevelUnit
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

            var result = new League
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

            var result = new Manager
            {
                UserId = await reader.ReadXmlValueAsUintAsync(),
                LoginName = await reader.ReadElementContentAsStringAsync(),
                SupporterTier = (await reader.ReadElementContentAsStringAsync()).ToSupporterTier()
            };

            if (reader.Name == lastLoginsNodeName)
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.Name == loginTimeNodeName)
                {
                    result.LastLogins.Add(await reader.ReadElementContentAsStringAsync());
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            result.Language = await ParseLanguageNodeAsync(reader);
            result.Country = await ParseCountryNodeAsync(reader);
            result.Currency = await ParseCurrencyNodeAsync(reader);

            if (reader.Name == teamsNodeName)
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.Name == teamNodeName)
                {
                    result.Teams.Add(await ParseTeamNodeAsync(reader));
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            if (reader.Name == nationalTeamCoachNodeName)
            {
                if (!reader.IsEmptyElement)
                {
                    // Reads opening element.
                    await reader.ReadAsync();

                    while (reader.Name == nationalTeamNodeName)
                    {
                        result.NationalTeamCoach.Add(await ParseNationalTeamNodeAsync(reader));
                    }

                    // Reads closing element.
                    await reader.ReadAsync();
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            if (reader.Name == nationalTeamAssistantNodeName)
            {
                if (!reader.IsEmptyElement)
                {
                    // Reads opening element.
                    await reader.ReadAsync();

                    while (reader.Name == nationalTeamNodeName)
                    {
                        result.NationalTeamCoach.Add(await ParseNationalTeamNodeAsync(reader));
                    }

                    // Reads closing element.
                    await reader.ReadAsync();
                }
            }

            if (reader.Name == avatarNodeName && !reader.IsEmptyElement)
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

            var result = new NationalTeam
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

            var result = new Region
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

            var result = new Team
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