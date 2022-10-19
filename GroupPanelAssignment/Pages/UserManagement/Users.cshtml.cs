using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace GroupPanelAssignment.Pages.UserManagement
{
    public class UsersModel : PageModel
    {
        private readonly IUserManagementService _userManagementService;

        [BindProperty(SupportsGet = true)]
        public int? PageNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public string RoleFilter { get; set; }

        public IPagedList<UserViewModel> Results { get; set; }

        public string PageTitle { get; set; }
        public InputModel Input { get; set; }
        public class InputModel
        {
            public string SearchText { get; set; }
            public string Role { get; set; }
        }
        
        public UsersModel(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        public void OnGet()
        {
            RoleFilter = "All";
            PageNumber = PageNumber == null ? 1 : PageNumber;
            Results = _userManagementService.GetAppUsers(RoleFilter).ToPagedList((int)PageNumber, PageSize);
            SetPageTitle();
        }

        public IActionResult OnPost(InputModel Input)
        {
            RoleFilter = Input.Role;
            PageNumber = PageNumber == null ? 1 : PageNumber;
            Results = _userManagementService.GetAppUsers(Input.Role).ToPagedList((int)PageNumber,PageSize);
            SetPageTitle();
            return Page();
        }

        private void SetPageTitle()
        {
            PageTitle = CustomStringHelper.Capitalize(CustomStringHelper.ConvertToSpaced(RoleFilter));
        }
    }
}
