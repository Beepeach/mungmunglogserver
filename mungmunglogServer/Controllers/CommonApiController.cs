using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using mungmunglogServer.Data;

namespace mungmunglogServer.Controllers
{
    public class CommonApiController: ControllerBase
    {
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;
        protected readonly IConfiguration _configuration;

        public CommonApiController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        protected string GetApiToken(User user)
        {
            try
            {
                var claims = new[]
                {
                    // Name에 id가 들어가는게 맞나..?
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtExpiryInDays"]));

                var token = new JwtSecurityToken
                (
                    _configuration["JwtIssuer"],
                    _configuration["JwtAudience"],
                    claims,
                    expires: expiry,
                    signingCredentials: credential
                );

                var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenStr;
            }
            catch
            {
                return "fail";
            }
        }
    }
}