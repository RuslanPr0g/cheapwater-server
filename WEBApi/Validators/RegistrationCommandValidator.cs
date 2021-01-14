using DataAccessLibrary.DB;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WEBApi.CQRS.Actions.Commands;
using WEBApi.DTOs;

namespace WEBApi.Validators
{
    public class RegistrationCommandValidator:AbstractValidator<RegistrationCommand>
    {
        private readonly IUserReadRepo _repo;
       
        public override Task<ValidationResult> ValidateAsync(ValidationContext<RegistrationCommand> context, CancellationToken cancellation = default)
        {
            RuleFor(command => command.DTO.Email)
               .NotEmpty().WithMessage("Email can't be empty")
               .EmailAddress().WithMessage("Not a proper email address")
               .MustAsync(async (email, cancellation) =>
              await BeAvailable(email, cancellation)).WithMessage("Email is already taken");
            RuleFor(command => command.DTO.Nickname)
                .NotEmpty().WithMessage("Nickname can't be empty")
                .MinimumLength(2).WithMessage("Nickname is too short")
                .MaximumLength(32).WithMessage("Nickname is too long");
            RuleFor(command => command.DTO.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .MinimumLength(6).WithMessage("Password is too short")
                .MaximumLength(64).WithMessage("Password is too long");
            return base.ValidateAsync(context, cancellation);
        }
        public RegistrationCommandValidator(IUserReadRepo repo)
        {
            this._repo = repo;
        }
        protected async Task<bool> BeAvailable(string email, CancellationToken cancellation)
        {
            return !await _repo.CheckIsEmailPresent(email, cancellation);
        }
    }
}
