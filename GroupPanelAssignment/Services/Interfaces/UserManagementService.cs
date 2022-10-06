﻿using GroupPanelAssignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Services.Interfaces
{
    public interface IUserManagementService
    {
        List<AppUser> GetAppUsers(string role);
        List<Role> GetRoles();
      
    }
}
