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
    public class AdminsController : Controller
    {
        private CanteenContext db = new CanteenContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            List<Admin> adminRes = db.Admins.Where(adm => adm.Login.Equals(admin.Login) && adm.Password.Equals(admin.Password)).ToList();
            if (adminRes.Count == 1)
            {
                Session["admin"] = adminRes.First();
                ViewBag.LoginAdmin = ((Admin)Session["admin"]).Login;
                return RedirectToAction("Index");
            }
            ViewBag.LoginFaultMessage = "Не верная пара логин/пароль";
            return View("Login");

        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["admin"] = null;
            return RedirectToAction("Index", "Home");
        }

        // GET: Admins
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                ViewBag.LoginFaultMessage = "Ошибка доступа. Авторизируйтесь";
                return RedirectToAction("Login");
            }
            ViewBag.LoginAdmin = ((Admin)Session["admin"]).Login;
            //int hc = db.Hairdressers.Count();
            //ViewBag.HairdressersCount = hc;
            GetDataForAdmin();
            //ViewBag.UsersCount = db.Users.Count();
            //ViewBag.IngredientsCount = db.Ingredients.Count();
            //ViewBag.DishesCount = db.Dishes.Count();
            //ViewBag.DishesCount = db.Dishes.Count();
            return View();
        }
        public ActionResult Users()
        {
            if (Session["admin"] == null)
            {
                ViewBag.LoginFaultMessage = "Ошибка доступа. Авторизируйтесь";
                return RedirectToAction("Login");
            }
            ViewBag.LoginAdmin = ((Admin)Session["admin"]).Login;
            return View(db);
        }
        public ActionResult Ingredients()
        {
            if (Session["admin"] == null)
            {
                ViewBag.LoginFaultMessage = "Ошибка доступа. Авторизируйтесь";
                return RedirectToAction("Login");
            }
            ViewBag.LoginAdmin = ((Admin)Session["admin"]).Login;
            return View(db);
        }
        public ActionResult Dishes()
        {
            if (Session["admin"] == null)
            {
                ViewBag.LoginFaultMessage = "Ошибка доступа. Авторизируйтесь";
                return RedirectToAction("Login");
            }
            ViewBag.LoginAdmin = ((Admin)Session["admin"]).Login;
            return View(db);
        }

        public ActionResult CreateMenu()
        {
            if (Session["admin"] == null)
            {
                ViewBag.LoginFaultMessage = "Ошибка доступа. Авторизируйтесь";
                return RedirectToAction("Login");
            }
            ViewBag.LoginAdmin = ((Admin)Session["admin"]).Login;
            GetDataForAdmin();
            return View();
        }
        public ActionResult Menus()
        {
            if (Session["admin"] == null)
            {
                ViewBag.LoginFaultMessage = "Ошибка доступа. Авторизируйтесь";
                return RedirectToAction("Login");
            }
            ViewBag.LoginAdmin = ((Admin)Session["admin"]).Login;
            
            return View();
        }

        public void GetDataForAdmin()
        {
            Admin admin = (Admin)Session["admin"];
            IEnumerable<Dish> dishes = db.Dishes;
            ViewBag.Dishes = dishes;

            IList<Menu> menus = db.Menus.Where(f => f.Id.Equals(admin.Id)).ToList();
            IList<List<Dish>> selectedDishes = new List<List<Dish>>();

            foreach (Menu menu in menus)
            {

                selectedDishes.Add(menu.Dishes.ToList());

            }

            ViewBag.ReadyDishes = selectedDishes;
        }

        public ActionResult CreateIngredient([Bind(Include = "Id,IngredientName")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Ingredients.Add(ingredient);
                db.SaveChanges();
            }
            return RedirectToAction("Ingredients");
        }

        public ActionResult CreateAdditionMenu([Bind(Include = "Id,dateOfMenu")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
            }
            return RedirectToAction("Menus");
        }
        public ActionResult CreateDish([Bind(Include = "Id,DishName,Category,Price,Weight,Note")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Dishes.Add(dish);
                db.SaveChanges();
            }
            return RedirectToAction("Create","Dishes");
        }
        public ActionResult DeleteDish(int? id)
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
            db.Dishes.Remove(dish);
            db.SaveChanges();
            return RedirectToAction(" Dishes");
        }
        public ActionResult DeleteIngredient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            db.Ingredients.Remove(ingredient);
            db.SaveChanges();
            return RedirectToAction("Ingredients");
        }
        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Password,Roles")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Password,Roles")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
