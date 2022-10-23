using AutoMapper;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Services
{
    public class TeamManagementService : ITeamManagementService
    {
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
            IMapper mapper
            )
        {
            _gropanObjectFactory = gropanObjectFactory;
            _transactionOperator = transactionOperator;
            _teamRepository = teamRepository;
            _appUserRepository = appUserRepository;
            _cwaGroupingRepository = cwaGroupingRepository;
            _mapper = mapper;
        }

        public List<TeamViewModel> AutoGroup(TeamAutoCreationViewModel model)
        {
            List<TeamViewModel> results = _gropanObjectFactory.CreateTeamViewModelList();

            //  get available users and supervisors
            var availableSupervisors = _appUserRepository
                .GetRoleUsers<SupervisorsForAssignmentViewModel>(ApplicationConstants.SupervisorRole)
                .ToList();

            //  filter out exempted
            availableSupervisors = model.ExemptedSupervisors == null ? availableSupervisors : availableSupervisors.Where(x => !model.ExemptedSupervisors.Contains(x.UserId)).ToList();

            var availableStudents = _appUserRepository
                .GetRoleUsers<StudentForAssignmentViewModel>(ApplicationConstants.StudentRole)
                .ToList();

            //  filter out exempted
            availableStudents = model.ExemptedStudents == null ? availableStudents : availableStudents.Where(x => !model.ExemptedStudents.Contains(x.UserId)).ToList();

            //  statistics
            int numberOfSupervisors = availableSupervisors.Count;
            int numberOfStudents = availableStudents.Count;
            int numberOfGroupsToCreate = numberOfStudents / numberOfSupervisors;
            int numberOfStudentsPerGroup = numberOfStudents / numberOfGroupsToCreate;

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

            //  main grouping algorithm
            for (int i = 0; i < numberOfGroupsToCreate; i++)
            {
                //  create new team
                var newTeam = _gropanObjectFactory.CreateTeamViewModel();

                //  assign supervisor
                var supervisorToAssign = availableSupervisors.Where(x => !x.IsAssigned).FirstOrDefault();
                var teamSupervisor = _mapper.Map<TeamSupervisorViewModel>(supervisorToAssign);
                newTeam.Supervisors.Add(teamSupervisor);

                int groupingIterator = 0;

                for (int j = 0; j < numberOfStudentsPerGroup; j++)
                {
                    StudentForAssignmentViewModel studentToAssign = null;

                    
                    //   assign students
                    while (studentToAssign == null && (groupingIterator < groupings.Count - 1))
                    {
                        studentToAssign = groupings[groupingIterator].Students.Where(x => !x.IsAssigned).FirstOrDefault();
                        groupingIterator++;
                    }

                    
                    if (studentToAssign != null)
                    {
                        var teamMember = _mapper.Map<TeamMemberViewModel>(studentToAssign);
                        newTeam.Members.Add(teamMember);
                        studentToAssign.IsAssigned = true;
                    }

                    j++;
                    groupingIterator = j;
                }

                //  add newyly created team to list of teams
                results.Add(newTeam);

                //  flagging used supervisor as assigned
                supervisorToAssign.IsAssigned = true;
                i++;
            }

            return results;
        }

        public List<TeamViewModel> GetTeams()
        {
            List<TeamViewModel> teams = _teamRepository.GetAll();
            return teams;
        }
    }
}
