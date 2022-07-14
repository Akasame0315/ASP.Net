using DataBasePrac.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace DataBasePrac.Controllers
{
    public class Member01Controller : Controller
    {
        private DB01 db = new DB01();

        // GET: Member01
        public ActionResult Index()
        {
            return View(db.Member01.ToList());
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
        #region public ActionResult ParticalDetail(int? id)
        public ActionResult ParticalDetail(int? id)
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
        #endregion

        // GET: Member01/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Member01/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,UserEmail,UserPwd,EditTime")] Member01 member01, string UserEmail, string UserName)
        {
            var acc = from c in db.Member01.ToList() select c;
            var name = from c in db.Member01.ToList() select c;
            if (!String.IsNullOrEmpty(UserEmail) | !String.IsNullOrEmpty(UserName))
            {
                acc = acc.Where(s => s.UserEmail.Equals(UserEmail));
                name = name.Where(s => s.UserName.Equals(UserName));
                if(acc.Count() > 0 | name.Count() > 0)
                {
                    ModelState.AddModelError("", "信箱或名稱已被使用030! :-P");
                }
                else
                {
                    if (ModelState.IsValid)
                    {

                        db.Member01.Add(member01);
                        member01.EditTime = DateTime.Now;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        //Get: Member01/Search/
        public ActionResult Search(string searchString)    //搜尋留言
        {
            var name = from c in db.Member01.ToList() select c;
            if (searchString != null)
            {
                name = name.Where(s => s.UserName.Contains(searchString));
            }
            return View(name);

        }

        // GET: Member01/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Member01/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,UserEmail,UserPwd,EditTime")] Member01 member01)
        {
            var initTime = member01.EditTime;
            if (ModelState.IsValid)
            {
                db.Entry(member01).State = EntityState.Modified;
                member01.EditTime = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member01);
        }

        // GET: Member01/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Member01/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member01 member01 = db.Member01.Find(id);
            db.Member01.Remove(member01);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MainMenu()
        {
            return PartialView();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserEmail, string UserPwd)
        {
            var acc = from c in db.Member01.ToList() select c;
            var pwd = from c in db.Member01.ToList() select c;

            if (!String.IsNullOrEmpty(UserEmail))
            {
                acc = acc.Where(s => s.UserEmail.Equals(UserEmail));
                if(acc.Count() > 0)
                {
                    pwd = pwd.Where(s => s.UserPwd.Equals(UserPwd));
                    if(pwd.Count() > 0 )
                    {
                        if (pwd.First().Equals((acc.First())))
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            ViewBag.ErrorMessage = "帳號密碼錯誤! OAO";
            //ModelState.AddModelError("", "請輸入帳號密碼! OAO");
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
