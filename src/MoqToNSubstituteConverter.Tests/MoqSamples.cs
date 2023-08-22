using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Moq;

namespace MoqToNSubstituteConverter.Tests
{
    [TestClass]
    public class MoqSamples
    {

        [TestMethod]
        public void MoqTest()
        {
            //Arrange
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
        }
    }
}
