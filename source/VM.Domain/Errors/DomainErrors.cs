using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Shared;

namespace VM.Domain.Errors;

# pragma warning disable

public static class DomainErrors
{
    public static class CartItem
    {
        public static Func<Guid, Error> NotFound = id => new Error(
            "CartItem.NotFound",
            $"The item with the id {id} was not found");

        public static Func<Guid, Error> ProductNotFound = productId =>
            new Error(
                "CartItem.ProductNotFound",
                $"The product with the id {productId} was not found");

        public static Func<Guid, Error> ShoppingCartNotFound = shoppingId =>
            new Error(
                "CartItem.ShoppingCartNotFound",
                $"The shopping cart with the id {shoppingId} was not found");
    }

    public static class Category
    {
        public static Func<string, Error> AlreadyExisting = name => new Error(
            "Category.AlreadyExisting",
            $"Category with name {name} already exists");

        public static Func<Guid, Error> ProductNotFound = productId =>
            new Error(
                "Category.ProductNotFound",
                $"The product with the id {productId} was not found");


        public static Func<string, Error> NotFound = id => new Error(
            "Category.NotFound",
            $"The category [{id}] was not found");
    }

    public static class Coupon
    {
        public static Func<string, Error> NotFound = id =>
            new Error(
                "Coupon.NotFound",
                $"The coupon [{id}] was not found");

        public static Func<Guid, Error> ProductNotFound = productId =>
            new Error(
                "Coupon.ProductNotFound",
                $"The product with the id {productId} was not found");

        public static Func<Guid, Error> OrderNotFound = orderId =>
            new Error(
                "Coupon.OrderNotFound",
                $"The order with the id {orderId} was not found");

        public static Func<double, Error> InvalidDiscount = discount =>
            new Error(
                "Coupon.InvalidDiscount",
                $"There was an error parsing the value {discount}, check if it's valid");
    }

    public static class OrderDetail
    {
        public static Func<Guid, Error> NotFound = id =>
            new Error(
                "OrderDetail.NotFound",
                $"The order detail with id {id} was not found");

        public static Func<Guid, Error> ProductNotFound = productId =>
            new Error(
                "OrderDetail.ProductNotFound",
                $"The product with id {productId} was not found");

        public static Func<Guid, Error> OrderNotFound = orderId =>
            new Error(
                "OrderDetail.OrderNotFound",
                $"The order with id {orderId} was not found");

        public static Error ProductOutOfStock = new Error(
            "OrderDetail.ProductOutOfStock",
            "Can't create a new order detail because product" +
            "currently has/will have 0 or less units in stock");
    }

    public static class Order
    {
        public static Func<Guid, Error> UserNotFound = userId =>
            new Error(
                "Order.UserNotFound",
                $"The user with id {userId} was not found");

        public static Func<Guid, Error> NotFound = id =>
            new Error(
                "Order.NotFound",
                $"The order with id {id} was not found");

        public static Func<Guid, Error> ProductNotFound =
            productId => new Error(
                "Order.ProductNotFound",
                $"The product with id {productId} was not found");
    }

    public static class Payment
    {
         public static Func<Guid, Error> NotFound = id =>
            new Error(
            "Payment.NotFound",
            $"The payment with the id {id} was not found");
    }

    public static class Product
    {
        public static Func<string, Error> NotFound = id =>
           new Error(
           "Product.NotFound",
           $"The product [{id}] was not found");

        public static Func<string, Error> CategoryNotFound = name =>
           new Error(
           "Product.CategoryNotFound",
           $"The category called {name} was not found");

        public static Error NoPhotoProvided = new Error(
            "Product.NoPhotoProvided",
            "No photo or image has been provided for product");
    }

    public static class Rating
    {
        public static Func<Guid, Error> NotFound = id =>
           new Error(
           "Rating.NotFound",
           $"The rating with id {id} was not found");

        public static Func<Guid, Error> ProductNotFound =
            productId => new Error(
                "Rating.ProductNotFound",
                $"The product with id {productId} was not found");
    }

    public static class Shipping
    {
        public static Func<Guid, Error> NotFound = id =>
           new Error(
           "Shipping.NotFound",
           $"The shipping with id {id} was not found");

        public static Func<Guid, Error> OrderNotFound = id =>
           new Error(
           "Shipping.OrderNotFound",
           $"The order with id {id} was not found");
    }

    public static class ShoppingCart
    {
        public static Func<Guid, Error> NotFound = id =>
            new Error(
                "ShoppingCart.NotFound",
                $"The shopping cart with id {id} was not found");

        public static Func<Guid, Error> UserNotFound = userId =>
            new Error(
                "ShoppingCart.UserNotFound",
                $"The user with id {userId} was not found");
    }

    public static class User
    {
        public static Func<Guid, Error> NotFound = id =>
            new Error(
                "User.NotFound",
                $"The user with id {id} was not found");

        public static Func<string, string, Error> InvalidCredentials = 
            (email, password) =>
            new Error(
                "User.InvalidCredentials", 
                $"The email {email} or the password {password} are invalid"
                + " or incorrect");

        public static Func<string, Error> EmailAlreadyInUse = email =>
            new Error(
                "User.EmailAlreadyInUse",
                $"The email {email} is already taken");
    }

    public static class Email
    {
        public static readonly Error Empty = new(
            "Email.Empty",
            "Email is empty");

        public static readonly Error TooLong = new(
            "Email.TooLong",
            "Email is too long, the limit is 255 characters");

        public static readonly Error InvalidFormat = new(
            "Email.InvalidFormat",
            "Email format is invalid");
    }

    public static class Password
    {
        public static readonly Error Empty = new(
            "Password.Empty",
            "Password is empty");

        public static readonly Error TooShort = new(
            "Password.TooShort",
            "Password too short. The minimum permitted are 8 characters");

        public static readonly Error TooLong = new(
            "Password.TooLong",
            "Password too long. The maximum permitted are 32 characters");

        public static readonly Error Invalid = new(
            "Password.Invalid",
            "The password must contain uppercase, lowercase, numbers and special characters");
    }

    public static class Name
    {
        public static readonly Error Empty = new(
            "Name.Empty",
            "Name is empty");

        public static readonly Error TooLong = new(
            "Name.TooLong",
            "Name is too long, the limit is 50 characters");
    }

    public static class LongText
    {
        public static readonly Error Empty = new(
            "LongText.Empty",
            "Long text is empty");

        public static readonly Error TooLong = new(
            "LongText.TooLong",
            "Long text exceeded the limit of 5000 characters");
    }

    public static class Numeric
    {
        public static readonly Error NegativeValue = new(
            "Numeric.NegativeValue",
            "The value provided can not be negative");

        public static readonly Error TooLong = new(
            "Numeric.TooLong",
            "The value exceeded the max value of 100000000.00");
    }

    public static class Quantity
    {
        public static readonly Error NegativeValue = new(
            "Quantity.NegativeValue",
            "The value provided can not be negative");

        public static readonly Error TooLong = new(
            "Quantity.TooLong",
            "The value exceeded the max value of 1000000");
    }

    public static class Code
    {
        public static readonly Error Empty = new(
            "Code.Empty",
            "Code is empty");

        public static readonly Error TooShort = new(
            "Code.TooShort",
            "Code too short. The minimum permitted are 5 characters");

        public static readonly Error TooLong = new(
            "Code.TooLong",
            "Code exceeded the limit of 15 characters");

        public static readonly Error InvalidFormat = new(
            "Code.InvalidFormat",
            "Code format is invalid");
    }
}
