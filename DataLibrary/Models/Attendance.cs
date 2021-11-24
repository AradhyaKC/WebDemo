using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class Attendance
    {
        public int employeeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
    }
}
