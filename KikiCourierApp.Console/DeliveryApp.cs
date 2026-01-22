using KikiCourierApp.BLL.Interfaces;
using KikiCourierApp.BLL.Services;
using KikiCourierApp.Infrastructure.InputProviders.DiscountInputProviders;
using KikiCourierApp.Infrastructure.OutputProviders;

namespace KikiCourierApp.Console
{
    public class DeliveryApp
    {
        private readonly PackageReader _packageReader;
        private readonly IDiscountRules _discountRules;

        public DeliveryApp(PackageReader packageReader, IDiscountRules discountRules)
        {
            _packageReader = packageReader;
            _discountRules = discountRules;
        }

        public void Run()
        {
            _packageReader.ReadInput();

            var pricingService = new PackagePricingService(
                _packageReader.BaseDeliveryPrice,
                _packageReader.Packages,
                _discountRules
            );
            var printPrincingResults = new DeliveryCostProvider(
                pricingService.GeneratePricingResults()
            );
            printPrincingResults.PrintDeliveryCost();
        }
    }
}
