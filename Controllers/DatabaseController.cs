using MVC_Tutorial_Complete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_Complete.Controllers
{
    public class DatabaseController : Controller
    {
        private Mvc_TutorialEntities _db = new Mvc_TutorialEntities();

        // GET: Database
        public ActionResult Index()
        {
            var vmlist = new List<EmployeeViewModel>();
            vmlist = _db.Employees.ToList().Select(x => new EmployeeViewModel() { EmployeeId = x.EmployeeId, EmpName = x.EmpName }).ToList();
            return View(vmlist);
        }

        public ActionResult GetDataById(int Empid)
        {

            EmployeeViewModel _emp = _db.Employees.Where(x => x.EmployeeId == Empid).Select(x => new EmployeeViewModel() { EmployeeId = x.EmployeeId, EmpName = x.EmpName, DepartmentId = x.DepartmentId, DepartName = x.Departmnet.DepartName,Address=x.Address }).SingleOrDefault();
            TempData["msg"] = _emp;
            TempData.Keep();
            return RedirectToAction("Index");
        }
        public ActionResult FormView ()
        {
            var data = _db.Departmnets.ToList();
            ViewBag.deplist = new SelectList(data, "DepartmentId", "DepartName");

            return View();
        }
        [HttpPost]
        public ActionResult FormView(EmployeeViewModel model)
        {
            if(model.DepartmentId!=null)
            {
                try
                {
                    var empmodel = new Employee();
                    empmodel.EmpName = model.EmpName;
                    empmodel.DepartmentId = model.DepartmentId;
                    empmodel.Address = model.Address;
                    empmodel.Remember = model.Remember;
                    _db.Employees.Add(empmodel);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                var data = _db.Departmnets.ToList();
                ViewBag.deplist = new SelectList(data, "DepartmentId", "DepartName");
                return View(model);

            }
        }

    }
}