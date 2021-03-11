using System;
using System.Collections.Generic;

namespace mungmunglogServer.Models
{
    public class FamilyMember
    {
        public int FamilyMemberId { get; set; }

        public bool IsMaster { get; set; }

        // 0: 거절, 1: 승낙, 2: 보류
        public int Status { get; set; }

        public string UserId { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }

        public List<History> Histories { get; set; }
        public List<WalkHistory> WalkHistories { get; set; }

    }
}
