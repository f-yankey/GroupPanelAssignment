using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GroupPanelAssignment.Pages.GroupManagement
{
    public class AutomaticGroupingModel : PageModel
    {
        private ITeamManagementService _teamManagementService;
        private IUserManagementService _userManagementService;

        public TeamAutoCreationViewModel Input { get; set; }
        public SelectList StudentSelectList { get; set; }
        public SelectList SupervisorSelectList { get; set; }

        public AutomaticGroupingModel
            (
            ITeamManagementService teamManagementService,
            IUserManagementService userManagementService
            )
        {
            _teamManagementService = teamManagementService;
            _userManagementService = userManagementService;
        }

        public void OnGet()
        {
            StudentSelectList = _userManagementService.GetUsersSelectList(ApplicationConstants.StudentRole);
            SupervisorSelectList = _userManagementService.GetUsersSelectList(ApplicationConstants.SupervisorRole);
        }

        public IActionResult OnPost(TeamAutoCreationViewModel Input)
        {
            var results = _teamManagementService.AutoGroup(Input);
            return Page();
        }
    }
}