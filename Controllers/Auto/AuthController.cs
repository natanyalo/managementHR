using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using managementHR.Data;
using managementHR.DTOs.User;
using managementHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace managementHR.Controllers.Auto
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Register(UserForRegisterDTO userRegisterDto)
        {

            userRegisterDto.UserName = userRegisterDto.UserName.ToLower();
            if (await _repo.UserExists(userRegisterDto.UserName))
                return BadRequest("user already exists");
            var userTocreate = new User { UserName = userRegisterDto.UserName };
            var createdUser = await _repo.Register(userTocreate, userRegisterDto.Password);
            if (createdUser == null)
                Unauthorized();
            return Ok(new { token = CreateToken(createdUser) });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            var user = await _repo.Login(userForLoginDTO.UserName.ToLower(), userForLoginDTO.Password);
            if (user == null)
                Unauthorized();
            return Ok(new { token = CreateToken(user) });
        }
        private string CreateToken(User user)
        {
            var claims = new[]
{
                new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString()),
                new Claim(ClaimTypes.Name ,user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }

}