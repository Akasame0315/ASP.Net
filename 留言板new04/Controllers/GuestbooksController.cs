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

        public ActionResult SearchIndex(string searchString)
        {
            var comments = from c in db.Guestbooks.ToList() select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                comments = comments.Where(s => s.Comment.Contains(searchString));
            }

            return View(comments);
        }

        // GET: Guestbooks/SelectLabel
        public ActionResult SelectLabel()
        {
            List<SelectListItem> labels = new List<SelectListItem>
            {
                new SelectListItem { Text = "->", Value = "0" },
                new SelectListItem { Text = "like", Value = "1", Selected = true },
                new SelectListItem { Text = "dislike", Value = "2" }
            };
            ViewBag.LabelType = labels;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectLabel([Bind(Include = "Id,Label,Name,Email,Comment,EditTime")] Guestbook guestbook)
        {
            List<SelectListItem> labels = new List<SelectListItem>
            {
                new SelectListItem { Text = "->", Value = "0" },
                new SelectListItem { Text = "like", Value = "1", Selected = true },
                new SelectListItem { Text = "dislike", Value = "2" }
            };
            ViewBag.LabelType = labels;

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

        public ActionResult Write()
        {
            List<SelectListItem> labels = new List<SelectListItem>
            {
                new SelectListItem { Text = "->", Value = "0" },
                new SelectListItem { Text = "like", Value = "1", Selected = true },
                new SelectListItem { Text = "dislike", Value = "2" }
            };
            ViewBag.LabelType = labels;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Write([Bind(Include = "Id,Label,Name,Email,Comment,EditTime")] Guestbook guestbook)
        {
            List<SelectListItem> labels = new List<SelectListItem>();
            labels.Add(new SelectListItem { Text = "->", Value = "0" });
            labels.Add(new SelectListItem { Text = "like", Value = "1" });
            labels.Add(new SelectListItem { Text = "dislike", Value = "2" });
            ViewBag.LabelType = labels;

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
        public ActionResult Edit([Bind(Include = "Id,Label,Name,Email,Comment,EditTime")] Guestbook guestbook)
        {
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
