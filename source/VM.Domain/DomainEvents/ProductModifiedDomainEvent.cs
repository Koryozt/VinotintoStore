using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Domain.DomainEvents;

public sealed record ProductModifiedDomainEvent(
    Guid Id, 
    Guid ProductId) : DomainEvent(Id);
