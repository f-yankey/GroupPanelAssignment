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
            AppUserClaims = new HashSet<AppUserClaim>();
            PanelMembers = new HashSet<PanelMember>();
            TeamMembers = new HashSet<TeamMember>();
            TeamSupervisors = new HashSet<TeamSupervisor>();
            UserRoles = new HashSet<UserRole>();
        }

        public string UserId { get; set; }
        public string Username { get; set; }
        public string SpecialId { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<AppUserAssignmentSession> AppUserAssignmentSessions { get; set; }
        public virtual ICollection<AppUserClaim> AppUserClaims { get; set; }
        public virtual ICollection<PanelMember> PanelMembers { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<TeamSupervisor> TeamSupervisors { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
