using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace mungmunglogServer.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(15)]
        public string NickName { get; set; }

        [Required]
        [MaxLength(15)]
        public string Relationship { get; set; }
        [Required]
        public bool Gender { get; set; }

        public string FileUrl { get; set; }

        [NotMapped]
        public IFormFile AttachmentFile { get; set; }
    }
}
