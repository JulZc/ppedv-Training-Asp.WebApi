using System.Web;
using System.Web.Mvc;

namespace WebApi_Course_ppedv
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
