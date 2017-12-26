using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class CanteenDbInitializer:DropCreateDatabaseIfModelChanges<CanteenContext>
    {
        protected override void Seed(CanteenContext context)
        {
            User user1 = new User
            {
                Id = 1,
                FName = "Olia",
                LName = "Artukh",
                Email = "oliala.oo@gmail.com",
                Sex = "f",
                Password = "user1",
                Visit = 0
            };
            User user2 = new User
            {
                Id = 2,
                FName = "Marina",
                LName = "Yablonska",
                Email = "mery1997@rambler.ru",
                Sex = "f",
                Password = "user2",
                Visit = 0
            };
            context.Users.Add(user1);
            context.Users.Add(user2);
            Admin adm1 = new Admin
            {
                Id = 1,
                Login = "admin1",
                Password = "admin1"
            };
            context.Admins.Add(adm1);
            base.Seed(context);
        }
    }
}