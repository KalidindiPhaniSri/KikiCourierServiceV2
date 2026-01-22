using KikiCourierApp.BLL.Interfaces;
using KikiCourierApp.BLL.Models;

namespace KikiCourierApp.BLL.Services
{
    public class PackagePricingService
    {
        private readonly double _basePrice;
        private readonly IReadOnlyList<Package> _packages;
        private readonly IDiscountRules _discountRules;

        public PackagePricingService(
            double basePrice,
            IReadOnlyList<Package> packages,
            IDiscountRules discountRules
        )
        {
            _basePrice = basePrice;
            _packages = packages;
            _discountRules = discountRules;
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
                packageCostResults.Add(
                    new PackageCostResult(package.Id, discountAmount, deliveryCostAfterDiscount)
                );
            }
            return packageCostResults;
        }
    }
}
