using MVC_Tutorial_Complete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Tutorial_Complete.Method
{
   
    public static  class Methods
    {
        private static Mvc_TutorialEntities2 _db = new Mvc_TutorialEntities2();

        public static bool Save(EmployeeViewModel model)
        {
            var empmodel = new Employee();
            empmodel.EmpName = model.EmpName;
            empmodel.DepartmentId = model.DepartmentId;
            empmodel.Address = model.Address;
            empmodel.Remember = model.Remember;
            _db.Employees.Add(empmodel);
            _db.SaveChanges();
            Site _model = new Site();
            _model.SiteName = model.SiteName;
            _model.EmployeeId = empmodel.EmployeeId;
            _db.Sites.Add(_model);
            int check = _db.SaveChanges();
            if (check > 0) return true;
            else return false;
        }

    }
}