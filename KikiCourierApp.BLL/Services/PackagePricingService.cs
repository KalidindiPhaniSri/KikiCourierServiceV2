using KikiCourierApp.BLL.Interfaces;
using KikiCourierApp.BLL.Models;

namespace KikiCourierApp.BLL.Services
{
    public class PackagePricingService
    {
        private readonly double _basePrice;
        private readonly IReadOnlyList<Package> _packages;
        private readonly IDiscountRules _discountRules;
        private readonly IReadOnlyDictionary<string, double> _deliveryTimes;

        public PackagePricingService(
            double basePrice,
            IReadOnlyList<Package> packages,
            IDiscountRules discountRules,
            IReadOnlyDictionary<string, double> deliveryTimes
        )
        {
            _basePrice = basePrice;
            _packages = packages;
            _discountRules = discountRules;
            _deliveryTimes = deliveryTimes;
        }

        private double GetDiscountPercentage(Package package)
        {
            DiscountRule? discountRule = _discountRules.GetRule(package.CouponCode);
            return discountRule == null
                ? 0
                : discountRule.CalculateDiscountPercentage(package.Weight, package.Distance);
        }

        public IReadOnlyList<PackageCostResult> GeneratePricingResults()
        {
            List<PackageCostResult> packageCostResults =  [ ];
            foreach (Package package in _packages)
            {
                double deliveryCost = DeliveryCostCalculator.CalculateDeliveryCost(
                    _basePrice,
                    package.Weight,
                    package.Distance
                );
                double discountAmount = deliveryCost * GetDiscountPercentage(package) / 100;
                double deliveryCostAfterDiscount = deliveryCost - discountAmount;
                double deliveryTime = _deliveryTimes.TryGetValue(package.Id, out var time)
                    ? time
                    : 0;
                packageCostResults.Add(
                    new PackageCostResult(
                        package.Id,
                        discountAmount,
                        deliveryCostAfterDiscount,
                        deliveryTime
                    )
                );
            }
            return packageCostResults;
        }
    }
}
