namespace Hyperar.HPA.Domain
{
    using System;
    using Common.Enums;
    using Domain.Interfaces;

    public class SeniorPlayerSkill : EntityBase, IEntity
    {
        public SkillLevel Defending { get; set; }

        public SkillLevel Experience { get; set; }

        public SkillLevel Form { get; set; }

        public SkillLevel Keeper { get; set; }

        public SkillLevel Loyalty { get; set; }

        public SkillLevel Passing { get; set; }

        public SkillLevel Playmaking { get; set; }

        public SkillLevel Scoring { get; set; }

        public virtual SeniorPlayer SeniorPlayer { get; set; } = new SeniorPlayer();

        public SkillLevel SetPieces { get; set; }

        public SkillLevel Stamina { get; set; }

        public DateTime UpdatedOn { get; set; }

        public SkillLevel Winger { get; set; }
    }
}