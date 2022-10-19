using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Pages.UserManagement;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
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
        private IClaimRepository _claimRepository;
        private IGropanObjectFactory _gropanObjectFactory;
        private IAppUserClaimRepository _appUserClaimRepository;
        private IMapper _mapper;

        public UserManagementService
            (
            IRoleRepository roleRepository,
            IAppUserRepository appUserRepository,
            IClaimRepository claimRepository,
            IGropanObjectFactory gropanObjectFactory,
            IAppUserClaimRepository appUserClaimRepository
            )
        {
            _roleRepository = roleRepository;
            _appUserRepository = appUserRepository;
            _claimRepository = claimRepository;
            _gropanObjectFactory = gropanObjectFactory;
            _appUserClaimRepository = appUserClaimRepository;
           
        }

        public async Task<KeyValuePair<bool, string>> AddNewUser(NewUserModel.UserAddModel newUserViewModel)
        {
            var userSaveResult = await _appUserRepository.AddAsync(newUserViewModel);
            return userSaveResult;
        }

        public List<Claim> GetAllClaims()
        {
            var claims = _claimRepository.GetAllClaims();
            return claims;
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
