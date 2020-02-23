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
        static readonly string filePath ="D:\\13100262.csv";
        public List<Record> recordsFromCsv = new List<Record>();

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

        public ViewResult Assignment3()
        {
            RecordsAndEntries allRecords = new RecordsAndEntries();
            allRecords.records = readAllFromCsv();
           // allRecords.entry = new Record(0, 0, "", "", "", "", "", 0, "", "", "", 0);
            return View(allRecords);
        }

        /// <summary>
        /// Method that reads the 5 first records from the CSV and creates a list of objects of type Record.
        /// </summary>
        /// <returns>A list of Records</returns>
        private List<Record> readFromCsv()
        {
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

        /// <summary>
        /// Read all records from CSV file
        /// </summary>
        /// <returns></returns>
       private List<Record> readAllFromCsv()
        {
            try
            {
                using (StreamReader stream = new StreamReader(filePath))
                {
                    stream.ReadLine();
                    while (!stream.EndOfStream)
                    {
                        recordsFromCsv.Add(Record.FromCsv(stream.ReadLine()));
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return recordsFromCsv;
        }

        [HttpPost]
        public void EntryTransform(Record entry)
        {
            this.recordsFromCsv.Add(entry);
            this.SaveToCsv(entry);
          }

        public void SaveToCsv(Record entry)
        {
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(entry.ToStringCSV());
            writer.Close();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
