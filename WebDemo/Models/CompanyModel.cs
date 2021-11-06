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
    }
}