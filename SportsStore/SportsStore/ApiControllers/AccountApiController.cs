using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportsStore.ApiControllers.Base;
using SportsStore.Domain;
using SportsStore.Helpers;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Models.Cart;
using SportsStore.Models.Identity;

namespace SportsStore.ApiControllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    [Authorize]
    public class AccountApiController : BaseApiController
    {
        private UserManager<SportUser> _userManager;
        private SignInManager<SportUser> _signInManager;
        private Cart _cart;

        public AccountApiController(IOptions<AppSettings> settings, UserManager<SportUser> userManager, SignInManager<SportUser> signInManager, Cart cart)
            : base(settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cart = cart;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]FrontUser user)
        {
            var dbUser = await _userManager.FindByNameAsync(user.UserName);

            if (dbUser == null)
                return BadRequest(new { message = "Niepoprawny login lub hasło" });

            if ((await _signInManager.PasswordSignInAsync(dbUser, user.Password, false, false)).Succeeded)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, dbUser.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { success = true, token = tokenString });
            }
            else
                return Ok(new { success = false });
        }
    }
}