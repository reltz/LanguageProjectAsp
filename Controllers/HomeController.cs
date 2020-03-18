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
        //static readonly string filePath ="D:\\13100262.csv";
        public static List<Record> allRecords = new List<Record>();

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
        /// Method that returns the view for Assignment 4 with DB connection
        /// </summary>
        /// <returns></returns>
        public ViewResult Assign4DB()
        {
            //this.readAllFromCsv();
            this.readAllFromDB();
            RecordsAndEntries recordsAndEntries = new RecordsAndEntries();
            recordsAndEntries.records = allRecords;
            return View(recordsAndEntries);
        }

        /// <summary>
        /// Method that returns the view for Assignment 4 with CSV connection
        /// </summary>
        /// <returns></returns>
        public ViewResult Assign4CSV()
        {
            this.readAllFromCsv();
            RecordsAndEntries recordsAndEntries = new RecordsAndEntries();
            recordsAndEntries.records = allRecords;
            return View(recordsAndEntries);
        }

        /// <summary>
        /// Read all records from CSV file
        /// </summary>
        /// <returns></returns>
        /// Rodrigo Eltz 040913098
        private void readAllFromCsv()
        {
            CsvController csvHandler = new CsvController();
            try
            {
                allRecords = csvHandler.readAll();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private void readAllFromDB()
        {
            DatabaseController controller = new DatabaseController();
            try
            {
                allRecords = controller.readAll();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// method to post the Add new Entry form - saves to file and adds to list object.
        /// Rodrigo Eltz - 040913098
        /// </summary>
        /// <param name="entry"></param>
        [HttpPost]
        public void addRecordDB(Record entry)
        {
            int newId = allRecords[allRecords.Count - 1].ID + 1;
            entry.ID = newId;
            allRecords.Add(entry);
            this.saveToDb(entry);
        }


        /// <summary>
        /// method to post the Add new Entry form - saves to file and adds to list object.
        /// Rodrigo Eltz - 040913098
        /// </summary>
        /// <param name="entry"></param>
        [HttpPost]
        public void addRecordCSV(Record entry)
        {
            int newId = allRecords[allRecords.Count - 1].ID + 1;
            entry.ID = newId;
            allRecords.Add(entry);
            this.SaveToCsv(entry);
        }


        ///// <summary>
        ///// Method that calls the CsvHelper controller to save an entry
        ///// </summary>
        ///// <param name="entry"></param>
        private void SaveToCsv(Record entry)
        {
            CsvController csvHelper = new CsvController();
            try
            {
                csvHelper.addEntry(entry);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Method that instantiates the DatabaseController and call the addEntry method 
        /// Rodrigo Eltz
        /// </summary>
        /// <param name="entry"></param>
        private void saveToDb(Record entry)
        {
            DatabaseController db = new DatabaseController();

            try
            {
                db.addEntry(entry);
            } catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Method that receives the ID from the UI view and calls the CsvHelper.
        /// Rodrigo Eltz 040913098
        /// </summary>
        [HttpPost]
         public void DeleteEntryDB()
        {
            string stringId = String.Format("{0}", Request.Form["idToDelete"]);
            int idToDelete = Int32.Parse(stringId);
            System.Diagnostics.Debug.WriteLine("called with id " + idToDelete);
            //CsvController csvHelper = new CsvController();
            DatabaseController dbController = new DatabaseController();

            try
            {
                dbController.deleteEntry(idToDelete);
                //csvHelper.DeleteEntry(idToDelete);
            } catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Method that receives the ID from the UI view and calls the CsvHelper.
        /// Rodrigo Eltz 040913098
        /// </summary>
        [HttpPost]
        public void DeleteEntryCSV()
        {
            string stringId = String.Format("{0}", Request.Form["idToDelete"]);
            int idToDelete = Int32.Parse(stringId);
            System.Diagnostics.Debug.WriteLine("called with id " + idToDelete);
            CsvController csvHelper = new CsvController();

            try
            {
                csvHelper.deleteEntry(idToDelete);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Method that navigates to the Update view and passes a Record object to it
        /// </summary>
        /// <returns>View</returns>
        [HttpPost]
        public ViewResult UpdateRecordDBPage()
        {
            string stringId = String.Format("{0}", Request.Form["idToUpdate"]);
            int idToUpdate = Int32.Parse(stringId);
            
            Debug.WriteLine("id received was " + idToUpdate);
            Record editRecord = new Record();
            
            editRecord = allRecords.Find(obj => obj.ID == idToUpdate);
            Debug.WriteLine("found in List was " + editRecord);
            
            return View("UpdateA3", editRecord);
        }


        /// <summary>
        /// Method that navigates to the Update view and passes a Record object to it
        /// </summary>
        /// <returns>View</returns>
        [HttpPost]
        public ViewResult UpdateRecordCSVPage()
        {
            string stringId = String.Format("{0}", Request.Form["idToUpdate"]);
            int idToUpdate = Int32.Parse(stringId);

            Debug.WriteLine("id received was " + idToUpdate);
            Record editRecord = new Record();

            editRecord = allRecords.Find(obj => obj.ID == idToUpdate);
            Debug.WriteLine("found in List was " + editRecord);

            return View("UpdateA3CSV", editRecord);
        }

        /// <summary>
        /// Method that actualy updates the entry changes to the csv file
        /// </summary>
        /// <param name="editRecord"></param>
        [HttpPost]
        public void UpdateRecordCSV(Record editRecord)
        {
            Debug.WriteLine("Inside method UpdateA3 with "+ editRecord.ID);
            CsvController csvHelper = new CsvController();

            try
            {
                csvHelper.updateEntry(editRecord);
            } catch(Exception e)
            {
                Debug.WriteLine(e);
            }
         }

        /// <summary>
        /// Method that actualy updates the entry changes to the database
        /// </summary>
        /// <param name="editRecord"></param>
        [HttpPost]
        public void UpdateRecordDB(Record editRecord)
        {
            Debug.WriteLine("Inside method UpdateA3 with " + editRecord.ID);
            DatabaseController dbController = new DatabaseController();

            try
            {
                dbController.updateEntry(editRecord);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
