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

            //  get exempted users
            List<SupervisorsForAssignmentViewModel> exemptedSupervisors = _appUserRepository.GetUsersByIds<SupervisorsForAssignmentViewModel>(model.ExemptedSupervisors);
            List<StudentViewModel> exemptedStudents = _appUserRepository.GetUsersByIds<StudentViewModel>(model.ExemptedStudents);

            //  get available users
            var availableSupervisors = _appUserRepository
                .GetRoleUsers<SupervisorsForAssignmentViewModel>(ApplicationConstants.SupervisorRole)
                .Except(exemptedSupervisors)
                .ToList();

            var availableStudents = _appUserRepository
                .GetRoleUsers<StudentViewModel>(ApplicationConstants.StudentRole)
                .Except(exemptedStudents)
                .ToList();

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
                var newGrouping = _gropanObjectFactory.CreateCWAGroupViewModel();
                newGrouping.Min = group.Min;
                newGrouping.Min = group.Max;

                //  get all students whose CWA falls within the range
                newGrouping.Students = availableStudents
                    .Where(x => x.CWA >= group.Min && (x.CWA <= group.Max))
                    .ToList();

                groupings.Add(newGrouping);
            }

            //  main grouping algorithm
            for (int i = 1; i < numberOfGroupsToCreate; i++)
            {
                //  create new team
                var newTeam = _gropanObjectFactory.CreateTeamViewModel();

                //  assign supervisor
                var supervisorToAssign = availableSupervisors.Where(x => !x.IsAssigned).FirstOrDefault();
                var teamSupervisor = _mapper.Map<TeamSupervisorViewModel>(supervisorToAssign);
                newTeam.Supervisors.Add(teamSupervisor);

                for (int j = 0; j < numberOfStudentsPerGroup; j++)
                {
                   //   assign students

                }

                //  add newyly created team to list of teams
                results.Add(newTeam);

                //  flagging used supervisor as assigned
                supervisorToAssign.IsAssigned = true;
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
