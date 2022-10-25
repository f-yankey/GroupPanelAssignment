using X.PagedList;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GroupPanelAssignment.Utils;

namespace GroupPanelAssignment.Pages.GroupManagement
{
    public class GroupsModel : PageModel
    {
        private ITeamManagementService _teamManagementService;

        public string SearchText { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? PageNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 9;

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

        public IActionResult OnPost(InputModel Input)
        {
            Teams = _teamManagementService.GetTeams(Input.SearchText).ToPagedList();

            return Page();
        }

        public IActionResult OnGetDownload()
        {
            var results = _teamManagementService.GetGroupingCSV();
            return File(results, ApplicationConstants.CSVContentType,"groups.csv");
        }
    }
}
