using DataAccessLibrary.DB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi.Controllers
{
    [ApiController]
    public class authController: ControllerBase
    {
        [Route("api/auth/register")]
        [HttpPost]
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
        [Route("api/auth/login")]
        [HttpPost]
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
