namespace Hyperar.HPA.Business.XmlFileParser
{
    using System.Xml;
    using Hyperar.HPA.BusinessContracts;
    using Hyperar.HPA.Common.ExtensionMethods;
    using Hyperar.HPA.Domain.Hattrick;
    using Hyperar.HPA.Domain.Hattrick.ManagerCompendium;

    public class ManagerCompendium : XmlFileParserBase, IXmlFileParserStrategy
    {
        private const string xAttributeName = "x";

        private const string yAttributeName = "y";

        private const string avatarNodeName = "Avatar";

        private const string lastLoginsNodeName = "LastLogins";

        private const string loginTimeNodeName = "LoginTime";

        private const string teamsNodeName = "Teams";

        private const string teamNodeName = "Team";

        private const string nationalTeamCoachNodeName = "NationalTeamCoach";

        private const string nationalTeamAssistantNodeName = "NationalTeamAssistant";

        private const string nationalTeamNodeName = "NationalTeam";

        private const string layerNodeName = "Layer";

        public override void ParseFileTypeSpecificContent(XmlReader reader, ref XmlFileBase entity)
        {
            var result = (HattrickData)entity;

            result.Manager = this.ParseManagerNode(reader);
        }

        private Team ParseTeamNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Team result = new Team
            {
                TeamId = uint.Parse(reader.ReadElementContentAsString()),
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

        private Manager ParseManagerNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Manager result = new Manager
            {
                UserId = uint.Parse(reader.ReadElementContentAsString()),
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

        private Language ParseLanguageNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Language result = new Language
            {
                LanguageId = uint.Parse(reader.ReadElementContentAsString()),
                LanguageName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Currency ParseCurrencyNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Currency result = new Currency
            {
                CurrencyName = reader.ReadElementContentAsString(),
                CurrencyRate = this.ParseDecimalValue(reader.ReadElementContentAsString())
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Arena ParseArenaNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Arena result = new Arena
            {
                ArenaId = uint.Parse(reader.ReadElementContentAsString()),
                ArenaName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private League ParseLeagueNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            League result = new League
            {
                LeagueId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueName = reader.ReadElementContentAsString(),
                Season = uint.Parse(reader.ReadElementContentAsString())
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Country ParseCountryNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Country result = new Country
            {
                CountryId = uint.Parse(reader.ReadElementContentAsString()),
                CountryName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private LeagueLevelUnit ParseLeagueLevelUnitNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            LeagueLevelUnit result = new LeagueLevelUnit
            {
                LeagueLevelUnitId = uint.Parse(reader.ReadElementContentAsString()),
                LeagueLevelUnitName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
            reader.Read();

            return result;
        }

        private Region ParseRegionNode(XmlReader reader)
        {
            // Reads opening element.
            reader.Read();

            Region result = new Region
            {
                RegionId = uint.Parse(reader.ReadElementContentAsString()),
                RegionName = reader.ReadElementContentAsString()
            };

            // Reads closing element.
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
                    YouthTeamId = uint.Parse(reader.ReadElementContentAsString()),
                    YouthTeamName = reader.ReadElementContentAsString(),
                    YouthLeague = this.ParseYouthLeagueNode(reader)
                };
            }

            // Reads closing node.
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
                    YouthLeagueId = uint.Parse(reader.ReadElementContentAsString()),
                    YouthLeagueName = reader.ReadElementContentAsString()
                };
            }

            // Reads closing node.
            reader.Read();

            return result;
        }

        private NationalTeam ParseNationalTeamNode(XmlReader reader)
        {
            // Reads opening node.
            reader.Read();

            NationalTeam result = new NationalTeam
            {
                NationalTeamId = uint.Parse(reader.ReadElementContentAsString()),
                NationalTeamName = reader.ReadElementContentAsString()
            };

            // Reads closing node.
            reader.Read();

            return result;
        }

        private Avatar? ParseAvatarNode(XmlReader reader)
        {
            // Reads opening node.
            reader.Read();

            Avatar result = new Avatar
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

        private Layer ParseLayerNode(XmlReader reader)
        {
            Layer result = new Layer
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
    }
}
