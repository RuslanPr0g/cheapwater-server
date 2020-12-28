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
using System.Threading;
using MediatR;
using WEBApi.CQRS.Actions.Commands;
using WEBApi.CQRS.Actions.Queries;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AutherizationController : ControllerBase
    {
        private readonly RegistrationValidator _validator;
        private readonly IMediator _mediator;

        public AutherizationController(IJWTokenManager manager, RegistrationValidator validator, IMediator mediator)
        {
            this._validator = validator;
            this._mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegistrationModel userDto, CancellationToken cancellation)
        {
            try
            {
                var results = await _validator.ValidateAsync(userDto, cancellation);

                if (!results.IsValid)
                {
                    List<string> ErrorMessages = new List<string>();
                    foreach (var Error in results.Errors)
                    {
                        ErrorMessages.Add(Error.ErrorMessage);
                    }
                    return BadRequest(ErrorMessages);
                }

                cancellation.ThrowIfCancellationRequested();

                var command = new RegistrationCommand(userDto);

                string token = await _mediator.Send(command, cancellationToken: cancellation);

                if (token is not null)
                {
                    return Ok(token);
                }

                return Unauthorized();
            }
            catch (TaskCanceledException)
            {
                return BadRequest("Canceled");
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong: " + e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] UserLoginModel user, CancellationToken cancellation)
        {
            try
            {
                cancellation.ThrowIfCancellationRequested();

                if (user is not null)
                {
                    LoginQuery query = new(user);

                    string token = await _mediator.Send(query, cancellation);
                    
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
            catch (TaskCanceledException)
            {
                return BadRequest("Canceled");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
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
