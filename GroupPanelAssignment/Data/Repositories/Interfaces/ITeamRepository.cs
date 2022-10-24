using GroupPanelAssignment.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        List<TeamViewModel> GetAll();
        Task DeleteAllCurrentSessionGroupsAsync();
        Task AddTeamAsync(TeamViewModel team);
    }
}
