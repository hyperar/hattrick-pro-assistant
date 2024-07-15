namespace Hyperar.HPA.Infrastructure.Features.Download.Parse.Strategies
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Application.Interfaces;
    using Infrastructure.Features.Download.Parse.Constants;
    using Infrastructure.Features.Download.Parse.Strategies.ExtensionMethods;
    using Shared.Models.Hattrick.Interfaces;
    using Shared.Models.Hattrick.YouthPlayerDetails;

    public class YouthPlayerDetails : ParserBase, IParserStrategy
    {
        public async Task ParseAsync(XmlReader reader, IXmlFile file, CancellationToken cancellationToken)
        {
            HattrickData result = (HattrickData)file;

            result.YouthPlayer = await ParseYouthPlayerNodeAsync(reader, cancellationToken);
        }

        private static async Task<LastMatch> ParseLastMatchNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            LastMatch result = new LastMatch
            {
                YouthMatchId = await reader.ReadXmlValueAsLongAsync(),
                Date = await reader.ReadXmlValueAsDateTimeAsync(),
                PositionCode = await reader.ReadXmlValueAsIntAsync(),
                PlayedMinutes = await reader.ReadXmlValueAsIntAsync(),
                Rating = await reader.ReadXmlValueAsDecimalAsync()
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<OwningYouthTeam> ParseOwningYouthTeamNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            OwningYouthTeam result = new OwningYouthTeam
            {
                YouthTeamId = await reader.ReadXmlValueAsLongAsync(),
                YouthTeamName = await reader.ReadElementContentAsStringAsync(),
                YouthTeamLeagueId = reader.CheckNode(NodeName.YouthTeamLeagueId) ? await reader.ReadXmlValueAsLongAsync() : null,
                SeniorTeam = await ParseIdNameNodeAsync(reader, cancellationToken)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<PlayerSkills> ParsePlayerSkillsNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            PlayerSkills result = new PlayerSkills
            {
                KeeperSkill = await ParseSkillNodeAsync(reader, cancellationToken),
                KeeperSkillMax = await ParseSkillMaxNodeAsync(reader, cancellationToken),
                DefenderSkill = await ParseSkillNodeAsync(reader, cancellationToken),
                DefenderSkillMax = await ParseSkillMaxNodeAsync(reader, cancellationToken),
                PlaymakerSkill = await ParseSkillNodeAsync(reader, cancellationToken),
                PlaymakerSkillMax = await ParseSkillMaxNodeAsync(reader, cancellationToken),
                WingerSkill = await ParseSkillNodeAsync(reader, cancellationToken),
                WingerSkillMax = await ParseSkillMaxNodeAsync(reader, cancellationToken),
                PassingSkill = await ParseSkillNodeAsync(reader, cancellationToken),
                PassingSkillMax = await ParseSkillMaxNodeAsync(reader, cancellationToken),
                ScorerSkill = await ParseSkillNodeAsync(reader, cancellationToken),
                ScorerSkillMax = await ParseSkillMaxNodeAsync(reader, cancellationToken),
                SetPiecesSkill = await ParseSkillNodeAsync(reader, cancellationToken),
                SetPiecesSkillMax = await ParseSkillMaxNodeAsync(reader, cancellationToken),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<ScoutCall> ParseScoutCallNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            ScoutCall result = new ScoutCall
            {
                Scout = await ParseIdNameNodeAsync(reader, cancellationToken),
                ScoutRegionId = await reader.ReadXmlValueAsLongAsync(),
                ScoutComments = await ParseScoutCommentsNodeAsync(reader, cancellationToken)
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<ScoutComment> ParseScoutCommentNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            ScoutComment result = new ScoutComment
            {
                CommentText = await reader.ReadElementContentAsStringAsync(),
                CommentType = await reader.ReadXmlValueAsIntAsync(),
                CommentVariation = await reader.ReadXmlValueAsIntAsync(),
                CommentSkillLevel = await reader.ReadXmlValueAsIntAsync(),
                CommentSkillType = await reader.ReadXmlValueAsIntAsync(),
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<List<ScoutComment>> ParseScoutCommentsNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            List<ScoutComment> result = new List<ScoutComment>();

            if (!reader.IsEmptyElement)
            {
                // Reads opening element.
                await reader.ReadAsync();

                while (reader.CheckNode(NodeName.ScoutComment))
                {
                    result.Add(
                        await ParseScoutCommentNodeAsync(
                            reader,
                            cancellationToken));
                }
            }

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }

        private static async Task<SkillMax> ParseSkillMaxNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            SkillMax result = new SkillMax
            {
                IsAvailable = bool.Parse(reader.GetAttribute(NodeName.IsAvailable) ?? bool.FalseString),
                MayUnlock = bool.Parse(reader.GetAttribute(NodeName.MayUnlock) ?? bool.FalseString),
                Value = await reader.ReadXmlValueAsNullableIntAsync()
            };

            return result;
        }

        private static async Task<Skill> ParseSkillNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            Skill result = new Skill
            {
                IsAvailable = bool.Parse(reader.GetAttribute(NodeName.IsAvailable) ?? bool.FalseString),
                IsMaxReached = bool.Parse(reader.GetAttribute(NodeName.IsMaxReached) ?? bool.FalseString),
                MayUnlock = bool.Parse(reader.GetAttribute(NodeName.MayUnlock) ?? bool.FalseString),
                Value = await reader.ReadXmlValueAsNullableIntAsync()
            };

            return result;
        }

        private static async Task<YouthPlayer> ParseYouthPlayerNodeAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            // Reads opening element.
            await reader.ReadAsync();

            YouthPlayer result = new YouthPlayer
            {
                YouthPlayerId = await reader.ReadXmlValueAsLongAsync(),
                FirstName = await reader.ReadElementContentAsStringAsync(),
                NickName = await reader.ReadXmlValueAsNullableStringAsync(),
                LastName = await reader.ReadElementContentAsStringAsync(),
                Age = await reader.ReadXmlValueAsIntAsync(),
                AgeDays = await reader.ReadXmlValueAsIntAsync(),
                ArrivalDate = await reader.ReadXmlValueAsDateTimeAsync(),
                CanBePromotedIn = await reader.ReadXmlValueAsIntAsync(),
                PlayerNumber = await reader.ReadXmlValueAsNullableIntAsync(100),
                Statement = await reader.ReadXmlValueAsNullableStringAsync(),
                NativeCountryId = await reader.ReadXmlValueAsLongAsync(),
                NativeCountryName = await reader.ReadElementContentAsStringAsync(),
                OwnerNotes = await reader.ReadXmlValueAsNullableStringAsync(),
                PlayerCategoryId = await reader.ReadXmlValueAsIntAsync(),
                Cards = await reader.ReadXmlValueAsIntAsync(),
                InjuryLevel = await reader.ReadXmlValueAsIntAsync(),
                Specialty = await reader.ReadXmlValueAsIntAsync(),
                CareerGoals = await reader.ReadXmlValueAsIntAsync(),
                CareerHattricks = await reader.ReadXmlValueAsIntAsync(),
                LeagueGoals = await reader.ReadXmlValueAsIntAsync(),
                FriendlyGoals = await reader.ReadXmlValueAsIntAsync(),
                OwningYouthTeam = await ParseOwningYouthTeamNodeAsync(reader, cancellationToken),
                PlayerSkills = await ParsePlayerSkillsNodeAsync(reader, cancellationToken),
                ScoutCall = reader.CheckNode(NodeName.ScoutCall) ? await ParseScoutCallNodeAsync(reader, cancellationToken) : null,
                LastMatch = reader.CheckNode(NodeName.LastMatch) ? await ParseLastMatchNodeAsync(reader, cancellationToken) : null
            };

            // Reads closing element.
            await reader.ReadAsync();

            return result;
        }
    }
}