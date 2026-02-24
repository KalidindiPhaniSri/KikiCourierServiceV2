using KikiCourierApp.BLL.Models;
using KikiCourierApp.BLL.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace KikiCourierApp.Tests.BLL.Services
{
    public class ShipmentBuilderTests
    {
        private ShipmentBuilder Builder()
        {
            return new ShipmentBuilder(NullLogger<ShipmentBuilder>.Instance);
        }

        [Fact]
        public void BuildShipments_SinglePackage_ReturnsSingleShipment()
        {
            ShipmentBuilder builder = Builder();
            List<Package> packages =  [ new("Pkg1", 5, 10, "") ];
            List<Shipment> shipments = builder.BuildShipments(packages, maxWeight: 20);
            Assert.Single(shipments);
            Assert.Single(shipments[0].Packages);
        }

        [Fact]
        public void BuildShipments_PackagesFitInOneShipment_ReturnsSingleShipment()
        {
            ShipmentBuilder builder = Builder();
            List<Package> packages =  [ new("Pkg1", 5, 10, ""), new("Pkg2", 4, 20, "") ];
            List<Shipment> shipments = builder.BuildShipments(packages, maxWeight: 10);
            Assert.Single(shipments);
            Assert.Equal(9, shipments[0].TotalWeight);
        }

        [Fact]
        public void BuildShipments_PackagesExceedsMaxWeight_ReturnsMultipleShipments()
        {
            ShipmentBuilder builder = Builder();
            List<Package> packages =  [ new("Pkg1", 10, 10, ""), new("Pkg2", 10, 20, "") ];
            List<Shipment> shipments = builder.BuildShipments(packages, maxWeight: 15);
            Assert.Equal(2, shipments.Count);
        }

        [Fact]
        public void BuildShipments_MaxDistance_ShouldBeHighestPackageDistance()
        {
            ShipmentBuilder builder = Builder();
            List<Package> packages =  [ new("Pkg1", 10, 10, ""), new("Pkg2", 10, 15, "") ];
            List<Shipment> shipments = builder.BuildShipments(packages, maxWeight: 25);
            Assert.Single(shipments);
            Assert.Equal(15, shipments[0].MaxDistance);
        }
    }
}
