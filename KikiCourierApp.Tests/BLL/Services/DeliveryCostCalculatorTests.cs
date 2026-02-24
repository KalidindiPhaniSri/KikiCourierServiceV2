using KikiCourierApp.BLL.Services;

namespace KikiCourierApp.Tests.BLL.Services
{
    public class DeliveryCostCalculatorTests
    {
        [Fact]
        public void CalculateDeliveryCost_ValidInput_ReturnsCorrectOutput()
        {
            double basePrice = 100;
            int weight = 5;
            int distance = 10;
            var result = DeliveryCostCalculator.CalculateDeliveryCost(basePrice, weight, distance);
            Assert.Equal(100 + 5 * 10 + 10 * 5, result);
        }

        [Theory]
        [InlineData(-1, 5, 10)]
        [InlineData(100, -5, 10)]
        [InlineData(100, 5, -10)]
        public void CalculateDeliveryCost_InvalidInput_ReturnsZero(
            double basePrice,
            int weight,
            int distance
        )
        {
            var result = DeliveryCostCalculator.CalculateDeliveryCost(basePrice, weight, distance);
            Assert.Equal(0, result);
        }
    }
}
