using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.DTOs;

namespace WEBApi.DTOs
{
    public interface IModelConverter
    {
        User ConvertUserFromDTO(UserRegistrationModel model);
    }
}
