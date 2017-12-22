using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KursachOliaMarina.Models;

namespace KursachOliaMarina.Controllers
{
   // public class OrderBinder : IModelBinder
   // {
        //public object BindModel(ControllerContext controllerContext,
         //                       ModelBindingContext bindingContext)
        //{
            //HttpRequestBase request = controllerContext.HttpContext.Request;

            //int clientId = Int32.Parse(request.Form.Get("ClientId"));
            //int hairStyleId = Int32.Parse(request.Form.Get("HairStyleId"));
            //int hairdresserId = Int32.Parse(request.Form.Get("HairdresserId"));
            //string date = request.Form.Get("Date");
            //string time = request.Form.Get("Time");
            //string[] dateParts = date.Split(new char[] { '-' });
            //string[] timeParts = time.Split(new char[] { '.' });
            //Order order = new Order
            //{
            //    ClientId = clientId,
            //    HairdresserId = hairdresserId,
            //    HairStyleId = hairStyleId,
            //    Date = new DateTime(Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2]),
            //                        Convert.ToInt32(timeParts[0]), 0, 0)
            //};
            //return order;
        }
   // }

    //public class StoreController : Controller
    //{
    //    CanteenContext db = new CanteenContext();

    //    [HttpPost]
    //    public ActionResult AddMenu([ModelBinder(typeof(MenuBinder))] Menu menu, int[] Menu_Ids)
    //    {
    //        order.Services.Clear();
    //        if (Service_Ids != null)
    //        {
    //            foreach (var c in db.Services.Where(c => Service_Ids.Contains(c.Id)))
    //            {
    //                order.Services.Add(c);
    //            }
    //        }
    //        db.Orders.Add(order);
    //        db.SaveChanges();
    //        Client client = db.Clients.Where(c => c.Id == order.ClientId).ToList().First();
    //        string userName = client.Name;
    //        Hairdresser hairdersser = db.Hairdressers.Where(c => c.Id == order.HairdresserId).ToList().First();
    //        string email = hairdersser.Email;
    //        new EmailSender(email, userName, order.Date.ToShortDateString() + " " + order.Date.ToShortTimeString()).send();
    //        return RedirectToAction("IndexClient", "Login");
    //    }

    //    [HttpGet]
    //    public ActionResult Delete(int id)
    //    {
    //        db.Orders.Remove(db.Orders.Find(id));
    //        db.SaveChanges();
    //        return RedirectToAction("IndexClient", "Login");
    //    }
    //}
//}