using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace VM.Domain.Primitives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
