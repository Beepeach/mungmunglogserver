using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace mungmunglogServer.Models
{
    public class History
    {
        public int HistoryId { get; set; }

        // 0: food, 1: snack, 2: pill, 3:hospital
        [Required]
        public int Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

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

    }

    public class HistoryDto
    {
        public HistoryDto(History history)
        {
            DateTime baseTime = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = history.Date;

            long dateTick = date.Ticks - baseTime.Ticks;
            TimeSpan dateTimeSpan = new TimeSpan(dateTick);

            HistoryId = history.HistoryId;
            Type = history.Type;
            Date = dateTimeSpan.TotalSeconds;
            Contents = history.Contents;
            Deleted = history.Deleted;
            FileUrl1 = history.FileUrl1;
            FileUrl2 = history.FileUrl2;
            FileUrl3 = history.FileUrl3;
            FileUrl4 = history.FileUrl4;
            FileUrl5 = history.FileUrl5;
            PetId = history.PetId;
            FamilyMemberId = history.FamilyMemberId;
        }

        public int HistoryId { get; set; }
        public int Type { get; set; }
        public double Date { get; set; }
        public string Contents { get; set; }
        public bool Deleted { get; set; }
        public string FileUrl1 { get; set; }
        public string FileUrl2 { get; set; }
        public string FileUrl3 { get; set; }
        public string FileUrl4 { get; set; }
        public string FileUrl5 { get; set; }

        public int PetId { get; set; }
        public int FamilyMemberId { get; set; }
    }
}

public class HistoryPutModel
{
    public int Type { get; set; }
    public double Date { get; set; }
    public string Contents { get; set; }
    public string FileUrl1 { get; set; }
    public string FileUrl2 { get; set; }
    public string FileUrl3 { get; set; }
    public string FileUrl4 { get; set; }
    public string FileUrl5 { get; set; }
}

public class HistoryPostModel: HistoryPutModel
{
    public int PetId { get; set; }
    public int FamilyMemberId { get; set; }
}