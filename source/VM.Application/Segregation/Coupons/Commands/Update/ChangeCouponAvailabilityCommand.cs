using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.Coupons.Commands.Update;

public sealed record ChangeCouponAvailabilityCommand(bool IsActive) : ICommand;