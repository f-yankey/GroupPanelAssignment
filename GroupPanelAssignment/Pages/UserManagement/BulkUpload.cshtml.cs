using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupPanelAssignment.Pages.UserManagement
{
    public class BulkUploadModel : PageModel
    {
        private IUserManagementService _userManagementService;

        public IFormFile Input { get; set; }

        public List<UploadViewModel> UploadResults { get; set; }

        public BulkUploadModel(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(IFormFile Input)
        {
            var uploadResult = await _userManagementService.BulkUploadAsync(Input);
            UploadResults = uploadResult;
            return Page();
        }
    }
}
