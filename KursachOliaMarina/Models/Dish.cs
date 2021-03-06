﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace KursachOliaMarina.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public int Category { get; set; } // первое\второе\десерт\напиток
        // можно сделать категорию тоже цифрой, тогда легко будет отсортировать
        // 1 = первое, 2 = второе, 3 = салат, 4 = десерт, 5 = напиток
        // в таком порядке они и будут идти потом в меню, а не вперемешку
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public int Popularity { get; set; }
        public string Note { get; set; } // Рыбное\мясное\сладкое\молочное

        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Zakaz> Zakazs { get; set; }

        public Dish()
        {
            Ingredients = new List<Ingredient>();
            Menus = new List<Menu>();
            Zakazs = new List<Zakaz>();
        }

    }

    
}