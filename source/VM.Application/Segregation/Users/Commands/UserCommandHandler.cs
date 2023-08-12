using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Application.Abstractions.Messaging;
using VM.Application.Segregation.Users.Commands.Create;
using VM.Application.Segregation.Users.Commands.Login;
using VM.Application.Segregation.Users.Commands.Update;
using VM.Domain.Abstractions;
using VM.Domain.Entities;
using VM.Domain.Errors;
using VM.Domain.Shared;
using VM.Domain.ValueObjects.General;
using VM.Domain.ValueObjects.Users;

namespace VM.Application.Segregation.Users.Commands;

internal sealed class UserCommandHandler :
    ICommandHandler<CreateUserCommand, Guid>,
    ICommandHandler<LoginCommand, string>,
    ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUnitOfWork _uow;

    public UserCommandHandler(
        IUserRepository userRepository,
        IShoppingCartRepository shoppingCartRepository,
        IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _uow = uow;
        _shoppingCartRepository = shoppingCartRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(request.Email);
        Result<Password> passwordResult = Password.Create(request.Password);

        if (emailResult.IsFailure || passwordResult.IsFailure)
        {
            return Result.Failure<Guid>(
                DomainErrors.User.InvalidCredentials(
                    request.Email,
                    request.Password));
        }

        Result<Name> firstnameResult = Name.Create(request.Firstname),
            lastnameResult = Name.Create(request.Lastname);

        if (await _userRepository.IsEmailInUseAsync(
            emailResult.Value, 
            cancellationToken))
        {
            return Result.Failure<Guid>(
                DomainErrors.User.EmailAlreadyInUse(
                    request.Email));
        }

        User user = User.Create(
            Guid.NewGuid(),
            firstnameResult.Value,
            lastnameResult.Value,
            emailResult.Value,
            passwordResult.Value);

        ShoppingCart shopping = ShoppingCart.Create(
            Guid.NewGuid(),
            user);

        user.AddShoppingCart(shopping);

        await _shoppingCartRepository.AddAsync(
            shopping, 
            cancellationToken);
        
        await _userRepository.AddAsync(
            user, 
            cancellationToken);

        await _uow.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (user is null)
        {
            return Result.Failure(DomainErrors.User.NotFound(
                request.Id));
        }

        Result<Name> firstnameResult = Name.Create(
                request.Firstname),
            lastnameResult = Name.Create(
                request.Lastname);

        user.ChangeNames(
            firstnameResult.Value, 
            lastnameResult.Value);

        _userRepository.Update(user);

        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
