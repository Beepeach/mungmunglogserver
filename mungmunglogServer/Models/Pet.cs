using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace mungmunglogServer.Models
{
    public class Pet
    {
        public int PetId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public bool Gender { get; set; }

        public string FileUrl { get; set; }
        [NotMapped]
        public IFormFile AttachmentFile { get; set; }

        public List<History> Histories { get; set; }
        public List<WalkHistory> WalkHistories { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}
