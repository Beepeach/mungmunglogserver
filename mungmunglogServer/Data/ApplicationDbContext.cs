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
        public DbSet<History> History { get; set; }
        public DbSet<WalkHistory> WalkHistory { get; set; }
        public DbSet<WalkPath> WalkPath { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 제대로 인식을 못할때 사용할 코드

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Pet>()
        //        .HasOne(pet => pet.Family)
        //        .WithMany(family => family.Pets);

        //    builder.Entity<FamilyMember>()
        //        .HasOne(familyMember => familyMember.Family)
        //        .WithMany(family => family.FamilyMembers);

        //    builder.Entity<History>()
        //        .HasOne(history => history.Pet)
        //        .WithMany(pet => pet.Histories);
        //    builder.Entity<History>()
        //      .HasOne(history => history.FamilyMember)
        //      .WithMany(familyMember => familyMember.Histories);

        //    builder.Entity<WalkHistory>()
        //        .HasOne(walkHistory => walkHistory.Pet)
        //        .WithMany(pet => pet.WalkHistories);
        //    builder.Entity<WalkHistory>()
        //        .HasOne(walkHistory => walkHistory.FamilyMember)
        //        .WithMany(familyMember => familyMember.WalkHistories);

        //    builder.Entity<WalkPath>()
        //        .HasOne(walkPath => walkPath.WalkHistory)
        //        .WithMany(walkHistory => walkHistory.walkPaths);

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
