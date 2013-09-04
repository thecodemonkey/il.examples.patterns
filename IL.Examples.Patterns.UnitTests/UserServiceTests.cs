using IL.Examples.Patterns.Logic;
using IL.Examples.Patterns.Model;
using IL.Examples.Patterns.Model.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;

namespace IL.Examples.Patterns.UnitTests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void AuthenticateUser_WithNameAndPassword_GetAuthenticatedUser() 
        {
            //arrange
            string name = "Howard Joel Wolowitz";
            string password = "hatkeinendoktor";
            IUserRepository userRepository = MockRepository.GenerateStub<IUserRepository>();
            userRepository.Stub(x => x.GetByName(name))
                          .Return(new User { Name = name, Password = password.EncryptMD5() });

            //act
            UserService userService = new UserService(userRepository);
            User authenticatedUser = userService.Authenticate(name, password);

            //assert
            Assert.IsNotNull(authenticatedUser);
            Assert.IsTrue(authenticatedUser.Name == name, "wrong user name!");

            userRepository.AssertWasCalled(x => x.GetByName(name));
        }
    }
}
