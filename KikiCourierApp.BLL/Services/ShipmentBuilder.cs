using KikiCourierApp.BLL.Interfaces;
using KikiCourierApp.BLL.Models;
using Microsoft.Extensions.Logging;

namespace KikiCourierApp.BLL.Services
{
    public class ShipmentBuilder : IShipmentBuilder
    {
        private readonly ILogger<ShipmentBuilder> _logger;

        public ShipmentBuilder(ILogger<ShipmentBuilder> logger)
        {
            _logger = logger;
        }

        public List<Shipment> BuildShipments(IReadOnlyList<Package> packages, int maxWeight)
        {
            _logger.LogInformation(
                "Building shipments. PackageCount={PackageCount}, MaxWeight={MaxWeight}",
                packages.Count,
                maxWeight
            );

            // sorting weight in desc
            //  and distance in asc
            var sortedPackages = packages.OrderByDescending(p => p.Weight).ToList();
            List<Shipment> shipments =  [ ];

            //Best fit decreasing algorithm
            foreach (Package package in sortedPackages)
            {
                Shipment? bestFit = null;
                // int minRemaining = int.MaxValue;
                foreach (Shipment ship in shipments)
                {
                    int remaining = maxWeight - ship.TotalWeight;
                    if (remaining > package.Weight)
                    {
                        bestFit = ship;
                        // minRemaining = remaining - package.Weight;
                    }
                }
                if (bestFit != null)
                {
                    bestFit.Packages.Add(package);
                    bestFit.TotalWeight += package.Weight;
                    bestFit.MaxDistance = Math.Max(bestFit.MaxDistance, package.Distance);
                }
                else
                {
                    shipments.Add(
                        new Shipment
                        {
                            Packages =  [ package ],
                            TotalWeight = package.Weight,
                            MaxDistance = package.Distance
                        }
                    );
                }
            }
            _logger.LogInformation(
                "Shipment building completed. TotalShipments={ShipmentCount}",
                shipments.Count
            );
            return shipments;
        }
    }
}
