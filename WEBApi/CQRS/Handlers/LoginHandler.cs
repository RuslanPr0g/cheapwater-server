using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WEBApi.Authentication;
using WEBApi.CQRS.Actions.Queries;
using WEBApi.DTOs;

namespace WEBApi.CQRS.Handlers
{
    public class LoginHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IJWTokenManager _manager;

        public LoginHandler(IJWTokenManager manager)
        {
            this._manager = manager;
        }
        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            UserLoginModel user = request.Dto;
            var token = await _manager.Authorize(user.Email, user.Password, cancellationToken);
            return token;

        }
    }
}
