using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Domain.ValueObjects.Coupons;

namespace VM.Application.Segregation.Coupons.Queries.Statements;

public sealed record GetCouponByCodeQuery(string Code) : IQuery<CouponResponse>;