using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.ViewModels
{
    public class UserAddViewModel
    {
        public List<string> Roles { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string Othernames { get; set; }

        [Required]
        public string Surname { get; set; }
        public string SpecialID { get; set; }
        public List<ExtraProperty> ExtraProperties { get; set; }
    }

    public class ExtraProperty
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
