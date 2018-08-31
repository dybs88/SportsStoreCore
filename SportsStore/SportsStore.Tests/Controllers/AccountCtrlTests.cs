using Microsoft.AspNetCore.Mvc;
using SportsStore.Controllers;
using SportsStore.Models.Account;
using SportsStore.Models.Cart;
using SportsStore.Models.Identity;
using SportsStore.Tests.FakeIdentity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportsStore.Tests.Controllers
{
    public class AccountCtrlTests //: IClassFixture<IdentityTestServer<Startup>>
    {
        AccountController target; 
        public AccountCtrlTests()
        {
            //target = new AccountController(MockIdentity.FakeUserManager, MockIdentity.FakeSignInManager, new Cart());
        }

        [Fact]
        public async void CheckLogin()
        {
            //arange
            LoginModel model = new LoginModel { Name = "lukasz", Password = "Lukasz1!", IsUserLogged = false, ReturnUrl = "", LoggedUser = new SportUser()};

            //act
            var actionResult = (await target.Login(model)) as ViewResult;

            //assert

        }
    }
}
