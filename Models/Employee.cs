//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Tutorial_Complete.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Sites = new HashSet<Site>();
        }
    
        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<bool> Remember { get; set; }
        public string Address { get; set; }
    
        public virtual Departmnet Departmnet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Site> Sites { get; set; }
    }
}
