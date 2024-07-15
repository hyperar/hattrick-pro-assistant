namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.YouthTeamDetails;

    public class YouthTeamDetails : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.YouthTeam = await ParseYouthTeamNodeAsync(reader, cancellationToken);
        }

        private static async Task<List<Scout>> ParseScoutListNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            List<Scout> result = new List<Scout>();

            if (reader.CheckNode(NodeName.ScoutList))
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.Scout))
                {
                    result.Add(
                        await ParseScoutNodeAsync(reader, cancellationToken));
                }

                // Reads closing element.
                await reader.ReadAsync();
            }

            return result;
        }

        private static async Task<Scout> ParseScoutNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            Scout result = new Scout
            {
                YouthScoutId = await reader.ReadXmlValueAsLongAsync(),
                ScoutName = await reader.ReadElementContentAsStringAsync(),
                Age = await reader.ReadXmlValueAsIntAsync(),
                Country = await ParseIdNameNodeAsync(reader, cancellationToken),
                Region = await ParseIdNameNodeAsync(reader, cancellationToken),
                InCountry = await ParseIdNameNodeAsync(reader, cancellationToken),
                InRegion = await ParseIdNameNodeAsync(reader, cancellationToken),
                HiredDate = await reader.ReadXmlValueAsDateTimeAsync(),
                LastCalled = await reader.ReadXmlValueAsDateTimeAsync(),
                PlayerTypeSearch = await reader.ReadXmlValueAsIntAsync(),
                HofPlayerId = await reader.ReadXmlValueAsLongAsync(),
                Travel = await ParseTravelNodeAsync(reader, cancellationToken)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<Travel?> ParseTravelNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            Travel? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                await reader.ReadAsync();

                result = new Travel
                {
                    TravelStartDate = await reader.ReadXmlValueAsDateTimeAsync(),
                    TravelLength = await reader.ReadXmlValueAsIntAsync(),
                    TravelType = await reader.ReadXmlValueAsIntAsync()
                };
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<YouthLeague?> ParseYouthLeagueNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            YouthLeague? result = null;

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                await reader.ReadAsync();

                result = new YouthLeague
                {
                    YouthLeagueId = await reader.ReadXmlValueAsLongAsync(),
                    YouthLeagueName = await reader.ReadElementContentAsStringAsync(),
                    YouthLeagueStatus = await reader.ReadXmlValueAsIntAsync()
                };
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<YouthTeam> ParseYouthTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            YouthTeam result = new YouthTeam
            {
                YouthTeamId = await reader.ReadXmlValueAsLongAsync(),
                YouthTeamName = await reader.ReadElementContentAsStringAsync(),
                ShortTeamName = await reader.ReadElementContentAsStringAsync(),
                CreatedDate = await reader.ReadXmlValueAsDateTimeAsync(),
                UserId = await reader.ReadXmlValueAsLongAsync(),
                Country = await ParseIdNameNodeAsync(reader, cancellationToken),
                Region = await ParseIdNameNodeAsync(reader, cancellationToken),
                YouthArena = await ParseIdNameNodeAsync(reader, cancellationToken),
                YouthLeague = await ParseYouthLeagueNodeAsync(reader, cancellationToken),
                OwningTeam = await ParseNullableIdNameNodeAsync(reader, cancellationToken),
                YouthTrainer = await ParseYouthTrainerNodeAsync(reader, cancellationToken),
                NextTrainingMatchDate = await reader.ReadXmlValueAsDateTimeAsync(),
                ScoutList = await ParseScoutListNodeAsync(reader, cancellationToken)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<YouthTrainer> ParseYouthTrainerNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            YouthTrainer result = new YouthTrainer
            {
                YouthPlayerId = await reader.ReadXmlValueAsLongAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}