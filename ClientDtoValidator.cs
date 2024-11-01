using System;
using FluentValidation;

public class ClientDtoValidator : AbstractValidator<ClientDto>
{
    public ClientDtoValidator()
    {
        RuleFor(u => u.FullName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя пользователя обязательно.");

        RuleFor(u => u.Email)
            .NotNull()
            .NotEmpty();
    }
}
