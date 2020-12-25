using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using WEBApi.Authentication;
using WEBApi.DTOs;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AutherizationController: ControllerBase
    {
        private readonly IJWTokenManager _manager;
        private readonly IUserReadRepository _readrepo;
        private readonly IUserAddRepository _writerepo;
        private readonly IModelConverter _converter;

        public AutherizationController(IJWTokenManager manager, IUserReadRepository readrepo,
            IUserAddRepository writerepo, IModelConverter converter)
        {
            this._manager = manager;
            this._readrepo = readrepo;
            this._writerepo = writerepo;
            this._converter = converter;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody]UserRegistrationModel userDto)
        {
            if (userDto is not null)
            {
                if(IsValidUser(userDto))
                {
                    User user = _converter.ConvertUserFromDTO(userDto);
                    if (!(await _readrepo.CheckIsEmailPresent(user.Email)))
                    {
                        await _writerepo.InsertUserIntoTheDb(user);
                        var token = await _manager.Authorize(user.Email, user.Password);
                        if (token is not null)
                        {
                            return Ok(token);
                        }
                        return Unauthorized();
                    }
                    else
                    {
                        return BadRequest("Email is already taken");
                    }
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
        private bool IsValidUser(UserRegistrationModel user)
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
        public async Task<ActionResult> LoginUser([FromBody] UserLoginModel user)
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
