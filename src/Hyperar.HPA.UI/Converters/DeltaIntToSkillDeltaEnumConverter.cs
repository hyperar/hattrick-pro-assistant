namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Hyperar.HPA.Common.Enums;

    internal class DeltaIntToSkillDeltaEnumConverter : IValueConverter
    {
        private const string plus = "+";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string deltaString)
            {
                return string.IsNullOrWhiteSpace(deltaString) || deltaString == "0"
                    ? SkillDelta.None
                    : deltaString.StartsWith(plus) ? SkillDelta.Increase : (object)SkillDelta.Decrease;
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}