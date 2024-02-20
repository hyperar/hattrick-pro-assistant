namespace Hyperar.HPA.Application.Hattrick.MatchLineUp
{
    using Common.Enums;

    public class StartingPlayer
    {
        public MatchRoleBehavior? Behavior { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string NickName { get; set; } = string.Empty;

        public uint PlayerId { get; set; }

        public ushort RoleId { get; set; }
    }
}