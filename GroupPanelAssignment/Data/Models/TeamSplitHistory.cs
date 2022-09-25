using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class TeamSplitHistory
    {
        public int TeamSplitHistoryId { get; set; }
        public int TeamId { get; set; }
        public int ParentTeamId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Team ParentTeam { get; set; }
        public virtual Team Team { get; set; }
    }
}
