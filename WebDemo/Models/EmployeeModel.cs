using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class EmployeeModel
    {
        [Display(Name = "Employee ID")]
        public int employeeId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string lastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is Required")]
        [StringLength(100, ErrorMessage = "Cannot Exceed 100 Characters")]
        public string emailAddress { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirm Email Address ")]
        [Required(ErrorMessage = "Email Address is Required")]
        [Compare("emailAddress", ErrorMessage = "EmailAddresses must match")]
        [StringLength(100, ErrorMessage = "Cannot Exceed 100 Characters")]
        public string confirmEmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone No")]
        [Required(ErrorMessage = "Phone Number is required")]
        public string phoneNo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = " Date Of Birth")]
        [Required(ErrorMessage ="Date of birth is required")]
        public DateTime dateOfBirth { get; set; }

        [Display(Name ="Salary")]
        [Required(ErrorMessage ="Salary is required")]
        public long salary { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password ")]
        [Required(ErrorMessage = "Password  is Required")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password ")]
        [Required(ErrorMessage = "Confirm Password  is Required")]
        [Compare("password", ErrorMessage = "Passwords must match")]
        public string confirmPassword { get; set; }

        [Display(Name ="Allot number of leaves")]
        [Required(ErrorMessage ="number of leaves must be entered" )]
        public int leavesAvailable { get; set; }

        [Display(Name = "Enter number of credits")]
        [Required(ErrorMessage = "nuumber of credits must be entered")]
        public int credits { get; set; }
    }
}