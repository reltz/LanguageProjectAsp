using System.Collections.Generic;
using LanguageProjectAsp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.UI.DataVisualization.Charting;
using System.Text.Json;

namespace LanguageProjectAsp.Controllers
{
    public class ChartController : Controller
    {
        List<Record> allRecords;
        DatabaseController db;

        public ChartController()
        {
            db = new DatabaseController();
            allRecords = db.readAll();
        }


        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method that returns the view for the charts
        /// </summary>
        /// <returns></returns>
        public ViewResult Charts()
        {
            return View("~/Views/Home/Charts.cshtml");
        }

        /// <summary>
        /// Method that navigates to the AgeGroupChart page View and passes the data from the database to the View
        /// </summary>
        /// <returns></returns>
        public ViewResult AgeGroupChart()
        {
            return View("~/Views/Home/AgeGroupChart.cshtml", AgeGroupChartData());
        }

        /// <summary>
        /// Method that navigates to the GenderChart page View and passes the data from the database to the View
        /// </summary>
        /// <returns></returns>
        public ViewResult GenderChart()
        {
            return View("~/Views/Home/GenderChart.cshtml", GenderChartData());
        }

        /// <summary>
        /// Method that navigates to the StudentResponseChart page View and passes the data from the database to the View
        /// </summary>
        /// <returns></returns>
        public ViewResult StudentResponseChart()
        {
            return View("~/Views/Home/StudentResponseChart.cshtml", StudentResponseData());
        }


        /// <summary>
        /// Method that navigates to the MaleStudentResponseChart page View and passes the data from the database to the View
        /// </summary>
        /// <returns></returns>
        public ViewResult MaleStudentResponseChart()
        {
            return View("~/Views/Home/MaleStudentResponse.cshtml", MaleStudentResponseData());
        }


        /// <summary>
        /// Method that navigates to the Female15yoChart page View and passes the data from the database to the View
        /// </summary>
        /// <returns></returns>
        public ViewResult ElevenAgeGroupResponseChart()
        {
            return View("~/Views/Home/EleventAgeGroupResponseChart.cshtml", ElevenAgeGroupResponseData());
        }

        /// <summary>
        /// Method that navigates to the Female15yoChart page View and passes the data from the database to the View
        /// </summary>
        /// <returns></returns>
        public ViewResult Females15yoChart()
        {
            return View("~/Views/Home/Females15yoResponseChart.cshtml", Females15yoResponseData());
        }

        // Data fetching methods

        /// <summary>
        /// Method that calls the query method from the database controller
        /// and converts the data array into a JSON object
        /// </summary>
        /// <returns>json object</returns>
        public string AgeGroupChartData()
        {
            var json = JsonSerializer.Serialize(db.countAgeGroup());
            return json;
        }


        /// <summary>
        /// Method that calls the query method from the database controller
        /// and converts the data array into a JSON object
        /// </summary>
        /// <returns>json object</returns>
        public string GenderChartData()
        {
            var json = JsonSerializer.Serialize(db.countGender());
            return json;
        }

        /// <summary>
        /// Method that calls the query method from the database controller
        /// and converts the data array into a JSON object
        /// </summary>
        /// <returns>json object</returns>
        public string StudentResponseData()
        {
            var json = JsonSerializer.Serialize(db.countStudentResponse());
            return json;
        }

        /// <summary>
        /// Method that calls the query method from the database controller
        /// and converts the data array into a JSON object
        /// </summary>
        /// <returns>json object</returns>
        public string MaleStudentResponseData()
        {
            var json = JsonSerializer.Serialize(db.studentResponseMale());
            return json;
        }

        /// <summary>
        /// Method that calls the query method from the database controller
        /// and converts the data array into a JSON object
        /// </summary>
        /// <returns> json object</returns>
        public string ElevenAgeGroupResponseData()
        {
            var json = JsonSerializer.Serialize(db.elevenStudentResponse());
            return json;
        }

        /// <summary>
        /// Method that calls the query method from the database controller
        /// and converts the data array into a JSON object
        /// </summary>
        /// <returns></returns>
        public string Females15yoResponseData()
        {
            var json = JsonSerializer.Serialize(db.females15yoResponse());
            return json;
        }
    }
}