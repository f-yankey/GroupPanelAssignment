using AutoMapper;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Services
{
    public class TeamManagementService : ITeamManagementService
    {
        private IWebHostEnvironment _env;
        private IGropanObjectFactory _gropanObjectFactory;
        private ITransactionOperator _transactionOperator;
        private ITeamRepository _teamRepository;
        private IAppUserRepository _appUserRepository;
        private ICwaGroupingRepository _cwaGroupingRepository;
        private IMapper _mapper;

        public TeamManagementService
            (
            IGropanObjectFactory gropanObjectFactory,
            ITransactionOperator transactionOperator,
            ITeamRepository teamRepository,
            IAppUserRepository appUserRepository,
            ICwaGroupingRepository cwaGroupingRepository,
            IMapper mapper,
            IWebHostEnvironment env
            )
        {
            _gropanObjectFactory = gropanObjectFactory;
            _transactionOperator = transactionOperator;
            _teamRepository = teamRepository;
            _appUserRepository = appUserRepository;
            _cwaGroupingRepository = cwaGroupingRepository;
            _mapper = mapper;
            _env = env;
        }

        public List<TeamViewModel> AutoGroup(TeamAutoCreationViewModel model)
        {
            List<TeamViewModel> createdTeams = _gropanObjectFactory.CreateTeamViewModelList();

            //  get available users and supervisors
            var availableSupervisors = _appUserRepository
                .GetRoleUsers<SupervisorsForAssignmentViewModel>(ApplicationConstants.SupervisorRole)
                .ToList();

            //  filter out exempted
            availableSupervisors = model.ExemptedSupervisors == null ? 
                availableSupervisors : availableSupervisors.Where(x => !model.ExemptedSupervisors.Contains(x.UserId)).ToList();

            var availableStudents = _appUserRepository
                .GetRoleUsers<StudentForAssignmentViewModel>(ApplicationConstants.StudentRole)
                .ToList();

            //  filter out exempted
            availableStudents = model.ExemptedStudents == null ? 
                availableStudents : availableStudents.Where(x => !model.ExemptedStudents.Contains(x.UserId)).ToList();

            if (availableSupervisors.Count > 0  && availableStudents.Count > 0)
            {
                //  group students by CWA
                var cwaGroups = _cwaGroupingRepository.GetAll();
                List<CWAGroupViewModel> groupings = _gropanObjectFactory.CreateCWAGroupViewModelList();

                foreach (var group in cwaGroups)
                {
                    //  get all students whose CWA falls within the range
                    var groupRangeStudents = availableStudents
                        .Where(x => x.CWA >= group.Min && (x.CWA <= group.Max))
                        .ToList();

                    if (groupRangeStudents.Count > 0)
                    {
                        var newGrouping = _gropanObjectFactory.CreateCWAGroupViewModel();
                        newGrouping.Min = group.Min;
                        newGrouping.Max = group.Max;
                        newGrouping.Students = groupRangeStudents;
                        groupings.Add(newGrouping);
                    }
                }

                //  grouping ordering 
                groupings = groupings.OrderByDescending(x => x.Max).ToList();

                int count = 1;
                foreach (var supervisorToAssign in availableSupervisors)
                {
                    //  create new team
                    var newTeam = _gropanObjectFactory.CreateTeamViewModel();
                    newTeam.TeamName = $"Group {count}";

                    //  assign supervisor
                    var teamSupervisor = _mapper.Map<TeamSupervisorViewModel>(supervisorToAssign);
                    newTeam.Supervisors.Add(teamSupervisor);
                    createdTeams.Add(newTeam);
                    supervisorToAssign.IsAssigned = true;

                    count++;
                }

                //  main grouping algorithm
                int teamIterator = 0;
                foreach (var group in groupings)
                {
                    foreach (var studentToAssign in group.Students)
                    {
                        var teamMember = _mapper.Map<TeamMemberViewModel>(studentToAssign);
                        createdTeams[teamIterator].Members.Add(teamMember);
                        studentToAssign.IsAssigned = true;
                        teamIterator = teamIterator == (createdTeams.Count - 1) ? 0 : teamIterator + 1;
                    }
                }
            }

            return createdTeams;
        }

        public List<TeamViewModel> GetTeams(string filter=null)
        {
            List<TeamViewModel> teams = _teamRepository.GetAll();
            
            if (string.IsNullOrWhiteSpace(filter))
                return teams;

            teams = GetFilteredTeams(teams,filter);

            return teams;
        }

        private List<TeamViewModel> GetFilteredTeams(List<TeamViewModel> teams, string filter)
        {
            teams = teams
                .Where(
                x => ( x.TeamName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.TeamName.ToLower().Trim() == filter.ToLower().Trim() )
                || (x.Members.Any(y => y.FullName.ToLower().Trim().Contains(filter.ToLower().Trim()) || y.FullName.ToLower().Trim() == filter.ToLower().Trim()))
                || (x.Members.Any(y => y.ProgrammeName.ToLower().Trim().Contains(filter.ToLower().Trim()) || y.ProgrammeName.ToLower().Trim() == filter.ToLower().Trim()))
                || (x.Members.Any(y => y.CWA != null && (y.CWA.Contains(filter.ToLower().Trim()) || y.CWA == filter.ToLower().Trim())))
                || (x.Supervisors.Any(y => y.FullName.ToLower().Trim().Contains(filter.ToLower().Trim()) || y.FullName.ToLower().Trim() == filter.ToLower().Trim()))
                || (x.TeamTopic != null && (x.TeamTopic.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.TeamTopic.ToLower().Trim() == filter.ToLower().Trim()))
            )
                .ToList();
            return teams;
        }

        public async Task<KeyValuePair<bool, string>> SaveGroupingAsync(List<TeamViewModel> teams)
        {
            string transactionName = ApplicationConstants.BeforeNewGroupSaveTransaction;
            var transaction = _transactionOperator.BeginTransaction(transactionName);

            try
            {
                await _teamRepository.DeleteAllCurrentSessionGroupsAsync();

                foreach (var item in teams)
                {
                   await _teamRepository.AddTeamAsync(item);
                }
                await _transactionOperator.CommitTransactionAsync(transaction);
                return new KeyValuePair<bool, string>(true, "Groups created successfully!");
            }
            catch (Exception ex)
            {
               await _transactionOperator.RollbackTransactionAsync(transaction, transactionName);
               return new KeyValuePair<bool, string>(false, $"An error occured! {ex.Message}");
            }
            
        }

        public byte[] GetGroupingCSV()
        {
            var allCurrentGrouping = GetTeams();

            string fileName = $"{Guid.NewGuid()}.csv";
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), $"{_env.WebRootPath}/{ApplicationConstants.UploadDirectory}", fileName);

            string lines = string.Empty;

            foreach (var item in allCurrentGrouping)
            {
                lines = $"{lines} {item.TeamName} - Supervisor(s): ";
              
                foreach (var supervisor in item.Supervisors)
                {
                    lines = $"{lines}  {item.Supervisors.FirstOrDefault().FullName}/";
                }
                lines = $"{lines}\n";

                foreach (var member in item.Members.OrderByDescending(x => x.CWA))
                {
                    lines = $"{lines} {member.FullName}, {member.CWA} \n";
                }
                lines = $"{lines}\n \n";
            }

            File.WriteAllText(savePath, lines);

            var bytes = File.ReadAllBytes(savePath);

            File.Delete(savePath);

            return bytes;
        }
    }
}
