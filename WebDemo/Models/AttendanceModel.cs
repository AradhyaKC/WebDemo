using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebDemo.Models
{
    public class AttendanceModel
    {
        [Display(Name ="Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Check In Time")]
        public DateTime CheckInTime { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Check Out Time")]
        public DateTime CheckOutTime { get; set; }

    }
}