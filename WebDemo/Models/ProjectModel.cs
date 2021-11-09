using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{
    public class ProjectModel
    {
        [Display(Name = "Project Id")]
        public int projectId { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "name of the project should be entered ")]
        public string projectName { get; set; }

        [Display(Name = "Project Leader ID")]
        [Required(ErrorMessage = "Id of the leader of the project should be entered")]
        public int projectLeaderId { get; set; }

        [Display(Name ="Project Leader Name")]
        public string projectLeaderName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description of the project must be entered")]
        public string description { get; set; }

        [Display(Name = "Company Name")]
        public string companyName { get; set; }
    }
}