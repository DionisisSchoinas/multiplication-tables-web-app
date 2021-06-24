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
            string question;
            string answer;
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    question = i + " x " + j + " = ";
                    answer = (i * j).ToString();
                    questions.Add(new Question(question, answer));
                }
            }
            questions.Shuffle();
            questions.RemoveRange(50, 50);

            Test test = new Test();
            test.test_id = 10;  // 10 tables each has an id from 0 to 9, so finaltest gets 10
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
                if (!ModelState.IsValid)
                {
                    ViewBag.error = "Γέμισε όλα τις απαντήσεις";
                    return RedirectToAction("Questions");
                }

                var test = TempData["testQuestions"] as Test;
                for (int i = 0; i < testAnswers.questions.Count; i++)
                    test.questions[i].given_answer = testAnswers.questions[i].given_answer;

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
