using MVC_Tutorial_Complete.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_Complete.Controllers
{
    [RoutePrefix("home")]
    public class ImageController : Controller
    {
        Mvc_TutorialEntities2 _db = new Mvc_TutorialEntities2();
        // GET: Image
        public ActionResult Index()
        {
            ViewBag.countrylist = new SelectList(GetCountryList(),"CountryId","CountryName");
            ViewBag.employeelist = GetEmployees();
            List<CheckboxItems> cblist = new List<CheckboxItems>();
            cblist.Add(new CheckboxItems() { ItemId = 1, ItemName = "Dosa", IsAvailable = true });
            cblist.Add(new CheckboxItems() { ItemId = 2, ItemName = "chowmein", IsAvailable = false });
            cblist.Add(new CheckboxItems() { ItemId = 3, ItemName = "special masala dosa", IsAvailable = true });
            cblist.Add(new CheckboxItems() { ItemId = 4, ItemName = "Uttpam", IsAvailable = false });
            cblist.Add(new CheckboxItems() { ItemId = 5, ItemName = "sambar", IsAvailable = true });
            cblist.Add(new CheckboxItems() { ItemId = 6, ItemName = "Idli", IsAvailable = false });
            ViewBag.Itemlist = cblist;



            return View();
        }
        public ActionResult Image()
        {
            return View();
        }
        public JsonResult Upload(ProductViewModel _model)
        {
            var file = _model.ImageFile;
            if(file!=null)
            {
                var filename = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                file.SaveAs(Server.MapPath("/UploadImage/" + file.FileName));

            }
            return Json(file.FileName, JsonRequestBehavior.AllowGet);


        }
        public ActionResult ImageUpload()
        {
            return View();

        }
        [HttpPost]
        public JsonResult Uploading(ProductViewModel _model)
        {
            Mvc_TutorialEntities2 _db = new Mvc_TutorialEntities2();
            var file = _model.ImageFile;
            byte[] ImageByte = null;
            var url = _model.ImageUrl;
            var imageid = 0;
            if (file!=null)
            {
                //var filename = Path.GetFileName(file.FileName);
                //var extension = Path.GetExtension(file.FileName);
                //var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                file.SaveAs(Server.MapPath("/UploadImage/" + file.FileName));
                BinaryReader reader = new BinaryReader(file.InputStream);
                ImageByte = reader.ReadBytes(file.ContentLength);
                ImageStoreInDatabase _img = new ImageStoreInDatabase();
                _img.ImageByte = ImageByte;
                _img.ImageName = file.FileName;
                _img.ImagePath = "/UploadImage/" + file.FileName;
                _img.IsDeleted = false;
                //_img.ImageId = 0;
                _db.ImageStoreInDatabases.Add(_img);
                _db.SaveChanges();
                imageid = _img.ImageId;
            }
            if(url != null)
            {
                try
                {
                    ImageByte = Download(url);
                    ImageStoreInDatabase _img = new ImageStoreInDatabase();
                    _img.ImageByte = ImageByte;
                    _img.ImageName = "DownloadedImage";
                    _img.ImagePath = url;
                    _img.IsDeleted = false;
                    _img.ImageId = 0;
                    _db.ImageStoreInDatabases.Add(_img);
                    _db.SaveChanges();
                    imageid = _img.ImageId;

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                 
            }
            return Json(imageid, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RetrieveImage(int Imgid)
        {
            Mvc_TutorialEntities2 _db = new Mvc_TutorialEntities2();
            var imgdata = _db.ImageStoreInDatabases.SingleOrDefault(x => x.ImageId == Imgid);
            return File(imgdata.ImageByte, "image/jpg");
        }
        
        public byte[] Download(string url)
        {
            var webclient = new WebClient();
            byte[] imagebyte = webclient.DownloadData(url);
            return imagebyte;

        }

        public List<Country> GetCountryList()
        {
            Mvc_TutorialEntities2 _db = new Mvc_TutorialEntities2();
            var countrylist = _db.Countries.ToList();
            return countrylist;
        }
        public ActionResult GetStateList(int CountryId)
        {
            var statelist = _db.States.Where(x => x.CountryId == CountryId).ToList();
            ViewBag.statelist = new SelectList(statelist, "StateId", "StateName");
            return PartialView("Stateoptionview");
            
        }
        public List<EmployeeViewModel> GetEmployees()
        {
            var elist = _db.Employees.Select(x => new EmployeeViewModel() { EmployeeId = x.EmployeeId, EmpName = x.EmpName, Address = x.Address, DepartName = x.Departmnet.DepartName }).ToList();
            return elist;
        }

        public ActionResult GetListBySearchText(string Searchtext)
        {
            var elist = _db.Employees.Where(x => x.EmpName.Contains(Searchtext) || x.Departmnet.DepartName.Contains(Searchtext)).
                Select(x => new EmployeeViewModel()
                { EmployeeId = x.EmployeeId, EmpName = x.EmpName, Address = x.Address, DepartName = x.Departmnet.DepartName }).ToList();
            return PartialView("SearchedEmployeeListViewModel", elist);
        }

        [Route("Students")]
        public string GetStudent()
        {
            return "All Student";
        }
        [Route("Students/{id}")]
        public string GetStudentById(int id)
        {
            return "student by Id" + id;
        }
        // optional parameter 
        // [Route("Students/Name/{Name?}")]
        // default parameter 
        // [Route("Students/Name/{Name=ashsish}")]
        [HttpPost]
        public JsonResult SaveList(string[] itemlist)
        {
            //string[] ids = itemlist.Split(',');
            return Json("", JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetItemlist (string itemtext)
        {
            List<CheckboxItems> cblist = new List<CheckboxItems>();
            cblist.Add(new CheckboxItems() { ItemId = 1, ItemName = "Dosa", IsAvailable = true });
            cblist.Add(new CheckboxItems() { ItemId = 2, ItemName = "chowmein", IsAvailable = false });
            cblist.Add(new CheckboxItems() { ItemId = 3, ItemName = "special masala dosa", IsAvailable = true });
            cblist.Add(new CheckboxItems() { ItemId = 4, ItemName = "Uttpam", IsAvailable = false });
            cblist.Add(new CheckboxItems() { ItemId = 5, ItemName = "sambar", IsAvailable = true });
            cblist.Add(new CheckboxItems() { ItemId = 6, ItemName = "Idli", IsAvailable = false });
            var list = cblist.Where(x => x.ItemName.ToLower().Contains(itemtext.ToLower())).Select(x =>x.ItemName).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SendEmailToClient()
        {
            var IsSent =  SendEmail("s.shivamjha98@gmail.com", "<p>Hi Shivam<br />This mail is for sending Purpose <br />Regards </p>", "Testing Email");
            return Json(IsSent, JsonRequestBehavior.AllowGet);

        }
        public bool SendEmail(string ToEmail,string Body,string Subject)
        {
            
            try
            {
                var SenderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                var Senderpassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(SenderEmail, Senderpassword);
                MailMessage mail = new MailMessage(SenderEmail, ToEmail, Subject, Body);
                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mail); //Go to mail security setting switch off 2-factor authentication and enable less secured apps then u can send mail
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            


        }

        public JsonResult GetTableDataForEmployees(DataTablesParams data,string Ename)
        {
            List<EmployeeViewModel> _emp = new List<EmployeeViewModel>();

            int pageNo = 1;
            int totalCount = 0;
            if(data.iDisplayStart >= data.iDisplayLength)
            {
                pageNo = (data.iDisplayStart / data.iDisplayLength) + 1;
            }
            if(data.sSearch != null)
            {
                totalCount = _db.Employees.Where(x => x.EmpName.Contains(data.sSearch) || x.Departmnet.DepartName.Contains(data.sSearch) || x.Address.Contains(data.sSearch)).Count();
                _emp = _db.Employees.Where(x=>x.EmpName.Contains(data.sSearch)||x.Departmnet.DepartName.Contains(data.sSearch)||x.Address.Contains(data.sSearch)).OrderBy(x=>x.EmployeeId)
                    .Skip((pageNo-1)*data.iDisplayLength).Take(data.iDisplayLength).Select(x => new EmployeeViewModel { EmpName = x.EmpName, EmployeeId = x.EmployeeId, DepartmentId = x.Departmnet.DepartmentId, DepartName = x.Departmnet.DepartName, Address = x.Address }).ToList();
                return Json(new
                {
                    aaData = _emp,
                    sEcho = data.sEcho,
                    iTotalDisplayRecords = totalCount,
                    iTotalRecords = totalCount
                }, JsonRequestBehavior.AllowGet);
            }
            else if (Ename != null)
            {
                totalCount = _db.Employees.Where(x => x.EmpName.Contains(Ename)).Count();
                _emp = _db.Employees.Where(x => x.EmpName.Contains(Ename)).OrderBy(x => x.EmployeeId)
                    .Skip((pageNo - 1) * data.iDisplayLength).Take(data.iDisplayLength).Select(x => new EmployeeViewModel { EmpName = x.EmpName, EmployeeId = x.EmployeeId, DepartmentId = x.Departmnet.DepartmentId, DepartName = x.Departmnet.DepartName, Address = x.Address }).ToList();
                return Json(new
                {
                    aaData = _emp,
                    sEcho = data.sEcho,
                    iTotalDisplayRecords = totalCount,
                    iTotalRecords = totalCount
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                totalCount = _db.Employees.Count();
                _emp = _db.Employees.OrderBy(x=>x.EmployeeId).Skip((pageNo-1)*data.iDisplayLength).Take(data.iDisplayLength).Select(x => new EmployeeViewModel { EmpName = x.EmpName, EmployeeId = x.EmployeeId, DepartmentId = x.Departmnet.DepartmentId, DepartName = x.Departmnet.DepartName, Address = x.Address }).ToList();
                return Json(new
                {
                    aaData = _emp,
                    sEcho = data.sEcho,
                    iTotalDisplayRecords = totalCount,
                    iTotalRecords = totalCount
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}