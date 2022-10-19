using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GroupPanelAssignment.Data.Models;
using GroupPanelAssignment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroupPanelAssignment.Pages.UserManagement
{
    public class NewUserModel : PageModel
    {
        private readonly IUserManagementService _userManagementService;
        public List<Claim> Claims { get; set; }

        public UserAddModel Input { get; set; }

       
        public class UserAddModel
        {
            public List<string> Roles { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }

            [Required]
            public string FirstName { get; set; }
            public string Othernames { get; set; }

            [Required]
            public string Surname { get; set; }
            public string SpecialID { get; set; }
            public List<ExtraProperty> ExtraProperties { get; set; }
        }

        public class ExtraProperty
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
        
        public NewUserModel(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }
        public void OnGet()
        {
            SetClaims();
            Input = new UserAddModel();
        }

        public async Task<IActionResult> OnPost(UserAddModel Input)
        {
            if (!ModelState.IsValid)
            {
                return BackToPage(Input);
            }

            var result = await _userManagementService.AddNewUser(Input);

            if (!result.Key)
                return BackToPage(Input);

            return RedirectToPage("users");
        }

        private IActionResult BackToPage(UserAddModel Input)
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
