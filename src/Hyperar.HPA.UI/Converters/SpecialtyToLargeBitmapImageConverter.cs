namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Windows.Data;
    using Common.Enums;

    internal class SpecialtyToLargeBitmapImageConverter : SpecialtyToBitmapImageConverterBase, IValueConverter
    {
        private const string headSpecialistFileName = "head-specialist-30.png";

        private const string powerfulFileName = "powerful-30.png";

        private const string quickFileName = "quick-30.png";

        private const string resilientFileName = "resilient-30.png";

        private const string supportFileName = "support-30.png";

        private const string technicalFileName = "technical-30.png";

        private const string unpredictableFileName = "unpredictable-30.png";

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