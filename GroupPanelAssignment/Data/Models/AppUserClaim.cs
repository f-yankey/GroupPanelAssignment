using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class AppUserClaim
    {
        public int AppUserClaimId { get; set; }
        public int AssignmentSessionId { get; set; }
        public string UserId { get; set; }
        public int ClaimId { get; set; }
        public string Value { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual AssignmentSession AssignmentSession { get; set; }
        public virtual Claim Claim { get; set; }
        public virtual AppUser User { get; set; }
    }
}
