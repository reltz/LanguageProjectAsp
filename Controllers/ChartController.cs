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

        public ViewResult AgeGroupChart()
        {
            return View("~/Views/Home/AgeGroupChart.cshtml", AgeGroupChartData());
        }


        public ViewResult GenderChart()
        {
            return View("~/Views/Home/GenderChart.cshtml", GenderChartData());
        }

        public ViewResult StudentResponseChart()
        {
            return View("~/Views/Home/StudentResponseChart.cshtml", StudentResponseData());
        }

        public ViewResult MaleStudentResponseChart()
        {
            return View("~/Views/Home/MaleStudentResponse.cshtml", MaleStudentResponseData());
        }
    
        public ViewResult ElevenAgeGroupResponseChart()
        {
            return View("~/Views/Home/EleventAgeGroupResponseChart.cshtml", ElevenAgeGroupResponseData());
        }

        public ViewResult Females15yoChart()
        {
            return View("~/Views/Home/Females15yoResponseChart.cshtml", Females15yoResponseData());
        }

        // Data fetching methods


        public string AgeGroupChartData()
        {
            var json = JsonSerializer.Serialize(db.countAgeGroup());
            return json;
        }

        public string GenderChartData()
        {
            var json = JsonSerializer.Serialize(db.countGender());
            return json;
        }

        public string StudentResponseData()
        {
            var json = JsonSerializer.Serialize(db.countStudentResponse());
            return json;
        }

        public string MaleStudentResponseData()
        {
            var json = JsonSerializer.Serialize(db.studentResponseMale());
            return json;
        }

        public string ElevenAgeGroupResponseData()
        {
            var json = JsonSerializer.Serialize(db.elevenStudentResponse());
            return json;
        }

        public string Females15yoResponseData()
        {
            var json = JsonSerializer.Serialize(db.females15yoResponse());
            return json;
        }
    }
}