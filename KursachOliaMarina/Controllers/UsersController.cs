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
               
                return RedirectToAction("IndexUser", "Users");
            }
            return View();
        }

       
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        [HttpGet]
        public ActionResult IndexUser()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("LoginUser", "Users");
            }
            User user = (User)Session["user"];
           // GetDataForClient();
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
                return RedirectToAction("IndexUser", "Users");
            }
            ViewBag.LoginFaultMessage = "Не верная пара логин/пароль";
            return View("LoginUser");
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
