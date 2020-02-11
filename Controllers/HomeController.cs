using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LanguageProjectAsp.Models;
using System.IO;
/** Main application controller
 * @Author Rodrigo Eltz
 */

namespace LanguageProjectAsp.Controllers
{
    /// <summary>
    ///  Declares Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor with logger parameter
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Method to retrieve the ViewResult with the view Exercise1 page. 
        /// </summary>
        /// <returns>a ViewResult</returns>
        public ViewResult Exercise1()
        {
            // Names of all the columns
            var columns = new List<string> { "REF_DATE", "GEO", "DGUID", "Sex", "Age group",
                "Student response", "UOM", "UOM_ID", "SCALAR_FACTOR", "SCALAR_ID", "VECTOR", "COORDINATE",
                "VALUE", "STATUS", "SYMBOL", "TERMINATED", "DECIMALS"};

            return View(columns);
        }

        /// <summary>
        /// Method to retrieve the ViewResult for the Exercise 3 page
        /// </summary>
        /// <returns>a ViewResult</returns>
        public ViewResult Exercise3()
        {
            return View(readFromCsv()); ;
        }

        /// <summary>
        /// Method that reads the 5 first records from the CSV and creates a list of objects of type Record.
        /// </summary>
        /// <returns>A list of Records</returns>
        private List<Record> readFromCsv()
        {
            var filePath = "D:\\13100262.csv";
            List<Record> fiveRecords = new List<Record>();
            using (StreamReader stream = new StreamReader(filePath))
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (i == 0)
                    {
                        stream.ReadLine();
                    }
                    else
                    {
                        fiveRecords.Add(Record.FromCsv(stream.ReadLine()));
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine(fiveRecords);
            return fiveRecords;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
