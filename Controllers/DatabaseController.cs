using MVC_Tutorial_Complete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVC_Tutorial_Complete.Controllers
{
    public class DatabaseController : Controller
    {
        private Mvc_TutorialEntities2 _db = new Mvc_TutorialEntities2();

        // GET: Database
        public ActionResult Index()
        {
            var vmlist = new List<EmployeeViewModel>();
            var abc = _db.Employees.ToList();

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

        //NOTES(Server-Side-Validation)
        //for server side validation pehle model pe required define kro phir view me
        //validatiionmessagefor lgao phir jb submit pe hit hoga or khaali model pass
        //hoga then humara validation call hoga model check krne ke
        //liye modelstate.isvalid=true lgaa ke che ck kro


        [HttpPost]
        public ActionResult FormView(EmployeeViewModel model)
        {
            if(ModelState.IsValid==true)
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
                    Site _model = new Site();
                    _model.SiteName = model.SiteName;
                    _model.EmployeeId = empmodel.EmployeeId;
                    _db.Sites.Add(_model);
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

        public ActionResult DeleteData()
        {
            var data = new List<EmployeeViewModel>();
            data = _db.Employees.ToList().Select(x => new EmployeeViewModel { EmployeeId = x.EmployeeId, EmpName = x.EmpName, Address = x.Address, DepartName = x.Departmnet.DepartName }).ToList();
            return View(data);
        }
        public JsonResult DeleteEmployeeById(int id)
        {
            
            if(id!=0)
            {
                var check = _db.Sites.FirstOrDefault(x => x.EmployeeId == id);
                if(check != null)
                {
                    _db.Sites.Remove(check);
                    _db.SaveChanges();
                    _db.Employees.Remove(_db.Employees.SingleOrDefault(x => x.EmployeeId == id));
                    _db.SaveChanges();
                    return Json(new { Status = true, msg = "Delete Successfully" });
                }
                else
                {
                    return Json(new { Status = false, msg = "Something went wrong" });
                }
            }
            else
            {
                return Json(new { Status=false,msg="Something went wrong"});
            }
        }

        public ActionResult GetPartialViewById()
        {
            TempData["data"] = _db.Employees.Where(x => x.EmployeeId == 15).Select(x => new EmployeeViewModel
            { EmployeeId = 15, EmpName = x.EmpName, DepartName = x.Departmnet.DepartName }).SingleOrDefault();
            var data = _db.Employees.Where(x => x.EmployeeId == 15).Select(x => new EmployeeViewModel
            { EmployeeId = 15, EmpName = x.EmpName, DepartName = x.Departmnet.DepartName }).SingleOrDefault();
            return PartialView("/Views/Shared/_partialview.cshtml");
        }

    }
}