using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.DomainEvents;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;
using VM.Domain.ValueObjects.Users;

namespace VM.Domain.Entities;

public sealed class User : AggregateRoot, IAuditableEntity
{
    private readonly List<Order> _orders = new();

    private User(
        Guid id,
        Name firstname,
        Name lastname,
        Email email,
        Password password) : base(id)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Password = password;
    }

    public Name Firstname { get; private set; }
    public Name Lastname { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Guid ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public IReadOnlyCollection<Order> Orders => _orders;

    public static User Create(
        Guid id,
        Name firstname,
        Name lastname,
        Email email,
        Password password)
    {
        var user = new User(
            id,
            firstname,
            lastname,
            email,
            password)
        {
            CreatedOnUtc = DateTime.UtcNow,
            ModifiedOnUtc = DateTime.UtcNow
        };

        user.RaiseDomainEvent( new UserRegisteredDomainEvent(
            Guid.NewGuid(),
            user.Id));

        return user;
    }

    public void AddShoppingCart(ShoppingCart cart)
    {
        ShoppingCart = cart;
        ShoppingCartId = cart.Id;
    }

    public void AddNewOrder(Order order) =>
        _orders.Add(order);

    public void ChangeNames(Name firstname, Name lastname)
    {
        if (!Firstname.Equals(firstname) || 
            !Lastname.Equals(lastname))
        {
            RaiseDomainEvent(new UserNameChangedDomainEvent(
                Guid.NewGuid(),
                Id));
        }

        Firstname = firstname;
        Lastname = lastname;
    }
}
