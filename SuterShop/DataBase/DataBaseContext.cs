using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SuterShop
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Goods> GoodsList { get; set; }
        public DbSet<GoodsForSale> GoodsForSaleList { get; set; }
        public DbSet<SoldGoods> SoldGoodsList { get; set; }
        public DbSet<Category> Category { get; set; }       
        public DbSet<User> Users { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Seller> Seller { get; set; }
        private string _cs { get; set; }
        public DataBaseContext(string cs)
        {
            _cs = cs;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_cs, new MySqlServerVersion(new Version(8, 0, 35)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goods>(pc =>
            {
                //создаем дискриминатор
                pc.HasDiscriminator<string>("Discriminator")
                .HasValue<SoldGoods>("Sold")
                .HasValue<GoodsForSale>("ForSale");
            });
            modelBuilder.Entity<User>(pc =>
            {
                //создаем дискриминатор
                pc.HasDiscriminator<string>("UserStatus")
                .HasValue<Admin>("Admin")
                .HasValue<Buyer>("Buyer")
                .HasValue<Seller>("Seller");
            });
        }
    }

    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Byte[] Image { get; set; }
        public int Price { get; set; }
        public int Count { get; set; } //количество товара
        public Category Category { get; set; }
        public Seller Seller { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Admin : User
    {

    }

    public class Buyer : User
    {
        public int Discount { get; set; }
    }

    public class Seller: User
    {
        public int sum { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public class GoodsForSale : Goods { }
    public class SoldGoods : Goods { }
}
