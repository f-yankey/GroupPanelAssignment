using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Pages.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GroupPanelAssignment.Pages.UserManagement.NewUserModel;

namespace GroupPanelAssignment.Services.Interfaces
{
    public interface IUserManagementService
    {
        List<UserViewModel> GetAppUsers(string role);
        List<Claim> GetAllClaims();
        Task<KeyValuePair<bool, string>> AddNewUser(UserAddModel newUserViewModel);
    }
}
