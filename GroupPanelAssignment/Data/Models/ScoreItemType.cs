using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class ScoreItemType
    {
        public ScoreItemType()
        {
            ScoreItems = new HashSet<ScoreItem>();
        }

        public int ScoreItemTypeId { get; set; }
        public string ScoreItemTypeName { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<ScoreItem> ScoreItems { get; set; }
    }
}
