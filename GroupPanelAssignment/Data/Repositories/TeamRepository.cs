using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories
{
    public class TeamRepository : BaseRepository, ITeamRepository
    {
        public TeamRepository(GroPanDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public List<TeamViewModel> GetAll()
        {
            var currentAssignmentSession = GetCurrentSession();
            var teams = _dbContext.Teams.Include(x => x.TeamMembers).Include(x => x.TeamSupervisors)
                .Where(x => x.AssignmentSessionId == currentAssignmentSession.AssignmentSessionId)
                .ToList();

            var result = _mapper.Map<List<TeamViewModel>>(teams);
            return result;
        }
    }
}
