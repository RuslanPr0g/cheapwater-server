using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WEBApi.DTOs;
using System.Threading;
using MediatR;
using WEBApi.CQRS.Actions.Queries;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("personal_data")]
        public async Task<ActionResult<UserInfoModel>> GetPersonalData(CancellationToken cancellation)
        {
            try
            {
                string id = GetUserIdOfCurrentRequest();
                if (!String.IsNullOrEmpty(id))
                {
                    PersonalDataQuery query = new(id);
                    UserInfoModel UserInfo = await _mediator.Send(query, cancellation);
                    if(UserInfo is null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(UserInfo);
                    }
                }
                else
                {
                    return BadRequest("Wrong ID");
                }
            }
            catch (TaskCanceledException)
            {
                return BadRequest("Canceled");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
        private string GetUserIdOfCurrentRequest()
        {
            return this.User.Claims.Where(
                x => x.Type.Equals(ClaimTypes.NameIdentifier)
                ).FirstOrDefault().Value;
        }
    }
}
