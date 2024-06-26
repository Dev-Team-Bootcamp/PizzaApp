﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Application.Common.Interfaces;

namespace Application.Product.Commands
{
    public class AddProductCommand : IRequest<ProductAggregate>
    {
        public AddProductCommand(string productName, string description, double price, List<Ingredients> ingredients, double quantity)
        {
            Name = productName;
            Description = description;
            Price = price;
            Quantity = quantity;
            Ingredients = ingredients;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public double Quantity { get; set; }

        public class Handler : IRequestHandler<AddProductCommand, ProductAggregate>
        {
            private IMediator _mediator;
            private readonly IPizzaAppDbContext _dbContext;

            public Handler(IPizzaAppDbContext dbContext, IMediator mediator)
            {
                _dbContext = dbContext;
                _mediator = mediator;
            }

            public async Task<ProductAggregate> Handle(AddProductCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    throw new Exception("PRODUCT_NAME_CANNOT_BE_EMPTY");
                }

                var product = ProductAggregate.Create(request.Name, request.Description, request.Price, request.Ingredients, request.Quantity);
                await _dbContext.Products.AddAsync(product, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return product;
            }
        }
    }
}
