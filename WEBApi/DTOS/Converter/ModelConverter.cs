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
        public User ConvertUserFromDTO(UserRegistrationModel model)
        {
            if (model is not null)
            {
                User output = new User
                {
                    Email = model.Email,
                    Password = model.Password,
                    Username = model.Nickname,
                    Balance =0,
                    Id = Guid.NewGuid().ToString()
                };
                return output;
            }
            else
            {
                return null;
            }
        }

        public UserInfoModel ConvertUserToInfoModel(User user)
        {
            if(user is not null)
            {
                UserInfoModel output = new UserInfoModel
                {
                    Email = user.Email,
                    Username = user.Username,
                    WaterBalance = user.Balance
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
