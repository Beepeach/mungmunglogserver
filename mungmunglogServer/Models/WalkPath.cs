using System;
namespace mungmunglogServer.Models
{
    // WalkPath를 하나하나 저장하는게 맞는건가?? 일단 보류!!
    public class WalkPath
    {
        public int WalkPathId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DateTime LocationBasedTime { get; set; }

        public int WalkHistoryId { get; set; }
        public WalkHistory WalkHistory { get; set; }
    }

    public class WalkPathDto
    {
        public WalkPathDto(WalkPath walkPath)
        {
            Latitude = walkPath.Latitude;
            Longitude = walkPath.Longitude;

            DateTime baseTime = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime locationBasedTime = walkPath.LocationBasedTime;
            long locationBasedTimeTicks = locationBasedTime.Ticks - baseTime.Ticks;
            TimeSpan locationBasedTimeSpan = new TimeSpan(locationBasedTimeTicks);

            LocationBasedTime = locationBasedTimeSpan.TotalSeconds;
            WalkHistoryId = walkPath.WalkHistoryId;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double LocationBasedTime { get; set; }

        public int WalkHistoryId { get; set; }
    }

    public class WalkPathPutModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double LocationBasedTime { get; set; }
    }

    public class WalkPathPostModel: WalkPathPutModel
    {

    }

}

