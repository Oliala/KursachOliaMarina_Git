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
                Email = "asas",
                Sex = "man",
                Password = "asd",
                Visit = 1,
                Roles = "ad",

            };
            context.Users.Add(user);
            base.Seed(context);
        }
    }
}