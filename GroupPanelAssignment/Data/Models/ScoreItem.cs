using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class ScoreItem
    {
        public ScoreItem()
        {
            SessionScoreItems = new HashSet<SessionScoreItem>();
        }

        public int ScoreItemId { get; set; }
        public string ScoreItemName { get; set; }
        public decimal MinimumScore { get; set; }
        public decimal MaximumScore { get; set; }
        public int ScoreItemTypeId { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ScoreItemType ScoreItemType { get; set; }
        public virtual ICollection<SessionScoreItem> SessionScoreItems { get; set; }
    }
}
