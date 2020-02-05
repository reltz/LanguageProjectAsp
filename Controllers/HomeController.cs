using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LanguageProjectAsp.Models;
using System.IO;

namespace LanguageProjectAsp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        /** Method to retrieve the ViewResult with the view Exercise1. 
        @Rodrigo Eltz
    */
        public ViewResult Exercise1()
        {
            // Names of all the columns
            var columns = new List<string> { "REF_DATE", "GEO", "DGUID", "Sex", "Age group",
                "Student response", "UOM", "UOM_ID", "SCALAR_FACTOR", "SCALAR_ID", "VECTOR", "COORDINATE",
                "VALUE", "STATUS", "SYMBOL", "TERMINATED", "DECIMALS"};

            return View(columns);
        }

        //Method to retrieve the ViewResult for the Exercise 3
        //@Rodrigo Eltz
        public ViewResult Exercise3()
        {
            return View(readFromCsv()); ;
        }

        /// Method that reads the 5 first records from the CSV and creates a list of objects of type Record.
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
