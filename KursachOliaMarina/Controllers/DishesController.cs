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
    public class DishesController : Controller
    {
        private CanteenContext db = new CanteenContext();

        // GET: Dishes
        public ActionResult Index()
        {
            return View(db.Dishes.ToList());
        }

        // GET: Dishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // GET: Dishes/Create
        public ActionResult Create()
        {
            ViewBag.SelectedIngredients = db.Ingredients.ToList();
            return View();
        }

        // POST: Dishes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dish dish, int[] ingredients)
        {
            Dish newDish = new Dish();
            newDish.DishName = dish.DishName;
            newDish.Category = dish.Category;
            newDish.Price = dish.Price;
            newDish.Weight = dish.Weight;
            newDish.Note = dish.Note;
            newDish.Popularity = 0;

            newDish.Ingredients.Clear();
            if (ingredients != null)
            {
                //получаем выбранные курсы
                foreach (var c in db.Ingredients.Where(co => ingredients.Contains(co.Id)))
                {
                    newDish.Ingredients.Add(c);
                }
            }


            db.Dishes.Add(newDish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Dishes/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ingredients = db.Ingredients.ToList();
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dish dish, int[] usedIngredients)
        {
            Dish newDish = db.Dishes.Find(dish.Id);
            newDish.DishName = dish.DishName;
            newDish.Category = dish.Category;
            newDish.Price = dish.Price;
            newDish.Weight = dish.Weight;
            newDish.Note = dish.Note;
           // newDish.Popularity = dish.Popularity;


            newDish.Ingredients.Clear();
            if (usedIngredients != null)
            {
                //получаем выбранные курсы
                foreach (var c in db.Ingredients.Where(co => usedIngredients.Contains(co.Id)))
                {
                    newDish.Ingredients.Add(c);
                }
            }

            db.Entry(newDish).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Dishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dish dish = db.Dishes.Find(id);
            db.Dishes.Remove(dish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateMenu()
        {
            return RedirectToAction("CreateMenu", "Admins");
        }

        public ActionResult TopDishes()
        {
 //           IEnumerable<Dish> dishes = db.D.Where(z => z.Sex.Equals(client.Sex));
            IEnumerable<Dish> dishes = db.Dishes.OrderByDescending(z => z.Popularity).Take(10);
            ViewBag.Dishes = dishes;
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
