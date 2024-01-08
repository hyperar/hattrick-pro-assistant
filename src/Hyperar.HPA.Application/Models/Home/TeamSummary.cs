namespace Hyperar.HPA.Application.Models.Home
{
    using System;

    public class TeamOverview
    {
        public Manager Manager { get; set; } = new Manager();

        public SeniorTeam SeniorTeam { get; set; } = new SeniorTeam();
    }
}