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

        public void deleteEntry(int id)
        {
            //Record entryToDelete = db.Records.Where(r => r.ID == id).FirstOrDefault();
            Record entryToDelete = db.Records.Find(id);
            try
            {
                db.Records.Remove(entryToDelete);
            } catch ( Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void addEntry(Record entry)
        {
            try
            {
                db.Records.Add(entry);
            } catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void updateEntry(Record entry)
        {
            try
            {
                db.Records.Update(entry);
            } catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}