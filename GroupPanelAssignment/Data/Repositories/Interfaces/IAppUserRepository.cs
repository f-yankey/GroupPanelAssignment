using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Pages.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories.Interfaces
{
    public interface IAppUserRepository
    {
        Task<KeyValuePair<bool,string>> AddAsync(UserAddViewModel newUserViewModel);
        List<UserViewModel> GetRoleUsers(string role);
    }
}
