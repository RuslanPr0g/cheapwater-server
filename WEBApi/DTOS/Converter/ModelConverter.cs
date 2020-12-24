using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi.DTOs;

namespace WEBApi.DTOs
{
    public class ModelConverter : IModelConverter
    {
        public User ConvertUserFromDTO(UserModel model)
        {
            if (model is not null)
            {
                User output = new User
                {
                    Email = model.Email,
                    Password = model.Password,
                    Username = model.Nickname
                };
                return output;
            }
            else
            {
                return null;
            }
        }
    }
}
