using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial_Complete
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
