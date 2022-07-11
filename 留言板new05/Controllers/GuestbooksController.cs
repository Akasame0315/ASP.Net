using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewGuestbook.Models;

namespace NewGuestbook.Controllers
{
    public class GuestbooksController : Controller
    {
        private NewGuestbookContext db = new NewGuestbookContext();

        // GET: Guestbooks
        public ActionResult Index()
        {
            return View(db.Guestbooks.ToList());
        }
        public ActionResult EditIndex(string searchString)  //編輯留言
        {
            var comments = from c in db.Guestbooks.ToList() select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                comments = comments.Where(s => s.Comment.Contains(searchString));
            }

            return View(comments);
        }

        public ActionResult SearchIndex(string searchString)    //搜尋留言
        {
            var comments = from c in db.Guestbooks.ToList() select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                comments = comments.Where(s => s.Comment.Contains(searchString));
            }

            return View(comments);
        }
        // GET: Guestbooks/Details/5
        public ActionResult Details(int? id)    //留言詳細資料
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

        public ActionResult Write() //留言
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Write([Bind(Include = "Id,Label,Name,Email,Comment,EditTime")] Guestbook guestbook)
        {   //留言
            if (ModelState.IsValid)
            {
                var currTime = DateTime.Now;    //現在時間
                db.Guestbooks.Add(guestbook);
                guestbook.EditTime = currTime;  //寫入現在時間
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guestbook);
        }

        // GET: Guestbooks/Edit/5
        public ActionResult Edit(int? id)   //編輯留言
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
        public ActionResult Edit([Bind(Include = "Id,Label,Name,Email,Comment,EditTime")] Guestbook guestbook)
        {   //編輯留言
            if (ModelState.IsValid)
            {
                var currTime = DateTime.Now;
                db.Entry(guestbook).State = EntityState.Modified;
                guestbook.EditTime = currTime;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guestbook);
        }

        // GET: Guestbooks/Delete/5
        public ActionResult Delete(int? id) //刪除留言
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

        public ActionResult DeleteComments(int? id)
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
