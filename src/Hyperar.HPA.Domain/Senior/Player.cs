namespace Hyperar.HPA.Domain.Senior
{
    using System;
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.Players;

    public class Player : HattrickEntityBase, IHattrickEntity
    {
        public Player()
        {
            this.Country = new Country();
            this.PlayerSkillSets = new HashSet<PlayerSkillSet>();
            this.Team = new Team();

            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }

        public byte AgeDays { get; set; }

        public byte AgeYears { get; set; }

        public AggressivenessLevel Aggressiveness { get; set; }

        public AgreeabilityLevel Agreeability { get; set; }

        public byte[]? AvatarBytes { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public short CareerGoals { get; set; }

        public short CareerHattricks { get; set; }

        public PlayerCategory Category { get; set; }

        public virtual Country Country { get; set; }

        public long CountryHattrickId { get; set; }

        public short CurrentSeasonCupGoals { get; set; }

        public short CurrentSeasonFriendlyGoals { get; set; }

        public short CurrentSeasonLeagueGoals { get; set; }

        public bool EnrolledOnNationalTeam { get; set; }

        public string FirstName { get; set; }

        public short GoalsOnTeam { get; set; }

        public bool HasMotherClubBonus { get; set; }

        public short Health { get; set; }

        public HonestyLevel Honesty { get; set; }

        public bool IsCoach { get; set; }

        public bool IsForeign { get; set; }

        public bool IsTransferListed { get; set; }

        public DateTime JoinedTeamOn { get; set; }

        public short JuniorNationalTeamCaps { get; set; }

        public string LastName { get; set; }

        public SkillLevel Leadership { get; set; }

        public short MatchesOnTeam { get; set; }

        public string? NickName { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<PlayerSkillSet> PlayerSkillSets { get; set; }

        public long Salary { get; set; }

        public short SeniorNationalTeamCaps { get; set; }

        public byte? ShirtNumber { get; set; }

        public Specialty Specialty { get; set; }

        public string? Statement { get; set; }

        public virtual Team Team { get; set; }

        public long TeamHattrickId { get; set; }

        public int TotalSkillIndex { get; set; }

        public static Player Create(Models.Player xmlPlayer, Country country, Team team)
        {
            return new Player
            {
                AgeDays = xmlPlayer.AgeDays,
                AgeYears = xmlPlayer.Age,
                Aggressiveness = (AggressivenessLevel)xmlPlayer.Aggressiveness,
                Agreeability = (AgreeabilityLevel)xmlPlayer.Agreeability,
                BookingStatus = (BookingStatus)xmlPlayer.Cards,
                CareerGoals = xmlPlayer.CareerGoals,
                CareerHattricks = xmlPlayer.CareerHattricks,
                Category = (PlayerCategory)xmlPlayer.PlayerCategoryId,
                Country = country,
                CurrentSeasonCupGoals = xmlPlayer.CupGoals,
                CurrentSeasonFriendlyGoals = xmlPlayer.FriendliesGoals,
                CurrentSeasonLeagueGoals = xmlPlayer.LeagueGoals,
                EnrolledOnNationalTeam = xmlPlayer.NationalTeamId != null,
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
                Notes = xmlPlayer.OwnerNotes,
                Salary = xmlPlayer.Salary,
                SeniorNationalTeamCaps = xmlPlayer.Caps,
                ShirtNumber = xmlPlayer.PlayerNumber,
                Specialty = (Specialty)xmlPlayer.Specialty,
                Statement = xmlPlayer.Statement,
                Team = team,
                TotalSkillIndex = xmlPlayer.Tsi
            };
        }

        public bool HasChanged(Models.Player xmlPlayer)
        {
            return this.AgeDays != xmlPlayer.AgeDays
                || this.AgeYears != xmlPlayer.Age
                || this.BookingStatus != (BookingStatus)xmlPlayer.Cards
                || this.CareerGoals != xmlPlayer.CareerGoals
                || this.CareerHattricks != xmlPlayer.CareerHattricks
                || this.Category != (PlayerCategory)xmlPlayer.PlayerCategoryId
                || this.CurrentSeasonCupGoals != xmlPlayer.CupGoals
                || this.CurrentSeasonFriendlyGoals != xmlPlayer.FriendliesGoals
                || this.CurrentSeasonLeagueGoals != xmlPlayer.LeagueGoals
                || this.EnrolledOnNationalTeam != (xmlPlayer.NationalTeamId != null)
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
                || this.NickName != xmlPlayer.NickName
                || this.Notes != xmlPlayer.OwnerNotes
                || this.Salary != xmlPlayer.Salary
                || this.SeniorNationalTeamCaps != xmlPlayer.Caps
                || this.ShirtNumber != xmlPlayer.PlayerNumber
                || this.Statement != xmlPlayer.Statement
                || this.TotalSkillIndex != xmlPlayer.Tsi;
        }

        public void Update(Models.Player xmlPlayer)
        {
            this.AgeDays = xmlPlayer.AgeDays;
            this.AgeYears = xmlPlayer.Age;
            this.BookingStatus = (BookingStatus)xmlPlayer.Cards;
            this.CareerGoals = xmlPlayer.CareerGoals;
            this.CareerHattricks = xmlPlayer.CareerHattricks;
            this.Category = (PlayerCategory)xmlPlayer.PlayerCategoryId;
            this.CurrentSeasonCupGoals = xmlPlayer.CupGoals;
            this.CurrentSeasonFriendlyGoals = xmlPlayer.FriendliesGoals;
            this.CurrentSeasonLeagueGoals = xmlPlayer.LeagueGoals;
            this.EnrolledOnNationalTeam = xmlPlayer.NationalTeamId != null;
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
            this.NickName = xmlPlayer.NickName;
            this.Notes = xmlPlayer.OwnerNotes;
            this.Salary = xmlPlayer.Salary;
            this.SeniorNationalTeamCaps = xmlPlayer.Caps;
            this.ShirtNumber = xmlPlayer.PlayerNumber;
            this.Statement = xmlPlayer.Statement;
            this.TotalSkillIndex = xmlPlayer.Tsi;
        }
    }
}