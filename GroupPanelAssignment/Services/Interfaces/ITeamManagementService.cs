using GroupPanelAssignment.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Services.Interfaces
{
    public interface ITeamManagementService
    {
        List<TeamViewModel> GetTeams();
    }
}
