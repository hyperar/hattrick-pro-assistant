namespace Hyperar.HPA.Application.Models.Home
{
    using System;

    public class TeamOverview
    {
        public Manager Manager { get; set; } = new Manager();

        public Team Team { get; set; } = new Team();
    }
}