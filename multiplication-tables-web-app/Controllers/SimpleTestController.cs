using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace multiplication_tables_web_app.Controllers
{
    public class SimpleTestController : Controller
    {
        // GET: SimpleTest/PickTest
        public ActionResult PickTest()
        {
            return View();
        }

        // GET: SimpleTest/Table/5
        // id : [1,10]
        public ActionResult Table(int id)
        {
            /*
            Create and return questions for the multiplication table of id
            id = 1
                1x1, 1x2, ...
            id = 2
                2x1, 2x2, ...
            */
            return View();
        }

        // POST: SimpleTest/Table
        [HttpPost]
        public ActionResult Table(FormCollection collection)
        {
            /*
            - Get questions and answers
            - Check them
            - Send to results page
            */

            try
            {
                // TODO: Add update logic here

                return Redirect("/Results");
            }
            catch
            {
                return View();
            }
        }
    }
}
