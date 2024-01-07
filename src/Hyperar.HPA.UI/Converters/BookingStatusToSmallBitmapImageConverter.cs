namespace Hyperar.HPA.UI.Converters
{
    using System;
    using System.Windows.Data;
    using Common.Enums;

    internal class BookingStatusToSmallBitmapImageConverter : BookingStatusToImageConverterBase, IValueConverter
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