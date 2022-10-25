using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Utils
{

    public interface IGropanObjectFactory
    {
        AppUser CreateNewAppUser();
        UserAddViewModel CreateNewUserAddViewModel();
        List<UserAddViewModel> CreateNewUserAddViewModelList();
        UploadViewModel CreateUploadViewModel();
        List<UploadViewModel> CreateUploadViewModelList();
        List<ExtraProperty> CreateExtraPropertyList();
        List<CWAGroupViewModel> CreateCWAGroupViewModelList();
        CWAGroupViewModel CreateCWAGroupViewModel();
        TeamViewModel CreateTeamViewModel();
        List<TeamViewModel> CreateTeamViewModelList();
    }

    public class GropanObjectFactory : IGropanObjectFactory
    {
        public CWAGroupViewModel CreateCWAGroupViewModel()
        {
            return new CWAGroupViewModel();
        }

        public List<CWAGroupViewModel> CreateCWAGroupViewModelList()
        {
            return new List<CWAGroupViewModel>();
        }

        public List<ExtraProperty> CreateExtraPropertyList()
        {
            return new List<ExtraProperty>();
        }

        public AppUser CreateNewAppUser()
        {
            return new AppUser();
        }

        public UserAddViewModel CreateNewUserAddViewModel()
        {
            return new UserAddViewModel();
        }

        public List<UserAddViewModel> CreateNewUserAddViewModelList()
        {
            return new List<UserAddViewModel>();
        }

        public TeamViewModel CreateTeamViewModel()
        {
            return new TeamViewModel() { Supervisors = new List<TeamSupervisorViewModel>(), Members = new List<TeamMemberViewModel>()};
        }

        public List<TeamViewModel> CreateTeamViewModelList()
        {
            return new List<TeamViewModel>();
        }

        public UploadViewModel CreateUploadViewModel()
        {
            return new UploadViewModel();
        }

        public List<UploadViewModel> CreateUploadViewModelList()
        {
            return new List<UploadViewModel>();
        }
    }
}
