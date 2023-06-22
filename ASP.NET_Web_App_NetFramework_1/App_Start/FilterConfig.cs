using System.Web;
using System.Web.Mvc;

namespace ASP.NET_Web_App_NetFramework_1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
