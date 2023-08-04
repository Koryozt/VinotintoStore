using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;
using VM.Domain.ValueObjects.Users;

namespace VM.Domain.Entities;

public sealed class User : AggregateRoot, IAuditableEntity
{
    private User(Guid id) : base(id)
    {

    }

    public Name Firstname { get; private set; }
    public Name Lastname { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Guid ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
}
