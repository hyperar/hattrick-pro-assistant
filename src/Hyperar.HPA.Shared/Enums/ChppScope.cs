namespace Hyperar.HPA.Shared.Enums
{
    using System;

    [Flags]
    public enum ChppScope : int
    {
        ReadOnly = 1,

        ManageChallenges = 2,

        SetMatchOrders = 4,

        ManageYouthPlayers = 8,

        SetTraining = 16,

        PlaceBid = 32
    }
}