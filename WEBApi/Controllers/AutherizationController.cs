using DataAccessLibrary.DB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AutherizationController: ControllerBase
    {
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]UserModel user)
        {
            if (user is not null)
            {
                //do stuff
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] UserModelBase user)
        {
            if (user is not null)
            {
                //do stuff
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
