﻿using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class AssignmentSession
    {
        public AssignmentSession()
        {
            Panels = new HashSet<Panel>();
            ScoringSessions = new HashSet<ScoringSession>();
            SessionScoreItems = new HashSet<SessionScoreItem>();
            Teams = new HashSet<Team>();
        }

        public int AssignmentSessionId { get; set; }
        public string SessionName { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<Panel> Panels { get; set; }
        public virtual ICollection<ScoringSession> ScoringSessions { get; set; }
        public virtual ICollection<SessionScoreItem> SessionScoreItems { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
