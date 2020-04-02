using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LanguageProjectAsp.Models;
using Microsoft.AspNetCore.Mvc;


/** CSV controller
* @Author Rodrigo Eltz
*/
namespace LanguageProjectAsp.Controllers
{
    public class CsvController : BaseDataController
    {
        private static readonly string filePath = "D:\\13100262.csv";

        /// <summary>
        /// Method that retrieves all records from CSV
        /// </summary>
        /// <returns></returns>
        public override List<Record> readAll()
        {
            List<Record> recordsFromCsv = new List<Record>();
            try
            {
                using (StreamReader stream = new StreamReader(filePath))
                {
                    //clear list to prevent duplications in the UI
                    recordsFromCsv = new List<Record>();
                    stream.ReadLine();
                    while (!stream.EndOfStream)
                    {
                        recordsFromCsv.Add(Record.FromCsv(stream.ReadLine()));
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return recordsFromCsv;
        }

        /// <summary>
        /// Method that saves the entry to the csv file
        /// </summary>
        /// <param name="entry"></param>
        public override void addEntry(Record entry)
        {
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(entry.ToStringCSV());
            writer.Close();
        }

        /// <summary>
        /// Method that deletes the entry from file.
        /// Rodrigo Eltz 040913098
        /// </summary>
        public override void deleteEntry(int idToDelete)
        {
            string tempFile = "D:\\13100262.csv-copy.csv";
            StreamWriter sw = System.IO.File.CreateText(tempFile);

            using (StreamReader stream = new StreamReader(filePath))
            {
                while (!stream.EndOfStream)
                {
                    string csvRow = stream.ReadLine();
                    string csvRowId = csvRow.Split(',')[0].Replace("\"", "");

                    if (csvRowId.Equals(idToDelete.ToString()))
                    {
                        Console.WriteLine(csvRowId);
                    }
                    else
                    {
                        sw.WriteLine(csvRow);
                        sw.Flush();
                    }
                }
            }

            sw.Close();
            this.readAll();
            System.IO.File.Delete(filePath);
            System.IO.File.Move(tempFile, filePath);
        }


        /// <summary>
        /// Method that actualy updates the entry changes to the csv file
        /// </summary>
        /// <param name="editRecord"></param>
        public override void updateEntry(Record editRecord)
        {
            Debug.WriteLine("Inside method UpdateA3 with " + editRecord.ID);

            string tempFile = "D:\\13100262.csv-copy.csv";
            StreamWriter sw = System.IO.File.CreateText(tempFile);

            using (StreamReader stream = new StreamReader(filePath))
            {

                while (!stream.EndOfStream)
                {
                    string csvRow = stream.ReadLine();
                    string csvRowId = csvRow.Split(',')[0];

                    if (csvRowId.Equals(editRecord.ID.ToString()))
                    {

                        sw.WriteLine(editRecord.ToStringCSV());
                        sw.Flush();
                    }
                    else
                    {
                        sw.WriteLine(csvRow);
                        sw.Flush();
                    }
                }
            }
            sw.Close();
            System.IO.File.Delete(filePath);
            System.IO.File.Move(tempFile, filePath);
        }
    }
}