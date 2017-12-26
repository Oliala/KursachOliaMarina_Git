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
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public CanteenContext(): base("CanteenContext") { }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Dish>().HasMany(d => d.Ingredients)
                    .WithMany(i => i.Dishes)
                    .Map(t => t.MapLeftKey("DishId")
                    .MapRightKey("IngredientId")
                    .ToTable("DishIngredient"));

            modelBuilder.Entity<Dish>().HasMany(d => d.Menus)
                    .WithMany(i => i.Dishes)
                    .Map(t => t.MapLeftKey("DishId")
                    .MapRightKey("MenuId")
                    .ToTable("DishMenu"));

            modelBuilder.Entity<Zakaz>().HasMany(d => d.Dishes)
                   .WithMany(i => i.Zakazs)
                   .Map(t => t.MapLeftKey("ZakazId")
                   .MapRightKey("DishId")
                   .ToTable("ZakazDish"));

        }


    }
}