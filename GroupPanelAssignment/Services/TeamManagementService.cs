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

        public TeamManagementService
            (
            IGropanObjectFactory gropanObjectFactory,
            ITransactionOperator transactionOperator,
            ITeamRepository teamRepository,
            IAppUserRepository appUserRepository,
            ICwaGroupingRepository cwaGroupingRepository
            )
        {
            _gropanObjectFactory = gropanObjectFactory;
            _transactionOperator = transactionOperator;
            _teamRepository = teamRepository;
            _appUserRepository = appUserRepository;
            _cwaGroupingRepository = cwaGroupingRepository;
        }

        public List<TeamViewModel> AutoGroup(TeamAutoCreationViewModel model)
        {
            //  get exempted users
            List<UserViewModel> exemptedSupervisors = _appUserRepository.GetUsersByIds(model.ExemptedSupervisors);
            List<UserViewModel> exemptedStudents = _appUserRepository.GetUsersByIds(model.ExemptedStudents);

            //  get available users
            var availableSupervisors = _appUserRepository
                .GetRoleUsers(ApplicationConstants.SupervisorRole)
                .Except(exemptedSupervisors)
                .ToList();

            var availableStudents = _appUserRepository
                .GetRoleUsers(ApplicationConstants.StudentRole)
                .Except(exemptedStudents)
                .ToList();

            //  statistics
            int numberOfSupervisors = availableSupervisors.Count;
            int numberOfStudents = availableStudents.Count;
            int numberOfGroupsToCreate = numberOfStudents / numberOfSupervisors;

            //  group students by CWA
            var groups = _cwaGroupingRepository.GetAll();
            List<CWAGroupViewModel> groupings = _gropanObjectFactory.CreateCWAGroupViewModelList();
            foreach (var group in groups)
            {
                var newGroup = _gropanObjectFactory.CreateCWAGroupViewModel();
                newGroup.Min = group.Min;
                newGroup.Min = group.Max;

                //get all students whose CWA falls within the range

            }

            for (int i = 1; i < numberOfGroupsToCreate; i++)
            {

            }

            throw new NotImplementedException();
        }

        public List<TeamViewModel> GetTeams()
        {
            List<TeamViewModel> teams = _teamRepository.GetAll();
            return teams;
        }
    }
}
