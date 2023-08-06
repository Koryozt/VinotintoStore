using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Coupons.Commands.Create;

public sealed record CreateCouponCommand(
    string Code,
    double Discount,
    DateTime ExpirationDate,
    bool IsActive) : ICommand<Guid>;
