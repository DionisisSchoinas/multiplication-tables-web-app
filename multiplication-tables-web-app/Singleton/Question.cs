using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace multiplication_tables_web_app
{
    public class Question
    {
        public int num1 { get; set; }
        public int num2 { get; set; }
        public string answer { get; set; }

        public Question()
        {
            this.num1 = -1;
            this.num2 = -1;
            this.answer = "";
        }

        public Question(int num1, int num2)
        {
            this.num1 = num1;
            this.num2 = num2;
        }
    }
}