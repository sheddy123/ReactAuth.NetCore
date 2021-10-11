using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactAuth.NetCore.Data.Dtos;
using ReactAuth.NetCore.Helpers;
using ReactAuth.NetCore.Models;
using AutoMapper;
using ReactAuth.NetCore.Repository.IRepository;
using ReactAuth.NetCore.Data;

namespace ReactAuth.NetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly UserContext _userContext;

        public AuthController(IUserRepository userRepository, JwtService jwtService, IMapper mapper, UserContext userContext)
        {
            _userRepository = userRepository;
            _userContext = userContext;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterDto userDto)
        {
            try
            {
                var newUser = _mapper.Map<User>(userDto);
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
                _userRepository.Create(newUser,_userContext);

                return !String.IsNullOrEmpty(newUser.ErrorMessage) ? StatusCode(400, new { Message = newUser.ErrorMessage })
                    : StatusCode(201, new RegisterResponse { Email = newUser.Email, Username = newUser.UserName, Message="User successfully created" });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = _userRepository.GetByEmail(loginDto.Email,_userContext);

                if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                    return StatusCode(500, new { message = "Invalid Credentials" });

                var jwtString = _jwtService.Generate(user.Id);
                Response.Cookies.Append("jwt", jwtString, new CookieOptions { HttpOnly = true });

                return Ok(new { username=user.UserName, message = "Successfully logged in." });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            try
            {
                var jwtString = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwtString);
                int userId = int.Parse(token.Issuer);
                var user = _userRepository.GetById(userId,_userContext);
                return Ok(user);
            }catch(Exception ex)
            {
                return Unauthorized("User not logged in");
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("jwt");
                return Ok(new
                {
                    message = "success"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
