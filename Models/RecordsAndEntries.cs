using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageProjectAsp.Models
{
    /// <summary>
    /// Class that returns the model to the Assignment3 view
    /// </summary>
    public class RecordsAndEntries
    {
        public List<Record> records { get; set; }

        public Record entry { get; set; }

        public int idToDelete { get; set; }

        public int idToUpdate { get; set; }
    }
}
