using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LanguageProjectAsp.Controllers
{
    public class FormController : Controller
    {
        public int ID { get; set; }
        public int RefDate { get; set; }
        public string Geo { get; set; }
        public string Sex { get; set; }
        public string AgeGroup { get; set; }
        public string StudentResponse { get; set; }
        public string Uom { get; set; }
        public int UomId { get; set; }
        public string ScalarFactor { get; set; }
        public string Vector { get; set; }
        public string Coordinate { get; set; }
        public int Value { get; set; }
    }
}