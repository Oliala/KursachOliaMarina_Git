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
    public class UsersController : Controller
    {
        private CanteenContext db = new CanteenContext();



        [HttpGet]
        public ActionResult LogoutUser()
        {
            Session["user"] = null;
            Session["remember"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult LoginUser()
        {
            if (Session["user"] != null)
            {
                User user = (User)Session["user"];

                return RedirectToAction("Index", "Users");
            }
            return View();
        }

        [HttpPost]
        public ActionResult LoginUserForm(string Email, string Password, bool Remember = false)
        {
            List<User> userList = db.Users.Where(a => a.Email.Equals(Email) && a.Password.Equals(Password)).ToList();
            if (userList.Count == 1)
            {
                ViewBag.LoginFaultMessage = null;
                User user = userList.First();
                Session["user"] = user;
                if (Remember)
                    Session["remember"] = "remember";
                return RedirectToAction("Index", "Users");
            }
            ViewBag.LoginFaultMessage = "Не верная пара логин/пароль";
            return View("LoginUser");
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login", "Admins");
            }
            Admin admin = (Admin)Session["admin"];
           // MenuForUser();
            return View();
        }

        [HttpGet]
        public ActionResult ToMainPage()
        {
            if (Session["remember"] == null)
            {
                Session["user"] = null;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateZakaz()
        {
            if (Session["user"] == null)
            {
                ViewBag.LoginFaultMessage = "Ошибка доступа. Авторизируйтесь";
                return RedirectToAction("LoginUser");
            }
            ViewBag.LoginUser = ((User)Session["user"]).Email;
            GetDataForUser();
            if (Session["error"] != null)
            {

            }
            return View();
        }

        //public ActionResult MyZakazs()
        //{
        //    User user = (User)Session["user"];
        //    ViewBag.IdUser = user.Id;
        //    IEnumerable<Dish> dishes = db.Dishes;
        //    ViewBag.Dishes = dishes;
        //    IList<Zakaz> zakazs = db.Zakazs.Where(f => f.UserId.Equals(user.Id)).ToList();
        //    //IList<HairStyle> orderedHairstyles = new List<HairStyle>();
        //    //IList<Hairdresser> orderedHairdressers = new List<Hairdresser>();
        //    IList<List<Dish>> orderedDishes = new List<List<Dish>>();
        //    List<double> orderCosts = new List<double>();
        //    foreach (Zakaz zakaz in zakazs)
        //    {
                
        //        orderedDishes.Add(zakaz.Dishes.ToList());
        //        double allprice = db.Dishes.Where(s => s.Id == zakaz.DishId).ToList().First().Cost;
        //        foreach (Service service in order.Services.ToList())
        //        {
        //            cost += service.Cost;
        //        }
        //        orderCosts.Add(cost);
        //    }
        //    ViewBag.OrderedHairstyles = orderedHairstyles;
        //    ViewBag.OrderedHairdressers = orderedHairdressers;
        //    ViewBag.Orders = orders;
        //    ViewBag.OrderedServices = orderedServices;
        //    ViewBag.CostOfOrders = orderCosts;
        //}

        public ActionResult MenuForUser()
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

        public void GetDataForUser()
        {
            User user = (User)Session["user"];
        //    IEnumerable<Menu> currentmenus = db.Menus.OrderByDescending(f => f.Id).Take(1);
         //   ViewBag.Menus = currentmenus;
            ViewBag.IdUser = user.Id;

            DateTime dayOfWeek = DateTime.Today;

            Menu menuForToday = db.Menus.Where(f => f.DateOfMenu.Equals(dayOfWeek)).First();

            IList<Dish> selectedDishes = new List<Dish>();

            foreach (Dish dish in menuForToday.Dishes)
            {

                selectedDishes.Add(dish);

            }

            ViewBag.SelectedDishes = selectedDishes;
        }



        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Password,LName,FName,Sex,Visit")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Password,LName,FName,Sex,Visit")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult TopUsers()
        {
            //           IEnumerable<Dish> dishes = db.D.Where(z => z.Sex.Equals(client.Sex));
            IEnumerable<User> users = db.Users.OrderByDescending(z => z.Visit).Take(10);
            ViewBag.Users = users;
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
