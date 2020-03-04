using LanguageProjectAsp.DAL;
using LanguageProjectAsp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
/** Database controller
* @Author Rodrigo Eltz
*/

namespace LanguageProjectAsp.Controllers
{
    public class DatabaseController : Controller
    {
        /// <summary>
        /// Instance of DatabaseContext
        /// </summary>
        DatabaseContext db = new DatabaseContext();
        
        /// <summary>
        /// Retrieves all entries from the database, table Records.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes record from database, table Records, from ID passed
        /// </summary>
        /// <param name="id"></param>
        public void deleteEntry(int id)
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
        public void addEntry(Record entry)
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
        public void updateEntry(Record entry)
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
    }
}