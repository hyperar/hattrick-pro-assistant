namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;
    using Models = Shared.Models.Hattrick.PlayerDetails;

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
                Form = (SkillLevel)xmlPlayer.PlayerForm,
                Stamina = (SkillLevel)xmlPlayer.PlayerSkills.StaminaSkill,
                Keeper = (SkillLevel)xmlPlayer.PlayerSkills.KeeperSkill,
                Defending = (SkillLevel)xmlPlayer.PlayerSkills.DefenderSkill,
                Playmaking = (SkillLevel)xmlPlayer.PlayerSkills.PlaymakerSkill,
                Winger = (SkillLevel)xmlPlayer.PlayerSkills.WingerSkill,
                Passing = (SkillLevel)xmlPlayer.PlayerSkills.PassingSkill,
                Scoring = (SkillLevel)xmlPlayer.PlayerSkills.ScorerSkill,
                SetPieces = (SkillLevel)xmlPlayer.PlayerSkills.SetPiecesSkill,
                Experience = (SkillLevel)xmlPlayer.Experience,
                Loyalty = (SkillLevel)xmlPlayer.Loyalty,
                Season = season,
                Week = week,
                Player = player
            };
        }

        public bool HasChanged(Models.Player xmlPlayer)
        {
            return this.Stamina != (SkillLevel)xmlPlayer.PlayerSkills.StaminaSkill
                || this.Defending != (SkillLevel)xmlPlayer.PlayerSkills.DefenderSkill
                || this.Form != (SkillLevel)xmlPlayer.PlayerForm
                || this.Keeper != (SkillLevel)xmlPlayer.PlayerSkills.KeeperSkill
                || this.Playmaking != (SkillLevel)xmlPlayer.PlayerSkills.PlaymakerSkill
                || this.Winger != (SkillLevel)xmlPlayer.PlayerSkills.WingerSkill
                || this.Passing != (SkillLevel)xmlPlayer.PlayerSkills.PassingSkill
                || this.Scoring != (SkillLevel)xmlPlayer.PlayerSkills.ScorerSkill
                || this.SetPieces != (SkillLevel)xmlPlayer.PlayerSkills.SetPiecesSkill
                || this.Experience != (SkillLevel)xmlPlayer.Experience
                || this.Loyalty != (SkillLevel)xmlPlayer.Loyalty;
        }

        public void Update(Models.Player xmlPlayer)
        {
            this.Form = (SkillLevel)xmlPlayer.PlayerForm;
            this.Stamina = (SkillLevel)xmlPlayer.PlayerSkills.StaminaSkill;
            this.Keeper = (SkillLevel)xmlPlayer.PlayerSkills.KeeperSkill;
            this.Defending = (SkillLevel)xmlPlayer.PlayerSkills.DefenderSkill;
            this.Playmaking = (SkillLevel)xmlPlayer.PlayerSkills.PlaymakerSkill;
            this.Winger = (SkillLevel)xmlPlayer.PlayerSkills.WingerSkill;
            this.Passing = (SkillLevel)xmlPlayer.PlayerSkills.PassingSkill;
            this.Scoring = (SkillLevel)xmlPlayer.PlayerSkills.ScorerSkill;
            this.SetPieces = (SkillLevel)xmlPlayer.PlayerSkills.SetPiecesSkill;
            this.Experience = (SkillLevel)xmlPlayer.Experience;
            this.Loyalty = (SkillLevel)xmlPlayer.Loyalty;
        }
    }
}