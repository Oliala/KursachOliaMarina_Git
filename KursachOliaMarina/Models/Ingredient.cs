using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }
        public Ingredient()
        {
            Dishes = new List<Dish>();
        }
    }
}
