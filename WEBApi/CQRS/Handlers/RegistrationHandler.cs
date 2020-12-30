using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Entities;
using MediatR;
using WEBApi.Authentication;
using WEBApi.CQRS.Actions.Commands;
using WEBApi.DTOs;
using WEBApi.Validators;

namespace WEBApi.CQRS.Handlers
{
    public class RegistrationHandler : IRequestHandler<RegistrationCommand, string>
    {
        private readonly IModelConverter _converter;
        private readonly IUserAddRepo _repo;
        private readonly IJWTokenManager _manager;

        public RegistrationHandler(IModelConverter converter, IUserAddRepo repo, IJWTokenManager manager)
        {
            this._converter = converter;
            this._repo = repo;
            this._manager = manager;
        }
        public async Task<string> Handle(RegistrationCommand request, CancellationToken cancellation)
        {


            User user = _converter.ConvertUserFromDTO(request.DTO);

            await _repo.InsertUserIntoTheDb(user);

            var token = await _manager.Authorize(user.Email, request.DTO.Password, cancellation);

            return token;

        }
    }
}
