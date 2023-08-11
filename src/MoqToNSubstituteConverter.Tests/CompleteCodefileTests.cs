namespace MoqToNSubstituteConverter.Tests;

[TestClass]
public class CompleteCodefileTests
{
    [TestMethod]
    public void UsingTest()
    {
        //Arrange
        Conversion conversion = new Conversion();
        string code = @"
using Moq;
";

        //Act
        ConversionResponse gitHubOutput = conversion.ConvertMoqToNSubstitute(code);

        //Assert
        string expected = @"
using NSubstitute;
";

        Assert.AreEqual(expected, gitHubOutput.ConvertedCode);

    }
}