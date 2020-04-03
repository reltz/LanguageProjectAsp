using LanguageProjectAsp.DAL;
using LanguageProjectAsp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
/** Database controller
* @Author Rodrigo Eltz
*/

namespace LanguageProjectAsp.Controllers
{
    public class DatabaseController : BaseDataController
    {
        /// <summary>
        /// Instance of DatabaseContext
        /// </summary>
        DatabaseContext db = new DatabaseContext();
        private static readonly string countAgeGroupQuery = "SELECT COUNT(AGE_GROUP) FROM RECORDS GROUP BY AGE_GROUP";
        private static readonly string distinctAgeGroupQuery = "SELECT DISTINCT AGE_GROUP FROM RECORDS";

        /// <summary>
        /// Retrieves all entries from the database, table Records.
        /// </summary>
        /// <returns></returns>
        public override List<Record> readAll()
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

        /// <summary>
        /// Deletes record from database, table Records, from ID passed
        /// </summary>
        /// <param name="id"></param>
        public override void deleteEntry(int id)
        {
            //Record entryToDelete = db.Records.Where(r => r.ID == id).FirstOrDefault();
            try
            {
                Record entryToDelete = db.Records.Find(id);
                db.Records.Remove(entryToDelete);
                db.SaveChanges();
            } catch ( Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Adds new entry to the database, table Records.
        /// </summary>
        /// <param name="entry"></param>
        public override void addEntry(Record entry)
        {
            Debug.WriteLine(entry);
            try
            {
                db.Records.Add(entry);
                db.SaveChanges();
            } catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Updates entry in the database, in the table Records.
        /// </summary>
        /// <param name="entry"></param>
        public override void updateEntry(Record entry)
        {
            try
            {
                db.Records.Update(entry);
                db.SaveChanges();
            } catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public Array countAgeGroup()
        {
            List<string> distinctAgeGroups = new List<string>();
           // List<int> countAgeGroups = new List<int>();

           distinctAgeGroups =  db.Records.Select(record => record.AgeGroup).Distinct().ToList<string>();

           Array countAgeGroups = db.Records.GroupBy(record => record.AgeGroup).Select(blob => new { blob.Key,  count = blob.Count() }).ToArray();

           return countAgeGroups;
        }
    }
}