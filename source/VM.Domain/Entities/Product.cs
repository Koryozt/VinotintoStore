﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.DomainEvents;
using VM.Domain.Primitives;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

#pragma warning disable

public sealed class Product : AggregateRoot, IAuditableEntity
{
    private readonly List<Category> _categories = new();
    private readonly List<OrderDetail> _orderDetails = new();
    private readonly List<Rating> _ratings = new();

    private Product(
        Guid id,
        string photo,
        Name name,
        LongText description,
        Amount price,
        Quantity stock) : base(id)
    {
        Photo = photo;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }

    public string Photo { get; set; }
    public Name Name { get; set; }
    public LongText Description { get; set; }
    public Amount Price { get; set; }
    public Quantity Stock { get; set; }
    public IReadOnlyCollection<Category> Categories => _categories;
    public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails;
    public IReadOnlyCollection<Rating> Ratings => _ratings;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Product Create(
        Guid id,
        string photo,
        Name name,
        LongText description,
        Amount price,
        Quantity stock)
    {
        var product = new Product(
            id,
            photo,
            name,
            description,
            price,
            stock);

        product.RaiseDomainEvent(new NewProductDomainEvent(
            Guid.NewGuid(),
            product.Id));

        return product;
    }

    public void AddCategory(Category category) =>
        _categories.Add(category);

    public void AddOrderDetail(
        OrderDetail orderDetail,
        int remaining)
    {
        Result<Quantity> remainingResult = Quantity.Create(
            remaining);

        Stock = remainingResult.Value;

        _orderDetails.Add(orderDetail);
    }

    public void AddRating(Rating rating) =>
        _ratings.Add(rating);

    public void ChangeData(
        Name name,
        LongText description,
        Amount price,
        Quantity stock)
    {
        if (!Name.Equals(name) || !Description.Equals(description) ||
            !Price.Equals(price) || !Stock.Equals(stock))
        {
            RaiseDomainEvent(new ProductModifiedDomainEvent(
                Guid.NewGuid(),
                Id));
        }

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }
}
