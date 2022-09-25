using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class Location
    {
        public Location()
        {
            Panels = new HashSet<Panel>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<Panel> Panels { get; set; }
    }
}
