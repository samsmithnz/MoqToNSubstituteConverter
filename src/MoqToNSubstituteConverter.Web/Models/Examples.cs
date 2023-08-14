namespace MoqToNSubstituteConverter.Web.Models
{
    public class Examples
    {
        public static string Example1()
        {
            string code = @"using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class MyUnitTests
    {

        [TestMethod]
        public async Task CheckMyUnitTest()
        {
            //Arrange
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            MyStorageTable context = new(mockConfiguration.Object);
            Mock<IMyStorageTable> mock = new Mock<IMyStorageTable>();
            mock.Setup(repo => repo.CheckResult(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            MyController controller = new MyController(mock.Object);
            string name = """"abc"""";
            string environment = """"def"""";

            //Act
            bool result = await controller.CheckResult(name, environment);

            //Assert
            Assert.IsTrue(result);
        }
    }
}";
            return code;
        }

        //This example uses var instead of explicit types and has multi-line statements
        public static string Example2()
        {
            string code = @"using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class MyUnitTests
    {

        [TestMethod]
        public async Task CheckMyUnitTest()
        {
            //Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            var context = new MyStorageTable(mockConfiguration.Object);
            var mock = new Mock<IMyStorageTable>();
            mock.Setup(repo => repo.
                CheckResult(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            var controller = new MyController(mock.Object);
            var name = """"abc"""";
            var environment = """"def"""";

            //Act
            var result = await controller.CheckResult(name, environment);

            //Assert
            Assert.IsTrue(result);
        }
    }
}";
            return code;
        }

        public static string SimpleExample1()
        {
            string code = @"using Moq;";

            return code;
        }

        public static string SimpleExample2()
        {
            string code = @"Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();";

            return code;
        }

        public static string SimpleExample3()
        {
            string code = @"mock.Setup(repo => repo.CheckResult(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));";

            return code;
        }

        public static string SimpleExample4()
        {
            string code = @"mock.Verify(x => x.Method(), Times.Once);
mock.Verify(_ => _.Transform(It.IsAny<string>()), Times.Exactly(3));";

            return code;
        }

        public static string SimpleExample5()
        {
            string code = @"mock.Invocations.Clear();";

            return code;
        }
    }
}
