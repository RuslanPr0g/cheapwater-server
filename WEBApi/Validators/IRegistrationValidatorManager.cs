using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WEBApi.Validators
{
    public interface IRegistrationValidatorManager
    {
        ValidationResult Validate(CancellationToken token);
    }
}
