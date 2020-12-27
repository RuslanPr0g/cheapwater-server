using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using WEBApi.Authentication;
using WEBApi.DTOs;
using WEBApi.Validators;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AutherizationController : ControllerBase
    {
        private readonly IJWTokenManager _manager;
        private readonly IUserAddRepository _writerepo;
        private readonly IModelConverter _converter;
        private readonly RegistrationValidator _validator;

        public AutherizationController(IJWTokenManager manager, IUserAddRepository writerepo, 
            IModelConverter converter, RegistrationValidator validator)
        {
            this._manager = manager;
            this._writerepo = writerepo;
            this._converter = converter;
            this._validator = validator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegistrationModel userDto)
        {
            var results = await _validator.ValidateAsync(userDto);
            if (!results.IsValid)
            {
                List<string> ErrorMessages = new List<string>();
                foreach (var Error in results.Errors)
                {
                    ErrorMessages.Add(Error.ErrorMessage);
                }
                return BadRequest(ErrorMessages);
            }

            User user = _converter.ConvertUserFromDTO(userDto);

            await _writerepo.InsertUserIntoTheDb(user);

            var token = await _manager.Authorize(user.Email, user.Password);
            if (token is not null)
            {
                return Ok(token);
            }
            return Unauthorized();
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
