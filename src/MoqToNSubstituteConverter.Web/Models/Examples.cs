﻿namespace MoqToNSubstituteConverter.Web.Models
{
    public class Examples
    {
        public static string Example1()
        {
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
    }
}