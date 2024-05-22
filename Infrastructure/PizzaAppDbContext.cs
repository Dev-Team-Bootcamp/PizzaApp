using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Application.Common.Interfaces;
using Infrastructure.Persistence.Configurations;

namespace Infrastructure
{
    public class PizzaAppDbContext : DbContext, IPizzaAppDbContext
    {
        public DbSet<ProductAggregate> Products { get; set; }
        public DbSet<OrderAggregate> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new NpgsqlDataSourceBuilder("Server=localhost;Port=5432;Database=pizzaapp;Userid=postgres;Password=123456;Include Error Detail=True;");
            builder.EnableDynamicJson();
            var dataSource = builder.Build();
            optionsBuilder.UseNpgsql(dataSource);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
