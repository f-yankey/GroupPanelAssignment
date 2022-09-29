using System;
using System.Collections.Generic;

#nullable disable

namespace GroupPanelAssignment.Data.Models
{
    public partial class AppUser
    {
        public AppUser()
        {
            AppUserAssignmentSessions = new HashSet<AppUserAssignmentSession>();
            PanelMembers = new HashSet<PanelMember>();
            StudentCwas = new HashSet<StudentCwa>();
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

        public virtual ICollection<AppUserAssignmentSession> AppUserAssignmentSessions { get; set; }
        public virtual ICollection<PanelMember> PanelMembers { get; set; }
        public virtual ICollection<StudentCwa> StudentCwas { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<TeamSupervisor> TeamSupervisors { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
