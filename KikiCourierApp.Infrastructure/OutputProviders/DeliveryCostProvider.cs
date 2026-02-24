using KikiCourierApp.BLL.Models;

namespace KikiCourierApp.Infrastructure.OutputProviders
{
    public class DeliveryCostProvider
    {
        private readonly IReadOnlyList<PackageCostResult> _packageCostResults;

        public DeliveryCostProvider(IReadOnlyList<PackageCostResult> packageCostResults)
        {
            _packageCostResults = packageCostResults;
        }

        public void PrintDeliveryCost()
        {
            foreach (PackageCostResult result in _packageCostResults)
            {
                Console.WriteLine(
                    $"{result.PackageId} "
                        + $"{result.DiscountAmount} "
                        + $"{result.DeliveryCost} "
                        + $"{result.DeliveryTime:F2}"
                );
            }
        }
    }
}
