using System.Web;
using System.Web.Mvc;

namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
