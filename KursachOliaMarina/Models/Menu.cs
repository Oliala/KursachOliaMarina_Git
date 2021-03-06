﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public DateTime DateOfMenu { get; set; }
        public int? AdminId { get; set; }
        public Admin Admin { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }
        public virtual ICollection<Zakaz> Zakazs { get; set; }

        public Menu()
        {
            Dishes = new List<Dish>();
            Zakazs = new List<Zakaz>();
        }
    }
}