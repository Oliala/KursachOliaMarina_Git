using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;

namespace KursachOliaMarina.Models
{
    public class CanteenContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Zakaz> Zakazs { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Dish>().HasMany(d => d.Ingredients)
                    .WithMany(i => i.Dishes)
                    .Map(t => t.MapLeftKey("DishId")
                    .MapRightKey("IngredientId")
                    .ToTable("DishIngredient"));
        }

    }
}