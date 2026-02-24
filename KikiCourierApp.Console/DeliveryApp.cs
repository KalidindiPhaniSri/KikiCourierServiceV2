using KikiCourierApp.BLL.Interfaces;
using KikiCourierApp.BLL.Services;
using KikiCourierApp.Infrastructure.OutputProviders;

namespace KikiCourierApp.Console
{
    public class DeliveryApp
    {
        private readonly PackageReader _packageReader;
        private readonly IDiscountRules _discountRules;
        private readonly IShipmentBuilder _shipmentBuilder;
        private readonly DeliveryTimeCalculator _deliveryTimeCalculator;

        public DeliveryApp(
            PackageReader packageReader,
            IDiscountRules discountRules,
            IShipmentBuilder shipmentBuilder,
            DeliveryTimeCalculator deliveryTimeCalculator
        )
        {
            _packageReader = packageReader;
            _discountRules = discountRules;
            _shipmentBuilder = shipmentBuilder;
            _deliveryTimeCalculator = deliveryTimeCalculator;
        }

        public void Run()
        {
            _packageReader.ReadInput();

            var shipments = _shipmentBuilder.BuildShipments(
                _packageReader.Packages,
                _packageReader.MaxWeight
            );

            var deliveryTimes = _deliveryTimeCalculator.CalculateDeliveryTimes(
                shipments,
                _packageReader.VehicleCount,
                _packageReader.Speed
            );

            var pricingService = new PackagePricingService(
                _packageReader.BaseDeliveryPrice,
                _packageReader.Packages,
                _discountRules,
                deliveryTimes
            );
            var printPrincingResults = new DeliveryCostProvider(
                pricingService.GeneratePricingResults()
            );
            printPrincingResults.PrintDeliveryCost();
        }
    }
}
