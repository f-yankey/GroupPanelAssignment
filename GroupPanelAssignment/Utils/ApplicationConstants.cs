using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Utils
{
    public static class ApplicationConstants
    {
        //  session constants
        public const string RolesInSession = "RolesInSession";
        public const string TeamsInSession = "TeamsInSession";

        //  transaction constants
        public const string BeforeNewUserTransaction = "BeforeNewUser";
        public const string BeforeNewGroupSaveTransaction = "BeforeNewGroupSaveTransaction";
        

        //  extra property(claim) name constants
        public const string ProgrammeClaim = "Programme Name";
        public const string CWAClaim = "CWA";

        //  word constants
        public const string RoleWord = "Role";
        public const string CSVExtensionWord = ".csv";

        //  directory contstants
        public const string UploadDirectory = "uploaded-files";

        //  side navigation view texts
        public const string UserManagement = "Users";
        public const string TeamManagement = "Groups/Teams";

        //  role constants
        public const string StudentRole = "Student";
        public const string SupervisorRole = "Supervisor";
        public const string PanelMemberRole = "Panel Member";
        public const string AdminRole = "Admin";
        public const string SuperAdminRole = "Super Admin";
    }
}
