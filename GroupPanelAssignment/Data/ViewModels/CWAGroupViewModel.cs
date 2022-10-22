using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPanelAssignment.Data.ViewModels
{
    public class CWAGroupViewModel
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public List<UserViewModel> Students { get; set; }
    }
}
