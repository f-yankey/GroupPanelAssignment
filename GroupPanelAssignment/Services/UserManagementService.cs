using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Pages.UserManagement;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Services
{
    public class UserManagementService : IUserManagementService
    {
        private IHostingEnvironment _env;
        private IRoleRepository _roleRepository;
        private IAppUserRepository _appUserRepository;
        private IClaimRepository _claimRepository;
        private IGropanObjectFactory _gropanObjectFactory;
        private ITransactionOperator _transactionOperator;

        public UserManagementService
            (
            IHostingEnvironment env,
            IRoleRepository roleRepository,
            IAppUserRepository appUserRepository,
            IClaimRepository claimRepository,
            IGropanObjectFactory gropanObjectFactory,
            ITransactionOperator transactionOperator
            )
        {
            _env = env;
            _roleRepository = roleRepository;
            _appUserRepository = appUserRepository;
            _claimRepository = claimRepository;
            _gropanObjectFactory = gropanObjectFactory;
            _transactionOperator = transactionOperator;
        }

        #region Queries
        public List<Claim> GetAllClaims()
        {
            var claims = _claimRepository.GetAllClaims();
            return claims;
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
        
        private List<UserViewModel> FilterUsersBySearchText(List<UserViewModel> users, string searchText)
        {
            users = users
                .Where(
                x => (x.Surname.Contains(searchText) || x.Surname.ToLower().Trim() == searchText.ToLower().Trim())
                || (x.FirstName.Contains(searchText) || x.FirstName.ToLower().Trim() == searchText.ToLower().Trim())
                || (x.Email.Contains(searchText) || x.Email.ToLower().Trim() == searchText.ToLower().Trim())
                || (x.Username.Contains(searchText) || x.Username.ToLower().Trim() == searchText.ToLower().Trim())
                ).ToList();

            return users;
        }
        #endregion

        #region Commands
        public async Task<KeyValuePair<bool, string>> AddNewUser(UserAddViewModel newUserViewModel)
        {
            string transactionName = ApplicationConstants.BeforeNewUserTransaction;
            var transaction = _transactionOperator.BeginTransaction(transactionName);

            try
            {
                var userSaveResult = await _appUserRepository.AddAsync(newUserViewModel);
                await _transactionOperator.CommitTransactionAsync(transaction);
                return userSaveResult;
            }
            catch (Exception ex)
            {
                await _transactionOperator.RollbackTransactionAsync(transaction, transactionName);
                return new KeyValuePair<bool, string>(false, $"{ex.Message}");
            }
        }

        public async Task<List<UploadViewModel>> BulkUploadAsync(IFormFile file)
        {
            //  todo: validate file
            string savePath = SaveFileInTempStorage(file);
            List<UploadViewModel> uploadRecords = ConvertCSVToUploadModel(savePath);
            DeleteFile(savePath);
            await SaveConvertedRecords(uploadRecords);
            return uploadRecords;
        }
        #endregion


        #region File Upload Private Methods
        private string SaveFileInTempStorage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), $"{_env.WebRootPath}/{ApplicationConstants.UploadDirectory}", fileName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return savePath;
        }

        private static void DeleteFile(string savePath)
        {
            File.Delete(savePath);
        }

        private List<UploadViewModel> ConvertCSVToUploadModel(string savePath)
        {
            //  todo: refactor
            List<UploadViewModel> uploadRecords = _gropanObjectFactory.CreateUploadViewModelList();
            var lines = File.ReadLines(savePath, Encoding.UTF8);
            var programmeProperty = _claimRepository.GetClaimByName(ApplicationConstants.ProgrammeClaim);
            var cwaProperty = _claimRepository.GetClaimByName(ApplicationConstants.CWAClaim);

            foreach (var line in lines)
            {
                var data = line.Split(',');
                UploadViewModel newUploadViewModel = _gropanObjectFactory.CreateUploadViewModel();
                newUploadViewModel.User = _gropanObjectFactory.CreateNewUserAddViewModel();
                newUploadViewModel.User.ExtraProperties = _gropanObjectFactory.CreateExtraPropertyList();

                string currentLineUserName = data[0];

                try
                {
                    int columns = data.Count();
                    newUploadViewModel.User.Username = currentLineUserName;
                    newUploadViewModel.User.SpecialID = data[1];
                    newUploadViewModel.User.FirstName = data[2];
                    newUploadViewModel.User.Othernames = data[3];
                    newUploadViewModel.User.Surname = data[4];
                    newUploadViewModel.User.Email = data[5];
                    newUploadViewModel.User.Roles = data[6].ToLower().Trim() == ApplicationConstants.RoleWord.ToLower().Trim() ?
                        new List<string> { data[6] } :
                        new List<string> { _roleRepository.GetByName(data[6]).RoleId };

                    if (columns > 7)
                    {
                        if (!string.IsNullOrWhiteSpace(data[7]))
                        {
                            newUploadViewModel.User.ExtraProperties.Add(new ExtraProperty { Id = programmeProperty.ClaimId, Value = data[7] });
                        }
                    }

                    if (columns > 8)
                    {
                        if (!string.IsNullOrWhiteSpace(data[8]))
                        {
                            newUploadViewModel.User.ExtraProperties.Add(new ExtraProperty { Id = cwaProperty.ClaimId, Value = data[8] });
                        }
                    }

                    newUploadViewModel.ConversionSucceeded = true;
                }
                catch (Exception ex)
                {
                    newUploadViewModel.ConversionSucceeded = false;
                    newUploadViewModel.Message = $"{currentLineUserName} - {ex.Message}";
                    newUploadViewModel.User = null;
                }

                uploadRecords.Add(newUploadViewModel);
            }

            uploadRecords.RemoveAt(0);
            return uploadRecords;
        }

        private async Task SaveConvertedRecords(List<UploadViewModel> uploadRecords)
        {
            var convertedRecords = uploadRecords.Where(x => x.ConversionSucceeded).ToList();

            foreach (var item in convertedRecords)
            {
                var result = await AddNewUser(item.User);
                item.DbSaveSucceeded = result.Key;
                item.Message = $"{item.User.Username} - {result.Value}";
            }
        }
        #endregion
    }
}
