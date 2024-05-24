namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.PlayerDetails;

    public class Player : HattrickEntityBase, IHattrickEntity
    {
        public Player()
        {
            this.Country = new Country();
            this.PlayerSkillSets = new HashSet<PlayerSkillSet>();
            this.Team = new Team();
            this.Matches = new HashSet<PlayerMatch>();

            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }

        public int AgeDays { get; set; }

        public int AgeYears { get; set; }

        public AggressivenessLevel Aggressiveness { get; set; }

        public AgreeabilityLevel Agreeability { get; set; }

        public long? AskingPrice { get; set; }

        public byte[]? AvatarBytes { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public long? BuyingTeamHattrickId { get; set; }

        public string? BuyingTeamName { get; set; }

        public int CareerGoals { get; set; }

        public int CareerHattricks { get; set; }

        public PlayerCategory Category { get; set; }

        public virtual Country Country { get; set; }

        public long CountryHattrickId { get; set; }

        public int CurrentSeasonCupGoals { get; set; }

        public int CurrentSeasonFriendlyGoals { get; set; }

        public int CurrentSeasonLeagueGoals { get; set; }

        public string FirstName { get; set; }

        public int GoalsOnTeam { get; set; }

        public bool HasMotherClubBonus { get; set; }

        public int Health { get; set; }

        public HonestyLevel Honesty { get; set; }

        public bool IsCoach { get; set; }

        public bool IsForeign { get; set; }

        public bool IsTransferListed { get; set; }

        public DateTime JoinedTeamOn { get; set; }

        public int JuniorNationalTeamCaps { get; set; }

        public string LastName { get; set; }

        public SkillLevel Leadership { get; set; }

        public virtual ICollection<PlayerMatch> Matches { get; set; }

        public int MatchesOnTeam { get; set; }

        public DateTime NextBirthDay { get; set; }

        public string? NickName { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<PlayerSkillSet> PlayerSkillSets { get; set; }

        public long Salary { get; set; }

        public int SeniorNationalTeamCaps { get; set; }

        public int? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public string? Statement { get; set; }

        public virtual Team Team { get; set; }

        public long TeamHattrickId { get; set; }

        public int TotalSkillIndex { get; set; }

        public long? WinningBid { get; set; }

        public static Player Create(Models.Player xmlPlayer, Country country, Team team)
        {
            return new Player
            {
                AgeDays = xmlPlayer.AgeDays,
                AgeYears = xmlPlayer.Age,
                Aggressiveness = (AggressivenessLevel)xmlPlayer.Aggressiveness,
                Agreeability = (AgreeabilityLevel)xmlPlayer.Agreeability,
                AskingPrice = xmlPlayer.TransferDetails?.AskingPrice,
                BookingStatus = (BookingStatus)xmlPlayer.Cards,
                BuyingTeamHattrickId = xmlPlayer.TransferDetails?.BidderTeam?.TeamId,
                BuyingTeamName = xmlPlayer.TransferDetails?.BidderTeam?.TeamName,
                CareerGoals = xmlPlayer.CareerGoals,
                CareerHattricks = xmlPlayer.CareerHattricks,
                Category = (PlayerCategory)xmlPlayer.PlayerCategoryId,
                Country = country,
                CurrentSeasonCupGoals = xmlPlayer.CupGoals,
                CurrentSeasonFriendlyGoals = xmlPlayer.FriendliesGoals,
                CurrentSeasonLeagueGoals = xmlPlayer.LeagueGoals,
                FirstName = xmlPlayer.FirstName,
                NickName = xmlPlayer.NickName,
                GoalsOnTeam = xmlPlayer.GoalsCurrentTeam,
                HasMotherClubBonus = xmlPlayer.MotherClubBonus,
                HattrickId = xmlPlayer.PlayerId,
                Health = xmlPlayer.InjuryLevel,
                Honesty = (HonestyLevel)xmlPlayer.Honesty,
                IsCoach = xmlPlayer.TrainerData != null,
                IsForeign = xmlPlayer.IsAbroad,
                IsTransferListed = xmlPlayer.TransferListed,
                JoinedTeamOn = xmlPlayer.ArrivalDate,
                JuniorNationalTeamCaps = xmlPlayer.CapsU20,
                LastName = xmlPlayer.LastName,
                Leadership = (SkillLevel)xmlPlayer.Leadership,
                MatchesOnTeam = xmlPlayer.MatchesCurrentTeam,
                NextBirthDay = xmlPlayer.NextBirthDay,
                Notes = xmlPlayer.OwnerNotes,
                Salary = xmlPlayer.Salary,
                SeniorNationalTeamCaps = xmlPlayer.Caps,
                ShirtNumber = xmlPlayer.PlayerNumber,
                Specialty = (Specialty)xmlPlayer.Specialty,
                Statement = xmlPlayer.Statement,
                Team = team,
                TotalSkillIndex = xmlPlayer.TSI,
                WinningBid = xmlPlayer.TransferDetails?.HighestBid
            };
        }

        public bool HasChanged(Models.Player xmlPlayer)
        {
            return this.AgeDays != xmlPlayer.AgeDays
                || this.AgeYears != xmlPlayer.Age
                || this.AskingPrice != xmlPlayer.TransferDetails?.AskingPrice
                || this.BookingStatus != (BookingStatus)xmlPlayer.Cards
                || this.BuyingTeamHattrickId != xmlPlayer.TransferDetails?.BidderTeam?.TeamId
                || this.BuyingTeamName != xmlPlayer.TransferDetails?.BidderTeam?.TeamName
                || this.CareerGoals != xmlPlayer.CareerGoals
                || this.CareerHattricks != xmlPlayer.CareerHattricks
                || this.Category != (PlayerCategory)xmlPlayer.PlayerCategoryId
                || this.CurrentSeasonCupGoals != xmlPlayer.CupGoals
                || this.CurrentSeasonFriendlyGoals != xmlPlayer.FriendliesGoals
                || this.CurrentSeasonLeagueGoals != xmlPlayer.LeagueGoals
                || this.FirstName != xmlPlayer.FirstName
                || this.GoalsOnTeam != xmlPlayer.GoalsCurrentTeam
                || this.HasMotherClubBonus != xmlPlayer.MotherClubBonus
                || this.Health != xmlPlayer.InjuryLevel
                || this.IsCoach != (xmlPlayer.TrainerData != null)
                || this.IsTransferListed != xmlPlayer.TransferListed
                || this.JoinedTeamOn != xmlPlayer.ArrivalDate
                || this.JuniorNationalTeamCaps != xmlPlayer.CapsU20
                || this.LastName != xmlPlayer.LastName
                || this.MatchesOnTeam != xmlPlayer.MatchesCurrentTeam
                || this.NextBirthDay != xmlPlayer.NextBirthDay
                || this.NickName != xmlPlayer.NickName
                || this.Notes != xmlPlayer.OwnerNotes
                || this.Salary != xmlPlayer.Salary
                || this.SeniorNationalTeamCaps != xmlPlayer.Caps
                || this.ShirtNumber != xmlPlayer.PlayerNumber
                || this.Statement != xmlPlayer.Statement
                || this.TotalSkillIndex != xmlPlayer.TSI
                || this.WinningBid != xmlPlayer.TransferDetails?.HighestBid;
        }

        public void Update(Models.Player xmlPlayer)
        {
            this.AgeDays = xmlPlayer.AgeDays;
            this.AgeYears = xmlPlayer.Age;
            this.AskingPrice = xmlPlayer.TransferDetails?.AskingPrice;
            this.BookingStatus = (BookingStatus)xmlPlayer.Cards;
            this.BuyingTeamHattrickId = xmlPlayer.TransferDetails?.BidderTeam?.TeamId;
            this.BuyingTeamName = xmlPlayer.TransferDetails?.BidderTeam?.TeamName;
            this.CareerGoals = xmlPlayer.CareerGoals;
            this.CareerHattricks = xmlPlayer.CareerHattricks;
            this.Category = (PlayerCategory)xmlPlayer.PlayerCategoryId;
            this.CurrentSeasonCupGoals = xmlPlayer.CupGoals;
            this.CurrentSeasonFriendlyGoals = xmlPlayer.FriendliesGoals;
            this.CurrentSeasonLeagueGoals = xmlPlayer.LeagueGoals;
            this.FirstName = xmlPlayer.FirstName;
            this.GoalsOnTeam = xmlPlayer.GoalsCurrentTeam;
            this.HasMotherClubBonus = xmlPlayer.MotherClubBonus;
            this.Health = xmlPlayer.InjuryLevel;
            this.IsCoach = xmlPlayer.TrainerData != null;
            this.IsTransferListed = xmlPlayer.TransferListed;
            this.JoinedTeamOn = xmlPlayer.ArrivalDate;
            this.JuniorNationalTeamCaps = xmlPlayer.CapsU20;
            this.LastName = xmlPlayer.LastName;
            this.MatchesOnTeam = xmlPlayer.MatchesCurrentTeam;
            this.NextBirthDay = xmlPlayer.NextBirthDay;
            this.NickName = xmlPlayer.NickName;
            this.Notes = xmlPlayer.OwnerNotes;
            this.Salary = xmlPlayer.Salary;
            this.SeniorNationalTeamCaps = xmlPlayer.Caps;
            this.ShirtNumber = xmlPlayer.PlayerNumber;
            this.Statement = xmlPlayer.Statement;
            this.TotalSkillIndex = xmlPlayer.TSI;
            this.WinningBid = xmlPlayer.TransferDetails?.HighestBid;
        }
    }
}