using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mungmunglogServer.Models;

namespace mungmunglogServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Pet> Pet { get; set; }
        public DbSet<Family> Family { get; set; }
        public DbSet<FamilyMember> FamilyMember { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<FamilyMember>()
        //        .HasOne(familyMember => familyMember.Family)
        //        .WithMany(family => family)

        //    base.OnModelCreating(builder);
        //}
    }

    public class User: IdentityUser
    {

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

        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}
