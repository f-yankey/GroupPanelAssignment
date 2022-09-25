using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class Panel
    {
        public Panel()
        {
            PanelMembers = new HashSet<PanelMember>();
            PanelTeams = new HashSet<PanelTeam>();
        }

        public int PanelId { get; set; }
        public string PanelName { get; set; }
        public int AssignmentSessionId { get; set; }
        public int? LocationId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual AssignmentSession AssignmentSession { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<PanelMember> PanelMembers { get; set; }
        public virtual ICollection<PanelTeam> PanelTeams { get; set; }
    }
}
