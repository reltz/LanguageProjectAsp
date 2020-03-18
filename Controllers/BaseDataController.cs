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
        public abstract List<Record> readAll();

        public abstract void deleteEntry(int id);

        public abstract void addEntry(Record entry);

        public abstract void updateEntry(Record entry);
    }
}