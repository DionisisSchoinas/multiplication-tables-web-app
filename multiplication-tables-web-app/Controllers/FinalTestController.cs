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
            List<Question> questions = new List<Question>();
            for (int i = 1; i <= 10; i++)
                for (int j = 1; j <= 10; j++)
                questions.Add(new Question(i, j));
            questions.Shuffle();
            questions.RemoveRange(50, 50);

            Test test = new Test();
            test.questions = questions;
            TempData["testQuestions"] = test;
            return View(test);
        }

        // POST: FinalTest/Questions
        [HttpPost]
        public ActionResult Questions(Test testAnswers)
        {
            try
            {
                var test = TempData["testQuestions"] as Test;
                for (int i = 0; i < testAnswers.questions.Count; i++)
                    test.questions[i].answer = testAnswers.questions[i].answer;

                TempData["test"] = test;
                return RedirectToAction("Index", "Results");
            }
            catch
            {
                return Redirect("/");
            }
        }
    }
}
