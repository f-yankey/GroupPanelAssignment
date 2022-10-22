using X.PagedList;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupPanelAssignment.Pages.GroupManagement
{
    public class GroupsModel : PageModel
    {
        private ITeamManagementService _teamManagementService;

        [BindProperty(SupportsGet = true)]
        public int? PageNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        public IPagedList<TeamViewModel> Teams { get; set; }

        public string PageTitle { get; set; }
        public InputModel Input { get; set; }
        public class InputModel
        {
            public string SearchText { get; set; }
        }

        public GroupsModel(ITeamManagementService teamManagementService)
        {
            _teamManagementService = teamManagementService;
        }
        public void OnGet()
        {
            Teams = _teamManagementService.GetTeams().ToPagedList();
        }
    }
}
