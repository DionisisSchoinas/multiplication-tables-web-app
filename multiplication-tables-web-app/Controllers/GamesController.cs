using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace multiplication_tables_web_app.Controllers
{
    public class GamesController : Controller
    {
        public ActionResult CloudGame()
        {
            ViewBag.hint = "Βάλε την απάντησή σου στο κουτάκι";
            return View();
        }

        public ActionResult CardGame()
        {
            ViewBag.hint = "Βάλε την απάντησή σου στο κουτάκι";
            return View();
        }
    }
}