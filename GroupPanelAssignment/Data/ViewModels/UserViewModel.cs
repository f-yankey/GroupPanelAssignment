using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
    }

    public class StudentViewModel : UserViewModel
    {
        public string ProgrammeName { get; set; }
        public decimal CWA { get; set; }
    }

    public class SupervisorsForAssignmentViewModel : UserViewModel
    {
        public bool IsAssigned { get; set; }
    }
}
