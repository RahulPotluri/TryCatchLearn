using Portfolio.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.API.Models.Login;
using System.Threading.Tasks;
using Portfolio.API.Data.DTO.Users;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this._config = config;
            this._repo = repo;
        }

        [HttpGet("getUser")]
        public string GetAuth()
        {
            return "users";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userDto)
        {
            if (userDto == null)
            {
                throw new System.ArgumentNullException(nameof(userDto));
            }

            userDto.Username = userDto.Username.ToLower();
            if (await _repo.UserExists(userDto.Username))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new Users
            {
                Username = userDto.Username
            };

            var usersCreated = await _repo.Register(userToCreate, userDto.Password);
            return Ok(usersCreated);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userLoginDto)
        {
            try
            {
                var userFromRepo = await _repo.Login(userLoginDto.Username.ToLower(), userLoginDto.Password);
                if(userFromRepo == null)
                {
                    return Unauthorized();
                }

                //Claim based Authorization
                var claims = new []
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username)
                };

                //To determine that valid token comes back, server needs to sign this token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSetting:Token").Value));

                //now creating signing credentials and encrypting key
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                
                int numberOfDaysToExpire =  Convert.ToInt32((_config.GetSection("AppSetting:JwtTokenExpiryDays").Value));
                DateTime expiryDate = DateTime.Now.AddDays(numberOfDaysToExpire);
                
                //now creating a token, first is token descriptor
                var tokenDescriptor = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(claims),
                    Expires = expiryDate,
                    SigningCredentials = creds
                };

                //initializing JSon web token handler
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token)});
            }
            catch(Exception ex)
            {
                return BadRequest("Unidentified Exception");
            }
        }
    }
}