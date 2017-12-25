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
    public class MenusController : Controller
    {
        private CanteenContext db = new CanteenContext();

        // GET: Menus
        public ActionResult Index()
        {
            var menus = db.Menus.Include(m => m.Admin);
            return View(menus.ToList());
        }

        // GET: Menus/Details/5
        public ActionResult Details()
        {
            int? id = (from m in db.Menus select m.Id).ToList().Last();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        public ActionResult CurrentMenu()
        {
            DateTime dayOfWeek = DateTime.Today;

            Menu menuForToday = db.Menus.Where(f => f.DateOfMenu.Equals(dayOfWeek)).First();

            IList<Dish> selectedDishes = new List<Dish>();

            foreach (Dish dish in menuForToday.Dishes)
            {
                selectedDishes.Add(dish);
            }
            
            ViewBag.SelectedDishes = selectedDishes;
        
            return View();

        }

        // GET: Menus/Create
        public ActionResult Create()
        {
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "Login");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateOfMenu,AdminId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminId = new SelectList(db.Admins, "Id", "Login", menu.AdminId);
            return View(menu);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "Login", menu.AdminId);
            return View(menu);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateOfMenu,AdminId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminId = new SelectList(db.Admins, "Id", "Login", menu.AdminId);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
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
