﻿using System;
using System.Collections.Generic;
using Domain.Model;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class version3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    total_amount = table.Column<double>(type: "numeric(18,2)", nullable: false),
                    discount_amount = table.Column<double>(type: "numeric(18,2)", nullable: false),
                    order_date = table.Column<DateTime>(type: "date", nullable: false),
                    customer_name = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    @int = table.Column<int>(name: "int", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_name = table.Column<string>(type: "varchar(250)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<double>(type: "numeric(18,2)", nullable: false),
                    created_date = table.Column<DateTime>(type: "date", nullable: false),
                    ingredients = table.Column<List<Ingredients>>(type: "jsonb", nullable: false),
                    quantity = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.@int);
                });

            migrationBuilder.CreateTable(
                name: "OrderAggregateProductAggregate",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAggregateProductAggregate", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderAggregateProductAggregate_order_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderAggregateProductAggregate_product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "product",
                        principalColumn: "int",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderAggregateProductAggregate_ProductsId",
                table: "OrderAggregateProductAggregate",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderAggregateProductAggregate");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
