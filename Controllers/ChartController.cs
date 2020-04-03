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
            return View("~/Views/Home/Charts.cshtml", AgeGroupChartData());
        }


        public string AgeGroupChartData()
        {
            var json = JsonSerializer.Serialize(db.countAgeGroup());
            System.Diagnostics.Debug.WriteLine(json);
            return json;
        }
    }
}