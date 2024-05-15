namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.Players;

    public class PlayerSkillSet : EntityBase, IEntity
    {
        public SkillLevel Defending { get; set; }

        public SkillLevel Experience { get; set; }

        public SkillLevel Form { get; set; }

        public SkillLevel Keeper { get; set; }

        public SkillLevel Loyalty { get; set; }

        public SkillLevel Passing { get; set; }

        public virtual Player Player { get; set; } = new Player();

        public long PlayerHattrickId { get; set; }

        public SkillLevel Playmaking { get; set; }

        public SkillLevel Scoring { get; set; }

        public int Season { get; set; }

        public SkillLevel SetPieces { get; set; }

        public SkillLevel Stamina { get; set; }

        public int Week { get; set; }

        public SkillLevel Winger { get; set; }

        public static PlayerSkillSet Create(
            Models.Player xmlPlayer,
            Player player,
            int season,
            int week)
        {
            return new PlayerSkillSet
            {
                Defending = (SkillLevel)xmlPlayer.DefenderSkill,
                Experience = (SkillLevel)xmlPlayer.Experience,
                Form = (SkillLevel)xmlPlayer.PlayerForm,
                Keeper = (SkillLevel)xmlPlayer.KeeperSkill,
                Loyalty = (SkillLevel)xmlPlayer.Loyalty,
                Passing = (SkillLevel)xmlPlayer.PassingSkill,
                Player = player,
                Playmaking = (SkillLevel)xmlPlayer.PlaymakerSkill,
                Scoring = (SkillLevel)xmlPlayer.ScorerSkill,
                Season = season,
                SetPieces = (SkillLevel)xmlPlayer.SetPiecesSkill,
                Stamina = (SkillLevel)xmlPlayer.StaminaSkill,
                Week = week,
                Winger = (SkillLevel)xmlPlayer.WingerSkill
            };
        }

        public bool HasChanged(Models.Player xmlPlayer)
        {
            return this.Defending != (SkillLevel)xmlPlayer.DefenderSkill
                || this.Experience != (SkillLevel)xmlPlayer.Experience
                || this.Form != (SkillLevel)xmlPlayer.PlayerForm
                || this.Keeper != (SkillLevel)xmlPlayer.KeeperSkill
                || this.Loyalty != (SkillLevel)xmlPlayer.Loyalty
                || this.Passing != (SkillLevel)xmlPlayer.PassingSkill
                || this.Playmaking != (SkillLevel)xmlPlayer.PlaymakerSkill
                || this.Scoring != (SkillLevel)xmlPlayer.ScorerSkill
                || this.SetPieces != (SkillLevel)xmlPlayer.SetPiecesSkill
                || this.Stamina != (SkillLevel)xmlPlayer.StaminaSkill
                || this.Winger != (SkillLevel)xmlPlayer.WingerSkill;
        }

        public void Update(Models.Player xmlPlayer)
        {
            this.Defending = (SkillLevel)xmlPlayer.DefenderSkill;
            this.Experience = (SkillLevel)xmlPlayer.Experience;
            this.Form = (SkillLevel)xmlPlayer.PlayerForm;
            this.Keeper = (SkillLevel)xmlPlayer.KeeperSkill;
            this.Loyalty = (SkillLevel)xmlPlayer.Loyalty;
            this.Passing = (SkillLevel)xmlPlayer.PassingSkill;
            this.Playmaking = (SkillLevel)xmlPlayer.PlaymakerSkill;
            this.Scoring = (SkillLevel)xmlPlayer.ScorerSkill;
            this.SetPieces = (SkillLevel)xmlPlayer.SetPiecesSkill;
            this.Stamina = (SkillLevel)xmlPlayer.StaminaSkill;
            this.Winger = (SkillLevel)xmlPlayer.WingerSkill;
        }
    }
}