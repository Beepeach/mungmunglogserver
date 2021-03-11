using System;
using System.Collections.Generic;

namespace mungmunglogServer.Models
{
    public class Family
    {
        public int FamilyId { get; set; }

        public List<FamilyMember> FamilyMembers { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
