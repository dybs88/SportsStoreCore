using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsStore.Controllers.Base;
using SportsStore.Domain;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Models.Cart;
using SportsStore.Models.Identity;

namespace SportsStore.ApiControllers
{
    [Produces("application/json")]
    [Route("api/accounts")]
    [Authorize]
    public class AccountController : BaseController
    {
        private UserManager<SportUser> _userManager;
        private SignInManager<SportUser> _signInManager;
        private Cart _cart;

        public AccountController(IServiceProvider provider, IConfiguration config, UserManager<SportUser> userManager, SignInManager<SportUser> signInManager, Cart cart)
            : base(provider, config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cart = cart;
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string login, string password)
        {
            var user = _userManager.FindByIdAsync(login);

            if (user == null)
                return BadRequest(new { message = "Niepoprawny login lub hasło" });

            var tokenHandler = new JwtSecurityTokenHandler();


            return null;
        }
    }
}