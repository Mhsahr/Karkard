using Karkard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Karkard.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private static List<Work> workData = new List<Work>() {
            new Work { Id =1, Name="حسن", From="8:00", Until="17:00" },
            new Work { Id =2, Name="مینا", From="9:00", Until="16:00" },
            new Work { Id =3, Name="مهسا", From="8:30", Until="16:45" }
        };



        public ReportController(ILogger<ReportController> logger)
        {
            _logger = logger;

        }



        public IActionResult Report()
        {

            return View(workData);

        }

        public IActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Form([FromForm] Work work)
        {
            workData.Add(work);
            // validate data

            return RedirectToAction("Report");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
