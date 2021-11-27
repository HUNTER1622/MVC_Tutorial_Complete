using System.Web.Mvc;

namespace MVC_Tutorial_Complete.Controllers
{
    public class TestController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            //List<string> _str = new List<string>();
            //_str.Add("shivam");
            //_str.Add("Divya");
            //_str.Add("Akanksha");
            //_str.Add("Shivaay");
            //ViewBag.list = _str;
            //return View();



            //List<Employee> _emplist = new List<Employee>();
            //_emplist.Add(new Employee() { EmpId = 1, Department = "IT", Name = "shivam" });
            //_emplist.Add(new Employee() { EmpId = 2, Department = "sales", Name = "rinku" });
            //_emplist.Add(new Employee() { EmpId = 3, Department = "HR", Name = "Divya" });

            //ViewBag.emplist = _emplist;
            //ViewData["emplist"] = _emplist;

            //ViewBag.Msg1 = "Viewbag";
            //ViewData["msg2"] = "ViewData";
            //TempData["Msg3"] = "TempData";
            //TempData.Keep();

            return View();
        }
        public ActionResult SecondPage()
        {
            //TempData.Keep();
            return View();

        }

        public ActionResult ThirdPage()
        {
            return View();

        }





    }
}