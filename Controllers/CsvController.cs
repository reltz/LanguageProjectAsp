﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LanguageProjectAsp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanguageProjectAsp.Controllers
{
    public class CsvController : Controller
    {
        private static readonly string filePath = "D:\\13100262.csv";
        
        public List<Record> readAllFromCsv()
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
        public void SaveToCsv(Record entry)
        {
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine(entry.ToStringCSV());
            writer.Close();
        }

        /// <summary>
        /// Method that deletes the entry from file.
        /// Rodrigo Eltz 040913098
        /// </summary>
        public void DeleteEntry(int idToDelete)
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
            this.readAllFromCsv();
            System.IO.File.Delete(filePath);
            System.IO.File.Move(tempFile, filePath);
        }


        /// <summary>
        /// Method that actualy updates the entry changes to the csv file
        /// </summary>
        /// <param name="entry"></param>
         public void UpdateA3(Record editRecord)
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