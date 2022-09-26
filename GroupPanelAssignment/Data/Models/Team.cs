using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class Team
    {
        public Team()
        {
            PanelMemberTeamScores = new HashSet<PanelMemberTeamScore>();
            PanelTeams = new HashSet<PanelTeam>();
            TeamMembers = new HashSet<TeamMember>();
            TeamSplitHistoryParentTeams = new HashSet<TeamSplitHistory>();
            TeamSplitHistoryTeams = new HashSet<TeamSplitHistory>();
            TeamSupervisors = new HashSet<TeamSupervisor>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Topic { get; set; }
        public int AssignmentSessionId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual AssignmentSession AssignmentSession { get; set; }
        public virtual ICollection<PanelMemberTeamScore> PanelMemberTeamScores { get; set; }
        public virtual ICollection<PanelTeam> PanelTeams { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<TeamSplitHistory> TeamSplitHistoryParentTeams { get; set; }
        public virtual ICollection<TeamSplitHistory> TeamSplitHistoryTeams { get; set; }
        public virtual ICollection<TeamSupervisor> TeamSupervisors { get; set; }
    }
}
