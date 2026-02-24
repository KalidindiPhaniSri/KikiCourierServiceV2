using KikiCourierApp.BLL.Interfaces;
using KikiCourierApp.BLL.Models;
using Microsoft.Extensions.Logging;

namespace KikiCourierApp.BLL.Services
{
    public class DeliveryTimeCalculator : IDeliveryTimeCalculator
    {
        private readonly ILogger<DeliveryTimeCalculator> _logger;

        public DeliveryTimeCalculator(ILogger<DeliveryTimeCalculator> logger)
        {
            _logger = logger;
        }

        public Dictionary<string, double> CalculateDeliveryTimes(
            List<Shipment> shipments,
            int vehicleCount,
            int speed
        )
        {
            var orderedShipments = shipments
                .OrderByDescending(s => s.TotalWeight)
                .ThenBy(s => s.MaxDistance)
                .ToList();
            _logger.LogInformation("Final shipment details:");

            int shipmentIndexx = 1;

            foreach (var shipment in orderedShipments)
            {
                _logger.LogInformation(
                    "Shipment {Index} | TotalWeight={Weight} | MaxDistance={Distance}",
                    shipmentIndexx,
                    shipment.TotalWeight,
                    shipment.MaxDistance
                );

                foreach (var pkg in shipment.Packages)
                {
                    _logger.LogInformation(
                        "  Package {PackageId} | Weight={Weight} | Distance={Distance}",
                        pkg.Id,
                        pkg.Weight,
                        pkg.Distance
                    );
                }

                shipmentIndexx++;
            }

            _logger.LogInformation(
                "Starting delivery time calculation. Shipments={ShipmentCount}, Vehicles={VehicleCount}, Speed={Speed}",
                orderedShipments.Count,
                vehicleCount,
                speed
            );
            var result = new Dictionary<string, double>();

            var vehicleQueue = new PriorityQueue<double, double>();

            for (int i = 0; i < vehicleCount; i++)
                vehicleQueue.Enqueue(0, 0);

            int shipmentIndex = 1;

            foreach (var shipment in orderedShipments)
            {
                double startTime = vehicleQueue.Dequeue();
                _logger.LogInformation(
                    "Shipment {ShipmentIndex} assigned to vehicle available at time {StartTime}",
                    shipmentIndex,
                    startTime
                );

                foreach (var pkg in shipment.Packages)
                {
                    double deliveryTime =
                        Math.Floor((startTime + (double)pkg.Distance / speed) * 100) / 100;
                    result[pkg.Id] = deliveryTime;
                    _logger.LogDebug(
                        "Package {PackageId} will be delivered at {DeliveryTime} hours (Distance={Distance})",
                        pkg.Id,
                        deliveryTime,
                        pkg.Distance
                    );
                }

                double vehicleFreeAt =
                    startTime + Math.Floor(2.0 * shipment.MaxDistance / speed * 100) / 100;
                _logger.LogInformation(
                    "Vehicle will return at {VehicleFreeAt} hours after completing shipment {ShipmentIndex}",
                    vehicleFreeAt,
                    shipmentIndex
                );

                vehicleQueue.Enqueue(vehicleFreeAt, vehicleFreeAt);
            }
            _logger.LogInformation("Completed delivery time calculation");
            return result;
        }
    }
}
