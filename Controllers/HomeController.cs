using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static List<Record> recordsFromCsv = new List<Record>();

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
                    //clear list to prevent duplications in the UI
                    recordsFromCsv = new List<Record>();
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

        /// <summary>
        /// method to post the Add new Entry form - saves to file and adds to list object.
        /// </summary>
        /// <param name="entry"></param>
        [HttpPost]
        public void EntryTransform(Record entry)
        {
            int newId = recordsFromCsv[recordsFromCsv.Count - 1].ID + 1;
            entry.ID = newId;
            recordsFromCsv.Add(entry);
            this.SaveToCsv(entry);
         }

        /// <summary>
        /// Method that saves the entry to the csv file
        /// </summary>
        /// <param name="entry"></param>
        public void SaveToCsv(Record entry)
        {
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(entry.ToStringCSV());
            writer.Close();
        }

        [HttpPost]
         public void DeleteEntry()
        {
            string tempFile = "D:\\13100262.csv-copy.csv";
            StreamWriter sw = System.IO.File.CreateText(tempFile);

            string stringId = String.Format("{0}", Request.Form["idToDelete"]);

            int idToDelete = Int32.Parse(stringId);
            System.Diagnostics.Debug.WriteLine("called with id " + idToDelete);

            using (StreamReader stream = new StreamReader(filePath))
            {
                while (!stream.EndOfStream)
                {
                    string csvRow = stream.ReadLine();
                    string csvRowId = csvRow.Split(',')[0].Replace("\"", "");

                    if (csvRowId.Equals(idToDelete.ToString()))
                    {
                        Console.WriteLine(csvRowId);
                    }
                    else
                    {
                        sw.WriteLine(csvRow);
                        sw.Flush();
                    }
                }
            }

            sw.Close();
            this.readAllFromCsv();
            System.IO.File.Delete(filePath);
            System.IO.File.Move(tempFile, filePath);
        }

        /// <summary>
        /// Method that navigates to the Update view and passes a Record object to it
        /// </summary>
        /// <returns>View</returns>
        [HttpPost]
        public ViewResult UpdateRecord()
        {
            string stringId = String.Format("{0}", Request.Form["idToUpdate"]);
            int idToUpdate = Int32.Parse(stringId);
            
            Debug.WriteLine("id received was " + idToUpdate);
            Record editRecord = new Record();
            
            editRecord = recordsFromCsv.Find(obj => obj.ID == idToUpdate);
            Debug.WriteLine("found in List was " + editRecord);
            
            return View("UpdateA3", editRecord);
        }

        /// <summary>
        /// Method that actualy updates the entry changes to the csv file
        /// </summary>
        /// <param name="entry"></param>
        [HttpPost]
        public void UpdateA3(Record editRecord)
        {
            Debug.WriteLine("Inside method UpdateA3 with "+ editRecord.ID);

            string tempFile = "D:\\13100262.csv-copy.csv";
            StreamWriter sw = System.IO.File.CreateText(tempFile);

            using (StreamReader stream = new StreamReader(filePath))
            {

                while (!stream.EndOfStream)
                {
                    string csvRow = stream.ReadLine();
                    string csvRowId = csvRow.Split(',')[0];

                    if (csvRowId.Equals(editRecord.ID.ToString()))
                    {

                        sw.WriteLine(editRecord.ToStringCSV());
                        sw.Flush();
                    }
                    else
                    {
                        sw.WriteLine(csvRow);
                        sw.Flush();
                    }
                }
            }
            sw.Close();
            System.IO.File.Delete(filePath);
            System.IO.File.Move(tempFile, filePath);
          }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
