﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class Menu
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }
        public Menu() {
            Dishes = new List<Dish>();
        }


    }
}