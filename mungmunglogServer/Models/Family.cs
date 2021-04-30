﻿using System;
using System.Collections.Generic;
using mungmunglogServer.Data;

namespace mungmunglogServer.Models
{
    public class Family
    {
        public int FamilyId { get; set; }

        // 유효기간이 있는 코드는 어떻게 생성할까??
        public string InvitationCode { get; set; }
        public DateTime CodeExpirationDate { get; set; }

        public List<FamilyMember> FamilyMembers { get; set; }
        public List<Pet> Pets { get; set; }
    }

    public class FamilyDto
    {
        public FamilyDto(Family family, ApplicationDbContext _context)
        {
            DateTime baseTime = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime codeExpirationDate = family.CodeExpirationDate;
            long codeExpirationDateTicks = codeExpirationDate.Ticks - baseTime.Ticks;
            TimeSpan codeExpirationDateTimeSpan = new TimeSpan(codeExpirationDateTicks);

            FamilyId = family.FamilyId;
            InvitationCode = family.InvitationCode;
            CodeExpirationDate = codeExpirationDateTimeSpan.TotalSeconds;
        }

        public FamilyDto(Family family)
        {
            DateTime baseTime = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime codeExpirationDate = family.CodeExpirationDate;
            long codeExpirationDateTicks = codeExpirationDate.Ticks - baseTime.Ticks;
            TimeSpan codeExpirationDateTimeSpan = new TimeSpan(codeExpirationDateTicks);

            FamilyId = family.FamilyId;
            InvitationCode = family.InvitationCode;
            CodeExpirationDate = codeExpirationDateTimeSpan.TotalSeconds;
        }

        public int FamilyId { get; set; }
        public string InvitationCode { get; set; }
        public double CodeExpirationDate { get; set; }
        public List<FamilyMemberDto> FamilyMembers { get; set; }
        public List<PetDto> Pets { get; set; }
    }

    public class InvitationCodeRequestModel
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }

    
}
