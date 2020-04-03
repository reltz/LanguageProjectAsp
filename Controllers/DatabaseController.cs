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
            }
            catch (Exception e)
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
            }
            catch (Exception e)
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
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Method that retrieves an array with all age groups and the count for each
        /// </summary>
        /// <returns></returns>
        public Array countAgeGroup()
        {
            Array countAgeGroups = db.Records.GroupBy(record => record.AgeGroup).Select(blob => new { blob.Key, count = blob.Count() }).ToArray();

            return countAgeGroups;
        }

        /// <summary>
        /// Query that retrieves from the database the data of gender of students
        /// </summary>
        /// <returns></returns>
        public Array countGender()
        {
            Array countGender = db.Records.GroupBy(record => record.Sex).Select(blob => new { Gender = blob.Key, count = blob.Count() }).ToArray();

            return countGender;
        }

        /// <summary>
        /// Query that retrieves from the database the data with all Student Response
        /// </summary>
        /// <returns></returns>
        public Array countStudentResponse()
        {
            Array countStudentResponse = db.Records.GroupBy(record => record.StudentResponse).Select(blob => new { Key = blob.Key, count = blob.Count() }).ToArray();

            return countStudentResponse;
        }

        /// <summary>
        /// Query that retrieves from the database the data of Student Response only by Males
        /// </summary>
        /// <returns></returns>
        public Array studentResponseMale()
        {
            Array countMaleResponse = db.Records
                .Where(r => r.Sex.Equals("Males"))
                .GroupBy(record => record.StudentResponse)
                .Select(blob => new { Key = blob.Key, count = blob.Count() })
                .ToArray();

            return countMaleResponse;
        }

        /// <summary>
        /// Query that retrieves an array with the data of Student Response only from the 11 years old group
        /// </summary>
        /// <returns></returns>
        public Array elevenStudentResponse()
        {
            Array elevenAgeGroupResponse = db.Records
                .Where(r => r.AgeGroup.Equals("11 Years"))
                .GroupBy(record => record.StudentResponse)
                .Select(blob => new { Key = blob.Key, count = blob.Count() })
                .ToArray();

            return elevenAgeGroupResponse;
        }

        /// <summary>
        /// Query that retrieves an array with the data of Student Response only from Females of 15 years old
        /// </summary>
        /// <returns></returns>
        public Array females15yoResponse()
        {
            Array females15yoResponse = db.Records
                .Where(r => r.Sex.Equals("Females") && r.AgeGroup.Equals("15 years"))
                .GroupBy(r => r.StudentResponse)
                .Select(blob => new { Key = blob.Key, count = blob.Count() })
                .ToArray();

            return females15yoResponse;
        }
    }
}