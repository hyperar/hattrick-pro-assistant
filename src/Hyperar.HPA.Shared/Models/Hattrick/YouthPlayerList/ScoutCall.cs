namespace Hyperar.HPA.Shared.Models.Hattrick.YouthPlayerList
{
    using System.Collections.Generic;

    public class ScoutCall
    {
        public IdName Scout { get; set; } = new IdName();

        public List<ScoutComment> ScoutComments { get; set; } = new List<ScoutComment>();

        public long ScoutRegionId { get; set; }
    }
}