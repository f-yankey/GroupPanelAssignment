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

        public async Task AddTeamAsync(TeamViewModel team)
        {
            var currentAssignmentSession = GetCurrentSession();
            DateTime createdAt = DateTime.Now;
            string createdBy = "admin";

            var newTeam = _mapper.Map<Team>(team);
            newTeam.AssignmentSessionId = currentAssignmentSession.AssignmentSessionId;
            newTeam.Created = createdAt;
            newTeam.CreatedBy = createdBy;
            _dbContext.Teams.Add(newTeam);
            await SaveDatabaseAsync();

            var supervisors = _mapper.Map<List<TeamSupervisor>>(team.Supervisors);
            foreach (var item in supervisors)
            {
                item.TeamId = newTeam.TeamId;
                item.Created = createdAt;
                item.CreatedBy = createdBy;

                _dbContext.TeamSupervisors.Add(item);
            }

            var members = _mapper.Map<List<TeamMember>>(team.Members);
            foreach (var item in members)
            {
                item.TeamId = newTeam.TeamId;
                item.Created = createdAt;
                item.CreatedBy = createdBy;

                _dbContext.TeamMembers.Add(item);
            }

            await SaveDatabaseAsync();

        }


        public async Task DeleteAllCurrentSessionGroupsAsync()
        {
            var currentAssignmentSession = GetCurrentSession();
            var allSessionGroups = _dbContext.Teams
                .Include(x => x.TeamMembers)
                .Include(x => x.TeamSupervisors)
                .Include(x => x.PanelTeams)
                .Where(x => x.AssignmentSessionId == currentAssignmentSession.AssignmentSessionId).ToList();

            if (allSessionGroups.Count > 0)
            {
                foreach (var group in allSessionGroups)
                {
                    _dbContext.TeamMembers.RemoveRange(group.TeamMembers);
                    _dbContext.TeamSupervisors.RemoveRange(group.TeamSupervisors);
                    _dbContext.PanelTeams.RemoveRange(group.PanelTeams);
                }

                _dbContext.Teams.RemoveRange(allSessionGroups);

                await SaveDatabaseAsync();
            }
        }

        public List<TeamViewModel> GetAll()
        {
            var currentAssignmentSession = GetCurrentSession();
            var teams = _dbContext.Teams
                .Include(x => x.TeamMembers).ThenInclude(x =>x.User).ThenInclude(x => x.AppUserClaims).ThenInclude(x => x.Claim)
                .Include(x => x.TeamSupervisors).ThenInclude(x => x.User)
                .Where(x => x.AssignmentSessionId == currentAssignmentSession.AssignmentSessionId)
                .ToList();

            var result = _mapper.Map<List<TeamViewModel>>(teams);
            return result;
        }
    }
}
