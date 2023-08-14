namespace MoqToNSubstituteConverter.Tests;

[TestClass]
public class CompleteCodefileTests
{
    [TestMethod]
    public void CompleteUnitTestFileTest()
    {
        //Arrange
        Conversion conversion = new Conversion();
        string code = @"
using Microsoft.Extensions.Configuration;
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
            string name = ""abc"";
            string environment = ""def"";

            //Act
            bool result = await controller.CheckResult(name, environment);

            //Assert
            Assert.IsTrue(result);
        }
    }
}";

        //Act
        ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);

        //Assert
        string expected = @"
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
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
            IConfiguration mockConfiguration = Substitute.For<IConfiguration>();
            MyStorageTable context = new(mockConfiguration);
            IMyStorageTable mock = Substitute.For<IMyStorageTable>();
            mock.CheckResult(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(true));
            MyController controller = new MyController(mock);
            string name = ""abc"";
            string environment = ""def"";

            //Act
            bool result = await controller.CheckResult(name, environment);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
";
        Assert.AreEqual(code, result.OriginalCode);
        Assert.AreEqual(expected, result.ConvertedCode);

    }
    [TestMethod]
    public void CompleteUnitTest2FileTest()
    {
        //Arrange
        Conversion conversion = new Conversion();
        string code = @"
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
            var name = ""abc"";
            var environment = ""def"";

            //Act
            var result = await controller.CheckResult(name, environment);

            //Assert
            Assert.IsTrue(result);
        }
    }
}";

        //Act
        ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);

        //Assert
        string expected = @"
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
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
            var mockConfiguration = Substitute.For<IConfiguration>();
            var context = new MyStorageTable(mockConfiguration);
            var mock = Substitute.For<IMyStorageTable>();
            mock.
                CheckResult(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(true));
            var controller = new MyController(mock);
            var name = ""abc"";
            var environment = ""def"";

            //Act
            var result = await controller.CheckResult(name, environment);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
";
        Assert.AreEqual(code, result.OriginalCode);
        Assert.AreEqual(expected, result.ConvertedCode);

    }
}