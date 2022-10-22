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

        public TeamManagementService
            (
            IGropanObjectFactory gropanObjectFactory,
            ITransactionOperator transactionOperator,
            ITeamRepository teamRepository
            )
        {
            _gropanObjectFactory = gropanObjectFactory;
            _transactionOperator = transactionOperator;
            _teamRepository = teamRepository;
        }

        public List<TeamViewModel> GetTeams()
        {
            throw new NotImplementedException();
        }
    }
}
