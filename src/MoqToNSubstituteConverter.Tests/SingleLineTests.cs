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
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
using NSubstitute;
";
            Assert.AreEqual(expected, result.ConvertedCode);
        }

        [TestMethod]
        public void InitializeVariableTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();";

            //Act
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
IConfiguration mockConfiguration = Substitute.For<IConfiguration>();
";
            Assert.AreEqual(expected, result.ConvertedCode);
        }

        [TestMethod]
        public void ObjectTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
MyStorageTable context = new MyStorageTable(mockConfiguration.Object);";

            //Act
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
MyStorageTable context = new MyStorageTable(mockConfiguration);
";
            Assert.AreEqual(expected, result.ConvertedCode);
        }

        [TestMethod]
        public void SetupTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
mock.Setup(repo => repo.CheckResult(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));";

            //Act
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
mock.CheckResult(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(true));
";
            Assert.AreEqual(expected, result.ConvertedCode);
        }

        [TestMethod]
        public void SetupWithAsyncReturnTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
mock.Setup(repo => repo.CheckResult(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(Task.FromResult(true));";

            //Act
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
mock.CheckResult(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(true));
";
            Assert.AreEqual(expected, result.ConvertedCode);
        }

        [TestMethod]
        public void VerifyTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
mock.Verify(x => x.Method(), Times.Once);";

            //Act
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
mock.Received().Method();
";
            Assert.AreEqual(expected, result.ConvertedCode);
        }

        [TestMethod]
        public void VerifyWithMoreTimesTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
mock.Verify(_ => _.Transform(It.IsAny<string>()), Times.Exactly(3));";

            //Act
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
mock.Received(3).Transform(Arg.Any<string>());
";
            Assert.AreEqual(expected, result.ConvertedCode);
        }

        [TestMethod]
        public void InvocationsClearTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
mock.Invocations.Clear();";

            //Act
            ConversionResponse result = conversion.ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
mock.ClearReceivedCalls();
";
            Assert.AreEqual(expected, result.ConvertedCode);
        }

        [TestMethod]
        public void SingleLineOnMultipleLinesTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
mock
    .Setup(repo => repo.CheckResult(
        It.IsAny<string>(), 
        It.IsAny<string>()))
    .Returns(Task.FromResult(true));";

            //Act
            ConversionResponse result = conversion.
                ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
mock
    .CheckResult(
        Arg.Any<string>(), 
        Arg.Any<string>())
    .Returns(Task.FromResult(true));
";
            Assert.AreEqual(expected, result.ConvertedCode);
        }

    }
}
