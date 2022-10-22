using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Pages.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Services.Interfaces
{
    public interface IUserManagementService
    {
        List<UserViewModel> GetAppUsers(string role, string searchText = null);
        SelectList GetUsersSelectList(string role);
        List<Claim> GetAllClaims();
        Task<KeyValuePair<bool, string>> AddNewUser(UserAddViewModel newUserViewModel);
        Task<List<UploadViewModel>> BulkUploadAsync(IFormFile file);
    }
}
