namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Common.Enums;

    internal class DeltaIntToSkillDeltaEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int delta ? delta == 0 ? SkillDelta.None : delta > 0 ? SkillDelta.Increase : (object)SkillDelta.Decrease : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}