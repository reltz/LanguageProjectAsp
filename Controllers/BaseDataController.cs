using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageProjectAsp.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Base data controller. Serves as a base class for all data manipulation controllers, such as database and csv controllers.
/// @Author Rodrigo Eltz
/// </summary>
namespace LanguageProjectAsp.Controllers
{
    public abstract class BaseDataController : Controller
    {
        /// <summary>
        /// Read operation
        /// </summary>
        /// <returns></returns>
        public abstract List<Record> readAll();

        /// <summary>
        /// Delete operation
        /// </summary>
        /// <param name="id"></param>
        public abstract void deleteEntry(int id);

        /// <summary>
        /// Create operation
        /// </summary>
        /// <param name="entry"></param>
        public abstract void addEntry(Record entry);

        /// <summary>
        /// Update operation
        /// </summary>
        /// <param name="entry"></param>
        public abstract void updateEntry(Record entry);
    }
}