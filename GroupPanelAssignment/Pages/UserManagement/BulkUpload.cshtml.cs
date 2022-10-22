using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupPanelAssignment.Pages.UserManagement
{
    public class BulkUploadModel : PageModel
    {
        private IUserManagementService _userManagementService;

        public bool HasErrorMessage { get; set; }
        public string ErrorMessage { get; set; }
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
            var validationResults = ValidateRequest(Input);
            HasErrorMessage = validationResults.Key == false;
            ErrorMessage = validationResults.Value;
            if (HasErrorMessage)
            {
                return Page();
            }

            var uploadResult = await _userManagementService.BulkUploadAsync(Input);
            UploadResults = uploadResult;
            return Page();
        }

        private KeyValuePair<bool,string> ValidateRequest(IFormFile file)
        {
            if (file == null)
                return new KeyValuePair<bool, string>(false, "We detected no file. Kindly select a file!");

            string extension = Path.GetExtension(file.FileName);
            bool isCSV = extension.ToLower().Trim() == ApplicationConstants.CSVExtensionWord;
            if (!isCSV)
                return new KeyValuePair<bool, string>(false, $"You provided a '{extension}' file. Only CSVs are allowed!");
          
            return new KeyValuePair<bool, string>(true,"Validation successful!");
        }
    }
}
