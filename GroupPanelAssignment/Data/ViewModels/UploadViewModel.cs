using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GroupPanelAssignment.Pages.UserManagement.NewUserModel;

namespace GroupPanelAssignment.Data.ViewModels
{
    public class UploadViewModel
    {
        public bool ConversionSucceeded { get; set; }
        public bool DbSaveSucceeded { get; set; }
        public string Message { get; set; }
        public UserAddViewModel User { get; set; }
    }
}
