using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class WorksOn
    {
        public int employeeId { get; set; }
        public int projectId { get; set; }
        public string role { get; set; }
        public DateTime shiftStartTime { get; set; }
        public DateTime shiftEndTime { get; set; }
    }
}
