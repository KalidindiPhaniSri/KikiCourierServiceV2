using KikiCourierApp.BLL.Models;
using KikiCourierApp.BLL.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace KikiCourierApp.Tests.BLL.Services
{
    public class DeliveryTimeCalculatorTests
    {
        private DeliveryTimeCalculator CreateCalculator()
        {
            return new DeliveryTimeCalculator(NullLogger<DeliveryTimeCalculator>.Instance);
        }

        [Fact]
        public void CalculateDeliveryTime_SingleShipment_ReturnsCorrectTime()
        {
            DeliveryTimeCalculator deliveryTimeCalculator = CreateCalculator();
            List<Shipment> shipments =
            [
                new Shipment
                {
                    Packages = [new Package("PKG1", 10, 100, "")],
                    TotalWeight = 10,
                    MaxDistance = 100
                }
            ];
            var result = deliveryTimeCalculator.CalculateDeliveryTimes(
                shipments,
                vehicleCount: 1,
                speed: 50
            );
            Assert.Single(result);
            Assert.Equal(2.0, result["PKG1"]);
        }

        [Fact]
        public void CalculateDeliveryTime_SingleShipmentsMultiplePackages_ReturnsCorrectTime()
        {
            DeliveryTimeCalculator deliveryTimeCalculator = CreateCalculator();
            List<Shipment> shipments =
            [
                new Shipment{
                     Packages = [  new Package("PKG1", 10, 100, ""),
                        new Package("PKG2", 5, 50, "")],
                    TotalWeight = 15,
                    MaxDistance = 100
                }
            ];
            var result = deliveryTimeCalculator.CalculateDeliveryTimes(
                shipments,
                vehicleCount: 1,
                speed: 50
            );

            //first shipment, start time is zero so time is 100/50 = 2
            Assert.Equal(2.0, result["PKG1"]);
            //second shipment,start time is zero so time is 50/50 = 1
            Assert.Equal(1.0, result["PKG2"]);
        }

        [Fact]
        public void CalculateDeliveryTime_MultipleShipments_ReturnsCorrectTime()
        {
            DeliveryTimeCalculator deliveryTimeCalculator = CreateCalculator();
            List<Shipment> shipments =
            [
                new Shipment{
                     Packages = [new Package("PKG1", 10, 50, "")],
                    TotalWeight = 10,
                    MaxDistance = 50
                },
                new Shipment{
                    Packages=[new Package("PKG1", 10, 100, "")],
                     TotalWeight = 10,
                    MaxDistance = 100
                }
            ];
            var result = deliveryTimeCalculator.CalculateDeliveryTimes(
                shipments,
                vehicleCount: 1,
                speed: 50
            );

            //first shipment, start time is zero so time is 50/50 = 1
            Assert.Equal(1.0, result["PKG2"]);
            //second shipment, start time is 1+1=2, so time =2+100/50=3
            Assert.Equal(3.0, result["PKG2"]);
        }

        [Fact]
        public void CalculateDeliveryTime_MultipleShipmentsMultipleVehicles_ReturnsCorrectTime()
        {
            DeliveryTimeCalculator deliveryTimeCalculator = CreateCalculator();
            List<Shipment> shipments =
            [
                new Shipment{
                     Packages = [new Package("PKG1", 10, 50, "")],
                    TotalWeight = 10,
                    MaxDistance = 50
                },
                new Shipment{
                    Packages=[new Package("PKG1", 10, 100, "")],
                     TotalWeight = 10,
                    MaxDistance = 100
                }
            ];
            var result = deliveryTimeCalculator.CalculateDeliveryTimes(
                shipments,
                vehicleCount: 2,
                speed: 50
            );

            //first shipment, start time is zero so time is 50/50 = 1
            Assert.Equal(1.0, result["PKG2"]);
            //second shipment, start time is zero, so time is 100/50= 2
            Assert.Equal(2.0, result["PKG2"]);
        }
    }
}
