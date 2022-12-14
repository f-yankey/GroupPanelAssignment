using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Pages.UserManagement;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories
{
    public class AppUserRepository : BaseRepository, IAppUserRepository
    {
        private IMapper _mapper;

        public AppUserRepository(GroPanDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

       
        public async Task<KeyValuePair<bool, string>> AddAsync(UserAddViewModel newUserViewModel)
        {
            bool isSuccess = false;
            string msg = string.Empty;

            DateTime createdAt = DateTime.Now;
            string createdBy = "admin";

            var currentSession = _dbContext.AssignmentSessions.FirstOrDefault(x => x.IsCurrent == true);

            var newAppUser = _mapper.Map<AppUser>(newUserViewModel);
            newAppUser.Created = createdAt;
            newAppUser.CreatedBy = createdBy;

            var validationResponse = ValidateEntry(newAppUser);
            if (!validationResponse.Key)
                return validationResponse;

            //  add user
            await _dbContext.AppUsers.AddAsync(newAppUser);
            await SaveDatabase();

            //  register user in current session
            await _dbContext.AppUserAssignmentSessions.AddAsync(new AppUserAssignmentSession { UserId = newAppUser.UserId, AssignmentSessionId = currentSession.AssignmentSessionId, Created = createdAt, CreatedBy = createdBy });

            //  add role
            foreach (var item in newUserViewModel.Roles)
            {
                await _dbContext.UserRoles.AddAsync(new UserRole { RoleId = item, UserId = newAppUser.UserId, Created = createdAt, CreatedBy = createdBy });
            }
            
            if (newUserViewModel.ExtraProperties != null)
            {
                foreach (var item in newUserViewModel.ExtraProperties)
                {
                    var newUserProperty = _mapper.Map<AppUserClaim>(item);
                    newUserProperty.UserId = newAppUser.UserId;
                    newUserProperty.Created = createdAt;
                    newUserProperty.CreatedBy = createdBy;
                    newUserProperty.AssignmentSessionId = currentSession.AssignmentSessionId;
                    await _dbContext.AppUserClaims.AddAsync(newUserProperty);
                }
            }

            await SaveDatabase();

            msg = "User created successfully!";
            isSuccess = true;

            return new KeyValuePair<bool, string>(isSuccess, msg);
        }

        private List<AppUser> AllUsers()
        {
            return _dbContext.AppUsers.ToList();
        }
        private List<AppUser> RoleUsers(string role)
        {
            return _dbContext.AppUsers
                .Where(x => x.UserRoles.Any(y => y.Role.RoleName.ToLower() == role.ToLower()))
                .ToList();
        }



        public List<UserViewModel> GetRoleUsers(string role)
        {
            var allUsers = role.ToLower() == "all" ? AllUsers() : RoleUsers(role);
            List<UserViewModel> users = _mapper.Map<List<UserViewModel>>(allUsers);
            return users;
        }

        #region Private Methods
        private KeyValuePair<bool, string> ValidateEntry(AppUser newAppUser)
        {
            var existingRecord = _dbContext.AppUsers
                .Where(
                    x => x.Username == newAppUser.Username || x.Email == newAppUser.Email ||
                    (
                        x.Firstname.ToLower().Trim() == newAppUser.Firstname.ToLower().Trim()
                        && x.Surname.ToLower().Trim() == newAppUser.Surname.ToLower().Trim()
                        //&& x.Othernames.ToLower().Trim() == newAppUser.Othernames.ToLower().Trim()
                    )
                )
                .FirstOrDefault();

            if (existingRecord != null)
                return new KeyValuePair<bool, string>(false, $"User with similar details exist!");

            return new KeyValuePair<bool, string>(true, $"Validation successful!");
        }

       
        #endregion
    }
}
