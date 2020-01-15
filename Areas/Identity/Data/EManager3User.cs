using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EManager3.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the EManager3User class
    public class EManager3User : IdentityUser
    {
        [PersonalData]
        [Required(ErrorMessage="First Name is Required")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [PersonalData]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [PersonalData]
        public string Sex { get; set; }

        [PersonalData]
        [DataType(DataType.Date)]
        [Display(Name="Date Of Birth")]
        public DateTime DOB { get; set; }

        [PersonalData]
        [Display(Name="Marital Status")]
        public string MaritalStatus { get; set; }

        [PersonalData]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [PersonalData]
        [Range(0, 20, ErrorMessage = "Children must be between 0 and 20")]
        [Display(Name="Number Of Children")]
        public int NumberOfChildren { get; set; }

        [PersonalData]
        [Display(Name="Active")]
        public bool isActive { get; set; }

        [PersonalData]
        [DataType(DataType.Date)]
        [Display(Name="Employment Date")]
        public DateTime EmploymentDate { get; set; }

        [PersonalData]
        [DataType(DataType.Text)]
        [Display(Name="Highest Educational Qualification")]
        public string HighestEducationQualification { get; set; }

        [PersonalData]
        public string School { get; set; }

        [PersonalData]
        [DataType(DataType.Date)]
        [Display(Name="Service Year")]
        public DateTime ServiceYear { get; set; }

        [PersonalData]
        [Display(Name="Company Position")]
        public string CompanyPosition { get; set; }

        [PersonalData]
        [DataType(DataType.Date)]
        [Display(Name="Last Promotion Date")]
        public DateTime LastPromotionDate { get; set; }

        [PersonalData]
        [Display(Name="Yearly Salary")]
        [Range(0, 10000000000, ErrorMessage = "Salary cannot be less than 0")]
        public int YearlySalary { get; set; }

        [Phone]
        [StringLength(11, ErrorMessage = "Phone number must be 11 digits long.", MinimumLength = 11)]
        [Display(Name="Phone Number")]
        public override string PhoneNumber { get; set; }
    }
}
