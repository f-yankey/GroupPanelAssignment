using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class AppUser
    {
        public AppUser()
        {
            PanelMembers = new HashSet<PanelMember>();
            TeamMembers = new HashSet<TeamMember>();
            TeamSupervisors = new HashSet<TeamSupervisor>();
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string SpecialId { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string Surname { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<PanelMember> PanelMembers { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<TeamSupervisor> TeamSupervisors { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
