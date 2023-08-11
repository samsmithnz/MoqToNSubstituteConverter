using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqToNSubstituteConverter.Tests
{
    [TestClass]
    public class UsingTests
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
}
