using KikiCourierApp.BLL.Models;

namespace KikiCourierApp.BLL.Interfaces
{
    public interface IDeliveryTimeCalculator
    {
        Dictionary<string, double> CalculateDeliveryTimes(
            List<Shipment> shipments,
            int vehicleCount,
            int speed
        );
    }
}
