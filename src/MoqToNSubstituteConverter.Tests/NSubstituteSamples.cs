using Castle.Core.Configuration;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace MoqToNSubstituteConverter.Tests
{
    [TestClass]
    public class NSubstituteSamples
    {
        [TestMethod]
        public void NSubstituteTest()
        {
            //Arrange
            IConfiguration mockConfiguration = Substitute.For<IConfiguration>();
            mockConfiguration.ToString().Throws(new ArgumentException());
        }
    }
}
