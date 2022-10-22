using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Pages.UserManagement;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
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
        private IRoleRepository _roleRepository;
        private IAppUserRepository _appUserRepository;
        private IClaimRepository _claimRepository;
        private IGropanObjectFactory _gropanObjectFactory;

        public UserManagementService
            (
            IRoleRepository roleRepository,
            IAppUserRepository appUserRepository,
            IClaimRepository claimRepository,
            IGropanObjectFactory gropanObjectFactory
            )
        {
            _roleRepository = roleRepository;
            _appUserRepository = appUserRepository;
            _claimRepository = claimRepository;
            _gropanObjectFactory = gropanObjectFactory;
        }

        public async Task<KeyValuePair<bool, string>> AddNewUser(UserAddViewModel newUserViewModel)
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

        public async Task<List<UploadViewModel>> BulkUploadAsync(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string csvData = string.Empty;

            //Get url To Save
            string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploaded-files", fileName);

            using (var stream = new FileStream(SavePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var lines = File.ReadLines(SavePath, Encoding.UTF8);

            List<UploadViewModel> uploadRecords = _gropanObjectFactory.CreateUploadViewModelList();

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
                    
                    newUploadViewModel.User.Username = currentLineUserName;
                    newUploadViewModel.User.SpecialID = data[1];
                    newUploadViewModel.User.FirstName = data[2];
                    newUploadViewModel.User.Othernames = data[3];
                    newUploadViewModel.User.Surname = data[4];
                    newUploadViewModel.User.Email = data[5];
                    newUploadViewModel.User.Roles = new List<string> { _roleRepository.GetByName(data[6]).RoleId };

                    if (data.Count() > 6)
                    {
                        if (!string.IsNullOrWhiteSpace(data[7]))
                        {
                            newUploadViewModel.User.ExtraProperties.Add(new ExtraProperty { Id = programmeProperty.ClaimId, Value = data[7] });
                        }
                    }
                    
                    if (data.Count() > 7)
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
                    newUploadViewModel.Message =  $"{currentLineUserName} - {ex.Message}";
                    newUploadViewModel.User = null;
                }

                uploadRecords.Add(newUploadViewModel);
            }

            File.Delete(SavePath);

            uploadRecords.RemoveAt(0);

            var convertedRecords = uploadRecords.Where(x => x.ConversionSucceeded).ToList();

            foreach (var item in convertedRecords)
            {
                var result =  await AddNewUser(item.User);
                item.DbSaveSucceeded = result.Key;
                item.Message = result.Value;
            }


            return uploadRecords;
        }
    }
}
