﻿namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Common.Enums;

    internal abstract class BookingStatusToImageConverterBase : ResourceImageValueConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is BookingStatus bookingStatus
                ? ConvertByteArrayToBitMapImage(
                    ReadImageFromResources(
                        GetFileNameFromBookingStatus(bookingStatus)))
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected abstract string GetFileNameFromBookingStatus(BookingStatus bookingStatus);
    }
}