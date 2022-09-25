using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class PanelTeam
    {
        public int PanelTeamId { get; set; }
        public int PanelId { get; set; }
        public int TeamId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Panel Panel { get; set; }
        public virtual Team Team { get; set; }
    }
}
