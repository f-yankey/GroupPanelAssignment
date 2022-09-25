using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class PanelMember
    {
        public PanelMember()
        {
            PanelMemberTeamMemberScores = new HashSet<PanelMemberTeamMemberScore>();
            PanelMemberTeamScores = new HashSet<PanelMemberTeamScore>();
        }

        public int PanelMemberId { get; set; }
        public int PanelId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Panel Panel { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<PanelMemberTeamMemberScore> PanelMemberTeamMemberScores { get; set; }
        public virtual ICollection<PanelMemberTeamScore> PanelMemberTeamScores { get; set; }
    }
}
