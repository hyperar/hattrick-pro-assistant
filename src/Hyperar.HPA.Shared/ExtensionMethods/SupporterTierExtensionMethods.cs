namespace Hyperar.HPA.Shared.ExtensionMethods
{
    using System;
    using Shared.Enums;

    public static class SupporterTierExtensionMethods
    {
        public static SupporterTier ToSupporterTier(this string value)
        {
            return value switch
            {
                Constants.SupporterTier.None => SupporterTier.None,
                Constants.SupporterTier.Silver => SupporterTier.Silver,
                Constants.SupporterTier.Gold => SupporterTier.Gold,
                Constants.SupporterTier.Platinum => SupporterTier.Platinum,
                Constants.SupporterTier.Diamond => SupporterTier.Diamond,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}