using System;
using FluentValidation;
using BankSystem.App.Dto;

namespace BankSystem.App.Validations
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(u => u.FullName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя сотрудника обязательно.");

            RuleFor(u => u.Phone)
                .NotNull()
                .NotEmpty()
                .WithMessage("Телефон сотрудника обязательно.");
            
            RuleFor(u => u.Salary)
                .NotNull()
                .NotEmpty()
                .WithMessage("Сумма ЗП не указана.");
            
            RuleFor(u => u.Contract)
                .NotNull()
                .NotEmpty()
                .WithMessage("Требуется заполненный контракт.");

        }
    }
}
