namespace Hyperar.HPA.WinUI.Converters
{
    using System;
    using System.Globalization;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Data.Converters;
    using Hyperar.HPA.Shared.Enums;

    internal class EnumToImageConverter : IValueConverter
    {
        private const string MissingIconKey = "MissingIcon";
        private const string CardIconKey = "CardIcon";
        private const string TechnicalIconKey = "TechnicalIcon";
        private const string QuickIconKey = "QuickIcon";
        private const string PowerfulIconKey = "PowerfulIcon";
        private const string UnpredictableIconKey = "UnpredictableIcon";
        private const string HeadSpecialistIconKey = "HeadSpecialistIcon";
        private const string ResilientIconKey = "ResilientIcon";
        private const string SupportIconKey = "SupportIcon";

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is BookingStatus bookingStatus)
            {
                if (parameter is BookingStatus compareValue)
                {
                    return bookingStatus == compareValue
                         ? Application.Current?.FindResource(CardIconKey) ?? Application.Current?.FindResource(MissingIconKey)
                         : null;
                }
                else
                {
                    return bookingStatus switch
                    {
                        BookingStatus.NoBookings => null,
                        _ => Application.Current?.FindResource(CardIconKey) ?? Application.Current?.FindResource(MissingIconKey)
                    };
                }

            }
            else if (value is Specialty specialty)
            {
                return specialty switch
                {
                    Specialty.Technical => Application.Current?.FindResource(TechnicalIconKey) ?? Application.Current?.FindResource(MissingIconKey),
                    Specialty.Quick => Application.Current?.FindResource(QuickIconKey) ?? Application.Current?.FindResource(MissingIconKey),
                    Specialty.Powerful => Application.Current?.FindResource(PowerfulIconKey) ?? Application.Current?.FindResource(MissingIconKey),
                    Specialty.Unpredictable => Application.Current?.FindResource(UnpredictableIconKey) ?? Application.Current?.FindResource(MissingIconKey),
                    Specialty.HeadSpecialist => Application.Current?.FindResource(HeadSpecialistIconKey) ?? Application.Current?.FindResource(MissingIconKey),
                    Specialty.Resilient => Application.Current?.FindResource(ResilientIconKey) ?? Application.Current?.FindResource(MissingIconKey),
                    Specialty.Support => Application.Current?.FindResource(SupportIconKey) ?? Application.Current?.FindResource(MissingIconKey),
                    _ => null
                };
            }
            else
            {
                return null;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}