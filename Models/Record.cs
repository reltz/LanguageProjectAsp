using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

/**
 * This is a model for the object Record (i.e. entry record in the csv table)
 * @Author: Rodrigo Eltz
 */
namespace LanguageProjectAsp.Models
{
    /// <summary>
    /// Class that defines a record entry object
    /// </summary>
    public class Record
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
        
        /// <summary>
        /// Constructor for Record class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="refDate"></param>
        /// <param name="geo"></param>
        /// <param name="sex"></param>
        /// <param name="ageGroup"></param>
        /// <param name="studentResponse"></param>
        /// <param name="uom"></param>
        /// <param name="uomId"></param>
        /// <param name="scalarFactor"></param>
        /// <param name="vector"></param>
        /// <param name="coordinate"></param>
        /// <param name="value"></param>
        public Record(int id, int refDate, string geo, string sex, string ageGroup, string studentResponse, string uom, int uomId, string scalarFactor, string vector, string coordinate, int value)
        {
            ID = id;
            RefDate = refDate;
            Geo = geo;
            Sex = sex;
            AgeGroup = ageGroup;
            StudentResponse = studentResponse;
            Uom = uom;
            UomId = uomId;
            ScalarFactor = scalarFactor;
            Vector = vector;
            Coordinate = coordinate;
            Value = value;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Record()
        {
            ID = -1;
            RefDate = -1;
            Geo = "";
            Sex = "";
            AgeGroup = "";
            StudentResponse = "";
            Uom = "";
            UomId = -1;
            ScalarFactor = "";
            Vector = "";
            Coordinate = "";
            Value = -1;
        }

        
        /// <summary>
        /// This method parses the csv and creates an object of type Record for each line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static Record FromCsv(string line)
        {
            var split = line.Split(',');

            //System.Diagnostics.Debug.WriteLine(split[0], split[0].GetType().FullName);

            int ID = Int32.Parse(split[0].Replace("\"", ""));
            int refDate = Int32.Parse(split[1].Replace("\"", ""));
            string geo = split[2].Replace("\"", "");
            string sex = split[3].Replace("\"", "");
            var ageGroup = split[4].Replace("\"", "");
            var studentResponse = split[5].Replace("\"", "");
            var uom = split[6].Replace("\"", "");
            int uomId = Int32.Parse(split[7].Replace("\"", ""));
            var scalarFactor = split[8].Replace("\"", "");
            var vector = split[9].Replace("\"", "");
            var coordinate = split[10].Replace("\"", "");
            int value = Int32.Parse(split[11].Replace("\"", ""));

            Record record = new Record(ID, refDate, geo, sex, ageGroup, studentResponse, uom, uomId, scalarFactor, vector, coordinate, value);

            return record;
        }

        /// <summary>
        /// Parse the object to CSV format
        /// </summary>
        /// <returns></returns>
        public string ToStringCSV()
        {
            return ID + ", " +
                RefDate + ", " +
                Geo + ", " +
                Sex + ", " +
                AgeGroup + ", " +
                StudentResponse + ", " +
                Uom + ", " +
                UomId + ", " +
                ScalarFactor + ", " +
                Vector + ", " +
                Coordinate + ", " +
                Value;
        }
    }
}

