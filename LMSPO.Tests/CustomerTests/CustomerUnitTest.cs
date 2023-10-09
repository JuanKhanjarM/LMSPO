using LMSPO.CoreBusiness.Entities;
using LMSPO.UseCase.CustomerUC;
using LMSPO.UseCase.Exceptions;
using LMSPO.UseCase.PluginsInterfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace LMSPO.Tests.CustomerTests
{
    public class CustomerUnitTest
    {
        [Fact]
        public async Task ExecuteAsync_ValidCustomerId_ReturnsCustomer()
        {
            // Arrange
            int customerId = 1;
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var loggerMock = new Mock<ILogger<GetCustomerWithGroupsAndProductsUC>>();
            var customer = new Customer { CustomerId = customerId };
            customerRepositoryMock.Setup(repo => repo.GetCustomerWithGroupsAndProductsAsync(customerId))
                                 .ReturnsAsync(customer);
            var getCustomerUC = new GetCustomerWithGroupsAndProductsUC(customerRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await getCustomerUC.ExecuteAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.CustomerId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ExecuteAsync_ManyDataProvided_ValidCustomerId_ReturnsCustomer(int customerId)
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var loggerMock = new Mock<ILogger<GetCustomerWithGroupsAndProductsUC>>();
            var customer = new Customer { CustomerId = customerId };
            customerRepositoryMock.Setup(repo => repo.GetCustomerWithGroupsAndProductsAsync(customerId))
                                 .ReturnsAsync(customer);
            var getCustomerUC = new GetCustomerWithGroupsAndProductsUC(customerRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await getCustomerUC.ExecuteAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.CustomerId);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidCustomerId_ThrowsException()
        {
            // Arrange
            int invalidCustomerId = -1;
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var loggerMock = new Mock<ILogger<GetCustomerWithGroupsAndProductsUC>>();
            var getCustomerUC = new GetCustomerWithGroupsAndProductsUC(customerRepositoryMock.Object, loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidCustomerIdException>(() => getCustomerUC.ExecuteAsync(invalidCustomerId));
        }

        [Fact]
        public async Task ExecuteAsync_CustomerNotFound_ThrowsException()
        {
            // Arrange
            int nonExistentCustomerId = 999;
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var loggerMock = new Mock<ILogger<GetCustomerWithGroupsAndProductsUC>>();
            customerRepositoryMock.Setup(repo => repo.GetCustomerWithGroupsAndProductsAsync(nonExistentCustomerId))
                                 .ReturnsAsync((Customer)null);
            var getCustomerUC = new GetCustomerWithGroupsAndProductsUC(customerRepositoryMock.Object, loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<CustomerNotFoundException>(() => getCustomerUC.ExecuteAsync(nonExistentCustomerId));
        }
    }
}