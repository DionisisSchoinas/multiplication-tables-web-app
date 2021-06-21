using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace multiplication_tables_web_app.Controllers
{
    public class FinalTestController : Controller
    {
        // GET: FinalTest/Questions
        public ActionResult Questions()
        {
            /*
            Create and return questions for all multiplication tables
            */
            return View();
        }

        // POST: FinalTest/Questions
        [HttpPost]
        public ActionResult Questions(FormCollection collection)
        {
            /*
            - Get questions and answers
            - Check them
            - Send to results page
            */

            try
            {
                // TODO: Add insert logic here

                return Redirect("/Results");
            }
            catch
            {
                return View();
            }
        }
    }
}
