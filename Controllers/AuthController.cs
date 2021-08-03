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
using ReactAuth.NetCore.Repository.IRepository;

namespace ReactAuth.NetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterDto userDto)
        {
            var newUser = new User()
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
            };
            _userRepository.Create(newUser);

            return !String.IsNullOrEmpty(newUser.ErrorMessage) ? StatusCode(400, new { Message = newUser.ErrorMessage }) 
                : StatusCode(201, new { email = newUser.Email, username = newUser.UserName });
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userRepository.GetByEmail(loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                return StatusCode(500, new { message = "Invalid Credentials" });

            var jwtString = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwtString, new CookieOptions { HttpOnly = true });

            return Ok(new { message = "Successfully logged in."});
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            try
            {
                var jwtString = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwtString);
                int userId = int.Parse(token.Issuer);
                var user = _userRepository.GetById(userId);
                return Ok(user);
            }catch(Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new
            {
                message = "success"
            });
        }
    }
}
