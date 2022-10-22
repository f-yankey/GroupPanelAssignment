using GroupPanelAssignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Role GetByName(string roleName);
    }
}
