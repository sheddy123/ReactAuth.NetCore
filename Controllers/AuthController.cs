using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactAuth.NetCore.Data.Dtos;
using ReactAuth.NetCore.Models;
using ReactAuth.NetCore.Repository.IRepository;

namespace ReactAuth.NetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterDto userDto)
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
    }
}
