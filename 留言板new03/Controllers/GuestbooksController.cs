using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcGuestbook.Models;

namespace MvcGuestbook.Controllers
{
    public class GuestbooksController : Controller
    {
        private MvcGuestbookContext db = new MvcGuestbookContext();

        // GET: Guestbooks
        //display comments
        public ActionResult Index()
        {
            return View(db.Guestbooks.ToList());
        }

        public ActionResult SearchIndex(string searchString)
        {
            var comments = from c in db.Guestbooks.ToList() select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                comments = comments.Where(c => c.comment.Contains(searchString));
            }
            return View(comments);
        }

        public ActionResult Write()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Write(Guestbook guestbook)
        {
            if (ModelState.IsValid)
            {
                var currentTime = DateTime.Now;
                db.Guestbooks.Add(guestbook);
                guestbook.CreateTime = currentTime;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guestbook);
        }

        // GET: Guestbooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guestbook guestbook = db.Guestbooks.Find(id);
            if (guestbook == null)
            {
                return HttpNotFound();
            }
            return View(guestbook);
        }

        // GET: Guestbooks/Create
        /*public ActionResult Create()
        {
            return View();
        }

        // POST: Guestbooks/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,comment")] Guestbook guestbook)
        {
            if (ModelState.IsValid)
            {
                db.Guestbooks.Add(guestbook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guestbook);
        }
        */
        // GET: Guestbooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guestbook guestbook = db.Guestbooks.Find(id);
            if (guestbook == null)
            {
                return HttpNotFound();
            }
            return View(guestbook);
        }

        // POST: Guestbooks/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,comment")] Guestbook guestbook)
        {
            if (ModelState.IsValid)
            {
                var currentTime = DateTime.Now;
                db.Entry(guestbook).State = EntityState.Modified;
                guestbook.CreateTime = currentTime;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guestbook);
        }

        // GET: Guestbooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guestbook guestbook = db.Guestbooks.Find(id);
            if (guestbook == null)
            {
                return HttpNotFound();
            }
            return View(guestbook);
        }

        // POST: Guestbooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guestbook guestbook = db.Guestbooks.Find(id);
            db.Guestbooks.Remove(guestbook);
            db.SaveChanges();
            return RedirectToAction("Index");
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
