using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public float Price { get; set; }
        public float Weight { get; set; }
        public int Popularity { get; set;}
        public int Weekday { get; set; }
        public string Note { get; set; } // Рыбное\мясное\сладкое\молочное


        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public Dish()
        {
            Ingredients = new List<Ingredient>();
        }
    }
}