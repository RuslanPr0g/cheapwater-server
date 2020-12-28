using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Entities;
using MediatR;
using WEBApi.CQRS.Actions.Queries;
using WEBApi.DTOs;

namespace WEBApi.CQRS.Handlers
{
    public class PersonalDataHandler : IRequestHandler<PersonalDataQuery, UserInfoModel>
    {
        private readonly IUserReadRepository _repo;
        private readonly IModelConverter _converter;

        public PersonalDataHandler(IUserReadRepository repo, IModelConverter converter)
        {
            this._repo = repo;
            this._converter = converter;
        }
        public async Task<UserInfoModel> Handle(PersonalDataQuery request, CancellationToken cancellation)
        {
            string id = request.Id;

            User user = await _repo.FindUserByIdAsync(id, cancellation);
            cancellation.ThrowIfCancellationRequested();

            UserInfoModel userInfo = _converter.ConvertUserToInfoModel(user);

            return userInfo;
        }
    }
}
