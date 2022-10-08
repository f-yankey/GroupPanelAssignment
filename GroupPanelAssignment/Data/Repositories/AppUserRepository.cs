using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using GroupPanelAssignment.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories
{
    public class AppUserRepository : BaseRepository, IAppUserRepository
    {
        public AppUserRepository(GroPanDbContext dbContext) : base(dbContext)
        {
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
            return result;
        }
    }
}
