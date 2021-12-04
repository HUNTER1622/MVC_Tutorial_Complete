using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Tutorial_Complete.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="Enter Name")]
        public string EmpName { get; set; }
        [Required(ErrorMessage = "Enter DepartmentName")]
        public Nullable<int> DepartmentId { get; set; }
        public string DepartName { get; set; }
        public bool Remember { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Site Name")]
        public string SiteName { get; set; }

    }
}