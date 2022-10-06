using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class AssignmentSession
    {
        public AssignmentSession()
        {
            AppUserAssignmentSessions = new HashSet<AppUserAssignmentSession>();
            AppUserClaims = new HashSet<AppUserClaim>();
            CwaGroupings = new HashSet<CwaGrouping>();
            Panels = new HashSet<Panel>();
            ScoringSessions = new HashSet<ScoringSession>();
            SessionScoreItems = new HashSet<SessionScoreItem>();
            Teams = new HashSet<Team>();
        }

        public int AssignmentSessionId { get; set; }
        public string SessionName { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<AppUserAssignmentSession> AppUserAssignmentSessions { get; set; }
        public virtual ICollection<AppUserClaim> AppUserClaims { get; set; }
        public virtual ICollection<CwaGrouping> CwaGroupings { get; set; }
        public virtual ICollection<Panel> Panels { get; set; }
        public virtual ICollection<ScoringSession> ScoringSessions { get; set; }
        public virtual ICollection<SessionScoreItem> SessionScoreItems { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
