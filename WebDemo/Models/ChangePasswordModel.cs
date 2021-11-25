using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Models
{
    public class ChangePasswordModel
    {

        [DataType(DataType.Password)]
        [Display(Name = "Old Password ")]
        [Required(ErrorMessage = "oldPassword  is Required")]
        public string oldpassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password ")]
        [Required(ErrorMessage = "Password  is Required")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password ")]
        [Required(ErrorMessage = "Confirm Password  is Required")]
        [Compare("password", ErrorMessage = "Passwords must match")]
        public string confirmPassword { get; set; }
    }
}