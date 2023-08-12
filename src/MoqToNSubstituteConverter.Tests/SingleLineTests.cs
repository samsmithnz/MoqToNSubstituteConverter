namespace MoqToNSubstituteConverter.Tests
{
    [TestClass]
    public class SingleLineTests
    {
        [TestMethod]
        public void UsingTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
using Moq;";

            //Act
            ConversionResponse gitHubOutput = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
using NSubstitute;
";

            Assert.AreEqual(expected, gitHubOutput.ConvertedCode);
        }

        [TestMethod]
        public void InitializeVariableTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();";

            //Act
            ConversionResponse gitHubOutput = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
IConfiguration mockConfiguration = Substitute.For<IConfiguration>();
";

            Assert.AreEqual(expected, gitHubOutput.ConvertedCode);
        }

        [TestMethod]
        public void ObjectTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
MyStorageTable context = new MyStorageTable(mockConfiguration.Object);";

            //Act
            ConversionResponse gitHubOutput = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
MyStorageTable context = new MyStorageTable(mockConfiguration);
";

            Assert.AreEqual(expected, gitHubOutput.ConvertedCode);
        }

        [TestMethod]
        public void SetupTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
mock.Setup(repo => repo.CheckResult(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));";

            //Act
            ConversionResponse gitHubOutput = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
mock.CheckResult(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(true));
";

            Assert.AreEqual(expected, gitHubOutput.ConvertedCode);
        }
       
    }
}
