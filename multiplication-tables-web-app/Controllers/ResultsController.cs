using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace multiplication_tables_web_app.Controllers
{
    public class ResultsController : Controller
    {
        // GET: Results
        public ActionResult Index()
        {
            try
            {
                Test testAnswers = TempData["test"] as Test;
                return View(testAnswers);
            }
            catch
            {
                return Redirect("/");
            }
        }
    }
}
