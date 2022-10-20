using GroupPanelAssignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories.Interfaces
{
    public interface IClaimRepository
    {
        List<Claim> GetAllClaims();
        Claim GetClaimByName(string programmeClaim);
    }
}
