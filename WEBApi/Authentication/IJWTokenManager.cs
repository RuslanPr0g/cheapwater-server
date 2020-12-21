using System.Threading.Tasks;

namespace WEBApi.Authentication
{
    public interface IJWTokenManager
    {
        Task<string> Authorize(string email, string password);
    }
}