using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupPanelAssignment.Pages.UserManagement
{
    public class NewUserModel : PageModel
    {
        private readonly IUserManagementService _userManagementService;
        public List<Claim> Claims { get; set; }

        public UserAddModel Input { get; set; }

        public class UserAddModel
        {
            public List<int> Roles { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string Othernames { get; set; }
            public string Surname { get; set; }
            public string SpecialID { get; set; }
            public List<ExtraProperty> ExtraProperties { get; set; }
        }

        public class ExtraProperty
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
        
        public NewUserModel(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        public void OnGet()
        {
            Claims = _userManagementService.GetAllClaims();
            Input = new UserAddModel();
        }

        public IActionResult OnPost(UserAddModel Input)
        {
            return Page();
        }
    }
}
