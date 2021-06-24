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

        // GET: SimpleTest/Table/:id
        // id : [1,10]
        public ActionResult Table(int id)
        {
            if (id < 1 || id > 10)
            {
                return Redirect("/SimpleTest/PickTest");
            }

            List<Question> questions = new List<Question>();
            string question;
            string answer;
            for (int i = 1; i <= 10; i++)
            {
                question = id + " x " + i + " = ";
                answer = (id * i).ToString();
                questions.Add(new Question(question, answer));
            }
            questions.Shuffle();

            Test test = new Test();
            test.questions = questions;
            TempData["testId"] = id.ToString();
            TempData["testQuestions"] = test;
            return View(test);
        }

        // POST: SimpleTest/Table
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Table(Test testAnswers)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var testId = TempData["testId"] as string;
                    ViewBag.error = "Γέμισε όλα τις απαντήσεις";
                    return RedirectToAction("Table/" + testId);
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
