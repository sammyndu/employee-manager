using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EManager3.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EManager3.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<EManager3User> _userManager;
        private readonly SignInManager<EManager3User> _signInManager;

        public IndexModel(
            UserManager<EManager3User> userManager,
            SignInManager<EManager3User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name="First Name")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name="Last Name")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name="Sex")]
            public string Sex { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name="Date of Birth")]
            public DateTime DOB { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name="Marital Status")]
            public string MaritalStatus { get; set; }

            [Required]
            [Display(Name="Number Of Children")]
            public int NumberOfChildren { get; set; }
            
            [Display(Name="Active")]
            public bool isActive { get; set; }
            
            [DataType(DataType.Date)]
            [Display(Name="Employment Date")]
            public DateTime? EmploymentDate { get; set; }
            
            [DataType(DataType.Text)]
            [Display(Name="Highest Educational Qualification")]
            public string HighestEducationQualification { get; set; }
            
            [DataType(DataType.Text)]
            [Display(Name="School")]
            public string School { get; set; }
            
            [DataType(DataType.Date)]
            [Display(Name="Service Year")]
            public DateTime? ServiceYear { get; set; }
            
            [DataType(DataType.Text)]
            [Display(Name="Company Position")]
            public string CompanyPosition { get; set; }

            [DataType(DataType.Date)]
            [Display(Name="Last Promotion Date")]
            public DateTime? LastPromotionDate { get; set; }

            [Display(Name="Yearly Salary")]
            public int YearlySalary { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(EManager3User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Sex = user.Sex,
                DOB = user.DOB,
                MaritalStatus = user.MaritalStatus,
                NumberOfChildren = user.NumberOfChildren,
                isActive = user.isActive,
                EmploymentDate = user.EmploymentDate,
                HighestEducationQualification = user.HighestEducationQualification,
                School= user.School,
                CompanyPosition = user.CompanyPosition,
                LastPromotionDate = user.LastPromotionDate,
                YearlySalary = user.YearlySalary,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
