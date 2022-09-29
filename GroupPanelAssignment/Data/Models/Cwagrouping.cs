using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class Cwagrouping
    {
        public int CwagroupingId { get; set; }
        public int AssignmentSessionId { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual AssignmentSession AssignmentSession { get; set; }
    }
}
