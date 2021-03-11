using System;
namespace mungmunglogServer.Models
{
    public class WalkPath
    {
        public int WalkPathId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DateTime LocationBasedTime { get; set; }

        public int WalkHistoryId { get; set; }
        public WalkHistory WalkHistory { get; set; }
    }
}
