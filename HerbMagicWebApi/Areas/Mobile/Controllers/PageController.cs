using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HerbMagicWebApi.Areas.Mobile.Controllers
{
    public class PageController : Controller
    {
        // GET: Mobile/Page
        public ActionResult Index()
        {
            return View();
        }

    }
}