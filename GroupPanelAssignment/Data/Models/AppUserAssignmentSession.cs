using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class AppUserAssignmentSession
    {
        public int AppUserAssignmentSessionId { get; set; }
        public int UserId { get; set; }
        public int AssignmentSessionId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual AssignmentSession AssignmentSession { get; set; }
        public virtual AppUser User { get; set; }
    }
}
