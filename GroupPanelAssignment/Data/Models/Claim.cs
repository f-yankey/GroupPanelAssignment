using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class Claim
    {
        public Claim()
        {
            AppUserClaims = new HashSet<AppUserClaim>();
        }

        public int ClaimId { get; set; }
        public string ClaimName { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<AppUserClaim> AppUserClaims { get; set; }
    }
}
