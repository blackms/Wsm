using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wsm.Contracts.Models;
using Wsm.Repository.MongoDB;

namespace Wsm.UnitTest.DownCast
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Repository<User> repo = new Repository<User>();

            var user = new UserRepository();
            user = (UserRepository)repo;
            Assert.IsNotNull(repo);
        }
    }
}
