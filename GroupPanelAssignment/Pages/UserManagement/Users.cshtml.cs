using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupPanelAssignment.Pages.UserManagement
{
    public class UsersModel : PageModel
    {
        private readonly IUserManagementService _userManagementService;

        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }

        public List<UserViewModel> Results { get; set; }

        public UsersModel(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        public void OnGet()
        {
            string roleParam = CustomStringHelper.ConvertToSpaced(Role);
            Results = _userManagementService.GetAppUsers(roleParam);
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
