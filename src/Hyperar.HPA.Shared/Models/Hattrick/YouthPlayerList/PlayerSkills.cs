namespace Hyperar.HPA.Shared.Models.Hattrick.YouthPlayerList
{
    public class PlayerSkills
    {
        public Skill DefenderSkill { get; set; } = new Skill();

        public SkillMax DefenderSkillMax { get; set; } = new SkillMax();

        public Skill KeeperSkill { get; set; } = new Skill();

        public SkillMax KeeperSkillMax { get; set; } = new SkillMax();

        public Skill PassingSkill { get; set; } = new Skill();

        public SkillMax PassingSkillMax { get; set; } = new SkillMax();

        public Skill PlaymakerSkill { get; set; } = new Skill();

        public SkillMax PlaymakerSkillMax { get; set; } = new SkillMax();

        public Skill ScorerSkill { get; set; } = new Skill();

        public SkillMax ScorerSkillMax { get; set; } = new SkillMax();

        public Skill SetPiecesSkill { get; set; } = new Skill();

        public SkillMax SetPiecesSkillMax { get; set; } = new SkillMax();

        public Skill WingerSkill { get; set; } = new Skill();

        public SkillMax WingerSkillMax { get; set; } = new SkillMax();
    }
}