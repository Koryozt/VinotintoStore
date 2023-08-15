using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.Coupons;
using VM.Domain.ValueObjects.General;

namespace VM.Application.Segregation.Coupons.Queries.Statements;
public sealed record GetAmountWithCouponQuery(string Code, Guid OrderId) : IQuery<Amount>;