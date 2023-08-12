using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Domain.DomainEvents;

public sealed record OrderMadeDomainEvent(
    Guid Id, 
    Guid OrderId) : DomainEvent(Id);