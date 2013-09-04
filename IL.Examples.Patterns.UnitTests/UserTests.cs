using System;                   
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IL.Examples.Patterns.Model;                           

namespace IL.Examples.Patterns.UnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void IsPasswordEncrypted_YesItIs()
        {
            //arrange
            User user = new User
            {
                Name = "Sheldon Cooper",
                Password = "however".EncryptMD5()
            };

            //act
            var result = user.IsPasswordEncrypted();

            //assert
            Assert.IsTrue(result, "ecryption check failed");
        }

        [TestMethod]
        public void IsPasswordEncrypted_NoItIsnt()
        {
            //arrange
            User user = new User
            {
                Name = "Dr. Rajesh „Raj“ Ramayan Koothrappali",
                Password = "whatever"
            };

            //act
            var result = user.IsPasswordEncrypted();

            //assert
            Assert.IsFalse(result, "ecryption check failed");
        }
    }
}
