namespace Hyperar.HPA.Common.ExtensionMethods
{
    using System;
    using Hyperar.HPA.Common.Enums;

    public static class MatchStatusExtensionMethods
    {
        public static MatchStatus ToMatchStatus(this string value)
        {
            return value switch
            {
                Constants.MatchStatus.Upcoming => MatchStatus.Upcoming,
                Constants.MatchStatus.Ongoing => MatchStatus.Ongoing,
                Constants.MatchStatus.Finished => MatchStatus.Finished,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}