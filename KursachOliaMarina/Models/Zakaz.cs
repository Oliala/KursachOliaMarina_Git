using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class Zakaz
    {
        public int Id { get; set; }

        public DateTime DateOfZakaz { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }
        

        public Zakaz()
        {
            Dishes = new List<Dish>();
            
        }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}