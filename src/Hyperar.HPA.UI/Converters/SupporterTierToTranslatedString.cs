namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Hyperar.HPA.Common.Enums;

    internal class SupporterTierToTranslatedString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is SupporterTier supporterTier
                ? supporterTier switch
                {
                    SupporterTier.None => Globalization.Strings.NoSupporter,
                    SupporterTier.Silver => Globalization.Strings.Silver,
                    SupporterTier.Gold => Globalization.Strings.Gold,
                    SupporterTier.Platinum => Globalization.Strings.Platinum,
                    SupporterTier.Diamond => Globalization.Strings.Diamond,
                    _ => throw new ArgumentOutOfRangeException(nameof(value))
                }
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(nameof(this.ConvertBack));
        }
    }
}