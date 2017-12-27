using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KursachOliaMarina.Models;

namespace KursachOliaMarina.Controllers
{
    public class ZakazsController : Controller
    {
        private CanteenContext db = new CanteenContext();

        // GET: Zakazs
        public ActionResult Index()
        {
            var zakazs = db.Zakazs.Include(z => z.User);
            return View(zakazs.ToList());
        }

        // GET: Zakazs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zakaz zakaz = db.Zakazs.Find(id);
            if (zakaz == null)
            {
                return HttpNotFound();
            }
            return View(zakaz);
        }

        // GET: Zakazs/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Zakazs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateOfZakaz,UserId")] Zakaz zakaz)
        {
            if (ModelState.IsValid)
            {
                db.Zakazs.Add(zakaz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", zakaz.UserId);
            return View(zakaz);
        }

        // GET: Zakazs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zakaz zakaz = db.Zakazs.Find(id);
            if (zakaz == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", zakaz.UserId);
            return View(zakaz);
        }

        // POST: Zakazs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateOfZakaz,UserId")] Zakaz zakaz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zakaz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", zakaz.UserId);
            return View(zakaz);
        }

        // GET: Zakazs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zakaz zakaz = db.Zakazs.Find(id);
            if (zakaz == null)
            {
                return HttpNotFound();
            }
            return View(zakaz);
        }

        // POST: Zakazs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zakaz zakaz = db.Zakazs.Find(id);
            db.Zakazs.Remove(zakaz);
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
