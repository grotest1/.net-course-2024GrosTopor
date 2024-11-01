using System;
using FluentValidation;
using BankSystem.App.Dto;

namespace BankSystem.App.Validations
{
    public class ClientDtoValidator : AbstractValidator<ClientDto>
    {
        public ClientDtoValidator()
        {
            RuleFor(u => u.FullName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя клиента обязательно.");

            RuleFor(u => u.Phone)
                .NotNull()
                .NotEmpty()
                .WithMessage("Телефон клиента обязательно.");

        }
    }
}
