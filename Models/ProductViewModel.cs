using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Tutorial_Complete.Models
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public int? ImageId { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }
        public string ImageUrl { get; set; }
    }
}