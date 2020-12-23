using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using WEBApi.Authentication;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AutherizationController: ControllerBase
    {
        private readonly IJWTokenManager _manager;
        private readonly IUserRepository _repo;

        public AutherizationController(IJWTokenManager manager, IUserRepository repo)
        {
            this._manager = manager;
            this._repo = repo;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody]UserModel user)
        {
            if (user is not null)
            {
                if(IsValidUser(user))
                {
                    await _repo.InsertUserIntoTheDb(user);
                    var token = await _manager.Authorize(user.Email, user.Password);
                    if (token is not null)
                    {
                        return Ok(token);
                    }
                    return Unauthorized();
                }
                else
                {
                    return BadRequest("User is not valid");
                }
                
            }
            else
            {
                return BadRequest("No data was provided");
            }
        }

        private bool IsValidUser(UserModel user)
        {
            return IsValidPassword(user.Password)&&IsValidEmail(user.Email)&&IsValidNickName(user.Nickname);
        }
        private bool IsValidPassword(string password)
        {
            return !String.IsNullOrEmpty(password)&& password.Length > 6;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address.Equals(email);
            }
            catch
            {
                return false;
            }
        }
        private bool IsValidNickName(string nickname)
        {
            return !String.IsNullOrEmpty(nickname) && nickname.Length > 2;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] UserModelBase user)
        {
            if (user is not null)
            {
                var token = await _manager.Authorize(user.Email, user.Password);
                if (token is not null)
                {
                    return Ok(token);
                }
                return Unauthorized();
            }
            else
            {
                return BadRequest();
            }
        }
        //Authorization check
        /*
        [Authorize]
        [HttpGet]
        public ActionResult<string> GetUSCapital()
        {
            return Ok("Washington DC");
        }*/
    }
}
