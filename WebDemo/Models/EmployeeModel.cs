using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class EmployeeModel
    {
        [Display(Name = "Emplayee ID" )]
        [Required(ErrorMessage ="Employee ID is Required")]
        [Range(100000,999999 ,ErrorMessage ="Must be a valid 6-digit number")]
        public int EmployeeID { get; set; }
        
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        public string  FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is Required")]
        [StringLength(100,ErrorMessage ="Cannot Exceed 100 Characters")]
        public string  EmailAddress { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirm Email Address ")]
        [Required(ErrorMessage = "Email Address is Required")]
        [Compare("EmailAddress",ErrorMessage ="EmailAddresses must match")]
        [StringLength(100, ErrorMessage = "Cannot Exceed 100 Characters")]
        public string ConfirmEmailAddress { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password ")]
        [Required(ErrorMessage = "Password  is Required")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password ")]
        [Required(ErrorMessage = "Confirm Password  is Required")]
        [Compare("Password", ErrorMessage ="Passwordsmust match")]
        public string ConfirmPassword { get; set; }

    }
}