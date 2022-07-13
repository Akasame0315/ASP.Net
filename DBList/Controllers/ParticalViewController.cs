using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBasePrac.Models;
using DataBasePrac.ViewModels;

namespace DataBasePrac.Controllers
{
    public class ParticalViewController : Controller
    {
        // GET: ParticalView
        public ActionResult Index()
        {
            return View();
        }
    }
}