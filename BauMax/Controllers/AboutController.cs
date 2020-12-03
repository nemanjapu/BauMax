using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BauMax.Controllers
{
    [RoutePrefix("o-nama")]
    public class AboutController : Controller
    {
        [Route]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, Duration = 86400)]
        public ActionResult Index()
        {
            return View();
        }
    }
}