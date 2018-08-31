using Microsoft.EntityFrameworkCore;
using Moq;
using SportsStore.Models.Identity;
using SportsStore.Tests.Base;
using System.Threading.Tasks;

namespace SportsStore.Tests.FakeIdentity
{
    public class MockIdentity
    {
        public static FakeUserManager FakeUserManager => GetFakeUserManager();

        public static FakeSignInManager FakeSignInManager => GetFakeSignInManager();

        //public static AppIdentityDbContext IdentityDbContext => GetMockIdentityDbContext();

        //public static IUserStore<SportUser> UserStore => GetMockStoreUser();

        //public static IOptions<IdentityOptions> IdentityOptions => GetMockIdentityOptions();


        private static FakeUserManager GetFakeUserManager()
        {
            Mock<FakeUserManager> mockManager = new Mock<FakeUserManager>();
            mockManager.Setup(x => x.Users).Returns(Repositories.Users);
            mockManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns<string>(userName => 
            {
                var result = Repositories.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                return Task.FromResult(result.Result);
            });

            return mockManager.Object;
        }

        private static FakeSignInManager GetFakeSignInManager()
        {
            Mock<FakeSignInManager> mockSignInManager = new Mock<FakeSignInManager>();
            //mockSignInManager.Setup(x => x.UserManager).Returns(FakeUserManager);
            mockSignInManager.Setup(x => x.SignOutAsync()).CallBase();
            mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<SportUser>(), It.IsAny<string>(), false, false)).CallBase();

            return mockSignInManager.Object;
        }

        //private static FakeIdentityDbContext GetMockIdentityDbContext()
        //{
        //    Mock<FakeIdentityDbContext> mockContext = new Mock<FakeIdentityDbContext>();
        //    mockContext.Setup(x => x.Users).Returns(Repositories.Users);

        //    return mockContext.Object;
        //}

        //private static IUserStore<SportUser> GetMockStoreUser()
        //{
        //    IUserStore<SportUser> store = new UserStore<SportUser>(IdentityDbContext);

        //    return store;
        //}

        //private static IOptions<IdentityOptions> GetMockIdentityOptions()
        //{
        //    IdentityOptions options = new IdentityOptions();

        //    Mock<IOptions<IdentityOptions>> mockOptions = new Mock<IOptions<IdentityOptions>>();
        //    mockOptions.Setup(x => x.Value).Returns(options);

        //    return mockOptions.Object;
        //}
    }
}
