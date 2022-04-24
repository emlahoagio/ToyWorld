using Contracts;
using Entities.RequestFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using ToyWorldSystem.Controller;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 2, 2, "Not equal");
        }

        [TestMethod]
        public void CreateAccount()
        {
            var account = new NewAccountParameters
            {
                Name = "quan",  
                Email = "abc@gmail.com",
                Password = "123456",
            };
            
            var accountController = new AccountController(null, null, null, null);
            Assert.IsNotNull(accountController.CreateNewAccountSystem(account));

        }
    }
}
