using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EManager3.Models
{
    // Add profile data for application users by adding properties to the EManager3User class
    public class UserUpdate
    {
        public string Id { get; set; }

        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name="Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name="Sex")]
        public string Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Date Of Birth")]
        public DateTime DOB { get; set; }

        [Required]
        [Display(Name="Marital Status")]
        public string MaritalStatus { get; set; }
        
        [Range(0, 20, ErrorMessage = "Children must be between 0 and 20")]
        [Display(Name="Number Of Children")]
        public int NumberOfChildren { get; set; }

        [Display(Name="Active")]
        public bool isActive { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Employment Date")]
        public DateTime EmploymentDate { get; set; }

        [Display(Name="Highest Educational Qualification")]
        public string HighestEducationQualification { get; set; }

        [Display(Name="School")]
        public string School { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Service Year")]
        public DateTime ServiceYear { get; set; }

        [Display(Name="Company Position")]
        public string CompanyPosition { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Last Promotion Date")]
        public DateTime LastPromotionDate { get; set; }

        [Display(Name="Yearly Salary")]
        public int YearlySalary { get; set; }

    }
}