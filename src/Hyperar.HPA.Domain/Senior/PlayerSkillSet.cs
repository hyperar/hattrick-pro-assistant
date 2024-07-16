namespace Hyperar.HPA.Domain.Senior
{
    using Domain.Interfaces;
    using Shared.Enums;

    public class PlayerSkillSet : AuditableEntityBase, IAuditableEntity
    {
        public SkillLevel Defending { get; set; }

        public SkillLevel Experience { get; set; }

        public SkillLevel Form { get; set; }

        public SkillLevel Goalkeeping { get; set; }

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
    }
}