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
        public List<TeamViewModel> Teams { get; set; }

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
            InitializePage();
        }

        public IActionResult OnPostGroup(TeamAutoCreationViewModel Input)
        {
            Teams = _teamManagementService.AutoGroup(Input);
            HttpContext.Session.Set(ApplicationConstants.TeamsInSession,Teams);
            InitializePage();
            return Page();
        }

        public async Task<IActionResult> OnPostSave()
        {
            Teams = HttpContext.Session.Get<List<TeamViewModel>>(ApplicationConstants.TeamsInSession);
            var result = await _teamManagementService.SaveGroupingAsync(Teams);
            //InitializePage();
            return RedirectToPage(Url.Page("GroupManagement", "Groups"));
        }

        private void InitializePage()
        {
            StudentSelectList = _userManagementService.GetUsersSelectList(ApplicationConstants.StudentRole);
            SupervisorSelectList = _userManagementService.GetUsersSelectList(ApplicationConstants.SupervisorRole);
        }
    }
}
