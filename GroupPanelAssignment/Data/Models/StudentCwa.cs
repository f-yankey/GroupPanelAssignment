using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class StudentCwa
    {
        public int StudentCwaId { get; set; }
        public int AssignmentSessionId { get; set; }
        public int UserId { get; set; }
        public decimal Cwa { get; set; }
        public int? ProgrammestreamId { get; set; }
        public string ProgrammestreamName { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual AssignmentSession AssignmentSession { get; set; }
        public virtual AppUser User { get; set; }
    }
}
