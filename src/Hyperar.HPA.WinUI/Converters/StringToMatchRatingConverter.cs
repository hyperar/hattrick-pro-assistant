namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Globalization;
    using System.IO;
    using Avalonia.Data.Converters;
    using Avalonia.Media.Imaging;
    using Avalonia.Platform;
    using Shared.Constants;

    internal class StringToMatchRatingConverter : IValueConverter
    {
        private const string BlueBigStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_big_blue.png";

        private const string BlueHalfStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_half_blue.png";

        private const string BlueWholeStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_blue.png";

        private const string BrownHalfStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_half_brown.png";

        private const string BrownWholeStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_brown.png";

        private const string RedHalfStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_half_red.png";

        private const string RedWholeStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_red.png";

        private const string YellowBigStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_big_yellow.png";

        private const string YellowHalfStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_half_yellow.png";

        private const string YellowToBrownStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_yellow_to_brown.png";

        private const string YellowToRedStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_yellow_to_red.png";

        private const string YellowWholeStar = "avares://Hyperar.HPA.WinUI/Assets/Images/star_yellow.png";

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string ratingCode)
            {
                var resourceUri = new Uri(
                    this.GetStarResourceUri(
                        ratingCode));

                using (Stream resource = AssetLoader.Open(resourceUri))
                {
                    return new Bitmap(resource);
                }
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetStarResourceUri(string ratingCode)
        {
            return ratingCode switch
            {
                MatchRatingStars.BlueBigStar => BlueBigStar,
                MatchRatingStars.BlueHalfStar => BlueHalfStar,
                MatchRatingStars.BlueWholeStar => BlueWholeStar,
                MatchRatingStars.BrownHalfStar => BrownHalfStar,
                MatchRatingStars.BrownWholeStar => BrownWholeStar,
                MatchRatingStars.RedHalfStar => RedHalfStar,
                MatchRatingStars.RedWholeStar => RedWholeStar,
                MatchRatingStars.YellowBigStar => YellowBigStar,
                MatchRatingStars.YellowHalfStar => YellowHalfStar,
                MatchRatingStars.YellowToBrownStar => YellowToBrownStar,
                MatchRatingStars.YellowToRedStar => YellowToRedStar,
                MatchRatingStars.YellowWholeStar => YellowWholeStar,
                _ => throw new ArgumentOutOfRangeException(nameof(ratingCode)),
            };
        }
    }
}