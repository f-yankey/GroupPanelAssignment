using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class ScoringSession
    {
        public ScoringSession()
        {
            PanelMemberTeamMemberScores = new HashSet<PanelMemberTeamMemberScore>();
            PanelMemberTeamScores = new HashSet<PanelMemberTeamScore>();
        }

        public int ScoringSessionId { get; set; }
        public int AssignmentSessionId { get; set; }
        public string ScoringSessionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual AssignmentSession AssignmentSession { get; set; }
        public virtual ICollection<PanelMemberTeamMemberScore> PanelMemberTeamMemberScores { get; set; }
        public virtual ICollection<PanelMemberTeamScore> PanelMemberTeamScores { get; set; }
    }
}
