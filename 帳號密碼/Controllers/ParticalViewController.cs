using DataBasePrac.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace DataBasePrac.Controllers
{
    public class ParticalViewController : Controller
    {
        private DB01 db = new DB01();
        // GET: ParticalView
        public ActionResult Index()
        {
            return View();
        }
        // GET: Member01/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member01 member01 = db.Member01.Find(id);
            if (member01 == null)
            {
                return HttpNotFound();
            }
            return View(member01);
        }
        public ActionResult MainMenu()
        {
            return PartialView();
        }
    }
}