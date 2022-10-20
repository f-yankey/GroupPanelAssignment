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
        
        public UserManagementService
            (
            IRoleRepository roleRepository,
            IAppUserRepository appUserRepository,
            IClaimRepository claimRepository
            )
        {
            _roleRepository = roleRepository;
            _appUserRepository = appUserRepository;
            _claimRepository = claimRepository;
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

        private List<UserViewModel> FilterUsersBySearchText(List<UserViewModel> users, string searchText)
        {
            users = users
                .Where(
                x => (x.Surname.Contains(searchText) || x.Surname.ToLower().Trim() == searchText.ToLower().Trim())
                //|| (x.Othernames.Contains(searchText) || x?.Othernames.ToLower().Trim() == searchText.ToLower().Trim())
                || (x.FirstName.Contains(searchText) || x.FirstName.ToLower().Trim() == searchText.ToLower().Trim())
                || (x.Email.Contains(searchText) || x.Email.ToLower().Trim() == searchText.ToLower().Trim())
                || (x.Username.Contains(searchText) || x.Username.ToLower().Trim() == searchText.ToLower().Trim())
                ).ToList();

             return users;
        }

        public List<UserViewModel> GetAppUsers(string role, string searchText = null)
        {
            List<UserViewModel> users = _appUserRepository.GetRoleUsers(role);
            
            //  filter users results by search text if search text has a value
            users = !string.IsNullOrWhiteSpace(searchText) ? FilterUsersBySearchText(users, searchText) : users;
            return users;
        }

        private List<Role> GetRoles()
        {
            throw new NotImplementedException();
        }
    }
}
