using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Pages.UserManagement;
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

       
        public async Task<KeyValuePair<bool, string>> AddAsync(NewUserModel.UserAddModel newUserViewModel)
        {
            bool isSuccess = false;
            string msg = string.Empty;

            var transaction = _dbContext.Database.BeginTransaction();
            var newAppUser = _mapper.Map<AppUser>(newUserViewModel);
            newAppUser.Created = DateTime.Now;
            newAppUser.CreatedBy = "admin";

            var validationResponse = ValidateEntry(newAppUser);

            if (!validationResponse.Key)
                return validationResponse;

            transaction.CreateSavepoint("BeforeNewUser");

            //  add user
            await _dbContext.AppUsers.AddAsync(newAppUser);

            //  add role

            var newUserRole = _mapper.Map<UserRole>(newUserViewModel);
            await _dbContext.UserRoles.AddAsync(newUserRole);

            try
            {
                foreach (var item in newUserViewModel.ExtraProperties)
                {
                    var newUserProperty = _mapper.Map<AppUserClaim>(item);
                    await _dbContext.AppUserClaims.AddAsync(newUserProperty);
                }
                await SaveDatabase();
                transaction.Commit();

                msg = "User created successfully!";
                isSuccess = true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                transaction.RollbackToSavepoint("BeforeNewUser");
            }
           

            return new KeyValuePair<bool, string>(isSuccess, msg);
        }

       

        public List<UserViewModel> GetRoleUsers(string role)
        {
            var result = _dbContext.AppUsers
                .Where(x => x.UserRoles.Any(y => y.Role.RoleName.ToLower() == role.ToLower()))
                .Select
                (
                    x => new UserViewModel
                    {
                        UserId = x.UserId,
                        FirstName = x.Firstname,
                        Othernames = x.Othernames,
                    }
                ).ToList();
            //return result;

            List<UserViewModel> list = new List<UserViewModel>();

            for (int i = 0; i < 100; i++)
            {
                list.Add(new UserViewModel { FirstName = "Test", Othernames = i.ToString(), Surname ="Justeeing"});
            }
            return list;
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

            if (existingRecord == null)
                return new KeyValuePair<bool, string>(false, $"User with similar details exist!");

            return new KeyValuePair<bool, string>(true, $"Validation successful!");
        }
        #endregion
    }
}
