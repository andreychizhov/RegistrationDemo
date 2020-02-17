using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PM.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using PM.Domain.Commands;
using PM.Domain.Queries;
using PM.Domain.Services;
using PM.Infrastructure;
using WebApi.DTO;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings, IMediator mediator)
        {
            _userService = userService;
            _mapper = mapper;
            _mediator = mediator;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest model)
        {
            var command = _mapper.Map<AuthenticateUserCommand>(model);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = "Login or password is incorrect" });

            var tokenString = result.CreateToken(_appSettings.Secret);

            return Ok(new
            {
                Id = result.Id,
                Login = result.Login,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var command = _mapper.Map<RegisterUserCommand>(model);

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _mediator.Send(new AllUsersQuery());
            var model = _mapper.Map<IList<UserResponse>>(users);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _mediator.Send(new UserByIdQuery(id));
            var model = _mapper.Map<UserResponse>(user);
            return Ok(model);
        }
    }
}
