using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;

namespace VM.Application.Segregation.Categories.Commands.Update;

public sealed record UpdateCategoryCommand(Guid Id, string Name) : ICommand;