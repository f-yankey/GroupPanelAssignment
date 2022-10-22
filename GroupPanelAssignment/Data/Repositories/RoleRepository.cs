using AutoMapper;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(GroPanDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public Role GetByName(string roleName)
        {
            return _dbContext.Roles
                .Where(x => x.RoleName.ToLower().Trim() == roleName.ToLower().Trim())
                .FirstOrDefault();
        }
    }
}
