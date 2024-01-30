namespace Hyperar.HPA.Domain
{
    using Common.Enums;
    using Domain.Interfaces;

    public class PlayerSkillSet : EntityBase, IEntity
    {
        public SkillLevel Defending { get; set; }

        public SkillLevel Experience { get; set; }

        public SkillLevel Form { get; set; }

        public SkillLevel Keeper { get; set; }

        public SkillLevel Loyalty { get; set; }

        public SkillLevel Passing { get; set; }

        public virtual Player Player { get; set; } = new Player();

        public SkillLevel Playmaking { get; set; }

        public SkillLevel Scoring { get; set; }

        public uint Season { get; set; }

        public SkillLevel SetPieces { get; set; }

        public SkillLevel Stamina { get; set; }

        public uint Week { get; set; }

        public SkillLevel Winger { get; set; }
    }
}