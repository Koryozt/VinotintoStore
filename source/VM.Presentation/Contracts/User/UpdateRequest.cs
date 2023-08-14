using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Presentation.Contracts.User;

public sealed record UpdateRequest(
    string? Firstname,
    string? Lastname);