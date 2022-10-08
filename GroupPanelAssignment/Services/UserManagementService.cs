using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Services
{
    public class UserManagementService : IUserManagementService
    {
        private IRoleRepository _roleRepository;
        private IAppUserRepository _appUserRepository;

        public UserManagementService
            (
            IRoleRepository roleRepository,
            IAppUserRepository appUserRepository)
        {
            _roleRepository = roleRepository;
            _appUserRepository = appUserRepository;
        }

        public List<UserViewModel> GetAppUsers(string role)
        {
            List<UserViewModel> users = _appUserRepository.GetRoleUsers(role);
            return users;
        }

        private List<Role> GetRoles()
        {
            throw new NotImplementedException();
        }
    }
}
