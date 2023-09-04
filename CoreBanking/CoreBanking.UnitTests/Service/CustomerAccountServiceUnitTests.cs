using CoreBanking.Repository.Interfaces;
using CoreBanking.Service;
using Moq;

namespace CoreBanking.UnitTests.Service
{
    public class CustomerAccountServiceUnitTests
    {
        private Mock<ICustomersRepository> _customersRepository;
        private CustomerAccountService _customerAccountService;

        public CustomerAccountServiceUnitTests()
        {
            _customersRepository = new Mock<ICustomersRepository>();
            _customerAccountService = new CustomerAccountService(
                    _customersRepository.Object
                );
        }

        [Fact]
        public void GetCustomerBalanceTest()
        {
            //Arrange
            _customersRepository.Setup(x => x.GetBalance(It.IsAny<int>())).Returns(new Domain.Entities.BankAccounts
            {
                AccountId = 10213,
                Balance = 500.00m,
                CustomerId = "a781e389-a0fc-49f3-a1f2-3437dfd8c37a"
            });

            //Act
            var response = _customerAccountService.GetCustomerBalanceAsync(It.IsAny<int>());
            var result = response.Result;

            //Assert
            Assert.True(result is not null);
        }

        [Fact]
        public void GetFinancialTransactionsTest()
        {
            //Arrange
            _customersRepository.Setup(x => x.GetFinancialTransactions(It.IsAny<int>())).Returns(new List<Domain.Entities.FinancialTransactions>
            {
                new Domain.Entities.FinancialTransactions
                {
                    AccountId = 10213,
                    AssetId = 11113,
                    Date = DateTime.Now,
                    Quantity = 11,
                    TotalValue = 215.56m,
                    TransactionId = 22135,
                    Type = "Buy"
                }
            });

            //Act
            var response = _customerAccountService.GetFinancialTransactionsAsync(It.IsAny<int>());
            var result = response.Result;

            //Assert
            Assert.True(result is not null);
        }
    }
}
