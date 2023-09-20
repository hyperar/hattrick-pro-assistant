namespace Hyperar.HPA.Common.ExtensionMethods
{
    using System;
    using Hyperar.HPA.Common.Enums;

    public static class SupporterTierExtensionMethods
    {
        public static SupporterTier ToSupporterTier(this string value)
        {
            switch (value)
            {
                case Constants.SupporterTier.None:
                    return SupporterTier.None;

                case Constants.SupporterTier.Silver:
                    return SupporterTier.Silver;

                case Constants.SupporterTier.Gold:
                    return SupporterTier.Gold;

                case Constants.SupporterTier.Platinum:
                    return SupporterTier.Platinum;

                case Constants.SupporterTier.Diamond:
                    return SupporterTier.Diamond;

                default:
                    throw new ArgumentException($"Unrecognized Supporter Tier Value: '{value}'.");
            }
        }
    }
}