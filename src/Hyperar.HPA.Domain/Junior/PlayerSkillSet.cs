namespace Hyperar.HPA.Domain.Junior
{
    using Domain.Interfaces;
    using Shared.Enums;

    public class PlayerSkillSet : AuditableEntityBase, IAuditableEntity
    {
        public SkillLevel? Defending { get; set; }

        public SkillLevel? DefendingMax { get; set; }

        public SkillLevel? Goalkeeping { get; set; }

        public SkillLevel? GoalkeepingMax { get; set; }

        public bool IsDefendingMaxReached { get; set; }

        public bool IsGoalkeepingMaxReached { get; set; }

        public bool IsPassingMaxReached { get; set; }

        public bool IsPlaymakingMaxReached { get; set; }

        public bool IsScoringMaxReached { get; set; }

        public bool IsSetPiecesMaxReached { get; set; }

        public bool IsWingerMaxReached { get; set; }

        public SkillLevel? Passing { get; set; }

        public SkillLevel? PassingMax { get; set; }

        public virtual Player Player { get; set; } = new Player();

        public long PlayerHattrickId { get; set; }

        public SkillLevel? Playmaking { get; set; }

        public SkillLevel? PlaymakingMax { get; set; }

        public SkillLevel? Scoring { get; set; }

        public SkillLevel? ScoringMax { get; set; }

        public int Season { get; set; }

        public SkillLevel? SetPieces { get; set; }

        public SkillLevel? SetPiecesMax { get; set; }

        public int Week { get; set; }

        public SkillLevel? Winger { get; set; }

        public SkillLevel? WingerMax { get; set; }
    }
}