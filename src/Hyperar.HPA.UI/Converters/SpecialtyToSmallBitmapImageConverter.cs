namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Windows.Data;
    using Hyperar.HPA.Common.Enums;

    internal class SpecialtyToSmallBitmapImageConverter : SpecialtyToBitmapImageConverterBase, IValueConverter
    {
        private const string headSpecialistFileName = "head-16.png";

        private const string powerfulFileName = "strong-16.png";

        private const string quickFileName = "exercise-16.png";

        private const string resilientFileName = "health-16.png";

        private const string supportFileName = "connection-16.png";

        private const string technicalFileName = "gears-16.png";

        private const string unpredictableFileName = "swap-16.png";

        protected override string GetFileNameFromSpecialty(Specialty specialty)
        {
            return specialty switch
            {
                Specialty.NoSpecialty => transparentFileName,
                Specialty.Technical => technicalFileName,
                Specialty.Quick => quickFileName,
                Specialty.Powerful => powerfulFileName,
                Specialty.Unpredictable => unpredictableFileName,
                Specialty.HeadSpecialist => headSpecialistFileName,
                Specialty.Resilient => resilientFileName,
                Specialty.Support => supportFileName,
                _ => throw new ArgumentOutOfRangeException(nameof(specialty))
            };
        }
    }
}