using Karkard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Karkard.Controllers
{
    public class QuizController : Controller
    {
        private readonly ILogger<QuizController> _logger;
        private static List<Answers> userAnswers = new List<Answers>();


        public QuizController(ILogger<QuizController> logger)
        {
            _logger = logger;

        }

        public IActionResult Quiz()
        {
            var answer = new Answers();
            List<Question> questions = new List<Question>
    {
        new Question
        {

            QuestionText = "بر اساس آیین نامه راهنمایی رانندگی ایستاندن ممنوع همان ........... است?",
            AnswerOptions = new List<string> { "توقف ممنوع", "توقف مطلقا ممنوع", "توقف", "ایست وسیله نقلیه در زمان کوتاه" },
            CorrectAnswerIndex = answer.Answer1
        },
                        new Question
                        { QuestionText = "در بزرگراه های درون شهری حداکثر سرعت چند کیلومتر در ساعت است?",
            AnswerOptions = new List<string> { "110 ", "120 ", "100", "95 " },
            CorrectAnswerIndex = answer.Answer2
                },
                                   new Question
                        { QuestionText = "از چند متری بعد از پیچ ها سبقت گرفتن آزاد است?",
            AnswerOptions = new List<string> { "30 متری ", "50 متری ", "20 متری", "100 متری " },
            CorrectAnswerIndex = answer.Answer3
                },
                                              new Question
                        { QuestionText = "قبل از شروع حرکت باید کدام یک از چراغ های خودرو مورد بازبینی قرار بگیرد?",
            AnswerOptions = new List<string> { "چراغ جلو - چراغ عقب - چراغ ABS - چراغ راهنما  ", "چراغ جلو - چراغ عقب - چراغ راهنما در صورت کثیف بودن ", "چراغ جلو - چراغ خطر  - چراغ دنده عقب - چراغ راهنما", "گزینه 2 و 3 صحیح است  " },
            CorrectAnswerIndex = answer.Answer4
                },
                                                         new Question
                        { QuestionText = "در آزادراه های برون شهری حداقل سرعت چند کیلومتر در ساعت است?",
            AnswerOptions = new List<string> { "95  ", "80  ", "70 ", "65  " },
            CorrectAnswerIndex = answer.Answer5
                },
    };


            Random rng = new Random();
            int n = questions.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var question = questions[k];
                questions[k] = questions[n];
                questions[n] = question;
            }

            return View(questions);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuizResult([Bind("Answer1,Answer2,Answer3,Answer4,Answer5")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                // Add the user's answers to the list
                userAnswers.Add(answers);

                // Calculate the number of correct answers
                int correctCount = CalculateCorrectAnswers(userAnswers);

                // Pass the user's answers, score, and correct count to the view
                ViewData["UserAnswers"] = userAnswers;
                ViewData["Score"] = correctCount;

                // Redirect to the quiz result view
                return View("QuizResult", userAnswers);
            }

            // If the model is not valid, return to the quiz form with validation errors
            return View("Quiz");
        }

        private int CalculateCorrectAnswers(List<Answers> answersList)
        {
            int correctCount = 0;

            // Compare user's answers with correct answers and increment score for each correct answer
            foreach (var userAnswers in answersList)
            {
                switch (userAnswers.Answer1)
                {
                    case 2:
                        correctCount++;
                        break ;
                    default:
                        break;
                }
                switch (userAnswers.Answer2)
                {
                    case 3: 
                        correctCount++; 
                        break ;
                    default: 
                        break;
                }
                switch(userAnswers.Answer3)
                {
                    case 2:
                        correctCount++;
                        break ;
                    default:
                        break;
                }

                switch (userAnswers.Answer4)
                {
                    case 3:
                        correctCount++;
                        break ;
                    default:
                        break;
                }

                switch(userAnswers.Answer5) {
                    case 3:
                        correctCount++;
                        break ;
                    default:
                        break;
                }
            }

            return correctCount;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}

