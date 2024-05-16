namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Globalization;
    using Avalonia.Data.Converters;
    using Avalonia.Media;
    using Shared.Enums;

    internal class SkillLevelToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Color color = Color.FromArgb(255, 39, 127, 49);

            if (value is SkillLevel skillLevel)
            {
                switch (skillLevel)
                {
                    case SkillLevel.NonExistent:
                    case SkillLevel.Disastrous:
                    case SkillLevel.Wretched:
                        color = Color.FromArgb(255, 221, 65, 64);
                        break;

                    case SkillLevel.Poor:
                    case SkillLevel.Weak:
                        color = Color.FromArgb(255, 245, 161, 4);
                        break;

                    case SkillLevel.Inadequate:
                    case SkillLevel.Passable:
                        color = Color.FromArgb(255, 241, 196, 10);
                        break;

                    case SkillLevel.Solid:
                    case SkillLevel.Excellent:
                    case SkillLevel.Formidable:
                        color = Color.FromArgb(255, 89, 150, 93);
                        break;
                }
            }

            return new SolidColorBrush(color);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}