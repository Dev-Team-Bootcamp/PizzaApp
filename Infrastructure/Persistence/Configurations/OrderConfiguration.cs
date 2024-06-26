﻿using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderAggregate>
    {
        public void Configure(EntityTypeBuilder<OrderAggregate> builder)
        {
            builder.ToTable("order");
            builder.HasKey(o => o.Id);

            builder.Property(x => x.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(o => o.OrderNumber).HasColumnName("order_number").HasMaxLength(200);
            builder.Property(o => o.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(18,2)");
            builder.Property(o => o.DiscountAmount).HasColumnName("discount_amount").HasColumnType("decimal(18,2)");
            builder.Property(o => o.OrderDate).HasColumnName("order_date").HasColumnType("date");
            builder.Property(o => o.CustomerName).HasColumnName("customer_name").HasColumnType("varchar(250)");

            builder.HasMany(o => o.Products).WithMany(p => p.Orders);
        }
    }
}
