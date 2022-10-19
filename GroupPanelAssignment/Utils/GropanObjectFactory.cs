using GroupPanelAssignment.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Utils
{

    public interface IGropanObjectFactory
    {
        AppUser CreateNewAppUser();
    }

    public class GropanObjectFactory : IGropanObjectFactory
    {
        public AppUser CreateNewAppUser()
        {
            return new AppUser();
        }
    }
}
