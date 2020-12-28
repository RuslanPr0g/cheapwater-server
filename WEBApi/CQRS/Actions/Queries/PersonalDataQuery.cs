using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WEBApi.DTOs;

namespace WEBApi.CQRS.Actions.Queries
{
    public class PersonalDataQuery:IRequest<UserInfoModel>
    {
        public string Id { get; }
        public PersonalDataQuery(string id)
        {
            this.Id = id;
        }
    }
}
