namespace Hyperar.HPA.Shared.ExtensionMethods
{
    using System;
    using Shared.Enums;

    public static class MatchSystemExtensionMethods
    {
        public static MatchSystem ToMatchSystem(this string value)
        {
            return value.ToLower() switch
            {
                Constants.MatchSystem.Hattrick => MatchSystem.Hattrick,
                Constants.MatchSystem.HtoIntegrated => MatchSystem.HtoIntegrated,
                Constants.MatchSystem.Youth => MatchSystem.Youth,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}