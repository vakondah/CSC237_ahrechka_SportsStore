using CSC237_ahrechka_SportsStore.DataLayer;
using CSC237_ahrechka_SportsStore.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportsPro.Tests
{
    public class CheckClassTests
    {
        [Fact]
        public void EmailExists_ReturnsAString()
        {
            // Arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Customer>>())).Returns(new Customer());

            //Act
            var result = Check.EmailExists(rep.Object, "fake@email.com");

            //Assert
            Assert.IsType<string>(result);
        }
        [Fact]
        public void EmailExists_ReturnsAnEmptyStringIfEmailIsNew()
        {
            // Arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Customer>>()));
            int expectedLength = 0;

            //Act
            var result = Check.EmailExists(rep.Object, "fake@email.com");

            // Arrange
            Assert.Equal(expectedLength, result.Length);
        }

        [Fact]
        public void EmailExists_ReturnsAnEmptyStringIfEmailIsMissing()
        {
            // Arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Customer>>())).Returns(new Customer());
            int expectedLength = 0;

            //Act
            var result = Check.EmailExists(rep.Object, "fake@email.com");

            // Arrange
            Assert.Equal(expectedLength, result.Length);

        }

        [Fact]
        public void EmailExists_ReturnsAnErrorMessageIfEmailExists()
        {
            // Arrange
            var rep = new Mock<IRepository<Customer>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Customer>>())).Returns(new Customer());
            bool isGreaterThanZero = true;

            //Act
            var result = Check.EmailExists(rep.Object, "fake@email.com");

            // Assert
            Assert.Equal(isGreaterThanZero, result.Length > 0);

        }
    }
}
