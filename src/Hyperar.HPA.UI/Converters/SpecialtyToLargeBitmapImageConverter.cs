namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Windows.Data;
    using Common.Enums;

    internal class SpecialtyToLargeBitmapImageConverter : SpecialtyToBitmapImageConverterBase, IValueConverter
    {
        private const string headSpecialistFileName = "head-30.png";

        private const string powerfulFileName = "strong-30.png";

        private const string quickFileName = "exercise-30.png";

        private const string resilientFileName = "health-30.png";

        private const string supportFileName = "connection-30.png";

        private const string technicalFileName = "gears-30.png";

        private const string unpredictableFileName = "swap-30.png";

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