using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class ProjectModel
    {
        public int projectId { get; set; }
        public string projectName { get; set; }
        public int projectLeaderId { get; set; }
        public string description { get; set; }
        public string companyName { get; set; }
    }
}
