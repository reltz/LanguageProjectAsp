using LanguageProjectAsp.DAL;
using LanguageProjectAsp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LanguageProjectAsp.Controllers
{
    public class DatabaseController : Controller
    {

        DatabaseContext db = new DatabaseContext();
        public List<Record> readAll()
        {
            List<Record> allRecords = new List<Record>();

            try
            {
                allRecords = db.Records.ToList<Record>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return allRecords;
        }
    }

}