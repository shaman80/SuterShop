using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuterShop
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Goods> GoodsList { get; set; }
        public DbSet<GoodsForSale> GoodsForSaleList { get; set; }
        public DbSet<SoldGoods> SoldGoodsList { get; set; }
        public DbSet<Category> Category { get; set; }       
        public DbSet<Message> Messages { get; set; }       
        public DbSet<User> Users { get; set; }
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
            //modelBuilder.Entity<User>(pc =>
            //{
            //    //создаем дискриминатор
            //    pc.HasDiscriminator<string>("UserStatus")
            //    .HasValue<Admin>("Admin")
            //    .HasValue<Buyer>("Buyer")
            //    .HasValue<Seller>("Seller");
            //});
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
        public int SoldCount { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Discount { get; set; }
        public int sum { get; set; }
        public Statuses Status { get; set; }
    }
    public class GoodsForSale : Goods { }
    public class SoldGoods : Goods { }

    public class Message
    {
        public int Id { get; set; }
        public string text { get; set; }
        public User user { get; set; }
        public DateTime data { get; set; }
    }
}
