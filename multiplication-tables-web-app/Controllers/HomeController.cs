using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using multiplication_tables_web_app.Models;

namespace multiplication_tables_web_app.Controllers
{
    public class HomeController : Controller
    {
        private SchoolDBEntities db = new SchoolDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Theory()
        {
            var testNames = db.TestNames.ToList();
            testNames.RemoveAt(testNames.Count()-1);
            ViewBag.testNames = testNames;
            return View();
        }
    }
}