using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectListMVC.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name="Project Manager")]
        public string ProjectManager { get; set; }
        [Display(Name = "Project Number")]
        public string ProjectNumber { get; set; }
    }
}
