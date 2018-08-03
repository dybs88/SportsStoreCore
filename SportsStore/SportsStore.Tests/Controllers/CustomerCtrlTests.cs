﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using SportsStore.Controllers;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.Identity;
using SportsStore.Tests.FakeIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsStore.Tests.Controllers
{
    public class CustomerCtrlTests
    {
        CustomerController target;
        private SignInManager<SportUser> signInManager;
        public CustomerCtrlTests()
        {
            //target = new CustomerController(Repositories.CustomerRepository, Repositories.AddressRepository, MockIdentity.FakeUserManager);
        }
    }
}