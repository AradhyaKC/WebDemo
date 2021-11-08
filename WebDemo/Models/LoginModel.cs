using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Models
{
    public class LoginModel
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = " Email Address")]
        [Required(ErrorMessage ="email address must be entered")]
        public string emailAddress { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        [Required(ErrorMessage ="Password must be entered")]
        public string password { get; set; }
    }
}