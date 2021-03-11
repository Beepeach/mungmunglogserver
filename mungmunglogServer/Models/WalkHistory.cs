﻿using System;
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

        public List<WalkPath> walkPaths { get; set; }
    }
}
