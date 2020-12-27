using DataAccessLibrary.DB;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.DTOs;

namespace WEBApi.Validators
{
    public class RegistrationValidator : AbstractValidator<UserRegistrationModel>
    {
        private readonly IUserReadRepository _repo;

        public RegistrationValidator(IUserReadRepository repo)
        {
            this._repo = repo;
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email can't be empty")
                .EmailAddress().WithMessage("Not a proper email address")
                .MustAsync( async(email, cancellation)=>
                await BeAvailable(email)).WithMessage("Email is already taken");
            RuleFor(user => user.Nickname)
                .NotEmpty().WithMessage("Nickname can't be empty")
                .MinimumLength(2).WithMessage("Nickname is too short")
                .MaximumLength(32).WithMessage("Nickname is too long");
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .MinimumLength(6).WithMessage("Password is too short")
                .MaximumLength(64).WithMessage("Password is too long");
        }
        protected async Task<bool> BeAvailable(string email)
        {
            return !await _repo.CheckIsEmailPresent(email);
        }
    }
}
