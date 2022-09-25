using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class PanelMemberTeamMemberScore
    {
        public int PanelMemberTeamMemberScoreId { get; set; }
        public int ScoringSessionId { get; set; }
        public int PanelMemberId { get; set; }
        public int TeamMemberId { get; set; }
        public int SessionScoreItemId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual PanelMember PanelMember { get; set; }
        public virtual ScoringSession ScoringSession { get; set; }
        public virtual SessionScoreItem SessionScoreItem { get; set; }
        public virtual TeamMember TeamMember { get; set; }
    }
}
