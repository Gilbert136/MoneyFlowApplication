using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFlowApplication.Controllers;
using System.Linq;

namespace MoneyFlowApplicationTest
{
    [TestClass]
    public class BankStatementControllerTest
    {
        private BankStatementController Controller { get; }

        public BankStatementControllerTest()
        {
            Controller = new BankStatementController();
        }

        [TestMethod]
        public void GetStatementByQueryExternalApiTest()
        {
            var result = Controller.GetStatementByQueryExternalApi().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.State);
            Assert.IsTrue(result.Message.Any());
            Assert.IsTrue(result.Data.Any());
        }
    }
}