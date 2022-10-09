using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories
{
    public class ClaimRepository : BaseRepository, IClaimRepository
    {
        public ClaimRepository(GroPanDbContext dbContext) : base(dbContext)
        {
        }

        public List<Claim> GetAllClaims()
        {
            var result = _dbContext.Claims.ToList();
            return result;
        }
    }
}
