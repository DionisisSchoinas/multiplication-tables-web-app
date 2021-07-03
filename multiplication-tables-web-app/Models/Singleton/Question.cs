using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace multiplication_tables_web_app
{
    public class Question
    {
        [Display(Name = "Ερώτηση")]
        public string question { get; set; }

        [Display(Name = "Σωστή Απάντηση")]
        public string answer { get; set; }

        [Display(Name = "Δοσμένη Απάντηση")]
        [Required]
        public string given_answer { get; set; }

        public Question()
        {
            this.question = "";
            this.answer = "";
            this.given_answer = "";
        }

        public Question(string question, string answer)
        {
            this.question = question;
            this.answer = answer;
            this.given_answer = "";
        }
    }
}