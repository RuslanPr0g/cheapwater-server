using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.DTOs;

namespace WEBApi.CQRS.Actions.Queries
{
    public class LoginQuery:IRequest<string>
    {
        public UserLoginModel Dto { get; }
        public LoginQuery(UserLoginModel dto)
        {
            Dto = dto;
        }
    }
}
