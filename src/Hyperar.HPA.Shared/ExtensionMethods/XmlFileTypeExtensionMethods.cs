namespace Hyperar.HPA.Shared.ExtensionMethods
{
    using System;
    using Shared.Enums;

    public static class XmlFileTypeExtensionMethods
    {
        public static XmlFileType ToXmlFileType(this string value)
        {
            return value switch
            {
                Constants.XmlFileName.ArenaDetails => XmlFileType.ArenaDetails,
                Constants.XmlFileName.Avatars => XmlFileType.Avatars,
                Constants.XmlFileName.HallOfFamePlayers => XmlFileType.HallOfFamePlayers,
                Constants.XmlFileName.ManagerCompendium => XmlFileType.ManagerCompendium,
                Constants.XmlFileName.MatchDetails => XmlFileType.MatchDetails,
                Constants.XmlFileName.Matches => XmlFileType.Matches,
                Constants.XmlFileName.MatchLineUp => XmlFileType.MatchLineUp,
                Constants.XmlFileName.Players => XmlFileType.Players,
                Constants.XmlFileName.StaffAvatars => XmlFileType.StaffAvatars,
                Constants.XmlFileName.StaffList => XmlFileType.StaffList,
                Constants.XmlFileName.TeamDetails => XmlFileType.TeamDetails,
                Constants.XmlFileName.WorldDetails => XmlFileType.WorldDetails,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}