using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Models
{
    public class WorksOnModel
    {
        [Display(Name ="Employee Id")]
        [Required(ErrorMessage = "Employee Id is Required")]
        public int employeeId { get; set; }

        [Display(Name = "Name")]
        [Validator.LettersOnly(ErrorMessage = "First Name must be letters only ")]
        public string firstName { get; set; }

        [Display(Name ="Project Id")]
        [Required(ErrorMessage = "Project Id is Required")]
        public int projectId { get; set; }

        [Display(Name ="Role")]
        [Required(ErrorMessage = "Role is Required")]
        public string role { get; set; }

        [Display(Name ="Shift Start Time")]
        [Required(ErrorMessage = "Shift Start Time is Required")]
        [DataType(DataType.Time)]
        public DateTime shiftStartTime { get; set; }

        [Display(Name = "Shift End Time")]
        [Required(ErrorMessage = "Shift End Time is Required")]
        [DataType(DataType.Time)]
        public DateTime shiftEndTime { get; set; }

    }
}