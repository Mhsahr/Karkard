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
            return View();
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

