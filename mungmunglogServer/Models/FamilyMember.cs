using System;
using System.Collections.Generic;
using System.Linq;

namespace mungmunglogServer.Models
{
    public class FamilyMember
    {
        public int FamilyMemberId { get; set; }

        public bool IsMaster { get; set; }

        // 0: 거절, 1: 승낙, 2: 보류
        public int Status { get; set; }

        public string UserId { get; set; }

        public int? FamilyId { get; set; }
        public Family Family { get; set; }

        public List<History> Histories { get; set; }
        public List<WalkHistory> WalkHistories { get; set; }

    }

    public class FamilyMemberDto
    {
        public FamilyMemberDto(FamilyMember familyMember)
        {
            FamilyMemberId = familyMember.FamilyMemberId;
            IsMaster = familyMember.IsMaster;
            Status = familyMember.Status;
            UserId = familyMember.UserId;
            FamilyId = familyMember.FamilyId;

            if (familyMember.Histories == null && familyMember.WalkHistories == null)
            {
                Histories = null;
                WalkHistories = null;
            }
            else if (familyMember.Histories != null && familyMember.WalkHistories != null)
            {
                Histories = familyMember.Histories
                .OrderByDescending(h => h.Date)
                .Select(h => new HistoryDto(h))
                .ToList();

                WalkHistories = familyMember.WalkHistories
                .OrderByDescending(h => h.EndTime)
                .Select(h => new WalkHistoryDto(h))
                .ToList();
            }
            else if (familyMember.Histories == null)
            {
                Histories = null;

                WalkHistories = familyMember.WalkHistories
               .OrderByDescending(h => h.EndTime)
               .Select(h => new WalkHistoryDto(h))
               .ToList();
            }
            else if (familyMember.WalkHistories == null)
            {
                Histories = Histories = familyMember.Histories
                .OrderByDescending(h => h.Date)
                .Select(h => new HistoryDto(h))
                .ToList();

                WalkHistories = null;
            }
        }

        public int FamilyMemberId { get; set; }
        public bool IsMaster { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public int? FamilyId { get; set; }

        public List<HistoryDto> Histories { get; set; }
        public List<WalkHistoryDto> WalkHistories { get; set; }
    }

    public class FamilyMemberPutModel
    {
        public bool IsMaster { get; set; }
        public int Status { get; set; }
    };

    public class FamilyMemberPostModel: FamilyMemberPutModel
    {
        public string Code { get; set; }
        public string Email { get; set; }
    };
}
