using DataAccessLibrary.DB.Entities;

namespace WEBApi.DTOs
{
    public interface IModelConverter
    {
        User ConvertUserFromDTO(UserRegistrationModel model);
        UserInfoModel ConvertUserToInfoModel(User user);
    }
}
