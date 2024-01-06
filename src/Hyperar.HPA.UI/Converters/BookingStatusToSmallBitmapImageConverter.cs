namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Windows.Data;
    using Hyperar.HPA.Common.Enums;

    internal class BookingStatusToSmallBitmapImageConverter : BookingStatusConverterBase, IValueConverter
    {
        private const string doubleYellowCardFileName = "double-yellow-card-16.png";

        private const string redCardFileName = "red-card-16.png";

        private const string yellowCardFileName = "yellow-card-16.png";

        protected override string GetFileNameFromBookingStatus(BookingStatus bookingStatus)
        {
            return bookingStatus switch
            {
                BookingStatus.NoBookings => transparentFileName,
                BookingStatus.OneYellowCard => yellowCardFileName,
                BookingStatus.TwoYellowCards => doubleYellowCardFileName,
                BookingStatus.Suspended => redCardFileName,
                _ => throw new ArgumentOutOfRangeException(nameof(bookingStatus))
            };
        }
    }
}