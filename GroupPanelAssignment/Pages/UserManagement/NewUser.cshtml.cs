using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Data.ViewModels;
using GroupPanelAssignment.Services.Interfaces;
using GroupPanelAssignment.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupPanelAssignment.Pages.UserManagement
{
    public class NewUserModel : PageModel
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IGropanObjectFactory _gropanObjectFactory;

        public List<Claim> Claims { get; set; }

        public UserAddViewModel Input { get; set; }

        public NewUserModel(IUserManagementService userManagementService, IGropanObjectFactory gropanObjectFactory)
        {
            _userManagementService = userManagementService;
            _gropanObjectFactory = gropanObjectFactory;
        }
        public void OnGet()
        {
            SetClaims();
            Input = _gropanObjectFactory.CreateNewUserAddViewModel();
        }

        public async Task<IActionResult> OnPost(UserAddViewModel Input)
        {
            if (!ModelState.IsValid)
            {
                return BackToPage(Input);
            }

            var result = await _userManagementService.AddNewUser(Input);

            if (!result.Key)
                return BackToPage(Input);

            return RedirectToPage("~/Pages/UserManagement/Users.cshtml");
        }

        private IActionResult BackToPage(UserAddViewModel Input)
        {
            // set error 
            this.Input = Input;
            SetClaims();
            return Page();
        }

        private void SetClaims()
        {
            Claims = _userManagementService.GetAllClaims();
        }
    }
}
