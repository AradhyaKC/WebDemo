using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Models
{
    public class AttendanceEdit
    {
        [DataType(DataType.Date)]
        [Validator.LesserThanNow(ErrorMessage = "fromDate must be before Now")]
        public DateTime fromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime toDate { get; set; }
    }
}