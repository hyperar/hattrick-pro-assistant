namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileParser
{
    using System.Xml;
    using Hyperar.HPA.Application.Hattrick.Interfaces;
    using Hyperar.HPA.Application.Hattrick.ManagerCompendium;
    using Hyperar.HPA.Application.Interfaces;
    using Hyperar.HPA.Common.ExtensionMethods;
    using Hyperar.HPA.Infrastructure.Strategies.XmlFileParser.ExtensionMethods;

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

        public override void ParseFileTypeSpecificContent(XmlReader reader, ref IXmlFile entity)
        {
            var result = (HattrickData)entity;

            result.Manager = this.ParseManagerNode(reader);
        }

        private Arena ParseArenaNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Arena
            {
                ArenaId = reader.ReadXmlValueAsUint(),
                ArenaName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Avatar? ParseAvatarNode(XmlReader reader)
        {
            // Reads opening node.
            reader.Read();

            var result = new Avatar
            {
                BackgroundImage = reader.ReadElementContentAsString()
            };

            while (reader.Name == layerNodeName)
            {
                result.Layers.Add(this.ParseLayerNode(reader));
            }

            // Reads closing node.
            reader.Read();

            return result;
        }

        private Country ParseCountryNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Country
            {
                CountryId = reader.ReadXmlValueAsUint(),
                CountryName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Currency ParseCurrencyNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Currency
            {
                CurrencyName = reader.ReadElementContentAsString(),
                CurrencyRate = reader.ReadXmlValueAsDecimal()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Language ParseLanguageNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Language
            {
                LanguageId = reader.ReadXmlValueAsUint(),
                LanguageName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Layer ParseLayerNode(XmlReader reader)
        {
            var result = new Layer
            {
                X = int.Parse(reader.GetAttribute(xAttributeName) ?? "0"),
                Y = int.Parse(reader.GetAttribute(yAttributeName) ?? "0"),
            };

            // Reads opening node.
            reader.Read();

            result.Image = reader.ReadElementContentAsString();

            // Reads closing node.
            reader.Read();

            return result;
        }

        private LeagueLevelUnit ParseLeagueLevelUnitNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new LeagueLevelUnit
            {
                LeagueLevelUnitId = reader.ReadXmlValueAsUint(),
                LeagueLevelUnitName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private League ParseLeagueNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new League
            {
                LeagueId = reader.ReadXmlValueAsUint(),
                LeagueName = reader.ReadElementContentAsString(),
                Season = reader.ReadXmlValueAsUint()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Manager ParseManagerNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Manager
            {
                UserId = reader.ReadXmlValueAsUint(),
                LoginName = reader.ReadElementContentAsString(),
                SupporterTier = reader.ReadElementContentAsString().ToSupporterTier()
            };

            if (reader.Name == lastLoginsNodeName)
            {
                // Reads opening element.
                reader.Read();

                while (reader.Name == loginTimeNodeName)
                {
                    result.LastLogins.Add(reader.ReadElementContentAsString());
                }

                // Reads closing element.
                reader.Read();
            }

            result.Language = this.ParseLanguageNode(reader);
            result.Country = this.ParseCountryNode(reader);
            result.Currency = this.ParseCurrencyNode(reader);

            if (reader.Name == teamsNodeName)
            {
                // Reads opening element.
                reader.Read();

                while (reader.Name == teamNodeName)
                {
                    result.Teams.Add(this.ParseTeamNode(reader));
                }

                // Reads closing element.
                reader.Read();
            }

            if (reader.Name == nationalTeamCoachNodeName)
            {
                if (!reader.IsEmptyElement)
                {
                    // Reads opening element.
                    reader.Read();

                    while (reader.Name == nationalTeamNodeName)
                    {
                        result.NationalTeamCoach.Add(this.ParseNationalTeamNode(reader));
                    }

                    // Reads closing element.
                    reader.Read();
                }

                // Reads closing element.
                reader.Read();
            }

            if (reader.Name == nationalTeamAssistantNodeName)
            {
                if (!reader.IsEmptyElement)
                {
                    // Reads opening element.
                    reader.Read();

                    while (reader.Name == nationalTeamNodeName)
                    {
                        result.NationalTeamCoach.Add(this.ParseNationalTeamNode(reader));
                    }

                    // Reads closing element.
                    reader.Read();
                }
            }

            if (reader.Name == avatarNodeName && !reader.IsEmptyElement)
            {
                result.Avatar = this.ParseAvatarNode(reader);
            }

            // Reads closing element.
            reader.Read();

            return result;
        }

        private NationalTeam ParseNationalTeamNode(XmlReader reader)
        {
            // Reads opening node.
            reader.Read();

            var result = new NationalTeam
            {
                NationalTeamId = reader.ReadXmlValueAsUint(),
                NationalTeamName = reader.ReadElementContentAsString()
            };

            // Reads closing node.
            reader.Read();

            return result;
        }

        private Region ParseRegionNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Region
            {
                RegionId = reader.ReadXmlValueAsUint(),
                RegionName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Team ParseTeamNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            var result = new Team
            {
                TeamId = reader.ReadXmlValueAsUint(),
                TeamName = reader.ReadElementContentAsString(),
                Arena = this.ParseArenaNode(reader),
                League = this.ParseLeagueNode(reader),
                Country = this.ParseCountryNode(reader),
                LeagueLevelUnit = this.ParseLeagueLevelUnitNode(reader),
                Region = this.ParseRegionNode(reader),
                YouthTeam = this.ParseYouthTeamNode(reader)
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private YouthLeague? ParseYouthLeagueNode(XmlReader reader)
        {
            YouthLeague? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                reader.Read();

                result = new YouthLeague
                {
                    YouthLeagueId = reader.ReadXmlValueAsUint(),
                    YouthLeagueName = reader.ReadElementContentAsString()
                };
            }

            // Reads closing node.
            reader.Read();

            return result;
        }

        private YouthTeam? ParseYouthTeamNode(XmlReader reader)
        {
            YouthTeam? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening node.
                reader.Read();

                result = new YouthTeam
                {
                    YouthTeamId = reader.ReadXmlValueAsUint(),
                    YouthTeamName = reader.ReadElementContentAsString(),
                    YouthLeague = this.ParseYouthLeagueNode(reader)
                };
            }

            // Reads closing node.
            reader.Read();

            return result;
        }
    }
}