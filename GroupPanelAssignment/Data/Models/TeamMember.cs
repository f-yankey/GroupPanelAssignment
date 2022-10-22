using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class TeamMember
    {
        public TeamMember()
        {
            PanelMemberTeamMemberScores = new HashSet<PanelMemberTeamMemberScore>();
        }

        public int TeamMemberId { get; set; }
        public int TeamId { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Team Team { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<PanelMemberTeamMemberScore> PanelMemberTeamMemberScores { get; set; }
    }
}
