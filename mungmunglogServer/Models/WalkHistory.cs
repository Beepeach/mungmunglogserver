using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace mungmunglogServer.Models
{
    public class WalkHistory
    {
        public int WalkHistoryId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public double Distance { get; set; }

        [MaxLength(1000)]
        public string Contents { get; set; }

        public bool Deleted { get; set; }

        public string FileUrl1 { get; set; }
        [NotMapped]
        public IFormFile AttachmentFile1 { get; set; }

        public string FileUrl2 { get; set; }
        [NotMapped]
        public IFormFile AttachmentFile2 { get; set; }

        public string FileUrl3 { get; set; }
        [NotMapped]
        public IFormFile AttachmentFile3 { get; set; }

        public string FileUrl4 { get; set; }
        [NotMapped]
        public IFormFile AttachmentFile4 { get; set; }

        public string FileUrl5 { get; set; }
        [NotMapped]
        public IFormFile AttachmentFile5 { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }

        public int FamilyMemberId { get; set; }
        public FamilyMember FamilyMember { get; set; }

        public List<WalkPath> WalkPaths { get; set; }
    }

    public class WalkHistoryDto
    {
        public WalkHistoryDto(WalkHistory walkHistory)
        {
            var baseTime = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var startTime = walkHistory.StartTime;
            long startTimeTicks = startTime.Ticks - baseTime.Ticks;
            TimeSpan startTimeSpan = new TimeSpan(startTimeTicks);

            var endTime = walkHistory.EndTime;
            long endTimeTicks = endTime.Ticks - baseTime.Ticks;
            TimeSpan endTimeSpan = new TimeSpan(endTimeTicks);

            WalkHistoryId = walkHistory.WalkHistoryId;
            StartTime = startTimeSpan.TotalSeconds;
            EndTime = endTimeSpan.TotalSeconds;
            Distance = walkHistory.Distance;
            Contents = walkHistory.Contents;
            Deleted = walkHistory.Deleted;
            FileUrl1 = walkHistory.FileUrl1;
            FileUrl2 = walkHistory.FileUrl2;
            FileUrl3 = walkHistory.FileUrl3;
            FileUrl4 = walkHistory.FileUrl4;
            FileUrl5 = walkHistory.FileUrl5;

            PetId = walkHistory.PetId;
            FamilyMemberId = walkHistory.FamilyMemberId;
        }

        public int WalkHistoryId { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public double Distance { get; set; }
        public string Contents { get; set; }
        public bool Deleted { get; set; }
        public string FileUrl1 { get; set; }
        public string FileUrl2 { get; set; }
        public string FileUrl3 { get; set; }
        public string FileUrl4 { get; set; }
        public string FileUrl5 { get; set; }

        public int PetId { get; set; }
        public int FamilyMemberId { get; set; }

        //public List<WalkPathDto> WalkPaths { get; set; }
    }

    public class WalkHistoryPutModel
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public double Distance { get; set; }
        public string Contents { get; set; }
        public string FileUrl1 { get; set; }
        public string FileUrl2 { get; set; }
        public string FileUrl3 { get; set; }
        public string FileUrl4 { get; set; }
        public string FileUrl5 { get; set; }
    }

    public class WalkHistoryPostModel: WalkHistoryPutModel
    {
        public int PetId { get; set; }
        public int FamilyMemberId { get; set; }
    }
}
