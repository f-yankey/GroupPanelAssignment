using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupPanelAssignment.Pages.GroupManagement
{
    public class GroupsModel : PageModel
    {
        private ITeamManagementService _teamManagementService;

        public List<TeamViewModel> Teams { get; set; }

        public GroupsModel(ITeamManagementService teamManagementService)
        {
            _teamManagementService = teamManagementService;
        }
        public void OnGet()
        {
            Teams = _teamManagementService.GetTeams();
        }
    }
}
