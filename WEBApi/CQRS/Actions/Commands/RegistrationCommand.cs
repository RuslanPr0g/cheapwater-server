using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.DTOs;

namespace WEBApi.CQRS.Actions.Commands
{
    public class RegistrationCommand:IRequest<string>
    {
        public UserRegistrationModel DTO { get; }
        public RegistrationCommand(UserRegistrationModel model)
        {
            DTO = model;
        }
    }
}
