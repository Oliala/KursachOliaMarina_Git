using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KursachOliaMarina.Models;
using KursachOliaMarina.Utils;

namespace KursachOliaMarina.Controllers
{
    public class MenuBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
                                ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            int adminId = Int32.Parse(request.Form.Get("AdminId"));
            //int hairStyleId = Int32.Parse(request.Form.Get("HairStyleId"));
            //int hairdresserId = Int32.Parse(request.Form.Get("HairdresserId"));
            string date = request.Form.Get("DateOfMenu");
            //string time = request.Form.Get("Time");
            string[] dateParts = date.Split(new char[] { '-' });
            //string[] timeParts = time.Split(new char[] { '.' });
            Menu menu = new Menu
            {
                AdminId = adminId,

                DateOfMenu = new DateTime(Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2]))
            };
            return menu;
        }

    }
    public class StoreController : Controller
    {
        CanteenContext db = new CanteenContext();

        [HttpPost]
        public ActionResult AddMenu([ModelBinder(typeof(MenuBinder))] Menu menu, int[] Dish_Ids)
        {
            menu.Dishes.Clear();
            if (Dish_Ids != null)
            {
                foreach (var c in db.Dishes.Where(c => Dish_Ids.Contains(c.Id)))
                {
                    menu.Dishes.Add(c);
                }
            }
            db.Menus.Add(menu);
            db.SaveChanges();
            //Admin admin = db.Admins.Where(c => c.Id == menu.AdminId).ToList().First();
            //string adminName = admin.Login;
            //var listuser = db.Users.Where(c => c.Email != null).ToList();
            //foreach (User r in listuser)
            //{

            //    //string email = user.Email.All;
            //    new EmailSender(r.Email, adminName, menu.DateOfMenu.ToShortDateString() + " " + menu.DateOfMenu.ToShortTimeString()).send();


            //}
            return RedirectToAction("Menus", "Admins");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            db.Menus.Remove(db.Menus.Find(id));
            db.SaveChanges();
            return RedirectToAction("Menus", "Admins");
        }
    }
}