using Karkard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Karkard.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private static List<Person> personData = new List<Person>() {
            new Person {Name="Mahsa", PhoneNumber=0343403820, Email="hfdlsasa@gmail.com"},
            new Person {Name="Asal", PhoneNumber=593485734, Email="adjkdfwsl@gmail.com"}

        };



        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;

        }



        public IActionResult Contact()
        {

            var sortedData = personData.OrderBy(p => p.Name).ToList();

            return View(sortedData);
        }

        public IActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Form([FromForm] Person person)
        {
            personData.Add(person);
            // validate data

            return RedirectToAction("Contact");
        }

        public IActionResult Search(string searchString)
        {
            //searchString = searchString?.Trim(); 

            var searchResults = new List<Person>();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower(); 

                searchResults = personData.Where(p => p.Name.ToLower().Contains(searchString) || p.Email.ToLower().Contains(searchString)).ToList();

                if (int.TryParse(searchString, out int phoneNumber))
                {
                    searchResults.AddRange(personData.Where(p => p.PhoneNumber == phoneNumber));
                }
            }

            return View("Search", searchResults);
        }


     




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
