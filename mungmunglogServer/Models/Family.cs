using System;
using System.Collections.Generic;

namespace mungmunglogServer.Models
{
    public class Family
    {
        public int FamilyId { get; set; }

        // 유효기간이 있는 코드는 어떻게 생성할까??
        public string InvitationCode { get; set; }
        public DateTime? CodeExpirationDate { get; set; }

        public List<FamilyMember> FamilyMembers { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
