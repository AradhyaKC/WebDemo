using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class Leave
    {
        public int employeeId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string reason { get; set; }
    }
}
