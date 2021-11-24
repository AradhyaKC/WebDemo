using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebDemo.Models
{
    public class CompanyModel
    {
        [Display(Name ="Name of Company")]
        [Required(ErrorMessage ="name of company should be entered")]
        public string companyName { get; set; }

        [Display(Name = "Motto")]
        [Required(ErrorMessage = "Motto of company should be entered")]
        public string motto { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date when company was started")]
        [Required(ErrorMessage = "Start date should be entered")]
        public DateTime startDate { get; set; }

        [Display(Name ="Address")]
        [Required(ErrorMessage ="Address must be entered")]
        public string address { get; set; }
        
        [Display(Name ="Phone No")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage ="Phone no must be entered")]
        public string phoneNo { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Email Address must be entered")]
        public string emailAddress { get; set; }
    }
}