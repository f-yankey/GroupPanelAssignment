using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
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

        public List<AppUser> GetAppUsers(string role)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRoles()
        {
            throw new NotImplementedException();
        }
    }
}
