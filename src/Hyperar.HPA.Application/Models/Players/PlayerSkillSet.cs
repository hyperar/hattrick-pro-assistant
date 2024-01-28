namespace Hyperar.HPA.Application.Models.Players
{
    using Common.Enums;

    public class PlayerSkillSet
    {
        public SkillLevel Defending { get; set; }

        public SkillLevel Experience { get; set; }

        public SkillLevel Form { get; set; }

        public SkillLevel Keeper { get; set; }

        public SkillLevel Loyalty { get; set; }

        public SkillLevel Passing { get; set; }

        public SkillLevel Playmaking { get; set; }

        public SkillLevel Scoring { get; set; }

        public SkillLevel SetPieces { get; set; }

        public Specialty Specialty { get; set; }

        public SkillLevel Stamina { get; set; }

        public SkillLevel Winger { get; set; }
    }
}