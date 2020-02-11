using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
 * This is a model for the object Record (i.e. entry record in the csv table)
 * @Author: Rodrigo Eltz
 */
namespace LanguageProjectAsp.Models
{
    public class Record
    {
        private int refDate;
        private string geo;
        private string dGuid;
        private string sex;
        private string ageGroup;
        private string studentResponse;
        private string uom;
        private int uomId;
        private string scalarFactor;
        private int scalarId;
        private string vector;
        private string coordinate;
        private string value;
        private string status;
        private string symbol;
        private string terminated;
        private double decimals;

        /// Just passed to the constructors the values that are different. In the event of the dataset changing, this has to be adapted to construct based on new variables
        public Record(string sex, string ageGroup, string studentResponse, string vector, string coordinate, string value)
        {
            this.refDate = 2002;
            this.geo = "Canada";
            this.dGuid = "";
            this.sex = sex;
            this.ageGroup = ageGroup;
            this.studentResponse = studentResponse;
            this.uom = "Percent";
            this.uomId = 239;
            this.scalarFactor = "units";
            this.scalarId = 0;
            this.vector = vector;
            this.coordinate = coordinate;
            this.value = value;
            this.status = "";
            this.symbol = "";
            this.terminated = "";
            this.decimals = 0d;
        }

        public int RefDate { get => refDate; set => refDate = value; }
        public string Geo { get => geo; set => geo = value; }
        public string DGuid { get => dGuid; set => dGuid = value; }
        public string Sex { get => sex; set => sex = value; }
        public string AgeGroup { get => ageGroup; set => ageGroup = value; }
        public string StudentResponse { get => studentResponse; set => studentResponse = value; }
        public string Uom { get => uom; set => uom = value; }
        public int UomId { get => uomId; set => uomId = value; }
        public string ScalarFactor { get => scalarFactor; set => scalarFactor = value; }
        public int ScalarId { get => scalarId; set => scalarId = value; }
        public string Vector { get => vector; set => vector = value; }
        public string Coordinate { get => coordinate; set => coordinate = value; }
        public string Value { get => value; set => this.value = value; }
        public string Status { get => status; set => status = value; }
        public string Symbol { get => symbol; set => symbol = value; }
        public string Terminated { get => terminated; set => terminated = value; }
        public double Decimals { get => decimals; set => decimals = value; }

        /// This method parses the csv and creates an object of type Record for each line
        public static Record FromCsv(string line)
        {
            var split = line.Split(',');
            Record record = new Record(Convert.ToString(split[3]),
                                       Convert.ToString(split[4]),
                                       Convert.ToString(split[5]),
                                       Convert.ToString(split[10]),
                                       Convert.ToString(split[11]),
                                       Convert.ToString(split[12]));
            return record;
        }

        //public override string ToString()
        //{
        //    return "Ref_Date: " + RefDate + " GEO: " + Geo + " Sex: " + Sex + " Age Group: " + AgeGroup
        //        + " Student Response: " + StudentResponse + " Vector: " + Vector + " Coordinate:  " + Coordinate
        //        + " Value: " + Value;
        //}
    }
}
