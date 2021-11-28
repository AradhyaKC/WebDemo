using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Models
{
    public class LeaveModel
    {
        [Display(Name = "Employee Id")]
        public int employeeId { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage ="Start Date must be entered")]
        [DataType(DataType.Date)]
        [Validator.GreaterThanNow(Years =0,ErrorMessage = "StartDate Must be after today" )]
        public DateTime startDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date must be entered")]
        [DataType(DataType.Date)]
        [Validator.MustBeGreaterThanDateTime(days =1,otherPropertyName ="startDate", ErrorMessage ="Must be after Start Date")]
        public DateTime endDate { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name ="Reason")]
        [Required(ErrorMessage ="Reason must be entered")]
        public string reason { get; set; }
    }
}