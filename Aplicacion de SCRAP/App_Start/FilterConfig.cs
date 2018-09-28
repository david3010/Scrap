using System.Web;
using System.Web.Mvc;

namespace Aplicacion_de_SCRAP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
