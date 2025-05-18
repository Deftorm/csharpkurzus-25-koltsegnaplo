using NA9ZHD; // vagy ahol a Transaction van

using Xunit;

namespace NA9ZHD.Tests
{
    public class StatisticsTests
    {
        [Fact]
        public void GetTotalIncome_ReturnsCorrectSum()
        {
            var transactions = new List<Transaction>
            {
                new Transaction(DateTime.MinValue, false, TransactionCategory.Salary, 1000, "asdad"),
                new Transaction(DateTime.MinValue, false, TransactionCategory.Salary, 500, "asdad"),
                new Transaction(DateTime.MinValue, true, TransactionCategory.Food, 1000, "asdad"),
            };
            int result = GeneralStatistics.GetTotalIncome(transactions);
            Assert.Equal(1500, result);
        }
    }
}
