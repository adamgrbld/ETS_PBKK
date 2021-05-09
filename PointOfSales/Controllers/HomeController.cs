using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PointOfSales.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StuffList()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}