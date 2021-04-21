using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using mungmunglogServer.Data;

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


    public class PetDto
    {
        public PetDto(Pet pet)
        {
            DateTime baseDate = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime birthday = pet.Birthday;

            long birthdayTicks = birthday.Ticks - baseDate.Ticks;

            TimeSpan birthdaySpan = new TimeSpan(birthdayTicks);

            PetId = pet.PetId;
            Name = pet.Name;
            Birthday = birthdaySpan.TotalSeconds;
            Breed = pet.Breed;
            Gender = pet.Gender;
            FileUrl = pet.FileUrl;
            FamilyId = pet.FamilyId;

            var historyDtoList = pet.Histories
                .OrderByDescending(h => h.Date)
                .Select(h => new HistoryDto(h))
                .ToList();

            var walkHistoryDtoList = pet.WalkHistories
                .OrderByDescending(h => h.EndTime)
                .Select(h => new WalkHistoryDto(h))
                .ToList();

            Histories = historyDtoList;
            WalkHistories = walkHistoryDtoList;
        }

        public int PetId { get; set; }
        public string Name { get; set; }
        public double Birthday { get; set; }
        public string Breed { get; set; }
        public bool Gender { get; set; }
        public string FileUrl { get; set; }

        public int FamilyId { get; set; }

        public List<HistoryDto> Histories { get; set; }
        public List<WalkHistoryDto> WalkHistories { get; set; }
    }

    public class PetPutModel
    {
        public string Name { get; set; }
        public double Birthday { get; set; }
        public string Breed { get; set; }
        public bool Gender { get; set; }
        public string FileUrl { get; set; }
    }

    public class PetPostModel: PetPutModel
    {
        public string Email { get; set; }
      
    }

    
}