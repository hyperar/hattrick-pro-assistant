namespace Hyperar.HPA.Shared.Models.Hattrick.PlayerDetails
{
    using System;

    public class TransferDetails
    {
        public long AskingPrice { get; set; }

        public IdName? BidderTeam { get; set; }

        public DateTime Deadline { get; set; }

        public long HighestBid { get; set; }
    }
}