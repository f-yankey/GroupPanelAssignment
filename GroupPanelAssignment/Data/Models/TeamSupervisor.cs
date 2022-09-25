using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class TeamSupervisor
    {
        public int TeamSupervisorId { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Team Team { get; set; }
        public virtual AppUser User { get; set; }
    }
}
