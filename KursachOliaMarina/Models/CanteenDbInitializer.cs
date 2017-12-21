using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class CanteenDbInitializer:DropCreateDatabaseAlways<CanteenContext>
    {
        protected override void Seed(CanteenContext context)
        {
            User user = new User
            { Id = 1,
                FName = "fdgg",
                LName = "fdf",
                Email = "user1",
                Sex = "man",
                Password = "user1",
                Visit = 1
                

            };
            context.Users.Add(user);
            Admin adm1 = new Admin
            {
                Id = 1,
                Login = "admin1",
                Password = "admin1"
            };
            Admin adm2 = new Admin
            {
                Id = 1,
                Login = "admin2",
                Password = "admin2"
            };
            context.Admins.Add(adm1);
            context.Admins.Add(adm2);
            base.Seed(context);
        }
    }
}