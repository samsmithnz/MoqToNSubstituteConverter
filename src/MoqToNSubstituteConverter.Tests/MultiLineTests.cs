namespace MoqToNSubstituteConverter.Tests
{
    [TestClass]
    public class MultiLineTests
    {
       
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

        [TestMethod]
        public void CallbackTest()
        {
            //Arrange
            Conversion conversion = new();
            string code = @"
int? capturedArg = null;

mock
  .Setup(x => x.MyMethod(It.IsAny<int>(), It.IsAny<string>()))
  .Returns(true)
  .Callback((int i, string s) => capturedArg = i);";

            //Act
            ConversionResponse result = conversion.
                ConvertMoqToNSubstitute(code);

            //Assert
            string expected = @"
int? capturedArg = null;

mock
  .MyMethod(Arg.Do<int>(i => capturedArg = i), Arg.Any<string>())
  .Returns(true);
";
            Assert.AreEqual(expected, result.ConvertedCode);
        }

    }
}
