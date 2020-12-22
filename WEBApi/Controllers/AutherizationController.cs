using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
                //add user to db

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
