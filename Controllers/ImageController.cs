using MVC_Tutorial_Complete.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_Complete.Controllers
{
    public class ImageController : Controller
    {
        Mvc_TutorialEntities2 _db = new Mvc_TutorialEntities2();
        // GET: Image
        public ActionResult Index()
        {
            ViewBag.countrylist = new SelectList(GetCountryList(),"CountryId","CountryName");
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
    }
}